namespace ICP.FRM.UI.InquireRates
{
    partial class InquireRatesMainWorkspace
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
            this.xtabMain = new DevExpress.XtraTab.XtraTabControl();
            this.tabOceanRates = new DevExpress.XtraTab.XtraTabPage();
            this.OceanRatesWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.tabAirRates = new DevExpress.XtraTab.XtraTabPage();
            this.AirRatesWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.tabTruckingRates = new DevExpress.XtraTab.XtraTabPage();
            this.TruckingRatesWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dpSearch = new DevExpress.XtraBars.Docking.DockPanel();
            this.ToolbarWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.controlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtabMain)).BeginInit();
            this.xtabMain.SuspendLayout();
            this.tabOceanRates.SuspendLayout();
            this.tabAirRates.SuspendLayout();
            this.tabTruckingRates.SuspendLayout();
            this.dpSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlContainer1
            // 
            this.controlContainer1.Controls.Add(this.SearchWorkspace);
            this.controlContainer1.Location = new System.Drawing.Point(3, 25);
            this.controlContainer1.Name = "controlContainer1";
            this.controlContainer1.Size = new System.Drawing.Size(165, 573);
            this.controlContainer1.TabIndex = 0;
            // 
            // SearchWorkspace
            // 
            this.SearchWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchWorkspace.Location = new System.Drawing.Point(0, 0);
            this.SearchWorkspace.Name = "SearchWorkspace";
            this.SearchWorkspace.Size = new System.Drawing.Size(165, 573);
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
            this.panel1.Controls.Add(this.xtabMain);
            this.panel1.Controls.Add(this.dpSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 601);
            this.panel1.TabIndex = 5;
            // 
            // xtabMain
            // 
            this.xtabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtabMain.Location = new System.Drawing.Point(171, 0);
            this.xtabMain.Name = "xtabMain";
            this.xtabMain.SelectedTabPage = this.tabOceanRates;
            this.xtabMain.Size = new System.Drawing.Size(829, 601);
            this.xtabMain.TabIndex = 2;
            this.xtabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabOceanRates,
            this.tabAirRates,
            this.tabTruckingRates});
            this.xtabMain.SelectedPageChanging += new DevExpress.XtraTab.TabPageChangingEventHandler(this.xtabMain_SelectedPageChanging);
            this.xtabMain.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtabMain_SelectedPageChanged);
            // 
            // tabOceanRates
            // 
            this.tabOceanRates.Controls.Add(this.OceanRatesWorkspace);
            this.tabOceanRates.Name = "tabOceanRates";
            this.tabOceanRates.Size = new System.Drawing.Size(822, 571);
            this.tabOceanRates.Text = "Ocean Rates";
            // 
            // OceanRatesWorkspace
            // 
            this.OceanRatesWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.OceanRatesWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OceanRatesWorkspace.Location = new System.Drawing.Point(0, 0);
            this.OceanRatesWorkspace.Name = "OceanRatesWorkspace";
            this.OceanRatesWorkspace.Size = new System.Drawing.Size(822, 571);
            this.OceanRatesWorkspace.TabIndex = 3;
            this.OceanRatesWorkspace.Text = "Ocean Rates";
            // 
            // tabAirRates
            // 
            this.tabAirRates.Controls.Add(this.AirRatesWorkspace);
            this.tabAirRates.Name = "tabAirRates";
            this.tabAirRates.Size = new System.Drawing.Size(993, 571);
            this.tabAirRates.Text = "Air Rates";
            // 
            // AirRatesWorkspace
            // 
            this.AirRatesWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.AirRatesWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AirRatesWorkspace.Location = new System.Drawing.Point(0, 0);
            this.AirRatesWorkspace.Name = "AirRatesWorkspace";
            this.AirRatesWorkspace.Size = new System.Drawing.Size(993, 571);
            this.AirRatesWorkspace.TabIndex = 3;
            this.AirRatesWorkspace.Text = "Air Rates";
            // 
            // tabTruckingRates
            // 
            this.tabTruckingRates.Controls.Add(this.TruckingRatesWorkspace);
            this.tabTruckingRates.Name = "tabTruckingRates";
            this.tabTruckingRates.Size = new System.Drawing.Size(993, 571);
            this.tabTruckingRates.Text = "Trucking Rates";
            // 
            // TruckingRatesWorkspace
            // 
            this.TruckingRatesWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.TruckingRatesWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TruckingRatesWorkspace.Location = new System.Drawing.Point(0, 0);
            this.TruckingRatesWorkspace.Name = "TruckingRatesWorkspace";
            this.TruckingRatesWorkspace.Size = new System.Drawing.Size(993, 571);
            this.TruckingRatesWorkspace.TabIndex = 3;
            this.TruckingRatesWorkspace.Text = "Trucking Rates";
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
            this.dpSearch.OriginalSize = new System.Drawing.Size(171, 200);
            this.dpSearch.Size = new System.Drawing.Size(171, 601);
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
            // InquireRatesMainWorkspace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ToolbarWorkspace);
            this.Name = "InquireRatesMainWorkspace";
            this.Size = new System.Drawing.Size(1000, 628);
            this.controlContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtabMain)).EndInit();
            this.xtabMain.ResumeLayout(false);
            this.tabOceanRates.ResumeLayout(false);
            this.tabAirRates.ResumeLayout(false);
            this.tabTruckingRates.ResumeLayout(false);
            this.dpSearch.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.ControlContainer controlContainer1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace SearchWorkspace;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl panel1;
        private DevExpress.XtraBars.Docking.DockPanel dpSearch;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ToolbarWorkspace;
        private DevExpress.XtraTab.XtraTabControl xtabMain;
        private DevExpress.XtraTab.XtraTabPage tabOceanRates;
        private DevExpress.XtraTab.XtraTabPage tabAirRates;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace OceanRatesWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace AirRatesWorkspace;
        private DevExpress.XtraTab.XtraTabPage tabTruckingRates;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace TruckingRatesWorkspace;
    }
}
