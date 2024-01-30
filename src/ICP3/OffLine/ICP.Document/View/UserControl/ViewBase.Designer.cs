namespace ICP.Document
{
    partial class ViewBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewBase));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "delete_16.png");
            this.imageList.Images.SetKeyName(1, "open.png");
            this.imageList.Images.SetKeyName(2, "openwith.png");
            this.imageList.Images.SetKeyName(3, "preview.png");
            this.imageList.Images.SetKeyName(4, "sync.png");
            this.imageList.Images.SetKeyName(5, "upload.png");
            this.imageList.Images.SetKeyName(6, "Down_16.png");
            this.imageList.Images.SetKeyName(7, "docOpen.png");
            this.imageList.Images.SetKeyName(8, "docPreview.png");
            this.imageList.Images.SetKeyName(9, "docWith.png");
            this.imageList.Images.SetKeyName(10, "open2.png");
            // 
            // ViewBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ViewBase";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ImageList imageList;

    }
}
