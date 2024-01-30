namespace ICP.WF.Controls.Form.CustomerExpense
{
    partial class WFCRMCustomerListPart
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
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKeyWord = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCountry = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.rchkSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
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
            this.rchkSelected});
            this.gcMain.Size = new System.Drawing.Size(935, 353);
            this.gcMain.TabIndex = 0;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.WF.ServiceInterface.WFCECRMCustomerList);
            this.bsList.PositionChanged+=new System.EventHandler(bsList_PositionChanged);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCode,
            this.colKeyWord,
            this.colCName,
            this.colEName,
            this.colCountry});
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

            // 
            // colCode
            // 
            this.colCode.Caption = "Code";
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.OptionsColumn.AllowEdit = false;
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 0;
            this.colCode.Width = 100;
            // 
            // colKeyWord
            // 
            this.colKeyWord.Caption = "KeyWord";
            this.colKeyWord.FieldName = "KeyWord";
            this.colKeyWord.Name = "colKeyWord";
            this.colKeyWord.OptionsColumn.AllowEdit = false;
            this.colKeyWord.Visible = true;
            this.colKeyWord.VisibleIndex = 1;
            this.colKeyWord.Width = 100;
            // 
            // colCName
            // 
            this.colCName.Caption = "CName";
            this.colCName.FieldName = "CName";
            this.colCName.Name = "colCName";
            this.colCName.OptionsColumn.AllowEdit = false;
            this.colCName.Visible = true;
            this.colCName.VisibleIndex = 2;
            this.colCName.Width = 250;
            // 
            // colEName
            // 
            this.colEName.Caption = "EName";
            this.colEName.FieldName = "EName";
            this.colEName.Name = "colEName";
            this.colEName.OptionsColumn.AllowEdit = false;
            this.colEName.Visible = true;
            this.colEName.VisibleIndex = 3;
            this.colEName.Width = 250;
            // 
            // colCountry
            // 
            this.colCountry.Caption = "Country";
            this.colCountry.FieldName = "Country";
            this.colCountry.Name = "colCountry";
            this.colCountry.OptionsColumn.AllowEdit = false;
            this.colCountry.Visible = true;
            this.colCountry.VisibleIndex = 4;
            this.colCountry.Width = 80;
         
            // 
            // WFCRMCustomerListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Name = "WFCRMCustomerListPart";
            this.Size = new System.Drawing.Size(935, 353);
            ((System.ComponentModel.ISupportInitialize)(this.rchkSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion

        private ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        private ICP.Framework.ClientComponents.Controls.LWGridView gvMain;
        private System.Windows.Forms.BindingSource bsList;

        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colKeyWord;
        private DevExpress.XtraGrid.Columns.GridColumn colCName;
        private DevExpress.XtraGrid.Columns.GridColumn colEName;
        private DevExpress.XtraGrid.Columns.GridColumn colCountry;

        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkSelected;
    }
}
