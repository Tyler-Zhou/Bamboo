namespace ICP.MailCenter.UI
{
    partial class SearchFolderPart
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
            this.btnSearchFolder = new DevExpress.XtraEditors.SimpleButton();
            this.txtSearchFolderKeyWord = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchFolderKeyWord.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearchFolder
            // 
            this.btnSearchFolder.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnSearchFolder.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSearchFolder.Image = global::ICP.MailCenter.UI.Properties.Resources.toolSearch_Image;
            this.btnSearchFolder.ImageIndex = 1;
            this.btnSearchFolder.Location = new System.Drawing.Point(234, 0);
            this.btnSearchFolder.Name = "btnSearchFolder";
            this.btnSearchFolder.Size = new System.Drawing.Size(22, 24);
            this.btnSearchFolder.TabIndex = 1;
            this.btnSearchFolder.Click += new System.EventHandler(this.btnSearchFolder_Click);
            // 
            // txtSearchFolderKeyWord
            // 
            this.txtSearchFolderKeyWord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchFolderKeyWord.Location = new System.Drawing.Point(0, 0);
            this.txtSearchFolderKeyWord.Name = "txtSearchFolderKeyWord";
            this.txtSearchFolderKeyWord.Size = new System.Drawing.Size(234, 21);
            this.txtSearchFolderKeyWord.TabIndex = 0;
            this.txtSearchFolderKeyWord.EditValueChanged += new System.EventHandler(this.txtSearchFolderKeyWord_EditValueChanged);
            this.txtSearchFolderKeyWord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchFolderKeyWord_KeyDown);
            // 
            // SearchFolderPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtSearchFolderKeyWord);
            this.Controls.Add(this.btnSearchFolder);
            this.DoubleBuffered = true;
            this.Name = "SearchFolderPart";
            this.Size = new System.Drawing.Size(256, 24);
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchFolderKeyWord.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSearchFolder;
        private DevExpress.XtraEditors.TextEdit txtSearchFolderKeyWord;
    }
}
