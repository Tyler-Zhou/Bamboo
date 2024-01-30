namespace ICP.FCM.Common.UI.Document
{
    partial class EditDocumentPart
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
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labCreateDate = new DevExpress.XtraEditors.LabelControl();
            this.labCreateBy = new DevExpress.XtraEditors.LabelControl();
            this.dteCreateDate = new DevExpress.XtraEditors.DateEdit();
            this.txtCreateBy = new DevExpress.XtraEditors.TextEdit();
            this.seNumberOfCopies = new DevExpress.XtraEditors.SpinEdit();
            this.labDocumentNo = new DevExpress.XtraEditors.LabelControl();
            this.txtDocumentNo = new DevExpress.XtraEditors.TextEdit();
            this.txtTrackingNo = new DevExpress.XtraEditors.TextEdit();
            this.RemarkMemoEdit = new DevExpress.XtraEditors.MemoEdit();
            this.dteReceivedDate = new DevExpress.XtraEditors.DateEdit();
            this.seNumberOfOriginal = new DevExpress.XtraEditors.SpinEdit();
            this.AttachmentNameTextEdit = new DevExpress.XtraEditors.ButtonEdit();
            this.cmbReleaseMode = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.cmbDocumentType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labNumberOfCopies = new DevExpress.XtraEditors.LabelControl();
            this.labNumberOfOriginal = new DevExpress.XtraEditors.LabelControl();
            this.dteReturnDate = new DevExpress.XtraEditors.DateEdit();
            this.dteReleaseDate = new DevExpress.XtraEditors.DateEdit();
            this.labTrackingNo = new DevExpress.XtraEditors.LabelControl();
            this.labReleaseMode = new DevExpress.XtraEditors.LabelControl();
            this.labReleaseDate = new DevExpress.XtraEditors.LabelControl();
            this.labAttachment = new DevExpress.XtraEditors.LabelControl();
            this.labRemark = new DevExpress.XtraEditors.LabelControl();
            this.labReturnDate = new DevExpress.XtraEditors.LabelControl();
            this.labReceivedDate = new DevExpress.XtraEditors.LabelControl();
            this.labDocumentType = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seNumberOfCopies.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDocumentNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTrackingNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemarkMemoEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReceivedDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReceivedDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seNumberOfOriginal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttachmentNameTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReleaseMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDocumentType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReturnDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReturnDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReleaseDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReleaseDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.FCM.Common.ServiceInterface.DataObjects.CommonDocumentInfo);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(237, 338);
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
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(330, 338);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labCreateDate
            // 
            this.labCreateDate.Location = new System.Drawing.Point(5, 304);
            this.labCreateDate.Name = "labCreateDate";
            this.labCreateDate.Size = new System.Drawing.Size(62, 14);
            this.labCreateDate.TabIndex = 28;
            this.labCreateDate.Text = "CreateDate";
            // 
            // labCreateBy
            // 
            this.labCreateBy.Location = new System.Drawing.Point(5, 281);
            this.labCreateBy.Name = "labCreateBy";
            this.labCreateBy.Size = new System.Drawing.Size(49, 14);
            this.labCreateBy.TabIndex = 29;
            this.labCreateBy.Text = "CreateBy";
            // 
            // dteCreateDate
            // 
            this.dteCreateDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "CreateDate", true));
            this.dteCreateDate.EditValue = null;
            this.dteCreateDate.Location = new System.Drawing.Point(106, 305);
            this.dteCreateDate.Name = "dteCreateDate";
            this.dteCreateDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteCreateDate.Properties.ReadOnly = true;
            this.dteCreateDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteCreateDate.Size = new System.Drawing.Size(299, 21);
            this.dteCreateDate.TabIndex = 27;
            // 
            // txtCreateBy
            // 
            this.txtCreateBy.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "CreateByName", true));
            this.txtCreateBy.Location = new System.Drawing.Point(106, 278);
            this.txtCreateBy.Name = "txtCreateBy";
            this.txtCreateBy.Properties.ReadOnly = true;
            this.txtCreateBy.Size = new System.Drawing.Size(299, 21);
            this.txtCreateBy.TabIndex = 26;
            // 
            // seNumberOfCopies
            // 
            this.seNumberOfCopies.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "NumberOfCopies", true));
            this.seNumberOfCopies.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seNumberOfCopies.Location = new System.Drawing.Point(303, 31);
            this.seNumberOfCopies.Name = "seNumberOfCopies";
            this.seNumberOfCopies.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seNumberOfCopies.Size = new System.Drawing.Size(102, 21);
            this.seNumberOfCopies.TabIndex = 10;
            // 
            // labDocumentNo
            // 
            this.labDocumentNo.Location = new System.Drawing.Point(5, 10);
            this.labDocumentNo.Name = "labDocumentNo";
            this.labDocumentNo.Size = new System.Drawing.Size(72, 14);
            this.labDocumentNo.TabIndex = 30;
            this.labDocumentNo.Text = "DocumentNo";
            // 
            // txtDocumentNo
            // 
            this.txtDocumentNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "DocumentNo", true));
            this.txtDocumentNo.Location = new System.Drawing.Point(106, 3);
            this.txtDocumentNo.Name = "txtDocumentNo";
            this.txtDocumentNo.Size = new System.Drawing.Size(299, 21);
            this.txtDocumentNo.TabIndex = 31;
            // 
            // txtTrackingNo
            // 
            this.txtTrackingNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "TrackingNo", true));
            this.txtTrackingNo.Location = new System.Drawing.Point(106, 165);
            this.txtTrackingNo.Name = "txtTrackingNo";
            this.txtTrackingNo.Size = new System.Drawing.Size(299, 21);
            this.txtTrackingNo.TabIndex = 15;
            // 
            // RemarkMemoEdit
            // 
            this.RemarkMemoEdit.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Remark", true));
            this.RemarkMemoEdit.Location = new System.Drawing.Point(106, 219);
            this.RemarkMemoEdit.Name = "RemarkMemoEdit";
            this.RemarkMemoEdit.Size = new System.Drawing.Size(299, 53);
            this.RemarkMemoEdit.TabIndex = 18;
            // 
            // dteReceivedDate
            // 
            this.dteReceivedDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ReceivedDate", true));
            this.dteReceivedDate.EditValue = null;
            this.dteReceivedDate.Location = new System.Drawing.Point(106, 84);
            this.dteReceivedDate.Name = "dteReceivedDate";
            this.dteReceivedDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteReceivedDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteReceivedDate.Size = new System.Drawing.Size(299, 21);
            this.dteReceivedDate.TabIndex = 11;
            // 
            // seNumberOfOriginal
            // 
            this.seNumberOfOriginal.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "NumberOfOriginal", true));
            this.seNumberOfOriginal.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seNumberOfOriginal.Location = new System.Drawing.Point(106, 30);
            this.seNumberOfOriginal.Name = "seNumberOfOriginal";
            this.seNumberOfOriginal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seNumberOfOriginal.Size = new System.Drawing.Size(102, 21);
            this.seNumberOfOriginal.TabIndex = 9;
            // 
            // AttachmentNameTextEdit
            // 
            this.AttachmentNameTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "AttachmentName", true));
            this.AttachmentNameTextEdit.Location = new System.Drawing.Point(106, 192);
            this.AttachmentNameTextEdit.Name = "AttachmentNameTextEdit";
            this.AttachmentNameTextEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.AttachmentNameTextEdit.Size = new System.Drawing.Size(299, 21);
            this.AttachmentNameTextEdit.TabIndex = 16;
            this.AttachmentNameTextEdit.TabStop = false;
            this.AttachmentNameTextEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtAttachmentName_ButtonClick);
            // 
            // cmbReleaseMode
            // 
            this.cmbReleaseMode.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ReleaseMode", true));
            this.cmbReleaseMode.Location = new System.Drawing.Point(303, 111);
            this.cmbReleaseMode.Name = "cmbReleaseMode";
            this.cmbReleaseMode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbReleaseMode.Size = new System.Drawing.Size(102, 21);
            this.cmbReleaseMode.TabIndex = 13;
            this.cmbReleaseMode.TabStop = false;
            // 
            // cmbDocumentType
            // 
            this.cmbDocumentType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "DocumentType", true));
            this.cmbDocumentType.Location = new System.Drawing.Point(106, 57);
            this.cmbDocumentType.Name = "cmbDocumentType";
            this.cmbDocumentType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDocumentType.Size = new System.Drawing.Size(299, 21);
            this.cmbDocumentType.TabIndex = 8;
            this.cmbDocumentType.TabStop = false;
            // 
            // labNumberOfCopies
            // 
            this.labNumberOfCopies.Location = new System.Drawing.Point(210, 34);
            this.labNumberOfCopies.Name = "labNumberOfCopies";
            this.labNumberOfCopies.Size = new System.Drawing.Size(91, 14);
            this.labNumberOfCopies.TabIndex = 30;
            this.labNumberOfCopies.Text = "NumberOfCopies";
            // 
            // labNumberOfOriginal
            // 
            this.labNumberOfOriginal.Location = new System.Drawing.Point(5, 34);
            this.labNumberOfOriginal.Name = "labNumberOfOriginal";
            this.labNumberOfOriginal.Size = new System.Drawing.Size(95, 14);
            this.labNumberOfOriginal.TabIndex = 30;
            this.labNumberOfOriginal.Text = "NumberOfOriginal";
            // 
            // dteReturnDate
            // 
            this.dteReturnDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ReturnDate", true));
            this.dteReturnDate.EditValue = null;
            this.dteReturnDate.Location = new System.Drawing.Point(106, 138);
            this.dteReturnDate.Name = "dteReturnDate";
            this.dteReturnDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteReturnDate.Properties.Mask.EditMask = "";
            this.dteReturnDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteReturnDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteReturnDate.Size = new System.Drawing.Size(299, 21);
            this.dteReturnDate.TabIndex = 14;
            this.dteReturnDate.TabStop = false;
            // 
            // dteReleaseDate
            // 
            this.dteReleaseDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ReleaseDate", true));
            this.dteReleaseDate.EditValue = null;
            this.dteReleaseDate.Location = new System.Drawing.Point(106, 111);
            this.dteReleaseDate.Name = "dteReleaseDate";
            this.dteReleaseDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteReleaseDate.Properties.Mask.EditMask = "";
            this.dteReleaseDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteReleaseDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteReleaseDate.Size = new System.Drawing.Size(102, 21);
            this.dteReleaseDate.TabIndex = 12;
            this.dteReleaseDate.TabStop = false;
            // 
            // labTrackingNo
            // 
            this.labTrackingNo.Location = new System.Drawing.Point(5, 168);
            this.labTrackingNo.Name = "labTrackingNo";
            this.labTrackingNo.Size = new System.Drawing.Size(61, 14);
            this.labTrackingNo.TabIndex = 30;
            this.labTrackingNo.Text = "TrackingNo";
            // 
            // labReleaseMode
            // 
            this.labReleaseMode.Location = new System.Drawing.Point(210, 114);
            this.labReleaseMode.Name = "labReleaseMode";
            this.labReleaseMode.Size = new System.Drawing.Size(71, 14);
            this.labReleaseMode.TabIndex = 30;
            this.labReleaseMode.Text = "ReleaseMode";
            // 
            // labReleaseDate
            // 
            this.labReleaseDate.Location = new System.Drawing.Point(5, 114);
            this.labReleaseDate.Name = "labReleaseDate";
            this.labReleaseDate.Size = new System.Drawing.Size(67, 14);
            this.labReleaseDate.TabIndex = 30;
            this.labReleaseDate.Text = "ReleaseDate";
            // 
            // labAttachment
            // 
            this.labAttachment.Location = new System.Drawing.Point(5, 195);
            this.labAttachment.Name = "labAttachment";
            this.labAttachment.Size = new System.Drawing.Size(66, 14);
            this.labAttachment.TabIndex = 30;
            this.labAttachment.Text = "Attachment";
            // 
            // labRemark
            // 
            this.labRemark.Location = new System.Drawing.Point(5, 222);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(40, 14);
            this.labRemark.TabIndex = 30;
            this.labRemark.Text = "Remark";
            // 
            // labReturnDate
            // 
            this.labReturnDate.Location = new System.Drawing.Point(5, 141);
            this.labReturnDate.Name = "labReturnDate";
            this.labReturnDate.Size = new System.Drawing.Size(63, 14);
            this.labReturnDate.TabIndex = 30;
            this.labReturnDate.Text = "ReturnDate";
            // 
            // labReceivedDate
            // 
            this.labReceivedDate.Location = new System.Drawing.Point(5, 87);
            this.labReceivedDate.Name = "labReceivedDate";
            this.labReceivedDate.Size = new System.Drawing.Size(75, 14);
            this.labReceivedDate.TabIndex = 30;
            this.labReceivedDate.Text = "ReceivedDate";
            // 
            // labDocumentType
            // 
            this.labDocumentType.Location = new System.Drawing.Point(5, 64);
            this.labDocumentType.Name = "labDocumentType";
            this.labDocumentType.Size = new System.Drawing.Size(85, 14);
            this.labDocumentType.TabIndex = 30;
            this.labDocumentType.Text = "DocumentType";
            // 
            // EditDocumentPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtDocumentNo);
            this.Controls.Add(this.labNumberOfOriginal);
            this.Controls.Add(this.labDocumentType);
            this.Controls.Add(this.labReceivedDate);
            this.Controls.Add(this.labReleaseDate);
            this.Controls.Add(this.labReleaseMode);
            this.Controls.Add(this.labRemark);
            this.Controls.Add(this.labAttachment);
            this.Controls.Add(this.labReturnDate);
            this.Controls.Add(this.labTrackingNo);
            this.Controls.Add(this.labNumberOfCopies);
            this.Controls.Add(this.labDocumentNo);
            this.Controls.Add(this.labCreateDate);
            this.Controls.Add(this.labCreateBy);
            this.Controls.Add(this.dteCreateDate);
            this.Controls.Add(this.txtCreateBy);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cmbDocumentType);
            this.Controls.Add(this.cmbReleaseMode);
            this.Controls.Add(this.AttachmentNameTextEdit);
            this.Controls.Add(this.seNumberOfOriginal);
            this.Controls.Add(this.seNumberOfCopies);
            this.Controls.Add(this.dteReceivedDate);
            this.Controls.Add(this.RemarkMemoEdit);
            this.Controls.Add(this.txtTrackingNo);
            this.Controls.Add(this.dteReturnDate);
            this.Controls.Add(this.dteReleaseDate);
            this.Name = "EditDocumentPart";
            this.Size = new System.Drawing.Size(437, 382);
            this.Load += new System.EventHandler(this.EditDocumentPart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seNumberOfCopies.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDocumentNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTrackingNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemarkMemoEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReceivedDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReceivedDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seNumberOfOriginal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttachmentNameTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReleaseMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDocumentType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReturnDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReturnDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReleaseDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReleaseDate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl labCreateDate;
        private DevExpress.XtraEditors.LabelControl labCreateBy;
        private DevExpress.XtraEditors.DateEdit dteCreateDate;
        private DevExpress.XtraEditors.TextEdit txtCreateBy;
        private DevExpress.XtraEditors.TextEdit txtDocumentNo;
        private DevExpress.XtraEditors.LabelControl labDocumentNo;
        private DevExpress.XtraEditors.SpinEdit seNumberOfCopies;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbDocumentType;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbReleaseMode;
        private DevExpress.XtraEditors.ButtonEdit AttachmentNameTextEdit;
        private DevExpress.XtraEditors.SpinEdit seNumberOfOriginal;
        private DevExpress.XtraEditors.DateEdit dteReceivedDate;
        private DevExpress.XtraEditors.MemoEdit RemarkMemoEdit;
        private DevExpress.XtraEditors.TextEdit txtTrackingNo;
        private DevExpress.XtraEditors.LabelControl labNumberOfOriginal;
        private DevExpress.XtraEditors.LabelControl labNumberOfCopies;
        private DevExpress.XtraEditors.DateEdit dteReturnDate;
        private DevExpress.XtraEditors.DateEdit dteReleaseDate;
        private DevExpress.XtraEditors.LabelControl labTrackingNo;
        private DevExpress.XtraEditors.LabelControl labDocumentType;
        private DevExpress.XtraEditors.LabelControl labReceivedDate;
        private DevExpress.XtraEditors.LabelControl labReleaseDate;
        private DevExpress.XtraEditors.LabelControl labReleaseMode;
        private DevExpress.XtraEditors.LabelControl labRemark;
        private DevExpress.XtraEditors.LabelControl labAttachment;
        private DevExpress.XtraEditors.LabelControl labReturnDate;
    }
}
