namespace ICP.Sys.UI.UserManage.MiniFinder
{
    partial class UMFS_MainWorkspace
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
            this.ListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.OrganizationListWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListWorkspace
            // 
            this.ListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListWorkspace.Location = new System.Drawing.Point(0, 0);
            this.ListWorkspace.Name = "ListWorkspace";
            this.ListWorkspace.Size = new System.Drawing.Size(375, 244);
            this.ListWorkspace.TabIndex = 9;
            this.ListWorkspace.Text = "deckWorkspace1";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Collapsed = true;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.OrganizationListWorkspace);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.ListWorkspace);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(563, 244);
            this.splitContainerControl1.SplitterPosition = 182;
            this.splitContainerControl1.TabIndex = 13;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // OrganizationListWorkspace
            // 
            this.OrganizationListWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OrganizationListWorkspace.Location = new System.Drawing.Point(0, 0);
            this.OrganizationListWorkspace.Name = "OrganizationListWorkspace";
            this.OrganizationListWorkspace.Size = new System.Drawing.Size(182, 244);
            this.OrganizationListWorkspace.TabIndex = 14;
            this.OrganizationListWorkspace.Text = "deckWorkspace1";
            // 
            // UMFS_MainWorkspace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "UMFS_MainWorkspace";
            this.Size = new System.Drawing.Size(563, 244);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ListWorkspace;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace OrganizationListWorkspace;
    }
}
