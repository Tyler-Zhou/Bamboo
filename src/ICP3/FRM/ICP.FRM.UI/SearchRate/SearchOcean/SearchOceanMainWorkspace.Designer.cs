namespace ICP.FRM.UI.SearchRate
{
    partial class SearchOceanMainWorkspace
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
            this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.SearchWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.panel1 = new ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl(this.components);
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.ListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.RemarkWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.tabDetail = new DevExpress.XtraTab.XtraTabControl();
            this.tabBaseInfo = new DevExpress.XtraTab.XtraTabPage();
            this.BaseInfoWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.tabContractInfo = new DevExpress.XtraTab.XtraTabPage();
            this.ContractWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.tabFees = new DevExpress.XtraTab.XtraTabPage();
            this.FeesWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dpSearch = new DevExpress.XtraBars.Docking.DockPanel();
            this.ToolbarWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.controlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabDetail)).BeginInit();
            this.tabDetail.SuspendLayout();
            this.tabBaseInfo.SuspendLayout();
            this.tabContractInfo.SuspendLayout();
            this.tabFees.SuspendLayout();
            this.dpSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlContainer1
            // 
            this.controlContainer1.Controls.Add(this.SearchWorkspace);
            this.controlContainer1.Location = new System.Drawing.Point(3, 25);
            this.controlContainer1.Name = "controlContainer1";
            this.controlContainer1.Size = new System.Drawing.Size(214, 501);
            this.controlContainer1.TabIndex = 0;
            // 
            // SearchWorkspace
            // 
            this.SearchWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchWorkspace.Location = new System.Drawing.Point(0, 0);
            this.SearchWorkspace.Name = "SearchWorkspace";
            this.SearchWorkspace.Size = new System.Drawing.Size(214, 501);
            this.SearchWorkspace.TabIndex = 5;
            this.SearchWorkspace.Text = "deckWorkspace1";
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this.panel1;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dpSearch});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainerControl1);
            this.panel1.Controls.Add(this.dpSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 529);
            this.panel1.TabIndex = 5;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(220, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.ListWorkspace);
            this.splitContainerControl1.Panel1.Controls.Add(this.RemarkWorkspace);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.tabDetail);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(780, 529);
            this.splitContainerControl1.SplitterPosition = 263;
            this.splitContainerControl1.TabIndex = 3;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // ListWorkspace
            // 
            this.ListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListWorkspace.Location = new System.Drawing.Point(400, 0);
            this.ListWorkspace.Name = "ListWorkspace";
            this.ListWorkspace.Size = new System.Drawing.Size(380, 263);
            this.ListWorkspace.TabIndex = 1;
            this.ListWorkspace.Text = "deckWorkspace4";
            // 
            // RemarkWorkspace
            // 
            this.RemarkWorkspace.Dock = System.Windows.Forms.DockStyle.Left;
            this.RemarkWorkspace.Location = new System.Drawing.Point(0, 0);
            this.RemarkWorkspace.Name = "RemarkWorkspace";
            this.RemarkWorkspace.Size = new System.Drawing.Size(400, 263);
            this.RemarkWorkspace.TabIndex = 2;
            this.RemarkWorkspace.Text = "deckWorkspace4";
            this.RemarkWorkspace.Visible = false;
            // 
            // tabDetail
            // 
            this.tabDetail.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.tabDetail.Appearance.Options.UseBackColor = true;
            this.tabDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabDetail.Location = new System.Drawing.Point(0, 0);
            this.tabDetail.Name = "tabDetail";
            this.tabDetail.SelectedTabPage = this.tabBaseInfo;
            this.tabDetail.Size = new System.Drawing.Size(780, 260);
            this.tabDetail.TabIndex = 0;
            this.tabDetail.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabBaseInfo,
            this.tabContractInfo,
            this.tabFees});
            // 
            // tabBaseInfo
            // 
            this.tabBaseInfo.Controls.Add(this.BaseInfoWorkspace);
            this.tabBaseInfo.Name = "tabBaseInfo";
            this.tabBaseInfo.Size = new System.Drawing.Size(773, 230);
            this.tabBaseInfo.Text = "Base Info";
            // 
            // BaseInfoWorkspace
            // 
            this.BaseInfoWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.BaseInfoWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BaseInfoWorkspace.Location = new System.Drawing.Point(0, 0);
            this.BaseInfoWorkspace.Name = "BaseInfoWorkspace";
            this.BaseInfoWorkspace.Size = new System.Drawing.Size(773, 230);
            this.BaseInfoWorkspace.TabIndex = 2;
            this.BaseInfoWorkspace.Text = "deckWorkspace4";
            // 
            // tabContractInfo
            // 
            this.tabContractInfo.Controls.Add(this.ContractWorkspace);
            this.tabContractInfo.Name = "tabContractInfo";
            this.tabContractInfo.Size = new System.Drawing.Size(773, 230);
            this.tabContractInfo.Text = "Contract Info";
            // 
            // ContractWorkspace
            // 
            this.ContractWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.ContractWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContractWorkspace.Location = new System.Drawing.Point(0, 0);
            this.ContractWorkspace.Name = "ContractWorkspace";
            this.ContractWorkspace.Size = new System.Drawing.Size(773, 230);
            this.ContractWorkspace.TabIndex = 2;
            this.ContractWorkspace.Text = "deckWorkspace4";
            // 
            // tabFees
            // 
            this.tabFees.Controls.Add(this.FeesWorkspace);
            this.tabFees.Name = "tabFees";
            this.tabFees.Size = new System.Drawing.Size(993, 230);
            this.tabFees.Text = "Fees";
            // 
            // FeesWorkspace
            // 
            this.FeesWorkspace.BackColor = System.Drawing.Color.White;
            this.FeesWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FeesWorkspace.Location = new System.Drawing.Point(0, 0);
            this.FeesWorkspace.Name = "FeesWorkspace";
            this.FeesWorkspace.Size = new System.Drawing.Size(993, 230);
            this.FeesWorkspace.TabIndex = 3;
            this.FeesWorkspace.Text = "deckWorkspace4";
            // 
            // dpSearch
            // 
            this.dpSearch.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.dpSearch.Appearance.Options.UseBackColor = true;
            this.dpSearch.Controls.Add(this.controlContainer1);
            this.dpSearch.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dpSearch.ID = new System.Guid("c57a44d9-87ee-4430-86e3-6058720756c3");
            this.dpSearch.Location = new System.Drawing.Point(0, 0);
            this.dpSearch.Name = "dpSearch";
            this.dpSearch.OriginalSize = new System.Drawing.Size(220, 200);
            this.dpSearch.Size = new System.Drawing.Size(220, 529);
            this.dpSearch.Text = "Search";
            // 
            // ToolbarWorkspace
            // 
            this.ToolbarWorkspace.Dock = System.Windows.Forms.DockStyle.Top;
            this.ToolbarWorkspace.Location = new System.Drawing.Point(0, 0);
            this.ToolbarWorkspace.Name = "ToolbarWorkspace";
            this.ToolbarWorkspace.Size = new System.Drawing.Size(1000, 27);
            this.ToolbarWorkspace.TabIndex = 6;
            this.ToolbarWorkspace.Text = "deckWorkspace1";
            // 
            // SearchOceanMainWorkspace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ToolbarWorkspace);
            this.Name = "SearchOceanMainWorkspace";
            this.Size = new System.Drawing.Size(1000, 556);
            this.controlContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabDetail)).EndInit();
            this.tabDetail.ResumeLayout(false);
            this.tabBaseInfo.ResumeLayout(false);
            this.tabContractInfo.ResumeLayout(false);
            this.tabFees.ResumeLayout(false);
            this.dpSearch.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.ControlContainer controlContainer1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace SearchWorkspace;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl panel1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ListWorkspace;
        private DevExpress.XtraBars.Docking.DockPanel dpSearch;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ToolbarWorkspace;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTab.XtraTabControl tabDetail;
        private DevExpress.XtraTab.XtraTabPage tabBaseInfo;
        private DevExpress.XtraTab.XtraTabPage tabContractInfo;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace BaseInfoWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ContractWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace RemarkWorkspace;
        private DevExpress.XtraTab.XtraTabPage tabFees;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace FeesWorkspace;
    }
}
