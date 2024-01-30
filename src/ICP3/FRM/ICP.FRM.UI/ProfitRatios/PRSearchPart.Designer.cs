namespace ICP.FRM.UI.ProfitRatios
{
    partial class PRSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PRSearchPart));
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panelBase = new System.Windows.Forms.Panel();
            this.checkIsNonContractNo = new DevExpress.XtraEditors.CheckEdit();
            this.cbcbPlaceOfDeliveryID = new ICP.Common.UI.Controls.CheckBoxComboBoxLocations();
            this.cbcbPODID = new ICP.Common.UI.Controls.CheckBoxComboBoxLocations();
            this.cbcbPOLID = new ICP.Common.UI.Controls.CheckBoxComboBoxLocations();
            this.txtOperationNo = new DevExpress.XtraEditors.TextEdit();
            this.txtBookingNo = new DevExpress.XtraEditors.TextEdit();
            this.txtContractNo = new DevExpress.XtraEditors.TextEdit();
            this.cbcbVessel = new ICP.Common.UI.Controls.CheckBoxComboBoxVessel();
            this.cbcbCarrier = new ICP.Common.UI.Controls.CheckBoxComboBoxCarrier();
            this.labCarrier = new DevExpress.XtraEditors.LabelControl();
            this.tccShippingLine = new ICP.Common.UI.Controls.TreeCheckControlShippingLine();
            this.labOperationNo = new DevExpress.XtraEditors.LabelControl();
            this.chkcmbCompany = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.labBookingNo = new DevExpress.XtraEditors.LabelControl();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.labContractNo = new DevExpress.XtraEditors.LabelControl();
            this.labPlaceOfDelivery = new DevExpress.XtraEditors.LabelControl();
            this.labPOD = new DevExpress.XtraEditors.LabelControl();
            this.labPOL = new DevExpress.XtraEditors.LabelControl();
            this.labVessel = new DevExpress.XtraEditors.LabelControl();
            this.labShippingLine = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panelDate = new System.Windows.Forms.Panel();
            this.dmcGateInRange = new ICP.Framework.ClientComponents.Controls.DateMonthControl();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.nbarDate = new DevExpress.XtraNavBar.NavBarGroup();
            this.pcBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panelForm = new DevExpress.XtraEditors.PanelControl();
            this.panelFill = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            this.panelBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsNonContractNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperationNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBookingNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContractNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkcmbCompany.Properties)).BeginInit();
            this.navBarGroupControlContainer1.SuspendLayout();
            this.panelDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcBottom)).BeginInit();
            this.pcBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelForm)).BeginInit();
            this.panelForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelFill)).BeginInit();
            this.panelFill.SuspendLayout();
            this.SuspendLayout();
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbarBase;
            this.navBarControl1.Controls.Add(this.navBarGroupBase);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.ExplorerBarGroupInterval = 2;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 2;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarBase,
            this.nbarDate});
            this.navBarControl1.Location = new System.Drawing.Point(2, 5);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(264, 579);
            this.navBarControl1.SkinExplorerBarViewScrollStyle = DevExpress.XtraNavBar.SkinExplorerBarViewScrollStyle.ScrollBar;
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // nbarBase
            // 
            this.nbarBase.Caption = "Base";
            this.nbarBase.ControlContainer = this.navBarGroupBase;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 285;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.panelBase);
            this.navBarGroupBase.Name = "navBarGroupBase";
            this.navBarGroupBase.Size = new System.Drawing.Size(256, 283);
            this.navBarGroupBase.TabIndex = 0;
            // 
            // panelBase
            // 
            this.panelBase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panelBase.Controls.Add(this.checkIsNonContractNo);
            this.panelBase.Controls.Add(this.cbcbPlaceOfDeliveryID);
            this.panelBase.Controls.Add(this.cbcbPODID);
            this.panelBase.Controls.Add(this.cbcbPOLID);
            this.panelBase.Controls.Add(this.txtOperationNo);
            this.panelBase.Controls.Add(this.txtBookingNo);
            this.panelBase.Controls.Add(this.txtContractNo);
            this.panelBase.Controls.Add(this.cbcbVessel);
            this.panelBase.Controls.Add(this.cbcbCarrier);
            this.panelBase.Controls.Add(this.labCarrier);
            this.panelBase.Controls.Add(this.tccShippingLine);
            this.panelBase.Controls.Add(this.labOperationNo);
            this.panelBase.Controls.Add(this.chkcmbCompany);
            this.panelBase.Controls.Add(this.labBookingNo);
            this.panelBase.Controls.Add(this.labCompany);
            this.panelBase.Controls.Add(this.labContractNo);
            this.panelBase.Controls.Add(this.labPlaceOfDelivery);
            this.panelBase.Controls.Add(this.labPOD);
            this.panelBase.Controls.Add(this.labPOL);
            this.panelBase.Controls.Add(this.labVessel);
            this.panelBase.Controls.Add(this.labShippingLine);
            this.panelBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBase.Location = new System.Drawing.Point(0, 0);
            this.panelBase.Name = "panelBase";
            this.panelBase.Size = new System.Drawing.Size(256, 283);
            this.panelBase.TabIndex = 0;
            // 
            // checkIsNonContractNo
            // 
            this.checkIsNonContractNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkIsNonContractNo.Location = new System.Drawing.Point(224, 197);
            this.checkIsNonContractNo.Name = "checkIsNonContractNo";
            this.checkIsNonContractNo.Properties.Caption = "";
            this.checkIsNonContractNo.Size = new System.Drawing.Size(18, 19);
            this.checkIsNonContractNo.TabIndex = 40;
            // 
            // cbcbPlaceOfDeliveryID
            // 
            this.cbcbPlaceOfDeliveryID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbcbPlaceOfDeliveryID.DataSource = null;
            this.cbcbPlaceOfDeliveryID.DisplayMember = "";
            this.cbcbPlaceOfDeliveryID.Location = new System.Drawing.Point(105, 172);
            this.cbcbPlaceOfDeliveryID.Name = "cbcbPlaceOfDeliveryID";
            this.cbcbPlaceOfDeliveryID.NullText = "";
            this.cbcbPlaceOfDeliveryID.Size = new System.Drawing.Size(137, 21);
            this.cbcbPlaceOfDeliveryID.TabIndex = 6;
            this.cbcbPlaceOfDeliveryID.ValueMember = "";
            // 
            // cbcbPODID
            // 
            this.cbcbPODID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbcbPODID.DataSource = null;
            this.cbcbPODID.DisplayMember = "";
            this.cbcbPODID.Location = new System.Drawing.Point(105, 146);
            this.cbcbPODID.Name = "cbcbPODID";
            this.cbcbPODID.NullText = "";
            this.cbcbPODID.Size = new System.Drawing.Size(137, 21);
            this.cbcbPODID.TabIndex = 5;
            this.cbcbPODID.ValueMember = "";
            // 
            // cbcbPOLID
            // 
            this.cbcbPOLID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbcbPOLID.DataSource = null;
            this.cbcbPOLID.DisplayMember = "";
            this.cbcbPOLID.Location = new System.Drawing.Point(105, 120);
            this.cbcbPOLID.Name = "cbcbPOLID";
            this.cbcbPOLID.NullText = "";
            this.cbcbPOLID.Size = new System.Drawing.Size(137, 21);
            this.cbcbPOLID.TabIndex = 4;
            this.cbcbPOLID.ValueMember = "";
            // 
            // txtOperationNo
            // 
            this.txtOperationNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOperationNo.Location = new System.Drawing.Point(105, 249);
            this.txtOperationNo.Name = "txtOperationNo";
            this.txtOperationNo.Size = new System.Drawing.Size(137, 21);
            this.txtOperationNo.TabIndex = 7;
            // 
            // txtBookingNo
            // 
            this.txtBookingNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBookingNo.Location = new System.Drawing.Point(105, 222);
            this.txtBookingNo.Name = "txtBookingNo";
            this.txtBookingNo.Size = new System.Drawing.Size(137, 21);
            this.txtBookingNo.TabIndex = 7;
            // 
            // txtContractNo
            // 
            this.txtContractNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContractNo.Location = new System.Drawing.Point(105, 195);
            this.txtContractNo.Name = "txtContractNo";
            this.txtContractNo.Size = new System.Drawing.Size(117, 21);
            this.txtContractNo.TabIndex = 7;
            // 
            // cbcbVessel
            // 
            this.cbcbVessel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbcbVessel.DataSource = null;
            this.cbcbVessel.DisplayMember = "";
            this.cbcbVessel.Location = new System.Drawing.Point(105, 93);
            this.cbcbVessel.Name = "cbcbVessel";
            this.cbcbVessel.NullText = "";
            this.cbcbVessel.Size = new System.Drawing.Size(137, 21);
            this.cbcbVessel.TabIndex = 3;
            this.cbcbVessel.ValueMember = "";
            // 
            // cbcbCarrier
            // 
            this.cbcbCarrier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbcbCarrier.DataSource = null;
            this.cbcbCarrier.DisplayMember = "";
            this.cbcbCarrier.Location = new System.Drawing.Point(105, 40);
            this.cbcbCarrier.Name = "cbcbCarrier";
            this.cbcbCarrier.NullText = "";
            this.cbcbCarrier.Size = new System.Drawing.Size(137, 21);
            this.cbcbCarrier.TabIndex = 1;
            this.cbcbCarrier.ValueMember = "";
            // 
            // labCarrier
            // 
            this.labCarrier.Location = new System.Drawing.Point(13, 42);
            this.labCarrier.Name = "labCarrier";
            this.labCarrier.Size = new System.Drawing.Size(34, 14);
            this.labCarrier.TabIndex = 12;
            this.labCarrier.Text = "Carrier";
            // 
            // tccShippingLine
            // 
            this.tccShippingLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tccShippingLine.EditText = "";
            this.tccShippingLine.EditValue = ((System.Collections.Generic.List<System.Guid>)(resources.GetObject("tccShippingLine.EditValue")));
            this.tccShippingLine.Location = new System.Drawing.Point(105, 67);
            this.tccShippingLine.Name = "tccShippingLine";
            this.tccShippingLine.ReadOnly = false;
            this.tccShippingLine.Size = new System.Drawing.Size(137, 21);
            this.tccShippingLine.SplitString = ",";
            this.tccShippingLine.TabIndex = 2;
            // 
            // labOperationNo
            // 
            this.labOperationNo.Location = new System.Drawing.Point(13, 252);
            this.labOperationNo.Name = "labOperationNo";
            this.labOperationNo.Size = new System.Drawing.Size(79, 14);
            this.labOperationNo.TabIndex = 1;
            this.labOperationNo.Text = "Operation NO.";
            // 
            // chkcmbCompany
            // 
            this.chkcmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcmbCompany.Location = new System.Drawing.Point(105, 13);
            this.chkcmbCompany.Name = "chkcmbCompany";
            this.chkcmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chkcmbCompany.Size = new System.Drawing.Size(137, 21);
            this.chkcmbCompany.TabIndex = 0;
            // 
            // labBookingNo
            // 
            this.labBookingNo.Location = new System.Drawing.Point(13, 225);
            this.labBookingNo.Name = "labBookingNo";
            this.labBookingNo.Size = new System.Drawing.Size(68, 14);
            this.labBookingNo.TabIndex = 1;
            this.labBookingNo.Text = "Booking NO.";
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(13, 16);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(50, 14);
            this.labCompany.TabIndex = 1;
            this.labCompany.Text = "Company";
            // 
            // labContractNo
            // 
            this.labContractNo.Location = new System.Drawing.Point(13, 198);
            this.labContractNo.Name = "labContractNo";
            this.labContractNo.Size = new System.Drawing.Size(72, 14);
            this.labContractNo.TabIndex = 1;
            this.labContractNo.Text = "Contract NO.";
            // 
            // labPlaceOfDelivery
            // 
            this.labPlaceOfDelivery.Location = new System.Drawing.Point(13, 172);
            this.labPlaceOfDelivery.Name = "labPlaceOfDelivery";
            this.labPlaceOfDelivery.Size = new System.Drawing.Size(83, 14);
            this.labPlaceOfDelivery.TabIndex = 1;
            this.labPlaceOfDelivery.Text = "PlaceOfDelivery";
            // 
            // labPOD
            // 
            this.labPOD.Location = new System.Drawing.Point(13, 146);
            this.labPOD.Name = "labPOD";
            this.labPOD.Size = new System.Drawing.Size(24, 14);
            this.labPOD.TabIndex = 1;
            this.labPOD.Text = "POD";
            // 
            // labPOL
            // 
            this.labPOL.Location = new System.Drawing.Point(13, 120);
            this.labPOL.Name = "labPOL";
            this.labPOL.Size = new System.Drawing.Size(22, 14);
            this.labPOL.TabIndex = 1;
            this.labPOL.Text = "POL";
            // 
            // labVessel
            // 
            this.labVessel.Location = new System.Drawing.Point(13, 94);
            this.labVessel.Name = "labVessel";
            this.labVessel.Size = new System.Drawing.Size(34, 14);
            this.labVessel.TabIndex = 1;
            this.labVessel.Text = "Vessel";
            // 
            // labShippingLine
            // 
            this.labShippingLine.Location = new System.Drawing.Point(13, 68);
            this.labShippingLine.Name = "labShippingLine";
            this.labShippingLine.Size = new System.Drawing.Size(72, 14);
            this.labShippingLine.TabIndex = 1;
            this.labShippingLine.Text = "Shipping Line";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.panelDate);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(256, 166);
            this.navBarGroupControlContainer1.TabIndex = 1;
            // 
            // panelDate
            // 
            this.panelDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panelDate.Controls.Add(this.dmcGateInRange);
            this.panelDate.Controls.Add(this.labTo);
            this.panelDate.Controls.Add(this.labFrom);
            this.panelDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDate.Location = new System.Drawing.Point(0, 0);
            this.panelDate.Name = "panelDate";
            this.panelDate.Size = new System.Drawing.Size(256, 166);
            this.panelDate.TabIndex = 1;
            // 
            // dmcGateInRange
            // 
            this.dmcGateInRange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dmcGateInRange.From = null;
            this.dmcGateInRange.IsEngish = true;
            this.dmcGateInRange.Location = new System.Drawing.Point(80, 8);
            this.dmcGateInRange.Name = "dmcGateInRange";
            this.dmcGateInRange.Size = new System.Drawing.Size(162, 133);
            this.dmcGateInRange.TabIndex = 0;
            this.dmcGateInRange.To = null;
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(7, 121);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(15, 14);
            this.labTo.TabIndex = 11;
            this.labTo.Text = "To";
            // 
            // labFrom
            // 
            this.labFrom.Location = new System.Drawing.Point(7, 94);
            this.labFrom.Name = "labFrom";
            this.labFrom.Size = new System.Drawing.Size(27, 14);
            this.labFrom.TabIndex = 1;
            this.labFrom.Text = "From";
            // 
            // nbarDate
            // 
            this.nbarDate.Caption = "Date";
            this.nbarDate.ControlContainer = this.navBarGroupControlContainer1;
            this.nbarDate.Expanded = true;
            this.nbarDate.GroupClientHeight = 168;
            this.nbarDate.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarDate.Name = "nbarDate";
            // 
            // pcBottom
            // 
            this.pcBottom.Controls.Add(this.btnClear);
            this.pcBottom.Controls.Add(this.btnSearch);
            this.pcBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pcBottom.Location = new System.Drawing.Point(0, 590);
            this.pcBottom.Name = "pcBottom";
            this.pcBottom.Size = new System.Drawing.Size(272, 40);
            this.pcBottom.TabIndex = 1;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(81, 8);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "C&lear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(180, 8);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "&Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panelForm
            // 
            this.panelForm.Controls.Add(this.panelFill);
            this.panelForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForm.Location = new System.Drawing.Point(0, 0);
            this.panelForm.Name = "panelForm";
            this.panelForm.Size = new System.Drawing.Size(272, 590);
            this.panelForm.TabIndex = 5;
            // 
            // panelFill
            // 
            this.panelFill.Controls.Add(this.navBarControl1);
            this.panelFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFill.Location = new System.Drawing.Point(2, 2);
            this.panelFill.Name = "panelFill";
            this.panelFill.Size = new System.Drawing.Size(268, 586);
            this.panelFill.TabIndex = 0;
            // 
            // PRSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.pcBottom);
            this.Name = "PRSearchPart";
            this.Size = new System.Drawing.Size(272, 630);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.panelBase.ResumeLayout(false);
            this.panelBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkIsNonContractNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperationNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBookingNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContractNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkcmbCompany.Properties)).EndInit();
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.panelDate.ResumeLayout(false);
            this.panelDate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcBottom)).EndInit();
            this.pcBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelForm)).EndInit();
            this.panelForm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelFill)).EndInit();
            this.panelFill.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nbarBase;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBase;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraNavBar.NavBarGroup nbarDate;
        private DevExpress.XtraEditors.PanelControl pcBottom;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.PanelControl panelForm;
        private DevExpress.XtraEditors.PanelControl panelFill;
        private ICP.Framework.ClientComponents.Controls.DateMonthControl dmcGateInRange;
        private DevExpress.XtraEditors.LabelControl labTo;
        private DevExpress.XtraEditors.LabelControl labFrom;
        private System.Windows.Forms.Panel panelBase;
        private System.Windows.Forms.Panel panelDate;
        private DevExpress.XtraEditors.LabelControl labShippingLine;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chkcmbCompany;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private ICP.Common.UI.Controls.TreeCheckControlShippingLine tccShippingLine;
        private Common.UI.Controls.CheckBoxComboBoxCarrier cbcbCarrier;
        private DevExpress.XtraEditors.LabelControl labCarrier;
        private Common.UI.Controls.CheckBoxComboBoxVessel cbcbVessel;
        private DevExpress.XtraEditors.LabelControl labVessel;
        private DevExpress.XtraEditors.LabelControl labPlaceOfDelivery;
        private DevExpress.XtraEditors.LabelControl labPOD;
        private DevExpress.XtraEditors.LabelControl labPOL;
        private DevExpress.XtraEditors.TextEdit txtContractNo;
        private DevExpress.XtraEditors.LabelControl labContractNo;
        private Common.UI.Controls.CheckBoxComboBoxLocations cbcbPOLID;
        private Common.UI.Controls.CheckBoxComboBoxLocations cbcbPlaceOfDeliveryID;
        private Common.UI.Controls.CheckBoxComboBoxLocations cbcbPODID;
        private DevExpress.XtraEditors.CheckEdit checkIsNonContractNo;
        private DevExpress.XtraEditors.TextEdit txtBookingNo;
        private DevExpress.XtraEditors.LabelControl labBookingNo;
        private DevExpress.XtraEditors.TextEdit txtOperationNo;
        private DevExpress.XtraEditors.LabelControl labOperationNo;
    }
}
