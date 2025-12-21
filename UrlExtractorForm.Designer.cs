namespace KemonoScraperGUI;

partial class UrlExtractorForm
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

        this.panelTop = new Panel();
        this.labelFolder = new Label();
        this.textBoxFolder = new TextBox();
        this.buttonBrowse = new Button();
        this.checkBoxExternalOnly = new CheckBox();
        this.buttonExtract = new Button();
        this.buttonExportCsv = new Button();
        this.buttonCopyUrls = new Button();

        this.dataGridViewUrls = new DataGridView();
        this.statusStrip = new StatusStrip();
        this.toolStripStatusLabel = new ToolStripStatusLabel();

        this.toolTip = new ToolTip(this.components);
        this.contextMenuStrip = new ContextMenuStrip(this.components);
        this.toolStripMenuItemCopyUrl = new ToolStripMenuItem();
        this.toolStripMenuItemCopyPassword = new ToolStripMenuItem();
        this.toolStripMenuItemOpenUrl = new ToolStripMenuItem();
        this.toolStripMenuItemOpenFolder = new ToolStripMenuItem();

        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUrls)).BeginInit();
        this.SuspendLayout();

        // 
        // panelTop
        // 
        this.panelTop.Controls.Add(this.labelFolder);
        this.panelTop.Controls.Add(this.textBoxFolder);
        this.panelTop.Controls.Add(this.buttonBrowse);
        this.panelTop.Controls.Add(this.checkBoxExternalOnly);
        this.panelTop.Controls.Add(this.buttonExtract);
        this.panelTop.Controls.Add(this.buttonExportCsv);
        this.panelTop.Controls.Add(this.buttonCopyUrls);
        this.panelTop.Dock = DockStyle.Top;
        this.panelTop.Height = 80;
        this.panelTop.Name = "panelTop";

        // 
        // labelFolder
        // 
        this.labelFolder.AutoSize = true;
        this.labelFolder.Location = new Point(10, 15);
        this.labelFolder.Text = "検索フォルダ:";

        // 
        // textBoxFolder
        // 
        this.textBoxFolder.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        this.textBoxFolder.Location = new Point(95, 12);
        this.textBoxFolder.Size = new Size(550, 23);

        // 
        // buttonBrowse
        // 
        this.buttonBrowse.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        this.buttonBrowse.Location = new Point(650, 11);
        this.buttonBrowse.Size = new Size(80, 25);
        this.buttonBrowse.Text = "参照...";

        // 
        // checkBoxExternalOnly
        // 
        this.checkBoxExternalOnly.AutoSize = true;
        this.checkBoxExternalOnly.Checked = true;
        this.checkBoxExternalOnly.CheckState = CheckState.Checked;
        this.checkBoxExternalOnly.Location = new Point(95, 45);
        this.checkBoxExternalOnly.Text = "外部ストレージのURLのみ抽出 (Mega, GDrive, Dropbox等)";
        this.toolTip.SetToolTip(this.checkBoxExternalOnly, "チェックを外すと全てのURLを抽出します");

        // 
        // buttonExtract
        // 
        this.buttonExtract.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        this.buttonExtract.Location = new Point(480, 42);
        this.buttonExtract.Size = new Size(100, 28);
        this.buttonExtract.Text = "🔍 抽出開始";

        // 
        // buttonExportCsv
        // 
        this.buttonExportCsv.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        this.buttonExportCsv.Enabled = false;
        this.buttonExportCsv.Location = new Point(585, 42);
        this.buttonExportCsv.Size = new Size(100, 28);
        this.buttonExportCsv.Text = "📄 CSV出力";

        // 
        // buttonCopyUrls
        // 
        this.buttonCopyUrls.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        this.buttonCopyUrls.Enabled = false;
        this.buttonCopyUrls.Location = new Point(690, 42);
        this.buttonCopyUrls.Size = new Size(100, 28);
        this.buttonCopyUrls.Text = "📋 URLコピー";
        this.toolTip.SetToolTip(this.buttonCopyUrls, "全URLをクリップボードにコピー");

        // 
        // dataGridViewUrls
        // 
        this.dataGridViewUrls.AllowUserToAddRows = false;
        this.dataGridViewUrls.AllowUserToDeleteRows = false;
        this.dataGridViewUrls.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        this.dataGridViewUrls.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridViewUrls.ContextMenuStrip = this.contextMenuStrip;
        this.dataGridViewUrls.Dock = DockStyle.Fill;
        this.dataGridViewUrls.Location = new Point(0, 80);
        this.dataGridViewUrls.Name = "dataGridViewUrls";
        this.dataGridViewUrls.ReadOnly = true;
        this.dataGridViewUrls.RowHeadersWidth = 25;
        this.dataGridViewUrls.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        this.dataGridViewUrls.TabIndex = 1;

        // 
        // statusStrip
        // 
        this.statusStrip.Items.AddRange(new ToolStripItem[] { this.toolStripStatusLabel });
        this.statusStrip.Location = new Point(0, 528);
        this.statusStrip.Name = "statusStrip";

        // 
        // toolStripStatusLabel
        // 
        this.toolStripStatusLabel.Name = "toolStripStatusLabel";
        this.toolStripStatusLabel.Size = new Size(700, 17);
        this.toolStripStatusLabel.Spring = true;
        this.toolStripStatusLabel.Text = "フォルダを選択して「抽出開始」をクリックしてください";
        this.toolStripStatusLabel.TextAlign = ContentAlignment.MiddleLeft;

        // 
        // contextMenuStrip
        // 
        this.contextMenuStrip.Items.AddRange(new ToolStripItem[] {
            this.toolStripMenuItemCopyUrl,
            this.toolStripMenuItemCopyPassword,
            new ToolStripSeparator(),
            this.toolStripMenuItemOpenUrl,
            this.toolStripMenuItemOpenFolder
        });
        this.contextMenuStrip.Name = "contextMenuStrip";

        // 
        // toolStripMenuItemCopyUrl
        // 
        this.toolStripMenuItemCopyUrl.Name = "toolStripMenuItemCopyUrl";
        this.toolStripMenuItemCopyUrl.Text = "URLをコピー (&C)";

        // 
        // toolStripMenuItemCopyPassword
        // 
        this.toolStripMenuItemCopyPassword.Name = "toolStripMenuItemCopyPassword";
        this.toolStripMenuItemCopyPassword.Text = "パスワードをコピー (&P)";

        // 
        // toolStripMenuItemOpenUrl
        // 
        this.toolStripMenuItemOpenUrl.Name = "toolStripMenuItemOpenUrl";
        this.toolStripMenuItemOpenUrl.Text = "URLを開く (&O)";

        // 
        // toolStripMenuItemOpenFolder
        // 
        this.toolStripMenuItemOpenFolder.Name = "toolStripMenuItemOpenFolder";
        this.toolStripMenuItemOpenFolder.Text = "フォルダを開く (&F)";

        // 
        // UrlExtractorForm
        // 
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(800, 550);
        this.Controls.Add(this.dataGridViewUrls);
        this.Controls.Add(this.panelTop);
        this.Controls.Add(this.statusStrip);
        this.MinimumSize = new Size(600, 400);
        this.Name = "UrlExtractorForm";
        this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "URL抽出ツール";

        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUrls)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private Panel panelTop;
    private Label labelFolder;
    private TextBox textBoxFolder;
    private Button buttonBrowse;
    private CheckBox checkBoxExternalOnly;
    private Button buttonExtract;
    private Button buttonExportCsv;
    private Button buttonCopyUrls;
    private DataGridView dataGridViewUrls;
    private StatusStrip statusStrip;
    private ToolStripStatusLabel toolStripStatusLabel;
    private ToolTip toolTip;
    private ContextMenuStrip contextMenuStrip;
    private ToolStripMenuItem toolStripMenuItemCopyUrl;
    private ToolStripMenuItem toolStripMenuItemCopyPassword;
    private ToolStripMenuItem toolStripMenuItemOpenUrl;
    private ToolStripMenuItem toolStripMenuItemOpenFolder;
}
