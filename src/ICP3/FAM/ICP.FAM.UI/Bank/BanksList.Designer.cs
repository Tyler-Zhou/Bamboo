namespace ICP.FAM.UI
{
    partial class BanksList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BanksList));
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.bsBanksList = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new ICP.Framework.ClientComponents.Controls.LWGridView();
            this.colSortName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContact = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPhone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colValid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.UCPageControl = new ICP.Framework.ClientComponents.Controls.PageControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsBanksList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsBanksList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gcMain.Size = new System.Drawing.Size(723, 509);
            this.gcMain.TabIndex = 2;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            this.gcMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcMain_KeyDown);
            // 
            // bsBanksList
            // 
            this.bsBanksList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.BankList);
            this.bsBanksList.CurrentChanged += new System.EventHandler(this.bsBanksList_CurrentChanged);
            this.bsBanksList.PositionChanged += new System.EventHandler(this.bsBanksList_PositionChanged);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSortName,
            this.colCName,
            this.colEName,
            this.colContact,
            this.colPhone,
            this.colFax,
            this.colCreateName,
            this.colCreateDate,
            this.colValid});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsDetail.AllowZoomDetail = false;
            this.gvMain.OptionsDetail.EnableMasterViewMode = false;
            this.gvMain.OptionsDetail.ShowDetailTabs = false;
            this.gvMain.OptionsDetail.SmartDetailExpand = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.RowAutoHeight = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colEName, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvMain.CustomerSorting += new ICP.Framework.ClientComponents.Controls.CustomerSortingHandler(this.gvMain_CustomerSorting);
            this.gvMain.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(this.gvMain_BeforeLeaveRow);
            this.gvMain.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvMain_RowCellClick);
            this.gvMain.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvMain_RowStyle);
            // 
            // colSortName
            // 
            this.colSortName.Caption = "简称";
            this.colSortName.FieldName = "CShortName";
            this.colSortName.Name = "colSortName";
            this.colSortName.Visible = true;
            this.colSortName.VisibleIndex = 0;
            this.colSortName.Width = 86;
            // 
            // colCName
            // 
            this.colCName.Caption = "中文名称";
            this.colCName.FieldName = "CName";
            this.colCName.Name = "colCName";
            this.colCName.Visible = true;
            this.colCName.VisibleIndex = 1;
            this.colCName.Width = 120;
            // 
            // colEName
            // 
            this.colEName.Caption = "英文名称";
            this.colEName.FieldName = "EName";
            this.colEName.Name = "colEName";
            this.colEName.Visible = true;
            this.colEName.VisibleIndex = 2;
            this.colEName.Width = 120;
            // 
            // colContact
            // 
            this.colContact.Caption = "联系人";
            this.colContact.FieldName = "Contact";
            this.colContact.Name = "colContact";
            this.colContact.Visible = true;
            this.colContact.VisibleIndex = 3;
            this.colContact.Width = 54;
            // 
            // colPhone
            // 
            this.colPhone.Caption = "电话";
            this.colPhone.FieldName = "Tel1";
            this.colPhone.Name = "colPhone";
            this.colPhone.Visible = true;
            this.colPhone.VisibleIndex = 4;
            this.colPhone.Width = 58;
            // 
            // colFax
            // 
            this.colFax.Caption = "传真";
            this.colFax.FieldName = "Fax";
            this.colFax.Name = "colFax";
            this.colFax.Visible = true;
            this.colFax.VisibleIndex = 5;
            this.colFax.Width = 61;
            // 
            // colCreateName
            // 
            this.colCreateName.Caption = "创建人";
            this.colCreateName.FieldName = "CreateByName";
            this.colCreateName.Name = "colCreateName";
            this.colCreateName.Visible = true;
            this.colCreateName.VisibleIndex = 6;
            this.colCreateName.Width = 60;
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "创建时间";
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 7;
            this.colCreateDate.Width = 80;
            // 
            // colValid
            // 
            this.colValid.Caption = "有效";
            this.colValid.FieldName = "IsValid";
            this.colValid.Name = "colValid";
            this.colValid.Visible = true;
            this.colValid.VisibleIndex = 8;
            this.colValid.Width = 63;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // UCPageControl
            // 
            this.UCPageControl.CurrentPage = 0;
            this.UCPageControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.UCPageControl.Location = new System.Drawing.Point(0, 509);
            this.UCPageControl.Name = "UCPageControl";
            this.UCPageControl.Size = new System.Drawing.Size(723, 26);
            this.UCPageControl.TabIndex = 3;
            this.UCPageControl.TotalPage = 0;
            this.UCPageControl.PageChanged += new ICP.Framework.ClientComponents.Controls.PageChangedHandler(this.UCPageControl_PageChanged);
            // 
            // BanksList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.UCPageControl);
            this.Name = "BanksList";
            this.Size = new System.Drawing.Size(723, 535);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsBanksList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcMain;
        private ICP.Framework.ClientComponents.Controls.LWGridView gvMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private System.Windows.Forms.BindingSource bsBanksList;
        private DevExpress.XtraGrid.Columns.GridColumn colSortName;
        private DevExpress.XtraGrid.Columns.GridColumn colCName;
        private DevExpress.XtraGrid.Columns.GridColumn colEName;
        private DevExpress.XtraGrid.Columns.GridColumn colContact;
        private DevExpress.XtraGrid.Columns.GridColumn colPhone;
        private DevExpress.XtraGrid.Columns.GridColumn colFax;
        private DevExpress.XtraGrid.Columns.GridColumn colValid;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private ICP.Framework.ClientComponents.Controls.PageControl UCPageControl;

    }
}
