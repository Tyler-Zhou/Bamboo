﻿namespace ICP.FAM.UI.GLCode
{
    partial class GLCompanyEdit
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
            this.gbxCompany = new DevExpress.XtraEditors.GroupControl();
            this.listCompany = new System.Windows.Forms.CheckedListBox();
            this.pnlButtom = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnClare = new DevExpress.XtraEditors.SimpleButton();
            this.btnAll = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gbxCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButtom)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxCompany
            // 
            this.gbxCompany.Controls.Add(this.listCompany);
            this.gbxCompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxCompany.Location = new System.Drawing.Point(0, 0);
            this.gbxCompany.Name = "gbxCompany";
            this.gbxCompany.Size = new System.Drawing.Size(300, 367);
            this.gbxCompany.TabIndex = 2;
            this.gbxCompany.Text = "公司列表";
            // 
            // listCompany
            // 
            this.listCompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listCompany.FormattingEnabled = true;
            this.listCompany.Location = new System.Drawing.Point(2, 23);
            this.listCompany.Name = "listCompany";
            this.listCompany.Size = new System.Drawing.Size(296, 342);
            this.listCompany.TabIndex = 0;
            // 
            // pnlButtom
            // 
            this.pnlButtom.Controls.Add(this.btnClose);
            this.pnlButtom.Controls.Add(this.btnOK);
            this.pnlButtom.Controls.Add(this.btnClare);
            this.pnlButtom.Controls.Add(this.btnAll);
            this.pnlButtom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtom.Location = new System.Drawing.Point(0, 367);
            this.pnlButtom.Name = "pnlButtom";
            this.pnlButtom.Size = new System.Drawing.Size(300, 42);
            this.pnlButtom.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(226, 7);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 27);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(152, 7);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(70, 27);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClare
            // 
            this.btnClare.Location = new System.Drawing.Point(77, 7);
            this.btnClare.Name = "btnClare";
            this.btnClare.Size = new System.Drawing.Size(70, 27);
            this.btnClare.TabIndex = 0;
            this.btnClare.Text = "清空(&L)";
            this.btnClare.Click += new System.EventHandler(this.btnClare_Click);
            // 
            // btnAll
            // 
            this.btnAll.Location = new System.Drawing.Point(2, 7);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(70, 27);
            this.btnAll.TabIndex = 0;
            this.btnAll.Text = "全选(&A)";
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // GLCompanyEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxCompany);
            this.Controls.Add(this.pnlButtom);
            this.Name = "GLCompanyEdit";
            this.Size = new System.Drawing.Size(300, 409);
            ((System.ComponentModel.ISupportInitialize)(this.gbxCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButtom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gbxCompany;
        private System.Windows.Forms.CheckedListBox listCompany;
        private DevExpress.XtraEditors.PanelControl pnlButtom;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnClare;
        private DevExpress.XtraEditors.SimpleButton btnAll;
    }
}