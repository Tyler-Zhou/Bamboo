namespace ICP.FRM.UI.BookingReport
{
    partial class BookingReportListPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BookingReportListPart));
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvMain = new ICP.Framework.ClientComponents.Controls.LWGridView();
            this.colCompanyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShipLineName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCarrierName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colClosingDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoyageName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSONO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOLName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPODName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContainerType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContainerCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSalesType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSalesName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFilerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerService = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContractNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProfit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCommodities = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.rSpinEditRate = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.rcmbTransportClause = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.cmbAccountType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.rchk1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.rcmbChangeState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemImageEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageEdit();
            this.cmbError = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.cmbType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.cmbState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rDateEdit1.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rSpinEditRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbTransportClause)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAccountType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchk1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbChangeState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FRM.ServiceInterface.BookingReportData);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(763, 403);
            this.gcMain.TabIndex = 5;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCompanyName,
            this.colShipLineName,
            this.colCarrierName,
            this.colETD,
            this.colClosingDate,
            this.colVoyageName,
            this.colSONO,
            this.colPOLName,
            this.colPODName,
            this.colContainerType,
            this.colContainerCount,
            this.colSalesType,
            this.colCustomerName,
            this.colSalesName,
            this.colFilerName,
            this.colCustomerService,
            this.colRemark,
            this.colContractNo,
            this.colProfit,
            this.colCommodities});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.GroupCount = 1;
            this.gvMain.GroupFormat = "[#image]{1} {2}";
            this.gvMain.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "CompanyName", null, "( Count:{0})"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ContainerCount", null, "(ContainerCount: Sum {0})"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Profit", null, "(Profit: Sum {0})")});
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsMenu.EnableFooterMenu = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.ShowFooter = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colCompanyName, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvMain.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.gvMain_CustomDrawGroupRow);
            // 
            // colCompanyName
            // 
            this.colCompanyName.Caption = "Company";
            this.colCompanyName.FieldName = "CompanyName";
            this.colCompanyName.Name = "colCompanyName";
            this.colCompanyName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colCompanyName.OptionsColumn.AllowMove = false;
            this.colCompanyName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colCompanyName.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.colCompanyName.Width = 77;
            // 
            // colShipLineName
            // 
            this.colShipLineName.Caption = "ShipLine";
            this.colShipLineName.FieldName = "ShipLineName";
            this.colShipLineName.Name = "colShipLineName";
            this.colShipLineName.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.colShipLineName.Visible = true;
            this.colShipLineName.VisibleIndex = 0;
            // 
            // colCarrierName
            // 
            this.colCarrierName.Caption = "Carrier";
            this.colCarrierName.FieldName = "CarrierName";
            this.colCarrierName.Name = "colCarrierName";
            this.colCarrierName.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.colCarrierName.Visible = true;
            this.colCarrierName.VisibleIndex = 1;
            // 
            // colETD
            // 
            this.colETD.Caption = "ETD";
            this.colETD.FieldName = "ETD";
            this.colETD.Name = "colETD";
            this.colETD.Visible = true;
            this.colETD.VisibleIndex = 3;
            this.colETD.Width = 89;
            // 
            // colClosingDate
            // 
            this.colClosingDate.Caption = "Closing Date";
            this.colClosingDate.FieldName = "ClosingDate";
            this.colClosingDate.Name = "colClosingDate";
            this.colClosingDate.Visible = true;
            this.colClosingDate.VisibleIndex = 2;
            this.colClosingDate.Width = 89;
            // 
            // colVoyageName
            // 
            this.colVoyageName.Caption = "Vessel&Voyage";
            this.colVoyageName.FieldName = "VoyageName";
            this.colVoyageName.Name = "colVoyageName";
            this.colVoyageName.Visible = true;
            this.colVoyageName.VisibleIndex = 4;
            this.colVoyageName.Width = 105;
            // 
            // colSONO
            // 
            this.colSONO.Caption = "S/O NO";
            this.colSONO.FieldName = "SONO";
            this.colSONO.Name = "colSONO";
            this.colSONO.Visible = true;
            this.colSONO.VisibleIndex = 5;
            this.colSONO.Width = 83;
            // 
            // colPOLName
            // 
            this.colPOLName.Caption = "POL";
            this.colPOLName.FieldName = "POLName";
            this.colPOLName.Name = "colPOLName";
            this.colPOLName.Visible = true;
            this.colPOLName.VisibleIndex = 6;
            this.colPOLName.Width = 59;
            // 
            // colPODName
            // 
            this.colPODName.Caption = "POD";
            this.colPODName.FieldName = "PODName";
            this.colPODName.Name = "colPODName";
            this.colPODName.Visible = true;
            this.colPODName.VisibleIndex = 7;
            this.colPODName.Width = 79;
            // 
            // colContainerType
            // 
            this.colContainerType.Caption = "Container Type";
            this.colContainerType.CustomizationCaption = "Count";
            this.colContainerType.FieldName = "ContainerType";
            this.colContainerType.Name = "colContainerType";
            this.colContainerType.Visible = true;
            this.colContainerType.VisibleIndex = 8;
            this.colContainerType.Width = 74;
            // 
            // colContainerCount
            // 
            this.colContainerCount.Caption = "Count";
            this.colContainerCount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colContainerCount.FieldName = "ContainerCount";
            this.colContainerCount.Name = "colContainerCount";
            this.colContainerCount.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colContainerCount.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colContainerCount.Visible = true;
            this.colContainerCount.VisibleIndex = 9;
            this.colContainerCount.Width = 60;
            // 
            // colSalesType
            // 
            this.colSalesType.Caption = "Type";
            this.colSalesType.FieldName = "SalesType";
            this.colSalesType.Name = "colSalesType";
            this.colSalesType.Visible = true;
            this.colSalesType.VisibleIndex = 10;
            this.colSalesType.Width = 60;
            // 
            // colCustomerName
            // 
            this.colCustomerName.Caption = "Customer";
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 11;
            this.colCustomerName.Width = 120;
            // 
            // colSalesName
            // 
            this.colSalesName.Caption = "Sales";
            this.colSalesName.FieldName = "SalesName";
            this.colSalesName.Name = "colSalesName";
            this.colSalesName.Visible = true;
            this.colSalesName.VisibleIndex = 12;
            // 
            // colFilerName
            // 
            this.colFilerName.Caption = "Filer";
            this.colFilerName.FieldName = "FilerName";
            this.colFilerName.Name = "colFilerName";
            this.colFilerName.Visible = true;
            this.colFilerName.VisibleIndex = 13;
            // 
            // colCustomerService
            // 
            this.colCustomerService.Caption = "Customer Service";
            this.colCustomerService.FieldName = "CustomerService";
            this.colCustomerService.Name = "colCustomerService";
            this.colCustomerService.Visible = true;
            this.colCustomerService.VisibleIndex = 14;
            // 
            // colRemark
            // 
            this.colRemark.Caption = "Remark";
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 15;
            // 
            // colContractNo
            // 
            this.colContractNo.Caption = "Contract No";
            this.colContractNo.FieldName = "ContractNo";
            this.colContractNo.Name = "colContractNo";
            this.colContractNo.Visible = true;
            this.colContractNo.VisibleIndex = 18;
            // 
            // colProfit
            // 
            this.colProfit.Caption = "Profit";
            this.colProfit.FieldName = "Profit";
            this.colProfit.Name = "colProfit";
            this.colProfit.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colProfit.Visible = true;
            this.colProfit.VisibleIndex = 16;
            // 
            // colCommodities
            // 
            this.colCommodities.Caption = "Commodities";
            this.colCommodities.FieldName = "Commodities";
            this.colCommodities.Name = "colCommodities";
            this.colCommodities.Visible = true;
            this.colCommodities.VisibleIndex = 17;
            // 
            // rDateEdit1
            // 
            this.rDateEdit1.AutoHeight = false;
            this.rDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rDateEdit1.Name = "rDateEdit1";
            this.rDateEdit1.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // rSpinEditRate
            // 
            this.rSpinEditRate.AutoHeight = false;
            this.rSpinEditRate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.rSpinEditRate.Mask.EditMask = "F2";
            this.rSpinEditRate.MaxValue = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.rSpinEditRate.MinValue = new decimal(new int[] {
            1215752192,
            23,
            0,
            -2147483648});
            this.rSpinEditRate.Name = "rSpinEditRate";
            // 
            // rcmbTransportClause
            // 
            this.rcmbTransportClause.AutoHeight = false;
            this.rcmbTransportClause.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbTransportClause.Name = "rcmbTransportClause";
            // 
            // cmbAccountType
            // 
            this.cmbAccountType.AutoHeight = false;
            this.cmbAccountType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbAccountType.Name = "cmbAccountType";
            // 
            // rchk1
            // 
            this.rchk1.AutoHeight = false;
            this.rchk1.Name = "rchk1";
            // 
            // rcmbChangeState
            // 
            this.rcmbChangeState.AutoHeight = false;
            this.rcmbChangeState.Name = "rcmbChangeState";
            // 
            // repositoryItemImageEdit1
            // 
            this.repositoryItemImageEdit1.AutoHeight = false;
            this.repositoryItemImageEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageEdit1.Name = "repositoryItemImageEdit1";
            // 
            // cmbError
            // 
            this.cmbError.AutoHeight = false;
            this.cmbError.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbError.Name = "cmbError";
            // 
            // cmbType
            // 
            this.cmbType.AutoHeight = false;
            this.cmbType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Name = "cmbType";
            // 
            // cmbState
            // 
            this.cmbState.AutoHeight = false;
            this.cmbState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbState.Name = "cmbState";
            this.cmbState.SmallImages = this.imageList1;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "EFFECTIVE.png");
            this.imageList1.Images.SetKeyName(1, "WILL BE EFFECTIVE.png");
            this.imageList1.Images.SetKeyName(2, "EXPIRED.png");
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // BookingReportListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.IsMultiLanguage = false;
            this.Name = "BookingReportListPart";
            this.Size = new System.Drawing.Size(763, 403);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rDateEdit1.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rSpinEditRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbTransportClause)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAccountType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchk1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbChangeState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        private ICP.Framework.ClientComponents.Controls.LWGridView gvMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbError;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbChangeState;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbAccountType;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchk1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbTransportClause;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rSpinEditRate;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit rDateEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageEdit repositoryItemImageEdit1;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbState;
        private System.Windows.Forms.ToolTip toolTip1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colClosingDate;
        private DevExpress.XtraGrid.Columns.GridColumn colVoyageName;
        private DevExpress.XtraGrid.Columns.GridColumn colSONO;
        private DevExpress.XtraGrid.Columns.GridColumn colPOLName;
        private DevExpress.XtraGrid.Columns.GridColumn colPODName;
        private DevExpress.XtraGrid.Columns.GridColumn colContainerType;
        private DevExpress.XtraGrid.Columns.GridColumn colContainerCount;
        private DevExpress.XtraGrid.Columns.GridColumn colSalesType;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colSalesName;
        private DevExpress.XtraGrid.Columns.GridColumn colFilerName;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerService;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;
        private DevExpress.XtraGrid.Columns.GridColumn colProfit;
        private DevExpress.XtraGrid.Columns.GridColumn colCommodities;
        private DevExpress.XtraGrid.Columns.GridColumn colContractNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanyName;
        private DevExpress.XtraGrid.Columns.GridColumn colShipLineName;
        private DevExpress.XtraGrid.Columns.GridColumn colCarrierName;
        private DevExpress.XtraGrid.Columns.GridColumn colETD;
    }
}
