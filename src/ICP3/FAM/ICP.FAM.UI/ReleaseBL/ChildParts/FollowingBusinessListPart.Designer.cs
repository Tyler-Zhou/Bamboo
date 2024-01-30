namespace ICP.FAM.UI.ReleaseBL
{
    partial class FollowingBusinessListPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FollowingBusinessListPart));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbCustomer = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.btnShow = new DevExpress.XtraEditors.SimpleButton();
            this.labBillTo = new DevExpress.XtraEditors.LabelControl();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colOperationNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillToName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRefNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperationDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProfitDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerService = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFiler = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbCustomer);
            this.panel1.Controls.Add(this.btnShow);
            this.panel1.Controls.Add(this.labBillTo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(691, 33);
            this.panel1.TabIndex = 8;
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.Location = new System.Drawing.Point(77, 5);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCustomer.Size = new System.Drawing.Size(354, 21);
            this.cmbCustomer.TabIndex = 4;
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(447, 5);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(81, 21);
            this.btnShow.TabIndex = 6;
            this.btnShow.Text = "&Show";
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // labBillTo
            // 
            this.labBillTo.Location = new System.Drawing.Point(8, 10);
            this.labBillTo.Name = "labBillTo";
            this.labBillTo.Size = new System.Drawing.Size(28, 14);
            this.labBillTo.TabIndex = 5;
            this.labBillTo.Text = "BillTo";
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.FollowingBusinessList);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 33);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemHyperLinkEdit1});
            this.gcMain.Size = new System.Drawing.Size(691, 236);
            this.gcMain.TabIndex = 9;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colOperationNO,
            this.colCustomerName,
            this.colBillToName,
            this.colRefNO,
            this.colOperationDescription,
            this.colDR,
            this.colCR,
            this.colProfitDescription,
            this.colCustomerService,
            this.colFiler});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvMain_RowCellClick);
            // 
            // colOperationNO
            // 
            this.colOperationNO.Caption = "OperationNO";
            this.colOperationNO.ColumnEdit = this.repositoryItemHyperLinkEdit1;
            this.colOperationNO.FieldName = "OperationNO";
            this.colOperationNO.Name = "colOperationNO";
            this.colOperationNO.OptionsColumn.AllowEdit = false;
            this.colOperationNO.Visible = true;
            this.colOperationNO.VisibleIndex = 0;
            this.colOperationNO.Width = 110;
            // 
            // repositoryItemHyperLinkEdit1
            // 
            this.repositoryItemHyperLinkEdit1.AutoHeight = false;
            this.repositoryItemHyperLinkEdit1.Name = "repositoryItemHyperLinkEdit1";
            // 
            // colCustomerName
            // 
            this.colCustomerName.Caption = "Customer";
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.OptionsColumn.AllowEdit = false;
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 1;
            this.colCustomerName.Width = 132;
            // 
            // colBillToName
            // 
            this.colBillToName.Caption = "BillTo";
            this.colBillToName.FieldName = "BillToName";
            this.colBillToName.Name = "colBillToName";
            this.colBillToName.Visible = true;
            this.colBillToName.VisibleIndex = 2;
            this.colBillToName.Width = 127;
            // 
            // colRefNO
            // 
            this.colRefNO.Caption = "RefNO";
            this.colRefNO.FieldName = "RefNO";
            this.colRefNO.Name = "colRefNO";
            this.colRefNO.OptionsColumn.AllowEdit = false;
            this.colRefNO.Visible = true;
            this.colRefNO.VisibleIndex = 3;
            this.colRefNO.Width = 137;
            // 
            // colOperationDescription
            // 
            this.colOperationDescription.Caption = "Description";
            this.colOperationDescription.FieldName = "OperationDescription";
            this.colOperationDescription.Name = "colOperationDescription";
            this.colOperationDescription.OptionsColumn.AllowEdit = false;
            this.colOperationDescription.Visible = true;
            this.colOperationDescription.VisibleIndex = 4;
            this.colOperationDescription.Width = 147;
            // 
            // colDR
            // 
            this.colDR.Caption = "AR";
            this.colDR.FieldName = "ARDescription";
            this.colDR.Name = "colDR";
            this.colDR.OptionsColumn.AllowEdit = false;
            this.colDR.Visible = true;
            this.colDR.VisibleIndex = 5;
            this.colDR.Width = 100;
            // 
            // colCR
            // 
            this.colCR.Caption = "AP";
            this.colCR.FieldName = "APDescription";
            this.colCR.Name = "colCR";
            this.colCR.OptionsColumn.AllowEdit = false;
            this.colCR.Visible = true;
            this.colCR.VisibleIndex = 6;
            this.colCR.Width = 100;
            // 
            // colProfitDescription
            // 
            this.colProfitDescription.Caption = "Profit";
            this.colProfitDescription.FieldName = "ProfitDescription";
            this.colProfitDescription.Name = "colProfitDescription";
            this.colProfitDescription.OptionsColumn.AllowEdit = false;
            this.colProfitDescription.Visible = true;
            this.colProfitDescription.VisibleIndex = 7;
            this.colProfitDescription.Width = 100;
            // 
            // colCustomerService
            // 
            this.colCustomerService.Caption = "CustomerServiceName";
            this.colCustomerService.FieldNameSortGroup = "CustomerService";
            this.colCustomerService.Name = "colCustomerService";
            this.colCustomerService.Visible = true;
            this.colCustomerService.VisibleIndex = 8;
            // 
            // colFiler
            // 
            this.colFiler.Caption = "Filer";
            this.colFiler.FieldName = "SIBy";
            this.colFiler.Name = "colFiler";
            this.colFiler.Visible = true;
            this.colFiler.VisibleIndex = 9;
            // 
            // FollowingBusinessListPart
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.panel1);
            this.Enabled = false;
            this.Name = "FollowingBusinessListPart";
            this.Size = new System.Drawing.Size(691, 269);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbCustomer;
        private DevExpress.XtraEditors.SimpleButton btnShow;
        private DevExpress.XtraEditors.LabelControl labBillTo;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationNO;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colRefNO;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colDR;
        private DevExpress.XtraGrid.Columns.GridColumn colCR;
        private DevExpress.XtraGrid.Columns.GridColumn colProfitDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colBillToName;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerService;
        private DevExpress.XtraGrid.Columns.GridColumn colFiler;



    }
}
