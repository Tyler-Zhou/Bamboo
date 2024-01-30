namespace ICP.FCM.OceanImport.UI.Business
{
    partial class UCHBL2CtnList
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
            this.gcBoxList = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsContainerInfo = new System.Windows.Forms.BindingSource(this.components);
            this.gvBoxList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIsRelation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ckbIsRelation = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colBoxType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbBoxType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colSealNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBLNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLocation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGODate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLFDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colValidDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsPartOf = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPickUpDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReturnDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcBoxList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsContainerInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBoxList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbIsRelation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBoxType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcBoxList
            // 
            this.gcBoxList.DataSource = this.bsContainerInfo;
            this.gcBoxList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcBoxList.Location = new System.Drawing.Point(0, 26);
            this.gcBoxList.MainView = this.gvBoxList;
            this.gcBoxList.Name = "gcBoxList";
            this.gcBoxList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1,
            this.ckbIsRelation,
            this.cmbBoxType,
            this.repositoryItemTextEdit1});
            this.gcBoxList.Size = new System.Drawing.Size(736, 277);
            this.gcBoxList.TabIndex = 4;
            this.gcBoxList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBoxList});
            // 
            // bsContainerInfo
            // 
            this.bsContainerInfo.DataSource = typeof(ICP.FCM.OceanImport.ServiceInterface.OIBusinessContainerInfo);
            // 
            // gvBoxList
            // 
            this.gvBoxList.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gvBoxList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIsRelation,
            this.colNo,
            this.colBoxType,
            this.colSealNo,
            this.colBLNo,
            this.colLocation,
            this.colGODate,
            this.colLFDate,
            this.colValidDate,
            this.colQty,
            this.colIsPartOf,
            this.colRemark,
            this.colPickUpDate,
            this.colReturnDate});
            this.gvBoxList.GridControl = this.gcBoxList;
            this.gvBoxList.IndicatorWidth = 30;
            this.gvBoxList.Name = "gvBoxList";
            this.gvBoxList.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvBoxList.OptionsBehavior.Editable = false;
            this.gvBoxList.OptionsSelection.MultiSelect = true;
            this.gvBoxList.OptionsView.ColumnAutoWidth = false;
            this.gvBoxList.OptionsView.EnableAppearanceEvenRow = true;
            this.gvBoxList.OptionsView.ShowDetailButtons = false;
            this.gvBoxList.OptionsView.ShowGroupPanel = false;
            this.gvBoxList.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvBoxList_CustomDrawRowIndicator);
            // 
            // colIsRelation
            // 
            this.colIsRelation.Caption = "关联";
            this.colIsRelation.ColumnEdit = this.ckbIsRelation;
            this.colIsRelation.FieldName = "IsSelected";
            this.colIsRelation.Name = "colIsRelation";
            this.colIsRelation.Visible = true;
            this.colIsRelation.VisibleIndex = 0;
            this.colIsRelation.Width = 45;
            // 
            // ckbIsRelation
            // 
            this.ckbIsRelation.AutoHeight = false;
            this.ckbIsRelation.Name = "ckbIsRelation";
            this.ckbIsRelation.Click += new System.EventHandler(this.ckbIsRelation_Click);
            // 
            // colNo
            // 
            this.colNo.Caption = "箱号";
            this.colNo.ColumnEdit = this.repositoryItemTextEdit1;
            this.colNo.FieldName = "No";
            this.colNo.Name = "colNo";
            this.colNo.Visible = true;
            this.colNo.VisibleIndex = 1;
            this.colNo.Width = 120;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // colBoxType
            // 
            this.colBoxType.Caption = "箱型";
            this.colBoxType.FieldName = "ContainerTypeName";
            this.colBoxType.Name = "colBoxType";
            this.colBoxType.Visible = true;
            this.colBoxType.VisibleIndex = 2;
            this.colBoxType.Width = 45;
            // 
            // cmbBoxType
            // 
            this.cmbBoxType.AutoHeight = false;
            this.cmbBoxType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBoxType.Name = "cmbBoxType";
            // 
            // colSealNo
            // 
            this.colSealNo.Caption = "封条号";
            this.colSealNo.FieldName = "SealNo";
            this.colSealNo.Name = "colSealNo";
            this.colSealNo.Visible = true;
            this.colSealNo.VisibleIndex = 3;
            this.colSealNo.Width = 64;
            // 
            // colBLNo
            // 
            this.colBLNo.Caption = "提货单号";
            this.colBLNo.FieldName = "BLNo";
            this.colBLNo.Name = "colBLNo";
            this.colBLNo.Visible = true;
            this.colBLNo.VisibleIndex = 4;
            this.colBLNo.Width = 80;
            // 
            // colLocation
            // 
            this.colLocation.Caption = "地点";
            this.colLocation.FieldName = "Location";
            this.colLocation.Name = "colLocation";
            this.colLocation.Visible = true;
            this.colLocation.VisibleIndex = 9;
            this.colLocation.Width = 80;
            // 
            // colGODate
            // 
            this.colGODate.Caption = "监管仓日";
            this.colGODate.FieldName = "GODate";
            this.colGODate.Name = "colGODate";
            this.colGODate.Visible = true;
            this.colGODate.VisibleIndex = 5;
            this.colGODate.Width = 80;
            // 
            // colLFDate
            // 
            this.colLFDate.Caption = "免堆日";
            this.colLFDate.FieldName = "LFDate";
            this.colLFDate.Name = "colLFDate";
            this.colLFDate.Visible = true;
            this.colLFDate.VisibleIndex = 6;
            this.colLFDate.Width = 80;
            // 
            // colValidDate
            // 
            this.colValidDate.Caption = "可以提货日";
            this.colValidDate.FieldName = "AvailableDate";
            this.colValidDate.Name = "colValidDate";
            this.colValidDate.Visible = true;
            this.colValidDate.VisibleIndex = 7;
            this.colValidDate.Width = 80;
            // 
            // colQty
            // 
            this.colQty.Caption = "数量";
            this.colQty.FieldName = "Quantity";
            this.colQty.Name = "colQty";
            this.colQty.Visible = true;
            this.colQty.VisibleIndex = 8;
            this.colQty.Width = 45;
            // 
            // colIsPartOf
            // 
            this.colIsPartOf.Caption = "分单";
            this.colIsPartOf.FieldName = "IsPartOf";
            this.colIsPartOf.Name = "colIsPartOf";
            this.colIsPartOf.Visible = true;
            this.colIsPartOf.VisibleIndex = 10;
            this.colIsPartOf.Width = 45;
            // 
            // colRemark
            // 
            this.colRemark.Caption = "备注";
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 11;
            this.colRemark.Width = 100;
            // 
            // colPickUpDate
            // 
            this.colPickUpDate.Caption = "提柜时间";
            this.colPickUpDate.FieldName = "PickUpDate";
            this.colPickUpDate.Name = "colPickUpDate";
            this.colPickUpDate.Visible = true;
            this.colPickUpDate.VisibleIndex = 12;
            this.colPickUpDate.Width = 80;
            // 
            // colReturnDate
            // 
            this.colReturnDate.Caption = "还空时间";
            this.colReturnDate.FieldName = "ReturnDate";
            this.colReturnDate.Name = "colReturnDate";
            this.colReturnDate.Visible = true;
            this.colReturnDate.VisibleIndex = 13;
            this.colReturnDate.Width = 80;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
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
            this.barClose});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 4;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barSave
            // 
            this.barSave.Caption = "&Save";
            this.barSave.Glyph = global::ICP.FCM.OceanImport.UI.Properties.Resources.Save_16;
            this.barSave.Id = 2;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "&Close";
            this.barClose.Glyph = global::ICP.FCM.OceanImport.UI.Properties.Resources.Left_D_16;
            this.barClose.Id = 3;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(736, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 303);
            this.barDockControlBottom.Size = new System.Drawing.Size(736, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 277);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(736, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 277);
            // 
            // UCHBL2CtnList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcBoxList);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "UCHBL2CtnList";
            this.Size = new System.Drawing.Size(736, 303);
            ((System.ComponentModel.ISupportInitialize)(this.gcBoxList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsContainerInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBoxList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbIsRelation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBoxType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }
       
        #endregion

        protected ICP.Framework.ClientComponents.Controls.LWGridControl gcBoxList;
        protected DevExpress.XtraGrid.Views.Grid.GridView gvBoxList;
        private DevExpress.XtraGrid.Columns.GridColumn colNo;
        private DevExpress.XtraGrid.Columns.GridColumn colBoxType;
        private DevExpress.XtraGrid.Columns.GridColumn colSealNo;
        private DevExpress.XtraGrid.Columns.GridColumn colQty;
        private DevExpress.XtraGrid.Columns.GridColumn colBLNo;
        private DevExpress.XtraGrid.Columns.GridColumn colLocation;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.Columns.GridColumn colGODate;
        private DevExpress.XtraGrid.Columns.GridColumn colLFDate;
        private DevExpress.XtraGrid.Columns.GridColumn colValidDate;
        private DevExpress.XtraGrid.Columns.GridColumn colIsRelation;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit ckbIsRelation;
        private System.Windows.Forms.BindingSource bsContainerInfo;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbBoxType;
        private DevExpress.XtraGrid.Columns.GridColumn colIsPartOf;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colPickUpDate;
        private DevExpress.XtraGrid.Columns.GridColumn colReturnDate;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarButtonItem barClose;
    }
}
