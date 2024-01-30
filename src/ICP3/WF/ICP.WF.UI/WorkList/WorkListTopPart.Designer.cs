namespace ICP.WF.UI
{
    partial class WorkListTopPart
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
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.popupContainerControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.treeDepartment = new DevExpress.XtraTreeList.TreeList();
            this.colCShortName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colEShortName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.cmbOrganization = new DevExpress.XtraEditors.PopupContainerEdit();
            this.txtNo = new DevExpress.XtraEditors.TextEdit();
            this.labDepartment = new DevExpress.XtraEditors.LabelControl();
            this.labNo = new DevExpress.XtraEditors.LabelControl();
            this.labWorkName = new DevExpress.XtraEditors.LabelControl();
            this.txtWorkFlow = new DevExpress.XtraEditors.TextEdit();
            this.txtWorkName = new DevExpress.XtraEditors.TextEdit();
            this.labFlowName = new DevExpress.XtraEditors.LabelControl();
            this.errorList = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
            this.popupContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeDepartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOrganization.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkFlow.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.popupContainerControl1);
            this.pnlTop.Controls.Add(this.cmbOrganization);
            this.pnlTop.Controls.Add(this.txtNo);
            this.pnlTop.Controls.Add(this.labDepartment);
            this.pnlTop.Controls.Add(this.labNo);
            this.pnlTop.Controls.Add(this.labWorkName);
            this.pnlTop.Controls.Add(this.txtWorkFlow);
            this.pnlTop.Controls.Add(this.txtWorkName);
            this.pnlTop.Controls.Add(this.labFlowName);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(893, 94);
            this.pnlTop.TabIndex = 1;
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Controls.Add(this.treeDepartment);
            this.popupContainerControl1.Location = new System.Drawing.Point(369, 78);
            this.popupContainerControl1.Name = "popupContainerControl1";
            this.popupContainerControl1.Size = new System.Drawing.Size(318, 297);
            this.popupContainerControl1.TabIndex = 2;
            // 
            // treeDepartment
            // 
            this.treeDepartment.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colCShortName,
            this.colEShortName});
            this.treeDepartment.DataSource = this.bsList;
            this.treeDepartment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeDepartment.Location = new System.Drawing.Point(0, 0);
            this.treeDepartment.Name = "treeDepartment";
            this.treeDepartment.OptionsBehavior.Editable = false;
            this.treeDepartment.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.treeDepartment.OptionsView.EnableAppearanceEvenRow = true;
            this.treeDepartment.Size = new System.Drawing.Size(318, 297);
            this.treeDepartment.TabIndex = 0;
            this.treeDepartment.DoubleClick += new System.EventHandler(this.treeDepartment_DoubleClick);
            // 
            // colCShortName
            // 
            this.colCShortName.Caption = "部门";
            this.colCShortName.FieldName = "CShortName";
            this.colCShortName.Name = "colCShortName";
            this.colCShortName.Width = 40;
            // 
            // colEShortName
            // 
            this.colEShortName.Caption = "Department";
            this.colEShortName.FieldName = "EShortName";
            this.colEShortName.Name = "colEShortName";
            this.colEShortName.Width = 40;
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.Sys.ServiceInterface.DataObjects.OrganizationList);
            // 
            // cmbOrganization
            // 
            this.cmbOrganization.Location = new System.Drawing.Point(73, 37);
            this.cmbOrganization.Name = "cmbOrganization";
            this.cmbOrganization.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbOrganization.Properties.PopupControl = this.popupContainerControl1;
            this.cmbOrganization.Properties.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.cmbOrganization_Properties_QueryPopUp);
            this.cmbOrganization.Size = new System.Drawing.Size(647, 21);
            this.cmbOrganization.TabIndex = 9;
            this.cmbOrganization.EditValueChanged += new System.EventHandler(this.cmbOrganization_EditValueChanged);
            // 
            // txtNo
            // 
            this.txtNo.Location = new System.Drawing.Point(73, 7);
            this.txtNo.Name = "txtNo";
            this.txtNo.Properties.ReadOnly = true;
            this.txtNo.Size = new System.Drawing.Size(153, 21);
            this.txtNo.TabIndex = 0;
            // 
            // labDepartment
            // 
            this.labDepartment.Location = new System.Drawing.Point(4, 40);
            this.labDepartment.Name = "labDepartment";
            this.labDepartment.Size = new System.Drawing.Size(66, 14);
            this.labDepartment.TabIndex = 7;
            this.labDepartment.Text = "Department";
            // 
            // labNo
            // 
            this.labNo.Location = new System.Drawing.Point(4, 10);
            this.labNo.Name = "labNo";
            this.labNo.Size = new System.Drawing.Size(17, 14);
            this.labNo.TabIndex = 8;
            this.labNo.Text = "NO";
            // 
            // labWorkName
            // 
            this.labWorkName.Location = new System.Drawing.Point(4, 67);
            this.labWorkName.Name = "labWorkName";
            this.labWorkName.Size = new System.Drawing.Size(64, 14);
            this.labWorkName.TabIndex = 5;
            this.labWorkName.Text = "Work Name";
            // 
            // txtWorkFlow
            // 
            this.txtWorkFlow.Location = new System.Drawing.Point(369, 8);
            this.txtWorkFlow.Name = "txtWorkFlow";
            this.txtWorkFlow.Properties.ReadOnly = true;
            this.txtWorkFlow.Size = new System.Drawing.Size(351, 21);
            this.txtWorkFlow.TabIndex = 1;
            // 
            // txtWorkName
            // 
            this.txtWorkName.Location = new System.Drawing.Point(73, 64);
            this.txtWorkName.Name = "txtWorkName";
            this.txtWorkName.Properties.MaxLength = 250;
            this.txtWorkName.Size = new System.Drawing.Size(647, 21);
            this.txtWorkName.TabIndex = 3;
            // 
            // labFlowName
            // 
            this.labFlowName.Location = new System.Drawing.Point(301, 11);
            this.labFlowName.Name = "labFlowName";
            this.labFlowName.Size = new System.Drawing.Size(58, 14);
            this.labFlowName.TabIndex = 3;
            this.labFlowName.Text = "Work Flow";
            // 
            // errorList
            // 
            this.errorList.ContainerControl = this;
            // 
            // WorkListTopPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlTop);
            this.Name = "WorkListTopPart";
            this.Size = new System.Drawing.Size(893, 97);
            this.Controls.SetChildIndex(this.pnlTop, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.popupContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeDepartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOrganization.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkFlow.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlTop;
        private DevExpress.XtraEditors.TextEdit txtNo;
        private DevExpress.XtraEditors.LabelControl labDepartment;
        private DevExpress.XtraEditors.LabelControl labNo;
        private DevExpress.XtraEditors.LabelControl labWorkName;
        private DevExpress.XtraEditors.TextEdit txtWorkFlow;
        private DevExpress.XtraEditors.TextEdit txtWorkName;
        private DevExpress.XtraEditors.LabelControl labFlowName;
        private DevExpress.XtraEditors.PopupContainerEdit cmbOrganization;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl1;
        private DevExpress.XtraTreeList.TreeList treeDepartment;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCShortName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colEShortName;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorList;
    }
}
