namespace ICP.FAM.UI
{
    partial class InvoiceListPart
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
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new ICP.Framework.ClientComponents.Controls.LWGridView();
            this.colSystemNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBLNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExpress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsValid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pageControl1 = new ICP.Framework.ClientComponents.Controls.PageControl();
            this.bsInvoiceFeeDate = new System.Windows.Forms.BindingSource(this.components);
            this.colInvoiceTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsInvoiceFeeDate)).BeginInit();
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
            this.repositoryItemCheckEdit1});
            this.gcMain.Size = new System.Drawing.Size(913, 509);
            this.gcMain.TabIndex = 2;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.InvoiceList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsList_PositionChanged);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSystemNo,
            this.colInvoiceNo,
            this.colInvoiceDate,
            this.colCustomerName,
            this.colInvoiceTitle,
            this.colAmount,
            this.colBillNo,
            this.colBLNo,
            this.colETD,
            this.colExpress,
            this.colRemark,
            this.colIsValid,
            this.colCreateByName});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.RowAutoHeight = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gvMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvMain_KeyDown);
            this.gvMain.CustomerSorting += new ICP.Framework.ClientComponents.Controls.CustomerSortingHandler(this.gvMain_CustomerSorting);
            this.gvMain.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(this.gvMain_BeforeLeaveRow);
            this.gvMain.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvMain_RowCellClick);
            this.gvMain.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvMain_RowStyle);
            // 
            // colSystemNo
            // 
            this.colSystemNo.Caption = "System No";
            this.colSystemNo.FieldName = "No";
            this.colSystemNo.Name = "colSystemNo";
            this.colSystemNo.Visible = true;
            this.colSystemNo.VisibleIndex = 0;
            this.colSystemNo.Width = 90;
            // 
            // colInvoiceNo
            // 
            this.colInvoiceNo.Caption = "InvoiceNo";
            this.colInvoiceNo.FieldName = "InvoiceNo";
            this.colInvoiceNo.Name = "colInvoiceNo";
            this.colInvoiceNo.Visible = true;
            this.colInvoiceNo.VisibleIndex = 1;
            this.colInvoiceNo.Width = 120;
            // 
            // colInvoiceDate
            // 
            this.colInvoiceDate.Caption = "InvoiceDate";
            this.colInvoiceDate.DisplayFormat.FormatString = "g";
            this.colInvoiceDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colInvoiceDate.FieldName = "InvoiceDate";
            this.colInvoiceDate.Name = "colInvoiceDate";
            this.colInvoiceDate.Visible = true;
            this.colInvoiceDate.VisibleIndex = 2;
            this.colInvoiceDate.Width = 131;
            // 
            // colCustomerName
            // 
            this.colCustomerName.Caption = "Customer";
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 3;
            this.colCustomerName.Width = 131;
            // 
            // colAmount
            // 
            this.colAmount.Caption = "Amount";
            this.colAmount.FieldName = "Amounts";
            this.colAmount.Name = "colAmount";
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 5;
            this.colAmount.Width = 69;
            // 
            // colBillNo
            // 
            this.colBillNo.Caption = "BillNo";
            this.colBillNo.FieldName = "BillNo";
            this.colBillNo.Name = "colBillNo";
            this.colBillNo.Visible = true;
            this.colBillNo.VisibleIndex = 6;
            this.colBillNo.Width = 87;
            // 
            // colBLNo
            // 
            this.colBLNo.Caption = "BLNo";
            this.colBLNo.FieldName = "BLNo";
            this.colBLNo.Name = "colBLNo";
            this.colBLNo.Visible = true;
            this.colBLNo.VisibleIndex = 7;
            this.colBLNo.Width = 65;
            // 
            // colETD
            // 
            this.colETD.Caption = "ETD";
            this.colETD.FieldName = "ETD";
            this.colETD.Name = "colETD";
            this.colETD.Visible = true;
            this.colETD.VisibleIndex = 8;
            this.colETD.Width = 69;
            // 
            // colExpress
            // 
            this.colExpress.Caption = "ExpressNo";
            this.colExpress.FieldName = "ExpressNo";
            this.colExpress.Name = "colExpress";
            this.colExpress.Visible = true;
            this.colExpress.VisibleIndex = 9;
            this.colExpress.Width = 94;
            // 
            // colRemark
            // 
            this.colRemark.Caption = "Remark";
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 10;
            this.colRemark.Width = 130;
            // 
            // colIsValid
            // 
            this.colIsValid.Caption = "Valid";
            this.colIsValid.FieldName = "IsValid";
            this.colIsValid.Name = "colIsValid";
            this.colIsValid.Visible = true;
            this.colIsValid.VisibleIndex = 11;
            this.colIsValid.Width = 80;
            // 
            // colCreateByName
            // 
            this.colCreateByName.Caption = "CreateBy";
            this.colCreateByName.FieldName = "CreateByName";
            this.colCreateByName.Name = "colCreateByName";
            this.colCreateByName.Visible = true;
            this.colCreateByName.VisibleIndex = 12;
            this.colCreateByName.Width = 80;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pageControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 509);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(913, 26);
            this.panel1.TabIndex = 3;
            // 
            // pageControl1
            // 
            this.pageControl1.CurrentPage = 0;
            this.pageControl1.Location = new System.Drawing.Point(3, 0);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.Size = new System.Drawing.Size(907, 26);
            this.pageControl1.TabIndex = 0;
            this.pageControl1.TotalPage = 0;
            this.pageControl1.PageChanged += new ICP.Framework.ClientComponents.Controls.PageChangedHandler(this.pageControl1_PageChanged);
            // 
            // bsInvoiceFeeDate
            // 
            this.bsInvoiceFeeDate.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.InvoiceFeeDate);
            // 
            // colInvoiceTitle
            // 
            this.colInvoiceTitle.Caption = "InvoiceTitle";
            this.colInvoiceTitle.FieldName = "InvoiceTitle";
            this.colInvoiceTitle.Name = "colInvoiceTitle";
            this.colInvoiceTitle.Visible = true;
            this.colInvoiceTitle.VisibleIndex = 4;
            this.colInvoiceTitle.Width = 131;
            // 
            // InvoiceListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.panel1);
            this.Name = "InvoiceListPart";
            this.Size = new System.Drawing.Size(913, 535);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsInvoiceFeeDate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        private ICP.Framework.ClientComponents.Controls.LWGridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceNo;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceDate;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colBLNo;
        private DevExpress.XtraGrid.Columns.GridColumn colETD;
        private DevExpress.XtraGrid.Columns.GridColumn colExpress;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;
        private DevExpress.XtraGrid.Columns.GridColumn colIsValid;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraGrid.Columns.GridColumn colBillNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateByName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.BindingSource bsInvoiceFeeDate;
        private ICP.Framework.ClientComponents.Controls.PageControl pageControl1;
        private DevExpress.XtraGrid.Columns.GridColumn colSystemNo;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceTitle;
    }
}
