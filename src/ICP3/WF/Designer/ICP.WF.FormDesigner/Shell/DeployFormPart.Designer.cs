namespace ICP.WF.FormDesigner
{
    partial class DeployFormPart
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
            this.labKeyWord = new DevExpress.XtraEditors.LabelControl();
            this.labCName = new DevExpress.XtraEditors.LabelControl();
            this.labEName = new DevExpress.XtraEditors.LabelControl();
            this.labVersion = new DevExpress.XtraEditors.LabelControl();
            this.cmbKeys = new DevExpress.XtraEditors.LookUpEdit();
            this.txtCName = new DevExpress.XtraEditors.TextEdit();
            this.txtEName = new DevExpress.XtraEditors.TextEdit();
            this.numVersion = new DevExpress.XtraEditors.SpinEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.errorTip = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.cmbKeys.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVersion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorTip)).BeginInit();
            this.SuspendLayout();
            // 
            // labKeyWord
            // 
            this.labKeyWord.Location = new System.Drawing.Point(20, 14);
            this.labKeyWord.Name = "labKeyWord";
            this.labKeyWord.Size = new System.Drawing.Size(36, 14);
            this.labKeyWord.TabIndex = 0;
            this.labKeyWord.Text = "关键字";
            // 
            // labCName
            // 
            this.labCName.Location = new System.Drawing.Point(20, 42);
            this.labCName.Name = "labCName";
            this.labCName.Size = new System.Drawing.Size(48, 14);
            this.labCName.TabIndex = 0;
            this.labCName.Text = "中文名称";
            // 
            // labEName
            // 
            this.labEName.Location = new System.Drawing.Point(20, 70);
            this.labEName.Name = "labEName";
            this.labEName.Size = new System.Drawing.Size(48, 14);
            this.labEName.TabIndex = 0;
            this.labEName.Text = "英文名称";
            // 
            // labVersion
            // 
            this.labVersion.Location = new System.Drawing.Point(20, 98);
            this.labVersion.Name = "labVersion";
            this.labVersion.Size = new System.Drawing.Size(24, 14);
            this.labVersion.TabIndex = 0;
            this.labVersion.Text = "版本";
            // 
            // cmbKeys
            // 
            this.errorTip.SetIconAlignment(this.cmbKeys, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbKeys.Location = new System.Drawing.Point(96, 11);
            this.cmbKeys.Name = "cmbKeys";
            this.cmbKeys.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.cmbKeys.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbKeys.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("KeyWord", "关键字"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CName", "中文名称"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("EName", "英文名称"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Version", "版本")});
            this.cmbKeys.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cmbKeys.Size = new System.Drawing.Size(219, 21);
            this.cmbKeys.TabIndex = 0;
            this.cmbKeys.Leave += new System.EventHandler(this.cmbKeys_Leave);
            // 
            // txtCName
            // 
            this.errorTip.SetIconAlignment(this.txtCName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCName.Location = new System.Drawing.Point(96, 40);
            this.txtCName.Name = "txtCName";
            this.txtCName.Size = new System.Drawing.Size(219, 21);
            this.txtCName.TabIndex = 1;
            // 
            // txtEName
            // 
            this.errorTip.SetIconAlignment(this.txtEName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtEName.Location = new System.Drawing.Point(96, 67);
            this.txtEName.Name = "txtEName";
            this.txtEName.Size = new System.Drawing.Size(219, 21);
            this.txtEName.TabIndex = 2;
            // 
            // numVersion
            // 
            this.numVersion.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.errorTip.SetIconAlignment(this.numVersion, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.numVersion.Location = new System.Drawing.Point(96, 95);
            this.numVersion.Name = "numVersion";
            this.numVersion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numVersion.Size = new System.Drawing.Size(219, 21);
            this.numVersion.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(96, 133);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(240, 133);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // errorTip
            // 
            this.errorTip.ContainerControl = this;
            // 
            // DeployFormPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.numVersion);
            this.Controls.Add(this.txtEName);
            this.Controls.Add(this.txtCName);
            this.Controls.Add(this.cmbKeys);
            this.Controls.Add(this.labVersion);
            this.Controls.Add(this.labEName);
            this.Controls.Add(this.labCName);
            this.Controls.Add(this.labKeyWord);
            this.Name = "DeployFormPart";
            this.Size = new System.Drawing.Size(350, 175);
            ((System.ComponentModel.ISupportInitialize)(this.cmbKeys.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVersion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorTip)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labKeyWord;
        private DevExpress.XtraEditors.LabelControl labCName;
        private DevExpress.XtraEditors.LabelControl labEName;
        private DevExpress.XtraEditors.LabelControl labVersion;
        private DevExpress.XtraEditors.LookUpEdit cmbKeys;
        private DevExpress.XtraEditors.TextEdit txtCName;
        private DevExpress.XtraEditors.TextEdit txtEName;
        private DevExpress.XtraEditors.SpinEdit numVersion;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorTip;
    }
}
