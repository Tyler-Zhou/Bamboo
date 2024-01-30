using DevExpress.XtraBars;
using System.ComponentModel;
using ICP.Business.Common.UI.Properties;
using System.Windows.Forms;
using System.Drawing;
namespace ICP.Business.Common.UI.BL
{
    partial class DeclareBLListPart
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
            this.barEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barCopy = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barPrint = new DevExpress.XtraBars.BarSubItem();
            this.barPrintBL = new DevExpress.XtraBars.BarButtonItem();
            this.barEDI = new DevExpress.XtraBars.BarSubItem();
            this.barBooking = new DevExpress.XtraBars.BarButtonItem();
            this.barContainerLoad = new DevExpress.XtraBars.BarButtonItem();
            this.barPreplan = new DevExpress.XtraBars.BarButtonItem();
            this.barSupplement = new DevExpress.XtraBars.BarButtonItem();
            this.barWharf = new DevExpress.XtraBars.BarButtonItem();
            this.barVGM = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.pnlToolPart = new DevExpress.XtraEditors.PanelControl();
            this.barSplitAndMerge = new DevExpress.XtraBars.BarSubItem();
            this.barSplitBL = new DevExpress.XtraBars.BarButtonItem();
            this.barMergeBL = new DevExpress.XtraBars.BarButtonItem();
            this.barProfit = new DevExpress.XtraBars.BarButtonItem();
            this.barPrintLoadCtn = new DevExpress.XtraBars.BarButtonItem();
            this.barPrintLoadGoods = new DevExpress.XtraBars.BarButtonItem();
            this.barEMBL = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barAMS = new DevExpress.XtraBars.BarButtonItem();
            this.barACI = new DevExpress.XtraBars.BarButtonItem();
            this.barISF = new DevExpress.XtraBars.BarButtonItem();
            this.barAMSACI = new DevExpress.XtraBars.BarButtonItem();
            this.barAMSISF = new DevExpress.XtraBars.BarButtonItem();
            this.barAMSACIISF = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barCopyAMSTo = new DevExpress.XtraBars.BarButtonItem();
            this.barbl = new DevExpress.XtraBars.BarSubItem();
            this.barchs = new DevExpress.XtraBars.BarButtonItem();
            this.bareng = new DevExpress.XtraBars.BarButtonItem();
            this.bartoAgentchs = new DevExpress.XtraBars.BarButtonItem();
            this.bartoAgenteng = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
            this.pnlGridList = new DevExpress.XtraEditors.PanelControl();
            this.gcBLList = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemF5 = new System.Windows.Forms.ToolStripMenuItem();
            this.bsBLList = new System.Windows.Forms.BindingSource(this.components);
            this.gvBLList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBLType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBLNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShipperName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConsigneeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNotifyPartyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVesselVoyage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReleaseState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repReleaseState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colReleaseType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repReleasetype = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colTelexNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit7 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repMBLD = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repBLCfm = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemLookMBLD = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemCheckEdit8 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit9 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.barAPICLP = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolPart)).BeginInit();
            this.pnlToolPart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridList)).BeginInit();
            this.pnlGridList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcBLList)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsBLList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBLList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repReleaseState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repReleasetype)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repMBLD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repBLCfm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookMBLD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit9)).BeginInit();
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
            this.barEDI,
            this.barBooking,
            this.barPreplan,
            this.barSupplement,
            this.barWharf,
            this.barVGM,
            this.barContainerLoad,
            this.barAPICLP});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 34;
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrint, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barEDI)});
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAddHBL, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
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
            // barPrint
            // 
            this.barPrint.Caption = "Print";
            this.barPrint.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Print_16;
            this.barPrint.Id = 6;
            this.barPrint.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrintBL, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.barPrint.Name = "barPrint";
            // 
            // barPrintBL
            // 
            this.barPrintBL.Caption = "&Print BL";
            this.barPrintBL.Id = 4;
            this.barPrintBL.Name = "barPrintBL";
            // 
            // barEDI
            // 
            this.barEDI.Caption = "EDI";
            this.barEDI.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Transfer_16;
            this.barEDI.Id = 26;
            this.barEDI.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barBooking),
            new DevExpress.XtraBars.LinkPersistInfo(this.barContainerLoad),
            new DevExpress.XtraBars.LinkPersistInfo(this.barPreplan),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSupplement),
            new DevExpress.XtraBars.LinkPersistInfo(this.barWharf),
            new DevExpress.XtraBars.LinkPersistInfo(this.barVGM),
            new DevExpress.XtraBars.LinkPersistInfo(this.barAPICLP)});
            this.barEDI.Name = "barEDI";
            this.barEDI.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barBooking
            // 
            this.barBooking.Caption = "电子订舱";
            this.barBooking.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Transfer_16;
            this.barBooking.Id = 27;
            this.barBooking.Name = "barBooking";
            this.barBooking.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barContainerLoad
            // 
            this.barContainerLoad.Caption = "电子装箱";
            this.barContainerLoad.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Transfer_16;
            this.barContainerLoad.Id = 32;
            this.barContainerLoad.Name = "barContainerLoad";
            // 
            // barPreplan
            // 
            this.barPreplan.Caption = "电子预配";
            this.barPreplan.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Transfer_16;
            this.barPreplan.Id = 28;
            this.barPreplan.Name = "barPreplan";
            this.barPreplan.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barSupplement
            // 
            this.barSupplement.Caption = "电子补料";
            this.barSupplement.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Transfer_16;
            this.barSupplement.Id = 29;
            this.barSupplement.Name = "barSupplement";
            this.barSupplement.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barWharf
            // 
            this.barWharf.Caption = "电子码头";
            this.barWharf.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Transfer_16;
            this.barWharf.Id = 30;
            this.barWharf.Name = "barWharf";
            this.barWharf.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barVGM
            // 
            this.barVGM.Caption = "VGM";
            this.barVGM.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Transfer_16;
            this.barVGM.Id = 31;
            this.barVGM.Name = "barVGM";
            this.barVGM.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(2, 2);
            this.barDockControlTop.Size = new System.Drawing.Size(699, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(2, 25);
            this.barDockControlBottom.Size = new System.Drawing.Size(699, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(2, 28);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 0);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(701, 28);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 0);
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
            this.pnlToolPart.Size = new System.Drawing.Size(703, 27);
            this.pnlToolPart.TabIndex = 0;
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
            // barEMBL
            // 
            this.barEMBL.Caption = "E-MBL";
            this.barEMBL.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Transfer_16;
            this.barEMBL.Id = 8;
            this.barEMBL.Name = "barEMBL";
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
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "AMS&ACI";
            this.barButtonItem4.Id = 15;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // barCopyAMSTo
            // 
            this.barCopyAMSTo.Caption = "Copy  AMS  To";
            this.barCopyAMSTo.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Transfer_16;
            this.barCopyAMSTo.Id = 19;
            this.barCopyAMSTo.Name = "barCopyAMSTo";
            // 
            // barbl
            // 
            this.barbl.Caption = "Mail BL Copy To Customer";
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
            this.pnlGridList.Controls.Add(this.gcBLList);
            this.pnlGridList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridList.Location = new System.Drawing.Point(0, 27);
            this.pnlGridList.Name = "pnlGridList";
            this.pnlGridList.Size = new System.Drawing.Size(703, 328);
            this.pnlGridList.TabIndex = 1;
            // 
            // gcBLList
            // 
            this.gcBLList.ContextMenuStrip = this.contextMenuStrip;
            this.gcBLList.DataSource = this.bsBLList;
            this.gcBLList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcBLList.Location = new System.Drawing.Point(2, 2);
            this.gcBLList.MainView = this.gvBLList;
            this.gcBLList.Name = "gcBLList";
            this.gcBLList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2,
            this.repositoryItemCheckEdit3,
            this.repositoryItemCheckEdit4,
            this.repositoryItemCheckEdit5,
            this.repositoryItemCheckEdit6,
            this.repositoryItemCheckEdit7,
            this.repMBLD,
            this.repBLCfm,
            this.repReleaseState,
            this.repositoryItemLookUpEdit1,
            this.repositoryItemLookMBLD,
            this.repositoryItemCheckEdit8,
            this.repositoryItemCheckEdit9,
            this.repReleasetype});
            this.gcBLList.Size = new System.Drawing.Size(699, 324);
            this.gcBLList.TabIndex = 0;
            this.gcBLList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBLList});
            this.gcBLList.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gcBLList_ProcessGridKey);
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
            // bsBLList
            // 
            this.bsBLList.DataSource = typeof(ICP.FCM.OceanExport.ServiceInterface.DataObjects.OceanBLList);
            // 
            // gvBLList
            // 
            this.gvBLList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBLType,
            this.colBLNo,
            this.colShipperName,
            this.colConsigneeName,
            this.colNotifyPartyName,
            this.colVesselVoyage,
            this.colPOL,
            this.colPOD,
            this.colETD,
            this.colETA,
            this.colReleaseState,
            this.colReleaseType,
            this.colTelexNo});
            this.gvBLList.GridControl = this.gcBLList;
            this.gvBLList.Name = "gvBLList";
            this.gvBLList.OptionsCustomization.AllowColumnResizing = false;
            this.gvBLList.OptionsSelection.MultiSelect = true;
            this.gvBLList.OptionsView.ColumnAutoWidth = false;
            this.gvBLList.OptionsView.ShowGroupPanel = false;
            this.gvBLList.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvBLList_RowClick);
            this.gvBLList.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvBLList_FocusedRowChanged);
            // 
            // colBLType
            // 
            this.colBLType.Caption = " ";
            this.colBLType.FieldName = "BLTypeName";
            this.colBLType.Name = "colBLType";
            this.colBLType.OptionsColumn.AllowEdit = false;
            this.colBLType.OptionsColumn.ReadOnly = true;
            this.colBLType.OptionsFilter.AllowAutoFilter = false;
            this.colBLType.OptionsFilter.AllowFilter = false;
            this.colBLType.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colBLType.Visible = true;
            this.colBLType.VisibleIndex = 0;
            this.colBLType.Width = 50;
            // 
            // colBLNo
            // 
            this.colBLNo.Caption = "BL NO";
            this.colBLNo.FieldName = "No";
            this.colBLNo.Name = "colBLNo";
            this.colBLNo.OptionsColumn.AllowEdit = false;
            this.colBLNo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colBLNo.OptionsColumn.ReadOnly = true;
            this.colBLNo.OptionsFilter.AllowAutoFilter = false;
            this.colBLNo.OptionsFilter.AllowFilter = false;
            this.colBLNo.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colBLNo.Visible = true;
            this.colBLNo.VisibleIndex = 1;
            this.colBLNo.Width = 150;
            // 
            // colShipperName
            // 
            this.colShipperName.Caption = "Shipper";
            this.colShipperName.FieldName = "ShipperName";
            this.colShipperName.Name = "colShipperName";
            this.colShipperName.OptionsColumn.AllowEdit = false;
            this.colShipperName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colShipperName.OptionsColumn.ReadOnly = true;
            this.colShipperName.OptionsFilter.AllowAutoFilter = false;
            this.colShipperName.OptionsFilter.AllowFilter = false;
            this.colShipperName.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colShipperName.Visible = true;
            this.colShipperName.VisibleIndex = 2;
            this.colShipperName.Width = 200;
            // 
            // colConsigneeName
            // 
            this.colConsigneeName.Caption = "Consignee";
            this.colConsigneeName.FieldName = "ConsigneeName";
            this.colConsigneeName.Name = "colConsigneeName";
            this.colConsigneeName.OptionsColumn.AllowEdit = false;
            this.colConsigneeName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colConsigneeName.OptionsColumn.ReadOnly = true;
            this.colConsigneeName.OptionsFilter.AllowAutoFilter = false;
            this.colConsigneeName.OptionsFilter.AllowFilter = false;
            this.colConsigneeName.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colConsigneeName.Visible = true;
            this.colConsigneeName.VisibleIndex = 3;
            this.colConsigneeName.Width = 200;
            // 
            // colNotifyPartyName
            // 
            this.colNotifyPartyName.Caption = "Noticer";
            this.colNotifyPartyName.FieldName = "NotifyPartyName";
            this.colNotifyPartyName.Name = "colNotifyPartyName";
            this.colNotifyPartyName.OptionsColumn.AllowEdit = false;
            this.colNotifyPartyName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colNotifyPartyName.OptionsColumn.ReadOnly = true;
            this.colNotifyPartyName.OptionsFilter.AllowAutoFilter = false;
            this.colNotifyPartyName.OptionsFilter.AllowFilter = false;
            this.colNotifyPartyName.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colNotifyPartyName.Visible = true;
            this.colNotifyPartyName.VisibleIndex = 4;
            this.colNotifyPartyName.Width = 200;
            // 
            // colVesselVoyage
            // 
            this.colVesselVoyage.Caption = "VesselVoyage";
            this.colVesselVoyage.FieldName = "VesselVoyage";
            this.colVesselVoyage.Name = "colVesselVoyage";
            this.colVesselVoyage.Visible = true;
            this.colVesselVoyage.VisibleIndex = 5;
            // 
            // colPOL
            // 
            this.colPOL.Caption = "POL";
            this.colPOL.FieldName = "POL";
            this.colPOL.Name = "colPOL";
            this.colPOL.Visible = true;
            this.colPOL.VisibleIndex = 6;
            // 
            // colPOD
            // 
            this.colPOD.Caption = "POD";
            this.colPOD.FieldName = "POD";
            this.colPOD.Name = "colPOD";
            this.colPOD.Visible = true;
            this.colPOD.VisibleIndex = 7;
            // 
            // colETD
            // 
            this.colETD.Caption = "ETD";
            this.colETD.FieldName = "ETD";
            this.colETD.Name = "colETD";
            this.colETD.Visible = true;
            this.colETD.VisibleIndex = 8;
            // 
            // colETA
            // 
            this.colETA.Caption = "ETA";
            this.colETA.FieldName = "ETA";
            this.colETA.Name = "colETA";
            this.colETA.Visible = true;
            this.colETA.VisibleIndex = 9;
            // 
            // colReleaseState
            // 
            this.colReleaseState.Caption = "ReleaseState";
            this.colReleaseState.ColumnEdit = this.repReleaseState;
            this.colReleaseState.FieldName = "ReleaseState";
            this.colReleaseState.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText;
            this.colReleaseState.Name = "colReleaseState";
            this.colReleaseState.OptionsColumn.AllowEdit = false;
            this.colReleaseState.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colReleaseState.OptionsColumn.ReadOnly = true;
            this.colReleaseState.OptionsFilter.AllowAutoFilter = false;
            this.colReleaseState.OptionsFilter.AllowFilter = false;
            this.colReleaseState.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colReleaseState.Visible = true;
            this.colReleaseState.VisibleIndex = 10;
            this.colReleaseState.Width = 80;
            // 
            // repReleaseState
            // 
            this.repReleaseState.AutoHeight = false;
            this.repReleaseState.Name = "repReleaseState";
            // 
            // colReleaseType
            // 
            this.colReleaseType.Caption = "ReleaseType";
            this.colReleaseType.ColumnEdit = this.repReleasetype;
            this.colReleaseType.FieldName = "ReleaseType";
            this.colReleaseType.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText;
            this.colReleaseType.Name = "colReleaseType";
            this.colReleaseType.OptionsColumn.AllowEdit = false;
            this.colReleaseType.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colReleaseType.OptionsColumn.ReadOnly = true;
            this.colReleaseType.OptionsFilter.AllowAutoFilter = false;
            this.colReleaseType.OptionsFilter.AllowFilter = false;
            this.colReleaseType.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colReleaseType.Visible = true;
            this.colReleaseType.VisibleIndex = 11;
            // 
            // repReleasetype
            // 
            this.repReleasetype.AutoHeight = false;
            this.repReleasetype.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repReleasetype.Name = "repReleasetype";
            // 
            // colTelexNo
            // 
            this.colTelexNo.Caption = "TelexNo";
            this.colTelexNo.FieldName = "TelexNo";
            this.colTelexNo.Name = "colTelexNo";
            this.colTelexNo.OptionsColumn.AllowEdit = false;
            this.colTelexNo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colTelexNo.OptionsColumn.ReadOnly = true;
            this.colTelexNo.Visible = true;
            this.colTelexNo.VisibleIndex = 12;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            this.repositoryItemCheckEdit2.ValueChecked = ((byte)(3));
            this.repositoryItemCheckEdit2.ValueGrayed = ((byte)(2));
            this.repositoryItemCheckEdit2.ValueUnchecked = ((byte)(1));
            // 
            // repositoryItemCheckEdit3
            // 
            this.repositoryItemCheckEdit3.AutoHeight = false;
            this.repositoryItemCheckEdit3.Name = "repositoryItemCheckEdit3";
            // 
            // repositoryItemCheckEdit4
            // 
            this.repositoryItemCheckEdit4.AutoHeight = false;
            this.repositoryItemCheckEdit4.Name = "repositoryItemCheckEdit4";
            // 
            // repositoryItemCheckEdit5
            // 
            this.repositoryItemCheckEdit5.AutoHeight = false;
            this.repositoryItemCheckEdit5.Name = "repositoryItemCheckEdit5";
            // 
            // repositoryItemCheckEdit6
            // 
            this.repositoryItemCheckEdit6.AutoHeight = false;
            this.repositoryItemCheckEdit6.Name = "repositoryItemCheckEdit6";
            // 
            // repositoryItemCheckEdit7
            // 
            this.repositoryItemCheckEdit7.AutoHeight = false;
            this.repositoryItemCheckEdit7.Name = "repositoryItemCheckEdit7";
            // 
            // repMBLD
            // 
            this.repMBLD.AutoHeight = false;
            this.repMBLD.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repMBLD.Name = "repMBLD";
            // 
            // repBLCfm
            // 
            this.repBLCfm.AutoHeight = false;
            this.repBLCfm.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repBLCfm.Name = "repBLCfm";
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            // 
            // repositoryItemLookMBLD
            // 
            this.repositoryItemLookMBLD.AutoHeight = false;
            this.repositoryItemLookMBLD.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemLookMBLD.Name = "repositoryItemLookMBLD";
            // 
            // repositoryItemCheckEdit8
            // 
            this.repositoryItemCheckEdit8.AutoHeight = false;
            this.repositoryItemCheckEdit8.Name = "repositoryItemCheckEdit8";
            this.repositoryItemCheckEdit8.ValueChecked = ((byte)(3));
            this.repositoryItemCheckEdit8.ValueGrayed = ((byte)(2));
            this.repositoryItemCheckEdit8.ValueUnchecked = ((byte)(1));
            // 
            // repositoryItemCheckEdit9
            // 
            this.repositoryItemCheckEdit9.AutoHeight = false;
            this.repositoryItemCheckEdit9.Name = "repositoryItemCheckEdit9";
            this.repositoryItemCheckEdit9.ValueChecked = ((byte)(3));
            this.repositoryItemCheckEdit9.ValueGrayed = ((byte)(2));
            this.repositoryItemCheckEdit9.ValueUnchecked = ((byte)(1));
            // 
            // barAPICLP
            // 
            this.barAPICLP.Caption = "无纸化箱单";
            this.barAPICLP.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Transfer_16;
            this.barAPICLP.Id = 33;
            this.barAPICLP.Name = "barAPICLP";
            // 
            // DeclareBLListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlGridList);
            this.Controls.Add(this.pnlToolPart);
            this.Name = "DeclareBLListPart";
            this.Size = new System.Drawing.Size(703, 355);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolPart)).EndInit();
            this.pnlToolPart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridList)).EndInit();
            this.pnlGridList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcBLList)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsBLList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBLList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repReleaseState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repReleasetype)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repMBLD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repBLCfm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookMBLD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit9)).EndInit();
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

        protected DevExpress.XtraGrid.GridControl gcBLList;
        protected DevExpress.XtraGrid.Views.Grid.GridView gvBLList;
        private DevExpress.XtraGrid.Columns.GridColumn colBLType;
        private DevExpress.XtraGrid.Columns.GridColumn colBLNo;
        private DevExpress.XtraGrid.Columns.GridColumn colShipperName;
        private DevExpress.XtraGrid.Columns.GridColumn colConsigneeName;
        private DevExpress.XtraGrid.Columns.GridColumn colNotifyPartyName;
        private System.Windows.Forms.BindingSource bsBLList;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit4;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit5;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit6;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit7;
        private DevExpress.XtraGrid.Columns.GridColumn colReleaseState;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repMBLD;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repBLCfm;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repReleaseState;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookMBLD;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit8;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit9;
        private DevExpress.XtraGrid.Columns.GridColumn colReleaseType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repReleasetype;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem ToolStripMenuItemF5;
        private DevExpress.XtraGrid.Columns.GridColumn colTelexNo;
        private BarSubItem barEDI;
        private BarButtonItem barBooking;
        private BarButtonItem barPreplan;
        private BarButtonItem barSupplement;
        private BarButtonItem barWharf;
        private DevExpress.XtraGrid.Columns.GridColumn colPOL;
        private DevExpress.XtraGrid.Columns.GridColumn colPOD;
        private DevExpress.XtraGrid.Columns.GridColumn colETD;
        private DevExpress.XtraGrid.Columns.GridColumn colETA;
        private DevExpress.XtraGrid.Columns.GridColumn colVesselVoyage;
        private BarButtonItem barVGM;
        private BarButtonItem barContainerLoad;
        private BarButtonItem barAPICLP;
    }
}
