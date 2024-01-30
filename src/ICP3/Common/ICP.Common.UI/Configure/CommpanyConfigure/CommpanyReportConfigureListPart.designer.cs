namespace ICP.Common.UI.Configure.CommpanyConfigure
{
    partial class CommpanyReportConfigureListPart
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommpanyReportConfigureListPart));
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIsSelected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colCDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.chkbtnHold = new DevExpress.XtraEditors.CheckButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.chkbtnUnChecked = new DevExpress.XtraEditors.CheckButton();
            this.chkbtnChecked = new DevExpress.XtraEditors.CheckButton();
            this.chkbtnAll = new DevExpress.XtraEditors.CheckButton();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.txtFind = new DevExpress.XtraEditors.ButtonEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barSet = new DevExpress.XtraBars.BarButtonItem();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barSelectAll = new DevExpress.XtraBars.BarButtonItem();
            this.barUnSelectAll = new DevExpress.XtraBars.BarButtonItem();
            this.barAntiSelect = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFind.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.Common.UI.Configure.CommpanyConfigure.CompanyReportConfigureListClient);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 57);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkEdit1,
            this.repositoryItemCheckEdit1});
            this.gcMain.Size = new System.Drawing.Size(448, 317);
            this.gcMain.TabIndex = 1;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            this.gcMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gcMain_MouseDown);
            // 
            // gvMain
            // 
            this.gvMain.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.gvMain.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.gvMain.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvMain.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIsSelected,
            this.colCDescription,
            this.colEDescription,
            this.colCode,
            this.colType,
            this.colCreateByName,
            this.colCreateDate});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.IndicatorWidth = 35;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowDetailButtons = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.mainGridView_CustomDrawRowIndicator);
            this.gvMain.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvMain_CellValueChanged);
            // 
            // colIsSelected
            // 
            this.colIsSelected.Caption = "Select";
            this.colIsSelected.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colIsSelected.FieldName = "IsSelected";
            this.colIsSelected.Name = "colIsSelected";
            this.colIsSelected.Visible = true;
            this.colIsSelected.VisibleIndex = 0;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // colCDescription
            // 
            this.colCDescription.Caption = "报表中文描述";
            this.colCDescription.FieldName = "CDescription";
            this.colCDescription.Name = "colCDescription";
            this.colCDescription.OptionsColumn.AllowEdit = false;
            this.colCDescription.Visible = true;
            this.colCDescription.VisibleIndex = 2;
            // 
            // colEDescription
            // 
            this.colEDescription.Caption = "报表英文描述";
            this.colEDescription.FieldName = "EDescription";
            this.colEDescription.Name = "colEDescription";
            this.colEDescription.OptionsColumn.AllowEdit = false;
            this.colEDescription.Visible = true;
            this.colEDescription.VisibleIndex = 3;
            // 
            // colCode
            // 
            this.colCode.Caption = "代码";
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.OptionsColumn.AllowEdit = false;
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 1;
            // 
            // colType
            // 
            this.colType.Caption = "类型";
            this.colType.FieldName = "ReportTypeName";
            this.colType.Name = "colType";
            this.colType.OptionsColumn.AllowEdit = false;
            this.colType.Visible = true;
            this.colType.VisibleIndex = 4;
            // 
            // colCreateByName
            // 
            this.colCreateByName.Caption = "创建人";
            this.colCreateByName.FieldName = "CreateByName";
            this.colCreateByName.Name = "colCreateByName";
            this.colCreateByName.OptionsColumn.AllowEdit = false;
            this.colCreateByName.Visible = true;
            this.colCreateByName.VisibleIndex = 5;
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "创建时间";
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.OptionsColumn.AllowEdit = false;
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 6;
            // 
            // rchkEdit1
            // 
            this.rchkEdit1.AutoHeight = false;
            this.rchkEdit1.Name = "rchkEdit1";
            this.rchkEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.chkbtnHold);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Controls.Add(this.btnNext);
            this.panelControl1.Controls.Add(this.txtFind);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 24);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(448, 33);
            this.panelControl1.TabIndex = 6;
            // 
            // chkbtnHold
            // 
            this.chkbtnHold.Image = ((System.Drawing.Image)(resources.GetObject("chkbtnHold.Image")));
            this.chkbtnHold.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.chkbtnHold.Location = new System.Drawing.Point(5, 6);
            this.chkbtnHold.Name = "chkbtnHold";
            this.chkbtnHold.Size = new System.Drawing.Size(21, 21);
            this.chkbtnHold.TabIndex = 10;
            this.chkbtnHold.ToolTip = "Hold";
            this.chkbtnHold.Click += new System.EventHandler(this.chkbtnHold_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.chkbtnUnChecked);
            this.panelControl2.Controls.Add(this.chkbtnChecked);
            this.panelControl2.Controls.Add(this.chkbtnAll);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl2.Location = new System.Drawing.Point(358, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(88, 29);
            this.panelControl2.TabIndex = 9;
            // 
            // chkbtnUnChecked
            // 
            this.chkbtnUnChecked.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkbtnUnChecked.Image = ((System.Drawing.Image)(resources.GetObject("chkbtnUnChecked.Image")));
            this.chkbtnUnChecked.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.chkbtnUnChecked.Location = new System.Drawing.Point(58, 2);
            this.chkbtnUnChecked.Name = "chkbtnUnChecked";
            this.chkbtnUnChecked.Size = new System.Drawing.Size(28, 25);
            this.chkbtnUnChecked.TabIndex = 8;
            this.chkbtnUnChecked.ToolTip = "UnSelected";
            this.chkbtnUnChecked.Click += new System.EventHandler(this.chkbtnUnChecked_Click);
            // 
            // chkbtnChecked
            // 
            this.chkbtnChecked.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkbtnChecked.Image = ((System.Drawing.Image)(resources.GetObject("chkbtnChecked.Image")));
            this.chkbtnChecked.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.chkbtnChecked.Location = new System.Drawing.Point(30, 2);
            this.chkbtnChecked.Name = "chkbtnChecked";
            this.chkbtnChecked.Size = new System.Drawing.Size(28, 25);
            this.chkbtnChecked.TabIndex = 8;
            this.chkbtnChecked.ToolTip = "Selected";
            this.chkbtnChecked.Click += new System.EventHandler(this.chkbtnChecked_Click);
            // 
            // chkbtnAll
            // 
            this.chkbtnAll.Checked = true;
            this.chkbtnAll.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkbtnAll.Image = ((System.Drawing.Image)(resources.GetObject("chkbtnAll.Image")));
            this.chkbtnAll.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.chkbtnAll.Location = new System.Drawing.Point(2, 2);
            this.chkbtnAll.Name = "chkbtnAll";
            this.chkbtnAll.Size = new System.Drawing.Size(28, 25);
            this.chkbtnAll.TabIndex = 8;
            this.chkbtnAll.ToolTip = "All";
            this.chkbtnAll.Click += new System.EventHandler(this.chkbtnAll_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Location = new System.Drawing.Point(284, 5);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(68, 21);
            this.btnNext.TabIndex = 6;
            this.btnNext.Text = "&Next";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // txtFind
            // 
            this.txtFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFind.Location = new System.Drawing.Point(32, 5);
            this.txtFind.Name = "txtFind";
            this.txtFind.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.txtFind.Size = new System.Drawing.Size(246, 21);
            this.txtFind.TabIndex = 0;
            this.txtFind.TabStop = false;
            this.txtFind.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtFind_ButtonClick);
            this.txtFind.TextChanged += new System.EventHandler(this.txtFind_TextChanged);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSelectAll,
            this.barUnSelectAll,
            this.barAntiSelect,
            this.barSet,
            this.barSave});
            this.barManager1.MaxItemId = 7;
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 2";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSet),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSave)});
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Custom 2";
            // 
            // barSet
            // 
            this.barSet.Caption = "设置报表参数值";
            this.barSet.Id = 5;
            this.barSet.Name = "barSet";
            this.barSet.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barSet.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSet_ItemClick);
            // 
            // barSave
            // 
            this.barSave.Caption = "保存(&S)";
            this.barSave.Id = 6;
            this.barSave.Name = "barSave";
            this.barSave.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(448, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 374);
            this.barDockControlBottom.Size = new System.Drawing.Size(448, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 350);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(448, 24);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 350);
            // 
            // barSelectAll
            // 
            this.barSelectAll.Caption = "SelectAll";
            this.barSelectAll.Id = 0;
            this.barSelectAll.Name = "barSelectAll";
            this.barSelectAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSelectAll_ItemClick);
            // 
            // barUnSelectAll
            // 
            this.barUnSelectAll.Caption = "UnSelectAll";
            this.barUnSelectAll.Id = 1;
            this.barUnSelectAll.Name = "barUnSelectAll";
            this.barUnSelectAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barUnSelectAll_ItemClick);
            // 
            // barAntiSelect
            // 
            this.barAntiSelect.Caption = "AntiSelect";
            this.barAntiSelect.Id = 2;
            this.barAntiSelect.Name = "barAntiSelect";
            this.barAntiSelect.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAntiSelect_ItemClick);
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSelectAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.barUnSelectAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.barAntiSelect)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // CommpanyReportConfigureListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "CommpanyReportConfigureListPart";
            this.Size = new System.Drawing.Size(448, 374);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFind.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);

        }      

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkEdit1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.CheckButton chkbtnUnChecked;
        private DevExpress.XtraEditors.CheckButton chkbtnChecked;
        private DevExpress.XtraEditors.CheckButton chkbtnAll;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarButtonItem barSelectAll;
        private DevExpress.XtraBars.BarButtonItem barUnSelectAll;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barAntiSelect;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraEditors.ButtonEdit txtFind;
        private DevExpress.XtraEditors.CheckButton chkbtnHold;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraGrid.Columns.GridColumn colIsSelected;
        private DevExpress.XtraGrid.Columns.GridColumn colCDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colEDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraBars.BarButtonItem barSet;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
    }
}
