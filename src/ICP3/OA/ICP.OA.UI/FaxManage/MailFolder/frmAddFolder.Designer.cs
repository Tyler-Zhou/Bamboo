namespace ICP.OA.UI.FaxManage
{
    partial class frmAddFolder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddFolder));
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.labName = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.bsFolder = new System.Windows.Forms.BindingSource(this.components);
            this.treeFolder = new DevExpress.XtraTreeList.TreeList();
            this.colCName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsFolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeFolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(246, 295);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 21;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dxErrorProvider1.SetIconAlignment(this.txtName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtName.Location = new System.Drawing.Point(61, 8);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(351, 21);
            this.txtName.TabIndex = 20;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            // 
            // labName
            // 
            this.labName.Location = new System.Drawing.Point(12, 11);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(31, 14);
            this.labName.TabIndex = 23;
            this.labName.Text = "Name";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(340, 295);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "&Cancel";
            // 
            // bsFolder
            // 
            this.bsFolder.DataSource = typeof(ICP.Message.ServiceInterface.MessageFolderList);
            // 
            // treeFolder
            // 
            this.treeFolder.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.treeFolder.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.treeFolder.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeFolder.Appearance.FocusedCell.Options.UseForeColor = true;
            this.treeFolder.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colCName});
            this.treeFolder.DataSource = this.bsFolder;
            this.treeFolder.Location = new System.Drawing.Point(12, 35);
            this.treeFolder.Name = "treeFolder";
            this.treeFolder.OptionsView.ShowColumns = false;
            this.treeFolder.OptionsView.ShowIndicator = false;
            this.treeFolder.Size = new System.Drawing.Size(403, 253);
            this.treeFolder.TabIndex = 24;
            this.treeFolder.GetStateImage += new DevExpress.XtraTreeList.GetStateImageEventHandler(this.treeFolder_GetStateImage);
            // 
            // colCName
            // 
            this.colCName.FieldName = "Name";
            this.colCName.Name = "colCName";
            this.colCName.OptionsColumn.AllowEdit = false;
            this.colCName.Visible = true;
            this.colCName.VisibleIndex = 0;
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // frmAddFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 330);
            this.Controls.Add(this.treeFolder);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.labName);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmAddFolder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Folder";
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsFolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeFolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl labName;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.BindingSource bsFolder;
        private DevExpress.XtraTreeList.TreeList treeFolder;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCName;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        //private System.Windows.Forms.ImageList imageListTree;
    }
}