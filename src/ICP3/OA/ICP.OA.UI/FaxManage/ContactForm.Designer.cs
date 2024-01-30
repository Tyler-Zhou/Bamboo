namespace ICP.OA.UI.EmailManage
{
    partial class ContactForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContactForm));
            this.btnFind = new DevExpress.XtraEditors.SimpleButton();
            this.txtFind = new DevExpress.XtraEditors.TextEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCC = new DevExpress.XtraEditors.SimpleButton();
            this.btnTo = new DevExpress.XtraEditors.SimpleButton();
            this.bsMailAccount = new System.Windows.Forms.BindingSource(this.components);
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvMailList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEMail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbTo = new DevExpress.XtraEditors.ListBoxControl();
            this.bsTo = new System.Windows.Forms.BindingSource(this.components);
            this.lbCC = new DevExpress.XtraEditors.ListBoxControl();
            this.bsCC = new System.Windows.Forms.BindingSource(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtFind.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMailAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMailList)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbCC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(206, 7);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 23);
            this.btnFind.TabIndex = 2;
            this.btnFind.Text = "&Find";
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtFind
            // 
            this.txtFind.Location = new System.Drawing.Point(12, 9);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(188, 21);
            this.txtFind.TabIndex = 4;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(393, 16);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(506, 16);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(605, 10);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // btnCC
            // 
            this.btnCC.Location = new System.Drawing.Point(312, 197);
            this.btnCC.Name = "btnCC";
            this.btnCC.Size = new System.Drawing.Size(75, 23);
            this.btnCC.TabIndex = 8;
            this.btnCC.Text = "CC->";
            this.btnCC.Click += new System.EventHandler(this.btnCC_Click);
            // 
            // btnTo
            // 
            this.btnTo.Location = new System.Drawing.Point(312, 52);
            this.btnTo.Name = "btnTo";
            this.btnTo.Size = new System.Drawing.Size(75, 23);
            this.btnTo.TabIndex = 7;
            this.btnTo.Text = "TO->";
            this.btnTo.Click += new System.EventHandler(this.btnTo_Click);
            // 
            // bsMailAccount
            // 
            this.bsMailAccount.DataSource = typeof(ICP.Sys.ServiceInterface.DataObjects.MailAccount);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsMailAccount;
            this.gcMain.Location = new System.Drawing.Point(12, 36);
            this.gcMain.MainView = this.gvMailList;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(284, 281);
            this.gcMain.TabIndex = 9;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMailList});
            // 
            // gvMailList
            // 
            this.gvMailList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUserName,
            this.colEMail,
            this.colDescription});
            this.gvMailList.GridControl = this.gcMain;
            this.gvMailList.Name = "gvMailList";
            this.gvMailList.OptionsDetail.EnableMasterViewMode = false;
            this.gvMailList.OptionsSelection.MultiSelect = true;
            this.gvMailList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMailList.OptionsView.ColumnAutoWidth = false;
            this.gvMailList.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMailList.OptionsView.ShowGroupPanel = false;
            // 
            // colUserName
            // 
            this.colUserName.Caption = "Name";
            this.colUserName.FieldName = "UserName";
            this.colUserName.Name = "colUserName";
            this.colUserName.OptionsColumn.AllowEdit = false;
            this.colUserName.Visible = true;
            this.colUserName.VisibleIndex = 0;
            this.colUserName.Width = 120;
            // 
            // colEMail
            // 
            this.colEMail.Caption = "EMail";
            this.colEMail.FieldName = "EMail";
            this.colEMail.Name = "colEMail";
            this.colEMail.OptionsColumn.AllowEdit = false;
            this.colEMail.Visible = true;
            this.colEMail.VisibleIndex = 1;
            this.colEMail.Width = 150;
            // 
            // colDescription
            // 
            this.colDescription.Caption = "Description";
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.OptionsColumn.AllowEdit = false;
            this.colDescription.OptionsColumn.ReadOnly = true;
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 2;
            this.colDescription.Width = 120;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 323);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(605, 45);
            this.panel1.TabIndex = 10;
            // 
            // lbTo
            // 
            this.lbTo.DataSource = this.bsTo;
            this.lbTo.DisplayMember = "UserName";
            this.lbTo.Location = new System.Drawing.Point(393, 36);
            this.lbTo.Name = "lbTo";
            this.lbTo.Size = new System.Drawing.Size(200, 139);
            this.lbTo.TabIndex = 11;
            this.lbTo.ValueMember = "ID";
            this.lbTo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbTo_MouseClick);
            this.lbTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbTo_KeyDown);
            // 
            // bsTo
            // 
            this.bsTo.DataSource = typeof(ICP.Sys.ServiceInterface.DataObjects.MailAccount);
            // 
            // lbCC
            // 
            this.lbCC.DataSource = this.bsCC;
            this.lbCC.DisplayMember = "UserName";
            this.lbCC.Location = new System.Drawing.Point(393, 180);
            this.lbCC.Name = "lbCC";
            this.lbCC.Size = new System.Drawing.Size(200, 139);
            this.lbCC.TabIndex = 11;
            this.lbCC.ValueMember = "ID";
            this.lbCC.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbTo_MouseClick);
            this.lbCC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbTo_KeyDown);
            // 
            // bsCC
            // 
            this.bsCC.DataSource = typeof(ICP.Sys.ServiceInterface.DataObjects.MailAccount);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barDelete});
            this.barManager1.MaxItemId = 1;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(605, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 368);
            this.barDockControlBottom.Size = new System.Drawing.Size(605, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 368);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(605, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 368);
            // 
            // barDelete
            // 
            this.barDelete.Caption = "&Delete";
            this.barDelete.Glyph = global::ICP.OA.UI.Properties.Resources.Transfer;
            this.barDelete.Id = 0;
            this.barDelete.Name = "barDelete";
            this.barDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barDelete)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // ContactForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 368);
            this.Controls.Add(this.lbCC);
            this.Controls.Add(this.lbTo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtFind);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.btnCC);
            this.Controls.Add(this.btnTo);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ContactForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Contact";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ContactForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.txtFind.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMailAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMailList)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lbTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbCC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnFind;
        private DevExpress.XtraEditors.TextEdit txtFind;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.SimpleButton btnCC;
        private DevExpress.XtraEditors.SimpleButton btnTo;
        private System.Windows.Forms.BindingSource bsMailAccount;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMailList;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.Columns.GridColumn colUserName;
        private DevExpress.XtraGrid.Columns.GridColumn colEMail;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraEditors.ListBoxControl lbTo;
        private DevExpress.XtraEditors.ListBoxControl lbCC;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private System.Windows.Forms.BindingSource bsTo;
        private System.Windows.Forms.BindingSource bsCC;

    }
}