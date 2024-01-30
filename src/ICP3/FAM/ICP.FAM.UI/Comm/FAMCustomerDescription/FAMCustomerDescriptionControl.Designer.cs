namespace ICP.FAM.UI.Comm
{
    partial class FAMCustomerDescriptionControl
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
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.txtFax = new DevExpress.XtraEditors.TextEdit();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.labFax = new DevExpress.XtraEditors.LabelControl();
            this.txtTel = new DevExpress.XtraEditors.TextEdit();
            this.labTel = new DevExpress.XtraEditors.LabelControl();
            this.labAddress = new DevExpress.XtraEditors.LabelControl();
            this.labName = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.MemoEdit();
            this.txtAddress = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(200, 124);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(54, 23);
            this.btnOK.TabIndex = 190;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(139, 124);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(54, 23);
            this.btnClear.TabIndex = 200;
            this.btnClear.Text = "C&lear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtFax
            // 
            this.txtFax.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Fax", true));
            this.txtFax.Location = new System.Drawing.Point(169, 97);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(85, 21);
            this.txtFax.TabIndex = 170;
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.FAMCustomerDescription);
            // 
            // labFax
            // 
            this.labFax.Location = new System.Drawing.Point(145, 100);
            this.labFax.Name = "labFax";
            this.labFax.Size = new System.Drawing.Size(18, 14);
            this.labFax.TabIndex = 7;
            this.labFax.Text = "Fax";
            // 
            // txtTel
            // 
            this.txtTel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Tel", true));
            this.txtTel.Location = new System.Drawing.Point(48, 97);
            this.txtTel.Name = "txtTel";
            this.txtTel.Size = new System.Drawing.Size(85, 21);
            this.txtTel.TabIndex = 160;
            // 
            // labTel
            // 
            this.labTel.Location = new System.Drawing.Point(2, 100);
            this.labTel.Name = "labTel";
            this.labTel.Size = new System.Drawing.Size(17, 14);
            this.labTel.TabIndex = 6;
            this.labTel.Text = "Tel";
            // 
            // labAddress
            // 
            this.labAddress.Location = new System.Drawing.Point(2, 34);
            this.labAddress.Name = "labAddress";
            this.labAddress.Size = new System.Drawing.Size(26, 14);
            this.labAddress.TabIndex = 3;
            this.labAddress.Text = "Add.";
            this.labAddress.ToolTip = "Address";
            // 
            // labName
            // 
            this.labName.Location = new System.Drawing.Point(2, 2);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(31, 14);
            this.labName.TabIndex = 9;
            this.labName.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Name", true));
            this.txtName.Location = new System.Drawing.Point(48, 2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(206, 31);
            this.txtName.TabIndex = 100;
            // 
            // txtAddress
            // 
            this.txtAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Address", true));
            this.txtAddress.Location = new System.Drawing.Point(48, 34);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(206, 54);
            this.txtAddress.TabIndex = 110;
            // 
            // FAMCustomerDescriptionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtFax);
            this.Controls.Add(this.labFax);
            this.Controls.Add(this.txtTel);
            this.Controls.Add(this.labTel);
            this.Controls.Add(this.labAddress);
            this.Controls.Add(this.labName);
            this.Name = "FAMCustomerDescriptionControl";
            this.Size = new System.Drawing.Size(257, 157);
            ((System.ComponentModel.ISupportInitialize)(this.txtFax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.TextEdit txtFax;
        private DevExpress.XtraEditors.LabelControl labFax;
        private DevExpress.XtraEditors.TextEdit txtTel;
        private DevExpress.XtraEditors.LabelControl labTel;
        private DevExpress.XtraEditors.LabelControl labAddress;
        private DevExpress.XtraEditors.LabelControl labName;
        private DevExpress.XtraEditors.MemoEdit txtName;
        private DevExpress.XtraEditors.MemoEdit txtAddress;
        private System.Windows.Forms.BindingSource bindingSource;

    }
}
