namespace ICP.MailCenter.UI
{
    partial class UCMailSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCMailSearch));
            this.btnFunction = new DevExpress.XtraEditors.SimpleButton();
            this.imageList = new System.Windows.Forms.ImageList();
            this.txtSearchText = new DevExpress.XtraEditors.TextEdit();
            this.SearchTimer = new System.Windows.Forms.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchText.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFunction
            // 
            this.btnFunction.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnFunction.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnFunction.ImageIndex = 1;
            this.btnFunction.ImageList = this.imageList;
            this.btnFunction.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnFunction.Location = new System.Drawing.Point(225, 2);
            this.btnFunction.Name = "btnFunction";
            this.btnFunction.Size = new System.Drawing.Size(20, 19);
            this.btnFunction.TabIndex = 1;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "delete.png");
            this.imageList.Images.SetKeyName(1, "toolSearch.Image.png");
            // 
            // txtSearchText
            // 
            this.txtSearchText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchText.Location = new System.Drawing.Point(0, 2);
            this.txtSearchText.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.txtSearchText.Name = "txtSearchText";
            this.txtSearchText.Properties.MaxLength = 200;
            this.txtSearchText.Size = new System.Drawing.Size(225, 21);
            this.txtSearchText.TabIndex = 0;
            this.txtSearchText.EditValueChanged += new System.EventHandler(this.txtSearchText_EditValueChanged);
            // 
            // SearchTimer
            // 
            this.SearchTimer.Interval = 1500;
            this.SearchTimer.Tick += new System.EventHandler(this.SearchTimer_Tick);
            // 
            // UCMailSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtSearchText);
            this.Controls.Add(this.btnFunction);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.Name = "UCMailSearch";
            this.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.Size = new System.Drawing.Size(245, 21);
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchText.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnFunction;
        private DevExpress.XtraEditors.TextEdit txtSearchText;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Timer SearchTimer;
    }
}
