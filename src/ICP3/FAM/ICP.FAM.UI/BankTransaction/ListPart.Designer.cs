namespace ICP.FAM.UI.BankTransaction
{
    partial class ListPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListPart));
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperationDateTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDebitAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreditAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRelativeAccountName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRelativeAccountNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRelativeBankName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFlowWaterNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchk = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchk)).BeginInit();
            this.SuspendLayout();
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchk});
            this.gcMain.Size = new System.Drawing.Size(802, 409);
            this.gcMain.TabIndex = 35;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.BankTransactionInfo);
            this.bsList.PositionChanged += new System.EventHandler(this.bsList_PositionChanged);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colAccountNO,
            this.colOperationDateTime,
            this.colDebitAmount,
            this.colCreditAmount,
            this.colCurrencyName,
            this.colRemark,
            this.colRelativeAccountName,
            this.colRelativeAccountNo,
            this.colRelativeBankName,
            this.colFlowWaterNO});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowFooter = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvMain_RowStyle);
            this.gvMain.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(this.gvMain_BeforeLeaveRow);
            this.gvMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvMain_KeyDown);
            // 
            // colID
            // 
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colAccountNO
            // 
            this.colAccountNO.FieldName = "AccountNO";
            this.colAccountNO.Name = "colAccountNO";
            this.colAccountNO.Visible = true;
            this.colAccountNO.VisibleIndex = 0;
            // 
            // colOperationDateTime
            // 
            this.colOperationDateTime.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.colOperationDateTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colOperationDateTime.FieldName = "OperationDateTime";
            this.colOperationDateTime.Name = "colOperationDateTime";
            this.colOperationDateTime.Visible = true;
            this.colOperationDateTime.VisibleIndex = 1;
            this.colOperationDateTime.Width = 120;
            // 
            // colDebitAmount
            // 
            this.colDebitAmount.FieldName = "DebitAmount";
            this.colDebitAmount.Name = "colDebitAmount";
            this.colDebitAmount.SummaryItem.DisplayFormat = "{0:n2}";
            this.colDebitAmount.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colDebitAmount.Visible = true;
            this.colDebitAmount.VisibleIndex = 2;
            // 
            // colCreditAmount
            // 
            this.colCreditAmount.FieldName = "CreditAmount";
            this.colCreditAmount.Name = "colCreditAmount";
            this.colCreditAmount.SummaryItem.DisplayFormat = "{0:n2}";
            this.colCreditAmount.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colCreditAmount.Visible = true;
            this.colCreditAmount.VisibleIndex = 3;
            // 
            // colCurrencyName
            // 
            this.colCurrencyName.FieldName = "CurrencyName";
            this.colCurrencyName.Name = "colCurrencyName";
            this.colCurrencyName.Visible = true;
            this.colCurrencyName.VisibleIndex = 4;
            // 
            // colRemark
            // 
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 5;
            // 
            // colRelativeAccountName
            // 
            this.colRelativeAccountName.FieldName = "RelativeAccountName";
            this.colRelativeAccountName.Name = "colRelativeAccountName";
            this.colRelativeAccountName.Visible = true;
            this.colRelativeAccountName.VisibleIndex = 6;
            // 
            // colRelativeAccountNo
            // 
            this.colRelativeAccountNo.FieldName = "RelativeAccountNo";
            this.colRelativeAccountNo.Name = "colRelativeAccountNo";
            this.colRelativeAccountNo.Visible = true;
            this.colRelativeAccountNo.VisibleIndex = 7;
            // 
            // colRelativeBankName
            // 
            this.colRelativeBankName.FieldName = "RelativeBankName";
            this.colRelativeBankName.Name = "colRelativeBankName";
            this.colRelativeBankName.Visible = true;
            this.colRelativeBankName.VisibleIndex = 8;
            // 
            // colFlowWaterNO
            // 
            this.colFlowWaterNO.FieldName = "FlowWaterNO";
            this.colFlowWaterNO.Name = "colFlowWaterNO";
            this.colFlowWaterNO.Visible = true;
            this.colFlowWaterNO.VisibleIndex = 9;
            // 
            // rchk
            // 
            this.rchk.AutoHeight = false;
            this.rchk.Name = "rchk";
            // 
            // ListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.gcMain);
            this.Name = "ListPart";
            this.Size = new System.Drawing.Size(802, 409);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchk)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchk;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colFlowWaterNO;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountNO;
        private DevExpress.XtraGrid.Columns.GridColumn colRelativeAccountNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyName;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationDateTime;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;
        private DevExpress.XtraGrid.Columns.GridColumn colRelativeAccountName;
        private DevExpress.XtraGrid.Columns.GridColumn colRelativeBankName;
        private DevExpress.XtraGrid.Columns.GridColumn colDebitAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colCreditAmount;
    }
}
