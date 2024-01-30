namespace Cityocean.TaskSchedule.Client
{
    partial class FormSailingSchedule
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
            this.txtCDestinationCode = new Cityocean.TaskSchedule.Client.WatermakTextBox();
            this.txtCOriginCode = new Cityocean.TaskSchedule.Client.WatermakTextBox();
            this.btnAddConfig = new System.Windows.Forms.Button();
            this.btnYunDangCarriers = new System.Windows.Forms.Button();
            this.btnSearchInttra = new System.Windows.Forms.Button();
            this.clbCarrier = new System.Windows.Forms.CheckedListBox();
            this.txtSResult = new Cityocean.TaskSchedule.Client.WatermakTextBox();
            this.txtSStartDate = new Cityocean.TaskSchedule.Client.WatermakTextBox();
            this.txtSOriginDesc = new Cityocean.TaskSchedule.Client.WatermakTextBox();
            this.txtSDestinationDesc = new Cityocean.TaskSchedule.Client.WatermakTextBox();
            this.txtSDestinationCode = new Cityocean.TaskSchedule.Client.WatermakTextBox();
            this.btnSearchCSP = new System.Windows.Forms.Button();
            this.txtSOriginCode = new Cityocean.TaskSchedule.Client.WatermakTextBox();
            this.cbOnlyAbnormalData = new System.Windows.Forms.CheckBox();
            this.txtSStartDate2 = new Cityocean.TaskSchedule.Client.WatermakTextBox();
            this.btnCrawlData = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tPageConfig = new System.Windows.Forms.TabPage();
            this.tPageSearch = new System.Windows.Forms.TabPage();
            this.tPageException = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tPageConfig.SuspendLayout();
            this.tPageSearch.SuspendLayout();
            this.tPageException.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCResult
            // 
            this.txtCResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCResult.EmptyTextTip = "船期配置结果";
            this.txtCResult.Location = new System.Drawing.Point(14, 71);
            this.txtCResult.Multiline = true;
            this.txtCResult.Name = "txtCResult";
            this.txtCResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCResult.Size = new System.Drawing.Size(752, 477);
            this.txtCResult.TabIndex = 4;
            // 
            // txtCDestinationCode
            // 
            this.txtCDestinationCode.EmptyTextTip = "目的地港口编码";
            this.txtCDestinationCode.Location = new System.Drawing.Point(171, 15);
            this.txtCDestinationCode.Name = "txtCDestinationCode";
            this.txtCDestinationCode.Size = new System.Drawing.Size(150, 21);
            this.txtCDestinationCode.TabIndex = 1;
            // 
            // txtCOriginCode
            // 
            this.txtCOriginCode.EmptyTextTip = "起始地港口编码";
            this.txtCOriginCode.Location = new System.Drawing.Point(15, 15);
            this.txtCOriginCode.Name = "txtCOriginCode";
            this.txtCOriginCode.Size = new System.Drawing.Size(150, 21);
            this.txtCOriginCode.TabIndex = 0;
            // 
            // btnAddConfig
            // 
            this.btnAddConfig.Location = new System.Drawing.Point(15, 42);
            this.btnAddConfig.Name = "btnAddConfig";
            this.btnAddConfig.Size = new System.Drawing.Size(150, 23);
            this.btnAddConfig.TabIndex = 2;
            this.btnAddConfig.Text = "添加配置";
            this.btnAddConfig.UseVisualStyleBackColor = true;
            this.btnAddConfig.Click += new System.EventHandler(this.btnAddConfig_Click);
            // 
            // btnYunDangCarriers
            // 
            this.btnYunDangCarriers.Location = new System.Drawing.Point(171, 42);
            this.btnYunDangCarriers.Name = "btnYunDangCarriers";
            this.btnYunDangCarriers.Size = new System.Drawing.Size(150, 23);
            this.btnYunDangCarriers.TabIndex = 3;
            this.btnYunDangCarriers.Text = "Inttra 船东添加 ";
            this.btnYunDangCarriers.UseVisualStyleBackColor = true;
            // 
            // btnSearchInttra
            // 
            this.btnSearchInttra.Location = new System.Drawing.Point(169, 104);
            this.btnSearchInttra.Name = "btnSearchInttra";
            this.btnSearchInttra.Size = new System.Drawing.Size(150, 23);
            this.btnSearchInttra.TabIndex = 6;
            this.btnSearchInttra.Text = "Inttra";
            this.btnSearchInttra.UseVisualStyleBackColor = true;
            this.btnSearchInttra.Click += new System.EventHandler(this.btnbtnSearchInttra_Click);
            // 
            // clbCarrier
            // 
            this.clbCarrier.FormattingEnabled = true;
            this.clbCarrier.Location = new System.Drawing.Point(13, 16);
            this.clbCarrier.Name = "clbCarrier";
            this.clbCarrier.ScrollAlwaysVisible = true;
            this.clbCarrier.Size = new System.Drawing.Size(150, 164);
            this.clbCarrier.TabIndex = 0;
            // 
            // txtSResult
            // 
            this.txtSResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSResult.EmptyTextTip = "船期查询或抓取结果";
            this.txtSResult.Location = new System.Drawing.Point(13, 186);
            this.txtSResult.Multiline = true;
            this.txtSResult.Name = "txtSResult";
            this.txtSResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSResult.Size = new System.Drawing.Size(744, 362);
            this.txtSResult.TabIndex = 8;
            // 
            // txtSStartDate
            // 
            this.txtSStartDate.EmptyTextTip = "出发日期";
            this.txtSStartDate.Location = new System.Drawing.Point(169, 77);
            this.txtSStartDate.Name = "txtSStartDate";
            this.txtSStartDate.Size = new System.Drawing.Size(150, 21);
            this.txtSStartDate.TabIndex = 5;
            // 
            // txtSOriginDesc
            // 
            this.txtSOriginDesc.EmptyTextTip = "起始地港口描述";
            this.txtSOriginDesc.Location = new System.Drawing.Point(169, 50);
            this.txtSOriginDesc.Name = "txtSOriginDesc";
            this.txtSOriginDesc.Size = new System.Drawing.Size(150, 21);
            this.txtSOriginDesc.TabIndex = 3;
            // 
            // txtSDestinationDesc
            // 
            this.txtSDestinationDesc.EmptyTextTip = "目的地港口描述";
            this.txtSDestinationDesc.Location = new System.Drawing.Point(325, 50);
            this.txtSDestinationDesc.Name = "txtSDestinationDesc";
            this.txtSDestinationDesc.Size = new System.Drawing.Size(150, 21);
            this.txtSDestinationDesc.TabIndex = 4;
            // 
            // txtSDestinationCode
            // 
            this.txtSDestinationCode.EmptyTextTip = "目的地港口编码";
            this.txtSDestinationCode.Location = new System.Drawing.Point(325, 20);
            this.txtSDestinationCode.Name = "txtSDestinationCode";
            this.txtSDestinationCode.Size = new System.Drawing.Size(150, 21);
            this.txtSDestinationCode.TabIndex = 2;
            // 
            // btnSearchCSP
            // 
            this.btnSearchCSP.Location = new System.Drawing.Point(325, 104);
            this.btnSearchCSP.Name = "btnSearchCSP";
            this.btnSearchCSP.Size = new System.Drawing.Size(150, 23);
            this.btnSearchCSP.TabIndex = 7;
            this.btnSearchCSP.Text = "CSP";
            this.btnSearchCSP.UseVisualStyleBackColor = true;
            this.btnSearchCSP.Click += new System.EventHandler(this.btnbtnSearchCSP_Click);
            // 
            // txtSOriginCode
            // 
            this.txtSOriginCode.EmptyTextTip = "起始地港口编码";
            this.txtSOriginCode.Location = new System.Drawing.Point(169, 20);
            this.txtSOriginCode.Name = "txtSOriginCode";
            this.txtSOriginCode.Size = new System.Drawing.Size(150, 21);
            this.txtSOriginCode.TabIndex = 1;
            // 
            // cbOnlyAbnormalData
            // 
            this.cbOnlyAbnormalData.AutoSize = true;
            this.cbOnlyAbnormalData.Location = new System.Drawing.Point(25, 13);
            this.cbOnlyAbnormalData.Name = "cbOnlyAbnormalData";
            this.cbOnlyAbnormalData.Size = new System.Drawing.Size(84, 16);
            this.cbOnlyAbnormalData.TabIndex = 0;
            this.cbOnlyAbnormalData.Text = "仅异常数据";
            this.cbOnlyAbnormalData.UseVisualStyleBackColor = true;
            // 
            // txtSStartDate2
            // 
            this.txtSStartDate2.EmptyTextTip = "出发日期";
            this.txtSStartDate2.Location = new System.Drawing.Point(25, 42);
            this.txtSStartDate2.Name = "txtSStartDate2";
            this.txtSStartDate2.Size = new System.Drawing.Size(150, 21);
            this.txtSStartDate2.TabIndex = 1;
            // 
            // btnCrawlData
            // 
            this.btnCrawlData.Location = new System.Drawing.Point(25, 78);
            this.btnCrawlData.Name = "btnCrawlData";
            this.btnCrawlData.Size = new System.Drawing.Size(150, 23);
            this.btnCrawlData.TabIndex = 2;
            this.btnCrawlData.Text = "抓取数据";
            this.btnCrawlData.UseVisualStyleBackColor = true;
            this.btnCrawlData.Click += new System.EventHandler(this.btnCrawlData_Click);
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
            this.tabControl1.Size = new System.Drawing.Size(784, 582);
            this.tabControl1.TabIndex = 0;
            // 
            // tPageConfig
            // 
            this.tPageConfig.Controls.Add(this.txtCResult);
            this.tPageConfig.Controls.Add(this.btnAddConfig);
            this.tPageConfig.Controls.Add(this.txtCDestinationCode);
            this.tPageConfig.Controls.Add(this.btnYunDangCarriers);
            this.tPageConfig.Controls.Add(this.txtCOriginCode);
            this.tPageConfig.Location = new System.Drawing.Point(4, 22);
            this.tPageConfig.Name = "tPageConfig";
            this.tPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tPageConfig.Size = new System.Drawing.Size(776, 556);
            this.tPageConfig.TabIndex = 0;
            this.tPageConfig.Text = "配置";
            this.tPageConfig.UseVisualStyleBackColor = true;
            // 
            // tPageSearch
            // 
            this.tPageSearch.Controls.Add(this.txtSResult);
            this.tPageSearch.Controls.Add(this.clbCarrier);
            this.tPageSearch.Controls.Add(this.txtSOriginCode);
            this.tPageSearch.Controls.Add(this.btnSearchInttra);
            this.tPageSearch.Controls.Add(this.txtSStartDate);
            this.tPageSearch.Controls.Add(this.btnSearchCSP);
            this.tPageSearch.Controls.Add(this.txtSOriginDesc);
            this.tPageSearch.Controls.Add(this.txtSDestinationCode);
            this.tPageSearch.Controls.Add(this.txtSDestinationDesc);
            this.tPageSearch.Location = new System.Drawing.Point(4, 22);
            this.tPageSearch.Name = "tPageSearch";
            this.tPageSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tPageSearch.Size = new System.Drawing.Size(776, 556);
            this.tPageSearch.TabIndex = 1;
            this.tPageSearch.Text = "查询";
            this.tPageSearch.UseVisualStyleBackColor = true;
            // 
            // tPageException
            // 
            this.tPageException.Controls.Add(this.cbOnlyAbnormalData);
            this.tPageException.Controls.Add(this.txtSStartDate2);
            this.tPageException.Controls.Add(this.btnCrawlData);
            this.tPageException.Location = new System.Drawing.Point(4, 22);
            this.tPageException.Name = "tPageException";
            this.tPageException.Padding = new System.Windows.Forms.Padding(3);
            this.tPageException.Size = new System.Drawing.Size(776, 556);
            this.tPageException.TabIndex = 2;
            this.tPageException.Text = "异常 / 调试";
            this.tPageException.UseVisualStyleBackColor = true;
            // 
            // FormSailingSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 582);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormSailingSchedule";
            this.ShowIcon = false;
            this.Text = "船期";
            this.Load += new System.EventHandler(this.FormSailingSchedule_Load);
            this.tabControl1.ResumeLayout(false);
            this.tPageConfig.ResumeLayout(false);
            this.tPageConfig.PerformLayout();
            this.tPageSearch.ResumeLayout(false);
            this.tPageSearch.PerformLayout();
            this.tPageException.ResumeLayout(false);
            this.tPageException.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnYunDangCarriers;
        private WatermakTextBox txtCDestinationCode;
        private WatermakTextBox txtCOriginCode;
        private System.Windows.Forms.Button btnAddConfig;
        private WatermakTextBox txtCResult;
        private System.Windows.Forms.Button btnSearchInttra;
        private WatermakTextBox txtSResult;
        private WatermakTextBox txtSStartDate;
        private WatermakTextBox txtSDestinationCode;
        private WatermakTextBox txtSOriginCode;
        private System.Windows.Forms.Button btnSearchCSP;
        private System.Windows.Forms.CheckedListBox clbCarrier;
        private WatermakTextBox txtSOriginDesc;
        private WatermakTextBox txtSDestinationDesc;
        private System.Windows.Forms.Button btnCrawlData;
        private WatermakTextBox txtSStartDate2;
        private System.Windows.Forms.CheckBox cbOnlyAbnormalData;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tPageConfig;
        private System.Windows.Forms.TabPage tPageSearch;
        private System.Windows.Forms.TabPage tPageException;
    }
}