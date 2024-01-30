namespace ICP.OA.UI
{
    partial class FormUpgradeCloud
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
            this.btnStart = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.barUpgradeProgress = new DevExpress.XtraEditors.ProgressBarControl();
            this.txtUpgradeState = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.barUpgradeProgress.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(371, 14);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(452, 14);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            // 
            // barUpgradeProgress
            // 
            this.barUpgradeProgress.Location = new System.Drawing.Point(14, 14);
            this.barUpgradeProgress.Name = "barUpgradeProgress";
            this.barUpgradeProgress.Size = new System.Drawing.Size(351, 26);
            this.barUpgradeProgress.TabIndex = 1;
            // 
            // txtUpgradeState
            // 
            this.txtUpgradeState.Location = new System.Drawing.Point(14, 55);
            this.txtUpgradeState.Multiline = true;
            this.txtUpgradeState.Name = "txtUpgradeState";
            this.txtUpgradeState.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtUpgradeState.Size = new System.Drawing.Size(513, 148);
            this.txtUpgradeState.TabIndex = 2;
            // 
            // FormUpgradeCloud
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtUpgradeState);
            this.Controls.Add(this.barUpgradeProgress);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnStart);
            this.Name = "FormUpgradeCloud";
            this.Size = new System.Drawing.Size(540, 216);
            ((System.ComponentModel.ISupportInitialize)(this.barUpgradeProgress.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnStart;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.ProgressBarControl barUpgradeProgress;
        private System.Windows.Forms.TextBox txtUpgradeState;
    }
}