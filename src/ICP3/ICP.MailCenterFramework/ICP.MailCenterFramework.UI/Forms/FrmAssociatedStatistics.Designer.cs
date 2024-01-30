namespace ICP.MailCenterFramework.UI
{
    partial class FrmAssociatedStatistics
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
            this.lblLocal = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnStatistics = new System.Windows.Forms.Button();
            this.groupBoxPanel = new System.Windows.Forms.GroupBox();
            this.dgvStatistics = new System.Windows.Forms.DataGridView();
            this.ColumnFolderID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFolder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAssiciated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnUnAssiciated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ssState = new System.Windows.Forms.StatusStrip();
            this.tsBarStatistics = new System.Windows.Forms.ToolStripProgressBar();
            this.groupBoxPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatistics)).BeginInit();
            this.ssState.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLocal
            // 
            this.lblLocal.AutoSize = true;
            this.lblLocal.Location = new System.Drawing.Point(18, 22);
            this.lblLocal.Name = "lblLocal";
            this.lblLocal.Size = new System.Drawing.Size(47, 12);
            this.lblLocal.TabIndex = 2;
            this.lblLocal.Text = "Local：";
            // 
            // txtLocation
            // 
            this.txtLocation.BackColor = System.Drawing.SystemColors.Control;
            this.txtLocation.Location = new System.Drawing.Point(62, 19);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.ReadOnly = true;
            this.txtLocation.Size = new System.Drawing.Size(382, 21);
            this.txtLocation.TabIndex = 3;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(450, 17);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // btnStatistics
            // 
            this.btnStatistics.Location = new System.Drawing.Point(20, 46);
            this.btnStatistics.Name = "btnStatistics";
            this.btnStatistics.Size = new System.Drawing.Size(75, 23);
            this.btnStatistics.TabIndex = 6;
            this.btnStatistics.Text = "Statistics";
            this.btnStatistics.UseVisualStyleBackColor = true;
            // 
            // groupBoxPanel
            // 
            this.groupBoxPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPanel.Controls.Add(this.dgvStatistics);
            this.groupBoxPanel.Location = new System.Drawing.Point(20, 75);
            this.groupBoxPanel.Name = "groupBoxPanel";
            this.groupBoxPanel.Size = new System.Drawing.Size(505, 132);
            this.groupBoxPanel.TabIndex = 7;
            this.groupBoxPanel.TabStop = false;
            this.groupBoxPanel.Text = "Statistics Result";
            // 
            // dgvStatistics
            // 
            this.dgvStatistics.AllowUserToAddRows = false;
            this.dgvStatistics.AllowUserToDeleteRows = false;
            this.dgvStatistics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStatistics.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnFolderID,
            this.ColumnFolder,
            this.ColumnAssiciated,
            this.ColumnUnAssiciated});
            this.dgvStatistics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStatistics.Location = new System.Drawing.Point(3, 17);
            this.dgvStatistics.MultiSelect = false;
            this.dgvStatistics.Name = "dgvStatistics";
            this.dgvStatistics.ReadOnly = true;
            this.dgvStatistics.RowHeadersVisible = false;
            this.dgvStatistics.RowTemplate.Height = 23;
            this.dgvStatistics.Size = new System.Drawing.Size(499, 112);
            this.dgvStatistics.TabIndex = 0;
            // 
            // ColumnFolderID
            // 
            this.ColumnFolderID.DataPropertyName = "FolderID";
            this.ColumnFolderID.HeaderText = "FolderID";
            this.ColumnFolderID.Name = "ColumnFolderID";
            this.ColumnFolderID.ReadOnly = true;
            this.ColumnFolderID.Visible = false;
            // 
            // ColumnFolder
            // 
            this.ColumnFolder.DataPropertyName = "FolderName";
            this.ColumnFolder.HeaderText = "Folder";
            this.ColumnFolder.Name = "ColumnFolder";
            this.ColumnFolder.ReadOnly = true;
            // 
            // ColumnAssiciated
            // 
            this.ColumnAssiciated.DataPropertyName = "AssociatedCount";
            this.ColumnAssiciated.HeaderText = "Assiciated";
            this.ColumnAssiciated.Name = "ColumnAssiciated";
            this.ColumnAssiciated.ReadOnly = true;
            // 
            // ColumnUnAssiciated
            // 
            this.ColumnUnAssiciated.DataPropertyName = "UnAssociatedCount";
            this.ColumnUnAssiciated.HeaderText = "UnAssiciated";
            this.ColumnUnAssiciated.Name = "ColumnUnAssiciated";
            this.ColumnUnAssiciated.ReadOnly = true;
            // 
            // ssState
            // 
            this.ssState.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBarStatistics});
            this.ssState.Location = new System.Drawing.Point(0, 214);
            this.ssState.Name = "ssState";
            this.ssState.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ssState.Size = new System.Drawing.Size(543, 22);
            this.ssState.TabIndex = 8;
            this.ssState.Text = "State";
            this.ssState.Visible = false;
            // 
            // tsBarStatistics
            // 
            this.tsBarStatistics.Name = "tsBarStatistics";
            this.tsBarStatistics.Size = new System.Drawing.Size(100, 16);
            // 
            // FrmAssociatedStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(543, 236);
            this.Controls.Add(this.ssState);
            this.Controls.Add(this.groupBoxPanel);
            this.Controls.Add(this.btnStatistics);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.lblLocal);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAssociatedStatistics";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Associated Statistics";
            this.groupBoxPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatistics)).EndInit();
            this.ssState.ResumeLayout(false);
            this.ssState.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLocal;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnStatistics;
        private System.Windows.Forms.GroupBox groupBoxPanel;
        private System.Windows.Forms.DataGridView dgvStatistics;
        private System.Windows.Forms.StatusStrip ssState;
        private System.Windows.Forms.ToolStripProgressBar tsBarStatistics;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFolderID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFolder;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAssiciated;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnUnAssiciated;
    }
}