namespace ICP.Business.Common.UI.Communication_History
{
    partial class AmsConfirm
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
            this.webAms = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webAms
            // 
            this.webAms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webAms.Location = new System.Drawing.Point(0, 0);
            this.webAms.MinimumSize = new System.Drawing.Size(20, 20);
            this.webAms.Name = "webAms";
            this.webAms.Size = new System.Drawing.Size(1344, 730);
            this.webAms.TabIndex = 0;
            this.webAms.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webAms_DocumentCompleted);
            // 
            // AmsConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 730);
            this.Controls.Add(this.webAms);
            this.Name = "AmsConfirm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AmsConfirm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webAms;
    }
}