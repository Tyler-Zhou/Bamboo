namespace ICP.FAM.UI
{
    partial class InvoiceDetailListPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoiceDetailListPart));
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.bsFee = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChargeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExchange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colActualAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.ckbIsCheck = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.bbiAdjustExtraTaxRate = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtTotal = new DevExpress.XtraEditors.TextEdit();
            this.labelControl22 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsFee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbIsCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsFee;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 26);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.ckbIsCheck});
            this.gcMain.Size = new System.Drawing.Size(817, 467);
            this.gcMain.TabIndex = 4;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // bsFee
            // 
            this.bsFee.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.InvoiceFeeDate);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCurrency,
            this.colChargeName,
            this.colExchange,
            this.colQty,
            this.colAmount,
            this.colTax,
            this.colActualAmount,
            this.colRemark});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsView.RowAutoHeight = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            // 
            // colCurrency
            // 
            this.colCurrency.Caption = "币种";
            this.colCurrency.FieldName = "CurrencyName";
            this.colCurrency.Name = "colCurrency";
            this.colCurrency.Visible = true;
            this.colCurrency.VisibleIndex = 0;
            this.colCurrency.Width = 135;
            // 
            // colChargeName
            // 
            this.colChargeName.Caption = "费用项目";
            this.colChargeName.FieldName = "ChargeName";
            this.colChargeName.Name = "colChargeName";
            this.colChargeName.Visible = true;
            this.colChargeName.VisibleIndex = 1;
            this.colChargeName.Width = 135;
            // 
            // colExchange
            // 
            this.colExchange.Caption = "汇率";
            this.colExchange.FieldName = "Exchange";
            this.colExchange.Name = "colExchange";
            this.colExchange.Visible = true;
            this.colExchange.VisibleIndex = 2;
            this.colExchange.Width = 135;
            // 
            // colQty
            // 
            this.colQty.Caption = "数量";
            this.colQty.FieldName = "Qty";
            this.colQty.Name = "colQty";
            this.colQty.Visible = true;
            this.colQty.VisibleIndex = 3;
            this.colQty.Width = 135;
            // 
            // colAmount
            // 
            this.colAmount.Caption = "应收";
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 4;
            this.colAmount.Width = 135;
            // 
            // colTax
            // 
            this.colTax.Caption = "税金";
            this.colTax.FieldName = "ExtraTaxRate";
            this.colTax.Name = "colTax";
            this.colTax.Visible = true;
            this.colTax.VisibleIndex = 5;
            // 
            // colActualAmount
            // 
            this.colActualAmount.Caption = "实际收";
            this.colActualAmount.FieldName = "ActualAmount";
            this.colActualAmount.Name = "colActualAmount";
            this.colActualAmount.Visible = true;
            this.colActualAmount.VisibleIndex = 6;
            // 
            // colRemark
            // 
            this.colRemark.Caption = "备注";
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 7;
            this.colRemark.Width = 138;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // ckbIsCheck
            // 
            this.ckbIsCheck.AutoHeight = false;
            this.ckbIsCheck.Name = "ckbIsCheck";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barAdd,
            this.barDelete,
            this.bbiAdjustExtraTaxRate});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 3;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAdd, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiAdjustExtraTaxRate, true)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barAdd
            // 
            this.barAdd.Caption = "新增(&N)";
            this.barAdd.Glyph = global::ICP.FAM.UI.Properties.Resources.Add_File_16;
            this.barAdd.Id = 0;
            this.barAdd.Name = "barAdd";
            // 
            // barDelete
            // 
            this.barDelete.Caption = "删除(&D)";
            this.barDelete.Glyph = global::ICP.FAM.UI.Properties.Resources.Delete_16;
            this.barDelete.Id = 1;
            this.barDelete.Name = "barDelete";
            // 
            // bbiAdjustExtraTaxRate
            // 
            this.bbiAdjustExtraTaxRate.Caption = "调整发票额外税率";
            this.bbiAdjustExtraTaxRate.Glyph = global::ICP.FAM.UI.Properties.Resources.DollarSign;
            this.bbiAdjustExtraTaxRate.Id = 2;
            this.bbiAdjustExtraTaxRate.Name = "bbiAdjustExtraTaxRate";
            this.bbiAdjustExtraTaxRate.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
         
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(817, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 524);
            this.barDockControlBottom.Size = new System.Drawing.Size(817, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 498);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(817, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 498);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtTotal);
            this.panel1.Controls.Add(this.labelControl22);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 493);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(817, 31);
            this.panel1.TabIndex = 9;
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(41, 5);
            this.txtTotal.MenuManager = this.barManager1;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Properties.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(759, 21);
            this.txtTotal.TabIndex = 1;
            // 
            // labelControl22
            // 
            this.labelControl22.Location = new System.Drawing.Point(3, 7);
            this.labelControl22.Name = "labelControl22";
            this.labelControl22.Size = new System.Drawing.Size(36, 14);
            this.labelControl22.TabIndex = 0;
            this.labelControl22.Text = "合计：";
            // 
            // InvoiceDetailListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "InvoiceDetailListPart";
            this.Size = new System.Drawing.Size(817, 524);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsFee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbIsCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit ckbIsCheck;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colChargeName;
        private DevExpress.XtraGrid.Columns.GridColumn colExchange;
        private DevExpress.XtraGrid.Columns.GridColumn colQty;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        public DevExpress.XtraBars.BarButtonItem barAdd;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.TextEdit txtTotal;
        private DevExpress.XtraEditors.LabelControl labelControl22;
        private System.Windows.Forms.BindingSource bsFee;
        private DevExpress.XtraGrid.Columns.GridColumn colTax;
        private DevExpress.XtraGrid.Columns.GridColumn colActualAmount;
        private DevExpress.XtraBars.BarButtonItem bbiAdjustExtraTaxRate;
    }
}
