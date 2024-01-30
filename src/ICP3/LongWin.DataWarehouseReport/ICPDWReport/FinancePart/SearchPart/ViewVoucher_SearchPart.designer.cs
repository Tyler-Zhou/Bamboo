
namespace LongWin.DataWarehouseReport.WinUI
{
    partial class ViewVoucher_SearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewVoucher_SearchPart));
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup2 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup3 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            this.ultraExplorerBarContainerControl1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.ucDateTime = new LongWin.DataWarehouseReport.WinUI.DateTimeCtl();
            this.ultraExplorerBarContainerControl2 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.cmbVoucherType = new LongWin.DataWarehouseReport.WinUI.CheckComboBox();
            this.cbPay = new System.Windows.Forms.CheckBox();
            this.checkTotalForGL = new System.Windows.Forms.CheckBox();
            this.cbReceive = new System.Windows.Forms.CheckBox();
            this.tbGLNo = new System.Windows.Forms.TextBox();
            this.tbVoucherNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbInnerType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtShipTo = new LongWin.DataWarehouseReport.WinUI.utlMultiSelect();
            this.label3 = new System.Windows.Forms.Label();
            this.ultraExplorerBarContainerControl3 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl();
            this.ucSelectCompany = new LongWin.DataWarehouseReport.WinUI.SelectJobPlaceCtl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btExport = new System.Windows.Forms.Button();
            this.btCheckFinanceCode = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.ultraExplorerBar1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBar();
            this.ultraExplorerBarContainerControl1.SuspendLayout();
            this.ultraExplorerBarContainerControl2.SuspendLayout();
            this.ultraExplorerBarContainerControl3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraExplorerBar1)).BeginInit();
            this.ultraExplorerBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ultraExplorerBarContainerControl1
            // 
            this.ultraExplorerBarContainerControl1.Controls.Add(this.ucDateTime);
            resources.ApplyResources(this.ultraExplorerBarContainerControl1, "ultraExplorerBarContainerControl1");
            this.ultraExplorerBarContainerControl1.Name = "ultraExplorerBarContainerControl1";
            // 
            // ucDateTime
            // 
            this.ucDateTime.BackColor = System.Drawing.SystemColors.Window;
            this.ucDateTime.ConditionType = LongWin.DataWarehouseReport.WinUI.ConditionDateType.Month;
            this.ucDateTime.DateTimeFrom = new System.DateTime(2010, 6, 1, 0, 0, 0, 0);
            this.ucDateTime.DateTimeTo = new System.DateTime(2010, 6, 30, 0, 0, 0, 0);
            this.ucDateTime.DefaultDateTimeType = LongWin.DataWarehouseReport.WinUI.DateTimeCtl.DefaultType.ThisMonth;
            this.ucDateTime.DefaultFrom = new System.DateTime(((long)(0)));
            this.ucDateTime.DefaultTo = new System.DateTime(((long)(0)));
            resources.ApplyResources(this.ucDateTime, "ucDateTime");
            this.ucDateTime.Name = "ucDateTime";
            // 
            // ultraExplorerBarContainerControl2
            // 
            this.ultraExplorerBarContainerControl2.Controls.Add(this.cmbVoucherType);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.cbPay);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.checkTotalForGL);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.cbReceive);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.tbGLNo);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.tbVoucherNo);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label4);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.cmbInnerType);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label5);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label1);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label2);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.txtShipTo);
            this.ultraExplorerBarContainerControl2.Controls.Add(this.label3);
            resources.ApplyResources(this.ultraExplorerBarContainerControl2, "ultraExplorerBarContainerControl2");
            this.ultraExplorerBarContainerControl2.Name = "ultraExplorerBarContainerControl2";
            // 
            // cmbVoucherType
            // 
            resources.ApplyResources(this.cmbVoucherType, "cmbVoucherType");
            this.cmbVoucherType.BackColor = System.Drawing.Color.White;
            this.cmbVoucherType.Items = new string[] {
        "业务员",
        "客户",
        "承运人",
        "航线",
        "船公司",
        "业务类型",
        "业务发生地",
        "揽货方式",
        "装货港"};
            this.cmbVoucherType.MaxChecked = 3;
            this.cmbVoucherType.Name = "cmbVoucherType";
            this.cmbVoucherType.TextChanaged += new System.EventHandler(this.cmbVoucherType_TextChanaged);
            // 
            // cbPay
            // 
            resources.ApplyResources(this.cbPay, "cbPay");
            this.cbPay.BackColor = System.Drawing.Color.Transparent;
            this.cbPay.Checked = true;
            this.cbPay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPay.Name = "cbPay";
            this.cbPay.UseVisualStyleBackColor = false;
            // 
            // checkTotalForGL
            // 
            resources.ApplyResources(this.checkTotalForGL, "checkTotalForGL");
            this.checkTotalForGL.BackColor = System.Drawing.Color.Transparent;
            this.checkTotalForGL.Checked = true;
            this.checkTotalForGL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkTotalForGL.Name = "checkTotalForGL";
            this.checkTotalForGL.UseVisualStyleBackColor = false;
            // 
            // cbReceive
            // 
            resources.ApplyResources(this.cbReceive, "cbReceive");
            this.cbReceive.BackColor = System.Drawing.Color.Transparent;
            this.cbReceive.Checked = true;
            this.cbReceive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbReceive.Name = "cbReceive";
            this.cbReceive.UseVisualStyleBackColor = false;
            // 
            // tbGLNo
            // 
            resources.ApplyResources(this.tbGLNo, "tbGLNo");
            this.tbGLNo.BackColor = System.Drawing.Color.White;
            this.tbGLNo.Name = "tbGLNo";
            // 
            // tbVoucherNo
            // 
            resources.ApplyResources(this.tbVoucherNo, "tbVoucherNo");
            this.tbVoucherNo.BackColor = System.Drawing.Color.White;
            this.tbVoucherNo.Name = "tbVoucherNo";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Name = "label4";
            // 
            // cmbInnerType
            // 
            resources.ApplyResources(this.cmbInnerType, "cmbInnerType");
            this.cmbInnerType.BackColor = System.Drawing.Color.White;
            this.cmbInnerType.FormattingEnabled = true;
            this.cmbInnerType.Items.AddRange(new object[] {
            resources.GetString("cmbInnerType.Items"),
            resources.GetString("cmbInnerType.Items1"),
            resources.GetString("cmbInnerType.Items2")});
            this.cmbInnerType.Name = "cmbInnerType";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Name = "label5";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // txtShipTo
            // 
            resources.ApplyResources(this.txtShipTo, "txtShipTo");
            this.txtShipTo.Name = "txtShipTo";
            this.txtShipTo.SelectedText = "";
            this.txtShipTo.SelectedValue = null;
            this.txtShipTo.DoSearching += new System.EventHandler<LongWin.DataWarehouseReport.WinUI.SearchEventArgs>(this.txtShipTo_DoSearching);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Name = "label3";
            // 
            // ultraExplorerBarContainerControl3
            // 
            this.ultraExplorerBarContainerControl3.Controls.Add(this.ucSelectCompany);
            resources.ApplyResources(this.ultraExplorerBarContainerControl3, "ultraExplorerBarContainerControl3");
            this.ultraExplorerBarContainerControl3.Name = "ultraExplorerBarContainerControl3";
            // 
            // ucSelectCompany
            // 
            resources.ApplyResources(this.ucSelectCompany, "ucSelectCompany");
            this.ucSelectCompany.BackColor = System.Drawing.Color.Transparent;
            this.ucSelectCompany.DataSource = null;
            this.ucSelectCompany.Name = "ucSelectCompany";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btExport);
            this.panel1.Controls.Add(this.btCheckFinanceCode);
            this.panel1.Controls.Add(this.btnSearch);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btExport
            // 
            resources.ApplyResources(this.btExport, "btExport");
            this.btExport.Name = "btExport";
            this.btExport.UseVisualStyleBackColor = true;
            this.btExport.Click += new System.EventHandler(this.btExport_Click);
            // 
            // btCheckFinanceCode
            // 
            resources.ApplyResources(this.btCheckFinanceCode, "btCheckFinanceCode");
            this.btCheckFinanceCode.Name = "btCheckFinanceCode";
            this.btCheckFinanceCode.UseVisualStyleBackColor = true;
            this.btCheckFinanceCode.Click += new System.EventHandler(this.btCheckFinanceCode_Click);
            // 
            // btnSearch
            // 
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // ultraExplorerBar1
            // 
            this.ultraExplorerBar1.Controls.Add(this.ultraExplorerBarContainerControl1);
            this.ultraExplorerBar1.Controls.Add(this.ultraExplorerBarContainerControl2);
            this.ultraExplorerBar1.Controls.Add(this.ultraExplorerBarContainerControl3);
            resources.ApplyResources(this.ultraExplorerBar1, "ultraExplorerBar1");
            ultraExplorerBarGroup1.Container = this.ultraExplorerBarContainerControl1;
            ultraExplorerBarGroup1.Settings.ContainerHeight = 178;
            resources.ApplyResources(ultraExplorerBarGroup1, "ultraExplorerBarGroup1");
            ultraExplorerBarGroup2.Container = this.ultraExplorerBarContainerControl2;
            ultraExplorerBarGroup2.Settings.ContainerHeight = 178;
            resources.ApplyResources(ultraExplorerBarGroup2, "ultraExplorerBarGroup2");
            ultraExplorerBarGroup3.Container = this.ultraExplorerBarContainerControl3;
            ultraExplorerBarGroup3.Settings.ContainerHeight = 70;
            resources.ApplyResources(ultraExplorerBarGroup3, "ultraExplorerBarGroup3");
            this.ultraExplorerBar1.Groups.AddRange(new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup[] {
            ultraExplorerBarGroup1,
            ultraExplorerBarGroup2,
            ultraExplorerBarGroup3});
            this.ultraExplorerBar1.GroupSettings.Style = Infragistics.Win.UltraWinExplorerBar.GroupStyle.ControlContainer;
            this.ultraExplorerBar1.Name = "ultraExplorerBar1";
            // 
            // ViewVoucher_SearchPart
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.ultraExplorerBar1);
            this.Controls.Add(this.panel1);
            this.Name = "ViewVoucher_SearchPart";
            this.ultraExplorerBarContainerControl1.ResumeLayout(false);
            this.ultraExplorerBarContainerControl2.ResumeLayout(false);
            this.ultraExplorerBarContainerControl2.PerformLayout();
            this.ultraExplorerBarContainerControl3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraExplorerBar1)).EndInit();
            this.ultraExplorerBar1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSearch;
        private Infragistics.Win.UltraWinExplorerBar.UltraExplorerBar ultraExplorerBar1;
        private Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl ultraExplorerBarContainerControl1;
        private DateTimeCtl ucDateTime;
        private Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl ultraExplorerBarContainerControl2;
        private System.Windows.Forms.Label label3;
        private Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarContainerControl ultraExplorerBarContainerControl3;
        private SelectJobPlaceCtl ucSelectCompany;
        private utlMultiSelect txtShipTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbVoucherNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btCheckFinanceCode;
        private System.Windows.Forms.Button btExport;
        private System.Windows.Forms.TextBox tbGLNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbPay;
        private System.Windows.Forms.CheckBox cbReceive;
        private System.Windows.Forms.ComboBox cmbInnerType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkTotalForGL;
        private CheckComboBox cmbVoucherType;
    }
}
