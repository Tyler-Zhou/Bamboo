namespace ICP.FRM.UI.SearchRate
{
    partial class SearchOceanSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchOceanSearchPart));
            this.panelControl2 = new System.Windows.Forms.Panel();
            this.cmbScope = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.dateMonthControl1 = new ICP.Framework.ClientComponents.Controls.DateMonthControl();
            this.txtContractNo = new DevExpress.XtraEditors.TextEdit();
            this.cmbCommodity = new ICP.FRM.UI.CheckBoxComboBox();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.labPRICE = new DevExpress.XtraEditors.LabelControl();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.labScope = new DevExpress.XtraEditors.LabelControl();
            this.labContractNo = new DevExpress.XtraEditors.LabelControl();
            this.labCommodity = new DevExpress.XtraEditors.LabelControl();
            this.cmbPOD = new ICP.FRM.UI.CheckBoxComboBox();
            this.labPOD = new DevExpress.XtraEditors.LabelControl();
            this.cmbDelivery = new ICP.FRM.UI.CheckBoxComboBox();
            this.cmbCarrier = new ICP.FRM.UI.CheckBoxComboBox();
            this.cmbPOL = new ICP.FRM.UI.CheckBoxComboBox();
            this.labDelivery = new DevExpress.XtraEditors.LabelControl();
            this.labCarrier = new DevExpress.XtraEditors.LabelControl();
            this.labPOL = new DevExpress.XtraEditors.LabelControl();
            this.cmbShipline = new ICP.FRM.UI.CheckBoxComboBox();
            this.labShipline = new DevExpress.XtraEditors.LabelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarBaseInfo = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.cmbType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labFinalDestination = new DevExpress.XtraEditors.LabelControl();
            this.cmbFinalDestination = new ICP.FRM.UI.CheckBoxComboBox();
            this.labType = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.navBarDuration = new DevExpress.XtraNavBar.NavBarGroup();
            ((System.ComponentModel.ISupportInitialize)(this.cmbScope.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContractNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.xtraScrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            this.navBarGroupControlContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.BackColor = System.Drawing.Color.Red;
            this.panelControl2.Location = new System.Drawing.Point(8, 413);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(21, 16);
            this.panelControl2.TabIndex = 8;
            // 
            // cmbScope
            // 
            this.cmbScope.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbScope.Location = new System.Drawing.Point(8, 386);
            this.cmbScope.Name = "cmbScope";
            this.cmbScope.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbScope.Properties.Appearance.Options.UseBackColor = true;
            this.cmbScope.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbScope.Properties.SmallImages = this.imageList1;
            this.cmbScope.Size = new System.Drawing.Size(211, 21);
            this.cmbScope.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbScope.TabIndex = 9;
            this.cmbScope.SelectedIndexChanged += new System.EventHandler(this.cmbScope_SelectedIndexChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Input_16.png");
            this.imageList1.Images.SetKeyName(1, "EFFECTIVE.png");
            this.imageList1.Images.SetKeyName(2, "WILL BE EFFECTIVE.png");
            this.imageList1.Images.SetKeyName(3, "EXPIRED.png");
            // 
            // dateMonthControl1
            // 
            this.dateMonthControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dateMonthControl1.From = null;
            this.dateMonthControl1.IsEngish = true;
            this.dateMonthControl1.Location = new System.Drawing.Point(55, 3);
            this.dateMonthControl1.Name = "dateMonthControl1";
            this.dateMonthControl1.Size = new System.Drawing.Size(148, 141);
            this.dateMonthControl1.TabIndex = 0;
            this.dateMonthControl1.To = null;
            this.dateMonthControl1.DataValueChaned += new ICP.Framework.ClientComponents.Controls.ControlDataCharnged(this.dateMonthControl1_DataValueChaned);
            // 
            // txtContractNo
            // 
            this.txtContractNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContractNo.Location = new System.Drawing.Point(8, 301);
            this.txtContractNo.Name = "txtContractNo";
            this.txtContractNo.Size = new System.Drawing.Size(211, 21);
            this.txtContractNo.TabIndex = 7;
            // 
            // cmbCommodity
            // 
            this.cmbCommodity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCommodity.DataSource = null;
            this.cmbCommodity.DisplayMember = "";
            this.cmbCommodity.Location = new System.Drawing.Point(8, 257);
            this.cmbCommodity.Name = "cmbCommodity";
            this.cmbCommodity.NullText = "";
            this.cmbCommodity.Size = new System.Drawing.Size(211, 21);
            this.cmbCommodity.TabIndex = 6;
            this.cmbCommodity.ValueMember = "";
            this.cmbCommodity.Enter += new System.EventHandler(this.cmbCommodity_Enter);
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(12, 122);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(15, 14);
            this.labTo.TabIndex = 0;
            this.labTo.Text = "To";
            // 
            // labPRICE
            // 
            this.labPRICE.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labPRICE.Appearance.Options.UseBackColor = true;
            this.labPRICE.Location = new System.Drawing.Point(54, 414);
            this.labPRICE.Name = "labPRICE";
            this.labPRICE.Size = new System.Drawing.Size(86, 14);
            this.labPRICE.TabIndex = 0;
            this.labPRICE.Text = "RESERVE PRICE";
            // 
            // labFrom
            // 
            this.labFrom.Location = new System.Drawing.Point(12, 94);
            this.labFrom.Name = "labFrom";
            this.labFrom.Size = new System.Drawing.Size(27, 14);
            this.labFrom.TabIndex = 0;
            this.labFrom.Text = "From";
            // 
            // labScope
            // 
            this.labScope.Location = new System.Drawing.Point(8, 370);
            this.labScope.Name = "labScope";
            this.labScope.Size = new System.Drawing.Size(34, 14);
            this.labScope.TabIndex = 0;
            this.labScope.Text = "Scope";
            // 
            // labContractNo
            // 
            this.labContractNo.Location = new System.Drawing.Point(8, 282);
            this.labContractNo.Name = "labContractNo";
            this.labContractNo.Size = new System.Drawing.Size(66, 14);
            this.labContractNo.TabIndex = 0;
            this.labContractNo.Text = "Contract No";
            // 
            // labCommodity
            // 
            this.labCommodity.Location = new System.Drawing.Point(8, 242);
            this.labCommodity.Name = "labCommodity";
            this.labCommodity.Size = new System.Drawing.Size(61, 14);
            this.labCommodity.TabIndex = 0;
            this.labCommodity.Text = "Commodity";
            // 
            // cmbPOD
            // 
            this.cmbPOD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPOD.DataSource = null;
            this.cmbPOD.DisplayMember = "";
            this.cmbPOD.Location = new System.Drawing.Point(8, 136);
            this.cmbPOD.Name = "cmbPOD";
            this.cmbPOD.NullText = "";
            this.cmbPOD.Size = new System.Drawing.Size(211, 21);
            this.cmbPOD.TabIndex = 3;
            this.cmbPOD.ValueMember = "";
            this.cmbPOD.Enter += new System.EventHandler(this.cmbPOD_Enter);
            // 
            // labPOD
            // 
            this.labPOD.Location = new System.Drawing.Point(8, 121);
            this.labPOD.Name = "labPOD";
            this.labPOD.Size = new System.Drawing.Size(24, 14);
            this.labPOD.TabIndex = 0;
            this.labPOD.Text = "POD";
            // 
            // cmbDelivery
            // 
            this.cmbDelivery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDelivery.DataSource = null;
            this.cmbDelivery.DisplayMember = "";
            this.cmbDelivery.Location = new System.Drawing.Point(8, 176);
            this.cmbDelivery.Name = "cmbDelivery";
            this.cmbDelivery.NullText = "";
            this.cmbDelivery.Size = new System.Drawing.Size(211, 21);
            this.cmbDelivery.TabIndex = 4;
            this.cmbDelivery.ValueMember = "";
            this.cmbDelivery.Enter += new System.EventHandler(this.cmbDelivery_Enter);
            // 
            // cmbCarrier
            // 
            this.cmbCarrier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCarrier.DataSource = null;
            this.cmbCarrier.DisplayMember = "";
            this.cmbCarrier.Location = new System.Drawing.Point(8, 57);
            this.cmbCarrier.Name = "cmbCarrier";
            this.cmbCarrier.NullText = "";
            this.cmbCarrier.Size = new System.Drawing.Size(211, 21);
            this.cmbCarrier.TabIndex = 1;
            this.cmbCarrier.ValueMember = "";
            this.cmbCarrier.EditValueChanged += new System.EventHandler(this.cmbCarrier_EditValueChanged);
            this.cmbCarrier.Enter += new System.EventHandler(this.cmbCarrier_Enter);
            // 
            // cmbPOL
            // 
            this.cmbPOL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPOL.DataSource = null;
            this.cmbPOL.DisplayMember = "";
            this.cmbPOL.Location = new System.Drawing.Point(8, 96);
            this.cmbPOL.Name = "cmbPOL";
            this.cmbPOL.NullText = "";
            this.cmbPOL.Size = new System.Drawing.Size(211, 21);
            this.cmbPOL.TabIndex = 2;
            this.cmbPOL.ValueMember = "";
            this.cmbPOL.Enter += new System.EventHandler(this.cmbPOL_Enter);
            // 
            // labDelivery
            // 
            this.labDelivery.Location = new System.Drawing.Point(8, 161);
            this.labDelivery.Name = "labDelivery";
            this.labDelivery.Size = new System.Drawing.Size(42, 14);
            this.labDelivery.TabIndex = 0;
            this.labDelivery.Text = "Delivery";
            // 
            // labCarrier
            // 
            this.labCarrier.Location = new System.Drawing.Point(8, 42);
            this.labCarrier.Name = "labCarrier";
            this.labCarrier.Size = new System.Drawing.Size(34, 14);
            this.labCarrier.TabIndex = 0;
            this.labCarrier.Text = "Carrier";
            // 
            // labPOL
            // 
            this.labPOL.Location = new System.Drawing.Point(8, 81);
            this.labPOL.Name = "labPOL";
            this.labPOL.Size = new System.Drawing.Size(22, 14);
            this.labPOL.TabIndex = 0;
            this.labPOL.Text = "POL";
            // 
            // cmbShipline
            // 
            this.cmbShipline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbShipline.DataSource = null;
            this.cmbShipline.DisplayMember = "";
            this.cmbShipline.Location = new System.Drawing.Point(8, 18);
            this.cmbShipline.Name = "cmbShipline";
            this.cmbShipline.NullText = "";
            this.cmbShipline.Size = new System.Drawing.Size(211, 21);
            this.cmbShipline.TabIndex = 0;
            this.cmbShipline.ValueMember = "";
            this.cmbShipline.EditValueChanged += new System.EventHandler(this.cmbShipline_EditValueChanged);
            // 
            // labShipline
            // 
            this.labShipline.Location = new System.Drawing.Point(8, 3);
            this.labShipline.Name = "labShipline";
            this.labShipline.Size = new System.Drawing.Size(41, 14);
            this.labShipline.TabIndex = 0;
            this.labShipline.Text = "Shipline";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(170, 8);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "&Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(62, 8);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "C&lear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Controls.Add(this.btnClear);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 607);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(253, 40);
            this.panelControl1.TabIndex = 0;
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.Controls.Add(this.navBarControl1);
            this.xtraScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            this.xtraScrollableControl1.Size = new System.Drawing.Size(253, 607);
            this.xtraScrollableControl1.TabIndex = 8;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarBaseInfo;
            this.navBarControl1.Controls.Add(this.navBarGroupBase);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer2);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navBarControl1.ExplorerBarGroupInterval = 2;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 2;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarBaseInfo,
            this.navBarDuration});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 586;
            this.navBarControl1.Size = new System.Drawing.Size(236, 669);
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarBaseInfo
            // 
            this.navBarBaseInfo.Caption = "BaseInfo";
            this.navBarBaseInfo.ControlContainer = this.navBarGroupBase;
            this.navBarBaseInfo.Expanded = true;
            this.navBarBaseInfo.GroupClientHeight = 440;
            this.navBarBaseInfo.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarBaseInfo.Name = "navBarBaseInfo";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.panelControl2);
            this.navBarGroupBase.Controls.Add(this.labShipline);
            this.navBarGroupBase.Controls.Add(this.cmbType);
            this.navBarGroupBase.Controls.Add(this.cmbScope);
            this.navBarGroupBase.Controls.Add(this.cmbShipline);
            this.navBarGroupBase.Controls.Add(this.labPRICE);
            this.navBarGroupBase.Controls.Add(this.labPOL);
            this.navBarGroupBase.Controls.Add(this.txtContractNo);
            this.navBarGroupBase.Controls.Add(this.labCarrier);
            this.navBarGroupBase.Controls.Add(this.cmbCommodity);
            this.navBarGroupBase.Controls.Add(this.labFinalDestination);
            this.navBarGroupBase.Controls.Add(this.labDelivery);
            this.navBarGroupBase.Controls.Add(this.cmbPOL);
            this.navBarGroupBase.Controls.Add(this.cmbCarrier);
            this.navBarGroupBase.Controls.Add(this.cmbFinalDestination);
            this.navBarGroupBase.Controls.Add(this.cmbDelivery);
            this.navBarGroupBase.Controls.Add(this.labType);
            this.navBarGroupBase.Controls.Add(this.labPOD);
            this.navBarGroupBase.Controls.Add(this.labScope);
            this.navBarGroupBase.Controls.Add(this.cmbPOD);
            this.navBarGroupBase.Controls.Add(this.labContractNo);
            this.navBarGroupBase.Controls.Add(this.labCommodity);
            this.navBarGroupBase.Name = "navBarGroupBase";
            this.navBarGroupBase.Size = new System.Drawing.Size(228, 438);
            this.navBarGroupBase.TabIndex = 0;
            // 
            // cmbType
            // 
            this.cmbType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbType.Location = new System.Drawing.Point(8, 345);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Size = new System.Drawing.Size(211, 21);
            this.cmbType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbType.TabIndex = 8;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbScope_SelectedIndexChanged);
            // 
            // labFinalDestination
            // 
            this.labFinalDestination.Location = new System.Drawing.Point(8, 201);
            this.labFinalDestination.Name = "labFinalDestination";
            this.labFinalDestination.Size = new System.Drawing.Size(88, 14);
            this.labFinalDestination.TabIndex = 0;
            this.labFinalDestination.Text = "Final Destination";
            // 
            // cmbFinalDestination
            // 
            this.cmbFinalDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFinalDestination.DataSource = null;
            this.cmbFinalDestination.DisplayMember = "";
            this.cmbFinalDestination.Location = new System.Drawing.Point(8, 216);
            this.cmbFinalDestination.Name = "cmbFinalDestination";
            this.cmbFinalDestination.NullText = "";
            this.cmbFinalDestination.Size = new System.Drawing.Size(211, 21);
            this.cmbFinalDestination.TabIndex = 5;
            this.cmbFinalDestination.ValueMember = "";
            this.cmbFinalDestination.Enter += new System.EventHandler(this.cmbFinalDestination_Enter);
            // 
            // labType
            // 
            this.labType.Location = new System.Drawing.Point(8, 325);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(28, 14);
            this.labType.TabIndex = 0;
            this.labType.Text = "Type";
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.dateMonthControl1);
            this.navBarGroupControlContainer2.Controls.Add(this.labTo);
            this.navBarGroupControlContainer2.Controls.Add(this.labFrom);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(228, 148);
            this.navBarGroupControlContainer2.TabIndex = 0;
            // 
            // navBarDuration
            // 
            this.navBarDuration.Caption = "Duration";
            this.navBarDuration.ControlContainer = this.navBarGroupControlContainer2;
            this.navBarDuration.Expanded = true;
            this.navBarDuration.GroupClientHeight = 150;
            this.navBarDuration.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarDuration.Name = "navBarDuration";
            // 
            // SearchOceanSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraScrollableControl1);
            this.Controls.Add(this.panelControl1);
            this.IsMultiLanguage = false;
            this.Name = "SearchOceanSearchPart";
            this.Size = new System.Drawing.Size(253, 647);
            ((System.ComponentModel.ISupportInitialize)(this.cmbScope.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContractNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.xtraScrollableControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            this.navBarGroupControlContainer2.PerformLayout();
            this.ResumeLayout(false);

        }      

        #endregion

        private DevExpress.XtraEditors.LabelControl labShipline;
        private CheckBoxComboBox cmbShipline;
        private CheckBoxComboBox cmbCommodity;
        private DevExpress.XtraEditors.LabelControl labCommodity;
        private CheckBoxComboBox cmbPOD;
        private DevExpress.XtraEditors.LabelControl labPOD;
        private CheckBoxComboBox cmbDelivery;
        private CheckBoxComboBox cmbCarrier;
        private CheckBoxComboBox cmbPOL;
        private DevExpress.XtraEditors.LabelControl labDelivery;
        private DevExpress.XtraEditors.LabelControl labCarrier;
        private DevExpress.XtraEditors.LabelControl labPOL;
        private DevExpress.XtraEditors.LabelControl labContractNo;
        private ICP.Framework.ClientComponents.Controls.DateMonthControl dateMonthControl1;
        private DevExpress.XtraEditors.TextEdit txtContractNo;
        private DevExpress.XtraEditors.LabelControl labTo;
        private DevExpress.XtraEditors.LabelControl labFrom;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbScope;
        private DevExpress.XtraEditors.LabelControl labScope;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panelControl2;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private DevExpress.XtraEditors.LabelControl labPRICE;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarBaseInfo;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBase;
        private DevExpress.XtraNavBar.NavBarGroup navBarDuration;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbType;
        private DevExpress.XtraEditors.LabelControl labType;
        private DevExpress.XtraEditors.LabelControl labFinalDestination;
        private CheckBoxComboBox cmbFinalDestination;
    }
}
