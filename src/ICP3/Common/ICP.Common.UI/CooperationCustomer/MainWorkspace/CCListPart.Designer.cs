namespace ICP.Common.UI.CC
{
    partial class CCListPart
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
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colContact = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colCountryName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colSalesName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colBusinessInfo = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colDebtInfo = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.CooperationCustomerList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsList_PositionChanged);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(949, 409);
            this.gcMain.TabIndex = 1;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gvMain.Appearance.EvenRow.Options.UseBackColor = true;
            this.gvMain.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1});
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colName,
            this.colContact,
            this.colCountryName,
            this.colSalesName,
            this.colBusinessInfo,
            this.colDebtInfo});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.IndicatorWidth = 27;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsSelection.UseIndicatorForSelection = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowDetailButtons = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "gridBand1";
            this.gridBand1.Columns.Add(this.colName);
            this.gridBand1.Columns.Add(this.colContact);
            this.gridBand1.Columns.Add(this.colCountryName);
            this.gridBand1.Columns.Add(this.colSalesName);
            this.gridBand1.Columns.Add(this.colBusinessInfo);
            this.gridBand1.Columns.Add(this.colDebtInfo);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.Width = 918;
            // 
            // colName
            // 
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.Width = 123;
            // 
            // colContact
            // 
            this.colContact.FieldName = "Contact";
            this.colContact.Name = "colContact";
            this.colContact.Visible = true;
            this.colContact.Width = 123;
            // 
            // colCountryName
            // 
            this.colCountryName.Caption = "Country";
            this.colCountryName.FieldName = "CountryName";
            this.colCountryName.Name = "colCountryName";
            this.colCountryName.Visible = true;
            this.colCountryName.Width = 123;
            // 
            // colSalesName
            // 
            this.colSalesName.Caption = "Sales";
            this.colSalesName.FieldName = "SalesName";
            this.colSalesName.Name = "colSalesName";
            this.colSalesName.Visible = true;
            this.colSalesName.Width = 123;
            // 
            // colBusinessInfo
            // 
            this.colBusinessInfo.FieldName = "BusinessInfo";
            this.colBusinessInfo.Name = "colBusinessInfo";
            this.colBusinessInfo.Visible = true;
            this.colBusinessInfo.Width = 122;
            // 
            // colDebtInfo
            // 
            this.colDebtInfo.FieldName = "DebtInfo";
            this.colDebtInfo.Name = "colDebtInfo";
            this.colDebtInfo.Visible = true;
            this.colDebtInfo.Width = 304;
            // 
            // CCListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Name = "CCListPart";
            this.Size = new System.Drawing.Size(949, 409);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        protected ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView gvMain;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colContact;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCountryName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSalesName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colBusinessInfo;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDebtInfo;
    }
}
