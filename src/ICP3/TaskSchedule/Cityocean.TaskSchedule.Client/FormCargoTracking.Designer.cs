namespace Cityocean.TaskSchedule.Client
{
    partial class FormCargoTracking
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
            this.txtCResult = new Cityocean.TaskSchedule.Client.WatermakTextBox();
            this.txtCCTNRNo = new Cityocean.TaskSchedule.Client.WatermakTextBox();
            this.txtCBLNo = new Cityocean.TaskSchedule.Client.WatermakTextBox();
            this.txtCCOCarrierID = new Cityocean.TaskSchedule.Client.WatermakTextBox();
            this.btnAddCargoTrackingConfig = new System.Windows.Forms.Button();
            this.btnYunDang = new System.Windows.Forms.Button();
            this.btnYunDangCarriers = new System.Windows.Forms.Button();
            this.btnClawlerData = new System.Windows.Forms.Button();
            this.txtSResult = new Cityocean.TaskSchedule.Client.WatermakTextBox();
            this.txtSCTNRNo = new Cityocean.TaskSchedule.Client.WatermakTextBox();
            this.txtSBLNo = new Cityocean.TaskSchedule.Client.WatermakTextBox();
            this.btnCSP = new System.Windows.Forms.Button();
            this.txtSCOCarrierID = new Cityocean.TaskSchedule.Client.WatermakTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tPageConfig = new System.Windows.Forms.TabPage();
            this.tPageSearch = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.clbCarrier = new System.Windows.Forms.CheckedListBox();
            this.btnOfficialWebsite = new System.Windows.Forms.Button();
            this.tPageException = new System.Windows.Forms.TabPage();
            this.btnDeserialize = new System.Windows.Forms.Button();
            this.btnSingleCrawlData = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtSContainerID = new Cityocean.TaskSchedule.Client.WatermakTextBox();
            this.tabControl1.SuspendLayout();
            this.tPageConfig.SuspendLayout();
            this.tPageSearch.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tPageException.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCResult
            // 
            this.txtCResult.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtCResult.EmptyTextTip = "配置结果";
            this.txtCResult.Location = new System.Drawing.Point(3, 147);
            this.txtCResult.Multiline = true;
            this.txtCResult.Name = "txtCResult";
            this.txtCResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCResult.Size = new System.Drawing.Size(770, 429);
            this.txtCResult.TabIndex = 5;
            // 
            // txtCCTNRNo
            // 
            this.txtCCTNRNo.EmptyTextTip = "箱号";
            this.txtCCTNRNo.Location = new System.Drawing.Point(332, 12);
            this.txtCCTNRNo.Name = "txtCCTNRNo";
            this.txtCCTNRNo.Size = new System.Drawing.Size(150, 21);
            this.txtCCTNRNo.TabIndex = 2;
            // 
            // txtCBLNo
            // 
            this.txtCBLNo.EmptyTextTip = "提单号";
            this.txtCBLNo.Location = new System.Drawing.Point(173, 12);
            this.txtCBLNo.Name = "txtCBLNo";
            this.txtCBLNo.Size = new System.Drawing.Size(150, 21);
            this.txtCBLNo.TabIndex = 1;
            // 
            // txtCCOCarrierID
            // 
            this.txtCCOCarrierID.EmptyTextTip = "鹏城海船东ID";
            this.txtCCOCarrierID.Location = new System.Drawing.Point(14, 12);
            this.txtCCOCarrierID.Name = "txtCCOCarrierID";
            this.txtCCOCarrierID.Size = new System.Drawing.Size(150, 21);
            this.txtCCOCarrierID.TabIndex = 0;
            // 
            // btnAddCargoTrackingConfig
            // 
            this.btnAddCargoTrackingConfig.Location = new System.Drawing.Point(11, 37);
            this.btnAddCargoTrackingConfig.Name = "btnAddCargoTrackingConfig";
            this.btnAddCargoTrackingConfig.Size = new System.Drawing.Size(150, 23);
            this.btnAddCargoTrackingConfig.TabIndex = 3;
            this.btnAddCargoTrackingConfig.Text = "添加配置";
            this.btnAddCargoTrackingConfig.UseVisualStyleBackColor = true;
            this.btnAddCargoTrackingConfig.Click += new System.EventHandler(this.btnAddCargoTrackingConfig_Click);
            // 
            // btnYunDang
            // 
            this.btnYunDang.Location = new System.Drawing.Point(168, 33);
            this.btnYunDang.Name = "btnYunDang";
            this.btnYunDang.Size = new System.Drawing.Size(150, 23);
            this.btnYunDang.TabIndex = 5;
            this.btnYunDang.Text = "YunDang";
            this.btnYunDang.UseVisualStyleBackColor = true;
            this.btnYunDang.Click += new System.EventHandler(this.btnYunDang_Click);
            // 
            // btnYunDangCarriers
            // 
            this.btnYunDangCarriers.Location = new System.Drawing.Point(173, 39);
            this.btnYunDangCarriers.Name = "btnYunDangCarriers";
            this.btnYunDangCarriers.Size = new System.Drawing.Size(150, 23);
            this.btnYunDangCarriers.TabIndex = 4;
            this.btnYunDangCarriers.Text = "云当船东添加 ";
            this.btnYunDangCarriers.UseVisualStyleBackColor = true;
            this.btnYunDangCarriers.Click += new System.EventHandler(this.btnYunDangCarriers_Click);
            // 
            // btnClawlerData
            // 
            this.btnClawlerData.Location = new System.Drawing.Point(20, 20);
            this.btnClawlerData.Name = "btnClawlerData";
            this.btnClawlerData.Size = new System.Drawing.Size(150, 23);
            this.btnClawlerData.TabIndex = 4;
            this.btnClawlerData.Text = "抓取数据";
            this.btnClawlerData.UseVisualStyleBackColor = true;
            this.btnClawlerData.Click += new System.EventHandler(this.btnClawlerData_Click);
            // 
            // txtSResult
            // 
            this.txtSResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSResult.EmptyTextTip = "查询结果";
            this.txtSResult.Location = new System.Drawing.Point(3, 176);
            this.txtSResult.Multiline = true;
            this.txtSResult.Name = "txtSResult";
            this.txtSResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSResult.Size = new System.Drawing.Size(770, 400);
            this.txtSResult.TabIndex = 6;
            // 
            // txtSCTNRNo
            // 
            this.txtSCTNRNo.EmptyTextTip = "箱号";
            this.txtSCTNRNo.Location = new System.Drawing.Point(490, 6);
            this.txtSCTNRNo.Name = "txtSCTNRNo";
            this.txtSCTNRNo.Size = new System.Drawing.Size(150, 21);
            this.txtSCTNRNo.TabIndex = 2;
            // 
            // txtSBLNo
            // 
            this.txtSBLNo.EmptyTextTip = "提单号";
            this.txtSBLNo.Location = new System.Drawing.Point(329, 6);
            this.txtSBLNo.Name = "txtSBLNo";
            this.txtSBLNo.Size = new System.Drawing.Size(150, 21);
            this.txtSBLNo.TabIndex = 1;
            // 
            // btnCSP
            // 
            this.btnCSP.Location = new System.Drawing.Point(329, 33);
            this.btnCSP.Name = "btnCSP";
            this.btnCSP.Size = new System.Drawing.Size(150, 23);
            this.btnCSP.TabIndex = 3;
            this.btnCSP.Text = "CSP";
            this.btnCSP.UseVisualStyleBackColor = true;
            this.btnCSP.Click += new System.EventHandler(this.btnCSP_Click);
            // 
            // txtSCOCarrierID
            // 
            this.txtSCOCarrierID.EmptyTextTip = "鹏城海船东ID";
            this.txtSCOCarrierID.Location = new System.Drawing.Point(168, 6);
            this.txtSCOCarrierID.Name = "txtSCOCarrierID";
            this.txtSCOCarrierID.Size = new System.Drawing.Size(150, 21);
            this.txtSCOCarrierID.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tPageConfig);
            this.tabControl1.Controls.Add(this.tPageSearch);
            this.tabControl1.Controls.Add(this.tPageException);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(784, 605);
            this.tabControl1.TabIndex = 0;
            // 
            // tPageConfig
            // 
            this.tPageConfig.Controls.Add(this.txtCResult);
            this.tPageConfig.Controls.Add(this.btnAddCargoTrackingConfig);
            this.tPageConfig.Controls.Add(this.txtCCTNRNo);
            this.tPageConfig.Controls.Add(this.btnYunDangCarriers);
            this.tPageConfig.Controls.Add(this.txtCBLNo);
            this.tPageConfig.Controls.Add(this.txtCCOCarrierID);
            this.tPageConfig.Location = new System.Drawing.Point(4, 22);
            this.tPageConfig.Name = "tPageConfig";
            this.tPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tPageConfig.Size = new System.Drawing.Size(776, 579);
            this.tPageConfig.TabIndex = 0;
            this.tPageConfig.Text = "配置";
            this.tPageConfig.UseVisualStyleBackColor = true;
            // 
            // tPageSearch
            // 
            this.tPageSearch.Controls.Add(this.txtSResult);
            this.tPageSearch.Controls.Add(this.panel1);
            this.tPageSearch.Location = new System.Drawing.Point(4, 22);
            this.tPageSearch.Name = "tPageSearch";
            this.tPageSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tPageSearch.Size = new System.Drawing.Size(776, 579);
            this.tPageSearch.TabIndex = 1;
            this.tPageSearch.Text = "数据查询";
            this.tPageSearch.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.clbCarrier);
            this.panel1.Controls.Add(this.btnOfficialWebsite);
            this.panel1.Controls.Add(this.txtSCOCarrierID);
            this.panel1.Controls.Add(this.btnYunDang);
            this.panel1.Controls.Add(this.btnCSP);
            this.panel1.Controls.Add(this.txtSBLNo);
            this.panel1.Controls.Add(this.txtSCTNRNo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(770, 173);
            this.panel1.TabIndex = 8;
            // 
            // clbCarrier
            // 
            this.clbCarrier.FormattingEnabled = true;
            this.clbCarrier.Location = new System.Drawing.Point(3, 3);
            this.clbCarrier.Name = "clbCarrier";
            this.clbCarrier.ScrollAlwaysVisible = true;
            this.clbCarrier.Size = new System.Drawing.Size(150, 164);
            this.clbCarrier.TabIndex = 7;
            // 
            // btnOfficialWebsite
            // 
            this.btnOfficialWebsite.Location = new System.Drawing.Point(490, 33);
            this.btnOfficialWebsite.Name = "btnOfficialWebsite";
            this.btnOfficialWebsite.Size = new System.Drawing.Size(150, 23);
            this.btnOfficialWebsite.TabIndex = 5;
            this.btnOfficialWebsite.Text = "Official website";
            this.btnOfficialWebsite.UseVisualStyleBackColor = true;
            this.btnOfficialWebsite.Click += new System.EventHandler(this.btnOfficialWebsite_Click);
            // 
            // tPageException
            // 
            this.tPageException.Controls.Add(this.groupBox3);
            this.tPageException.Controls.Add(this.groupBox2);
            this.tPageException.Controls.Add(this.groupBox1);
            this.tPageException.Location = new System.Drawing.Point(4, 22);
            this.tPageException.Name = "tPageException";
            this.tPageException.Size = new System.Drawing.Size(776, 579);
            this.tPageException.TabIndex = 2;
            this.tPageException.Text = "异常 / 调试";
            this.tPageException.UseVisualStyleBackColor = true;
            // 
            // btnDeserialize
            // 
            this.btnDeserialize.Location = new System.Drawing.Point(20, 20);
            this.btnDeserialize.Name = "btnDeserialize";
            this.btnDeserialize.Size = new System.Drawing.Size(150, 23);
            this.btnDeserialize.TabIndex = 4;
            this.btnDeserialize.Text = "解密数据";
            this.btnDeserialize.UseVisualStyleBackColor = true;
            this.btnDeserialize.Click += new System.EventHandler(this.btnDeserialize_Click);
            // 
            // btnSingleCrawlData
            // 
            this.btnSingleCrawlData.Location = new System.Drawing.Point(308, 31);
            this.btnSingleCrawlData.Name = "btnSingleCrawlData";
            this.btnSingleCrawlData.Size = new System.Drawing.Size(150, 23);
            this.btnSingleCrawlData.TabIndex = 4;
            this.btnSingleCrawlData.Text = "单个抓取数据";
            this.btnSingleCrawlData.UseVisualStyleBackColor = true;
            this.btnSingleCrawlData.Click += new System.EventHandler(this.btnSingleCrawlData_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClawlerData);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 56);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "抓取数据";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDeserialize);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 56);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(776, 49);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "解密数据";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtSContainerID);
            this.groupBox3.Controls.Add(this.btnSingleCrawlData);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 105);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(776, 73);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "单个抓取数据";
            // 
            // txtSContainerID
            // 
            this.txtSContainerID.EmptyTextTip = "箱ID";
            this.txtSContainerID.Location = new System.Drawing.Point(20, 33);
            this.txtSContainerID.Name = "txtSContainerID";
            this.txtSContainerID.Size = new System.Drawing.Size(282, 21);
            this.txtSContainerID.TabIndex = 5;
            // 
            // FormCargoTracking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 605);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormCargoTracking";
            this.ShowIcon = false;
            this.Text = "货物动态";
            this.Load += new System.EventHandler(this.FormCargoTracking_Load);
            this.tabControl1.ResumeLayout(false);
            this.tPageConfig.ResumeLayout(false);
            this.tPageConfig.PerformLayout();
            this.tPageSearch.ResumeLayout(false);
            this.tPageSearch.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tPageException.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnYunDangCarriers;
        private WatermakTextBox txtCCTNRNo;
        private WatermakTextBox txtCBLNo;
        private WatermakTextBox txtCCOCarrierID;
        private System.Windows.Forms.Button btnAddCargoTrackingConfig;
        private WatermakTextBox txtCResult;
        private System.Windows.Forms.Button btnClawlerData;
        private WatermakTextBox txtSResult;
        private WatermakTextBox txtSCTNRNo;
        private WatermakTextBox txtSBLNo;
        private WatermakTextBox txtSCOCarrierID;
        private System.Windows.Forms.Button btnCSP;
        private System.Windows.Forms.Button btnYunDang;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tPageConfig;
        private System.Windows.Forms.TabPage tPageSearch;
        private System.Windows.Forms.TabPage tPageException;
        private System.Windows.Forms.CheckedListBox clbCarrier;
        private System.Windows.Forms.Button btnOfficialWebsite;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDeserialize;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSingleCrawlData;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private WatermakTextBox txtSContainerID;
    }
}