namespace ICP.FRM.UI.InquireRates
{
    partial class InquireAirRatesDiscussingPart
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
            this.discussingPart = new ICP.FRM.UI.InquireRates.DiscussingPart();
            this.SuspendLayout();
            // 
            // discussingPart
            // 
            this.discussingPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.discussingPart.Location = new System.Drawing.Point(0, 0);
            this.discussingPart.Name = "discussingPart";
            this.discussingPart.Size = new System.Drawing.Size(393, 269);
            this.discussingPart.TabIndex = 0;
            // 
            // InquireOceanRatesDiscussingPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.discussingPart);
            this.Name = "InquireOceanRatesDiscussingPart";
            this.Size = new System.Drawing.Size(393, 269);
            this.ResumeLayout(false);

        }

        private DiscussingPart discussingPart;

        #endregion
    }
}