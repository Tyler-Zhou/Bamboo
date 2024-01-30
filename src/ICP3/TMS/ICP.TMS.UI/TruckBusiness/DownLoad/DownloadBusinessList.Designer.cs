namespace ICP.TMS.UI
{
    partial class DownloadBusinessList
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
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerRefNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContainerNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPortName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPortDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVesselVoyage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTruckBusinessNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.ckbIsCheck = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.stxtConsigneeID = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbIsCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtConsigneeID)).BeginInit();
            this.SuspendLayout();
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbState,
            this.ckbIsCheck,
            this.stxtConsigneeID});
            this.gcMain.Size = new System.Drawing.Size(1100, 685);
            this.gcMain.TabIndex = 2;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
           
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.TMS.ServiceInterface.DownLoadOceanBusinessList);
            // 
            // gvMain
            // 
            this.gvMain.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSelect,
            this.colState,
            this.colType,
            this.colCustomer,
            this.colCustomerRefNo,
            this.colContainerNo,
            this.colPortName,
            this.colPortDate,
            this.colVesselVoyage,
            this.colTruckBusinessNo});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowDetailButtons = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            // 
            // colSelect
            // 
            this.colSelect.Caption = "选择";
            this.colSelect.FieldName = "IsSelect";
            this.colSelect.Name = "colSelect";
            this.colSelect.Visible = true;
            this.colSelect.VisibleIndex = 0;
            this.colSelect.Width = 47;
            // 
            // colState
            // 
            this.colState.Caption = "下载状态";
            this.colState.FieldName = "StateDescription";
            this.colState.Name = "colState";
            this.colState.Visible = true;
            this.colState.VisibleIndex = 1;
            this.colState.Width = 96;
            // 
            // colType
            // 
            this.colType.Caption = "进/出";
            this.colType.FieldName = "TypeDescripttion";
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 2;
            // 
            // colCustomer
            // 
            this.colCustomer.Caption = "客户";
            this.colCustomer.FieldName = "CustomerName";
            this.colCustomer.Name = "colCustomer";
            this.colCustomer.Visible = true;
            this.colCustomer.VisibleIndex = 3;
            this.colCustomer.Width = 172;
            // 
            // colCustomerRefNo
            // 
            this.colCustomerRefNo.Caption = "客户参考号";
            this.colCustomerRefNo.FieldName = "CustomerRefNo";
            this.colCustomerRefNo.Name = "colCustomerRefNo";
            this.colCustomerRefNo.Visible = true;
            this.colCustomerRefNo.VisibleIndex = 4;
            this.colCustomerRefNo.Width = 123;
            // 
            // colContainerNo
            // 
            this.colContainerNo.Caption = "箱号";
            this.colContainerNo.FieldName = "ContainerNo";
            this.colContainerNo.Name = "colContainerNo";
            this.colContainerNo.Visible = true;
            this.colContainerNo.VisibleIndex = 5;
            this.colContainerNo.Width = 136;
            // 
            // colPortName
            // 
            this.colPortName.Caption = "装/卸货港";
            this.colPortName.FieldName = "PortName";
            this.colPortName.Name = "colPortName";
            this.colPortName.Visible = true;
            this.colPortName.VisibleIndex = 6;
            this.colPortName.Width = 110;
            // 
            // colPortDate
            // 
            this.colPortDate.Caption = "离/到港日";
            this.colPortDate.FieldName = "PortDate";
            this.colPortDate.Name = "colPortDate";
            this.colPortDate.Visible = true;
            this.colPortDate.VisibleIndex = 7;
            this.colPortDate.Width = 81;
            // 
            // colVesselVoyage
            // 
            this.colVesselVoyage.Caption = "船名/航次";
            this.colVesselVoyage.FieldName = "VesselVoyage";
            this.colVesselVoyage.Name = "colVesselVoyage";
            this.colVesselVoyage.Visible = true;
            this.colVesselVoyage.VisibleIndex = 8;
            this.colVesselVoyage.Width = 110;
            // 
            // colTruckBusinessNo
            // 
            this.colTruckBusinessNo.Caption = "拖车业务号";
            this.colTruckBusinessNo.FieldName = "RefNo";
            this.colTruckBusinessNo.Name = "colTruckBusinessNo";
            this.colTruckBusinessNo.Visible = true;
            this.colTruckBusinessNo.VisibleIndex = 9;
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
            this.ckbIsCheck.AutoHeight = false;
            this.ckbIsCheck.Name = "ckbIsCheck";
            // 
            // stxtConsigneeID
            // 
            this.stxtConsigneeID.AutoHeight = false;
            this.stxtConsigneeID.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtConsigneeID.Name = "stxtConsigneeID";
            // 
            // DownloadBusinessList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Name = "DownloadBusinessList";
            this.Size = new System.Drawing.Size(1100, 685);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbIsCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtConsigneeID)).EndInit();
            this.ResumeLayout(false);

        }
      
        #endregion

        protected ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        protected DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbState;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit ckbIsCheck;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit stxtConsigneeID;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerRefNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomer;
        private DevExpress.XtraGrid.Columns.GridColumn colPortName;
        private DevExpress.XtraGrid.Columns.GridColumn colPortDate;
        private DevExpress.XtraGrid.Columns.GridColumn colVesselVoyage;
        private DevExpress.XtraGrid.Columns.GridColumn colSelect;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraGrid.Columns.GridColumn colContainerNo;
        private DevExpress.XtraGrid.Columns.GridColumn colTruckBusinessNo;
    }
}
