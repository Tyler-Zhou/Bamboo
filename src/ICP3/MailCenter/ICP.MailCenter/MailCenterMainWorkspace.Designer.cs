namespace ICP.MailCenter.UI
{
    partial class MailCenterMainWorkspace
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
            this.splitContainerControlMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitContainerControlFolderAndList = new DevExpress.XtraEditors.SplitContainerControl();
            this.deckWorkspaceFolder = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.deckWorkspaceEmailList = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.mailSearchdeckWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.deckWorkspaceTool = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.deckWorkspaceEmailDetail = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.deckSearchFolders = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControlMain)).BeginInit();
            this.splitContainerControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControlFolderAndList)).BeginInit();
            this.splitContainerControlFolderAndList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControlMain
            // 
            this.splitContainerControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControlMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControlMain.Name = "splitContainerControlMain";
            this.splitContainerControlMain.Panel1.Controls.Add(this.splitContainerControlFolderAndList);
            this.splitContainerControlMain.Panel1.Controls.Add(this.panelControl1);
            this.splitContainerControlMain.Panel1.Text = "Panel1";
            this.splitContainerControlMain.Panel2.Controls.Add(this.deckWorkspaceEmailDetail);
            this.splitContainerControlMain.Panel2.Text = "Panel2";
            this.splitContainerControlMain.Size = new System.Drawing.Size(1000, 768);
            this.splitContainerControlMain.SplitterPosition = 435;
            this.splitContainerControlMain.TabIndex = 3;
            this.splitContainerControlMain.Text = "splitContainerControl1";
            // 
            // splitContainerControlFolderAndList
            // 
            this.splitContainerControlFolderAndList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControlFolderAndList.Location = new System.Drawing.Point(0, 31);
            this.splitContainerControlFolderAndList.Name = "splitContainerControlFolderAndList";
            this.splitContainerControlFolderAndList.Panel1.Controls.Add(this.deckWorkspaceFolder);
            this.splitContainerControlFolderAndList.Panel1.Controls.Add(this.deckSearchFolders);
            this.splitContainerControlFolderAndList.Panel1.Text = "Panel1";
            this.splitContainerControlFolderAndList.Panel2.Controls.Add(this.deckWorkspaceEmailList);
            this.splitContainerControlFolderAndList.Panel2.Controls.Add(this.mailSearchdeckWorkspace);
            this.splitContainerControlFolderAndList.Panel2.Text = "Panel2";
            this.splitContainerControlFolderAndList.Size = new System.Drawing.Size(435, 737);
            this.splitContainerControlFolderAndList.SplitterPosition = 178;
            this.splitContainerControlFolderAndList.TabIndex = 1;
            this.splitContainerControlFolderAndList.Text = "splitContainerControl1";
            // 
            // deckWorkspaceFolder
            // 
            this.deckWorkspaceFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deckWorkspaceFolder.Location = new System.Drawing.Point(0, 24);
            this.deckWorkspaceFolder.Name = "deckWorkspaceFolder";
            this.deckWorkspaceFolder.Size = new System.Drawing.Size(178, 713);
            this.deckWorkspaceFolder.TabIndex = 0;
            this.deckWorkspaceFolder.Text = "deckWorkspace1";
            // 
            // deckWorkspaceEmailList
            // 
            this.deckWorkspaceEmailList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deckWorkspaceEmailList.Location = new System.Drawing.Point(0, 24);
            this.deckWorkspaceEmailList.Name = "deckWorkspaceEmailList";
            this.deckWorkspaceEmailList.Size = new System.Drawing.Size(251, 713);
            this.deckWorkspaceEmailList.TabIndex = 2;
            this.deckWorkspaceEmailList.Text = "deckWorkspace2";
            // 
            // mailSearchdeckWorkspace
            // 
            this.mailSearchdeckWorkspace.Dock = System.Windows.Forms.DockStyle.Top;
            this.mailSearchdeckWorkspace.Location = new System.Drawing.Point(0, 0);
            this.mailSearchdeckWorkspace.Name = "mailSearchdeckWorkspace";
            this.mailSearchdeckWorkspace.Size = new System.Drawing.Size(251, 24);
            this.mailSearchdeckWorkspace.TabIndex = 1;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.deckWorkspaceTool);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(435, 31);
            this.panelControl1.TabIndex = 0;
            // 
            // deckWorkspaceTool
            // 
            this.deckWorkspaceTool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deckWorkspaceTool.Location = new System.Drawing.Point(2, 2);
            this.deckWorkspaceTool.Name = "deckWorkspaceTool";
            this.deckWorkspaceTool.Size = new System.Drawing.Size(431, 27);
            this.deckWorkspaceTool.TabIndex = 0;
            this.deckWorkspaceTool.Text = "deckWorkspace3";
            // 
            // deckWorkspaceEmailDetail
            // 
            this.deckWorkspaceEmailDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deckWorkspaceEmailDetail.Location = new System.Drawing.Point(0, 0);
            this.deckWorkspaceEmailDetail.Name = "deckWorkspaceEmailDetail";
            this.deckWorkspaceEmailDetail.Size = new System.Drawing.Size(559, 768);
            this.deckWorkspaceEmailDetail.TabIndex = 0;
            this.deckWorkspaceEmailDetail.Text = "deckWorkspace1";
            // 
            // deckSearchFolders
            // 
            this.deckSearchFolders.Dock = System.Windows.Forms.DockStyle.Top;
            this.deckSearchFolders.Location = new System.Drawing.Point(0, 0);
            this.deckSearchFolders.Name = "deckSearchFolders";
            this.deckSearchFolders.Size = new System.Drawing.Size(178, 24);
            this.deckSearchFolders.TabIndex = 2;
            // 
            // MailCenterMainWorkspace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControlMain);
            this.Name = "MailCenterMainWorkspace";
            this.Size = new System.Drawing.Size(1000, 768);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControlMain)).EndInit();
            this.splitContainerControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControlFolderAndList)).EndInit();
            this.splitContainerControlFolderAndList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControlMain;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControlFolderAndList;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace deckWorkspaceFolder;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace deckWorkspaceTool;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace deckWorkspaceEmailDetail;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace mailSearchdeckWorkspace;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace deckWorkspaceEmailList;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace deckSearchFolders;
    }
}
