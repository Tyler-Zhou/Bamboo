namespace ICP.Common.Business.ServiceInterface
{
    partial class BkgFailed
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
            this.simpleOk = new DevExpress.XtraEditors.SimpleButton();
            this.simpleClose = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkSales = new DevExpress.XtraEditors.CheckEdit();
            this.checkCustomer = new DevExpress.XtraEditors.CheckEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.memoDescription = new DevExpress.XtraEditors.MemoEdit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkSales.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkCustomer.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleOk
            // 
            this.simpleOk.Location = new System.Drawing.Point(383, 237);
            this.simpleOk.Name = "simpleOk";
            this.simpleOk.Size = new System.Drawing.Size(87, 27);
            this.simpleOk.TabIndex = 4;
            this.simpleOk.Text = "OK";
            this.simpleOk.Click += new System.EventHandler(this.simpleOk_Click);
            // 
            // simpleClose
            // 
            this.simpleClose.Location = new System.Drawing.Point(496, 237);
            this.simpleClose.Name = "simpleClose";
            this.simpleClose.Size = new System.Drawing.Size(87, 27);
            this.simpleClose.TabIndex = 5;
            this.simpleClose.Text = "Close";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkSales);
            this.groupBox1.Controls.Add(this.checkCustomer);
            this.groupBox1.Location = new System.Drawing.Point(17, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(589, 74);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Recipient";
            // 
            // checkSales
            // 
            this.checkSales.EditValue = true;
            this.checkSales.Location = new System.Drawing.Point(6, 49);
            this.checkSales.Name = "checkSales";
            this.checkSales.Properties.Caption = "Mail to Sales";
            this.checkSales.Size = new System.Drawing.Size(203, 19);
            this.checkSales.TabIndex = 3;
            // 
            // checkCustomer
            // 
            this.checkCustomer.EditValue = true;
            this.checkCustomer.Location = new System.Drawing.Point(6, 21);
            this.checkCustomer.Name = "checkCustomer";
            this.checkCustomer.Properties.Caption = "Mail to Customer Service";
            this.checkCustomer.Size = new System.Drawing.Size(203, 19);
            this.checkCustomer.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.memoDescription);
            this.groupBox2.Controls.Add(this.simpleOk);
            this.groupBox2.Controls.Add(this.simpleClose);
            this.groupBox2.Location = new System.Drawing.Point(17, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(589, 270);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Description";
            // 
            // memoDescription
            // 
            this.memoDescription.Location = new System.Drawing.Point(6, 21);
            this.memoDescription.Name = "memoDescription";
            this.memoDescription.Size = new System.Drawing.Size(577, 191);
            this.memoDescription.TabIndex = 3;
            // 
            // BkgFailed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "BkgFailed";
            this.Size = new System.Drawing.Size(628, 380);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkSales.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkCustomer.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleOk;
        private DevExpress.XtraEditors.SimpleButton simpleClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.CheckEdit checkSales;
        private DevExpress.XtraEditors.CheckEdit checkCustomer;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.MemoEdit memoDescription;

    }
}
