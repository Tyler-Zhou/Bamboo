namespace ICP.FCM.Common.UI.Memo
{
    partial class EditMemoPart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.txtSubject = new DevExpress.XtraEditors.TextEdit();
            this.chkIsShowAgent = new DevExpress.XtraEditors.CheckEdit();
            this.chkIsShowCustomer = new DevExpress.XtraEditors.CheckEdit();
            this.txtCreateBy = new DevExpress.XtraEditors.TextEdit();
            this.dteCreateDate = new DevExpress.XtraEditors.DateEdit();
            this.cmbType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labType = new DevExpress.XtraEditors.LabelControl();
            this.labSubject = new DevExpress.XtraEditors.LabelControl();
            this.labAttachmentName = new DevExpress.XtraEditors.LabelControl();
            this.labCreateBy = new DevExpress.XtraEditors.LabelControl();
            this.labCreateDate = new DevExpress.XtraEditors.LabelControl();
            this.txtContent = new DevExpress.XtraEditors.MemoEdit();
            this.labContent = new DevExpress.XtraEditors.LabelControl();
            this.txtAttachmentName = new DevExpress.XtraEditors.ButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsShowAgent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsShowCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAttachmentName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.FCM.Common.ServiceInterface.DataObjects.CommonMemoInfo);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(189, 247);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bindingSource1;
            // 
            // txtSubject
            // 
            this.txtSubject.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "Subject", true));
            this.txtSubject.Location = new System.Drawing.Point(81, 3);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(276, 21);
            this.txtSubject.TabIndex = 15;
            // 
            // chkIsShowAgent
            // 
            this.chkIsShowAgent.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "IsShowAgent", true));
            this.chkIsShowAgent.Location = new System.Drawing.Point(81, 166);
            this.chkIsShowAgent.Name = "chkIsShowAgent";
            this.chkIsShowAgent.Properties.Caption = "Is Show Agent";
            this.chkIsShowAgent.Size = new System.Drawing.Size(356, 19);
            this.chkIsShowAgent.TabIndex = 17;
            // 
            // chkIsShowCustomer
            // 
            this.chkIsShowCustomer.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "IsShowCustomer", true));
            this.chkIsShowCustomer.Location = new System.Drawing.Point(81, 141);
            this.chkIsShowCustomer.Name = "chkIsShowCustomer";
            this.chkIsShowCustomer.Properties.Caption = "Is Show Customer";
            this.chkIsShowCustomer.Size = new System.Drawing.Size(356, 19);
            this.chkIsShowCustomer.TabIndex = 18;
            // 
            // txtCreateBy
            // 
            this.txtCreateBy.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "CreateByName", true));
            this.txtCreateBy.Location = new System.Drawing.Point(81, 193);
            this.txtCreateBy.Name = "txtCreateBy";
            this.txtCreateBy.Properties.ReadOnly = true;
            this.txtCreateBy.Size = new System.Drawing.Size(276, 21);
            this.txtCreateBy.TabIndex = 19;
            // 
            // dteCreateDate
            // 
            this.dteCreateDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "CreateDate", true));
            this.dteCreateDate.EditValue = null;
            this.dteCreateDate.Location = new System.Drawing.Point(81, 220);
            this.dteCreateDate.Name = "dteCreateDate";
            this.dteCreateDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteCreateDate.Properties.ReadOnly = true;
            this.dteCreateDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteCreateDate.Size = new System.Drawing.Size(276, 21);
            this.dteCreateDate.TabIndex = 20;
            // 
            // cmbType
            // 
            this.cmbType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "Type", true));
            this.cmbType.Location = new System.Drawing.Point(81, 87);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Size = new System.Drawing.Size(276, 21);
            this.cmbType.TabIndex = 8;
            this.cmbType.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(282, 247);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labType
            // 
            this.labType.Location = new System.Drawing.Point(12, 90);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(28, 14);
            this.labType.TabIndex = 21;
            this.labType.Text = "Type";
            // 
            // labSubject
            // 
            this.labSubject.Location = new System.Drawing.Point(12, 6);
            this.labSubject.Name = "labSubject";
            this.labSubject.Size = new System.Drawing.Size(42, 14);
            this.labSubject.TabIndex = 21;
            this.labSubject.Text = "Subject";
            // 
            // labAttachmentName
            // 
            this.labAttachmentName.Location = new System.Drawing.Point(12, 117);
            this.labAttachmentName.Name = "labAttachmentName";
            this.labAttachmentName.Size = new System.Drawing.Size(66, 14);
            this.labAttachmentName.TabIndex = 21;
            this.labAttachmentName.Text = "Attachment";
            // 
            // labCreateBy
            // 
            this.labCreateBy.Location = new System.Drawing.Point(12, 200);
            this.labCreateBy.Name = "labCreateBy";
            this.labCreateBy.Size = new System.Drawing.Size(49, 14);
            this.labCreateBy.TabIndex = 21;
            this.labCreateBy.Text = "CreateBy";
            // 
            // labCreateDate
            // 
            this.labCreateDate.Location = new System.Drawing.Point(12, 223);
            this.labCreateDate.Name = "labCreateDate";
            this.labCreateDate.Size = new System.Drawing.Size(62, 14);
            this.labCreateDate.TabIndex = 21;
            this.labCreateDate.Text = "CreateDate";
            // 
            // txtContent
            // 
            this.txtContent.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "Content", true));
            this.txtContent.Location = new System.Drawing.Point(81, 30);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(276, 51);
            this.txtContent.TabIndex = 16;
            this.txtContent.TabStop = false;
            // 
            // labContent
            // 
            this.labContent.Location = new System.Drawing.Point(12, 33);
            this.labContent.Name = "labContent";
            this.labContent.Size = new System.Drawing.Size(45, 14);
            this.labContent.TabIndex = 21;
            this.labContent.Text = "Content";
            // 
            // txtAttachmentName
            // 
            this.txtAttachmentName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "AttachmentName", true));
            this.txtAttachmentName.Location = new System.Drawing.Point(81, 114);
            this.txtAttachmentName.Name = "txtAttachmentName";
            this.txtAttachmentName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtAttachmentName.Size = new System.Drawing.Size(276, 21);
            this.txtAttachmentName.TabIndex = 6;
            this.txtAttachmentName.TabStop = false;
            this.txtAttachmentName.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtAttachmentName_ButtonClick);
            // 
            // EditMemoPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labContent);
            this.Controls.Add(this.labCreateDate);
            this.Controls.Add(this.labCreateBy);
            this.Controls.Add(this.labAttachmentName);
            this.Controls.Add(this.labSubject);
            this.Controls.Add(this.labType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dteCreateDate);
            this.Controls.Add(this.txtCreateBy);
            this.Controls.Add(this.chkIsShowAgent);
            this.Controls.Add(this.chkIsShowCustomer);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.txtAttachmentName);
            this.Name = "EditMemoPart";
            this.Size = new System.Drawing.Size(374, 279);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsShowAgent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsShowCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAttachmentName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraEditors.TextEdit txtSubject;
        private DevExpress.XtraEditors.CheckEdit chkIsShowAgent;
        private DevExpress.XtraEditors.CheckEdit chkIsShowCustomer;
        private DevExpress.XtraEditors.TextEdit txtCreateBy;
        private DevExpress.XtraEditors.DateEdit dteCreateDate;
        private DevExpress.XtraEditors.LabelControl labType;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbType;
        private DevExpress.XtraEditors.LabelControl labSubject;
        private DevExpress.XtraEditors.LabelControl labAttachmentName;
        private DevExpress.XtraEditors.LabelControl labCreateBy;
        private DevExpress.XtraEditors.LabelControl labCreateDate;
        private DevExpress.XtraEditors.LabelControl labContent;
        private DevExpress.XtraEditors.MemoEdit txtContent;
        private DevExpress.XtraEditors.ButtonEdit txtAttachmentName;
    }
}
