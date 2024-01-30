namespace ICP.FCM.Common.UI.BillRevise
{
    partial class OEBillRevisePartNew
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
            this.gcRemark = new DevExpress.XtraEditors.GroupControl();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.lblDisptDate = new System.Windows.Forms.Label();
            this.lblDisptDateValue = new System.Windows.Forms.Label();
            this.lblDisptUserValue = new System.Windows.Forms.Label();
            this.lblDisptUser = new System.Windows.Forms.Label();
            this.pnlCenter = new DevExpress.XtraEditors.PanelControl();
            this.pnlFill = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcRemark)).BeginInit();
            this.gcRemark.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCenter)).BeginInit();
            this.pnlCenter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.gcRemark);
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 522);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(802, 68);
            this.panelControl1.TabIndex = 4;
            // 
            // gcRemark
            // 
            this.gcRemark.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcRemark.Controls.Add(this.txtRemark);
            this.gcRemark.Location = new System.Drawing.Point(109, 4);
            this.gcRemark.Name = "gcRemark";
            this.gcRemark.Size = new System.Drawing.Size(690, 64);
            this.gcRemark.TabIndex = 2;
            this.gcRemark.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRemark.Enabled = false;
            this.txtRemark.Location = new System.Drawing.Point(2, 23);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(686, 39);
            this.txtRemark.TabIndex = 0;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(3, 5);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(102, 58);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "签     收";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lblDisptDate);
            this.pnlTop.Controls.Add(this.lblDisptDateValue);
            this.pnlTop.Controls.Add(this.lblDisptUserValue);
            this.pnlTop.Controls.Add(this.lblDisptUser);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(802, 34);
            this.pnlTop.TabIndex = 5;
            // 
            // lblDisptDate
            // 
            this.lblDisptDate.AutoSize = true;
            this.lblDisptDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDisptDate.Location = new System.Drawing.Point(215, 10);
            this.lblDisptDate.Name = "lblDisptDate";
            this.lblDisptDate.Size = new System.Drawing.Size(67, 14);
            this.lblDisptDate.TabIndex = 11;
            this.lblDisptDate.Text = "申请时间：";
            // 
            // lblDisptDateValue
            // 
            this.lblDisptDateValue.AutoSize = true;
            this.lblDisptDateValue.BackColor = System.Drawing.Color.Transparent;
            this.lblDisptDateValue.Location = new System.Drawing.Point(302, 10);
            this.lblDisptDateValue.Name = "lblDisptDateValue";
            this.lblDisptDateValue.Size = new System.Drawing.Size(129, 14);
            this.lblDisptDateValue.TabIndex = 12;
            this.lblDisptDateValue.Text = "2013-06-14  12:23:34";
            // 
            // lblDisptUserValue
            // 
            this.lblDisptUserValue.AutoSize = true;
            this.lblDisptUserValue.BackColor = System.Drawing.Color.Transparent;
            this.lblDisptUserValue.Location = new System.Drawing.Point(106, 10);
            this.lblDisptUserValue.Name = "lblDisptUserValue";
            this.lblDisptUserValue.Size = new System.Drawing.Size(24, 14);
            this.lblDisptUserValue.TabIndex = 10;
            this.lblDisptUserValue.Text = "joe";
            // 
            // lblDisptUser
            // 
            this.lblDisptUser.AutoSize = true;
            this.lblDisptUser.BackColor = System.Drawing.Color.Transparent;
            this.lblDisptUser.Location = new System.Drawing.Point(29, 10);
            this.lblDisptUser.Name = "lblDisptUser";
            this.lblDisptUser.Size = new System.Drawing.Size(55, 14);
            this.lblDisptUser.TabIndex = 9;
            this.lblDisptUser.Text = "申请人：";
            // 
            // pnlCenter
            // 
            this.pnlCenter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.pnlCenter.Controls.Add(this.pnlFill);
            this.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCenter.Location = new System.Drawing.Point(0, 34);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Size = new System.Drawing.Size(802, 488);
            this.pnlCenter.TabIndex = 6;
            // 
            // pnlFill
            // 
            this.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFill.Location = new System.Drawing.Point(2, 2);
            this.pnlFill.Name = "pnlFill";
            this.pnlFill.Size = new System.Drawing.Size(798, 484);
            this.pnlFill.TabIndex = 0;
            // 
            // OEBillRevisePartNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlCenter);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.panelControl1);
            this.Name = "OEBillRevisePartNew";
            this.Size = new System.Drawing.Size(802, 590);
            this.Load += new System.EventHandler(this.OEBillRevisePartNew_Load);
            this.Resize += new System.EventHandler(this.OEBillRevisePartNew_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcRemark)).EndInit();
            this.gcRemark.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCenter)).EndInit();
            this.pnlCenter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Practices.CompositeUI.WinForms.TabWorkspace tabWsCampare;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl gcRemark;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.PanelControl pnlTop;
        private System.Windows.Forms.Label lblDisptDate;
        private System.Windows.Forms.Label lblDisptDateValue;
        private System.Windows.Forms.Label lblDisptUserValue;
        private System.Windows.Forms.Label lblDisptUser;
        private DevExpress.XtraEditors.PanelControl pnlCenter;
        private System.Windows.Forms.Panel pnlFill;
    }
}
