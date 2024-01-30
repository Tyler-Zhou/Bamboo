namespace ICP.Sys.UI.PermissionManage.Function
{
    partial class FunctionSelectPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FunctionSelectPart));
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.treeMain = new ICP.Framework.ClientComponents.Controls.LWTreeGridControl();
            this.colCName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colEName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.rcmbType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.chkbtnHold = new DevExpress.XtraEditors.CheckButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.chkbtnUnChecked = new DevExpress.XtraEditors.CheckButton();
            this.chkbtnChecked = new DevExpress.XtraEditors.CheckButton();
            this.chkbtnAll = new DevExpress.XtraEditors.CheckButton();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.txtFind = new DevExpress.XtraEditors.ButtonEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barSelectAll = new DevExpress.XtraBars.BarButtonItem();
            this.barUnSelectAll = new DevExpress.XtraBars.BarButtonItem();
            this.barAntiSelect = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbType)).BeginInit();
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
            this.bsList.DataSource = typeof(ICP.Sys.ServiceInterface.DataObjects.FunctionList);
            // 
            // treeMain
            // 
            this.treeMain.AllowDrop = true;
            this.treeMain.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.treeMain.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.treeMain.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeMain.Appearance.FocusedCell.Options.UseForeColor = true;
            this.treeMain.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colCName,
            this.colEName});
            this.treeMain.DataSource = this.bsList;
            this.treeMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMain.Location = new System.Drawing.Point(0, 33);
            this.treeMain.Name = "treeMain";
            this.treeMain.OptionsBehavior.Editable = false;
            this.treeMain.OptionsSelection.InvertSelection = true;
            this.treeMain.OptionsView.ShowCheckBoxes = true;
            this.treeMain.OptionsView.ShowColumns = false;
            this.treeMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbType});
            this.treeMain.RootValue = null;
            this.treeMain.Size = new System.Drawing.Size(433, 204);
            this.treeMain.StateImageList = this.imageList1;
            this.treeMain.TabIndex = 4;
            this.treeMain.BeforeCheckNode += new DevExpress.XtraTreeList.CheckNodeEventHandler(this.treeMain_BeforeCheckNode);
            this.treeMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeMain_MouseDown);
            this.treeMain.GetStateImage += new DevExpress.XtraTreeList.GetStateImageEventHandler(this.treeMain_GetStateImage);
            this.treeMain.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeMain_AfterCheckNode);
            // 
            // colCName
            // 
            this.colCName.FieldName = "CName";
            this.colCName.MinWidth = 35;
            this.colCName.Name = "colCName";
            this.colCName.Width = 169;
            // 
            // colEName
            // 
            this.colEName.FieldName = "EName";
            this.colEName.MinWidth = 35;
            this.colEName.Name = "colEName";
            this.colEName.Visible = true;
            this.colEName.VisibleIndex = 0;
            // 
            // rcmbType
            // 
            this.rcmbType.AutoHeight = false;
            this.rcmbType.LargeImages = this.imageList1;
            this.rcmbType.Name = "rcmbType";
            this.rcmbType.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
            this.rcmbType.SmallImages = this.imageList1;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Data_16.png");
            this.imageList1.Images.SetKeyName(1, "Data_16.png");
            this.imageList1.Images.SetKeyName(2, "BlueFile_16.png");
            this.imageList1.Images.SetKeyName(3, "Add_16.png");
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.chkbtnHold);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Controls.Add(this.btnNext);
            this.panelControl1.Controls.Add(this.txtFind);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(433, 33);
            this.panelControl1.TabIndex = 5;
            // 
            // chkbtnHold
            // 
            this.chkbtnHold.Image = ((System.Drawing.Image)(resources.GetObject("chkbtnHold.Image")));
            this.chkbtnHold.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.chkbtnHold.Location = new System.Drawing.Point(5, 5);
            this.chkbtnHold.Name = "chkbtnHold";
            this.chkbtnHold.Size = new System.Drawing.Size(21, 21);
            this.chkbtnHold.TabIndex = 11;
            this.chkbtnHold.ToolTip = "Hold";
            this.chkbtnHold.Click += new System.EventHandler(this.chkbtnHold_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.chkbtnUnChecked);
            this.panelControl2.Controls.Add(this.chkbtnChecked);
            this.panelControl2.Controls.Add(this.chkbtnAll);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl2.Location = new System.Drawing.Point(343, 2);
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
            this.btnNext.Location = new System.Drawing.Point(269, 5);
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
            this.txtFind.Size = new System.Drawing.Size(231, 21);
            this.txtFind.TabIndex = 0;
            this.txtFind.TabStop = false;
            this.txtFind.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtFind_ButtonClick);
            this.txtFind.TextChanged += new System.EventHandler(this.txtFind_TextChanged);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSelectAll,
            this.barUnSelectAll,
            this.barAntiSelect});
            this.barManager1.MaxItemId = 3;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(433, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 237);
            this.barDockControlBottom.Size = new System.Drawing.Size(433, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 237);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(433, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 237);
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
            this.barAntiSelect.Caption = "Anti Select";
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
            // FunctionSelectPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeMain);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FunctionSelectPart";
            this.Size = new System.Drawing.Size(433, 237);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbType)).EndInit();
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
        private ICP.Framework.ClientComponents.Controls.LWTreeGridControl treeMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbType;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCName;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.CheckButton chkbtnAll;
        private DevExpress.XtraEditors.CheckButton chkbtnUnChecked;
        private DevExpress.XtraEditors.CheckButton chkbtnChecked;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarButtonItem barSelectAll;
        private DevExpress.XtraBars.BarButtonItem barUnSelectAll;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barAntiSelect;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colEName;
        private DevExpress.XtraEditors.ButtonEdit txtFind;
        private DevExpress.XtraEditors.CheckButton chkbtnHold;
    }
}
