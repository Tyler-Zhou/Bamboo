namespace ICP.FAM.UI.BankTransaction.Finder
{
    partial class FinderListPart
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
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBusinessNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFlowWaterNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRelativeAccountNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTransactionAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperationDateTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDebitCreditFlag = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.BankTransactionInfo);
            this.bsList.PositionChanged += new System.EventHandler(this.bsMainList_PositionChanged);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(944, 248);
            this.gcMain.TabIndex = 0;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colBusinessNO,
            this.colFlowWaterNO,
            this.colAccountNO,
            this.colRelativeAccountNo,
            this.colCurrencyName,
            this.colTransactionAmount,
            this.colOperationDateTime,
            this.colDebitCreditFlag,
            this.colRemark});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(this.gvMain_BeforeLeaveRow);
            // 
            // colID
            // 
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colBusinessNO
            // 
            this.colBusinessNO.Caption = "Business NO";
            this.colBusinessNO.FieldName = "BusinessNO";
            this.colBusinessNO.Name = "colBusinessNO";
            this.colBusinessNO.Visible = true;
            this.colBusinessNO.VisibleIndex = 0;
            this.colBusinessNO.Width = 115;
            // 
            // colFlowWaterNO
            // 
            this.colFlowWaterNO.FieldName = "FlowWaterNO";
            this.colFlowWaterNO.Name = "colFlowWaterNO";
            this.colFlowWaterNO.Visible = true;
            this.colFlowWaterNO.VisibleIndex = 1;
            this.colFlowWaterNO.Width = 115;
            // 
            // colAccountNO
            // 
            this.colAccountNO.FieldName = "AccountNO";
            this.colAccountNO.Name = "colAccountNO";
            this.colAccountNO.Visible = true;
            this.colAccountNO.VisibleIndex = 2;
            this.colAccountNO.Width = 115;
            // 
            // colRelativeAccountNo
            // 
            this.colRelativeAccountNo.FieldName = "RelativeAccountNo";
            this.colRelativeAccountNo.Name = "colRelativeAccountNo";
            this.colRelativeAccountNo.Visible = true;
            this.colRelativeAccountNo.VisibleIndex = 3;
            this.colRelativeAccountNo.Width = 115;
            // 
            // colCurrencyName
            // 
            this.colCurrencyName.FieldName = "CurrencyName";
            this.colCurrencyName.Name = "colCurrencyName";
            this.colCurrencyName.Visible = true;
            this.colCurrencyName.VisibleIndex = 4;
            this.colCurrencyName.Width = 115;
            // 
            // colTransactionAmount
            // 
            this.colTransactionAmount.FieldName = "TransactionAmount";
            this.colTransactionAmount.Name = "colTransactionAmount";
            this.colTransactionAmount.Visible = true;
            this.colTransactionAmount.VisibleIndex = 5;
            this.colTransactionAmount.Width = 115;
            // 
            // colOperationDateTime
            // 
            this.colOperationDateTime.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.colOperationDateTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colOperationDateTime.FieldName = "OperationDateTime";
            this.colOperationDateTime.Name = "colOperationDateTime";
            this.colOperationDateTime.Visible = true;
            this.colOperationDateTime.VisibleIndex = 6;
            this.colOperationDateTime.Width = 120;
            // 
            // colDebitCreditFlag
            // 
            this.colDebitCreditFlag.FieldName = "DebitCreditFlag";
            this.colDebitCreditFlag.Name = "colDebitCreditFlag";
            this.colDebitCreditFlag.Visible = true;
            this.colDebitCreditFlag.VisibleIndex = 7;
            this.colDebitCreditFlag.Width = 113;
            // 
            // colRemark
            // 
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            // 
            // FinderListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.gcMain);
            this.Name = "FinderListPart";
            this.Size = new System.Drawing.Size(944, 248);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.BindingSource bsList;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        protected DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colBusinessNO;
        private DevExpress.XtraGrid.Columns.GridColumn colFlowWaterNO;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountNO;
        private DevExpress.XtraGrid.Columns.GridColumn colRelativeAccountNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyName;
        private DevExpress.XtraGrid.Columns.GridColumn colTransactionAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationDateTime;
        private DevExpress.XtraGrid.Columns.GridColumn colDebitCreditFlag;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;
    }
}
