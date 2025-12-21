using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using KemonoScraperGUI.Models;

namespace KemonoScraperGUI.Services;

/// <summary>
/// データの保存・読込を担当するサービス
/// </summary>
public class DataService
{
    private static readonly string AppDataFolder = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "KemonoScraperGUI"
    );

    private static readonly string ArtistsFilePath = Path.Combine(AppDataFolder, "artists.json");
    private static readonly string SettingsFilePath = Path.Combine(AppDataFolder, "settings.json");

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    };

    /// <summary>
    /// DataServiceのコンストラクタ
    /// </summary>
    public DataService()
    {
        // アプリデータフォルダが存在しない場合は作成
        if (!Directory.Exists(AppDataFolder))
        {
            Directory.CreateDirectory(AppDataFolder);
        }
    }

    /// <summary>
    /// アーティストリストを読み込む
    /// </summary>
    public List<Artist> LoadArtists()
    {
        try
        {
            if (File.Exists(ArtistsFilePath))
            {
                var json = File.ReadAllText(ArtistsFilePath);
                var artists = JsonSerializer.Deserialize<List<Artist>>(json, JsonOptions);
                return artists ?? new List<Artist>();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"アーティストリスト読み込みエラー: {ex.Message}");
        }
        return new List<Artist>();
    }

    /// <summary>
    /// アーティストリストを保存する
    /// </summary>
    public void SaveArtists(List<Artist> artists)
    {
        try
        {
            var json = JsonSerializer.Serialize(artists, JsonOptions);
            File.WriteAllText(ArtistsFilePath, json);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"アーティストリスト保存エラー: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// 設定を読み込む
    /// </summary>
    public AppSettings LoadSettings()
    {
        try
        {
            if (File.Exists(SettingsFilePath))
            {
                var json = File.ReadAllText(SettingsFilePath);
                var settings = JsonSerializer.Deserialize<AppSettings>(json, JsonOptions);
                return settings ?? new AppSettings();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"設定読み込みエラー: {ex.Message}");
        }
        return new AppSettings();
    }

    /// <summary>
    /// 設定を保存する
    /// </summary>
    public void SaveSettings(AppSettings settings)
    {
        try
        {
            var json = JsonSerializer.Serialize(settings, JsonOptions);
            File.WriteAllText(SettingsFilePath, json);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"設定保存エラー: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// batch_exec.batファイルからアーティストリストをインポート
    /// </summary>
    public List<Artist> ImportFromBatchFile(string filePath)
    {
        var artists = new List<Artist>();

        try
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                // "call exec.bat patreon 98831372" の形式をパース
                var trimmed = line.Trim();
                if (trimmed.StartsWith("call exec.bat ", StringComparison.OrdinalIgnoreCase))
                {
                    var parts = trimmed.Substring("call exec.bat ".Length).Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 2)
                    {
                        var artist = new Artist
                        {
                            Service = parts[0].ToLower(),
                            CreatorId = parts[1]
                        };
                        artists.Add(artist);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"バッチファイルインポートエラー: {ex.Message}");
            throw;
        }

        return artists;
    }

    /// <summary>
    /// kemono URLからサービス名とクリエイターIDを抽出
    /// </summary>
    public (string service, string creatorId)? ParseKemonoUrl(string url)
    {
        try
        {
            // https://kemono.cr/patreon/user/12345678 の形式
            // https://kemono.su/fanbox/user/12345678 の形式
            // https://coomer.su/onlyfans/user/12345678 の形式
            var uri = new Uri(url);
            var segments = uri.AbsolutePath.Trim('/').Split('/');

            if (segments.Length >= 3 && segments[1].Equals("user", StringComparison.OrdinalIgnoreCase))
            {
                return (segments[0].ToLower(), segments[2]);
            }
        }
        catch
        {
            // URL解析エラーは無視
        }

        return null;
    }

    /// <summary>
    /// サービス名とクリエイターIDの文字列をパース
    /// </summary>
    public (string service, string creatorId)? ParseServiceCreatorId(string input)
    {
        // "patreon:12345678" または "patreon 12345678" の形式
        var parts = input.Split(new[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length >= 2)
        {
            return (parts[0].ToLower(), parts[1]);
        }
        return null;
    }

    /// <summary>
    /// アプリデータフォルダのパスを取得
    /// </summary>
    public string GetAppDataFolder() => AppDataFolder;
}
