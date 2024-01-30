namespace ICP.FRM.UI.OceanPrice
{
    partial class OPAttachmentPart
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
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barPublish = new DevExpress.XtraBars.BarButtonItem();
            this.barUpLoad = new DevExpress.XtraBars.BarButtonItem();
            this.barEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barDownLoad = new DevExpress.XtraBars.BarButtonItem();
            this.barRemove = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gridControlFileList = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvFileList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colFileName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlFileList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFileList)).BeginInit();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FRM.ServiceInterface.DataObjects.OceanFileList);
            this.bsList.PositionChanged += new System.EventHandler(this.bsFileList_PositionChanged);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barUpLoad,
            this.barRemove,
            this.barEdit,
            this.barDownLoad,
            this.barPublish});
            this.barManager1.MaxItemId = 6;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPublish, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barUpLoad, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDownLoad, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRemove, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // barPublish
            // 
            this.barPublish.Caption = "&Publish";
            this.barPublish.Glyph = global::ICP.FRM.UI.Properties.Resources.Assign_16;
            this.barPublish.Id = 5;
            this.barPublish.Name = "barPublish";
            this.barPublish.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPublish_ItemClick);
            // 
            // barUpLoad
            // 
            this.barUpLoad.Caption = "&UpLoad";
            this.barUpLoad.Glyph = global::ICP.FRM.UI.Properties.Resources.UpLoad_16;
            this.barUpLoad.Id = 0;
            this.barUpLoad.Name = "barUpLoad";
            this.barUpLoad.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barUpLoad_ItemClick);
            // 
            // barEdit
            // 
            this.barEdit.Caption = "&Edit";
            this.barEdit.Enabled = false;
            this.barEdit.Glyph = global::ICP.FRM.UI.Properties.Resources.Edit_16;
            this.barEdit.Id = 3;
            this.barEdit.Name = "barEdit";
            this.barEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barEdit_ItemClick);
            // 
            // barDownLoad
            // 
            this.barDownLoad.Caption = "Down&Load";
            this.barDownLoad.Enabled = false;
            this.barDownLoad.Glyph = global::ICP.FRM.UI.Properties.Resources.Close_16;
            this.barDownLoad.Id = 4;
            this.barDownLoad.Name = "barDownLoad";
            this.barDownLoad.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDownLoad_ItemClick);
            // 
            // barRemove
            // 
            this.barRemove.Caption = "&Remove";
            this.barRemove.Enabled = false;
            this.barRemove.Glyph = global::ICP.FRM.UI.Properties.Resources.Delete_16;
            this.barRemove.Id = 2;
            this.barRemove.Name = "barRemove";
            this.barRemove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(674, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 489);
            this.barDockControlBottom.Size = new System.Drawing.Size(674, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 463);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(674, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 463);
            // 
            // gridControlFileList
            // 
            this.gridControlFileList.DataSource = this.bsList;
            this.gridControlFileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlFileList.Location = new System.Drawing.Point(0, 26);
            this.gridControlFileList.MainView = this.gvFileList;
            this.gridControlFileList.MenuManager = this.barManager1;
            this.gridControlFileList.Name = "gridControlFileList";
            this.gridControlFileList.Size = new System.Drawing.Size(674, 463);
            this.gridControlFileList.TabIndex = 4;
            this.gridControlFileList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvFileList});
            this.gridControlFileList.DoubleClick += new System.EventHandler(this.gridControlFileList_DoubleClick);
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
            // 
            // colFileName
            // 
            this.colFileName.Caption = "FileName";
            this.colFileName.FieldName = "FileName";
            this.colFileName.Name = "colFileName";
            this.colFileName.Visible = true;
            this.colFileName.VisibleIndex = 0;
            // 
            // colRemark
            // 
            this.colRemark.Caption = "Description";
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 1;
            // 
            // colCreateByName
            // 
            this.colCreateByName.Caption = "CreateBy";
            this.colCreateByName.FieldName = "CreateByName";
            this.colCreateByName.Name = "colCreateByName";
            this.colCreateByName.Visible = true;
            this.colCreateByName.VisibleIndex = 2;
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "CreateDate";
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 3;
            // 
            // OPAttachmentPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlFileList);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IsMultiLanguage = false;
            this.Name = "OPAttachmentPart";
            this.Size = new System.Drawing.Size(674, 489);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlFileList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFileList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gridControlFileList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvFileList;
        private DevExpress.XtraGrid.Columns.GridColumn colFileName;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraBars.BarButtonItem barUpLoad;
        private DevExpress.XtraBars.BarButtonItem barRemove;
        private DevExpress.XtraBars.BarButtonItem barEdit;
        private DevExpress.XtraBars.BarButtonItem barDownLoad;
        private DevExpress.XtraBars.BarButtonItem barPublish;
    }
}
