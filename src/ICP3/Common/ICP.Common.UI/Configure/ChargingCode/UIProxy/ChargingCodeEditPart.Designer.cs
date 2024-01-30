namespace ICP.Common.UI.Configure.ChargingCode
{
    partial class ChargingCodeEditPart
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
            this.labCode = new DevExpress.XtraEditors.LabelControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.labEName = new DevExpress.XtraEditors.LabelControl();
            this.labCName = new DevExpress.XtraEditors.LabelControl();
            this.labParent = new DevExpress.XtraEditors.LabelControl();
            this.txtEName = new DevExpress.XtraEditors.TextEdit();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.txtCName = new DevExpress.XtraEditors.TextEdit();
            this.chkIsCommission = new DevExpress.XtraEditors.CheckEdit();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.bsParent = new System.Windows.Forms.BindingSource(this.components);
            this.cmbCategary = new ICP.Framework.ClientComponents.Controls.LWComboBoxTree();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsCommission.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsParent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCategary.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labCode
            // 
            this.labCode.Location = new System.Drawing.Point(6, 6);
            this.labCode.Name = "labCode";
            this.labCode.Size = new System.Drawing.Size(28, 14);
            this.labCode.TabIndex = 60;
            this.labCode.Text = "Code";
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.ChargingCodeInfo);
            // 
            // labEName
            // 
            this.labEName.Location = new System.Drawing.Point(6, 60);
            this.labEName.Name = "labEName";
            this.labEName.Size = new System.Drawing.Size(38, 14);
            this.labEName.TabIndex = 59;
            this.labEName.Text = "EName";
            // 
            // labCName
            // 
            this.labCName.Location = new System.Drawing.Point(6, 33);
            this.labCName.Name = "labCName";
            this.labCName.Size = new System.Drawing.Size(38, 14);
            this.labCName.TabIndex = 61;
            this.labCName.Text = "CName";
            // 
            // labParent
            // 
            this.labParent.Location = new System.Drawing.Point(6, 112);
            this.labParent.Name = "labParent";
            this.labParent.Size = new System.Drawing.Size(49, 14);
            this.labParent.TabIndex = 62;
            this.labParent.Text = "Category";
            // 
            // txtEName
            // 
            this.txtEName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "EName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtEName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtEName.Location = new System.Drawing.Point(74, 57);
            this.txtEName.Name = "txtEName";
            this.txtEName.Properties.MaxLength = 100;
            this.txtEName.Size = new System.Drawing.Size(249, 21);
            this.txtEName.TabIndex = 2;
            // 
            // txtCode
            // 
            this.txtCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Code", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtCode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCode.Location = new System.Drawing.Point(74, 3);
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.MaxLength = 10;
            this.txtCode.Size = new System.Drawing.Size(249, 21);
            this.txtCode.TabIndex = 0;
            // 
            // txtCName
            // 
            this.txtCName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "CName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtCName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCName.Location = new System.Drawing.Point(74, 30);
            this.txtCName.Name = "txtCName";
            this.txtCName.Properties.MaxLength = 100;
            this.txtCName.Size = new System.Drawing.Size(249, 21);
            this.txtCName.TabIndex = 1;
            // 
            // chkIsCommission
            // 
            this.chkIsCommission.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "IsCommission", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkIsCommission.Location = new System.Drawing.Point(72, 84);
            this.chkIsCommission.Name = "chkIsCommission";
            this.chkIsCommission.Properties.Caption = "IsCommission";
            this.chkIsCommission.Size = new System.Drawing.Size(111, 19);
            this.chkIsCommission.TabIndex = 3;
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bindingSource1;
            // 
            // bsParent
            // 
            this.bsParent.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.ChargingGroupList);
            // 
            // cmbCategary
            // 
            this.cmbCategary.AllowMultSelect = false;
            this.cmbCategary.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingSource1, "GroupID", true));
            this.cmbCategary.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "GroupName", true));
            this.cmbCategary.DataSource = null;
            this.cmbCategary.DisplayMember = "CName";
            this.cmbCategary.Location = new System.Drawing.Point(74, 109);
            this.cmbCategary.Name = "cmbCategary";
            this.cmbCategary.ParentMember = "ParentID";
            this.cmbCategary.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCategary.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbCategary.Properties.PopupSizeable = false;
            this.cmbCategary.Properties.ShowPopupCloseButton = false;
            this.cmbCategary.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cmbCategary.RootValue = 0;
            this.cmbCategary.SelectedValue = null;
            this.cmbCategary.Separator = ",";
            this.cmbCategary.Size = new System.Drawing.Size(249, 21);
            this.cmbCategary.TabIndex = 63;
            this.cmbCategary.ValueMember = "ID";
            // 
            // ChargingCodeEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.cmbCategary);
            this.Controls.Add(this.chkIsCommission);
            this.Controls.Add(this.labCode);
            this.Controls.Add(this.labEName);
            this.Controls.Add(this.labCName);
            this.Controls.Add(this.labParent);
            this.Controls.Add(this.txtEName);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtCName);
            this.Name = "ChargingCodeEditPart";
            this.Size = new System.Drawing.Size(335, 156);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsCommission.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsParent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCategary.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bsParent;
        private DevExpress.XtraEditors.LabelControl labCode;
        private DevExpress.XtraEditors.LabelControl labEName;
        private DevExpress.XtraEditors.LabelControl labCName;
        private DevExpress.XtraEditors.LabelControl labParent;
        private DevExpress.XtraEditors.TextEdit txtEName;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.TextEdit txtCName;
        private DevExpress.XtraEditors.CheckEdit chkIsCommission;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private ICP.Framework.ClientComponents.Controls.LWComboBoxTree cmbCategary;
    }
}
