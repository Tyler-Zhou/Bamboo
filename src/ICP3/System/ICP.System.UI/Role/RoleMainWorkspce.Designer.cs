﻿namespace ICP.Sys.UI.Role
{
    partial class RoleMainWorkspace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoleMainWorkspace));
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.layoutContainerControl1 = new ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl(this.components);
            this.ListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.panelContainer1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dpEdit = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.EditWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dpOrgJob = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.OrgJobWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dpFunction = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer2 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.FunctionWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dpSearch = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.SearchWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.ToolBarWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.layoutContainerControl1.SuspendLayout();
            this.panelContainer1.SuspendLayout();
            this.dpEdit.SuspendLayout();
            this.dockPanel2_Container.SuspendLayout();
            this.dpOrgJob.SuspendLayout();
            this.controlContainer1.SuspendLayout();
            this.dpFunction.SuspendLayout();
            this.controlContainer2.SuspendLayout();
            this.dpSearch.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this.layoutContainerControl1;
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
            // layoutContainerControl1
            // 
            this.layoutContainerControl1.Controls.Add(this.ListWorkspace);
            this.layoutContainerControl1.Controls.Add(this.panelContainer1);
            this.layoutContainerControl1.Controls.Add(this.dpSearch);
            this.layoutContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutContainerControl1.Location = new System.Drawing.Point(0, 29);
            this.layoutContainerControl1.Name = "layoutContainerControl1";
            this.layoutContainerControl1.Size = new System.Drawing.Size(715, 473);
            this.layoutContainerControl1.TabIndex = 15;
            this.layoutContainerControl1.Text = "layoutContainerControl1";
            // 
            // ListWorkspace
            // 
            this.ListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListWorkspace.Location = new System.Drawing.Point(220, 0);
            this.ListWorkspace.Name = "ListWorkspace";
            this.ListWorkspace.Size = new System.Drawing.Size(495, 165);
            this.ListWorkspace.TabIndex = 9;
            this.ListWorkspace.Text = "deckWorkspace1";
            // 
            // panelContainer1
            // 
            this.panelContainer1.ActiveChild = this.dpEdit;
            this.panelContainer1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.panelContainer1.Appearance.Options.UseBackColor = true;
            this.panelContainer1.Controls.Add(this.dpEdit);
            this.panelContainer1.Controls.Add(this.dpOrgJob);
            this.panelContainer1.Controls.Add(this.dpFunction);
            this.panelContainer1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.panelContainer1.FloatVertical = true;
            this.panelContainer1.ID = new System.Guid("988eae6a-c679-4ff4-b166-bb1937c8b8eb");
            this.panelContainer1.Location = new System.Drawing.Point(220, 165);
            this.panelContainer1.Name = "panelContainer1";
            this.panelContainer1.OriginalSize = new System.Drawing.Size(200, 308);
            this.panelContainer1.Size = new System.Drawing.Size(495, 308);
            this.panelContainer1.Tabbed = true;
            this.panelContainer1.Text = "panelContainer1";
            // 
            // dpEdit
            // 
            this.dpEdit.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.dpEdit.Appearance.Options.UseBackColor = true;
            this.dpEdit.Controls.Add(this.dockPanel2_Container);
            this.dpEdit.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpEdit.FloatVertical = true;
            this.dpEdit.ID = new System.Guid("a8487fe3-8619-46dd-ab77-e2f9fc41c334");
            this.dpEdit.Location = new System.Drawing.Point(3, 25);
            this.dpEdit.Name = "dpEdit";
            this.dpEdit.Options.ShowCloseButton = false;
            this.dpEdit.Options.ShowMaximizeButton = false;
            this.dpEdit.OriginalSize = new System.Drawing.Size(509, 257);
            this.dpEdit.Size = new System.Drawing.Size(489, 257);
            this.dpEdit.Text = "Edit";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.EditWorkspace);
            this.dockPanel2_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(489, 257);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // EditWorkspace
            // 
            this.EditWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditWorkspace.Location = new System.Drawing.Point(0, 0);
            this.EditWorkspace.Name = "EditWorkspace";
            this.EditWorkspace.Size = new System.Drawing.Size(489, 257);
            this.EditWorkspace.TabIndex = 0;
            this.EditWorkspace.Text = "deckWorkspace1";
            // 
            // dpOrgJob
            // 
            this.dpOrgJob.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.dpOrgJob.Appearance.Options.UseBackColor = true;
            this.dpOrgJob.Controls.Add(this.controlContainer1);
            this.dpOrgJob.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpOrgJob.ID = new System.Guid("3b24f12f-fa5f-48ca-b92d-4158396b5cac");
            this.dpOrgJob.Location = new System.Drawing.Point(3, 25);
            this.dpOrgJob.Name = "dpOrgJob";
            this.dpOrgJob.OriginalSize = new System.Drawing.Size(509, 257);
            this.dpOrgJob.Size = new System.Drawing.Size(489, 257);
            this.dpOrgJob.Text = "Job";
            // 
            // controlContainer1
            // 
            this.controlContainer1.Controls.Add(this.OrgJobWorkspace);
            this.controlContainer1.Location = new System.Drawing.Point(0, 0);
            this.controlContainer1.Name = "controlContainer1";
            this.controlContainer1.Size = new System.Drawing.Size(489, 257);
            this.controlContainer1.TabIndex = 0;
            // 
            // OrgJobWorkspace
            // 
            this.OrgJobWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OrgJobWorkspace.Location = new System.Drawing.Point(0, 0);
            this.OrgJobWorkspace.Name = "OrgJobWorkspace";
            this.OrgJobWorkspace.Size = new System.Drawing.Size(489, 257);
            this.OrgJobWorkspace.TabIndex = 16;
            // 
            // dpFunction
            // 
            this.dpFunction.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.dpFunction.Appearance.Options.UseBackColor = true;
            this.dpFunction.Controls.Add(this.controlContainer2);
            this.dpFunction.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpFunction.ID = new System.Guid("e2a529f0-19d8-48c1-bed0-d1e61abc8f9f");
            this.dpFunction.Location = new System.Drawing.Point(3, 25);
            this.dpFunction.Name = "dpFunction";
            this.dpFunction.OriginalSize = new System.Drawing.Size(509, 257);
            this.dpFunction.Size = new System.Drawing.Size(489, 257);
            this.dpFunction.Text = "Function";
            // 
            // controlContainer2
            // 
            this.controlContainer2.Controls.Add(this.FunctionWorkspace);
            this.controlContainer2.Location = new System.Drawing.Point(0, 0);
            this.controlContainer2.Name = "controlContainer2";
            this.controlContainer2.Size = new System.Drawing.Size(489, 257);
            this.controlContainer2.TabIndex = 0;
            // 
            // FunctionWorkspace
            // 
            this.FunctionWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FunctionWorkspace.Location = new System.Drawing.Point(0, 0);
            this.FunctionWorkspace.Name = "FunctionWorkspace";
            this.FunctionWorkspace.Size = new System.Drawing.Size(489, 257);
            this.FunctionWorkspace.TabIndex = 17;
            // 
            // dpSearch
            // 
            this.dpSearch.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.dpSearch.Appearance.Options.UseBackColor = true;
            this.dpSearch.Controls.Add(this.dockPanel1_Container);
            this.dpSearch.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dpSearch.ID = new System.Guid("552d2451-1d3e-476a-8374-49b97269da25");
            this.dpSearch.Location = new System.Drawing.Point(0, 0);
            this.dpSearch.Name = "dpSearch";
            this.dpSearch.Options.ShowCloseButton = false;
            this.dpSearch.OriginalSize = new System.Drawing.Size(220, 200);
            this.dpSearch.Size = new System.Drawing.Size(220, 473);
            this.dpSearch.Text = "Search";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.SearchWorkspace);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(214, 445);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // SearchWorkspace
            // 
            this.SearchWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchWorkspace.Location = new System.Drawing.Point(0, 0);
            this.SearchWorkspace.Name = "SearchWorkspace";
            this.SearchWorkspace.Size = new System.Drawing.Size(214, 445);
            this.SearchWorkspace.TabIndex = 0;
            this.SearchWorkspace.Text = "deckWorkspace1";
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
            // RoleMainWorkspace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutContainerControl1);
            this.Controls.Add(this.ToolBarWorkspace);
            this.Name = "RoleMainWorkspace";
            this.Size = new System.Drawing.Size(715, 502);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.layoutContainerControl1.ResumeLayout(false);
            this.panelContainer1.ResumeLayout(false);
            this.dpEdit.ResumeLayout(false);
            this.dockPanel2_Container.ResumeLayout(false);
            this.dpOrgJob.ResumeLayout(false);
            this.controlContainer1.ResumeLayout(false);
            this.dpFunction.ResumeLayout(false);
            this.controlContainer2.ResumeLayout(false);
            this.dpSearch.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dpEdit;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace EditWorkspace;
        private DevExpress.XtraBars.Docking.DockPanel dpSearch;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace SearchWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ListWorkspace;
        private DevExpress.XtraBars.Docking.DockPanel panelContainer1;
        private DevExpress.XtraBars.Docking.DockPanel dpOrgJob;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace OrgJobWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ToolBarWorkspace;
        private DevExpress.XtraBars.Docking.DockPanel dpFunction;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer2;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace FunctionWorkspace;
        private ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl layoutContainerControl1;
    }
}