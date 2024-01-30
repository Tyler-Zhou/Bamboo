namespace ICP.FRM.UI.InquireRates
{
    partial class InquireOceanRatesEmailPart
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EmailPart = new ICP.FRM.UI.InquireRates.InquireRatesEmailPart();
            this.SuspendLayout();
            // 
            // EmailPart
            // 
            this.EmailPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EmailPart.Location = new System.Drawing.Point(0, 0);
            this.EmailPart.Name = "EmailPart";
            this.EmailPart.Presenter = null;
            this.EmailPart.Size = new System.Drawing.Size(603, 441);
            this.EmailPart.TabIndex = 0;
            // 
            // InquireOceanRatesEmailPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.EmailPart);
            this.Name = "InquireOceanRatesEmailPart";
            this.Size = new System.Drawing.Size(603, 441);
            this.ResumeLayout(false);

        }

        #endregion

        private InquireRatesEmailPart EmailPart;
    }
}
