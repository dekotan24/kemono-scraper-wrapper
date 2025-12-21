using System.Text.Json.Serialization;

namespace KemonoScraperGUI.Models;

/// <summary>
/// アプリケーション設定を保持するモデル
/// </summary>
public class AppSettings
{
    /// <summary>
    /// kemono-scraper.exeのパス
    /// </summary>
    [JsonPropertyName("scraperPath")]
    public string ScraperPath { get; set; } = "kemono-scraper.exe";

    /// <summary>
    /// 出力先ディレクトリ
    /// </summary>
    [JsonPropertyName("outputPath")]
    public string OutputPath { get; set; } = @"Y:\kemono";

    /// <summary>
    /// コンテンツ（テキスト）をダウンロードするか
    /// </summary>
    [JsonPropertyName("downloadContent")]
    public bool DownloadContent { get; set; } = true;

    /// <summary>
    /// バナーをダウンロードするか
    /// </summary>
    [JsonPropertyName("downloadBanner")]
    public bool DownloadBanner { get; set; } = false;

    /// <summary>
    /// 非同期ダウンロードを使用するか
    /// </summary>
    [JsonPropertyName("asyncDownload")]
    public bool AsyncDownload { get; set; } = false;

    /// <summary>
    /// 既存ファイルを上書きするか
    /// </summary>
    [JsonPropertyName("overwrite")]
    public bool Overwrite { get; set; } = false;

    /// <summary>
    /// プレフィックス番号を付けるか
    /// </summary>
    [JsonPropertyName("withPrefixNumber")]
    public bool WithPrefixNumber { get; set; } = false;

    /// <summary>
    /// 最大並列ダウンロード数
    /// </summary>
    [JsonPropertyName("maxDownloadParallel")]
    public int MaxDownloadParallel { get; set; } = 3;

    /// <summary>
    /// リトライ回数
    /// </summary>
    [JsonPropertyName("retry")]
    public int Retry { get; set; } = 3;

    /// <summary>
    /// リトライ間隔（秒）
    /// </summary>
    [JsonPropertyName("retryInterval")]
    public int RetryInterval { get; set; } = 10;

    /// <summary>
    /// ダウンロードタイムアウト（秒）
    /// </summary>
    [JsonPropertyName("downloadTimeout")]
    public int DownloadTimeout { get; set; } = 1800;

    /// <summary>
    /// レート制限（リクエスト/秒）
    /// </summary>
    [JsonPropertyName("rateLimit")]
    public int RateLimit { get; set; } = 2;

    /// <summary>
    /// プロキシURL（空の場合は使用しない）
    /// </summary>
    [JsonPropertyName("proxyUrl")]
    public string ProxyUrl { get; set; } = string.Empty;

    /// <summary>
    /// パステンプレート
    /// </summary>
    [JsonPropertyName("template")]
    public string Template { get; set; } = string.Empty;

    /// <summary>
    /// 拡張子フィルター（カンマ区切り）
    /// </summary>
    [JsonPropertyName("extensionOnly")]
    public string ExtensionOnly { get; set; } = string.Empty;

    /// <summary>
    /// 除外拡張子（カンマ区切り）
    /// </summary>
    [JsonPropertyName("extensionExclude")]
    public string ExtensionExclude { get; set; } = string.Empty;

    /// <summary>
    /// 最大ファイルサイズ
    /// </summary>
    [JsonPropertyName("maxSize")]
    public string MaxSize { get; set; } = string.Empty;

    /// <summary>
    /// 最小ファイルサイズ
    /// </summary>
    [JsonPropertyName("minSize")]
    public string MinSize { get; set; } = string.Empty;

    /// <summary>
    /// 差分ダウンロードを有効にするか（前回完了日時以降のみダウンロード）
    /// </summary>
    [JsonPropertyName("enableIncrementalDownload")]
    public bool EnableIncrementalDownload { get; set; } = true;

    /// <summary>
    /// 完了後に自動で次のアーティストを実行するか
    /// </summary>
    [JsonPropertyName("autoNextOnComplete")]
    public bool AutoNextOnComplete { get; set; } = true;

    /// <summary>
    /// エラー時に自動リトライするか
    /// </summary>
    [JsonPropertyName("autoRetryOnError")]
    public bool AutoRetryOnError { get; set; } = false;

    /// <summary>
    /// 自動リトライ回数
    /// </summary>
    [JsonPropertyName("autoRetryCount")]
    public int AutoRetryCount { get; set; } = 1;

    /// <summary>
    /// ウィンドウ位置X
    /// </summary>
    [JsonPropertyName("windowX")]
    public int WindowX { get; set; } = 100;

    /// <summary>
    /// ウィンドウ位置Y
    /// </summary>
    [JsonPropertyName("windowY")]
    public int WindowY { get; set; } = 100;

    /// <summary>
    /// ウィンドウ幅
    /// </summary>
    [JsonPropertyName("windowWidth")]
    public int WindowWidth { get; set; } = 1200;

    /// <summary>
    /// ウィンドウ高さ
    /// </summary>
    [JsonPropertyName("windowHeight")]
    public int WindowHeight { get; set; } = 800;
}
