namespace ICP.MailCenterFramework.UI
{
    partial class FrmAssociatedBatch
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
            this.xtraTabMain = new System.Windows.Forms.TabControl();
            this.xtraTabAssociation = new System.Windows.Forms.TabPage();
            this.panelAssociatedState = new System.Windows.Forms.Panel();
            this.lblAssociatedResult = new System.Windows.Forms.LinkLabel();
            this.lblSccesfullyCount = new System.Windows.Forms.Label();
            this.lblSccesfullyTitle = new System.Windows.Forms.Label();
            this.lblFailedCount = new System.Windows.Forms.Label();
            this.lblHandledState = new System.Windows.Forms.Label();
            this.lblFailedTitle = new System.Windows.Forms.Label();
            this.checkMoveMailAR = new System.Windows.Forms.CheckBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.panelTool = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAssociate = new System.Windows.Forms.Button();
            this.xtraTabMain.SuspendLayout();
            this.xtraTabAssociation.SuspendLayout();
            this.panelAssociatedState.SuspendLayout();
            this.panelTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabMain
            // 
            this.xtraTabMain.Controls.Add(this.xtraTabAssociation);
            this.xtraTabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabMain.Location = new System.Drawing.Point(0, 0);
            this.xtraTabMain.Name = "xtraTabMain";
            this.xtraTabMain.SelectedIndex = 0;
            this.xtraTabMain.Size = new System.Drawing.Size(475, 350);
            this.xtraTabMain.TabIndex = 0;
            // 
            // xtraTabAssociation
            // 
            this.xtraTabAssociation.Controls.Add(this.panelAssociatedState);
            this.xtraTabAssociation.Controls.Add(this.checkMoveMailAR);
            this.xtraTabAssociation.Controls.Add(this.btnBrowse);
            this.xtraTabAssociation.Controls.Add(this.txtLocation);
            this.xtraTabAssociation.Controls.Add(this.lblLocation);
            this.xtraTabAssociation.Location = new System.Drawing.Point(4, 22);
            this.xtraTabAssociation.Name = "xtraTabAssociation";
            this.xtraTabAssociation.Size = new System.Drawing.Size(467, 324);
            this.xtraTabAssociation.TabIndex = 0;
            this.xtraTabAssociation.Text = "Associated";
            // 
            // panelAssociatedState
            // 
            this.panelAssociatedState.Controls.Add(this.lblAssociatedResult);
            this.panelAssociatedState.Controls.Add(this.lblSccesfullyCount);
            this.panelAssociatedState.Controls.Add(this.lblSccesfullyTitle);
            this.panelAssociatedState.Controls.Add(this.lblFailedCount);
            this.panelAssociatedState.Controls.Add(this.lblHandledState);
            this.panelAssociatedState.Controls.Add(this.lblFailedTitle);
            this.panelAssociatedState.Location = new System.Drawing.Point(19, 69);
            this.panelAssociatedState.Name = "panelAssociatedState";
            this.panelAssociatedState.Size = new System.Drawing.Size(391, 86);
            this.panelAssociatedState.TabIndex = 7;
            this.panelAssociatedState.Visible = false;
            // 
            // lblAssociatedResult
            // 
            this.lblAssociatedResult.AutoSize = true;
            this.lblAssociatedResult.Location = new System.Drawing.Point(260, 47);
            this.lblAssociatedResult.Name = "lblAssociatedResult";
            this.lblAssociatedResult.Size = new System.Drawing.Size(71, 12);
            this.lblAssociatedResult.TabIndex = 8;
            this.lblAssociatedResult.TabStop = true;
            this.lblAssociatedResult.Text = "Show Result";
            this.lblAssociatedResult.Visible = false;
            // 
            // lblSccesfullyCount
            // 
            this.lblSccesfullyCount.AutoSize = true;
            this.lblSccesfullyCount.Location = new System.Drawing.Point(183, 15);
            this.lblSccesfullyCount.Name = "lblSccesfullyCount";
            this.lblSccesfullyCount.Size = new System.Drawing.Size(11, 12);
            this.lblSccesfullyCount.TabIndex = 4;
            this.lblSccesfullyCount.Text = "0";
            // 
            // lblSccesfullyTitle
            // 
            this.lblSccesfullyTitle.Location = new System.Drawing.Point(48, 15);
            this.lblSccesfullyTitle.Name = "lblSccesfullyTitle";
            this.lblSccesfullyTitle.Size = new System.Drawing.Size(117, 12);
            this.lblSccesfullyTitle.TabIndex = 4;
            this.lblSccesfullyTitle.Text = "Successfully associated：";
            // 
            // lblFailedCount
            // 
            this.lblFailedCount.AutoSize = true;
            this.lblFailedCount.Location = new System.Drawing.Point(183, 47);
            this.lblFailedCount.Name = "lblFailedCount";
            this.lblFailedCount.Size = new System.Drawing.Size(11, 12);
            this.lblFailedCount.TabIndex = 5;
            this.lblFailedCount.Text = "0";
            // 
            // lblHandledState
            // 
            this.lblHandledState.Location = new System.Drawing.Point(260, 32);
            this.lblHandledState.Name = "lblHandledState";
            this.lblHandledState.Size = new System.Drawing.Size(127, 12);
            this.lblHandledState.TabIndex = 6;
            this.lblHandledState.Text = "Associating...";
            // 
            // lblFailedTitle
            // 
            this.lblFailedTitle.Location = new System.Drawing.Point(48, 47);
            this.lblFailedTitle.Name = "lblFailedTitle";
            this.lblFailedTitle.Size = new System.Drawing.Size(87, 12);
            this.lblFailedTitle.TabIndex = 5;
            this.lblFailedTitle.Text = "Failed associated：";
            // 
            // checkMoveMailAR
            // 
            this.checkMoveMailAR.Location = new System.Drawing.Point(19, 41);
            this.checkMoveMailAR.Name = "checkMoveMailAR";
            this.checkMoveMailAR.Size = new System.Drawing.Size(434, 16);
            this.checkMoveMailAR.TabIndex = 3;
            this.checkMoveMailAR.Text = "Archives these mails that are Associated and Read and Un-Follow Up.";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(345, 13);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(64, 20);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse";
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(76, 14);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.ReadOnly = true;
            this.txtLocation.Size = new System.Drawing.Size(265, 21);
            this.txtLocation.TabIndex = 1;
            // 
            // lblLocation
            // 
            this.lblLocation.Location = new System.Drawing.Point(8, 17);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(62, 12);
            this.lblLocation.TabIndex = 0;
            this.lblLocation.Text = "Location:";
            // 
            // panelTool
            // 
            this.panelTool.Controls.Add(this.btnClose);
            this.panelTool.Controls.Add(this.btnAssociate);
            this.panelTool.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelTool.Location = new System.Drawing.Point(0, 350);
            this.panelTool.Name = "panelTool";
            this.panelTool.Size = new System.Drawing.Size(475, 30);
            this.panelTool.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(347, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(64, 20);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            // 
            // btnAssociate
            // 
            this.btnAssociate.Location = new System.Drawing.Point(278, 5);
            this.btnAssociate.Name = "btnAssociate";
            this.btnAssociate.Size = new System.Drawing.Size(64, 20);
            this.btnAssociate.TabIndex = 0;
            this.btnAssociate.Text = "Associate";
            // 
            // FrmAssociatedBatch
            // 
            this.AcceptButton = this.btnAssociate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(475, 380);
            this.Controls.Add(this.xtraTabMain);
            this.Controls.Add(this.panelTool);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(445, 350);
            this.Name = "FrmAssociatedBatch";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bulk Mail Association";
            this.TopMost = true;
            this.xtraTabMain.ResumeLayout(false);
            this.xtraTabAssociation.ResumeLayout(false);
            this.xtraTabAssociation.PerformLayout();
            this.panelAssociatedState.ResumeLayout(false);
            this.panelAssociatedState.PerformLayout();
            this.panelTool.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl xtraTabMain;
        private System.Windows.Forms.TabPage xtraTabAssociation;
        private System.Windows.Forms.Panel panelTool;
        private System.Windows.Forms.Button btnAssociate;
        private System.Windows.Forms.CheckBox checkMoveMailAR;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Panel panelAssociatedState;
        private System.Windows.Forms.Label lblSccesfullyCount;
        private System.Windows.Forms.Label lblSccesfullyTitle;
        private System.Windows.Forms.Label lblFailedCount;
        private System.Windows.Forms.Label lblHandledState;
        private System.Windows.Forms.Label lblFailedTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.LinkLabel lblAssociatedResult;
    }
}