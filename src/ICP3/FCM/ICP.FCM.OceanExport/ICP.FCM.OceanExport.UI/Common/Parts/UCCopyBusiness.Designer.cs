namespace ICP.FCM.OceanExport.UI.Common
{
    partial class UCCopyBusiness
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
            this.checkEditBooking = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditBL = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditAcc = new DevExpress.XtraEditors.CheckEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditBooking.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditBL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditAcc.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // checkEditBooking
            // 
            this.checkEditBooking.Location = new System.Drawing.Point(41, 26);
            this.checkEditBooking.Name = "checkEditBooking";
            this.checkEditBooking.Properties.Caption = "Copy Booking";
            this.checkEditBooking.Size = new System.Drawing.Size(307, 19);
            this.checkEditBooking.TabIndex = 0;
            // 
            // checkEditBL
            // 
            this.checkEditBL.Location = new System.Drawing.Point(41, 51);
            this.checkEditBL.Name = "checkEditBL";
            this.checkEditBL.Properties.Caption = "Copy BL";
            this.checkEditBL.Size = new System.Drawing.Size(307, 19);
            this.checkEditBL.TabIndex = 0;
            // 
            // checkEditAcc
            // 
            this.checkEditAcc.Location = new System.Drawing.Point(41, 76);
            this.checkEditAcc.Name = "checkEditAcc";
            this.checkEditAcc.Properties.Caption = "Copy Acc Info（Without contract Acc Info）";
            this.checkEditAcc.Size = new System.Drawing.Size(307, 19);
            this.checkEditAcc.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(86, 109);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(167, 109);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            // 
            // UCCopyBusiness
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.checkEditAcc);
            this.Controls.Add(this.checkEditBL);
            this.Controls.Add(this.checkEditBooking);
            this.Name = "UCCopyBusiness";
            this.Size = new System.Drawing.Size(345, 143);
            ((System.ComponentModel.ISupportInitialize)(this.checkEditBooking.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditBL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditAcc.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.CheckEdit checkEditBooking;
        private DevExpress.XtraEditors.CheckEdit checkEditBL;
        private DevExpress.XtraEditors.CheckEdit checkEditAcc;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}
