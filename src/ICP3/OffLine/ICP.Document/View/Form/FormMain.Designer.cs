namespace ICP.Document
{
    partial class FormMain
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.ucBusinessList = new ICP.Document.UCBusinessList();
            this.xtraTabControlMain = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.ucDocumentList1 = new ICP.Document.UCDocumentList();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlMain)).BeginInit();
            this.xtraTabControlMain.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.ucBusinessList);
            this.splitContainerControl1.Panel1.Text = "List Order";
            this.splitContainerControl1.Panel2.Controls.Add(this.xtraTabControlMain);
            this.splitContainerControl1.Panel2.MinSize = 200;
            this.splitContainerControl1.Panel2.Text = "Documents";
            this.splitContainerControl1.Size = new System.Drawing.Size(584, 563);
            this.splitContainerControl1.SplitterPosition = 357;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // ucBusinessList
            // 
            this.ucBusinessList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucBusinessList.Location = new System.Drawing.Point(0, 0);
            this.ucBusinessList.Name = "ucBusinessList";
            this.ucBusinessList.Size = new System.Drawing.Size(584, 357);
            this.ucBusinessList.TabIndex = 0;
            // 
            // xtraTabControlMain
            // 
            this.xtraTabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControlMain.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControlMain.Name = "xtraTabControlMain";
            this.xtraTabControlMain.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControlMain.Size = new System.Drawing.Size(584, 200);
            this.xtraTabControlMain.TabIndex = 0;
            this.xtraTabControlMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.ucDocumentList1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(577, 170);
            this.xtraTabPage1.Text = "Document List";
            // 
            // ucDocumentList1
            // 
            this.ucDocumentList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDocumentList1.Location = new System.Drawing.Point(0, 0);
            this.ucDocumentList1.Name = "ucDocumentList1";
            this.ucDocumentList1.Size = new System.Drawing.Size(577, 170);
            this.ucDocumentList1.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 563);
            this.Controls.Add(this.splitContainerControl1);
            this.KeyPreview = true;
            this.Name = "FormMain";
            this.Text = "ICP Document Off Line View";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlMain)).EndInit();
            this.xtraTabControlMain.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControlMain;
        private UCBusinessList ucBusinessList;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private UCDocumentList ucDocumentList1;
        
    }
}