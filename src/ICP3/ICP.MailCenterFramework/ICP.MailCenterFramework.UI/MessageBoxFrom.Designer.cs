namespace ICP.MailCenterFramework.UI
{
    partial class MessageBoxFrom
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageBoxFrom));
            this.labText = new System.Windows.Forms.Label();
            this.butok = new System.Windows.Forms.Button();
            this.refreshTime = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // labText
            // 
            this.labText.AutoEllipsis = true;
            this.labText.AutoSize = true;
            this.labText.Location = new System.Drawing.Point(12, 21);
            this.labText.Name = "labText";
            this.labText.Size = new System.Drawing.Size(0, 15);
            this.labText.TabIndex = 0;
            this.labText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labText.UseCompatibleTextRendering = true;
            // 
            // butok
            // 
            this.butok.Location = new System.Drawing.Point(85, 64);
            this.butok.Name = "butok";
            this.butok.Size = new System.Drawing.Size(75, 23);
            this.butok.TabIndex = 1;
            this.butok.Text = "确定";
            this.butok.UseVisualStyleBackColor = true;
            this.butok.Click += new System.EventHandler(this.butok_Click);
            // 
            // refreshTime
            // 
            this.refreshTime.Enabled = true;
            this.refreshTime.Interval = 1000;
            this.refreshTime.Tick += new System.EventHandler(this.refreshTime_Tick);
            // 
            // MessageBoxFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 90);
            this.Controls.Add(this.butok);
            this.Controls.Add(this.labText);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageBoxFrom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tip";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MessageBoxFrom_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labText;
        private System.Windows.Forms.Button butok;
        private System.Windows.Forms.Timer refreshTime;
    }
}