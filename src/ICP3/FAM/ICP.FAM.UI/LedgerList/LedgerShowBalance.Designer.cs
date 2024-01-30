namespace ICP.FAM.UI.LedgerList
{
    partial class LedgerShowBalance
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
            this.lab_GL = new System.Windows.Forms.Label();
            this.lab_Date = new System.Windows.Forms.Label();
            this.lab_time = new System.Windows.Forms.Label();
            this.lab_gltext = new System.Windows.Forms.Label();
            this.lab_Auxiliarytext = new System.Windows.Forms.Label();
            this.lab_Auxiliary = new System.Windows.Forms.Label();
            this.lab_unittext = new System.Windows.Forms.Label();
            this.lab_unit = new System.Windows.Forms.Label();
            this.lab_Currencytext = new System.Windows.Forms.Label();
            this.lab_Currency = new System.Windows.Forms.Label();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.gvDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colItem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDirection = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.rcmbCompany = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.rlueCompany = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlueCompany)).BeginInit();
            this.SuspendLayout();
            // 
            // lab_GL
            // 
            this.lab_GL.AutoSize = true;
            this.lab_GL.Location = new System.Drawing.Point(20, 17);
            this.lab_GL.Name = "lab_GL";
            this.lab_GL.Size = new System.Drawing.Size(55, 14);
            this.lab_GL.TabIndex = 1;
            this.lab_GL.Text = "科   目：";
            // 
            // lab_Date
            // 
            this.lab_Date.AutoSize = true;
            this.lab_Date.Location = new System.Drawing.Point(20, 61);
            this.lab_Date.Name = "lab_Date";
            this.lab_Date.Size = new System.Drawing.Size(47, 14);
            this.lab_Date.TabIndex = 3;
            this.lab_Date.Text = "时   间:";
            // 
            // lab_time
            // 
            this.lab_time.AutoSize = true;
            this.lab_time.Location = new System.Drawing.Point(73, 61);
            this.lab_time.Name = "lab_time";
            this.lab_time.Size = new System.Drawing.Size(38, 14);
            this.lab_time.TabIndex = 13;
            this.lab_time.Text = "label2";
            // 
            // lab_gltext
            // 
            this.lab_gltext.AutoSize = true;
            this.lab_gltext.Location = new System.Drawing.Point(74, 18);
            this.lab_gltext.Name = "lab_gltext";
            this.lab_gltext.Size = new System.Drawing.Size(38, 14);
            this.lab_gltext.TabIndex = 14;
            this.lab_gltext.Text = "label2";
            // 
            // lab_Auxiliarytext
            // 
            this.lab_Auxiliarytext.AutoSize = true;
            this.lab_Auxiliarytext.Location = new System.Drawing.Point(73, 39);
            this.lab_Auxiliarytext.Name = "lab_Auxiliarytext";
            this.lab_Auxiliarytext.Size = new System.Drawing.Size(38, 14);
            this.lab_Auxiliarytext.TabIndex = 16;
            this.lab_Auxiliarytext.Text = "label2";
            // 
            // lab_Auxiliary
            // 
            this.lab_Auxiliary.AutoSize = true;
            this.lab_Auxiliary.Location = new System.Drawing.Point(20, 39);
            this.lab_Auxiliary.Name = "lab_Auxiliary";
            this.lab_Auxiliary.Size = new System.Drawing.Size(47, 14);
            this.lab_Auxiliary.TabIndex = 15;
            this.lab_Auxiliary.Text = "辅助项:";
            // 
            // lab_unittext
            // 
            this.lab_unittext.AutoSize = true;
            this.lab_unittext.Location = new System.Drawing.Point(234, 61);
            this.lab_unittext.Name = "lab_unittext";
            this.lab_unittext.Size = new System.Drawing.Size(0, 14);
            this.lab_unittext.TabIndex = 18;
            // 
            // lab_unit
            // 
            this.lab_unit.AutoSize = true;
            this.lab_unit.Location = new System.Drawing.Point(174, 61);
            this.lab_unit.Name = "lab_unit";
            this.lab_unit.Size = new System.Drawing.Size(59, 14);
            this.lab_unit.TabIndex = 17;
            this.lab_unit.Text = "计量单位:";
            // 
            // lab_Currencytext
            // 
            this.lab_Currencytext.AutoSize = true;
            this.lab_Currencytext.Location = new System.Drawing.Point(378, 61);
            this.lab_Currencytext.Name = "lab_Currencytext";
            this.lab_Currencytext.Size = new System.Drawing.Size(38, 14);
            this.lab_Currencytext.TabIndex = 20;
            this.lab_Currencytext.Text = "label2";
            // 
            // lab_Currency
            // 
            this.lab_Currency.AutoSize = true;
            this.lab_Currency.Location = new System.Drawing.Point(325, 61);
            this.lab_Currency.Name = "lab_Currency";
            this.lab_Currency.Size = new System.Drawing.Size(47, 14);
            this.lab_Currency.TabIndex = 19;
            this.lab_Currency.Text = "币   种:";
            // 
            // gcMain
            // 
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gcMain.Location = new System.Drawing.Point(0, 87);
            this.gcMain.MainView = this.gvDetails;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1,
            this.rcmbCompany,
            this.rlueCompany});
            this.gcMain.Size = new System.Drawing.Size(465, 210);
            this.gcMain.TabIndex = 21;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDetails});
            // 
            // gvDetails
            // 
            this.gvDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colItem,
            this.colDirection,
            this.colAmount,
            this.colFCurrency,
            this.colCount});
            this.gvDetails.GridControl = this.gcMain;
            this.gvDetails.Name = "gvDetails";
            this.gvDetails.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvDetails.OptionsPrint.EnableAppearanceEvenRow = true;
            this.gvDetails.OptionsPrint.EnableAppearanceOddRow = true;
            this.gvDetails.OptionsSelection.MultiSelect = true;
            this.gvDetails.OptionsView.ColumnAutoWidth = false;
            this.gvDetails.OptionsView.EnableAppearanceEvenRow = true;
            this.gvDetails.OptionsView.ShowGroupPanel = false;
            this.gvDetails.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gvDetails_CustomColumnDisplayText);
            // 
            // colItem
            // 
            this.colItem.Caption = " ";
            this.colItem.FieldName = "Item";
            this.colItem.Name = "colItem";
            this.colItem.OptionsColumn.AllowEdit = false;
            this.colItem.Visible = true;
            this.colItem.VisibleIndex = 0;
            this.colItem.Width = 110;
            // 
            // colDirection
            // 
            this.colDirection.Caption = "方向";
            this.colDirection.FieldName = "Direction";
            this.colDirection.Name = "colDirection";
            this.colDirection.OptionsColumn.AllowEdit = false;
            this.colDirection.Visible = true;
            this.colDirection.VisibleIndex = 1;
            this.colDirection.Width = 50;
            // 
            // colAmount
            // 
            this.colAmount.Caption = "金额";
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.AllowEdit = false;
            this.colAmount.SummaryItem.DisplayFormat = "合计";
            this.colAmount.SummaryItem.FieldName = "GLName";
            this.colAmount.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 2;
            this.colAmount.Width = 100;
            // 
            // colFCurrency
            // 
            this.colFCurrency.Caption = "外币";
            this.colFCurrency.FieldName = "FCurrency";
            this.colFCurrency.Name = "colFCurrency";
            this.colFCurrency.OptionsColumn.AllowEdit = false;
            this.colFCurrency.Visible = true;
            this.colFCurrency.VisibleIndex = 3;
            this.colFCurrency.Width = 100;
            // 
            // colCount
            // 
            this.colCount.Caption = "数量";
            this.colCount.DisplayFormat.FormatString = "n";
            this.colCount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCount.FieldName = "Count";
            this.colCount.Name = "colCount";
            this.colCount.OptionsColumn.AllowEdit = false;
            this.colCount.Visible = true;
            this.colCount.VisibleIndex = 4;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // rcmbCompany
            // 
            this.rcmbCompany.AutoHeight = false;
            this.rcmbCompany.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbCompany.Name = "rcmbCompany";
            // 
            // rlueCompany
            // 
            this.rlueCompany.AutoHeight = false;
            this.rlueCompany.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rlueCompany.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "公司", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CShortName", 120, "公司")});
            this.rlueCompany.Name = "rlueCompany";
            this.rlueCompany.NullText = "";
            // 
            // LedgerShowBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.lab_Currencytext);
            this.Controls.Add(this.lab_Currency);
            this.Controls.Add(this.lab_unittext);
            this.Controls.Add(this.lab_unit);
            this.Controls.Add(this.lab_Auxiliarytext);
            this.Controls.Add(this.lab_Auxiliary);
            this.Controls.Add(this.lab_gltext);
            this.Controls.Add(this.lab_time);
            this.Controls.Add(this.lab_Date);
            this.Controls.Add(this.lab_GL);
            this.Name = "LedgerShowBalance";
            this.Size = new System.Drawing.Size(465, 297);
            this.Controls.SetChildIndex(this.lab_GL, 0);
            this.Controls.SetChildIndex(this.lab_Date, 0);
            this.Controls.SetChildIndex(this.lab_time, 0);
            this.Controls.SetChildIndex(this.lab_gltext, 0);
            this.Controls.SetChildIndex(this.lab_Auxiliary, 0);
            this.Controls.SetChildIndex(this.lab_Auxiliarytext, 0);
            this.Controls.SetChildIndex(this.lab_unit, 0);
            this.Controls.SetChildIndex(this.lab_unittext, 0);
            this.Controls.SetChildIndex(this.lab_Currency, 0);
            this.Controls.SetChildIndex(this.lab_Currencytext, 0);
            this.Controls.SetChildIndex(this.gcMain, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlueCompany)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lab_GL;
        private System.Windows.Forms.Label lab_Date;
        private System.Windows.Forms.Label lab_time;
        private System.Windows.Forms.Label lab_gltext;
        private System.Windows.Forms.Label lab_Auxiliarytext;
        private System.Windows.Forms.Label lab_Auxiliary;
        private System.Windows.Forms.Label lab_unittext;
        private System.Windows.Forms.Label lab_unit;
        private System.Windows.Forms.Label lab_Currencytext;
        private System.Windows.Forms.Label lab_Currency;
        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetails;
        private DevExpress.XtraGrid.Columns.GridColumn colItem;
        private DevExpress.XtraGrid.Columns.GridColumn colDirection;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colFCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colCount;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox rcmbCompany;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rlueCompany;
    }
}
