using ICP.Framework.ClientComponents.Controls;
namespace ICP.TMS.UI
{
    partial class DownLoadBusinessSearch
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        protected System.ComponentModel.IContainer components = null;

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
            this.bcMain = new DevExpress.XtraNavBar.NavBarControl();
            this.bgBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.bgcBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cmbCompany = new LWImageComboBoxEdit();
            this.cmbState = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.numMax = new DevExpress.XtraEditors.SpinEdit();
            this.labMaxNum = new DevExpress.XtraEditors.LabelControl();
            this.labVoyage = new DevExpress.XtraEditors.LabelControl();
            this.labVesselName = new DevExpress.XtraEditors.LabelControl();
            this.labState = new DevExpress.XtraEditors.LabelControl();
            this.labCtnNo = new DevExpress.XtraEditors.LabelControl();
            this.labType = new DevExpress.XtraEditors.LabelControl();
            this.labCustomerRefNo = new DevExpress.XtraEditors.LabelControl();
            this.labBLNO = new DevExpress.XtraEditors.LabelControl();
            this.txtCustomerRefNo = new DevExpress.XtraEditors.TextEdit();
            this.txtBLNo = new DevExpress.XtraEditors.TextEdit();
            this.txtBoxNo = new DevExpress.XtraEditors.TextEdit();
            this.txtVesselName = new DevExpress.XtraEditors.TextEdit();
            this.txtVoyage = new DevExpress.XtraEditors.TextEdit();
            this.bgcDate = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.dateMonthControl1 = new ICP.Framework.ClientComponents.Controls.DateMonthControl();
            this.labEndDate = new DevExpress.XtraEditors.LabelControl();
            this.labBeginDate = new DevExpress.XtraEditors.LabelControl();
            this.bgDate = new DevExpress.XtraNavBar.NavBarGroup();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.bcMain)).BeginInit();
            this.bcMain.SuspendLayout();
            this.bgcBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerRefNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBLNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBoxNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVesselName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoyage.Properties)).BeginInit();
            this.bgcDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // bcMain
            // 
            this.bcMain.ActiveGroup = this.bgBase;
            this.bcMain.Controls.Add(this.bgcBase);
            this.bcMain.Controls.Add(this.bgcDate);
            this.bcMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.bcMain.ExplorerBarGroupInterval = 2;
            this.bcMain.ExplorerBarGroupOuterIndent = 2;
            this.bcMain.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.bgBase,
            this.bgDate});
            this.bcMain.Location = new System.Drawing.Point(0, 0);
            this.bcMain.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.bcMain.Name = "bcMain";
            this.bcMain.OptionsNavPane.ExpandedWidth = 140;
            this.bcMain.Size = new System.Drawing.Size(205, 490);
            this.bcMain.TabIndex = 1;
            this.bcMain.Text = "navBarControl1";
            // 
            // bgBase
            // 
            this.bgBase.Caption = "基础";
            this.bgBase.ControlContainer = this.bgcBase;
            this.bgBase.Expanded = true;
            this.bgBase.GroupClientHeight = 254;
            this.bgBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.bgBase.Name = "bgBase";
            // 
            // bgcBase
            // 
            this.bgcBase.Controls.Add(this.labelControl1);
            this.bgcBase.Controls.Add(this.cmbCompany);
            this.bgcBase.Controls.Add(this.cmbState);
            this.bgcBase.Controls.Add(this.cmbType);
            this.bgcBase.Controls.Add(this.numMax);
            this.bgcBase.Controls.Add(this.labMaxNum);
            this.bgcBase.Controls.Add(this.labVoyage);
            this.bgcBase.Controls.Add(this.labVesselName);
            this.bgcBase.Controls.Add(this.labState);
            this.bgcBase.Controls.Add(this.labCtnNo);
            this.bgcBase.Controls.Add(this.labType);
            this.bgcBase.Controls.Add(this.labCustomerRefNo);
            this.bgcBase.Controls.Add(this.labBLNO);
            this.bgcBase.Controls.Add(this.txtCustomerRefNo);
            this.bgcBase.Controls.Add(this.txtBLNo);
            this.bgcBase.Controls.Add(this.txtBoxNo);
            this.bgcBase.Controls.Add(this.txtVesselName);
            this.bgcBase.Controls.Add(this.txtVoyage);
            this.bgcBase.Name = "bgcBase";
            this.bgcBase.Size = new System.Drawing.Size(197, 252);
            this.bgcBase.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(4, 34);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 17;
            this.labelControl1.Text = "操作口岸";
            // 
            // cmbCompany
            // 
            this.cmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCompany.Location = new System.Drawing.Point(79, 32);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCompany.Size = new System.Drawing.Size(115, 21);
            this.cmbCompany.TabIndex = 16;
            // 
            // cmbState
            // 
            this.cmbState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbState.Location = new System.Drawing.Point(79, 194);
            this.cmbState.Name = "cmbState";
            this.cmbState.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbState.Properties.Appearance.Options.UseBackColor = true;
            this.cmbState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbState.Size = new System.Drawing.Size(115, 21);
            this.cmbState.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbState.TabIndex = 15;
            // 
            // cmbType
            // 
            this.cmbType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbType.Location = new System.Drawing.Point(79, 4);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Size = new System.Drawing.Size(115, 21);
            this.cmbType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbType.TabIndex = 15;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // numMax
            // 
            this.numMax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numMax.EditValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMax.Location = new System.Drawing.Point(79, 221);
            this.numMax.Name = "numMax";
            this.numMax.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numMax.Properties.IsFloatValue = false;
            this.numMax.Properties.Mask.EditMask = "N00";
            this.numMax.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numMax.Size = new System.Drawing.Size(115, 21);
            this.numMax.TabIndex = 13;
            // 
            // labMaxNum
            // 
            this.labMaxNum.Location = new System.Drawing.Point(5, 224);
            this.labMaxNum.Name = "labMaxNum";
            this.labMaxNum.Size = new System.Drawing.Size(48, 14);
            this.labMaxNum.TabIndex = 14;
            this.labMaxNum.Text = "最大行数";
            // 
            // labVoyage
            // 
            this.labVoyage.Location = new System.Drawing.Point(5, 170);
            this.labVoyage.Name = "labVoyage";
            this.labVoyage.Size = new System.Drawing.Size(24, 14);
            this.labVoyage.TabIndex = 1;
            this.labVoyage.Text = "航次";
            // 
            // labVesselName
            // 
            this.labVesselName.Location = new System.Drawing.Point(5, 144);
            this.labVesselName.Name = "labVesselName";
            this.labVesselName.Size = new System.Drawing.Size(24, 14);
            this.labVesselName.TabIndex = 1;
            this.labVesselName.Text = "船名";
            // 
            // labState
            // 
            this.labState.Location = new System.Drawing.Point(4, 194);
            this.labState.Name = "labState";
            this.labState.Size = new System.Drawing.Size(24, 14);
            this.labState.TabIndex = 1;
            this.labState.Text = "状态";
            // 
            // labCtnNo
            // 
            this.labCtnNo.Location = new System.Drawing.Point(5, 118);
            this.labCtnNo.Name = "labCtnNo";
            this.labCtnNo.Size = new System.Drawing.Size(24, 14);
            this.labCtnNo.TabIndex = 1;
            this.labCtnNo.Text = "箱号";
            // 
            // labType
            // 
            this.labType.Location = new System.Drawing.Point(4, 4);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(29, 14);
            this.labType.TabIndex = 1;
            this.labType.Text = "进\\出";
            // 
            // labCustomerRefNo
            // 
            this.labCustomerRefNo.Location = new System.Drawing.Point(5, 64);
            this.labCustomerRefNo.Name = "labCustomerRefNo";
            this.labCustomerRefNo.Size = new System.Drawing.Size(60, 14);
            this.labCustomerRefNo.TabIndex = 1;
            this.labCustomerRefNo.Text = "客户参考号";
            // 
            // labBLNO
            // 
            this.labBLNO.Location = new System.Drawing.Point(5, 92);
            this.labBLNO.Name = "labBLNO";
            this.labBLNO.Size = new System.Drawing.Size(36, 14);
            this.labBLNO.TabIndex = 1;
            this.labBLNO.Text = "提单号";
            // 
            // txtCustomerRefNo
            // 
            this.txtCustomerRefNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomerRefNo.Location = new System.Drawing.Point(79, 61);
            this.txtCustomerRefNo.Name = "txtCustomerRefNo";
            this.txtCustomerRefNo.Size = new System.Drawing.Size(115, 21);
            this.txtCustomerRefNo.TabIndex = 2;
            // 
            // txtBLNo
            // 
            this.txtBLNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBLNo.Location = new System.Drawing.Point(79, 89);
            this.txtBLNo.Name = "txtBLNo";
            this.txtBLNo.Size = new System.Drawing.Size(115, 21);
            this.txtBLNo.TabIndex = 2;
            // 
            // txtBoxNo
            // 
            this.txtBoxNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxNo.Location = new System.Drawing.Point(79, 115);
            this.txtBoxNo.Name = "txtBoxNo";
            this.txtBoxNo.Size = new System.Drawing.Size(115, 21);
            this.txtBoxNo.TabIndex = 3;
            // 
            // txtVesselName
            // 
            this.txtVesselName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVesselName.Location = new System.Drawing.Point(79, 141);
            this.txtVesselName.Name = "txtVesselName";
            this.txtVesselName.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtVesselName.Properties.Appearance.Options.UseBackColor = true;
            this.txtVesselName.Size = new System.Drawing.Size(115, 21);
            this.txtVesselName.TabIndex = 12;
            // 
            // txtVoyage
            // 
            this.txtVoyage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVoyage.Location = new System.Drawing.Point(79, 167);
            this.txtVoyage.Name = "txtVoyage";
            this.txtVoyage.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtVoyage.Properties.Appearance.Options.UseBackColor = true;
            this.txtVoyage.Size = new System.Drawing.Size(115, 21);
            this.txtVoyage.TabIndex = 13;
            // 
            // bgcDate
            // 
            this.bgcDate.Controls.Add(this.dateMonthControl1);
            this.bgcDate.Controls.Add(this.labEndDate);
            this.bgcDate.Controls.Add(this.labBeginDate);
            this.bgcDate.Name = "bgcDate";
            this.bgcDate.Size = new System.Drawing.Size(197, 144);
            this.bgcDate.TabIndex = 1;
            // 
            // dateMonthControl1
            // 
            this.dateMonthControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dateMonthControl1.From = null;
            this.dateMonthControl1.IsEngish = true;
            this.dateMonthControl1.Location = new System.Drawing.Point(79, 2);
            this.dateMonthControl1.Name = "dateMonthControl1";
            this.dateMonthControl1.Size = new System.Drawing.Size(115, 139);
            this.dateMonthControl1.TabIndex = 3;
            this.dateMonthControl1.To = null;
            // 
            // labEndDate
            // 
            this.labEndDate.Location = new System.Drawing.Point(6, 117);
            this.labEndDate.Name = "labEndDate";
            this.labEndDate.Size = new System.Drawing.Size(12, 14);
            this.labEndDate.TabIndex = 14;
            this.labEndDate.Text = "到";
            // 
            // labBeginDate
            // 
            this.labBeginDate.Location = new System.Drawing.Point(6, 90);
            this.labBeginDate.Name = "labBeginDate";
            this.labBeginDate.Size = new System.Drawing.Size(12, 14);
            this.labBeginDate.TabIndex = 14;
            this.labBeginDate.Text = "从";
            // 
            // bgDate
            // 
            this.bgDate.Caption = "日期";
            this.bgDate.ControlContainer = this.bgcDate;
            this.bgDate.Expanded = true;
            this.bgDate.GroupClientHeight = 146;
            this.bgDate.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.bgDate.Name = "bgDate";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnClear);
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 496);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(205, 51);
            this.panelControl1.TabIndex = 4;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(17, 12);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(64, 23);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "清空(&L)";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(103, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(64, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "查询(&S)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.Controls.Add(this.bcMain);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(205, 496);
            this.pnlMain.TabIndex = 0;
            // 
            // DownLoadBusinessSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panelControl1);
            this.Name = "DownLoadBusinessSearch";
            this.Size = new System.Drawing.Size(205, 547);
            ((System.ComponentModel.ISupportInitialize)(this.bcMain)).EndInit();
            this.bcMain.ResumeLayout(false);
            this.bgcBase.ResumeLayout(false);
            this.bgcBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerRefNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBLNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBoxNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVesselName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoyage.Properties)).EndInit();
            this.bgcDate.ResumeLayout(false);
            this.bgcDate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraNavBar.NavBarControl bcMain;
        protected DevExpress.XtraNavBar.NavBarGroup bgBase;
        protected DevExpress.XtraNavBar.NavBarGroupControlContainer bgcBase;
        protected DevExpress.XtraEditors.LabelControl labBLNO;
        protected DevExpress.XtraEditors.TextEdit txtBLNo;
        protected DevExpress.XtraEditors.LabelControl labCtnNo;
        protected DevExpress.XtraNavBar.NavBarGroupControlContainer bgcDate;
        protected DevExpress.XtraNavBar.NavBarGroup bgDate;
        protected DevExpress.XtraEditors.TextEdit txtBoxNo;
        protected DevExpress.XtraEditors.PanelControl panelControl1;
        protected DevExpress.XtraEditors.SimpleButton btnClear;
        protected DevExpress.XtraEditors.SimpleButton btnSearch;
        protected System.Windows.Forms.Panel pnlMain;
        protected DevExpress.XtraEditors.LabelControl labVoyage;
        protected DevExpress.XtraEditors.LabelControl labVesselName;
        protected DevExpress.XtraEditors.TextEdit txtVesselName;
        protected DevExpress.XtraEditors.TextEdit txtVoyage;
        private DevExpress.XtraEditors.SpinEdit numMax;
        private DevExpress.XtraEditors.LabelControl labMaxNum;
        protected DevExpress.XtraEditors.LabelControl labType;
        private LWImageComboBoxEdit cmbType;
        private LWImageComboBoxEdit cmbState;
        protected DevExpress.XtraEditors.LabelControl labState;
        protected DevExpress.XtraEditors.LabelControl labCustomerRefNo;
        protected DevExpress.XtraEditors.TextEdit txtCustomerRefNo;
        private DateMonthControl dateMonthControl1;
        private DevExpress.XtraEditors.LabelControl labEndDate;
        private DevExpress.XtraEditors.LabelControl labBeginDate;
        private LWImageComboBoxEdit cmbCompany;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}

