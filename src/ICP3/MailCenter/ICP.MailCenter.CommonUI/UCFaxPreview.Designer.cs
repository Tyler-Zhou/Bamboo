namespace ICP.MailCenter.CommonUI
{
    partial class UCFaxPreview
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
                EventUtility.PreviewAction -= OpenAttachment;
                EventUtility.ShowBodyAction -= ShowBody;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCFaxPreview));
            this.pnlContent = new DevExpress.XtraEditors.PanelControl();
            this.richEditBody = new DevExpress.XtraRichEdit.RichEditControl();
            this.ucFaxDetail = new ICP.MailCenter.CommonUI.UCFaxDetail();
            ((System.ComponentModel.ISupportInitialize)(this.pnlContent)).BeginInit();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.richEditBody);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 142);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(747, 487);
            this.pnlContent.TabIndex = 8;
            // 
            // richEditBody
            // 
            this.richEditBody.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple;
            this.richEditBody.Appearance.Text.Options.UseFont = true;
            this.richEditBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richEditBody.Location = new System.Drawing.Point(2, 2);
            this.richEditBody.Name = "richEditBody";
            this.richEditBody.Options.FormattingMarkVisibility.ShowHiddenText = false;
            this.richEditBody.Options.Import.Html.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("richEditBody.Options.Import.Html.ActualEncoding")));
            this.richEditBody.Options.Import.Html.AsyncImageLoading = true;
            this.richEditBody.Options.Import.Html.Encoding = ((System.Text.Encoding)(resources.GetObject("richEditBody.Options.Import.Html.Encoding")));
            this.richEditBody.Options.Import.Mht.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("richEditBody.Options.Import.Mht.ActualEncoding")));
            this.richEditBody.Options.Import.Mht.AsyncImageLoading = true;
            this.richEditBody.Options.Import.OpenDocument.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("richEditBody.Options.Import.OpenDocument.ActualEncoding")));
            this.richEditBody.Options.Import.OpenXml.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("richEditBody.Options.Import.OpenXml.ActualEncoding")));
            this.richEditBody.Options.Import.PlainText.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("richEditBody.Options.Import.PlainText.ActualEncoding")));
            this.richEditBody.Options.Import.Rtf.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("richEditBody.Options.Import.Rtf.ActualEncoding")));
            this.richEditBody.Options.Import.WordML.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("richEditBody.Options.Import.WordML.ActualEncoding")));
            this.richEditBody.ReadOnly = true;
            this.richEditBody.Size = new System.Drawing.Size(743, 483);
            this.richEditBody.TabIndex = 2;
            this.richEditBody.Views.SimpleView.HidePartiallyVisibleRow = false;
            // 
            // ucFaxDetail
            // 
            this.ucFaxDetail.DataSource = null;
            this.ucFaxDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucFaxDetail.Location = new System.Drawing.Point(0, 0);
            this.ucFaxDetail.MinimumSize = new System.Drawing.Size(250, 50);
            this.ucFaxDetail.Name = "ucFaxDetail";
            this.ucFaxDetail.ReadOnly = false;
            this.ucFaxDetail.Size = new System.Drawing.Size(747, 142);
            this.ucFaxDetail.TabIndex = 7;
            // 
            // UCFaxPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.ucFaxDetail);
            this.Name = "UCFaxPreview";
            this.Size = new System.Drawing.Size(747, 629);
            this.DoubleBuffered = true;
            ((System.ComponentModel.ISupportInitialize)(this.pnlContent)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UCFaxDetail ucFaxDetail;
        private DevExpress.XtraEditors.PanelControl pnlContent;
        private DevExpress.XtraRichEdit.RichEditControl richEditBody;



    }
}
