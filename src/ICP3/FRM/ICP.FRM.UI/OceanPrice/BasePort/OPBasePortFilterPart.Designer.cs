namespace ICP.FRM.UI.OceanPrice
{
    partial class OPBasePortFilterForm
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
            this.chkDeliveryExcl = new DevExpress.XtraEditors.CheckEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.chkDescriptionExcl = new DevExpress.XtraEditors.CheckEdit();
            this.chkPOLExcl = new DevExpress.XtraEditors.CheckEdit();
            this.chkVIAExcl = new DevExpress.XtraEditors.CheckEdit();
            this.chkPODExcl = new DevExpress.XtraEditors.CheckEdit();
            this.labDelivery = new DevExpress.XtraEditors.LabelControl();
            this.labVIA = new DevExpress.XtraEditors.LabelControl();
            this.labPOD = new DevExpress.XtraEditors.LabelControl();
            this.labDescription = new DevExpress.XtraEditors.LabelControl();
            this.labComm = new DevExpress.XtraEditors.LabelControl();
            this.labPOL = new DevExpress.XtraEditors.LabelControl();
            this.txtDelivery = new DevExpress.XtraEditors.TextEdit();
            this.txtVIA = new DevExpress.XtraEditors.TextEdit();
            this.txtPOD = new DevExpress.XtraEditors.TextEdit();
            this.txtPOL = new DevExpress.XtraEditors.TextEdit();
            this.btnClean = new DevExpress.XtraEditors.SimpleButton();
            this.btnFind = new DevExpress.XtraEditors.SimpleButton();
            this.txtTerm = new DevExpress.XtraEditors.TextEdit();
            this.txtSurCharge = new DevExpress.XtraEditors.TextEdit();
            this.chkSurChargeExcl = new DevExpress.XtraEditors.CheckEdit();
            this.labTerm = new DevExpress.XtraEditors.LabelControl();
            this.labSurCharge = new DevExpress.XtraEditors.LabelControl();
            this.txtAccount = new DevExpress.XtraEditors.TextEdit();
            this.labAccount = new DevExpress.XtraEditors.LabelControl();
            this.chkAccountExcl = new DevExpress.XtraEditors.CheckEdit();
            this.txtItemCode = new DevExpress.XtraEditors.TextEdit();
            this.labItemCode = new DevExpress.XtraEditors.LabelControl();
            this.chkItemCodeExcl = new DevExpress.XtraEditors.CheckEdit();
            this.txtComm = new DevExpress.XtraEditors.TextEdit();
            this.chkCommExcl = new DevExpress.XtraEditors.CheckEdit();
            this.chkTermExcl = new DevExpress.XtraEditors.CheckEdit();
            this.txtDescription = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.chkShowError = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDeliveryExcl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDescriptionExcl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPOLExcl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkVIAExcl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPODExcl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDelivery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVIA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTerm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSurCharge.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSurChargeExcl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAccountExcl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkItemCodeExcl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtComm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCommExcl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTermExcl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowError.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // chkDeliveryExcl
            // 
            this.chkDeliveryExcl.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "DeliveryExcl", true));
            this.chkDeliveryExcl.Location = new System.Drawing.Point(640, 140);
            this.chkDeliveryExcl.Name = "chkDeliveryExcl";
            this.chkDeliveryExcl.Properties.Caption = "Excl.";
            this.chkDeliveryExcl.Size = new System.Drawing.Size(54, 19);
            this.chkDeliveryExcl.TabIndex = 14;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.FRM.UI.OceanPrice.BasePortFilterObject);
            // 
            // chkDescriptionExcl
            // 
            this.chkDescriptionExcl.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "DescriptionExcl", true));
            this.chkDescriptionExcl.Location = new System.Drawing.Point(640, 273);
            this.chkDescriptionExcl.Name = "chkDescriptionExcl";
            this.chkDescriptionExcl.Properties.Caption = "Excl.";
            this.chkDescriptionExcl.Size = new System.Drawing.Size(54, 19);
            this.chkDescriptionExcl.TabIndex = 19;
            // 
            // chkPOLExcl
            // 
            this.chkPOLExcl.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "POLExcl", true));
            this.chkPOLExcl.Location = new System.Drawing.Point(640, 62);
            this.chkPOLExcl.Name = "chkPOLExcl";
            this.chkPOLExcl.Properties.Caption = "Excl.";
            this.chkPOLExcl.Size = new System.Drawing.Size(54, 19);
            this.chkPOLExcl.TabIndex = 11;
            // 
            // chkVIAExcl
            // 
            this.chkVIAExcl.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "VIAExcl", true));
            this.chkVIAExcl.Location = new System.Drawing.Point(640, 87);
            this.chkVIAExcl.Name = "chkVIAExcl";
            this.chkVIAExcl.Properties.Caption = "Excl.";
            this.chkVIAExcl.Size = new System.Drawing.Size(54, 19);
            this.chkVIAExcl.TabIndex = 12;
            // 
            // chkPODExcl
            // 
            this.chkPODExcl.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "PODExcl", true));
            this.chkPODExcl.Location = new System.Drawing.Point(640, 114);
            this.chkPODExcl.Name = "chkPODExcl";
            this.chkPODExcl.Properties.Caption = "Excl.";
            this.chkPODExcl.Size = new System.Drawing.Size(54, 19);
            this.chkPODExcl.TabIndex = 13;
            // 
            // labDelivery
            // 
            this.labDelivery.Location = new System.Drawing.Point(12, 143);
            this.labDelivery.Name = "labDelivery";
            this.labDelivery.Size = new System.Drawing.Size(42, 14);
            this.labDelivery.TabIndex = 24;
            this.labDelivery.Text = "Delivery";
            // 
            // labVIA
            // 
            this.labVIA.Location = new System.Drawing.Point(12, 89);
            this.labVIA.Name = "labVIA";
            this.labVIA.Size = new System.Drawing.Size(20, 14);
            this.labVIA.TabIndex = 23;
            this.labVIA.Text = "VIA";
            // 
            // labPOD
            // 
            this.labPOD.Location = new System.Drawing.Point(12, 118);
            this.labPOD.Name = "labPOD";
            this.labPOD.Size = new System.Drawing.Size(24, 14);
            this.labPOD.TabIndex = 19;
            this.labPOD.Text = "POD";
            // 
            // labDescription
            // 
            this.labDescription.Location = new System.Drawing.Point(12, 276);
            this.labDescription.Name = "labDescription";
            this.labDescription.Size = new System.Drawing.Size(60, 14);
            this.labDescription.TabIndex = 15;
            this.labDescription.Text = "Description";
            // 
            // labComm
            // 
            this.labComm.Location = new System.Drawing.Point(12, 197);
            this.labComm.Name = "labComm";
            this.labComm.Size = new System.Drawing.Size(34, 14);
            this.labComm.TabIndex = 14;
            this.labComm.Text = "Comm";
            // 
            // labPOL
            // 
            this.labPOL.Location = new System.Drawing.Point(12, 62);
            this.labPOL.Name = "labPOL";
            this.labPOL.Size = new System.Drawing.Size(22, 14);
            this.labPOL.TabIndex = 16;
            this.labPOL.Text = "POL";
            // 
            // txtDelivery
            // 
            this.txtDelivery.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Delivery", true));
            this.txtDelivery.Location = new System.Drawing.Point(78, 140);
            this.txtDelivery.Name = "txtDelivery";
            this.txtDelivery.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtDelivery.Size = new System.Drawing.Size(556, 21);
            this.txtDelivery.TabIndex = 4;
            // 
            // txtVIA
            // 
            this.txtVIA.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "VIA", true));
            this.txtVIA.Location = new System.Drawing.Point(78, 86);
            this.txtVIA.Name = "txtVIA";
            this.txtVIA.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtVIA.Size = new System.Drawing.Size(556, 21);
            this.txtVIA.TabIndex = 2;
            // 
            // txtPOD
            // 
            this.txtPOD.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "POD", true));
            this.txtPOD.Location = new System.Drawing.Point(78, 113);
            this.txtPOD.Name = "txtPOD";
            this.txtPOD.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtPOD.Size = new System.Drawing.Size(556, 21);
            this.txtPOD.TabIndex = 3;
            // 
            // txtPOL
            // 
            this.txtPOL.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "POL", true));
            this.txtPOL.Location = new System.Drawing.Point(78, 59);
            this.txtPOL.Name = "txtPOL";
            this.txtPOL.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtPOL.Size = new System.Drawing.Size(556, 21);
            this.txtPOL.TabIndex = 1;
            // 
            // btnClean
            // 
            this.btnClean.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClean.Location = new System.Drawing.Point(511, 310);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(75, 21);
            this.btnClean.TabIndex = 23;
            this.btnClean.Text = "C&lean";
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // btnFind
            // 
            this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFind.Location = new System.Drawing.Point(418, 310);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 21);
            this.btnFind.TabIndex = 22;
            this.btnFind.Text = "&Filter";
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtTerm
            // 
            this.txtTerm.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Term", true));
            this.txtTerm.Location = new System.Drawing.Point(78, 220);
            this.txtTerm.Name = "txtTerm";
            this.txtTerm.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtTerm.Size = new System.Drawing.Size(556, 21);
            this.txtTerm.TabIndex = 7;
            // 
            // txtSurCharge
            // 
            this.txtSurCharge.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "SurCharge", true));
            this.txtSurCharge.Location = new System.Drawing.Point(78, 245);
            this.txtSurCharge.Name = "txtSurCharge";
            this.txtSurCharge.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtSurCharge.Size = new System.Drawing.Size(556, 21);
            this.txtSurCharge.TabIndex = 8;
            // 
            // chkSurChargeExcl
            // 
            this.chkSurChargeExcl.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "SurChargeExcl", true));
            this.chkSurChargeExcl.Location = new System.Drawing.Point(640, 247);
            this.chkSurChargeExcl.Name = "chkSurChargeExcl";
            this.chkSurChargeExcl.Properties.Caption = "Excl.";
            this.chkSurChargeExcl.Size = new System.Drawing.Size(54, 19);
            this.chkSurChargeExcl.TabIndex = 18;
            // 
            // labTerm
            // 
            this.labTerm.Location = new System.Drawing.Point(12, 223);
            this.labTerm.Name = "labTerm";
            this.labTerm.Size = new System.Drawing.Size(29, 14);
            this.labTerm.TabIndex = 20;
            this.labTerm.Text = "Term";
            // 
            // labSurCharge
            // 
            this.labSurCharge.Location = new System.Drawing.Point(12, 249);
            this.labSurCharge.Name = "labSurCharge";
            this.labSurCharge.Size = new System.Drawing.Size(60, 14);
            this.labSurCharge.TabIndex = 21;
            this.labSurCharge.Text = "Sur Charge";
            // 
            // txtAccount
            // 
            this.txtAccount.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Account", true));
            this.txtAccount.Location = new System.Drawing.Point(78, 32);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtAccount.Size = new System.Drawing.Size(556, 21);
            this.txtAccount.TabIndex = 0;
            // 
            // labAccount
            // 
            this.labAccount.Location = new System.Drawing.Point(12, 35);
            this.labAccount.Name = "labAccount";
            this.labAccount.Size = new System.Drawing.Size(46, 14);
            this.labAccount.TabIndex = 16;
            this.labAccount.Text = "Account";
            // 
            // chkAccountExcl
            // 
            this.chkAccountExcl.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "AccountExcl", true));
            this.chkAccountExcl.Location = new System.Drawing.Point(640, 34);
            this.chkAccountExcl.Name = "chkAccountExcl";
            this.chkAccountExcl.Properties.Caption = "Excl.";
            this.chkAccountExcl.Size = new System.Drawing.Size(54, 19);
            this.chkAccountExcl.TabIndex = 10;
            // 
            // txtItemCode
            // 
            this.txtItemCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ItemCode", true));
            this.txtItemCode.Location = new System.Drawing.Point(78, 167);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtItemCode.Size = new System.Drawing.Size(556, 21);
            this.txtItemCode.TabIndex = 5;
            // 
            // labItemCode
            // 
            this.labItemCode.Location = new System.Drawing.Point(12, 170);
            this.labItemCode.Name = "labItemCode";
            this.labItemCode.Size = new System.Drawing.Size(54, 14);
            this.labItemCode.TabIndex = 24;
            this.labItemCode.Text = "ItemCode";
            // 
            // chkItemCodeExcl
            // 
            this.chkItemCodeExcl.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ItemCodeExcl", true));
            this.chkItemCodeExcl.Location = new System.Drawing.Point(640, 167);
            this.chkItemCodeExcl.Name = "chkItemCodeExcl";
            this.chkItemCodeExcl.Properties.Caption = "Excl.";
            this.chkItemCodeExcl.Size = new System.Drawing.Size(54, 19);
            this.chkItemCodeExcl.TabIndex = 15;
            // 
            // txtComm
            // 
            this.txtComm.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Comm", true));
            this.txtComm.Location = new System.Drawing.Point(78, 194);
            this.txtComm.Name = "txtComm";
            this.txtComm.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtComm.Size = new System.Drawing.Size(556, 21);
            this.txtComm.TabIndex = 6;
            // 
            // chkCommExcl
            // 
            this.chkCommExcl.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "CommExcl", true));
            this.chkCommExcl.Location = new System.Drawing.Point(640, 194);
            this.chkCommExcl.Name = "chkCommExcl";
            this.chkCommExcl.Properties.Caption = "Excl.";
            this.chkCommExcl.Size = new System.Drawing.Size(54, 19);
            this.chkCommExcl.TabIndex = 16;
            // 
            // chkTermExcl
            // 
            this.chkTermExcl.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "TermExcl", true));
            this.chkTermExcl.Location = new System.Drawing.Point(640, 220);
            this.chkTermExcl.Name = "chkTermExcl";
            this.chkTermExcl.Properties.Caption = "Excl.";
            this.chkTermExcl.Size = new System.Drawing.Size(54, 19);
            this.chkTermExcl.TabIndex = 17;
            // 
            // txtDescription
            // 
            this.txtDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Description", true));
            this.txtDescription.Location = new System.Drawing.Point(78, 272);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtDescription.Size = new System.Drawing.Size(556, 21);
            this.txtDescription.TabIndex = 9;
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
            this.btnClose.Location = new System.Drawing.Point(606, 310);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 21);
            this.btnClose.TabIndex = 24;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // chkShowError
            // 
            this.chkShowError.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "OnlyShowError", true));
            this.chkShowError.Location = new System.Drawing.Point(76, 300);
            this.chkShowError.Name = "chkShowError";
            this.chkShowError.Properties.Caption = "Only Show Error";
            this.chkShowError.Size = new System.Drawing.Size(129, 19);
            this.chkShowError.TabIndex = 21;
            // 
            // OPBasePortFilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkShowError);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.chkTermExcl);
            this.Controls.Add(this.chkCommExcl);
            this.Controls.Add(this.chkItemCodeExcl);
            this.Controls.Add(this.chkDeliveryExcl);
            this.Controls.Add(this.chkDescriptionExcl);
            this.Controls.Add(this.chkAccountExcl);
            this.Controls.Add(this.chkPOLExcl);
            this.Controls.Add(this.chkVIAExcl);
            this.Controls.Add(this.chkPODExcl);
            this.Controls.Add(this.labItemCode);
            this.Controls.Add(this.labDelivery);
            this.Controls.Add(this.labVIA);
            this.Controls.Add(this.labPOD);
            this.Controls.Add(this.labDescription);
            this.Controls.Add(this.labAccount);
            this.Controls.Add(this.labComm);
            this.Controls.Add(this.labPOL);
            this.Controls.Add(this.txtComm);
            this.Controls.Add(this.txtItemCode);
            this.Controls.Add(this.txtDelivery);
            this.Controls.Add(this.txtVIA);
            this.Controls.Add(this.txtPOD);
            this.Controls.Add(this.txtAccount);
            this.Controls.Add(this.txtPOL);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnClean);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.txtTerm);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtSurCharge);
            this.Controls.Add(this.chkSurChargeExcl);
            this.Controls.Add(this.labTerm);
            this.Controls.Add(this.labSurCharge);
            this.IsMultiLanguage = false;
            this.Name = "OPBasePortFilterForm";
            this.Size = new System.Drawing.Size(709, 354);
            ((System.ComponentModel.ISupportInitialize)(this.chkDeliveryExcl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDescriptionExcl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPOLExcl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkVIAExcl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPODExcl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDelivery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVIA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTerm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSurCharge.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSurChargeExcl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAccountExcl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkItemCodeExcl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtComm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCommExcl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTermExcl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowError.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.CheckEdit chkDeliveryExcl;
        private DevExpress.XtraEditors.CheckEdit chkDescriptionExcl;
        private DevExpress.XtraEditors.CheckEdit chkPOLExcl;
        private DevExpress.XtraEditors.CheckEdit chkVIAExcl;
        private DevExpress.XtraEditors.CheckEdit chkPODExcl;
        private DevExpress.XtraEditors.LabelControl labDelivery;
        private DevExpress.XtraEditors.LabelControl labVIA;
        private DevExpress.XtraEditors.LabelControl labPOD;
        private DevExpress.XtraEditors.LabelControl labDescription;
        private DevExpress.XtraEditors.LabelControl labComm;
        private DevExpress.XtraEditors.LabelControl labPOL;
        private DevExpress.XtraEditors.TextEdit txtDelivery;
        private DevExpress.XtraEditors.TextEdit txtVIA;
        private DevExpress.XtraEditors.TextEdit txtPOD;
        private DevExpress.XtraEditors.TextEdit txtPOL;
        private DevExpress.XtraEditors.SimpleButton btnClean;
        private DevExpress.XtraEditors.SimpleButton btnFind;
        private DevExpress.XtraEditors.TextEdit txtTerm;
        private DevExpress.XtraEditors.TextEdit txtSurCharge;
        private DevExpress.XtraEditors.CheckEdit chkSurChargeExcl;
        private DevExpress.XtraEditors.LabelControl labTerm;
        private DevExpress.XtraEditors.LabelControl labSurCharge;
        private DevExpress.XtraEditors.TextEdit txtAccount;
        private DevExpress.XtraEditors.LabelControl labAccount;
        private DevExpress.XtraEditors.CheckEdit chkAccountExcl;
        private DevExpress.XtraEditors.TextEdit txtItemCode;
        private DevExpress.XtraEditors.LabelControl labItemCode;
        private DevExpress.XtraEditors.CheckEdit chkItemCodeExcl;
        private DevExpress.XtraEditors.TextEdit txtComm;
        private DevExpress.XtraEditors.CheckEdit chkCommExcl;
        private DevExpress.XtraEditors.CheckEdit chkTermExcl;
        private DevExpress.XtraEditors.TextEdit txtDescription;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.CheckEdit chkShowError;
    }
}
