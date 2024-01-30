namespace ICP.FAM.UI.WriteOff.Parts
{
    partial class MultiSelections
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiSelections));
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barArrive = new DevExpress.XtraBars.BarButtonItem();
            this.barUnArrive = new DevExpress.XtraBars.BarButtonItem();
            this.barAuditor = new DevExpress.XtraBars.BarButtonItem();
            this.barUnAuditor = new DevExpress.XtraBars.BarButtonItem();
            this.barRemove = new DevExpress.XtraBars.BarButtonItem();
            this.barClear = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.bbiBullion = new DevExpress.XtraBars.BarButtonItem();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.bsList = new System.Windows.Forms.BindingSource();
            this.gvWriteOffList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colWay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbWay = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imgType = new System.Windows.Forms.ImageList();
            this.colNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankAccount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFinalAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReachedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAuditBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcolAuditBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.txtTotal = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWriteOffList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).BeginInit();
            this.SuspendLayout();
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
            this.barArrive,
            this.barButtonItem2,
            this.barAuditor,
            this.barUnAuditor,
            this.barUnArrive,
            this.barRemove,
            this.barClear});
            this.barManager1.MaxItemId = 7;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barArrive),
            new DevExpress.XtraBars.LinkPersistInfo(this.barUnArrive),
            new DevExpress.XtraBars.LinkPersistInfo(this.barAuditor, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barUnAuditor),
            new DevExpress.XtraBars.LinkPersistInfo(this.barRemove, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barClear)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barArrive
            // 
            this.barArrive.Caption = "到账";
            this.barArrive.Glyph = global::ICP.FAM.UI.Properties.Resources.DollarSign;
            this.barArrive.Id = 0;
            this.barArrive.Name = "barArrive";
            this.barArrive.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barArrive.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barArrive_ItemClick);
            // 
            // barUnArrive
            // 
            this.barUnArrive.Caption = "取消到账";
            this.barUnArrive.Glyph = global::ICP.FAM.UI.Properties.Resources.CancelBullion;
            this.barUnArrive.Id = 4;
            this.barUnArrive.Name = "barUnArrive";
            this.barUnArrive.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barUnArrive.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barUnArrive_ItemClick);
            // 
            // barAuditor
            // 
            this.barAuditor.Caption = "审核";
            this.barAuditor.Glyph = global::ICP.FAM.UI.Properties.Resources.Review;
            this.barAuditor.Id = 2;
            this.barAuditor.Name = "barAuditor";
            this.barAuditor.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barAuditor.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAuditor_ItemClick);
            // 
            // barUnAuditor
            // 
            this.barUnAuditor.Caption = "取消审核";
            this.barUnAuditor.Glyph = global::ICP.FAM.UI.Properties.Resources.CancelBullion;
            this.barUnAuditor.Id = 3;
            this.barUnAuditor.Name = "barUnAuditor";
            this.barUnAuditor.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barUnAuditor.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barUnAuditor_ItemClick);
            // 
            // barRemove
            // 
            this.barRemove.Caption = "移除";
            this.barRemove.Glyph = global::ICP.FAM.UI.Properties.Resources.Delete_16;
            this.barRemove.Id = 5;
            this.barRemove.Name = "barRemove";
            this.barRemove.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barRemove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRemove_ItemClick);
            // 
            // barClear
            // 
            this.barClear.Caption = "清空";
            this.barClear.Glyph = global::ICP.FAM.UI.Properties.Resources.Empty;
            this.barClear.Id = 6;
            this.barClear.Name = "barClear";
            this.barClear.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barClear.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClear_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(790, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 429);
            this.barDockControlBottom.Size = new System.Drawing.Size(790, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 403);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(790, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 403);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "取消到账";
            this.barButtonItem2.Id = 1;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // bbiBullion
            // 
            this.bbiBullion.Caption = "到账";
            this.bbiBullion.Glyph = global::ICP.FAM.UI.Properties.Resources.DollarSign;
            this.bbiBullion.Id = 2;
            this.bbiBullion.Name = "bbiBullion";
            this.bbiBullion.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 26);
            this.gcMain.MainView = this.gvWriteOffList;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.cmbWay});
            this.gcMain.Size = new System.Drawing.Size(790, 379);
            this.gcMain.TabIndex = 4;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvWriteOffList});
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.WriteOffItemList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsList_PositionChanged);
            // 
            // gvWriteOffList
            // 
            this.gvWriteOffList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colWay,
            this.colNo,
            this.colCheckNo,
            this.colCustomer,
            this.colBankAccount,
            this.colCurrency,
            this.colAmount,
            this.colFinalAmount,
            this.colReachedDate,
            this.colAuditBy,
            this.colcolAuditBy,
            this.colCreatedOn});
            this.gvWriteOffList.GridControl = this.gcMain;
            this.gvWriteOffList.Name = "gvWriteOffList";
            this.gvWriteOffList.OptionsSelection.MultiSelect = true;
            this.gvWriteOffList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvWriteOffList.OptionsView.ColumnAutoWidth = false;
            this.gvWriteOffList.OptionsView.ShowGroupPanel = false;
            // 
            // colWay
            // 
            this.colWay.Caption = "方向";
            this.colWay.ColumnEdit = this.cmbWay;
            this.colWay.FieldName = "Type";
            this.colWay.Name = "colWay";
            this.colWay.OptionsColumn.AllowEdit = false;
            this.colWay.Visible = true;
            this.colWay.VisibleIndex = 0;
            this.colWay.Width = 45;
            // 
            // cmbWay
            // 
            this.cmbWay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbWay.Name = "cmbWay";
            this.cmbWay.SmallImages = this.imgType;
            // 
            // imgType
            // 
            this.imgType.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgType.ImageStream")));
            this.imgType.TransparentColor = System.Drawing.Color.Transparent;
            this.imgType.Images.SetKeyName(0, "+.png");
            this.imgType.Images.SetKeyName(1, "-.png");
            // 
            // colNo
            // 
            this.colNo.Caption = "单号";
            this.colNo.FieldName = "No";
            this.colNo.Name = "colNo";
            this.colNo.OptionsColumn.AllowEdit = false;
            this.colNo.Visible = true;
            this.colNo.VisibleIndex = 1;
            this.colNo.Width = 70;
            // 
            // colCheckNo
            // 
            this.colCheckNo.Caption = "支票号";
            this.colCheckNo.FieldName = "CheckNo";
            this.colCheckNo.Name = "colCheckNo";
            this.colCheckNo.OptionsColumn.AllowEdit = false;
            this.colCheckNo.Visible = true;
            this.colCheckNo.VisibleIndex = 2;
            this.colCheckNo.Width = 80;
            // 
            // colCustomer
            // 
            this.colCustomer.Caption = "客户";
            this.colCustomer.FieldName = "CustomerName";
            this.colCustomer.Name = "colCustomer";
            this.colCustomer.OptionsColumn.AllowEdit = false;
            this.colCustomer.Visible = true;
            this.colCustomer.VisibleIndex = 3;
            this.colCustomer.Width = 120;
            // 
            // colBankAccount
            // 
            this.colBankAccount.Caption = "银行账号";
            this.colBankAccount.FieldName = "BankAccount";
            this.colBankAccount.Name = "colBankAccount";
            this.colBankAccount.OptionsColumn.AllowEdit = false;
            this.colBankAccount.Visible = true;
            this.colBankAccount.VisibleIndex = 4;
            this.colBankAccount.Width = 120;
            // 
            // colCurrency
            // 
            this.colCurrency.Caption = "币种";
            this.colCurrency.FieldName = "Currency";
            this.colCurrency.Name = "colCurrency";
            this.colCurrency.OptionsColumn.AllowEdit = false;
            this.colCurrency.Visible = true;
            this.colCurrency.VisibleIndex = 5;
            this.colCurrency.Width = 45;
            // 
            // colAmount
            // 
            this.colAmount.Caption = "金额";
            this.colAmount.DisplayFormat.FormatString = "n";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.AllowEdit = false;
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 6;
            this.colAmount.Width = 70;
            // 
            // colFinalAmount
            // 
            this.colFinalAmount.Caption = "实际金额";
            this.colFinalAmount.DisplayFormat.FormatString = "n";
            this.colFinalAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colFinalAmount.FieldName = "FinalAmount";
            this.colFinalAmount.Name = "colFinalAmount";
            this.colFinalAmount.Visible = true;
            this.colFinalAmount.VisibleIndex = 7;
            // 
            // colReachedDate
            // 
            this.colReachedDate.Caption = "到账日期";
            this.colReachedDate.FieldName = "ReachedDate";
            this.colReachedDate.Name = "colReachedDate";
            this.colReachedDate.Visible = true;
            this.colReachedDate.VisibleIndex = 8;
            // 
            // colAuditBy
            // 
            this.colAuditBy.Caption = "审核人";
            this.colAuditBy.FieldName = "ApprovalByName";
            this.colAuditBy.Name = "colAuditBy";
            this.colAuditBy.OptionsColumn.AllowEdit = false;
            this.colAuditBy.Visible = true;
            this.colAuditBy.VisibleIndex = 9;
            // 
            // colcolAuditBy
            // 
            this.colcolAuditBy.Caption = "到账人";
            this.colcolAuditBy.FieldName = "BankByName";
            this.colcolAuditBy.Name = "colcolAuditBy";
            this.colcolAuditBy.OptionsColumn.AllowEdit = false;
            this.colcolAuditBy.Visible = true;
            this.colcolAuditBy.VisibleIndex = 10;
            // 
            // colCreatedOn
            // 
            this.colCreatedOn.Caption = "输入日期";
            this.colCreatedOn.FieldName = "CreateDate";
            this.colCreatedOn.Name = "colCreatedOn";
            this.colCreatedOn.OptionsColumn.AllowEdit = false;
            this.colCreatedOn.Visible = true;
            this.colCreatedOn.VisibleIndex = 11;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.txtTotal);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 405);
            this.pnlBottom.MaximumSize = new System.Drawing.Size(0, 24);
            this.pnlBottom.MinimumSize = new System.Drawing.Size(0, 24);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(790, 24);
            this.pnlBottom.TabIndex = 9;
            // 
            // txtTotal
            // 
            this.txtTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTotal.Location = new System.Drawing.Point(2, 2);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtTotal.Properties.Appearance.Options.UseBackColor = true;
            this.txtTotal.Properties.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(786, 21);
            this.txtTotal.TabIndex = 0;
            // 
            // MultiSelections
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "MultiSelections";
            this.Size = new System.Drawing.Size(790, 429);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWriteOffList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barArrive;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barAuditor;
        private DevExpress.XtraBars.BarButtonItem barUnAuditor;
        private DevExpress.XtraBars.BarButtonItem bbiBullion;
        private DevExpress.XtraBars.BarButtonItem barUnArrive;
        private DevExpress.XtraBars.BarButtonItem barRemove;
        private DevExpress.XtraBars.BarButtonItem barClear;
        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvWriteOffList;
        private DevExpress.XtraGrid.Columns.GridColumn colWay;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbWay;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomer;
        private DevExpress.XtraGrid.Columns.GridColumn colBankAccount;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colReachedDate;
        private DevExpress.XtraGrid.Columns.GridColumn colAuditBy;
        private DevExpress.XtraGrid.Columns.GridColumn colcolAuditBy;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedOn;
        private System.Windows.Forms.BindingSource bsList;
        private System.Windows.Forms.ImageList imgType;
        private DevExpress.XtraGrid.Columns.GridColumn colFinalAmount;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.TextEdit txtTotal;
    }
}
