namespace ICP.FAM.UI.Controls
{
    partial class CustomerBankPopupContainerControl
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
            this.bsData = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAccountName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBranchName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.rcmbCompany = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.rlueCompany = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlueCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsData;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1,
            this.rcmbCompany,
            this.rlueCompany});
            this.gcMain.Size = new System.Drawing.Size(454, 289);
            this.gcMain.TabIndex = 1;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // bsData
            // 
            this.bsData.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.CustomerBankInfo);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAccountName,
            this.colAccountNO,
            this.colBranchName,
            this.colBankNumber,
            this.colBankName});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsBehavior.ReadOnly = true;
            this.gvMain.OptionsPrint.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsPrint.EnableAppearanceOddRow = true;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.RowAutoHeight = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            // 
            // colAccountName
            // 
            this.colAccountName.FieldName = "AccountName";
            this.colAccountName.MinWidth = 200;
            this.colAccountName.Name = "colAccountName";
            this.colAccountName.Visible = true;
            this.colAccountName.VisibleIndex = 0;
            this.colAccountName.Width = 200;
            // 
            // colAccountNO
            // 
            this.colAccountNO.FieldName = "AccountNO";
            this.colAccountNO.MinWidth = 120;
            this.colAccountNO.Name = "colAccountNO";
            this.colAccountNO.Visible = true;
            this.colAccountNO.VisibleIndex = 1;
            this.colAccountNO.Width = 120;
            // 
            // colBranchName
            // 
            this.colBranchName.FieldName = "BranchName";
            this.colBranchName.MinWidth = 150;
            this.colBranchName.Name = "colBranchName";
            this.colBranchName.Visible = true;
            this.colBranchName.VisibleIndex = 2;
            this.colBranchName.Width = 150;
            // 
            // colBankNumber
            // 
            this.colBankNumber.FieldName = "BankNumber";
            this.colBankNumber.MinWidth = 120;
            this.colBankNumber.Name = "colBankNumber";
            this.colBankNumber.Visible = true;
            this.colBankNumber.VisibleIndex = 3;
            this.colBankNumber.Width = 120;
            // 
            // colBankName
            // 
            this.colBankName.FieldName = "BankName";
            this.colBankName.MinWidth = 120;
            this.colBankName.Name = "colBankName";
            this.colBankName.Visible = true;
            this.colBankName.VisibleIndex = 4;
            this.colBankName.Width = 120;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // rcmbCompany
            // 
            this.rcmbCompany.AutoHeight = false;
            this.rcmbCompany.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbCompany.Name = "rcmbCompany";
            // 
            // rlueCompany
            // 
            this.rlueCompany.AutoHeight = false;
            this.rlueCompany.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rlueCompany.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "公司", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CShortName", 120, "公司")});
            this.rlueCompany.Name = "rlueCompany";
            this.rlueCompany.NullText = "";
            // 
            // CustomerBankPopupContainerControl
            // 
            this.Controls.Add(this.gcMain);
            this.Size = new System.Drawing.Size(454, 289);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlueCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rlueCompany;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox rcmbCompany;
        private System.Windows.Forms.BindingSource bsData;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountName;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountNO;
        private DevExpress.XtraGrid.Columns.GridColumn colBranchName;
        private DevExpress.XtraGrid.Columns.GridColumn colBankNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colBankName;
    }
}
