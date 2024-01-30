namespace ICP.FCM.OceanExport.UI.Container
{
    partial class ContainerListPart
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
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsContainer = new System.Windows.Forms.BindingSource(this.components);
            this.gvCtn = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colRelation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkRelation = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rTextEditUpper = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colSealNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShippingOrderNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rspinEditInt = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colWeight = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rspinEditFloat = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colMeasurement = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMarks = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.colCommodity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsSOC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsPartOf = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labCtnNo = new DevExpress.XtraEditors.LabelControl();
            this.rdoSelectMode = new DevExpress.XtraEditors.RadioGroup();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.txtFind = new DevExpress.XtraEditors.ButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkRelation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rTextEditUpper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rspinEditInt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rspinEditFloat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoSelectMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFind.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsContainer;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 61);
            this.gcMain.MainView = this.gvCtn;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbType,
            this.rspinEditInt,
            this.rspinEditFloat,
            this.rTextEditUpper,
            this.rchkRelation,
            this.rMemoExEdit1});
            this.gcMain.Size = new System.Drawing.Size(1100, 503);
            this.gcMain.TabIndex = 3;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCtn});
            // 
            // bsContainer
            // 
            this.bsContainer.DataSource = typeof(ICP.FCM.OceanExport.ServiceInterface.DataObjects.OceanBLContainerList);
            this.bsContainer.PositionChanged += new System.EventHandler(this.bsContainer_PositionChanged);
            // 
            // gvCtn
            // 
            this.gvCtn.ChildGridLevelName = "Level1";
            this.gvCtn.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRelation,
            this.colNo,
            this.colType,
            this.colSealNo,
            this.colQuantity,
            this.colWeight,
            this.colMeasurement,
            this.colShippingOrderNo,
            this.colMarks,
            this.colCommodity,
            this.colIsSOC,
            this.colIsPartOf});
            this.gvCtn.DetailTabHeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Right;
            this.gvCtn.GridControl = this.gcMain;
            this.gvCtn.LevelIndent = 0;
            this.gvCtn.Name = "gvCtn";
            this.gvCtn.OptionsNavigation.EnterMoveNextColumn = true;
            this.gvCtn.OptionsSelection.MultiSelect = true;
            this.gvCtn.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvCtn.OptionsView.EnableAppearanceEvenRow = true;
            this.gvCtn.OptionsView.ShowDetailButtons = false;
            this.gvCtn.OptionsView.ShowGroupPanel = false;
            this.gvCtn.PreviewFieldName = "Date";
            this.gvCtn.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvCtn_CellValueChanged);
            // 
            // colRelation
            // 
            this.colRelation.Caption = "Relation";
            this.colRelation.ColumnEdit = this.rchkRelation;
            this.colRelation.FieldName = "Relation";
            this.colRelation.Name = "colRelation";
            this.colRelation.Visible = true;
            this.colRelation.VisibleIndex = 0;
            this.colRelation.Width = 50;
            // 
            // rchkRelation
            // 
            this.rchkRelation.AutoHeight = false;
            this.rchkRelation.Name = "rchkRelation";
            // 
            // colNo
            // 
            this.colNo.Caption = "No";
            this.colNo.ColumnEdit = this.rTextEditUpper;
            this.colNo.FieldName = "No";
            this.colNo.Name = "colNo";
            this.colNo.Visible = true;
            this.colNo.VisibleIndex = 1;
            this.colNo.Width = 122;
            // 
            // rTextEditUpper
            // 
            this.rTextEditUpper.AutoHeight = false;
            this.rTextEditUpper.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.rTextEditUpper.Name = "rTextEditUpper";
            // 
            // colType
            // 
            this.colType.Caption = "Type";
            this.colType.ColumnEdit = this.cmbType;
            this.colType.FieldName = "TypeID";
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 2;
            this.colType.Width = 81;
            // 
            // cmbType
            // 
            this.cmbType.AutoHeight = false;
            this.cmbType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Name = "cmbType";
            // 
            // colSealNo
            // 
            this.colSealNo.Caption = "SealNo";
            this.colSealNo.FieldName = "SealNo";
            this.colSealNo.Name = "colSealNo";
            this.colSealNo.Visible = true;
            this.colSealNo.VisibleIndex = 3;
            this.colSealNo.ColumnEdit = this.rTextEditUpper;
            this.colSealNo.Width = 98;
            // 
            // colShippingOrderNo
            // 
            this.colShippingOrderNo.Caption = "S/O NO";
            this.colShippingOrderNo.FieldName = "ShippingOrderNo";
            this.colShippingOrderNo.Name = "colShippingOrderNo";
            this.colShippingOrderNo.Visible = true;
            this.colShippingOrderNo.VisibleIndex = 7;
            this.colShippingOrderNo.ColumnEdit = this.rTextEditUpper;
            this.colShippingOrderNo.Width = 107;
            // 
            // colQuantity
            // 
            this.colQuantity.Caption = "Qty";
            this.colQuantity.ColumnEdit = this.rspinEditInt;
            this.colQuantity.FieldName = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.Visible = true;
            this.colQuantity.VisibleIndex = 4;
            this.colQuantity.Width = 98;
            // 
            // rspinEditInt
            // 
            this.rspinEditInt.AutoHeight = false;
            this.rspinEditInt.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.rspinEditInt.IsFloatValue = false;
            this.rspinEditInt.Mask.EditMask = "N00";
            this.rspinEditInt.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.rspinEditInt.Name = "rspinEditInt";
            // 
            // colWeight
            // 
            this.colWeight.Caption = "Weight";
            this.colWeight.ColumnEdit = this.rspinEditFloat;
            this.colWeight.FieldName = "Weight";
            this.colWeight.Name = "colWeight";
            this.colWeight.Visible = true;
            this.colWeight.VisibleIndex = 5;
            this.colWeight.Width = 96;
            // 
            // rspinEditFloat
            // 
            this.rspinEditFloat.AutoHeight = false;
            this.rspinEditFloat.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.rspinEditFloat.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.rspinEditFloat.Name = "rspinEditFloat";
            // 
            // colMeasurement
            // 
            this.colMeasurement.Caption = "Measurement";
            this.colMeasurement.ColumnEdit = this.rspinEditFloat;
            this.colMeasurement.FieldName = "Measurement";
            this.colMeasurement.Name = "colMeasurement";
            this.colMeasurement.Visible = true;
            this.colMeasurement.VisibleIndex = 6;
            this.colMeasurement.Width = 101;
            // 
            // colMarks
            // 
            this.colMarks.ColumnEdit = this.rMemoExEdit1;
            this.colMarks.FieldName = "Marks";
            this.colMarks.Name = "colMarks";
            this.colMarks.Visible = true;
            this.colMarks.VisibleIndex = 8;
            this.colMarks.ColumnEdit = this.rTextEditUpper;
            this.colMarks.Width = 83;
            // 
            // rMemoExEdit1
            // 
            this.rMemoExEdit1.AutoHeight = false;
            this.rMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rMemoExEdit1.MaxLength = 500;
            this.rMemoExEdit1.Name = "rMemoExEdit1";
            this.rMemoExEdit1.ShowIcon = false;
            // 
            // colCommodity
            // 
            this.colCommodity.Caption = "Commodity";
            this.colCommodity.FieldName = "Commodity";
            this.colCommodity.Name = "colCommodity";
            this.colCommodity.Visible = true;
            this.colCommodity.VisibleIndex = 9;
            this.colCommodity.ColumnEdit = this.rTextEditUpper;
            this.colCommodity.Width = 100;
            // 
            // colIsSOC
            // 
            this.colIsSOC.Caption = "IsSOC";
            this.colIsSOC.FieldName = "IsSOC";
            this.colIsSOC.Name = "colIsSOC";
            this.colIsSOC.Visible = true;
            this.colIsSOC.VisibleIndex = 10;
            this.colIsSOC.Width = 64;
            // 
            // colIsPartOf
            // 
            this.colIsPartOf.Caption = "IsPartOf";
            this.colIsPartOf.FieldName = "IsPartOf";
            this.colIsPartOf.Name = "colIsPartOf";
            this.colIsPartOf.Visible = true;
            this.colIsPartOf.VisibleIndex = 11;
            this.colIsPartOf.Width = 76;
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
            this.barAdd,
            this.barDelete});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 8;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAdd, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barAdd
            // 
            this.barAdd.Caption = "&Add";
            this.barAdd.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Add_16;
            this.barAdd.Id = 0;
            this.barAdd.Name = "barAdd";
            this.barAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdd_ItemClick);
            // 
            // barDelete
            // 
            this.barDelete.Caption = "&Delete";
            this.barDelete.Glyph = global::ICP.FCM.OceanExport.UI.Properties.Resources.Delete_16;
            this.barDelete.Id = 1;
            this.barDelete.Name = "barDelete";
            this.barDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1100, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 600);
            this.barDockControlBottom.Size = new System.Drawing.Size(1100, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 574);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1100, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 574);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(1010, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(919, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 564);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1100, 36);
            this.panel1.TabIndex = 10;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labCtnNo);
            this.panelControl1.Controls.Add(this.rdoSelectMode);
            this.panelControl1.Controls.Add(this.btnNext);
            this.panelControl1.Controls.Add(this.txtFind);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 26);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1100, 35);
            this.panelControl1.TabIndex = 20;
            // 
            // labCtnNo
            // 
            this.labCtnNo.Location = new System.Drawing.Point(280, 8);
            this.labCtnNo.Name = "labCtnNo";
            this.labCtnNo.Size = new System.Drawing.Size(67, 14);
            this.labCtnNo.TabIndex = 8;
            this.labCtnNo.Text = "ContainerNo";
            // 
            // rdoSelectMode
            // 
            this.rdoSelectMode.Location = new System.Drawing.Point(5, 4);
            this.rdoSelectMode.MenuManager = this.barManager1;
            this.rdoSelectMode.Name = "rdoSelectMode";
            this.rdoSelectMode.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "ALL"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Selected"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Un Selected")});
            this.rdoSelectMode.Size = new System.Drawing.Size(269, 23);
            this.rdoSelectMode.TabIndex = 7;
            this.rdoSelectMode.SelectedIndexChanged += new System.EventHandler(this.rdoSelectMode_SelectedIndexChanged);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Location = new System.Drawing.Point(1011, 5);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(68, 21);
            this.btnNext.TabIndex = 6;
            this.btnNext.Text = "&Next";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // txtFind
            // 
            this.txtFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFind.Location = new System.Drawing.Point(355, 5);
            this.txtFind.Name = "txtFind";
            this.txtFind.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.txtFind.Size = new System.Drawing.Size(639, 21);
            this.txtFind.TabIndex = 0;
            this.txtFind.TabStop = false;
            this.txtFind.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtFind_ButtonClick);
            this.txtFind.TextChanged += new System.EventHandler(this.txtFind_TextChanged);
            // 
            // ContainerListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ContainerListPart";
            this.Size = new System.Drawing.Size(1100, 600);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsContainer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkRelation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rTextEditUpper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rspinEditInt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rspinEditFloat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoSelectMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFind.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCtn;
        private DevExpress.XtraGrid.Columns.GridColumn colNo;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colSealNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCommodity;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colWeight;
        private DevExpress.XtraGrid.Columns.GridColumn colMeasurement;
        private DevExpress.XtraGrid.Columns.GridColumn colIsSOC;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbType;
        private DevExpress.XtraBars.BarButtonItem barAdd;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rspinEditInt;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rspinEditFloat;
        private DevExpress.XtraGrid.Columns.GridColumn colIsPartOf;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rTextEditUpper;
        private System.Windows.Forms.BindingSource bsContainer;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraEditors.ButtonEdit txtFind;
        private DevExpress.XtraGrid.Columns.GridColumn colRelation;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkRelation;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit rMemoExEdit1;
        private DevExpress.XtraEditors.RadioGroup rdoSelectMode;
        private DevExpress.XtraEditors.LabelControl labCtnNo;
        private DevExpress.XtraGrid.Columns.GridColumn colShippingOrderNo;
        private DevExpress.XtraGrid.Columns.GridColumn colMarks;
    }
}
