namespace ICP.FAM.UI.WriteOff
{
    partial class CurrencyRateEditor
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtCurrencyName = new DevExpress.XtraEditors.TextEdit();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbCurrency = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrencyName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(25, 34);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "目标币种";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(175, 281);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(71, 23);
            this.btnConfirm.TabIndex = 0;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtCurrencyName);
            this.groupControl1.Controls.Add(this.gcMain);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(10, 9);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(251, 262);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "汇率信息";
            // 
            // txtCurrencyName
            // 
            this.txtCurrencyName.Location = new System.Drawing.Point(97, 31);
            this.txtCurrencyName.Name = "txtCurrencyName";
            this.txtCurrencyName.Properties.ReadOnly = true;
            this.txtCurrencyName.Size = new System.Drawing.Size(136, 21);
            this.txtCurrencyName.TabIndex = 2;
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Location = new System.Drawing.Point(8, 59);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbCurrency});
            this.gcMain.Size = new System.Drawing.Size(231, 200);
            this.gcMain.TabIndex = 1;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.SolutionExchangeRateList);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCurrency,
            this.colRate});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            // 
            // colCurrency
            // 
            this.colCurrency.Caption = "原始币种";
            this.colCurrency.FieldName = "SourceCurrency";
            this.colCurrency.Name = "colCurrency";
            this.colCurrency.OptionsColumn.AllowEdit = false;
            this.colCurrency.Visible = true;
            this.colCurrency.VisibleIndex = 0;
            this.colCurrency.Width = 120;
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.AutoHeight = false;
            this.cmbCurrency.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCurrency.Name = "cmbCurrency";
            // 
            // colRate
            // 
            this.colRate.Caption = "汇率";
            this.colRate.FieldName = "Rate";
            this.colRate.Name = "colRate";
            this.colRate.Visible = true;
            this.colRate.VisibleIndex = 1;
            this.colRate.Width = 90;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(32, 281);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "取消";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // CurrencyRateEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnConfirm);
            this.Name = "CurrencyRateEditor";
            this.Size = new System.Drawing.Size(281, 319);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrencyName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrency;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colRate;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraEditors.TextEdit txtCurrencyName;
    }
}
