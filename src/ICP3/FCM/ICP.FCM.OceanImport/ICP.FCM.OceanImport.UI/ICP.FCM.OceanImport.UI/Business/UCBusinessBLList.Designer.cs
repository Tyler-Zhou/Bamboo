namespace ICP.FCM.OceanImport.UI
{
    partial class UCBusinessBLList
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
            this.gcHBLList = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsHBLInfo = new System.Windows.Forms.BindingSource(this.components);
            this.gvHBLList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIsRelease = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colShipperName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAMSNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colISFNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colOBLDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGoodsInfo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWeight = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMeasurement = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barCtn = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gcHBLList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsHBLInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHBLList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcHBLList
            // 
            this.gcHBLList.DataSource = this.bsHBLInfo;
            this.gcHBLList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcHBLList.Location = new System.Drawing.Point(0, 26);
            this.gcHBLList.MainView = this.gvHBLList;
            this.gcHBLList.Name = "gcHBLList";
            this.gcHBLList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbState,
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2,
            this.repositoryItemTextEdit3});
            this.gcHBLList.Size = new System.Drawing.Size(713, 386);
            this.gcHBLList.TabIndex = 4;
            this.gcHBLList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvHBLList});
            // 
            // bsHBLInfo
            // 
            this.bsHBLInfo.DataSource = typeof(ICP.FCM.OceanImport.ServiceInterface.OceanBusinessHBLInfo);
            this.bsHBLInfo.PositionChanged += new System.EventHandler(this.bsHBLInfo_PositionChanged);
            // 
            // gvHBLList
            // 
            this.gvHBLList.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gvHBLList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIsRelease,
            this.colSubNo,
            this.colShipperName,
            this.colAMSNo,
            this.colISFNo,
            this.colOBLDate,
            this.colGoodsInfo,
            this.colQty,
            this.colWeight,
            this.colMeasurement});
            this.gvHBLList.GridControl = this.gcHBLList;
            this.gvHBLList.Name = "gvHBLList";
            this.gvHBLList.OptionsSelection.MultiSelect = true;
            this.gvHBLList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvHBLList.OptionsView.ColumnAutoWidth = false;
            this.gvHBLList.OptionsView.EnableAppearanceEvenRow = true;
            this.gvHBLList.OptionsView.ShowDetailButtons = false;
            this.gvHBLList.OptionsView.ShowGroupPanel = false;
            // 
            // colIsRelease
            // 
            this.colIsRelease.Caption = "IsRelease";
            this.colIsRelease.FieldName = "IsRelease";
            this.colIsRelease.Name = "colIsRelease";
            this.colIsRelease.OptionsColumn.AllowEdit = false;
            this.colIsRelease.Visible = true;
            this.colIsRelease.VisibleIndex = 0;
            // 
            // colSubNo
            // 
            this.colSubNo.Caption = "分提单号";
            this.colSubNo.ColumnEdit = this.repositoryItemTextEdit3;
            this.colSubNo.FieldName = "HBLNo";
            this.colSubNo.Name = "colSubNo";
            this.colSubNo.Visible = true;
            this.colSubNo.VisibleIndex = 1;
            this.colSubNo.Width = 78;
            // 
            // repositoryItemTextEdit3
            // 
            this.repositoryItemTextEdit3.AutoHeight = false;
            this.repositoryItemTextEdit3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
            // 
            // colShipperName
            // 
            this.colShipperName.Caption = "发货人";
            this.colShipperName.FieldName = "ShipperName";
            this.colShipperName.Name = "colShipperName";
            this.colShipperName.Visible = true;
            this.colShipperName.VisibleIndex = 2;
            this.colShipperName.Width = 134;
            // 
            // colAMSNo
            // 
            this.colAMSNo.Caption = "AMS NO";
            this.colAMSNo.ColumnEdit = this.repositoryItemTextEdit1;
            this.colAMSNo.FieldName = "AMSNo";
            this.colAMSNo.Name = "colAMSNo";
            this.colAMSNo.Visible = true;
            this.colAMSNo.VisibleIndex = 3;
            this.colAMSNo.Width = 67;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // colISFNo
            // 
            this.colISFNo.Caption = "ISF NO";
            this.colISFNo.ColumnEdit = this.repositoryItemTextEdit2;
            this.colISFNo.FieldName = "ISFNo";
            this.colISFNo.Name = "colISFNo";
            this.colISFNo.Visible = true;
            this.colISFNo.VisibleIndex = 4;
            this.colISFNo.Width = 64;
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // colOBLDate
            // 
            this.colOBLDate.Caption = "收到正本日";
            this.colOBLDate.FieldName = "ReceiveOBLDate";
            this.colOBLDate.Name = "colOBLDate";
            this.colOBLDate.Visible = true;
            this.colOBLDate.VisibleIndex = 5;
            this.colOBLDate.Width = 83;
            // 
            // colGoodsInfo
            // 
            this.colGoodsInfo.Caption = "货物信息";
            this.colGoodsInfo.FieldName = "GoodsInfo";
            this.colGoodsInfo.Name = "colGoodsInfo";
            this.colGoodsInfo.Visible = true;
            this.colGoodsInfo.VisibleIndex = 6;
            this.colGoodsInfo.Width = 98;
            // 
            // colQty
            // 
            this.colQty.Caption = "数量";
            this.colQty.FieldName = "Qty";
            this.colQty.Name = "colQty";
            this.colQty.Visible = true;
            this.colQty.VisibleIndex = 7;
            this.colQty.Width = 48;
            // 
            // colWeight
            // 
            this.colWeight.Caption = "重量";
            this.colWeight.FieldName = "Weight";
            this.colWeight.Name = "colWeight";
            this.colWeight.Visible = true;
            this.colWeight.VisibleIndex = 8;
            this.colWeight.Width = 58;
            // 
            // colMeasurement
            // 
            this.colMeasurement.Caption = "体积";
            this.colMeasurement.FieldName = "Measurement";
            this.colMeasurement.Name = "colMeasurement";
            this.colMeasurement.Visible = true;
            this.colMeasurement.VisibleIndex = 9;
            this.colMeasurement.Width = 62;
            // 
            // rcmbState
            // 
            this.rcmbState.AutoHeight = false;
            this.rcmbState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbState.Name = "rcmbState";
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
            this.barCtn});
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barCtn, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barAdd
            // 
            this.barAdd.Caption = "新增";
            this.barAdd.Glyph = global::ICP.FCM.OceanImport.UI.Properties.Resources.Add_16;
            this.barAdd.Id = 0;
            this.barAdd.Name = "barAdd";
            this.barAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdd_ItemClick);
            // 
            // barDelete
            // 
            this.barDelete.Caption = "删除";
            this.barDelete.Glyph = global::ICP.FCM.OceanImport.UI.Properties.Resources.Delete_16;
            this.barDelete.Id = 1;
            this.barDelete.Name = "barDelete";
            this.barDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // barCtn
            // 
            this.barCtn.Caption = "关联箱";
            this.barCtn.Glyph = global::ICP.FCM.OceanImport.UI.Properties.Resources.Memo_16;
            this.barCtn.Id = 2;
            this.barCtn.Name = "barCtn";
            this.barCtn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCtn_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(713, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 412);
            this.barDockControlBottom.Size = new System.Drawing.Size(713, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 386);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(713, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 386);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bsHBLInfo;
            // 
            // UCBusinessBLList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcHBLList);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "UCBusinessBLList";
            this.Size = new System.Drawing.Size(713, 412);
            ((System.ComponentModel.ISupportInitialize)(this.gcHBLList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsHBLInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHBLList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected ICP.Framework.ClientComponents.Controls.LWGridControl gcHBLList;
        protected DevExpress.XtraGrid.Views.Grid.GridView gvHBLList;
        private DevExpress.XtraGrid.Columns.GridColumn colSubNo;
        private DevExpress.XtraGrid.Columns.GridColumn colAMSNo;
        private DevExpress.XtraGrid.Columns.GridColumn colISFNo;
        private DevExpress.XtraGrid.Columns.GridColumn colOBLDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbState;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barAdd;
        private DevExpress.XtraGrid.Columns.GridColumn colShipperName;
        private System.Windows.Forms.BindingSource bsHBLInfo;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit3;
        private DevExpress.XtraBars.BarButtonItem barCtn;
        private DevExpress.XtraGrid.Columns.GridColumn colGoodsInfo;
        private DevExpress.XtraGrid.Columns.GridColumn colQty;
        private DevExpress.XtraGrid.Columns.GridColumn colWeight;
        private DevExpress.XtraGrid.Columns.GridColumn colMeasurement;
        private DevExpress.XtraGrid.Columns.GridColumn colIsRelease;
    }
}
