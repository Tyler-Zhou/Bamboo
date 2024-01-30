namespace ICP.MailCenter.UI
{
    partial class frmShowProgressBar
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.procBar = new System.Windows.Forms.ProgressBar();
            this.lblMessage = new System.Windows.Forms.Label();
            this.pnlProgress = new System.Windows.Forms.Panel();
            this.pnlProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // procBar
            // 
            this.procBar.Location = new System.Drawing.Point(25, 52);
            this.procBar.Minimum = 1;
            this.procBar.Name = "procBar";
            this.procBar.Size = new System.Drawing.Size(350, 23);
            this.procBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.procBar.TabIndex = 0;
            this.procBar.Value = 1;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(24, 22);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(347, 12);
            this.lblMessage.TabIndex = 3;
            this.lblMessage.Text = "共: 0封邮件,正在移动邮件至文件夹:\"收件箱\",当前进度为: 0%.";
            // 
            // pnlProgress
            // 
            this.pnlProgress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlProgress.Controls.Add(this.lblMessage);
            this.pnlProgress.Controls.Add(this.procBar);
            this.pnlProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProgress.Location = new System.Drawing.Point(0, 0);
            this.pnlProgress.Name = "pnlProgress";
            this.pnlProgress.Size = new System.Drawing.Size(417, 97);
            this.pnlProgress.TabIndex = 4;
            // 
            // frmShowProgressBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(417, 97);
            this.Controls.Add(this.pnlProgress);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmShowProgressBar";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "移动至文件夹:\" \"";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmProcess_FormClosing);
            this.pnlProgress.ResumeLayout(false);
            this.pnlProgress.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar procBar;
        public  System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Panel pnlProgress;
    }
}

