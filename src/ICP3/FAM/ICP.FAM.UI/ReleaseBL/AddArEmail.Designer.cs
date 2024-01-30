namespace ICP.FAM.UI.ReleaseBL
{
    partial class AddArEmail
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
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.cmbAREmail = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labAREmail = new DevExpress.XtraEditors.LabelControl();
            this.labTips = new DevExpress.XtraEditors.LabelControl();
            this.btDel = new DevExpress.XtraEditors.SimpleButton();
            this.gcCustomer = new DevExpress.XtraGrid.GridControl();
            this.customerCarrierObjectsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvCustomer = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEMail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAr = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemPopupContainerEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit();
            this.repositoryItemCheckedComboBoxEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.repositoryItemCheckEditType = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAREmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerCarrierObjectsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPopupContainerEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditType)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(423, 25);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(595, 25);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cmbAREmail
            // 
            this.cmbAREmail.EditValue = "";
            this.cmbAREmail.Location = new System.Drawing.Point(74, 27);
            this.cmbAREmail.Name = "cmbAREmail";
            this.cmbAREmail.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbAREmail.Properties.Appearance.Options.UseBackColor = true;
            this.cmbAREmail.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbAREmail.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cmbAREmail.Size = new System.Drawing.Size(343, 21);
            this.cmbAREmail.TabIndex = 38;
            // 
            // labAREmail
            // 
            this.labAREmail.Location = new System.Drawing.Point(8, 30);
            this.labAREmail.Name = "labAREmail";
            this.labAREmail.Size = new System.Drawing.Size(60, 14);
            this.labAREmail.TabIndex = 37;
            this.labAREmail.Text = "收款联系人";
            // 
            // labTips
            // 
            this.labTips.Location = new System.Drawing.Point(74, 7);
            this.labTips.Name = "labTips";
            this.labTips.Size = new System.Drawing.Size(118, 14);
            this.labTips.TabIndex = 39;
            this.labTips.Text = "(多个邮箱请以；隔开)";
            // 
            // btDel
            // 
            this.btDel.Location = new System.Drawing.Point(508, 25);
            this.btDel.Name = "btDel";
            this.btDel.Size = new System.Drawing.Size(75, 23);
            this.btDel.TabIndex = 40;
            this.btDel.Text = "&Delete";
            this.btDel.Click += new System.EventHandler(this.btDel_Click);
            // 
            // gcCustomer
            // 
            this.gcCustomer.DataSource = this.customerCarrierObjectsBindingSource;
            this.gcCustomer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gcCustomer.Location = new System.Drawing.Point(0, 69);
            this.gcCustomer.MainView = this.gvCustomer;
            this.gcCustomer.Name = "gcCustomer";
            this.gcCustomer.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemPopupContainerEdit1,
            this.repositoryItemCheckedComboBoxEdit,
            this.repositoryItemCheckEditType});
            this.gcCustomer.Size = new System.Drawing.Size(699, 297);
            this.gcCustomer.TabIndex = 41;
            this.gcCustomer.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCustomer});
            // 
            // customerCarrierObjectsBindingSource
            // 
            this.customerCarrierObjectsBindingSource.DataSource = typeof(ICP.FCM.Common.ServiceInterface.DataObjects.CustomerCarrierObjects);
            // 
            // gvCustomer
            // 
            this.gvCustomer.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colName,
            this.colEMail,
            this.colAr});
            this.gvCustomer.GridControl = this.gcCustomer;
            this.gvCustomer.Name = "gvCustomer";
            this.gvCustomer.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvCustomer.OptionsBehavior.Editable = false;
            this.gvCustomer.OptionsView.ColumnAutoWidth = false;
            this.gvCustomer.OptionsView.ShowGroupedColumns = true;
            this.gvCustomer.OptionsView.ShowGroupPanel = false;
            this.gvCustomer.OptionsView.ShowIndicator = false;
            this.gvCustomer.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvCustomer_FocusedRowChanged);
            // 
            // colName
            // 
            this.colName.Caption = "CustomerName";
            this.colName.FieldName = "CustomerName";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            this.colName.Width = 169;
            // 
            // colEMail
            // 
            this.colEMail.Caption = "EMail";
            this.colEMail.FieldName = "Mail";
            this.colEMail.Name = "colEMail";
            this.colEMail.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colEMail.OptionsFilter.AllowFilter = false;
            this.colEMail.Visible = true;
            this.colEMail.VisibleIndex = 1;
            this.colEMail.Width = 448;
            // 
            // colAr
            // 
            this.colAr.Caption = "IsAr";
            this.colAr.FieldName = "AR";
            this.colAr.Name = "colAr";
            this.colAr.Visible = true;
            this.colAr.VisibleIndex = 2;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // repositoryItemPopupContainerEdit1
            // 
            this.repositoryItemPopupContainerEdit1.AutoHeight = false;
            this.repositoryItemPopupContainerEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemPopupContainerEdit1.Name = "repositoryItemPopupContainerEdit1";
            // 
            // repositoryItemCheckedComboBoxEdit
            // 
            this.repositoryItemCheckedComboBoxEdit.AutoHeight = false;
            this.repositoryItemCheckedComboBoxEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCheckedComboBoxEdit.DisplayMember = "StageName";
            this.repositoryItemCheckedComboBoxEdit.Name = "repositoryItemCheckedComboBoxEdit";
            this.repositoryItemCheckedComboBoxEdit.ValueMember = "Stage";
            // 
            // repositoryItemCheckEditType
            // 
            this.repositoryItemCheckEditType.AutoHeight = false;
            this.repositoryItemCheckEditType.Name = "repositoryItemCheckEditType";
            // 
            // AddArEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcCustomer);
            this.Controls.Add(this.btDel);
            this.Controls.Add(this.labTips);
            this.Controls.Add(this.cmbAREmail);
            this.Controls.Add(this.labAREmail);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnClose);
            this.Name = "AddArEmail";
            this.Size = new System.Drawing.Size(699, 366);
            this.Load += new System.EventHandler(this.AddArEmail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbAREmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerCarrierObjectsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPopupContainerEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.ComboBoxEdit cmbAREmail;
        private DevExpress.XtraEditors.LabelControl labAREmail;
        private DevExpress.XtraEditors.LabelControl labTips;
        private DevExpress.XtraEditors.SimpleButton btDel;
        private DevExpress.XtraGrid.GridControl gcCustomer;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCustomer;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colEMail;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit repositoryItemCheckedComboBoxEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditType;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit repositoryItemPopupContainerEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colAr;
        private System.Windows.Forms.BindingSource customerCarrierObjectsBindingSource;
    }
}
