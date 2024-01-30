namespace ICP.FAM.UI.ReleaseRC
{
    partial class ReleaseRCListPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReleaseRCListPart));
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.bvMain = new ICP.Framework.ClientComponents.Controls.LWBandedGridView();
            this.gridBandBLInfo = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colState = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.rcmbStatea = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.colETA = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colFETA = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colBlNo = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colAgentName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colBLType = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.ByType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colContainerNos = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colVesselVoyage = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colConsigneeName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colConsigneeContact = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colReleaseType = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.ReleaseType1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.gridBandReleaseInfo = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colTelexNo = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colReleaseBy = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colReleaseDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colRcCompanyName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colRcBy = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colRcDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colRcRemark = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colCreateByName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.rcmbState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.rcmbReleaseType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.cmbWriteOffStatus = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.cmbPaidStatus = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pageControl1 = new ICP.Framework.ClientComponents.Controls.PageControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labTip = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbStatea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ByType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReleaseType1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbReleaseType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWriteOffStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaidStatus)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.bvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.rcmbState,
            this.rcmbReleaseType,
            this.repositoryItemCheckEdit2,
            this.cmbWriteOffStatus,
            this.cmbPaidStatus,
            this.rcmbStatea,
            this.ReleaseType1,
            this.ByType});
            this.gcMain.Size = new System.Drawing.Size(928, 474);
            this.gcMain.TabIndex = 0;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bvMain});
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.ReleaseRCList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsMainList_PositionChanged);
            // 
            // bvMain
            // 
            this.bvMain.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBandBLInfo,
            this.gridBandReleaseInfo});
            this.bvMain.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colETA,
            this.colFETA,
            this.colBlNo,
            this.colAgentName,
            this.colBLType,
            this.colContainerNos,
            this.colConsigneeName,
            this.colConsigneeContact,
            this.colReleaseType,
            this.colState,
            this.colTelexNo,
            this.colReleaseBy,
            this.colReleaseDate,
            this.colRcCompanyName,
            this.colRcBy,
            this.colRcDate,
            this.colRcRemark,
            this.colCreateByName,
            this.colCreateDate,
            this.colVesselVoyage});
            this.bvMain.GridControl = this.gcMain;
            this.bvMain.IndicatorWidth = 28;
            this.bvMain.Name = "bvMain";
            this.bvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.bvMain.OptionsSelection.MultiSelect = true;
            this.bvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.bvMain.OptionsView.ColumnAutoWidth = false;
            this.bvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.bvMain.OptionsView.ShowGroupPanel = false;
            this.bvMain.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.bvMain_CustomDrawRowIndicator);
            this.bvMain.CustomerSorting += new ICP.Framework.ClientComponents.Controls.CustomerSortingHandler(this.bvMain_CustomerSorting);
            this.bvMain.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.bvMain_RowCellStyle);
            this.bvMain.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.bvMain_RowCellClick);
            // 
            // gridBandBLInfo
            // 
            this.gridBandBLInfo.Caption = "BL Info";
            this.gridBandBLInfo.Columns.Add(this.colState);
            this.gridBandBLInfo.Columns.Add(this.colETA);
            this.gridBandBLInfo.Columns.Add(this.colFETA);
            this.gridBandBLInfo.Columns.Add(this.colBlNo);
            this.gridBandBLInfo.Columns.Add(this.colAgentName);
            this.gridBandBLInfo.Columns.Add(this.colBLType);
            this.gridBandBLInfo.Columns.Add(this.colContainerNos);
            this.gridBandBLInfo.Columns.Add(this.colVesselVoyage);
            this.gridBandBLInfo.Columns.Add(this.colConsigneeName);
            this.gridBandBLInfo.Columns.Add(this.colConsigneeContact);
            this.gridBandBLInfo.Columns.Add(this.colReleaseType);
            this.gridBandBLInfo.MinWidth = 20;
            this.gridBandBLInfo.Name = "gridBandBLInfo";
            this.gridBandBLInfo.Width = 766;
            // 
            // colState
            // 
            this.colState.ColumnEdit = this.rcmbStatea;
            this.colState.FieldName = "State";
            this.colState.Name = "colState";
            this.colState.OptionsColumn.AllowEdit = false;
            this.colState.Visible = true;
            this.colState.Width = 91;
            // 
            // rcmbStatea
            // 
            this.rcmbStatea.AutoHeight = false;
            this.rcmbStatea.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbStatea.Name = "rcmbStatea";
            this.rcmbStatea.SmallImages = this.imageList1;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "NewOrder.png");
            // 
            // colETA
            // 
            this.colETA.FieldName = "ETA";
            this.colETA.Name = "colETA";
            this.colETA.Visible = true;
            // 
            // colFETA
            // 
            this.colFETA.FieldName = "FETA";
            this.colFETA.Name = "colFETA";
            this.colFETA.Visible = true;
            // 
            // colBlNo
            // 
            this.colBlNo.FieldName = "BlNo";
            this.colBlNo.Name = "colBlNo";
            this.colBlNo.Visible = true;
            // 
            // colAgentName
            // 
            this.colAgentName.FieldName = "AgentName";
            this.colAgentName.Name = "colAgentName";
            this.colAgentName.Visible = true;
            // 
            // colBLType
            // 
            this.colBLType.ColumnEdit = this.ByType;
            this.colBLType.FieldName = "BLType";
            this.colBLType.Name = "colBLType";
            this.colBLType.OptionsColumn.AllowEdit = false;
            // 
            // ByType
            // 
            this.ByType.AutoHeight = false;
            this.ByType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ByType.Name = "ByType";
            // 
            // colContainerNos
            // 
            this.colContainerNos.FieldName = "ContainerNos";
            this.colContainerNos.Name = "colContainerNos";
            this.colContainerNos.Visible = true;
            // 
            // colVesselVoyage
            // 
            this.colVesselVoyage.FieldName = "VesselVoyage";
            this.colVesselVoyage.Name = "colVesselVoyage";
            this.colVesselVoyage.Visible = true;
            // 
            // colConsigneeName
            // 
            this.colConsigneeName.FieldName = "ConsigneeName";
            this.colConsigneeName.Name = "colConsigneeName";
            this.colConsigneeName.Visible = true;
            // 
            // colConsigneeContact
            // 
            this.colConsigneeContact.FieldName = "ConsigneeContact";
            this.colConsigneeContact.Name = "colConsigneeContact";
            this.colConsigneeContact.Visible = true;
            // 
            // colReleaseType
            // 
            this.colReleaseType.ColumnEdit = this.ReleaseType1;
            this.colReleaseType.FieldName = "ReleaseType";
            this.colReleaseType.Name = "colReleaseType";
            this.colReleaseType.Visible = true;
            // 
            // ReleaseType1
            // 
            this.ReleaseType1.AutoHeight = false;
            this.ReleaseType1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ReleaseType1.Name = "ReleaseType1";
            // 
            // gridBandReleaseInfo
            // 
            this.gridBandReleaseInfo.Caption = "Release Info";
            this.gridBandReleaseInfo.Columns.Add(this.colTelexNo);
            this.gridBandReleaseInfo.Columns.Add(this.colReleaseBy);
            this.gridBandReleaseInfo.Columns.Add(this.colReleaseDate);
            this.gridBandReleaseInfo.Columns.Add(this.colRcCompanyName);
            this.gridBandReleaseInfo.Columns.Add(this.colRcBy);
            this.gridBandReleaseInfo.Columns.Add(this.colRcDate);
            this.gridBandReleaseInfo.Columns.Add(this.colRcRemark);
            this.gridBandReleaseInfo.Columns.Add(this.colCreateByName);
            this.gridBandReleaseInfo.Columns.Add(this.colCreateDate);
            this.gridBandReleaseInfo.MinWidth = 20;
            this.gridBandReleaseInfo.Name = "gridBandReleaseInfo";
            this.gridBandReleaseInfo.Width = 675;
            // 
            // colTelexNo
            // 
            this.colTelexNo.FieldName = "TelexNo";
            this.colTelexNo.Name = "colTelexNo";
            this.colTelexNo.Visible = true;
            // 
            // colReleaseBy
            // 
            this.colReleaseBy.FieldName = "ReleaseBy";
            this.colReleaseBy.Name = "colReleaseBy";
            this.colReleaseBy.Visible = true;
            // 
            // colReleaseDate
            // 
            this.colReleaseDate.FieldName = "ReleaseDate";
            this.colReleaseDate.Name = "colReleaseDate";
            this.colReleaseDate.Visible = true;
            // 
            // colRcCompanyName
            // 
            this.colRcCompanyName.FieldName = "RcCompanyName";
            this.colRcCompanyName.Name = "colRcCompanyName";
            this.colRcCompanyName.Visible = true;
            // 
            // colRcBy
            // 
            this.colRcBy.FieldName = "RcBy";
            this.colRcBy.Name = "colRcBy";
            this.colRcBy.Visible = true;
            // 
            // colRcDate
            // 
            this.colRcDate.FieldName = "RcDate";
            this.colRcDate.Name = "colRcDate";
            this.colRcDate.Visible = true;
            // 
            // colRcRemark
            // 
            this.colRcRemark.FieldName = "RcRemark";
            this.colRcRemark.Name = "colRcRemark";
            this.colRcRemark.Visible = true;
            // 
            // colCreateByName
            // 
            this.colCreateByName.FieldName = "CreateByName";
            this.colCreateByName.Name = "colCreateByName";
            this.colCreateByName.Visible = true;
            // 
            // colCreateDate
            // 
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // rcmbState
            // 
            this.rcmbState.AutoHeight = false;
            this.rcmbState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbState.Name = "rcmbState";
            this.rcmbState.SmallImages = this.imageList1;
            // 
            // rcmbReleaseType
            // 
            this.rcmbReleaseType.AutoHeight = false;
            this.rcmbReleaseType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbReleaseType.Name = "rcmbReleaseType";
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // cmbWriteOffStatus
            // 
            this.cmbWriteOffStatus.AutoHeight = false;
            this.cmbWriteOffStatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbWriteOffStatus.Name = "cmbWriteOffStatus";
            // 
            // cmbPaidStatus
            // 
            this.cmbPaidStatus.AutoHeight = false;
            this.cmbPaidStatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPaidStatus.Name = "cmbPaidStatus";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pageControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 500);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(928, 26);
            this.panel1.TabIndex = 4;
            // 
            // pageControl1
            // 
            this.pageControl1.CurrentPage = 0;
            this.pageControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageControl1.Location = new System.Drawing.Point(0, 0);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.Size = new System.Drawing.Size(928, 26);
            this.pageControl1.TabIndex = 0;
            this.pageControl1.TotalPage = 0;
            this.pageControl1.PageChanged += new ICP.Framework.ClientComponents.Controls.PageChangedHandler(this.pageControl1_PageChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labTip);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 474);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(928, 26);
            this.panelControl1.TabIndex = 5;
            this.panelControl1.Visible = false;
            // 
            // labTip
            // 
            this.labTip.AutoSize = true;
            this.labTip.Location = new System.Drawing.Point(9, 5);
            this.labTip.Name = "labTip";
            this.labTip.Size = new System.Drawing.Size(0, 14);
            this.labTip.TabIndex = 0;
            this.labTip.Visible = false;
            // 
            // ReleaseRCListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panel1);
            this.Name = "ReleaseRCListPart";
            this.Size = new System.Drawing.Size(928, 526);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbStatea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ByType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReleaseType1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbReleaseType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWriteOffStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaidStatus)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbState;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbReleaseType;
        private ICP.Framework.ClientComponents.Controls.LWBandedGridView bvMain;
        private System.Windows.Forms.Panel panel1;
        private ICP.Framework.ClientComponents.Controls.PageControl pageControl1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label labTip;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbWriteOffStatus;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbPaidStatus;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colETA;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colFETA;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colBlNo;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colAgentName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colBLType;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colContainerNos;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colConsigneeName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colConsigneeContact;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colReleaseType;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colState;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTelexNo;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colReleaseBy;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colReleaseDate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCreateByName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCreateDate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colRcBy;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colRcCompanyName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colRcDate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colRcRemark;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colVesselVoyage;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbStatea;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox ReleaseType1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox ByType;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandBLInfo;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandReleaseInfo;
    }
}
