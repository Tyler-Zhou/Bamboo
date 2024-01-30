namespace ICP.MailCenterFramework.UI
{
    partial class FrmEmailArchiving
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
            this.lblTotalEmail = new System.Windows.Forms.Label();
            this.panelControl1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblMailProjectTitle = new System.Windows.Forms.Label();
            this.lblMailFolderNameTitle = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblMoveToFolderTitle = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblMoveToFolder = new System.Windows.Forms.Label();
            this.lblMailProject = new System.Windows.Forms.Label();
            this.lblMailFolderName = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.barCurrentProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.lblCurrentState = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSpring = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelControl1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTotalEmail
            // 
            this.lblTotalEmail.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblTotalEmail, 2);
            this.lblTotalEmail.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTotalEmail.Location = new System.Drawing.Point(3, 72);
            this.lblTotalEmail.Name = "lblTotalEmail";
            this.lblTotalEmail.Size = new System.Drawing.Size(179, 24);
            this.lblTotalEmail.TabIndex = 0;
            this.lblTotalEmail.Text = "Mail Account: 0 ,Progress: 0%";
            this.lblTotalEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTotalEmail.Visible = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.tableLayoutPanel1);
            this.panelControl1.Controls.Add(this.statusStrip1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(604, 184);
            this.panelControl1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.Controls.Add(this.lblMailProjectTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblMailFolderNameTitle, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblMoveToFolderTitle, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.progressBar1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblTotalEmail, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblMoveToFolder, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblMailProject, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblMailFolderName, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(604, 162);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // lblMailProjectTitle
            // 
            this.lblMailProjectTitle.AutoSize = true;
            this.lblMailProjectTitle.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblMailProjectTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMailProjectTitle.Location = new System.Drawing.Point(65, 0);
            this.lblMailProjectTitle.Name = "lblMailProjectTitle";
            this.lblMailProjectTitle.Size = new System.Drawing.Size(83, 24);
            this.lblMailProjectTitle.TabIndex = 9;
            this.lblMailProjectTitle.Text = "Mail Project:";
            this.lblMailProjectTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMailFolderNameTitle
            // 
            this.lblMailFolderNameTitle.AutoSize = true;
            this.lblMailFolderNameTitle.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblMailFolderNameTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMailFolderNameTitle.Location = new System.Drawing.Point(71, 24);
            this.lblMailFolderNameTitle.Name = "lblMailFolderNameTitle";
            this.lblMailFolderNameTitle.Size = new System.Drawing.Size(77, 24);
            this.lblMailFolderNameTitle.TabIndex = 9;
            this.lblMailFolderNameTitle.Text = "Mail Folder:";
            this.lblMailFolderNameTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(537, 123);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 36);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            // 
            // lblMoveToFolderTitle
            // 
            this.lblMoveToFolderTitle.AutoSize = true;
            this.lblMoveToFolderTitle.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblMoveToFolderTitle.Location = new System.Drawing.Point(95, 48);
            this.lblMoveToFolderTitle.Name = "lblMoveToFolderTitle";
            this.lblMoveToFolderTitle.Size = new System.Drawing.Size(53, 24);
            this.lblMoveToFolderTitle.TabIndex = 3;
            this.lblMoveToFolderTitle.Text = "Move To:";
            this.lblMoveToFolderTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // progressBar1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.progressBar1, 2);
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(3, 99);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(598, 18);
            this.progressBar1.TabIndex = 7;
            this.progressBar1.Visible = false;
            // 
            // lblMoveToFolder
            // 
            this.lblMoveToFolder.AutoSize = true;
            this.lblMoveToFolder.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblMoveToFolder.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMoveToFolder.Location = new System.Drawing.Point(154, 48);
            this.lblMoveToFolder.Name = "lblMoveToFolder";
            this.lblMoveToFolder.Size = new System.Drawing.Size(19, 24);
            this.lblMoveToFolder.TabIndex = 4;
            this.lblMoveToFolder.Text = "To";
            this.lblMoveToFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMailProject
            // 
            this.lblMailProject.AutoSize = true;
            this.lblMailProject.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblMailProject.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMailProject.Location = new System.Drawing.Point(154, 0);
            this.lblMailProject.Name = "lblMailProject";
            this.lblMailProject.Size = new System.Drawing.Size(82, 24);
            this.lblMailProject.TabIndex = 10;
            this.lblMailProject.Text = "MailProject";
            this.lblMailProject.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMailFolderName
            // 
            this.lblMailFolderName.AutoSize = true;
            this.lblMailFolderName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblMailFolderName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMailFolderName.Location = new System.Drawing.Point(154, 24);
            this.lblMailFolderName.Name = "lblMailFolderName";
            this.lblMailFolderName.Size = new System.Drawing.Size(75, 24);
            this.lblMailFolderName.TabIndex = 10;
            this.lblMailFolderName.Text = "MailFolder";
            this.lblMailFolderName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusStrip1
            // 
            this.statusStrip1.AllowMerge = false;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.barCurrentProgress,
            this.lblCurrentState,
            this.lblSpring});
            this.statusStrip1.Location = new System.Drawing.Point(0, 162);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(604, 22);
            this.statusStrip1.TabIndex = 11;
            // 
            // barCurrentProgress
            // 
            this.barCurrentProgress.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.barCurrentProgress.Name = "barCurrentProgress";
            this.barCurrentProgress.Size = new System.Drawing.Size(100, 16);
            this.barCurrentProgress.Step = 1;
            // 
            // lblCurrentState
            // 
            this.lblCurrentState.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblCurrentState.Name = "lblCurrentState";
            this.lblCurrentState.Size = new System.Drawing.Size(84, 17);
            this.lblCurrentState.Text = "Current State";
            // 
            // lblSpring
            // 
            this.lblSpring.Name = "lblSpring";
            this.lblSpring.Size = new System.Drawing.Size(372, 17);
            this.lblSpring.Spring = true;
            // 
            // FrmEmailArchiving
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(604, 184);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEmailArchiving";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Email Archiving";
            this.TopMost = true;
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTotalEmail;
        private System.Windows.Forms.Panel panelControl1;
        private System.Windows.Forms.Label lblMoveToFolderTitle;
        private System.Windows.Forms.Label lblMoveToFolder;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblMailProjectTitle;
        private System.Windows.Forms.Label lblMailProject;
        private System.Windows.Forms.Label lblMailFolderNameTitle;
        private System.Windows.Forms.Label lblMailFolderName;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblCurrentState;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripProgressBar barCurrentProgress;
        private System.Windows.Forms.ToolStripStatusLabel lblSpring;
    }
}