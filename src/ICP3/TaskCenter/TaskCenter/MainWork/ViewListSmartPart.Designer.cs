namespace ICP.TaskCenter.UI
{
    partial class ViewListSmartPart
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
         
                this.ViewNodeChangedEventHandler = null;
            
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewListSmartPart));
            this.treeListNodes = new ICP.Framework.ClientComponents.Controls.LWTreeGridControl();
            this.ColumnCaption = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageListState = new System.Windows.Forms.ImageList(this.components);
            this.toolTipTreeList = new DevExpress.Utils.ToolTipController(this.components);
            this.bindingSourceNodes = new System.Windows.Forms.BindingSource(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.barAdvancedquery = new DevExpress.XtraBars.BarButtonItem();
            this.barButKeep = new DevExpress.XtraBars.BarButtonItem();
            this.barButDelete = new DevExpress.XtraBars.BarButtonItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.txtquery = new DevExpress.XtraEditors.TextEdit();
            this.btnAdvancedquery = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.cmbOpBranch = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.btnEndtAdvancedquery = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAssists = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.treeListNodes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceNodes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtquery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOpBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeListNodes
            // 
            this.treeListNodes.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.treeListNodes.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.ColumnCaption});
            this.treeListNodes.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListNodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListNodes.KeyFieldName = "Id";
            this.treeListNodes.Location = new System.Drawing.Point(2, 2);
            this.treeListNodes.Name = "treeListNodes";
            this.treeListNodes.OptionsBehavior.AllowExpandOnDblClick = false;
            this.treeListNodes.OptionsBehavior.Editable = false;
            this.treeListNodes.OptionsBehavior.UseTabKey = true;
            this.treeListNodes.OptionsSelection.InvertSelection = true;
            this.treeListNodes.OptionsView.ShowButtons = false;
            this.treeListNodes.OptionsView.ShowColumns = false;
            this.treeListNodes.OptionsView.ShowHorzLines = false;
            this.treeListNodes.OptionsView.ShowIndicator = false;
            this.treeListNodes.OptionsView.ShowVertLines = false;
            this.treeListNodes.ParentFieldName = "ParentId";
            this.treeListNodes.Size = new System.Drawing.Size(349, 710);
            this.treeListNodes.StateImageList = this.imageListState;
            this.treeListNodes.TabIndex = 0;
            this.treeListNodes.ToolTipController = this.toolTipTreeList;
            this.treeListNodes.GetStateImage += new DevExpress.XtraTreeList.GetStateImageEventHandler(this.treeListNodes_GetStateImage);
            this.treeListNodes.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeListNodes_FocusedNodeChanged);
            this.treeListNodes.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeListNodes_MouseClick);
            this.treeListNodes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeListNodes_MouseUp);
            // 
            // ColumnCaption
            // 
            this.ColumnCaption.Caption = "Caption";
            this.ColumnCaption.FieldName = "Caption";
            this.ColumnCaption.Name = "ColumnCaption";
            this.ColumnCaption.Visible = true;
            this.ColumnCaption.VisibleIndex = 0;
            this.ColumnCaption.Width = 92;
            // 
            // imageListState
            // 
            this.imageListState.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListState.ImageStream")));
            this.imageListState.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListState.Images.SetKeyName(0, "plus.png");
            this.imageListState.Images.SetKeyName(1, "Minus.png");
            // 
            // toolTipTreeList
            // 
            this.toolTipTreeList.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(this.toolTipTreeList_GetActiveObjectInfo);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barEditItem1,
            this.barAdvancedquery,
            this.barButKeep,
            this.barButDelete});
            this.barManager1.MaxItemId = 11;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1,
            this.repositoryItemTextEdit1});
            this.barManager1.HighlightedLinkChanged += new DevExpress.XtraBars.HighlightedLinkChangedEventHandler(this.barManager1_HighlightedLinkChanged);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(353, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 791);
            this.barDockControlBottom.Size = new System.Drawing.Size(353, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 791);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(353, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 791);
            // 
            // barEditItem1
            // 
            this.barEditItem1.Caption = "Searshtext";
            this.barEditItem1.Edit = this.repositoryItemButtonEdit1;
            this.barEditItem1.Id = 0;
            this.barEditItem1.Name = "barEditItem1";
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // barAdvancedquery
            // 
            this.barAdvancedquery.Caption = "...";
            this.barAdvancedquery.Enabled = false;
            this.barAdvancedquery.Id = 4;
            this.barAdvancedquery.Name = "barAdvancedquery";
            // 
            // barButKeep
            // 
            this.barButKeep.Glyph = global::ICP.TaskCenter.UI.Properties.Resources.ding;
            this.barButKeep.Id = 7;
            this.barButKeep.Name = "barButKeep";
            this.barButKeep.Tag = "";
            this.barButKeep.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButKeep_ItemClick);
            // 
            // barButDelete
            // 
            this.barButDelete.Glyph = global::ICP.TaskCenter.UI.Properties.Resources.Delete_16;
            this.barButDelete.Id = 8;
            this.barButDelete.Name = "barButDelete";
            this.barButDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButDelete_ItemClick);
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // txtquery
            // 
            this.txtquery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtquery.Location = new System.Drawing.Point(5, 4);
            this.txtquery.MenuManager = this.barManager1;
            this.txtquery.Name = "txtquery";
            this.txtquery.Size = new System.Drawing.Size(273, 21);
            this.txtquery.TabIndex = 5;
            // 
            // btnAdvancedquery
            // 
            this.btnAdvancedquery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdvancedquery.Enabled = false;
            this.btnAdvancedquery.Location = new System.Drawing.Point(317, 3);
            this.btnAdvancedquery.Name = "btnAdvancedquery";
            this.btnAdvancedquery.Size = new System.Drawing.Size(32, 23);
            this.btnAdvancedquery.TabIndex = 10;
            this.btnAdvancedquery.Text = "...";
            this.btnAdvancedquery.Click += new System.EventHandler(this.BtnAdvancedquery_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.cmbOpBranch);
            this.panelControl1.Controls.Add(this.btnEndtAdvancedquery);
            this.panelControl1.Controls.Add(this.btnAdvancedquery);
            this.panelControl1.Controls.Add(this.txtquery);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(353, 44);
            this.panelControl1.TabIndex = 15;
            // 
            // cmbOpBranch
            // 
            this.cmbOpBranch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbOpBranch.EditValue = "";
            this.cmbOpBranch.Location = new System.Drawing.Point(5, 23);
            this.cmbOpBranch.MenuManager = this.barManager1;
            this.cmbOpBranch.Name = "cmbOpBranch";
            this.cmbOpBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbOpBranch.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cmbOpBranch.Size = new System.Drawing.Size(273, 21);
            this.cmbOpBranch.TabIndex = 12;
            this.cmbOpBranch.TabStop = false;
            // 
            // btnEndtAdvancedquery
            // 
            this.btnEndtAdvancedquery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEndtAdvancedquery.Location = new System.Drawing.Point(284, 3);
            this.btnEndtAdvancedquery.Name = "btnEndtAdvancedquery";
            this.btnEndtAdvancedquery.Size = new System.Drawing.Size(32, 23);
            this.btnEndtAdvancedquery.TabIndex = 11;
            this.btnEndtAdvancedquery.Text = "→";
            this.btnEndtAdvancedquery.Click += new System.EventHandler(this.btnEndtAdvancedquery_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.treeListNodes);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 77);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(353, 714);
            this.panelControl2.TabIndex = 16;
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButKeep),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButDelete)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnAssists);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(353, 33);
            this.panel1.TabIndex = 21;
            // 
            // btnAssists
            // 
            this.btnAssists.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAssists.Image = global::ICP.TaskCenter.UI.Properties.Resources.button;
            this.btnAssists.Location = new System.Drawing.Point(0, 0);
            this.btnAssists.Name = "btnAssists";
            this.btnAssists.Size = new System.Drawing.Size(103, 33);
            this.btnAssists.TabIndex = 0;
            this.btnAssists.Text = "协助同事";
            this.btnAssists.Click += new System.EventHandler(this.simpleButtonAssists_Click);
            // 
            // ViewListSmartPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ViewListSmartPart";
            this.Size = new System.Drawing.Size(353, 791);
            ((System.ComponentModel.ISupportInitialize)(this.treeListNodes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceNodes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtquery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbOpBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

   

        #endregion

        private ICP.Framework.ClientComponents.Controls.LWTreeGridControl treeListNodes;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCode;
        private System.Windows.Forms.BindingSource bindingSourceNodes;
        private System.Windows.Forms.ImageList imageListState;
        private DevExpress.XtraTreeList.Columns.TreeListColumn ColumnCaption;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarEditItem barEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraBars.BarButtonItem barAdvancedquery;
        private DevExpress.XtraEditors.TextEdit txtquery;
        private DevExpress.XtraEditors.SimpleButton btnAdvancedquery;
        private DevExpress.Utils.ToolTipController toolTipTreeList;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarButtonItem barButKeep;
        private DevExpress.XtraBars.BarButtonItem barButDelete;
        private DevExpress.XtraEditors.SimpleButton btnEndtAdvancedquery;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnAssists;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cmbOpBranch;


    }
}
