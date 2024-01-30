using DevExpress.XtraBars;
using System.ComponentModel;
using ICP.Business.Common.UI.Properties;
using System.Windows.Forms;
using System.Drawing;
namespace ICP.Business.Common.UI.BL
{
    partial class UCBLListPart
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
            this.barManager1 = new DevExpress.XtraBars.BarManager();
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
            this.barConfirmedAMS = new DevExpress.XtraBars.BarButtonItem();
            this.barCopyAMSTo = new DevExpress.XtraBars.BarButtonItem();
            this.barSplitAndMerge = new DevExpress.XtraBars.BarSubItem();
            this.barSplitBL = new DevExpress.XtraBars.BarButtonItem();
            this.barMergeBL = new DevExpress.XtraBars.BarButtonItem();
            this.barImportPO = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.pnlToolPart = new DevExpress.XtraEditors.PanelControl();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
            this.pnlGridList = new DevExpress.XtraEditors.PanelControl();
            this.gcBLList = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.ToolStripMenuItemF5 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemMarkSendAMS = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemMarkConfirmedAMS = new System.Windows.Forms.ToolStripMenuItem();
            this.bsBLList = new System.Windows.Forms.BindingSource();
            this.gvBLList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBLType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBLNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBLCfm = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colMBLD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colRBLA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colRBLH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colRBLD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colRBLRcv = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit6 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colRC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit7 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colAMS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colISF = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit9 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colShipperName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConsigneeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNotifyPartyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReleaseState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repReleaseState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colReleaseType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repReleasetype = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colTelexNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repMBLD = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repBLCfm = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemLookMBLD = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemCheckEdit8 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolPart)).BeginInit();
            this.pnlToolPart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridList)).BeginInit();
            this.pnlGridList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcBLList)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsBLList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBLList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repReleaseState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repReleasetype)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repMBLD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repBLCfm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookMBLD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit8)).BeginInit();
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
            this.barDeclarationBL,
            this.barConfirmedAMS,
            this.barImportPO});
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barbl, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrint, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barEDI, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubItem1, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barCopyAMSTo, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSplitAndMerge, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barImportPO)});
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
            new DevExpress.XtraBars.LinkPersistInfo(this.barAMSACIISF),
            new DevExpress.XtraBars.LinkPersistInfo(this.barConfirmedAMS)});
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
            // barConfirmedAMS
            // 
            this.barConfirmedAMS.Caption = "Confirmed AMS";
            this.barConfirmedAMS.Id = 32;
            this.barConfirmedAMS.Name = "barConfirmedAMS";
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
            // barImportPO
            // 
            this.barImportPO.Caption = "Import PO";
            this.barImportPO.Id = 33;
            this.barImportPO.Name = "barImportPO";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(2, 2);
            this.barDockControlTop.Size = new System.Drawing.Size(1241, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(2, 25);
            this.barDockControlBottom.Size = new System.Drawing.Size(1241, 0);
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
            this.barDockControlRight.Location = new System.Drawing.Point(1243, 28);
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
            this.pnlToolPart.Size = new System.Drawing.Size(1245, 27);
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
            this.pnlGridList.Controls.Add(this.gcBLList);
            this.pnlGridList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridList.Location = new System.Drawing.Point(0, 27);
            this.pnlGridList.Name = "pnlGridList";
            this.pnlGridList.Size = new System.Drawing.Size(1245, 318);
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
            this.gcBLList.Size = new System.Drawing.Size(1241, 314);
            this.gcBLList.TabIndex = 0;
            this.gcBLList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBLList});
            this.gcBLList.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gcBLList_ProcessGridKey);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemF5,
            this.ToolStripMenuItemMarkSendAMS,
            this.ToolStripMenuItemMarkConfirmedAMS});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(286, 70);
            // 
            // ToolStripMenuItemF5
            // 
            this.ToolStripMenuItemF5.Name = "ToolStripMenuItemF5";
            this.ToolStripMenuItemF5.Size = new System.Drawing.Size(285, 22);
            this.ToolStripMenuItemF5.Text = "According to the selected Query(F5)";
            this.ToolStripMenuItemF5.Click += new System.EventHandler(this.ToolStripMenuItemF5_Click);
            // 
            // ToolStripMenuItemMarkSendAMS
            // 
            this.ToolStripMenuItemMarkSendAMS.Name = "ToolStripMenuItemMarkSendAMS";
            this.ToolStripMenuItemMarkSendAMS.Size = new System.Drawing.Size(285, 22);
            this.ToolStripMenuItemMarkSendAMS.Text = "Mark Send AMS";
            this.ToolStripMenuItemMarkSendAMS.Click += new System.EventHandler(this.ToolStripMenuItemMarkSendAMS_Click);
            // 
            // ToolStripMenuItemMarkConfirmedAMS
            // 
            this.ToolStripMenuItemMarkConfirmedAMS.Name = "ToolStripMenuItemMarkConfirmedAMS";
            this.ToolStripMenuItemMarkConfirmedAMS.Size = new System.Drawing.Size(285, 22);
            this.ToolStripMenuItemMarkConfirmedAMS.Text = "Mark Confirmed AMS";
            this.ToolStripMenuItemMarkConfirmedAMS.Click += new System.EventHandler(this.ToolStripMenuItemMarkConfirmedAMS_Click);
            // 
            // gvBLList
            // 
            this.gvBLList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBLType,
            this.colBLNo,
            this.colBLCfm,
            this.colMBLD,
            this.colRBLA,
            this.colRBLH,
            this.colRBLD,
            this.colRBLRcv,
            this.colRC,
            this.colAMS,
            this.colISF,
            this.colShipperName,
            this.colConsigneeName,
            this.colNotifyPartyName,
            this.colReleaseState,
            this.colReleaseType,
            this.colTelexNo});
            this.gvBLList.GridControl = this.gcBLList;
            this.gvBLList.Name = "gvBLList";
            this.gvBLList.OptionsCustomization.AllowColumnResizing = false;
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
            // colBLCfm
            // 
            this.colBLCfm.Caption = "BLCfm";
            this.colBLCfm.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colBLCfm.FieldName = "BLCFM";
            this.colBLCfm.Name = "colBLCfm";
            this.colBLCfm.OptionsColumn.AllowEdit = false;
            this.colBLCfm.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colBLCfm.OptionsColumn.ReadOnly = true;
            this.colBLCfm.OptionsFilter.AllowAutoFilter = false;
            this.colBLCfm.OptionsFilter.AllowFilter = false;
            this.colBLCfm.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colBLCfm.ToolTip = "Customer has confirmed BL";
            this.colBLCfm.Visible = true;
            this.colBLCfm.VisibleIndex = 2;
            this.colBLCfm.Width = 45;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // colMBLD
            // 
            this.colMBLD.Caption = "MBLD";
            this.colMBLD.ColumnEdit = this.repositoryItemCheckEdit2;
            this.colMBLD.FieldName = "MBLD";
            this.colMBLD.Name = "colMBLD";
            this.colMBLD.OptionsColumn.AllowEdit = false;
            this.colMBLD.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colMBLD.OptionsColumn.ReadOnly = true;
            this.colMBLD.OptionsFilter.AllowAutoFilter = false;
            this.colMBLD.OptionsFilter.AllowFilter = false;
            this.colMBLD.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colMBLD.ToolTip = "SI is sent to the carrier";
            this.colMBLD.Visible = true;
            this.colMBLD.VisibleIndex = 3;
            this.colMBLD.Width = 40;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            this.repositoryItemCheckEdit2.ValueChecked = ((byte)(3));
            this.repositoryItemCheckEdit2.ValueGrayed = ((byte)(2));
            this.repositoryItemCheckEdit2.ValueUnchecked = ((byte)(1));
            // 
            // colRBLA
            // 
            this.colRBLA.Caption = "RBLA";
            this.colRBLA.ColumnEdit = this.repositoryItemCheckEdit3;
            this.colRBLA.FieldName = "RBLA";
            this.colRBLA.Name = "colRBLA";
            this.colRBLA.OptionsColumn.AllowEdit = false;
            this.colRBLA.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colRBLA.OptionsColumn.ReadOnly = true;
            this.colRBLA.OptionsFilter.AllowAutoFilter = false;
            this.colRBLA.OptionsFilter.AllowFilter = false;
            this.colRBLA.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colRBLA.ToolTip = "Applied release BL";
            this.colRBLA.Visible = true;
            this.colRBLA.VisibleIndex = 4;
            this.colRBLA.Width = 40;
            // 
            // repositoryItemCheckEdit3
            // 
            this.repositoryItemCheckEdit3.AutoHeight = false;
            this.repositoryItemCheckEdit3.Name = "repositoryItemCheckEdit3";
            // 
            // colRBLH
            // 
            this.colRBLH.Caption = "RBLH";
            this.colRBLH.ColumnEdit = this.repositoryItemCheckEdit4;
            this.colRBLH.FieldName = "RBLH";
            this.colRBLH.Name = "colRBLH";
            this.colRBLH.OptionsColumn.AllowEdit = false;
            this.colRBLH.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colRBLH.OptionsColumn.ReadOnly = true;
            this.colRBLH.OptionsFilter.AllowAutoFilter = false;
            this.colRBLH.OptionsFilter.AllowFilter = false;
            this.colRBLH.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colRBLH.ToolTip = "Release BL is holding";
            this.colRBLH.Visible = true;
            this.colRBLH.VisibleIndex = 5;
            this.colRBLH.Width = 40;
            // 
            // repositoryItemCheckEdit4
            // 
            this.repositoryItemCheckEdit4.AutoHeight = false;
            this.repositoryItemCheckEdit4.Name = "repositoryItemCheckEdit4";
            // 
            // colRBLD
            // 
            this.colRBLD.Caption = "RBLD";
            this.colRBLD.ColumnEdit = this.repositoryItemCheckEdit5;
            this.colRBLD.FieldName = "RBLD";
            this.colRBLD.Name = "colRBLD";
            this.colRBLD.OptionsColumn.AllowEdit = false;
            this.colRBLD.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colRBLD.OptionsColumn.ReadOnly = true;
            this.colRBLD.OptionsFilter.AllowAutoFilter = false;
            this.colRBLD.OptionsFilter.AllowFilter = false;
            this.colRBLD.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colRBLD.ToolTip = "Is released";
            this.colRBLD.Visible = true;
            this.colRBLD.VisibleIndex = 6;
            this.colRBLD.Width = 40;
            // 
            // repositoryItemCheckEdit5
            // 
            this.repositoryItemCheckEdit5.AutoHeight = false;
            this.repositoryItemCheckEdit5.Name = "repositoryItemCheckEdit5";
            // 
            // colRBLRcv
            // 
            this.colRBLRcv.Caption = "RBLRcv";
            this.colRBLRcv.ColumnEdit = this.repositoryItemCheckEdit6;
            this.colRBLRcv.FieldName = "RBLRcv";
            this.colRBLRcv.Name = "colRBLRcv";
            this.colRBLRcv.OptionsColumn.AllowEdit = false;
            this.colRBLRcv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colRBLRcv.OptionsColumn.ReadOnly = true;
            this.colRBLRcv.OptionsFilter.AllowAutoFilter = false;
            this.colRBLRcv.OptionsFilter.AllowFilter = false;
            this.colRBLRcv.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colRBLRcv.ToolTip = "CS/Agent has received the notice of the releasing BL.";
            this.colRBLRcv.Visible = true;
            this.colRBLRcv.VisibleIndex = 7;
            this.colRBLRcv.Width = 50;
            // 
            // repositoryItemCheckEdit6
            // 
            this.repositoryItemCheckEdit6.AutoHeight = false;
            this.repositoryItemCheckEdit6.Name = "repositoryItemCheckEdit6";
            // 
            // colRC
            // 
            this.colRC.Caption = "RC";
            this.colRC.ColumnEdit = this.repositoryItemCheckEdit7;
            this.colRC.FieldName = "BLRC";
            this.colRC.Name = "colRC";
            this.colRC.OptionsColumn.AllowEdit = false;
            this.colRC.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colRC.OptionsColumn.ReadOnly = true;
            this.colRC.OptionsFilter.AllowAutoFilter = false;
            this.colRC.OptionsFilter.AllowFilter = false;
            this.colRC.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colRC.ToolTip = "The cargo of BL is released";
            this.colRC.Visible = true;
            this.colRC.VisibleIndex = 8;
            this.colRC.Width = 40;
            // 
            // repositoryItemCheckEdit7
            // 
            this.repositoryItemCheckEdit7.AutoHeight = false;
            this.repositoryItemCheckEdit7.Name = "repositoryItemCheckEdit7";
            // 
            // colAMS
            // 
            this.colAMS.Caption = "AMS";
            this.colAMS.FieldName = "AMSStateName";
            this.colAMS.Name = "colAMS";
            this.colAMS.OptionsColumn.AllowEdit = false;
            this.colAMS.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colAMS.OptionsColumn.ReadOnly = true;
            this.colAMS.OptionsFilter.AllowAutoFilter = false;
            this.colAMS.OptionsFilter.AllowFilter = false;
            this.colAMS.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colAMS.ToolTip = "AMS is done";
            this.colAMS.Visible = true;
            this.colAMS.VisibleIndex = 9;
            this.colAMS.Width = 60;
            // 
            // colISF
            // 
            this.colISF.Caption = "ISF";
            this.colISF.ColumnEdit = this.repositoryItemCheckEdit9;
            this.colISF.FieldName = "ISF";
            this.colISF.Name = "colISF";
            this.colISF.OptionsColumn.AllowEdit = false;
            this.colISF.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colISF.OptionsColumn.ReadOnly = true;
            this.colISF.OptionsFilter.AllowAutoFilter = false;
            this.colISF.OptionsFilter.AllowFilter = false;
            this.colISF.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colISF.ToolTip = "ISF is done.";
            this.colISF.Visible = true;
            this.colISF.VisibleIndex = 10;
            this.colISF.Width = 40;
            // 
            // repositoryItemCheckEdit9
            // 
            this.repositoryItemCheckEdit9.AutoHeight = false;
            this.repositoryItemCheckEdit9.Name = "repositoryItemCheckEdit9";
            this.repositoryItemCheckEdit9.ValueChecked = ((byte)(3));
            this.repositoryItemCheckEdit9.ValueGrayed = ((byte)(2));
            this.repositoryItemCheckEdit9.ValueUnchecked = ((byte)(1));
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
            this.colShipperName.VisibleIndex = 11;
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
            this.colConsigneeName.VisibleIndex = 12;
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
            this.colNotifyPartyName.VisibleIndex = 13;
            this.colNotifyPartyName.Width = 200;
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
            this.colReleaseState.VisibleIndex = 14;
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
            this.colReleaseType.VisibleIndex = 15;
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
            this.colTelexNo.VisibleIndex = 16;
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
            // UCBLListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlGridList);
            this.Controls.Add(this.pnlToolPart);
            this.Name = "UCBLListPart";
            this.Size = new System.Drawing.Size(1245, 345);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolPart)).EndInit();
            this.pnlToolPart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridList)).EndInit();
            this.pnlGridList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcBLList)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsBLList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBLList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repReleaseState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repReleasetype)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repMBLD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repBLCfm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookMBLD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit8)).EndInit();
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
        private DevExpress.XtraGrid.Columns.GridColumn colBLCfm;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colMBLD;
        private DevExpress.XtraGrid.Columns.GridColumn colRBLA;
        private DevExpress.XtraGrid.Columns.GridColumn colRBLH;
        private DevExpress.XtraGrid.Columns.GridColumn colRBLD;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit4;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit5;
        private DevExpress.XtraGrid.Columns.GridColumn colRBLRcv;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit6;
        private DevExpress.XtraGrid.Columns.GridColumn colRC;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit7;
        private DevExpress.XtraGrid.Columns.GridColumn colReleaseState;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repMBLD;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repBLCfm;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repReleaseState;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookMBLD;
        private DevExpress.XtraGrid.Columns.GridColumn colAMS;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit8;
        private DevExpress.XtraGrid.Columns.GridColumn colISF;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit9;
        private DevExpress.XtraGrid.Columns.GridColumn colReleaseType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repReleasetype;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem ToolStripMenuItemF5;
        private DevExpress.XtraGrid.Columns.GridColumn colTelexNo;
        private BarButtonItem barvgm;
        private BarButtonItem barLoadContainer;
        private BarButtonItem barLoadContainerCopy;
        private BarSubItem barEDI;
        private BarButtonItem barDeclarationBL;
        private BarButtonItem barConfirmedAMS;
        private ToolStripMenuItem ToolStripMenuItemMarkSendAMS;
        private ToolStripMenuItem ToolStripMenuItemMarkConfirmedAMS;
        private BarButtonItem barImportPO;
    }
}
