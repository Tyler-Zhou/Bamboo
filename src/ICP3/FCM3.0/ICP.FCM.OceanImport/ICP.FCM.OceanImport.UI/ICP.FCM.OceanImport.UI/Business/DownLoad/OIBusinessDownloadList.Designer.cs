namespace ICP.FCM.OceanImport.UI
{
    partial class OIBusinessDownloadList
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
            this.bsOEList = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDownLoadState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPODRefNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHBLState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMBLNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContainerNos = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVesselVoyageName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPolName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPODName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlaceOfDeliveryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlaceofdeliveryDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConsigneeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.stxtConsigneeID = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colPOLFilerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAgentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.ckbIsCheck = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsOEList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtConsigneeID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbIsCheck)).BeginInit();
            this.SuspendLayout();
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsOEList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbState,
            this.ckbIsCheck,
            this.stxtConsigneeID});
            this.gcMain.Size = new System.Drawing.Size(839, 685);
            this.gcMain.TabIndex = 2;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            this.gcMain.Click += new System.EventHandler(this.gcMain_Click);
            // 
            // bsOEList
            // 
            this.bsOEList.DataSource = typeof(ICP.FCM.OceanImport.ServiceInterface.OceanBusinessDownLoadList);
            this.bsOEList.PositionChanged += new System.EventHandler(this.bsOEList_PositionChanged);
            // 
            // gvMain
            // 
            this.gvMain.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCheck,
            this.colDownLoadState,
            this.colPODRefNo,
            this.colAgentName,
            this.colHBLState,
            this.colMBLNo,
            this.colSubNo,
            this.colContainerNos,
            this.colVesselVoyageName,
            this.colPolName,
            this.colETD,
            this.colPODName,
            this.colETA,
            this.colPlaceOfDeliveryName,
            this.colPlaceofdeliveryDate,
            this.colConsigneeID,
            this.colPOLFilerName});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.IndicatorWidth = 40;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowDetailButtons = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.mainGridView_CustomDrawRowIndicator);
            this.gvMain.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvMain_RowCellClick);
            // 
            // colCheck
            // 
            this.colCheck.Caption = "选择";
            this.colCheck.FieldName = "IsCheck";
            this.colCheck.Name = "colCheck";
            this.colCheck.OptionsColumn.AllowEdit = false;
            this.colCheck.Visible = true;
            this.colCheck.VisibleIndex = 0;
            this.colCheck.Width = 50;
            // 
            // colDownLoadState
            // 
            this.colDownLoadState.Caption = "下载状态";
            this.colDownLoadState.FieldName = "DownLoadStateDescription";
            this.colDownLoadState.Name = "colDownLoadState";
            this.colDownLoadState.OptionsColumn.AllowEdit = false;
            this.colDownLoadState.Visible = true;
            this.colDownLoadState.VisibleIndex = 1;
            // 
            // colPODRefNo
            // 
            this.colPODRefNo.Caption = "港后业务号";
            this.colPODRefNo.FieldName = "PODRefNo";
            this.colPODRefNo.Name = "colPODRefNo";
            this.colPODRefNo.OptionsColumn.AllowEdit = false;
            this.colPODRefNo.Visible = true;
            this.colPODRefNo.VisibleIndex = 2;
            // 
            // colHBLState
            // 
            this.colHBLState.Caption = "提单状态";
            this.colHBLState.FieldName = "HBLStateDescription";
            this.colHBLState.Name = "colHBLState";
            this.colHBLState.OptionsColumn.AllowEdit = false;
            this.colHBLState.Visible = true;
            this.colHBLState.VisibleIndex = 4;
            // 
            // colMBLNo
            // 
            this.colMBLNo.Caption = "主提单号";
            this.colMBLNo.FieldName = "MBLNo";
            this.colMBLNo.Name = "colMBLNo";
            this.colMBLNo.OptionsColumn.AllowEdit = false;
            this.colMBLNo.Visible = true;
            this.colMBLNo.VisibleIndex = 5;
            // 
            // colSubNo
            // 
            this.colSubNo.Caption = "分提单号";
            this.colSubNo.FieldName = "HBLNo";
            this.colSubNo.Name = "colSubNo";
            this.colSubNo.OptionsColumn.AllowEdit = false;
            this.colSubNo.Visible = true;
            this.colSubNo.VisibleIndex = 6;
            // 
            // colContainerNos
            // 
            this.colContainerNos.Caption = "箱号";
            this.colContainerNos.FieldName = "ContainerNos";
            this.colContainerNos.Name = "colContainerNos";
            this.colContainerNos.OptionsColumn.AllowEdit = false;
            this.colContainerNos.Visible = true;
            this.colContainerNos.VisibleIndex = 7;
            // 
            // colVesselVoyageName
            // 
            this.colVesselVoyageName.Caption = "船名/航次";
            this.colVesselVoyageName.FieldName = "VesselVoyage";
            this.colVesselVoyageName.Name = "colVesselVoyageName";
            this.colVesselVoyageName.OptionsColumn.AllowEdit = false;
            this.colVesselVoyageName.Visible = true;
            this.colVesselVoyageName.VisibleIndex = 8;
            // 
            // colPolName
            // 
            this.colPolName.Caption = "装货港";
            this.colPolName.FieldName = "POLName";
            this.colPolName.Name = "colPolName";
            this.colPolName.OptionsColumn.AllowEdit = false;
            this.colPolName.Visible = true;
            this.colPolName.VisibleIndex = 9;
            // 
            // colETD
            // 
            this.colETD.Caption = "离港日";
            this.colETD.FieldName = "ETD";
            this.colETD.Name = "colETD";
            this.colETD.OptionsColumn.AllowEdit = false;
            this.colETD.Visible = true;
            this.colETD.VisibleIndex = 10;
            // 
            // colPODName
            // 
            this.colPODName.Caption = "卸货港";
            this.colPODName.FieldName = "PODName";
            this.colPODName.Name = "colPODName";
            this.colPODName.OptionsColumn.AllowEdit = false;
            this.colPODName.Visible = true;
            this.colPODName.VisibleIndex = 11;
            // 
            // colETA
            // 
            this.colETA.Caption = "到港日";
            this.colETA.FieldName = "ETA";
            this.colETA.Name = "colETA";
            this.colETA.OptionsColumn.AllowEdit = false;
            this.colETA.Visible = true;
            this.colETA.VisibleIndex = 12;
            // 
            // colPlaceOfDeliveryName
            // 
            this.colPlaceOfDeliveryName.Caption = "交货地";
            this.colPlaceOfDeliveryName.FieldName = "PlaceOfDeliveryNames";
            this.colPlaceOfDeliveryName.Name = "colPlaceOfDeliveryName";
            this.colPlaceOfDeliveryName.OptionsColumn.AllowEdit = false;
            this.colPlaceOfDeliveryName.Visible = true;
            this.colPlaceOfDeliveryName.VisibleIndex = 13;
            // 
            // colPlaceofdeliveryDate
            // 
            this.colPlaceofdeliveryDate.Caption = "到交货地日";
            this.colPlaceofdeliveryDate.FieldName = "PlaceofdeliveryDates";
            this.colPlaceofdeliveryDate.Name = "colPlaceofdeliveryDate";
            this.colPlaceofdeliveryDate.Visible = true;
            this.colPlaceofdeliveryDate.VisibleIndex = 14;
            this.colPlaceofdeliveryDate.Width = 100;
            // 
            // colConsigneeID
            // 
            this.colConsigneeID.Caption = "收货人";
            this.colConsigneeID.ColumnEdit = this.stxtConsigneeID;
            this.colConsigneeID.FieldName = "ConsigneeName";
            this.colConsigneeID.Name = "colConsigneeID";
            this.colConsigneeID.Visible = true;
            this.colConsigneeID.VisibleIndex = 15;
            this.colConsigneeID.Width = 200;
            // 
            // stxtConsigneeID
            // 
            this.stxtConsigneeID.AutoHeight = false;
            this.stxtConsigneeID.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtConsigneeID.Name = "stxtConsigneeID";
            // 
            // colPOLFilerName
            // 
            this.colPOLFilerName.Caption = "港前客服";
            this.colPOLFilerName.FieldName = "POLFilerName";
            this.colPOLFilerName.Name = "colPOLFilerName";
            this.colPOLFilerName.OptionsColumn.AllowEdit = false;
            this.colPOLFilerName.Visible = true;
            this.colPOLFilerName.VisibleIndex = 16;
            // 
            // colAgentName
            // 
            this.colAgentName.Caption = "代理公司";
            this.colAgentName.FieldName = "AgentName";
            this.colAgentName.Name = "colAgentName";
            this.colAgentName.OptionsColumn.AllowEdit = false;
            this.colAgentName.Visible = true;
            this.colAgentName.VisibleIndex = 3;
            // 
            // rcmbState
            // 
            this.rcmbState.AutoHeight = false;
            this.rcmbState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbState.Name = "rcmbState";
            // 
            // ckbIsCheck
            // 
            this.ckbIsCheck.Name = "ckbIsCheck";
            // 
            // OIBusinessDownloadList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Name = "OIBusinessDownloadList";
            this.Size = new System.Drawing.Size(839, 685);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsOEList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtConsigneeID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbIsCheck)).EndInit();
            this.ResumeLayout(false);

        }
      
        #endregion

        protected ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        protected DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbState;
        private DevExpress.XtraGrid.Columns.GridColumn colCheck;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit ckbIsCheck;
        private DevExpress.XtraGrid.Columns.GridColumn colDownLoadState;
        private DevExpress.XtraGrid.Columns.GridColumn colMBLNo;
        private DevExpress.XtraGrid.Columns.GridColumn colSubNo;
        private DevExpress.XtraGrid.Columns.GridColumn colContainerNos;
        private DevExpress.XtraGrid.Columns.GridColumn colVesselVoyageName;
        private DevExpress.XtraGrid.Columns.GridColumn colPolName;
        private DevExpress.XtraGrid.Columns.GridColumn colETD;
        private DevExpress.XtraGrid.Columns.GridColumn colPODName;
        private DevExpress.XtraGrid.Columns.GridColumn colETA;
        private DevExpress.XtraGrid.Columns.GridColumn colPlaceOfDeliveryName;
        private DevExpress.XtraGrid.Columns.GridColumn colPlaceofdeliveryDate;
        private DevExpress.XtraGrid.Columns.GridColumn colConsigneeID;
        private DevExpress.XtraGrid.Columns.GridColumn colPOLFilerName;
        private DevExpress.XtraGrid.Columns.GridColumn colAgentName;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit stxtConsigneeID;
        private System.Windows.Forms.BindingSource bsOEList;
        private DevExpress.XtraGrid.Columns.GridColumn colHBLState;
        private DevExpress.XtraGrid.Columns.GridColumn colPODRefNo;
    }
}
