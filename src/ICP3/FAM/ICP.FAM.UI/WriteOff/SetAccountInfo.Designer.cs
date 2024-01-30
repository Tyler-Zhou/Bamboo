namespace ICP.FAM.UI.WriteOff
{
    partial class SetAccountInfo
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.dtpReachedDate = new DevExpress.XtraEditors.DateEdit();
            this.numFinalAmount = new DevExpress.XtraEditors.SpinEdit();
            this.cmbBankAccountID = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReachedDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReachedDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFinalAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBankAccountID.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Controls.Add(this.btnOk);
            this.panelControl1.Controls.Add(this.dtpReachedDate);
            this.panelControl1.Controls.Add(this.numFinalAmount);
            this.panelControl1.Controls.Add(this.cmbBankAccountID);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Location = new System.Drawing.Point(3, 15);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(273, 151);
            this.panelControl1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(51, 110);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(150, 110);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // dtpReachedDate
            // 
            this.dtpReachedDate.EditValue = null;
            this.dtpReachedDate.Location = new System.Drawing.Point(58, 12);
            this.dtpReachedDate.Name = "dtpReachedDate";
            this.dtpReachedDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpReachedDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpReachedDate.Size = new System.Drawing.Size(211, 21);
            this.dtpReachedDate.TabIndex = 0;
            // 
            // numFinalAmount
            // 
            this.numFinalAmount.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numFinalAmount.Location = new System.Drawing.Point(58, 42);
            this.numFinalAmount.Name = "numFinalAmount";
            this.numFinalAmount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numFinalAmount.Size = new System.Drawing.Size(211, 21);
            this.numFinalAmount.TabIndex = 1;
            // 
            // cmbBankAccountID
            // 
            this.cmbBankAccountID.Location = new System.Drawing.Point(58, 73);
            this.cmbBankAccountID.Name = "cmbBankAccountID";
            this.cmbBankAccountID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBankAccountID.Size = new System.Drawing.Size(211, 21);
            this.cmbBankAccountID.TabIndex = 2;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(7, 76);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 14);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "银行";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(7, 45);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "实收金额";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(7, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "到帐时间";
            // 
            // SetAccountInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "SetAccountInfo";
            this.Size = new System.Drawing.Size(283, 199);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReachedDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReachedDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFinalAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBankAccountID.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbBankAccountID;
        private DevExpress.XtraEditors.SpinEdit numFinalAmount;
        private DevExpress.XtraEditors.DateEdit dtpReachedDate;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOk;
    }
}
