namespace ICP.Business.Common.UI.QuotedPrice
{
    partial class QuotedPriceSearchPart
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
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarControlBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.lwchkIsSure = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.labIsSure = new DevExpress.XtraEditors.LabelControl();
            this.lwchkIsValid = new ICP.Framework.ClientComponents.Controls.LWCheckButton();
            this.labIsValid = new DevExpress.XtraEditors.LabelControl();
            this.labQuoteBy = new DevExpress.XtraEditors.LabelControl();
            this.txtQuoteBy = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.snumMax = new DevExpress.XtraEditors.SpinEdit();
            this.labMax = new DevExpress.XtraEditors.LabelControl();
            this.labCustomerName = new DevExpress.XtraEditors.LabelControl();
            this.labNO = new DevExpress.XtraEditors.LabelControl();
            this.stxtCustomerName = new DevExpress.XtraEditors.TextEdit();
            this.stxtNO = new DevExpress.XtraEditors.TextEdit();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.dteTo = new DevExpress.XtraEditors.DateEdit();
            this.dteFrom = new DevExpress.XtraEditors.DateEdit();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.chkDate = new DevExpress.XtraEditors.CheckEdit();
            this.nbarDuration = new DevExpress.XtraNavBar.NavBarGroup();
            this.panelButtons = new DevExpress.XtraEditors.PanelControl();
            this.btnClean = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarControlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.snumMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtNO.Properties)).BeginInit();
            this.navBarGroupControlContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelButtons)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbarBase;
            this.navBarControl1.Controls.Add(this.navBarControlBase);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer2);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarBase,
            this.nbarDuration});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 186;
            this.navBarControl1.Size = new System.Drawing.Size(255, 632);
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // nbarBase
            // 
            this.nbarBase.Caption = "Base";
            this.nbarBase.ControlContainer = this.navBarControlBase;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 338;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // navBarControlBase
            // 
            this.navBarControlBase.Controls.Add(this.lwchkIsSure);
            this.navBarControlBase.Controls.Add(this.labIsSure);
            this.navBarControlBase.Controls.Add(this.lwchkIsValid);
            this.navBarControlBase.Controls.Add(this.labIsValid);
            this.navBarControlBase.Controls.Add(this.labQuoteBy);
            this.navBarControlBase.Controls.Add(this.txtQuoteBy);
            this.navBarControlBase.Controls.Add(this.snumMax);
            this.navBarControlBase.Controls.Add(this.labMax);
            this.navBarControlBase.Controls.Add(this.labCustomerName);
            this.navBarControlBase.Controls.Add(this.labNO);
            this.navBarControlBase.Controls.Add(this.stxtCustomerName);
            this.navBarControlBase.Controls.Add(this.stxtNO);
            this.navBarControlBase.Name = "navBarControlBase";
            this.navBarControlBase.Size = new System.Drawing.Size(251, 336);
            this.navBarControlBase.TabIndex = 1;
            this.navBarControlBase.Text = "Base";
            // 
            // lwchkIsSure
            // 
            this.lwchkIsSure.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lwchkIsSure.Checked = null;
            this.lwchkIsSure.CheckedText = "Sure";
            this.lwchkIsSure.Location = new System.Drawing.Point(88, 83);
            this.lwchkIsSure.Name = "lwchkIsSure";
            this.lwchkIsSure.NULLText = "ALL";
            this.lwchkIsSure.Size = new System.Drawing.Size(122, 24);
            this.lwchkIsSure.TabIndex = 8;
            this.lwchkIsSure.UnCheckedText = "UnSure";
            // 
            // labIsSure
            // 
            this.labIsSure.Location = new System.Drawing.Point(5, 88);
            this.labIsSure.Name = "labIsSure";
            this.labIsSure.Size = new System.Drawing.Size(34, 14);
            this.labIsSure.TabIndex = 42;
            this.labIsSure.Text = "IsSure";
            // 
            // lwchkIsValid
            // 
            this.lwchkIsValid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lwchkIsValid.Checked = true;
            this.lwchkIsValid.CheckedText = "Valid";
            this.lwchkIsValid.Location = new System.Drawing.Point(88, 119);
            this.lwchkIsValid.Name = "lwchkIsValid";
            this.lwchkIsValid.NULLText = "ALL";
            this.lwchkIsValid.Size = new System.Drawing.Size(122, 24);
            this.lwchkIsValid.TabIndex = 9;
            this.lwchkIsValid.UnCheckedText = "UnValid";
            // 
            // labIsValid
            // 
            this.labIsValid.Location = new System.Drawing.Point(5, 124);
            this.labIsValid.Name = "labIsValid";
            this.labIsValid.Size = new System.Drawing.Size(34, 14);
            this.labIsValid.TabIndex = 42;
            this.labIsValid.Text = "IsValid";
            // 
            // labQuoteBy
            // 
            this.labQuoteBy.Location = new System.Drawing.Point(4, 194);
            this.labQuoteBy.Name = "labQuoteBy";
            this.labQuoteBy.Size = new System.Drawing.Size(52, 14);
            this.labQuoteBy.TabIndex = 30;
            this.labQuoteBy.Text = "Quote By";
            // 
            // txtQuoteBy
            // 
            this.txtQuoteBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuoteBy.EditText = "";
            this.txtQuoteBy.EditValue = null;
            this.txtQuoteBy.Location = new System.Drawing.Point(87, 187);
            this.txtQuoteBy.Name = "txtQuoteBy";
            this.txtQuoteBy.ReadOnly = false;
            this.txtQuoteBy.RefreshButtonToolTip = "";
            this.txtQuoteBy.ShowRefreshButton = false;
            this.txtQuoteBy.Size = new System.Drawing.Size(158, 21);
            this.txtQuoteBy.SpecifiedBackColor = System.Drawing.Color.White;
            this.txtQuoteBy.TabIndex = 7;
            this.txtQuoteBy.ToolTip = "";
            // 
            // snumMax
            // 
            this.snumMax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.snumMax.EditValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.snumMax.Location = new System.Drawing.Point(87, 159);
            this.snumMax.Name = "snumMax";
            this.snumMax.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.snumMax.Properties.IsFloatValue = false;
            this.snumMax.Properties.Mask.EditMask = "N00";
            this.snumMax.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.snumMax.Size = new System.Drawing.Size(158, 21);
            this.snumMax.TabIndex = 10;
            // 
            // labMax
            // 
            this.labMax.Location = new System.Drawing.Point(4, 166);
            this.labMax.Name = "labMax";
            this.labMax.Size = new System.Drawing.Size(58, 14);
            this.labMax.TabIndex = 27;
            this.labMax.Text = "Max Count";
            // 
            // labCustomerName
            // 
            this.labCustomerName.Location = new System.Drawing.Point(5, 46);
            this.labCustomerName.Name = "labCustomerName";
            this.labCustomerName.Size = new System.Drawing.Size(87, 14);
            this.labCustomerName.TabIndex = 21;
            this.labCustomerName.Text = "Customer Name";
            // 
            // labNO
            // 
            this.labNO.Location = new System.Drawing.Point(5, 19);
            this.labNO.Name = "labNO";
            this.labNO.Size = new System.Drawing.Size(17, 14);
            this.labNO.TabIndex = 18;
            this.labNO.Text = "NO";
            // 
            // stxtCustomerName
            // 
            this.stxtCustomerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stxtCustomerName.Location = new System.Drawing.Point(88, 43);
            this.stxtCustomerName.Name = "stxtCustomerName";
            this.stxtCustomerName.Size = new System.Drawing.Size(158, 21);
            this.stxtCustomerName.TabIndex = 1;
            // 
            // stxtNO
            // 
            this.stxtNO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stxtNO.Location = new System.Drawing.Point(88, 16);
            this.stxtNO.Name = "stxtNO";
            this.stxtNO.Size = new System.Drawing.Size(158, 21);
            this.stxtNO.TabIndex = 0;
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.dteTo);
            this.navBarGroupControlContainer2.Controls.Add(this.dteFrom);
            this.navBarGroupControlContainer2.Controls.Add(this.labTo);
            this.navBarGroupControlContainer2.Controls.Add(this.labFrom);
            this.navBarGroupControlContainer2.Controls.Add(this.chkDate);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(251, 129);
            this.navBarGroupControlContainer2.TabIndex = 1;
            // 
            // dteTo
            // 
            this.dteTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dteTo.EditValue = null;
            this.dteTo.Enabled = false;
            this.dteTo.Location = new System.Drawing.Point(87, 64);
            this.dteTo.Name = "dteTo";
            this.dteTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteTo.Size = new System.Drawing.Size(159, 21);
            this.dteTo.TabIndex = 1;
            // 
            // dteFrom
            // 
            this.dteFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dteFrom.EditValue = null;
            this.dteFrom.Enabled = false;
            this.dteFrom.Location = new System.Drawing.Point(87, 37);
            this.dteFrom.Name = "dteFrom";
            this.dteFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteFrom.Size = new System.Drawing.Size(159, 21);
            this.dteFrom.TabIndex = 0;
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(5, 67);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(15, 14);
            this.labTo.TabIndex = 16;
            this.labTo.Text = "To";
            // 
            // labFrom
            // 
            this.labFrom.Location = new System.Drawing.Point(5, 40);
            this.labFrom.Name = "labFrom";
            this.labFrom.Size = new System.Drawing.Size(27, 14);
            this.labFrom.TabIndex = 17;
            this.labFrom.Text = "From";
            // 
            // chkDate
            // 
            this.chkDate.Location = new System.Drawing.Point(3, 12);
            this.chkDate.Name = "chkDate";
            this.chkDate.Properties.Caption = "Date";
            this.chkDate.Size = new System.Drawing.Size(182, 19);
            this.chkDate.TabIndex = 13;
            // 
            // nbarDuration
            // 
            this.nbarDuration.Caption = "Duration";
            this.nbarDuration.ControlContainer = this.navBarGroupControlContainer2;
            this.nbarDuration.Expanded = true;
            this.nbarDuration.GroupClientHeight = 131;
            this.nbarDuration.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarDuration.Name = "nbarDuration";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnClean);
            this.panelButtons.Controls.Add(this.btnSearch);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 596);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(255, 36);
            this.panelButtons.TabIndex = 0;
            // 
            // btnClean
            // 
            this.btnClean.Location = new System.Drawing.Point(14, 5);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(75, 23);
            this.btnClean.TabIndex = 1;
            this.btnClean.Text = "C&lean";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(103, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Sea&rch";
            // 
            // QuotedPriceSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.navBarControl1);
            this.Name = "QuotedPriceSearchPart";
            this.Size = new System.Drawing.Size(255, 632);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarControlBase.ResumeLayout(false);
            this.navBarControlBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.snumMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtNO.Properties)).EndInit();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            this.navBarGroupControlContainer2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelButtons)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nbarBase;
        private DevExpress.XtraEditors.PanelControl panelButtons;
        private DevExpress.XtraEditors.SimpleButton btnClean;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarControlBase;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private DevExpress.XtraNavBar.NavBarGroup nbarDuration;
        private DevExpress.XtraEditors.DateEdit dteTo;
        private DevExpress.XtraEditors.DateEdit dteFrom;
        private DevExpress.XtraEditors.LabelControl labTo;
        private DevExpress.XtraEditors.LabelControl labFrom;
        private DevExpress.XtraEditors.CheckEdit chkDate;
        private DevExpress.XtraEditors.LabelControl labNO;
        private DevExpress.XtraEditors.TextEdit stxtNO;
        private DevExpress.XtraEditors.LabelControl labCustomerName;
        protected DevExpress.XtraEditors.SpinEdit snumMax;
        protected DevExpress.XtraEditors.LabelControl labMax;
        private DevExpress.XtraEditors.LabelControl labQuoteBy;
        private Framework.ClientComponents.Controls.MultiSearchCommonBox txtQuoteBy;
        private DevExpress.XtraEditors.TextEdit stxtCustomerName;
        private Framework.ClientComponents.Controls.LWCheckButton lwchkIsValid;
        private DevExpress.XtraEditors.LabelControl labIsValid;
        private Framework.ClientComponents.Controls.LWCheckButton lwchkIsSure;
        private DevExpress.XtraEditors.LabelControl labIsSure;

    }
}
