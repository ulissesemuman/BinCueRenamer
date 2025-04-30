namespace BinRenamer
{
    partial class frmExecutionLog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnClose = new Button();
            txtExecutionLog = new TextBox();
            SuspendLayout();
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top;
            btnClose.Location = new Point(364, 489);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(70, 20);
            btnClose.TabIndex = 1;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // txtExecutionLog
            // 
            txtExecutionLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtExecutionLog.Location = new Point(12, 12);
            txtExecutionLog.Multiline = true;
            txtExecutionLog.Name = "txtExecutionLog";
            txtExecutionLog.ReadOnly = true;
            txtExecutionLog.ScrollBars = ScrollBars.Both;
            txtExecutionLog.Size = new Size(771, 459);
            txtExecutionLog.TabIndex = 0;
            // 
            // frmExecutionLog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(795, 523);
            Controls.Add(txtExecutionLog);
            Controls.Add(btnClose);
            Name = "frmExecutionLog";
            Text = "Execution Log";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnClose;
        private TextBox txtExecutionLog;
    }
}