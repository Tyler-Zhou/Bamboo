namespace IC.FRM.Prototype
{
    partial class InquireOpen
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
            this.discussingAndResult1 = new IC.FRM.Prototype.DiscussingAndResult();
            this.inquireInfo1 = new IC.FRM.Prototype.InquireInfo();
            this.SuspendLayout();
            // 
            // discussingAndResult1
            // 
            this.discussingAndResult1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.discussingAndResult1.Location = new System.Drawing.Point(0, 347);
            this.discussingAndResult1.Mode = 1;
            this.discussingAndResult1.Name = "discussingAndResult1";
            this.discussingAndResult1.Size = new System.Drawing.Size(803, 218);
            this.discussingAndResult1.TabIndex = 0;
            // 
            // inquireInfo1
            // 
            this.inquireInfo1.AutoScroll = true;
            this.inquireInfo1.Dock = System.Windows.Forms.DockStyle.Top;
            this.inquireInfo1.Location = new System.Drawing.Point(0, 0);
            this.inquireInfo1.Name = "inquireInfo1";
            this.inquireInfo1.Size = new System.Drawing.Size(803, 347);
            this.inquireInfo1.TabIndex = 1;
            // 
            // InquireOpen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 565);
            this.Controls.Add(this.discussingAndResult1);
            this.Controls.Add(this.inquireInfo1);
            this.Name = "InquireOpen";
            this.Text = "InquireOpen";
            this.ResumeLayout(false);

        }

        #endregion

        private DiscussingAndResult discussingAndResult1;
        private InquireInfo inquireInfo1;
    }
}