﻿namespace ICP.MailCenter.UI
{
    partial class FolderLink
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FolderLink));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "newfldr.gif");
            this.imageList.Images.SetKeyName(1, "rename.png");
            this.imageList.Images.SetKeyName(2, "openfolder.gif");
            this.imageList.Images.SetKeyName(3, "delete.gif");
            this.imageList.Images.SetKeyName(4, "folder-deleted.png");
            // 
            // FolderLink
            // 
            this.ActiveLinkColor = System.Drawing.Color.Blue;
            this.AutoSize = true;
            this.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.LinkColor = System.Drawing.SystemColors.ControlText;
            this.Size = new System.Drawing.Size(100, 23);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList;
    }
}