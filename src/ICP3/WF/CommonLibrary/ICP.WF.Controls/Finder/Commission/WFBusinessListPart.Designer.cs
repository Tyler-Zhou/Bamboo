namespace ICP.WF.Controls.Form.Commission
{
    partial class WFBusinessListPart
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
            this.rchkSelected = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new ICP.Framework.ClientComponents.Controls.LWGridView();
            this.colPaidStatue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbPaidStatue = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colSelected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperationNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStateDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompanyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSalesName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRefNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperationDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colECCost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRatio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProfitDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmountDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCommissionPayAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pageControl1 = new ICP.Framework.ClientComponents.Controls.PageControl();
            ((System.ComponentModel.ISupportInitialize)(this.rchkSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaidStatue)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rchkSelected
            // 
            this.rchkSelected.AutoHeight = false;
            this.rchkSelected.Name = "rchkSelected";
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkSelected,
            this.cmbPaidStatue});
            this.gcMain.Size = new System.Drawing.Size(935, 327);
            this.gcMain.TabIndex = 0;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.WF.ServiceInterface.WFBusinessList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsList_PositionChanged);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPaidStatue,
            this.colSelected,
            this.colOperationNO,
            this.colStateDescription,
            this.colCompanyName,
            this.colSalesName,
            this.colCustomerName,
            this.colRefNO,
            this.colOperationDescription,
            this.colDR,
            this.colCR,
            this.colECCost,
            this.colRatio,
            this.colProfitDescription,
            this.colAmountDescription,
            this.colCommissionPayAmount});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.IndicatorWidth = 28;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvMain.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.OptionsView.ShowVertLines = false;
            this.gvMain.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvMain_CustomDrawRowIndicator);
            this.gvMain.CustomerSorting += new ICP.Framework.ClientComponents.Controls.CustomerSortingHandler(this.gvMain_CustomerSorting);
            this.gvMain.DoubleClick += new System.EventHandler(this.gvMain_DoubleClick);
            this.gvMain.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvMain_RowStyle);
            // 
            // colPaidStatue
            // 
            this.colPaidStatue.Caption = "运费是否收回";
            this.colPaidStatue.ColumnEdit = this.cmbPaidStatue;
            this.colPaidStatue.FieldName = "PaidStatus";
            this.colPaidStatue.Name = "colPaidStatue";
            this.colPaidStatue.OptionsColumn.AllowEdit = false;
            this.colPaidStatue.Visible = true;
            this.colPaidStatue.VisibleIndex = 0;
            this.colPaidStatue.Width = 95;
            // 
            // cmbPaidStatue
            // 
            this.cmbPaidStatue.AutoHeight = false;
            this.cmbPaidStatue.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPaidStatue.Name = "cmbPaidStatue";
            // 
            // colSelected
            // 
            this.colSelected.Name = "colSelected";
            // 
            // colOperationNO
            // 
            this.colOperationNO.Caption = "OperationNO";
            this.colOperationNO.FieldName = "OperationNO";
            this.colOperationNO.Name = "colOperationNO";
            this.colOperationNO.OptionsColumn.AllowEdit = false;
            this.colOperationNO.Visible = true;
            this.colOperationNO.VisibleIndex = 1;
            this.colOperationNO.Width = 120;
            // 
            // colStateDescription
            // 
            this.colStateDescription.Caption = "State";
            this.colStateDescription.FieldName = "StateDescription";
            this.colStateDescription.Name = "colStateDescription";
            this.colStateDescription.OptionsColumn.AllowEdit = false;
            this.colStateDescription.Visible = true;
            this.colStateDescription.VisibleIndex = 2;
            this.colStateDescription.Width = 80;
            // 
            // colCompanyName
            // 
            this.colCompanyName.Caption = "Company";
            this.colCompanyName.FieldName = "CompanyName";
            this.colCompanyName.Name = "colCompanyName";
            this.colCompanyName.OptionsColumn.AllowEdit = false;
            this.colCompanyName.Visible = true;
            this.colCompanyName.VisibleIndex = 3;
            this.colCompanyName.Width = 120;
            // 
            // colSalesName
            // 
            this.colSalesName.Caption = "Sales";
            this.colSalesName.FieldName = "SalesName";
            this.colSalesName.Name = "colSalesName";
            this.colSalesName.OptionsColumn.AllowEdit = false;
            this.colSalesName.Visible = true;
            this.colSalesName.VisibleIndex = 4;
            this.colSalesName.Width = 80;
            // 
            // colCustomerName
            // 
            this.colCustomerName.Caption = "Customer";
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.OptionsColumn.AllowEdit = false;
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 5;
            this.colCustomerName.Width = 120;
            // 
            // colRefNO
            // 
            this.colRefNO.Caption = "RefNO";
            this.colRefNO.FieldName = "RefNO";
            this.colRefNO.Name = "colRefNO";
            this.colRefNO.OptionsColumn.AllowEdit = false;
            this.colRefNO.Visible = true;
            this.colRefNO.VisibleIndex = 6;
            this.colRefNO.Width = 80;
            // 
            // colOperationDescription
            // 
            this.colOperationDescription.Caption = "Description";
            this.colOperationDescription.FieldName = "OperationDescription";
            this.colOperationDescription.Name = "colOperationDescription";
            this.colOperationDescription.OptionsColumn.AllowEdit = false;
            this.colOperationDescription.Visible = true;
            this.colOperationDescription.VisibleIndex = 7;
            this.colOperationDescription.Width = 120;
            // 
            // colDR
            // 
            this.colDR.Caption = "AR";
            this.colDR.FieldName = "ARDescription";
            this.colDR.Name = "colDR";
            this.colDR.OptionsColumn.AllowEdit = false;
            this.colDR.Visible = true;
            this.colDR.VisibleIndex = 8;
            this.colDR.Width = 80;
            // 
            // colCR
            // 
            this.colCR.Caption = "AP";
            this.colCR.FieldName = "APDescription";
            this.colCR.Name = "colCR";
            this.colCR.OptionsColumn.AllowEdit = false;
            this.colCR.Visible = true;
            this.colCR.VisibleIndex = 9;
            this.colCR.Width = 80;
            // 
            // colECCost
            // 
            this.colECCost.Caption = "电商费用";
            this.colECCost.FieldName = "ECCost";
            this.colECCost.Name = "colECCost";
            this.colECCost.OptionsColumn.AllowEdit = false;
            this.colECCost.Visible = true;
            this.colECCost.VisibleIndex = 10;
            this.colECCost.Width = 80;
            // 
            // colRatio
            // 
            this.colRatio.Caption = "配比费用";
            this.colRatio.FieldName = "Ratio";
            this.colRatio.Name = "colRatio";
            this.colRatio.OptionsColumn.AllowEdit = false;
            this.colRatio.Visible = true;
            this.colRatio.VisibleIndex = 10;
            this.colRatio.Width = 80;
            // 
            // colProfitDescription
            // 
            this.colProfitDescription.Caption = "Profit";
            this.colProfitDescription.FieldName = "ProfitDescription";
            this.colProfitDescription.Name = "colProfitDescription";
            this.colProfitDescription.OptionsColumn.AllowEdit = false;
            this.colProfitDescription.Visible = true;
            this.colProfitDescription.VisibleIndex = 10;
            this.colProfitDescription.Width = 80;
            // 
            // colAmountDescription
            // 
            this.colAmountDescription.Caption = "Commission Amount";
            this.colAmountDescription.FieldName = "CommissionAmountDescription";
            this.colAmountDescription.Name = "colAmountDescription";
            this.colAmountDescription.OptionsColumn.AllowEdit = false;
            this.colAmountDescription.Visible = true;
            this.colAmountDescription.VisibleIndex = 11;
            this.colAmountDescription.Width = 80;
            // 
            // colCommissionPayAmount
            // 
            this.colCommissionPayAmount.Caption = "Pay Amount";
            this.colCommissionPayAmount.FieldName = "CommissionPayAmount";
            this.colCommissionPayAmount.Name = "colCommissionPayAmount";
            this.colCommissionPayAmount.OptionsColumn.AllowEdit = false;
            this.colCommissionPayAmount.Visible = true;
            this.colCommissionPayAmount.VisibleIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pageControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 327);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(935, 26);
            this.panel1.TabIndex = 1;
            // 
            // pageControl1
            // 
            this.pageControl1.CurrentPage = 0;
            this.pageControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageControl1.Location = new System.Drawing.Point(0, 0);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.Size = new System.Drawing.Size(935, 26);
            this.pageControl1.TabIndex = 0;
            this.pageControl1.TotalPage = 0;
            this.pageControl1.PageChanged += new ICP.Framework.ClientComponents.Controls.PageChangedHandler(this.pageControl1_PageChanged);
            // 
            // WFBusinessListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.panel1);
            this.Name = "WFBusinessListPart";
            this.Size = new System.Drawing.Size(935, 353);
            ((System.ComponentModel.ISupportInitialize)(this.rchkSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaidStatue)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        private ICP.Framework.ClientComponents.Controls.LWGridView gvMain;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraGrid.Columns.GridColumn colSelected;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationNO;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colRefNO;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colDR;
        private DevExpress.XtraGrid.Columns.GridColumn colCR;
        private DevExpress.XtraGrid.Columns.GridColumn colECCost;
        private DevExpress.XtraGrid.Columns.GridColumn colRatio;
        private DevExpress.XtraGrid.Columns.GridColumn colProfitDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colAmountDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colStateDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanyName;
        private DevExpress.XtraGrid.Columns.GridColumn colSalesName;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkSelected;
        private System.Windows.Forms.Panel panel1;
        private ICP.Framework.ClientComponents.Controls.PageControl pageControl1;
        private DevExpress.XtraGrid.Columns.GridColumn colCommissionPayAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colPaidStatue;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbPaidStatue;
    }
}
