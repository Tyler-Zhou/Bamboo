namespace ICP.FRM.UI.OceanPrice
{
    partial class OPArbitraryFilterForm
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
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.chkPOLExcl = new DevExpress.XtraEditors.CheckEdit();
            this.chkPODExcl = new DevExpress.XtraEditors.CheckEdit();
            this.labPOD = new DevExpress.XtraEditors.LabelControl();
            this.labPOL = new DevExpress.XtraEditors.LabelControl();
            this.txtPOD = new DevExpress.XtraEditors.TextEdit();
            this.txtPOL = new DevExpress.XtraEditors.TextEdit();
            this.btnClean = new DevExpress.XtraEditors.SimpleButton();
            this.btnFind = new DevExpress.XtraEditors.SimpleButton();
            this.txtTerm = new DevExpress.XtraEditors.TextEdit();
            this.labTerm = new DevExpress.XtraEditors.LabelControl();
            this.chkTermExcl = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtItemCode = new DevExpress.XtraEditors.TextEdit();
            this.labItemCode = new DevExpress.XtraEditors.LabelControl();
            this.chkItemCodeExcl = new DevExpress.XtraEditors.CheckEdit();
            this.chkShowError = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPOLExcl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPODExcl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTerm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTermExcl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkItemCodeExcl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowError.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.FRM.UI.OceanPrice.ArbitraryFilterObject);
            // 
            // chkPOLExcl
            // 
            this.chkPOLExcl.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "POLExcl", true));
            this.chkPOLExcl.Location = new System.Drawing.Point(639, 30);
            this.chkPOLExcl.Name = "chkPOLExcl";
            this.chkPOLExcl.Properties.Caption = "Excl.";
            this.chkPOLExcl.Size = new System.Drawing.Size(54, 19);
            this.chkPOLExcl.TabIndex = 1;
            // 
            // chkPODExcl
            // 
            this.chkPODExcl.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "PODExcl", true));
            this.chkPODExcl.Location = new System.Drawing.Point(639, 60);
            this.chkPODExcl.Name = "chkPODExcl";
            this.chkPODExcl.Properties.Caption = "Excl.";
            this.chkPODExcl.Size = new System.Drawing.Size(54, 19);
            this.chkPODExcl.TabIndex = 3;
            // 
            // labPOD
            // 
            this.labPOD.Location = new System.Drawing.Point(11, 64);
            this.labPOD.Name = "labPOD";
            this.labPOD.Size = new System.Drawing.Size(15, 14);
            this.labPOD.TabIndex = 19;
            this.labPOD.Text = "To";
            // 
            // labPOL
            // 
            this.labPOL.Location = new System.Drawing.Point(11, 35);
            this.labPOL.Name = "labPOL";
            this.labPOL.Size = new System.Drawing.Size(27, 14);
            this.labPOL.TabIndex = 16;
            this.labPOL.Text = "Form";
            // 
            // txtPOD
            // 
            this.txtPOD.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "POD", true));
            this.txtPOD.Location = new System.Drawing.Point(77, 59);
            this.txtPOD.Name = "txtPOD";
            this.txtPOD.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtPOD.Size = new System.Drawing.Size(556, 21);
            this.txtPOD.TabIndex = 2;
            // 
            // txtPOL
            // 
            this.txtPOL.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "POL", true));
            this.txtPOL.Location = new System.Drawing.Point(77, 32);
            this.txtPOL.Name = "txtPOL";
            this.txtPOL.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtPOL.Size = new System.Drawing.Size(556, 21);
            this.txtPOL.TabIndex = 0;
            // 
            // btnClean
            // 
            this.btnClean.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClean.Location = new System.Drawing.Point(523, 142);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(75, 21);
            this.btnClean.TabIndex = 10;
            this.btnClean.Text = "C&lean";
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // btnFind
            // 
            this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFind.Location = new System.Drawing.Point(430, 142);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 21);
            this.btnFind.TabIndex = 9;
            this.btnFind.Text = "&Filter";
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtTerm
            // 
            this.txtTerm.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Term", true));
            this.txtTerm.Location = new System.Drawing.Point(77, 113);
            this.txtTerm.Name = "txtTerm";
            this.txtTerm.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtTerm.Size = new System.Drawing.Size(556, 21);
            this.txtTerm.TabIndex = 6;
            // 
            // labTerm
            // 
            this.labTerm.Location = new System.Drawing.Point(11, 116);
            this.labTerm.Name = "labTerm";
            this.labTerm.Size = new System.Drawing.Size(29, 14);
            this.labTerm.TabIndex = 20;
            this.labTerm.Text = "Term";
            // 
            // chkTermExcl
            // 
            this.chkTermExcl.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "TermExcl", true));
            this.chkTermExcl.Location = new System.Drawing.Point(639, 113);
            this.chkTermExcl.Name = "chkTermExcl";
            this.chkTermExcl.Properties.Caption = "Excl.";
            this.chkTermExcl.Size = new System.Drawing.Size(54, 19);
            this.chkTermExcl.TabIndex = 7;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(404, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(287, 14);
            this.labelControl1.TabIndex = 42;
            this.labelControl1.Text = "Checked if the filtering would not contain the value.";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(618, 142);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 21);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtItemCode
            // 
            this.txtItemCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ItemCode", true));
            this.txtItemCode.Location = new System.Drawing.Point(77, 86);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtItemCode.Size = new System.Drawing.Size(556, 21);
            this.txtItemCode.TabIndex = 4;
            // 
            // labItemCode
            // 
            this.labItemCode.Location = new System.Drawing.Point(11, 89);
            this.labItemCode.Name = "labItemCode";
            this.labItemCode.Size = new System.Drawing.Size(54, 14);
            this.labItemCode.TabIndex = 24;
            this.labItemCode.Text = "ItemCode";
            // 
            // chkItemCodeExcl
            // 
            this.chkItemCodeExcl.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ItemCodeExcl", true));
            this.chkItemCodeExcl.Location = new System.Drawing.Point(639, 86);
            this.chkItemCodeExcl.Name = "chkItemCodeExcl";
            this.chkItemCodeExcl.Properties.Caption = "Excl.";
            this.chkItemCodeExcl.Size = new System.Drawing.Size(54, 19);
            this.chkItemCodeExcl.TabIndex = 5;
            // 
            // chkShowError
            // 
            this.chkShowError.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "OnlyShowError", true));
            this.chkShowError.Location = new System.Drawing.Point(75, 143);
            this.chkShowError.Name = "chkShowError";
            this.chkShowError.Properties.Caption = "Only Show Error";
            this.chkShowError.Size = new System.Drawing.Size(129, 19);
            this.chkShowError.TabIndex = 8;
            // 
            // OPArbitraryFilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkShowError);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.chkTermExcl);
            this.Controls.Add(this.chkItemCodeExcl);
            this.Controls.Add(this.chkPOLExcl);
            this.Controls.Add(this.chkPODExcl);
            this.Controls.Add(this.labItemCode);
            this.Controls.Add(this.labPOD);
            this.Controls.Add(this.labPOL);
            this.Controls.Add(this.txtItemCode);
            this.Controls.Add(this.txtPOD);
            this.Controls.Add(this.txtPOL);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnClean);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.txtTerm);
            this.Controls.Add(this.labTerm);
            this.IsMultiLanguage = false;
            this.Name = "OPArbitraryFilterForm";
            this.Size = new System.Drawing.Size(709, 177);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPOLExcl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPODExcl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTerm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTermExcl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkItemCodeExcl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowError.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.CheckEdit chkPOLExcl;
        private DevExpress.XtraEditors.CheckEdit chkPODExcl;
        private DevExpress.XtraEditors.LabelControl labPOD;
        private DevExpress.XtraEditors.LabelControl labPOL;
        private DevExpress.XtraEditors.TextEdit txtPOD;
        private DevExpress.XtraEditors.TextEdit txtPOL;
        private DevExpress.XtraEditors.SimpleButton btnClean;
        private DevExpress.XtraEditors.SimpleButton btnFind;
        private DevExpress.XtraEditors.TextEdit txtTerm;
        private DevExpress.XtraEditors.LabelControl labTerm;
        private DevExpress.XtraEditors.CheckEdit chkTermExcl;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.TextEdit txtItemCode;
        private DevExpress.XtraEditors.LabelControl labItemCode;
        private DevExpress.XtraEditors.CheckEdit chkItemCodeExcl;
        private DevExpress.XtraEditors.CheckEdit chkShowError;
    }
}
