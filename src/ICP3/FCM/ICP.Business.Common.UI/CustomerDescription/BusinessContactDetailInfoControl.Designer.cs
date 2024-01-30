namespace ICP.Business.Common.UI
{
    partial class BusinessContactDetailInfoControl
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
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtCityZip = new DevExpress.XtraEditors.TextEdit();
            this.labCityZip = new DevExpress.XtraEditors.LabelControl();
            this.labCountry = new DevExpress.XtraEditors.LabelControl();
            this.labAddress = new DevExpress.XtraEditors.LabelControl();
            this.labName = new DevExpress.XtraEditors.LabelControl();
            this.cmbCountry = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.txtCompanyName = new DevExpress.XtraEditors.MemoEdit();
            this.txtAddress = new DevExpress.XtraEditors.MemoEdit();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.pnlAction = new DevExpress.XtraEditors.PanelControl();
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.ucNewCustomerList = new ICP.Business.Common.UI.Contact.UCCustomerList();
            this.txtFax = new DevExpress.XtraEditors.TextEdit();
            this.labFax = new DevExpress.XtraEditors.LabelControl();
            this.txtTel = new DevExpress.XtraEditors.TextEdit();
            this.labTel = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCityZip.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCountry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompanyName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlAction)).BeginInit();
            this.pnlAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTel.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(320, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(69, 27);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(247, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(69, 27);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "C&lear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // labRemark
            // 
            this.labRemark.Location = new System.Drawing.Point(3, 180);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(40, 14);
            this.labRemark.TabIndex = 8;
            this.labRemark.Text = "Remark";
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = typeof(ICP.Framework.CommonLibrary.Common.CustomerDescription);
            // 
            // txtCityZip
            // 
            this.txtCityZip.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "City", true));
            this.txtCityZip.Location = new System.Drawing.Point(56, 127);
            this.txtCityZip.Name = "txtCityZip";
            this.txtCityZip.Size = new System.Drawing.Size(334, 21);
            this.txtCityZip.TabIndex = 3;
            // 
            // labCityZip
            // 
            this.labCityZip.Location = new System.Drawing.Point(2, 131);
            this.labCityZip.Name = "labCityZip";
            this.labCityZip.Size = new System.Drawing.Size(24, 14);
            this.labCityZip.TabIndex = 5;
            this.labCityZip.Text = "City.";
            this.labCityZip.ToolTip = "City/State";
            // 
            // labCountry
            // 
            this.labCountry.Location = new System.Drawing.Point(2, 107);
            this.labCountry.Name = "labCountry";
            this.labCountry.Size = new System.Drawing.Size(43, 14);
            this.labCountry.TabIndex = 4;
            this.labCountry.Text = "Country";
            // 
            // labAddress
            // 
            this.labAddress.Location = new System.Drawing.Point(2, 40);
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
            // cmbCountry
            // 
            this.cmbCountry.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Country", true));
            this.cmbCountry.Location = new System.Drawing.Point(56, 104);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "Click to Clear Country And City.", null, null, true)});
            this.cmbCountry.Size = new System.Drawing.Size(334, 21);
            this.cmbCountry.TabIndex = 2;
            // 
            // txtRemark
            // 
            this.txtRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Remark", true));
            this.txtRemark.Location = new System.Drawing.Point(56, 174);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(334, 44);
            this.txtRemark.TabIndex = 6;
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Name", true));
            this.txtCompanyName.Location = new System.Drawing.Point(56, 2);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(334, 36);
            this.txtCompanyName.TabIndex = 0;
            // 
            // txtAddress
            // 
            this.txtAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Address", true));
            this.txtAddress.Location = new System.Drawing.Point(56, 40);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(334, 63);
            this.txtAddress.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(174, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(69, 27);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // pnlAction
            // 
            this.pnlAction.Controls.Add(this.btnClear);
            this.pnlAction.Controls.Add(this.btnRefresh);
            this.pnlAction.Controls.Add(this.btnOK);
            this.pnlAction.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAction.Location = new System.Drawing.Point(0, 314);
            this.pnlAction.Name = "pnlAction";
            this.pnlAction.Size = new System.Drawing.Size(393, 33);
            this.pnlAction.TabIndex = 208;
            // 
            // dxErrorProvider
            // 
            this.dxErrorProvider.ContainerControl = this;
            // 
            // ucNewCustomerList
            // 
            this.ucNewCustomerList.AllowColumnEditable = true;
            this.ucNewCustomerList.CompanyID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ucNewCustomerList.conditions = new bool[] {
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false};
            this.ucNewCustomerList.ContactStage = ICP.Framework.CommonLibrary.Common.ContactStage.Unknown;
            this.ucNewCustomerList.ContactType = ICP.Framework.CommonLibrary.Common.ContactType.Unknown;
            this.ucNewCustomerList.CustomerID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ucNewCustomerList.CustomerName = null;
            this.ucNewCustomerList.IsChanged = false;
            this.ucNewCustomerList.IsShowCaption = false;
            this.ucNewCustomerList.IsValidatePass = false;
            this.ucNewCustomerList.Location = new System.Drawing.Point(3, 224);
            this.ucNewCustomerList.Name = "ucNewCustomerList";
            
           
            this.ucNewCustomerList.Size = new System.Drawing.Size(386, 87);
            this.ucNewCustomerList.TabIndex = 7;
            this.ucNewCustomerList.ValidateReturnErrorString = true;
            // 
            // txtFax
            // 
            this.txtFax.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Fax", true));
            this.txtFax.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource, "Fax", true));
            this.txtFax.Location = new System.Drawing.Point(231, 150);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(159, 21);
            this.txtFax.TabIndex = 5;
            // 
            // labFax
            // 
            this.labFax.Location = new System.Drawing.Point(207, 153);
            this.labFax.Name = "labFax";
            this.labFax.Size = new System.Drawing.Size(18, 14);
            this.labFax.TabIndex = 211;
            this.labFax.Text = "Fax";
            // 
            // txtTel
            // 
            this.txtTel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Tel", true));
            this.txtTel.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource, "Tel", true));
            this.txtTel.Location = new System.Drawing.Point(56, 150);
            this.txtTel.Name = "txtTel";
            this.txtTel.Size = new System.Drawing.Size(144, 21);
            this.txtTel.TabIndex = 4;
            // 
            // labTel
            // 
            this.labTel.Location = new System.Drawing.Point(2, 153);
            this.labTel.Name = "labTel";
            this.labTel.Size = new System.Drawing.Size(17, 14);
            this.labTel.TabIndex = 210;
            this.labTel.Text = "Tel";
            // 
            // BusinessContactDetailInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtFax);
            this.Controls.Add(this.labFax);
            this.Controls.Add(this.txtTel);
            this.Controls.Add(this.labTel);
            this.Controls.Add(this.ucNewCustomerList);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.labRemark);
            this.Controls.Add(this.pnlAction);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtCompanyName);
            this.Controls.Add(this.txtCityZip);
            this.Controls.Add(this.labCityZip);
            this.Controls.Add(this.labCountry);
            this.Controls.Add(this.labAddress);
            this.Controls.Add(this.labName);
            this.Controls.Add(this.cmbCountry);
            this.Name = "BusinessContactDetailInfoControl";
            this.Size = new System.Drawing.Size(393, 347);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCityZip.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCountry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompanyName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlAction)).EndInit();
            this.pnlAction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTel.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.LabelControl labRemark;
        private DevExpress.XtraEditors.TextEdit txtCityZip;
        private DevExpress.XtraEditors.LabelControl labCityZip;
        private DevExpress.XtraEditors.LabelControl labCountry;
        private DevExpress.XtraEditors.LabelControl labAddress;
        private DevExpress.XtraEditors.LabelControl labName;
        private DevExpress.XtraEditors.ComboBoxEdit cmbCountry;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraEditors.MemoEdit txtCompanyName;
        private DevExpress.XtraEditors.MemoEdit txtAddress;
        private System.Windows.Forms.BindingSource bindingSource;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private DevExpress.XtraEditors.PanelControl pnlAction;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
        private ICP.Business.Common.UI.Contact.UCCustomerList ucNewCustomerList;
        private DevExpress.XtraEditors.TextEdit txtFax;
        private DevExpress.XtraEditors.LabelControl labFax;
        private DevExpress.XtraEditors.TextEdit txtTel;
        private DevExpress.XtraEditors.LabelControl labTel;
       

    }
}
