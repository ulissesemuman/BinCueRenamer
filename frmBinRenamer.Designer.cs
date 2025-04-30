namespace BinRenamer
{
    partial class frmBinRenamer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label4 = new Label();
            txtFolderName = new TextBox();
            chkRecursive = new CheckBox();
            btnFolderName = new Button();
            btnRename = new Button();
            btnExit = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            groupBox1 = new GroupBox();
            label6 = new Label();
            txtNewWordDelimiter = new TextBox();
            txtWordDelimiter = new TextBox();
            label1 = new Label();
            groupBox2 = new GroupBox();
            label9 = new Label();
            label8 = new Label();
            txtCurrentTrackText = new TextBox();
            txtCurrentDiscText = new TextBox();
            chkSingleTrackText = new CheckBox();
            chkTrackLeadingZeroes = new CheckBox();
            chkDiscLeadingZeroes = new CheckBox();
            txtTrackNaming = new TextBox();
            txtDiscNaming = new TextBox();
            label5 = new Label();
            label3 = new Label();
            groupBox3 = new GroupBox();
            label7 = new Label();
            cmbBaseName = new ComboBox();
            cmbRegionFormat = new ComboBox();
            label2 = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(18, 18);
            label4.Name = "label4";
            label4.Size = new Size(73, 15);
            label4.TabIndex = 3;
            label4.Text = "Folder name";
            // 
            // txtFolderName
            // 
            txtFolderName.Location = new Point(105, 20);
            txtFolderName.Name = "txtFolderName";
            txtFolderName.Size = new Size(361, 23);
            txtFolderName.TabIndex = 11;
            // 
            // chkRecursive
            // 
            chkRecursive.AutoSize = true;
            chkRecursive.Location = new Point(502, 24);
            chkRecursive.Name = "chkRecursive";
            chkRecursive.Size = new Size(76, 19);
            chkRecursive.TabIndex = 12;
            chkRecursive.Text = "Recursive";
            chkRecursive.UseVisualStyleBackColor = true;
            // 
            // btnFolderName
            // 
            btnFolderName.Location = new Point(472, 20);
            btnFolderName.Name = "btnFolderName";
            btnFolderName.Size = new Size(24, 23);
            btnFolderName.TabIndex = 13;
            btnFolderName.Text = "...";
            btnFolderName.UseVisualStyleBackColor = true;
            btnFolderName.Click += btnFolderName_Click;
            // 
            // btnRename
            // 
            btnRename.Location = new Point(160, 262);
            btnRename.Name = "btnRename";
            btnRename.Size = new Size(75, 23);
            btnRename.TabIndex = 14;
            btnRename.Text = "Rename";
            btnRename.UseVisualStyleBackColor = true;
            btnRename.Click += btnRename_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(340, 262);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(75, 23);
            btnExit.TabIndex = 15;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(txtNewWordDelimiter);
            groupBox1.Controls.Add(txtWordDelimiter);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(309, 160);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(276, 86);
            groupBox1.TabIndex = 20;
            groupBox1.TabStop = false;
            groupBox1.Text = "Word Delimiter";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 58);
            label6.Name = "label6";
            label6.Size = new Size(31, 15);
            label6.TabIndex = 23;
            label6.Text = "New";
            // 
            // txtNewWordDelimiter
            // 
            txtNewWordDelimiter.Location = new Point(74, 54);
            txtNewWordDelimiter.Name = "txtNewWordDelimiter";
            txtNewWordDelimiter.Size = new Size(143, 23);
            txtNewWordDelimiter.TabIndex = 22;
            // 
            // txtWordDelimiter
            // 
            txtWordDelimiter.Location = new Point(74, 23);
            txtWordDelimiter.Name = "txtWordDelimiter";
            txtWordDelimiter.Size = new Size(143, 23);
            txtWordDelimiter.TabIndex = 21;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 28);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 20;
            label1.Text = "Current";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(txtCurrentTrackText);
            groupBox2.Controls.Add(txtCurrentDiscText);
            groupBox2.Controls.Add(chkSingleTrackText);
            groupBox2.Controls.Add(chkTrackLeadingZeroes);
            groupBox2.Controls.Add(chkDiscLeadingZeroes);
            groupBox2.Controls.Add(txtTrackNaming);
            groupBox2.Controls.Add(txtDiscNaming);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label3);
            groupBox2.Location = new Point(18, 59);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(285, 187);
            groupBox2.TabIndex = 21;
            groupBox2.TabStop = false;
            groupBox2.Text = "Naming";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(10, 158);
            label9.Name = "label9";
            label9.Size = new Size(102, 15);
            label9.TabIndex = 27;
            label9.Text = "Current Track Text";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(10, 129);
            label8.Name = "label8";
            label8.Size = new Size(96, 15);
            label8.TabIndex = 26;
            label8.Text = "Current Disc Text";
            // 
            // txtCurrentTrackText
            // 
            txtCurrentTrackText.Location = new Point(117, 155);
            txtCurrentTrackText.Name = "txtCurrentTrackText";
            txtCurrentTrackText.Size = new Size(100, 23);
            txtCurrentTrackText.TabIndex = 25;
            txtCurrentTrackText.Text = "Track";
            txtCurrentTrackText.Leave += txtCurrentTrackText_Leave;
            // 
            // txtCurrentDiscText
            // 
            txtCurrentDiscText.Location = new Point(117, 126);
            txtCurrentDiscText.Name = "txtCurrentDiscText";
            txtCurrentDiscText.Size = new Size(100, 23);
            txtCurrentDiscText.TabIndex = 24;
            txtCurrentDiscText.Text = "Disc,Disk,CD,$n";
            // 
            // chkSingleTrackText
            // 
            chkSingleTrackText.AutoSize = true;
            chkSingleTrackText.Location = new Point(10, 95);
            chkSingleTrackText.Name = "chkSingleTrackText";
            chkSingleTrackText.Size = new Size(113, 19);
            chkSingleTrackText.TabIndex = 23;
            chkSingleTrackText.Text = "Single Track Text";
            chkSingleTrackText.UseVisualStyleBackColor = true;
            // 
            // chkTrackLeadingZeroes
            // 
            chkTrackLeadingZeroes.AutoSize = true;
            chkTrackLeadingZeroes.Checked = true;
            chkTrackLeadingZeroes.CheckState = CheckState.Checked;
            chkTrackLeadingZeroes.Location = new Point(175, 58);
            chkTrackLeadingZeroes.Name = "chkTrackLeadingZeroes";
            chkTrackLeadingZeroes.Size = new Size(104, 19);
            chkTrackLeadingZeroes.TabIndex = 16;
            chkTrackLeadingZeroes.Text = "Leading zeroes";
            chkTrackLeadingZeroes.UseVisualStyleBackColor = true;
            // 
            // chkDiscLeadingZeroes
            // 
            chkDiscLeadingZeroes.AutoSize = true;
            chkDiscLeadingZeroes.Checked = true;
            chkDiscLeadingZeroes.CheckState = CheckState.Checked;
            chkDiscLeadingZeroes.Location = new Point(175, 28);
            chkDiscLeadingZeroes.Name = "chkDiscLeadingZeroes";
            chkDiscLeadingZeroes.Size = new Size(104, 19);
            chkDiscLeadingZeroes.TabIndex = 15;
            chkDiscLeadingZeroes.Text = "Leading zeroes";
            chkDiscLeadingZeroes.UseVisualStyleBackColor = true;
            // 
            // txtTrackNaming
            // 
            txtTrackNaming.Location = new Point(73, 57);
            txtTrackNaming.Name = "txtTrackNaming";
            txtTrackNaming.Size = new Size(96, 23);
            txtTrackNaming.TabIndex = 14;
            txtTrackNaming.Text = "(Track %1)";
            // 
            // txtDiscNaming
            // 
            txtDiscNaming.Location = new Point(73, 25);
            txtDiscNaming.Name = "txtDiscNaming";
            txtDiscNaming.Size = new Size(96, 23);
            txtDiscNaming.TabIndex = 13;
            txtDiscNaming.Text = "(Disc %1)";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(10, 58);
            label5.Name = "label5";
            label5.Size = new Size(66, 15);
            label5.TabIndex = 12;
            label5.Text = "Track Mask";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 28);
            label3.Name = "label3";
            label3.Size = new Size(60, 15);
            label3.TabIndex = 11;
            label3.Text = "Disc Mask";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(cmbBaseName);
            groupBox3.Controls.Add(cmbRegionFormat);
            groupBox3.Controls.Add(label2);
            groupBox3.Location = new Point(309, 59);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(276, 91);
            groupBox3.TabIndex = 22;
            groupBox3.TabStop = false;
            groupBox3.Text = "Pattern";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(21, 24);
            label7.Name = "label7";
            label7.Size = new Size(64, 15);
            label7.TabIndex = 28;
            label7.Text = "Base name";
            // 
            // cmbBaseName
            // 
            cmbBaseName.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBaseName.FormattingEnabled = true;
            cmbBaseName.Items.AddRange(new object[] { "CUE", "Folder" });
            cmbBaseName.Location = new Point(112, 21);
            cmbBaseName.Name = "cmbBaseName";
            cmbBaseName.Size = new Size(142, 23);
            cmbBaseName.TabIndex = 27;
            // 
            // cmbRegionFormat
            // 
            cmbRegionFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRegionFormat.FormattingEnabled = true;
            cmbRegionFormat.Items.AddRange(new object[] { "Region name", "Two letters acronym" });
            cmbRegionFormat.Location = new Point(112, 50);
            cmbRegionFormat.Name = "cmbRegionFormat";
            cmbRegionFormat.Size = new Size(142, 23);
            cmbRegionFormat.TabIndex = 26;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(21, 53);
            label2.Name = "label2";
            label2.Size = new Size(85, 15);
            label2.TabIndex = 25;
            label2.Text = "Region Format";
            // 
            // frmBinRenamer
            // 
            AcceptButton = btnRename;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnExit;
            ClientSize = new Size(603, 298);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(btnExit);
            Controls.Add(btnRename);
            Controls.Add(btnFolderName);
            Controls.Add(chkRecursive);
            Controls.Add(txtFolderName);
            Controls.Add(label4);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "frmBinRenamer";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bin/Cue Renamer";
            Load += frmBinRenamer_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label4;
        private TextBox txtFolderName;
        private CheckBox chkRecursive;
        private Button btnFolderName;
        private Button btnRename;
        private Button btnExit;
        private FolderBrowserDialog folderBrowserDialog1;
        private GroupBox groupBox1;
        private Label label6;
        private TextBox txtNewWordDelimiter;
        private TextBox txtWordDelimiter;
        private Label label1;
        private GroupBox groupBox2;
        private CheckBox chkTrackLeadingZeroes;
        private CheckBox chkDiscLeadingZeroes;
        private TextBox txtTrackNaming;
        private TextBox txtDiscNaming;
        private Label label5;
        private Label label3;
        private GroupBox groupBox3;
        private Label label7;
        private ComboBox cmbBaseName;
        private ComboBox cmbRegionFormat;
        private Label label2;
        private CheckBox chkSingleTrackText;
        private Label label9;
        private Label label8;
        private TextBox txtCurrentTrackText;
        private TextBox txtCurrentDiscText;
    }
}
