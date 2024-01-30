namespace ICP.FAM.UI.AccReceControl
{
    partial class AccControlList
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
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new ICP.Framework.ClientComponents.Controls.LWGridView();
            this.colCustomerID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompanyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompanyID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTerms = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreditLimit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShptWorth = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBalance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMainCurBalance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPastDu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPastDue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLastMemo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLastUpdate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLess30 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOver30 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOver45 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOver60 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOver90 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.linkCustomerName = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.colLastUpdateBy = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkCustomerName)).BeginInit();
            this.SuspendLayout();
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
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.linkCustomerName});
            this.gcMain.Size = new System.Drawing.Size(601, 409);
            this.gcMain.TabIndex = 2;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            this.gcMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcMain_KeyDown);
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.CustomerAgingList);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCustomerID,
            this.colCompanyName,
            this.colCustomerName,
            this.colCompanyID,
            this.colTerms,
            this.colCreditLimit,
            this.colShptWorth,
            this.colCurrency,
            this.colBalance,
            this.colCurrent,
            this.colMainCurBalance,
            this.colPastDu,
            this.colPastDue,
            this.colLastMemo,
            this.colLastUpdate,
            this.colLastUpdateBy,
            this.colLess30,
            this.colOver30,
            this.colOver45,
            this.colOver60,
            this.colOver90});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvMain_RowStyle);
            this.gvMain.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvMain_FocusedRowChanged);
            // 
            // colCustomerID
            // 
            this.colCustomerID.FieldName = "CustomerID";
            this.colCustomerID.Name = "colCustomerID";
            // 
            // colCompanyName
            // 
            this.colCompanyName.FieldName = "CompanyName";
            this.colCompanyName.Name = "colCompanyName";
            this.colCompanyName.Visible = true;
            this.colCompanyName.VisibleIndex = 0;
            this.colCompanyName.Width = 80;
            // 
            // colCustomerName
            // 
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 1;
            this.colCustomerName.Width = 120;
            // 
            // colCompanyID
            // 
            this.colCompanyID.FieldName = "CompanyID";
            this.colCompanyID.Name = "colCompanyID";
            // 
            // colTerms
            // 
            this.colTerms.FieldName = "Terms";
            this.colTerms.Name = "colTerms";
            this.colTerms.Visible = true;
            this.colTerms.VisibleIndex = 2;
            // 
            // colCreditLimit
            // 
            this.colCreditLimit.Caption = "CreditLimit";
            this.colCreditLimit.DisplayFormat.FormatString = "N2";
            this.colCreditLimit.FieldName = "CreditLimit";
            this.colCreditLimit.Name = "colCreditLimit";
            this.colCreditLimit.Visible = true;
            this.colCreditLimit.VisibleIndex = 3;
            // 
            // colShptWorth
            // 
            this.colShptWorth.Caption = "ShptWorth";
            this.colShptWorth.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colShptWorth.FieldName = "ShptWorth";
            this.colShptWorth.Name = "colShptWorth";
            this.colShptWorth.Visible = true;
            this.colShptWorth.VisibleIndex = 4;
            // 
            // colCurrency
            // 
            this.colCurrency.FieldName = "Currency";
            this.colCurrency.Name = "colCurrency";
            this.colCurrency.Visible = true;
            this.colCurrency.VisibleIndex = 5;
            // 
            // colBalance
            // 
            this.colBalance.FieldName = "Balance";
            this.colBalance.Name = "colBalance";
            this.colBalance.Visible = true;
            this.colBalance.VisibleIndex = 6;
            // 
            // colCurrent
            // 
            this.colCurrent.FieldName = "Current";
            this.colCurrent.Name = "colCurrent";
            // 
            // colMainCurBalance
            // 
            this.colMainCurBalance.FieldName = "MainCurBalance";
            this.colMainCurBalance.Name = "colMainCurBalance";
            // 
            // colPastDu
            // 
            this.colPastDu.FieldName = "PastDu";
            this.colPastDu.Name = "colPastDu";
            this.colPastDu.Visible = true;
            this.colPastDu.VisibleIndex = 7;
            // 
            // colPastDue
            // 
            this.colPastDue.Caption = "Past Due";
            this.colPastDue.FieldName = "PastDueAmount";
            this.colPastDue.Name = "colPastDue";
            this.colPastDue.Visible = true;
            this.colPastDue.VisibleIndex = 8;
            // 
            // colLastMemo
            // 
            this.colLastMemo.FieldName = "LastMemo";
            this.colLastMemo.Name = "colLastMemo";
            this.colLastMemo.Visible = true;
            this.colLastMemo.VisibleIndex = 9;
            this.colLastMemo.Width = 120;
            // 
            // colLastUpdate
            // 
            this.colLastUpdate.FieldName = "LastUpdate";
            this.colLastUpdate.Name = "colLastUpdate";
            this.colLastUpdate.Visible = true;
            this.colLastUpdate.VisibleIndex = 10;
            // 
            // colLess30
            // 
            this.colLess30.FieldName = "Less30";
            this.colLess30.Name = "colLess30";
            this.colLess30.Visible = true;
            this.colLess30.VisibleIndex = 11;
            // 
            // colOver30
            // 
            this.colOver30.FieldName = "Over30";
            this.colOver30.Name = "colOver30";
            this.colOver30.Visible = true;
            this.colOver30.VisibleIndex = 12;
            // 
            // colOver45
            // 
            this.colOver45.FieldName = "Over45";
            this.colOver45.Name = "colOver45";
            this.colOver45.Visible = true;
            this.colOver45.VisibleIndex = 13;
            // 
            // colOver60
            // 
            this.colOver60.FieldName = "Over60";
            this.colOver60.Name = "colOver60";
            this.colOver60.Visible = true;
            this.colOver60.VisibleIndex = 14;
            // 
            // colOver90
            // 
            this.colOver90.FieldName = "Over90";
            this.colOver90.Name = "colOver90";
            this.colOver90.Visible = true;
            this.colOver90.VisibleIndex = 16;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // linkCustomerName
            // 
            this.linkCustomerName.AutoHeight = false;
            this.linkCustomerName.Name = "linkCustomerName";
            // 
            // colLastUpdateBy
            // 
            this.colLastUpdateBy.Caption = "Update By";
            this.colLastUpdateBy.FieldName = "LastUpdateBy";
            this.colLastUpdateBy.Name = "colLastUpdateBy";
            this.colLastUpdateBy.Visible = true;
            this.colLastUpdateBy.VisibleIndex = 15;
            // 
            // AccControlList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Name = "AccControlList";
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkCustomerName)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcMain;
        private Framework.ClientComponents.Controls.LWGridView gvMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit linkCustomerName;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        protected System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerID;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanyName;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanyID;
        private DevExpress.XtraGrid.Columns.GridColumn colTerms;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colBalance;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrent;
        private DevExpress.XtraGrid.Columns.GridColumn colLess30;
        private DevExpress.XtraGrid.Columns.GridColumn colOver30;
        private DevExpress.XtraGrid.Columns.GridColumn colOver45;
        private DevExpress.XtraGrid.Columns.GridColumn colOver60;
        private DevExpress.XtraGrid.Columns.GridColumn colOver90;
        private DevExpress.XtraGrid.Columns.GridColumn colMainCurBalance;
        private DevExpress.XtraGrid.Columns.GridColumn colCreditLimit;
        private DevExpress.XtraGrid.Columns.GridColumn colShptWorth;
        private DevExpress.XtraGrid.Columns.GridColumn colPastDu;
        private DevExpress.XtraGrid.Columns.GridColumn colPastDue;
        private DevExpress.XtraGrid.Columns.GridColumn colLastMemo;
        private DevExpress.XtraGrid.Columns.GridColumn colLastUpdate;
        private DevExpress.XtraGrid.Columns.GridColumn colLastUpdateBy;
    }
}
