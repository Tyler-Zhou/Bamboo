
namespace ICP.TaskCenter.UI.MainWork
{
    partial class MainWorkSpace
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
       
        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
       
            if (disposing && (components != null))
            {
               
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.ViewListWorkSpace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.ViewListWorkSpace);
            this.splitContainerControl1.Panel1.MinSize = 150;
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.MinSize = 700;
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(980, 645);
            this.splitContainerControl1.SplitterPosition = 211;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            this.splitContainerControl1.SplitterPositionChanged += new System.EventHandler(this.splitContainerControl1_SplitterPositionChanged);
            // 
            // ViewListWorkSpace
            // 
            this.ViewListWorkSpace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.ViewListWorkSpace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ViewListWorkSpace.Location = new System.Drawing.Point(0, 0);
            this.ViewListWorkSpace.Name = "ViewListWorkSpace";
            this.ViewListWorkSpace.Size = new System.Drawing.Size(211, 645);
            this.ViewListWorkSpace.TabIndex = 6;
            this.ViewListWorkSpace.Text = "deckWorkspace2";
            // 
            // MainWorkSpace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "MainWorkSpace";
            this.Size = new System.Drawing.Size(980, 645);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        internal Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ViewListWorkSpace;
    }
}
