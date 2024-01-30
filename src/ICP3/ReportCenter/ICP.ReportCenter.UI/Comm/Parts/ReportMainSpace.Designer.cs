namespace ICP.ReportCenter.UI
{
    partial class ReportMainSpace
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
            this.ReportWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dpSearch = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.panel1.SuspendLayout();
            this.dpSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlContainer1
            // 
            this.controlContainer1.Controls.Add(this.SearchWorkspace);
            this.controlContainer1.Location = new System.Drawing.Point(3, 25);
            this.controlContainer1.Name = "controlContainer1";
            this.controlContainer1.Size = new System.Drawing.Size(239, 348);
            this.controlContainer1.TabIndex = 0;
            // 
            // SearchWorkspace
            // 
            this.SearchWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchWorkspace.Location = new System.Drawing.Point(0, 0);
            this.SearchWorkspace.Name = "SearchWorkspace";
            this.SearchWorkspace.Size = new System.Drawing.Size(239, 348);
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
            this.panel1.Controls.Add(this.ReportWorkspace);
            this.panel1.Controls.Add(this.dpSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(587, 376);
            this.panel1.TabIndex = 5;
            // 
            // ReportWorkspace
            // 
            this.ReportWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportWorkspace.Location = new System.Drawing.Point(245, 0);
            this.ReportWorkspace.Name = "ReportWorkspace";
            this.ReportWorkspace.Size = new System.Drawing.Size(342, 376);
            this.ReportWorkspace.TabIndex = 1;
            this.ReportWorkspace.Text = "deckWorkspace4";
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
            this.dpSearch.Options.ShowCloseButton = false;
            this.dpSearch.OriginalSize = new System.Drawing.Size(245, 200);
            this.dpSearch.Size = new System.Drawing.Size(245, 376);
            this.dpSearch.Text = "Search";
            // 
            // ReportMainSpace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ReportMainSpace";
            this.Size = new System.Drawing.Size(587, 376);
            this.controlContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.dpSearch.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.ControlContainer controlContainer1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace SearchWorkspace;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl panel1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ReportWorkspace;
        private DevExpress.XtraBars.Docking.DockPanel dpSearch;
    }
}
