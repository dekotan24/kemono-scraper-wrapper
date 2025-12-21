using System.Text.Json.Serialization;

namespace KemonoScraperGUI.Models;

/// <summary>
/// アーティスト情報を保持するモデル
/// </summary>
public class Artist
{
    /// <summary>
    /// 一意のID（GUID）
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// サービス名（patreon, fanbox, fantia, gumroad等）
    /// </summary>
    [JsonPropertyName("service")]
    public string Service { get; set; } = string.Empty;

    /// <summary>
    /// クリエイターID
    /// </summary>
    [JsonPropertyName("creatorId")]
    public string CreatorId { get; set; } = string.Empty;

    /// <summary>
    /// クリエイター名（表示用、オプション）
    /// </summary>
    [JsonPropertyName("creatorName")]
    public string CreatorName { get; set; } = string.Empty;

    /// <summary>
    /// 最後に正常完了した日時
    /// </summary>
    [JsonPropertyName("lastCompletedAt")]
    public DateTime? LastCompletedAt { get; set; }

    /// <summary>
    /// 最後に実行した日時
    /// </summary>
    [JsonPropertyName("lastRunAt")]
    public DateTime? LastRunAt { get; set; }

    /// <summary>
    /// 最後の実行結果（成功/失敗）
    /// </summary>
    [JsonPropertyName("lastRunSuccess")]
    public bool? LastRunSuccess { get; set; }

    /// <summary>
    /// 最後のエラーメッセージ
    /// </summary>
    [JsonPropertyName("lastErrorMessage")]
    public string? LastErrorMessage { get; set; }

    /// <summary>
    /// 有効/無効フラグ（無効にすると一括実行時にスキップ）
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// メモ欄
    /// </summary>
    [JsonPropertyName("notes")]
    public string Notes { get; set; } = string.Empty;

    /// <summary>
    /// 追加日時
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// 表示用のサービス:ID文字列
    /// </summary>
    [JsonIgnore]
    public string ServiceCreatorId => $"{Service}:{CreatorId}";

    /// <summary>
    /// 表示用の名前（名前があれば名前、なければID）
    /// </summary>
    [JsonIgnore]
    public string DisplayName => string.IsNullOrEmpty(CreatorName) ? CreatorId : CreatorName;

    /// <summary>
    /// 最終完了日時の表示用文字列
    /// </summary>
    [JsonIgnore]
    public string LastCompletedDisplay => LastCompletedAt?.ToString("yyyy/MM/dd HH:mm") ?? "未実行";

    /// <summary>
    /// ステータス表示用文字列
    /// </summary>
    [JsonIgnore]
    public string StatusDisplay
    {
        get
        {
            if (LastRunSuccess == null) return "未実行";
            return LastRunSuccess.Value ? "成功" : "失敗";
        }
    }
}
