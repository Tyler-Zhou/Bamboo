namespace ICP.Common.UI.ReportView
{
    partial class CompositeReportViewPart
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
        /// 
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompositeReportViewPart));
            this.pnlQuery = new System.Windows.Forms.Panel();
            this.pnlCondition = new System.Windows.Forms.Panel();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
            this.splitContainerControl.SuspendLayout();
            this.pnlQuery.SuspendLayout();
            this.pnlCondition.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQuery.Location = new System.Drawing.Point(49, 9);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 1;
            this.btnQuery.Text = "&Query";
            this.btnQuery.Click += new System.EventHandler(btnQuery_Click);

            // 
            // pnlCondition
            // 
            this.pnlCondition.AutoScroll = true;
            this.pnlCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCondition.Location = new System.Drawing.Point(0, 0);
            this.pnlCondition.Name = "pnlCondition";
            this.pnlCondition.Size = new System.Drawing.Size(197, 449);
            this.pnlCondition.TabIndex = 0;

            // 
            // pnlQuery
            // 
            this.pnlQuery.AutoScroll = true;
            this.pnlQuery.Controls.Add(this.btnQuery);
            this.pnlQuery.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlQuery.Location = new System.Drawing.Point(0, 0);
            this.pnlQuery.Name = "pnlQuery";
            this.pnlQuery.Size = new System.Drawing.Size(197, 30);
            this.pnlQuery.TabIndex = 0;
            
            // 
            // splitContainerControl
            // 
            this.splitContainerControl.Panel1.Controls.Add(pnlQuery);
            this.splitContainerControl.Panel1.Controls.Add(pnlCondition);
            this.splitContainerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl.Name = "splitContainerControl";
            this.splitContainerControl.Panel1.Text = "Panel1";
  
            this.splitContainerControl.Panel2.Text = "Panel2";
            this.splitContainerControl.Size = new System.Drawing.Size(802, 496);
            this.splitContainerControl.SplitterPosition = 210;
            this.splitContainerControl.TabIndex = 1;
            this.splitContainerControl.Text = "splitContainerControl";
           

            // 
            // CompositeReportViewPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl);
            this.Name = "CompositeReportViewPart";
            this.Size = new System.Drawing.Size(791, 471);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
            this.pnlQuery.ResumeLayout(false);
            this.pnlCondition.ResumeLayout(false);
            this.splitContainerControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        protected System.Windows.Forms.Panel pnlQuery;
        protected DevExpress.XtraEditors.SimpleButton btnQuery;
        protected System.Windows.Forms.Panel pnlCondition;
        protected DevExpress.XtraEditors.SplitContainerControl splitContainerControl;
      
    }
}
