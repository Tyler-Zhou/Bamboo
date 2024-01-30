namespace ICP.MailCenter.CommonUI
{
    partial class UCFaxAttachment
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.attachmentLabel = new ICP.MailCenter.CommonUI.AttachmentLabel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attachmentLabel.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(25, 23);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // attachmentLabel
            // 
            this.attachmentLabel.AttachmentContent = null;
            this.attachmentLabel.CommunicationHistoryService = null;
            this.attachmentLabel.Context = null;
            this.attachmentLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.attachmentLabel.Location = new System.Drawing.Point(25, 0);
            this.attachmentLabel.Margin = new System.Windows.Forms.Padding(0);
            this.attachmentLabel.MaximumSize = new System.Drawing.Size(800, 60);
            this.attachmentLabel.MinimumSize = new System.Drawing.Size(50, 21);
            this.attachmentLabel.Name = "attachmentLabel";
            this.attachmentLabel.Properties.Appearance.Options.UseFont = true;
            this.attachmentLabel.Size = new System.Drawing.Size(202, 21);
            this.attachmentLabel.TabIndex = 1;
            // 
            // UCFaxAttachment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.attachmentLabel);
            this.Controls.Add(this.pictureBox);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(227, 23);
            this.Name = "UCFaxAttachment";
            this.Size = new System.Drawing.Size(227, 23);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attachmentLabel.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private AttachmentLabel attachmentLabel;
    }
}
