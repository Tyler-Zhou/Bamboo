using DevExpress.XtraBars;
using System.Windows.Forms;
namespace ICP.Business.Common.UI.BL
{
    partial class UCBLTreePart
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
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barRefersh = new DevExpress.XtraBars.BarButtonItem();
            this.barAdd = new DevExpress.XtraBars.BarSubItem();
            this.barAddMBL = new DevExpress.XtraBars.BarButtonItem();
            this.barAddHBL = new DevExpress.XtraBars.BarButtonItem();
            this.barDeclarationBL = new DevExpress.XtraBars.BarButtonItem();
            this.barEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barCopy = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barbl = new DevExpress.XtraBars.BarSubItem();
            this.barchs = new DevExpress.XtraBars.BarButtonItem();
            this.bareng = new DevExpress.XtraBars.BarButtonItem();
            this.bartoAgentchs = new DevExpress.XtraBars.BarButtonItem();
            this.bartoAgenteng = new DevExpress.XtraBars.BarButtonItem();
            this.barPrint = new DevExpress.XtraBars.BarSubItem();
            this.barPrintBL = new DevExpress.XtraBars.BarButtonItem();
            this.barProfit = new DevExpress.XtraBars.BarButtonItem();
            this.barPrintLoadCtn = new DevExpress.XtraBars.BarButtonItem();
            this.barPrintLoadGoods = new DevExpress.XtraBars.BarButtonItem();
            this.barLoadContainer = new DevExpress.XtraBars.BarButtonItem();
            this.barLoadContainerCopy = new DevExpress.XtraBars.BarButtonItem();
            this.barEDI = new DevExpress.XtraBars.BarSubItem();
            this.barEMBL = new DevExpress.XtraBars.BarButtonItem();
            this.barvgm = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barAMS = new DevExpress.XtraBars.BarButtonItem();
            this.barACI = new DevExpress.XtraBars.BarButtonItem();
            this.barISF = new DevExpress.XtraBars.BarButtonItem();
            this.barAMSACI = new DevExpress.XtraBars.BarButtonItem();
            this.barAMSISF = new DevExpress.XtraBars.BarButtonItem();
            this.barAMSACIISF = new DevExpress.XtraBars.BarButtonItem();
            this.barCopyAMSTo = new DevExpress.XtraBars.BarButtonItem();
            this.barSplitAndMerge = new DevExpress.XtraBars.BarSubItem();
            this.barSplitBL = new DevExpress.XtraBars.BarButtonItem();
            this.barMergeBL = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.pnlToolPart = new DevExpress.XtraEditors.PanelControl();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
            this.pnlGridList = new DevExpress.XtraEditors.PanelControl();
            this.treeMain = new ICP.Framework.ClientComponents.Controls.LWTreeGridControl();
            this.colBLTypeName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colNo = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colBLCFM = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colMBLD = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colRBLA = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colRBLH = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colRBLD = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colRBLRcv = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colBLRC = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colAMS = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colISF = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colShipperName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colConsigneeName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colNotifyPartyName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colReleaseStateName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colReleaseTypeName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTelexNo = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this._BSMain = new System.Windows.Forms.BindingSource(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemF5 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolPart)).BeginInit();
            this.pnlToolPart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridList)).BeginInit();
            this.pnlGridList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._BSMain)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
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
            this.barManager1.Form = this.pnlToolPart;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barAdd,
            this.barAddMBL,
            this.barAddHBL,
            this.barEdit,
            this.barCopy,
            this.barDelete,
            this.barSplitAndMerge,
            this.barSplitBL,
            this.barMergeBL,
            this.barPrint,
            this.barPrintBL,
            this.barProfit,
            this.barPrintLoadCtn,
            this.barPrintLoadGoods,
            this.barRefersh,
            this.barEMBL,
            this.barSubItem1,
            this.barAMS,
            this.barACI,
            this.barISF,
            this.barButtonItem4,
            this.barAMSACI,
            this.barAMSISF,
            this.barAMSACIISF,
            this.barCopyAMSTo,
            this.barbl,
            this.barchs,
            this.bareng,
            this.barButtonItem1,
            this.bartoAgentchs,
            this.bartoAgenteng,
            this.barvgm,
            this.barLoadContainer,
            this.barLoadContainerCopy,
            this.barEDI,
            this.barDeclarationBL});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 32;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefersh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAdd, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barCopy, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSplitAndMerge, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barbl, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrint, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barEDI, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubItem1, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barCopyAMSTo, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barRefersh
            // 
            this.barRefersh.Caption = "Refersh";
            this.barRefersh.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Refresh_161;
            this.barRefersh.Id = 7;
            this.barRefersh.Name = "barRefersh";
            // 
            // barAdd
            // 
            this.barAdd.Caption = "Add";
            this.barAdd.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Add;
            this.barAdd.Id = 23;
            this.barAdd.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAddMBL, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAddHBL, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDeclarationBL)});
            this.barAdd.Name = "barAdd";
            // 
            // barAddMBL
            // 
            this.barAddMBL.Caption = "MBL";
            this.barAddMBL.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Add;
            this.barAddMBL.Id = 24;
            this.barAddMBL.Name = "barAddMBL";
            // 
            // barAddHBL
            // 
            this.barAddHBL.Caption = "HBL";
            this.barAddHBL.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Add;
            this.barAddHBL.Id = 26;
            this.barAddHBL.Name = "barAddHBL";
            // 
            // barDeclarationBL
            // 
            this.barDeclarationBL.Caption = "DBL";
            this.barDeclarationBL.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Add;
            this.barDeclarationBL.Id = 31;
            this.barDeclarationBL.Name = "barDeclarationBL";
            // 
            // barEdit
            // 
            this.barEdit.Caption = "Edit";
            this.barEdit.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Edit_16;
            this.barEdit.Id = 3;
            this.barEdit.Name = "barEdit";
            // 
            // barCopy
            // 
            this.barCopy.Caption = "Copy";
            this.barCopy.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Copy_16;
            this.barCopy.Id = 4;
            this.barCopy.Name = "barCopy";
            // 
            // barDelete
            // 
            this.barDelete.Caption = "Delete";
            this.barDelete.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Delete_16;
            this.barDelete.Id = 5;
            this.barDelete.Name = "barDelete";
            // 
            // barbl
            // 
            this.barbl.Caption = "Send Mail";
            this.barbl.Glyph = global::ICP.Business.Common.UI.Properties.Resources.airmail;
            this.barbl.Id = 20;
            this.barbl.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barchs),
            new DevExpress.XtraBars.LinkPersistInfo(this.bareng),
            new DevExpress.XtraBars.LinkPersistInfo(this.bartoAgentchs),
            new DevExpress.XtraBars.LinkPersistInfo(this.bartoAgenteng)});
            this.barbl.Name = "barbl";
            // 
            // barchs
            // 
            this.barchs.Caption = "Mail BL Copy To Customer (CHS)";
            this.barchs.Id = 21;
            this.barchs.Name = "barchs";
            // 
            // bareng
            // 
            this.bareng.Caption = "Mail BL Copy To Customer (ENG)";
            this.bareng.Id = 22;
            this.bareng.Name = "bareng";
            // 
            // bartoAgentchs
            // 
            this.bartoAgentchs.Caption = "Mail All BL Copy To Agent(CHS)";
            this.bartoAgentchs.Id = 24;
            this.bartoAgentchs.Name = "bartoAgentchs";
            // 
            // bartoAgenteng
            // 
            this.bartoAgenteng.Caption = "Mail All BL Copy To Agent(ENG)";
            this.bartoAgenteng.Id = 25;
            this.bartoAgenteng.Name = "bartoAgenteng";
            // 
            // barPrint
            // 
            this.barPrint.Caption = "Print";
            this.barPrint.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Print_16;
            this.barPrint.Id = 6;
            this.barPrint.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrintBL, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barProfit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrintLoadCtn, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barPrintLoadGoods),
            new DevExpress.XtraBars.LinkPersistInfo(this.barLoadContainer),
            new DevExpress.XtraBars.LinkPersistInfo(this.barLoadContainerCopy)});
            this.barPrint.Name = "barPrint";
            // 
            // barPrintBL
            // 
            this.barPrintBL.Caption = "&Print BL";
            this.barPrintBL.Id = 4;
            this.barPrintBL.Name = "barPrintBL";
            // 
            // barProfit
            // 
            this.barProfit.Caption = "Profit";
            this.barProfit.Id = 23;
            this.barProfit.Name = "barProfit";
            // 
            // barPrintLoadCtn
            // 
            this.barPrintLoadCtn.Caption = "Print Load Ctn";
            this.barPrintLoadCtn.Id = 29;
            this.barPrintLoadCtn.Name = "barPrintLoadCtn";
            this.barPrintLoadCtn.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barPrintLoadGoods
            // 
            this.barPrintLoadGoods.Caption = "Print LoadGoods";
            this.barPrintLoadGoods.Id = 30;
            this.barPrintLoadGoods.Name = "barPrintLoadGoods";
            // 
            // barLoadContainer
            // 
            this.barLoadContainer.Caption = "Print LoadContainer";
            this.barLoadContainer.Id = 27;
            this.barLoadContainer.Name = "barLoadContainer";
            // 
            // barLoadContainerCopy
            // 
            this.barLoadContainerCopy.Caption = "Print LoadContainer(Copy)";
            this.barLoadContainerCopy.Id = 28;
            this.barLoadContainerCopy.Name = "barLoadContainerCopy";
            // 
            // barEDI
            // 
            this.barEDI.Caption = "EDI";
            this.barEDI.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Transfer_16;
            this.barEDI.Id = 30;
            this.barEDI.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barEMBL, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barvgm)});
            this.barEDI.Name = "barEDI";
            // 
            // barEMBL
            // 
            this.barEMBL.Caption = "MBL";
            this.barEMBL.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Transfer_16;
            this.barEMBL.Id = 8;
            this.barEMBL.Name = "barEMBL";
            // 
            // barvgm
            // 
            this.barvgm.Caption = "VGM";
            this.barvgm.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Transfer_16;
            this.barvgm.Id = 26;
            this.barvgm.Name = "barvgm";
            this.barvgm.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "E-AMS/ISF";
            this.barSubItem1.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Transfer_16;
            this.barSubItem1.Id = 11;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barAMS),
            new DevExpress.XtraBars.LinkPersistInfo(this.barACI),
            new DevExpress.XtraBars.LinkPersistInfo(this.barISF),
            new DevExpress.XtraBars.LinkPersistInfo(this.barAMSACI),
            new DevExpress.XtraBars.LinkPersistInfo(this.barAMSISF),
            new DevExpress.XtraBars.LinkPersistInfo(this.barAMSACIISF)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // barAMS
            // 
            this.barAMS.Caption = "AMS";
            this.barAMS.Id = 12;
            this.barAMS.Name = "barAMS";
            // 
            // barACI
            // 
            this.barACI.Caption = "ACI";
            this.barACI.Id = 13;
            this.barACI.Name = "barACI";
            // 
            // barISF
            // 
            this.barISF.Caption = "ISF";
            this.barISF.Id = 14;
            this.barISF.Name = "barISF";
            // 
            // barAMSACI
            // 
            this.barAMSACI.Caption = "AMS&&ACI";
            this.barAMSACI.Id = 16;
            this.barAMSACI.Name = "barAMSACI";
            // 
            // barAMSISF
            // 
            this.barAMSISF.Caption = "AMS&&ISF";
            this.barAMSISF.Id = 17;
            this.barAMSISF.Name = "barAMSISF";
            // 
            // barAMSACIISF
            // 
            this.barAMSACIISF.Caption = "AMS&&ACI&&ISF";
            this.barAMSACIISF.Id = 18;
            this.barAMSACIISF.Name = "barAMSACIISF";
            // 
            // barCopyAMSTo
            // 
            this.barCopyAMSTo.Caption = "Copy  AMS  To";
            this.barCopyAMSTo.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Transfer_16;
            this.barCopyAMSTo.Id = 19;
            this.barCopyAMSTo.Name = "barCopyAMSTo";
            // 
            // barSplitAndMerge
            // 
            this.barSplitAndMerge.Caption = "Split/Merge";
            this.barSplitAndMerge.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Transfer_16;
            this.barSplitAndMerge.Id = 33;
            this.barSplitAndMerge.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSplitBL),
            new DevExpress.XtraBars.LinkPersistInfo(this.barMergeBL)});
            this.barSplitAndMerge.Name = "barSplitAndMerge";
            // 
            // barSplitBL
            // 
            this.barSplitBL.Caption = "&Split";
            this.barSplitBL.Id = 34;
            this.barSplitBL.Name = "barSplitBL";
            // 
            // barMergeBL
            // 
            this.barMergeBL.Caption = "Merge";
            this.barMergeBL.Id = 35;
            this.barMergeBL.Name = "barMergeBL";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(2, 2);
            this.barDockControlTop.Size = new System.Drawing.Size(1448, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(2, 29);
            this.barDockControlBottom.Size = new System.Drawing.Size(1448, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(2, 28);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 1);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1450, 28);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 1);
            // 
            // pnlToolPart
            // 
            this.pnlToolPart.Controls.Add(this.barDockControlLeft);
            this.pnlToolPart.Controls.Add(this.barDockControlRight);
            this.pnlToolPart.Controls.Add(this.barDockControlBottom);
            this.pnlToolPart.Controls.Add(this.barDockControlTop);
            this.pnlToolPart.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolPart.Location = new System.Drawing.Point(0, 0);
            this.pnlToolPart.Name = "pnlToolPart";
            this.pnlToolPart.Size = new System.Drawing.Size(1452, 31);
            this.pnlToolPart.TabIndex = 0;
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "AMS&ACI";
            this.barButtonItem4.Id = 15;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 23;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barSubItem2
            // 
            this.barSubItem2.Id = -1;
            this.barSubItem2.Name = "barSubItem2";
            // 
            // pnlGridList
            // 
            this.pnlGridList.Controls.Add(this.treeMain);
            this.pnlGridList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridList.Location = new System.Drawing.Point(0, 31);
            this.pnlGridList.Name = "pnlGridList";
            this.pnlGridList.Size = new System.Drawing.Size(1452, 371);
            this.pnlGridList.TabIndex = 1;
            // 
            // treeMain
            // 
            this.treeMain.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colBLTypeName,
            this.colNo,
            this.colBLCFM,
            this.colMBLD,
            this.colRBLA,
            this.colRBLH,
            this.colRBLD,
            this.colRBLRcv,
            this.colBLRC,
            this.colAMS,
            this.colISF,
            this.colShipperName,
            this.colConsigneeName,
            this.colNotifyPartyName,
            this.colReleaseStateName,
            this.colReleaseTypeName,
            this.colTelexNo});
            this.treeMain.DataSource = this._BSMain;
            this.treeMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMain.Location = new System.Drawing.Point(2, 2);
            this.treeMain.Name = "treeMain";
            this.treeMain.OptionsBehavior.Editable = false;
            this.treeMain.OptionsSelection.MultiSelect = true;
            this.treeMain.OptionsView.AutoWidth = false;
            this.treeMain.OptionsView.EnableAppearanceEvenRow = true;
            this.treeMain.RootValue = null;
            this.treeMain.Size = new System.Drawing.Size(1448, 367);
            this.treeMain.TabIndex = 2;
            // 
            // colBLTypeName
            // 
            this.colBLTypeName.FieldName = "BLTypeName";
            this.colBLTypeName.Name = "colBLTypeName";
            this.colBLTypeName.OptionsColumn.ReadOnly = true;
            this.colBLTypeName.Visible = true;
            this.colBLTypeName.VisibleIndex = 0;
            // 
            // colNo
            // 
            this.colNo.FieldName = "No";
            this.colNo.Name = "colNo";
            this.colNo.Visible = true;
            this.colNo.VisibleIndex = 1;
            // 
            // colBLCFM
            // 
            this.colBLCFM.FieldName = "BLCFM";
            this.colBLCFM.Name = "colBLCFM";
            this.colBLCFM.Visible = true;
            this.colBLCFM.VisibleIndex = 2;
            // 
            // colMBLD
            // 
            this.colMBLD.FieldName = "MBLD";
            this.colMBLD.Name = "colMBLD";
            this.colMBLD.Visible = true;
            this.colMBLD.VisibleIndex = 3;
            // 
            // colRBLA
            // 
            this.colRBLA.FieldName = "RBLA";
            this.colRBLA.Name = "colRBLA";
            this.colRBLA.Visible = true;
            this.colRBLA.VisibleIndex = 4;
            // 
            // colRBLH
            // 
            this.colRBLH.FieldName = "RBLH";
            this.colRBLH.Name = "colRBLH";
            this.colRBLH.Visible = true;
            this.colRBLH.VisibleIndex = 5;
            // 
            // colRBLD
            // 
            this.colRBLD.FieldName = "RBLD";
            this.colRBLD.Name = "colRBLD";
            this.colRBLD.Visible = true;
            this.colRBLD.VisibleIndex = 6;
            // 
            // colRBLRcv
            // 
            this.colRBLRcv.FieldName = "RBLRcv";
            this.colRBLRcv.Name = "colRBLRcv";
            this.colRBLRcv.Visible = true;
            this.colRBLRcv.VisibleIndex = 7;
            // 
            // colBLRC
            // 
            this.colBLRC.FieldName = "BLRC";
            this.colBLRC.Name = "colBLRC";
            this.colBLRC.Visible = true;
            this.colBLRC.VisibleIndex = 9;
            // 
            // colAMS
            // 
            this.colAMS.FieldName = "AMS";
            this.colAMS.Name = "colAMS";
            this.colAMS.Visible = true;
            this.colAMS.VisibleIndex = 10;
            // 
            // colISF
            // 
            this.colISF.FieldName = "ISF";
            this.colISF.Name = "colISF";
            this.colISF.Visible = true;
            this.colISF.VisibleIndex = 12;
            // 
            // colShipperName
            // 
            this.colShipperName.FieldName = "ShipperName";
            this.colShipperName.Name = "colShipperName";
            this.colShipperName.Visible = true;
            this.colShipperName.VisibleIndex = 8;
            // 
            // colConsigneeName
            // 
            this.colConsigneeName.FieldName = "ConsigneeName";
            this.colConsigneeName.Name = "colConsigneeName";
            this.colConsigneeName.Visible = true;
            this.colConsigneeName.VisibleIndex = 11;
            // 
            // colNotifyPartyName
            // 
            this.colNotifyPartyName.FieldName = "NotifyPartyName";
            this.colNotifyPartyName.Name = "colNotifyPartyName";
            this.colNotifyPartyName.Visible = true;
            this.colNotifyPartyName.VisibleIndex = 13;
            // 
            // colReleaseStateName
            // 
            this.colReleaseStateName.FieldName = "ReleaseStateName";
            this.colReleaseStateName.Name = "colReleaseStateName";
            this.colReleaseStateName.OptionsColumn.ReadOnly = true;
            this.colReleaseStateName.Visible = true;
            this.colReleaseStateName.VisibleIndex = 14;
            // 
            // colReleaseTypeName
            // 
            this.colReleaseTypeName.FieldName = "ReleaseTypeName";
            this.colReleaseTypeName.Name = "colReleaseTypeName";
            this.colReleaseTypeName.OptionsColumn.ReadOnly = true;
            this.colReleaseTypeName.Visible = true;
            this.colReleaseTypeName.VisibleIndex = 15;
            // 
            // colTelexNo
            // 
            this.colTelexNo.FieldName = "TelexNo";
            this.colTelexNo.Name = "colTelexNo";
            this.colTelexNo.Visible = true;
            this.colTelexNo.VisibleIndex = 16;
            // 
            // _BSMain
            // 
            this._BSMain.DataSource = typeof(ICP.FCM.Common.ServiceInterface.BillOfLadingList);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemF5});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(286, 26);
            // 
            // ToolStripMenuItemF5
            // 
            this.ToolStripMenuItemF5.Name = "ToolStripMenuItemF5";
            this.ToolStripMenuItemF5.Size = new System.Drawing.Size(285, 22);
            this.ToolStripMenuItemF5.Text = "According to the selected Query(F5)";
            this.ToolStripMenuItemF5.Click += new System.EventHandler(this.ToolStripMenuItemF5_Click);
            // 
            // UCBLTreePart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlGridList);
            this.Controls.Add(this.pnlToolPart);
            this.Name = "UCBLTreePart";
            this.Size = new System.Drawing.Size(1452, 402);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolPart)).EndInit();
            this.pnlToolPart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridList)).EndInit();
            this.pnlGridList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._BSMain)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarSubItem barAdd;
        private DevExpress.XtraBars.BarButtonItem barAddMBL;
        private DevExpress.XtraBars.BarButtonItem barAddHBL;
        private DevExpress.XtraBars.BarButtonItem barEdit;
        private DevExpress.XtraBars.BarButtonItem barCopy;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarSubItem barSubItem2;
        private DevExpress.XtraBars.BarSubItem barSplitAndMerge;
        private DevExpress.XtraBars.BarButtonItem barSplitBL;
        private DevExpress.XtraBars.BarButtonItem barMergeBL;
        private DevExpress.XtraBars.BarSubItem barPrint;
        private DevExpress.XtraBars.BarButtonItem barPrintBL;
        private DevExpress.XtraBars.BarButtonItem barProfit;
        private DevExpress.XtraBars.BarButtonItem barPrintLoadCtn;
        private DevExpress.XtraBars.BarButtonItem barPrintLoadGoods;
        private DevExpress.XtraBars.BarButtonItem barRefersh;
        private DevExpress.XtraBars.BarButtonItem barEMBL;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem barAMS;
        private DevExpress.XtraBars.BarButtonItem barACI;
        private DevExpress.XtraBars.BarButtonItem barISF;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barAMSACI;
        private DevExpress.XtraBars.BarButtonItem barAMSISF;
        private DevExpress.XtraBars.BarButtonItem barAMSACIISF;
        private DevExpress.XtraBars.BarButtonItem barCopyAMSTo;
        private DevExpress.XtraBars.BarSubItem barbl;
        private DevExpress.XtraBars.BarButtonItem barchs;
        private DevExpress.XtraBars.BarButtonItem bareng;
        private DevExpress.XtraBars.BarButtonItem bartoAgentchs;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem bartoAgenteng;
        private DevExpress.XtraEditors.PanelControl pnlToolPart;
        private DevExpress.XtraEditors.PanelControl pnlGridList;
        private System.Windows.Forms.BindingSource _BSMain;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem ToolStripMenuItemF5;
        private BarButtonItem barvgm;
        private BarButtonItem barLoadContainer;
        private BarButtonItem barLoadContainerCopy;
        private BarSubItem barEDI;
        private BarButtonItem barDeclarationBL;
        private Framework.ClientComponents.Controls.LWTreeGridControl treeMain;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNo;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colBLTypeName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colShipperName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colConsigneeName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNotifyPartyName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colReleaseStateName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colReleaseTypeName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTelexNo;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colBLCFM;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colMBLD;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRBLA;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRBLH;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRBLD;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRBLRcv;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colBLRC;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colAMS;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colISF;
    }
}
