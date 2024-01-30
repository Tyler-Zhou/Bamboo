namespace ICP.Common.UI.Geography.Location
{
    partial class LocationEditPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocationEditPart));
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.labEName = new DevExpress.XtraEditors.LabelControl();
            this.labCode = new DevExpress.XtraEditors.LabelControl();
            this.labCName = new DevExpress.XtraEditors.LabelControl();
            this.txtEName = new DevExpress.XtraEditors.TextEdit();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.txtCName = new DevExpress.XtraEditors.TextEdit();
            this.bsGeography = new System.Windows.Forms.BindingSource(this.components);
            this.labGeography = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkIsOther = new DevExpress.XtraEditors.CheckEdit();
            this.chkIsAir = new DevExpress.XtraEditors.CheckEdit();
            this.chkIsOcean = new DevExpress.XtraEditors.CheckEdit();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.labType = new DevExpress.XtraEditors.LabelControl();
            this.cmbCountryProvince = new ICP.Framework.ClientComponents.Controls.LWComboBoxTree();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsGeography)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsOther.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsAir.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsOcean.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCountryProvince.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.LocationInfo);
            // 
            // labEName
            // 
            resources.ApplyResources(this.labEName, "labEName");
            this.labEName.Name = "labEName";
            // 
            // labCode
            // 
            resources.ApplyResources(this.labCode, "labCode");
            this.labCode.Name = "labCode";
            // 
            // labCName
            // 
            resources.ApplyResources(this.labCName, "labCName");
            this.labCName.Name = "labCName";
            // 
            // txtEName
            // 
            this.txtEName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "EName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtEName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            resources.ApplyResources(this.txtEName, "txtEName");
            this.txtEName.Name = "txtEName";
            this.txtEName.Properties.MaxLength = 100;
            // 
            // txtCode
            // 
            this.txtCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Code", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtCode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            resources.ApplyResources(this.txtCode, "txtCode");
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.MaxLength = 10;
            // 
            // txtCName
            // 
            this.txtCName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "CName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtCName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            resources.ApplyResources(this.txtCName, "txtCName");
            this.txtCName.Name = "txtCName";
            this.txtCName.Properties.MaxLength = 100;
            // 
            // bsGeography
            // 
            this.bsGeography.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.CountryProvinceList);
            // 
            // labGeography
            // 
            resources.ApplyResources(this.labGeography, "labGeography");
            this.labGeography.Name = "labGeography";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkIsOther);
            this.groupBox1.Controls.Add(this.chkIsAir);
            this.groupBox1.Controls.Add(this.chkIsOcean);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // chkIsOther
            // 
            this.chkIsOther.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "IsOther", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.chkIsOther, "chkIsOther");
            this.chkIsOther.Name = "chkIsOther";
            this.chkIsOther.Properties.AllowFocused = false;
            this.chkIsOther.Properties.Caption = resources.GetString("chkIsOther.Properties.Caption");
            // 
            // chkIsAir
            // 
            this.chkIsAir.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "IsAir", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.chkIsAir, "chkIsAir");
            this.chkIsAir.Name = "chkIsAir";
            this.chkIsAir.Properties.AllowFocused = false;
            this.chkIsAir.Properties.Caption = resources.GetString("chkIsAir.Properties.Caption");
            // 
            // chkIsOcean
            // 
            this.chkIsOcean.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "IsOcean", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.chkIsOcean, "chkIsOcean");
            this.chkIsOcean.Name = "chkIsOcean";
            this.chkIsOcean.Properties.AllowFocused = false;
            this.chkIsOcean.Properties.Caption = resources.GetString("chkIsOcean.Properties.Caption");
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bindingSource1;
            // 
            // labType
            // 
            resources.ApplyResources(this.labType, "labType");
            this.labType.Name = "labType";
            // 
            // cmbCountryProvince
            // 
            this.cmbCountryProvince.AllowMultSelect = false;
            this.cmbCountryProvince.DataSource = null;
            this.cmbCountryProvince.DisplayMember = "CName";
            resources.ApplyResources(this.cmbCountryProvince, "cmbCountryProvince");
            this.cmbCountryProvince.Name = "cmbCountryProvince";
            this.cmbCountryProvince.ParentMember = "ParentID";
            this.cmbCountryProvince.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbCountryProvince.Properties.Buttons"))))});
            this.cmbCountryProvince.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbCountryProvince.Properties.PopupSizeable = false;
            this.cmbCountryProvince.Properties.ShowPopupCloseButton = false;
            this.cmbCountryProvince.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cmbCountryProvince.RootValue = 0;
            this.cmbCountryProvince.SelectedValue = null;
            this.cmbCountryProvince.Separator = ",";
            this.cmbCountryProvince.ValueMember = "ID";
            // 
            // LocationEditPart
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbCountryProvince);
            this.Controls.Add(this.labType);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labGeography);
            this.Controls.Add(this.labCode);
            this.Controls.Add(this.labEName);
            this.Controls.Add(this.labCName);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtEName);
            this.Controls.Add(this.txtCName);
            this.Name = "LocationEditPart";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsGeography)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkIsOther.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsAir.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsOcean.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCountryProvince.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labEName;
        private DevExpress.XtraEditors.LabelControl labCode;
        private DevExpress.XtraEditors.LabelControl labCName;
        private DevExpress.XtraEditors.TextEdit txtEName;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.TextEdit txtCName;
        private System.Windows.Forms.BindingSource bsGeography;
        private DevExpress.XtraEditors.LabelControl labGeography;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.CheckEdit chkIsOther;
        private DevExpress.XtraEditors.CheckEdit chkIsAir;
        private DevExpress.XtraEditors.CheckEdit chkIsOcean;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraEditors.LabelControl labType;
        private ICP.Framework.ClientComponents.Controls.LWComboBoxTree cmbCountryProvince;
    }
}
