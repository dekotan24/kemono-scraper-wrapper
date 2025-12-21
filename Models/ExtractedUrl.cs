using System.Text.Json.Serialization;

namespace KemonoScraperGUI.Models;

/// <summary>
/// 抽出されたURL情報
/// </summary>
public class ExtractedUrl
{
    /// <summary>
    /// アーティストのサービス名
    /// </summary>
    [JsonPropertyName("service")]
    public string Service { get; set; } = string.Empty;

    /// <summary>
    /// アーティストのID/名前
    /// </summary>
    [JsonPropertyName("creator")]
    public string Creator { get; set; } = string.Empty;

    /// <summary>
    /// 投稿タイトル（フォルダ名から）
    /// </summary>
    [JsonPropertyName("postTitle")]
    public string PostTitle { get; set; } = string.Empty;

    /// <summary>
    /// 抽出されたURL
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// URLのドメイン/種類
    /// </summary>
    [JsonPropertyName("urlType")]
    public string UrlType { get; set; } = string.Empty;

    /// <summary>
    /// 見つかったパスワード（なければ空）
    /// </summary>
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// 元ファイルのパス
    /// </summary>
    [JsonPropertyName("sourceFile")]
    public string SourceFile { get; set; } = string.Empty;

    /// <summary>
    /// 表示用：Creator/Post
    /// </summary>
    [JsonIgnore]
    public string Location => $"{Creator}/{PostTitle}";
}
