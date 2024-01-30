namespace ICP.FCM.AirExport.UI.Common.Controls
{
    partial class UserOrganizationTreeCheckBox
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserOrganizationTreeCheckBox));
            this.popControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.treeMain = new ICP.Framework.ClientComponents.Controls.LWTreeGridControl();
            this.colCShortName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colEShortName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.bsSource = new System.Windows.Forms.BindingSource(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.popParent = new DevExpress.XtraEditors.PopupContainerEdit();
            ((System.ComponentModel.ISupportInitialize)(this.popControl1)).BeginInit();
            this.popControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popParent.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // popControl1
            // 
            this.popControl1.Controls.Add(this.treeMain);
            this.popControl1.Location = new System.Drawing.Point(3, 27);
            this.popControl1.Name = "popControl1";
            this.popControl1.Size = new System.Drawing.Size(275, 118);
            this.popControl1.TabIndex = 6;
            // 
            // treeMain
            // 
            this.treeMain.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.treeMain.Appearance.EvenRow.Options.UseBackColor = true;
            this.treeMain.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.treeMain.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.treeMain.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeMain.Appearance.FocusedCell.Options.UseForeColor = true;
            this.treeMain.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colCShortName,
            this.colEShortName});
            this.treeMain.DataSource = this.bsSource;
            this.treeMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMain.Location = new System.Drawing.Point(0, 0);
            this.treeMain.Name = "treeMain";
            this.treeMain.OptionsBehavior.AllowExpandOnDblClick = false;
            this.treeMain.OptionsBehavior.Editable = false;
            this.treeMain.OptionsBehavior.PopulateServiceColumns = true;
            this.treeMain.OptionsLayout.AddNewColumns = false;
            this.treeMain.OptionsSelection.InvertSelection = true;
            this.treeMain.OptionsView.EnableAppearanceEvenRow = true;
            this.treeMain.OptionsView.ShowCheckBoxes = true;
            this.treeMain.OptionsView.ShowColumns = false;
            this.treeMain.OptionsView.ShowIndicator = false;
            this.treeMain.Size = new System.Drawing.Size(275, 118);
            this.treeMain.StateImageList = this.imageList1;
            this.treeMain.TabIndex = 0;
            this.treeMain.Click += new System.EventHandler(this.treeMain_Click);
            // 
            // colCShortName
            // 
            this.colCShortName.Caption = "CName";
            this.colCShortName.FieldName = "CShortName";
            this.colCShortName.MinWidth = 35;
            this.colCShortName.Name = "colCShortName";
            this.colCShortName.Visible = true;
            this.colCShortName.VisibleIndex = 0;
            this.colCShortName.Width = 91;
            // 
            // colEShortName
            // 
            this.colEShortName.Caption = "EName";
            this.colEShortName.FieldName = "EShortName";
            this.colEShortName.Name = "colEShortName";
            this.colEShortName.Visible = true;
            this.colEShortName.VisibleIndex = 1;
            // 
            // bsSource
            // 
            this.bsSource.DataSource = typeof(ICP.Sys.ServiceInterface.DataObjects.UserOrganizationTreeList);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "UnChecked.png");
            this.imageList1.Images.SetKeyName(1, "Checked.png");
            // 
            // popParent
            // 
            this.popParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.popParent.Location = new System.Drawing.Point(0, 0);
            this.popParent.Name = "popParent";
            this.popParent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.popParent.Properties.PopupControl = this.popControl1;
            this.popParent.Size = new System.Drawing.Size(290, 21);
            this.popParent.TabIndex = 5;
            this.popParent.TabStop = false;
            // 
            // UserOrganizationTreeCheckBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.popControl1);
            this.Controls.Add(this.popParent);
            this.Name = "UserOrganizationTreeCheckBox";
            this.Size = new System.Drawing.Size(290, 187);
            ((System.ComponentModel.ISupportInitialize)(this.popControl1)).EndInit();
            this.popControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popParent.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PopupContainerControl popControl1;
        private ICP.Framework.ClientComponents.Controls.LWTreeGridControl treeMain;
        private DevExpress.XtraEditors.PopupContainerEdit popParent;
        private System.Windows.Forms.BindingSource bsSource;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCShortName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colEShortName;
        private System.Windows.Forms.ImageList imageList1;
    }
}
