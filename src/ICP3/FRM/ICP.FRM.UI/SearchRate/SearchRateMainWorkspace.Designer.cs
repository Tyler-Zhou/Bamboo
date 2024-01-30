namespace ICP.FRM.UI.SearchRate
{
    partial class SearchRateMainWorkspace
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
            this.xtraTabWorkspace1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabOceanRates = new DevExpress.XtraTab.XtraTabPage();
            this.OceanMainWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.tabAirRates = new DevExpress.XtraTab.XtraTabPage();
            this.AirMainWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.tabTruckRates = new DevExpress.XtraTab.XtraTabPage();
            this.TruckMainWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.infoProvider = new Microsoft.Practices.CompositeUI.SmartParts.SmartPartInfoProvider();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabWorkspace1)).BeginInit();
            this.xtraTabWorkspace1.SuspendLayout();
            this.tabOceanRates.SuspendLayout();
            this.tabAirRates.SuspendLayout();
            this.tabTruckRates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabWorkspace1
            // 
            this.xtraTabWorkspace1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabWorkspace1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabWorkspace1.Name = "xtraTabWorkspace1";
            this.xtraTabWorkspace1.SelectedTabPage = this.tabOceanRates;
            this.xtraTabWorkspace1.Size = new System.Drawing.Size(676, 628);
            this.xtraTabWorkspace1.TabIndex = 1;
            this.xtraTabWorkspace1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabOceanRates,
            this.tabAirRates,
            this.tabTruckRates});
            this.xtraTabWorkspace1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabWorkspace1_SelectedPageChanged);
            // 
            // tabOceanRates
            // 
            this.tabOceanRates.Controls.Add(this.OceanMainWorkspace);
            this.tabOceanRates.Name = "tabOceanRates";
            this.tabOceanRates.Size = new System.Drawing.Size(669, 598);
            this.tabOceanRates.Text = "Search Ocean Rates";
            // 
            // OceanMainWorkspace
            // 
            this.OceanMainWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.OceanMainWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OceanMainWorkspace.Location = new System.Drawing.Point(0, 0);
            this.OceanMainWorkspace.Name = "OceanMainWorkspace";
            this.OceanMainWorkspace.Size = new System.Drawing.Size(669, 598);
            this.OceanMainWorkspace.TabIndex = 3;
            this.OceanMainWorkspace.Text = "OceanMainWorkspace";
            // 
            // tabAirRates
            // 
            this.tabAirRates.Controls.Add(this.AirMainWorkspace);
            this.tabAirRates.Name = "tabAirRates";
            this.tabAirRates.Size = new System.Drawing.Size(669, 598);
            this.tabAirRates.Text = "Search Air Rates";
            // 
            // AirMainWorkspace
            // 
            this.AirMainWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.AirMainWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AirMainWorkspace.Location = new System.Drawing.Point(0, 0);
            this.AirMainWorkspace.Name = "AirMainWorkspace";
            this.AirMainWorkspace.Size = new System.Drawing.Size(669, 598);
            this.AirMainWorkspace.TabIndex = 4;
            // 
            // tabTruckRates
            // 
            this.tabTruckRates.Controls.Add(this.TruckMainWorkspace);
            this.tabTruckRates.Name = "tabTruckRates";
            this.tabTruckRates.Size = new System.Drawing.Size(669, 598);
            this.tabTruckRates.Text = "Search Truck Rates";
            // 
            // TruckMainWorkspace
            // 
            this.TruckMainWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.TruckMainWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TruckMainWorkspace.Location = new System.Drawing.Point(0, 0);
            this.TruckMainWorkspace.Name = "TruckMainWorkspace";
            this.TruckMainWorkspace.Size = new System.Drawing.Size(669, 598);
            this.TruckMainWorkspace.TabIndex = 4;
            // 
            // dockManager1
            // 
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // SearchRateMainWorkspace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabWorkspace1);
            this.Name = "SearchRateMainWorkspace";
            this.Size = new System.Drawing.Size(676, 628);
            this.Controls.SetChildIndex(this.xtraTabWorkspace1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabWorkspace1)).EndInit();
            this.xtraTabWorkspace1.ResumeLayout(false);
            this.tabOceanRates.ResumeLayout(false);
            this.tabAirRates.ResumeLayout(false);
            this.tabTruckRates.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabWorkspace1;
        private DevExpress.XtraTab.XtraTabPage tabOceanRates;
        private DevExpress.XtraTab.XtraTabPage tabAirRates;
        private DevExpress.XtraTab.XtraTabPage tabTruckRates;
        private Microsoft.Practices.CompositeUI.SmartParts.SmartPartInfoProvider infoProvider;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace OceanMainWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace AirMainWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace TruckMainWorkspace;

    }
}
