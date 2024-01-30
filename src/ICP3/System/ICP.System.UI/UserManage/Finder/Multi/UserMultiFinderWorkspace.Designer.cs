namespace ICP.Sys.UI.UserManage.Finder
{
    partial class UserMultiFinderWorkspace
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
            this.SearchWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.panelContainer1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dpSelected = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.SelectedListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.SelectedToolBarWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dpEdit = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.EditWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.ListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.ToolBarWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dpSearch.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.panelContainer1.SuspendLayout();
            this.dpSelected.SuspendLayout();
            this.dockPanel2_Container.SuspendLayout();
            this.dpEdit.SuspendLayout();
            this.controlContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dpSearch,
            this.panelContainer1});
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
            this.dpSearch.OriginalSize = new System.Drawing.Size(200, 200);
            this.dpSearch.Size = new System.Drawing.Size(200, 473);
            this.dpSearch.Text = "Search";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.SearchWorkspace);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(194, 474);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // SearchWorkspace
            // 
            this.SearchWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchWorkspace.Location = new System.Drawing.Point(0, 0);
            this.SearchWorkspace.Name = "SearchWorkspace";
            this.SearchWorkspace.Size = new System.Drawing.Size(194, 474);
            this.SearchWorkspace.TabIndex = 0;
            this.SearchWorkspace.Text = "deckWorkspace1";
            // 
            // panelContainer1
            // 
            this.panelContainer1.ActiveChild = this.dpSelected;
            this.panelContainer1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.panelContainer1.Appearance.Options.UseBackColor = true;
            this.panelContainer1.Controls.Add(this.dpSelected);
            this.panelContainer1.Controls.Add(this.dpEdit);
            this.panelContainer1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.panelContainer1.FloatVertical = true;
            this.panelContainer1.ID = new System.Guid("26ed686c-ee28-4221-8797-9240b33fb8e0");
            this.panelContainer1.Location = new System.Drawing.Point(200, 194);
            this.panelContainer1.Name = "panelContainer1";
            this.panelContainer1.OriginalSize = new System.Drawing.Size(200, 308);
            this.panelContainer1.Size = new System.Drawing.Size(515, 308);
            this.panelContainer1.Tabbed = true;
            this.panelContainer1.Text = "panelContainer1";
            // 
            // dpSelected
            // 
            this.dpSelected.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.dpSelected.Appearance.Options.UseBackColor = true;
            this.dpSelected.Controls.Add(this.dockPanel2_Container);
            this.dpSelected.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpSelected.FloatVertical = true;
            this.dpSelected.ID = new System.Guid("a8487fe3-8619-46dd-ab77-e2f9fc41c334");
            this.dpSelected.Location = new System.Drawing.Point(3, 25);
            this.dpSelected.Name = "dpSelected";
            this.dpSelected.Options.ShowCloseButton = false;
            this.dpSelected.Options.ShowMaximizeButton = false;
            this.dpSelected.OriginalSize = new System.Drawing.Size(200, 200);
            this.dpSelected.Size = new System.Drawing.Size(509, 257);
            this.dpSelected.Text = "Selected";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.SelectedListWorkspace);
            this.dockPanel2_Container.Controls.Add(this.SelectedToolBarWorkspace);
            this.dockPanel2_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(509, 257);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // SelectedListWorkspace
            // 
            this.SelectedListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectedListWorkspace.Location = new System.Drawing.Point(0, 29);
            this.SelectedListWorkspace.Name = "SelectedListWorkspace";
            this.SelectedListWorkspace.Size = new System.Drawing.Size(509, 228);
            this.SelectedListWorkspace.TabIndex = 18;
            this.SelectedListWorkspace.Text = "deckWorkspace1";
            // 
            // SelectedToolBarWorkspace
            // 
            this.SelectedToolBarWorkspace.Dock = System.Windows.Forms.DockStyle.Top;
            this.SelectedToolBarWorkspace.Location = new System.Drawing.Point(0, 0);
            this.SelectedToolBarWorkspace.Name = "SelectedToolBarWorkspace";
            this.SelectedToolBarWorkspace.Size = new System.Drawing.Size(509, 29);
            this.SelectedToolBarWorkspace.TabIndex = 18;
            this.SelectedToolBarWorkspace.Text = "deckWorkspace1";
            // 
            // dpEdit
            // 
            this.dpEdit.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.dpEdit.Appearance.Options.UseBackColor = true;
            this.dpEdit.Controls.Add(this.controlContainer1);
            this.dpEdit.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpEdit.ID = new System.Guid("168202af-9673-491e-962f-54fa1b788a05");
            this.dpEdit.Location = new System.Drawing.Point(3, 25);
            this.dpEdit.Name = "dpEdit";
            this.dpEdit.OriginalSize = new System.Drawing.Size(200, 200);
            this.dpEdit.Size = new System.Drawing.Size(509, 257);
            this.dpEdit.Text = "Edit";
            // 
            // controlContainer1
            // 
            this.controlContainer1.Controls.Add(this.EditWorkspace);
            this.controlContainer1.Location = new System.Drawing.Point(0, 0);
            this.controlContainer1.Name = "controlContainer1";
            this.controlContainer1.Size = new System.Drawing.Size(509, 257);
            this.controlContainer1.TabIndex = 0;
            // 
            // EditWorkspace
            // 
            this.EditWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditWorkspace.Location = new System.Drawing.Point(0, 0);
            this.EditWorkspace.Name = "EditWorkspace";
            this.EditWorkspace.Size = new System.Drawing.Size(509, 257);
            this.EditWorkspace.TabIndex = 17;
            this.EditWorkspace.Text = "deckWorkspace1";
            // 
            // ListWorkspace
            // 
            this.ListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListWorkspace.Location = new System.Drawing.Point(200, 29);
            this.ListWorkspace.Name = "ListWorkspace";
            this.ListWorkspace.Size = new System.Drawing.Size(515, 165);
            this.ListWorkspace.TabIndex = 9;
            this.ListWorkspace.Text = "deckWorkspace1";
            // 
            // ToolBarWorkspace
            // 
            this.ToolBarWorkspace.Dock = System.Windows.Forms.DockStyle.Top;
            this.ToolBarWorkspace.Location = new System.Drawing.Point(0, 0);
            this.ToolBarWorkspace.Name = "ToolBarWorkspace";
            this.ToolBarWorkspace.Size = new System.Drawing.Size(715, 29);
            this.ToolBarWorkspace.TabIndex = 12;
            this.ToolBarWorkspace.Text = "deckWorkspace1";
            // 
            // UserMultiFinderWorkspace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ListWorkspace);
            this.Controls.Add(this.panelContainer1);
            this.Controls.Add(this.dpSearch);
            this.Controls.Add(this.ToolBarWorkspace);
            this.Name = "UserMultiFinderWorkspace";
            this.Size = new System.Drawing.Size(715, 502);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dpSearch.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.panelContainer1.ResumeLayout(false);
            this.dpSelected.ResumeLayout(false);
            this.dockPanel2_Container.ResumeLayout(false);
            this.dpEdit.ResumeLayout(false);
            this.controlContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dpSelected;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private DevExpress.XtraBars.Docking.DockPanel dpSearch;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace SearchWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ListWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ToolBarWorkspace;
        private DevExpress.XtraBars.Docking.DockPanel dpEdit;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer1;
        private DevExpress.XtraBars.Docking.DockPanel panelContainer1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace SelectedListWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace EditWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace SelectedToolBarWorkspace;
    }
}
