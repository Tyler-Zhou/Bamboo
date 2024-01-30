namespace ICP.Common.UI.CustomerManager
{
    partial class FindInvoiceTitleMainWorkSpace
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
            this.FindInvoiceTitleSearchWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.panel1 = new ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl(this.components);
            this.FindInvoiceTitleListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.dpSearch = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.panel1.SuspendLayout();
            this.dpSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlContainer1
            // 
            this.controlContainer1.Controls.Add(this.FindInvoiceTitleSearchWorkspace);
            this.controlContainer1.Location = new System.Drawing.Point(3, 25);
            this.controlContainer1.Name = "controlContainer1";
            this.controlContainer1.Size = new System.Drawing.Size(194, 528);
            this.controlContainer1.TabIndex = 0;
            // 
            // FindInvoiceTitleSearchWorkspace
            // 
            this.FindInvoiceTitleSearchWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FindInvoiceTitleSearchWorkspace.Location = new System.Drawing.Point(0, 0);
            this.FindInvoiceTitleSearchWorkspace.Name = "FindInvoiceTitleSearchWorkspace";
            this.FindInvoiceTitleSearchWorkspace.Size = new System.Drawing.Size(194, 528);
            this.FindInvoiceTitleSearchWorkspace.TabIndex = 5;
            this.FindInvoiceTitleSearchWorkspace.Text = "deckWorkspace1";
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
            this.panel1.Controls.Add(this.FindInvoiceTitleListWorkspace);
            this.panel1.Controls.Add(this.dpSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(731, 556);
            this.panel1.TabIndex = 5;
            // 
            // FindInvoiceTitleListWorkspace
            // 
            this.FindInvoiceTitleListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FindInvoiceTitleListWorkspace.Location = new System.Drawing.Point(200, 0);
            this.FindInvoiceTitleListWorkspace.Name = "FindInvoiceTitleListWorkspace";
            this.FindInvoiceTitleListWorkspace.Size = new System.Drawing.Size(531, 556);
            this.FindInvoiceTitleListWorkspace.TabIndex = 1;
            this.FindInvoiceTitleListWorkspace.Text = "deckWorkspace4";
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
            this.dpSearch.OriginalSize = new System.Drawing.Size(200, 200);
            this.dpSearch.Size = new System.Drawing.Size(200, 556);
            this.dpSearch.Text = "Search";
            // 
            // FindInvoiceTitleMainWorkSpace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "FindInvoiceTitleMainWorkSpace";
            this.Size = new System.Drawing.Size(731, 556);
            this.controlContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.dpSearch.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.ControlContainer controlContainer1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace FindInvoiceTitleSearchWorkspace;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private ICP.Framework.ClientComponents.UIFramework.LayoutContainerControl panel1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace FindInvoiceTitleListWorkspace;
        private DevExpress.XtraBars.Docking.DockPanel dpSearch;
    }
}
