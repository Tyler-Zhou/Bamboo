namespace ICP.Sys.UI.UserManage.MiniFinder
{
    partial class UMFS_OrganizationListPart
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
            this.treeMain = new ICP.Framework.ClientComponents.Controls.LWTreeGridControl();
            this.colCode = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCShortName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colEShortName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.Sys.ServiceInterface.DataObjects.OrganizationList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsMainList_PositionChanged);
            // 
            // treeMain
            // 
            this.treeMain.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.treeMain.Appearance.EvenRow.Options.UseBackColor = true;
            this.treeMain.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colCode,
            this.colCShortName,
            this.colEShortName});
            this.treeMain.DataSource = this.bsList;
            this.treeMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMain.Location = new System.Drawing.Point(0, 0);
            this.treeMain.Name = "treeMain";
            this.treeMain.OptionsBehavior.Editable = false;
            this.treeMain.OptionsView.EnableAppearanceEvenRow = true;
            this.treeMain.Size = new System.Drawing.Size(182, 410);
            this.treeMain.TabIndex = 6;
            // 
            // colCode
            // 
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            // 
            // colCShortName
            // 
            this.colCShortName.Caption = "Name";
            this.colCShortName.FieldName = "CShortName";
            this.colCShortName.Name = "colCShortName";
            // 
            // colEShortName
            // 
            this.colEShortName.Caption = "Name";
            this.colEShortName.FieldName = "EShortName";
            this.colEShortName.Name = "colEShortName";
            this.colEShortName.Visible = true;
            this.colEShortName.VisibleIndex = 0;
            // 
            // UMFS_OrganizationListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeMain);
            this.Name = "UMFS_OrganizationListPart";
            this.Size = new System.Drawing.Size(182, 410);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ICP.Framework.ClientComponents.Controls.LWTreeGridControl treeMain;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCode;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCShortName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colEShortName;
    }
}

