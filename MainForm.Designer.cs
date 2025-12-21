namespace KemonoScraperGUI;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();

        // メインスプリッター
        this.splitContainerMain = new SplitContainer();
        this.splitContainerLeft = new SplitContainer();

        // アーティストリスト関連
        this.dataGridViewArtists = new DataGridView();
        this.panelArtistButtons = new Panel();
        this.buttonAddArtist = new Button();
        this.buttonEditArtist = new Button();
        this.buttonDeleteArtist = new Button();
        this.buttonImportBatch = new Button();
        this.buttonUrlExtractor = new Button();
        this.textBoxAddUrl = new TextBox();
        this.buttonAddFromUrl = new Button();

        // 設定パネル関連
        this.panelSettings = new Panel();
        this.groupBoxPaths = new GroupBox();
        this.labelScraperPath = new Label();
        this.textBoxScraperPath = new TextBox();
        this.buttonBrowseScraper = new Button();
        this.labelOutputPath = new Label();
        this.textBoxOutputPath = new TextBox();
        this.buttonBrowseOutput = new Button();

        this.groupBoxOptions = new GroupBox();
        this.checkBoxContent = new CheckBox();
        this.checkBoxBanner = new CheckBox();
        this.checkBoxAsync = new CheckBox();
        this.checkBoxOverwrite = new CheckBox();
        this.checkBoxPrefixNumber = new CheckBox();
        this.checkBoxIncrementalDownload = new CheckBox();

        this.groupBoxAdvanced = new GroupBox();
        this.labelMaxParallel = new Label();
        this.numericMaxParallel = new NumericUpDown();
        this.labelRetry = new Label();
        this.numericRetry = new NumericUpDown();
        this.labelRetryInterval = new Label();
        this.numericRetryInterval = new NumericUpDown();
        this.labelRateLimit = new Label();
        this.numericRateLimit = new NumericUpDown();
        this.labelTimeout = new Label();
        this.numericTimeout = new NumericUpDown();
        this.labelProxy = new Label();
        this.textBoxProxy = new TextBox();

        // 実行ボタン関連
        this.panelExecute = new Panel();
        this.buttonRunSelected = new Button();
        this.buttonRunAll = new Button();
        this.buttonCancel = new Button();
        this.buttonSaveSettings = new Button();

        // ログ表示
        this.richTextBoxLog = new RichTextBox();

        // ステータスバー
        this.statusStrip = new StatusStrip();
        this.toolStripStatusLabel = new ToolStripStatusLabel();
        this.toolStripProgressBar = new ToolStripProgressBar();

        // ツールチップ
        this.toolTip = new ToolTip(this.components);

        // コンテキストメニュー
        this.contextMenuStripArtist = new ContextMenuStrip(this.components);
        this.toolStripMenuItemRun = new ToolStripMenuItem();
        this.toolStripMenuItemEdit = new ToolStripMenuItem();
        this.toolStripMenuItemDelete = new ToolStripMenuItem();
        this.toolStripMenuItemOpenFolder = new ToolStripMenuItem();
        this.toolStripMenuItemCopyUrl = new ToolStripMenuItem();
        this.toolStripMenuItemResetDate = new ToolStripMenuItem();

        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewArtists)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
        this.splitContainerMain.Panel1.SuspendLayout();
        this.splitContainerMain.Panel2.SuspendLayout();
        this.splitContainerMain.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeft)).BeginInit();
        this.splitContainerLeft.Panel1.SuspendLayout();
        this.splitContainerLeft.Panel2.SuspendLayout();
        this.splitContainerLeft.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.numericMaxParallel)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.numericRetry)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.numericRetryInterval)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.numericRateLimit)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.numericTimeout)).BeginInit();
        this.SuspendLayout();

        // 
        // splitContainerMain
        // 
        this.splitContainerMain.Dock = DockStyle.Fill;
        this.splitContainerMain.Location = new Point(0, 0);
        this.splitContainerMain.Name = "splitContainerMain";
        this.splitContainerMain.Panel1.Controls.Add(this.splitContainerLeft);
        this.splitContainerMain.Panel2.Controls.Add(this.richTextBoxLog);
        this.splitContainerMain.Size = new Size(1200, 770);
        this.splitContainerMain.SplitterDistance = 750;
        this.splitContainerMain.TabIndex = 0;

        // 
        // splitContainerLeft
        // 
        this.splitContainerLeft.Dock = DockStyle.Fill;
        this.splitContainerLeft.Orientation = Orientation.Horizontal;
        this.splitContainerLeft.Panel1.Controls.Add(this.dataGridViewArtists);
        this.splitContainerLeft.Panel1.Controls.Add(this.panelArtistButtons);
        this.splitContainerLeft.Panel2.Controls.Add(this.panelSettings);
        this.splitContainerLeft.Panel2.Controls.Add(this.panelExecute);
        this.splitContainerLeft.Size = new Size(750, 770);
        this.splitContainerLeft.SplitterDistance = 350;
        this.splitContainerLeft.TabIndex = 0;

        // 
        // dataGridViewArtists
        // 
        this.dataGridViewArtists.AllowUserToAddRows = false;
        this.dataGridViewArtists.AllowUserToDeleteRows = false;
        this.dataGridViewArtists.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        this.dataGridViewArtists.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridViewArtists.ContextMenuStrip = this.contextMenuStripArtist;
        this.dataGridViewArtists.Dock = DockStyle.Fill;
        this.dataGridViewArtists.Location = new Point(0, 40);
        this.dataGridViewArtists.Name = "dataGridViewArtists";
        this.dataGridViewArtists.ReadOnly = true;
        this.dataGridViewArtists.RowHeadersWidth = 25;
        this.dataGridViewArtists.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        this.dataGridViewArtists.Size = new Size(750, 310);
        this.dataGridViewArtists.TabIndex = 1;

        // 
        // panelArtistButtons
        // 
        this.panelArtistButtons.Controls.Add(this.buttonAddArtist);
        this.panelArtistButtons.Controls.Add(this.buttonEditArtist);
        this.panelArtistButtons.Controls.Add(this.buttonDeleteArtist);
        this.panelArtistButtons.Controls.Add(this.buttonImportBatch);
        this.panelArtistButtons.Controls.Add(this.buttonUrlExtractor);
        this.panelArtistButtons.Controls.Add(this.textBoxAddUrl);
        this.panelArtistButtons.Controls.Add(this.buttonAddFromUrl);
        this.panelArtistButtons.Dock = DockStyle.Top;
        this.panelArtistButtons.Height = 40;
        this.panelArtistButtons.Name = "panelArtistButtons";
        this.panelArtistButtons.TabIndex = 0;

        // 
        // buttonAddArtist
        // 
        this.buttonAddArtist.Location = new Point(5, 8);
        this.buttonAddArtist.Name = "buttonAddArtist";
        this.buttonAddArtist.Size = new Size(60, 25);
        this.buttonAddArtist.TabIndex = 0;
        this.buttonAddArtist.Text = "追加";
        this.toolTip.SetToolTip(this.buttonAddArtist, "アーティストを手動で追加");

        // 
        // buttonEditArtist
        // 
        this.buttonEditArtist.Location = new Point(70, 8);
        this.buttonEditArtist.Name = "buttonEditArtist";
        this.buttonEditArtist.Size = new Size(60, 25);
        this.buttonEditArtist.TabIndex = 1;
        this.buttonEditArtist.Text = "編集";
        this.toolTip.SetToolTip(this.buttonEditArtist, "選択したアーティストを編集");

        // 
        // buttonDeleteArtist
        // 
        this.buttonDeleteArtist.Location = new Point(135, 8);
        this.buttonDeleteArtist.Name = "buttonDeleteArtist";
        this.buttonDeleteArtist.Size = new Size(60, 25);
        this.buttonDeleteArtist.TabIndex = 2;
        this.buttonDeleteArtist.Text = "削除";
        this.toolTip.SetToolTip(this.buttonDeleteArtist, "選択したアーティストを削除");

        // 
        // buttonImportBatch
        // 
        this.buttonImportBatch.Location = new Point(200, 8);
        this.buttonImportBatch.Name = "buttonImportBatch";
        this.buttonImportBatch.Size = new Size(100, 25);
        this.buttonImportBatch.TabIndex = 3;
        this.buttonImportBatch.Text = "batインポート";
        this.toolTip.SetToolTip(this.buttonImportBatch, "batch_exec.batからインポート");

        // 
        // buttonUrlExtractor
        // 
        this.buttonUrlExtractor.Location = new Point(305, 8);
        this.buttonUrlExtractor.Name = "buttonUrlExtractor";
        this.buttonUrlExtractor.Size = new Size(90, 25);
        this.buttonUrlExtractor.TabIndex = 4;
        this.buttonUrlExtractor.Text = "🔗 URL抽出";
        this.toolTip.SetToolTip(this.buttonUrlExtractor, "content.txtからURLとパスワードを抽出");

        // 
        // textBoxAddUrl
        // 
        this.textBoxAddUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        this.textBoxAddUrl.Location = new Point(400, 9);
        this.textBoxAddUrl.Name = "textBoxAddUrl";
        this.textBoxAddUrl.Size = new Size(260, 23);
        this.textBoxAddUrl.TabIndex = 5;
        this.textBoxAddUrl.PlaceholderText = "URLまたは service:id を入力して追加";
        this.toolTip.SetToolTip(this.textBoxAddUrl, "kemono URL または patreon:12345 形式で入力");

        // 
        // buttonAddFromUrl
        // 
        this.buttonAddFromUrl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.buttonAddFromUrl.Location = new Point(665, 8);
        this.buttonAddFromUrl.Name = "buttonAddFromUrl";
        this.buttonAddFromUrl.Size = new Size(75, 25);
        this.buttonAddFromUrl.TabIndex = 5;
        this.buttonAddFromUrl.Text = "URL追加";

        // 
        // panelSettings
        // 
        this.panelSettings.AutoScroll = true;
        // Dock=Topは逆順で追加（最後に追加したものが一番上になる）
        this.panelSettings.Controls.Add(this.groupBoxAdvanced);
        this.panelSettings.Controls.Add(this.groupBoxOptions);
        this.panelSettings.Controls.Add(this.groupBoxPaths);
        this.panelSettings.Dock = DockStyle.Fill;
        this.panelSettings.Name = "panelSettings";
        this.panelSettings.TabIndex = 0;

        // 
        // groupBoxPaths
        // 
        this.groupBoxPaths.Controls.Add(this.labelScraperPath);
        this.groupBoxPaths.Controls.Add(this.textBoxScraperPath);
        this.groupBoxPaths.Controls.Add(this.buttonBrowseScraper);
        this.groupBoxPaths.Controls.Add(this.labelOutputPath);
        this.groupBoxPaths.Controls.Add(this.textBoxOutputPath);
        this.groupBoxPaths.Controls.Add(this.buttonBrowseOutput);
        this.groupBoxPaths.Dock = DockStyle.Top;
        this.groupBoxPaths.Height = 90;
        this.groupBoxPaths.Name = "groupBoxPaths";
        this.groupBoxPaths.Text = "パス設定";
        this.groupBoxPaths.TabIndex = 0;

        // 
        // labelScraperPath
        // 
        this.labelScraperPath.AutoSize = true;
        this.labelScraperPath.Location = new Point(10, 22);
        this.labelScraperPath.Text = "Scraper:";

        // 
        // textBoxScraperPath
        // 
        this.textBoxScraperPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        this.textBoxScraperPath.Location = new Point(80, 19);
        this.textBoxScraperPath.Size = new Size(550, 23);

        // 
        // buttonBrowseScraper
        // 
        this.buttonBrowseScraper.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.buttonBrowseScraper.Location = new Point(635, 18);
        this.buttonBrowseScraper.Size = new Size(100, 25);
        this.buttonBrowseScraper.Text = "参照...";

        // 
        // labelOutputPath
        // 
        this.labelOutputPath.AutoSize = true;
        this.labelOutputPath.Location = new Point(10, 55);
        this.labelOutputPath.Text = "出力先:";

        // 
        // textBoxOutputPath
        // 
        this.textBoxOutputPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        this.textBoxOutputPath.Location = new Point(80, 52);
        this.textBoxOutputPath.Size = new Size(550, 23);

        // 
        // buttonBrowseOutput
        // 
        this.buttonBrowseOutput.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.buttonBrowseOutput.Location = new Point(635, 51);
        this.buttonBrowseOutput.Size = new Size(100, 25);
        this.buttonBrowseOutput.Text = "参照...";

        // 
        // groupBoxOptions
        // 
        this.groupBoxOptions.Controls.Add(this.checkBoxContent);
        this.groupBoxOptions.Controls.Add(this.checkBoxBanner);
        this.groupBoxOptions.Controls.Add(this.checkBoxAsync);
        this.groupBoxOptions.Controls.Add(this.checkBoxOverwrite);
        this.groupBoxOptions.Controls.Add(this.checkBoxPrefixNumber);
        this.groupBoxOptions.Controls.Add(this.checkBoxIncrementalDownload);
        this.groupBoxOptions.Dock = DockStyle.Top;
        this.groupBoxOptions.Height = 55;
        this.groupBoxOptions.Location = new Point(0, 90);
        this.groupBoxOptions.Name = "groupBoxOptions";
        this.groupBoxOptions.Text = "オプション";
        this.groupBoxOptions.TabIndex = 1;

        // 
        // checkBoxContent
        // 
        this.checkBoxContent.AutoSize = true;
        this.checkBoxContent.Location = new Point(15, 22);
        this.checkBoxContent.Text = "Content (txt/html)";
        this.toolTip.SetToolTip(this.checkBoxContent, "投稿のテキスト内容をダウンロード");

        // 
        // checkBoxBanner
        // 
        this.checkBoxBanner.AutoSize = true;
        this.checkBoxBanner.Location = new Point(150, 22);
        this.checkBoxBanner.Text = "Banner";
        this.toolTip.SetToolTip(this.checkBoxBanner, "バナー画像をダウンロード");

        // 
        // checkBoxAsync
        // 
        this.checkBoxAsync.AutoSize = true;
        this.checkBoxAsync.Location = new Point(230, 22);
        this.checkBoxAsync.Text = "Async";
        this.toolTip.SetToolTip(this.checkBoxAsync, "非同期ダウンロード（並列処理）");

        // 
        // checkBoxOverwrite
        // 
        this.checkBoxOverwrite.AutoSize = true;
        this.checkBoxOverwrite.Location = new Point(300, 22);
        this.checkBoxOverwrite.Text = "Overwrite";
        this.toolTip.SetToolTip(this.checkBoxOverwrite, "既存ファイルを上書き");

        // 
        // checkBoxPrefixNumber
        // 
        this.checkBoxPrefixNumber.AutoSize = true;
        this.checkBoxPrefixNumber.Location = new Point(400, 22);
        this.checkBoxPrefixNumber.Text = "Prefix Number";
        this.toolTip.SetToolTip(this.checkBoxPrefixNumber, "ファイル名に番号プレフィックスを追加");

        // 
        // checkBoxIncrementalDownload
        // 
        this.checkBoxIncrementalDownload.AutoSize = true;
        this.checkBoxIncrementalDownload.Location = new Point(520, 22);
        this.checkBoxIncrementalDownload.Text = "差分DL";
        this.toolTip.SetToolTip(this.checkBoxIncrementalDownload, "前回完了日時以降の投稿のみダウンロード");

        // 
        // groupBoxAdvanced
        // 
        this.groupBoxAdvanced.Controls.Add(this.labelMaxParallel);
        this.groupBoxAdvanced.Controls.Add(this.numericMaxParallel);
        this.groupBoxAdvanced.Controls.Add(this.labelRetry);
        this.groupBoxAdvanced.Controls.Add(this.numericRetry);
        this.groupBoxAdvanced.Controls.Add(this.labelRetryInterval);
        this.groupBoxAdvanced.Controls.Add(this.numericRetryInterval);
        this.groupBoxAdvanced.Controls.Add(this.labelRateLimit);
        this.groupBoxAdvanced.Controls.Add(this.numericRateLimit);
        this.groupBoxAdvanced.Controls.Add(this.labelTimeout);
        this.groupBoxAdvanced.Controls.Add(this.numericTimeout);
        this.groupBoxAdvanced.Controls.Add(this.labelProxy);
        this.groupBoxAdvanced.Controls.Add(this.textBoxProxy);
        this.groupBoxAdvanced.Dock = DockStyle.Top;
        this.groupBoxAdvanced.Height = 120;
        this.groupBoxAdvanced.Location = new Point(0, 145);
        this.groupBoxAdvanced.Name = "groupBoxAdvanced";
        this.groupBoxAdvanced.Text = "詳細設定";
        this.groupBoxAdvanced.TabIndex = 2;

        // 
        // labelMaxParallel
        // 
        this.labelMaxParallel.AutoSize = true;
        this.labelMaxParallel.Location = new Point(15, 25);
        this.labelMaxParallel.Text = "並列数:";

        // 
        // numericMaxParallel
        // 
        this.numericMaxParallel.Location = new Point(70, 22);
        this.numericMaxParallel.Minimum = 1;
        this.numericMaxParallel.Maximum = 10;
        this.numericMaxParallel.Size = new Size(50, 23);
        this.numericMaxParallel.Value = 3;

        // 
        // labelRetry
        // 
        this.labelRetry.AutoSize = true;
        this.labelRetry.Location = new Point(135, 25);
        this.labelRetry.Text = "Retry:";

        // 
        // numericRetry
        // 
        this.numericRetry.Location = new Point(180, 22);
        this.numericRetry.Minimum = 0;
        this.numericRetry.Maximum = 20;
        this.numericRetry.Size = new Size(50, 23);
        this.numericRetry.Value = 3;

        // 
        // labelRetryInterval
        // 
        this.labelRetryInterval.AutoSize = true;
        this.labelRetryInterval.Location = new Point(245, 25);
        this.labelRetryInterval.Text = "間隔(秒):";

        // 
        // numericRetryInterval
        // 
        this.numericRetryInterval.Location = new Point(305, 22);
        this.numericRetryInterval.Minimum = 1;
        this.numericRetryInterval.Maximum = 300;
        this.numericRetryInterval.Size = new Size(55, 23);
        this.numericRetryInterval.Value = 10;

        // 
        // labelRateLimit
        // 
        this.labelRateLimit.AutoSize = true;
        this.labelRateLimit.Location = new Point(375, 25);
        this.labelRateLimit.Text = "Rate:";

        // 
        // numericRateLimit
        // 
        this.numericRateLimit.Location = new Point(415, 22);
        this.numericRateLimit.Minimum = 1;
        this.numericRateLimit.Maximum = 20;
        this.numericRateLimit.Size = new Size(50, 23);
        this.numericRateLimit.Value = 2;
        this.toolTip.SetToolTip(this.numericRateLimit, "リクエスト/秒");

        // 
        // labelTimeout
        // 
        this.labelTimeout.AutoSize = true;
        this.labelTimeout.Location = new Point(480, 25);
        this.labelTimeout.Text = "Timeout(秒):";

        // 
        // numericTimeout
        // 
        this.numericTimeout.Location = new Point(555, 22);
        this.numericTimeout.Minimum = 60;
        this.numericTimeout.Maximum = 7200;
        this.numericTimeout.Size = new Size(70, 23);
        this.numericTimeout.Value = 1800;

        // 
        // labelProxy
        // 
        this.labelProxy.AutoSize = true;
        this.labelProxy.Location = new Point(15, 58);
        this.labelProxy.Text = "Proxy:";

        // 
        // textBoxProxy
        // 
        this.textBoxProxy.Location = new Point(70, 55);
        this.textBoxProxy.Size = new Size(400, 23);
        this.textBoxProxy.PlaceholderText = "例: socks5://proxy:1080";

        // 
        // panelExecute
        // 
        this.panelExecute.Controls.Add(this.buttonRunSelected);
        this.panelExecute.Controls.Add(this.buttonRunAll);
        this.panelExecute.Controls.Add(this.buttonCancel);
        this.panelExecute.Controls.Add(this.buttonSaveSettings);
        this.panelExecute.Dock = DockStyle.Bottom;
        this.panelExecute.Height = 45;
        this.panelExecute.Name = "panelExecute";
        this.panelExecute.TabIndex = 1;

        // 
        // buttonRunSelected
        // 
        this.buttonRunSelected.Location = new Point(10, 10);
        this.buttonRunSelected.Name = "buttonRunSelected";
        this.buttonRunSelected.Size = new Size(120, 28);
        this.buttonRunSelected.TabIndex = 0;
        this.buttonRunSelected.Text = "▶ 選択を実行";
        this.toolTip.SetToolTip(this.buttonRunSelected, "選択したアーティストをダウンロード");

        // 
        // buttonRunAll
        // 
        this.buttonRunAll.Location = new Point(140, 10);
        this.buttonRunAll.Name = "buttonRunAll";
        this.buttonRunAll.Size = new Size(120, 28);
        this.buttonRunAll.TabIndex = 1;
        this.buttonRunAll.Text = "▶▶ 全て実行";
        this.toolTip.SetToolTip(this.buttonRunAll, "有効な全アーティストを順番にダウンロード");

        // 
        // buttonCancel
        // 
        this.buttonCancel.Enabled = false;
        this.buttonCancel.Location = new Point(270, 10);
        this.buttonCancel.Name = "buttonCancel";
        this.buttonCancel.Size = new Size(100, 28);
        this.buttonCancel.TabIndex = 2;
        this.buttonCancel.Text = "■ キャンセル";

        // 
        // buttonSaveSettings
        // 
        this.buttonSaveSettings.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.buttonSaveSettings.Location = new Point(630, 10);
        this.buttonSaveSettings.Name = "buttonSaveSettings";
        this.buttonSaveSettings.Size = new Size(100, 28);
        this.buttonSaveSettings.TabIndex = 3;
        this.buttonSaveSettings.Text = "💾 設定保存";

        // 
        // richTextBoxLog
        // 
        this.richTextBoxLog.BackColor = Color.FromArgb(30, 30, 30);
        this.richTextBoxLog.Dock = DockStyle.Fill;
        this.richTextBoxLog.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
        this.richTextBoxLog.ForeColor = Color.LightGray;
        this.richTextBoxLog.Name = "richTextBoxLog";
        this.richTextBoxLog.ReadOnly = true;
        this.richTextBoxLog.TabIndex = 0;
        this.richTextBoxLog.Text = "";
        this.richTextBoxLog.WordWrap = false;

        // 
        // statusStrip
        // 
        this.statusStrip.Items.AddRange(new ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar
        });
        this.statusStrip.Location = new Point(0, 770);
        this.statusStrip.Name = "statusStrip";
        this.statusStrip.Size = new Size(1200, 30);
        this.statusStrip.TabIndex = 1;

        // 
        // toolStripStatusLabel
        // 
        this.toolStripStatusLabel.Name = "toolStripStatusLabel";
        this.toolStripStatusLabel.Size = new Size(1000, 25);
        this.toolStripStatusLabel.Spring = true;
        this.toolStripStatusLabel.Text = "準備完了";
        this.toolStripStatusLabel.TextAlign = ContentAlignment.MiddleLeft;

        // 
        // toolStripProgressBar
        // 
        this.toolStripProgressBar.Name = "toolStripProgressBar";
        this.toolStripProgressBar.Size = new Size(150, 24);
        this.toolStripProgressBar.Visible = false;

        // 
        // contextMenuStripArtist
        // 
        this.contextMenuStripArtist.Items.AddRange(new ToolStripItem[] {
            this.toolStripMenuItemRun,
            this.toolStripMenuItemEdit,
            this.toolStripMenuItemDelete,
            new ToolStripSeparator(),
            this.toolStripMenuItemOpenFolder,
            this.toolStripMenuItemCopyUrl,
            new ToolStripSeparator(),
            this.toolStripMenuItemResetDate
        });
        this.contextMenuStripArtist.Name = "contextMenuStripArtist";

        // 
        // toolStripMenuItemRun
        // 
        this.toolStripMenuItemRun.Name = "toolStripMenuItemRun";
        this.toolStripMenuItemRun.Text = "実行 (&R)";

        // 
        // toolStripMenuItemEdit
        // 
        this.toolStripMenuItemEdit.Name = "toolStripMenuItemEdit";
        this.toolStripMenuItemEdit.Text = "編集 (&E)";

        // 
        // toolStripMenuItemDelete
        // 
        this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
        this.toolStripMenuItemDelete.Text = "削除 (&D)";

        // 
        // toolStripMenuItemOpenFolder
        // 
        this.toolStripMenuItemOpenFolder.Name = "toolStripMenuItemOpenFolder";
        this.toolStripMenuItemOpenFolder.Text = "フォルダを開く (&O)";

        // 
        // toolStripMenuItemCopyUrl
        // 
        this.toolStripMenuItemCopyUrl.Name = "toolStripMenuItemCopyUrl";
        this.toolStripMenuItemCopyUrl.Text = "URLをコピー (&C)";

        // 
        // toolStripMenuItemResetDate
        // 
        this.toolStripMenuItemResetDate.Name = "toolStripMenuItemResetDate";
        this.toolStripMenuItemResetDate.Text = "完了日時をリセット";

        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(1200, 800);
        this.Controls.Add(this.splitContainerMain);
        this.Controls.Add(this.statusStrip);
        this.MinimumSize = new Size(800, 600);
        this.Name = "MainForm";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "Kemono Scraper GUI";

        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewArtists)).EndInit();
        this.splitContainerMain.Panel1.ResumeLayout(false);
        this.splitContainerMain.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
        this.splitContainerMain.ResumeLayout(false);
        this.splitContainerLeft.Panel1.ResumeLayout(false);
        this.splitContainerLeft.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeft)).EndInit();
        this.splitContainerLeft.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.numericMaxParallel)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.numericRetry)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.numericRetryInterval)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.numericRateLimit)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.numericTimeout)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    // コントロール宣言
    private SplitContainer splitContainerMain;
    private SplitContainer splitContainerLeft;
    private DataGridView dataGridViewArtists;
    private Panel panelArtistButtons;
    private Button buttonAddArtist;
    private Button buttonEditArtist;
    private Button buttonDeleteArtist;
    private Button buttonImportBatch;
    private Button buttonUrlExtractor;
    private TextBox textBoxAddUrl;
    private Button buttonAddFromUrl;
    private Panel panelSettings;
    private GroupBox groupBoxPaths;
    private Label labelScraperPath;
    private TextBox textBoxScraperPath;
    private Button buttonBrowseScraper;
    private Label labelOutputPath;
    private TextBox textBoxOutputPath;
    private Button buttonBrowseOutput;
    private GroupBox groupBoxOptions;
    private CheckBox checkBoxContent;
    private CheckBox checkBoxBanner;
    private CheckBox checkBoxAsync;
    private CheckBox checkBoxOverwrite;
    private CheckBox checkBoxPrefixNumber;
    private CheckBox checkBoxIncrementalDownload;
    private GroupBox groupBoxAdvanced;
    private Label labelMaxParallel;
    private NumericUpDown numericMaxParallel;
    private Label labelRetry;
    private NumericUpDown numericRetry;
    private Label labelRetryInterval;
    private NumericUpDown numericRetryInterval;
    private Label labelRateLimit;
    private NumericUpDown numericRateLimit;
    private Label labelTimeout;
    private NumericUpDown numericTimeout;
    private Label labelProxy;
    private TextBox textBoxProxy;
    private Panel panelExecute;
    private Button buttonRunSelected;
    private Button buttonRunAll;
    private Button buttonCancel;
    private Button buttonSaveSettings;
    private RichTextBox richTextBoxLog;
    private StatusStrip statusStrip;
    private ToolStripStatusLabel toolStripStatusLabel;
    private ToolStripProgressBar toolStripProgressBar;
    private ToolTip toolTip;
    private ContextMenuStrip contextMenuStripArtist;
    private ToolStripMenuItem toolStripMenuItemRun;
    private ToolStripMenuItem toolStripMenuItemEdit;
    private ToolStripMenuItem toolStripMenuItemDelete;
    private ToolStripMenuItem toolStripMenuItemOpenFolder;
    private ToolStripMenuItem toolStripMenuItemCopyUrl;
    private ToolStripMenuItem toolStripMenuItemResetDate;
}
