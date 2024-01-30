namespace ICP.Sys.UI.Job
{
    partial class JobMainWorkspace
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
            this.dpOrganization = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.OrganizationWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dpEdit = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.EditWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.bsMainList = new System.Windows.Forms.BindingSource(this.components);
            this.ListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dpOrganization.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.dpEdit.SuspendLayout();
            this.dockPanel2_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsMainList)).BeginInit();
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dpOrganization,
            this.dpEdit});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // dpOrganization
            // 
            this.dpOrganization.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.dpOrganization.Appearance.Options.UseBackColor = true;
            this.dpOrganization.Controls.Add(this.dockPanel1_Container);
            this.dpOrganization.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dpOrganization.ID = new System.Guid("552d2451-1d3e-476a-8374-49b97269da25");
            this.dpOrganization.Location = new System.Drawing.Point(515, 0);
            this.dpOrganization.Name = "dpOrganization";
            this.dpOrganization.Options.ShowCloseButton = false;
            this.dpOrganization.OriginalSize = new System.Drawing.Size(200, 200);
            this.dpOrganization.Size = new System.Drawing.Size(200, 502);
            this.dpOrganization.Text = "Organization";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.OrganizationWorkspace);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(194, 474);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // OrganizationWorkspace
            // 
            this.OrganizationWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OrganizationWorkspace.Location = new System.Drawing.Point(0, 0);
            this.OrganizationWorkspace.Name = "OrganizationWorkspace";
            this.OrganizationWorkspace.Size = new System.Drawing.Size(194, 474);
            this.OrganizationWorkspace.TabIndex = 0;
            this.OrganizationWorkspace.Text = "deckWorkspace1";
            // 
            // dpEdit
            // 
            this.dpEdit.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.dpEdit.Appearance.Options.UseBackColor = true;
            this.dpEdit.Controls.Add(this.dockPanel2_Container);
            this.dpEdit.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dpEdit.FloatVertical = true;
            this.dpEdit.ID = new System.Guid("a8487fe3-8619-46dd-ab77-e2f9fc41c334");
            this.dpEdit.Location = new System.Drawing.Point(0, 194);
            this.dpEdit.Name = "dpEdit";
            this.dpEdit.Options.ShowCloseButton = false;
            this.dpEdit.Options.ShowMaximizeButton = false;
            this.dpEdit.OriginalSize = new System.Drawing.Size(200, 308);
            this.dpEdit.Size = new System.Drawing.Size(515, 308);
            this.dpEdit.Text = "Edit";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.EditWorkspace);
            this.dockPanel2_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(509, 280);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // EditWorkspace
            // 
            this.EditWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditWorkspace.Location = new System.Drawing.Point(0, 0);
            this.EditWorkspace.Name = "EditWorkspace";
            this.EditWorkspace.Size = new System.Drawing.Size(509, 280);
            this.EditWorkspace.TabIndex = 0;
            this.EditWorkspace.Text = "deckWorkspace1";
            // 
            // ListWorkspace
            // 
            this.ListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListWorkspace.Location = new System.Drawing.Point(0, 0);
            this.ListWorkspace.Name = "ListWorkspace";
            this.ListWorkspace.Size = new System.Drawing.Size(515, 194);
            this.ListWorkspace.TabIndex = 9;
            this.ListWorkspace.Text = "deckWorkspace1";
            // 
            // JobMainWorkspace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ListWorkspace);
            this.Controls.Add(this.dpEdit);
            this.Controls.Add(this.dpOrganization);
            this.Name = "JobMainWorkspace";
            this.Size = new System.Drawing.Size(715, 502);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dpOrganization.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.dpEdit.ResumeLayout(false);
            this.dockPanel2_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsMainList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dpEdit;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace EditWorkspace;
        private DevExpress.XtraBars.Docking.DockPanel dpOrganization;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace OrganizationWorkspace;
        private System.Windows.Forms.BindingSource bsMainList;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ListWorkspace;
    }
}
