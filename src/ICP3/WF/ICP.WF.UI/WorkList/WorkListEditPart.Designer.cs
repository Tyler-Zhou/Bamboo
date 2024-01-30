namespace ICP.WF.UI
{
    partial class WorkListEditPart
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkListEditPart));
            this.NCMain = new DevExpress.XtraNavBar.NavBarControl();
            this.bgMain = new DevExpress.XtraNavBar.NavBarGroup();
            this.bgcMain = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.bgcForm = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.bgForm = new DevExpress.XtraNavBar.NavBarGroup();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barFinish = new DevExpress.XtraBars.BarButtonItem();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.pnlScroll = new DevExpress.XtraEditors.XtraScrollableControl();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bar3 = new DevExpress.XtraBars.Bar();
            ((System.ComponentModel.ISupportInitialize)(this.NCMain)).BeginInit();
            this.NCMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.pnlScroll.SuspendLayout();
            this.SuspendLayout();
            // 
            // NCMain
            // 
            this.NCMain.ActiveGroup = this.bgMain;
            this.NCMain.Controls.Add(this.bgcMain);
            this.NCMain.Controls.Add(this.bgcForm);
            resources.ApplyResources(this.NCMain, "NCMain");
            this.NCMain.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.bgMain,
            this.bgForm});
            this.NCMain.Name = "NCMain";
            this.NCMain.OptionsNavPane.ExpandedWidth = 461;
            this.NCMain.SkinExplorerBarViewScrollStyle = DevExpress.XtraNavBar.SkinExplorerBarViewScrollStyle.ScrollBar;
            // 
            // bgMain
            // 
            resources.ApplyResources(this.bgMain, "bgMain");
            this.bgMain.ControlContainer = this.bgcMain;
            this.bgMain.Expanded = true;
            this.bgMain.GroupClientHeight = 89;
            this.bgMain.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.bgMain.Name = "bgMain";
            // 
            // bgcMain
            // 
            this.bgcMain.Name = "bgcMain";
            resources.ApplyResources(this.bgcMain, "bgcMain");
            // 
            // bgcForm
            // 
            this.bgcForm.Name = "bgcForm";
            resources.ApplyResources(this.bgcForm, "bgcForm");
            // 
            // bgForm
            // 
            resources.ApplyResources(this.bgForm, "bgForm");
            this.bgForm.ControlContainer = this.bgcForm;
            this.bgForm.Expanded = true;
            this.bgForm.GroupClientHeight = 500;
            this.bgForm.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.bgForm.Name = "bgForm";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControl1);
            this.barManager1.DockControls.Add(this.barDockControl2);
            this.barManager1.DockControls.Add(this.barDockControl3);
            this.barManager1.DockControls.Add(this.barDockControl4);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSave,
            this.barFinish,
            this.barPrint,
            this.barClose});
            this.barManager1.MaxItemId = 4;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barFinish, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrint, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.bar1, "bar1");
            // 
            // barFinish
            // 
            resources.ApplyResources(this.barFinish, "barFinish");
            this.barFinish.Glyph = global::ICP.WF.UI.Properties.Resources.签收1;
            this.barFinish.Id = 1;
            this.barFinish.Name = "barFinish";
            this.barFinish.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barFinish_ItemClick);
            // 
            // barSave
            // 
            resources.ApplyResources(this.barSave, "barSave");
            this.barSave.Glyph = global::ICP.WF.UI.Properties.Resources.Save;
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barPrint
            // 
            resources.ApplyResources(this.barPrint, "barPrint");
            this.barPrint.Glyph = global::ICP.WF.UI.Properties.Resources.打印;
            this.barPrint.Id = 2;
            this.barPrint.Name = "barPrint";
            this.barPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrint_ItemClick);
            // 
            // barClose
            // 
            resources.ApplyResources(this.barClose, "barClose");
            this.barClose.Glyph = global::ICP.WF.UI.Properties.Resources.Close;
            this.barClose.Id = 3;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barDockControl1
            // 
            resources.ApplyResources(this.barDockControl1, "barDockControl1");
            // 
            // barDockControl2
            // 
            resources.ApplyResources(this.barDockControl2, "barDockControl2");
            // 
            // barDockControl3
            // 
            resources.ApplyResources(this.barDockControl3, "barDockControl3");
            // 
            // barDockControl4
            // 
            resources.ApplyResources(this.barDockControl4, "barDockControl4");
            // 
            // pnlScroll
            // 
            this.pnlScroll.Controls.Add(this.NCMain);
            resources.ApplyResources(this.pnlScroll, "pnlScroll");
            this.pnlScroll.Name = "pnlScroll";
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
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.bar2, "bar2");
            // 
            // bar3
            // 
            this.bar3.BarName = "Main menu";
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar3.OptionsBar.MultiLine = true;
            this.bar3.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.bar3, "bar3");
            // 
            // WorkListEditPart
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlScroll);
            this.Controls.Add(this.barDockControlTop);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.Name = "WorkListEditPart";
            ((System.ComponentModel.ISupportInitialize)(this.NCMain)).EndInit();
            this.NCMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.pnlScroll.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl NCMain;
        private DevExpress.XtraNavBar.NavBarGroup bgMain;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer bgcMain;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer bgcForm;
        private DevExpress.XtraNavBar.NavBarGroup bgForm;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarButtonItem barFinish;
        private DevExpress.XtraBars.BarButtonItem barPrint;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraEditors.XtraScrollableControl pnlScroll;
       
    }
}