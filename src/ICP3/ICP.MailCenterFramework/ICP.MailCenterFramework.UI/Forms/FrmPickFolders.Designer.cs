namespace ICP.MailCenterFramework.UI
{
    partial class FrmPickFolders
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPickFolders));
            this.panelControl1 = new System.Windows.Forms.Panel();
            this.tvFolders = new System.Windows.Forms.TreeView();
            this.lblTopTitle = new System.Windows.Forms.Label();
            this.btnSure = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.folderImages = new System.Windows.Forms.ImageList(this.components);
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.tvFolders);
            this.panelControl1.Location = new System.Drawing.Point(8, 28);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(214, 236);
            this.panelControl1.TabIndex = 0;
            // 
            // tvFolders
            // 
            this.tvFolders.CheckBoxes = true;
            this.tvFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvFolders.ImageIndex = 0;
            this.tvFolders.ImageList = this.folderImages;
            this.tvFolders.Location = new System.Drawing.Point(0, 0);
            this.tvFolders.Name = "tvFolders";
            this.tvFolders.SelectedImageIndex = 0;
            this.tvFolders.ShowLines = false;
            this.tvFolders.Size = new System.Drawing.Size(214, 236);
            this.tvFolders.TabIndex = 0;
            // 
            // lblTopTitle
            // 
            this.lblTopTitle.Location = new System.Drawing.Point(11, 11);
            this.lblTopTitle.Name = "lblTopTitle";
            this.lblTopTitle.Size = new System.Drawing.Size(36, 12);
            this.lblTopTitle.TabIndex = 1;
            this.lblTopTitle.Text = "Folders:";
            // 
            // btnSure
            // 
            this.btnSure.Location = new System.Drawing.Point(231, 41);
            this.btnSure.Name = "btnSure";
            this.btnSure.Size = new System.Drawing.Size(80, 20);
            this.btnSure.TabIndex = 2;
            this.btnSure.Text = "OK";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(231, 68);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 20);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(231, 94);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(80, 20);
            this.btnClearAll.TabIndex = 4;
            this.btnClearAll.Text = "Clear All";
            // 
            // folderImages
            // 
            this.folderImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("folderImages.ImageStream")));
            this.folderImages.TransparentColor = System.Drawing.Color.Transparent;
            this.folderImages.Images.SetKeyName(0, "folder-home.png");
            this.folderImages.Images.SetKeyName(1, "folder-inbox.png");
            this.folderImages.Images.SetKeyName(2, "folder-drafts.png");
            this.folderImages.Images.SetKeyName(3, "folder-outbox.png");
            this.folderImages.Images.SetKeyName(4, "folder-deleted.png");
            this.folderImages.Images.SetKeyName(5, "folder-sent.png");
            this.folderImages.Images.SetKeyName(6, "folder-junk.png");
            this.folderImages.Images.SetKeyName(7, "folder.png");
            this.folderImages.Images.SetKeyName(8, "folder-store.png");
            this.folderImages.Images.SetKeyName(9, "folder-search.png");
            // 
            // FrmPickFolders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(319, 266);
            this.Controls.Add(this.btnClearAll);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSure);
            this.Controls.Add(this.lblTopTitle);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(335, 304);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(335, 304);
            this.Name = "FrmPickFolders";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Folder";
            this.TopMost = true;
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelControl1;
        private System.Windows.Forms.Label lblTopTitle;
        private System.Windows.Forms.TreeView tvFolders;
        private System.Windows.Forms.Button btnSure;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.ImageList folderImages;
    }
}