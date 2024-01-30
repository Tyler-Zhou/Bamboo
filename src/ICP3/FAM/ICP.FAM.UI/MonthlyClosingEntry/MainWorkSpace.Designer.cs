namespace ICP.FAM.UI.MonthlyClosingEntry
{
    partial class MainWorkSpace
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ToolbarWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.layoutContainerControl1 = new ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl(this.components);
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.ListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.xtab = new DevExpress.XtraTab.XtraTabControl();
            this.tabPageEdit = new DevExpress.XtraTab.XtraTabPage();
            this.EditWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.tabPageDocumentList = new DevExpress.XtraTab.XtraTabPage();
            this.DocumentListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dpSearch = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.SearchWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.tabMemo = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.layoutContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtab)).BeginInit();
            this.xtab.SuspendLayout();
            this.tabPageEdit.SuspendLayout();
            this.tabPageDocumentList.SuspendLayout();
            this.dpSearch.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ToolbarWorkspace);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.layoutContainerControl1);
            this.splitContainer1.Size = new System.Drawing.Size(814, 463);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 9;
            // 
            // ToolbarWorkspace
            // 
            this.ToolbarWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolbarWorkspace.Location = new System.Drawing.Point(0, 0);
            this.ToolbarWorkspace.Name = "ToolbarWorkspace";
            this.ToolbarWorkspace.Size = new System.Drawing.Size(814, 25);
            this.ToolbarWorkspace.TabIndex = 6;
            this.ToolbarWorkspace.Text = "deckWorkspace1";
            // 
            // layoutContainerControl1
            // 
            this.layoutContainerControl1.Controls.Add(this.splitContainerControl1);
            this.layoutContainerControl1.Controls.Add(this.dpSearch);
            this.layoutContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutContainerControl1.Name = "layoutContainerControl1";
            this.layoutContainerControl1.Size = new System.Drawing.Size(814, 437);
            this.layoutContainerControl1.TabIndex = 3;
            this.layoutContainerControl1.Text = "layoutContainerControl1";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(222, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.ListWorkspace);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.xtab);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(592, 437);
            this.splitContainerControl1.SplitterPosition = 247;
            this.splitContainerControl1.TabIndex = 2;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // ListWorkspace
            // 
            this.ListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListWorkspace.Location = new System.Drawing.Point(0, 0);
            this.ListWorkspace.Name = "ListWorkspace";
            this.ListWorkspace.Size = new System.Drawing.Size(592, 184);
            this.ListWorkspace.TabIndex = 1;
            this.ListWorkspace.Text = "deckWorkspace4";
            // 
            // xtab
            // 
            this.xtab.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.xtab.Appearance.Options.UseBackColor = true;
            this.xtab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtab.Location = new System.Drawing.Point(0, 0);
            this.xtab.Name = "xtab";
            this.xtab.SelectedTabPage = this.tabPageEdit;
            this.xtab.Size = new System.Drawing.Size(592, 247);
            this.xtab.TabIndex = 1;
            this.xtab.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageEdit,
            this.tabPageDocumentList});
            // 
            // tabPageEdit
            // 
            this.tabPageEdit.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.tabPageEdit.Appearance.PageClient.Options.UseBackColor = true;
            this.tabPageEdit.Controls.Add(this.EditWorkspace);
            this.tabPageEdit.Name = "tabPageEdit";
            this.tabPageEdit.Size = new System.Drawing.Size(585, 217);
            this.tabPageEdit.Text = "Edit";
            // 
            // EditWorkspace
            // 
            this.EditWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.EditWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditWorkspace.Location = new System.Drawing.Point(0, 0);
            this.EditWorkspace.Name = "EditWorkspace";
            this.EditWorkspace.Size = new System.Drawing.Size(585, 217);
            this.EditWorkspace.TabIndex = 6;
            // 
            // tabPageDocumentList
            // 
            this.tabPageDocumentList.Controls.Add(this.DocumentListWorkspace);
            this.tabPageDocumentList.Name = "tabPageDocumentList";
            this.tabPageDocumentList.Size = new System.Drawing.Size(585, 217);
            this.tabPageDocumentList.Text = "Document List";
            // 
            // DocumentListWorkspace
            // 
            this.DocumentListWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.DocumentListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocumentListWorkspace.Location = new System.Drawing.Point(0, 0);
            this.DocumentListWorkspace.Name = "DocumentListWorkspace";
            this.DocumentListWorkspace.Size = new System.Drawing.Size(585, 217);
            this.DocumentListWorkspace.TabIndex = 4;
            this.DocumentListWorkspace.Text = "Document List";
            // 
            // dpSearch
            // 
            this.dpSearch.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.dpSearch.Appearance.Options.UseBackColor = true;
            this.dpSearch.Controls.Add(this.dockPanel1_Container);
            this.dpSearch.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dpSearch.ID = new System.Guid("37830ced-1584-474d-82ed-09fc8e01990d");
            this.dpSearch.Location = new System.Drawing.Point(0, 0);
            this.dpSearch.Name = "dpSearch";
            this.dpSearch.OriginalSize = new System.Drawing.Size(222, 200);
            this.dpSearch.Size = new System.Drawing.Size(222, 437);
            this.dpSearch.Text = "查询条件";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.SearchWorkspace);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(216, 409);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // SearchWorkspace
            // 
            this.SearchWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchWorkspace.Location = new System.Drawing.Point(0, 0);
            this.SearchWorkspace.Name = "SearchWorkspace";
            this.SearchWorkspace.Size = new System.Drawing.Size(216, 409);
            this.SearchWorkspace.TabIndex = 0;
            this.SearchWorkspace.Text = "deckWorkspace1";
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this.layoutContainerControl1;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dpSearch});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // tabMemo
            // 
            this.tabMemo.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.tabMemo.Appearance.PageClient.Options.UseBackColor = true;
            this.tabMemo.Name = "tabMemo";
            this.tabMemo.Size = new System.Drawing.Size(585, 192);
            // 
            // MainWorkSpace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainWorkSpace";
            this.Size = new System.Drawing.Size(814, 463);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.layoutContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtab)).EndInit();
            this.xtab.ResumeLayout(false);
            this.tabPageEdit.ResumeLayout(false);
            this.tabPageDocumentList.ResumeLayout(false);
            this.dpSearch.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ToolbarWorkspace;
        private ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl layoutContainerControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ListWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace SearchWorkspace;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dpSearch;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraTab.XtraTabControl xtab;
        private DevExpress.XtraTab.XtraTabPage tabPageEdit;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace EditWorkspace;
        private DevExpress.XtraTab.XtraTabPage tabPageDocumentList;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace DocumentListWorkspace;
        private DevExpress.XtraTab.XtraTabPage tabMemo;
    }
}
