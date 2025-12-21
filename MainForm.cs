using System.Diagnostics;
using KemonoScraperGUI.Models;
using KemonoScraperGUI.Services;

namespace KemonoScraperGUI;

public partial class MainForm : Form
{
    private readonly DataService _dataService;
    private readonly ScraperService _scraperService;
    private List<Artist> _artists;
    private AppSettings _settings;
    private CancellationTokenSource? _cancellationTokenSource;
    private bool _isRunning;

    public MainForm()
    {
        InitializeComponent();

        _dataService = new DataService();
        _scraperService = new ScraperService();
        _artists = new List<Artist>();
        _settings = new AppSettings();

        InitializeForm();
        LoadData();
        SetupEventHandlers();
    }

    /// <summary>
    /// フォームの初期化
    /// </summary>
    private void InitializeForm()
    {
        // DataGridViewの列を設定
        SetupDataGridView();

        // スクレイパーの出力イベントをログに表示
        _scraperService.OutputReceived += OnOutputReceived;
        _scraperService.ErrorReceived += OnErrorReceived;
    }

    /// <summary>
    /// DataGridViewの列設定
    /// </summary>
    private void SetupDataGridView()
    {
        dataGridViewArtists.Columns.Clear();

        dataGridViewArtists.Columns.Add(new DataGridViewCheckBoxColumn
        {
            Name = "Enabled",
            HeaderText = "有効",
            Width = 45,
            DataPropertyName = "Enabled"
        });

        dataGridViewArtists.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Service",
            HeaderText = "サービス",
            Width = 70,
            DataPropertyName = "Service"
        });

        dataGridViewArtists.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "CreatorId",
            HeaderText = "ID",
            Width = 100,
            DataPropertyName = "CreatorId"
        });

        dataGridViewArtists.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "CreatorName",
            HeaderText = "名前",
            Width = 150,
            DataPropertyName = "CreatorName"
        });

        dataGridViewArtists.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "LastCompleted",
            HeaderText = "最終完了",
            Width = 120,
            DataPropertyName = "LastCompletedDisplay"
        });

        dataGridViewArtists.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Status",
            HeaderText = "状態",
            Width = 60,
            DataPropertyName = "StatusDisplay"
        });

        dataGridViewArtists.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Notes",
            HeaderText = "メモ",
            Width = 150,
            DataPropertyName = "Notes"
        });

        // 有効列はクリックで切り替え可能に
        dataGridViewArtists.Columns["Enabled"]!.ReadOnly = false;
        dataGridViewArtists.ReadOnly = false;
        foreach (DataGridViewColumn col in dataGridViewArtists.Columns)
        {
            if (col.Name != "Enabled")
            {
                col.ReadOnly = true;
            }
        }
    }

    /// <summary>
    /// データを読み込む
    /// </summary>
    private void LoadData()
    {
        try
        {
            // 設定を読み込み
            _settings = _dataService.LoadSettings();
            ApplySettingsToUI();

            // アーティストリストを読み込み
            _artists = _dataService.LoadArtists();
            RefreshArtistList();

            // ウィンドウ位置を復元
            if (_settings.WindowX > 0 && _settings.WindowY > 0)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(_settings.WindowX, _settings.WindowY);
            }
            if (_settings.WindowWidth > 0 && _settings.WindowHeight > 0)
            {
                this.Size = new Size(_settings.WindowWidth, _settings.WindowHeight);
            }

            LogMessage("アプリケーションを起動しました", Color.LightGreen);
            LogMessage($"登録アーティスト数: {_artists.Count}", Color.LightGray);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"データ読み込みエラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// イベントハンドラの設定
    /// </summary>
    private void SetupEventHandlers()
    {
        // ボタンイベント
        buttonAddArtist.Click += ButtonAddArtist_Click;
        buttonEditArtist.Click += ButtonEditArtist_Click;
        buttonDeleteArtist.Click += ButtonDeleteArtist_Click;
        buttonImportBatch.Click += ButtonImportBatch_Click;
        buttonUrlExtractor.Click += ButtonUrlExtractor_Click;
        buttonAddFromUrl.Click += ButtonAddFromUrl_Click;
        buttonBrowseScraper.Click += ButtonBrowseScraper_Click;
        buttonBrowseOutput.Click += ButtonBrowseOutput_Click;
        buttonRunSelected.Click += ButtonRunSelected_Click;
        buttonRunAll.Click += ButtonRunAll_Click;
        buttonCancel.Click += ButtonCancel_Click;
        buttonSaveSettings.Click += ButtonSaveSettings_Click;

        // DataGridViewイベント
        dataGridViewArtists.CellValueChanged += DataGridViewArtists_CellValueChanged;
        dataGridViewArtists.CellDoubleClick += DataGridViewArtists_CellDoubleClick;
        dataGridViewArtists.CurrentCellDirtyStateChanged += DataGridViewArtists_CurrentCellDirtyStateChanged;

        // コンテキストメニュー
        toolStripMenuItemRun.Click += ToolStripMenuItemRun_Click;
        toolStripMenuItemEdit.Click += ButtonEditArtist_Click;
        toolStripMenuItemDelete.Click += ButtonDeleteArtist_Click;
        toolStripMenuItemOpenFolder.Click += ToolStripMenuItemOpenFolder_Click;
        toolStripMenuItemCopyUrl.Click += ToolStripMenuItemCopyUrl_Click;
        toolStripMenuItemResetDate.Click += ToolStripMenuItemResetDate_Click;

        // URL入力でEnterキー
        textBoxAddUrl.KeyDown += (s, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                ButtonAddFromUrl_Click(s, e);
                e.SuppressKeyPress = true;
            }
        };

        // フォームクローズ時
        this.FormClosing += MainForm_FormClosing;
    }

    /// <summary>
    /// 設定をUIに反映
    /// </summary>
    private void ApplySettingsToUI()
    {
        textBoxScraperPath.Text = _settings.ScraperPath;
        textBoxOutputPath.Text = _settings.OutputPath;
        checkBoxContent.Checked = _settings.DownloadContent;
        checkBoxBanner.Checked = _settings.DownloadBanner;
        checkBoxAsync.Checked = _settings.AsyncDownload;
        checkBoxOverwrite.Checked = _settings.Overwrite;
        checkBoxPrefixNumber.Checked = _settings.WithPrefixNumber;
        checkBoxIncrementalDownload.Checked = _settings.EnableIncrementalDownload;
        numericMaxParallel.Value = _settings.MaxDownloadParallel;
        numericRetry.Value = _settings.Retry;
        numericRetryInterval.Value = _settings.RetryInterval;
        numericRateLimit.Value = _settings.RateLimit;
        numericTimeout.Value = _settings.DownloadTimeout;
        textBoxProxy.Text = _settings.ProxyUrl;
    }

    /// <summary>
    /// UIから設定を取得
    /// </summary>
    private void ApplyUIToSettings()
    {
        _settings.ScraperPath = textBoxScraperPath.Text;
        _settings.OutputPath = textBoxOutputPath.Text;
        _settings.DownloadContent = checkBoxContent.Checked;
        _settings.DownloadBanner = checkBoxBanner.Checked;
        _settings.AsyncDownload = checkBoxAsync.Checked;
        _settings.Overwrite = checkBoxOverwrite.Checked;
        _settings.WithPrefixNumber = checkBoxPrefixNumber.Checked;
        _settings.EnableIncrementalDownload = checkBoxIncrementalDownload.Checked;
        _settings.MaxDownloadParallel = (int)numericMaxParallel.Value;
        _settings.Retry = (int)numericRetry.Value;
        _settings.RetryInterval = (int)numericRetryInterval.Value;
        _settings.RateLimit = (int)numericRateLimit.Value;
        _settings.DownloadTimeout = (int)numericTimeout.Value;
        _settings.ProxyUrl = textBoxProxy.Text;

        // ウィンドウ位置を保存
        _settings.WindowX = this.Location.X;
        _settings.WindowY = this.Location.Y;
        _settings.WindowWidth = this.Size.Width;
        _settings.WindowHeight = this.Size.Height;
    }

    /// <summary>
    /// アーティストリストを更新
    /// </summary>
    private void RefreshArtistList()
    {
        dataGridViewArtists.DataSource = null;
        dataGridViewArtists.DataSource = _artists;
        UpdateStatusBar();
    }

    /// <summary>
    /// ステータスバーを更新
    /// </summary>
    private void UpdateStatusBar(string? message = null)
    {
        if (message != null)
        {
            toolStripStatusLabel.Text = message;
        }
        else
        {
            var enabledCount = _artists.Count(a => a.Enabled);
            toolStripStatusLabel.Text = $"登録: {_artists.Count} / 有効: {enabledCount}";
        }
    }

    /// <summary>
    /// ログの最大文字数（これを超えると古いログを削除）
    /// </summary>
    private const int MaxLogLength = 100000;

    /// <summary>
    /// ログにメッセージを追加
    /// </summary>
    private void LogMessage(string message, Color? color = null)
    {
        if (InvokeRequired)
        {
            Invoke(() => LogMessage(message, color));
            return;
        }

        // ログが長くなりすぎたら古い部分を削除
        if (richTextBoxLog.TextLength > MaxLogLength)
        {
            richTextBoxLog.SuspendLayout();
            try
            {
                // 先頭の30%を削除
                int removeLength = (int)(MaxLogLength * 0.3);
                richTextBoxLog.Select(0, removeLength);
                richTextBoxLog.SelectedText = "[...古いログを削除...]\n";
            }
            finally
            {
                richTextBoxLog.ResumeLayout();
            }
        }

        richTextBoxLog.SuspendLayout();
        try
        {
            richTextBoxLog.SelectionStart = richTextBoxLog.TextLength;
            richTextBoxLog.SelectionColor = color ?? Color.LightGray;
            richTextBoxLog.AppendText(message + Environment.NewLine);
            richTextBoxLog.ScrollToCaret();
        }
        finally
        {
            richTextBoxLog.ResumeLayout();
        }
    }

    /// <summary>
    /// 出力受信時
    /// </summary>
    private void OnOutputReceived(string message)
    {
        LogMessage(message, Color.White);
    }

    /// <summary>
    /// エラー出力受信時
    /// </summary>
    private void OnErrorReceived(string message)
    {
        LogMessage(message, Color.Orange);
    }

    /// <summary>
    /// 実行中状態を設定
    /// </summary>
    private void SetRunningState(bool running)
    {
        _isRunning = running;

        if (InvokeRequired)
        {
            Invoke(() => SetRunningState(running));
            return;
        }

        buttonRunSelected.Enabled = !running;
        buttonRunAll.Enabled = !running;
        buttonCancel.Enabled = running;
        toolStripProgressBar.Visible = running;

        if (running)
        {
            toolStripProgressBar.Style = ProgressBarStyle.Marquee;
        }
    }

    #region イベントハンドラ

    private void ButtonAddArtist_Click(object? sender, EventArgs e)
    {
        using var dialog = new ArtistEditForm(null);
        if (dialog.ShowDialog() == DialogResult.OK && dialog.Artist != null)
        {
            // 重複チェック
            if (_artists.Any(a => a.Service == dialog.Artist.Service && a.CreatorId == dialog.Artist.CreatorId))
            {
                MessageBox.Show("同じサービス・IDのアーティストが既に登録されています。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _artists.Add(dialog.Artist);
            SaveArtists();
            RefreshArtistList();
            LogMessage($"アーティストを追加: {dialog.Artist.ServiceCreatorId}", Color.LightGreen);
        }
    }

    private void ButtonEditArtist_Click(object? sender, EventArgs e)
    {
        var artist = GetSelectedArtist();
        if (artist == null) return;

        using var dialog = new ArtistEditForm(artist);
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            SaveArtists();
            RefreshArtistList();
            LogMessage($"アーティストを編集: {artist.ServiceCreatorId}", Color.LightBlue);
        }
    }

    private void ButtonDeleteArtist_Click(object? sender, EventArgs e)
    {
        var selectedRows = dataGridViewArtists.SelectedRows;
        if (selectedRows.Count == 0) return;

        var message = selectedRows.Count == 1
            ? "選択したアーティストを削除しますか？"
            : $"{selectedRows.Count}件のアーティストを削除しますか？";

        if (MessageBox.Show(message, "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            var toDelete = new List<Artist>();
            foreach (DataGridViewRow row in selectedRows)
            {
                if (row.DataBoundItem is Artist artist)
                {
                    toDelete.Add(artist);
                }
            }

            foreach (var artist in toDelete)
            {
                _artists.Remove(artist);
                LogMessage($"アーティストを削除: {artist.ServiceCreatorId}", Color.Orange);
            }

            SaveArtists();
            RefreshArtistList();
        }
    }

    private void ButtonImportBatch_Click(object? sender, EventArgs e)
    {
        using var openFileDialog = new OpenFileDialog
        {
            Filter = "Batch files (*.bat)|*.bat|All files (*.*)|*.*",
            Title = "batch_exec.batを選択"
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                var imported = _dataService.ImportFromBatchFile(openFileDialog.FileName);
                var addedCount = 0;

                foreach (var artist in imported)
                {
                    if (!_artists.Any(a => a.Service == artist.Service && a.CreatorId == artist.CreatorId))
                    {
                        _artists.Add(artist);
                        addedCount++;
                    }
                }

                SaveArtists();
                RefreshArtistList();

                MessageBox.Show($"{imported.Count}件中 {addedCount}件をインポートしました。\n({imported.Count - addedCount}件は重複のためスキップ)",
                    "インポート完了", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LogMessage($"バッチファイルからインポート: {addedCount}件追加", Color.LightGreen);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"インポートエラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void ButtonUrlExtractor_Click(object? sender, EventArgs e)
    {
        // 出力フォルダがあればそれを初期値に
        var initialFolder = !string.IsNullOrEmpty(_settings.OutputPath) && Directory.Exists(_settings.OutputPath)
            ? _settings.OutputPath
            : null;

        using var form = new UrlExtractorForm(initialFolder);
        form.ShowDialog(this);
    }

    private void ButtonAddFromUrl_Click(object? sender, EventArgs e)
    {
        var input = textBoxAddUrl.Text.Trim();
        if (string.IsNullOrEmpty(input)) return;

        (string service, string creatorId)? parsed = null;

        // URLとして解析を試みる
        if (input.StartsWith("http", StringComparison.OrdinalIgnoreCase))
        {
            parsed = _dataService.ParseKemonoUrl(input);
        }

        // service:id形式として解析
        if (parsed == null)
        {
            parsed = _dataService.ParseServiceCreatorId(input);
        }

        if (parsed == null)
        {
            MessageBox.Show("URLまたはサービス:IDの形式で入力してください。\n例: https://kemono.cr/patreon/user/12345 または patreon:12345",
                "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // 重複チェック
        if (_artists.Any(a => a.Service == parsed.Value.service && a.CreatorId == parsed.Value.creatorId))
        {
            MessageBox.Show("同じサービス・IDのアーティストが既に登録されています。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var artist = new Artist
        {
            Service = parsed.Value.service,
            CreatorId = parsed.Value.creatorId
        };

        _artists.Add(artist);
        SaveArtists();
        RefreshArtistList();

        textBoxAddUrl.Clear();
        LogMessage($"アーティストを追加: {artist.ServiceCreatorId}", Color.LightGreen);
    }

    private void ButtonBrowseScraper_Click(object? sender, EventArgs e)
    {
        using var openFileDialog = new OpenFileDialog
        {
            Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*",
            Title = "kemono-scraper.exeを選択"
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            textBoxScraperPath.Text = openFileDialog.FileName;
        }
    }

    private void ButtonBrowseOutput_Click(object? sender, EventArgs e)
    {
        using var folderDialog = new FolderBrowserDialog
        {
            Description = "出力先フォルダを選択",
            UseDescriptionForTitle = true
        };

        if (folderDialog.ShowDialog() == DialogResult.OK)
        {
            textBoxOutputPath.Text = folderDialog.SelectedPath;
        }
    }

    private async void ButtonRunSelected_Click(object? sender, EventArgs e)
    {
        var artist = GetSelectedArtist();
        if (artist == null)
        {
            MessageBox.Show("アーティストを選択してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        await RunArtistsAsync(new[] { artist });
    }

    private async void ButtonRunAll_Click(object? sender, EventArgs e)
    {
        var enabledArtists = _artists.Where(a => a.Enabled).ToArray();
        if (enabledArtists.Length == 0)
        {
            MessageBox.Show("有効なアーティストがありません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (MessageBox.Show($"{enabledArtists.Length}件のアーティストを実行しますか？", "確認",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            await RunArtistsAsync(enabledArtists);
        }
    }

    private void ButtonCancel_Click(object? sender, EventArgs e)
    {
        _cancellationTokenSource?.Cancel();
        _scraperService.Cancel();
        LogMessage("キャンセルをリクエストしました...", Color.Yellow);
    }

    private void ButtonSaveSettings_Click(object? sender, EventArgs e)
    {
        try
        {
            ApplyUIToSettings();
            _dataService.SaveSettings(_settings);
            LogMessage("設定を保存しました", Color.LightGreen);
            MessageBox.Show("設定を保存しました。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"設定の保存に失敗しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void DataGridViewArtists_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
        {
            if (dataGridViewArtists.Columns[e.ColumnIndex].Name == "Enabled")
            {
                SaveArtists();
                UpdateStatusBar();
            }
        }
    }

    private void DataGridViewArtists_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
    {
        if (dataGridViewArtists.IsCurrentCellDirty)
        {
            dataGridViewArtists.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
    }

    private void DataGridViewArtists_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            ButtonEditArtist_Click(sender, e);
        }
    }

    private async void ToolStripMenuItemRun_Click(object? sender, EventArgs e)
    {
        var artist = GetSelectedArtist();
        if (artist != null)
        {
            await RunArtistsAsync(new[] { artist });
        }
    }

    private void ToolStripMenuItemOpenFolder_Click(object? sender, EventArgs e)
    {
        var artist = GetSelectedArtist();
        if (artist == null) return;

        // kemono-scraperの出力フォルダパターンを複数試行
        var possibleFolders = new List<string>();

        // パターン1: [service]CreatorName (スペースなし - kemono-scraper標準形式)
        if (!string.IsNullOrEmpty(artist.CreatorName))
        {
            possibleFolders.Add(Path.Combine(_settings.OutputPath, $"[{artist.Service}]{artist.CreatorName}"));
        }

        // パターン2: [service] CreatorName (スペースあり)
        if (!string.IsNullOrEmpty(artist.CreatorName))
        {
            possibleFolders.Add(Path.Combine(_settings.OutputPath, $"[{artist.Service}] {artist.CreatorName}"));
        }

        // パターン3: [service]CreatorId (名前がない場合)
        possibleFolders.Add(Path.Combine(_settings.OutputPath, $"[{artist.Service}]{artist.CreatorId}"));
        possibleFolders.Add(Path.Combine(_settings.OutputPath, $"[{artist.Service}] {artist.CreatorId}"));

        // 実際に存在するフォルダを検索
        var existingFolder = possibleFolders.FirstOrDefault(f => Directory.Exists(f));

        // パターンマッチで検索（[service]で始まるフォルダ内からCreatorIdまたはCreatorNameを含むものを探す）
        if (existingFolder == null && Directory.Exists(_settings.OutputPath))
        {
            try
            {
                var servicePrefix = $"[{artist.Service}]";
                existingFolder = Directory.GetDirectories(_settings.OutputPath)
                    .FirstOrDefault(d =>
                    {
                        var dirName = Path.GetFileName(d);
                        if (!dirName.StartsWith(servicePrefix, StringComparison.OrdinalIgnoreCase))
                            return false;

                        // CreatorNameまたはCreatorIdが含まれているか確認
                        var afterService = dirName.Substring(servicePrefix.Length).TrimStart();
                        return afterService.Equals(artist.CreatorName, StringComparison.OrdinalIgnoreCase) ||
                               afterService.Equals(artist.CreatorId, StringComparison.OrdinalIgnoreCase) ||
                               (!string.IsNullOrEmpty(artist.CreatorName) && afterService.Contains(artist.CreatorName, StringComparison.OrdinalIgnoreCase));
                    });
            }
            catch { }
        }

        if (!string.IsNullOrEmpty(existingFolder) && Directory.Exists(existingFolder))
        {
            Process.Start("explorer.exe", existingFolder);
        }
        else
        {
            MessageBox.Show("フォルダが見つかりません。\nまだダウンロードしていない可能性があります。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void ToolStripMenuItemCopyUrl_Click(object? sender, EventArgs e)
    {
        var artist = GetSelectedArtist();
        if (artist == null) return;

        var url = $"https://kemono.cr/{artist.Service}/user/{artist.CreatorId}";
        Clipboard.SetText(url);
        LogMessage($"URLをコピー: {url}", Color.LightGray);
    }

    private void ToolStripMenuItemResetDate_Click(object? sender, EventArgs e)
    {
        var artist = GetSelectedArtist();
        if (artist == null) return;

        if (MessageBox.Show($"{artist.ServiceCreatorId} の完了日時をリセットしますか？\n次回実行時に全件ダウンロードされます。",
                "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            artist.LastCompletedAt = null;
            artist.LastRunAt = null;
            artist.LastRunSuccess = null;
            SaveArtists();
            RefreshArtistList();
            LogMessage($"完了日時をリセット: {artist.ServiceCreatorId}", Color.Yellow);
        }
    }

    private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (_isRunning)
        {
            if (MessageBox.Show("ダウンロード中です。終了しますか？", "確認",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            _cancellationTokenSource?.Cancel();
            _scraperService.Cancel();
        }

        // 設定を保存
        try
        {
            ApplyUIToSettings();
            _dataService.SaveSettings(_settings);
        }
        catch { }
    }

    #endregion

    #region ヘルパーメソッド

    /// <summary>
    /// 選択中のアーティストを取得
    /// </summary>
    private Artist? GetSelectedArtist()
    {
        if (dataGridViewArtists.SelectedRows.Count > 0)
        {
            return dataGridViewArtists.SelectedRows[0].DataBoundItem as Artist;
        }
        return null;
    }

    /// <summary>
    /// アーティストリストを保存
    /// </summary>
    private void SaveArtists()
    {
        try
        {
            _dataService.SaveArtists(_artists);
        }
        catch (Exception ex)
        {
            LogMessage($"保存エラー: {ex.Message}", Color.Red);
        }
    }

    /// <summary>
    /// アーティストのダウンロードを実行
    /// </summary>
    private async Task RunArtistsAsync(Artist[] artists)
    {
        if (_isRunning) return;

        // 設定を適用
        ApplyUIToSettings();

        // スクレイパーの存在確認
        if (!File.Exists(_settings.ScraperPath))
        {
            MessageBox.Show($"kemono-scraper.exeが見つかりません:\n{_settings.ScraperPath}",
                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        SetRunningState(true);
        _cancellationTokenSource = new CancellationTokenSource();

        var successCount = 0;
        var failCount = 0;

        try
        {
            for (int i = 0; i < artists.Length; i++)
            {
                if (_cancellationTokenSource.Token.IsCancellationRequested)
                    break;

                var artist = artists[i];
                UpdateStatusBar($"実行中: {i + 1}/{artists.Length} - {artist.ServiceCreatorId}");

                LogMessage($"\n{'=',-60}", Color.Cyan);
                LogMessage($"[{i + 1}/{artists.Length}] {artist.ServiceCreatorId} - 開始", Color.Cyan);
                LogMessage($"{'=',-60}", Color.Cyan);

                var result = await _scraperService.RunAsync(artist, _settings, _cancellationTokenSource.Token);

                artist.LastRunAt = DateTime.Now;
                artist.LastRunSuccess = result.Success;

                if (result.Success)
                {
                    artist.LastCompletedAt = DateTime.Now;
                    artist.LastErrorMessage = null;
                    successCount++;
                    LogMessage($"✓ {artist.ServiceCreatorId} 完了", Color.LightGreen);
                }
                else
                {
                    artist.LastErrorMessage = result.ErrorOutput;
                    failCount++;
                    LogMessage($"✗ {artist.ServiceCreatorId} 失敗", Color.Red);
                }

                SaveArtists();
                RefreshArtistList();
            }
        }
        catch (Exception ex)
        {
            LogMessage($"実行エラー: {ex.Message}", Color.Red);
        }
        finally
        {
            SetRunningState(false);
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;

            var summary = $"完了: 成功 {successCount} / 失敗 {failCount}";
            UpdateStatusBar(summary);
            LogMessage($"\n{summary}", Color.Yellow);
        }
    }

    #endregion
}
