namespace ICP.FRM.UI.InquireRates
{
    partial class InquireRatesSearchPart
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
            this.bcMain = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.chkUnReply = new DevExpress.XtraEditors.CheckEdit();
            this.labType = new DevExpress.XtraEditors.LabelControl();
            this.labDelivery = new DevExpress.XtraEditors.LabelControl();
            this.labComm = new DevExpress.XtraEditors.LabelControl();
            this.labPOD = new DevExpress.XtraEditors.LabelControl();
            this.labPOL = new DevExpress.XtraEditors.LabelControl();
            this.txtDelivery = new DevExpress.XtraEditors.TextEdit();
            this.txtInquireOrRespondBy = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.txtComm = new DevExpress.XtraEditors.TextEdit();
            this.txtPOD = new DevExpress.XtraEditors.TextEdit();
            this.txtPOL = new DevExpress.XtraEditors.TextEdit();
            this.cmbType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblReject = new DevExpress.XtraEditors.LabelControl();
            this.labInquireOrRespondBy = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.fromToDateMonthControl1 = new ICP.Framework.ClientComponents.Controls.DateMonthControl();
            this.nbarDate = new DevExpress.XtraNavBar.NavBarGroup();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.pnlScroll = new DevExpress.XtraEditors.XtraScrollableControl();
            this.txtSNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.bcMain)).BeginInit();
            this.bcMain.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkUnReply.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDelivery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtComm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.pnlScroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // bcMain
            // 
            this.bcMain.ActiveGroup = this.nbarBase;
            this.bcMain.Controls.Add(this.navBarGroupControlContainer1);
            this.bcMain.Controls.Add(this.navBarGroupBase);
            this.bcMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.bcMain.ExplorerBarGroupInterval = 3;
            this.bcMain.ExplorerBarGroupOuterIndent = 0;
            this.bcMain.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarBase,
            this.nbarDate});
            this.bcMain.Location = new System.Drawing.Point(0, 0);
            this.bcMain.Name = "bcMain";
            this.bcMain.OptionsNavPane.ExpandedWidth = 140;
            this.bcMain.Size = new System.Drawing.Size(171, 495);
            this.bcMain.TabIndex = 1;
            this.bcMain.Text = "navBarControl1";
            // 
            // nbarBase
            // 
            this.nbarBase.Caption = "Base";
            this.nbarBase.ControlContainer = this.navBarGroupBase;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 240;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.chkUnReply);
            this.navBarGroupBase.Controls.Add(this.labType);
            this.navBarGroupBase.Controls.Add(this.labDelivery);
            this.navBarGroupBase.Controls.Add(this.labComm);
            this.navBarGroupBase.Controls.Add(this.labPOD);
            this.navBarGroupBase.Controls.Add(this.labelControl1);
            this.navBarGroupBase.Controls.Add(this.labPOL);
            this.navBarGroupBase.Controls.Add(this.txtDelivery);
            this.navBarGroupBase.Controls.Add(this.txtInquireOrRespondBy);
            this.navBarGroupBase.Controls.Add(this.txtComm);
            this.navBarGroupBase.Controls.Add(this.txtPOD);
            this.navBarGroupBase.Controls.Add(this.txtSNo);
            this.navBarGroupBase.Controls.Add(this.txtPOL);
            this.navBarGroupBase.Controls.Add(this.cmbType);
            this.navBarGroupBase.Controls.Add(this.panel2);
            this.navBarGroupBase.Controls.Add(this.lblReject);
            this.navBarGroupBase.Controls.Add(this.labInquireOrRespondBy);
            this.navBarGroupBase.Name = "navBarGroupBase";
            this.navBarGroupBase.Size = new System.Drawing.Size(167, 238);
            this.navBarGroupBase.TabIndex = 0;
            // 
            // chkUnReply
            // 
            this.chkUnReply.Location = new System.Drawing.Point(52, 202);
            this.chkUnReply.Name = "chkUnReply";
            this.chkUnReply.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.chkUnReply.Properties.Appearance.Options.UseBackColor = true;
            this.chkUnReply.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.chkUnReply.Properties.Caption = "Un-reply";
            this.chkUnReply.Size = new System.Drawing.Size(110, 23);
            this.chkUnReply.TabIndex = 77;
            this.chkUnReply.ToolTip = "Means the inquiring has new messages for you, or has no rates.";
            // 
            // labType
            // 
            this.labType.Location = new System.Drawing.Point(5, 6);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(28, 14);
            this.labType.TabIndex = 68;
            this.labType.Text = "Type";
            // 
            // labDelivery
            // 
            this.labDelivery.Location = new System.Drawing.Point(5, 90);
            this.labDelivery.Name = "labDelivery";
            this.labDelivery.Size = new System.Drawing.Size(15, 14);
            this.labDelivery.TabIndex = 72;
            this.labDelivery.Text = "To";
            this.labDelivery.ToolTip = "To (Delivery)";
            // 
            // labComm
            // 
            this.labComm.Location = new System.Drawing.Point(5, 146);
            this.labComm.Name = "labComm";
            this.labComm.Size = new System.Drawing.Size(34, 14);
            this.labComm.TabIndex = 73;
            this.labComm.Text = "Comm";
            this.labComm.ToolTip = "Commodity";
            // 
            // labPOD
            // 
            this.labPOD.Location = new System.Drawing.Point(5, 118);
            this.labPOD.Name = "labPOD";
            this.labPOD.Size = new System.Drawing.Size(24, 14);
            this.labPOD.TabIndex = 76;
            this.labPOD.Text = "POD";
            // 
            // labPOL
            // 
            this.labPOL.Location = new System.Drawing.Point(5, 62);
            this.labPOL.Name = "labPOL";
            this.labPOL.Size = new System.Drawing.Size(27, 14);
            this.labPOL.TabIndex = 74;
            this.labPOL.Text = "From";
            this.labPOL.ToolTip = "From(POL)";
            // 
            // txtDelivery
            // 
            this.txtDelivery.Location = new System.Drawing.Point(53, 87);
            this.txtDelivery.Name = "txtDelivery";
            this.txtDelivery.Size = new System.Drawing.Size(110, 21);
            this.txtDelivery.TabIndex = 66;
            // 
            // txtInquireOrRespondBy
            // 
            this.txtInquireOrRespondBy.EditText = "";
            this.txtInquireOrRespondBy.EditValue = null;
            this.txtInquireOrRespondBy.Location = new System.Drawing.Point(53, 172);
            this.txtInquireOrRespondBy.Name = "txtInquireOrRespondBy";
            this.txtInquireOrRespondBy.ReadOnly = false;
            this.txtInquireOrRespondBy.RefreshButtonToolTip = "";
            this.txtInquireOrRespondBy.ShowRefreshButton = false;
            this.txtInquireOrRespondBy.Size = new System.Drawing.Size(110, 21);
            this.txtInquireOrRespondBy.SpecifiedBackColor = System.Drawing.Color.White;
            this.txtInquireOrRespondBy.TabIndex = 10;
            this.txtInquireOrRespondBy.ToolTip = "";
            // 
            // txtComm
            // 
            this.txtComm.Location = new System.Drawing.Point(53, 143);
            this.txtComm.Name = "txtComm";
            this.txtComm.Size = new System.Drawing.Size(110, 21);
            this.txtComm.TabIndex = 62;
            // 
            // txtPOD
            // 
            this.txtPOD.Location = new System.Drawing.Point(53, 115);
            this.txtPOD.Name = "txtPOD";
            this.txtPOD.Size = new System.Drawing.Size(110, 21);
            this.txtPOD.TabIndex = 65;
            // 
            // txtPOL
            // 
            this.txtPOL.Location = new System.Drawing.Point(53, 59);
            this.txtPOL.Name = "txtPOL";
            this.txtPOL.Size = new System.Drawing.Size(110, 21);
            this.txtPOL.TabIndex = 61;
            // 
            // cmbType
            // 
            this.cmbType.Location = new System.Drawing.Point(53, 3);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Size = new System.Drawing.Size(110, 21);
            this.cmbType.TabIndex = 67;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightYellow;
            this.panel2.Location = new System.Drawing.Point(155, 176);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(14, 14);
            this.panel2.TabIndex = 49;
            this.panel2.Visible = false;
            // 
            // lblReject
            // 
            this.lblReject.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.lblReject.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblReject.Appearance.Options.UseBackColor = true;
            this.lblReject.Appearance.Options.UseForeColor = true;
            this.lblReject.Location = new System.Drawing.Point(171, 202);
            this.lblReject.Name = "lblReject";
            this.lblReject.Size = new System.Drawing.Size(45, 14);
            this.lblReject.TabIndex = 46;
            this.lblReject.Text = "Un-reply";
            this.lblReject.Visible = false;
            // 
            // labInquireOrRespondBy
            // 
            this.labInquireOrRespondBy.Appearance.Options.UseTextOptions = true;
            this.labInquireOrRespondBy.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labInquireOrRespondBy.Location = new System.Drawing.Point(5, 174);
            this.labInquireOrRespondBy.Name = "labInquireOrRespondBy";
            this.labInquireOrRespondBy.Size = new System.Drawing.Size(42, 14);
            this.labInquireOrRespondBy.TabIndex = 70;
            this.labInquireOrRespondBy.Text = "Inq/Rsp";
            this.labInquireOrRespondBy.ToolTip = "Inquire or  Respond By";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.labTo);
            this.navBarGroupControlContainer1.Controls.Add(this.labFrom);
            this.navBarGroupControlContainer1.Controls.Add(this.fromToDateMonthControl1);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(167, 156);
            this.navBarGroupControlContainer1.TabIndex = 1;
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(5, 117);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(15, 14);
            this.labTo.TabIndex = 19;
            this.labTo.Text = "To";
            // 
            // labFrom
            // 
            this.labFrom.Location = new System.Drawing.Point(5, 90);
            this.labFrom.Name = "labFrom";
            this.labFrom.Size = new System.Drawing.Size(27, 14);
            this.labFrom.TabIndex = 18;
            this.labFrom.Text = "From";
            // 
            // fromToDateMonthControl1
            // 
            this.fromToDateMonthControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fromToDateMonthControl1.From = null;
            this.fromToDateMonthControl1.IsEngish = true;
            this.fromToDateMonthControl1.Location = new System.Drawing.Point(42, 5);
            this.fromToDateMonthControl1.Name = "fromToDateMonthControl1";
            this.fromToDateMonthControl1.Size = new System.Drawing.Size(127, 133);
            this.fromToDateMonthControl1.TabIndex = 17;
            this.fromToDateMonthControl1.To = null;
            // 
            // nbarDate
            // 
            this.nbarDate.Caption = "Inquire Date";
            this.nbarDate.ControlContainer = this.navBarGroupControlContainer1;
            this.nbarDate.Expanded = true;
            this.nbarDate.GroupClientHeight = 158;
            this.nbarDate.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarDate.Name = "nbarDate";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnClear);
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 499);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(171, 55);
            this.panelControl1.TabIndex = 4;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(5, 15);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "C&lear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(89, 15);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "&Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pnlScroll
            // 
            this.pnlScroll.Controls.Add(this.bcMain);
            this.pnlScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScroll.Location = new System.Drawing.Point(0, 0);
            this.pnlScroll.Name = "pnlScroll";
            this.pnlScroll.Size = new System.Drawing.Size(171, 499);
            this.pnlScroll.TabIndex = 0;
            this.pnlScroll.SizeChanged += new System.EventHandler(this.pnlScroll_SizeChanged);
            // 
            // txtSNo
            // 
            this.txtSNo.Location = new System.Drawing.Point(53, 32);
            this.txtSNo.Name = "txtSNo";
            this.txtSNo.Size = new System.Drawing.Size(110, 21);
            this.txtSNo.TabIndex = 61;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 35);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(15, 14);
            this.labelControl1.TabIndex = 74;
            this.labelControl1.Text = "No";
            this.labelControl1.ToolTip = "From(POL)";
            // 
            // InquireRatesSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlScroll);
            this.Controls.Add(this.panelControl1);
            this.IsMultiLanguage = false;
            this.Name = "InquireRatesSearchPart";
            this.Size = new System.Drawing.Size(171, 554);
            ((System.ComponentModel.ISupportInitialize)(this.bcMain)).EndInit();
            this.bcMain.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkUnReply.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDelivery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtComm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.pnlScroll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSNo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl bcMain;
        private DevExpress.XtraNavBar.NavBarGroup nbarBase;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBase;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraNavBar.NavBarGroup nbarDate;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.XtraScrollableControl pnlScroll;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.LabelControl lblReject;
        private DevExpress.XtraEditors.CheckEdit chkUnReply;
        private DevExpress.XtraEditors.LabelControl labType;
        private DevExpress.XtraEditors.LabelControl labDelivery;
        private DevExpress.XtraEditors.LabelControl labInquireOrRespondBy;
        private DevExpress.XtraEditors.LabelControl labComm;
        private DevExpress.XtraEditors.LabelControl labPOD;
        private DevExpress.XtraEditors.LabelControl labPOL;
        private DevExpress.XtraEditors.TextEdit txtDelivery;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox txtInquireOrRespondBy;
        private DevExpress.XtraEditors.TextEdit txtComm;
        private DevExpress.XtraEditors.TextEdit txtPOD;
        private DevExpress.XtraEditors.TextEdit txtPOL;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbType;
        protected DevExpress.XtraEditors.LabelControl labTo;
        protected DevExpress.XtraEditors.LabelControl labFrom;
        protected ICP.Framework.ClientComponents.Controls.DateMonthControl fromToDateMonthControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtSNo;
    }
}

