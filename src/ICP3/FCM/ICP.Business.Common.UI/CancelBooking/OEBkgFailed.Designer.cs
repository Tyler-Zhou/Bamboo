namespace ICP.Business.Common.UI
{
    partial class OEBkgFailed
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkSales = new DevExpress.XtraEditors.CheckEdit();
            this.checkinternal = new DevExpress.XtraEditors.CheckEdit();
            this.memoDescription = new DevExpress.XtraEditors.MemoEdit();
            this.simpleOk = new DevExpress.XtraEditors.SimpleButton();
            this.simpleClose = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkSales.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkinternal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkSales);
            this.groupBox1.Controls.Add(this.checkinternal);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(589, 74);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Recipient";
            // 
            // checkSales
            // 
            this.checkSales.EditValue = true;
            this.checkSales.Location = new System.Drawing.Point(6, 49);
            this.checkSales.Name = "checkSales";
            this.checkSales.Properties.Caption = "";
            this.checkSales.Size = new System.Drawing.Size(577, 19);
            this.checkSales.TabIndex = 3;
            // 
            // checkinternal
            // 
            this.checkinternal.EditValue = true;
            this.checkinternal.Location = new System.Drawing.Point(6, 21);
            this.checkinternal.Name = "checkinternal";
            this.checkinternal.Properties.Caption = "";
            this.checkinternal.Size = new System.Drawing.Size(577, 19);
            this.checkinternal.TabIndex = 2;
            // 
            // memoDescription
            // 
            this.memoDescription.Location = new System.Drawing.Point(6, 21);
            this.memoDescription.Name = "memoDescription";
            this.memoDescription.Size = new System.Drawing.Size(577, 191);
            this.memoDescription.TabIndex = 3;
            // 
            // simpleOk
            // 
            this.simpleOk.Location = new System.Drawing.Point(383, 237);
            this.simpleOk.Name = "simpleOk";
            this.simpleOk.Size = new System.Drawing.Size(87, 27);
            this.simpleOk.TabIndex = 4;
            this.simpleOk.Text = "Send";
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.memoDescription);
            this.groupBox2.Controls.Add(this.simpleOk);
            this.groupBox2.Controls.Add(this.simpleClose);
            this.groupBox2.Location = new System.Drawing.Point(12, 92);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(589, 270);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Description";
            // 
            // OEBkgFailed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 390);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "OEBkgFailed";
            this.Text = "OEBkgFailed";
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkSales.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkinternal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.CheckEdit checkSales;
        private DevExpress.XtraEditors.CheckEdit checkinternal;
        private DevExpress.XtraEditors.MemoEdit memoDescription;
        private DevExpress.XtraEditors.SimpleButton simpleOk;
        private DevExpress.XtraEditors.SimpleButton simpleClose;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}