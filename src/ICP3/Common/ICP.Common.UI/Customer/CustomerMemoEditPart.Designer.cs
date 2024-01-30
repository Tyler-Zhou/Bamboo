namespace ICP.Common.UI.Customer
{
    partial class CustomerMemoEditPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerMemoEditPart));
            this.labCreateDate = new DevExpress.XtraEditors.LabelControl();
            this.labCreateBy = new DevExpress.XtraEditors.LabelControl();
            this.labSubject = new DevExpress.XtraEditors.LabelControl();
            this.labAttachmentName = new DevExpress.XtraEditors.LabelControl();
            this.txtCreateBy = new DevExpress.XtraEditors.TextEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.txtSubject = new DevExpress.XtraEditors.TextEdit();
            this.dteCreateDate = new DevExpress.XtraEditors.DateEdit();
            this.txtContent = new DevExpress.XtraEditors.MemoEdit();
            this.labContent = new DevExpress.XtraEditors.LabelControl();
            this.cmbType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labType = new DevExpress.XtraEditors.LabelControl();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.btnEditAttachment = new DevExpress.XtraEditors.ButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditAttachment.Properties)).BeginInit();
            this.SuspendLayout();
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
            // labSubject
            // 
            resources.ApplyResources(this.labSubject, "labSubject");
            this.labSubject.Name = "labSubject";
            // 
            // labAttachmentName
            // 
            resources.ApplyResources(this.labAttachmentName, "labAttachmentName");
            this.labAttachmentName.Name = "labAttachmentName";
            // 
            // txtCreateBy
            // 
            this.txtCreateBy.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "CreateByName", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            resources.ApplyResources(this.txtCreateBy, "txtCreateBy");
            this.txtCreateBy.Name = "txtCreateBy";
            this.txtCreateBy.Properties.ReadOnly = true;
            this.txtCreateBy.TabStop = false;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.CustomerMemoInfo);
            // 
            // txtSubject
            // 
            this.txtSubject.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Subject", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtSubject, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            resources.ApplyResources(this.txtSubject, "txtSubject");
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Properties.MaxLength = 200;
            // 
            // dteCreateDate
            // 
            this.dteCreateDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "CreateDate", true, System.Windows.Forms.DataSourceUpdateMode.Never));
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
            // txtContent
            // 
            this.txtContent.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Content", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtContent, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            resources.ApplyResources(this.txtContent, "txtContent");
            this.txtContent.Name = "txtContent";
            this.txtContent.Properties.MaxLength = 500;
            // 
            // labContent
            // 
            resources.ApplyResources(this.labContent, "labContent");
            this.labContent.Name = "labContent";
            // 
            // cmbType
            // 
            this.cmbType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "Type", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.cmbType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            resources.ApplyResources(this.cmbType, "cmbType");
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbType.Properties.Buttons"))))});
            // 
            // labType
            // 
            resources.ApplyResources(this.labType, "labType");
            this.labType.Name = "labType";
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bindingSource1;
            // 
            // btnEditAttachment
            // 
            this.btnEditAttachment.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "AttachmentName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.btnEditAttachment, "btnEditAttachment");
            this.btnEditAttachment.Name = "btnEditAttachment";
            this.btnEditAttachment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnEditAttachment.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.btnEditAttachment.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnEditAttachment_ButtonClick);
            // 
            // CustomerMemoEditPart
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnEditAttachment);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.labType);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.labContent);
            this.Controls.Add(this.labCreateDate);
            this.Controls.Add(this.labCreateBy);
            this.Controls.Add(this.labSubject);
            this.Controls.Add(this.labAttachmentName);
            this.Controls.Add(this.txtCreateBy);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.dteCreateDate);
            this.Name = "CustomerMemoEditPart";
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditAttachment.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labCreateDate;
        private DevExpress.XtraEditors.LabelControl labCreateBy;
        private DevExpress.XtraEditors.LabelControl labSubject;
        private DevExpress.XtraEditors.LabelControl labAttachmentName;
        private DevExpress.XtraEditors.TextEdit txtCreateBy;
        private DevExpress.XtraEditors.TextEdit txtSubject;
        private DevExpress.XtraEditors.DateEdit dteCreateDate;
        private DevExpress.XtraEditors.MemoEdit txtContent;
        private DevExpress.XtraEditors.LabelControl labContent;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbType;
        private DevExpress.XtraEditors.LabelControl labType;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private  DevExpress.XtraEditors.ButtonEdit  btnEditAttachment;
    }
}
