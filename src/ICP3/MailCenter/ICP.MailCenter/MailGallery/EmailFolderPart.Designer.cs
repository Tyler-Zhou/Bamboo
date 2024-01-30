using System.Windows.Forms;
namespace ICP.MailCenter.UI
{
    partial class EmailFolderPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmailFolderPart));
            this.folderImages = new System.Windows.Forms.ImageList(this.components);
            this.pnlFolderPart = new System.Windows.Forms.Panel();
            this.pnlLoading = new System.Windows.Forms.Panel();
            this.lblLoading = new System.Windows.Forms.Label();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.trvFolder = new ICP.MailCenter.UI.TreeViewEx();
            this.pnlFolderPart.SuspendLayout();
            this.pnlLoading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // folderImages
            // 
            this.folderImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("folderImages.ImageStream")));
            this.folderImages.TransparentColor = System.Drawing.Color.Transparent;
            this.folderImages.Images.SetKeyName(0, "folder-home.png");
            this.folderImages.Images.SetKeyName(1, "folder-inbox.png");
            this.folderImages.Images.SetKeyName(2, "folder-drafts.png");
            this.folderImages.Images.SetKeyName(3, "folder-outbox.png");
            this.folderImages.Images.SetKeyName(4, "folder-deleted.png");
            this.folderImages.Images.SetKeyName(5, "folder-sent.png");
            this.folderImages.Images.SetKeyName(6, "folder-junk.png");
            this.folderImages.Images.SetKeyName(7, "folder.png");
            this.folderImages.Images.SetKeyName(8, "folder-store.png");
            this.folderImages.Images.SetKeyName(9, "folder-search.png");
            // 
            // pnlFolderPart
            // 
            this.pnlFolderPart.Controls.Add(this.pnlLoading);
            this.pnlFolderPart.Controls.Add(this.trvFolder);
            this.pnlFolderPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFolderPart.Location = new System.Drawing.Point(0, 0);
            this.pnlFolderPart.Name = "pnlFolderPart";
            this.pnlFolderPart.Size = new System.Drawing.Size(337, 650);
            this.pnlFolderPart.TabIndex = 3;
            // 
            // pnlLoading
            // 
            this.pnlLoading.AutoScroll = true;
            this.pnlLoading.BackColor = System.Drawing.Color.GhostWhite;
            this.pnlLoading.Controls.Add(this.lblLoading);
            this.pnlLoading.Controls.Add(this.picLoading);
            this.pnlLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLoading.Location = new System.Drawing.Point(0, 0);
            this.pnlLoading.Name = "pnlLoading";
            this.pnlLoading.Size = new System.Drawing.Size(337, 650);
            this.pnlLoading.TabIndex = 2;
            // 
            // lblLoading
            // 
            this.lblLoading.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblLoading.AutoSize = true;
            this.lblLoading.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            this.lblLoading.Location = new System.Drawing.Point(126, 480);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(97, 15);
            this.lblLoading.TabIndex = 2;
            this.lblLoading.Text = "Loading...";
            // 
            // picLoading
            // 
            this.picLoading.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.picLoading.BackColor = System.Drawing.Color.Transparent;
            this.picLoading.Location = new System.Drawing.Point(139, 402);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(64, 66);
            this.picLoading.TabIndex = 1;
            this.picLoading.TabStop = false;
            // 
            // trvFolder
            // 
            this.trvFolder.AllowDrop = true;
            this.trvFolder.BackColor = System.Drawing.Color.White;
            this.trvFolder.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvFolder.Font = new System.Drawing.Font("宋体", 10F);
            this.trvFolder.ForeColor = System.Drawing.Color.Black;
            this.trvFolder.HideSelection = false;
            this.trvFolder.ImageIndex = 0;
            this.trvFolder.ImageList = this.folderImages;
            this.trvFolder.Indent = 18;
            this.trvFolder.ItemHeight = 20;
            this.trvFolder.LineColor = System.Drawing.Color.White;
            this.trvFolder.Location = new System.Drawing.Point(0, 0);
            this.trvFolder.Name = "trvFolder";
            this.trvFolder.SelectedImageKey = "folder-home.png";
            this.trvFolder.Size = new System.Drawing.Size(337, 650);
            this.trvFolder.TabIndex = 0;
            this.trvFolder.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.trvFolder_AfterLabelEdit);
            this.trvFolder.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.trvFolder_BeforeCollapse);
            this.trvFolder.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.trvFolder_BeforeExpand);
            this.trvFolder.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.trvFolder_ItemDrag);
            this.trvFolder.DragDrop += new System.Windows.Forms.DragEventHandler(this.trvFolder_DragDrop);
            this.trvFolder.DragEnter += new System.Windows.Forms.DragEventHandler(this.trvFolder_DragEnter);
            this.trvFolder.DragOver += new System.Windows.Forms.DragEventHandler(this.trvFolder_DragOver);
            this.trvFolder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.trvFolder_KeyDown);
            this.trvFolder.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.trvFolder_PreviewKeyDown);
            // 
            // EmailFolderPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlFolderPart);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.Name = "EmailFolderPart";
            this.Size = new System.Drawing.Size(337, 650);
            this.pnlFolderPart.ResumeLayout(false);
            this.pnlLoading.ResumeLayout(false);
            this.pnlLoading.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion

        public TreeViewEx trvFolder;
        private System.Windows.Forms.Panel pnlFolderPart;
        private System.Windows.Forms.ImageList folderImages;
        private PictureBox picLoading;
        private Panel pnlLoading;
        private Label lblLoading;        
    }
}
