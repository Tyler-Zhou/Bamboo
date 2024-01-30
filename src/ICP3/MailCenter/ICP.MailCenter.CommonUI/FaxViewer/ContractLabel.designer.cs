namespace ICP.MailCenter.CommonUI
{
    partial class ContractLabel
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
           
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripItemSend = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.fProperties)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // fProperties
            // 
            this.fProperties.ContextMenuStrip = this.contextMenuStrip;
            this.fProperties.Name = "fProperties";
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripItemSend,
            this.toolStripItemCopy});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(119, 48);
            // 
            // toolStripItemSend
            // 
           // this.toolStripItemSend.Image = global::ICP.OA.UI.Properties.Resources.send;
            this.toolStripItemSend.Name = "toolStripItemSend";
            this.toolStripItemSend.Size = new System.Drawing.Size(118, 22);
            this.toolStripItemSend.Text = "Send Fax";
            this.toolStripItemSend.Click += new System.EventHandler(this.toolStripItemSend_Click);
            // 
            // toolStripItemCopy
            // 
           // this.toolStripItemCopy.Image = global::ICP.OA.UI.Properties.Resources.copy;
            this.toolStripItemCopy.Name = "toolStripItemCopy";
            this.toolStripItemCopy.Size = new System.Drawing.Size(130, 22);
            this.toolStripItemCopy.Text = "Copy";
            this.toolStripItemCopy.Click += new System.EventHandler(this.toolStripItemCopy_Click);
            // 
            // ContractLabel
            // 
            this.Size = new System.Drawing.Size(100, 21);
            this.AutoSizeInLayoutControl = true;
            ((System.ComponentModel.ISupportInitialize)(this.fProperties)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

       
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripItemSend;
        private System.Windows.Forms.ToolStripMenuItem toolStripItemCopy;

    }
}
