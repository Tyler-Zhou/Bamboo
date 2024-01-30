namespace ICP.MailCenter.UI
{
    partial class AttachmentsControl
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
            this.picAtt = new System.Windows.Forms.PictureBox();
            this.lnkAtt = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.picAtt)).BeginInit();
            this.SuspendLayout();
            // 
            // picAtt
            // 
            this.picAtt.Location = new System.Drawing.Point(0, -1);
            this.picAtt.Name = "picAtt";
            this.picAtt.Size = new System.Drawing.Size(17, 17);
            this.picAtt.TabIndex = 0;
            this.picAtt.TabStop = false;
            // 
            // lnkAtt
            // 
            this.lnkAtt.ActiveLinkColor = System.Drawing.Color.Blue;
            this.lnkAtt.AllowDrop = true;
            this.lnkAtt.AutoSize = true;
            this.lnkAtt.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkAtt.LinkColor = System.Drawing.SystemColors.ControlText;
            this.lnkAtt.Location = new System.Drawing.Point(15, 1);
            this.lnkAtt.Name = "lnkAtt";
            this.lnkAtt.Size = new System.Drawing.Size(65, 12);
            this.lnkAtt.TabIndex = 1;
            this.lnkAtt.TabStop = true;
            this.lnkAtt.Text = "Attachment";
            this.lnkAtt.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lnkAtt_MouseDoubleClick);
            this.lnkAtt.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lnkAtt_MouseClick);
            this.lnkAtt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lnkAtt_MouseDown);
            this.lnkAtt.DragLeave += new System.EventHandler(this.lnkAtt_DragLeave);
            this.lnkAtt.DragEnter += new System.Windows.Forms.DragEventHandler(this.lnkAtt_DragEnter);
            // 
            // AttachmentsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picAtt);
            this.Controls.Add(this.lnkAtt);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "AttachmentsControl";
            this.Size = new System.Drawing.Size(83, 16);
            ((System.ComponentModel.ISupportInitialize)(this.picAtt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
       

        #endregion

        private System.Windows.Forms.PictureBox picAtt;
        public System.Windows.Forms.LinkLabel lnkAtt;
    }
}
