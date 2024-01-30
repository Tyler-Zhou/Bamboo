namespace ICP.FCM.AirExport.UI.BL
{
    partial class NewBLPrintPart
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewBLPrintPart));
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupStyle = new System.Windows.Forms.GroupBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.imageComboBoxEdit1 = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labStyle = new DevExpress.XtraEditors.LabelControl();
            this.cmbReportStyle = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbCompany = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labTitle = new DevExpress.XtraEditors.LabelControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pnlQuery.SuspendLayout();
            this.pnlCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
            this.splitContainerControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupStyle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageComboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReportStyle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlQuery
            // 
            this.pnlQuery.Location = new System.Drawing.Point(0, 428);
            this.pnlQuery.Size = new System.Drawing.Size(209, 43);
            // 
            // btnQuery
            // 
            this.btnQuery.TabIndex = 0;
            // 
            // pnlCondition
            // 
            this.pnlCondition.Controls.Add(this.pictureBox1);
            this.pnlCondition.Controls.Add(this.groupStyle);
            this.pnlCondition.Size = new System.Drawing.Size(209, 471);
            // 
            // splitContainerControl
            // 
            this.splitContainerControl.Size = new System.Drawing.Size(791, 471);
            this.splitContainerControl.SplitterPosition = 209;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "1.gif");
            this.imageCollection1.Images.SetKeyName(1, "2.gif");
            this.imageCollection1.Images.SetKeyName(2, "3.gif");
            this.imageCollection1.Images.SetKeyName(3, "4.gif");
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(8, 159);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(135, 110);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // groupStyle
            // 
            this.groupStyle.Controls.Add(this.labelControl1);
            this.groupStyle.Controls.Add(this.imageComboBoxEdit1);
            this.groupStyle.Controls.Add(this.labStyle);
            this.groupStyle.Controls.Add(this.cmbReportStyle);
            this.groupStyle.Controls.Add(this.cmbCompany);
            this.groupStyle.Controls.Add(this.labTitle);
            this.groupStyle.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupStyle.Location = new System.Drawing.Point(0, 0);
            this.groupStyle.Name = "groupStyle";
            this.groupStyle.Size = new System.Drawing.Size(209, 152);
            this.groupStyle.TabIndex = 4;
            this.groupStyle.TabStop = false;
            this.groupStyle.Text = "Style";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(6, 70);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(27, 14);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "Logo";
            // 
            // imageComboBoxEdit1
            // 
            this.imageComboBoxEdit1.Location = new System.Drawing.Point(5, 90);
            this.imageComboBoxEdit1.Name = "imageComboBoxEdit1";
            this.imageComboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.imageComboBoxEdit1.Size = new System.Drawing.Size(183, 21);
            this.imageComboBoxEdit1.TabIndex = 2;
            this.imageComboBoxEdit1.SelectedIndexChanged += new System.EventHandler(this.imageComboBoxEdit1_SelectedIndexChanged);
            // 
            // labStyle
            // 
            this.labStyle.Location = new System.Drawing.Point(6, 24);
            this.labStyle.Name = "labStyle";
            this.labStyle.Size = new System.Drawing.Size(27, 14);
            this.labStyle.TabIndex = 0;
            this.labStyle.Text = "Style";
            // 
            // cmbReportStyle
            // 
            this.cmbReportStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbReportStyle.Location = new System.Drawing.Point(65, 21);
            this.cmbReportStyle.Name = "cmbReportStyle";
            this.cmbReportStyle.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbReportStyle.Properties.Appearance.Options.UseBackColor = true;
            this.cmbReportStyle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbReportStyle.Size = new System.Drawing.Size(136, 21);
            this.cmbReportStyle.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbReportStyle.TabIndex = 0;
            this.cmbReportStyle.SelectedIndexChanged += new System.EventHandler(this.cmbReportStyle_SelectedIndexChanged);
            // 
            // cmbCompany
            // 
            this.cmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCompany.Location = new System.Drawing.Point(65, 48);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCompany.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCompany.Size = new System.Drawing.Size(136, 21);
            this.cmbCompany.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCompany.TabIndex = 1;
            // 
            // labTitle
            // 
            this.labTitle.Location = new System.Drawing.Point(6, 51);
            this.labTitle.Name = "labTitle";
            this.labTitle.Size = new System.Drawing.Size(24, 14);
            this.labTitle.TabIndex = 0;
            this.labTitle.Text = "Title";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "1.gif");
            this.imageList1.Images.SetKeyName(1, "2.gif");
            this.imageList1.Images.SetKeyName(2, "3.gif");
            this.imageList1.Images.SetKeyName(3, "4.gif");
            // 
            // NewBLPrintPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "NewBLPrintPart";
            this.pnlQuery.ResumeLayout(false);
            this.pnlCondition.ResumeLayout(false);
            this.pnlCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
            this.splitContainerControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupStyle.ResumeLayout(false);
            this.groupStyle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageComboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReportStyle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupStyle;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ImageComboBoxEdit imageComboBoxEdit1;
        private DevExpress.XtraEditors.LabelControl labStyle;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbReportStyle;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCompany;
        private DevExpress.XtraEditors.LabelControl labTitle;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private System.Windows.Forms.ImageList imageList1;



    }
}
