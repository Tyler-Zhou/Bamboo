namespace ICP.Test
{
    partial class TestList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestList));
            this.pnlLeft = new DevExpress.XtraEditors.PanelControl();
            this.treeList1 = new ICP.Framework.ClientComponents.Controls.LWTreeGridControl();
            this.colName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.gbError = new System.Windows.Forms.GroupBox();
            this.txtError = new DevExpress.XtraEditors.MemoEdit();
            this.splitterControl2 = new DevExpress.XtraEditors.SplitterControl();
            this.pnlTopMain = new System.Windows.Forms.Panel();
            this.splitterControl3 = new DevExpress.XtraEditors.SplitterControl();
            this.gbMessage = new System.Windows.Forms.GroupBox();
            this.txtMessagge = new DevExpress.XtraEditors.MemoEdit();
            this.gbWarning = new System.Windows.Forms.GroupBox();
            this.txtWarning = new DevExpress.XtraEditors.MemoEdit();
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.btnStartTest = new DevExpress.XtraEditors.SimpleButton();
            this.txtSecond = new DevExpress.XtraEditors.TextEdit();
            this.checkDate = new DevExpress.XtraEditors.CheckEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.btnStop = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft)).BeginInit();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.gbError.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtError.Properties)).BeginInit();
            this.pnlTopMain.SuspendLayout();
            this.gbMessage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMessagge.Properties)).BeginInit();
            this.gbWarning.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtWarning.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSecond.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.treeList1);
            this.pnlLeft.Controls.Add(this.barDockControlLeft);
            this.pnlLeft.Controls.Add(this.barDockControlRight);
            this.pnlLeft.Controls.Add(this.barDockControlBottom);
            this.pnlLeft.Controls.Add(this.barDockControlTop);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(275, 670);
            this.pnlLeft.TabIndex = 0;
            // 
            // treeList1
            // 
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colName});
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(2, 28);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsView.ShowCheckBoxes = true;
            this.treeList1.Size = new System.Drawing.Size(271, 640);
            this.treeList1.TabIndex = 0;
            this.treeList1.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeList1_AfterCheckNode);
            // 
            // colName
            // 
            this.colName.Caption = "方法名";
            this.colName.FieldName = "MethodDescription";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            this.colName.Width = 91;
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(2, 28);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 640);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(273, 28);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 640);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(2, 668);
            this.barDockControlBottom.Size = new System.Drawing.Size(271, 0);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(2, 2);
            this.barDockControlTop.Size = new System.Drawing.Size(271, 26);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(275, 0);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(6, 670);
            this.splitterControl1.TabIndex = 1;
            this.splitterControl1.TabStop = false;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.gbError);
            this.pnlMain.Controls.Add(this.splitterControl2);
            this.pnlMain.Controls.Add(this.pnlTopMain);
            this.pnlMain.Controls.Add(this.pnlTop);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(281, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(625, 670);
            this.pnlMain.TabIndex = 2;
            // 
            // gbError
            // 
            this.gbError.Controls.Add(this.txtError);
            this.gbError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbError.Location = new System.Drawing.Point(2, 361);
            this.gbError.Name = "gbError";
            this.gbError.Size = new System.Drawing.Size(621, 307);
            this.gbError.TabIndex = 12;
            this.gbError.TabStop = false;
            this.gbError.Text = "错误信息";
            // 
            // txtError
            // 
            this.txtError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtError.Location = new System.Drawing.Point(3, 18);
            this.txtError.Name = "txtError";
            this.txtError.Size = new System.Drawing.Size(615, 286);
            this.txtError.TabIndex = 6;
            // 
            // splitterControl2
            // 
            this.splitterControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterControl2.Location = new System.Drawing.Point(2, 355);
            this.splitterControl2.Name = "splitterControl2";
            this.splitterControl2.Size = new System.Drawing.Size(621, 6);
            this.splitterControl2.TabIndex = 5;
            this.splitterControl2.TabStop = false;
            // 
            // pnlTopMain
            // 
            this.pnlTopMain.Controls.Add(this.splitterControl3);
            this.pnlTopMain.Controls.Add(this.gbMessage);
            this.pnlTopMain.Controls.Add(this.gbWarning);
            this.pnlTopMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopMain.Location = new System.Drawing.Point(2, 31);
            this.pnlTopMain.Name = "pnlTopMain";
            this.pnlTopMain.Size = new System.Drawing.Size(621, 324);
            this.pnlTopMain.TabIndex = 15;
            // 
            // splitterControl3
            // 
            this.splitterControl3.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitterControl3.Location = new System.Drawing.Point(197, 0);
            this.splitterControl3.Name = "splitterControl3";
            this.splitterControl3.Size = new System.Drawing.Size(6, 324);
            this.splitterControl3.TabIndex = 15;
            this.splitterControl3.TabStop = false;
            // 
            // gbMessage
            // 
            this.gbMessage.Controls.Add(this.txtMessagge);
            this.gbMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMessage.Location = new System.Drawing.Point(0, 0);
            this.gbMessage.Name = "gbMessage";
            this.gbMessage.Size = new System.Drawing.Size(203, 324);
            this.gbMessage.TabIndex = 14;
            this.gbMessage.TabStop = false;
            this.gbMessage.Text = "执行信息";
            // 
            // txtMessagge
            // 
            this.txtMessagge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessagge.Location = new System.Drawing.Point(3, 18);
            this.txtMessagge.Name = "txtMessagge";
            this.txtMessagge.Size = new System.Drawing.Size(197, 303);
            this.txtMessagge.TabIndex = 4;
            // 
            // gbWarning
            // 
            this.gbWarning.Controls.Add(this.txtWarning);
            this.gbWarning.Dock = System.Windows.Forms.DockStyle.Right;
            this.gbWarning.Location = new System.Drawing.Point(203, 0);
            this.gbWarning.Name = "gbWarning";
            this.gbWarning.Size = new System.Drawing.Size(418, 324);
            this.gbWarning.TabIndex = 13;
            this.gbWarning.TabStop = false;
            this.gbWarning.Text = "预警信息";
            // 
            // txtWarning
            // 
            this.txtWarning.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWarning.Location = new System.Drawing.Point(3, 18);
            this.txtWarning.Name = "txtWarning";
            this.txtWarning.Size = new System.Drawing.Size(412, 303);
            this.txtWarning.TabIndex = 7;
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.btnStop);
            this.pnlTop.Controls.Add(this.btnStartTest);
            this.pnlTop.Controls.Add(this.txtSecond);
            this.pnlTop.Controls.Add(this.checkDate);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(2, 2);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(621, 29);
            this.pnlTop.TabIndex = 11;
            // 
            // btnStartTest
            // 
            this.btnStartTest.Location = new System.Drawing.Point(218, 3);
            this.btnStartTest.Name = "btnStartTest";
            this.btnStartTest.Size = new System.Drawing.Size(75, 23);
            this.btnStartTest.TabIndex = 2;
            this.btnStartTest.Text = "开始测试";
            this.btnStartTest.Click += new System.EventHandler(this.btnStartTest_Click);
            // 
            // txtSecond
            // 
            this.txtSecond.EditValue = "5";
            this.txtSecond.Location = new System.Drawing.Point(122, 4);
            this.txtSecond.Name = "txtSecond";
            this.txtSecond.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtSecond.Size = new System.Drawing.Size(25, 21);
            this.txtSecond.TabIndex = 0;
            // 
            // checkDate
            // 
            this.checkDate.EditValue = true;
            this.checkDate.Location = new System.Drawing.Point(5, 5);
            this.checkDate.Name = "checkDate";
            this.checkDate.Properties.Caption = "预警执行时间超过        秒的方法";
            this.checkDate.Size = new System.Drawing.Size(207, 19);
            this.checkDate.TabIndex = 1;
            this.checkDate.CheckedChanged += new System.EventHandler(this.checkDate_CheckedChanged);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this.pnlLeft;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barRefresh,
            this.barDelete,
            this.barAdd});
            this.barManager1.MaxItemId = 3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAdd, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.Text = "Tools";
            // 
            // barRefresh
            // 
            this.barRefresh.Caption = "刷新";
            this.barRefresh.Glyph = ((System.Drawing.Image)(resources.GetObject("barRefresh.Glyph")));
            this.barRefresh.Id = 0;
            this.barRefresh.Name = "barRefresh";
            this.barRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRefresh_ItemClick);
            // 
            // barAdd
            // 
            this.barAdd.Caption = "新增";
            this.barAdd.Glyph = ((System.Drawing.Image)(resources.GetObject("barAdd.Glyph")));
            this.barAdd.Id = 2;
            this.barAdd.Name = "barAdd";
            this.barAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdd_ItemClick);
            // 
            // barDelete
            // 
            this.barDelete.Caption = "删除";
            this.barDelete.Glyph = ((System.Drawing.Image)(resources.GetObject("barDelete.Glyph")));
            this.barDelete.Id = 1;
            this.barDelete.Name = "barDelete";
            this.barDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(308, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "停止";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // TestList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.pnlLeft);
            this.Name = "TestList";
            this.Size = new System.Drawing.Size(906, 670);
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft)).EndInit();
            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.gbError.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtError.Properties)).EndInit();
            this.pnlTopMain.ResumeLayout(false);
            this.gbMessage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtMessagge.Properties)).EndInit();
            this.gbWarning.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtWarning.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSecond.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlLeft;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private ICP.Framework.ClientComponents.Controls.LWTreeGridControl treeList1;
        private DevExpress.XtraEditors.MemoEdit txtError;
        private DevExpress.XtraEditors.SplitterControl splitterControl2;
        private DevExpress.XtraEditors.MemoEdit txtMessagge;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colName;
        private DevExpress.XtraEditors.PanelControl pnlTop;
        private DevExpress.XtraEditors.TextEdit txtSecond;
        private DevExpress.XtraEditors.CheckEdit checkDate;
        private DevExpress.XtraEditors.SimpleButton btnStartTest;
        private System.Windows.Forms.GroupBox gbError;
        private System.Windows.Forms.GroupBox gbWarning;
        private DevExpress.XtraEditors.MemoEdit txtWarning;
        private System.Windows.Forms.GroupBox gbMessage;
        private System.Windows.Forms.Panel pnlTopMain;
        private DevExpress.XtraEditors.SplitterControl splitterControl3;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barRefresh;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private DevExpress.XtraBars.BarButtonItem barAdd;
        private DevExpress.XtraEditors.SimpleButton btnStop;
    }
}
