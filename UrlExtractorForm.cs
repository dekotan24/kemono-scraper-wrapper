using System.Diagnostics;
using System.Text;
using KemonoScraperGUI.Models;
using KemonoScraperGUI.Services;

namespace KemonoScraperGUI;

public partial class UrlExtractorForm : Form
{
    private readonly ContentExtractorService _extractorService;
    private List<ExtractedUrl> _extractedUrls = new();

    public UrlExtractorForm(string? initialFolder = null)
    {
        InitializeComponent();

        _extractorService = new ContentExtractorService();
        _extractorService.ProgressChanged += OnProgressChanged;

        if (!string.IsNullOrEmpty(initialFolder))
        {
            textBoxFolder.Text = initialFolder;
        }

        SetupDataGridView();
        SetupEventHandlers();
    }

    /// <summary>
    /// DataGridViewの列設定
    /// </summary>
    private void SetupDataGridView()
    {
        dataGridViewUrls.Columns.Clear();

        dataGridViewUrls.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "UrlType",
            HeaderText = "種類",
            Width = 80,
            DataPropertyName = "UrlType"
        });

        dataGridViewUrls.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Creator",
            HeaderText = "Creator",
            Width = 100,
            DataPropertyName = "Creator"
        });

        dataGridViewUrls.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "PostTitle",
            HeaderText = "投稿",
            Width = 150,
            DataPropertyName = "PostTitle"
        });

        dataGridViewUrls.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Url",
            HeaderText = "URL",
            Width = 300,
            DataPropertyName = "Url"
        });

        dataGridViewUrls.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Password",
            HeaderText = "パスワード",
            Width = 100,
            DataPropertyName = "Password"
        });
    }

    /// <summary>
    /// イベントハンドラの設定
    /// </summary>
    private void SetupEventHandlers()
    {
        buttonBrowse.Click += ButtonBrowse_Click;
        buttonExtract.Click += ButtonExtract_Click;
        buttonExportCsv.Click += ButtonExportCsv_Click;
        buttonCopyUrls.Click += ButtonCopyUrls_Click;

        toolStripMenuItemCopyUrl.Click += ToolStripMenuItemCopyUrl_Click;
        toolStripMenuItemCopyPassword.Click += ToolStripMenuItemCopyPassword_Click;
        toolStripMenuItemOpenUrl.Click += ToolStripMenuItemOpenUrl_Click;
        toolStripMenuItemOpenFolder.Click += ToolStripMenuItemOpenFolder_Click;

        dataGridViewUrls.CellDoubleClick += DataGridViewUrls_CellDoubleClick;

        // セルにマウスオーバーでツールチップ表示
        dataGridViewUrls.CellMouseEnter += (s, e) =>
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var cell = dataGridViewUrls.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.ToolTipText = cell.Value?.ToString() ?? "";
            }
        };
    }

    /// <summary>
    /// 進捗更新
    /// </summary>
    private void OnProgressChanged(string message)
    {
        if (InvokeRequired)
        {
            Invoke(() => OnProgressChanged(message));
            return;
        }

        toolStripStatusLabel.Text = message;
    }

    /// <summary>
    /// フォルダ参照
    /// </summary>
    private void ButtonBrowse_Click(object? sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog
        {
            Description = "検索するフォルダを選択",
            UseDescriptionForTitle = true
        };

        if (!string.IsNullOrEmpty(textBoxFolder.Text) && Directory.Exists(textBoxFolder.Text))
        {
            dialog.SelectedPath = textBoxFolder.Text;
        }

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            textBoxFolder.Text = dialog.SelectedPath;
        }
    }

    /// <summary>
    /// 抽出実行
    /// </summary>
    private async void ButtonExtract_Click(object? sender, EventArgs e)
    {
        var folder = textBoxFolder.Text;

        if (string.IsNullOrEmpty(folder) || !Directory.Exists(folder))
        {
            MessageBox.Show("有効なフォルダを選択してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        buttonExtract.Enabled = false;
        buttonExportCsv.Enabled = false;
        buttonCopyUrls.Enabled = false;
        Cursor = Cursors.WaitCursor;

        try
        {
            var externalOnly = checkBoxExternalOnly.Checked;

            _extractedUrls = await Task.Run(() => _extractorService.ExtractUrls(folder, externalOnly));

            dataGridViewUrls.DataSource = null;
            dataGridViewUrls.DataSource = _extractedUrls;

            toolStripStatusLabel.Text = $"完了: {_extractedUrls.Count}件のURLを抽出";

            buttonExportCsv.Enabled = _extractedUrls.Count > 0;
            buttonCopyUrls.Enabled = _extractedUrls.Count > 0;

            if (_extractedUrls.Count == 0)
            {
                MessageBox.Show("URLが見つかりませんでした。", "結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"抽出エラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            buttonExtract.Enabled = true;
            Cursor = Cursors.Default;
        }
    }

    /// <summary>
    /// CSV出力
    /// </summary>
    private void ButtonExportCsv_Click(object? sender, EventArgs e)
    {
        if (_extractedUrls.Count == 0) return;

        using var dialog = new SaveFileDialog
        {
            Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
            DefaultExt = "csv",
            FileName = $"extracted_urls_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
        };

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                _extractorService.ExportToCsv(_extractedUrls, dialog.FileName);
                MessageBox.Show($"CSVファイルを保存しました:\n{dialog.FileName}", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存エラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    /// <summary>
    /// 全URLをコピー
    /// </summary>
    private void ButtonCopyUrls_Click(object? sender, EventArgs e)
    {
        if (_extractedUrls.Count == 0) return;

        var sb = new StringBuilder();
        foreach (var url in _extractedUrls)
        {
            sb.AppendLine(url.Url);
        }

        Clipboard.SetText(sb.ToString());
        toolStripStatusLabel.Text = $"{_extractedUrls.Count}件のURLをクリップボードにコピーしました";
    }

    /// <summary>
    /// 選択行のURLをコピー（複数行対応）
    /// </summary>
    private void ToolStripMenuItemCopyUrl_Click(object? sender, EventArgs e)
    {
        if (dataGridViewUrls.SelectedRows.Count == 0) return;

        var sb = new StringBuilder();
        foreach (DataGridViewRow row in dataGridViewUrls.SelectedRows)
        {
            if (row.DataBoundItem is ExtractedUrl url)
            {
                sb.AppendLine(url.Url);
            }
        }

        if (sb.Length > 0)
        {
            Clipboard.SetText(sb.ToString().TrimEnd());
            toolStripStatusLabel.Text = $"{dataGridViewUrls.SelectedRows.Count}件のURLをコピーしました";
        }
    }

    /// <summary>
    /// 選択行のパスワードをコピー
    /// </summary>
    private void ToolStripMenuItemCopyPassword_Click(object? sender, EventArgs e)
    {
        var url = GetSelectedUrl();
        if (url != null && !string.IsNullOrEmpty(url.Password))
        {
            Clipboard.SetText(url.Password);
            toolStripStatusLabel.Text = "パスワードをコピーしました";
        }
        else
        {
            toolStripStatusLabel.Text = "パスワードがありません";
        }
    }

    /// <summary>
    /// URLを開く
    /// </summary>
    private void ToolStripMenuItemOpenUrl_Click(object? sender, EventArgs e)
    {
        var url = GetSelectedUrl();
        if (url != null)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url.Url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"URLを開けませんでした: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    /// <summary>
    /// フォルダを開く
    /// </summary>
    private void ToolStripMenuItemOpenFolder_Click(object? sender, EventArgs e)
    {
        var url = GetSelectedUrl();
        if (url != null && !string.IsNullOrEmpty(url.SourceFile))
        {
            var folder = Path.GetDirectoryName(url.SourceFile);
            if (folder != null && Directory.Exists(folder))
            {
                Process.Start("explorer.exe", folder);
            }
        }
    }

    /// <summary>
    /// ダブルクリックでURLを開く
    /// </summary>
    private void DataGridViewUrls_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            ToolStripMenuItemOpenUrl_Click(sender, e);
        }
    }

    /// <summary>
    /// 選択中のURLを取得
    /// </summary>
    private ExtractedUrl? GetSelectedUrl()
    {
        if (dataGridViewUrls.SelectedRows.Count > 0)
        {
            return dataGridViewUrls.SelectedRows[0].DataBoundItem as ExtractedUrl;
        }
        return null;
    }
}
