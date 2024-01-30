namespace ICP.Business.Common.UI.Contact
{
    partial class UCContactStaffListPart
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
            this.bsStaff = new System.Windows.Forms.BindingSource(this.components);
            this.grpStaff = new DevExpress.XtraEditors.GroupControl();
            this.gcStaff = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvStaffList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colRole = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbRole = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbName = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.colMail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.hyperlinkmail = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.colTel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bsStaff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpStaff)).BeginInit();
            this.grpStaff.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcStaff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStaffList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyperlinkmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // grpStaff
            // 
            this.grpStaff.Controls.Add(this.gcStaff);
            this.grpStaff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpStaff.Location = new System.Drawing.Point(0, 0);
            this.grpStaff.Name = "grpStaff";
            this.grpStaff.Size = new System.Drawing.Size(728, 142);
            this.grpStaff.TabIndex = 0;
            this.grpStaff.Text = "Assistant";
            // 
            // gcStaff
            // 
            this.gcStaff.DataSource = this.bsStaff;
            this.gcStaff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcStaff.Location = new System.Drawing.Point(2, 23);
            this.gcStaff.MainView = this.gvStaffList;
            this.gcStaff.Name = "gcStaff";
            this.gcStaff.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.hyperlinkmail,
            this.cmbRole,
            this.cmbName});
            this.gcStaff.Size = new System.Drawing.Size(724, 117);
            this.gcStaff.TabIndex = 6;
            this.gcStaff.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvStaffList});
            this.gcStaff.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gcStaff_MouseClick);
            // 
            // gvStaffList
            // 
            this.gvStaffList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRole,
            this.colName,
            this.colMail,
            this.colTel});
            this.gvStaffList.GridControl = this.gcStaff;
            this.gvStaffList.Name = "gvStaffList";
            this.gvStaffList.OptionsView.ShowGroupPanel = false;
            this.gvStaffList.OptionsView.ShowIndicator = false;
            this.gvStaffList.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridView1_RowCellClick);
            this.gvStaffList.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvStaffList_RowStyle);
            // 
            // colRole
            // 
            this.colRole.Caption = "Role";
            this.colRole.ColumnEdit = this.cmbRole;
            this.colRole.FieldName = "Role";
            this.colRole.Name = "colRole";
            this.colRole.ToolTip = "Choose The Role";
            this.colRole.Visible = true;
            this.colRole.VisibleIndex = 0;
            this.colRole.Width = 94;
            // 
            // cmbRole
            // 
            this.cmbRole.AutoHeight = false;
            this.cmbRole.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbRole.Name = "cmbRole";
            // 
            // colName
            // 
            this.colName.Caption = "Name";
            this.colName.ColumnEdit = this.cmbName;
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 1;
            this.colName.Width = 109;
            // 
            // cmbName
            // 
            this.cmbName.AutoHeight = false;
            this.cmbName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbName.Name = "cmbName";
            // 
            // colMail
            // 
            this.colMail.Caption = "Mail";
            this.colMail.ColumnEdit = this.hyperlinkmail;
            this.colMail.FieldName = "Mail";
            this.colMail.Name = "colMail";
            this.colMail.OptionsColumn.AllowEdit = false;
            this.colMail.Visible = true;
            this.colMail.VisibleIndex = 2;
            this.colMail.Width = 225;
            // 
            // hyperlinkmail
            // 
            this.hyperlinkmail.AutoHeight = false;
            this.hyperlinkmail.Name = "hyperlinkmail";
            // 
            // colTel
            // 
            this.colTel.Caption = "Tel";
            this.colTel.FieldName = "Tel";
            this.colTel.Name = "colTel";
            this.colTel.OptionsColumn.AllowEdit = false;
            this.colTel.Visible = true;
            this.colTel.VisibleIndex = 3;
            this.colTel.Width = 148;
            // 
            // popupMenu1
            // 
            this.popupMenu1.Name = "popupMenu1";
            // 
            // UCContactStaffListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpStaff);
            this.Name = "UCContactStaffListPart";
            this.Size = new System.Drawing.Size(728, 142);
            ((System.ComponentModel.ISupportInitialize)(this.bsStaff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpStaff)).EndInit();
            this.grpStaff.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcStaff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStaffList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyperlinkmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsStaff;
        private DevExpress.XtraEditors.GroupControl grpStaff;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gcStaff;
        private DevExpress.XtraGrid.Views.Grid.GridView gvStaffList;
        private DevExpress.XtraGrid.Columns.GridColumn colRole;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colMail;
        private DevExpress.XtraGrid.Columns.GridColumn colTel;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit hyperlinkmail;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmbRole;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cmbName;
    }
}
