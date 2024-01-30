namespace ICP.Common.UI.Controls
{
    partial class CustomerForNewPopupContainer
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.labRemark = new DevExpress.XtraEditors.LabelControl();
            this.txtFax = new DevExpress.XtraEditors.TextEdit();
            this._BSCustomerInfo = new System.Windows.Forms.BindingSource(this.components);
            this.labFax = new DevExpress.XtraEditors.LabelControl();
            this.txtTel = new DevExpress.XtraEditors.TextEdit();
            this.labTel = new DevExpress.XtraEditors.LabelControl();
            this.txtCityZip = new DevExpress.XtraEditors.TextEdit();
            this.labCityZip = new DevExpress.XtraEditors.LabelControl();
            this.labCountry = new DevExpress.XtraEditors.LabelControl();
            this.labAddress = new DevExpress.XtraEditors.LabelControl();
            this.labName = new DevExpress.XtraEditors.LabelControl();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.txtName = new DevExpress.XtraEditors.MemoEdit();
            this.txtAddress = new DevExpress.XtraEditors.MemoEdit();
            this.txtEnterpriseCodeType = new DevExpress.XtraEditors.TextEdit();
            this.labEnterpriseCodeType = new DevExpress.XtraEditors.LabelControl();
            this.txtEnterpriseCode = new DevExpress.XtraEditors.TextEdit();
            this.labEnterpriseCode = new DevExpress.XtraEditors.LabelControl();
            this.labContact = new DevExpress.XtraEditors.LabelControl();
            this.txtContact = new DevExpress.XtraEditors.TextEdit();
            this.cmbCountry = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.txtCountryCode = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._BSCustomerInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCityZip.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEnterpriseCodeType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEnterpriseCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContact.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCountry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCountryCode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(223, 273);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(54, 23);
            this.btnOK.TabIndex = 190;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(162, 273);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(54, 23);
            this.btnClear.TabIndex = 200;
            this.btnClear.Text = "C&lear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // labRemark
            // 
            this.labRemark.Location = new System.Drawing.Point(2, 226);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(40, 14);
            this.labRemark.TabIndex = 8;
            this.labRemark.Text = "Remark";
            // 
            // txtFax
            // 
            this.txtFax.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._BSCustomerInfo, "Fax", true));
            this.txtFax.Location = new System.Drawing.Point(192, 201);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(85, 21);
            this.txtFax.TabIndex = 170;
            // 
            // _BSCustomerInfo
            // 
            this._BSCustomerInfo.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.CustomerDescriptionForNew);
            // 
            // labFax
            // 
            this.labFax.Location = new System.Drawing.Point(168, 204);
            this.labFax.Name = "labFax";
            this.labFax.Size = new System.Drawing.Size(18, 14);
            this.labFax.TabIndex = 7;
            this.labFax.Text = "Fax";
            // 
            // txtTel
            // 
            this.txtTel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._BSCustomerInfo, "Tel", true));
            this.txtTel.Location = new System.Drawing.Point(71, 201);
            this.txtTel.Name = "txtTel";
            this.txtTel.Size = new System.Drawing.Size(85, 21);
            this.txtTel.TabIndex = 160;
            // 
            // labTel
            // 
            this.labTel.Location = new System.Drawing.Point(2, 204);
            this.labTel.Name = "labTel";
            this.labTel.Size = new System.Drawing.Size(17, 14);
            this.labTel.TabIndex = 6;
            this.labTel.Text = "Tel";
            // 
            // txtCityZip
            // 
            this.txtCityZip.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._BSCustomerInfo, "City", true));
            this.txtCityZip.Location = new System.Drawing.Point(71, 156);
            this.txtCityZip.Name = "txtCityZip";
            this.txtCityZip.Size = new System.Drawing.Size(206, 21);
            this.txtCityZip.TabIndex = 140;
            // 
            // labCityZip
            // 
            this.labCityZip.Location = new System.Drawing.Point(2, 159);
            this.labCityZip.Name = "labCityZip";
            this.labCityZip.Size = new System.Drawing.Size(24, 14);
            this.labCityZip.TabIndex = 5;
            this.labCityZip.Text = "City.";
            this.labCityZip.ToolTip = "City/State";
            // 
            // labCountry
            // 
            this.labCountry.Location = new System.Drawing.Point(2, 92);
            this.labCountry.Name = "labCountry";
            this.labCountry.Size = new System.Drawing.Size(43, 14);
            this.labCountry.TabIndex = 4;
            this.labCountry.Text = "Country";
            // 
            // labAddress
            // 
            this.labAddress.Location = new System.Drawing.Point(2, 34);
            this.labAddress.Name = "labAddress";
            this.labAddress.Size = new System.Drawing.Size(26, 14);
            this.labAddress.TabIndex = 3;
            this.labAddress.Text = "Add.";
            this.labAddress.ToolTip = "Address";
            // 
            // labName
            // 
            this.labName.Location = new System.Drawing.Point(2, 2);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(31, 14);
            this.labName.TabIndex = 9;
            this.labName.Text = "Name";
            // 
            // txtRemark
            // 
            this.txtRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._BSCustomerInfo, "Remark", true));
            this.txtRemark.Location = new System.Drawing.Point(71, 223);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(206, 46);
            this.txtRemark.TabIndex = 180;
            // 
            // txtName
            // 
            this.txtName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._BSCustomerInfo, "Name", true));
            this.txtName.Location = new System.Drawing.Point(71, 2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(206, 31);
            this.txtName.TabIndex = 100;
            // 
            // txtAddress
            // 
            this.txtAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._BSCustomerInfo, "Address", true));
            this.txtAddress.Location = new System.Drawing.Point(71, 34);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(206, 54);
            this.txtAddress.TabIndex = 110;
            // 
            // txtEnterpriseCodeType
            // 
            this.txtEnterpriseCodeType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this._BSCustomerInfo, "EnterpriseCodeType", true));
            this.txtEnterpriseCodeType.Location = new System.Drawing.Point(71, 111);
            this.txtEnterpriseCodeType.Name = "txtEnterpriseCodeType";
            this.txtEnterpriseCodeType.Size = new System.Drawing.Size(206, 21);
            this.txtEnterpriseCodeType.TabIndex = 140;
            // 
            // labEnterpriseCodeType
            // 
            this.labEnterpriseCodeType.Location = new System.Drawing.Point(2, 114);
            this.labEnterpriseCodeType.Name = "labEnterpriseCodeType";
            this.labEnterpriseCodeType.Size = new System.Drawing.Size(67, 14);
            this.labEnterpriseCodeType.TabIndex = 5;
            this.labEnterpriseCodeType.Text = "ECode Type";
            this.labEnterpriseCodeType.ToolTip = "City/State";
            // 
            // txtEnterpriseCode
            // 
            this.txtEnterpriseCode.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this._BSCustomerInfo, "EnterpriseCode", true));
            this.txtEnterpriseCode.Location = new System.Drawing.Point(71, 133);
            this.txtEnterpriseCode.Name = "txtEnterpriseCode";
            this.txtEnterpriseCode.Size = new System.Drawing.Size(206, 21);
            this.txtEnterpriseCode.TabIndex = 140;
            // 
            // labEnterpriseCode
            // 
            this.labEnterpriseCode.Location = new System.Drawing.Point(2, 136);
            this.labEnterpriseCode.Name = "labEnterpriseCode";
            this.labEnterpriseCode.Size = new System.Drawing.Size(35, 14);
            this.labEnterpriseCode.TabIndex = 5;
            this.labEnterpriseCode.Text = "ECode";
            this.labEnterpriseCode.ToolTip = "City/State";
            // 
            // labContact
            // 
            this.labContact.Location = new System.Drawing.Point(2, 181);
            this.labContact.Name = "labContact";
            this.labContact.Size = new System.Drawing.Size(43, 14);
            this.labContact.TabIndex = 5;
            this.labContact.Text = "Contact";
            this.labContact.ToolTip = "City/State";
            // 
            // txtContact
            // 
            this.txtContact.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this._BSCustomerInfo, "Contact", true));
            this.txtContact.Location = new System.Drawing.Point(71, 178);
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(206, 21);
            this.txtContact.TabIndex = 140;
            // 
            // cmbCountry
            // 
            this.cmbCountry.DataBindings.Add(new System.Windows.Forms.Binding("Text", this._BSCustomerInfo, "Country", true));
            this.cmbCountry.Location = new System.Drawing.Point(71, 89);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "Click to Clear Country And City.", null, null, true)});
            this.cmbCountry.Size = new System.Drawing.Size(165, 21);
            this.cmbCountry.TabIndex = 120;
            this.cmbCountry.TabStop = false;
            // 
            // txtCountryCode
            // 
            this.txtCountryCode.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this._BSCustomerInfo, "CountryCode", true));
            this.txtCountryCode.Location = new System.Drawing.Point(242, 89);
            this.txtCountryCode.Name = "txtCountryCode";
            this.txtCountryCode.Properties.ReadOnly = true;
            this.txtCountryCode.Size = new System.Drawing.Size(33, 21);
            this.txtCountryCode.TabIndex = 170;
            // 
            // CustomerForNewPopupContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.labRemark);
            this.Controls.Add(this.txtCountryCode);
            this.Controls.Add(this.txtFax);
            this.Controls.Add(this.labFax);
            this.Controls.Add(this.txtTel);
            this.Controls.Add(this.labTel);
            this.Controls.Add(this.txtEnterpriseCodeType);
            this.Controls.Add(this.txtContact);
            this.Controls.Add(this.labContact);
            this.Controls.Add(this.txtEnterpriseCode);
            this.Controls.Add(this.labEnterpriseCode);
            this.Controls.Add(this.labEnterpriseCodeType);
            this.Controls.Add(this.txtCityZip);
            this.Controls.Add(this.labCityZip);
            this.Controls.Add(this.labCountry);
            this.Controls.Add(this.labAddress);
            this.Controls.Add(this.labName);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.cmbCountry);
            this.Name = "CustomerForNewPopupContainer";
            this.Size = new System.Drawing.Size(278, 302);
            ((System.ComponentModel.ISupportInitialize)(this.txtFax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._BSCustomerInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCityZip.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEnterpriseCodeType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEnterpriseCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContact.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCountry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCountryCode.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.LabelControl labRemark;
        private DevExpress.XtraEditors.TextEdit txtFax;
        private DevExpress.XtraEditors.LabelControl labFax;
        private DevExpress.XtraEditors.TextEdit txtTel;
        private DevExpress.XtraEditors.LabelControl labTel;
        private DevExpress.XtraEditors.TextEdit txtCityZip;
        private DevExpress.XtraEditors.LabelControl labCityZip;
        private DevExpress.XtraEditors.LabelControl labCountry;
        private DevExpress.XtraEditors.LabelControl labAddress;
        private DevExpress.XtraEditors.LabelControl labName;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraEditors.MemoEdit txtName;
        private DevExpress.XtraEditors.MemoEdit txtAddress;
        private System.Windows.Forms.BindingSource _BSCustomerInfo;
        private DevExpress.XtraEditors.TextEdit txtEnterpriseCodeType;
        private DevExpress.XtraEditors.LabelControl labEnterpriseCodeType;
        private DevExpress.XtraEditors.TextEdit txtEnterpriseCode;
        private DevExpress.XtraEditors.LabelControl labEnterpriseCode;
        private DevExpress.XtraEditors.LabelControl labContact;
        private DevExpress.XtraEditors.TextEdit txtContact;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbCountry;
        private DevExpress.XtraEditors.TextEdit txtCountryCode;

    }
}
