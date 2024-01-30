namespace ICP.FRM.UI.InquireRates
{
    partial class InquireOceanRatesMainWorkspace
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
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.panel1 = new ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl(this.components);
            this.ListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.mainPanel = new DevExpress.XtraBars.Docking.DockPanel();
            this.dpFaxEmailEDI = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.CommunicationHistoryWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dpHistory = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.HistoryWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.HistoryToolBarWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dpGeneralInfo = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer2 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.GeneralInfoWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dpShipment = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer3 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.ShipmentWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.panel1.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.dpFaxEmailEDI.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.dpHistory.SuspendLayout();
            this.controlContainer1.SuspendLayout();
            this.dpGeneralInfo.SuspendLayout();
            this.controlContainer2.SuspendLayout();
            this.dpShipment.SuspendLayout();
            this.controlContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this.panel1;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.mainPanel});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ListWorkspace);
            this.panel1.Controls.Add(this.mainPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(731, 556);
            this.panel1.TabIndex = 5;
            // 
            // ListWorkspace
            // 
            this.ListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListWorkspace.Location = new System.Drawing.Point(0, 0);
            this.ListWorkspace.Name = "ListWorkspace";
            this.ListWorkspace.Size = new System.Drawing.Size(731, 222);
            this.ListWorkspace.TabIndex = 5;
            this.ListWorkspace.Text = "deckWorkspace4";
            // 
            // mainPanel
            // 
            this.mainPanel.ActiveChild = this.dpHistory;
            this.mainPanel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.mainPanel.Appearance.Options.UseBackColor = true;
            this.mainPanel.Controls.Add(this.dpFaxEmailEDI);
            this.mainPanel.Controls.Add(this.dpHistory);
            this.mainPanel.Controls.Add(this.dpGeneralInfo);
            this.mainPanel.Controls.Add(this.dpShipment);
            this.mainPanel.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.mainPanel.FloatVertical = true;
            this.mainPanel.ID = new System.Guid("e7e736cc-b941-4fcd-a667-9894b13f320b");
            this.mainPanel.Location = new System.Drawing.Point(0, 222);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.OriginalSize = new System.Drawing.Size(200, 334);
            this.mainPanel.Size = new System.Drawing.Size(731, 334);
            this.mainPanel.Tabbed = true;
            this.mainPanel.Text = "panelContainer1";
            // 
            // dpFaxEmailEDI
            // 
            this.dpFaxEmailEDI.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.dpFaxEmailEDI.Appearance.Options.UseBackColor = true;
            this.dpFaxEmailEDI.Controls.Add(this.dockPanel1_Container);
            this.dpFaxEmailEDI.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpFaxEmailEDI.FloatSize = new System.Drawing.Size(800, 300);
            this.dpFaxEmailEDI.ID = new System.Guid("7237983f-e9d3-498a-b3e0-5a0f4ec86e15");
            this.dpFaxEmailEDI.Location = new System.Drawing.Point(3, 25);
            this.dpFaxEmailEDI.Name = "dpFaxEmailEDI";
            this.dpFaxEmailEDI.OriginalSize = new System.Drawing.Size(725, 283);
            this.dpFaxEmailEDI.Size = new System.Drawing.Size(725, 283);
            this.dpFaxEmailEDI.Text = "Mail";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.CommunicationHistoryWorkspace);
            this.dockPanel1_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(725, 283);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // CommunicationHistoryWorkspace
            // 
            this.CommunicationHistoryWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.CommunicationHistoryWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CommunicationHistoryWorkspace.Location = new System.Drawing.Point(0, 0);
            this.CommunicationHistoryWorkspace.Name = "CommunicationHistoryWorkspace";
            this.CommunicationHistoryWorkspace.Size = new System.Drawing.Size(725, 283);
            this.CommunicationHistoryWorkspace.TabIndex = 3;
            this.CommunicationHistoryWorkspace.Text = "Ocean Rates";
            // 
            // dpHistory
            // 
            this.dpHistory.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.dpHistory.Appearance.Options.UseBackColor = true;
            this.dpHistory.Controls.Add(this.controlContainer1);
            this.dpHistory.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpHistory.FloatSize = new System.Drawing.Size(800, 300);
            this.dpHistory.ID = new System.Guid("0144c350-9ea7-46e2-9bfd-123942d7caea");
            this.dpHistory.Location = new System.Drawing.Point(3, 25);
            this.dpHistory.Name = "dpHistory";
            this.dpHistory.OriginalSize = new System.Drawing.Size(725, 283);
            this.dpHistory.Size = new System.Drawing.Size(725, 283);
            this.dpHistory.Text = "History";
            // 
            // controlContainer1
            // 
            this.controlContainer1.Controls.Add(this.HistoryWorkspace);
            this.controlContainer1.Controls.Add(this.HistoryToolBarWorkspace);
            this.controlContainer1.Location = new System.Drawing.Point(0, 0);
            this.controlContainer1.Name = "controlContainer1";
            this.controlContainer1.Size = new System.Drawing.Size(725, 283);
            this.controlContainer1.TabIndex = 0;
            // 
            // HistoryWorkspace
            // 
            this.HistoryWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HistoryWorkspace.Location = new System.Drawing.Point(0, 27);
            this.HistoryWorkspace.Name = "HistoryWorkspace";
            this.HistoryWorkspace.Size = new System.Drawing.Size(725, 256);
            this.HistoryWorkspace.TabIndex = 9;
            this.HistoryWorkspace.Text = "Ocean Rates";
            // 
            // HistoryToolBarWorkspace
            // 
            this.HistoryToolBarWorkspace.Dock = System.Windows.Forms.DockStyle.Top;
            this.HistoryToolBarWorkspace.Location = new System.Drawing.Point(0, 0);
            this.HistoryToolBarWorkspace.Name = "HistoryToolBarWorkspace";
            this.HistoryToolBarWorkspace.Size = new System.Drawing.Size(725, 27);
            this.HistoryToolBarWorkspace.TabIndex = 8;
            this.HistoryToolBarWorkspace.Text = "deckWorkspace1";
            // 
            // dpGeneralInfo
            // 
            this.dpGeneralInfo.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.dpGeneralInfo.Appearance.Options.UseBackColor = true;
            this.dpGeneralInfo.Controls.Add(this.controlContainer2);
            this.dpGeneralInfo.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpGeneralInfo.FloatSize = new System.Drawing.Size(800, 300);
            this.dpGeneralInfo.ID = new System.Guid("3f41803e-4337-404a-883d-d0fee4039585");
            this.dpGeneralInfo.Location = new System.Drawing.Point(3, 25);
            this.dpGeneralInfo.Name = "dpGeneralInfo";
            this.dpGeneralInfo.OriginalSize = new System.Drawing.Size(725, 283);
            this.dpGeneralInfo.Size = new System.Drawing.Size(725, 283);
            this.dpGeneralInfo.Text = "General Info";
            // 
            // controlContainer2
            // 
            this.controlContainer2.Controls.Add(this.GeneralInfoWorkspace);
            this.controlContainer2.Location = new System.Drawing.Point(0, 0);
            this.controlContainer2.Name = "controlContainer2";
            this.controlContainer2.Size = new System.Drawing.Size(725, 283);
            this.controlContainer2.TabIndex = 0;
            // 
            // GeneralInfoWorkspace
            // 
            this.GeneralInfoWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.GeneralInfoWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GeneralInfoWorkspace.Location = new System.Drawing.Point(0, 0);
            this.GeneralInfoWorkspace.Name = "GeneralInfoWorkspace";
            this.GeneralInfoWorkspace.Size = new System.Drawing.Size(725, 283);
            this.GeneralInfoWorkspace.TabIndex = 5;
            this.GeneralInfoWorkspace.Text = "Ocean Rates";
            // 
            // dpShipment
            // 
            this.dpShipment.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.dpShipment.Appearance.Options.UseBackColor = true;
            this.dpShipment.Controls.Add(this.controlContainer3);
            this.dpShipment.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpShipment.FloatSize = new System.Drawing.Size(800, 300);
            this.dpShipment.ID = new System.Guid("92cd06fb-5e52-49b6-b4f7-e1fdf2544c88");
            this.dpShipment.Location = new System.Drawing.Point(3, 25);
            this.dpShipment.Name = "dpShipment";
            this.dpShipment.OriginalSize = new System.Drawing.Size(725, 283);
            this.dpShipment.Size = new System.Drawing.Size(725, 283);
            this.dpShipment.Text = "Shipment";
            // 
            // controlContainer3
            // 
            this.controlContainer3.Controls.Add(this.ShipmentWorkspace);
            this.controlContainer3.Location = new System.Drawing.Point(0, 0);
            this.controlContainer3.Name = "controlContainer3";
            this.controlContainer3.Size = new System.Drawing.Size(725, 283);
            this.controlContainer3.TabIndex = 0;
            // 
            // ShipmentWorkspace
            // 
            this.ShipmentWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ShipmentWorkspace.Location = new System.Drawing.Point(0, 0);
            this.ShipmentWorkspace.Name = "ShipmentWorkspace";
            this.ShipmentWorkspace.Size = new System.Drawing.Size(725, 283);
            this.ShipmentWorkspace.TabIndex = 5;
            this.ShipmentWorkspace.Text = "Ocean Rates";
            // 
            // InquireOceanRatesMainWorkspace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "InquireOceanRatesMainWorkspace";
            this.Size = new System.Drawing.Size(731, 556);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.dpFaxEmailEDI.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.dpHistory.ResumeLayout(false);
            this.controlContainer1.ResumeLayout(false);
            this.dpGeneralInfo.ResumeLayout(false);
            this.controlContainer2.ResumeLayout(false);
            this.dpShipment.ResumeLayout(false);
            this.controlContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl panel1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ListWorkspace;
        private DevExpress.XtraBars.Docking.DockPanel dpFaxEmailEDI;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraBars.Docking.DockPanel mainPanel;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace CommunicationHistoryWorkspace;
        private DevExpress.XtraBars.Docking.DockPanel dpHistory;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace HistoryWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace HistoryToolBarWorkspace;
        private DevExpress.XtraBars.Docking.DockPanel dpGeneralInfo;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer2;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace GeneralInfoWorkspace;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer3;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ShipmentWorkspace;
        public DevExpress.XtraBars.Docking.DockManager dockManager1;
        public DevExpress.XtraBars.Docking.DockPanel dpShipment;
    }
}
