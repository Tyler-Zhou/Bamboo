
namespace ICP.FCM.OceanExport.UI.Common.Parts
{
    partial class UCInquirePrice
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
            this.grbInquirePrice = new System.Windows.Forms.GroupBox();
            this.chkNeedConfirmation = new DevExpress.XtraEditors.CheckEdit();
            this.lblConfirmed = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.bsInquirePrice = new System.Windows.Forms.BindingSource(this.components);
            this.btnNew = new DevExpress.XtraEditors.SimpleButton();
            this.btnInquirePrice = new DevExpress.XtraEditors.ButtonEdit();
            this.lblNo = new DevExpress.XtraEditors.LabelControl();
            this.grbInquirePrice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkNeedConfirmation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsInquirePrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnInquirePrice.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grbInquirePrice
            // 
            this.grbInquirePrice.Controls.Add(this.chkNeedConfirmation);
            this.grbInquirePrice.Controls.Add(this.lblConfirmed);
            this.grbInquirePrice.Controls.Add(this.textEdit1);
            this.grbInquirePrice.Controls.Add(this.btnNew);
            this.grbInquirePrice.Controls.Add(this.btnInquirePrice);
            this.grbInquirePrice.Controls.Add(this.lblNo);
            this.grbInquirePrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbInquirePrice.Location = new System.Drawing.Point(0, 0);
            this.grbInquirePrice.Name = "grbInquirePrice";
            this.grbInquirePrice.Size = new System.Drawing.Size(324, 109);
            this.grbInquirePrice.TabIndex = 0;
            this.grbInquirePrice.TabStop = false;
            this.grbInquirePrice.Text = "询价";
            // 
            // chkNeedConfirmation
            // 
            this.chkNeedConfirmation.Location = new System.Drawing.Point(87, 74);
            this.chkNeedConfirmation.Name = "chkNeedConfirmation";
            this.chkNeedConfirmation.Properties.Caption = "需要商务(重新)确认";
            this.chkNeedConfirmation.Size = new System.Drawing.Size(210, 19);
            this.chkNeedConfirmation.TabIndex = 6;
            // 
            // lblConfirmed
            // 
            this.lblConfirmed.Location = new System.Drawing.Point(12, 50);
            this.lblConfirmed.Name = "lblConfirmed";
            this.lblConfirmed.Size = new System.Drawing.Size(36, 14);
            this.lblConfirmed.TabIndex = 5;
            this.lblConfirmed.Text = "确认人";
            // 
            // textEdit1
            // 
            this.textEdit1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsInquirePrice, "ConfirmedByEame", true));
            this.textEdit1.Location = new System.Drawing.Point(88, 47);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(133, 21);
            this.textEdit1.TabIndex = 4;
            // 
            // bsInquirePrice
            // 
            this.bsInquirePrice.DataSource = typeof(ICP.FCM.OceanExport.ServiceInterface.DataObjects.InquirePricePartInfo);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(235, 17);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 3;
            this.btnNew.Text = "新增";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnInquirePrice
            // 
            this.btnInquirePrice.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsInquirePrice, "InquirePriceNO", true));
            this.btnInquirePrice.Location = new System.Drawing.Point(89, 19);
            this.btnInquirePrice.Name = "btnInquirePrice";
            this.btnInquirePrice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnInquirePrice.Size = new System.Drawing.Size(132, 21);
            this.btnInquirePrice.TabIndex = 2;
            // 
            // lblNo
            // 
            this.lblNo.Location = new System.Drawing.Point(12, 22);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(36, 14);
            this.lblNo.TabIndex = 0;
            this.lblNo.Text = "询价号";
            // 
            // UCInquirePrice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grbInquirePrice);
            this.Name = "UCInquirePrice";
            this.Size = new System.Drawing.Size(324, 109);
            this.grbInquirePrice.ResumeLayout(false);
            this.grbInquirePrice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkNeedConfirmation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsInquirePrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnInquirePrice.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbInquirePrice;
        private DevExpress.XtraEditors.ButtonEdit btnInquirePrice;
        private DevExpress.XtraEditors.LabelControl lblNo;
        private DevExpress.XtraEditors.CheckEdit chkNeedConfirmation;
        private DevExpress.XtraEditors.LabelControl lblConfirmed;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.SimpleButton btnNew;
        private System.Windows.Forms.BindingSource bsInquirePrice;
    }
}
