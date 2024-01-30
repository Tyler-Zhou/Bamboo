namespace ICP.TMS.UI
{
    partial class TruckBookingsListPart
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
            this.treeMain = new ICP.Framework.ClientComponents.Controls.LWTreeGridControl();
            this.colState = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colNo = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colType = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colContainerNo = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colContainerType = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTrayNo = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTruckDate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTruckPlace = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colLastFreeDate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDriverName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTruckNo = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCustomer = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCustomerRefNo = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colMBLNo = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCreateByName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCreateDate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colIsValid = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colRemark = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.cmbState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.cmbType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colUpdateByName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colBookingUpdateDate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.TMS.ServiceInterface.TruckBookingsList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsList_PositionChanged);
            // 
            // treeMain
            // 
            this.treeMain.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colState,
            this.colNo,
            this.colType,
            this.colContainerNo,
            this.colContainerType,
            this.colTrayNo,
            this.colTruckDate,
            this.colTruckPlace,
            this.colLastFreeDate,
            this.colDriverName,
            this.colTruckNo,
            this.colCustomer,
            this.colCustomerRefNo,
            this.colMBLNo,
            this.colCreateByName,
            this.colCreateDate,
            this.colIsValid,
            this.colRemark,
            this.colUpdateByName,
            this.colBookingUpdateDate});
            this.treeMain.DataSource = this.bsList;
            this.treeMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMain.Location = new System.Drawing.Point(0, 0);
            this.treeMain.Name = "treeMain";
            this.treeMain.OptionsBehavior.Editable = false;
            this.treeMain.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.treeMain.OptionsSelection.MultiSelect = true;
            this.treeMain.OptionsView.AutoWidth = false;
            this.treeMain.OptionsView.EnableAppearanceEvenRow = true;
            this.treeMain.ParentFieldName = "BookingID";
            this.treeMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbState,
            this.cmbType});
            this.treeMain.RootValue = null;
            this.treeMain.Size = new System.Drawing.Size(601, 409);
            this.treeMain.TabIndex = 2;
            this.treeMain.CustomDrawNodeIndicator += new DevExpress.XtraTreeList.CustomDrawNodeIndicatorEventHandler(this.treeMain_CustomDrawNodeIndicator);
            this.treeMain.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeMain_NodeCellStyle);
            this.treeMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeMain_KeyDown);
            this.treeMain.DoubleClick += new System.EventHandler(this.treeMain_DoubleClick);
            // 
            // colState
            // 
            this.colState.Caption = "状态";
            this.colState.FieldName = "StateDescription";
            this.colState.Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            this.colState.Name = "colState";
            this.colState.OptionsColumn.AllowEdit = false;
            this.colState.Visible = true;
            this.colState.VisibleIndex = 0;
            this.colState.Width = 60;
            // 
            // colNo
            // 
            this.colNo.Caption = "业务号";
            this.colNo.FieldName = "No";
            this.colNo.Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            this.colNo.Name = "colNo";
            this.colNo.Visible = true;
            this.colNo.VisibleIndex = 1;
            this.colNo.Width = 100;
            // 
            // colType
            // 
            this.colType.Caption = "进/出";
            this.colType.FieldName = "TypeDescription";
            this.colType.Name = "colType";
            this.colType.OptionsColumn.AllowEdit = false;
            this.colType.Visible = true;
            this.colType.VisibleIndex = 2;
            // 
            // colContainerNo
            // 
            this.colContainerNo.Caption = "箱号";
            this.colContainerNo.FieldName = "ContainerNo";
            this.colContainerNo.Name = "colContainerNo";
            this.colContainerNo.Visible = true;
            this.colContainerNo.VisibleIndex = 3;
            this.colContainerNo.Width = 80;
            // 
            // colContainerType
            // 
            this.colContainerType.Caption = "箱型";
            this.colContainerType.FieldName = "ContainerType";
            this.colContainerType.Name = "colContainerType";
            this.colContainerType.Visible = true;
            this.colContainerType.VisibleIndex = 4;
            // 
            // colTrayNo
            // 
            this.colTrayNo.Caption = "托盘号";
            this.colTrayNo.FieldName = "TrayNo";
            this.colTrayNo.Name = "colTrayNo";
            this.colTrayNo.Visible = true;
            this.colTrayNo.VisibleIndex = 5;
            this.colTrayNo.Width = 80;
            // 
            // colTruckDate
            // 
            this.colTruckDate.Caption = "派车日期";
            this.colTruckDate.FieldName = "TruckDate";
            this.colTruckDate.Name = "colTruckDate";
            this.colTruckDate.Visible = true;
            this.colTruckDate.VisibleIndex = 6;
            this.colTruckDate.Width = 85;
            // 
            // colTruckPlace
            // 
            this.colTruckPlace.Caption = "地点";
            this.colTruckPlace.FieldName = "TruckPlace";
            this.colTruckPlace.Name = "colTruckPlace";
            this.colTruckPlace.Visible = true;
            this.colTruckPlace.VisibleIndex = 7;
            this.colTruckPlace.Width = 100;
            // 
            // colLastFreeDate
            // 
            this.colLastFreeDate.Caption = "免堆日";
            this.colLastFreeDate.FieldName = "LastFreeDate";
            this.colLastFreeDate.Name = "colLastFreeDate";
            this.colLastFreeDate.Visible = true;
            this.colLastFreeDate.VisibleIndex = 8;
            this.colLastFreeDate.Width = 85;
            // 
            // colDriverName
            // 
            this.colDriverName.Caption = "司机";
            this.colDriverName.FieldName = "DriverName";
            this.colDriverName.Name = "colDriverName";
            this.colDriverName.Visible = true;
            this.colDriverName.VisibleIndex = 9;
            this.colDriverName.Width = 80;
            // 
            // colTruckNo
            // 
            this.colTruckNo.Caption = "车牌号";
            this.colTruckNo.FieldName = "TruckNo";
            this.colTruckNo.Name = "colTruckNo";
            this.colTruckNo.Visible = true;
            this.colTruckNo.VisibleIndex = 10;
            this.colTruckNo.Width = 90;
            // 
            // colCustomer
            // 
            this.colCustomer.Caption = "客户";
            this.colCustomer.FieldName = "CustomerName";
            this.colCustomer.Name = "colCustomer";
            this.colCustomer.Visible = true;
            this.colCustomer.VisibleIndex = 11;
            this.colCustomer.Width = 166;
            // 
            // colCustomerRefNo
            // 
            this.colCustomerRefNo.Caption = "客户参考号";
            this.colCustomerRefNo.FieldName = "CustomerRefNo";
            this.colCustomerRefNo.Name = "colCustomerRefNo";
            this.colCustomerRefNo.Visible = true;
            this.colCustomerRefNo.VisibleIndex = 12;
            this.colCustomerRefNo.Width = 110;
            // 
            // colMBLNo
            // 
            this.colMBLNo.Caption = "船东提单号";
            this.colMBLNo.FieldName = "MBLNo";
            this.colMBLNo.Name = "colMBLNo";
            this.colMBLNo.Visible = true;
            this.colMBLNo.VisibleIndex = 13;
            // 
            // colCreateByName
            // 
            this.colCreateByName.Caption = "创建人";
            this.colCreateByName.FieldName = "CreateByName";
            this.colCreateByName.Name = "colCreateByName";
            this.colCreateByName.Visible = true;
            this.colCreateByName.VisibleIndex = 14;
            this.colCreateByName.Width = 80;
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "创建时间";
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Format.FormatString = "d";
            this.colCreateDate.Format.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 16;
            this.colCreateDate.Width = 100;
            // 
            // colIsValid
            // 
            this.colIsValid.Caption = "有效性";
            this.colIsValid.FieldName = "IsValid";
            this.colIsValid.Name = "colIsValid";
            this.colIsValid.Visible = true;
            this.colIsValid.VisibleIndex = 15;
            // 
            // colRemark
            // 
            this.colRemark.Caption = "备注";
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 17;
            this.colRemark.Width = 120;
            // 
            // cmbState
            // 
            this.cmbState.AutoHeight = false;
            this.cmbState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbState.Name = "cmbState";
            // 
            // cmbType
            // 
            this.cmbType.AutoHeight = false;
            this.cmbType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Name = "cmbType";
            // 
            // colUpdateByName
            // 
            this.colUpdateByName.Caption = "更新人";
            this.colUpdateByName.FieldName = "UpdateByName";
            this.colUpdateByName.Name = "colUpdateByName";
            this.colUpdateByName.Visible = true;
            this.colUpdateByName.VisibleIndex = 18;
            // 
            // colBookingUpdateDate
            // 
            this.colBookingUpdateDate.Caption = "更新时间";
            this.colBookingUpdateDate.FieldName = "BookingUpdateDate";
            this.colBookingUpdateDate.Name = "colBookingUpdateDate";
            this.colBookingUpdateDate.Visible = true;
            this.colBookingUpdateDate.VisibleIndex = 19;
            // 
            // TruckBookingsListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeMain);
            this.Name = "TruckBookingsListPart";
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        private ICP.Framework.ClientComponents.Controls.LWTreeGridControl treeMain;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colState;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbState;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNo;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbType;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colContainerType;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTrayNo;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTruckDate;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colContainerNo;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colLastFreeDate;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDriverName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTruckNo;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCustomer;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCreateByName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCreateDate;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRemark;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTruckPlace;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCustomerRefNo;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colIsValid;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colMBLNo;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colUpdateByName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colBookingUpdateDate;

    }
}
