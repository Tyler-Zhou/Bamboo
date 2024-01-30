namespace ICP.Common.UI.TransportFoundation.Container
{
    partial class ContainerEditPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContainerEditPart));
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.labCreateDate = new DevExpress.XtraEditors.LabelControl();
            this.labCreateBy = new DevExpress.XtraEditors.LabelControl();
            this.labCode = new DevExpress.XtraEditors.LabelControl();
            this.labIsoCode = new DevExpress.XtraEditors.LabelControl();
            this.txtCreateBy = new DevExpress.XtraEditors.TextEdit();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.txtIsoCode = new DevExpress.XtraEditors.TextEdit();
            this.dteCreateDate = new DevExpress.XtraEditors.DateEdit();
            this.labTeu = new DevExpress.XtraEditors.LabelControl();
            this.numTeu = new DevExpress.XtraEditors.SpinEdit();
            this.txtDescription = new DevExpress.XtraEditors.MemoEdit();
            this.labDescription = new DevExpress.XtraEditors.LabelControl();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIsoCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTeu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.ContainerInfo);
            // 
            // labCreateDate
            // 
            resources.ApplyResources(this.labCreateDate, "labCreateDate");
            this.labCreateDate.Name = "labCreateDate";
            // 
            // labCreateBy
            // 
            resources.ApplyResources(this.labCreateBy, "labCreateBy");
            this.labCreateBy.Name = "labCreateBy";
            // 
            // labCode
            // 
            resources.ApplyResources(this.labCode, "labCode");
            this.labCode.Name = "labCode";
            // 
            // labIsoCode
            // 
            resources.ApplyResources(this.labIsoCode, "labIsoCode");
            this.labIsoCode.Name = "labIsoCode";
            // 
            // txtCreateBy
            // 
            this.txtCreateBy.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "CreateByName", true));
            resources.ApplyResources(this.txtCreateBy, "txtCreateBy");
            this.txtCreateBy.Name = "txtCreateBy";
            this.txtCreateBy.Properties.ReadOnly = true;
            this.txtCreateBy.TabStop = false;
            // 
            // txtCode
            // 
            this.txtCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Code", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtCode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            resources.ApplyResources(this.txtCode, "txtCode");
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.MaxLength = 10;
            // 
            // txtIsoCode
            // 
            this.txtIsoCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ISOCode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtIsoCode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            resources.ApplyResources(this.txtIsoCode, "txtIsoCode");
            this.txtIsoCode.Name = "txtIsoCode";
            this.txtIsoCode.Properties.MaxLength = 10;
            // 
            // dteCreateDate
            // 
            this.dteCreateDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "CreateDate", true));
            this.dteCreateDate.EditValue = null;
            resources.ApplyResources(this.dteCreateDate, "dteCreateDate");
            this.dteCreateDate.Name = "dteCreateDate";
            this.dteCreateDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dteCreateDate.Properties.Buttons"))))});
            this.dteCreateDate.Properties.Mask.EditMask = resources.GetString("dteCreateDate.Properties.Mask.EditMask");
            this.dteCreateDate.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("dteCreateDate.Properties.Mask.MaskType")));
            this.dteCreateDate.Properties.ReadOnly = true;
            this.dteCreateDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteCreateDate.TabStop = false;
            // 
            // labTeu
            // 
            resources.ApplyResources(this.labTeu, "labTeu");
            this.labTeu.Name = "labTeu";
            // 
            // numTeu
            // 
            this.numTeu.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "TEU", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.numTeu, "numTeu");
            this.dxErrorProvider1.SetIconAlignment(this.numTeu, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.numTeu.Name = "numTeu";
            this.numTeu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numTeu.Properties.Mask.EditMask = resources.GetString("numTeu.Properties.Mask.EditMask");
            this.numTeu.Properties.MaxLength = 2;
            this.numTeu.Properties.MaxValue = new decimal(new int[] {
            4,
            0,
            0,
            0});
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
            // ContainerEditPart
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.labDescription);
            this.Controls.Add(this.numTeu);
            this.Controls.Add(this.labCreateDate);
            this.Controls.Add(this.labCreateBy);
            this.Controls.Add(this.labTeu);
            this.Controls.Add(this.labCode);
            this.Controls.Add(this.labIsoCode);
            this.Controls.Add(this.txtCreateBy);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtIsoCode);
            this.Controls.Add(this.dteCreateDate);
            this.Name = "ContainerEditPart";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIsoCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTeu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labCreateDate;
        private DevExpress.XtraEditors.LabelControl labCreateBy;
        private DevExpress.XtraEditors.LabelControl labCode;
        private DevExpress.XtraEditors.LabelControl labIsoCode;
        private DevExpress.XtraEditors.TextEdit txtCreateBy;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.TextEdit txtIsoCode;
        private DevExpress.XtraEditors.DateEdit dteCreateDate;
        private DevExpress.XtraEditors.LabelControl labTeu;
        private DevExpress.XtraEditors.SpinEdit numTeu;
        private DevExpress.XtraEditors.MemoEdit txtDescription;
        private DevExpress.XtraEditors.LabelControl labDescription;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
    }
}
