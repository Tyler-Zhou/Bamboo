namespace ICP.Sys.UI.WorkSpaceView
{
    partial class WorkSpaceOPViewEdit
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
            this.labCName = new DevExpress.XtraEditors.LabelControl();
            this.txtCName = new DevExpress.XtraEditors.TextEdit();
            this.labCode = new DevExpress.XtraEditors.LabelControl();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.labEName = new DevExpress.XtraEditors.LabelControl();
            this.txtEName = new DevExpress.XtraEditors.TextEdit();
            this.labOperationType = new DevExpress.XtraEditors.LabelControl();
            this.cmbOperationType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labSelectedColumn = new DevExpress.XtraEditors.LabelControl();
            this.labBaseCriteria = new DevExpress.XtraEditors.LabelControl();
            this.labTooltiopCN = new DevExpress.XtraEditors.LabelControl();
            this.labToolTipEN = new DevExpress.XtraEditors.LabelControl();
            this.txtSelectedColumn = new DevExpress.XtraEditors.MemoEdit();
            this.txtBaseCriteria = new DevExpress.XtraEditors.MemoEdit();
            this.txtToolTipCN = new DevExpress.XtraEditors.MemoEdit();
            this.txtToolTipEN = new DevExpress.XtraEditors.MemoEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOperationType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSelectedColumn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBaseCriteria.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToolTipCN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToolTipEN.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labCName
            // 
            this.labCName.Location = new System.Drawing.Point(17, 45);
            this.labCName.Name = "labCName";
            this.labCName.Size = new System.Drawing.Size(36, 14);
            this.labCName.TabIndex = 22;
            this.labCName.Text = "中文名";
            // 
            // txtCName
            // 
            this.txtCName.Location = new System.Drawing.Point(62, 42);
            this.txtCName.Name = "txtCName";
            this.txtCName.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.txtCName.Properties.Appearance.Options.UseBackColor = true;
            this.txtCName.Properties.MaxLength = 200;
            this.txtCName.Size = new System.Drawing.Size(213, 21);
            this.txtCName.TabIndex = 2;
            // 
            // labCode
            // 
            this.labCode.Location = new System.Drawing.Point(321, 14);
            this.labCode.Name = "labCode";
            this.labCode.Size = new System.Drawing.Size(24, 14);
            this.labCode.TabIndex = 21;
            this.labCode.Text = "代码";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(371, 11);
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.txtCode.Properties.Appearance.Options.UseBackColor = true;
            this.txtCode.Properties.MaxLength = 100;
            this.txtCode.Size = new System.Drawing.Size(222, 21);
            this.txtCode.TabIndex = 1;
            // 
            // labEName
            // 
            this.labEName.Location = new System.Drawing.Point(321, 45);
            this.labEName.Name = "labEName";
            this.labEName.Size = new System.Drawing.Size(36, 14);
            this.labEName.TabIndex = 24;
            this.labEName.Text = "英文名";
            // 
            // txtEName
            // 
            this.txtEName.Location = new System.Drawing.Point(371, 42);
            this.txtEName.Name = "txtEName";
            this.txtEName.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.txtEName.Properties.Appearance.Options.UseBackColor = true;
            this.txtEName.Properties.MaxLength = 200;
            this.txtEName.Size = new System.Drawing.Size(222, 21);
            this.txtEName.TabIndex = 3;
            // 
            // labOperationType
            // 
            this.labOperationType.Location = new System.Drawing.Point(5, 14);
            this.labOperationType.Name = "labOperationType";
            this.labOperationType.Size = new System.Drawing.Size(48, 14);
            this.labOperationType.TabIndex = 25;
            this.labOperationType.Text = "业务类型";
            // 
            // cmbOperationType
            // 
            this.cmbOperationType.Location = new System.Drawing.Point(62, 11);
            this.cmbOperationType.Name = "cmbOperationType";
            this.cmbOperationType.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbOperationType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbOperationType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbOperationType.Size = new System.Drawing.Size(213, 21);
            this.cmbOperationType.TabIndex = 0;
            // 
            // labSelectedColumn
            // 
            this.labSelectedColumn.Location = new System.Drawing.Point(17, 72);
            this.labSelectedColumn.Name = "labSelectedColumn";
            this.labSelectedColumn.Size = new System.Drawing.Size(36, 14);
            this.labSelectedColumn.TabIndex = 31;
            this.labSelectedColumn.Text = "查询列";
            // 
            // labBaseCriteria
            // 
            this.labBaseCriteria.Location = new System.Drawing.Point(5, 158);
            this.labBaseCriteria.Name = "labBaseCriteria";
            this.labBaseCriteria.Size = new System.Drawing.Size(48, 14);
            this.labBaseCriteria.TabIndex = 32;
            this.labBaseCriteria.Text = "查询条件";
            // 
            // labTooltiopCN
            // 
            this.labTooltiopCN.Location = new System.Drawing.Point(5, 253);
            this.labTooltiopCN.Name = "labTooltiopCN";
            this.labTooltiopCN.Size = new System.Drawing.Size(48, 14);
            this.labTooltiopCN.TabIndex = 32;
            this.labTooltiopCN.Text = "中文描述";
            // 
            // labToolTipEN
            // 
            this.labToolTipEN.Location = new System.Drawing.Point(5, 317);
            this.labToolTipEN.Name = "labToolTipEN";
            this.labToolTipEN.Size = new System.Drawing.Size(48, 14);
            this.labToolTipEN.TabIndex = 32;
            this.labToolTipEN.Text = "英文描述";
            // 
            // txtSelectedColumn
            // 
            this.txtSelectedColumn.EditValue = "";
            this.txtSelectedColumn.Location = new System.Drawing.Point(62, 70);
            this.txtSelectedColumn.Name = "txtSelectedColumn";
            this.txtSelectedColumn.Properties.MaxLength = 2000;
            this.txtSelectedColumn.Size = new System.Drawing.Size(531, 80);
            this.txtSelectedColumn.TabIndex = 4;
            // 
            // txtBaseCriteria
            // 
            this.txtBaseCriteria.EditValue = "";
            this.txtBaseCriteria.Location = new System.Drawing.Point(62, 158);
            this.txtBaseCriteria.Name = "txtBaseCriteria";
            this.txtBaseCriteria.Properties.MaxLength = 2000;
            this.txtBaseCriteria.Size = new System.Drawing.Size(531, 87);
            this.txtBaseCriteria.TabIndex = 5;
            // 
            // txtToolTipCN
            // 
            this.txtToolTipCN.EditValue = "";
            this.txtToolTipCN.Location = new System.Drawing.Point(62, 253);
            this.txtToolTipCN.Name = "txtToolTipCN";
            this.txtToolTipCN.Properties.MaxLength = 2000;
            this.txtToolTipCN.Size = new System.Drawing.Size(531, 55);
            this.txtToolTipCN.TabIndex = 6;
            // 
            // txtToolTipEN
            // 
            this.txtToolTipEN.EditValue = "";
            this.txtToolTipEN.Location = new System.Drawing.Point(62, 317);
            this.txtToolTipEN.Name = "txtToolTipEN";
            this.txtToolTipEN.Properties.MaxLength = 2000;
            this.txtToolTipEN.Size = new System.Drawing.Size(531, 55);
            this.txtToolTipEN.TabIndex = 7;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(162, 386);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 100;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(371, 386);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "保存(&S)";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // WorkSpaceOPViewEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtToolTipEN);
            this.Controls.Add(this.txtToolTipCN);
            this.Controls.Add(this.txtBaseCriteria);
            this.Controls.Add(this.txtSelectedColumn);
            this.Controls.Add(this.labToolTipEN);
            this.Controls.Add(this.labTooltiopCN);
            this.Controls.Add(this.labBaseCriteria);
            this.Controls.Add(this.labSelectedColumn);
            this.Controls.Add(this.cmbOperationType);
            this.Controls.Add(this.labOperationType);
            this.Controls.Add(this.labEName);
            this.Controls.Add(this.txtEName);
            this.Controls.Add(this.labCName);
            this.Controls.Add(this.txtCName);
            this.Controls.Add(this.labCode);
            this.Controls.Add(this.txtCode);
            this.Name = "WorkSpaceOPViewEdit";
            this.Size = new System.Drawing.Size(616, 421);
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOperationType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSelectedColumn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBaseCriteria.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToolTipCN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToolTipEN.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labEName;
        private DevExpress.XtraEditors.TextEdit txtEName;
        private DevExpress.XtraEditors.LabelControl labCName;
        private DevExpress.XtraEditors.TextEdit txtCName;
        private DevExpress.XtraEditors.LabelControl labCode;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.LabelControl labOperationType;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbOperationType;
        private DevExpress.XtraEditors.LabelControl labSelectedColumn;
        private DevExpress.XtraEditors.LabelControl labBaseCriteria;
        private DevExpress.XtraEditors.LabelControl labTooltiopCN;
        private DevExpress.XtraEditors.LabelControl labToolTipEN;
        private DevExpress.XtraEditors.MemoEdit txtSelectedColumn;
        private DevExpress.XtraEditors.MemoEdit txtBaseCriteria;
        private DevExpress.XtraEditors.MemoEdit txtToolTipCN;
        private DevExpress.XtraEditors.MemoEdit txtToolTipEN;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnOK;
    }
}
