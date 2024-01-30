namespace ICP.FRM.UI.OceanPrice
{
    partial class EditRemarkPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditRemarkPart));
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.labRemark = new DevExpress.XtraEditors.LabelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtRemark = new System.Windows.Forms.RichTextBox();
            this.richEditControl1 = new DevExpress.XtraRichEdit.RichEditControl();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(716, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(601, 13);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // labRemark
            // 
            this.labRemark.Location = new System.Drawing.Point(11, 15);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(40, 14);
            this.labRemark.TabIndex = 3;
            this.labRemark.Text = "Remark";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 452);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(805, 53);
            this.panel1.TabIndex = 7;
            // 
            // txtRemark
            // 
            this.txtRemark.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRemark.Location = new System.Drawing.Point(73, 13);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.txtRemark.Size = new System.Drawing.Size(715, 423);
            this.txtRemark.TabIndex = 8;
            this.txtRemark.Text = "";
            // 
            // richEditControl1
            // 
            this.richEditControl1.Location = new System.Drawing.Point(0, 0);
            this.richEditControl1.Name = "richEditControl1";
            this.richEditControl1.Options.FormattingMarkVisibility.ShowHiddenText = false;
            this.richEditControl1.Options.Import.Html.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("richEditControl1.Options.Import.Html.ActualEncoding")));
            this.richEditControl1.Options.Import.Html.AsyncImageLoading = true;
            this.richEditControl1.Options.Import.Mht.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("richEditControl1.Options.Import.Mht.ActualEncoding")));
            this.richEditControl1.Options.Import.Mht.AsyncImageLoading = true;
            this.richEditControl1.Options.Import.OpenDocument.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("richEditControl1.Options.Import.OpenDocument.ActualEncoding")));
            this.richEditControl1.Options.Import.OpenXml.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("richEditControl1.Options.Import.OpenXml.ActualEncoding")));
            this.richEditControl1.Options.Import.PlainText.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("richEditControl1.Options.Import.PlainText.ActualEncoding")));
            this.richEditControl1.Options.Import.Rtf.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("richEditControl1.Options.Import.Rtf.ActualEncoding")));
            this.richEditControl1.Options.Import.WordML.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("richEditControl1.Options.Import.WordML.ActualEncoding")));
            this.richEditControl1.TabIndex = 0;
            this.richEditControl1.Text = "34";
            this.richEditControl1.Views.SimpleView.HidePartiallyVisibleRow = false;
            // 
            // EditRemarkPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labRemark);
            this.IsMultiLanguage = false;
            this.Name = "EditRemarkPart";
            this.Size = new System.Drawing.Size(805, 505);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.LabelControl labRemark;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox txtRemark;
        private DevExpress.XtraRichEdit.RichEditControl richEditControl1;
    }
}
