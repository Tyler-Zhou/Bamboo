namespace ICP.FRM.UI.ProfitRatios
{
    partial class PREditPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PREditPart));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.labAdjustmentType = new DevExpress.XtraBars.BarStaticItem();
            this.cmbAdjustmentType = new DevExpress.XtraBars.BarEditItem();
            this.riicmbAdjustmentType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barReset = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this._BSEdit = new System.Windows.Forms.BindingSource(this.components);
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colContainerTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOriginalAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAdjustmentAfterAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContractBaseItemID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContainerTypeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUndoable = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsNew = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsDirty = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riicmbAdjustmentType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._BSEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            this.SuspendLayout();
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
            this.barSave,
            this.labAdjustmentType,
            this.cmbAdjustmentType,
            this.barReset});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 19;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riicmbAdjustmentType});
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.labAdjustmentType),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.cmbAdjustmentType, "", false, true, true, 81),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barReset, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // labAdjustmentType
            // 
            this.labAdjustmentType.Caption = "Adjustment Type";
            this.labAdjustmentType.Id = 16;
            this.labAdjustmentType.Name = "labAdjustmentType";
            this.labAdjustmentType.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // cmbAdjustmentType
            // 
            this.cmbAdjustmentType.Edit = this.riicmbAdjustmentType;
            this.cmbAdjustmentType.Id = 17;
            this.cmbAdjustmentType.Name = "cmbAdjustmentType";
            this.cmbAdjustmentType.Width = 100;
            // 
            // riicmbAdjustmentType
            // 
            this.riicmbAdjustmentType.AutoHeight = false;
            this.riicmbAdjustmentType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riicmbAdjustmentType.Name = "riicmbAdjustmentType";
            // 
            // barSave
            // 
            this.barSave.Caption = "&Save";
            this.barSave.Glyph = ((System.Drawing.Image)(resources.GetObject("barSave.Glyph")));
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barReset
            // 
            this.barReset.Caption = "Reset";
            this.barReset.Glyph = global::ICP.FRM.UI.Properties.Resources.Delete_16;
            this.barReset.Id = 18;
            this.barReset.Name = "barReset";
            this.barReset.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barReset_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(639, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 316);
            this.barDockControlBottom.Size = new System.Drawing.Size(639, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 290);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(639, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 290);
            // 
            // _BSEdit
            // 
            this._BSEdit.DataSource = typeof(ICP.FRM.ServiceInterface.DataObjects.ProfitRatiosAdjustment);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this._BSEdit;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 26);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(639, 290);
            this.gcMain.TabIndex = 26;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colContainerTypeName,
            this.colOriginalAmount,
            this.colAmount,
            this.colAdjustmentAfterAmount,
            this.colUpdateDate,
            this.colContractBaseItemID,
            this.colContainerTypeID,
            this.colUndoable,
            this.colIsNew,
            this.colIsDirty});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.IndicatorWidth = 35;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.ShowDetailButtons = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            // 
            // colContainerTypeName
            // 
            this.colContainerTypeName.FieldName = "ContainerTypeName";
            this.colContainerTypeName.Name = "colContainerTypeName";
            this.colContainerTypeName.OptionsColumn.AllowEdit = false;
            this.colContainerTypeName.Visible = true;
            this.colContainerTypeName.VisibleIndex = 0;
            this.colContainerTypeName.Width = 100;
            // 
            // colOriginalAmount
            // 
            this.colOriginalAmount.FieldName = "OriginalAmount";
            this.colOriginalAmount.Name = "colOriginalAmount";
            this.colOriginalAmount.OptionsColumn.AllowEdit = false;
            this.colOriginalAmount.Visible = true;
            this.colOriginalAmount.VisibleIndex = 1;
            this.colOriginalAmount.Width = 120;
            // 
            // colAmount
            // 
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 2;
            this.colAmount.Width = 120;
            // 
            // colAdjustmentAfterAmount
            // 
            this.colAdjustmentAfterAmount.FieldName = "AdjustmentAfterAmount";
            this.colAdjustmentAfterAmount.Name = "colAdjustmentAfterAmount";
            this.colAdjustmentAfterAmount.OptionsColumn.AllowEdit = false;
            this.colAdjustmentAfterAmount.Visible = true;
            this.colAdjustmentAfterAmount.VisibleIndex = 3;
            this.colAdjustmentAfterAmount.Width = 120;
            // 
            // colUpdateDate
            // 
            this.colUpdateDate.FieldName = "UpdateDate";
            this.colUpdateDate.Name = "colUpdateDate";
            // 
            // colContractBaseItemID
            // 
            this.colContractBaseItemID.Caption = "Contract Base Item ID";
            this.colContractBaseItemID.FieldName = "ContractBaseItemID";
            this.colContractBaseItemID.Name = "colContractBaseItemID";
            // 
            // colContainerTypeID
            // 
            this.colContainerTypeID.FieldName = "ContainerTypeID";
            this.colContainerTypeID.Name = "colContainerTypeID";
            // 
            // colUndoable
            // 
            this.colUndoable.FieldName = "Undoable";
            this.colUndoable.Name = "colUndoable";
            // 
            // colIsNew
            // 
            this.colIsNew.FieldName = "IsNew";
            this.colIsNew.Name = "colIsNew";
            this.colIsNew.OptionsColumn.ReadOnly = true;
            // 
            // colIsDirty
            // 
            this.colIsDirty.FieldName = "IsDirty";
            this.colIsDirty.Name = "colIsDirty";
            // 
            // PREditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "PREditPart";
            this.Size = new System.Drawing.Size(639, 316);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riicmbAdjustmentType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._BSEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
       
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private System.Windows.Forms.BindingSource _BSEdit;
        private Framework.ClientComponents.Controls.LWGridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colContractBaseItemID;
        private DevExpress.XtraGrid.Columns.GridColumn colUndoable;
        private DevExpress.XtraGrid.Columns.GridColumn colIsNew;
        private DevExpress.XtraGrid.Columns.GridColumn colIsDirty;
        private DevExpress.XtraGrid.Columns.GridColumn colContainerTypeID;
        private DevExpress.XtraGrid.Columns.GridColumn colContainerTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colOriginalAmount;
        private DevExpress.XtraBars.BarStaticItem labAdjustmentType;
        private DevExpress.XtraBars.BarEditItem cmbAdjustmentType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox riicmbAdjustmentType;
        private DevExpress.XtraGrid.Columns.GridColumn colAdjustmentAfterAmount;
        private DevExpress.XtraBars.BarButtonItem barReset;
    }
}
