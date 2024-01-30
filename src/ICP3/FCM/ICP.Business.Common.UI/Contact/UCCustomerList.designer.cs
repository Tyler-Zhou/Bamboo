using ICP.Framework.CommonLibrary.Client;
namespace ICP.Business.Common.UI.Contact
{
    partial class UCCustomerList
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
            this.bsCustomerCarrier = new System.Windows.Forms.BindingSource();
            this.groupCustomer = new DevExpress.XtraEditors.GroupControl();
            this.gcCustomer = new DevExpress.XtraGrid.GridControl();
            this.gvCustomer = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEMail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckedComboBoxEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.ColType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEditType = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colCC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colAR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRelease = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemPopupContainerEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barSendEmail = new DevExpress.XtraBars.BarButtonItem();
            this.barNew = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu();
            ((System.ComponentModel.ISupportInitialize)(this.bsCustomerCarrier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupCustomer)).BeginInit();
            this.groupCustomer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPopupContainerEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupCustomer
            // 
            this.groupCustomer.Controls.Add(this.gcCustomer);
            this.groupCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupCustomer.Location = new System.Drawing.Point(0, 0);
            this.groupCustomer.Name = "groupCustomer";
            this.groupCustomer.ShowCaption = false;
            this.groupCustomer.Size = new System.Drawing.Size(614, 315);
            this.groupCustomer.TabIndex = 1;
            this.groupCustomer.Text = "Customer";
            // 
            // gcCustomer
            // 
            this.gcCustomer.DataSource = this.bsCustomerCarrier;
            this.gcCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcCustomer.Location = new System.Drawing.Point(2, 2);
            this.gcCustomer.MainView = this.gvCustomer;
            this.gcCustomer.Name = "gcCustomer";
            this.gcCustomer.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemPopupContainerEdit1,
            this.repositoryItemCheckedComboBoxEdit,
            this.repositoryItemCheckEditType});
            this.gcCustomer.Size = new System.Drawing.Size(610, 311);
            this.gcCustomer.TabIndex = 0;
            this.gcCustomer.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCustomer});
            this.gcCustomer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gcCustomer_MouseClick);
            // 
            // gvCustomer
            // 
            this.gvCustomer.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCustomerName,
            this.colName,
            this.colEMail,
            this.colTel,
            this.colFax,
            this.colStage,
            this.ColType,
            this.colCC,
            this.colAR,
            this.colRelease,
            this.colCustomerID});
            this.gvCustomer.GridControl = this.gcCustomer;
            this.gvCustomer.GroupCount = 1;
            this.gvCustomer.Name = "gvCustomer";
            this.gvCustomer.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvCustomer.OptionsView.ColumnAutoWidth = false;
            this.gvCustomer.OptionsView.ShowGroupedColumns = true;
            this.gvCustomer.OptionsView.ShowGroupPanel = false;
            this.gvCustomer.OptionsView.ShowIndicator = false;
            this.gvCustomer.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colCustomerName, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvCustomer.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvCustomer_RowCellClick);
            // 
            // colCustomerName
            // 
            this.colCustomerName.Caption = "Customer Name";
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 0;
            this.colCustomerName.Width = 142;
            // 
            // colName
            // 
            this.colName.Caption = "Name";
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 1;
            this.colName.Width = 91;
            // 
            // colEMail
            // 
            this.colEMail.Caption = "EMail";
            this.colEMail.FieldName = "Mail";
            this.colEMail.Name = "colEMail";
            this.colEMail.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colEMail.OptionsFilter.AllowFilter = false;
            this.colEMail.Visible = true;
            this.colEMail.VisibleIndex = 2;
            this.colEMail.Width = 132;
            // 
            // colTel
            // 
            this.colTel.Caption = "Tel";
            this.colTel.FieldName = "Tel";
            this.colTel.Name = "colTel";
            this.colTel.Visible = true;
            this.colTel.VisibleIndex = 3;
            // 
            // colFax
            // 
            this.colFax.Caption = "Fax";
            this.colFax.FieldName = "Fax";
            this.colFax.Name = "colFax";
            this.colFax.Visible = true;
            this.colFax.VisibleIndex = 4;
            // 
            // colStage
            // 
            this.colStage.Caption = "Stage";
            this.colStage.ColumnEdit = this.repositoryItemCheckedComboBoxEdit;
            this.colStage.FieldName = "Stage";
            this.colStage.Name = "colStage";
            this.colStage.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colStage.OptionsFilter.AllowFilter = false;
            this.colStage.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.colStage.Visible = true;
            this.colStage.VisibleIndex = 5;
            this.colStage.Width = 67;
            // 
            // repositoryItemCheckedComboBoxEdit
            // 
            this.repositoryItemCheckedComboBoxEdit.AutoHeight = false;
            this.repositoryItemCheckedComboBoxEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCheckedComboBoxEdit.DisplayMember = "StageName";
            this.repositoryItemCheckedComboBoxEdit.Name = "repositoryItemCheckedComboBoxEdit";
            this.repositoryItemCheckedComboBoxEdit.ValueMember = "Stage";
            // 
            // ColType
            // 
            this.ColType.Caption = "T";
            this.ColType.ColumnEdit = this.repositoryItemCheckEditType;
            this.ColType.FieldName = "Type";
            this.ColType.Name = "ColType";
            this.ColType.Visible = true;
            this.ColType.VisibleIndex = 6;
            this.ColType.Width = 30;
            // 
            // repositoryItemCheckEditType
            // 
            this.repositoryItemCheckEditType.AutoHeight = false;
            this.repositoryItemCheckEditType.Name = "repositoryItemCheckEditType";
            // 
            // colCC
            // 
            this.colCC.Caption = "CC";
            this.colCC.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colCC.FieldName = "IsCC";
            this.colCC.Name = "colCC";
            this.colCC.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colCC.OptionsFilter.AllowFilter = false;
            this.colCC.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            this.colCC.Visible = true;
            this.colCC.VisibleIndex = 7;
            this.colCC.Width = 28;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // colAR
            // 
            this.colAR.Caption = "AR";
            this.colAR.FieldName = "AR";
            this.colAR.Name = "colAR";
            this.colAR.Visible = true;
            this.colAR.VisibleIndex = 8;
            this.colAR.Width = 30;
            // 
            // colRelease
            // 
            this.colRelease.Caption = "Release";
            this.colRelease.FieldName = "Release";
            this.colRelease.Name = "colRelease";
            this.colRelease.Visible = true;
            this.colRelease.VisibleIndex = 9;
            this.colRelease.Width = 50;
            // 
            // colCustomerID
            // 
            this.colCustomerID.Caption = "CustomerID";
            this.colCustomerID.FieldName = "CustomerID";
            this.colCustomerID.Name = "colCustomerID";
            // 
            // repositoryItemPopupContainerEdit1
            // 
            this.repositoryItemPopupContainerEdit1.AutoHeight = false;
            this.repositoryItemPopupContainerEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemPopupContainerEdit1.Name = "repositoryItemPopupContainerEdit1";
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSendEmail,
            this.barNew,
            this.barDelete});
            this.barManager1.MaxItemId = 3;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(614, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 315);
            this.barDockControlBottom.Size = new System.Drawing.Size(614, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 315);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(614, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 315);
            // 
            // barSendEmail
            // 
            this.barSendEmail.Caption = "Send Email";
            this.barSendEmail.Id = 0;
            this.barSendEmail.Name = "barSendEmail";
            this.barSendEmail.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSendEmail_ItemClick);
            // 
            // barNew
            // 
            this.barNew.Caption = "New";
            this.barNew.Id = 1;
            this.barNew.Name = "barNew";
            this.barNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barNew_ItemClick);
            // 
            // barDelete
            // 
            this.barDelete.Caption = "Delete";
            this.barDelete.Id = 2;
            this.barDelete.Name = "barDelete";
            this.barDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSendEmail),
            new DevExpress.XtraBars.LinkPersistInfo(this.barNew),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDelete)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // UCCustomerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupCustomer);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.DoubleBuffered = true;
            this.Name = "UCCustomerList";
            this.Size = new System.Drawing.Size(614, 315);
            ((System.ComponentModel.ISupportInitialize)(this.bsCustomerCarrier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupCustomer)).EndInit();
            this.groupCustomer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPopupContainerEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);

        }

    
    


    

      

    

    

      
   
        #endregion

        private System.Windows.Forms.BindingSource bsCustomerCarrier;
        private DevExpress.XtraEditors.GroupControl groupCustomer;
   
        private DevExpress.XtraGrid.GridControl gcCustomer;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCustomer;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colStage;
        private DevExpress.XtraGrid.Columns.GridColumn colEMail;
        private DevExpress.XtraGrid.Columns.GridColumn colTel;
        private DevExpress.XtraGrid.Columns.GridColumn colFax;
        private DevExpress.XtraGrid.Columns.GridColumn colCC;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerID;
        private DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit repositoryItemPopupContainerEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit repositoryItemCheckedComboBoxEdit;
        private DevExpress.XtraGrid.Columns.GridColumn ColType;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditType;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barSendEmail;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarButtonItem barNew;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private DevExpress.XtraGrid.Columns.GridColumn colAR;
        private DevExpress.XtraGrid.Columns.GridColumn colRelease;
 
    }
}
