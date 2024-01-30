namespace ICP.FAM.UI.BankReceiptList.Finder
{
    partial class SingleFinderWorkspace
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
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dpSearch = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.SingleFinder_Search_BankReceipt = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.SingleFinder_List_BankReceipt = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.SingleFinder_ToolBar_BankReceipt = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dpSearch.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dpSearch});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // dpSearch
            // 
            this.dpSearch.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.dpSearch.Appearance.Options.UseBackColor = true;
            this.dpSearch.Controls.Add(this.dockPanel1_Container);
            this.dpSearch.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dpSearch.ID = new System.Guid("552d2451-1d3e-476a-8374-49b97269da25");
            this.dpSearch.Location = new System.Drawing.Point(0, 29);
            this.dpSearch.Name = "dpSearch";
            this.dpSearch.Options.ShowCloseButton = false;
            this.dpSearch.OriginalSize = new System.Drawing.Size(245, 200);
            this.dpSearch.Size = new System.Drawing.Size(245, 473);
            this.dpSearch.Text = "Search";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.SingleFinder_Search_BankReceipt);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(239, 474);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // SingleFinder_Search_BankReceipt
            // 
            this.SingleFinder_Search_BankReceipt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SingleFinder_Search_BankReceipt.Location = new System.Drawing.Point(0, 0);
            this.SingleFinder_Search_BankReceipt.Name = "SingleFinder_Search_BankReceipt";
            this.SingleFinder_Search_BankReceipt.Size = new System.Drawing.Size(239, 474);
            this.SingleFinder_Search_BankReceipt.TabIndex = 0;
            this.SingleFinder_Search_BankReceipt.Text = "deckWorkspace1";
            // 
            // SingleFinder_List_BankReceipt
            // 
            this.SingleFinder_List_BankReceipt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SingleFinder_List_BankReceipt.Location = new System.Drawing.Point(245, 29);
            this.SingleFinder_List_BankReceipt.Name = "SingleFinder_List_BankReceipt";
            this.SingleFinder_List_BankReceipt.Size = new System.Drawing.Size(470, 473);
            this.SingleFinder_List_BankReceipt.TabIndex = 9;
            this.SingleFinder_List_BankReceipt.Text = "deckWorkspace1";
            // 
            // SingleFinder_ToolBar_BankReceipt
            // 
            this.SingleFinder_ToolBar_BankReceipt.Dock = System.Windows.Forms.DockStyle.Top;
            this.SingleFinder_ToolBar_BankReceipt.Location = new System.Drawing.Point(0, 0);
            this.SingleFinder_ToolBar_BankReceipt.Name = "SingleFinder_ToolBar_BankReceipt";
            this.SingleFinder_ToolBar_BankReceipt.Size = new System.Drawing.Size(715, 29);
            this.SingleFinder_ToolBar_BankReceipt.TabIndex = 12;
            this.SingleFinder_ToolBar_BankReceipt.Text = "deckWorkspace1";
            // 
            // SingleFinderWorkspace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SingleFinder_List_BankReceipt);
            this.Controls.Add(this.dpSearch);
            this.Controls.Add(this.SingleFinder_ToolBar_BankReceipt);
            this.Name = "SingleFinderWorkspace";
            this.Size = new System.Drawing.Size(715, 502);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dpSearch.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dpSearch;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace SingleFinder_Search_BankReceipt;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace SingleFinder_List_BankReceipt;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace SingleFinder_ToolBar_BankReceipt;
    }
}
