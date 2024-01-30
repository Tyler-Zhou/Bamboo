namespace ICP.WF.WorkFlowDesigner
{
    partial class UCConfigJobList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCConfigJobList));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.gcJobList = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.JobBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvJobList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colOrganizationName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colJobName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DCDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.rcmbPermission = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.bar1 = new DevExpress.XtraBars.Bar();
            ((System.ComponentModel.ISupportInitialize)(this.gcJobList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.JobBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvJobList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbPermission)).BeginInit();
            this.SuspendLayout();
            // 
            // gcJobList
            // 
            this.gcJobList.AccessibleDescription = null;
            this.gcJobList.AccessibleName = null;
            resources.ApplyResources(this.gcJobList, "gcJobList");
            this.gcJobList.BackgroundImage = null;
            this.gcJobList.DataSource = this.JobBindingSource;
            this.gcJobList.EmbeddedNavigator.AccessibleDescription = null;
            this.gcJobList.EmbeddedNavigator.AccessibleName = null;
            this.gcJobList.EmbeddedNavigator.AllowHtmlTextInToolTip = ((DevExpress.Utils.DefaultBoolean)(resources.GetObject("gcJobList.EmbeddedNavigator.AllowHtmlTextInToolTip")));
            this.gcJobList.EmbeddedNavigator.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gcJobList.EmbeddedNavigator.Anchor")));
            this.gcJobList.EmbeddedNavigator.BackgroundImage = null;
            this.gcJobList.EmbeddedNavigator.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("gcJobList.EmbeddedNavigator.BackgroundImageLayout")));
            this.gcJobList.EmbeddedNavigator.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gcJobList.EmbeddedNavigator.ImeMode")));
            this.gcJobList.EmbeddedNavigator.TextLocation = ((DevExpress.XtraEditors.NavigatorButtonsTextLocation)(resources.GetObject("gcJobList.EmbeddedNavigator.TextLocation")));
            this.gcJobList.EmbeddedNavigator.ToolTip = resources.GetString("gcJobList.EmbeddedNavigator.ToolTip");
            this.gcJobList.EmbeddedNavigator.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("gcJobList.EmbeddedNavigator.ToolTipIconType")));
            this.gcJobList.EmbeddedNavigator.ToolTipTitle = resources.GetString("gcJobList.EmbeddedNavigator.ToolTipTitle");
            this.gcJobList.Font = null;
            this.gcJobList.MainView = this.gvJobList;
            this.gcJobList.Name = "gcJobList";
            this.gcJobList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbPermission,
            this.btnDelete});
            this.gcJobList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvJobList});
            // 
            // JobBindingSource
            // 
            this.JobBindingSource.AddingNew += new System.ComponentModel.AddingNewEventHandler(this.JobBindingSource_AddingNew);
            // 
            // gvJobList
            // 
            resources.ApplyResources(this.gvJobList, "gvJobList");
            this.gvJobList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colOrganizationName,
            this.colJobName,
            this.DCDelete});
            this.gvJobList.GridControl = this.gcJobList;
            this.gvJobList.Name = "gvJobList";
            this.gvJobList.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gvJobList.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gvJobList.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvJobList.OptionsSelection.MultiSelect = true;
            this.gvJobList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvJobList.OptionsSelection.UseIndicatorForSelection = false;
            this.gvJobList.OptionsView.EnableAppearanceEvenRow = true;
            this.gvJobList.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gvJobList.OptionsView.ShowGroupPanel = false;
            // 
            // colOrganizationName
            // 
            resources.ApplyResources(this.colOrganizationName, "colOrganizationName");
            this.colOrganizationName.FieldName = "OrganizationName";
            this.colOrganizationName.Name = "colOrganizationName";
            // 
            // colJobName
            // 
            resources.ApplyResources(this.colJobName, "colJobName");
            this.colJobName.FieldName = "JobName";
            this.colJobName.Name = "colJobName";
            // 
            // DCDelete
            // 
            resources.ApplyResources(this.DCDelete, "DCDelete");
            this.DCDelete.ColumnEdit = this.btnDelete;
            this.DCDelete.Name = "DCDelete";
            this.DCDelete.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleDescription = null;
            this.btnDelete.AccessibleName = null;
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btnDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("btnDelete.Buttons"))), resources.GetString("btnDelete.Buttons1"), ((int)(resources.GetObject("btnDelete.Buttons2"))), ((bool)(resources.GetObject("btnDelete.Buttons3"))), ((bool)(resources.GetObject("btnDelete.Buttons4"))), ((bool)(resources.GetObject("btnDelete.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("btnDelete.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("btnDelete.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, resources.GetString("btnDelete.Buttons8"), null, null, ((bool)(resources.GetObject("btnDelete.Buttons9"))))});
            this.btnDelete.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnDelete.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("btnDelete.Mask.AutoComplete")));
            this.btnDelete.Mask.BeepOnError = ((bool)(resources.GetObject("btnDelete.Mask.BeepOnError")));
            this.btnDelete.Mask.EditMask = resources.GetString("btnDelete.Mask.EditMask");
            this.btnDelete.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("btnDelete.Mask.IgnoreMaskBlank")));
            this.btnDelete.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("btnDelete.Mask.MaskType")));
            this.btnDelete.Mask.PlaceHolder = ((char)(resources.GetObject("btnDelete.Mask.PlaceHolder")));
            this.btnDelete.Mask.SaveLiteral = ((bool)(resources.GetObject("btnDelete.Mask.SaveLiteral")));
            this.btnDelete.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("btnDelete.Mask.ShowPlaceHolders")));
            this.btnDelete.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("btnDelete.Mask.UseMaskAsDisplayFormat")));
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnDelete.UseParentBackground = true;
            // 
            // rcmbPermission
            // 
            this.rcmbPermission.AccessibleDescription = null;
            this.rcmbPermission.AccessibleName = null;
            resources.ApplyResources(this.rcmbPermission, "rcmbPermission");
            this.rcmbPermission.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rcmbPermission.Buttons"))))});
            this.rcmbPermission.Name = "rcmbPermission";
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 2";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.bar1, "bar1");
            // 
            // UCConfigJobList
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.gcJobList);
            this.Name = "UCConfigJobList";
            ((System.ComponentModel.ISupportInitialize)(this.gcJobList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.JobBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvJobList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbPermission)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected ICP.Framework.ClientComponents.Controls.LWGridControl gcJobList;
        protected DevExpress.XtraGrid.Views.Grid.GridView gvJobList;
        private DevExpress.XtraGrid.Columns.GridColumn colOrganizationName;
        private DevExpress.XtraGrid.Columns.GridColumn colJobName;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbPermission;
        private DevExpress.XtraBars.Bar bar1;
        private System.Windows.Forms.BindingSource JobBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn DCDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDelete;
    }
}
