namespace ICP.MailCenter.UI
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
            this.lblTotalEmail = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblRemaindedTitle = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.lblMailProject = new DevExpress.XtraEditors.LabelControl();
            this.lblToTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblRemainded = new DevExpress.XtraEditors.LabelControl();
            this.lblMailProjectTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblTo = new DevExpress.XtraEditors.LabelControl();
            this.progressBar1 = new DevExpress.XtraEditors.ProgressBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTotalEmail
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.lblTotalEmail, 2);
            this.lblTotalEmail.Location = new System.Drawing.Point(3, 3);
            this.lblTotalEmail.Name = "lblTotalEmail";
            this.lblTotalEmail.Size = new System.Drawing.Size(165, 14);
            this.lblTotalEmail.TabIndex = 0;
            this.lblTotalEmail.Text = "Mail Account: 0 ,Progress: 0%";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl1.Controls.Add(this.tableLayoutPanel1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(412, 137);
            this.panelControl1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblRemaindedTitle, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblMailProject, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblTotalEmail, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblToTitle, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblRemainded, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblMailProjectTitle, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblTo, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.progressBar1, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(408, 133);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // lblRemaindedTitle
            // 
            this.lblRemaindedTitle.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblRemaindedTitle.Location = new System.Drawing.Point(3, 63);
            this.lblRemaindedTitle.Name = "lblRemaindedTitle";
            this.lblRemaindedTitle.Size = new System.Drawing.Size(94, 14);
            this.lblRemaindedTitle.TabIndex = 5;
            this.lblRemaindedTitle.Text = "Remainded Item:";
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(330, 108);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 22);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            // 
            // lblMailProject
            // 
            this.lblMailProject.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblMailProject.Appearance.Options.UseFont = true;
            this.lblMailProject.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblMailProject.Location = new System.Drawing.Point(103, 23);
            this.lblMailProject.Name = "lblMailProject";
            this.lblMailProject.Size = new System.Drawing.Size(68, 14);
            this.lblMailProject.TabIndex = 10;
            this.lblMailProject.Text = "MailProject";
            // 
            // lblToTitle
            // 
            this.lblToTitle.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblToTitle.Location = new System.Drawing.Point(45, 43);
            this.lblToTitle.Name = "lblToTitle";
            this.lblToTitle.Size = new System.Drawing.Size(52, 14);
            this.lblToTitle.TabIndex = 3;
            this.lblToTitle.Text = "Move To:";
            // 
            // lblRemainded
            // 
            this.lblRemainded.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblRemainded.Location = new System.Drawing.Point(103, 63);
            this.lblRemainded.Name = "lblRemainded";
            this.lblRemainded.Size = new System.Drawing.Size(60, 14);
            this.lblRemainded.TabIndex = 6;
            this.lblRemainded.Text = "Remainded";
            // 
            // lblMailProjectTitle
            // 
            this.lblMailProjectTitle.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblMailProjectTitle.Location = new System.Drawing.Point(31, 23);
            this.lblMailProjectTitle.Name = "lblMailProjectTitle";
            this.lblMailProjectTitle.Size = new System.Drawing.Size(66, 14);
            this.lblMailProjectTitle.TabIndex = 9;
            this.lblMailProjectTitle.Text = "Mail Project:";
            // 
            // lblTo
            // 
            this.lblTo.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblTo.Appearance.Options.UseFont = true;
            this.lblTo.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTo.Location = new System.Drawing.Point(103, 43);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(15, 14);
            this.lblTo.TabIndex = 4;
            this.lblTo.Text = "To";
            // 
            // progressBar1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.progressBar1, 2);
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(3, 83);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(402, 19);
            this.progressBar1.TabIndex = 7;
            // 
            // FrmEmailArchiving
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 137);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEmailArchiving";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Email Archiving";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblTotalEmail;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lblToTitle;
        private DevExpress.XtraEditors.LabelControl lblTo;
        private DevExpress.XtraEditors.LabelControl lblRemaindedTitle;
        private DevExpress.XtraEditors.LabelControl lblRemainded;
        private DevExpress.XtraEditors.ProgressBarControl progressBar1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl lblMailProjectTitle;
        private DevExpress.XtraEditors.LabelControl lblMailProject;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}