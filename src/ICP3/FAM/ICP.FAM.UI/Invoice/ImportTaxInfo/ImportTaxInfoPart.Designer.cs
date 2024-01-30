namespace ICP.FAM.UI.Invoice
{
    partial class ImportTaxInfoPart
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
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.btnOpenFile = new DevExpress.XtraEditors.SimpleButton();
            this.txtFileName = new DevExpress.XtraEditors.TextEdit();
            this.rgType = new DevExpress.XtraEditors.RadioGroup();
            this.btnImport = new DevExpress.XtraEditors.SimpleButton();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new ICP.Framework.ClientComponents.Controls.LWGridView();
            this.colSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShortName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTaxNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAddressTel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankAccountNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlCustomerInfo = new DevExpress.XtraEditors.PanelControl();
            this.btnUnAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnAll = new DevExpress.XtraEditors.SimpleButton();
            this.txtImportName = new DevExpress.XtraEditors.TextEdit();
            this.cmbImportType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCustomerInfo)).BeginInit();
            this.pnlCustomerInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtImportName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbImportType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.btnOpenFile);
            this.pnlTop.Controls.Add(this.txtFileName);
            this.pnlTop.Controls.Add(this.rgType);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(776, 34);
            this.pnlTop.TabIndex = 1;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(742, 7);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(28, 23);
            this.btnOpenFile.TabIndex = 4;
            this.btnOpenFile.Text = "...";
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(363, 8);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Properties.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(374, 21);
            this.txtFileName.TabIndex = 3;
            // 
            // rgType
            // 
            this.rgType.Location = new System.Drawing.Point(3, 3);
            this.rgType.Name = "rgType";
            this.rgType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "导入客户"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("2", "导入操作员"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("3", "导入银行帐号"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("4", "商品编码")});
            this.rgType.Size = new System.Drawing.Size(354, 27);
            this.rgType.TabIndex = 2;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(625, 5);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(65, 23);
            this.btnImport.TabIndex = 4;
            this.btnImport.Text = "导入";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.gcMain);
            this.pnlMain.Controls.Add(this.pnlCustomerInfo);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 34);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(776, 404);
            this.pnlMain.TabIndex = 2;
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(2, 33);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gcMain.Size = new System.Drawing.Size(772, 369);
            this.gcMain.TabIndex = 3;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FAM.UI.Invoice.ImportTaxCustomerInfo);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSelect,
            this.colCode,
            this.colName,
            this.colShortName,
            this.colRemark,
            this.colTaxNo,
            this.colAddressTel,
            this.colBankAccountNo});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.RowAutoHeight = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // colSelect
            // 
            this.colSelect.Caption = "选择";
            this.colSelect.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colSelect.FieldName = "IsSelect";
            this.colSelect.Name = "colSelect";
            this.colSelect.Visible = true;
            this.colSelect.VisibleIndex = 0;
            this.colSelect.Width = 46;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // colCode
            // 
            this.colCode.Caption = "代码";
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.OptionsColumn.AllowEdit = false;
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 1;
            this.colCode.Width = 63;
            // 
            // colName
            // 
            this.colName.Caption = "名称";
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 2;
            this.colName.Width = 146;
            // 
            // colShortName
            // 
            this.colShortName.Caption = "简码";
            this.colShortName.FieldName = "ShortName";
            this.colShortName.Name = "colShortName";
            this.colShortName.OptionsColumn.AllowEdit = false;
            this.colShortName.Visible = true;
            this.colShortName.VisibleIndex = 3;
            this.colShortName.Width = 50;
            // 
            // colRemark
            // 
            this.colRemark.Caption = "备注";
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.OptionsColumn.AllowEdit = false;
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 4;
            // 
            // colTaxNo
            // 
            this.colTaxNo.Caption = "税号";
            this.colTaxNo.FieldName = "TaxNo";
            this.colTaxNo.Name = "colTaxNo";
            this.colTaxNo.OptionsColumn.AllowEdit = false;
            this.colTaxNo.Visible = true;
            this.colTaxNo.VisibleIndex = 5;
            this.colTaxNo.Width = 69;
            // 
            // colAddressTel
            // 
            this.colAddressTel.Caption = "地址电话";
            this.colAddressTel.FieldName = "AddressTel";
            this.colAddressTel.Name = "colAddressTel";
            this.colAddressTel.OptionsColumn.AllowEdit = false;
            this.colAddressTel.Visible = true;
            this.colAddressTel.VisibleIndex = 6;
            this.colAddressTel.Width = 180;
            // 
            // colBankAccountNo
            // 
            this.colBankAccountNo.Caption = "银行帐号";
            this.colBankAccountNo.FieldName = "BankAccountNo";
            this.colBankAccountNo.Name = "colBankAccountNo";
            this.colBankAccountNo.OptionsColumn.AllowEdit = false;
            this.colBankAccountNo.Visible = true;
            this.colBankAccountNo.VisibleIndex = 7;
            this.colBankAccountNo.Width = 226;
            // 
            // pnlCustomerInfo
            // 
            this.pnlCustomerInfo.Controls.Add(this.btnImport);
            this.pnlCustomerInfo.Controls.Add(this.btnUnAll);
            this.pnlCustomerInfo.Controls.Add(this.btnAll);
            this.pnlCustomerInfo.Controls.Add(this.txtImportName);
            this.pnlCustomerInfo.Controls.Add(this.cmbImportType);
            this.pnlCustomerInfo.Controls.Add(this.labelControl2);
            this.pnlCustomerInfo.Controls.Add(this.labelControl1);
            this.pnlCustomerInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCustomerInfo.Location = new System.Drawing.Point(2, 2);
            this.pnlCustomerInfo.Name = "pnlCustomerInfo";
            this.pnlCustomerInfo.Size = new System.Drawing.Size(772, 31);
            this.pnlCustomerInfo.TabIndex = 4;
            // 
            // btnUnAll
            // 
            this.btnUnAll.Location = new System.Drawing.Point(132, 5);
            this.btnUnAll.Name = "btnUnAll";
            this.btnUnAll.Size = new System.Drawing.Size(75, 23);
            this.btnUnAll.TabIndex = 3;
            this.btnUnAll.Text = "反选";
            this.btnUnAll.Click += new System.EventHandler(this.btnUnAll_Click);
            // 
            // btnAll
            // 
            this.btnAll.Location = new System.Drawing.Point(27, 5);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(75, 23);
            this.btnAll.TabIndex = 3;
            this.btnAll.Text = "选中";
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // txtImportName
            // 
            this.txtImportName.Location = new System.Drawing.Point(502, 6);
            this.txtImportName.Name = "txtImportName";
            this.txtImportName.Size = new System.Drawing.Size(112, 21);
            this.txtImportName.TabIndex = 2;
            // 
            // cmbImportType
            // 
            this.cmbImportType.Location = new System.Drawing.Point(346, 6);
            this.cmbImportType.Name = "cmbImportType";
            this.cmbImportType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbImportType.Size = new System.Drawing.Size(100, 21);
            this.cmbImportType.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(452, 9);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "所属公司";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(291, 9);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "导入类型";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ImportTaxInfoPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTop);
            this.Name = "ImportTaxInfoPart";
            this.Size = new System.Drawing.Size(776, 438);
            this.Controls.SetChildIndex(this.pnlTop, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCustomerInfo)).EndInit();
            this.pnlCustomerInfo.ResumeLayout(false);
            this.pnlCustomerInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtImportName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbImportType.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlTop;
        private DevExpress.XtraEditors.RadioGroup rgType;
        private DevExpress.XtraEditors.TextEdit txtFileName;
        private DevExpress.XtraEditors.SimpleButton btnImport;
        private DevExpress.XtraEditors.SimpleButton btnOpenFile;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        private ICP.Framework.ClientComponents.Controls.LWGridView gvMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraGrid.Columns.GridColumn colSelect;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colTaxNo;
        private DevExpress.XtraGrid.Columns.GridColumn colAddressTel;
        private DevExpress.XtraGrid.Columns.GridColumn colBankAccountNo;
        private DevExpress.XtraEditors.PanelControl pnlCustomerInfo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtImportName;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbImportType;
        private DevExpress.XtraEditors.SimpleButton btnUnAll;
        private DevExpress.XtraEditors.SimpleButton btnAll;
        private DevExpress.XtraGrid.Columns.GridColumn colShortName;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;
    }
}
