namespace ICP.FRM.UI
{
    partial class WeeklyReportEdit
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
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvMain = new ICP.Framework.ClientComponents.Controls.LWGridView();
            this.colDivision = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShipline = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbShiplineID = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colShippingSpace = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtShippingSpace = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtDescription = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.colCreateName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtRates = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.cmbCarrier = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShiplineID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShippingSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCarrier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
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
            this.txtRates,
            this.txtShippingSpace,
            this.txtDescription,
            this.cmbShiplineID,
            this.cmbCarrier});
            this.gcMain.Size = new System.Drawing.Size(1020, 509);
            this.gcMain.TabIndex = 2;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.Red;
            this.gvMain.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.gvMain.Appearance.EvenRow.BorderColor = System.Drawing.Color.Red;
            this.gvMain.Appearance.EvenRow.Options.UseBorderColor = true;
            this.gvMain.Appearance.FocusedCell.BorderColor = System.Drawing.Color.Red;
            this.gvMain.Appearance.FocusedCell.Options.UseBorderColor = true;
            this.gvMain.Appearance.FocusedRow.BorderColor = System.Drawing.Color.Red;
            this.gvMain.Appearance.FocusedRow.Options.UseBorderColor = true;
            this.gvMain.Appearance.HorzLine.BorderColor = System.Drawing.Color.Red;
            this.gvMain.Appearance.HorzLine.Options.UseBorderColor = true;
            this.gvMain.Appearance.Row.BorderColor = System.Drawing.Color.Red;
            this.gvMain.Appearance.Row.Options.UseBorderColor = true;
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDivision,
            this.colShipline,
            this.colShippingSpace,
            this.colDescription,
            this.colCreateName,
            this.colCreateDate});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.RowAutoHeight = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvMain_KeyDown);
            // 
            // colDivision
            // 
            this.colDivision.AppearanceCell.BorderColor = System.Drawing.Color.Red;
            this.colDivision.AppearanceCell.Options.UseBorderColor = true;
            this.colDivision.AppearanceHeader.BorderColor = System.Drawing.Color.Red;
            this.colDivision.AppearanceHeader.Options.UseBorderColor = true;
            this.colDivision.Caption = "Loading port";
            this.colDivision.FieldName = "DivisionName";
            this.colDivision.Name = "colDivision";
            this.colDivision.OptionsColumn.AllowEdit = false;
            this.colDivision.Visible = true;
            this.colDivision.VisibleIndex = 0;
            this.colDivision.Width = 101;
            // 
            // colShipline
            // 
            this.colShipline.AppearanceCell.BorderColor = System.Drawing.Color.Red;
            this.colShipline.AppearanceCell.Options.UseBorderColor = true;
            this.colShipline.Caption = "Trading area";
            this.colShipline.ColumnEdit = this.cmbShiplineID;
            this.colShipline.FieldName = "ShiplineID";
            this.colShipline.Name = "colShipline";
            this.colShipline.Visible = true;
            this.colShipline.VisibleIndex = 1;
            this.colShipline.Width = 110;
            // 
            // cmbShiplineID
            // 
            this.cmbShiplineID.AutoHeight = false;
            this.cmbShiplineID.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbShiplineID.Name = "cmbShiplineID";
            // 
            // colShippingSpace
            // 
            this.colShippingSpace.Caption = "Marketing/Space situation & analyse";
            this.colShippingSpace.ColumnEdit = this.txtShippingSpace;
            this.colShippingSpace.FieldName = "ShippingSpace";
            this.colShippingSpace.Name = "colShippingSpace";
            this.colShippingSpace.Visible = true;
            this.colShippingSpace.VisibleIndex = 2;
            this.colShippingSpace.Width = 300;
            // 
            // txtShippingSpace
            // 
            this.txtShippingSpace.Name = "txtShippingSpace";
            this.txtShippingSpace.PopupFormMinSize = new System.Drawing.Size(600, 400);
            this.txtShippingSpace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtShippingSpace.ShowIcon = false;
            this.txtShippingSpace.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.txtShippingSpace_Closed);
            this.txtShippingSpace.Popup += new System.EventHandler(this.txtShippingSpace_Popup);
            // 
            // colDescription
            // 
            this.colDescription.Caption = "Selling guide";
            this.colDescription.ColumnEdit = this.txtDescription;
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 3;
            this.colDescription.Width = 295;
            // 
            // txtDescription
            // 
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.PopupFormMinSize = new System.Drawing.Size(600, 400);
            this.txtDescription.ShowIcon = false;
            this.txtDescription.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.txtDescription_Closed);
            this.txtDescription.Popup += new System.EventHandler(this.txtDescription_Popup);
            // 
            // colCreateName
            // 
            this.colCreateName.Caption = "Create Name";
            this.colCreateName.FieldName = "CreateByName";
            this.colCreateName.Name = "colCreateName";
            this.colCreateName.OptionsColumn.AllowEdit = false;
            this.colCreateName.Visible = true;
            this.colCreateName.VisibleIndex = 4;
            this.colCreateName.Width = 100;
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "Create Date";
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.OptionsColumn.AllowEdit = false;
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 5;
            this.colCreateDate.Width = 90;
            // 
            // txtRates
            // 
            this.txtRates.Name = "txtRates";
            this.txtRates.PopupFormMinSize = new System.Drawing.Size(600, 400);
            this.txtRates.ShowIcon = false;
            this.txtRates.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.txtRates_Closed);
            this.txtRates.Popup += new System.EventHandler(this.txtRates_Popup);
            // 
            // cmbCarrier
            // 
            this.cmbCarrier.AutoHeight = false;
            this.cmbCarrier.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCarrier.Name = "cmbCarrier";
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FRM.ServiceInterface.BusinessWeeklyReportInfo);
            // 
            // WeeklyReportEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.IsMultiLanguage = false;
            this.Name = "WeeklyReportEdit";
            this.Size = new System.Drawing.Size(1020, 509);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShiplineID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShippingSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCarrier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
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
        private DevExpress.XtraGrid.Columns.GridColumn colCreateName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit txtRates;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit txtShippingSpace;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit txtDescription;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbShiplineID;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbCarrier;
    }
}
