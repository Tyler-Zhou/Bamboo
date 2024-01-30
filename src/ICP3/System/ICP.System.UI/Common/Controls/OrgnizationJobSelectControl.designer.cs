namespace ICP.Sys.UI.Common
{
    partial class OrgnizationJobSelectControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrgnizationJobSelectControl));
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.treeMain = new ICP.Framework.ClientComponents.Controls.LWTreeGridControl();
            this.colName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.rcmbType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbType)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.Sys.ServiceInterface.DataObjects.Organization2JobList);
            // 
            // treeMain
            // 
            this.treeMain.AllowDrop = true;
            this.treeMain.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colName});
            this.treeMain.DataSource = this.bsList;
            this.treeMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMain.Location = new System.Drawing.Point(0, 0);
            this.treeMain.Name = "treeMain";
            this.treeMain.OptionsBehavior.DragNodes = true;
            this.treeMain.OptionsBehavior.Editable = false;
            this.treeMain.OptionsView.ShowColumns = false;
            this.treeMain.OptionsView.ShowIndicator = false;
            this.treeMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbType});
            this.treeMain.Size = new System.Drawing.Size(199, 139);
            this.treeMain.StateImageList = this.imageList1;
            this.treeMain.TabIndex = 5;
            this.treeMain.GetStateImage += new DevExpress.XtraTreeList.GetStateImageEventHandler(this.treeMain_GetStateImage);
            this.treeMain.DoubleClick += new System.EventHandler(this.treeMain_DoubleClick);
            // 
            // colName
            // 
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            this.colName.Width = 169;
            // 
            // rcmbType
            // 
            this.rcmbType.AutoHeight = false;
            this.rcmbType.Name = "rcmbType";
            this.rcmbType.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Data_16.png");
            this.imageList1.Images.SetKeyName(1, "User_16.png");
            // 
            // OrgnizationJobSelectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeMain);
            this.Name = "OrgnizationJobSelectControl";
            this.Size = new System.Drawing.Size(199, 139);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        private ICP.Framework.ClientComponents.Controls.LWTreeGridControl treeMain;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colName;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbType;
        private System.Windows.Forms.ImageList imageList1;

    }
}
