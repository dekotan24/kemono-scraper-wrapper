using System.Diagnostics;
using System.Text;
using KemonoScraperGUI.Models;

namespace KemonoScraperGUI.Services;

/// <summary>
/// kemono-scraperの実行結果
/// </summary>
public class ScraperResult
{
    public bool Success { get; set; }
    public string Output { get; set; } = string.Empty;
    public string ErrorOutput { get; set; } = string.Empty;
    public int ExitCode { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan Duration => EndTime - StartTime;
}

/// <summary>
/// kemono-scraperの実行を担当するサービス
/// </summary>
public class ScraperService
{
    private Process? _currentProcess;
    private CancellationTokenSource? _cancellationTokenSource;

    // 進捗バー判定用のコンパイル済み正規表現
    private static readonly System.Text.RegularExpressions.Regex ProgressTimeRegex = new(
        @"^\s*\d+:\d+.*[━╺]",
        System.Text.RegularExpressions.RegexOptions.Compiled
    );

    private static readonly System.Text.RegularExpressions.Regex ProgressPercentRegex = new(
        @"\d+\.\d+%.*\/s",
        System.Text.RegularExpressions.RegexOptions.Compiled
    );

    /// <summary>
    /// 出力があった時に発火するイベント
    /// </summary>
    public event Action<string>? OutputReceived;

    /// <summary>
    /// エラー出力があった時に発火するイベント
    /// </summary>
    public event Action<string>? ErrorReceived;

    /// <summary>
    /// 実行中かどうか
    /// </summary>
    public bool IsRunning => _currentProcess != null && !_currentProcess.HasExited;

    /// <summary>
    /// kemono-scraperを実行する
    /// </summary>
    public async Task<ScraperResult> RunAsync(Artist artist, AppSettings settings, CancellationToken cancellationToken = default)
    {
        var result = new ScraperResult
        {
            StartTime = DateTime.Now
        };

        var outputBuilder = new StringBuilder();
        var errorBuilder = new StringBuilder();

        try
        {
            // コマンドライン引数を構築
            var arguments = BuildArguments(artist, settings);

            OutputReceived?.Invoke($"[{DateTime.Now:HH:mm:ss}] 実行開始: {artist.ServiceCreatorId}");
            OutputReceived?.Invoke($"[{DateTime.Now:HH:mm:ss}] コマンド: {settings.ScraperPath} {arguments}");

            var scraperFullPath = Path.GetFullPath(settings.ScraperPath);
            var workingDir = Path.GetDirectoryName(scraperFullPath);
            
            var startInfo = new ProcessStartInfo
            {
                FileName = settings.ScraperPath,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8,
                WorkingDirectory = !string.IsNullOrEmpty(workingDir) ? workingDir : Environment.CurrentDirectory
            };

            _currentProcess = new Process { StartInfo = startInfo };

            // 出力イベントハンドラ
            _currentProcess.OutputDataReceived += (sender, e) =>
            {
                if (e.Data != null)
                {
                    // 進捗バー行はログに出力しない（重くなる原因）
                    if (!IsProgressLine(e.Data))
                    {
                        outputBuilder.AppendLine(e.Data);
                        OutputReceived?.Invoke(e.Data);
                    }
                }
            };

            _currentProcess.ErrorDataReceived += (sender, e) =>
            {
                if (e.Data != null)
                {
                    // エラー出力も進捗行をフィルタ
                    if (!IsProgressLine(e.Data))
                    {
                        errorBuilder.AppendLine(e.Data);
                        ErrorReceived?.Invoke(e.Data);
                    }
                }
            };

            _currentProcess.Start();
            _currentProcess.BeginOutputReadLine();
            _currentProcess.BeginErrorReadLine();

            // プロセス終了を待機（キャンセル可能）
            await _currentProcess.WaitForExitAsync(cancellationToken);

            result.ExitCode = _currentProcess.ExitCode;
            result.Success = _currentProcess.ExitCode == 0;
        }
        catch (OperationCanceledException)
        {
            // キャンセルされた場合
            result.Success = false;
            errorBuilder.AppendLine("処理がキャンセルされました");
            OutputReceived?.Invoke($"[{DateTime.Now:HH:mm:ss}] 処理がキャンセルされました");

            // プロセスを強制終了
            try
            {
                if (_currentProcess != null && !_currentProcess.HasExited)
                {
                    _currentProcess.Kill(true);
                }
            }
            catch { }
        }
        catch (Exception ex)
        {
            result.Success = false;
            errorBuilder.AppendLine($"実行エラー: {ex.Message}");
            ErrorReceived?.Invoke($"[{DateTime.Now:HH:mm:ss}] 実行エラー: {ex.Message}");
        }
        finally
        {
            result.EndTime = DateTime.Now;
            result.Output = outputBuilder.ToString();
            result.ErrorOutput = errorBuilder.ToString();

            _currentProcess?.Dispose();
            _currentProcess = null;

            OutputReceived?.Invoke($"[{DateTime.Now:HH:mm:ss}] 実行完了: {(result.Success ? "成功" : "失敗")} (所要時間: {result.Duration.TotalSeconds:F1}秒)");
        }

        return result;
    }

    /// <summary>
    /// 実行中のプロセスをキャンセル
    /// </summary>
    public void Cancel()
    {
        try
        {
            if (_currentProcess != null && !_currentProcess.HasExited)
            {
                _currentProcess.Kill(true);
            }
        }
        catch { }

        _cancellationTokenSource?.Cancel();
    }

    /// <summary>
    /// 進捗バー行かどうかを判定
    /// </summary>
    /// <remarks>
    /// kemono-scraperがリダイレクトされた出力に対して毎回改行で進捗を出力するため、
    /// 進捗バー行をフィルタリングしてログの肥大化を防ぐ
    /// </remarks>
    private static bool IsProgressLine(string line)
    {
        if (string.IsNullOrEmpty(line))
            return false;

        // ANSIカラーコードで始まる行（進捗バーに使用されている）
        if (line.Contains("\x1b[38;5;"))
            return true;

        // 進捗バーのパターン: "    0:00" のような時間表示 + プログレスバー
        if (ProgressTimeRegex.IsMatch(line))
            return true;

        // パーセント表示を含む行
        if (ProgressPercentRegex.IsMatch(line))
            return true;

        return false;
    }

    /// <summary>
    /// コマンドライン引数を構築
    /// </summary>
    private string BuildArguments(Artist artist, AppSettings settings)
    {
        var args = new List<string>();

        // クリエイター指定（必須）
        args.Add($"--creator {artist.Service}:{artist.CreatorId}");

        // 出力先
        if (!string.IsNullOrEmpty(settings.OutputPath))
        {
            args.Add($"--output \"{settings.OutputPath}\"");
        }

        // 差分ダウンロード（前回完了日時以降）
        if (settings.EnableIncrementalDownload && artist.LastCompletedAt.HasValue)
        {
            var dateStr = artist.LastCompletedAt.Value.ToString("yyyyMMdd");
            args.Add($"--date-after {dateStr}");
        }

        // コンテンツダウンロード
        if (settings.DownloadContent)
        {
            args.Add("--content");
        }

        // バナーダウンロード
        if (settings.DownloadBanner)
        {
            args.Add("--banner");
        }

        // 非同期ダウンロード
        if (settings.AsyncDownload)
        {
            args.Add("--async");
        }

        // 上書き
        if (settings.Overwrite)
        {
            args.Add("--overwrite");
        }

        // プレフィックス番号
        if (settings.WithPrefixNumber)
        {
            args.Add("--with-prefix-number");
        }

        // 最大並列数
        if (settings.MaxDownloadParallel > 0)
        {
            args.Add($"--max-download-parallel {settings.MaxDownloadParallel}");
        }

        // リトライ
        if (settings.Retry > 0)
        {
            args.Add($"--retry {settings.Retry}");
        }

        // リトライ間隔
        if (settings.RetryInterval > 0)
        {
            args.Add($"--retry-interval {settings.RetryInterval}");
        }

        // タイムアウト
        if (settings.DownloadTimeout > 0)
        {
            args.Add($"--download-timeout {settings.DownloadTimeout}");
        }

        // レート制限
        if (settings.RateLimit > 0)
        {
            args.Add($"--rate-limit {settings.RateLimit}");
        }

        // プロキシ
        if (!string.IsNullOrEmpty(settings.ProxyUrl))
        {
            args.Add($"--proxy \"{settings.ProxyUrl}\"");
        }

        // テンプレート
        if (!string.IsNullOrEmpty(settings.Template))
        {
            args.Add($"--template \"{settings.Template}\"");
        }

        // 拡張子フィルター
        if (!string.IsNullOrEmpty(settings.ExtensionOnly))
        {
            args.Add($"--extension-only {settings.ExtensionOnly}");
        }

        // 除外拡張子
        if (!string.IsNullOrEmpty(settings.ExtensionExclude))
        {
            args.Add($"--extension-exclude {settings.ExtensionExclude}");
        }

        // 最大サイズ
        if (!string.IsNullOrEmpty(settings.MaxSize))
        {
            args.Add($"--max-size \"{settings.MaxSize}\"");
        }

        // 最小サイズ
        if (!string.IsNullOrEmpty(settings.MinSize))
        {
            args.Add($"--min-size \"{settings.MinSize}\"");
        }

        return string.Join(" ", args);
    }
}
