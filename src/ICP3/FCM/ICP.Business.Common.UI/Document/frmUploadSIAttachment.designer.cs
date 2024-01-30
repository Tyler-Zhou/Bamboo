namespace ICP.Business.Common.UI.Document
{
    partial class frmUploadSIAttachment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUploadSIAttachment));
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.chkOverride = new DevExpress.XtraEditors.CheckEdit();
            this.cbxEmailAsAttachment = new DevExpress.XtraEditors.CheckEdit();
            this.pnlAttachment = new DevExpress.XtraEditors.PanelControl();
            this.cmbDocumentType = new ICP.Common.UI.Controls.LWComboBoxTree();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barDocumentType1 = new DevExpress.XtraBars.BarEditItem();
            this.pnlCustomer = new DevExpress.XtraEditors.PanelControl();
            this.pnlCustomerList = new DevExpress.XtraEditors.PanelControl();
            this.pnlCustomerToolBar = new DevExpress.XtraEditors.PanelControl();
            this.pblToolBar = new DevExpress.XtraEditors.PanelControl();
            this.lblSaveTip = new DevExpress.XtraEditors.LabelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.cmbActionType = new ICP.Common.UI.Controls.LWComboBoxTree();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkOverride.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxEmailAsAttachment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlAttachment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDocumentType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCustomer)).BeginInit();
            this.pnlCustomer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCustomerList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCustomerToolBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pblToolBar)).BeginInit();
            this.pblToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbActionType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.chkOverride);
            this.pnlTop.Controls.Add(this.cbxEmailAsAttachment);
            this.pnlTop.Controls.Add(this.pnlAttachment);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTop.Location = new System.Drawing.Point(2, 32);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(756, 249);
            this.pnlTop.TabIndex = 20;
            // 
            // chkOverride
            // 
            this.chkOverride.EditValue = true;
            this.chkOverride.Location = new System.Drawing.Point(315, 6);
            this.chkOverride.Name = "chkOverride";
            this.chkOverride.Properties.Caption = "override if it\'s duplicated";
            this.chkOverride.Size = new System.Drawing.Size(238, 19);
            this.chkOverride.TabIndex = 44;
            this.chkOverride.EditValueChanged += new System.EventHandler(this.chkOverride_EditValueChanged);
            // 
            // cbxEmailAsAttachment
            // 
            this.cbxEmailAsAttachment.Location = new System.Drawing.Point(12, 6);
            this.cbxEmailAsAttachment.Name = "cbxEmailAsAttachment";
            this.cbxEmailAsAttachment.Properties.Caption = "Upload the current mail as an attachment";
            this.cbxEmailAsAttachment.Size = new System.Drawing.Size(284, 19);
            this.cbxEmailAsAttachment.TabIndex = 31;
            this.cbxEmailAsAttachment.CheckedChanged += new System.EventHandler(this.cbxEmailAsAttachment_CheckedChanged);
            // 
            // pnlAttachment
            // 
            this.pnlAttachment.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAttachment.Location = new System.Drawing.Point(2, 26);
            this.pnlAttachment.Name = "pnlAttachment";
            this.pnlAttachment.Size = new System.Drawing.Size(752, 221);
            this.pnlAttachment.TabIndex = 21;
            // 
            // cmbDocumentType
            // 
            this.cmbDocumentType.AllowMultSelect = false;
            this.cmbDocumentType.DataSource = null;
            this.cmbDocumentType.DisplayMember = "CName";
            this.cmbDocumentType.EditValue = "-- Attachment Type --";
            this.cmbDocumentType.Location = new System.Drawing.Point(136, 5);
            this.cmbDocumentType.Name = "cmbDocumentType";
            this.cmbDocumentType.ParentMember = "ParentID";
            this.cmbDocumentType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.cmbDocumentType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDocumentType.Properties.PopupSizeable = false;
            this.cmbDocumentType.Properties.ShowPopupCloseButton = false;
            this.cmbDocumentType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbDocumentType.RootValue = 0;
            this.cmbDocumentType.SelectedValue = null;
            this.cmbDocumentType.Separator = ",";
            this.cmbDocumentType.Size = new System.Drawing.Size(150, 19);
            this.cmbDocumentType.TabIndex = 43;
            this.cmbDocumentType.ValueMember = "ID";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 283);
            this.barDockControlTop.Size = new System.Drawing.Size(760, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 595);
            this.barDockControlBottom.Size = new System.Drawing.Size(760, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 283);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 312);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(760, 283);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 312);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "j";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "barSubItem1";
            this.barSubItem1.Id = 1;
            this.barSubItem1.Name = "barSubItem1";
            // 
            // barDocumentType1
            // 
            this.barDocumentType1.Edit = null;
            this.barDocumentType1.Id = -1;
            this.barDocumentType1.Name = "barDocumentType1";
            // 
            // pnlCustomer
            // 
            this.pnlCustomer.Controls.Add(this.pnlCustomerList);
            this.pnlCustomer.Controls.Add(this.pnlCustomerToolBar);
            this.pnlCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCustomer.Location = new System.Drawing.Point(0, 283);
            this.pnlCustomer.Name = "pnlCustomer";
            this.pnlCustomer.Size = new System.Drawing.Size(760, 312);
            this.pnlCustomer.TabIndex = 21;
            this.pnlCustomer.Visible = false;
            // 
            // pnlCustomerList
            // 
            this.pnlCustomerList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCustomerList.Location = new System.Drawing.Point(2, 31);
            this.pnlCustomerList.Name = "pnlCustomerList";
            this.pnlCustomerList.Size = new System.Drawing.Size(756, 279);
            this.pnlCustomerList.TabIndex = 1;
            // 
            // pnlCustomerToolBar
            // 
            this.pnlCustomerToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCustomerToolBar.Location = new System.Drawing.Point(2, 2);
            this.pnlCustomerToolBar.Name = "pnlCustomerToolBar";
            this.pnlCustomerToolBar.Size = new System.Drawing.Size(756, 29);
            this.pnlCustomerToolBar.TabIndex = 0;
            // 
            // pblToolBar
            // 
            this.pblToolBar.Controls.Add(this.lblSaveTip);
            this.pblToolBar.Controls.Add(this.btnClose);
            this.pblToolBar.Controls.Add(this.btnSave);
            this.pblToolBar.Controls.Add(this.cmbActionType);
            this.pblToolBar.Controls.Add(this.cmbDocumentType);
            this.pblToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pblToolBar.Location = new System.Drawing.Point(2, 2);
            this.pblToolBar.Name = "pblToolBar";
            this.pblToolBar.Size = new System.Drawing.Size(756, 30);
            this.pblToolBar.TabIndex = 26;
            // 
            // lblSaveTip
            // 
            this.lblSaveTip.Location = new System.Drawing.Point(504, 11);
            this.lblSaveTip.Name = "lblSaveTip";
            this.lblSaveTip.Size = new System.Drawing.Size(0, 14);
            this.lblSaveTip.TabIndex = 45;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(408, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 44;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(317, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 44;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // cmbActionType
            // 
            this.cmbActionType.AllowMultSelect = false;
            this.cmbActionType.DataSource = null;
            this.cmbActionType.DisplayMember = "CName";
            this.cmbActionType.EditValue = "-- Action Type --";
            this.cmbActionType.Location = new System.Drawing.Point(5, 5);
            this.cmbActionType.Name = "cmbActionType";
            this.cmbActionType.ParentMember = "ParentID";
            this.cmbActionType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbActionType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbActionType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.cmbActionType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbActionType.Properties.PopupSizeable = false;
            this.cmbActionType.Properties.ShowPopupCloseButton = false;
            this.cmbActionType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbActionType.RootValue = 0;
            this.cmbActionType.SelectedValue = null;
            this.cmbActionType.Separator = ",";
            this.cmbActionType.Size = new System.Drawing.Size(125, 19);
            this.cmbActionType.TabIndex = 43;
            this.cmbActionType.ValueMember = "ID";            
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.pnlTop);
            this.panelControl1.Controls.Add(this.pblToolBar);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(760, 283);
            this.panelControl1.TabIndex = 31;
            // 
            // frmUploadSIAttachment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 595);
            this.Controls.Add(this.pnlCustomer);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Controls.Add(this.panelControl1);
            this.Name = "frmUploadSIAttachment";
            this.Text = "Upload Attachment";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmUploadSIAttachment_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkOverride.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxEmailAsAttachment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlAttachment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDocumentType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCustomer)).EndInit();
            this.pnlCustomer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCustomerList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCustomerToolBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pblToolBar)).EndInit();
            this.pblToolBar.ResumeLayout(false);
            this.pblToolBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbActionType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlTop;
        private DevExpress.XtraEditors.PanelControl pnlAttachment;
        private ICP.Common.UI.Controls.LWComboBoxTree cmbDocumentType;
        private DevExpress.XtraEditors.CheckEdit cbxEmailAsAttachment;
        private DevExpress.XtraEditors.PanelControl pnlCustomer;
        private DevExpress.XtraEditors.PanelControl pnlCustomerToolBar;
        private DevExpress.XtraEditors.PanelControl pnlCustomerList;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarEditItem barDocumentType1;
        private DevExpress.XtraEditors.CheckEdit chkOverride;
        private DevExpress.XtraEditors.PanelControl pblToolBar;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private ICP.Common.UI.Controls.LWComboBoxTree cmbActionType;
        private DevExpress.XtraEditors.LabelControl lblSaveTip;
        private DevExpress.XtraEditors.PanelControl panelControl1;



    }
}
