namespace ICP.MailCenter.Business.UI
{
    partial class WorkFlowBusinessQueryPart
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
            this.chcState = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.stxtOrganization = new ICP.Framework.ClientComponents.Controls.MiniFinderPopupContainerEdit();
            this.grxType = new DevExpress.XtraEditors.RadioGroup();
            this.txtApplyName = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.txtWorkName = new DevExpress.XtraEditors.TextEdit();
            this.txtNo = new DevExpress.XtraEditors.TextEdit();
            this.cmbWorkFlowCode = new ICP.Framework.ClientComponents.Controls.LWComboBoxTree();
            this.labStatus = new DevExpress.XtraEditors.LabelControl();
            this.labApplyName = new DevExpress.XtraEditors.LabelControl();
            this.labWorkName = new DevExpress.XtraEditors.LabelControl();
            this.labNo = new DevExpress.XtraEditors.LabelControl();
            this.labDepartment = new DevExpress.XtraEditors.LabelControl();
            this.labWorkFlow = new DevExpress.XtraEditors.LabelControl();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.labBegin = new DevExpress.XtraEditors.LabelControl();
            this.dateMonthControl1 = new ICP.Framework.ClientComponents.Controls.DateMonthControl();
            this.gcDate = new DevExpress.XtraEditors.GroupControl();
            this.gcType = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.chcState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtOrganization.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grxType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWorkFlowCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDate)).BeginInit();
            this.gcDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcType)).BeginInit();
            this.gcType.SuspendLayout();
            this.SuspendLayout();
            // 
            // chcState
            // 
            this.chcState.EditValue = "";
            this.chcState.EnterMoveNextControl = true;
            this.chcState.Location = new System.Drawing.Point(75, 160);
            this.chcState.Name = "chcState";
            this.chcState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chcState.Size = new System.Drawing.Size(179, 21);
            this.chcState.TabIndex = 5;
            // 
            // stxtOrganization
            // 
            this.stxtOrganization.FinderDisplayMeber = null;
            this.stxtOrganization.FinderValueMember = null;
            this.stxtOrganization.Location = new System.Drawing.Point(75, 10);
            this.stxtOrganization.Name = "stxtOrganization";
            this.stxtOrganization.Properties.ActionButtonIndex = 1;
            this.stxtOrganization.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinDown)});
            this.stxtOrganization.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.stxtOrganization.Properties.PopupFormSize = new System.Drawing.Size(500, 200);
            this.stxtOrganization.Properties.PopupSizeable = false;
            this.stxtOrganization.Properties.ShowPopupCloseButton = false;
            this.stxtOrganization.Size = new System.Drawing.Size(179, 21);
            this.stxtOrganization.TabIndex = 0;
            // 
            // grxType
            // 
            this.grxType.Location = new System.Drawing.Point(27, 26);
            this.grxType.Name = "grxType";
            this.grxType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("All", "全部"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("ME", "我创建的"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("CY", "我参与的"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("XS", "下属创建的")});
            this.grxType.Size = new System.Drawing.Size(165, 146);
            this.grxType.TabIndex = 0;
            // 
            // txtApplyName
            // 
            this.txtApplyName.EditText = "";
            this.txtApplyName.EditValue = null;
            this.txtApplyName.Location = new System.Drawing.Point(75, 130);
            this.txtApplyName.Name = "txtApplyName";
            this.txtApplyName.ReadOnly = false;
            this.txtApplyName.RefreshButtonToolTip = "";
            this.txtApplyName.ShowRefreshButton = false;
            this.txtApplyName.Size = new System.Drawing.Size(179, 21);
            this.txtApplyName.SpecifiedBackColor = System.Drawing.Color.White;
            this.txtApplyName.TabIndex = 4;
            this.txtApplyName.ToolTip = "";
            // 
            // txtWorkName
            // 
            this.txtWorkName.Location = new System.Drawing.Point(75, 100);
            this.txtWorkName.Name = "txtWorkName";
            this.txtWorkName.Size = new System.Drawing.Size(179, 21);
            this.txtWorkName.TabIndex = 3;
            // 
            // txtNo
            // 
            this.txtNo.Location = new System.Drawing.Point(75, 70);
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(179, 21);
            this.txtNo.TabIndex = 2;
            // 
            // cmbWorkFlowCode
            // 
            this.cmbWorkFlowCode.AllowMultSelect = false;
            this.cmbWorkFlowCode.DataSource = null;
            this.cmbWorkFlowCode.DisplayMember = "CName";
            this.cmbWorkFlowCode.Location = new System.Drawing.Point(75, 40);
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
            this.cmbWorkFlowCode.Size = new System.Drawing.Size(179, 21);
            this.cmbWorkFlowCode.TabIndex = 1;
            this.cmbWorkFlowCode.ValueMember = "ID";
            // 
            // labStatus
            // 
            this.labStatus.Location = new System.Drawing.Point(11, 166);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(24, 14);
            this.labStatus.TabIndex = 23;
            this.labStatus.Text = "状态";
            // 
            // labApplyName
            // 
            this.labApplyName.Location = new System.Drawing.Point(11, 135);
            this.labApplyName.Name = "labApplyName";
            this.labApplyName.Size = new System.Drawing.Size(36, 14);
            this.labApplyName.TabIndex = 13;
            this.labApplyName.Text = "申请人";
            // 
            // labWorkName
            // 
            this.labWorkName.Location = new System.Drawing.Point(11, 100);
            this.labWorkName.Name = "labWorkName";
            this.labWorkName.Size = new System.Drawing.Size(36, 14);
            this.labWorkName.TabIndex = 14;
            this.labWorkName.Text = "工作名";
            // 
            // labNo
            // 
            this.labNo.Location = new System.Drawing.Point(11, 71);
            this.labNo.Name = "labNo";
            this.labNo.Size = new System.Drawing.Size(24, 14);
            this.labNo.TabIndex = 15;
            this.labNo.Text = "单号";
            // 
            // labDepartment
            // 
            this.labDepartment.Location = new System.Drawing.Point(11, 13);
            this.labDepartment.Name = "labDepartment";
            this.labDepartment.Size = new System.Drawing.Size(24, 14);
            this.labDepartment.TabIndex = 16;
            this.labDepartment.Text = "部门";
            // 
            // labWorkFlow
            // 
            this.labWorkFlow.Location = new System.Drawing.Point(11, 43);
            this.labWorkFlow.Name = "labWorkFlow";
            this.labWorkFlow.Size = new System.Drawing.Size(24, 14);
            this.labWorkFlow.TabIndex = 17;
            this.labWorkFlow.Text = "流程";
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(13, 152);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(12, 14);
            this.labTo.TabIndex = 27;
            this.labTo.Text = "到";
            // 
            // labBegin
            // 
            this.labBegin.Location = new System.Drawing.Point(13, 124);
            this.labBegin.Name = "labBegin";
            this.labBegin.Size = new System.Drawing.Size(12, 14);
            this.labBegin.TabIndex = 26;
            this.labBegin.Text = "从";
            // 
            // dateMonthControl1
            // 
            this.dateMonthControl1.From = null;
            this.dateMonthControl1.IsEngish = true;
            this.dateMonthControl1.Location = new System.Drawing.Point(60, 26);
            this.dateMonthControl1.Name = "dateMonthControl1";
            this.dateMonthControl1.Size = new System.Drawing.Size(165, 147);
            this.dateMonthControl1.TabIndex = 1;
            this.dateMonthControl1.To = null;
            // 
            // gcDate
            // 
            this.gcDate.Controls.Add(this.dateMonthControl1);
            this.gcDate.Controls.Add(this.labTo);
            this.gcDate.Controls.Add(this.labBegin);
            this.gcDate.Location = new System.Drawing.Point(481, 9);
            this.gcDate.Name = "gcDate";
            this.gcDate.Size = new System.Drawing.Size(237, 180);
            this.gcDate.TabIndex = 7;
            this.gcDate.Text = "申请时间";
            // 
            // gcType
            // 
            this.gcType.Controls.Add(this.grxType);
            this.gcType.Location = new System.Drawing.Point(265, 9);
            this.gcType.Name = "gcType";
            this.gcType.Size = new System.Drawing.Size(210, 180);
            this.gcType.TabIndex = 6;
            this.gcType.Text = "类型";
            // 
            // WorkFlowBusinessQueryPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcType);
            this.Controls.Add(this.gcDate);
            this.Controls.Add(this.chcState);
            this.Controls.Add(this.stxtOrganization);
            this.Controls.Add(this.txtApplyName);
            this.Controls.Add(this.txtWorkName);
            this.Controls.Add(this.txtNo);
            this.Controls.Add(this.cmbWorkFlowCode);
            this.Controls.Add(this.labStatus);
            this.Controls.Add(this.labApplyName);
            this.Controls.Add(this.labWorkName);
            this.Controls.Add(this.labNo);
            this.Controls.Add(this.labDepartment);
            this.Controls.Add(this.labWorkFlow);
            this.Name = "WorkFlowBusinessQueryPart";
            this.Size = new System.Drawing.Size(729, 207);
            ((System.ComponentModel.ISupportInitialize)(this.chcState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtOrganization.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grxType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWorkFlowCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDate)).EndInit();
            this.gcDate.ResumeLayout(false);
            this.gcDate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcType)).EndInit();
            this.gcType.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.CheckedComboBoxEdit chcState;
        private Framework.ClientComponents.Controls.MiniFinderPopupContainerEdit stxtOrganization;
        private DevExpress.XtraEditors.RadioGroup grxType;
        private Framework.ClientComponents.Controls.MultiSearchCommonBox txtApplyName;
        private DevExpress.XtraEditors.TextEdit txtWorkName;
        private DevExpress.XtraEditors.TextEdit txtNo;
        private Framework.ClientComponents.Controls.LWComboBoxTree cmbWorkFlowCode;
        private DevExpress.XtraEditors.LabelControl labStatus;
        private DevExpress.XtraEditors.LabelControl labApplyName;
        private DevExpress.XtraEditors.LabelControl labWorkName;
        private DevExpress.XtraEditors.LabelControl labNo;
        private DevExpress.XtraEditors.LabelControl labDepartment;
        private DevExpress.XtraEditors.LabelControl labWorkFlow;
        private DevExpress.XtraEditors.LabelControl labTo;
        private DevExpress.XtraEditors.LabelControl labBegin;
        private Framework.ClientComponents.Controls.DateMonthControl dateMonthControl1;
        private DevExpress.XtraEditors.GroupControl gcDate;
        private DevExpress.XtraEditors.GroupControl gcType;

    }
}
