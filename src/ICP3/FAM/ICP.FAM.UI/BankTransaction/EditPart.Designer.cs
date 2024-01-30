namespace ICP.FAM.UI.BankTransaction
{
    partial class EditPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditPart));
            this.bsEidt = new System.Windows.Forms.BindingSource(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barAssociation = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colChecksNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChecksWay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChecksAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankAccountName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckser = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChecksDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchk = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEidt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchk)).BeginInit();
            this.SuspendLayout();
            // 
            // bsEidt
            // 
            this.bsEidt.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.BankTransaction2Checks);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barAssociation});
            this.barManager1.MaxItemId = 2;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barAssociation)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barAssociation
            // 
            this.barAssociation.Caption = "Association(&A)";
            this.barAssociation.Glyph = global::ICP.FAM.UI.Properties.Resources.Transfer_16;
            this.barAssociation.Id = 0;
            this.barAssociation.Name = "barAssociation";
            this.barAssociation.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(748, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 340);
            this.barDockControlBottom.Size = new System.Drawing.Size(748, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 314);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(748, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 314);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsEidt;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 26);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchk});
            this.gcMain.Size = new System.Drawing.Size(748, 314);
            this.gcMain.TabIndex = 36;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colChecksNO,
            this.colChecksWay,
            this.colChecksAmount,
            this.colCustomerName,
            this.colBankAccountName,
            this.colCurrencyName,
            this.colCheckser,
            this.colChecksDate});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            // 
            // colChecksNO
            // 
            this.colChecksNO.FieldName = "ChecksNO";
            this.colChecksNO.MinWidth = 120;
            this.colChecksNO.Name = "colChecksNO";
            this.colChecksNO.OptionsColumn.AllowEdit = false;
            this.colChecksNO.Visible = true;
            this.colChecksNO.VisibleIndex = 0;
            this.colChecksNO.Width = 120;
            // 
            // colChecksWay
            // 
            this.colChecksWay.FieldName = "ChecksWay";
            this.colChecksWay.MinWidth = 80;
            this.colChecksWay.Name = "colChecksWay";
            this.colChecksWay.OptionsColumn.AllowEdit = false;
            this.colChecksWay.Visible = true;
            this.colChecksWay.VisibleIndex = 1;
            this.colChecksWay.Width = 80;
            // 
            // colChecksAmount
            // 
            this.colChecksAmount.FieldName = "ChecksAmount";
            this.colChecksAmount.MinWidth = 120;
            this.colChecksAmount.Name = "colChecksAmount";
            this.colChecksAmount.OptionsColumn.AllowEdit = false;
            this.colChecksAmount.Visible = true;
            this.colChecksAmount.VisibleIndex = 2;
            this.colChecksAmount.Width = 120;
            // 
            // colCustomerName
            // 
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.MinWidth = 150;
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.OptionsColumn.AllowEdit = false;
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 3;
            this.colCustomerName.Width = 150;
            // 
            // colBankAccountName
            // 
            this.colBankAccountName.FieldName = "BankAccountName";
            this.colBankAccountName.MinWidth = 150;
            this.colBankAccountName.Name = "colBankAccountName";
            this.colBankAccountName.OptionsColumn.AllowEdit = false;
            this.colBankAccountName.Visible = true;
            this.colBankAccountName.VisibleIndex = 4;
            this.colBankAccountName.Width = 150;
            // 
            // colCurrencyName
            // 
            this.colCurrencyName.FieldName = "CurrencyName";
            this.colCurrencyName.MinWidth = 70;
            this.colCurrencyName.Name = "colCurrencyName";
            this.colCurrencyName.OptionsColumn.AllowEdit = false;
            this.colCurrencyName.Visible = true;
            this.colCurrencyName.VisibleIndex = 5;
            this.colCurrencyName.Width = 70;
            // 
            // colCheckser
            // 
            this.colCheckser.FieldName = "Checkser";
            this.colCheckser.MinWidth = 100;
            this.colCheckser.Name = "colCheckser";
            this.colCheckser.OptionsColumn.AllowEdit = false;
            this.colCheckser.Visible = true;
            this.colCheckser.VisibleIndex = 6;
            this.colCheckser.Width = 100;
            // 
            // colChecksDate
            // 
            this.colChecksDate.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.colChecksDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colChecksDate.FieldName = "ChecksDate";
            this.colChecksDate.MinWidth = 120;
            this.colChecksDate.Name = "colChecksDate";
            this.colChecksDate.OptionsColumn.AllowEdit = false;
            this.colChecksDate.Visible = true;
            this.colChecksDate.VisibleIndex = 7;
            this.colChecksDate.Width = 120;
            // 
            // rchk
            // 
            this.rchk.AutoHeight = false;
            this.rchk.Name = "rchk";
            // 
            // EditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "EditPart";
            this.Size = new System.Drawing.Size(748, 340);
            ((System.ComponentModel.ISupportInitialize)(this.bsEidt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchk)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource bsEidt;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barAssociation;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchk;
        private DevExpress.XtraGrid.Columns.GridColumn colChecksNO;
        private DevExpress.XtraGrid.Columns.GridColumn colChecksWay;
        private DevExpress.XtraGrid.Columns.GridColumn colChecksAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colBankAccountName;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyName;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckser;
        private DevExpress.XtraGrid.Columns.GridColumn colChecksDate;
    }
}
