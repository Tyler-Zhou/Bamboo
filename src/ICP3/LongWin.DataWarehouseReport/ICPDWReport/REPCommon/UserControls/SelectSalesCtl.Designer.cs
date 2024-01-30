namespace LongWin.DataWarehouseReport.WinUI
{
    partial class SelectSalesCtl
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
            this.SalesSelect = new global::LongWin.Framework.ClientComponents.MultipleSelect(); 
            this.SuspendLayout();
            // 
            // SalesSelect
            // 
            this.SalesSelect.DataSource = null;
            this.SalesSelect.DisplayMember = null;
            this.SalesSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SalesSelect.Location = new System.Drawing.Point(0, 0);
            this.SalesSelect.Name = "SalesSelect";
            this.SalesSelect.SearchColumns = null;
            this.SalesSelect.Size = new System.Drawing.Size(153, 21);
            this.SalesSelect.TabIndex = 0;
            this.SalesSelect.Value = "";
            this.SalesSelect.ValueMember = null;
            // 
            // SelectSalesCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SalesSelect);
            this.Name = "SelectSalesCtl";
            this.Size = new System.Drawing.Size(153, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private global::LongWin.Framework.ClientComponents.MultipleSelect SalesSelect;
    }
}
