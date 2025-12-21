namespace KemonoScraperGUI;

partial class ArtistEditForm
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
        this.labelService = new Label();
        this.comboBoxService = new ComboBox();
        this.labelCreatorId = new Label();
        this.textBoxCreatorId = new TextBox();
        this.labelCreatorName = new Label();
        this.textBoxCreatorName = new TextBox();
        this.labelNotes = new Label();
        this.textBoxNotes = new TextBox();
        this.checkBoxEnabled = new CheckBox();
        this.buttonOK = new Button();
        this.buttonCancel = new Button();
        this.groupBoxInfo = new GroupBox();
        this.labelLastCompleted = new Label();
        this.textBoxLastCompleted = new TextBox();
        this.labelLastStatus = new Label();
        this.textBoxLastStatus = new TextBox();

        this.SuspendLayout();

        // 
        // labelService
        // 
        this.labelService.AutoSize = true;
        this.labelService.Location = new Point(20, 25);
        this.labelService.Text = "サービス:";

        // 
        // comboBoxService
        // 
        this.comboBoxService.DropDownStyle = ComboBoxStyle.DropDownList;
        this.comboBoxService.Items.AddRange(new object[] {
            "patreon",
            "fanbox",
            "fantia",
            "gumroad",
            "subscribestar",
            "dlsite",
            "discord",
            "boosty",
            "afdian"
        });
        this.comboBoxService.Location = new Point(100, 22);
        this.comboBoxService.Size = new Size(200, 23);
        this.comboBoxService.TabIndex = 0;

        // 
        // labelCreatorId
        // 
        this.labelCreatorId.AutoSize = true;
        this.labelCreatorId.Location = new Point(20, 60);
        this.labelCreatorId.Text = "クリエイターID:";

        // 
        // textBoxCreatorId
        // 
        this.textBoxCreatorId.Location = new Point(100, 57);
        this.textBoxCreatorId.Size = new Size(200, 23);
        this.textBoxCreatorId.TabIndex = 1;

        // 
        // labelCreatorName
        // 
        this.labelCreatorName.AutoSize = true;
        this.labelCreatorName.Location = new Point(20, 95);
        this.labelCreatorName.Text = "名前 (任意):";

        // 
        // textBoxCreatorName
        // 
        this.textBoxCreatorName.Location = new Point(100, 92);
        this.textBoxCreatorName.Size = new Size(200, 23);
        this.textBoxCreatorName.TabIndex = 2;
        this.textBoxCreatorName.PlaceholderText = "表示用の名前";

        // 
        // labelNotes
        // 
        this.labelNotes.AutoSize = true;
        this.labelNotes.Location = new Point(20, 130);
        this.labelNotes.Text = "メモ:";

        // 
        // textBoxNotes
        // 
        this.textBoxNotes.Location = new Point(100, 127);
        this.textBoxNotes.Size = new Size(200, 23);
        this.textBoxNotes.TabIndex = 3;

        // 
        // checkBoxEnabled
        // 
        this.checkBoxEnabled.AutoSize = true;
        this.checkBoxEnabled.Checked = true;
        this.checkBoxEnabled.CheckState = CheckState.Checked;
        this.checkBoxEnabled.Location = new Point(100, 162);
        this.checkBoxEnabled.Text = "有効";
        this.checkBoxEnabled.TabIndex = 4;

        // 
        // groupBoxInfo
        // 
        this.groupBoxInfo.Controls.Add(this.labelLastCompleted);
        this.groupBoxInfo.Controls.Add(this.textBoxLastCompleted);
        this.groupBoxInfo.Controls.Add(this.labelLastStatus);
        this.groupBoxInfo.Controls.Add(this.textBoxLastStatus);
        this.groupBoxInfo.Location = new Point(20, 195);
        this.groupBoxInfo.Size = new Size(280, 85);
        this.groupBoxInfo.Text = "実行情報";

        // 
        // labelLastCompleted
        // 
        this.labelLastCompleted.AutoSize = true;
        this.labelLastCompleted.Location = new Point(10, 25);
        this.labelLastCompleted.Text = "最終完了:";

        // 
        // textBoxLastCompleted
        // 
        this.textBoxLastCompleted.Location = new Point(80, 22);
        this.textBoxLastCompleted.Size = new Size(180, 23);
        this.textBoxLastCompleted.ReadOnly = true;
        this.textBoxLastCompleted.BackColor = SystemColors.Control;

        // 
        // labelLastStatus
        // 
        this.labelLastStatus.AutoSize = true;
        this.labelLastStatus.Location = new Point(10, 55);
        this.labelLastStatus.Text = "状態:";

        // 
        // textBoxLastStatus
        // 
        this.textBoxLastStatus.Location = new Point(80, 52);
        this.textBoxLastStatus.Size = new Size(180, 23);
        this.textBoxLastStatus.ReadOnly = true;
        this.textBoxLastStatus.BackColor = SystemColors.Control;

        // 
        // buttonOK
        // 
        this.buttonOK.Location = new Point(110, 295);
        this.buttonOK.Size = new Size(85, 30);
        this.buttonOK.TabIndex = 5;
        this.buttonOK.Text = "OK";

        // 
        // buttonCancel
        // 
        this.buttonCancel.DialogResult = DialogResult.Cancel;
        this.buttonCancel.Location = new Point(205, 295);
        this.buttonCancel.Size = new Size(85, 30);
        this.buttonCancel.TabIndex = 6;
        this.buttonCancel.Text = "キャンセル";

        // 
        // ArtistEditForm
        // 
        this.AcceptButton = this.buttonOK;
        this.CancelButton = this.buttonCancel;
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(320, 340);
        this.Controls.Add(this.labelService);
        this.Controls.Add(this.comboBoxService);
        this.Controls.Add(this.labelCreatorId);
        this.Controls.Add(this.textBoxCreatorId);
        this.Controls.Add(this.labelCreatorName);
        this.Controls.Add(this.textBoxCreatorName);
        this.Controls.Add(this.labelNotes);
        this.Controls.Add(this.textBoxNotes);
        this.Controls.Add(this.checkBoxEnabled);
        this.Controls.Add(this.groupBoxInfo);
        this.Controls.Add(this.buttonOK);
        this.Controls.Add(this.buttonCancel);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "ArtistEditForm";
        this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "アーティスト";

        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private Label labelService;
    private ComboBox comboBoxService;
    private Label labelCreatorId;
    private TextBox textBoxCreatorId;
    private Label labelCreatorName;
    private TextBox textBoxCreatorName;
    private Label labelNotes;
    private TextBox textBoxNotes;
    private CheckBox checkBoxEnabled;
    private GroupBox groupBoxInfo;
    private Label labelLastCompleted;
    private TextBox textBoxLastCompleted;
    private Label labelLastStatus;
    private TextBox textBoxLastStatus;
    private Button buttonOK;
    private Button buttonCancel;
}
