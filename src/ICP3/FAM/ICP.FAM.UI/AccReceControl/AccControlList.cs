using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.FAM.UI.MonthlyClosingEntry;
using ICP.ReportCenter.UI;
using Microsoft.Reporting.WinForms;
using ICP.Framework.CommonLibrary.Common;
using System.Text;
using System.Xml;
using System.Linq;
using ICP.Message.ServiceInterface;
using ICP.MailCenter.ServiceInterface;
using ICP.MailCenter.UI;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.DataCache.ServiceInterface;
using ICP.FileSystem.ServiceInterface;

namespace ICP.FAM.UI.AccReceControl
{
    /// <summary>
    /// 账款控制列表
    /// </summary>
    [ToolboxItem(false)]
    public partial class AccControlList : BaseListPart
    {
        #region 服务注入
        /// <summary>
        /// 服务注入
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 财务服务
        /// </summary>
        IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        /// <summary>
        /// 发送邮件模板服务
        /// </summary>
        IMailCenterTemplateService MailCenterTemplateService
        {
            get
            {
                ClientHelper.EnsureEmailCenterAppStarted();
                return new MailCenterTemplateService();
            }
        }
        /// <summary>
        /// 财务报表服务
        /// </summary>
        IFinanceReportService FinanceReportService
        {
            get
            {
                return ServiceClient.GetService<IFinanceReportService>();
            }
        }
        /// <summary>
        /// 报表中心帮助类
        /// </summary>
        public ReportCenterHelper ReportCenterHelper
        {
            get
            {
                return ClientHelper.Get<ReportCenterHelper, ReportCenterHelper>();
            }
        }

        /// <summary>
        /// 客户端上传文档服务
        /// </summary>
        public IClientFileService ClientFileService
        {
            get
            {
                return ServiceClient.GetService<IClientFileService>();
            }

        }

        #endregion
        /// <summary>
        /// 账款控制列表
        /// </summary>
        public AccControlList()
        {
            InitializeComponent();
            Disposed += delegate
            {
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
                FAMUtility.ShowGridRowNo(gvMain);
            }
        }

        public override object Current
        {
            get { return bsList.Current; }
        }
        protected CustomerAgingList CurrentRow
        {
            get { return Current as CustomerAgingList; }
        }

        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }

        private void BindingData(object value)
        {
            List<CustomerAgingList> source = value as List<CustomerAgingList>;


            //PageList<CurrencyBillList> source = value as PageList<CurrencyBillList>;
            if (source == null || source.Count == 0)
            {
                bsList.DataSource = new List<CurrencyBillList>();
                gvMain.SortInfo.Clear();
            }
            else
            {

                bsList.DataSource = source;
                bsList.ResetBindings(false);
                gvMain.BestFitColumns();

                if (CurrentChanged != null)
                {
                    CurrentChanged(this, CurrentRow);
                }
            }
        }

        private List<CustomerAgingList> DataList
        {
            get
            {
                List<CustomerAgingList> list = bsList.DataSource as List<CustomerAgingList>;
                if (list == null)
                {
                    list = new List<CustomerAgingList>();
                }
                return list;
            }
        }
        public SearchParameter _SearchParameter;
        public new event KeyEventHandler KeyDown;
        public override event CurrentChangedHandler CurrentChanged;


        private void gcMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CurrentRow != null)
            {
                Workitem.Commands[BankCommandConstants.Command_BankEdit].Execute();
            }
            else if (KeyDown != null
               && e.KeyCode == Keys.F5
               && gvMain.FocusedColumn != null
               && gvMain.FocusedValue != null)
            {
                string text = gvMain.GetFocusedDisplayText();
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add(gvMain.FocusedColumn.FieldName, text);
                KeyDown(keyValue, null);
            }
            if (e.KeyCode == Keys.F6 && CurrentRow != null)
            {
                Workitem.Commands[BankCommandConstants.Command_BankShowSearch].Execute();
            }
        }

        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            BankList list = gvMain.GetRow(e.RowHandle) as BankList;
            if (list == null) return;

            if (list.IsValid == false)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Disabled);
            }
        }

        #region 通知客户账单
        /// <summary>
        /// 通知客户账单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(AccControlCommandConstants.Command_AccControlMail)]
        public void Command_AccControlMail(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "Please select the record first!" : "请先查询记录！",
                                                                           LocalData.IsEnglish ? "Tip" : "提示",
                                                                           MessageBoxButtons.OK,
                                                                           MessageBoxIcon.Warning);
                return;
            }

            List<CustomerPreferencesInfo> customerPreferenceList = FinanceService.GetCustomerPreferencesInfo(null, CurrentRow.CustomerID, CurrentRow.CompanyID);
            if (customerPreferenceList == null || customerPreferenceList.Count == 0)
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "Please set customer preferences first!" : "请先设置客户偏好！",
                                                            LocalData.IsEnglish ? "Tip" : "提示",
                                                            MessageBoxButtons.OK,
                                                            MessageBoxIcon.Warning);
                Command_AccControlCustomerPreference(sender, e);
                return;
            }
            CustomerPreferencesInfo customerPreference = customerPreferenceList[0];
            List<AttachmentContent> atts = new List<AttachmentContent>();

            int theradID = 0;
            string mess = "正在生成邮件内容,请稍候...";
            string returnmess = string.Empty;
            theradID = LoadingServce.ShowLoadingForm(mess);
            try
            {
                CheckType checkType = CheckType.None;
                if (_SearchParameter.BillTypes.Contains(BillType.AR) && _SearchParameter.BillTypes.Contains(BillType.AP)) checkType = CheckType.None;
                else if (_SearchParameter.BillTypes.Contains(BillType.AP)) checkType = CheckType.AP;
                else if (_SearchParameter.BillTypes.Contains(BillType.AR)) checkType = CheckType.AR;
                LocalStatementOrderByEnum orderBy = _SearchParameter.SearchType == 0 ? LocalStatementOrderByEnum.BillDate : LocalStatementOrderByEnum.DueDate;
                CustomerInfo userCompany = ReportCenterHelper.GetCompanyInfo(CurrentRow.CompanyID);

                List<LocalStatementReportData> data = FinanceReportService.GetLocalStatementReportData
                         (CurrentRow.CustomerID
                         , DateTime.Parse("2013-1-1")
                         , _SearchParameter.EndingDate
                         , orderBy
                         , checkType
                         , StatementBillStateEnum.Open
                         , new Guid[1] { CurrentRow.CompanyID }
                         , null, null
                         , null, null);

                List<LocalStatementReportDetailData> detailData = FinanceReportService.GetLocalStatementReportDetailData
                      (CurrentRow.CustomerID
                      , DateTime.Parse("2013-1-1")
                      , _SearchParameter.EndingDate
                      , orderBy
                      , checkType
                      , StatementBillStateEnum.Open
                      , new Guid[1] { CurrentRow.CompanyID }
                      , null, null
                      , null, null);

                foreach (LocalStatementReportDetailData detail in detailData)
                {
                    //if (string.IsNullOrEmpty(customerPreference.InvoiceTitle))
                    //{
                    //    detail.CompanyName = detail.CompanyName + Environment.NewLine + detail.CompanyAddress + Environment.NewLine + detail.CompanyTel + Environment.NewLine + detail.CompanyFax;
                    //}
                    //else
                    //{
                    //    detail.CompanyName = customerPreference.InvoiceTitle;
                    //}
                    if (string.IsNullOrEmpty(customerPreference.ShipTo))
                    {
                        detail.BillToName = detail.BillToName + Environment.NewLine + detail.BillToAddress + Environment.NewLine + detail.BillToTelFax;
                        detail.BillToAttn = detail.BillToName + Environment.NewLine + detail.BillToAddress + Environment.NewLine + detail.BillToTelFax;
                    }
                    else
                    {
                        detail.BillToName = detail.BillToName + Environment.NewLine + detail.BillToAddress + Environment.NewLine + detail.BillToTelFax;
                        detail.BillToAttn = customerPreference.ShipTo;
                    }
                }

                if (customerPreference.PdfAssembled == 0)
                {
                    ReportViewBase report = new ReportViewBase();

                    //主报表的数据源
                    List<ReportDataSource> ds = new List<ReportDataSource>();
                    ds.Add(new ReportDataSource("AGTLocalStatement_LocalDebit", data));

                    List<LocalStatementTotalReportData> totallist = BulidTotalList(data);//汇总数据 ,用于子报表
                    //子报表的数据源
                    List<ReportDataSource> sbds = new List<ReportDataSource>();
                    sbds.Add(new ReportDataSource("LocalStatementTotalReportData", totallist));


                    //LocalStatementDataTable.TableName = "AGTLocalStatement_LocalDebit";
                    List<ReportParameter> paramList = new List<ReportParameter>();

                    #region 报表参数1

                    paramList.Add(new ReportParameter("BeginTime", "2013-1-1"));
                    paramList.Add(new ReportParameter("EndTime", _SearchParameter.EndingDate.ToShortDateString()));
                    paramList.Add(new ReportParameter("CompanyName", userCompany.EName));
                    paramList.Add(new ReportParameter("UserAddress", userCompany.EAddress));
                    paramList.Add(new ReportParameter("UserTel", userCompany.Tel1));
                    paramList.Add(new ReportParameter("UserFax", userCompany.Fax));
                    paramList.Add(new ReportParameter("ReportTypeName", ""));
                    paramList.Add(new ReportParameter("DataType", orderBy == LocalStatementOrderByEnum.BillDate ? "By InvioceDate(SortByInvoiceNO)" : "By Due Date"));
                    paramList.Add(new ReportParameter("OpenorAll", "Open"));
                    paramList.Add(new ReportParameter("IsAttached", true.ToString()));
                    paramList.Add(new ReportParameter("CurrentUser", LocalData.UserInfo.LoginName));
                    paramList.Add(new ReportParameter("AmountCurrency", string.Empty));

                    if (totallist != null && totallist.Count > 0) paramList.Add(new ReportParameter("IsHideTotalReport", "False"));
                    else paramList.Add(new ReportParameter("IsHideTotalReport", "True"));

                    #endregion

                    if (detailData != null && detailData.Count > 0)
                    {
                        sbds.Add(new ReportDataSource("AGTLocalStatement_LocalDebitDetail", detailData));
                    }

                    paramList.Add(new ReportParameter("IsHideAttachReport", false.ToString()));
                    paramList.Add(new ReportParameter("IsHideAttachReportForRelax", true.ToString()));

                    ReportData rd = new ReportData();

                    rd.CustomerID = CurrentRow.CustomerID;

                    rd.IsLocalReport = true;
                    rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.RptLocalStatementReportCopy.rdlc";
                    rd.Parameters = paramList;
                    rd.DataSource = ds;
                    rd.SubDataSource = sbds;

                    report.DisplayLocalReport(rd.ReportName, rd.DataSource, rd.Parameters, rd.SubDataSource);
                    string path = report.pdfExportLocal("LocalStatement.pdf");

                    AttachmentContent att = new AttachmentContent();
                    att.Content = report.GetpdfExport("LocalStatement.pdf");
                    att.DisplayName = "LocalStatement.pdf";
                    att.Name = "LocalStatement.pdf";
                    att.ClientPath = path;
                    atts.Add(att);
                    foreach (IGrouping<string, LocalStatementReportDetailData> detail in detailData.GroupBy(r => r.InvoiceNo))
                    {
                        List<CurrencyBillList> billlist = FinanceService.GetBillListByIds(new Guid[] { detail.ToList()[0].BillId }, LocalData.IsEnglish);
                        if (billlist != null && billlist.Count > 0)
                        {
                            BusinessOperationContext context = new BusinessOperationContext();
                            context.OperationID = billlist[0].OperationID;
                            List<DocumentInfo> list = ClientFileService.GetBusinessDocumentList(context, DataSearchType.ALL);
                            DocumentInfo info = list.Find(r => r.DocumentType == DocumentType.HBL);
                            if (info != null)
                            {
                                ContentInfo file = ClientFileService.GetDocumentContent(info.Id, DataSearchType.ALL);
                                if (file != null)
                                {
                                    string filePath = DataCacheUtility.SaveFileContentToDisk(file);
                                    AttachmentContent detailhbl = new AttachmentContent();
                                    detailhbl.Content = file.Content;
                                    detailhbl.DisplayName = info.Name;
                                    detailhbl.Name = info.Name;
                                    detailhbl.ClientPath = filePath;
                                    atts.Add(detailhbl);
                                }

                            }
                            else
                            {
                                returnmess += "The shipment [" + billlist[0].BLNo + "] has no Copy of HBL in Document List" + Environment.NewLine;
                            }
                        }

                        if (billlist != null && billlist.Count > 0)
                        {
                            BusinessOperationContext context = new BusinessOperationContext();
                            context.OperationID = billlist[0].OperationID;
                            List<DocumentInfo> list = ClientFileService.GetBusinessDocumentList(context, DataSearchType.ALL);
                            DocumentInfo info = list.Find(r => r.DocumentType == DocumentType.AN);
                            if (info != null)
                            {
                                ContentInfo file = ClientFileService.GetDocumentContent(info.Id, DataSearchType.ALL);
                                if (file != null)
                                {
                                    string filePath = DataCacheUtility.SaveFileContentToDisk(file);
                                    AttachmentContent detailan = new AttachmentContent();
                                    detailan.Content = info.Content;
                                    detailan.DisplayName = info.Name;
                                    detailan.Name = info.Name;
                                    detailan.ClientPath = filePath;
                                    atts.Add(detailan);
                                }
                            }
                            else
                            {
                                returnmess += "The shipment [" + billlist[0].BLNo + "] has no Arrival Notice in Document List" + Environment.NewLine;
                            }
                        }
                    }
                }
                else if (customerPreference.PdfAssembled == 1)
                {
                    foreach (IGrouping<string, LocalStatementReportData> group in data.GroupBy(r => r.ETA.ToString("yyyyMM")))
                    {
                        ReportViewBase report = new ReportViewBase();

                        //主报表的数据源
                        List<ReportDataSource> ds = new List<ReportDataSource>();
                        ds.Add(new ReportDataSource("AGTLocalStatement_LocalDebit", group.ToList()));

                        List<LocalStatementTotalReportData> totallist = BulidTotalList(group.ToList());//汇总数据 ,用于子报表
                        //子报表的数据源
                        List<ReportDataSource> sbds = new List<ReportDataSource>();
                        sbds.Add(new ReportDataSource("LocalStatementTotalReportData", totallist));


                        //LocalStatementDataTable.TableName = "AGTLocalStatement_LocalDebit";
                        List<ReportParameter> paramList = new List<ReportParameter>();

                        #region 报表参数1

                        paramList.Add(new ReportParameter("BeginTime", "2013-1-1"));
                        paramList.Add(new ReportParameter("EndTime", _SearchParameter.EndingDate.ToShortDateString()));
                        paramList.Add(new ReportParameter("CompanyName", userCompany.EName));
                        paramList.Add(new ReportParameter("UserAddress", userCompany.EAddress));
                        paramList.Add(new ReportParameter("UserTel", userCompany.Tel1));
                        paramList.Add(new ReportParameter("UserFax", userCompany.Fax));
                        paramList.Add(new ReportParameter("ReportTypeName", ""));
                        paramList.Add(new ReportParameter("DataType", orderBy == LocalStatementOrderByEnum.BillDate ? "By InvioceDate(SortByInvoiceNO)" : "By Due Date"));
                        paramList.Add(new ReportParameter("OpenorAll", "Open"));
                        paramList.Add(new ReportParameter("IsAttached", true.ToString()));
                        paramList.Add(new ReportParameter("CurrentUser", LocalData.UserInfo.LoginName));
                        paramList.Add(new ReportParameter("AmountCurrency", string.Empty));

                        if (totallist != null && totallist.Count > 0) paramList.Add(new ReportParameter("IsHideTotalReport", "False"));
                        else paramList.Add(new ReportParameter("IsHideTotalReport", "True"));

                        #endregion

                        if (detailData != null && detailData.Count > 0)
                        {
                            sbds.Add(new ReportDataSource("AGTLocalStatement_LocalDebitDetail", detailData.FindAll(r =>
                            {
                                string[] strs = r.PODAndETA.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                                if (strs.Length == 4)
                                {
                                    if (group.Key == strs[3] + strs[1])
                                    {
                                        return true;
                                    }
                                }

                                return false;
                            })));
                        }

                        paramList.Add(new ReportParameter("IsHideAttachReport", false.ToString()));
                        paramList.Add(new ReportParameter("IsHideAttachReportForRelax", true.ToString()));

                        ReportData rd = new ReportData();

                        rd.CustomerID = CurrentRow.CustomerID;

                        rd.IsLocalReport = true;
                        rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.RptLocalStatementReportCopy.rdlc";
                        rd.Parameters = paramList;
                        rd.DataSource = ds;
                        rd.SubDataSource = sbds;

                        report.DisplayLocalReport(rd.ReportName, rd.DataSource, rd.Parameters, rd.SubDataSource);
                        string path = report.pdfExportLocal("LocalStatement-" + group.Key + ".pdf");

                        AttachmentContent att = new AttachmentContent();
                        att.Content = report.GetpdfExport("LocalStatement-" + group.Key + ".pdf");
                        att.DisplayName = "LocalStatement-" + group.Key + ".pdf";
                        att.Name = "LocalStatement-" + group.Key + ".pdf";
                        att.ClientPath = path;
                        atts.Add(att);
                    }
                }
                else if (customerPreference.PdfAssembled == 2)
                {
                    ReportViewBase report = new ReportViewBase();

                    //主报表的数据源
                    List<ReportDataSource> ds = new List<ReportDataSource>();
                    ds.Add(new ReportDataSource("AGTLocalStatement_LocalDebit", data));

                    List<LocalStatementTotalReportData> totallist = BulidTotalList(data);//汇总数据 ,用于子报表
                    //子报表的数据源
                    List<ReportDataSource> sbds = new List<ReportDataSource>();
                    sbds.Add(new ReportDataSource("LocalStatementTotalReportData", totallist));


                    //LocalStatementDataTable.TableName = "AGTLocalStatement_LocalDebit";
                    List<ReportParameter> paramList = new List<ReportParameter>();

                    #region 报表参数1

                    paramList.Add(new ReportParameter("BeginTime", "2013-1-1"));
                    paramList.Add(new ReportParameter("EndTime", _SearchParameter.EndingDate.ToShortDateString()));
                    paramList.Add(new ReportParameter("CompanyName", userCompany.EName));
                    paramList.Add(new ReportParameter("UserAddress", userCompany.EAddress));
                    paramList.Add(new ReportParameter("UserTel", userCompany.Tel1));
                    paramList.Add(new ReportParameter("UserFax", userCompany.Fax));
                    paramList.Add(new ReportParameter("ReportTypeName", ""));
                    paramList.Add(new ReportParameter("DataType", orderBy == LocalStatementOrderByEnum.BillDate ? "By InvioceDate(SortByInvoiceNO)" : "By Due Date"));
                    paramList.Add(new ReportParameter("OpenorAll", "Open"));
                    paramList.Add(new ReportParameter("IsAttached", false.ToString()));
                    paramList.Add(new ReportParameter("CurrentUser", LocalData.UserInfo.LoginName));
                    paramList.Add(new ReportParameter("AmountCurrency", string.Empty));

                    if (totallist != null && totallist.Count > 0) paramList.Add(new ReportParameter("IsHideTotalReport", "False"));
                    else paramList.Add(new ReportParameter("IsHideTotalReport", "True"));

                    #endregion

                    paramList.Add(new ReportParameter("IsHideAttachReport", true.ToString()));
                    paramList.Add(new ReportParameter("IsHideAttachReportForRelax", true.ToString()));

                    ReportData rd = new ReportData();

                    rd.CustomerID = CurrentRow.CustomerID;

                    rd.IsLocalReport = true;
                    rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.RptLocalStatementReportCopy.rdlc";
                    rd.Parameters = paramList;
                    rd.DataSource = ds;
                    rd.SubDataSource = sbds;

                    report.DisplayLocalReport(rd.ReportName, rd.DataSource, rd.Parameters, rd.SubDataSource);
                    string path = report.pdfExportLocal("LocalStatement.pdf");

                    AttachmentContent att = new AttachmentContent();
                    att.Content = report.GetpdfExport("LocalStatement.pdf");
                    att.DisplayName = "LocalStatement.pdf";
                    att.Name = "LocalStatement.pdf";
                    att.ClientPath = path;
                    atts.Add(att);

                    if (detailData != null && detailData.Count > 0)
                    {
                        foreach (IGrouping<string, LocalStatementReportDetailData> detail in detailData.GroupBy(r => r.InvoiceNo))
                        {
                            #region NO
                            report = new ReportViewBase();
                            ds = new List<ReportDataSource>();
                            List<LocalStatementReportDetailData> sours = detailData.FindAll(r => r.BillId == detail.ToList()[0].BillId);
                            ds.Add(new ReportDataSource("AGTLocalStatement_LocalDebitDetail", sours));
                            paramList = new List<ReportParameter>();

                            #region 报表参数1

                            paramList.Add(new ReportParameter("BillToId", detail.ToList()[0].BillToId.ToString()));
                            paramList.Add(new ReportParameter("CompanyName", userCompany.EName));
                            paramList.Add(new ReportParameter("CompanyAddress", userCompany.EAddress));
                            paramList.Add(new ReportParameter("TeL", userCompany.Tel1));
                            paramList.Add(new ReportParameter("FAX", userCompany.Fax));

                            #endregion

                            rd = new ReportData();

                            rd.CustomerID = CurrentRow.CustomerID;

                            rd.IsLocalReport = true;
                            //rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.RptLocalStatementAttachReport.rdlc";
                            rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.InvoiceInfo.rdlc";
                            rd.Parameters = paramList;
                            rd.DataSource = ds;
                            report.DisplayLocalReport(rd.ReportName, rd.DataSource, rd.Parameters, rd.SubDataSource);
                            string detailpath = report.pdfExportLocal(detail.ToList()[0].InvoiceNo + ".pdf");

                            AttachmentContent detailatt = new AttachmentContent();
                            detailatt.Content = report.GetpdfExport(detail.ToList()[0].InvoiceNo + ".pdf");
                            detailatt.DisplayName = detail.ToList()[0].InvoiceNo + ".pdf";
                            detailatt.Name = detail.ToList()[0].InvoiceNo + ".pdf";
                            detailatt.ClientPath = detailpath;
                            atts.Add(detailatt);
                            #endregion

                            if (customerPreference.OtherAttachments == 1 || customerPreference.OtherAttachments == 3)
                            {
                                List<CurrencyBillList> billlist = FinanceService.GetBillListByIds(new Guid[] { detail.ToList()[0].BillId }, LocalData.IsEnglish);
                                if (billlist != null && billlist.Count > 0)
                                {
                                    BusinessOperationContext context = new BusinessOperationContext();
                                    context.OperationID = billlist[0].OperationID;
                                    List<DocumentInfo> list = ClientFileService.GetBusinessDocumentList(context, DataSearchType.ALL);
                                    DocumentInfo info = list.Find(r => r.DocumentType == DocumentType.HBL);
                                    if (info != null)
                                    {
                                        ContentInfo file = ClientFileService.GetDocumentContent(info.Id, DataSearchType.ALL);
                                        if (file != null)
                                        {
                                            string filePath = DataCacheUtility.SaveFileContentToDisk(file);
                                            AttachmentContent detailhbl = new AttachmentContent();
                                            detailhbl.Content = file.Content;
                                            detailhbl.DisplayName = info.Name;
                                            detailhbl.Name = info.Name;
                                            detailhbl.ClientPath = filePath;
                                            atts.Add(detailhbl);
                                        }

                                    }
                                    else
                                    {
                                        returnmess += "The shipment [" + billlist[0].BLNo + "] has no Copy of HBL in Document List" + Environment.NewLine;
                                    }
                                }
                            }

                            if (customerPreference.OtherAttachments == 2 || customerPreference.OtherAttachments == 3)
                            {
                                List<CurrencyBillList> billlist = FinanceService.GetBillListByIds(new Guid[] { detail.ToList()[0].BillId }, LocalData.IsEnglish);
                                if (billlist != null && billlist.Count > 0)
                                {
                                    BusinessOperationContext context = new BusinessOperationContext();
                                    context.OperationID = billlist[0].OperationID;
                                    List<DocumentInfo> list = ClientFileService.GetBusinessDocumentList(context, DataSearchType.ALL);
                                    DocumentInfo info = list.Find(r => r.DocumentType == DocumentType.AN);
                                    if (info != null)
                                    {
                                        ContentInfo file = ClientFileService.GetDocumentContent(info.Id, DataSearchType.ALL);
                                        if (file != null)
                                        {
                                            string filePath = DataCacheUtility.SaveFileContentToDisk(file);
                                            AttachmentContent detailan = new AttachmentContent();
                                            detailan.Content = info.Content;
                                            detailan.DisplayName = info.Name;
                                            detailan.Name = info.Name;
                                            detailan.ClientPath = filePath;
                                            atts.Add(detailan);
                                        }
                                    }
                                    else
                                    {
                                        returnmess += "The shipment [" + billlist[0].BLNo + "] has no Arrival Notice in Document List" + Environment.NewLine;
                                    }
                                }
                            }
                            //FastReport.Report report1 = new FastReport.Report();
                            //FastReport.Preview.PreviewControl previewControl1 = new FastReport.Preview.PreviewControl();
                            //report1.Preview = previewControl1;
                            //report1.Load("");
                            //report1.RegisterData(null, "ReportSource");
                            //report1.Save("E:\\1.pdf");
                        }
                    }
                }

                StringBuilder body = new StringBuilder();
                StringBuilder subject = new StringBuilder();
                subject.Append("Payment notice");
                //内容
                body.Append("<html>");
                body.Append("<head>");
                body.Append("<meta http-equiv=" + "Content-Type" + "content=" + "text/html;" + "charset=utf-8" + "/>");
                body.Append(" <style type=" + "text/css" + ">");
                body.Append(" .MsoNormal");
                body.Append("{" + " margin-bottom: .0001pt;" + " font-size: 11.0pt;" + " font-family:" + "Calibri," + "sans-serif" + ";");
                body.Append(" margin-left: 0cm;  margin-right: 0cm; margin-top: 0cm;}");
                body.Append(" </style>");
                body.Append("</head>");
                body.Append("<body><div class=" + "MsoNormal" + ">");
                body.Append("Dear All,<br/><br />");
                body.Append("Payment notice is sent with Past<br/>");
                body.Append("<br/>");
                body.Append("<br/>");
                string[] messages = returnmess.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (string mestr in messages)
                {
                    body.Append(mestr + "<br/>");
                }
                body.Append("</div>");
                body.Append("</body>");
                body.Append("</html>");

                string messto = string.Empty;
                string cc = string.Empty;

                XmlDocument xmlmail = new XmlDocument();
                xmlmail.LoadXml(customerPreference.NotifyMail);
                XmlNode root = xmlmail.FirstChild;
                List<PaymentMail> mailist = new List<PaymentMail>();
                foreach (XmlNode mail in root.ChildNodes)
                {
                    PaymentMail pm = new PaymentMail();
                    pm.Type = mail.Name;
                    pm.Mail = mail.InnerText;
                    pm.Name = mail.Attributes["Alias"].Value;
                    mailist.Add(pm);
                }
                messto = string.Join(";", (from p in mailist where p.Type == "To" select p.Mail).ToArray());
                cc = string.Join(";", (from p in mailist where p.Type == "CC" select p.Mail).ToArray());

                var message = CreateMessageInfo(MessageType.Email,
                               MessageWay.Send, messto, LocalData.UserInfo.EmailAddress,
                                                       FormType.Booking, OperationType.OceanImport,
                                                       customerPreference.ID, Guid.Empty,
                                                       body.ToString(), subject.ToString(),
                                                       cc, null, null);
                message.BodyFormat = BodyFormat.olFormatHTML;
                message.State = MessageState.Success;
                message.Attachments = atts;

                MailCenterTemplateService.SendMailWithTemplate(message, false, string.Empty, null);

                //if (!string.IsNullOrEmpty(returnmess))
                //{
                //    DevExpress.XtraEditors.XtraMessageBox.Show(returnmess,
                //                                                LocalData.IsEnglish ? "Tip" : "提示",
                //                                                MessageBoxButtons.OK,
                //                                                MessageBoxIcon.Warning);
                //}
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
            finally
            {
                if (theradID != 0)
                {
                    LoadingServce.CloseLoadingForm(theradID);
                }
            }
        }
        #endregion

        #region Mark Aging Status
        /// <summary>
        /// Mark Aging Status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(AccControlCommandConstants.Command_AccControlMarkStatus)]
        public void Command_AccControlMarkStatus(object sender, EventArgs e)
        {

        }
        #endregion

        #region 客户偏好设置
        /// <summary>
        /// 客户偏好设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(AccControlCommandConstants.Command_AccControlCustomerPreference)]
        public void Command_AccControlCustomerPreference(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "Please select the record first!" : "请先查询记录！",
                                                                           LocalData.IsEnglish ? "Tip" : "提示",
                                                                           MessageBoxButtons.OK,
                                                                           MessageBoxIcon.Warning);
                return;
            }

            SetCustomerPreferences voucherPart = new SetCustomerPreferences();
            voucherPart.SetCompanyAndCustomer(CurrentRow.CompanyID, CurrentRow.CompanyName, CurrentRow.CustomerID, CurrentRow.CustomerName);
            string title = LocalData.IsEnglish ? "Customer Preferences Set" : "客户账单偏好设置";
            PartLoader.ShowDialog(voucherPart, title);
        }
        #endregion

        private void gvMain_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (gvMain.FocusedRowHandle < 0) return;

            CurrentChanged(null, gvMain.GetRow(gvMain.FocusedRowHandle));
        }

        /// <summary>
        /// 返回消息实体类
        /// </summary>
        /// <param name="type">发送类型</param>
        /// <param name="way">发送方向</param>
        /// <param name="sendTo">接收人邮箱</param>
        /// <param name="sendFrom">发送人邮箱</param>
        /// <param name="formType">表单类型</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="operationId">操作ID</param>
        /// <param name="formId">表单ID</param>
        /// <param name="body">发送内容</param>
        /// <param name="subject">主题</param>
        /// <param name="cc">邮件抄送地址</param>
        /// <param name="action">操作动作</param>
        /// <param name="eventObjects">事件列表实体</param>
        /// <returns></returns>
        public Message.ServiceInterface.Message CreateMessageInfo(MessageType type,
           MessageWay way, string sendTo, string sendFrom, FormType formType,
           OperationType operationType, Guid operationId, Guid formId, string body, string subject, string cc,
           string action, EventObjects eventObjects)
        {
            // 邮件发送的消息实体
            var message = new Message.ServiceInterface.Message();

            message.Type = type;
            message.Way = way;
            //收件人邮箱地址
            message.SendTo = sendTo;
            //发件人邮箱地址
            message.SendFrom = sendFrom;
            //邮件抄送地址
            message.CC = cc;

            message.UserProperties = new MessageUserPropertiesObject
            {
                FormType = formType,
                OperationType = operationType,
                OperationId = operationId,
                FormId = formId
            };
            if (!string.IsNullOrEmpty(action))
            {
                //MessageUserPropertiesObject(消息自定义属性类)

                message.UserProperties.Action = action;
            }
            if (!string.IsNullOrEmpty(body) && !string.IsNullOrEmpty(subject))
            {
                message.Body = body;
                message.Subject = subject;
            }
            message.UserProperties.EventInfo = eventObjects;
            return message;

        }

        /// <summary>
        /// 分币种汇总
        /// </summary>
        private List<LocalStatementTotalReportData> BulidTotalList(List<LocalStatementReportData> data)
        {
            List<LocalStatementTotalReportData> totallist = new List<LocalStatementTotalReportData>();
            foreach (LocalStatementReportData row in data)
            {
                if (row.Balance != 0)
                {
                    LocalStatementTotalReportData total = new LocalStatementTotalReportData();
                    double day = (DateTime.Now.Date - row.InvoiceDate.Date).TotalDays;
                    total.CurrencyName = row.Currency;
                    total.Less30 = 0m;
                    total.Over30 = 0m;
                    total.Over45 = 0m;
                    total.Over60 = 0m;
                    total.Over90 = 0m;
                    if (day <= 30)
                    {
                        total.Less30 = row.Balance;
                    }
                    else if (day >= 31 && day <= 45)
                    {
                        total.Over30 = row.Balance;
                    }
                    else if (day >= 46 && day <= 60)
                    {
                        total.Over45 = row.Balance;
                    }
                    else if (day >= 61 && day <= 90)
                    {
                        total.Over60 = row.Balance;
                    }
                    else if (day > 90)
                    {
                        total.Over90 = row.Balance;
                    }
                    totallist.Add(total);
                }
            }

            return totallist;

        }

        /// <summary>
        /// 用于LocalStatementReport后面汇总
        /// </summary>
        [Serializable]
        public class LocalStatementTotalReportData
        {
            public string CurrencyName { get; set; }

            public decimal Less30 { get; set; }

            public decimal Over60 { get; set; }

            public decimal Over30 { get; set; }

            public decimal Over45 { get; set; }

            public decimal Over90 { get; set; }
        }
    }
}
