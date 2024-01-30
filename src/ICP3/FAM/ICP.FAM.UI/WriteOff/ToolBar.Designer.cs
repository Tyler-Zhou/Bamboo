namespace ICP.FAM.UI.WriteOff
{
    partial class ToolBar
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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.bbiAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barVoid = new DevExpress.XtraBars.BarButtonItem();
            this.bbiMultiSelectionView = new DevExpress.XtraBars.BarCheckItem();
            this.bbiBullion = new DevExpress.XtraBars.BarButtonItem();
            this.bbiCancelBullion = new DevExpress.XtraBars.BarButtonItem();
            this.bbiAudit = new DevExpress.XtraBars.BarButtonItem();
            this.bbiCancelAudit = new DevExpress.XtraBars.BarButtonItem();
            this.barDirectBank = new DevExpress.XtraBars.BarButtonItem();
            this.barUntieLock = new DevExpress.XtraBars.BarButtonItem();
            this.bbiListCredentials = new DevExpress.XtraBars.BarButtonItem();
            this.barPrint = new DevExpress.XtraBars.BarButtonItem();
            this.btnCredentialsPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barAllCheck = new DevExpress.XtraBars.BarButtonItem();
            this.barShoeSearch = new DevExpress.XtraBars.BarButtonItem();
            this.btnClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem11 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
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
            this.barDelete,
            this.bbiBullion,
            this.barEdit,
            this.bbiListCredentials,
            this.barButtonItem6,
            this.btnClose,
            this.barSubItem1,
            this.bbiAdd,
            this.barButtonItem11,
            this.barButtonItem5,
            this.bbiCancelBullion,
            this.bbiAudit,
            this.bbiCancelAudit,
            this.bbiMultiSelectionView,
            this.barShoeSearch,
            this.barPrint,
            this.barAllCheck,
            this.barVoid,
            this.btnCredentialsPrint,
            this.barUntieLock,
            this.barDirectBank});
            this.barManager1.MaxItemId = 32;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDelete),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barVoid, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiMultiSelectionView, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiBullion, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiCancelBullion),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiAudit, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiCancelAudit),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDirectBank, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barUntieLock, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiListCredentials),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnCredentialsPrint, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAllCheck, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barShoeSearch, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnClose, true)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "新增";
            this.barSubItem1.Glyph = global::ICP.FAM.UI.Properties.Resources.Add_File_16;
            this.barSubItem1.Id = 9;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem5)});
            this.barSubItem1.Name = "barSubItem1";
            this.barSubItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // bbiAdd
            // 
            this.bbiAdd.Caption = "付款销账";
            this.bbiAdd.Id = 10;
            this.bbiAdd.Name = "bbiAdd";
            this.bbiAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiAdd_ItemClick);
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "收款销账";
            this.barButtonItem5.Id = 14;
            this.barButtonItem5.Name = "barButtonItem5";
            this.barButtonItem5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiAddDR_ItemClick);
            // 
            // barEdit
            // 
            this.barEdit.Caption = "编辑(&E)";
            this.barEdit.Glyph = global::ICP.FAM.UI.Properties.Resources.Edit_16;
            this.barEdit.Id = 3;
            this.barEdit.Name = "barEdit";
            this.barEdit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barEdit_ItemClick);
            // 
            // barDelete
            // 
            this.barDelete.Caption = "删除(&D)";
            this.barDelete.Glyph = global::ICP.FAM.UI.Properties.Resources.Delete_16;
            this.barDelete.Id = 1;
            this.barDelete.Name = "barDelete";
            this.barDelete.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // barVoid
            // 
            this.barVoid.Caption = "Void";
            this.barVoid.Glyph = global::ICP.FAM.UI.Properties.Resources.Empty;
            this.barVoid.Id = 27;
            this.barVoid.Name = "barVoid";
            this.barVoid.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barVoid_ItemClick);
            // 
            // bbiMultiSelectionView
            // 
            this.bbiMultiSelectionView.Caption = "多选视图(&M)";
            this.bbiMultiSelectionView.Glyph = global::ICP.FAM.UI.Properties.Resources.MultiCheck;
            this.bbiMultiSelectionView.Id = 21;
            this.bbiMultiSelectionView.Name = "bbiMultiSelectionView";
            this.bbiMultiSelectionView.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbiMultiSelectionView.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiAllowMultiSelect_ItemClick);
            // 
            // bbiBullion
            // 
            this.bbiBullion.Caption = "到账(&H)";
            this.bbiBullion.Glyph = global::ICP.FAM.UI.Properties.Resources.DollarSign;
            this.bbiBullion.Id = 2;
            this.bbiBullion.Name = "bbiBullion";
            this.bbiBullion.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbiBullion.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiBullion_ItemClick);
            // 
            // bbiCancelBullion
            // 
            this.bbiCancelBullion.Caption = "取消到账(&B)";
            this.bbiCancelBullion.Glyph = global::ICP.FAM.UI.Properties.Resources.CancelBullion;
            this.bbiCancelBullion.Id = 17;
            this.bbiCancelBullion.Name = "bbiCancelBullion";
            this.bbiCancelBullion.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbiCancelBullion.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiCancelBullion_ItemClick);
            // 
            // bbiAudit
            // 
            this.bbiAudit.Caption = "审核(&A)";
            this.bbiAudit.Glyph = global::ICP.FAM.UI.Properties.Resources.Review;
            this.bbiAudit.Id = 18;
            this.bbiAudit.Name = "bbiAudit";
            this.bbiAudit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbiAudit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiAudit_ItemClick);
            // 
            // bbiCancelAudit
            // 
            this.bbiCancelAudit.Caption = "取消审核(&C)";
            this.bbiCancelAudit.Glyph = global::ICP.FAM.UI.Properties.Resources.CancelBullion;
            this.bbiCancelAudit.Id = 19;
            this.bbiCancelAudit.Name = "bbiCancelAudit";
            this.bbiCancelAudit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbiCancelAudit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiCancelAudit_ItemClick);
            // 
            // barDirectBank
            // 
            this.barDirectBank.Caption = "直连支付";
            this.barDirectBank.Glyph = global::ICP.FAM.UI.Properties.Resources.Transfer_16;
            this.barDirectBank.Id = 31;
            this.barDirectBank.Name = "barDirectBank";
            this.barDirectBank.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDirectBank_ItemClick);
            // 
            // barUntieLock
            // 
            this.barUntieLock.Caption = "解锁";
            this.barUntieLock.Glyph = global::ICP.FAM.UI.Properties.Resources.TelexPassword;
            this.barUntieLock.Id = 29;
            this.barUntieLock.Name = "barUntieLock";
            this.barUntieLock.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barUntieLock_ItemClick);
            // 
            // bbiListCredentials
            // 
            this.bbiListCredentials.Caption = "凭证明细(&V)";
            this.bbiListCredentials.Glyph = global::ICP.FAM.UI.Properties.Resources.BillDetails;
            this.bbiListCredentials.Id = 4;
            this.bbiListCredentials.Name = "bbiListCredentials";
            this.bbiListCredentials.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbiListCredentials.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiListCredentials_ItemClick);
            // 
            // barPrint
            // 
            this.barPrint.Caption = "打印(&P)";
            this.barPrint.Glyph = global::ICP.FAM.UI.Properties.Resources.Print_16;
            this.barPrint.Id = 25;
            this.barPrint.Name = "barPrint";
            this.barPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrint_ItemClick);
            // 
            // btnCredentialsPrint
            // 
            this.btnCredentialsPrint.Caption = "打印凭证";
            this.btnCredentialsPrint.Glyph = global::ICP.FAM.UI.Properties.Resources.Print_16;
            this.btnCredentialsPrint.Id = 28;
            this.btnCredentialsPrint.Name = "btnCredentialsPrint";
            this.btnCredentialsPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCredentialsPrint_ItemClick);
            // 
            // barAllCheck
            // 
            this.barAllCheck.Caption = "全选(&A)";
            this.barAllCheck.Glyph = global::ICP.FAM.UI.Properties.Resources.Check_16;
            this.barAllCheck.Id = 26;
            this.barAllCheck.Name = "barAllCheck";
            toolTipTitleItem1.Text = "选中所有跟当前行数据类型、是否到账、是否审核的相同数据";
            superToolTip1.Items.Add(toolTipTitleItem1);
            this.barAllCheck.SuperTip = superToolTip1;
            this.barAllCheck.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAllCheck_ItemClick);
            // 
            // barShoeSearch
            // 
            this.barShoeSearch.Glyph = global::ICP.FAM.UI.Properties.Resources.Sarch_16;
            this.barShoeSearch.Id = 23;
            this.barShoeSearch.Name = "barShoeSearch";
            toolTipTitleItem2.Text = "Search";
            superToolTip2.Items.Add(toolTipTitleItem2);
            this.barShoeSearch.SuperTip = superToolTip2;
            this.barShoeSearch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barShoeSearch_ItemClick);
            // 
            // btnClose
            // 
            this.btnClose.Caption = "关闭(&C)";
            this.btnClose.Glyph = global::ICP.FAM.UI.Properties.Resources.Close_16;
            this.btnClose.Id = 6;
            this.btnClose.Name = "btnClose";
            this.btnClose.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1260, 60);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 158);
            this.barDockControlBottom.Size = new System.Drawing.Size(1260, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 60);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 98);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1260, 60);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 98);
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "预览";
            this.barButtonItem6.Id = 5;
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // barButtonItem11
            // 
            this.barButtonItem11.Caption = "付款单";
            this.barButtonItem11.Id = 11;
            this.barButtonItem11.Name = "barButtonItem11";
            // 
            // ToolBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ToolBar";
            this.Size = new System.Drawing.Size(1260, 158);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private DevExpress.XtraBars.BarButtonItem bbiBullion;
        private DevExpress.XtraBars.BarButtonItem barEdit;
        private DevExpress.XtraBars.BarButtonItem bbiListCredentials;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.BarButtonItem btnClose;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem bbiAdd;
        private DevExpress.XtraBars.BarButtonItem barButtonItem11;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarButtonItem bbiCancelBullion;
        private DevExpress.XtraBars.BarButtonItem bbiAudit;
        private DevExpress.XtraBars.BarButtonItem bbiCancelAudit;
        public DevExpress.XtraBars.BarCheckItem bbiMultiSelectionView;
        private DevExpress.XtraBars.BarButtonItem barShoeSearch;
        private DevExpress.XtraBars.BarButtonItem barPrint;
        private DevExpress.XtraBars.BarButtonItem barAllCheck;
        private DevExpress.XtraBars.BarButtonItem barVoid;
        private DevExpress.XtraBars.BarButtonItem btnCredentialsPrint;
        private DevExpress.XtraBars.BarButtonItem barUntieLock;
        private DevExpress.XtraBars.BarButtonItem barDirectBank;
    }
}
