namespace ICP.WF.UI
{
    partial class WorkListSearch
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
            this.bgBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.bgcBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.chcState = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.stxtOrganization = new ICP.Framework.ClientComponents.Controls.MiniFinderPopupContainerEdit();
            this.grxType = new DevExpress.XtraEditors.RadioGroup();
            this.numMax = new DevExpress.XtraEditors.SpinEdit();
            this.txtApplyName = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.txtWorkName = new DevExpress.XtraEditors.TextEdit();
            this.txtNo = new DevExpress.XtraEditors.TextEdit();
            this.cmbWorkFlowCode = new ICP.Framework.ClientComponents.Controls.LWComboBoxTree();
            this.labMaxCount = new DevExpress.XtraEditors.LabelControl();
            this.labStatus = new DevExpress.XtraEditors.LabelControl();
            this.labApplyName = new DevExpress.XtraEditors.LabelControl();
            this.labWorkName = new DevExpress.XtraEditors.LabelControl();
            this.labNo = new DevExpress.XtraEditors.LabelControl();
            this.labDepartment = new DevExpress.XtraEditors.LabelControl();
            this.labWorkFlow = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.labBegin = new DevExpress.XtraEditors.LabelControl();
            this.dateMonthControl1 = new ICP.Framework.ClientComponents.Controls.DateMonthControl();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.lwDatePicker2 = new ICP.WF.Controls.LWDatePicker();
            this.lwDatePicker1 = new ICP.WF.Controls.LWDatePicker();
            this.bgDate = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnclear = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.bgcBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chcState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtOrganization.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grxType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWorkFlowCode.Properties)).BeginInit();
            this.navBarGroupControlContainer1.SuspendLayout();
            this.navBarGroupControlContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.bgBase;
            this.navBarControl1.Controls.Add(this.bgcBase);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer2);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.bgBase,
            this.bgDate,
            this.navBarGroup1});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(192, 682);
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // bgBase
            // 
            this.bgBase.Caption = "Base";
            this.bgBase.ControlContainer = this.bgcBase;
            this.bgBase.Expanded = true;
            this.bgBase.GroupClientHeight = 281;
            this.bgBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.bgBase.Name = "bgBase";
            // 
            // bgcBase
            // 
            this.bgcBase.Controls.Add(this.chcState);
            this.bgcBase.Controls.Add(this.stxtOrganization);
            this.bgcBase.Controls.Add(this.grxType);
            this.bgcBase.Controls.Add(this.numMax);
            this.bgcBase.Controls.Add(this.txtApplyName);
            this.bgcBase.Controls.Add(this.txtWorkName);
            this.bgcBase.Controls.Add(this.txtNo);
            this.bgcBase.Controls.Add(this.cmbWorkFlowCode);
            this.bgcBase.Controls.Add(this.labMaxCount);
            this.bgcBase.Controls.Add(this.labStatus);
            this.bgcBase.Controls.Add(this.labApplyName);
            this.bgcBase.Controls.Add(this.labWorkName);
            this.bgcBase.Controls.Add(this.labNo);
            this.bgcBase.Controls.Add(this.labDepartment);
            this.bgcBase.Controls.Add(this.labWorkFlow);
            this.bgcBase.Name = "bgcBase";
            this.bgcBase.Size = new System.Drawing.Size(188, 279);
            this.bgcBase.TabIndex = 0;
            // 
            // chcState
            // 
            this.chcState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chcState.EditValue = "";
            this.chcState.EnterMoveNextControl = true;
            this.chcState.Location = new System.Drawing.Point(64, 227);
            this.chcState.Name = "chcState";
            this.chcState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chcState.Size = new System.Drawing.Size(121, 21);
            this.chcState.TabIndex = 9;
            // 
            // stxtOrganization
            // 
            this.stxtOrganization.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stxtOrganization.FinderDisplayMeber = null;
            this.stxtOrganization.FinderValueMember = null;
            this.stxtOrganization.Location = new System.Drawing.Point(64, 6);
            this.stxtOrganization.Name = "stxtOrganization";
            this.stxtOrganization.Properties.ActionButtonIndex = 1;
            this.stxtOrganization.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinDown)});
            this.stxtOrganization.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtOrganization.Properties.PopupFormSize = new System.Drawing.Size(500, 200);
            this.stxtOrganization.Properties.PopupSizeable = false;
            this.stxtOrganization.Properties.ShowPopupCloseButton = false;
            this.stxtOrganization.Size = new System.Drawing.Size(121, 21);
            this.stxtOrganization.TabIndex = 0;
            // 
            // grxType
            // 
            this.grxType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grxType.Location = new System.Drawing.Point(64, 130);
            this.grxType.Name = "grxType";
            this.grxType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("All", "全部"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("ME", "我创建的"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("CY", "我参与的"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("XS", "下属创建的")});
            this.grxType.Size = new System.Drawing.Size(121, 92);
            this.grxType.TabIndex = 4;
            // 
            // numMax
            // 
            this.numMax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numMax.EditValue = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numMax.Location = new System.Drawing.Point(64, 252);
            this.numMax.Name = "numMax";
            this.numMax.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numMax.Size = new System.Drawing.Size(120, 21);
            this.numMax.TabIndex = 6;
            // 
            // txtApplyName
            // 
            this.txtApplyName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtApplyName.EditText = "";
            this.txtApplyName.EditValue = null;
            this.txtApplyName.Location = new System.Drawing.Point(64, 107);
            this.txtApplyName.Name = "txtApplyName";
            this.txtApplyName.ReadOnly = false;
            this.txtApplyName.RefreshButtonToolTip = "";
            this.txtApplyName.ShowRefreshButton = false;
            this.txtApplyName.Size = new System.Drawing.Size(120, 21);
            this.txtApplyName.SpecifiedBackColor = System.Drawing.Color.White;
            this.txtApplyName.TabIndex = 3;
            this.txtApplyName.ToolTip = "";
            // 
            // txtWorkName
            // 
            this.txtWorkName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWorkName.Location = new System.Drawing.Point(64, 83);
            this.txtWorkName.Name = "txtWorkName";
            this.txtWorkName.Size = new System.Drawing.Size(120, 21);
            this.txtWorkName.TabIndex = 2;
            // 
            // txtNo
            // 
            this.txtNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNo.Location = new System.Drawing.Point(64, 58);
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(120, 21);
            this.txtNo.TabIndex = 1;
            // 
            // cmbWorkFlowCode
            // 
            this.cmbWorkFlowCode.AllowMultSelect = false;
            this.cmbWorkFlowCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWorkFlowCode.DataSource = null;
            this.cmbWorkFlowCode.DisplayMember = "CName";
            this.cmbWorkFlowCode.Location = new System.Drawing.Point(64, 33);
            this.cmbWorkFlowCode.Name = "cmbWorkFlowCode";
            this.cmbWorkFlowCode.ParentMember = "ParentID";
            this.cmbWorkFlowCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbWorkFlowCode.Properties.PopupSizeable = false;
            this.cmbWorkFlowCode.Properties.ShowPopupCloseButton = false;
            this.cmbWorkFlowCode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cmbWorkFlowCode.RootValue = 0;
            this.cmbWorkFlowCode.SelectedValue = null;
            this.cmbWorkFlowCode.Separator = ",";
            this.cmbWorkFlowCode.Size = new System.Drawing.Size(120, 21);
            this.cmbWorkFlowCode.TabIndex = 0;
            this.cmbWorkFlowCode.ValueMember = "ID";
            // 
            // labMaxCount
            // 
            this.labMaxCount.Location = new System.Drawing.Point(4, 255);
            this.labMaxCount.Name = "labMaxCount";
            this.labMaxCount.Size = new System.Drawing.Size(48, 14);
            this.labMaxCount.TabIndex = 8;
            this.labMaxCount.Text = "最大纪录";
            // 
            // labStatus
            // 
            this.labStatus.Location = new System.Drawing.Point(4, 230);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(24, 14);
            this.labStatus.TabIndex = 8;
            this.labStatus.Text = "状态";
            // 
            // labApplyName
            // 
            this.labApplyName.Location = new System.Drawing.Point(4, 110);
            this.labApplyName.Name = "labApplyName";
            this.labApplyName.Size = new System.Drawing.Size(36, 14);
            this.labApplyName.TabIndex = 1;
            this.labApplyName.Text = "申请人";
            // 
            // labWorkName
            // 
            this.labWorkName.Location = new System.Drawing.Point(4, 86);
            this.labWorkName.Name = "labWorkName";
            this.labWorkName.Size = new System.Drawing.Size(36, 14);
            this.labWorkName.TabIndex = 1;
            this.labWorkName.Text = "工作名";
            // 
            // labNo
            // 
            this.labNo.Location = new System.Drawing.Point(4, 61);
            this.labNo.Name = "labNo";
            this.labNo.Size = new System.Drawing.Size(24, 14);
            this.labNo.TabIndex = 1;
            this.labNo.Text = "单号";
            // 
            // labDepartment
            // 
            this.labDepartment.Location = new System.Drawing.Point(3, 9);
            this.labDepartment.Name = "labDepartment";
            this.labDepartment.Size = new System.Drawing.Size(24, 14);
            this.labDepartment.TabIndex = 1;
            this.labDepartment.Text = "部门";
            // 
            // labWorkFlow
            // 
            this.labWorkFlow.Location = new System.Drawing.Point(4, 36);
            this.labWorkFlow.Name = "labWorkFlow";
            this.labWorkFlow.Size = new System.Drawing.Size(24, 14);
            this.labWorkFlow.TabIndex = 1;
            this.labWorkFlow.Text = "流程";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.labTo);
            this.navBarGroupControlContainer1.Controls.Add(this.labBegin);
            this.navBarGroupControlContainer1.Controls.Add(this.dateMonthControl1);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(188, 236);
            this.navBarGroupControlContainer1.TabIndex = 1;
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(14, 154);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(12, 14);
            this.labTo.TabIndex = 1;
            this.labTo.Text = "到";
            // 
            // labBegin
            // 
            this.labBegin.Location = new System.Drawing.Point(15, 127);
            this.labBegin.Name = "labBegin";
            this.labBegin.Size = new System.Drawing.Size(12, 14);
            this.labBegin.TabIndex = 1;
            this.labBegin.Text = "从";
            // 
            // dateMonthControl1
            // 
            this.dateMonthControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateMonthControl1.From = null;
            this.dateMonthControl1.IsEngish = true;
            this.dateMonthControl1.Location = new System.Drawing.Point(46, 28);
            this.dateMonthControl1.Name = "dateMonthControl1";
            this.dateMonthControl1.Size = new System.Drawing.Size(122, 147);
            this.dateMonthControl1.TabIndex = 0;
            this.dateMonthControl1.To = null;
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.lwDatePicker2);
            this.navBarGroupControlContainer2.Controls.Add(this.lwDatePicker1);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(188, 181);
            this.navBarGroupControlContainer2.TabIndex = 2;
            // 
            // lwDatePicker2
            // 
            this.lwDatePicker2.AllowNull = true;
            this.lwDatePicker2.Caption = "";
            this.lwDatePicker2.ColumnName = "";
            this.lwDatePicker2.ColumnSpan = 1;
            this.lwDatePicker2.ControlProperty = "EditValue";
            this.lwDatePicker2.DataProperty = null;
            this.lwDatePicker2.EditValue = null;
            this.lwDatePicker2.FiledType = ICP.WF.Controls.FieldType.Other;
            this.lwDatePicker2.Location = new System.Drawing.Point(54, 98);
            this.lwDatePicker2.MaxLength = 0;
            this.lwDatePicker2.Name = "lwDatePicker2";
            this.lwDatePicker2.ReadOnly = false;
            this.lwDatePicker2.RowSpan = 1;
            this.lwDatePicker2.Size = new System.Drawing.Size(100, 21);
            this.lwDatePicker2.TabIndex = 2;
            // 
            // lwDatePicker1
            // 
            this.lwDatePicker1.AllowNull = true;
            this.lwDatePicker1.Caption = "";
            this.lwDatePicker1.ColumnName = "";
            this.lwDatePicker1.ColumnSpan = 1;
            this.lwDatePicker1.ControlProperty = "EditValue";
            this.lwDatePicker1.DataProperty = null;
            this.lwDatePicker1.EditValue = null;
            this.lwDatePicker1.FiledType = ICP.WF.Controls.FieldType.Other;
            this.lwDatePicker1.Location = new System.Drawing.Point(54, 47);
            this.lwDatePicker1.MaxLength = 0;
            this.lwDatePicker1.Name = "lwDatePicker1";
            this.lwDatePicker1.ReadOnly = false;
            this.lwDatePicker1.RowSpan = 1;
            this.lwDatePicker1.Size = new System.Drawing.Size(100, 21);
            this.lwDatePicker1.TabIndex = 1;
            // 
            // bgDate
            // 
            this.bgDate.Caption = "Date";
            this.bgDate.ControlContainer = this.navBarGroupControlContainer1;
            this.bgDate.Expanded = true;
            this.bgDate.GroupClientHeight = 238;
            this.bgDate.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.bgDate.Name = "bgDate";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "按结束时间查询";
            this.navBarGroup1.ControlContainer = this.navBarGroupControlContainer2;
            this.navBarGroup1.GroupClientHeight = 183;
            this.navBarGroup1.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnSearch);
            this.pnlBottom.Controls.Add(this.btnclear);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 682);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(192, 39);
            this.pnlBottom.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(109, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "查询(&S)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnclear
            // 
            this.btnclear.Location = new System.Drawing.Point(11, 10);
            this.btnclear.Name = "btnclear";
            this.btnclear.Size = new System.Drawing.Size(75, 23);
            this.btnclear.TabIndex = 0;
            this.btnclear.Text = "清空(&L)";
            this.btnclear.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // WorkListSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.navBarControl1);
            this.Controls.Add(this.pnlBottom);
            this.Name = "WorkListSearch";
            this.Size = new System.Drawing.Size(192, 721);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.bgcBase.ResumeLayout(false);
            this.bgcBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chcState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtOrganization.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grxType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWorkFlowCode.Properties)).EndInit();
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup bgBase;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraNavBar.NavBarGroup bgDate;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnclear;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer bgcBase;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraEditors.LabelControl labTo;
        private DevExpress.XtraEditors.LabelControl labBegin;
        private ICP.Framework.ClientComponents.Controls.DateMonthControl dateMonthControl1;
        private DevExpress.XtraEditors.LabelControl labWorkFlow;
        private DevExpress.XtraEditors.LabelControl labNo;
        private DevExpress.XtraEditors.LabelControl labWorkName;
        private DevExpress.XtraEditors.LabelControl labApplyName;
        private ICP.Framework.ClientComponents.Controls.LWComboBoxTree cmbWorkFlowCode;
        private DevExpress.XtraEditors.LabelControl labMaxCount;
        private DevExpress.XtraEditors.LabelControl labStatus;
        private DevExpress.XtraEditors.TextEdit txtNo;
        private DevExpress.XtraEditors.TextEdit txtWorkName;
        private DevExpress.XtraEditors.SpinEdit numMax;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox txtApplyName;
        private DevExpress.XtraEditors.RadioGroup grxType;
        private DevExpress.XtraEditors.LabelControl labDepartment;
        private ICP.Framework.ClientComponents.Controls.MiniFinderPopupContainerEdit stxtOrganization;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chcState;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private Controls.LWDatePicker lwDatePicker2;
        private Controls.LWDatePicker lwDatePicker1;
    }
}
