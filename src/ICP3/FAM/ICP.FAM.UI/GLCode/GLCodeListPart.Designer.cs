namespace ICP.FAM.UI
{
    partial class GLCodeListPart
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
            this.lwTreeGridControl1 = new ICP.Framework.ClientComponents.Controls.LWTreeGridControl();
            this.colName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCode = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colLevel = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colGLCodeType = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.cmbGLCodeType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colForeignCurrency = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colAidCheck = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colLedgerStyle = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.cmbLedgerStyle = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colUnitName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colGLCodeProperty = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.cmbGLCodeProperty = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colBankAccount = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colJournal = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.lwTreeGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGLCodeType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLedgerStyle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGLCodeProperty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            this.SuspendLayout();
            // 
            // lwTreeGridControl1
            // 
            this.lwTreeGridControl1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colName,
            this.colCode,
            this.colLevel,
            this.colGLCodeType,
            this.colForeignCurrency,
            this.colAidCheck,
            this.colLedgerStyle,
            this.colUnitName,
            this.colGLCodeProperty,
            this.colBankAccount,
            this.colJournal});
            this.lwTreeGridControl1.DataSource = this.bsList;
            this.lwTreeGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lwTreeGridControl1.Location = new System.Drawing.Point(0, 0);
            this.lwTreeGridControl1.Name = "lwTreeGridControl1";
            this.lwTreeGridControl1.OptionsBehavior.Editable = false;
            this.lwTreeGridControl1.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.lwTreeGridControl1.OptionsSelection.InvertSelection = true;
            this.lwTreeGridControl1.OptionsSelection.MultiSelect = true;
            this.lwTreeGridControl1.OptionsView.EnableAppearanceEvenRow = true;
            this.lwTreeGridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbGLCodeType,
            this.cmbLedgerStyle,
            this.cmbGLCodeProperty});
            this.lwTreeGridControl1.Size = new System.Drawing.Size(738, 432);
            this.lwTreeGridControl1.TabIndex = 0;
            this.lwTreeGridControl1.CustomDrawNodeIndicator += new DevExpress.XtraTreeList.CustomDrawNodeIndicatorEventHandler(this.lwTreeGridControl1_CustomDrawNodeIndicator);
            this.lwTreeGridControl1.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.lwTreeGridControl1_NodeCellStyle);
            this.lwTreeGridControl1.DoubleClick += new System.EventHandler(this.lwTreeGridControl1_DoubleClick);
            // 
            // colName
            // 
            this.colName.Caption = "科目名称";
            this.colName.FieldName = "CName";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            this.colName.Width = 118;
            // 
            // colCode
            // 
            this.colCode.Caption = "科目编码";
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 1;
            this.colCode.Width = 95;
            // 
            // colLevel
            // 
            this.colLevel.Caption = "级次";
            this.colLevel.FieldName = "LevelCode";
            this.colLevel.Name = "colLevel";
            this.colLevel.Visible = true;
            this.colLevel.VisibleIndex = 2;
            this.colLevel.Width = 40;
            // 
            // colGLCodeType
            // 
            this.colGLCodeType.Caption = "类型";
            this.colGLCodeType.ColumnEdit = this.cmbGLCodeType;
            this.colGLCodeType.FieldName = "GLCodeType";
            this.colGLCodeType.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colGLCodeType.Name = "colGLCodeType";
            this.colGLCodeType.Visible = true;
            this.colGLCodeType.VisibleIndex = 3;
            this.colGLCodeType.Width = 41;
            // 
            // cmbGLCodeType
            // 
            this.cmbGLCodeType.AutoHeight = false;
            this.cmbGLCodeType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbGLCodeType.Name = "cmbGLCodeType";
            // 
            // colForeignCurrency
            // 
            this.colForeignCurrency.Caption = "外币币种";
            this.colForeignCurrency.FieldName = "ForeignCurrencyName";
            this.colForeignCurrency.Name = "colForeignCurrency";
            this.colForeignCurrency.Visible = true;
            this.colForeignCurrency.VisibleIndex = 4;
            this.colForeignCurrency.Width = 58;
            // 
            // colAidCheck
            // 
            this.colAidCheck.Caption = "辅助核算";
            this.colAidCheck.FieldName = "AidCheckDescription";
            this.colAidCheck.Name = "colAidCheck";
            this.colAidCheck.Visible = true;
            this.colAidCheck.VisibleIndex = 5;
            this.colAidCheck.Width = 78;
            // 
            // colLedgerStyle
            // 
            this.colLedgerStyle.Caption = "账页格式";
            this.colLedgerStyle.ColumnEdit = this.cmbLedgerStyle;
            this.colLedgerStyle.FieldName = "LedgerStyle";
            this.colLedgerStyle.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colLedgerStyle.Name = "colLedgerStyle";
            this.colLedgerStyle.Visible = true;
            this.colLedgerStyle.VisibleIndex = 6;
            this.colLedgerStyle.Width = 73;
            // 
            // cmbLedgerStyle
            // 
            this.cmbLedgerStyle.AutoHeight = false;
            this.cmbLedgerStyle.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbLedgerStyle.Name = "cmbLedgerStyle";
            // 
            // colUnitName
            // 
            this.colUnitName.Caption = "计量单位";
            this.colUnitName.FieldName = "UnitName";
            this.colUnitName.Name = "colUnitName";
            this.colUnitName.Visible = true;
            this.colUnitName.VisibleIndex = 7;
            this.colUnitName.Width = 54;
            // 
            // colGLCodeProperty
            // 
            this.colGLCodeProperty.Caption = "余额方向";
            this.colGLCodeProperty.ColumnEdit = this.cmbGLCodeProperty;
            this.colGLCodeProperty.FieldName = "GLCodeProperty";
            this.colGLCodeProperty.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colGLCodeProperty.Name = "colGLCodeProperty";
            this.colGLCodeProperty.Visible = true;
            this.colGLCodeProperty.VisibleIndex = 8;
            this.colGLCodeProperty.Width = 61;
            // 
            // cmbGLCodeProperty
            // 
            this.cmbGLCodeProperty.AutoHeight = false;
            this.cmbGLCodeProperty.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbGLCodeProperty.Name = "cmbGLCodeProperty";
            // 
            // colBankAccount
            // 
            this.colBankAccount.Caption = "银行帐";
            this.colBankAccount.FieldName = "IsBankAccount";
            this.colBankAccount.Name = "colBankAccount";
            this.colBankAccount.Visible = true;
            this.colBankAccount.VisibleIndex = 9;
            this.colBankAccount.Width = 49;
            // 
            // colJournal
            // 
            this.colJournal.Caption = "日记帐";
            this.colJournal.FieldName = "IsJournal";
            this.colJournal.Name = "colJournal";
            this.colJournal.Visible = true;
            this.colJournal.VisibleIndex = 10;
            this.colJournal.Width = 50;
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.SolutionGLCodeList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsList_PositionChanged);
            // 
            // GLCodeListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lwTreeGridControl1);
            this.Name = "GLCodeListPart";
            this.Size = new System.Drawing.Size(738, 432);
            ((System.ComponentModel.ISupportInitialize)(this.lwTreeGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGLCodeType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLedgerStyle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGLCodeProperty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.Columns.TreeListColumn colCode;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colForeignCurrency;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colAidCheck;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colUnitName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colGLCodeProperty;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colBankAccount;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colJournal;
        protected System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colGLCodeType;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colLedgerStyle;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbGLCodeType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbLedgerStyle;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbGLCodeProperty;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colLevel;
        protected ICP.Framework.ClientComponents.Controls.LWTreeGridControl lwTreeGridControl1;
    }
}
