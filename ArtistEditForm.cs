using KemonoScraperGUI.Models;

namespace KemonoScraperGUI;

public partial class ArtistEditForm : Form
{
    private readonly Artist? _originalArtist;

    /// <summary>
    /// 編集結果のアーティスト
    /// </summary>
    public Artist? Artist { get; private set; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="artist">編集するアーティスト（nullの場合は新規作成）</param>
    public ArtistEditForm(Artist? artist)
    {
        InitializeComponent();

        _originalArtist = artist;

        if (artist != null)
        {
            // 編集モード
            this.Text = "アーティストを編集";
            comboBoxService.SelectedItem = artist.Service;
            textBoxCreatorId.Text = artist.CreatorId;
            textBoxCreatorName.Text = artist.CreatorName;
            textBoxNotes.Text = artist.Notes;
            checkBoxEnabled.Checked = artist.Enabled;

            // 実行情報
            textBoxLastCompleted.Text = artist.LastCompletedAt?.ToString("yyyy/MM/dd HH:mm:ss") ?? "未実行";
            textBoxLastStatus.Text = artist.LastRunSuccess switch
            {
                true => "成功",
                false => "失敗",
                null => "未実行"
            };

            // 既存のサービス・IDは変更不可にする（重複防止）
            comboBoxService.Enabled = false;
            textBoxCreatorId.ReadOnly = true;
        }
        else
        {
            // 新規作成モード
            this.Text = "アーティストを追加";
            comboBoxService.SelectedIndex = 0;
            groupBoxInfo.Visible = false;
            this.Height -= 100;
        }

        buttonOK.Click += ButtonOK_Click;
    }

    private void ButtonOK_Click(object? sender, EventArgs e)
    {
        // バリデーション
        if (comboBoxService.SelectedItem == null)
        {
            MessageBox.Show("サービスを選択してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            comboBoxService.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(textBoxCreatorId.Text))
        {
            MessageBox.Show("クリエイターIDを入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBoxCreatorId.Focus();
            return;
        }

        // アーティストを作成/更新
        if (_originalArtist != null)
        {
            // 編集モード：既存のアーティストを更新
            _originalArtist.CreatorName = textBoxCreatorName.Text.Trim();
            _originalArtist.Notes = textBoxNotes.Text.Trim();
            _originalArtist.Enabled = checkBoxEnabled.Checked;
            Artist = _originalArtist;
        }
        else
        {
            // 新規作成モード
            Artist = new Artist
            {
                Service = comboBoxService.SelectedItem!.ToString()!.ToLower(),
                CreatorId = textBoxCreatorId.Text.Trim(),
                CreatorName = textBoxCreatorName.Text.Trim(),
                Notes = textBoxNotes.Text.Trim(),
                Enabled = checkBoxEnabled.Checked
            };
        }

        this.DialogResult = DialogResult.OK;
        this.Close();
    }
}
