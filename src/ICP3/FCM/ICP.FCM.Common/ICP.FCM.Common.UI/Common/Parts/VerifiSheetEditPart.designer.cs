using System.Windows.Forms;
namespace ICP.FCM.Common.UI.Common.Parts
{
    partial class VerifiSheetEditPart
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
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.bsSheetInfo = new System.Windows.Forms.BindingSource(this.components);
            this.gridControl1 = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSheetNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExpressNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReceiptDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReturnDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperationNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupBase = new System.Windows.Forms.GroupBox();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.txtCustomer = new DevExpress.XtraEditors.ButtonEdit();
            this.txtExpressNO = new DevExpress.XtraEditors.TextEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.labExpressNO = new DevExpress.XtraEditors.LabelControl();
            this.txtSheetNo = new DevExpress.XtraEditors.TextEdit();
            this.chkIsFreightArrive = new DevExpress.XtraEditors.CheckEdit();
            this.labReturnDate = new DevExpress.XtraEditors.LabelControl();
            this.dteReturnDate = new DevExpress.XtraEditors.DateEdit();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.labRemark = new DevExpress.XtraEditors.LabelControl();
            this.labReceiptDate = new DevExpress.XtraEditors.LabelControl();
            this.dteReceiptDate = new DevExpress.XtraEditors.DateEdit();
            this.labSheetNo = new DevExpress.XtraEditors.LabelControl();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.dxErrorContainers = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.panelScroll = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSheetInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            this.groupBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpressNO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSheetNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsFreightArrive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReturnDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReturnDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReceiptDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReceiptDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorContainers)).BeginInit();
            this.panelScroll.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.VerificationSheet);
            this.bsList.CurrentChanged += new System.EventHandler(this.bsList_CurrentChanged);
            // 
            // bsSheetInfo
            // 
            this.bsSheetInfo.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.VerificationSheet);
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bsList;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gvMain;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(800, 150);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSheetNo,
            this.colCustomerName,
            this.colExpressNO,
            this.colReceiptDate,
            this.colReturnDate,
            this.colOperationNo});
            this.gvMain.GridControl = this.gridControl1;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowDetailButtons = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvMain_RowStyle);
            // 
            // colSheetNo
            // 
            this.colSheetNo.FieldName = "SheetNo";
            this.colSheetNo.Name = "colSheetNo";
            this.colSheetNo.OptionsColumn.AllowEdit = false;
            this.colSheetNo.Visible = true;
            this.colSheetNo.VisibleIndex = 0;
            this.colSheetNo.Width = 120;
            // 
            // colCustomerName
            // 
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.OptionsColumn.AllowEdit = false;
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 1;
            this.colCustomerName.Width = 150;
            // 
            // colExpressNO
            // 
            this.colExpressNO.FieldName = "ExpressNO";
            this.colExpressNO.Name = "colExpressNO";
            this.colExpressNO.OptionsColumn.AllowEdit = false;
            this.colExpressNO.Visible = true;
            this.colExpressNO.VisibleIndex = 2;
            this.colExpressNO.Width = 150;
            // 
            // colReceiptDate
            // 
            this.colReceiptDate.FieldName = "ReceiptDate";
            this.colReceiptDate.MinWidth = 50;
            this.colReceiptDate.Name = "colReceiptDate";
            this.colReceiptDate.OptionsColumn.AllowEdit = false;
            this.colReceiptDate.Visible = true;
            this.colReceiptDate.VisibleIndex = 3;
            this.colReceiptDate.Width = 120;
            // 
            // colReturnDate
            // 
            this.colReturnDate.FieldName = "ReturnDate";
            this.colReturnDate.Name = "colReturnDate";
            this.colReturnDate.OptionsColumn.AllowEdit = false;
            this.colReturnDate.Visible = true;
            this.colReturnDate.VisibleIndex = 4;
            this.colReturnDate.Width = 239;
            // 
            // colOperationNo
            // 
            this.colOperationNo.FieldName = "OperationNo";
            this.colOperationNo.Name = "colOperationNo";
            this.colOperationNo.Visible = true;
            this.colOperationNo.VisibleIndex = 5;
            // 
            // groupBase
            // 
            this.groupBase.Controls.Add(this.labCustomer);
            this.groupBase.Controls.Add(this.txtCustomer);
            this.groupBase.Controls.Add(this.txtExpressNO);
            this.groupBase.Controls.Add(this.labExpressNO);
            this.groupBase.Controls.Add(this.txtSheetNo);
            this.groupBase.Controls.Add(this.chkIsFreightArrive);
            this.groupBase.Controls.Add(this.labReturnDate);
            this.groupBase.Controls.Add(this.dteReturnDate);
            this.groupBase.Controls.Add(this.txtRemark);
            this.groupBase.Controls.Add(this.labRemark);
            this.groupBase.Controls.Add(this.labReceiptDate);
            this.groupBase.Controls.Add(this.dteReceiptDate);
            this.groupBase.Controls.Add(this.labSheetNo);
            this.groupBase.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBase.Location = new System.Drawing.Point(0, 150);
            this.groupBase.Name = "groupBase";
            this.groupBase.Size = new System.Drawing.Size(800, 517);
            this.groupBase.TabIndex = 0;
            this.groupBase.TabStop = false;
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(16, 73);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(52, 14);
            this.labCustomer.TabIndex = 386;
            this.labCustomer.Text = "Customer";
            // 
            // txtCustomer
            // 
            this.txtCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsSheetInfo, "CustomerId", true));
            this.txtCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSheetInfo, "CustomerName", true));
            this.dxErrorProvider1.SetIconAlignment(this.txtCustomer, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxErrorContainers.SetIconAlignment(this.txtCustomer, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCustomer.Location = new System.Drawing.Point(99, 70);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtCustomer.Properties.Appearance.Options.UseBackColor = true;
            this.txtCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCustomer.Size = new System.Drawing.Size(302, 21);
            this.txtCustomer.TabIndex = 385;
            this.txtCustomer.TabStop = false;
            // 
            // txtExpressNO
            // 
            this.txtExpressNO.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSheetInfo, "ExpressNO", true));
            this.txtExpressNO.Location = new System.Drawing.Point(99, 44);
            this.txtExpressNO.MenuManager = this.barManager1;
            this.txtExpressNO.Name = "txtExpressNO";
            this.txtExpressNO.Size = new System.Drawing.Size(302, 21);
            this.txtExpressNO.TabIndex = 384;
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
            this.barSave,
            this.barAdd,
            this.barDelete,
            this.barClose});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 12;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAdd, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barAdd
            // 
            this.barAdd.Caption = "&Add";
            this.barAdd.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Add_16;
            this.barAdd.Id = 1;
            this.barAdd.Name = "barAdd";
            this.barAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdd_ItemClick);
            // 
            // barDelete
            // 
            this.barDelete.Caption = "&Delete";
            this.barDelete.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Delete_16;
            this.barDelete.Id = 2;
            this.barDelete.Name = "barDelete";
            this.barDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // barSave
            // 
            this.barSave.Caption = "&Save";
            this.barSave.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Save_Blue_16;
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "&Close";
            this.barClose.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Left_D_16;
            this.barClose.Id = 4;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(807, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 674);
            this.barDockControlBottom.Size = new System.Drawing.Size(807, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 648);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(807, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 648);
            // 
            // labExpressNO
            // 
            this.labExpressNO.Location = new System.Drawing.Point(16, 47);
            this.labExpressNO.Name = "labExpressNO";
            this.labExpressNO.Size = new System.Drawing.Size(58, 14);
            this.labExpressNO.TabIndex = 383;
            this.labExpressNO.Text = "ExpressNO";
            // 
            // txtSheetNo
            // 
            this.txtSheetNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSheetInfo, "SheetNo", true));
            this.txtSheetNo.Location = new System.Drawing.Point(99, 18);
            this.txtSheetNo.MenuManager = this.barManager1;
            this.txtSheetNo.Name = "txtSheetNo";
            this.txtSheetNo.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.txtSheetNo.Properties.Appearance.Options.UseBackColor = true;
            this.txtSheetNo.Size = new System.Drawing.Size(302, 21);
            this.txtSheetNo.TabIndex = 382;
            // 
            // chkIsFreightArrive
            // 
            this.chkIsFreightArrive.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsSheetInfo, "IsFreightArrive", true));
            this.chkIsFreightArrive.Location = new System.Drawing.Point(16, 151);
            this.chkIsFreightArrive.MenuManager = this.barManager1;
            this.chkIsFreightArrive.Name = "chkIsFreightArrive";
            this.chkIsFreightArrive.Properties.Caption = "IsFreightArrive";
            this.chkIsFreightArrive.Size = new System.Drawing.Size(136, 19);
            this.chkIsFreightArrive.TabIndex = 375;
            // 
            // labReturnDate
            // 
            this.labReturnDate.Location = new System.Drawing.Point(16, 125);
            this.labReturnDate.Name = "labReturnDate";
            this.labReturnDate.Size = new System.Drawing.Size(63, 14);
            this.labReturnDate.TabIndex = 380;
            this.labReturnDate.Text = "ReturnDate";
            // 
            // dteReturnDate
            // 
            this.dteReturnDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsSheetInfo, "ReturnDate", true));
            this.dteReturnDate.EditValue = null;
            this.dteReturnDate.Location = new System.Drawing.Point(99, 122);
            this.dteReturnDate.Name = "dteReturnDate";
            this.dteReturnDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteReturnDate.Properties.Mask.EditMask = "";
            this.dteReturnDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteReturnDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteReturnDate.Size = new System.Drawing.Size(302, 21);
            this.dteReturnDate.TabIndex = 374;
            // 
            // txtRemark
            // 
            this.txtRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSheetInfo, "Remark", true));
            this.txtRemark.Location = new System.Drawing.Point(99, 181);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(302, 85);
            this.txtRemark.TabIndex = 376;
            // 
            // labRemark
            // 
            this.labRemark.Location = new System.Drawing.Point(16, 182);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(40, 14);
            this.labRemark.TabIndex = 379;
            this.labRemark.Text = "Remark";
            // 
            // labReceiptDate
            // 
            this.labReceiptDate.Location = new System.Drawing.Point(16, 99);
            this.labReceiptDate.Name = "labReceiptDate";
            this.labReceiptDate.Size = new System.Drawing.Size(67, 14);
            this.labReceiptDate.TabIndex = 378;
            this.labReceiptDate.Text = "ReceiptDate";
            // 
            // dteReceiptDate
            // 
            this.dteReceiptDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsSheetInfo, "ReceiptDate", true));
            this.dteReceiptDate.EditValue = null;
            this.dteReceiptDate.Location = new System.Drawing.Point(99, 96);
            this.dteReceiptDate.Name = "dteReceiptDate";
            this.dteReceiptDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteReceiptDate.Properties.Mask.EditMask = "";
            this.dteReceiptDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteReceiptDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteReceiptDate.Size = new System.Drawing.Size(302, 21);
            this.dteReceiptDate.TabIndex = 373;
            // 
            // labSheetNo
            // 
            this.labSheetNo.Location = new System.Drawing.Point(16, 21);
            this.labSheetNo.Name = "labSheetNo";
            this.labSheetNo.Size = new System.Drawing.Size(48, 14);
            this.labSheetNo.TabIndex = 377;
            this.labSheetNo.Text = "SheetNo";
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bsList;
            // 
            // dxErrorContainers
            // 
            this.dxErrorContainers.ContainerControl = this;
            // 
            // panelScroll
            // 
            this.panelScroll.AllowDrop = true;
            this.panelScroll.Controls.Add(this.panel1);
            this.panelScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelScroll.Location = new System.Drawing.Point(0, 26);
            this.panelScroll.Name = "panelScroll";
            this.panelScroll.Size = new System.Drawing.Size(807, 648);
            this.panelScroll.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBase);
            this.panel1.Controls.Add(this.gridControl1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 632);
            this.panel1.TabIndex = 4;
            // 
            // VerifiSheetEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelScroll);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "VerifiSheetEditPart";
            this.Size = new System.Drawing.Size(807, 674);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSheetInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            this.groupBase.ResumeLayout(false);
            this.groupBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpressNO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSheetNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsFreightArrive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReturnDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReturnDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReceiptDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReceiptDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorContainers)).EndInit();
            this.panelScroll.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private System.Windows.Forms.GroupBox groupBase;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorContainers;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarButtonItem barAdd;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraGrid.Columns.GridColumn colExpressNO;
        private DevExpress.XtraGrid.Columns.GridColumn colReturnDate;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private Panel panelScroll;
        private BindingSource bsSheetInfo;
        private Panel panel1;
        private DevExpress.XtraGrid.Columns.GridColumn colReceiptDate;
        private DevExpress.XtraGrid.Columns.GridColumn colSheetNo;
        private DevExpress.XtraEditors.LabelControl labCustomer;
        private DevExpress.XtraEditors.ButtonEdit txtCustomer;
        private DevExpress.XtraEditors.TextEdit txtExpressNO;
        private DevExpress.XtraEditors.LabelControl labExpressNO;
        private DevExpress.XtraEditors.TextEdit txtSheetNo;
        private DevExpress.XtraEditors.CheckEdit chkIsFreightArrive;
        private DevExpress.XtraEditors.LabelControl labReturnDate;
        private DevExpress.XtraEditors.DateEdit dteReturnDate;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraEditors.LabelControl labRemark;
        private DevExpress.XtraEditors.LabelControl labReceiptDate;
        private DevExpress.XtraEditors.DateEdit dteReceiptDate;
        private DevExpress.XtraEditors.LabelControl labSheetNo;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationNo;
    }
}
