namespace ICP.Sys.UI.UserManage.Finder
{
    partial class UserSingleFinderWorkspace
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
            this.dpEdit = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.EditWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.ListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.ToolBarWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dpSearch.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.dpEdit.SuspendLayout();
            this.controlContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dpSearch,
            this.dpEdit});
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
            // dpEdit
            // 
            this.dpEdit.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.dpEdit.Appearance.Options.UseBackColor = true;
            this.dpEdit.Controls.Add(this.controlContainer1);
            this.dpEdit.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dpEdit.ID = new System.Guid("83e26f8e-e427-4ac0-a439-066f0f8a75ce");
            this.dpEdit.Location = new System.Drawing.Point(200, 219);
            this.dpEdit.Name = "dpEdit";
            this.dpEdit.OriginalSize = new System.Drawing.Size(200, 283);
            this.dpEdit.Size = new System.Drawing.Size(515, 283);
            this.dpEdit.Text = "Edit";
            // 
            // controlContainer1
            // 
            this.controlContainer1.Controls.Add(this.EditWorkspace);
            this.controlContainer1.Location = new System.Drawing.Point(3, 25);
            this.controlContainer1.Name = "controlContainer1";
            this.controlContainer1.Size = new System.Drawing.Size(509, 255);
            this.controlContainer1.TabIndex = 0;
            // 
            // EditWorkspace
            // 
            this.EditWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditWorkspace.Location = new System.Drawing.Point(0, 0);
            this.EditWorkspace.Name = "EditWorkspace";
            this.EditWorkspace.Size = new System.Drawing.Size(509, 255);
            this.EditWorkspace.TabIndex = 15;
            this.EditWorkspace.Text = "deckWorkspace1";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(509, 280);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // ListWorkspace
            // 
            this.ListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListWorkspace.Location = new System.Drawing.Point(200, 29);
            this.ListWorkspace.Name = "ListWorkspace";
            this.ListWorkspace.Size = new System.Drawing.Size(515, 190);
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
            // UserSingleFinderWorkspace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ListWorkspace);
            this.Controls.Add(this.dpEdit);
            this.Controls.Add(this.dpSearch);
            this.Controls.Add(this.ToolBarWorkspace);
            this.Name = "UserSingleFinderWorkspace";
            this.Size = new System.Drawing.Size(715, 502);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dpSearch.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.dpEdit.ResumeLayout(false);
            this.controlContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private DevExpress.XtraBars.Docking.DockPanel dpSearch;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace SearchWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ListWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ToolBarWorkspace;
        private DevExpress.XtraBars.Docking.DockPanel dpEdit;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace EditWorkspace;
    }
}
