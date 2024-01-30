using ICP.Framework.ClientComponents.Controls;
namespace ICP.FRM.UI.OceanPrice
{
    partial class OPPermissionsPart
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
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.labPermissionMode = new DevExpress.XtraEditors.LabelControl();
            this.cmbPermissionMode = new LWImageComboBoxEdit();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.panelOrgJob = new System.Windows.Forms.Panel();
            this.gcOrgJob = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsOrgJob = new System.Windows.Forms.BindingSource(this.components);
            this.gvOrgJob = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colOrganizationName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colJobName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrgJobPermission = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbOrgJobPermission = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barRemoveOrgJob = new DevExpress.XtraBars.BarButtonItem();
            this.standaloneBarDockControl1 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barRemoveUser = new DevExpress.XtraBars.BarButtonItem();
            this.standaloneBarDockControl2 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.panelUser = new System.Windows.Forms.Panel();
            this.gcUser = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsUser = new System.Windows.Forms.BindingSource(this.components);
            this.gvUser = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserPermission = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbUserPermission = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.barPublish = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPermissionMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.panelOrgJob.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcOrgJob)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsOrgJob)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrgJob)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbOrgJobPermission)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.panelUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbUserPermission)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labPermissionMode);
            this.panel1.Controls.Add(this.cmbPermissionMode);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(930, 32);
            this.panel1.TabIndex = 41;
            // 
            // labPermissionMode
            // 
            this.labPermissionMode.Location = new System.Drawing.Point(15, 8);
            this.labPermissionMode.Name = "labPermissionMode";
            this.labPermissionMode.Size = new System.Drawing.Size(30, 14);
            this.labPermissionMode.TabIndex = 10;
            this.labPermissionMode.Text = "Mode";
            // 
            // cmbPermissionMode
            // 
            this.cmbPermissionMode.Location = new System.Drawing.Point(76, 5);
            this.cmbPermissionMode.Name = "cmbPermissionMode";
            this.cmbPermissionMode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbPermissionMode.Properties.Appearance.Options.UseBackColor = true;
            this.cmbPermissionMode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPermissionMode.Size = new System.Drawing.Size(100, 21);
            this.cmbPermissionMode.TabIndex = 11;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 58);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.panelOrgJob);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.panelUser);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(930, 428);
            this.splitContainerControl1.SplitterPosition = 466;
            this.splitContainerControl1.TabIndex = 42;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // panelOrgJob
            // 
            this.panelOrgJob.Controls.Add(this.gcOrgJob);
            this.panelOrgJob.Controls.Add(this.standaloneBarDockControl1);
            this.panelOrgJob.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOrgJob.Location = new System.Drawing.Point(0, 0);
            this.panelOrgJob.Name = "panelOrgJob";
            this.panelOrgJob.Size = new System.Drawing.Size(466, 428);
            this.panelOrgJob.TabIndex = 6;
            // 
            // gcOrgJob
            // 
            this.gcOrgJob.DataSource = this.bsOrgJob;
            this.gcOrgJob.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcOrgJob.Location = new System.Drawing.Point(0, 27);
            this.gcOrgJob.MainView = this.gvOrgJob;
            this.gcOrgJob.MenuManager = this.barManager1;
            this.gcOrgJob.Name = "gcOrgJob";
            this.gcOrgJob.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbOrgJobPermission});
            this.gcOrgJob.Size = new System.Drawing.Size(466, 401);
            this.gcOrgJob.TabIndex = 5;
            this.gcOrgJob.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvOrgJob});
            // 
            // bsOrgJob
            // 
            this.bsOrgJob.DataSource = typeof(ICP.FRM.ServiceInterface.DataObjects.OceanPermissionList);
            this.bsOrgJob.PositionChanged += new System.EventHandler(this.bsOrgJob_PositionChanged);
            // 
            // gvOrgJob
            // 
            this.gvOrgJob.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colOrganizationName,
            this.colJobName,
            this.colOrgJobPermission});
            this.gvOrgJob.GridControl = this.gcOrgJob;
            this.gvOrgJob.Name = "gvOrgJob";
            this.gvOrgJob.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvOrgJob.OptionsSelection.MultiSelect = true;
            this.gvOrgJob.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvOrgJob.OptionsView.EnableAppearanceEvenRow = true;
            this.gvOrgJob.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gvOrgJob.OptionsView.ShowGroupPanel = false;
            this.gvOrgJob.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvOrgJob_InitNewRow);
            this.gvOrgJob.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvOrgJob_RowStyle);
            // 
            // colOrganizationName
            // 
            this.colOrganizationName.Caption = "Organization";
            this.colOrganizationName.FieldName = "OrganizationName";
            this.colOrganizationName.Name = "colOrganizationName";
            this.colOrganizationName.Visible = true;
            this.colOrganizationName.VisibleIndex = 0;
            // 
            // colJobName
            // 
            this.colJobName.Caption = "Job";
            this.colJobName.FieldName = "JobName";
            this.colJobName.Name = "colJobName";
            this.colJobName.Visible = true;
            this.colJobName.VisibleIndex = 1;
            // 
            // colOrgJobPermission
            // 
            this.colOrgJobPermission.ColumnEdit = this.rcmbOrgJobPermission;
            this.colOrgJobPermission.FieldName = "Permission";
            this.colOrgJobPermission.Name = "colOrgJobPermission";
            this.colOrgJobPermission.Visible = true;
            this.colOrgJobPermission.VisibleIndex = 2;
            // 
            // rcmbOrgJobPermission
            // 
            this.rcmbOrgJobPermission.AutoHeight = false;
            this.rcmbOrgJobPermission.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbOrgJobPermission.Name = "rcmbOrgJobPermission";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2,
            this.bar1,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl1);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl2);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSave,
            this.barRemoveUser,
            this.barRemoveOrgJob,
            this.barPublish});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 4;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPublish, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barSave
            // 
            this.barSave.Caption = "&Save";
            this.barSave.Glyph = global::ICP.FRM.UI.Properties.Resources.Save_16;
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 3";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar1.FloatLocation = new System.Drawing.Point(78, 216);
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRemoveOrgJob, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.StandaloneBarDockControl = this.standaloneBarDockControl1;
            this.bar1.Text = "Custom 3";
            // 
            // barRemoveOrgJob
            // 
            this.barRemoveOrgJob.Caption = "&Remove";
            this.barRemoveOrgJob.Glyph = global::ICP.FRM.UI.Properties.Resources.Delete_16;
            this.barRemoveOrgJob.Id = 2;
            this.barRemoveOrgJob.Name = "barRemoveOrgJob";
            this.barRemoveOrgJob.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRemoveOrgJob_ItemClick);
            // 
            // standaloneBarDockControl1
            // 
            this.standaloneBarDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(0, 0);
            this.standaloneBarDockControl1.Name = "standaloneBarDockControl1";
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(466, 27);
            this.standaloneBarDockControl1.Text = "standaloneBarDockControl1";
            // 
            // bar3
            // 
            this.bar3.BarName = "Custom 4";
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar3.FloatLocation = new System.Drawing.Point(567, 218);
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRemoveUser, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.StandaloneBarDockControl = this.standaloneBarDockControl2;
            this.bar3.Text = "Custom 4";
            // 
            // barRemoveUser
            // 
            this.barRemoveUser.Caption = "&Remove";
            this.barRemoveUser.Glyph = global::ICP.FRM.UI.Properties.Resources.Delete_16;
            this.barRemoveUser.Id = 1;
            this.barRemoveUser.Name = "barRemoveUser";
            this.barRemoveUser.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRemoveUser_ItemClick);
            // 
            // standaloneBarDockControl2
            // 
            this.standaloneBarDockControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl2.Location = new System.Drawing.Point(0, 0);
            this.standaloneBarDockControl2.Name = "standaloneBarDockControl2";
            this.standaloneBarDockControl2.Size = new System.Drawing.Size(458, 26);
            this.standaloneBarDockControl2.Text = "standaloneBarDockControl2";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(930, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 486);
            this.barDockControlBottom.Size = new System.Drawing.Size(930, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 460);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(930, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 460);
            // 
            // panelUser
            // 
            this.panelUser.Controls.Add(this.gcUser);
            this.panelUser.Controls.Add(this.standaloneBarDockControl2);
            this.panelUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUser.Location = new System.Drawing.Point(0, 0);
            this.panelUser.Name = "panelUser";
            this.panelUser.Size = new System.Drawing.Size(458, 428);
            this.panelUser.TabIndex = 7;
            // 
            // gcUser
            // 
            this.gcUser.DataSource = this.bsUser;
            this.gcUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcUser.Location = new System.Drawing.Point(0, 26);
            this.gcUser.MainView = this.gvUser;
            this.gcUser.MenuManager = this.barManager1;
            this.gcUser.Name = "gcUser";
            this.gcUser.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbUserPermission});
            this.gcUser.Size = new System.Drawing.Size(458, 402);
            this.gcUser.TabIndex = 5;
            this.gcUser.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUser});
            // 
            // bsUser
            // 
            this.bsUser.DataSource = typeof(ICP.FRM.ServiceInterface.DataObjects.OceanPermissionList);
            this.bsUser.PositionChanged += new System.EventHandler(this.bsUser_PositionChanged);
            // 
            // gvUser
            // 
            this.gvUser.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUserName,
            this.colUserPermission});
            this.gvUser.GridControl = this.gcUser;
            this.gvUser.Name = "gvUser";
            this.gvUser.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvUser.OptionsSelection.MultiSelect = true;
            this.gvUser.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvUser.OptionsView.EnableAppearanceEvenRow = true;
            this.gvUser.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gvUser.OptionsView.ShowGroupPanel = false;
            this.gvUser.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvUser_InitNewRow);
            this.gvUser.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvUser_RowStyle);
            // 
            // colUserName
            // 
            this.colUserName.FieldName = "UserName";
            this.colUserName.Name = "colUserName";
            this.colUserName.Visible = true;
            this.colUserName.VisibleIndex = 0;
            // 
            // colUserPermission
            // 
            this.colUserPermission.ColumnEdit = this.rcmbUserPermission;
            this.colUserPermission.FieldName = "Permission";
            this.colUserPermission.Name = "colUserPermission";
            this.colUserPermission.Visible = true;
            this.colUserPermission.VisibleIndex = 1;
            // 
            // rcmbUserPermission
            // 
            this.rcmbUserPermission.AutoHeight = false;
            this.rcmbUserPermission.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbUserPermission.Name = "rcmbUserPermission";
            // 
            // barPublish
            // 
            this.barPublish.Caption = "&Publish";
            this.barPublish.Glyph = global::ICP.FRM.UI.Properties.Resources.Assign_16;
            this.barPublish.Id = 3;
            this.barPublish.Name = "barPublish";
            this.barPublish.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPublish_ItemClick);
            // 
            // OPPermissionsPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "OPPermissionsPart";
            this.IsMultiLanguage = false;
            this.Size = new System.Drawing.Size(930, 486);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPermissionMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.panelOrgJob.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcOrgJob)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsOrgJob)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrgJob)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbOrgJobPermission)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.panelUser.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbUserPermission)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.LabelControl labPermissionMode;
        private LWImageComboBoxEdit cmbPermissionMode;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.Panel panelOrgJob;
        protected ICP.Framework.ClientComponents.Controls.LWGridControl gcOrgJob;
        protected System.Windows.Forms.BindingSource bsOrgJob;
        protected DevExpress.XtraGrid.Views.Grid.GridView gvOrgJob;
        private DevExpress.XtraGrid.Columns.GridColumn colOrganizationName;
        private DevExpress.XtraGrid.Columns.GridColumn colJobName;
        private DevExpress.XtraGrid.Columns.GridColumn colOrgJobPermission;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbOrgJobPermission;
        private System.Windows.Forms.Panel panelUser;
        protected ICP.Framework.ClientComponents.Controls.LWGridControl gcUser;
        protected System.Windows.Forms.BindingSource bsUser;
        protected DevExpress.XtraGrid.Views.Grid.GridView gvUser;
        private DevExpress.XtraGrid.Columns.GridColumn colUserName;
        private DevExpress.XtraGrid.Columns.GridColumn colUserPermission;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbUserPermission;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barRemoveOrgJob;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem barRemoveUser;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl2;
        private DevExpress.XtraBars.BarButtonItem barPublish;
    }
}
