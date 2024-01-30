namespace ICP.Common.UI.Configure.CommpanyConfigure
{
    partial class ConfigureKeyValueEditPart
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
            this.labValue = new DevExpress.XtraEditors.LabelControl();
            this.txtDescription = new DevExpress.XtraEditors.MemoEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.cmbConfigureKey = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.barManager2 = new DevExpress.XtraBars.BarManager(this.components);
            this.labKey = new DevExpress.XtraEditors.LabelControl();
            this.labCreateDate = new DevExpress.XtraEditors.LabelControl();
            this.labCreateBy = new DevExpress.XtraEditors.LabelControl();
            this.txtCreateBy = new DevExpress.XtraEditors.TextEdit();
            this.dteCreateDate = new DevExpress.XtraEditors.DateEdit();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbConfigureKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // labValue
            // 
            this.labValue.Location = new System.Drawing.Point(4, 33);
            this.labValue.Name = "labValue";
            this.labValue.Size = new System.Drawing.Size(30, 14);
            this.labValue.TabIndex = 20;
            this.labValue.Text = "Value";
            // 
            // txtDescription
            // 
            this.txtDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Value", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtDescription.Location = new System.Drawing.Point(72, 30);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Properties.MaxLength = 500;
            this.txtDescription.Size = new System.Drawing.Size(249, 129);
            this.txtDescription.TabIndex = 2;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.ConfigureKeyValueInfo);
            // 
            // cmbConfigureKey
            // 
            this.cmbConfigureKey.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ConfigureKeyID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbConfigureKey.Location = new System.Drawing.Point(72, 3);
            this.cmbConfigureKey.MenuManager = this.barManager2;
            this.cmbConfigureKey.Name = "cmbConfigureKey";
            this.cmbConfigureKey.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbConfigureKey.Size = new System.Drawing.Size(249, 21);
            this.cmbConfigureKey.TabIndex = 1;
            // 
            // barManager2
            // 
            this.barManager2.MaxItemId = 0;
            // 
            // labKey
            // 
            this.labKey.Location = new System.Drawing.Point(4, 6);
            this.labKey.Name = "labKey";
            this.labKey.Size = new System.Drawing.Size(20, 14);
            this.labKey.TabIndex = 19;
            this.labKey.Text = "Key";
            // 
            // labCreateDate
            // 
            this.labCreateDate.Location = new System.Drawing.Point(4, 195);
            this.labCreateDate.Name = "labCreateDate";
            this.labCreateDate.Size = new System.Drawing.Size(66, 14);
            this.labCreateDate.TabIndex = 26;
            this.labCreateDate.Text = "Create Date";
            // 
            // labCreateBy
            // 
            this.labCreateBy.Location = new System.Drawing.Point(4, 168);
            this.labCreateBy.Name = "labCreateBy";
            this.labCreateBy.Size = new System.Drawing.Size(53, 14);
            this.labCreateBy.TabIndex = 25;
            this.labCreateBy.Text = "Create By";
            // 
            // txtCreateBy
            // 
            this.txtCreateBy.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "CreateByName", true));
            this.txtCreateBy.Location = new System.Drawing.Point(72, 165);
            this.txtCreateBy.Name = "txtCreateBy";
            this.txtCreateBy.Properties.ReadOnly = true;
            this.txtCreateBy.Size = new System.Drawing.Size(249, 21);
            this.txtCreateBy.TabIndex = 3;
            this.txtCreateBy.TabStop = false;
            // 
            // dteCreateDate
            // 
            this.dteCreateDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "CreateDate", true));
            this.dteCreateDate.EditValue = null;
            this.dteCreateDate.Location = new System.Drawing.Point(72, 192);
            this.dteCreateDate.Name = "dteCreateDate";
            this.dteCreateDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteCreateDate.Properties.Mask.EditMask = "";
            this.dteCreateDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteCreateDate.Properties.ReadOnly = true;
            this.dteCreateDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.dteCreateDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteCreateDate.Size = new System.Drawing.Size(249, 21);
            this.dteCreateDate.TabIndex = 4;
            this.dteCreateDate.TabStop = false;
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bindingSource1;
            // 
            // ConfigureKeyValueEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.labCreateDate);
            this.Controls.Add(this.labCreateBy);
            this.Controls.Add(this.txtCreateBy);
            this.Controls.Add(this.dteCreateDate);
            this.Controls.Add(this.cmbConfigureKey);
            this.Controls.Add(this.labValue);
            this.Controls.Add(this.labKey);
            this.Controls.Add(this.txtDescription);
            this.Name = "ConfigureKeyValueEditPart";
            this.Size = new System.Drawing.Size(331, 222);
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbConfigureKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.BarManager barManager2;
        private DevExpress.XtraEditors.LabelControl labValue;
        private DevExpress.XtraEditors.MemoEdit txtDescription;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbConfigureKey;
        private DevExpress.XtraEditors.LabelControl labKey;
        private DevExpress.XtraEditors.LabelControl labCreateDate;
        private DevExpress.XtraEditors.LabelControl labCreateBy;
        private DevExpress.XtraEditors.TextEdit txtCreateBy;
        private DevExpress.XtraEditors.DateEdit dteCreateDate;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
    }
}
