namespace ICP.Sys.UI.WorkSpaceView
{
    partial class WorkSpaceViewOpViewPart
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
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.pnlOperationViews = new DevExpress.XtraEditors.GroupControl();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gvDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIsCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkCheck = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colShowIndex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperationType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbOperationType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colUpdateBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.pnlEdit = new DevExpress.XtraEditors.GroupControl();
            this.labEName = new DevExpress.XtraEditors.LabelControl();
            this.txtEName = new DevExpress.XtraEditors.TextEdit();
            this.labCName = new DevExpress.XtraEditors.LabelControl();
            this.txtCName = new DevExpress.XtraEditors.TextEdit();
            this.labCode = new DevExpress.XtraEditors.LabelControl();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barManager2 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barNew = new DevExpress.XtraBars.BarButtonItem();
            this.barSaves = new DevExpress.XtraBars.BarButtonItem();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlOperationViews)).BeginInit();
            this.pnlOperationViews.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOperationType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlEdit)).BeginInit();
            this.pnlEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this.pnlOperationViews;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barAdd,
            this.barEdit,
            this.barSave});
            this.barManager1.MaxItemId = 3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAdd, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.Text = "Tools";
            // 
            // barAdd
            // 
            this.barAdd.Caption = "新增(&A)";
            this.barAdd.Glyph = global::ICP.Sys.UI.Properties.Resources.Add_File_16;
            this.barAdd.Id = 0;
            this.barAdd.Name = "barAdd";
            this.barAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdd_ItemClick);
            // 
            // barEdit
            // 
            this.barEdit.Caption = "编辑(&E)";
            this.barEdit.Glyph = global::ICP.Sys.UI.Properties.Resources.BillDetails;
            this.barEdit.Id = 1;
            this.barEdit.Name = "barEdit";
            this.barEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barEdit_ItemClick);
            // 
            // barSave
            // 
            this.barSave.Caption = "保存(&S)";
            this.barSave.Glyph = global::ICP.Sys.UI.Properties.Resources.Save_Blue_16;
            this.barSave.Id = 2;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(2, 23);
            this.barDockControlTop.Size = new System.Drawing.Size(635, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(2, 407);
            this.barDockControlBottom.Size = new System.Drawing.Size(635, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(2, 49);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 358);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(637, 49);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 358);
            // 
            // pnlOperationViews
            // 
            this.pnlOperationViews.Controls.Add(this.gcMain);
            this.pnlOperationViews.Controls.Add(this.barDockControlLeft);
            this.pnlOperationViews.Controls.Add(this.barDockControlRight);
            this.pnlOperationViews.Controls.Add(this.barDockControlBottom);
            this.pnlOperationViews.Controls.Add(this.barDockControlTop);
            this.pnlOperationViews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOperationViews.Location = new System.Drawing.Point(225, 0);
            this.pnlOperationViews.Name = "pnlOperationViews";
            this.pnlOperationViews.Size = new System.Drawing.Size(639, 409);
            this.pnlOperationViews.TabIndex = 10;
            this.pnlOperationViews.Text = "OperationViews";
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(2, 49);
            this.gcMain.MainView = this.gvDetails;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1,
            this.chkCheck,
            this.cmbOperationType});
            this.gcMain.Size = new System.Drawing.Size(635, 358);
            this.gcMain.TabIndex = 4;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDetails});
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.Sys.ServiceInterface.DataObjects.OperationViewList);
            // 
            // gvDetails
            // 
            this.gvDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIsCheck,
            this.colShowIndex,
            this.colCode,
            this.colCName,
            this.colEName,
            this.colOperationType,
            this.colUpdateBy,
            this.colUpdateDate});
            this.gvDetails.GridControl = this.gcMain;
            this.gvDetails.Name = "gvDetails";
            this.gvDetails.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvDetails.OptionsPrint.EnableAppearanceEvenRow = true;
            this.gvDetails.OptionsPrint.EnableAppearanceOddRow = true;
            this.gvDetails.OptionsSelection.MultiSelect = true;
            this.gvDetails.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvDetails.OptionsView.ColumnAutoWidth = false;
            this.gvDetails.OptionsView.EnableAppearanceEvenRow = true;
            this.gvDetails.OptionsView.ShowGroupPanel = false;
            this.gvDetails.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvDetails_RowCellClick);
            this.gvDetails.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvDetails_RowCellStyle);
            this.gvDetails.DoubleClick += new System.EventHandler(this.gvDetails_DoubleClick);
            // 
            // colIsCheck
            // 
            this.colIsCheck.Caption = "选择";
            this.colIsCheck.ColumnEdit = this.chkCheck;
            this.colIsCheck.FieldName = "IsCheck";
            this.colIsCheck.Name = "colIsCheck";
            this.colIsCheck.OptionsColumn.AllowEdit = false;
            this.colIsCheck.Visible = true;
            this.colIsCheck.VisibleIndex = 0;
            this.colIsCheck.Width = 57;
            // 
            // chkCheck
            // 
            this.chkCheck.AutoHeight = false;
            this.chkCheck.Name = "chkCheck";
            // 
            // colShowIndex
            // 
            this.colShowIndex.Caption = "顺序";
            this.colShowIndex.FieldName = "ShowIndex";
            this.colShowIndex.Name = "colShowIndex";
            this.colShowIndex.Visible = true;
            this.colShowIndex.VisibleIndex = 1;
            // 
            // colCode
            // 
            this.colCode.Caption = "代码";
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.OptionsColumn.AllowEdit = false;
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 2;
            this.colCode.Width = 356;
            // 
            // colCName
            // 
            this.colCName.Caption = "中文名";
            this.colCName.FieldName = "CName";
            this.colCName.Name = "colCName";
            this.colCName.OptionsColumn.AllowEdit = false;
            this.colCName.Visible = true;
            this.colCName.VisibleIndex = 3;
            this.colCName.Width = 146;
            // 
            // colEName
            // 
            this.colEName.Caption = "英文名";
            this.colEName.FieldName = "EName";
            this.colEName.Name = "colEName";
            this.colEName.OptionsColumn.AllowEdit = false;
            this.colEName.Visible = true;
            this.colEName.VisibleIndex = 4;
            this.colEName.Width = 126;
            // 
            // colOperationType
            // 
            this.colOperationType.Caption = "业务类型";
            this.colOperationType.ColumnEdit = this.cmbOperationType;
            this.colOperationType.FieldName = "OperationType";
            this.colOperationType.Name = "colOperationType";
            this.colOperationType.OptionsColumn.AllowEdit = false;
            this.colOperationType.Visible = true;
            this.colOperationType.VisibleIndex = 5;
            this.colOperationType.Width = 111;
            // 
            // cmbOperationType
            // 
            this.cmbOperationType.AutoHeight = false;
            this.cmbOperationType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cmbOperationType.Name = "cmbOperationType";
            // 
            // colUpdateBy
            // 
            this.colUpdateBy.Caption = "更新人";
            this.colUpdateBy.FieldName = "UpdateBy";
            this.colUpdateBy.Name = "colUpdateBy";
            this.colUpdateBy.OptionsColumn.AllowEdit = false;
            this.colUpdateBy.Visible = true;
            this.colUpdateBy.VisibleIndex = 6;
            // 
            // colUpdateDate
            // 
            this.colUpdateDate.Caption = "更新时间";
            this.colUpdateDate.FieldName = "UpdateDate";
            this.colUpdateDate.Name = "colUpdateDate";
            this.colUpdateDate.OptionsColumn.AllowEdit = false;
            this.colUpdateDate.Visible = true;
            this.colUpdateDate.VisibleIndex = 7;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // pnlEdit
            // 
            this.pnlEdit.Controls.Add(this.labEName);
            this.pnlEdit.Controls.Add(this.txtEName);
            this.pnlEdit.Controls.Add(this.labCName);
            this.pnlEdit.Controls.Add(this.txtCName);
            this.pnlEdit.Controls.Add(this.labCode);
            this.pnlEdit.Controls.Add(this.txtCode);
            this.pnlEdit.Controls.Add(this.barDockControl3);
            this.pnlEdit.Controls.Add(this.barDockControl4);
            this.pnlEdit.Controls.Add(this.barDockControl2);
            this.pnlEdit.Controls.Add(this.barDockControl1);
            this.pnlEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlEdit.Location = new System.Drawing.Point(0, 0);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new System.Drawing.Size(219, 409);
            this.pnlEdit.TabIndex = 9;
            this.pnlEdit.Text = "编辑";
            // 
            // labEName
            // 
            this.labEName.Location = new System.Drawing.Point(10, 114);
            this.labEName.Name = "labEName";
            this.labEName.Size = new System.Drawing.Size(36, 14);
            this.labEName.TabIndex = 30;
            this.labEName.Text = "英文名";
            // 
            // txtEName
            // 
            this.txtEName.Location = new System.Drawing.Point(55, 111);
            this.txtEName.Name = "txtEName";
            this.txtEName.Size = new System.Drawing.Size(146, 21);
            this.txtEName.TabIndex = 2;
            // 
            // labCName
            // 
            this.labCName.Location = new System.Drawing.Point(10, 87);
            this.labCName.Name = "labCName";
            this.labCName.Size = new System.Drawing.Size(36, 14);
            this.labCName.TabIndex = 28;
            this.labCName.Text = "中文名";
            // 
            // txtCName
            // 
            this.txtCName.Location = new System.Drawing.Point(55, 84);
            this.txtCName.Name = "txtCName";
            this.txtCName.Size = new System.Drawing.Size(146, 21);
            this.txtCName.TabIndex = 1;
            // 
            // labCode
            // 
            this.labCode.Location = new System.Drawing.Point(10, 58);
            this.labCode.Name = "labCode";
            this.labCode.Size = new System.Drawing.Size(24, 14);
            this.labCode.TabIndex = 27;
            this.labCode.Text = "代码";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(55, 55);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(146, 21);
            this.txtCode.TabIndex = 0;
            // 
            // barDockControl3
            // 
            this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl3.Location = new System.Drawing.Point(2, 49);
            this.barDockControl3.Size = new System.Drawing.Size(0, 358);
            // 
            // barDockControl4
            // 
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl4.Location = new System.Drawing.Point(217, 49);
            this.barDockControl4.Size = new System.Drawing.Size(0, 358);
            // 
            // barDockControl2
            // 
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl2.Location = new System.Drawing.Point(2, 407);
            this.barDockControl2.Size = new System.Drawing.Size(215, 0);
            // 
            // barDockControl1
            // 
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(2, 23);
            this.barDockControl1.Size = new System.Drawing.Size(215, 26);
            // 
            // barManager2
            // 
            this.barManager2.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager2.DockControls.Add(this.barDockControl1);
            this.barManager2.DockControls.Add(this.barDockControl2);
            this.barManager2.DockControls.Add(this.barDockControl3);
            this.barManager2.DockControls.Add(this.barDockControl4);
            this.barManager2.Form = this.pnlEdit;
            this.barManager2.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barNew,
            this.barSaves});
            this.barManager2.MaxItemId = 2;
            // 
            // bar2
            // 
            this.bar2.BarName = "Tools";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barNew, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSaves, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.Text = "Tools";
            // 
            // barNew
            // 
            this.barNew.Caption = "新增(&A)";
            this.barNew.Glyph = global::ICP.Sys.UI.Properties.Resources.Add_File_16;
            this.barNew.Id = 0;
            this.barNew.Name = "barNew";
            this.barNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barNew_ItemClick);
            // 
            // barSaves
            // 
            this.barSaves.Caption = "保存(&S)";
            this.barSaves.Glyph = global::ICP.Sys.UI.Properties.Resources.Save_Blue_16;
            this.barSaves.Id = 1;
            this.barSaves.Name = "barSaves";
            this.barSaves.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSaves_ItemClick);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(219, 0);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(6, 409);
            this.splitterControl1.TabIndex = 11;
            this.splitterControl1.TabStop = false;
            // 
            // WorkSpaceViewOpViewPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlOperationViews);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.pnlEdit);
            this.Name = "WorkSpaceViewOpViewPart";
            this.Size = new System.Drawing.Size(864, 409);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlOperationViews)).EndInit();
            this.pnlOperationViews.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOperationType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlEdit)).EndInit();
            this.pnlEdit.ResumeLayout(false);
            this.pnlEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barAdd;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barEdit;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetails;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraGrid.Columns.GridColumn colIsCheck;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkCheck;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colCName;
        private DevExpress.XtraGrid.Columns.GridColumn colEName;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbOperationType;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateBy;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateDate;
        private DevExpress.XtraEditors.GroupControl pnlEdit;
        private DevExpress.XtraEditors.LabelControl labEName;
        private DevExpress.XtraEditors.TextEdit txtEName;
        private DevExpress.XtraEditors.LabelControl labCName;
        private DevExpress.XtraEditors.TextEdit txtCName;
        private DevExpress.XtraEditors.LabelControl labCode;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.GroupControl pnlOperationViews;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarManager barManager2;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barNew;
        private DevExpress.XtraBars.BarButtonItem barSaves;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraGrid.Columns.GridColumn colShowIndex;


    }
}
