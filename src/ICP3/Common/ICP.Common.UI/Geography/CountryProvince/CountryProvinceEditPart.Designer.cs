namespace ICP.Common.UI.Geography.CountryProvince
{
    partial class CountryProvinceEditPart
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
            this.labCode = new DevExpress.XtraEditors.LabelControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.labEName = new DevExpress.XtraEditors.LabelControl();
            this.labCName = new DevExpress.XtraEditors.LabelControl();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.txtEName = new DevExpress.XtraEditors.TextEdit();
            this.txtCName = new DevExpress.XtraEditors.TextEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lookUpCountry = new DevExpress.XtraEditors.LookUpEdit();
            this.barManager2 = new DevExpress.XtraBars.BarManager(this.components);
            this.bsCountry = new System.Windows.Forms.BindingSource(this.components);
            this.labCountry = new DevExpress.XtraEditors.LabelControl();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpCountry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCountry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // labCode
            // 
            this.labCode.Location = new System.Drawing.Point(8, 12);
            this.labCode.Name = "labCode";
            this.labCode.Size = new System.Drawing.Size(28, 14);
            this.labCode.TabIndex = 48;
            this.labCode.Text = "Code";
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.CountryProvinceInfo);
            // 
            // labEName
            // 
            this.labEName.Location = new System.Drawing.Point(8, 66);
            this.labEName.Name = "labEName";
            this.labEName.Size = new System.Drawing.Size(38, 14);
            this.labEName.TabIndex = 46;
            this.labEName.Text = "EName";
            // 
            // labCName
            // 
            this.labCName.Location = new System.Drawing.Point(8, 39);
            this.labCName.Name = "labCName";
            this.labCName.Size = new System.Drawing.Size(38, 14);
            this.labCName.TabIndex = 47;
            this.labCName.Text = "CName";
            // 
            // txtCode
            // 
            this.txtCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Code", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtCode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCode.Location = new System.Drawing.Point(76, 9);
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.MaxLength = 2;
            this.txtCode.Size = new System.Drawing.Size(193, 21);
            this.txtCode.TabIndex = 0;
            // 
            // txtEName
            // 
            this.txtEName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "EName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtEName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtEName.Location = new System.Drawing.Point(76, 63);
            this.txtEName.Name = "txtEName";
            this.txtEName.Properties.MaxLength = 100;
            this.txtEName.Size = new System.Drawing.Size(193, 21);
            this.txtEName.TabIndex = 2;
            // 
            // txtCName
            // 
            this.txtCName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "CName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtCName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCName.Location = new System.Drawing.Point(76, 36);
            this.txtCName.Name = "txtCName";
            this.txtCName.Properties.MaxLength = 100;
            this.txtCName.Size = new System.Drawing.Size(193, 21);
            this.txtCName.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lookUpCountry);
            this.panel1.Controls.Add(this.labCountry);
            this.panel1.Controls.Add(this.labCode);
            this.panel1.Controls.Add(this.txtCName);
            this.panel1.Controls.Add(this.txtEName);
            this.panel1.Controls.Add(this.txtCode);
            this.panel1.Controls.Add(this.labCName);
            this.panel1.Controls.Add(this.labEName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(328, 138);
            this.panel1.TabIndex = 0;
            // 
            // lookUpCountry
            // 
            this.lookUpCountry.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ParentID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.lookUpCountry, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.lookUpCountry.Location = new System.Drawing.Point(76, 90);
            this.lookUpCountry.MenuManager = this.barManager2;
            this.lookUpCountry.Name = "lookUpCountry";
            this.lookUpCountry.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpCountry.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CName", "名称", 48, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("EName", "Name", 48, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Near)});
            this.lookUpCountry.Properties.DataSource = this.bsCountry;
            this.lookUpCountry.Properties.DisplayMember = "CName";
            this.lookUpCountry.Properties.ValueMember = "ID";
            this.lookUpCountry.Size = new System.Drawing.Size(193, 21);
            this.lookUpCountry.TabIndex = 0;
            this.lookUpCountry.Visible = false;
            // 
            // barManager2
            // 
            this.barManager2.MaxItemId = 0;
            // 
            // bsCountry
            // 
            this.bsCountry.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.CurrencyList);
            // 
            // labCountry
            // 
            this.labCountry.Location = new System.Drawing.Point(8, 92);
            this.labCountry.Name = "labCountry";
            this.labCountry.Size = new System.Drawing.Size(43, 14);
            this.labCountry.TabIndex = 46;
            this.labCountry.Text = "Country";
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bindingSource1;
            // 
            // CountryProvinceEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.panel1);
            this.Name = "CountryProvinceEditPart";
            this.Size = new System.Drawing.Size(328, 141);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpCountry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCountry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraBars.BarManager barManager2;
        private DevExpress.XtraEditors.LabelControl labCode;
        private DevExpress.XtraEditors.LabelControl labEName;
        private DevExpress.XtraEditors.LabelControl labCName;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.TextEdit txtEName;
        private DevExpress.XtraEditors.TextEdit txtCName;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labCountry;
        private DevExpress.XtraEditors.LookUpEdit lookUpCountry;
        private System.Windows.Forms.BindingSource bsCountry;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
    }
}
