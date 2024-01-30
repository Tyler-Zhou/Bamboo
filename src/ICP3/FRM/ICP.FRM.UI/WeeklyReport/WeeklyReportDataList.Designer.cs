namespace ICP.FRM.UI
{
    partial class WeeklyReportDataList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WeeklyReportDataList));
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvMain = new ICP.Framework.ClientComponents.Controls.LWGridView();
            this.colShipline = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrderByCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDivision = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShippingSpace = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemRichTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMangerComments = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colManager = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.repositoryItemRichTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FRM.ServiceInterface.BusinessWeeklyReportData);
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
            this.repositoryItemHyperLinkEdit2,
            this.repositoryItemRichTextEdit1,
            this.repositoryItemRichTextEdit2});
            this.gcMain.Size = new System.Drawing.Size(1020, 509);
            this.gcMain.TabIndex = 2;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colShipline,
            this.colOrderByCode,
            this.colDivision,
            this.colShippingSpace,
            this.colDescription,
            this.colMangerComments,
            this.colManager});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.GroupCount = 1;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsCustomization.AllowRowSizing = true;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.GroupDrawMode = DevExpress.XtraGrid.Views.Grid.GroupDrawMode.Office2003;
            this.gvMain.OptionsView.RowAutoHeight = true;
            this.gvMain.OptionsView.ShowGroupedColumns = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colShipline, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colOrderByCode, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvMain_KeyDown);
          
            this.gvMain.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvMain_RowCellStyle);
            this.gvMain.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvMain_RowCellClick);
            this.gvMain.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvMain_RowStyle);
            // 
            // colShipline
            // 
            this.colShipline.AppearanceCell.BorderColor = System.Drawing.Color.Red;
            this.colShipline.AppearanceCell.Options.UseBorderColor = true;
            this.colShipline.AppearanceHeader.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.colShipline.Caption = "Trading area";
            this.colShipline.FieldName = "ShiplineName";
            this.colShipline.Name = "colShipline";
            this.colShipline.OptionsColumn.AllowEdit = false;
            this.colShipline.Visible = true;
            this.colShipline.VisibleIndex = 0;
            this.colShipline.Width = 39;
            // 
            // colOrderByCode
            // 
            this.colOrderByCode.Caption = "OrderBy";
            this.colOrderByCode.FieldName = "OrderByCode";
            this.colOrderByCode.Name = "colOrderByCode";
            this.colOrderByCode.OptionsColumn.ReadOnly = true;
            this.colOrderByCode.Visible = true;
            this.colOrderByCode.VisibleIndex = 1;
            // 
            // colDivision
            // 
            this.colDivision.Caption = "Loading port";
            this.colDivision.FieldName = "CompanyName";
            this.colDivision.Name = "colDivision";
            this.colDivision.OptionsColumn.AllowEdit = false;
            this.colDivision.Visible = true;
            this.colDivision.VisibleIndex = 2;
            this.colDivision.Width = 91;
            // 
            // colShippingSpace
            // 
            this.colShippingSpace.Caption = "Marketing/Space situation & analyse";
            this.colShippingSpace.ColumnEdit = this.repositoryItemRichTextEdit2;
            this.colShippingSpace.FieldName = "Marketing";
            this.colShippingSpace.Name = "colShippingSpace";
            this.colShippingSpace.OptionsColumn.ReadOnly = true;
            this.colShippingSpace.Visible = true;
            this.colShippingSpace.VisibleIndex = 3;
            this.colShippingSpace.Width = 300;
            // 
            // repositoryItemRichTextEdit2
            // 
            this.repositoryItemRichTextEdit2.Name = "repositoryItemRichTextEdit2";
            this.repositoryItemRichTextEdit2.OptionsImport.Html.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("repositoryItemRichTextEdit2.OptionsImport.Html.ActualEncoding")));
            this.repositoryItemRichTextEdit2.OptionsImport.Html.AsyncImageLoading = true;
            this.repositoryItemRichTextEdit2.OptionsImport.Mht.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("repositoryItemRichTextEdit2.OptionsImport.Mht.ActualEncoding")));
            this.repositoryItemRichTextEdit2.OptionsImport.Mht.AsyncImageLoading = true;
            this.repositoryItemRichTextEdit2.OptionsImport.OpenDocument.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("repositoryItemRichTextEdit2.OptionsImport.OpenDocument.ActualEncoding")));
            this.repositoryItemRichTextEdit2.OptionsImport.OpenXml.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("repositoryItemRichTextEdit2.OptionsImport.OpenXml.ActualEncoding")));
            this.repositoryItemRichTextEdit2.OptionsImport.PlainText.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("repositoryItemRichTextEdit2.OptionsImport.PlainText.ActualEncoding")));
            this.repositoryItemRichTextEdit2.OptionsImport.Rtf.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("repositoryItemRichTextEdit2.OptionsImport.Rtf.ActualEncoding")));
            this.repositoryItemRichTextEdit2.OptionsImport.WordML.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("repositoryItemRichTextEdit2.OptionsImport.WordML.ActualEncoding")));
            // 
            // colDescription
            // 
            this.colDescription.Caption = "Selling guide";
            this.colDescription.ColumnEdit = this.repositoryItemRichTextEdit2;
            this.colDescription.FieldName = "SellingGuide";
            this.colDescription.Name = "colDescription";
            this.colDescription.OptionsColumn.ReadOnly = true;
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 4;
            this.colDescription.Width = 300;
            // 
            // colMangerComments
            // 
            this.colMangerComments.Caption = "Manger Comments";
            this.colMangerComments.ColumnEdit = this.repositoryItemRichTextEdit2;
            this.colMangerComments.FieldName = "MangerComments";
            this.colMangerComments.Name = "colMangerComments";
            this.colMangerComments.OptionsColumn.ReadOnly = true;
            this.colMangerComments.Visible = true;
            this.colMangerComments.VisibleIndex = 5;
            this.colMangerComments.Width = 200;
            // 
            // colManager
            // 
            this.colManager.Caption = "Manager";
            this.colManager.ColumnEdit = this.repositoryItemHyperLinkEdit2;
            this.colManager.FieldName = "BtnName";
            this.colManager.Name = "colManager";
            this.colManager.Visible = true;
            this.colManager.VisibleIndex = 6;
            this.colManager.Width = 60;
            // 
            // repositoryItemHyperLinkEdit2
            // 
            this.repositoryItemHyperLinkEdit2.AutoHeight = false;
            this.repositoryItemHyperLinkEdit2.Name = "repositoryItemHyperLinkEdit2";
            // 
            // repositoryItemRichTextEdit1
            // 
            this.repositoryItemRichTextEdit1.Appearance.Font = new System.Drawing.Font("宋体", 10F);
            this.repositoryItemRichTextEdit1.Appearance.Options.UseFont = true;
            this.repositoryItemRichTextEdit1.AppearanceDisabled.Font = new System.Drawing.Font("宋体", 10F);
            this.repositoryItemRichTextEdit1.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemRichTextEdit1.AppearanceFocused.Font = new System.Drawing.Font("宋体", 10F);
            this.repositoryItemRichTextEdit1.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemRichTextEdit1.AppearanceReadOnly.Font = new System.Drawing.Font("宋体", 10F);
            this.repositoryItemRichTextEdit1.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemRichTextEdit1.MaxHeight = 500;
            this.repositoryItemRichTextEdit1.Name = "repositoryItemRichTextEdit1";
            this.repositoryItemRichTextEdit1.OptionsImport.Html.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("repositoryItemRichTextEdit1.OptionsImport.Html.ActualEncoding")));
            this.repositoryItemRichTextEdit1.OptionsImport.Html.AsyncImageLoading = true;
            this.repositoryItemRichTextEdit1.OptionsImport.Mht.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("repositoryItemRichTextEdit1.OptionsImport.Mht.ActualEncoding")));
            this.repositoryItemRichTextEdit1.OptionsImport.Mht.AsyncImageLoading = true;
            this.repositoryItemRichTextEdit1.OptionsImport.OpenDocument.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("repositoryItemRichTextEdit1.OptionsImport.OpenDocument.ActualEncoding")));
            this.repositoryItemRichTextEdit1.OptionsImport.OpenXml.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("repositoryItemRichTextEdit1.OptionsImport.OpenXml.ActualEncoding")));
            this.repositoryItemRichTextEdit1.OptionsImport.PlainText.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("repositoryItemRichTextEdit1.OptionsImport.PlainText.ActualEncoding")));
            this.repositoryItemRichTextEdit1.OptionsImport.Rtf.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("repositoryItemRichTextEdit1.OptionsImport.Rtf.ActualEncoding")));
            this.repositoryItemRichTextEdit1.OptionsImport.WordML.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("repositoryItemRichTextEdit1.OptionsImport.WordML.ActualEncoding")));
            // 
            // WeeklyReportDataList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.IsMultiLanguage = false;
            this.Name = "WeeklyReportDataList";
            this.Size = new System.Drawing.Size(1020, 509);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichTextEdit1)).EndInit();
            this.ResumeLayout(false);

        }

    
   

        #endregion

        private ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        private ICP.Framework.ClientComponents.Controls.LWGridView gvMain;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraGrid.Columns.GridColumn colDivision;
        private DevExpress.XtraGrid.Columns.GridColumn colShipline;
        private DevExpress.XtraGrid.Columns.GridColumn colShippingSpace;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colMangerComments;
        private DevExpress.XtraGrid.Columns.GridColumn colManager;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderByCode;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit repositoryItemRichTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit repositoryItemRichTextEdit2;
    }
}
