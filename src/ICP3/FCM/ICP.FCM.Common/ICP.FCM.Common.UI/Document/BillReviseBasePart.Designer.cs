namespace ICP.FCM.Common.UI.Document
{
    partial class BillReviseBasePart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BillReviseBasePart));
            this.gvFees = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColBillID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColWay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageListType = new System.Windows.Forms.ImageList();
            this.gridColChargeCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColChargeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColIsAgent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColOldSumMoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColNewSumMoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColOldRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColNewRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColFeeIsState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reImgCmbUpdateState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.gridBill = new DevExpress.XtraGrid.GridControl();
            this.bsBills = new System.Windows.Forms.BindingSource();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.pnlCurrent = new DevExpress.XtraEditors.PanelControl();
            this.lblDisptDate = new System.Windows.Forms.Label();
            this.lblDisptDateValue = new System.Windows.Forms.Label();
            this.lblOIBusinessNo = new System.Windows.Forms.Label();
            this.lblOIBusinessNoValue = new System.Windows.Forms.Label();
            this.lblDisptUserValue = new System.Windows.Forms.Label();
            this.lblOEBusinessNo = new System.Windows.Forms.Label();
            this.lblDisptUser = new System.Windows.Forms.Label();
            this.lblOEBusinessNoValue = new System.Windows.Forms.Label();
            this.gcBillInfo = new DevExpress.XtraEditors.GroupControl();
            this.xtraSCBillInfo = new DevExpress.XtraEditors.XtraScrollableControl();
            ((System.ComponentModel.ISupportInitialize)(this.gvFees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reImgCmbUpdateState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsBills)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCurrent)).BeginInit();
            this.pnlCurrent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcBillInfo)).BeginInit();
            this.gcBillInfo.SuspendLayout();
            this.xtraSCBillInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // gvFees
            // 
            this.gvFees.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColID,
            this.gridColBillID,
            this.gridColWay,
            this.gridColChargeCode,
            this.gridColChargeName,
            this.gridColIsAgent,
            this.gridColOldSumMoney,
            this.gridColNewSumMoney,
            this.gridColOldRemark,
            this.gridColNewRemark,
            this.gridColFeeIsState});
            this.gvFees.GridControl = this.gridBill;
            this.gvFees.Name = "gvFees";
            this.gvFees.OptionsBehavior.Editable = false;
            this.gvFees.OptionsBehavior.ReadOnly = true;
            this.gvFees.OptionsView.ShowGroupPanel = false;
            this.gvFees.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvFees_CustomDrawCell);
            this.gvFees.CustomDrawEmptyForeground += new DevExpress.XtraGrid.Views.Base.CustomDrawEventHandler(this.gvFees_CustomDrawEmptyForeground);
            // 
            // gridColID
            // 
            this.gridColID.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColID.Caption = "ID";
            this.gridColID.FieldName = "ID";
            this.gridColID.Name = "gridColID";
            this.gridColID.OptionsColumn.AllowEdit = false;
            this.gridColID.OptionsColumn.AllowMove = false;
            this.gridColID.OptionsFilter.AllowAutoFilter = false;
            this.gridColID.OptionsFilter.AllowFilter = false;
            // 
            // gridColBillID
            // 
            this.gridColBillID.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColBillID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColBillID.Caption = "BillID";
            this.gridColBillID.FieldName = "BillID";
            this.gridColBillID.Name = "gridColBillID";
            this.gridColBillID.OptionsColumn.AllowEdit = false;
            this.gridColBillID.OptionsColumn.AllowMove = false;
            this.gridColBillID.OptionsFilter.AllowAutoFilter = false;
            this.gridColBillID.OptionsFilter.AllowFilter = false;
            // 
            // gridColWay
            // 
            this.gridColWay.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColWay.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColWay.Caption = "方向";
            this.gridColWay.ColumnEdit = this.repositoryItemImageComboBox2;
            this.gridColWay.FieldName = "Way";
            this.gridColWay.MaxWidth = 80;
            this.gridColWay.MinWidth = 60;
            this.gridColWay.Name = "gridColWay";
            this.gridColWay.OptionsColumn.AllowEdit = false;
            this.gridColWay.OptionsColumn.AllowMove = false;
            this.gridColWay.OptionsFilter.AllowAutoFilter = false;
            this.gridColWay.OptionsFilter.AllowFilter = false;
            this.gridColWay.Visible = true;
            this.gridColWay.VisibleIndex = 0;
            this.gridColWay.Width = 64;
            // 
            // repositoryItemImageComboBox2
            // 
            this.repositoryItemImageComboBox2.AutoHeight = false;
            this.repositoryItemImageComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox2.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("应收", 1, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("应付", 2, 0)});
            this.repositoryItemImageComboBox2.Name = "repositoryItemImageComboBox2";
            this.repositoryItemImageComboBox2.ReadOnly = true;
            this.repositoryItemImageComboBox2.SmallImages = this.imageListType;
            // 
            // imageListType
            // 
            this.imageListType.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListType.ImageStream")));
            this.imageListType.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListType.Images.SetKeyName(0, "-.png");
            this.imageListType.Images.SetKeyName(1, "+.png");
            this.imageListType.Images.SetKeyName(2, "-.png");
            this.imageListType.Images.SetKeyName(3, "+-.png");
            // 
            // gridColChargeCode
            // 
            this.gridColChargeCode.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColChargeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColChargeCode.Caption = "费用代码";
            this.gridColChargeCode.FieldName = "ChargeCode";
            this.gridColChargeCode.MaxWidth = 200;
            this.gridColChargeCode.MinWidth = 100;
            this.gridColChargeCode.Name = "gridColChargeCode";
            this.gridColChargeCode.OptionsColumn.AllowEdit = false;
            this.gridColChargeCode.OptionsColumn.AllowMove = false;
            this.gridColChargeCode.OptionsFilter.AllowAutoFilter = false;
            this.gridColChargeCode.OptionsFilter.AllowFilter = false;
            this.gridColChargeCode.Visible = true;
            this.gridColChargeCode.VisibleIndex = 1;
            this.gridColChargeCode.Width = 100;
            // 
            // gridColChargeName
            // 
            this.gridColChargeName.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColChargeName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColChargeName.Caption = "费用名称";
            this.gridColChargeName.FieldName = "ChargeName";
            this.gridColChargeName.MaxWidth = 300;
            this.gridColChargeName.MinWidth = 60;
            this.gridColChargeName.Name = "gridColChargeName";
            this.gridColChargeName.OptionsColumn.AllowEdit = false;
            this.gridColChargeName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColChargeName.OptionsColumn.AllowMove = false;
            this.gridColChargeName.OptionsColumn.ReadOnly = true;
            this.gridColChargeName.OptionsFilter.AllowAutoFilter = false;
            this.gridColChargeName.OptionsFilter.AllowFilter = false;
            this.gridColChargeName.Visible = true;
            this.gridColChargeName.VisibleIndex = 2;
            this.gridColChargeName.Width = 100;
            // 
            // gridColIsAgent
            // 
            this.gridColIsAgent.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColIsAgent.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColIsAgent.Caption = "代理";
            this.gridColIsAgent.ColumnEdit = this.repositoryItemCheckEdit2;
            this.gridColIsAgent.FieldName = "IsAgent";
            this.gridColIsAgent.MaxWidth = 60;
            this.gridColIsAgent.MinWidth = 50;
            this.gridColIsAgent.Name = "gridColIsAgent";
            this.gridColIsAgent.OptionsColumn.AllowEdit = false;
            this.gridColIsAgent.OptionsColumn.AllowMove = false;
            this.gridColIsAgent.OptionsFilter.AllowAutoFilter = false;
            this.gridColIsAgent.OptionsFilter.AllowFilter = false;
            this.gridColIsAgent.Visible = true;
            this.gridColIsAgent.VisibleIndex = 3;
            this.gridColIsAgent.Width = 59;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            this.repositoryItemCheckEdit2.ValueChecked = 1;
            this.repositoryItemCheckEdit2.ValueUnchecked = 0;
            // 
            // gridColOldSumMoney
            // 
            this.gridColOldSumMoney.AppearanceCell.Options.UseTextOptions = true;
            this.gridColOldSumMoney.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColOldSumMoney.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColOldSumMoney.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColOldSumMoney.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColOldSumMoney.Caption = "旧金额";
            this.gridColOldSumMoney.CustomizationCaption = "旧金额";
            this.gridColOldSumMoney.FieldName = "OldSumMoney";
            this.gridColOldSumMoney.MinWidth = 200;
            this.gridColOldSumMoney.Name = "gridColOldSumMoney";
            this.gridColOldSumMoney.OptionsColumn.AllowEdit = false;
            this.gridColOldSumMoney.OptionsColumn.AllowMove = false;
            this.gridColOldSumMoney.OptionsFilter.AllowAutoFilter = false;
            this.gridColOldSumMoney.OptionsFilter.AllowFilter = false;
            this.gridColOldSumMoney.Visible = true;
            this.gridColOldSumMoney.VisibleIndex = 4;
            this.gridColOldSumMoney.Width = 200;
            // 
            // gridColNewSumMoney
            // 
            this.gridColNewSumMoney.AppearanceCell.Options.UseTextOptions = true;
            this.gridColNewSumMoney.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColNewSumMoney.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColNewSumMoney.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColNewSumMoney.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColNewSumMoney.Caption = "新金额";
            this.gridColNewSumMoney.CustomizationCaption = "新金额";
            this.gridColNewSumMoney.FieldName = "NewSumMoney";
            this.gridColNewSumMoney.MinWidth = 200;
            this.gridColNewSumMoney.Name = "gridColNewSumMoney";
            this.gridColNewSumMoney.OptionsColumn.AllowEdit = false;
            this.gridColNewSumMoney.OptionsColumn.AllowMove = false;
            this.gridColNewSumMoney.OptionsFilter.AllowAutoFilter = false;
            this.gridColNewSumMoney.OptionsFilter.AllowFilter = false;
            this.gridColNewSumMoney.Visible = true;
            this.gridColNewSumMoney.VisibleIndex = 5;
            this.gridColNewSumMoney.Width = 200;
            // 
            // gridColOldRemark
            // 
            this.gridColOldRemark.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColOldRemark.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColOldRemark.Caption = "新备注";
            this.gridColOldRemark.CustomizationCaption = "新备注";
            this.gridColOldRemark.FieldName = "OldRemark";
            this.gridColOldRemark.MinWidth = 100;
            this.gridColOldRemark.Name = "gridColOldRemark";
            this.gridColOldRemark.OptionsColumn.AllowEdit = false;
            this.gridColOldRemark.OptionsColumn.AllowMove = false;
            this.gridColOldRemark.OptionsFilter.AllowAutoFilter = false;
            this.gridColOldRemark.OptionsFilter.AllowFilter = false;
            this.gridColOldRemark.Visible = true;
            this.gridColOldRemark.VisibleIndex = 6;
            this.gridColOldRemark.Width = 100;
            // 
            // gridColNewRemark
            // 
            this.gridColNewRemark.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColNewRemark.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColNewRemark.Caption = "旧备注";
            this.gridColNewRemark.CustomizationCaption = "旧备注";
            this.gridColNewRemark.FieldName = "NewRemark";
            this.gridColNewRemark.MinWidth = 100;
            this.gridColNewRemark.Name = "gridColNewRemark";
            this.gridColNewRemark.OptionsColumn.AllowEdit = false;
            this.gridColNewRemark.OptionsColumn.AllowMove = false;
            this.gridColNewRemark.OptionsFilter.AllowAutoFilter = false;
            this.gridColNewRemark.OptionsFilter.AllowFilter = false;
            this.gridColNewRemark.Visible = true;
            this.gridColNewRemark.VisibleIndex = 7;
            this.gridColNewRemark.Width = 100;
            // 
            // gridColFeeIsState
            // 
            this.gridColFeeIsState.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColFeeIsState.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColFeeIsState.Caption = "修改状态";
            this.gridColFeeIsState.ColumnEdit = this.reImgCmbUpdateState;
            this.gridColFeeIsState.FieldName = "UpdateState";
            this.gridColFeeIsState.MaxWidth = 80;
            this.gridColFeeIsState.MinWidth = 60;
            this.gridColFeeIsState.Name = "gridColFeeIsState";
            this.gridColFeeIsState.OptionsColumn.AllowEdit = false;
            this.gridColFeeIsState.OptionsColumn.AllowMove = false;
            this.gridColFeeIsState.OptionsFilter.AllowAutoFilter = false;
            this.gridColFeeIsState.OptionsFilter.AllowFilter = false;
            this.gridColFeeIsState.Visible = true;
            this.gridColFeeIsState.VisibleIndex = 8;
            this.gridColFeeIsState.Width = 60;
            // 
            // reImgCmbUpdateState
            // 
            this.reImgCmbUpdateState.AutoHeight = false;
            this.reImgCmbUpdateState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.reImgCmbUpdateState.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Original", 0, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Added", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Modified", 2, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Deleteed", 3, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("SelfAdd", 4, -1)});
            this.reImgCmbUpdateState.Name = "reImgCmbUpdateState";
            // 
            // gridBill
            // 
            this.gridBill.DataSource = this.bsBills;
            this.gridBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridBill.Location = new System.Drawing.Point(0, 0);
            this.gridBill.MainView = this.gvFees;
            this.gridBill.Name = "gridBill";
            this.gridBill.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox2,
            this.repositoryItemCheckEdit2,
            this.reImgCmbUpdateState});
            this.gridBill.Size = new System.Drawing.Size(895, 534);
            this.gridBill.TabIndex = 7;
            this.gridBill.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvFees});
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "应付.png");
            this.imageCollection1.Images.SetKeyName(1, "应收.png");
            // 
            // pnlCurrent
            // 
            this.pnlCurrent.Controls.Add(this.lblDisptDate);
            this.pnlCurrent.Controls.Add(this.lblDisptDateValue);
            this.pnlCurrent.Controls.Add(this.lblOIBusinessNo);
            this.pnlCurrent.Controls.Add(this.lblOIBusinessNoValue);
            this.pnlCurrent.Controls.Add(this.lblDisptUserValue);
            this.pnlCurrent.Controls.Add(this.lblOEBusinessNo);
            this.pnlCurrent.Controls.Add(this.lblDisptUser);
            this.pnlCurrent.Controls.Add(this.lblOEBusinessNoValue);
            this.pnlCurrent.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCurrent.Location = new System.Drawing.Point(0, 0);
            this.pnlCurrent.Name = "pnlCurrent";
            this.pnlCurrent.Size = new System.Drawing.Size(899, 1);
            this.pnlCurrent.TabIndex = 9;
            // 
            // lblDisptDate
            // 
            this.lblDisptDate.AutoSize = true;
            this.lblDisptDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDisptDate.Location = new System.Drawing.Point(650, 4);
            this.lblDisptDate.Name = "lblDisptDate";
            this.lblDisptDate.Size = new System.Drawing.Size(67, 14);
            this.lblDisptDate.TabIndex = 7;
            this.lblDisptDate.Text = "申请时间：";
            // 
            // lblDisptDateValue
            // 
            this.lblDisptDateValue.AutoSize = true;
            this.lblDisptDateValue.BackColor = System.Drawing.Color.Transparent;
            this.lblDisptDateValue.Location = new System.Drawing.Point(737, 4);
            this.lblDisptDateValue.Name = "lblDisptDateValue";
            this.lblDisptDateValue.Size = new System.Drawing.Size(129, 14);
            this.lblDisptDateValue.TabIndex = 8;
            this.lblDisptDateValue.Text = "2013-06-14  12:23:34";
            // 
            // lblOIBusinessNo
            // 
            this.lblOIBusinessNo.AutoSize = true;
            this.lblOIBusinessNo.BackColor = System.Drawing.Color.Transparent;
            this.lblOIBusinessNo.Location = new System.Drawing.Point(4, 4);
            this.lblOIBusinessNo.Name = "lblOIBusinessNo";
            this.lblOIBusinessNo.Size = new System.Drawing.Size(79, 14);
            this.lblOIBusinessNo.TabIndex = 1;
            this.lblOIBusinessNo.Text = "港后业务号：";
            // 
            // lblOIBusinessNoValue
            // 
            this.lblOIBusinessNoValue.AutoSize = true;
            this.lblOIBusinessNoValue.BackColor = System.Drawing.Color.Transparent;
            this.lblOIBusinessNoValue.Location = new System.Drawing.Point(106, 4);
            this.lblOIBusinessNoValue.Name = "lblOIBusinessNoValue";
            this.lblOIBusinessNoValue.Size = new System.Drawing.Size(98, 14);
            this.lblOIBusinessNoValue.TabIndex = 2;
            this.lblOIBusinessNoValue.Text = "SZ20130609136";
            // 
            // lblDisptUserValue
            // 
            this.lblDisptUserValue.AutoSize = true;
            this.lblDisptUserValue.BackColor = System.Drawing.Color.Transparent;
            this.lblDisptUserValue.Location = new System.Drawing.Point(581, 4);
            this.lblDisptUserValue.Name = "lblDisptUserValue";
            this.lblDisptUserValue.Size = new System.Drawing.Size(24, 14);
            this.lblDisptUserValue.TabIndex = 6;
            this.lblDisptUserValue.Text = "joe";
            // 
            // lblOEBusinessNo
            // 
            this.lblOEBusinessNo.AutoSize = true;
            this.lblOEBusinessNo.BackColor = System.Drawing.Color.Transparent;
            this.lblOEBusinessNo.Location = new System.Drawing.Point(240, 4);
            this.lblOEBusinessNo.Name = "lblOEBusinessNo";
            this.lblOEBusinessNo.Size = new System.Drawing.Size(79, 14);
            this.lblOEBusinessNo.TabIndex = 3;
            this.lblOEBusinessNo.Text = "港前业务号：";
            // 
            // lblDisptUser
            // 
            this.lblDisptUser.AutoSize = true;
            this.lblDisptUser.BackColor = System.Drawing.Color.Transparent;
            this.lblDisptUser.Location = new System.Drawing.Point(504, 4);
            this.lblDisptUser.Name = "lblDisptUser";
            this.lblDisptUser.Size = new System.Drawing.Size(55, 14);
            this.lblDisptUser.TabIndex = 5;
            this.lblDisptUser.Text = "申请人：";
            // 
            // lblOEBusinessNoValue
            // 
            this.lblOEBusinessNoValue.AutoSize = true;
            this.lblOEBusinessNoValue.BackColor = System.Drawing.Color.Transparent;
            this.lblOEBusinessNoValue.Location = new System.Drawing.Point(349, 5);
            this.lblOEBusinessNoValue.Name = "lblOEBusinessNoValue";
            this.lblOEBusinessNoValue.Size = new System.Drawing.Size(124, 14);
            this.lblOEBusinessNoValue.TabIndex = 4;
            this.lblOEBusinessNoValue.Text = "SZ201306091fdsaf36";
            // 
            // gcBillInfo
            // 
            this.gcBillInfo.Controls.Add(this.xtraSCBillInfo);
            this.gcBillInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcBillInfo.Location = new System.Drawing.Point(0, 1);
            this.gcBillInfo.Name = "gcBillInfo";
            this.gcBillInfo.Size = new System.Drawing.Size(899, 559);
            this.gcBillInfo.TabIndex = 10;
            this.gcBillInfo.Text = "账单信息";
            // 
            // xtraSCBillInfo
            // 
            this.xtraSCBillInfo.Controls.Add(this.gridBill);
            this.xtraSCBillInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraSCBillInfo.Location = new System.Drawing.Point(2, 23);
            this.xtraSCBillInfo.Name = "xtraSCBillInfo";
            this.xtraSCBillInfo.Size = new System.Drawing.Size(895, 534);
            this.xtraSCBillInfo.TabIndex = 0;
            // 
            // BillReviseBasePart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcBillInfo);
            this.Controls.Add(this.pnlCurrent);
            this.Name = "BillReviseBasePart";
            this.Size = new System.Drawing.Size(899, 560);
            this.Load += new System.EventHandler(this.OEBillReviseBasePart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvFees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reImgCmbUpdateState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsBills)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCurrent)).EndInit();
            this.pnlCurrent.ResumeLayout(false);
            this.pnlCurrent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcBillInfo)).EndInit();
            this.gcBillInfo.ResumeLayout(false);
            this.xtraSCBillInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlCurrent;
        private System.Windows.Forms.Label lblDisptDate;
        private System.Windows.Forms.Label lblDisptDateValue;
        private System.Windows.Forms.Label lblOIBusinessNo;
        private System.Windows.Forms.Label lblOIBusinessNoValue;
        private System.Windows.Forms.Label lblDisptUserValue;
        private System.Windows.Forms.Label lblOEBusinessNo;
        private System.Windows.Forms.Label lblDisptUser;
        public System.Windows.Forms.Label lblOEBusinessNoValue;
        private DevExpress.XtraEditors.GroupControl gcBillInfo;
        private DevExpress.XtraEditors.XtraScrollableControl xtraSCBillInfo;


        private DevExpress.XtraGrid.GridControl gridBill;


        private System.Windows.Forms.BindingSource bsBills;
        private DevExpress.XtraGrid.Views.Grid.GridView gvFees;
        private DevExpress.XtraGrid.Columns.GridColumn gridColID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColBillID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColWay;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox2;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColChargeCode;
        private DevExpress.XtraGrid.Columns.GridColumn gridColChargeName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColIsAgent;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColOldSumMoney;
        private DevExpress.XtraGrid.Columns.GridColumn gridColNewSumMoney;
        private DevExpress.XtraGrid.Columns.GridColumn gridColNewRemark;
        private DevExpress.XtraGrid.Columns.GridColumn gridColOldRemark;
        private DevExpress.XtraGrid.Columns.GridColumn gridColFeeIsState;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox reImgCmbUpdateState;
        private System.Windows.Forms.ImageList imageListType;
    }
}
