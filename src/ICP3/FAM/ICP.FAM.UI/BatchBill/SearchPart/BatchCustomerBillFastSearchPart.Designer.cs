namespace ICP.FAM.UI.BatchBill
{
    partial class BatchCustomerBillFastSearchPart
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
            this.llabMore = new System.Windows.Forms.LinkLabel();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.txtNo = new DevExpress.XtraEditors.TextEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.stxtCustomer = new DevExpress.XtraEditors.TextEdit();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.labInvoiceNo = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomer.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // llabMore
            // 
            this.llabMore.AutoSize = true;
            this.llabMore.Location = new System.Drawing.Point(3, 7);
            this.llabMore.Name = "llabMore";
            this.llabMore.Size = new System.Drawing.Size(34, 14);
            this.llabMore.TabIndex = 17;
            this.llabMore.TabStop = true;
            this.llabMore.Text = "&More";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(445, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 19;
            this.btnSearch.Text = "&Search";
            // 
            // txtNo
            // 
            this.txtNo.Location = new System.Drawing.Point(140, 4);
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(89, 21);
            this.txtNo.TabIndex = 12;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.panel1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(897, 32);
            this.panelControl2.TabIndex = 22;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.labInvoiceNo);
            this.panel1.Controls.Add(this.labCustomer);
            this.panel1.Controls.Add(this.llabMore);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtNo);
            this.panel1.Controls.Add(this.stxtCustomer);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(893, 28);
            this.panel1.TabIndex = 0;
            // 
            // stxtCustomer
            // 
            this.stxtCustomer.Location = new System.Drawing.Point(326, 4);
            this.stxtCustomer.Name = "stxtCustomer";
            this.stxtCustomer.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtCustomer.Properties.Appearance.Options.UseBackColor = true;
            this.stxtCustomer.Size = new System.Drawing.Size(86, 21);
            this.stxtCustomer.TabIndex = 18;
            this.stxtCustomer.TabStop = false;
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(258, 7);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(52, 14);
            this.labCustomer.TabIndex = 20;
            this.labCustomer.Text = "Customer";
            // 
            // labInvoiceNo
            // 
            this.labInvoiceNo.Location = new System.Drawing.Point(63, 7);
            this.labInvoiceNo.Name = "labInvoiceNo";
            this.labInvoiceNo.Size = new System.Drawing.Size(58, 14);
            this.labInvoiceNo.TabIndex = 20;
            this.labInvoiceNo.Text = "Invoice No";
            // 
            // BatchCustomerBillFastSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Name = "BatchCustomerBillFastSearchPart";
            this.Size = new System.Drawing.Size(897, 32);
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomer.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.LinkLabel llabMore;
        protected DevExpress.XtraEditors.SimpleButton btnSearch;
        protected DevExpress.XtraEditors.TextEdit txtNo;
        protected DevExpress.XtraEditors.PanelControl panelControl2;
        protected System.Windows.Forms.Panel panel1;
        protected DevExpress.XtraEditors.TextEdit stxtCustomer;
        private DevExpress.XtraEditors.LabelControl labCustomer;
        private DevExpress.XtraEditors.LabelControl labInvoiceNo;

    }
}
