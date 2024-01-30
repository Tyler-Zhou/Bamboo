using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using ICP.ReportCenter.ServiceInterface;
using ICP.Framework.ClientComponents.Controls;
using System.Xml;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.ReportCenter.UI.FinanceOEReports._7凭证明细列表;
using ICP.Framework.ClientComponents;

namespace ICP.ReportCenter.UI.FinanceOEReports
{
    [ToolboxItem(false)]
    public partial class VoucherInfoSearchPart : ReportBaseSearchPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IReportCenterService ReportCenterService
        {
            get
            {
                return ServiceClient.GetService<IReportCenterService>();
            }
        }

        public ICP.FAM.ServiceInterface.IFinanceReportService FinanceReportService
        {
            get
            {
                return ServiceClient.GetService<ICP.FAM.ServiceInterface.IFinanceReportService>();
            }
        }
        public ICP.FAM.ServiceInterface.IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<ICP.FAM.ServiceInterface.IFinanceService>();
            }
        }
        #endregion

        #region  init

        public VoucherInfoSearchPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.OnSearched = null;
                this._listLedgerData = null;
                this._uFCustomerList = null;

                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
            }
        }

        private void InitControls()
        {
            Utility.BulidComboboxItem<ReportYesNoType3>(cmbInnerType, 2);

            btnCheckLedger.ToolTip = "验证应收应付盈余表和凭证明细表";
            chkcmbVoucherType.AddItem(0, LocalData.IsEnglish ? "Billing Voucher" : "计帐凭证");
            chkcmbVoucherType.AddItem(1, LocalData.IsEnglish ? "Account Voucher" : "实收实付凭证");
            chkcmbVoucherType.AddItem(2, LocalData.IsEnglish ? "Return Commission Report" : "管理成本");
            chkcmbVoucherType.Items[0].CheckState = CheckState.Checked;
            chkcmbVoucherType.RefreshText();
            chkcmbVoucherType.EditValueChanged += new EventHandler(chkcmbVoucherType_EditValueChanged);
        }

        void chkcmbVoucherType_EditValueChanged(object sender, EventArgs e)
        {
            if (chkcmbVoucherType.CheckCount == 1)
            {
                //只选中[管理成本]时
                this.btnCheckFinanceCode.Text = "验证财务数据";

                //只选中[实收实付凭证]时
                if (chkcmbVoucherType.Items[1].CheckState == CheckState.Checked)
                {
                    this.chkPay.Enabled = this.chkReceive.Enabled = this.cmbInnerType.Enabled = true;
                    this.chkPay.Checked = this.chkReceive.Checked = true;
                    this.cmbInnerType.SelectedIndex = 2;
                }
                else
                {
                    this.chkPay.Enabled = this.chkReceive.Enabled = this.cmbInnerType.Enabled = false;
                    this.chkPay.Checked = this.chkReceive.Checked = false;
                    this.cmbInnerType.SelectedIndex = 2;
                }

            }
            else
            {
                this.chkPay.Enabled = this.chkReceive.Enabled = this.cmbInnerType.Enabled = false;
                this.chkPay.Checked = this.chkReceive.Checked = false;
                this.cmbInnerType.SelectedIndex = 2;
                this.btnCheckFinanceCode.Text = "验证财务代码";
            }
        }

        #endregion

        #region 属性
        string _onlyDisplayGLError = "0";
        public string XMLCondition
        {
            get
            {
                System.IO.StringWriter str = new System.IO.StringWriter();
                XmlTextWriter writer = new XmlTextWriter(str);
                writer.Formatting = Formatting.Indented;

                writer.WriteStartDocument();
                writer.WriteComment(" 查询条件XML");

                writer.WriteStartElement("root");

                writer.WriteStartElement("StructNodeId");
                writer.WriteValue(treeBoxSalesDep.GetAllEditValue.ToSplitString(","));
                writer.WriteEndElement();

                writer.WriteStartElement("Beginning_Date");
                writer.WriteValue(operationDatePart1.FromDate.ToString("yyyy-MM-dd"));
                writer.WriteEndElement();

                writer.WriteStartElement("Ending_Date");
                writer.WriteValue(operationDatePart1.ToDate.ToString("yyyy-MM-dd"));
                writer.WriteEndElement();

                writer.WriteStartElement("JobNo");
                writer.WriteValue(this.txtVoucherNo.Text.Trim());
                writer.WriteEndElement();

                writer.WriteStartElement("VoucherType");
                writer.WriteValue(chkcmbVoucherType.EditValue.ObjectsToSplitString(","));
                writer.WriteEndElement();

                writer.WriteStartElement("IsEnglish");
                writer.WriteValue(LocalData.IsEnglish ? "1" : "0");
                writer.WriteEndElement();

                writer.WriteStartElement("GLNO");
                writer.WriteValue(this.txtGLNo.Text.Trim());
                writer.WriteEndElement();

                writer.WriteStartElement("IsAgentPR");
                writer.WriteValue(this.cmbInnerType.SelectedIndex.ToString());
                writer.WriteEndElement();

                string IsPR = "2";//全部
                switch ((this.chkReceive.Checked ? "1" : "0") + (this.chkPay.Checked ? "1" : "0"))
                {
                    case "11":
                        IsPR = "2";
                        break;
                    case "00":
                        IsPR = "3";
                        break;
                    case "01":
                        IsPR = "1";
                        break;
                    case "10":
                        IsPR = "0";
                        break;
                }

                writer.WriteStartElement("IsPR");
                writer.WriteValue(IsPR);
                writer.WriteEndElement();

                writer.WriteStartElement("OnlyDisplayGLError");
                writer.WriteValue(_onlyDisplayGLError);
                writer.WriteEndElement();



                writer.WriteEndElement();

                writer.WriteEndDocument();

                writer.Close();

                return str.ToString();

            }
        }
        bool isCost = false;
        #endregion

        #region event

        #region Search

        private void btnSearch_Click(object sender, EventArgs e)
        {
            _onlyDisplayGLError = "0";
            this.btnSearch.Enabled = false;
            try
            {
                if (OnSearched != null)
                {
                    using (new CursorHelper())
                    {
                        OnSearched(this, GetData());
                    }
                }

            }
            finally
            {
                this.btnSearch.Enabled = true;
            }
        }

        public override object GetData()
        {
            if (this.chkcmbVoucherType.CheckCount == 0)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "请选择至少一种凭证." : "请选择至少一种凭证.");
                return null;
            }

            DateTime from = DateTime.Parse(this.operationDatePart1.FromDate.ToShortDateString());
            DateTime to = DateTime.Parse(this.operationDatePart1.ToDate.ToShortDateString());

            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("Condition", this.XMLCondition));

            string reportName = string.Empty;
            if (chkTotalForGL.Checked)
                reportName = "RPT_GetVoucherInfoGroupByGL";
            else
                reportName = "RPT_ALLGetVoucherInfo";

            ReportData rd = new ReportData { Parameters = paramList, ReportName = reportName };
            return rd;
        }

        #endregion

        #region CheckFinanceCode

        private void btnCheckFinanceCode_Click(object sender, EventArgs e)
        {
            CheckCustomerCode();
        }

        private void CheckCustomerCode()
        {
            try
            {
                if (this.chkcmbVoucherType.CheckCount > 1)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm()
                       , LocalData.IsEnglish ? "不能同时对多种凭证进行财务代码验证." : "不能同时对多种凭证进行财务代码验证.");
                    return;
                }

                if (this.chkcmbVoucherType.CheckCount == 0)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm()
                       , LocalData.IsEnglish ? "请选择一种凭证进行财务代码验证." : "请选择一种凭证进行财务代码验证.");
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                _uFCustomerList = new List<UFData>();
                _uFDeptList = new List<UFData>();
                _uFPersonList = new List<UFData>();
                _uFGLCodeList = new List<UFData>();

                if (chkcmbVoucherType.Items[2].CheckState == CheckState.Checked) isCost = true;
                else isCost = false;

                UFServicePart ufp = this.Workitem.Items.AddNew<UFServicePart>();
                ufp.SetSouce(isCost);
                DialogResult result = PartLoader.ShowDialog(ufp, LocalData.IsEnglish ? "用友服务器" : "用友服务器");
                if (result == DialogResult.OK)
                {
                    _uFCustomerList = ufp.UFCustomerList;
                    _uFDeptList = ufp.UFDepartmentList;
                    _uFPersonList = ufp.UFPersonList;
                    _uFGLCodeList = ufp.UFGLCodeList;

                    _listLedgerData = FinanceReportService.GetLedgerData(this.operationDatePart1.FromDate
                                     , this.operationDatePart1.ToDate
                                     , treeBoxSalesDep.GetAllEditValue.ToSplitString(",")
                                     , Convert.ToInt16(chkcmbVoucherType.EditValue.ObjectsToSplitString(",")));

                    this.ShowReportByCustomerCode();

                }
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void ShowReportByCustomerCode()
        {
            List<CheckVoucherDate> errorLst = new List<CheckVoucherDate>();

            if (chkcmbVoucherType.Items[2].CheckState == CheckState.Checked)
            {
                //验证管理费用的代码
                ufCode2ICPList.Clear();
                ufCode2ICPList = FinanceService.GetUFCode2ICPList(treeBoxSalesDep.GetAllEditValue.ToArray());
                errorLst = VoucherExport.CheckUserDepartCode(_uFCustomerList,
                                                                  _uFGLCodeList,
                                                                  _uFDeptList, 
                                                                  _uFPersonList, 
                                                                  _listLedgerData, 
                                                                  ufCode2ICPList);
            }
            else
            {
                errorLst = VoucherExport.CheckFinceCode(_uFCustomerList, _uFGLCodeList, _listLedgerData);
            }

            if (errorLst.Count == 0)
            {
                MessageBoxService.ShowInfo("不存在对应不上的数据");
                return;
            }


            ReportData rd = new ReportData();
            rd.IsLocalReport = true;
            rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.PersonFinanceCodeList.rdlc";

            rd.Parameters = new List<ReportParameter>();
            List<ReportDataSource> ds = new List<ReportDataSource>();
            ds.Add(new ReportDataSource("LedgerData", errorLst));
            rd.DataSource = ds;

            if (OnSearched != null)
            {
                using (new CursorHelper())
                {
                    OnSearched(this, rd);
                }
            }
        }

        #endregion

        #region Export

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportVoucher();
        }
        List<UFData> _uFCustomerList = new List<UFData>();

        List<UFData> _uFDeptList = new List<UFData>();

        List<UFData> _uFPersonList = new List<UFData>();

        List<UFData> _uFGLCodeList = new List<UFData>();

        List<UFCode2ICP> ufCode2ICPList = new List<UFCode2ICP>();

        List<Guid> _companyIDs = new List<Guid>();

        List<VoucherLedgerData> _listLedgerData = null;

        private void ExportVoucher()
        {
            try
            {
                if (this.chkcmbVoucherType.CheckCount == 0)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm()
                        , LocalData.IsEnglish ? "请选择至少一种凭证." : "请选择至少一种凭证.");
                    return;
                }
                else if (this.chkcmbVoucherType.CheckCount > 1)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm()
                       , LocalData.IsEnglish ? "不能同时导出多种凭证." : "不能同时导出多种凭证.");
                    return;
                }
                _uFCustomerList = new List<UFData>();
                ufCode2ICPList = new List<UFCode2ICP>();
                isCost = false;

                //管理成本
                if (this.chkcmbVoucherType.Items[2].CheckState == CheckState.Checked)
                {
                    isCost = true;
                    this.Cursor = Cursors.WaitCursor;

                    UFServicePart ufp = this.Workitem.Items.AddNew<UFServicePart>();
                    ufp.SetSouce(true);
                    DialogResult result = PartLoader.ShowDialog(ufp, LocalData.IsEnglish ? "" : "用友服务器");
                    if (result == DialogResult.OK)
                    {
                        _uFCustomerList = ufp.UFCustomerList;
                        _uFPersonList = ufp.UFPersonList;
                    }
                    else
                    {
                        return;
                    }
                    ufCode2ICPList = FinanceService.GetUFCode2ICPList(treeBoxSalesDep.GetAllEditValue.ToArray());
                }
                this.Cursor = Cursors.WaitCursor;

                _listLedgerData = FinanceReportService.GetLedgerData(this.operationDatePart1.FromDate
              , this.operationDatePart1.ToDate
              , treeBoxSalesDep.GetAllEditValue.ToSplitString(",")
              , Convert.ToInt16(chkcmbVoucherType.EditValue.ObjectsToSplitString(",")));

                if (_listLedgerData == null || _listLedgerData.Count == 0)
                {
                    MessageBoxService.ShowInfo("未找到数据.");
                    return;
                }

                VoucherLedgerData led = _listLedgerData.Find(d => d.GLCode == "会计科目错误" || d.GLCode == string.Empty);
                if (led != null)
                {
                    MessageBoxService.ShowInfo("存在会计科目有误的数据,请修改后再导入");
                    _onlyDisplayGLError = "1";
                    if (OnSearched != null)
                        OnSearched(this, GetData());
                }
                ExportVoucherToFile();

            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
            finally { this.Cursor = Cursors.Default; }
        }

        void ExportVoucherToFile()
        {
            string strVoucher = VoucherExport.BuilderPlanVoucher(_listLedgerData, _uFCustomerList,_uFPersonList, ufCode2ICPList, isCost);
            System.Windows.Forms.SaveFileDialog sDialog = new SaveFileDialog();

            //弹出保存对话框
            sDialog.CheckPathExists = true;
            sDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            sDialog.RestoreDirectory = true;

            if (sDialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.Stream stream = sDialog.OpenFile();
                byte[] buffer = System.Text.Encoding.Default.GetBytes(strVoucher);
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();
            }
        }

        #endregion

        #endregion

        #region ISearchPart 成员

        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion

        private void btnCheckLedger_Click(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm(LocalData.IsEnglish ? "Verifying,Please Waiting..." : "正在验证,请稍等...");
            ReportCenterService.CheckLedgersForBill(this.operationDatePart1.FromDate, this.operationDatePart1.ToDate, treeBoxSalesDep.GetAllEditValue.ToSplitString("&#;"));
            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                UCBackups bk = this.Workitem.Items.AddNew<UCBackups>();
                DialogResult result = PartLoader.ShowDialog(bk, "凭证备份");
                if (result == DialogResult.OK)
                {
                    _companyIDs = bk.CompanyIDs;
                    ReportCenterService.BackupLedger(bk.YearMonth, _companyIDs.ToArray(), LocalData.UserInfo.LoginID);
                }
                else
                {
                    return;
                }
                MessageBox.Show("凭证备份成功！");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }


}
