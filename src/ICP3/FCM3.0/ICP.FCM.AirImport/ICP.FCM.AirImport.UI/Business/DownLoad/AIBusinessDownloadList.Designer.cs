namespace ICP.FCM.AirImport.UI
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
            this.ckbIsCheck = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colDownLoadState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPODRefNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHBLState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMBLNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFlightNo = new DevExpress.XtraGrid.Columns.GridColumn();
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
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsOEList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbIsCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtConsigneeID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).BeginInit();
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
            //this.gcMain.Click += new System.EventHandler(this.gcMain_Click);
            // 
            // bsOEList
            // 
            this.bsOEList.DataSource = typeof(ICP.FCM.AirImport.ServiceInterface.AirBusinessDownLoadList);
            this.bsOEList.PositionChanged += new System.EventHandler(this.bsOEList_PositionChanged);
            // 
            // gvMain
            // 
            this.gvMain.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCheck,
            this.colDownLoadState,
            this.colPODRefNo,
            this.colHBLState,
            this.colMBLNo,
            this.colSubNo,
            this.colFlightNo,
            this.colPolName,
            this.colETD,
            this.colPODName,
            this.colETA,
            this.colPlaceOfDeliveryName,
            this.colPlaceofdeliveryDate,
            this.colConsigneeID,
            this.colPOLFilerName,
            this.colAgentName});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowDetailButtons = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(gvMain_RowCellClick);
          
            // 
            // colCheck
            // 
            this.colCheck.Caption = "选择";
            //this.colCheck.ColumnEdit = this.ckbIsCheck;
            this.colCheck.FieldName = "IsCheck";
            this.colCheck.Name = "colCheck";
            this.colCheck.OptionsColumn.AllowEdit = false;
            this.colCheck.Visible = true;
            this.colCheck.VisibleIndex = 0;
            this.colCheck.Width = 50;
            //// 
            //// ckbIsCheck
            //// 
            //this.ckbIsCheck.AutoHeight = false;
            //this.ckbIsCheck.Name = "ckbIsCheck";
            //this.ckbIsCheck.Click += new System.EventHandler(this.ckbIsCheck_Click);
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
            this.colHBLState.VisibleIndex = 3;
            // 
            // colMBLNo
            // 
            this.colMBLNo.Caption = "主提单号";
            this.colMBLNo.FieldName = "MBLNo";
            this.colMBLNo.Name = "colMBLNo";
            this.colMBLNo.OptionsColumn.AllowEdit = false;
            this.colMBLNo.Visible = true;
            this.colMBLNo.VisibleIndex = 4;
            // 
            // colSubNo
            // 
            this.colSubNo.Caption = "分提单号";
            this.colSubNo.FieldName = "HBLNo";
            this.colSubNo.Name = "colSubNo";
            this.colSubNo.OptionsColumn.AllowEdit = false;
            this.colSubNo.Visible = true;
            this.colSubNo.VisibleIndex = 5;
            // 
            // colFlightNo
            // 
            this.colFlightNo.Caption = "航班号";
            this.colFlightNo.FieldName = "FlightNo";
            this.colFlightNo.Name = "colFlightNo";
            this.colFlightNo.OptionsColumn.AllowEdit = false;
            this.colFlightNo.Visible = true;
            this.colFlightNo.VisibleIndex = 6;
            // 
            // colPolName
            // 
            this.colPolName.Caption = "起运港";
            this.colPolName.FieldName = "POLName";
            this.colPolName.Name = "colPolName";
            this.colPolName.OptionsColumn.AllowEdit = false;
            this.colPolName.Visible = true;
            this.colPolName.VisibleIndex = 7;
            // 
            // colETD
            // 
            this.colETD.Caption = "起航日";
            this.colETD.FieldName = "ETD";
            this.colETD.Name = "colETD";
            this.colETD.OptionsColumn.AllowEdit = false;
            this.colETD.Visible = true;
            this.colETD.VisibleIndex = 9;
            // 
            // colPODName
            // 
            this.colPODName.Caption = "目的港";
            this.colPODName.FieldName = "PODName";
            this.colPODName.Name = "colPODName";
            this.colPODName.OptionsColumn.AllowEdit = false;
            this.colPODName.Visible = true;
            this.colPODName.VisibleIndex = 8;
            // 
            // colETA
            // 
            this.colETA.Caption = "到达日";
            this.colETA.FieldName = "ETA";
            this.colETA.Name = "colETA";
            this.colETA.OptionsColumn.AllowEdit = false;
            this.colETA.Visible = true;
            this.colETA.VisibleIndex = 10;
            // 
            // colPlaceOfDeliveryName
            // 
            this.colPlaceOfDeliveryName.Caption = "交货地";
            this.colPlaceOfDeliveryName.FieldName = "PlaceOfDeliveryNames";
            this.colPlaceOfDeliveryName.Name = "colPlaceOfDeliveryName";
            this.colPlaceOfDeliveryName.OptionsColumn.AllowEdit = false;
            this.colPlaceOfDeliveryName.Visible = true;
            this.colPlaceOfDeliveryName.VisibleIndex = 11;
            // 
            // colPlaceofdeliveryDate
            // 
            this.colPlaceofdeliveryDate.Caption = "到交货地日";
            this.colPlaceofdeliveryDate.FieldName = "PlaceofdeliveryDates";
            this.colPlaceofdeliveryDate.Name = "colPlaceofdeliveryDate";
            this.colPlaceofdeliveryDate.Visible = true;
            this.colPlaceofdeliveryDate.VisibleIndex = 12;
            this.colPlaceofdeliveryDate.Width = 100;
            // 
            // colConsigneeID
            // 
            this.colConsigneeID.Caption = "收货人";
            this.colConsigneeID.ColumnEdit = this.stxtConsigneeID;
            this.colConsigneeID.FieldName = "ConsigneeName";
            this.colConsigneeID.Name = "colConsigneeID";
            this.colConsigneeID.Visible = true;
            this.colConsigneeID.VisibleIndex = 13;
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
            this.colPOLFilerName.VisibleIndex = 14;
            // 
            // colAgentName
            // 
            this.colAgentName.Caption = "代理公司";
            this.colAgentName.FieldName = "AgentName";
            this.colAgentName.Name = "colAgentName";
            this.colAgentName.OptionsColumn.AllowEdit = false;
            this.colAgentName.Visible = true;
            this.colAgentName.VisibleIndex = 15;
            // 
            // rcmbState
            // 
            this.rcmbState.AutoHeight = false;
            this.rcmbState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbState.Name = "rcmbState";
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
            ((System.ComponentModel.ISupportInitialize)(this.ckbIsCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtConsigneeID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).EndInit();
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
        private DevExpress.XtraGrid.Columns.GridColumn colFlightNo;
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
