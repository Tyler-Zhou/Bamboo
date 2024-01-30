namespace ICP.Common.UI.CC
{
    partial class CCBusinessPart
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
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.gvMain = new ICP.Framework.ClientComponents.Controls.LWGridView();
            this.colSelected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkSelected = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colOperationNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.colStateDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompanyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSalesName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRefNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperationDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProfitDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            this.SuspendLayout();
            //
            //bsList
            //
            this.bsList.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.CCBusinessList);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemHyperLinkEdit1,
            this.rchkSelected});
            this.gcMain.Size = new System.Drawing.Size(864, 409);
            this.gcMain.TabIndex = 1;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
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
            this.colProfitDescription});
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
            // 
            // colSelected
            // 
            this.colSelected.Caption = "Selected";
            this.colSelected.ColumnEdit = this.rchkSelected;
            this.colSelected.FieldName = "Selected";
            this.colSelected.Name = "colSelected";
            this.colSelected.Visible = true;
            this.colSelected.VisibleIndex = 0;
            this.colSelected.Width = 80;
            // 
            // rchkSelected
            // 
            this.rchkSelected.AutoHeight = false;
            this.rchkSelected.Name = "rchkSelected";
            // 
            // colOperationNO
            // 
            this.colOperationNO.Caption = "OperationNO";
            this.colOperationNO.ColumnEdit = this.repositoryItemHyperLinkEdit1;
            this.colOperationNO.FieldName = "OperationNO";
            this.colOperationNO.Name = "colOperationNO";
            this.colOperationNO.OptionsColumn.AllowEdit = false;
            this.colOperationNO.Visible = true;
            this.colOperationNO.VisibleIndex = 1;
            this.colOperationNO.Width = 120;
            // 
            // repositoryItemHyperLinkEdit1
            // 
            this.repositoryItemHyperLinkEdit1.AutoHeight = false;
            this.repositoryItemHyperLinkEdit1.Name = "repositoryItemHyperLinkEdit1";
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
            this.colDR.Caption = "DR";
            this.colDR.FieldName = "DRDescription";
            this.colDR.Name = "colDR";
            this.colDR.OptionsColumn.AllowEdit = false;
            this.colDR.Visible = true;
            this.colDR.VisibleIndex = 8;
            this.colDR.Width = 80;
            // 
            // colCR
            // 
            this.colCR.Caption = "CR";
            this.colCR.FieldName = "CRDescription";
            this.colCR.Name = "colCR";
            this.colCR.OptionsColumn.AllowEdit = false;
            this.colCR.Visible = true;
            this.colCR.VisibleIndex = 9;
            this.colCR.Width = 80;
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
            // CCBusinessPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Name = "CCBusinessPart";
            this.Size = new System.Drawing.Size(864, 409);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraGrid.GridControl gcMain;
        private ICP.Framework.ClientComponents.Controls.LWGridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colSelected;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkSelected;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationNO;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colStateDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanyName;
        private DevExpress.XtraGrid.Columns.GridColumn colSalesName;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colRefNO;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colDR;
        private DevExpress.XtraGrid.Columns.GridColumn colCR;
        private DevExpress.XtraGrid.Columns.GridColumn colProfitDescription;
    }
}
