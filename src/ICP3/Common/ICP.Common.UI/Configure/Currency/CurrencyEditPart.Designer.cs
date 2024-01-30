namespace ICP.Common.UI.Configure.Currency
{
    partial class CurrencyEditPart
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
            this.cmbCountry = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.labCountry = new DevExpress.XtraEditors.LabelControl();
            this.labEName = new DevExpress.XtraEditors.LabelControl();
            this.labCode = new DevExpress.XtraEditors.LabelControl();
            this.labCName = new DevExpress.XtraEditors.LabelControl();
            this.txtEName = new DevExpress.XtraEditors.TextEdit();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.txtCName = new DevExpress.XtraEditors.TextEdit();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.cmbCountry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbCountry
            // 
            this.cmbCountry.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "CountryID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbCountry, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbCountry.Location = new System.Drawing.Point(72, 84);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCountry.Size = new System.Drawing.Size(249, 21);
            this.cmbCountry.TabIndex = 3;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.CurrencyInfo);
            // 
            // labCountry
            // 
            this.labCountry.Location = new System.Drawing.Point(5, 87);
            this.labCountry.Name = "labCountry";
            this.labCountry.Size = new System.Drawing.Size(43, 14);
            this.labCountry.TabIndex = 10;
            this.labCountry.Text = "Country";
            // 
            // labEName
            // 
            this.labEName.Location = new System.Drawing.Point(4, 60);
            this.labEName.Name = "labEName";
            this.labEName.Size = new System.Drawing.Size(38, 14);
            this.labEName.TabIndex = 9;
            this.labEName.Text = "EName";
            // 
            // labCode
            // 
            this.labCode.Location = new System.Drawing.Point(4, 6);
            this.labCode.Name = "labCode";
            this.labCode.Size = new System.Drawing.Size(28, 14);
            this.labCode.TabIndex = 7;
            this.labCode.Text = "Code";
            // 
            // labCName
            // 
            this.labCName.Location = new System.Drawing.Point(4, 33);
            this.labCName.Name = "labCName";
            this.labCName.Size = new System.Drawing.Size(38, 14);
            this.labCName.TabIndex = 8;
            this.labCName.Text = "CName";
            // 
            // txtEName
            // 
            this.txtEName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "EName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtEName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtEName.Location = new System.Drawing.Point(72, 57);
            this.txtEName.Name = "txtEName";
            this.txtEName.Properties.MaxLength = 50;
            this.txtEName.Size = new System.Drawing.Size(249, 21);
            this.txtEName.TabIndex = 2;
            // 
            // txtCode
            // 
            this.txtCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Code", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtCode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCode.Location = new System.Drawing.Point(72, 3);
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.MaxLength = 10;
            this.txtCode.Size = new System.Drawing.Size(249, 21);
            this.txtCode.TabIndex = 0;
            // 
            // txtCName
            // 
            this.txtCName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "CName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtCName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCName.Location = new System.Drawing.Point(72, 30);
            this.txtCName.Name = "txtCName";
            this.txtCName.Properties.MaxLength = 50;
            this.txtCName.Size = new System.Drawing.Size(249, 21);
            this.txtCName.TabIndex = 1;
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bindingSource1;
            // 
            // CurrencyEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.labEName);
            this.Controls.Add(this.labCode);
            this.Controls.Add(this.labCName);
            this.Controls.Add(this.txtEName);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtCName);
            this.Controls.Add(this.cmbCountry);
            this.Controls.Add(this.labCountry);
            this.Name = "CurrencyEditPart";
            this.Size = new System.Drawing.Size(328, 127);
            ((System.ComponentModel.ISupportInitialize)(this.cmbCountry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ImageComboBoxEdit cmbCountry;
        private DevExpress.XtraEditors.LabelControl labCountry;
        private DevExpress.XtraEditors.LabelControl labEName;
        private DevExpress.XtraEditors.LabelControl labCode;
        private DevExpress.XtraEditors.LabelControl labCName;
        private DevExpress.XtraEditors.TextEdit txtEName;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.TextEdit txtCName;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
    }
}
