namespace ICP.FCM.OtherBusiness.UI.ECommerce
{
    partial class OBECListPart
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
            this.rcmbState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RBLD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colMblno = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHblno = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPolName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPodName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNotifyPartyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAgengofCarrierName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConsigneeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShipperName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsValid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsOBList = new System.Windows.Forms.BindingSource();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsOBList)).BeginInit();
            this.SuspendLayout();
            // 
            // rcmbState
            // 
            this.rcmbState.AutoHeight = false;
            this.rcmbState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbState.Name = "rcmbState";
            // 
            // gvMain
            // 
            this.gvMain.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNO,
            this.RBLD,
            this.colState,
            this.colMblno,
            this.colHblno,
            this.colCustomerName,
            this.colPolName,
            this.colPodName,
            this.colNotifyPartyName,
            this.colAgengofCarrierName,
            this.colConsigneeName,
            this.colShipperName,
            this.colIsValid,
            this.colUpdateByName,
            this.colUpdateDate});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.IndicatorWidth = 35;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowDetailButtons = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvMain_RowCellClick);
            this.gvMain.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvMain_CustomDrawRowIndicator);
            this.gvMain.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvMain_RowStyle);
            this.gvMain.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(this.gvMain_BeforeLeaveRow);
            this.gvMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvMain_KeyDown);
            this.gvMain.DoubleClick += new System.EventHandler(this.gvMain_DoubleClick);
            // 
            // colNO
            // 
            this.colNO.FieldName = "NO";
            this.colNO.Name = "colNO";
            this.colNO.Visible = true;
            this.colNO.VisibleIndex = 0;
            // 
            // RBLD
            // 
            this.RBLD.Caption = "RBLD";
            this.RBLD.FieldName = "RBLD";
            this.RBLD.Name = "RBLD";
            this.RBLD.Visible = true;
            this.RBLD.VisibleIndex = 1;
            // 
            // colState
            // 
            this.colState.ColumnEdit = this.rcmState;
            this.colState.FieldName = "State";
            this.colState.Name = "colState";
            this.colState.Visible = true;
            this.colState.VisibleIndex = 2;
            // 
            // rcmState
            // 
            this.rcmState.AutoHeight = false;
            this.rcmState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmState.Name = "rcmState";
            // 
            // colMblno
            // 
            this.colMblno.FieldName = "Mblno";
            this.colMblno.Name = "colMblno";
            this.colMblno.Visible = true;
            this.colMblno.VisibleIndex = 3;
            // 
            // colHblno
            // 
            this.colHblno.FieldName = "Hblno";
            this.colHblno.Name = "colHblno";
            this.colHblno.Visible = true;
            this.colHblno.VisibleIndex = 4;
            // 
            // colCustomerName
            // 
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 5;
            // 
            // colPolName
            // 
            this.colPolName.FieldName = "PolName";
            this.colPolName.Name = "colPolName";
            this.colPolName.Visible = true;
            this.colPolName.VisibleIndex = 6;
            // 
            // colPodName
            // 
            this.colPodName.FieldName = "PodName";
            this.colPodName.Name = "colPodName";
            this.colPodName.Visible = true;
            this.colPodName.VisibleIndex = 7;
            // 
            // colNotifyPartyName
            // 
            this.colNotifyPartyName.FieldName = "NotifyPartyName";
            this.colNotifyPartyName.Name = "colNotifyPartyName";
            this.colNotifyPartyName.Visible = true;
            this.colNotifyPartyName.VisibleIndex = 11;
            // 
            // colAgengofCarrierName
            // 
            this.colAgengofCarrierName.FieldName = "AgengofCarrierName";
            this.colAgengofCarrierName.Name = "colAgengofCarrierName";
            this.colAgengofCarrierName.Visible = true;
            this.colAgengofCarrierName.VisibleIndex = 8;
            // 
            // colConsigneeName
            // 
            this.colConsigneeName.FieldName = "ConsigneeName";
            this.colConsigneeName.Name = "colConsigneeName";
            this.colConsigneeName.Visible = true;
            this.colConsigneeName.VisibleIndex = 10;
            // 
            // colShipperName
            // 
            this.colShipperName.FieldName = "ShipperName";
            this.colShipperName.Name = "colShipperName";
            this.colShipperName.Visible = true;
            this.colShipperName.VisibleIndex = 9;
            // 
            // colIsValid
            // 
            this.colIsValid.FieldName = "IsValid";
            this.colIsValid.Name = "colIsValid";
            this.colIsValid.Visible = true;
            this.colIsValid.VisibleIndex = 12;
            // 
            // colUpdateByName
            // 
            this.colUpdateByName.Caption = "UpdateByName";
            this.colUpdateByName.FieldName = "UpdateByName";
            this.colUpdateByName.Name = "colUpdateByName";
            this.colUpdateByName.Visible = true;
            this.colUpdateByName.VisibleIndex = 13;
            // 
            // colUpdateDate
            // 
            this.colUpdateDate.FieldName = "UpdateDate";
            this.colUpdateDate.Name = "colUpdateDate";
            this.colUpdateDate.Visible = true;
            this.colUpdateDate.VisibleIndex = 14;
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsOBList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbState,
            this.rcmState});
            this.gcMain.Size = new System.Drawing.Size(938, 409);
            this.gcMain.TabIndex = 2;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // bsOBList
            // 
            this.bsOBList.DataSource = typeof(ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.OtherBusinessList);
            this.bsOBList.PositionChanged += new System.EventHandler(this.bsOBList_PositionChanged);
            // 
            // OBECListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Name = "OBECListPart";
            this.Size = new System.Drawing.Size(938, 409);
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsOBList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbState;
        protected DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        protected ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        private System.Windows.Forms.BindingSource bsOBList;
        private DevExpress.XtraGrid.Columns.GridColumn colNO;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraGrid.Columns.GridColumn colMblno;
        private DevExpress.XtraGrid.Columns.GridColumn colHblno;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colPolName;
        private DevExpress.XtraGrid.Columns.GridColumn colPodName;
        private DevExpress.XtraGrid.Columns.GridColumn colAgengofCarrierName;
        private DevExpress.XtraGrid.Columns.GridColumn colShipperName;
        private DevExpress.XtraGrid.Columns.GridColumn colConsigneeName;
        private DevExpress.XtraGrid.Columns.GridColumn colNotifyPartyName;
        private DevExpress.XtraGrid.Columns.GridColumn colIsValid;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmState;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateDate;
        private DevExpress.XtraGrid.Columns.GridColumn RBLD;

    }
}
