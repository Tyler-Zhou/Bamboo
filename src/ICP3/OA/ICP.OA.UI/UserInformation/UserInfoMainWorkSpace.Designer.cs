namespace ICP.OA.UI.UserInformation
{
    partial class UserInfoMainWorkSpace
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
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.trvUserInfo = new System.Windows.Forms.TreeView();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._Name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.JobName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MobilePhone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TelePhone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Department = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Email = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Fax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.UserID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.paHeadSpace = new DevExpress.XtraEditors.PanelControl();
            this.paLogo = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paHeadSpace)).BeginInit();
            this.paHeadSpace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // trvUserInfo
            // 
            this.trvUserInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.trvUserInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvUserInfo.Location = new System.Drawing.Point(0, 34);
            this.trvUserInfo.Name = "trvUserInfo";
            this.trvUserInfo.Size = new System.Drawing.Size(205, 449);
            this.trvUserInfo.TabIndex = 16;
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(205, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(507, 486);
            this.gridControl1.TabIndex = 18;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this._Name,
            this.JobName,
            this.MobilePhone,
            this.TelePhone,
            this.Department,
            this.Email,
            this.Fax,
            this.UserID});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            // 
            // _Name
            // 
            this._Name.Caption = "Name";
            this._Name.FieldName = "Name";
            this._Name.Name = "_Name";
            this._Name.Visible = true;
            this._Name.VisibleIndex = 0;
            // 
            // JobName
            // 
            this.JobName.Caption = "JobName";
            this.JobName.FieldName = "JobName";
            this.JobName.Name = "JobName";
            this.JobName.Visible = true;
            this.JobName.VisibleIndex = 1;
            // 
            // MobilePhone
            // 
            this.MobilePhone.Caption = "MobilePhone";
            this.MobilePhone.FieldName = "MobilePhone";
            this.MobilePhone.Name = "MobilePhone";
            this.MobilePhone.Visible = true;
            this.MobilePhone.VisibleIndex = 2;
            // 
            // TelePhone
            // 
            this.TelePhone.Caption = "TelePhone";
            this.TelePhone.FieldName = "TelePhone";
            this.TelePhone.Name = "TelePhone";
            this.TelePhone.Visible = true;
            this.TelePhone.VisibleIndex = 3;
            // 
            // Department
            // 
            this.Department.Caption = "Department";
            this.Department.FieldName = "DepartmentName";
            this.Department.Name = "Department";
            this.Department.Visible = true;
            this.Department.VisibleIndex = 4;
            // 
            // Email
            // 
            this.Email.Caption = "Email";
            this.Email.FieldName = "Email";
            this.Email.Name = "Email";
            this.Email.Visible = true;
            this.Email.VisibleIndex = 5;
            // 
            // Fax
            // 
            this.Fax.Caption = "Fax";
            this.Fax.FieldName = "Fax";
            this.Fax.Name = "Fax";
            this.Fax.Visible = true;
            this.Fax.VisibleIndex = 6;
            // 
            // UserID
            // 
            this.UserID.Caption = "UserID";
            this.UserID.FieldName = "UserID";
            this.UserID.Name = "UserID";
            // 
            // paHeadSpace
            // 
            this.paHeadSpace.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.paHeadSpace.Controls.Add(this.paLogo);
            this.paHeadSpace.Controls.Add(this.labelControl1);
            this.paHeadSpace.Dock = System.Windows.Forms.DockStyle.Top;
            this.paHeadSpace.Location = new System.Drawing.Point(0, 0);
            this.paHeadSpace.Name = "paHeadSpace";
            this.paHeadSpace.Size = new System.Drawing.Size(712, 35);
            this.paHeadSpace.TabIndex = 19;
            this.paHeadSpace.SizeChanged += new System.EventHandler(this.paHeadSpace_SizeChanged);
            // 
            // paLogo
            // 
            this.paLogo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.paLogo.Location = new System.Drawing.Point(20, 1);
            this.paLogo.Name = "paLogo";
            this.paLogo.Size = new System.Drawing.Size(133, 33);
            this.paLogo.TabIndex = 20;
            // 
            // labelControl1
            // 
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl1.Location = new System.Drawing.Point(229, 11);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(70, 14);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "详细信息";
            // 
            // UserInfoMainWorkSpace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.paHeadSpace);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.trvUserInfo);
            this.Name = "UserInfoMainWorkSpace";
            this.Size = new System.Drawing.Size(712, 486);
            this.Controls.SetChildIndex(this.trvUserInfo, 0);
            this.Controls.SetChildIndex(this.gridControl1, 0);
            this.Controls.SetChildIndex(this.paHeadSpace, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paHeadSpace)).EndInit();
            this.paHeadSpace.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.paLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraBars.Docking.DockManager dockManager1;
        private System.Windows.Forms.TreeView trvUserInfo;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn _Name;
        private DevExpress.XtraGrid.Columns.GridColumn JobName;
        private DevExpress.XtraEditors.PanelControl paHeadSpace;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn MobilePhone;
        private DevExpress.XtraGrid.Columns.GridColumn TelePhone;
        private DevExpress.XtraGrid.Columns.GridColumn Department;
        private DevExpress.XtraGrid.Columns.GridColumn Email;
        private DevExpress.XtraGrid.Columns.GridColumn Fax;
        private DevExpress.XtraGrid.Columns.GridColumn UserID;
        private DevExpress.XtraEditors.PanelControl paLogo;

    }
}
