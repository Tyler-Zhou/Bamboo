namespace ICP.Common.UI.CC
{
    partial class CCMainWorkspace
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
            this.ListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dpSearch = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.SearchWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.panel1 = new ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl(this.components);
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.xtraTabMain = new DevExpress.XtraTab.XtraTabControl();
            this.pagePartner = new DevExpress.XtraTab.XtraTabPage();
            this.PartnerWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.pageBusiness = new DevExpress.XtraTab.XtraTabPage();
            this.BusinessWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.pageReport = new DevExpress.XtraTab.XtraTabPage();
            this.ReportWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.ToolBarWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.pageCustomerInfo = new DevExpress.XtraTab.XtraTabPage();
            this.pageCustoemrArchives = new DevExpress.XtraTab.XtraTabPage();
            this.CustoemrArchivesWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dpSearch.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabMain)).BeginInit();
            this.xtraTabMain.SuspendLayout();
            this.pagePartner.SuspendLayout();
            this.pageBusiness.SuspendLayout();
            this.pageReport.SuspendLayout();
            this.pageCustoemrArchives.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListWorkspace
            // 
            this.ListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListWorkspace.Location = new System.Drawing.Point(0, 0);
            this.ListWorkspace.Name = "ListWorkspace";
            this.ListWorkspace.Size = new System.Drawing.Size(676, 156);
            this.ListWorkspace.TabIndex = 10;
            this.ListWorkspace.Text = "deckWorkspace1";
            // 
            // dpSearch
            // 
            this.dpSearch.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.dpSearch.Appearance.Options.UseBackColor = true;
            this.dpSearch.Controls.Add(this.dockPanel1_Container);
            this.dpSearch.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dpSearch.ID = new System.Guid("53eeb54d-d0c0-492d-9990-38a1d8dbba3b");
            this.dpSearch.Location = new System.Drawing.Point(0, 0);
            this.dpSearch.Name = "dpSearch";
            this.dpSearch.OriginalSize = new System.Drawing.Size(220, 200);
            this.dpSearch.Size = new System.Drawing.Size(220, 558);
            this.dpSearch.Text = "Search";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.SearchWorkspace);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(214, 530);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // SearchWorkspace
            // 
            this.SearchWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchWorkspace.Location = new System.Drawing.Point(0, 0);
            this.SearchWorkspace.Name = "SearchWorkspace";
            this.SearchWorkspace.Size = new System.Drawing.Size(214, 530);
            this.SearchWorkspace.TabIndex = 1;
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
            this.panel1.Location = new System.Drawing.Point(0, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(896, 558);
            this.panel1.TabIndex = 14;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(220, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.ListWorkspace);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.xtraTabMain);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(676, 558);
            this.splitContainerControl1.SplitterPosition = 156;
            this.splitContainerControl1.TabIndex = 13;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // xtraTabMain
            // 
            this.xtraTabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabMain.Location = new System.Drawing.Point(0, 0);
            this.xtraTabMain.Name = "xtraTabMain";
            this.xtraTabMain.SelectedTabPage = this.pagePartner;
            this.xtraTabMain.Size = new System.Drawing.Size(676, 396);
            this.xtraTabMain.TabIndex = 12;
            this.xtraTabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.pageCustoemrArchives,
            this.pagePartner,
            this.pageBusiness,
            this.pageReport});
            // 
            // pagePartner
            // 
            this.pagePartner.Controls.Add(this.PartnerWorkspace);
            this.pagePartner.Name = "pagePartner";
            this.pagePartner.Size = new System.Drawing.Size(669, 366);
            this.pagePartner.Text = "Partner";
            // 
            // PartnerWorkspace
            // 
            this.PartnerWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.PartnerWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PartnerWorkspace.Location = new System.Drawing.Point(0, 0);
            this.PartnerWorkspace.Name = "PartnerWorkspace";
            this.PartnerWorkspace.Size = new System.Drawing.Size(669, 366);
            this.PartnerWorkspace.TabIndex = 12;
            this.PartnerWorkspace.Text = "deckWorkspace2";
            // 
            // pageBusiness
            // 
            this.pageBusiness.Controls.Add(this.BusinessWorkspace);
            this.pageBusiness.Name = "pageBusiness";
            this.pageBusiness.Size = new System.Drawing.Size(669, 366);
            this.pageBusiness.Text = "History Business";
            // 
            // BusinessWorkspace
            // 
            this.BusinessWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.BusinessWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BusinessWorkspace.Location = new System.Drawing.Point(0, 0);
            this.BusinessWorkspace.Name = "BusinessWorkspace";
            this.BusinessWorkspace.Size = new System.Drawing.Size(669, 366);
            this.BusinessWorkspace.TabIndex = 12;
            this.BusinessWorkspace.Text = "deckWorkspace3";
            // 
            // pageReport
            // 
            this.pageReport.Controls.Add(this.ReportWorkspace);
            this.pageReport.Name = "pageReport";
            this.pageReport.Size = new System.Drawing.Size(889, 366);
            this.pageReport.Text = "Report";
            // 
            // ReportWorkspace
            // 
            this.ReportWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.ReportWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportWorkspace.Location = new System.Drawing.Point(0, 0);
            this.ReportWorkspace.Name = "ReportWorkspace";
            this.ReportWorkspace.Size = new System.Drawing.Size(889, 366);
            this.ReportWorkspace.TabIndex = 12;
            this.ReportWorkspace.Text = "deckWorkspace4";
            // 
            // ToolBarWorkspace
            // 
            this.ToolBarWorkspace.Dock = System.Windows.Forms.DockStyle.Top;
            this.ToolBarWorkspace.Location = new System.Drawing.Point(0, 0);
            this.ToolBarWorkspace.Name = "ToolBarWorkspace";
            this.ToolBarWorkspace.Size = new System.Drawing.Size(896, 29);
            this.ToolBarWorkspace.TabIndex = 15;
            this.ToolBarWorkspace.Text = "deckWorkspace1";
            // 
            // pageCustomerInfo
            // 
            this.pageCustomerInfo.Name = "pageCustomerInfo";
            this.pageCustomerInfo.Size = new System.Drawing.Size(669, 366);
            // 
            // pageCustoemrArchives
            // 
            this.pageCustoemrArchives.Controls.Add(this.CustoemrArchivesWorkspace);
            this.pageCustoemrArchives.Name = "pageCustoemrArchives";
            this.pageCustoemrArchives.Size = new System.Drawing.Size(669, 366);
            this.pageCustoemrArchives.Text = "Custoemr Archives";
            // 
            // CustoemrArchivesWorkspace
            // 
            this.CustoemrArchivesWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.CustoemrArchivesWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CustoemrArchivesWorkspace.Location = new System.Drawing.Point(0, 0);
            this.CustoemrArchivesWorkspace.Name = "CustoemrArchivesWorkspace";
            this.CustoemrArchivesWorkspace.Size = new System.Drawing.Size(669, 366);
            this.CustoemrArchivesWorkspace.TabIndex = 11;
            this.CustoemrArchivesWorkspace.Text = "deckWorkspace1";
            // 
            // CCMainWorkspace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ToolBarWorkspace);
            this.Name = "CCMainWorkspace";
            this.Size = new System.Drawing.Size(896, 587);
            this.dpSearch.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabMain)).EndInit();
            this.xtraTabMain.ResumeLayout(false);
            this.pagePartner.ResumeLayout(false);
            this.pageBusiness.ResumeLayout(false);
            this.pageReport.ResumeLayout(false);
            this.pageCustoemrArchives.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ListWorkspace;
        private DevExpress.XtraBars.Docking.DockPanel dpSearch;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace SearchWorkspace;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl panel1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ToolBarWorkspace;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTab.XtraTabControl xtraTabMain;
        private DevExpress.XtraTab.XtraTabPage pagePartner;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace PartnerWorkspace;
        private DevExpress.XtraTab.XtraTabPage pageBusiness;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace BusinessWorkspace;
        private DevExpress.XtraTab.XtraTabPage pageReport;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ReportWorkspace;
        private DevExpress.XtraTab.XtraTabPage pageCustoemrArchives;
        private DevExpress.XtraTab.XtraTabPage pageCustomerInfo;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace CustoemrArchivesWorkspace;
    }
}
