namespace ICP.FRM.UI.SearchRate
{
    partial class SearchOceanRemark
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
            this.txtRemark = new System.Windows.Forms.RichTextBox();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gridControlFileList = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvFileList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colFileName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barDownLoad = new DevExpress.XtraBars.BarButtonItem();
            this.standaloneBarDockControl1 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.panelFile = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlFileList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFileList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rHyperLinkEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.panelFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtRemark
            // 
            this.txtRemark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRemark.Location = new System.Drawing.Point(0, 0);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(400, 330);
            this.txtRemark.TabIndex = 0;
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FRM.ServiceInterface.DataObjects.OceanFileList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsList_PositionChanged);
            // 
            // gridControlFileList
            // 
            this.gridControlFileList.DataSource = this.bsList;
            this.gridControlFileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlFileList.Location = new System.Drawing.Point(0, 26);
            this.gridControlFileList.MainView = this.gvFileList;
            this.gridControlFileList.Name = "gridControlFileList";
            this.gridControlFileList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rHyperLinkEdit1});
            this.gridControlFileList.Size = new System.Drawing.Size(400, 304);
            this.gridControlFileList.TabIndex = 5;
            this.gridControlFileList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvFileList});
            // 
            // gvFileList
            // 
            this.gvFileList.Appearance.FocusedRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gvFileList.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.Red;
            this.gvFileList.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvFileList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colFileName,
            this.colRemark,
            this.colCreateByName,
            this.colCreateDate});
            this.gvFileList.GridControl = this.gridControlFileList;
            this.gvFileList.Name = "gvFileList";
            this.gvFileList.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvFileList.OptionsBehavior.Editable = false;
            this.gvFileList.OptionsSelection.MultiSelect = true;
            this.gvFileList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvFileList.OptionsView.ShowGroupPanel = false;
            this.gvFileList.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvFileList_RowCellClick);
            // 
            // colFileName
            // 
            this.colFileName.Caption = "FileName";
            this.colFileName.ColumnEdit = this.rHyperLinkEdit1;
            this.colFileName.FieldName = "FileName";
            this.colFileName.Name = "colFileName";
            this.colFileName.Visible = true;
            this.colFileName.VisibleIndex = 0;
            this.colFileName.Width = 101;
            // 
            // rHyperLinkEdit1
            // 
            this.rHyperLinkEdit1.AutoHeight = false;
            this.rHyperLinkEdit1.Name = "rHyperLinkEdit1";
            // 
            // colRemark
            // 
            this.colRemark.Caption = "Description";
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 1;
            this.colRemark.Width = 85;
            // 
            // colCreateByName
            // 
            this.colCreateByName.Caption = "CreateBy";
            this.colCreateByName.FieldName = "CreateByName";
            this.colCreateByName.Name = "colCreateByName";
            this.colCreateByName.Visible = true;
            this.colCreateByName.VisibleIndex = 2;
            this.colCreateByName.Width = 85;
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "CreateDate";
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 3;
            this.colCreateDate.Width = 103;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl1);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barDownLoad});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 1;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar2.FloatLocation = new System.Drawing.Point(308, 346);
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDownLoad, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.StandaloneBarDockControl = this.standaloneBarDockControl1;
            this.bar2.Text = "Main menu";
            // 
            // barDownLoad
            // 
            this.barDownLoad.Caption = "Down&Load";
            this.barDownLoad.Glyph = global::ICP.FRM.UI.Properties.Resources.Close_16;
            this.barDownLoad.Id = 0;
            this.barDownLoad.Name = "barDownLoad";
            this.barDownLoad.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDownLoad_ItemClick);
            // 
            // standaloneBarDockControl1
            // 
            this.standaloneBarDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(0, 0);
            this.standaloneBarDockControl1.Name = "standaloneBarDockControl1";
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(400, 26);
            this.standaloneBarDockControl1.Text = "standaloneBarDockControl1";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(400, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 330);
            this.barDockControlBottom.Size = new System.Drawing.Size(400, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 330);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(400, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 330);
            // 
            // panelFile
            // 
            this.panelFile.Controls.Add(this.gridControlFileList);
            this.panelFile.Controls.Add(this.standaloneBarDockControl1);
            this.panelFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFile.Location = new System.Drawing.Point(0, 0);
            this.panelFile.Name = "panelFile";
            this.panelFile.Size = new System.Drawing.Size(400, 330);
            this.panelFile.TabIndex = 10;
            // 
            // SearchOceanRemark
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelFile);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "SearchOceanRemark";
            this.IsMultiLanguage = false;
            this.Size = new System.Drawing.Size(400, 330);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlFileList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFileList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rHyperLinkEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.panelFile.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtRemark;
        private System.Windows.Forms.BindingSource bsList;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gridControlFileList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvFileList;
        private DevExpress.XtraGrid.Columns.GridColumn colFileName;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barDownLoad;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.Panel panelFile;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl1;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit rHyperLinkEdit1;
    }
}
