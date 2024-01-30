namespace ICP.WF.UI
{
    partial class WorkListToolBarPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkListToolBarPart));
            this.barFormDesignToolBarManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barApplication = new DevExpress.XtraBars.BarSubItem();
            this.barDoTask = new DevExpress.XtraBars.BarButtonItem();
            this.barMultiFinish = new DevExpress.XtraBars.BarButtonItem();
            this.barCancel = new DevExpress.XtraBars.BarButtonItem();
            this.barAuditor = new DevExpress.XtraBars.BarButtonItem();
            this.barAuditorMerger = new DevExpress.XtraBars.BarButtonItem();
            this.barUnAudiror = new DevExpress.XtraBars.BarButtonItem();
            this.barPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barPrintReport = new DevExpress.XtraBars.BarButtonItem();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.barFormDesignToolBarManager)).BeginInit();
            this.SuspendLayout();
            // 
            // barFormDesignToolBarManager
            // 
            this.barFormDesignToolBarManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.barFormDesignToolBarManager.DockControls.Add(this.barDockControlTop);
            this.barFormDesignToolBarManager.DockControls.Add(this.barDockControlBottom);
            this.barFormDesignToolBarManager.DockControls.Add(this.barDockControlLeft);
            this.barFormDesignToolBarManager.DockControls.Add(this.barDockControlRight);
            this.barFormDesignToolBarManager.Form = this;
            this.barFormDesignToolBarManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barDoTask,
            this.barClose,
            this.barRefresh,
            this.barApplication,
            this.barPrint,
            this.barCancel,
            this.barPrintReport,
            this.barAuditor,
            this.barUnAudiror,
            this.barAuditorMerger,
            this.barMultiFinish,
            this.barDelete});
            this.barFormDesignToolBarManager.MainMenu = this.bar3;
            this.barFormDesignToolBarManager.MaxItemId = 48;
            // 
            // bar3
            // 
            this.bar3.BarName = "Main menu";
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barApplication, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDoTask, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barMultiFinish, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barCancel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAuditor, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAuditorMerger, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barUnAudiror, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrintReport, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefresh, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar3.OptionsBar.MultiLine = true;
            this.bar3.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.bar3, "bar3");
            // 
            // barApplication
            // 
            resources.ApplyResources(this.barApplication, "barApplication");
            this.barApplication.Glyph = global::ICP.WF.UI.Properties.Resources.工作流;
            this.barApplication.Id = 31;
            this.barApplication.Name = "barApplication";
            // 
            // barDoTask
            // 
            resources.ApplyResources(this.barDoTask, "barDoTask");
            this.barDoTask.Glyph = global::ICP.WF.UI.Properties.Resources.编辑;
            this.barDoTask.Id = 0;
            this.barDoTask.Name = "barDoTask";
            this.barDoTask.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDoTask_ItemClick);
            // 
            // barMultiFinish
            // 
            resources.ApplyResources(this.barMultiFinish, "barMultiFinish");
            this.barMultiFinish.Glyph = global::ICP.WF.UI.Properties.Resources.签收1;
            this.barMultiFinish.Id = 46;
            this.barMultiFinish.Name = "barMultiFinish";
            this.barMultiFinish.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barMultiFinish_ItemClick);
            // 
            // barCancel
            // 
            resources.ApplyResources(this.barCancel, "barCancel");
            this.barCancel.Glyph = global::ICP.WF.UI.Properties.Resources.取消;
            this.barCancel.Id = 40;
            this.barCancel.Name = "barCancel";
            this.barCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCancel_ItemClick);
            // 
            // barAuditor
            // 
            resources.ApplyResources(this.barAuditor, "barAuditor");
            this.barAuditor.Glyph = global::ICP.WF.UI.Properties.Resources.Review;
            this.barAuditor.Id = 43;
            this.barAuditor.Name = "barAuditor";
            this.barAuditor.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAuditor_ItemClick);
            // 
            // barAuditorMerger
            // 
            resources.ApplyResources(this.barAuditorMerger, "barAuditorMerger");
            this.barAuditorMerger.Glyph = global::ICP.WF.UI.Properties.Resources.Review;
            this.barAuditorMerger.Id = 45;
            this.barAuditorMerger.Name = "barAuditorMerger";
            this.barAuditorMerger.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAuditorMerger_ItemClick);
            // 
            // barUnAudiror
            // 
            resources.ApplyResources(this.barUnAudiror, "barUnAudiror");
            this.barUnAudiror.Glyph = global::ICP.WF.UI.Properties.Resources.CancelBullion;
            this.barUnAudiror.Id = 44;
            this.barUnAudiror.Name = "barUnAudiror";
            this.barUnAudiror.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barUnAudiror_ItemClick);
            // 
            // barPrint
            // 
            resources.ApplyResources(this.barPrint, "barPrint");
            this.barPrint.Glyph = global::ICP.WF.UI.Properties.Resources.打印;
            this.barPrint.Id = 39;
            this.barPrint.Name = "barPrint";
            this.barPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrint_ItemClick);
            // 
            // barPrintReport
            // 
            resources.ApplyResources(this.barPrintReport, "barPrintReport");
            this.barPrintReport.Glyph = global::ICP.WF.UI.Properties.Resources.打印;
            this.barPrintReport.Id = 42;
            this.barPrintReport.Name = "barPrintReport";
            this.barPrintReport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrintReport_ItemClick);
            // 
            // barRefresh
            // 
            resources.ApplyResources(this.barRefresh, "barRefresh");
            this.barRefresh.Glyph = global::ICP.WF.UI.Properties.Resources.刷新_;
            this.barRefresh.Id = 29;
            this.barRefresh.Name = "barRefresh";
            this.barRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRefresh_ItemClick);
            // 
            // barClose
            // 
            resources.ApplyResources(this.barClose, "barClose");
            this.barClose.Glyph = global::ICP.WF.UI.Properties.Resources.Close;
            this.barClose.Id = 28;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barDockControlTop
            // 
            resources.ApplyResources(this.barDockControlTop, "barDockControlTop");
            // 
            // barDockControlBottom
            // 
            resources.ApplyResources(this.barDockControlBottom, "barDockControlBottom");
            // 
            // barDockControlLeft
            // 
            resources.ApplyResources(this.barDockControlLeft, "barDockControlLeft");
            // 
            // barDockControlRight
            // 
            resources.ApplyResources(this.barDockControlRight, "barDockControlRight");
            // 
            // barDelete
            // 
            resources.ApplyResources(this.barDelete, "barDelete");
            this.barDelete.Glyph = global::ICP.WF.UI.Properties.Resources.Delete_16;
            this.barDelete.Id = 47;
            this.barDelete.Name = "barDelete";
            this.barDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // WorkListToolBarPart
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "WorkListToolBarPart";
            ((System.ComponentModel.ISupportInitialize)(this.barFormDesignToolBarManager)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarManager barFormDesignToolBarManager;

        private DevExpress.XtraBars.BarButtonItem barDoTask;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private DevExpress.XtraBars.BarSubItem barApplication;
        private DevExpress.XtraBars.BarButtonItem barCancel;
        private DevExpress.XtraBars.BarButtonItem barPrint;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barPrintReport;
        private DevExpress.XtraBars.BarButtonItem barAuditor;
        private DevExpress.XtraBars.BarButtonItem barUnAudiror;
        private DevExpress.XtraBars.BarButtonItem barAuditorMerger;
        private DevExpress.XtraBars.BarButtonItem barMultiFinish;
        private DevExpress.XtraBars.BarButtonItem barDelete;

    }
}
