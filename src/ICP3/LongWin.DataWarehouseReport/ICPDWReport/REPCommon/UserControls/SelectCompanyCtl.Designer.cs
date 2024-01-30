namespace LongWin.DataWarehouseReport.WinUI
{
    partial class SelectCompanyCtl
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
            this.txtTreeTextBox = new global::LongWin.Framework.ClientComponents.TreeTextBox();
            this.SuspendLayout();
            // 
            // txtTreeTextBox
            // 
            this.txtTreeTextBox.Description = "";
            this.txtTreeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTreeTextBox.Location = new System.Drawing.Point(0, 0);
            this.txtTreeTextBox.Multiple = false;
            this.txtTreeTextBox.Name = "txtTreeTextBox";
            this.txtTreeTextBox.Size = new System.Drawing.Size(180, 21);
            this.txtTreeTextBox.TabIndex = 0;
            this.txtTreeTextBox.TreeDataSource = null;
            this.txtTreeTextBox.TreeDisplayMember = null;
            this.txtTreeTextBox.TreeInitParentKey = null;
            this.txtTreeTextBox.TreeParentMember = null;
            this.txtTreeTextBox.TreeValueMember = null;
            this.txtTreeTextBox.Value = null;
            
            // 
            // SelectCompanyCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtTreeTextBox);
            this.Name = "SelectCompanyCtl";
            this.Size = new System.Drawing.Size(180, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private global::LongWin.Framework.ClientComponents.TreeTextBox txtTreeTextBox;
    }
}
