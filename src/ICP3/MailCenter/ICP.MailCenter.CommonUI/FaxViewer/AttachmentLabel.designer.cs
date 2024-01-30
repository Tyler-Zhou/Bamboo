namespace ICP.MailCenter.CommonUI
{
    partial class AttachmentLabel
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
            this.fProperties = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripItemPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripItemSaveas = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.fProperties)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // fProperties
            // 
            this.fProperties.Name = "fProperties";
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripItemPreview,
            this.toolStripItemOpen,
            this.toolStripItemSaveas});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(113, 70);
            // 
            // toolStripItemPreview
            // 
            this.toolStripItemPreview.Image = global::ICP.MailCenter.CommonUI.Properties.Resources.preview;
            this.toolStripItemPreview.Name = "toolStripItemPreview";
            this.toolStripItemPreview.Size = new System.Drawing.Size(112, 22);
            this.toolStripItemPreview.Text = "Preview";
            this.toolStripItemPreview.Click += new System.EventHandler(this.toolStripItemPreview_Click);
            // 
            // toolStripItemOpen
            // 
            this.toolStripItemOpen.Image = global::ICP.MailCenter.CommonUI.Properties.Resources.open;
            this.toolStripItemOpen.Name = "toolStripItemOpen";
            this.toolStripItemOpen.Size = new System.Drawing.Size(112, 22);
            this.toolStripItemOpen.Text = "Open";
            this.toolStripItemOpen.Click += new System.EventHandler(this.toolStripItemOpen_Click);
            // 
            // toolStripItemSaveas
            // 
            this.toolStripItemSaveas.Image = global::ICP.MailCenter.CommonUI.Properties.Resources.Save;
            this.toolStripItemSaveas.Name = "toolStripItemSaveas";
            this.toolStripItemSaveas.Size = new System.Drawing.Size(112, 22);
            this.toolStripItemSaveas.Text = "Save As";
            this.toolStripItemSaveas.Click += new System.EventHandler(this.toolStripItemSaveas_Click);
            // 
            // AttachmentLabel
            // 
            this.MinimumSize = new System.Drawing.Size(50, 21);
            this.Size = new System.Drawing.Size(50, 21);
            this.AutoSizeInLayoutControl = true;
            this.DoubleBuffered = true;
            this.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.AttachmentLabel_OpenLink);
            ((System.ComponentModel.ISupportInitialize)(this.fProperties)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripItemPreview;
        private System.Windows.Forms.ToolStripMenuItem toolStripItemOpen;
        private System.Windows.Forms.ToolStripMenuItem toolStripItemSaveas;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit fProperties;
       
        
        
    }
}
