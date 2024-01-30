namespace ICP.Business.Common.UI.Communication
{
    partial class CommunicationHistoryStage
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
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.labEndingDate = new DevExpress.XtraEditors.LabelControl();
            this.chklStage = new DevExpress.XtraEditors.CheckedListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.chklStage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(104, 205);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(17, 205);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(70, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // labEndingDate
            // 
            this.labEndingDate.Location = new System.Drawing.Point(17, 13);
            this.labEndingDate.Name = "labEndingDate";
            this.labEndingDate.Size = new System.Drawing.Size(32, 14);
            this.labEndingDate.TabIndex = 5;
            this.labEndingDate.Text = "Stage";
            // 
            // chklStage
            // 
            this.chklStage.Location = new System.Drawing.Point(17, 30);
            this.chklStage.Name = "chklStage";
            this.chklStage.Size = new System.Drawing.Size(157, 166);
            this.chklStage.TabIndex = 9;
            // 
            // CommunicationHistoryStage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chklStage);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.labEndingDate);
            this.Name = "CommunicationHistoryStage";
            this.Size = new System.Drawing.Size(192, 239);
            ((System.ComponentModel.ISupportInitialize)(this.chklStage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.LabelControl labEndingDate;
        private DevExpress.XtraEditors.CheckedListBoxControl chklStage;
    }
}
