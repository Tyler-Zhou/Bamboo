namespace ICP.WF.WorkFlowDesigner
{
    partial class UCConfigUserList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCConfigUserList));
            this.gcUserList = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.UserBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvUserList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dcDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.bar3 = new DevExpress.XtraBars.Bar();
            ((System.ComponentModel.ISupportInitialize)(this.gcUserList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUserList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcUserList
            // 
            this.gcUserList.DataSource = this.UserBindingSource;
            resources.ApplyResources(this.gcUserList, "gcUserList");
            this.gcUserList.MainView = this.gvUserList;
            this.gcUserList.Name = "gcUserList";
            this.gcUserList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1,
            this.btnDelete});
            this.gcUserList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUserList});
           
            // 
            // UserBindingSource
            // 
            this.UserBindingSource.AddingNew += new System.ComponentModel.AddingNewEventHandler(this.UserBindingSource_AddingNew);
            // 
            // gvUserList
            // 
            this.gvUserList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUserName,
            this.dcDelete});
            this.gvUserList.GridControl = this.gcUserList;
            this.gvUserList.Name = "gvUserList";
            this.gvUserList.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvUserList.OptionsSelection.MultiSelect = true;
            this.gvUserList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvUserList.OptionsView.EnableAppearanceEvenRow = true;
            this.gvUserList.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gvUserList.OptionsView.ShowGroupPanel = false;
            this.gvUserList.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colUserName, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colUserName
            // 
            resources.ApplyResources(this.colUserName, "colUserName");
            this.colUserName.FieldName = "UserName";
            this.colUserName.Name = "colUserName";
            // 
            // dcDelete
            // 
            this.dcDelete.ColumnEdit = this.btnDelete;
            this.dcDelete.Name = "dcDelete";
            this.dcDelete.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            resources.ApplyResources(this.dcDelete, "dcDelete");
            // 
            // btnDelete
            // 
            this.btnDelete.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btnDelete.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // repositoryItemImageComboBox1
            // 
            resources.ApplyResources(this.repositoryItemImageComboBox1, "repositoryItemImageComboBox1");
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // bar3
            // 
            this.bar3.BarName = "Main menu";
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar3.OptionsBar.MultiLine = true;
            this.bar3.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.bar3, "bar3");
            // 
            // UCConfigUserList
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcUserList);
            this.Name = "UCConfigUserList";
            ((System.ComponentModel.ISupportInitialize)(this.gcUserList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUserList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraGrid.Views.Grid.GridView gvUserList;
        private DevExpress.XtraGrid.Columns.GridColumn colUserName;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private System.Windows.Forms.BindingSource UserBindingSource;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraGrid.Columns.GridColumn dcDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDelete;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gcUserList;
    }
}
