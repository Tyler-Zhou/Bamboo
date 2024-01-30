namespace ICP.Sys.UI.Job.Finder
{
    partial class JobMiniFinderListPart
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
            this.components = new System.ComponentModel.Container();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.treeOrgJob = new ICP.Framework.ClientComponents.Controls.LWTreeGridControl();
            this.colCName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colEName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.rcmbType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.txtFind = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeOrgJob)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFind.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.Sys.ServiceInterface.DataObjects.JobList);
            // 
            // treeOrgJob
            // 
            this.treeOrgJob.AllowDrop = true;
            this.treeOrgJob.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.treeOrgJob.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.treeOrgJob.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeOrgJob.Appearance.FocusedCell.Options.UseForeColor = true;
            this.treeOrgJob.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colCName,
            this.colEName});
            this.treeOrgJob.DataSource = this.bsList;
            this.treeOrgJob.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeOrgJob.Location = new System.Drawing.Point(0, 34);
            this.treeOrgJob.Name = "treeOrgJob";
            this.treeOrgJob.OptionsBehavior.Editable = false;
            this.treeOrgJob.OptionsView.ShowColumns = false;
            this.treeOrgJob.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbType});
            this.treeOrgJob.Size = new System.Drawing.Size(291, 250);
            this.treeOrgJob.TabIndex = 4;
            this.treeOrgJob.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvMain_KeyDown);
            this.treeOrgJob.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeOrgJob_MouseDoubleClick);
            // 
            // colCName
            // 
            this.colCName.FieldName = "CName";
            this.colCName.Name = "colCName";
            this.colCName.Width = 92;
            // 
            // colEName
            // 
            this.colEName.Caption = "Name";
            this.colEName.FieldName = "EName";
            this.colEName.Name = "colEName";
            this.colEName.Visible = true;
            this.colEName.VisibleIndex = 0;
            // 
            // rcmbType
            // 
            this.rcmbType.AutoHeight = false;
            this.rcmbType.Name = "rcmbType";
            this.rcmbType.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnConfirm);
            this.panelControl1.Controls.Add(this.btnNext);
            this.panelControl1.Controls.Add(this.txtFind);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(291, 34);
            this.panelControl1.TabIndex = 5;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.Location = new System.Drawing.Point(220, 5);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(68, 21);
            this.btnConfirm.TabIndex = 6;
            this.btnConfirm.Text = "C&onfirm";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Location = new System.Drawing.Point(146, 5);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(68, 21);
            this.btnNext.TabIndex = 6;
            this.btnNext.Text = "&Next";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // txtFind
            // 
            this.txtFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFind.Location = new System.Drawing.Point(5, 5);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(135, 21);
            this.txtFind.TabIndex = 0;
            this.txtFind.TextChanged += new System.EventHandler(this.txtFind_TextChanged);
            // 
            // JobMiniFinderListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeOrgJob);
            this.Controls.Add(this.panelControl1);
            this.Name = "JobMiniFinderListPart";
            this.Size = new System.Drawing.Size(291, 284);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeOrgJob)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFind.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        private ICP.Framework.ClientComponents.Controls.LWTreeGridControl treeOrgJob;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbType;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit txtFind;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colEName;
    }
}
