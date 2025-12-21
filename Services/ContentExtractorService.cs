using System.Text;
using System.Text.RegularExpressions;
using KemonoScraperGUI.Models;

namespace KemonoScraperGUI.Services;

/// <summary>
/// content.txt/htmlからURLとパスワードを抽出するサービス
/// </summary>
public class ContentExtractorService
{
    // 静的コンストラクタでエンコーディングプロバイダを一度だけ登録
    static ContentExtractorService()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

    // 抽出対象の外部ストレージドメイン
    private static readonly Dictionary<string, string> TargetDomains = new(StringComparer.OrdinalIgnoreCase)
    {
        // Mega
        { "mega.nz", "Mega" },
        { "mega.co.nz", "Mega" },
        
        // Google Drive
        { "drive.google.com", "Google Drive" },
        { "docs.google.com", "Google Drive" },
        
        // Dropbox
        { "dropbox.com", "Dropbox" },
        { "dl.dropboxusercontent.com", "Dropbox" },
        
        // OneDrive
        { "onedrive.live.com", "OneDrive" },
        { "1drv.ms", "OneDrive" },
        
        // MediaFire
        { "mediafire.com", "MediaFire" },
        
        // Iwara
        { "iwara.zip", "Iwara.zip" },
        { "iwara.tv", "Iwara" },
        
        // Gofile
        { "gofile.io", "Gofile" },
        
        // Pixeldrain
        { "pixeldrain.com", "Pixeldrain" },
        
        // Catbox
        { "catbox.moe", "Catbox" },
        { "files.catbox.moe", "Catbox" },
        
        // Litterbox
        { "litter.catbox.moe", "Litterbox" },
        
        // Anonfiles系
        { "anonfiles.com", "Anonfiles" },
        { "bayfiles.com", "Bayfiles" },
        
        // Workupload
        { "workupload.com", "Workupload" },
        
        // Uploadhaven
        { "uploadhaven.com", "Uploadhaven" },
        
        // Send.cm
        { "send.cm", "Send.cm" },
        
        // Bowfile
        { "bowfile.com", "Bowfile" },
        
        // Buzzheavier
        { "buzzheavier.com", "Buzzheavier" },
        
        // Bunkr
        { "bunkr.si", "Bunkr" },
        { "bunkr.is", "Bunkr" },
        { "bunkrr.su", "Bunkr" },
        
        // Cyberdrop
        { "cyberdrop.me", "Cyberdrop" },
        
        // その他（一般的なファイル共有）
        { "sendspace.com", "Sendspace" },
        { "zippyshare.com", "Zippyshare" },
        { "krakenfiles.com", "Krakenfiles" },
    };

    // URLを抽出する正規表現
    private static readonly Regex UrlRegex = new(
        @"https?://[^\s<>\""'\)\]\}]+",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    // HTMLタグ除去用正規表現
    private static readonly Regex HtmlTagRegex = new(
        @"<[^>]+>",
        RegexOptions.Compiled
    );

    // パス解析用正規表現
    private static readonly Regex ServiceCreatorRegex = new(
        @"^\[(\w+)\]\s*(.+)$",
        RegexOptions.Compiled
    );

    private static readonly Regex PostTitleRegex = new(
        @"^\[\d+\]\s*\[\d+\]\s*(.+)$",
        RegexOptions.Compiled
    );

    // パスワードを抽出する正規表現パターン
    private static readonly Regex[] PasswordPatterns = new[]
    {
        // 日本語パターン
        new Regex(@"パスワード[：:\s]*[「『]?([^\s「」『』\<\>\r\n]{1,50})[」』]?", RegexOptions.Compiled),
        new Regex(@"パス[：:\s]+[「『]?([^\s「」『』\<\>\r\n]{1,50})[」』]?", RegexOptions.Compiled),
        new Regex(@"Pass[：:\s]+[「『]?([^\s「」『』\<\>\r\n]{1,50})[」』]?", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex(@"PW[：:\s]+[「『]?([^\s「」『』\<\>\r\n]{1,50})[」』]?", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex(@"解凍パス[：:\s]*[「『]?([^\s「」『』\<\>\r\n]{1,50})[」』]?", RegexOptions.Compiled),
        
        // 英語パターン
        new Regex(@"password[：:\s]+[""']?([^\s\<\>""'\r\n]{1,50})[""']?", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex(@"pwd[：:\s]+[""']?([^\s\<\>""'\r\n]{1,50})[""']?", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex(@"key[：:\s]+[""']?([^\s\<\>""'\r\n]{1,50})[""']?", RegexOptions.Compiled | RegexOptions.IgnoreCase),
        new Regex(@"decrypt(?:ion)?[：:\s]+[""']?([^\s\<\>""'\r\n]{1,50})[""']?", RegexOptions.Compiled | RegexOptions.IgnoreCase),
    };

    /// <summary>
    /// 進捗報告用イベント
    /// </summary>
    public event Action<string>? ProgressChanged;

    /// <summary>
    /// 指定フォルダからURLを抽出
    /// </summary>
    public List<ExtractedUrl> ExtractUrls(string rootFolder, bool externalStorageOnly = true)
    {
        var results = new System.Collections.Concurrent.ConcurrentBag<ExtractedUrl>();

        if (!Directory.Exists(rootFolder))
        {
            return results.ToList();
        }

        // content.txt と content.html を検索
        var contentFiles = Directory.GetFiles(rootFolder, "content.*", SearchOption.AllDirectories)
            .Where(f => f.EndsWith(".txt", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".html", StringComparison.OrdinalIgnoreCase))
            .ToList();

        ProgressChanged?.Invoke($"検索対象ファイル数: {contentFiles.Count}");

        int processed = 0;
        var lockObj = new object();

        // 並列処理で高速化
        Parallel.ForEach(contentFiles, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, file =>
        {
            try
            {
                var extracted = ExtractFromFile(file, rootFolder, externalStorageOnly);
                foreach (var item in extracted)
                {
                    results.Add(item);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ファイル処理エラー: {file} - {ex.Message}");
            }

            // 進捗報告
            lock (lockObj)
            {
                processed++;
                if (processed % 100 == 0)
                {
                    ProgressChanged?.Invoke($"処理中: {processed}/{contentFiles.Count}");
                }
            }
        });

        ProgressChanged?.Invoke($"完了: {results.Count}件のURLを抽出");

        // サービス→クリエイター→投稿タイトル順でソート
        return results.OrderBy(x => x.Service)
                      .ThenBy(x => x.Creator)
                      .ThenBy(x => x.PostTitle)
                      .ToList();
    }

    /// <summary>
    /// 単一ファイルからURLを抽出
    /// </summary>
    private List<ExtractedUrl> ExtractFromFile(string filePath, string rootFolder, bool externalStorageOnly)
    {
        var results = new List<ExtractedUrl>();

        // ファイル内容を読み込み
        string content;
        try
        {
            content = File.ReadAllText(filePath, Encoding.UTF8);
        }
        catch
        {
            // UTF-8で読めない場合はShift-JISを試す
            try
            {
                content = File.ReadAllText(filePath, Encoding.GetEncoding("Shift_JIS"));
            }
            catch
            {
                return results;
            }
        }

        // HTMLタグを除去（簡易的に）
        var textContent = StripHtmlTags(content);

        // パスワードを先に抽出
        var passwords = ExtractPasswords(textContent);

        // URLを抽出
        var urls = UrlRegex.Matches(content)
            .Select(m => CleanUrl(m.Value))
            .Distinct()
            .ToList();

        // パス情報を取得
        var pathInfo = ParsePathInfo(filePath, rootFolder);

        foreach (var url in urls)
        {
            var urlType = GetUrlType(url);

            // 外部ストレージのみモードの場合、対象外はスキップ
            if (externalStorageOnly && string.IsNullOrEmpty(urlType))
            {
                continue;
            }

            // kemono/coomer自体のURLはスキップ
            if (url.Contains("kemono.") || url.Contains("coomer."))
            {
                continue;
            }

            var extracted = new ExtractedUrl
            {
                Service = pathInfo.service,
                Creator = pathInfo.creator,
                PostTitle = pathInfo.post,
                Url = url,
                UrlType = urlType ?? "Other",
                Password = passwords.FirstOrDefault() ?? "",
                SourceFile = filePath
            };

            results.Add(extracted);
        }

        return results;
    }

    /// <summary>
    /// URLをクリーンアップ（末尾の不要文字を除去）
    /// </summary>
    private string CleanUrl(string url)
    {
        // 末尾の句読点や括弧を除去
        return url.TrimEnd('.', ',', ')', ']', '}', '>', '"', '\'', '。', '、', '）', '」', '』');
    }

    /// <summary>
    /// URLの種類を判定
    /// </summary>
    private string? GetUrlType(string url)
    {
        try
        {
            var uri = new Uri(url);
            var host = uri.Host.ToLower();

            // www. を除去
            if (host.StartsWith("www."))
            {
                host = host.Substring(4);
            }

            // 完全一致
            if (TargetDomains.TryGetValue(host, out var type))
            {
                return type;
            }

            // サブドメインを含む場合
            foreach (var domain in TargetDomains)
            {
                if (host.EndsWith("." + domain.Key) || host == domain.Key)
                {
                    return domain.Value;
                }
            }
        }
        catch
        {
            // URL解析エラー
        }

        return null;
    }

    /// <summary>
    /// パスワードを抽出
    /// </summary>
    private List<string> ExtractPasswords(string content)
    {
        var passwords = new List<string>();

        foreach (var pattern in PasswordPatterns)
        {
            var matches = pattern.Matches(content);
            foreach (Match match in matches)
            {
                if (match.Groups.Count > 1)
                {
                    var pw = match.Groups[1].Value.Trim();
                    if (!string.IsNullOrEmpty(pw) && pw.Length >= 2 && pw.Length <= 50)
                    {
                        // 明らかに違うものを除外
                        if (!pw.Contains("http") && !pw.Contains("<") && !pw.Contains(">"))
                        {
                            passwords.Add(pw);
                        }
                    }
                }
            }
        }

        return passwords.Distinct().ToList();
    }

    /// <summary>
    /// ファイルパスからサービス/クリエイター/投稿情報を抽出
    /// </summary>
    private (string service, string creator, string post) ParsePathInfo(string filePath, string rootFolder)
    {
        try
        {
            var relativePath = Path.GetRelativePath(rootFolder, filePath);
            var parts = relativePath.Split(Path.DirectorySeparatorChar);

            // 期待するパス構造: [service] Creator/[date] [id] PostTitle/content.txt
            if (parts.Length >= 3)
            {
                var creatorFolder = parts[0];
                var postFolder = parts[1];

                // [service] Creator からサービスとクリエイター名を抽出
                var serviceMatch = ServiceCreatorRegex.Match(creatorFolder);
                var service = serviceMatch.Success ? serviceMatch.Groups[1].Value : "";
                var creator = serviceMatch.Success ? serviceMatch.Groups[2].Value : creatorFolder;

                // [date] [id] PostTitle から投稿タイトルを抽出
                var postMatch = PostTitleRegex.Match(postFolder);
                var post = postMatch.Success ? postMatch.Groups[1].Value : postFolder;

                return (service, creator, post);
            }
        }
        catch
        {
            // パス解析エラー
        }

        return ("", "", Path.GetDirectoryName(filePath) ?? "");
    }

    /// <summary>
    /// HTMLタグを除去
    /// </summary>
    private string StripHtmlTags(string html)
    {
        // コンパイル済み正規表現でHTMLタグ除去
        var result = HtmlTagRegex.Replace(html, " ");
        result = System.Net.WebUtility.HtmlDecode(result);
        return result;
    }

    /// <summary>
    /// 結果をCSVにエクスポート
    /// </summary>
    public void ExportToCsv(List<ExtractedUrl> urls, string outputPath)
    {
        var sb = new StringBuilder();
        
        // ヘッダー
        sb.AppendLine("Service,Creator,PostTitle,UrlType,Url,Password,SourceFile");

        foreach (var url in urls)
        {
            sb.AppendLine($"\"{Escape(url.Service)}\",\"{Escape(url.Creator)}\",\"{Escape(url.PostTitle)}\",\"{Escape(url.UrlType)}\",\"{Escape(url.Url)}\",\"{Escape(url.Password)}\",\"{Escape(url.SourceFile)}\"");
        }

        File.WriteAllText(outputPath, sb.ToString(), new UTF8Encoding(true));
    }

    private string Escape(string value)
    {
        if (string.IsNullOrEmpty(value)) return "";
        return value.Replace("\"", "\"\"");
    }
}
