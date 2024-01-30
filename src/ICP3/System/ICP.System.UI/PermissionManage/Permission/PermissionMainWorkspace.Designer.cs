namespace ICP.Sys.UI.PermissionManage.Permission
{
    partial class PermissionMainWorkspace
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
            this.MenuWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tpMenu = new DevExpress.XtraTab.XtraTabPage();
            this.tpToolbar = new DevExpress.XtraTab.XtraTabPage();
            this.tpStatusbar = new DevExpress.XtraTab.XtraTabPage();
            this.ToolbarWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.StatusbarWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tpMenu.SuspendLayout();
            this.tpToolbar.SuspendLayout();
            this.tpStatusbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuWorkspace
            // 
            this.MenuWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.MenuWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MenuWorkspace.Location = new System.Drawing.Point(0, 0);
            this.MenuWorkspace.Name = "MenuWorkspace";
            this.MenuWorkspace.Size = new System.Drawing.Size(543, 457);
            this.MenuWorkspace.TabIndex = 10;
            this.MenuWorkspace.Text = "deckWorkspace1";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.xtraTabControl1.Appearance.Options.UseBackColor = true;
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tpMenu;
            this.xtraTabControl1.Size = new System.Drawing.Size(573, 464);
            this.xtraTabControl1.TabIndex = 11;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpMenu,
            this.tpToolbar,
            this.tpStatusbar});
            // 
            // tpMenu
            // 
            this.tpMenu.Controls.Add(this.MenuWorkspace);
            this.tpMenu.Name = "tpMenu";
            this.tpMenu.Size = new System.Drawing.Size(543, 457);
            this.tpMenu.Text = "Menu";
            // 
            // tpToolbar
            // 
            this.tpToolbar.Controls.Add(this.ToolbarWorkspace);
            this.tpToolbar.Name = "tpToolbar";
            this.tpToolbar.Size = new System.Drawing.Size(543, 457);
            this.tpToolbar.Text = "Toolbar";
            // 
            // tpStatusbar
            // 
            this.tpStatusbar.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.tpStatusbar.Appearance.PageClient.Options.UseBackColor = true;
            this.tpStatusbar.Controls.Add(this.StatusbarWorkspace);
            this.tpStatusbar.Name = "tpStatusbar";
            this.tpStatusbar.Size = new System.Drawing.Size(543, 457);
            this.tpStatusbar.Text = "Statusbar";
            // 
            // ToolbarWorkspace
            // 
            this.ToolbarWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.ToolbarWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolbarWorkspace.Location = new System.Drawing.Point(0, 0);
            this.ToolbarWorkspace.Name = "ToolbarWorkspace";
            this.ToolbarWorkspace.Size = new System.Drawing.Size(543, 457);
            this.ToolbarWorkspace.TabIndex = 10;
            this.ToolbarWorkspace.Text = "deckWorkspace1";
            // 
            // StatusbarWorkspace
            // 
            this.StatusbarWorkspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.StatusbarWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StatusbarWorkspace.Location = new System.Drawing.Point(0, 0);
            this.StatusbarWorkspace.Name = "StatusbarWorkspace";
            this.StatusbarWorkspace.Size = new System.Drawing.Size(543, 457);
            this.StatusbarWorkspace.TabIndex = 10;
            this.StatusbarWorkspace.Text = "deckWorkspace1";
            // 
            // PermissionMainWorkspace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "PermissionMainWorkspace";
            this.Size = new System.Drawing.Size(573, 464);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tpMenu.ResumeLayout(false);
            this.tpToolbar.ResumeLayout(false);
            this.tpStatusbar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace MenuWorkspace;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tpMenu;
        private DevExpress.XtraTab.XtraTabPage tpToolbar;
        private DevExpress.XtraTab.XtraTabPage tpStatusbar;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ToolbarWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace StatusbarWorkspace;
    }
}
