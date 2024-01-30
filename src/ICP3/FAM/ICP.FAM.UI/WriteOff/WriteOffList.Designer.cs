namespace ICP.FAM.UI.WriteOff
{
    partial class WriteOffList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WriteOffList));
            this.cmbWay = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imgType = new System.Windows.Forms.ImageList();
            this.bsList = new System.Windows.Forms.BindingSource();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.gvWriteOffList = new ICP.Framework.ClientComponents.Controls.LWGridView();
            this.colIsCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsBullion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankAccount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemitter = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWriteOffDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReachedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAuditBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcolAuditBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pageControl1 = new ICP.Framework.ClientComponents.Controls.PageControl();
            this.colVoidDate = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWriteOffList)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbWay
            // 
            this.cmbWay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbWay.Name = "cmbWay";
            this.cmbWay.SmallImages = this.imgType;
            // 
            // imgType
            // 
            this.imgType.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgType.ImageStream")));
            this.imgType.TransparentColor = System.Drawing.Color.Transparent;
            this.imgType.Images.SetKeyName(0, "+.png");
            this.imgType.Images.SetKeyName(1, "-.png");
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.WriteOffItemList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsList_PositionChanged);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcMain.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcMain.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcMain.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcMain.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcMain.EmbeddedNavigator.TextStringFormat = "Page {0} of {1}";
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvWriteOffList;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(747, 423);
            this.gcMain.TabIndex = 5;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvWriteOffList});
            // 
            // gvWriteOffList
            // 
            this.gvWriteOffList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIsCheck,
            this.colWay,
            this.colIsBullion,
            this.colNo,
            this.colCheckNo,
            this.colCustomerName,
            this.colBankAccount,
            this.colCurrency,
            this.colAmount,
            this.colRemitter,
            this.colWriteOffDate,
            this.colReachedDate,
            this.colVoucherNo,
            this.colAuditBy,
            this.colcolAuditBy,
            this.colCreatedOn,
            this.colVoidDate,
            this.colRemark});
            this.gvWriteOffList.GridControl = this.gcMain;
            this.gvWriteOffList.Name = "gvWriteOffList";
            this.gvWriteOffList.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvWriteOffList.OptionsSelection.MultiSelect = true;
            this.gvWriteOffList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvWriteOffList.OptionsView.ColumnAutoWidth = false;
            this.gvWriteOffList.OptionsView.EnableAppearanceEvenRow = true;
            this.gvWriteOffList.OptionsView.ShowGroupPanel = false;
            this.gvWriteOffList.CustomerSorting += new ICP.Framework.ClientComponents.Controls.CustomerSortingHandler(this.gvWriteOffList_CustomerSorting);
            this.gvWriteOffList.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvWriteOffList_RowCellClick);
            this.gvWriteOffList.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvWriteOffList_RowStyle);
            this.gvWriteOffList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvWriteOffList_KeyDown);
            // 
            // colIsCheck
            // 
            this.colIsCheck.Caption = "选择";
            this.colIsCheck.FieldName = "IsCheck";
            this.colIsCheck.Name = "colIsCheck";
            this.colIsCheck.Width = 45;
            // 
            // colWay
            // 
            this.colWay.Caption = "方向";
            this.colWay.ColumnEdit = this.cmbWay;
            this.colWay.FieldName = "Type";
            this.colWay.Name = "colWay";
            this.colWay.Visible = true;
            this.colWay.VisibleIndex = 0;
            this.colWay.Width = 45;
            // 
            // colIsBullion
            // 
            this.colIsBullion.Caption = "到账";
            this.colIsBullion.FieldName = "HasRecoginized";
            this.colIsBullion.Name = "colIsBullion";
            this.colIsBullion.Visible = true;
            this.colIsBullion.VisibleIndex = 1;
            this.colIsBullion.Width = 45;
            // 
            // colNo
            // 
            this.colNo.Caption = "单号";
            this.colNo.FieldName = "No";
            this.colNo.Name = "colNo";
            this.colNo.Visible = true;
            this.colNo.VisibleIndex = 2;
            this.colNo.Width = 70;
            // 
            // colCheckNo
            // 
            this.colCheckNo.Caption = "支票号";
            this.colCheckNo.FieldName = "CheckNo";
            this.colCheckNo.Name = "colCheckNo";
            this.colCheckNo.Visible = true;
            this.colCheckNo.VisibleIndex = 3;
            this.colCheckNo.Width = 80;
            // 
            // colCustomerName
            // 
            this.colCustomerName.Caption = "客户";
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 4;
            this.colCustomerName.Width = 120;
            // 
            // colBankAccount
            // 
            this.colBankAccount.Caption = "银行账号";
            this.colBankAccount.FieldName = "BankAccount";
            this.colBankAccount.Name = "colBankAccount";
            this.colBankAccount.Visible = true;
            this.colBankAccount.VisibleIndex = 5;
            this.colBankAccount.Width = 120;
            // 
            // colCurrency
            // 
            this.colCurrency.Caption = "币种";
            this.colCurrency.FieldName = "Currency";
            this.colCurrency.Name = "colCurrency";
            this.colCurrency.Visible = true;
            this.colCurrency.VisibleIndex = 6;
            this.colCurrency.Width = 45;
            // 
            // colAmount
            // 
            this.colAmount.Caption = "金额";
            this.colAmount.DisplayFormat.FormatString = "#,##0.00";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 7;
            this.colAmount.Width = 70;
            // 
            // colRemitter
            // 
            this.colRemitter.Caption = "销账人";
            this.colRemitter.FieldName = "CreatedByName";
            this.colRemitter.Name = "colRemitter";
            this.colRemitter.Visible = true;
            this.colRemitter.VisibleIndex = 8;
            // 
            // colWriteOffDate
            // 
            this.colWriteOffDate.Caption = "销账日期";
            this.colWriteOffDate.DisplayFormat.FormatString = "g";
            this.colWriteOffDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colWriteOffDate.FieldName = "WriteOffDate";
            this.colWriteOffDate.Name = "colWriteOffDate";
            this.colWriteOffDate.Visible = true;
            this.colWriteOffDate.VisibleIndex = 9;
            // 
            // colReachedDate
            // 
            this.colReachedDate.Caption = "到账日期";
            this.colReachedDate.FieldName = "ReachedDate";
            this.colReachedDate.Name = "colReachedDate";
            this.colReachedDate.Visible = true;
            this.colReachedDate.VisibleIndex = 10;
            // 
            // colVoucherNo
            // 
            this.colVoucherNo.Caption = "凭证号";
            this.colVoucherNo.FieldName = "VoucherSeqNo";
            this.colVoucherNo.Name = "colVoucherNo";
            this.colVoucherNo.Visible = true;
            this.colVoucherNo.VisibleIndex = 11;
            this.colVoucherNo.Width = 60;
            // 
            // colAuditBy
            // 
            this.colAuditBy.Caption = "审核人";
            this.colAuditBy.FieldName = "ApprovalByName";
            this.colAuditBy.Name = "colAuditBy";
            this.colAuditBy.Visible = true;
            this.colAuditBy.VisibleIndex = 12;
            // 
            // colcolAuditBy
            // 
            this.colcolAuditBy.Caption = "到账人";
            this.colcolAuditBy.FieldName = "BankByName";
            this.colcolAuditBy.Name = "colcolAuditBy";
            this.colcolAuditBy.Visible = true;
            this.colcolAuditBy.VisibleIndex = 13;
            // 
            // colCreatedOn
            // 
            this.colCreatedOn.Caption = "输入日期";
            this.colCreatedOn.FieldName = "CreateDate";
            this.colCreatedOn.Name = "colCreatedOn";
            this.colCreatedOn.Visible = true;
            this.colCreatedOn.VisibleIndex = 14;
            // 
            // colRemark
            // 
            this.colRemark.Caption = "备注";
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 15;
            // 
            // pageControl1
            // 
            this.pageControl1.CurrentPage = 0;
            this.pageControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pageControl1.Location = new System.Drawing.Point(0, 423);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.Size = new System.Drawing.Size(747, 26);
            this.pageControl1.TabIndex = 6;
            this.pageControl1.TotalPage = 0;
            this.pageControl1.PageChanged += new ICP.Framework.ClientComponents.Controls.PageChangedHandler(this.pageControl1_PageChanged);
            // 
            // colVoidDate
            // 
            this.colVoidDate.Caption = "作废日期";
            this.colVoidDate.FieldName = "VoidDate";
            this.colVoidDate.Name = "colVoidDate";
            this.colVoidDate.Visible = true;
            this.colVoidDate.VisibleIndex = 16;
            // 
            // WriteOffList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.pageControl1);
            this.Name = "WriteOffList";
            this.Size = new System.Drawing.Size(747, 449);
            ((System.ComponentModel.ISupportInitialize)(this.cmbWay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWriteOffList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        private System.Windows.Forms.ImageList imgType;
        private DevExpress.XtraGrid.GridControl gcMain;
        private ICP.Framework.ClientComponents.Controls.LWGridView gvWriteOffList;
        private DevExpress.XtraGrid.Columns.GridColumn colIsCheck;
        private DevExpress.XtraGrid.Columns.GridColumn colWay;
        private DevExpress.XtraGrid.Columns.GridColumn colNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colBankAccount;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colRemitter;
        private DevExpress.XtraGrid.Columns.GridColumn colWriteOffDate;
        private DevExpress.XtraGrid.Columns.GridColumn colReachedDate;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherNo;
        private DevExpress.XtraGrid.Columns.GridColumn colAuditBy;
        private DevExpress.XtraGrid.Columns.GridColumn colcolAuditBy;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedOn;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbWay;
        private DevExpress.XtraGrid.Columns.GridColumn colIsBullion;
        private ICP.Framework.ClientComponents.Controls.PageControl pageControl1;
        private DevExpress.XtraGrid.Columns.GridColumn colVoidDate;
    }
}
