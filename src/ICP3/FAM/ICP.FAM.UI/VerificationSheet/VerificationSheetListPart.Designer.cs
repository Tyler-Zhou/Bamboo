namespace ICP.FAM.UI.VerificationSheet
{
    partial class VerificationSheetListPart
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
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSheetNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExpressNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReceiptDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReturnDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperationNo = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.bsList.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.VerificationSheet);
            this.bsList.PositionChanged += new System.EventHandler(this.bsList_PositionChanged);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSheetNo,
            this.ColCustomerName,
            this.colExpressNO,
            this.colReceiptDate,
            this.colReturnDate,
            this.colOperationNo});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvMain_KeyDown);
            this.gvMain.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(this.gvMain_BeforeLeaveRow);
            this.gvMain.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvMain_RowStyle);
            // 
            // colSheetNo
            // 
            this.colSheetNo.Caption = "核销单号";
            this.colSheetNo.FieldName = "SheetNo";
            this.colSheetNo.Name = "colSheetNo";
            this.colSheetNo.OptionsColumn.ReadOnly = true;
            this.colSheetNo.Visible = true;
            this.colSheetNo.VisibleIndex = 0;
            this.colSheetNo.Width = 150;
            // 
            // ColCustomerName
            // 
            this.ColCustomerName.Caption = "经营单位";
            this.ColCustomerName.FieldName = "CustomerName";
            this.ColCustomerName.Name = "ColCustomerName";
            this.ColCustomerName.OptionsColumn.ReadOnly = true;
            this.ColCustomerName.Visible = true;
            this.ColCustomerName.VisibleIndex = 1;
            this.ColCustomerName.Width = 220;
            // 
            // colExpressNO
            // 
            this.colExpressNO.Caption = "快递单号";
            this.colExpressNO.FieldName = "ExpressNO";
            this.colExpressNO.Name = "colExpressNO";
            this.colExpressNO.OptionsColumn.ReadOnly = true;
            this.colExpressNO.Visible = true;
            this.colExpressNO.VisibleIndex = 2;
            this.colExpressNO.Width = 80;
            // 
            // colReceiptDate
            // 
            this.colReceiptDate.Caption = "收到日期";
            this.colReceiptDate.FieldName = "ReceiptDate";
            this.colReceiptDate.Name = "colReceiptDate";
            this.colReceiptDate.OptionsColumn.ReadOnly = true;
            this.colReceiptDate.Visible = true;
            this.colReceiptDate.VisibleIndex = 3;
            this.colReceiptDate.Width = 80;
            // 
            // colReturnDate
            // 
            this.colReturnDate.Caption = "寄还日期";
            this.colReturnDate.FieldName = "ReturnDate";
            this.colReturnDate.Name = "colReturnDate";
            this.colReturnDate.Visible = true;
            this.colReturnDate.VisibleIndex = 4;
            // 
            // colOperationNo
            // 
            this.colOperationNo.Caption = "业务号";
            this.colOperationNo.FieldName = "OperationNo";
            this.colOperationNo.Name = "colOperationNo";
            this.colOperationNo.Visible = true;
            this.colOperationNo.VisibleIndex = 5;
            this.colOperationNo.Width = 120;
            // 
            // rchk
            // 
            this.rchk.AutoHeight = false;
            this.rchk.Name = "rchk";
            // 
            // VerificationSheetListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Name = "VerificationSheetListPart";
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
        private DevExpress.XtraGrid.Columns.GridColumn colSheetNo;
        private DevExpress.XtraGrid.Columns.GridColumn ColCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colExpressNO;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchk;
        private DevExpress.XtraGrid.Columns.GridColumn colReceiptDate;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationNo;
        private DevExpress.XtraGrid.Columns.GridColumn colReturnDate;
    }
}
