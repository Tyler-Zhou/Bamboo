namespace ICP.FCM.OceanExport.UI.Temp
{
    partial class DocumentListPart
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
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDocumentType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colDocumentNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumberOfCopies = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumberOfOriginal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReceivedDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReleaseDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReleaseMode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReturnDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTrackingNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbType)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FCM.Common.ServiceInterface.DataObjects.CommonDocumentList);
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
            this.barEdit});
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
            new DevExpress.XtraBars.LinkPersistInfo(this.barAdd, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDelete, true)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barAdd
            // 
            this.barAdd.Caption = "新增(&N)";
            this.barAdd.Id = 0;
            this.barAdd.Name = "barAdd";
            // 
            // barEdit
            // 
            this.barEdit.Caption = "编辑(&E)";
            this.barEdit.Id = 3;
            this.barEdit.Name = "barEdit";
            // 
            // barDelete
            // 
            this.barDelete.Caption = "删除(&R)";
            this.barDelete.Id = 2;
            this.barDelete.Name = "barDelete";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(612, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 297);
            this.barDockControlBottom.Size = new System.Drawing.Size(612, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 273);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(612, 24);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 273);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 24);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbType});
            this.gcMain.Size = new System.Drawing.Size(612, 273);
            this.gcMain.TabIndex = 4;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDocumentType,
            this.colDocumentNo,
            this.colNumberOfCopies,
            this.colNumberOfOriginal,
            this.colReceivedDate,
            this.colReleaseDate,
            this.colReleaseMode,
            this.colReturnDate,
            this.colTrackingNo,
            this.colRemark});
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
            // colDocumentType
            // 
            this.colDocumentType.Caption = "类型";
            this.colDocumentType.ColumnEdit = this.rcmbType;
            this.colDocumentType.FieldName = "DocumentType";
            this.colDocumentType.Name = "colDocumentType";
            this.colDocumentType.Visible = true;
            this.colDocumentType.VisibleIndex = 0;
            this.colDocumentType.Width = 80;
            // 
            // rcmbType
            // 
            this.rcmbType.AutoHeight = false;
            this.rcmbType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbType.Name = "rcmbType";
            // 
            // colDocumentNo
            // 
            this.colDocumentNo.Caption = "单证号";
            this.colDocumentNo.FieldName = "DocumentNo";
            this.colDocumentNo.Name = "colDocumentNo";
            this.colDocumentNo.Visible = true;
            this.colDocumentNo.VisibleIndex = 1;
            this.colDocumentNo.Width = 120;
            // 
            // colNumberOfCopies
            // 
            this.colNumberOfCopies.Caption = "副本份数";
            this.colNumberOfCopies.FieldName = "NumberOfCopies";
            this.colNumberOfCopies.Name = "colNumberOfCopies";
            this.colNumberOfCopies.Visible = true;
            this.colNumberOfCopies.VisibleIndex = 2;
            this.colNumberOfCopies.Width = 80;
            // 
            // colNumberOfOriginal
            // 
            this.colNumberOfOriginal.Caption = "正本份数";
            this.colNumberOfOriginal.FieldName = "NumberOfOriginal";
            this.colNumberOfOriginal.Name = "colNumberOfOriginal";
            this.colNumberOfOriginal.Visible = true;
            this.colNumberOfOriginal.VisibleIndex = 3;
            this.colNumberOfOriginal.Width = 80;
            // 
            // colReceivedDate
            // 
            this.colReceivedDate.Caption = "接收日期";
            this.colReceivedDate.FieldName = "ReceivedDate";
            this.colReceivedDate.Name = "colReceivedDate";
            this.colReceivedDate.Visible = true;
            this.colReceivedDate.VisibleIndex = 4;
            this.colReceivedDate.Width = 80;
            // 
            // colReleaseDate
            // 
            this.colReleaseDate.Caption = "放单日期";
            this.colReleaseDate.FieldName = "ReleaseDate";
            this.colReleaseDate.Name = "colReleaseDate";
            this.colReleaseDate.Visible = true;
            this.colReleaseDate.VisibleIndex = 5;
            this.colReleaseDate.Width = 80;
            // 
            // colReleaseMode
            // 
            this.colReleaseMode.Caption = "放单方式";
            this.colReleaseMode.FieldName = "ReleaseMode";
            this.colReleaseMode.Name = "colReleaseMode";
            this.colReleaseMode.Visible = true;
            this.colReleaseMode.VisibleIndex = 6;
            this.colReleaseMode.Width = 100;
            // 
            // colReturnDate
            // 
            this.colReturnDate.Caption = "归还日期";
            this.colReturnDate.FieldName = "ReturnDate";
            this.colReturnDate.Name = "colReturnDate";
            this.colReturnDate.Visible = true;
            this.colReturnDate.VisibleIndex = 7;
            this.colReturnDate.Width = 100;
            // 
            // colTrackingNo
            // 
            this.colTrackingNo.Caption = "快递单号";
            this.colTrackingNo.FieldName = "TrackingNo";
            this.colTrackingNo.Name = "colTrackingNo";
            this.colTrackingNo.Visible = true;
            this.colTrackingNo.VisibleIndex = 8;
            this.colTrackingNo.Width = 100;
            // 
            // colRemark
            // 
            this.colRemark.Caption = "备注";
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 9;
            this.colRemark.Width = 100;
            // 
            // DocumentListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "DocumentListPart";
            this.Size = new System.Drawing.Size(612, 297);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barAdd;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barEdit;
        protected ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        protected DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbType;
        private DevExpress.XtraGrid.Columns.GridColumn colDocumentNo;
        private DevExpress.XtraGrid.Columns.GridColumn colDocumentType;
        private DevExpress.XtraGrid.Columns.GridColumn colNumberOfCopies;
        private DevExpress.XtraGrid.Columns.GridColumn colNumberOfOriginal;
        private DevExpress.XtraGrid.Columns.GridColumn colReceivedDate;
        private DevExpress.XtraGrid.Columns.GridColumn colReleaseDate;
        private DevExpress.XtraGrid.Columns.GridColumn colReleaseMode;
        private DevExpress.XtraGrid.Columns.GridColumn colReturnDate;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;
        private DevExpress.XtraGrid.Columns.GridColumn colTrackingNo;
    }
}
