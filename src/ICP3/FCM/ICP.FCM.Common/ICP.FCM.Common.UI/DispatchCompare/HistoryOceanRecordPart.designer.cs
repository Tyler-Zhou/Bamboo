namespace ICP.FCM.Common.UI.DispatchCompare
{
    partial class HistoryOceanRecordPart
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
            this.bsList = new System.Windows.Forms.BindingSource();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.gvMain = new ICP.Framework.ClientComponents.Controls.LWGridView();
            this.colOEBookingID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRefID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTypeDesc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDispatchBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDispatchOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAcceptBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAcceptOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssignTo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FCM.Common.ServiceInterface.DataObjects.HistoryOceanRecord);
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
            this.gcMain.Location = new System.Drawing.Point(0, 26);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(761, 371);
            this.gcMain.TabIndex = 2;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colOEBookingID,
            this.colID,
            this.colRefID,
            this.colType,
            this.colTypeDesc,
            this.colDispatchBy,
            this.colDispatchOn,
            this.colAcceptBy,
            this.colAcceptOn,
            this.colAssignTo});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvMain_RowClick);
            this.gvMain.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gvMain_CustomColumnDisplayText);
            // 
            // colOEBookingID
            // 
            this.colOEBookingID.Caption = "业务号";
            this.colOEBookingID.FieldName = "OEBookingID";
            this.colOEBookingID.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colOEBookingID.Name = "colOEBookingID";
            this.colOEBookingID.OptionsColumn.AllowEdit = false;
            this.colOEBookingID.Width = 110;
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "ID";
            this.colID.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colID.Name = "colID";
            this.colID.OptionsColumn.AllowEdit = false;
            this.colID.OptionsColumn.AllowMove = false;
            this.colID.Width = 120;
            // 
            // colRefID
            // 
            this.colRefID.Caption = "RefID";
            this.colRefID.FieldName = "RefID";
            this.colRefID.Name = "colRefID";
            this.colRefID.OptionsColumn.AllowEdit = false;
            this.colRefID.Width = 100;
            // 
            // colType
            // 
            this.colType.Caption = "类型";
            this.colType.FieldName = "OperationType";
            this.colType.MinWidth = 100;
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 0;
            this.colType.Width = 100;
            // 
            // colTypeDesc
            // 
            this.colTypeDesc.Caption = "类型";
            this.colTypeDesc.MinWidth = 100;
            this.colTypeDesc.Name = "colTypeDesc";
            this.colTypeDesc.Width = 100;
            // 
            // colDispatchBy
            // 
            this.colDispatchBy.Caption = "分发人";
            this.colDispatchBy.FieldName = "DispatchBy";
            this.colDispatchBy.Name = "colDispatchBy";
            this.colDispatchBy.OptionsColumn.AllowEdit = false;
            this.colDispatchBy.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDispatchBy.OptionsColumn.ReadOnly = true;
            this.colDispatchBy.OptionsFilter.AllowAutoFilter = false;
            this.colDispatchBy.OptionsFilter.AllowFilter = false;
            this.colDispatchBy.Visible = true;
            this.colDispatchBy.VisibleIndex = 1;
            this.colDispatchBy.Width = 173;
            // 
            // colDispatchOn
            // 
            this.colDispatchOn.Caption = "分发时间";
            this.colDispatchOn.DisplayFormat.FormatString = "yy-MM-dd HH:mm ";
            this.colDispatchOn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDispatchOn.FieldName = "DispatchOn";
            this.colDispatchOn.Name = "colDispatchOn";
            this.colDispatchOn.OptionsColumn.AllowEdit = false;
            this.colDispatchOn.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDispatchOn.OptionsColumn.ReadOnly = true;
            this.colDispatchOn.OptionsFilter.AllowAutoFilter = false;
            this.colDispatchOn.OptionsFilter.AllowFilter = false;
            this.colDispatchOn.Visible = true;
            this.colDispatchOn.VisibleIndex = 2;
            this.colDispatchOn.Width = 126;
            // 
            // colAcceptBy
            // 
            this.colAcceptBy.Caption = "签收人";
            this.colAcceptBy.FieldName = "AcceptBy";
            this.colAcceptBy.Name = "colAcceptBy";
            this.colAcceptBy.OptionsColumn.AllowEdit = false;
            this.colAcceptBy.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colAcceptBy.OptionsColumn.ReadOnly = true;
            this.colAcceptBy.OptionsFilter.AllowAutoFilter = false;
            this.colAcceptBy.OptionsFilter.AllowFilter = false;
            this.colAcceptBy.Visible = true;
            this.colAcceptBy.VisibleIndex = 3;
            this.colAcceptBy.Width = 116;
            // 
            // colAcceptOn
            // 
            this.colAcceptOn.Caption = "签收时间";
            this.colAcceptOn.DisplayFormat.FormatString = "yy-MM-dd HH:mm ";
            this.colAcceptOn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colAcceptOn.FieldName = "AcceptOn";
            this.colAcceptOn.Name = "colAcceptOn";
            this.colAcceptOn.OptionsColumn.AllowEdit = false;
            this.colAcceptOn.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colAcceptOn.OptionsColumn.ReadOnly = true;
            this.colAcceptOn.OptionsFilter.AllowAutoFilter = false;
            this.colAcceptOn.OptionsFilter.AllowFilter = false;
            this.colAcceptOn.Visible = true;
            this.colAcceptOn.VisibleIndex = 4;
            this.colAcceptOn.Width = 153;
            // 
            // colAssignTo
            // 
            this.colAssignTo.Caption = "分发给";
            this.colAssignTo.FieldName = "AssignTo";
            this.colAssignTo.Name = "colAssignTo";
            this.colAssignTo.OptionsColumn.AllowEdit = false;
            this.colAssignTo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colAssignTo.OptionsColumn.ReadOnly = true;
            this.colAssignTo.OptionsFilter.AllowAutoFilter = false;
            this.colAssignTo.OptionsFilter.AllowFilter = false;
            this.colAssignTo.Width = 129;
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
            this.barRefresh});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 1;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barRefresh
            // 
            this.barRefresh.Caption = "Refresh";
            this.barRefresh.Glyph = global::ICP.FCM.Common.UI.Properties.Resources.Refresh_161;
            this.barRefresh.Id = 0;
            this.barRefresh.Name = "barRefresh";
            this.barRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRefresh_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(761, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 397);
            this.barDockControlBottom.Size = new System.Drawing.Size(761, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 371);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(761, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 371);
            // 
            // HistoryOceanRecordPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Enabled = false;
            this.Name = "HistoryOceanRecordPart";
            this.Size = new System.Drawing.Size(761, 397);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraGrid.GridControl gcMain;
        private ICP.Framework.ClientComponents.Controls.LWGridView gvMain;
  
        private DevExpress.XtraGrid.Columns.GridColumn colOEBookingID;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colRefID;
        private DevExpress.XtraGrid.Columns.GridColumn colDispatchBy;
        private DevExpress.XtraGrid.Columns.GridColumn colDispatchOn;
        private DevExpress.XtraGrid.Columns.GridColumn colAcceptBy;
        private DevExpress.XtraGrid.Columns.GridColumn colAcceptOn;

        private DevExpress.XtraGrid.Columns.GridColumn colAssignTo;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colTypeDesc;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
       
        


    }
}
