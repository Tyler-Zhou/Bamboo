namespace ICP.ReportCenter.UI.Comm.Controls
{
    partial class SelectUserListPart
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
            this.components = new System.ComponentModel.Container();
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.listBoxLeft = new DevExpress.XtraEditors.ListBoxControl();
            this.listBoxRight = new DevExpress.XtraEditors.ListBoxControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.dsList1 = new System.Windows.Forms.BindingSource(this.components);
            this.dsList2 = new System.Windows.Forms.BindingSource(this.components);
            this.labSearch = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsList2)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.labSearch);
            this.pnlTop.Controls.Add(this.txtCode);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(411, 42);
            this.pnlTop.TabIndex = 0;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(57, 11);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(343, 21);
            this.txtCode.TabIndex = 0;
            this.txtCode.EditValueChanged += new System.EventHandler(this.txtCode_EditValueChanged);
            // 
            // listBoxLeft
            // 
            this.listBoxLeft.DataSource = this.dsList1;
            this.listBoxLeft.Location = new System.Drawing.Point(7, 48);
            this.listBoxLeft.Name = "listBoxLeft";
            this.listBoxLeft.Size = new System.Drawing.Size(170, 363);
            this.listBoxLeft.TabIndex = 1;
            this.listBoxLeft.DoubleClick += new System.EventHandler(this.listBoxLeft_DoubleClick);
            // 
            // listBoxRight
            // 
            this.listBoxRight.DataSource = this.dsList2;
            this.listBoxRight.Location = new System.Drawing.Point(230, 48);
            this.listBoxRight.Name = "listBoxRight";
            this.listBoxRight.Size = new System.Drawing.Size(170, 363);
            this.listBoxRight.TabIndex = 1;
            this.listBoxRight.DoubleClick += new System.EventHandler(this.listBoxRight_DoubleClick);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(325, 421);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(230, 421);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Cancel";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(183, 90);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(41, 24);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = ">";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(183, 123);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(41, 24);
            this.simpleButton2.TabIndex = 1;
            this.simpleButton2.Text = ">>";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(183, 251);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(41, 24);
            this.simpleButton3.TabIndex = 1;
            this.simpleButton3.Text = "<";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton4
            // 
            this.simpleButton4.Location = new System.Drawing.Point(183, 219);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(41, 24);
            this.simpleButton4.TabIndex = 1;
            this.simpleButton4.Text = "<<";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // labSearch
            // 
            this.labSearch.Location = new System.Drawing.Point(12, 14);
            this.labSearch.Name = "labSearch";
            this.labSearch.Size = new System.Drawing.Size(37, 14);
            this.labSearch.TabIndex = 1;
            this.labSearch.Text = "Search";
            // 
            // SelectUserListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listBoxRight);
            this.Controls.Add(this.simpleButton4);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.listBoxLeft);
            this.Controls.Add(this.pnlTop);
            this.Name = "SelectUserListPart";
            this.Size = new System.Drawing.Size(411, 457);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsList2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlTop;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.ListBoxControl listBoxLeft;
        private DevExpress.XtraEditors.ListBoxControl listBoxRight;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private System.Windows.Forms.BindingSource dsList1;
        private System.Windows.Forms.BindingSource dsList2;
        private DevExpress.XtraEditors.LabelControl labSearch;
    }
}
