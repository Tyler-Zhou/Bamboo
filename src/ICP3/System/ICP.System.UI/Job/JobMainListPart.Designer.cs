namespace ICP.Sys.UI.Job
{
    partial class JobMainListPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JobMainListPart));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barDisuse = new DevExpress.XtraBars.BarButtonItem();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barSearch = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.treeMain = new ICP.Framework.ClientComponents.Controls.LWTreeGridControl();
            this.colCode = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colEName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDescription = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.lwchkIsValid = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.numMax = new DevExpress.XtraEditors.SpinEdit();
            this.labMax = new DevExpress.XtraEditors.LabelControl();
            this.labIsValid = new DevExpress.XtraEditors.LabelControl();
            this.labName = new DevExpress.XtraEditors.LabelControl();
            this.labCode = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.colCreateBy = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCreateDate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
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
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barAdd,
            this.barDisuse,
            this.barClose,
            this.barSearch,
            this.barRefresh});
            this.barManager1.MaxItemId = 5;
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 1";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAdd, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDisuse, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefresh, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSearch, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Custom 1";
            // 
            // barAdd
            // 
            this.barAdd.Caption = "&Add";
            this.barAdd.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Add_16;
            this.barAdd.Id = 0;
            this.barAdd.Name = "barAdd";
            this.barAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdd_ItemClick);
            // 
            // barDisuse
            // 
            this.barDisuse.Caption = "&Disuse";
            this.barDisuse.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Disuse_16;
            this.barDisuse.Id = 1;
            this.barDisuse.Name = "barDisuse";
            this.barDisuse.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDisuse_ItemClick);
            // 
            // barRefresh
            // 
            this.barRefresh.Caption = "&Refresh";
            this.barRefresh.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Refresh_16;
            this.barRefresh.Id = 4;
            this.barRefresh.Name = "barRefresh";
            this.barRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRefresh_ItemClick);
            // 
            // barSearch
            // 
            this.barSearch.Caption = "Searc&h";
            this.barSearch.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Search_16;
            this.barSearch.Id = 3;
            this.barSearch.Name = "barSearch";
            this.barSearch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSearch_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "Close";
            this.barClose.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Close_16;
            this.barClose.Id = 2;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(843, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 502);
            this.barDockControlBottom.Size = new System.Drawing.Size(843, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 476);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(843, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 476);
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.Sys.ServiceInterface.DataObjects.JobList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsMainList_PositionChanged);
            // 
            // treeMain
            // 
            this.treeMain.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.treeMain.Appearance.EvenRow.Options.UseBackColor = true;
            this.treeMain.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.treeMain.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.treeMain.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeMain.Appearance.FocusedCell.Options.UseForeColor = true;
            this.treeMain.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colCode,
            this.colCName,
            this.colEName,
            this.colDescription,
            this.colCreateBy,
            this.colCreateDate});
            this.treeMain.DataSource = this.bsList;
            this.treeMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMain.Location = new System.Drawing.Point(0, 0);
            this.treeMain.Name = "treeMain";
            this.treeMain.OptionsBehavior.DragNodes = true;
            this.treeMain.OptionsBehavior.Editable = false;
            this.treeMain.OptionsSelection.InvertSelection = true;
            this.treeMain.OptionsSelection.MultiSelect = true;
            this.treeMain.OptionsView.EnableAppearanceEvenRow = true;
            this.treeMain.RootValue = null;
            this.treeMain.Size = new System.Drawing.Size(843, 470);
            this.treeMain.TabIndex = 6;
            this.treeMain.AfterDragNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeMain_AfterDragNode);
            this.treeMain.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeMain_NodeCellStyle);
            this.treeMain.BeforeDragNode += new DevExpress.XtraTreeList.BeforeDragNodeEventHandler(this.treeMain_BeforeDragNode);
            this.treeMain.BeforeFocusNode += new DevExpress.XtraTreeList.BeforeFocusNodeEventHandler(this.treeMain_BeforeFocusNode);
            // 
            // colCode
            // 
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 0;
            this.colCode.Width = 68;
            // 
            // colCName
            // 
            this.colCName.FieldName = "CName";
            this.colCName.Name = "colCName";
            this.colCName.Visible = true;
            this.colCName.VisibleIndex = 1;
            this.colCName.Width = 68;
            // 
            // colEName
            // 
            this.colEName.FieldName = "EName";
            this.colEName.Name = "colEName";
            this.colEName.Visible = true;
            this.colEName.VisibleIndex = 2;
            this.colEName.Width = 68;
            // 
            // colDescription
            // 
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 3;
            this.colDescription.Width = 68;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Collapsed = true;
            this.splitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel1;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 26);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.btnClear);
            this.splitContainerControl1.Panel1.Controls.Add(this.btnSearch);
            this.splitContainerControl1.Panel1.Controls.Add(this.lwchkIsValid);
            this.splitContainerControl1.Panel1.Controls.Add(this.numMax);
            this.splitContainerControl1.Panel1.Controls.Add(this.labMax);
            this.splitContainerControl1.Panel1.Controls.Add(this.labIsValid);
            this.splitContainerControl1.Panel1.Controls.Add(this.labName);
            this.splitContainerControl1.Panel1.Controls.Add(this.labCode);
            this.splitContainerControl1.Panel1.Controls.Add(this.txtName);
            this.splitContainerControl1.Panel1.Controls.Add(this.txtCode);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.treeMain);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(843, 476);
            this.splitContainerControl1.SplitterPosition = 33;
            this.splitContainerControl1.TabIndex = 11;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(684, 6);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 17;
            this.btnClear.Text = "C&lear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(-91, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 16;
            this.btnSearch.Text = "Sea&rch";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lwchkIsValid
            // 
            this.lwchkIsValid.Checked = true;
            this.lwchkIsValid.CheckedText = "Valid";
            this.lwchkIsValid.Location = new System.Drawing.Point(377, 6);
            this.lwchkIsValid.Name = "lwchkIsValid";
            this.lwchkIsValid.NULLText = "ALL";
            this.lwchkIsValid.Size = new System.Drawing.Size(115, 21);
            this.lwchkIsValid.TabIndex = 10;
            this.lwchkIsValid.UnCheckedText = "UnValid";
            // 
            // numMax
            // 
            this.numMax.EditValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMax.Location = new System.Drawing.Point(558, 6);
            this.numMax.Name = "numMax";
            this.numMax.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numMax.Properties.IsFloatValue = false;
            this.numMax.Properties.Mask.EditMask = "N00";
            this.numMax.Properties.MaxValue = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMax.Size = new System.Drawing.Size(99, 21);
            this.numMax.TabIndex = 11;
            // 
            // labMax
            // 
            this.labMax.Location = new System.Drawing.Point(496, 9);
            this.labMax.Name = "labMax";
            this.labMax.Size = new System.Drawing.Size(58, 14);
            this.labMax.TabIndex = 14;
            this.labMax.Text = "Max Count";
            // 
            // labIsValid
            // 
            this.labIsValid.Location = new System.Drawing.Point(337, 9);
            this.labIsValid.Name = "labIsValid";
            this.labIsValid.Size = new System.Drawing.Size(34, 14);
            this.labIsValid.TabIndex = 15;
            this.labIsValid.Text = "IsValid";
            // 
            // labName
            // 
            this.labName.Location = new System.Drawing.Point(170, 9);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(31, 14);
            this.labName.TabIndex = 12;
            this.labName.Text = "Name";
            // 
            // labCode
            // 
            this.labCode.Location = new System.Drawing.Point(6, 9);
            this.labCode.Name = "labCode";
            this.labCode.Size = new System.Drawing.Size(28, 14);
            this.labCode.TabIndex = 13;
            this.labCode.Text = "Code";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(232, 6);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(99, 21);
            this.txtName.TabIndex = 9;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(68, 6);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(99, 21);
            this.txtCode.TabIndex = 8;
            // 
            // colCreateBy
            // 
            this.colCreateBy.FieldName = "CreateBy";
            this.colCreateBy.Name = "colCreateBy";
            this.colCreateBy.Visible = true;
            this.colCreateBy.VisibleIndex = 4;
            // 
            // colCreateDate
            // 
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 5;
            // 
            // JobMainListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "JobMainListPart";
            this.Size = new System.Drawing.Size(843, 502);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private ICP.Framework.ClientComponents.Controls.LWTreeGridControl treeMain;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraBars.BarButtonItem barAdd;
        private DevExpress.XtraBars.BarButtonItem barDisuse;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraBars.BarButtonItem barSearch;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private ICP.Framework.ClientComponents.Controls.LWCheckButton lwchkIsValid;
        private DevExpress.XtraEditors.SpinEdit numMax;
        private DevExpress.XtraEditors.LabelControl labMax;
        private DevExpress.XtraEditors.LabelControl labIsValid;
        private DevExpress.XtraEditors.LabelControl labName;
        private DevExpress.XtraEditors.LabelControl labCode;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCode;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colEName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDescription;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCreateBy;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCreateDate;
    }
}
