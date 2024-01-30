namespace ICP.FAM.UI
{
    partial class LedgerListPart
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
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.pnlAuditor = new System.Windows.Forms.Panel();
            this.pnlUnAuditor = new System.Windows.Forms.Panel();
            this.labUnAuditor = new DevExpress.XtraEditors.LabelControl();
            this.labAuditorTotal = new DevExpress.XtraEditors.LabelControl();
            this.labLedgerTotoal = new DevExpress.XtraEditors.LabelControl();
            this.cmbStatus = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.cmbType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemCheckIsValid = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRefNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCRAmt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDRAmt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsCarryOver = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsValid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateUser = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCashier = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCashierDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAuditor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckIsValid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.LedgerListInfo);
            this.bsList.PositionChanged += new System.EventHandler(this.bsList_PositionChanged);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.pnlAuditor);
            this.pnlBottom.Controls.Add(this.pnlUnAuditor);
            this.pnlBottom.Controls.Add(this.labUnAuditor);
            this.pnlBottom.Controls.Add(this.labAuditorTotal);
            this.pnlBottom.Controls.Add(this.labLedgerTotoal);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 672);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1006, 28);
            this.pnlBottom.TabIndex = 1;
            // 
            // pnlAuditor
            // 
            this.pnlAuditor.BackColor = System.Drawing.Color.LightYellow;
            this.pnlAuditor.Location = new System.Drawing.Point(153, 6);
            this.pnlAuditor.Name = "pnlAuditor";
            this.pnlAuditor.Size = new System.Drawing.Size(26, 18);
            this.pnlAuditor.TabIndex = 3;
            // 
            // pnlUnAuditor
            // 
            this.pnlUnAuditor.BackColor = System.Drawing.Color.White;
            this.pnlUnAuditor.Location = new System.Drawing.Point(314, 6);
            this.pnlUnAuditor.Name = "pnlUnAuditor";
            this.pnlUnAuditor.Size = new System.Drawing.Size(26, 18);
            this.pnlUnAuditor.TabIndex = 4;
            // 
            // labUnAuditor
            // 
            this.labUnAuditor.Location = new System.Drawing.Point(345, 8);
            this.labUnAuditor.Name = "labUnAuditor";
            this.labUnAuditor.Size = new System.Drawing.Size(63, 14);
            this.labUnAuditor.TabIndex = 0;
            this.labUnAuditor.Text = "未审核 0 张";
            // 
            // labAuditorTotal
            // 
            this.labAuditorTotal.Location = new System.Drawing.Point(185, 8);
            this.labAuditorTotal.Name = "labAuditorTotal";
            this.labAuditorTotal.Size = new System.Drawing.Size(63, 14);
            this.labAuditorTotal.TabIndex = 0;
            this.labAuditorTotal.Text = "已审核 0 张";
            // 
            // labLedgerTotoal
            // 
            this.labLedgerTotoal.Location = new System.Drawing.Point(10, 8);
            this.labLedgerTotoal.Name = "labLedgerTotoal";
            this.labLedgerTotoal.Size = new System.Drawing.Size(63, 14);
            this.labLedgerTotoal.TabIndex = 0;
            this.labLedgerTotoal.Text = "凭证共 0 张";
            // 
            // cmbStatus
            // 
            this.cmbStatus.AutoHeight = false;
            this.cmbStatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbStatus.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("制单", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("审核", 2, -1)});
            this.cmbStatus.Name = "cmbStatus";
            // 
            // cmbType
            // 
            this.cmbType.AutoHeight = false;
            this.cmbType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Name = "cmbType";
            // 
            // repositoryItemCheckIsValid
            // 
            this.repositoryItemCheckIsValid.AutoHeight = false;
            this.repositoryItemCheckIsValid.Name = "repositoryItemCheckIsValid";
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colStatus,
            this.colNo,
            this.colRefNo,
            this.colCreateDate,
            this.colRemark,
            this.colCRAmt,
            this.colDRAmt,
            this.colType,
            this.colIsCarryOver,
            this.colIsValid,
            this.colCreateUser,
            this.colCashier,
            this.colCashierDate,
            this.colAuditor});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.GroupCount = 1;
            this.gvMain.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "Status", null, "共:{0} 张")});
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsPrint.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsPrint.EnableAppearanceOddRow = true;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colStatus, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvMain.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvMain_RowCellClick);
            this.gvMain.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvMain_RowStyle);
            this.gvMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvMain_KeyDown);
            // 
            // colStatus
            // 
            this.colStatus.Caption = "状态";
            this.colStatus.ColumnEdit = this.cmbStatus;
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.Width = 53;
            // 
            // colNo
            // 
            this.colNo.Caption = "凭证号";
            this.colNo.FieldName = "No";
            this.colNo.Name = "colNo";
            this.colNo.SummaryItem.DisplayFormat = "凭证共:";
            this.colNo.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            this.colNo.Visible = true;
            this.colNo.VisibleIndex = 0;
            this.colNo.Width = 102;
            // 
            // colRefNo
            // 
            this.colRefNo.Caption = "参考号";
            this.colRefNo.FieldName = "RefNo";
            this.colRefNo.Name = "colRefNo";
            this.colRefNo.Visible = true;
            this.colRefNo.VisibleIndex = 1;
            this.colRefNo.Width = 120;
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "制单时间";
            this.colCreateDate.FieldName = "DATE";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.SummaryItem.DisplayFormat = "{0} 张";
            this.colCreateDate.SummaryItem.FieldName = "No";
            this.colCreateDate.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 2;
            this.colCreateDate.Width = 84;
            // 
            // colRemark
            // 
            this.colRemark.Caption = "摘要";
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 3;
            this.colRemark.Width = 262;
            // 
            // colCRAmt
            // 
            this.colCRAmt.Caption = "借方金额";
            this.colCRAmt.DisplayFormat.FormatString = "n";
            this.colCRAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCRAmt.FieldName = "CRAmt";
            this.colCRAmt.Name = "colCRAmt";
            this.colCRAmt.Visible = true;
            this.colCRAmt.VisibleIndex = 4;
            this.colCRAmt.Width = 90;
            // 
            // colDRAmt
            // 
            this.colDRAmt.Caption = "贷方金额";
            this.colDRAmt.DisplayFormat.FormatString = "n";
            this.colDRAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDRAmt.FieldName = "DRAmt";
            this.colDRAmt.Name = "colDRAmt";
            this.colDRAmt.Visible = true;
            this.colDRAmt.VisibleIndex = 5;
            this.colDRAmt.Width = 88;
            // 
            // colType
            // 
            this.colType.Caption = "类型";
            this.colType.ColumnEdit = this.cmbType;
            this.colType.FieldName = "Type";
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 6;
            this.colType.Width = 73;
            // 
            // colIsCarryOver
            // 
            this.colIsCarryOver.Caption = "结转凭证";
            this.colIsCarryOver.ColumnEdit = this.repositoryItemCheckIsValid;
            this.colIsCarryOver.FieldName = "IsCarryOver";
            this.colIsCarryOver.Name = "colIsCarryOver";
            this.colIsCarryOver.Visible = true;
            this.colIsCarryOver.VisibleIndex = 8;
            this.colIsCarryOver.Width = 69;
            // 
            // colIsValid
            // 
            this.colIsValid.Caption = "有效性";
            this.colIsValid.ColumnEdit = this.repositoryItemCheckIsValid;
            this.colIsValid.FieldName = "IsValid";
            this.colIsValid.Name = "colIsValid";
            this.colIsValid.Visible = true;
            this.colIsValid.VisibleIndex = 7;
            this.colIsValid.Width = 63;
            // 
            // colCreateUser
            // 
            this.colCreateUser.Caption = "制单人";
            this.colCreateUser.FieldName = "CreateUser";
            this.colCreateUser.Name = "colCreateUser";
            this.colCreateUser.Visible = true;
            this.colCreateUser.VisibleIndex = 9;
            this.colCreateUser.Width = 74;
            // 
            // colCashier
            // 
            this.colCashier.Caption = "出纳员";
            this.colCashier.FieldName = "Cashier";
            this.colCashier.Name = "colCashier";
            this.colCashier.Visible = true;
            this.colCashier.VisibleIndex = 10;
            // 
            // colCashierDate
            // 
            this.colCashierDate.Caption = "出纳签字时间";
            this.colCashierDate.FieldName = "CashierDate";
            this.colCashierDate.Name = "colCashierDate";
            this.colCashierDate.Visible = true;
            this.colCashierDate.VisibleIndex = 12;
            this.colCashierDate.Width = 92;
            // 
            // colAuditor
            // 
            this.colAuditor.Caption = "审核人";
            this.colAuditor.FieldName = "Auditor";
            this.colAuditor.Name = "colAuditor";
            this.colAuditor.Visible = true;
            this.colAuditor.VisibleIndex = 11;
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbStatus,
            this.cmbType,
            this.repositoryItemCheckIsValid});
            this.gcMain.Size = new System.Drawing.Size(1006, 672);
            this.gcMain.TabIndex = 0;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // LedgerListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.pnlBottom);
            this.Name = "LedgerListPart";
            this.Size = new System.Drawing.Size(1006, 700);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckIsValid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.LabelControl labLedgerTotoal;
        private DevExpress.XtraEditors.LabelControl labAuditorTotal;
        private DevExpress.XtraEditors.LabelControl labUnAuditor;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbStatus;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbType;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckIsValid;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;
        private DevExpress.XtraGrid.Columns.GridColumn colCRAmt;
        private DevExpress.XtraGrid.Columns.GridColumn colDRAmt;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colIsValid;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateUser;
        private DevExpress.XtraGrid.Columns.GridColumn colAuditor;
        private DevExpress.XtraGrid.GridControl gcMain;
        private System.Windows.Forms.Panel pnlAuditor;
        private System.Windows.Forms.Panel pnlUnAuditor;
        private DevExpress.XtraGrid.Columns.GridColumn colRefNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCashierDate;
        private DevExpress.XtraGrid.Columns.GridColumn colCashier;
        private DevExpress.XtraGrid.Columns.GridColumn colIsCarryOver;
    }
}
