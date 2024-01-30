namespace ICP.OA.UI.Document
{
    partial class DocMainWorkspce
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
            this.MainViewWorkspce = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.panelContainer1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dpJob = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.JobWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dpUser = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.UserWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.panelContainer1.SuspendLayout();
            this.dpJob.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.dpUser.SuspendLayout();
            this.dockPanel2_Container.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainViewWorkspce
            // 
            this.MainViewWorkspce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainViewWorkspce.Location = new System.Drawing.Point(0, 0);
            this.MainViewWorkspce.Name = "MainViewWorkspce";
            this.MainViewWorkspce.Size = new System.Drawing.Size(506, 287);
            this.MainViewWorkspce.TabIndex = 10;
            this.MainViewWorkspce.Text = "deckWorkspace1";
            // 
            // dockManager1
            // 
            this.dockManager1.DockingOptions.ShowCloseButton = false;
            this.dockManager1.DockingOptions.ShowMaximizeButton = false;
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.panelContainer1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // panelContainer1
            // 
            this.panelContainer1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.panelContainer1.Appearance.Options.UseBackColor = true;
            this.panelContainer1.Controls.Add(this.dpJob);
            this.panelContainer1.Controls.Add(this.dpUser);
            this.panelContainer1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.panelContainer1.FloatVertical = true;
            this.panelContainer1.ID = new System.Guid("e7438aef-b128-4f41-b912-44421e226a1b");
            this.panelContainer1.Location = new System.Drawing.Point(0, 287);
            this.panelContainer1.Name = "panelContainer1";
            this.panelContainer1.OriginalSize = new System.Drawing.Size(200, 213);
            this.panelContainer1.Size = new System.Drawing.Size(506, 213);
            this.panelContainer1.Text = "panelContainer1";
            // 
            // dpJob
            // 
            this.dpJob.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.dpJob.Appearance.Options.UseBackColor = true;
            this.dpJob.Controls.Add(this.dockPanel1_Container);
            this.dpJob.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpJob.ID = new System.Guid("f951ec92-e278-478e-b60f-00631a26ab4f");
            this.dpJob.Location = new System.Drawing.Point(0, 0);
            this.dpJob.Name = "dpJob";
            this.dpJob.OriginalSize = new System.Drawing.Size(254, 200);
            this.dpJob.Size = new System.Drawing.Size(254, 213);
            this.dpJob.Text = "Job";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.JobWorkspace);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(248, 185);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // JobWorkspace
            // 
            this.JobWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.JobWorkspace.Location = new System.Drawing.Point(0, 0);
            this.JobWorkspace.Name = "JobWorkspace";
            this.JobWorkspace.Size = new System.Drawing.Size(248, 185);
            this.JobWorkspace.TabIndex = 13;
            this.JobWorkspace.Text = "deckWorkspace1";
            // 
            // dpUser
            // 
            this.dpUser.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.dpUser.Appearance.Options.UseBackColor = true;
            this.dpUser.Controls.Add(this.dockPanel2_Container);
            this.dpUser.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpUser.FloatVertical = true;
            this.dpUser.ID = new System.Guid("dfef1167-8c44-4b2b-8237-306d4ef1c1fa");
            this.dpUser.Location = new System.Drawing.Point(254, 0);
            this.dpUser.Name = "dpUser";
            this.dpUser.OriginalSize = new System.Drawing.Size(252, 200);
            this.dpUser.Size = new System.Drawing.Size(252, 213);
            this.dpUser.Text = "User";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.UserWorkspace);
            this.dockPanel2_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(246, 185);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // UserWorkspace
            // 
            this.UserWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserWorkspace.Location = new System.Drawing.Point(0, 0);
            this.UserWorkspace.Name = "UserWorkspace";
            this.UserWorkspace.Size = new System.Drawing.Size(246, 185);
            this.UserWorkspace.TabIndex = 13;
            this.UserWorkspace.Text = "deckWorkspace1";
            // 
            // DocMainWorkspce
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainViewWorkspce);
            this.Controls.Add(this.panelContainer1);
            this.Name = "DocMainWorkspce";
            this.Size = new System.Drawing.Size(506, 500);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.panelContainer1.ResumeLayout(false);
            this.dpJob.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.dpUser.ResumeLayout(false);
            this.dockPanel2_Container.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace MainViewWorkspce;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dpJob;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraBars.Docking.DockPanel dpUser;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace JobWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace UserWorkspace;
        private DevExpress.XtraBars.Docking.DockPanel panelContainer1;
    }
}
