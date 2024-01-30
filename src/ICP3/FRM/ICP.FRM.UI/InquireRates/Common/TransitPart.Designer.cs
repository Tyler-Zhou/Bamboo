namespace ICP.FRM.UI.InquireRates
{
    partial class TransitPart
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
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.labCurrentRespondBy = new DevExpress.XtraEditors.LabelControl();
            this.txtCurrentRespondBy = new DevExpress.XtraEditors.TextEdit();
            this.labNewRespondBy = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtDiscussing = new DevExpress.XtraEditors.MemoEdit();
            this.labDiscussing = new DevExpress.XtraEditors.LabelControl();
            this.cmbRespondBy = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrentRespondBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscussing.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(284, 175);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 21);
            this.btnClose.TabIndex = 23;
            this.btnClose.Text = "&Close";
            this.btnClose.Click +=new System.EventHandler(btnClose_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(189, 175);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 21);
            this.btnOk.TabIndex = 22;
            this.btnOk.Text = "O&k";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // labCurrentRespondBy
            // 
            this.labCurrentRespondBy.Location = new System.Drawing.Point(3, 6);
            this.labCurrentRespondBy.Name = "labCurrentRespondBy";
            this.labCurrentRespondBy.Size = new System.Drawing.Size(134, 14);
            this.labCurrentRespondBy.TabIndex = 24;
            this.labCurrentRespondBy.Text = "The current Respond by";
            // 
            // txtCurrentRespondBy
            // 
            this.txtCurrentRespondBy.Location = new System.Drawing.Point(3, 26);
            this.txtCurrentRespondBy.Name = "txtCurrentRespondBy";
            this.txtCurrentRespondBy.Properties.ReadOnly = true;
            this.txtCurrentRespondBy.Size = new System.Drawing.Size(134, 21);
            this.txtCurrentRespondBy.TabIndex = 25;
            // 
            // labNewRespondBy
            // 
            this.labNewRespondBy.Location = new System.Drawing.Point(208, 6);
            this.labNewRespondBy.Name = "labNewRespondBy";
            this.labNewRespondBy.Size = new System.Drawing.Size(118, 14);
            this.labNewRespondBy.TabIndex = 26;
            this.labNewRespondBy.Text = "The new Respond By";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(167, 29);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(13, 14);
            this.labelControl1.TabIndex = 28;
            this.labelControl1.Text = "->";
            // 
            // txtDiscussing
            // 
            this.txtDiscussing.Location = new System.Drawing.Point(3, 89);
            this.txtDiscussing.Name = "txtDiscussing";
            this.txtDiscussing.Size = new System.Drawing.Size(361, 74);
            this.txtDiscussing.TabIndex = 29;
            // 
            // labDiscussing
            // 
            this.labDiscussing.Location = new System.Drawing.Point(3, 69);
            this.labDiscussing.Name = "labDiscussing";
            this.labDiscussing.Size = new System.Drawing.Size(54, 14);
            this.labDiscussing.TabIndex = 30;
            this.labDiscussing.Text = "Discussing";
            // 
            // cmbRespondBy
            // 
            this.cmbRespondBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbRespondBy.EditText = "";
            this.cmbRespondBy.EditValue = null;
            this.cmbRespondBy.Location = new System.Drawing.Point(208, 26);
            this.cmbRespondBy.Name = "cmbRespondBy";
            this.cmbRespondBy.ReadOnly = false;
            this.cmbRespondBy.Size = new System.Drawing.Size(156, 21);
            this.cmbRespondBy.SpecifiedBackColor = System.Drawing.Color.LightYellow;
            this.cmbRespondBy.TabIndex = 189;
            this.cmbRespondBy.ToolTip = "";
            // 
            // TransitPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbRespondBy);
            this.Controls.Add(this.labDiscussing);
            this.Controls.Add(this.txtDiscussing);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labNewRespondBy);
            this.Controls.Add(this.txtCurrentRespondBy);
            this.Controls.Add(this.labCurrentRespondBy);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOk);
            this.Name = "TransitPart";
            this.IsMultiLanguage = false;
            this.Size = new System.Drawing.Size(371, 210);
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrentRespondBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscussing.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }      

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.LabelControl labCurrentRespondBy;
        private DevExpress.XtraEditors.TextEdit txtCurrentRespondBy;
        private DevExpress.XtraEditors.LabelControl labNewRespondBy;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.MemoEdit txtDiscussing;
        private DevExpress.XtraEditors.LabelControl labDiscussing;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox cmbRespondBy;
    }
}
