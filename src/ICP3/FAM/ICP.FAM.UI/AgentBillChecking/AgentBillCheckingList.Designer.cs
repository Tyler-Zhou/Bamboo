namespace ICP.FAM.UI
{
    partial class AgentBillCheckingList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgentBillCheckingList));
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new ICP.Framework.ClientComponents.Controls.LWGridView();
            this.colNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillCheckUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillCheckRange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbStatus = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colCreateName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pageControl1 = new ICP.Framework.ClientComponents.Controls.PageControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStatus)).BeginInit();
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
            this.cmbStatus});
            this.gcMain.Size = new System.Drawing.Size(739, 457);
            this.gcMain.TabIndex = 3;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            this.gcMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcMain_KeyDown);
           
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.AgnetBillCheckList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsList_PositionChanged);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNo,
            this.colBillCheckUnit,
            this.colBillCheckRange,
            this.colStatus,
            this.colCreateName,
            this.colCreateDate});
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
            this.gvMain.CustomerSorting += new ICP.Framework.ClientComponents.Controls.CustomerSortingHandler(this.gvMain_CustomerSorting);
            this.gvMain.DoubleClick += new System.EventHandler(this.gvMain_DoubleClick);
            this.gvMain.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvMain_RowStyle);
            // 
            // colNo
            // 
            this.colNo.Caption = "对账单号";
            this.colNo.FieldName = "No";
            this.colNo.Name = "colNo";
            this.colNo.Visible = true;
            this.colNo.VisibleIndex = 0;
            this.colNo.Width = 124;
            // 
            // colBillCheckUnit
            // 
            this.colBillCheckUnit.Caption = "对账单位";
            this.colBillCheckUnit.FieldName = "BillCheckInfo";
            this.colBillCheckUnit.Name = "colBillCheckUnit";
            this.colBillCheckUnit.Visible = true;
            this.colBillCheckUnit.VisibleIndex = 1;
            this.colBillCheckUnit.Width = 300;
            // 
            // colBillCheckRange
            // 
            this.colBillCheckRange.Caption = "对账范围";
            this.colBillCheckRange.FieldName = "BillCheckRange";
            this.colBillCheckRange.Name = "colBillCheckRange";
            this.colBillCheckRange.Visible = true;
            this.colBillCheckRange.VisibleIndex = 2;
            this.colBillCheckRange.Width = 200;
            // 
            // colStatus
            // 
            this.colStatus.Caption = "状态";
            this.colStatus.ColumnEdit = this.cmbStatus;
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 3;
            this.colStatus.Width = 74;
            // 
            // cmbStatus
            // 
            this.cmbStatus.AutoHeight = false;
            this.cmbStatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbStatus.Name = "cmbStatus";
            // 
            // colCreateName
            // 
            this.colCreateName.Caption = "创建人";
            this.colCreateName.FieldName = "CreateName";
            this.colCreateName.Name = "colCreateName";
            this.colCreateName.Visible = true;
            this.colCreateName.VisibleIndex = 4;
            this.colCreateName.Width = 70;
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "创建时间";
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 5;
            this.colCreateDate.Width = 80;
            // 
            // pageControl1
            // 
            this.pageControl1.CurrentPage = 0;
            this.pageControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pageControl1.Location = new System.Drawing.Point(0, 457);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.Size = new System.Drawing.Size(739, 26);
            this.pageControl1.TabIndex = 4;
            this.pageControl1.TotalPage = 0;
            this.pageControl1.PageChanged += new ICP.Framework.ClientComponents.Controls.PageChangedHandler(this.pageControl1_PageChanged);
            // 
            // AgentBillCheckingList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.pageControl1);
            this.Name = "AgentBillCheckingList";
            this.Size = new System.Drawing.Size(739, 483);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStatus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcMain;
        private ICP.Framework.ClientComponents.Controls.LWGridView gvMain;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraGrid.Columns.GridColumn colBillCheckUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colBillCheckRange;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colNo;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbStatus;
        private ICP.Framework.ClientComponents.Controls.PageControl pageControl1;
    }
}
