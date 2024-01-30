namespace ICP.MailCenter.CommonUI
{
    partial class UCThumbImageListView
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
                this.CurrentIndexChanged=null;
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
            this.components = new System.ComponentModel.Container();
            this.listViewThumbView = new System.Windows.Forms.ListView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.SuspendLayout();
            // 
            // listViewThumbView
            // 
            this.listViewThumbView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewThumbView.Location = new System.Drawing.Point(0, 0);
            this.listViewThumbView.Name = "listViewThumbView";
            this.listViewThumbView.Size = new System.Drawing.Size(260, 572);
            this.listViewThumbView.TabIndex = 0;
            this.listViewThumbView.UseCompatibleStateImageBehavior = false;
            this.listViewThumbView.SelectedIndexChanged += new System.EventHandler(this.listViewThumbView_SelectedIndexChanged);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // toolStrip
            // 
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(260, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // UCThumbImageListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.listViewThumbView);
            this.Name = "UCThumbImageListView";
            this.Size = new System.Drawing.Size(260, 572);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewThumbView;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStrip toolStrip;
    }
}
