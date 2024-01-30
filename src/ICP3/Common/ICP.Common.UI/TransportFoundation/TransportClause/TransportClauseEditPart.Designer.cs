namespace ICP.Common.UI.TransportFoundation.TransportClause
{
    partial class TransportClauseEditPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransportClauseEditPart));
            this.labOriginal = new DevExpress.XtraEditors.LabelControl();
            this.labDestination = new DevExpress.XtraEditors.LabelControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.txtDescription = new DevExpress.XtraEditors.MemoEdit();
            this.labDescription = new DevExpress.XtraEditors.LabelControl();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.cmbOriginalCode = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbDestinationCode = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOriginalCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDestinationCode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labOriginal
            // 
            resources.ApplyResources(this.labOriginal, "labOriginal");
            this.labOriginal.Name = "labOriginal";
            // 
            // labDestination
            // 
            resources.ApplyResources(this.labDestination, "labDestination");
            this.labDestination.Name = "labDestination";
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.TransportClauseInfo);
            // 
            // txtDescription
            // 
            this.txtDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Description", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtDescription, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            resources.ApplyResources(this.txtDescription, "txtDescription");
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Properties.MaxLength = 500;
            // 
            // labDescription
            // 
            resources.ApplyResources(this.labDescription, "labDescription");
            this.labDescription.Name = "labDescription";
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bindingSource1;
            // 
            // cmbOriginalCode
            // 
            this.cmbOriginalCode.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "OriginalCodeID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbOriginalCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "OriginalCode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbOriginalCode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            resources.ApplyResources(this.cmbOriginalCode, "cmbOriginalCode");
            this.cmbOriginalCode.Name = "cmbOriginalCode";
            this.cmbOriginalCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbOriginalCode.Properties.Appearance.Options.UseBackColor = true;
            this.cmbOriginalCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbOriginalCode.Properties.Buttons"))))});
            // 
            // cmbDestinationCode
            // 
            this.cmbDestinationCode.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "DestinationCodeID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmbDestinationCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "DestinationCode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbDestinationCode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            resources.ApplyResources(this.cmbDestinationCode, "cmbDestinationCode");
            this.cmbDestinationCode.Name = "cmbDestinationCode";
            this.cmbDestinationCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbDestinationCode.Properties.Appearance.Options.UseBackColor = true;
            this.cmbDestinationCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbDestinationCode.Properties.Buttons"))))});
            // 
            // TransportClauseEditPart
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbDestinationCode);
            this.Controls.Add(this.cmbOriginalCode);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.labDescription);
            this.Controls.Add(this.labOriginal);
            this.Controls.Add(this.labDestination);
            this.Name = "TransportClauseEditPart";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOriginalCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDestinationCode.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labOriginal;
        private DevExpress.XtraEditors.LabelControl labDestination;
        private DevExpress.XtraEditors.MemoEdit txtDescription;
        private DevExpress.XtraEditors.LabelControl labDescription;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbDestinationCode;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbOriginalCode;
    }
}
