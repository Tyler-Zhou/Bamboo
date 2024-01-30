﻿namespace ICP.FAM.UI
{
    partial class AgentBillCheckingMainWorkSpace
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
            this.panel1 = new ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl(this.components);
            this.layoutContainerControl1 = new ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl(this.components);
            this.layoutContainerControl2 = new ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl(this.components);
            this.ListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dpSearch = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.SearchWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.ToolbarWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.panel1.SuspendLayout();
            this.layoutContainerControl1.SuspendLayout();
            this.layoutContainerControl2.SuspendLayout();
            this.dpSearch.SuspendLayout();
            this.controlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.layoutContainerControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(731, 556);
            this.panel1.TabIndex = 5;
            // 
            // layoutContainerControl1
            // 
            this.layoutContainerControl1.Controls.Add(this.layoutContainerControl2);
            this.layoutContainerControl1.Controls.Add(this.ToolbarWorkspace);
            this.layoutContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutContainerControl1.Name = "layoutContainerControl1";
            this.layoutContainerControl1.Size = new System.Drawing.Size(731, 556);
            this.layoutContainerControl1.TabIndex = 4;
            this.layoutContainerControl1.Text = "layoutContainerControl1";
            // 
            // layoutContainerControl2
            // 
            this.layoutContainerControl2.Controls.Add(this.ListWorkspace);
            this.layoutContainerControl2.Controls.Add(this.dpSearch);
            this.layoutContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutContainerControl2.Location = new System.Drawing.Point(0, 27);
            this.layoutContainerControl2.Name = "layoutContainerControl2";
            this.layoutContainerControl2.Size = new System.Drawing.Size(731, 529);
            this.layoutContainerControl2.TabIndex = 7;
            // 
            // ListWorkspace
            // 
            this.ListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListWorkspace.Location = new System.Drawing.Point(219, 0);
            this.ListWorkspace.Name = "ListWorkspace";
            this.ListWorkspace.Size = new System.Drawing.Size(512, 529);
            this.ListWorkspace.TabIndex = 1;
            this.ListWorkspace.Text = "deckWorkspace4";
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
            this.dpSearch.OriginalSize = new System.Drawing.Size(219, 200);
            this.dpSearch.Size = new System.Drawing.Size(219, 529);
            this.dpSearch.Text = "Search";
            // 
            // controlContainer1
            // 
            this.controlContainer1.Controls.Add(this.SearchWorkspace);
            this.controlContainer1.Location = new System.Drawing.Point(3, 25);
            this.controlContainer1.Name = "controlContainer1";
            this.controlContainer1.Size = new System.Drawing.Size(213, 501);
            this.controlContainer1.TabIndex = 0;
            // 
            // SearchWorkspace
            // 
            this.SearchWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchWorkspace.Location = new System.Drawing.Point(0, 0);
            this.SearchWorkspace.Name = "SearchWorkspace";
            this.SearchWorkspace.Size = new System.Drawing.Size(213, 501);
            this.SearchWorkspace.TabIndex = 5;
            this.SearchWorkspace.Text = "deckWorkspace1";
            // 
            // ToolbarWorkspace
            // 
            this.ToolbarWorkspace.Dock = System.Windows.Forms.DockStyle.Top;
            this.ToolbarWorkspace.Location = new System.Drawing.Point(0, 0);
            this.ToolbarWorkspace.Name = "ToolbarWorkspace";
            this.ToolbarWorkspace.Size = new System.Drawing.Size(731, 27);
            this.ToolbarWorkspace.TabIndex = 8;
            this.ToolbarWorkspace.Text = "deckWorkspace1";
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this.layoutContainerControl2;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dpSearch});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // AgentBillCheckingMainWorkSpace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "AgentBillCheckingMainWorkSpace";
            this.Size = new System.Drawing.Size(731, 556);
            this.panel1.ResumeLayout(false);
            this.layoutContainerControl1.ResumeLayout(false);
            this.layoutContainerControl2.ResumeLayout(false);
            this.dpSearch.ResumeLayout(false);
            this.controlContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl panel1;
        private ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl layoutContainerControl1;
        private ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl layoutContainerControl2;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ListWorkspace;
        private DevExpress.XtraBars.Docking.DockPanel dpSearch;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace SearchWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ToolbarWorkspace;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
    }
}