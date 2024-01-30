namespace ICP.Sys.UI.WorkSpaceView
{
    partial class WorkSpaceViewUserPart
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
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.gcList = new DevExpress.XtraGrid.GridControl();
            this.bsUsreList = new System.Windows.Forms.BindingSource(this.components);
            this.gvList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrganizationName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colJobName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.pnlSearch = new DevExpress.XtraEditors.PanelControl();
            this.btnAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.stxtJob = new ICP.Framework.ClientComponents.Controls.MiniFinderPopupContainerEdit();
            this.labJob = new DevExpress.XtraEditors.LabelControl();
            this.stxtOrganization = new ICP.Framework.ClientComponents.Controls.MiniFinderPopupContainerEdit();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.labName = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.labCode = new DevExpress.XtraEditors.LabelControl();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.pnlSelect = new DevExpress.XtraEditors.PanelControl();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.bsSelectList = new System.Windows.Forms.BindingSource(this.components);
            this.gvDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSelectCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSelectCName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsUsreList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSearch)).BeginInit();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtJob.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtOrganization.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSelect)).BeginInit();
            this.pnlSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSelectList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.gcList);
            this.pnlMain.Controls.Add(this.pnlSearch);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(697, 473);
            this.pnlMain.TabIndex = 0;
            // 
            // gcList
            // 
            this.gcList.DataSource = this.bsUsreList;
            this.gcList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcList.Location = new System.Drawing.Point(2, 31);
            this.gcList.MainView = this.gvList;
            this.gcList.Name = "gcList";
            this.gcList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit2});
            this.gcList.Size = new System.Drawing.Size(693, 440);
            this.gcList.TabIndex = 5;
            this.gcList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvList});
            // 
            // bsUsreList
            // 
            this.bsUsreList.DataSource = typeof(ICP.Sys.ServiceInterface.DataObjects.UserList);
            // 
            // gvList
            // 
            this.gvList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCode,
            this.colCName,
            this.colOrganizationName,
            this.colJobName});
            this.gvList.GridControl = this.gcList;
            this.gvList.Name = "gvList";
            this.gvList.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvList.OptionsBehavior.Editable = false;
            this.gvList.OptionsBehavior.ReadOnly = true;
            this.gvList.OptionsPrint.EnableAppearanceEvenRow = true;
            this.gvList.OptionsPrint.EnableAppearanceOddRow = true;
            this.gvList.OptionsSelection.MultiSelect = true;
            this.gvList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvList.OptionsView.ColumnAutoWidth = false;
            this.gvList.OptionsView.EnableAppearanceEvenRow = true;
            this.gvList.OptionsView.ShowGroupPanel = false;
            this.gvList.DoubleClick += new System.EventHandler(this.gvList_DoubleClick);
            // 
            // colCode
            // 
            this.colCode.Caption = "代码";
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 0;
            // 
            // colCName
            // 
            this.colCName.Caption = "姓名";
            this.colCName.FieldName = "CName";
            this.colCName.Name = "colCName";
            this.colCName.Visible = true;
            this.colCName.VisibleIndex = 1;
            // 
            // colOrganizationName
            // 
            this.colOrganizationName.Caption = "部门";
            this.colOrganizationName.FieldName = "OrganizationName";
            this.colOrganizationName.Name = "colOrganizationName";
            this.colOrganizationName.Visible = true;
            this.colOrganizationName.VisibleIndex = 2;
            this.colOrganizationName.Width = 500;
            // 
            // colJobName
            // 
            this.colJobName.Caption = "职位";
            this.colJobName.FieldName = "JobName";
            this.colJobName.Name = "colJobName";
            this.colJobName.Visible = true;
            this.colJobName.VisibleIndex = 3;
            this.colJobName.Width = 100;
            // 
            // repositoryItemButtonEdit2
            // 
            this.repositoryItemButtonEdit2.AutoHeight = false;
            this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.btnAll);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Controls.Add(this.stxtJob);
            this.pnlSearch.Controls.Add(this.labJob);
            this.pnlSearch.Controls.Add(this.stxtOrganization);
            this.pnlSearch.Controls.Add(this.labCompany);
            this.pnlSearch.Controls.Add(this.labName);
            this.pnlSearch.Controls.Add(this.txtName);
            this.pnlSearch.Controls.Add(this.labCode);
            this.pnlSearch.Controls.Add(this.txtCode);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(2, 2);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(693, 29);
            this.pnlSearch.TabIndex = 0;
            // 
            // btnAll
            // 
            this.btnAll.Location = new System.Drawing.Point(673, 3);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(63, 23);
            this.btnAll.TabIndex = 23;
            this.btnAll.Text = "全部-->";
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(543, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // stxtJob
            // 
            this.stxtJob.FinderDisplayMeber = null;
            this.stxtJob.FinderValueMember = null;
            this.stxtJob.Location = new System.Drawing.Point(433, 4);
            this.stxtJob.Name = "stxtJob";
            this.stxtJob.Properties.ActionButtonIndex = 1;
            this.stxtJob.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinDown)});
            this.stxtJob.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtJob.Properties.PopupFormSize = new System.Drawing.Size(500, 200);
            this.stxtJob.Properties.PopupSizeable = false;
            this.stxtJob.Properties.ShowPopupCloseButton = false;
            this.stxtJob.Size = new System.Drawing.Size(84, 21);
            this.stxtJob.TabIndex = 3;
            // 
            // labJob
            // 
            this.labJob.Location = new System.Drawing.Point(395, 7);
            this.labJob.Name = "labJob";
            this.labJob.Size = new System.Drawing.Size(24, 14);
            this.labJob.TabIndex = 22;
            this.labJob.Text = "职位";
            // 
            // stxtOrganization
            // 
            this.stxtOrganization.FinderDisplayMeber = null;
            this.stxtOrganization.FinderValueMember = null;
            this.stxtOrganization.Location = new System.Drawing.Point(284, 4);
            this.stxtOrganization.Name = "stxtOrganization";
            this.stxtOrganization.Properties.ActionButtonIndex = 1;
            this.stxtOrganization.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinDown)});
            this.stxtOrganization.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtOrganization.Properties.PopupFormSize = new System.Drawing.Size(500, 200);
            this.stxtOrganization.Properties.PopupSizeable = false;
            this.stxtOrganization.Properties.ShowPopupCloseButton = false;
            this.stxtOrganization.Size = new System.Drawing.Size(105, 21);
            this.stxtOrganization.TabIndex = 2;
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(246, 7);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(24, 14);
            this.labCompany.TabIndex = 20;
            this.labCompany.Text = "公司";
            // 
            // labName
            // 
            this.labName.Location = new System.Drawing.Point(126, 7);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(24, 14);
            this.labName.TabIndex = 18;
            this.labName.Text = "姓名";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(157, 4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(80, 21);
            this.txtName.TabIndex = 1;
            // 
            // labCode
            // 
            this.labCode.Location = new System.Drawing.Point(7, 7);
            this.labCode.Name = "labCode";
            this.labCode.Size = new System.Drawing.Size(24, 14);
            this.labCode.TabIndex = 16;
            this.labCode.Text = "代码";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(37, 4);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(83, 21);
            this.txtCode.TabIndex = 0;
            // 
            // pnlSelect
            // 
            this.pnlSelect.Controls.Add(this.gcMain);
            this.pnlSelect.Controls.Add(this.barDockControlLeft);
            this.pnlSelect.Controls.Add(this.barDockControlRight);
            this.pnlSelect.Controls.Add(this.barDockControlBottom);
            this.pnlSelect.Controls.Add(this.barDockControlTop);
            this.pnlSelect.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlSelect.Location = new System.Drawing.Point(703, 0);
            this.pnlSelect.Name = "pnlSelect";
            this.pnlSelect.Size = new System.Drawing.Size(249, 473);
            this.pnlSelect.TabIndex = 1;
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsSelectList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(2, 28);
            this.gcMain.MainView = this.gvDetails;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1});
            this.gcMain.Size = new System.Drawing.Size(245, 443);
            this.gcMain.TabIndex = 4;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDetails});
            // 
            // bsSelectList
            // 
            this.bsSelectList.DataSource = typeof(ICP.Sys.ServiceInterface.DataObjects.UserList);
            // 
            // gvDetails
            // 
            this.gvDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSelectCode,
            this.colSelectCName});
            this.gvDetails.GridControl = this.gcMain;
            this.gvDetails.Name = "gvDetails";
            this.gvDetails.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvDetails.OptionsBehavior.Editable = false;
            this.gvDetails.OptionsBehavior.ReadOnly = true;
            this.gvDetails.OptionsPrint.EnableAppearanceEvenRow = true;
            this.gvDetails.OptionsPrint.EnableAppearanceOddRow = true;
            this.gvDetails.OptionsSelection.MultiSelect = true;
            this.gvDetails.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvDetails.OptionsView.ColumnAutoWidth = false;
            this.gvDetails.OptionsView.EnableAppearanceEvenRow = true;
            this.gvDetails.OptionsView.ShowGroupPanel = false;
            this.gvDetails.DoubleClick += new System.EventHandler(this.gvDetails_DoubleClick);
            // 
            // colSelectCode
            // 
            this.colSelectCode.Caption = "代码";
            this.colSelectCode.FieldName = "Code";
            this.colSelectCode.Name = "colSelectCode";
            this.colSelectCode.Visible = true;
            this.colSelectCode.VisibleIndex = 0;
            this.colSelectCode.Width = 105;
            // 
            // colSelectCName
            // 
            this.colSelectCName.Caption = "姓名";
            this.colSelectCName.FieldName = "CName";
            this.colSelectCName.Name = "colSelectCName";
            this.colSelectCName.Visible = true;
            this.colSelectCName.VisibleIndex = 1;
            this.colSelectCName.Width = 118;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(2, 28);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 443);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(247, 28);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 443);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(2, 471);
            this.barDockControlBottom.Size = new System.Drawing.Size(245, 0);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(2, 2);
            this.barDockControlTop.Size = new System.Drawing.Size(245, 26);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitterControl1.Location = new System.Drawing.Point(697, 0);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(6, 473);
            this.splitterControl1.TabIndex = 2;
            this.splitterControl1.TabStop = false;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this.pnlSelect;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSave,
            this.barDelete});
            this.barManager1.MaxItemId = 2;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDelete, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.Text = "Tools";
            // 
            // barSave
            // 
            this.barSave.Caption = "保存(&S)";
            this.barSave.Glyph = global::ICP.Sys.UI.Properties.Resources.Save_Blue_16;
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barDelete
            // 
            this.barDelete.Caption = "删除(&D)";
            this.barDelete.Glyph = global::ICP.Sys.UI.Properties.Resources.Delete_16;
            this.barDelete.Id = 1;
            this.barDelete.Name = "barDelete";
            this.barDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // WorkSpaceViewUserPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.pnlSelect);
            this.Name = "WorkSpaceViewUserPart";
            this.Size = new System.Drawing.Size(952, 473);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsUsreList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSearch)).EndInit();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtJob.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtOrganization.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSelect)).EndInit();
            this.pnlSelect.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSelectList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.PanelControl pnlSelect;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetails;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraEditors.PanelControl pnlSearch;
        private DevExpress.XtraGrid.GridControl gcList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvList;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit2;
        private DevExpress.XtraEditors.LabelControl labCode;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.LabelControl labName;
        private DevExpress.XtraEditors.TextEdit txtName;
        private Framework.ClientComponents.Controls.MiniFinderPopupContainerEdit stxtOrganization;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private Framework.ClientComponents.Controls.MiniFinderPopupContainerEdit stxtJob;
        private DevExpress.XtraEditors.LabelControl labJob;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private System.Windows.Forms.BindingSource bsUsreList;
        private System.Windows.Forms.BindingSource bsSelectList;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colCName;
        private DevExpress.XtraGrid.Columns.GridColumn colOrganizationName;
        private DevExpress.XtraGrid.Columns.GridColumn colJobName;
        private DevExpress.XtraGrid.Columns.GridColumn colSelectCode;
        private DevExpress.XtraGrid.Columns.GridColumn colSelectCName;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private DevExpress.XtraEditors.SimpleButton btnAll;
    }
}
