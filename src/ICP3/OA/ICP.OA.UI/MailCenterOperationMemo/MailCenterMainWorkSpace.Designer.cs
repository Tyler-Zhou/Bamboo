namespace ICP.OA.UI.Contact
{
    partial class MailCenterMainWorkSpace
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
            this.MailCenterReportWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.MailCenterSearchWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.pnlSearchPart = new DevExpress.XtraEditors.PanelControl();
            this.pnlReportViewPart = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSearchPart)).BeginInit();
            this.pnlSearchPart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlReportViewPart)).BeginInit();
            this.pnlReportViewPart.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // MailCenterReportWorkspace
            // 
            this.MailCenterReportWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MailCenterReportWorkspace.Location = new System.Drawing.Point(2, 2);
            this.MailCenterReportWorkspace.Name = "MailCenterReportWorkspace";
            this.MailCenterReportWorkspace.Size = new System.Drawing.Size(523, 498);
            this.MailCenterReportWorkspace.TabIndex = 11;
            this.MailCenterReportWorkspace.Text = "Viewspace";
            // 
            // MailCenterSearchWorkspace
            // 
            this.MailCenterSearchWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MailCenterSearchWorkspace.Location = new System.Drawing.Point(2, 2);
            this.MailCenterSearchWorkspace.Name = "MailCenterSearchWorkspace";
            this.MailCenterSearchWorkspace.Size = new System.Drawing.Size(523, 49);
            this.MailCenterSearchWorkspace.TabIndex = 14;
            this.MailCenterSearchWorkspace.Text = "searchSpace";
            // 
            // pnlSearchPart
            // 
            this.pnlSearchPart.Controls.Add(this.MailCenterSearchWorkspace);
            this.pnlSearchPart.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchPart.Location = new System.Drawing.Point(0, 0);
            this.pnlSearchPart.Name = "pnlSearchPart";
            this.pnlSearchPart.Size = new System.Drawing.Size(527, 53);
            this.pnlSearchPart.TabIndex = 15;
            // 
            // pnlReportViewPart
            // 
            this.pnlReportViewPart.Controls.Add(this.MailCenterReportWorkspace);
            this.pnlReportViewPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlReportViewPart.Location = new System.Drawing.Point(0, 53);
            this.pnlReportViewPart.Name = "pnlReportViewPart";
            this.pnlReportViewPart.Size = new System.Drawing.Size(527, 502);
            this.pnlReportViewPart.TabIndex = 16;
            // 
            // MailCenterMainWorkSpace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlReportViewPart);
            this.Controls.Add(this.pnlSearchPart);
            this.Name = "MailCenterMainWorkSpace";
            this.Size = new System.Drawing.Size(527, 555);
            this.Controls.SetChildIndex(this.pnlSearchPart, 0);
            this.Controls.SetChildIndex(this.pnlReportViewPart, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSearchPart)).EndInit();
            this.pnlSearchPart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlReportViewPart)).EndInit();
            this.pnlReportViewPart.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace MailCenterReportWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace MailCenterSearchWorkspace;
        private DevExpress.XtraEditors.PanelControl pnlReportViewPart;
        private DevExpress.XtraEditors.PanelControl pnlSearchPart;


    }
}
