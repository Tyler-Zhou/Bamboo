using ICP.Framework.ClientComponents.Controls;
namespace ICP.Common.UI.TransportFoundation.VesselVoyage
{
    partial class VesselEditPart
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
            this.labCarrier = new DevExpress.XtraEditors.LabelControl();
            this.labCode = new DevExpress.XtraEditors.LabelControl();
            this.labName = new DevExpress.XtraEditors.LabelControl();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.cmbCarrier = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.txtIMO = new DevExpress.XtraEditors.TextEdit();
            this.txtUNCode = new DevExpress.XtraEditors.TextEdit();
            this.lblIMO = new DevExpress.XtraEditors.LabelControl();
            this.lblVesselFlag = new DevExpress.XtraEditors.LabelControl();
            this.mscmbCountry = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.countryListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCarrier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIMO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUNCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryListBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // labCarrier
            // 
            this.labCarrier.Location = new System.Drawing.Point(3, 46);
            this.labCarrier.Name = "labCarrier";
            this.labCarrier.Size = new System.Drawing.Size(34, 14);
            this.labCarrier.TabIndex = 35;
            this.labCarrier.Text = "Carrier";
            // 
            // labCode
            // 
            this.labCode.Location = new System.Drawing.Point(3, 6);
            this.labCode.Name = "labCode";
            this.labCode.Size = new System.Drawing.Size(28, 14);
            this.labCode.TabIndex = 37;
            this.labCode.Text = "Code";
            // 
            // labName
            // 
            this.labName.Location = new System.Drawing.Point(3, 26);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(31, 14);
            this.labName.TabIndex = 36;
            this.labName.Text = "Name";
            // 
            // txtCode
            // 
            this.txtCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Code", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtCode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCode.Location = new System.Drawing.Point(65, 3);
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.MaxLength = 20;
            this.txtCode.Size = new System.Drawing.Size(250, 21);
            this.txtCode.TabIndex = 0;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.VesselInfo);
            // 
            // txtName
            // 
            this.txtName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Name", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtName.Location = new System.Drawing.Point(65, 23);
            this.txtName.Name = "txtName";
            this.txtName.Properties.MaxLength = 100;
            this.txtName.Size = new System.Drawing.Size(250, 21);
            this.txtName.TabIndex = 1;
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bindingSource1;
            // 
            // cmbCarrier
            // 
            this.cmbCarrier.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "CarrierID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbCarrier, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbCarrier.Location = new System.Drawing.Point(65, 43);
            this.cmbCarrier.Name = "cmbCarrier";
            this.cmbCarrier.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCarrier.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCarrier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCarrier.Size = new System.Drawing.Size(250, 21);
            this.cmbCarrier.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCarrier.TabIndex = 2;
            this.cmbCarrier.TabStop = false;
            // 
            // txtIMO
            // 
            this.txtIMO.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "IMO", true));
            this.dxErrorProvider1.SetIconAlignment(this.txtIMO, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtIMO.Location = new System.Drawing.Point(65, 83);
            this.txtIMO.Name = "txtIMO";
            this.txtIMO.Properties.MaxLength = 100;
            this.txtIMO.Size = new System.Drawing.Size(250, 21);
            this.txtIMO.TabIndex = 4;
            // 
            // txtUNCode
            // 
            this.txtUNCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "UNCode", true));
            this.dxErrorProvider1.SetIconAlignment(this.txtUNCode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtUNCode.Location = new System.Drawing.Point(65, 104);
            this.txtUNCode.Name = "txtUNCode";
            this.txtUNCode.Properties.MaxLength = 100;
            this.txtUNCode.Size = new System.Drawing.Size(250, 21);
            this.txtUNCode.TabIndex = 125;
            // 
            // lblIMO
            // 
            this.lblIMO.Location = new System.Drawing.Point(3, 86);
            this.lblIMO.Name = "lblIMO";
            this.lblIMO.Size = new System.Drawing.Size(22, 14);
            this.lblIMO.TabIndex = 124;
            this.lblIMO.Text = "IMO";
            // 
            // lblVesselFlag
            // 
            this.lblVesselFlag.Location = new System.Drawing.Point(3, 66);
            this.lblVesselFlag.Name = "lblVesselFlag";
            this.lblVesselFlag.Size = new System.Drawing.Size(55, 14);
            this.lblVesselFlag.TabIndex = 123;
            this.lblVesselFlag.Text = "VesselFlag";
            // 
            // mscmbCountry
            // 
            this.mscmbCountry.EditText = "";
            this.mscmbCountry.EditValue = null;
            this.mscmbCountry.Location = new System.Drawing.Point(65, 63);
            this.mscmbCountry.Name = "mscmbCountry";
            this.mscmbCountry.ReadOnly = false;
            this.mscmbCountry.RefreshButtonToolTip = "";
            this.mscmbCountry.ShowRefreshButton = false;
            this.mscmbCountry.Size = new System.Drawing.Size(250, 21);
            this.mscmbCountry.SpecifiedBackColor = System.Drawing.Color.White;
            this.mscmbCountry.TabIndex = 3;
            this.mscmbCountry.ToolTip = "";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(3, 108);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(44, 14);
            this.labelControl1.TabIndex = 126;
            this.labelControl1.Text = "UNCode";
            // 
            // countryListBindingSource
            // 
            this.countryListBindingSource.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.CountryList);
            // 
            // VesselEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtUNCode);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.mscmbCountry);
            this.Controls.Add(this.txtIMO);
            this.Controls.Add(this.lblIMO);
            this.Controls.Add(this.lblVesselFlag);
            this.Controls.Add(this.labCarrier);
            this.Controls.Add(this.labCode);
            this.Controls.Add(this.labName);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.cmbCarrier);
            this.Name = "VesselEditPart";
            this.Size = new System.Drawing.Size(324, 130);
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCarrier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIMO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUNCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryListBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labCarrier;
        private DevExpress.XtraEditors.LabelControl labCode;
        private DevExpress.XtraEditors.LabelControl labName;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.TextEdit txtName;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private LWImageComboBoxEdit cmbCarrier;
        private DevExpress.XtraEditors.TextEdit txtIMO;
        private DevExpress.XtraEditors.LabelControl lblIMO;
        private DevExpress.XtraEditors.LabelControl lblVesselFlag;
        public ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mscmbCountry;
        private System.Windows.Forms.BindingSource countryListBindingSource;
        private DevExpress.XtraEditors.TextEdit txtUNCode;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}
