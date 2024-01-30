using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Common.ServiceInterface.Client;
using ICP.FAM.UI.Invoice;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.Service;
using DevExpress.Data;
using DevExpress.XtraGrid.Columns;
using ICP.FAM.ServiceInterface.DataObjects.Report;
using Microsoft.Practices.CompositeUI.SmartParts;
namespace ICP.FAM.UI
{
    [ToolboxItem(false)]
    public partial class InvoiceListPart : BaseListPart
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        public IReportViewService ReportViewService
        {
            get
            {
                return ServiceClient.GetClientService<IReportViewService>();
            }
        }
        public InvoiceReportHelper InvoiceReportHelper
        {
            get
            {
                return ClientHelper.Get<InvoiceReportHelper, InvoiceReportHelper>();
            }
        }
        #endregion

        #region init

        public InvoiceListPart()
        {
            InitializeComponent();
            Disposed += delegate {
                CurrentChanged = null;
                CurrentChanging = null;
                gcMain.DataSource = null;
                bsInvoiceFeeDate.DataSource = null;
                bsInvoiceFeeDate.Dispose();
                bsList.PositionChanged -= bsList_PositionChanged;
                bsList.DataSource = null;
                bsList.Dispose();
                KeyDown = null;
                InvokeGetData = null;
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
            FAMUtility.ShowGridRowNo(gvMain);
            RegisterMessage("1110170001", LocalData.IsEnglish ? "Invoices exceeding100000RMB, do you want to continue printing" : "发票金额超过了100000RMB,是否继续打印?");
        }
        #endregion

        #region 列表事件

        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }
        #endregion

        #region IListPart 成员
        /// <summary>
        /// 当前行
        /// </summary>
        public override object Current
        {
            get
            {
                return bsList.Current;
            }
        }
        /// <summary>
        /// 当前数据列表
        /// </summary>
        public List<InvoiceList> DataSourceList
        {
            get
            {
                return bsList.DataSource as List<InvoiceList>;
            }
        }
        /// <summary>
        /// 选择的行
        /// </summary>
        public List<InvoiceList> SelectList
        {
            get
            {
                int[] rowIndexs = gvMain.GetSelectedRows();
                List<InvoiceList> tagers = new List<InvoiceList>();

                if (rowIndexs.Length == 0)
                {
                    return tagers;
                }              
                foreach (var item in rowIndexs)
                {
                    InvoiceList dr = gvMain.GetRow(item) as InvoiceList;
                    if (dr != null)
                    {
                        tagers.Add(dr);
                    }
                }

                return tagers;
            }
        }
        /// <summary>
        /// 当前行数据
        /// </summary>
        protected InvoiceList CurrentRow
        {
            get
            {
                return bsList.Current as InvoiceList;
            }
        }
        /// <summary>
        /// 数据源
        /// </summary>
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
        /// <summary>
        /// 费用列表(打印用）
        /// </summary>
        public List<InvoiceFeeDate> Details
        {
            get
            {
                if (CurrentRow == null)
                {
                    return null;
                }
                bsInvoiceFeeDate.EndEdit();
                InvoiceInfo invoice = FinanceService.GetInvoiceInfo(CurrentRow.ID, LocalData.IsEnglish);
                List<InvoiceFeeDate> list = invoice.Fees as List<InvoiceFeeDate>;
                if (list == null)
                {
                    list = new List<InvoiceFeeDate>();
                }
                return list;
            }
        }

        /// <summary>
        /// 大连公司ID
        /// </summary>
        Guid _daLianCompanyID = new Guid("b1afad8f-55dd-4e29-a250-eb82ab3971fe");

        /// <summary>
        /// 宁波公司ID
        /// </summary>
        Guid _ningBoCompanyID = new Guid("a62a9f8e-e69c-4e6e-ad85-e75aed3c6cf9");

        Dictionary<Guid, string> _dicCurrency = new Dictionary<Guid, string>();
        #region 数据绑定
        DataPageInfo _dataPageInfo = new DataPageInfo();
        private void BindingData(object value)
        {
            PageList source = value as PageList;
            List<InvoiceList> invoiceList = new List<InvoiceList>();
            if(source !=null) invoiceList=source.GetList<InvoiceList>();
            if (source == null || invoiceList.Count == 0)
            {
                bsList.DataSource = typeof(InvoiceList);
                bsInvoiceFeeDate.DataSource = typeof(InvoiceFeeDate);
                pageControl1.TotalPage = 0;
                pageControl1.CurrentPage = 0;
                gvMain.SortInfo.Clear();
            }
            else
            {
                gvMain.BeginUpdate();

                bsList.DataSource = source.GetList<InvoiceList>();
                bsList.ResetBindings(false);
                _dataPageInfo = source.DataPageInfo;
                if (source.DataPageInfo != null)
                {
                    int pageSize = _dataPageInfo.PageSize;
                    int totalCount = _dataPageInfo.TotalCount;
                    int pageCount = totalCount / pageSize;
                    if (pageCount == 1 && totalCount > pageSize)
                    {
                        pageCount = 2;
                    }
                    if (pageCount == 0 && totalCount > 0)
                    {
                        pageCount = 1;
                    }
                    pageControl1.TotalPage = pageCount;

                    pageControl1.CurrentPage = _dataPageInfo.CurrentPage;
                    ColumnSortOrder sortOrder = _dataPageInfo.SortOrderType == SortOrderType.Asc ? ColumnSortOrder.Ascending : ColumnSortOrder.Descending;
                    GridColumn col = gvMain.Columns.ColumnByFieldName(_dataPageInfo.SortByName);
                    if (col != null)
                    {
                        gvMain.SortInfo.Clear();
                        gvMain.SortInfo.Add(new GridColumnSortInfo(col, sortOrder));
                    }
                }
                gvMain.EndUpdate();
            }
        }
        #endregion
        public override event CurrentChangedHandler CurrentChanged;
        public override event CancelEventHandler CurrentChanging;
        public override event InvokeGetDataHandler InvokeGetData;

        #endregion

        #region 按钮方法
        /// <summary>
        /// 新增
        /// </summary>
        [CommandHandler(InvoiceCommandConstants.Command_InvoiceAdd)]
        public void Command_AddData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                AddData();
            }
        }

        private void AddData()
        {
            InvoiceInfo newData = new InvoiceInfo();
            newData.InvoiceDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            newData.ETD = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            newData.EndEdit();
            PartLoader.ShowEditPart<InvoiceEditPart>(Workitem, null, LocalData.IsEnglish ? "Add Invoice" : "新增发票", EditPartSaved);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        [CommandHandler(InvoiceCommandConstants.Command_InvoiceEdit)]
        public void Command_InvoiceEdit(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow != null)
                {
                    InvoiceInfo info = FinanceService.GetInvoiceInfo(CurrentRow.ID, LocalData.IsEnglish);//传入行ID获取InvoiceInfo对象,非List
                    PartLoader.ShowEditPart<InvoiceEditPart>(Workitem, info, LocalData.IsEnglish ? "Edit Invoice" : "编辑发票", EditPartSaved);
                }
            }
        }

        /// <summary>
        /// 编辑界面保存数据
        /// </summary>
        /// <param name="prams"></param>
        private void EditPartSaved(object[] prams)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (prams == null || prams.Length == 0) return;

                InvoiceList data = prams[0] as InvoiceList;

                List<InvoiceList> source = DataSource as List<InvoiceList>;
                if (source == null || source.Count == 0)
                {
                    bsList.Add(data);
                    bsList.ResetBindings(false);
                }
                else
                {
                    InvoiceList tager = source.Find(delegate(InvoiceList item) { return item.ID == data.ID; });
                    if (tager == null)
                    {
                        bsList.Insert(0, data);
                        bsList.ResetBindings(false);
                    }
                    else
                    {
                        FAMUtility.CopyToValue(data, tager, typeof(InvoiceList));

                        bsList.ResetItem(bsList.IndexOf(tager));
                    }

                }
                if (CurrentChanged != null)
                {
                    CurrentChanged(this, CurrentRow);
                }

            }
        }

        /// <summary>
        /// 作废
        /// </summary>
        [CommandHandler(InvoiceCommandConstants.Command_InvoiceCancel)]
        public void Command_InvoiceCancel(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;
            string message = string.Empty;
            bool isCancel = CurrentRow.IsValid;
            if (isCancel)
                message = LocalData.IsEnglish ? "Srue Cancel Current Invoice?" : "你真的要作废当前发票吗?";
            else
                message = LocalData.IsEnglish ? "Srue Available Current Invoice?" : "你真的要恢复当前发票吗?";
            if (CurrentRow.IsValid)
            {
                if (!FAMUtility.ShowResultMessage(NativeLanguageService.GetText(this, message)))
                {
                    return;
                }
            }
            else
            {
                if (!FAMUtility.ShowResultMessage(NativeLanguageService.GetText(this, message)))
                {
                    return;
                }
            }
            try
            {

                SingleResult result = FinanceService.CancelInvoice(CurrentRow.ID, isCancel, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate, LocalData.IsEnglish);

                InvoiceList currentRow = CurrentRow;
                currentRow.IsValid = !currentRow.IsValid;
                currentRow.ID = result.GetValue<Guid>("ID");
                currentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                bsList.ResetCurrentItem();

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Changed Invoice State Successfully" : "更改发票状态成功");
                if (CurrentChanged != null) CurrentChanged(this, CurrentRow);

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Changed Invoice State Failed" : "更改发票状态失败") + ex.Message);
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        [CommandHandler(InvoiceCommandConstants.Command_InvoiceRefreshData)]
        public void Command_InvoiceRefreshData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                RefreshData();
            }
        }
        /// <summary>
        /// 获得发票号
        /// </summary>
        [CommandHandler(InvoiceCommandConstants.Command_GetInvoiceNo)]
        public void Command_GetInvoiceNo(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (SelectList.Count == 0)
                {
                    return;
                }
                List<string> systemNoList = (from d in SelectList select d.No).ToList();
                
                TasSystemCommon.GetTaxInvoiceNo(FindForm(),FinanceService, systemNoList);

                Timer timer = new Timer();
                timer.Interval = 1000;
                timer.Enabled = true;
                int tcount=0;

                timer.Tick += delegate(object oj, EventArgs ea)
                {
                    tcount++;
                    if (!TasSystemCommon.isSuccess)
                    {
                        return;
                    }
                    if (tcount > 70)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), "获取超时,请稍候重试");
                        timer.Enabled = false;
                        return;
                    }
                    timer.Enabled = false;
                    List<InvoiceInfo> list = TasSystemCommon.List;

                    if (list == null || list.Count == 0)
                    {
                        return;
                    }
                    foreach (InvoiceInfo data in list)
                    {
                        if (DataSourceList == null || DataSourceList.Count == 0)
                        {
                            return;
                        }
                        else
                        {
                            InvoiceList tager = DataSourceList.Find(delegate(InvoiceList item) { return item.ID == data.ID; });
                            if (tager != null)
                            {
                                tager.InvoiceNo = data.InvoiceNo;
                                tager.UpdateDate = data.UpdateDate;

                                bsList.ResetItem(bsList.IndexOf(tager));
                            }
                        }
                    }
                };
                if (CurrentChanged != null)
                {
                    CurrentChanged(this, CurrentRow);
                }
            
            }
        }
        /// <summary>
        /// 设置快递单号
        /// </summary>
        [CommandHandler(InvoiceCommandConstants.Command_Express)]
        public void Command_Express(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null) return;

                SetExpressPart expressPart = Workitem.Items.AddNew<SetExpressPart>();
                expressPart.DataSource = CurrentRow;
                string title = LocalData.IsEnglish ? "Set Express" : "设置快递信息";
                expressPart.Saved += delegate(object[] prams)
                {
                    if (prams != null && prams.Length > 0)
                    {
                        InvoiceList data = prams[0] as InvoiceList;
                        if (data != null)
                        {
                            InvoiceList currentRow = CurrentRow;
                            currentRow.ExpressDate = data.ExpressDate;
                            currentRow.ExpressNo = data.ExpressNo;
                            currentRow.UpdateDate = data.UpdateDate;
                            bsList.ResetCurrentItem();
                        }
                    }
                };

                PartLoader.ShowDialog(expressPart, title);
            }
        }

        /// <summary>
        /// 预览(打印)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(InvoiceCommandConstants.Command_PreviewInvoice)]
        public void Command_PreviewInvoice(object sender, EventArgs e)
        {
            //报表设置
            if (CurrentRow == null) return;
            string[] reportPara = new string[7];
            PrintSet expressPart = Workitem.Items.AddNew<PrintSet>();
            expressPart.DataSource = CurrentRow;
            string title = LocalData.IsEnglish ? "Report Set" : "报表设置";
            expressPart.Saved += delegate(object[] prams)
            {
                if (prams != null && prams.Length > 0)
                {
                    string[] configs = prams[0] as string[];//模态窗口,返回值
                    if (configs != null)
                    {
                        reportPara[0] = configs[0].ToString();//代理文本
                        if (reportPara[0] == "true") { reportPara[0] = "代理"; } else { reportPara[0] = string.Empty; }
                        reportPara[1] = configs[1].ToString();//本位币
                        if (reportPara[1] == "true") { reportPara[1] = "RMB"; } else { reportPara[1] = string.Empty; }
                        reportPara[2] = configs[2].ToString();//公司
                        reportPara[3] = configs[3].ToString();//币种
                        reportPara[4] = configs[4].ToString();//汇率
                        reportPara[5] = configs[5].ToString();//新旧格式
                        reportPara[6] = configs[6].ToString();//报表种类
                    }
                }
            };
            if (DialogResult.OK == PartLoader.ShowDialog(expressPart, title))
            {
                InvoiceInfo invoice = FinanceService.GetInvoiceInfo(CurrentRow.ID, LocalData.IsEnglish);
                invoice.EndEdit();

                Dictionary<string, object> reportSource = InvoiceReportHelper.Print(invoice, Details, reportPara);
                string fileNameNew = Application.StartupPath + "\\Reports\\FAM\\Invoice\\transportation\\New\\InvoiceReportSZ.frx";
                string fileNameOld = Application.StartupPath + "\\Reports\\FAM\\Invoice\\transportation\\Old\\InvoiceReportSZ.frx";
                //string fileNameNew_TD = string.Empty;
                //if (LocalData.UserInfo.DefaultCompanyID == _daLianCompanyID) //大连公司要求的模板
                //{
                //    fileNameNew_TD = System.Windows.Forms.Application.StartupPath + "\\Reports\\FAM\\Invoice\\transportation\\New\\InvoiceReportSZ_TDForDalian.frx";
                //}
                //else
                //{
                string fileNameNew_TD = Application.StartupPath + "\\Reports\\FAM\\Invoice\\transportation\\New\\InvoiceReportSZ_TD.frx";
                //}                 
                string fileNameOld_TD = string.Empty;
                if (LocalData.UserInfo.DefaultCompanyID == _daLianCompanyID) //大连公司要求的模板
                {
                    fileNameOld_TD = Application.StartupPath + "\\Reports\\FAM\\Invoice\\transportation\\Old\\InvoiceReport_TDForDalian.frx";  
                }
                else
                {
                    fileNameOld_TD = Application.StartupPath + "\\Reports\\FAM\\Invoice\\transportation\\Old\\InvoiceReportSZ_TD.frx";            
                }
                
                string fileNameNew_SHIP = Application.StartupPath + "\\Reports\\FAM\\Invoice\\transportation\\New\\InvoiceReportSZ_SHIP.frx";
                string fileNameOld_SHIP = Application.StartupPath + "\\Reports\\FAM\\Invoice\\transportation\\Old\\InvoiceReportSZ_SHIP.frx";
                IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Invoice" : "发票打印", (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);
                if (reportPara[5].ToLower() == "new" && reportPara[6] == "Own") { viewer.BindData(fileNameNew, reportSource, null); }//新格式自有格式
                else if (reportPara[5].ToLower() == "new" && reportPara[6] == "Chromatography") { viewer.BindData(fileNameNew_TD, reportSource, null); }//新格式套打
                else if (reportPara[5].ToLower() == "new" && reportPara[6] == "Ship") { viewer.BindData(fileNameNew_SHIP, reportSource, null); }//新格式船务
                else if (reportPara[5].ToLower() == "old" && reportPara[6] == "Own") { viewer.BindData(fileNameOld, reportSource, null); }//旧格式自有
                else if (reportPara[5].ToLower() == "old" && reportPara[6] == "Chromatography") { viewer.BindData(fileNameOld_TD, reportSource, null); }//旧格式套打
                else if (reportPara[5].ToLower() == "old" && reportPara[6] == "Ship") { viewer.BindData(fileNameOld_SHIP, reportSource, null); }//旧格式船务       
                else { viewer.BindData(fileNameOld, reportSource, null); }
            }
        }
        /// <summary>
        /// 打印Invoice(越南)
        /// </summary>
        [CommandHandler(InvoiceCommandConstants.Command_PrintInvoice)]
        public void Command_PrintInvoice(object sender, EventArgs e)
        {
            string fileName = Application.StartupPath + "\\Reports\\FAM\\Invoice\\InvoiceReport.frx";
            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Invoice" : "Invoice", (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);
            viewer.BindData(fileName, null, null);
        }
        /// <summary>
        /// 打印Vientam
        /// </summary>
        [CommandHandler(InvoiceCommandConstants.Command_PrintVientam)]
        public void Command_PrintVientam(object sender, EventArgs e)
        {
            if (CurrentRow == null || DataSourceList.Count < 1 || Details == null) return;
            InvoiceReportData invoiceReportData = new InvoiceReportData();//发票数据对象
            invoiceReportData.VietnamInvoiceReportData = new VietnamInvoiceReportData();//基本数据
            invoiceReportData.VietnamInvoiceReportFeeData = new List<VietnamInvoiceReportFeeData>();//费用明细列表

            InvoiceInfo invoice = FinanceService.GetInvoiceInfo(CurrentRow.ID, LocalData.IsEnglish);
            bsInvoiceFeeDate.DataSource = invoice.Fees as List<InvoiceFeeDate>;
            /*******越南发票费用信息*************/
            invoiceReportData.VietnamInvoiceReportFeeData = new List<VietnamInvoiceReportFeeData>();
            decimal GrandTotal = 0;
            foreach (var detailItem in Details)
            {
                VietnamInvoiceReportFeeData feeData = new VietnamInvoiceReportFeeData();

                if (_dicCurrency.Keys.Contains(detailItem.CurrencyID))
                {
                    feeData.Currency = _dicCurrency[detailItem.CurrencyID];
                }
                if (string.IsNullOrEmpty(detailItem.Amount.ToString())) { detailItem.Amount = Convert.ToDecimal(string.Empty); }
                if (string.IsNullOrEmpty(detailItem.ChargingCode.ToString())) { detailItem.ChargingCode = string.Empty; }
                if (string.IsNullOrEmpty(detailItem.Rate.ToString())) { detailItem.Rate = Convert.ToDecimal(string.Empty); }
                if (string.IsNullOrEmpty(detailItem.Rate.ToString())) { detailItem.Rate = Convert.ToDecimal(string.Empty); }
                if (string.IsNullOrEmpty(detailItem.Quantity.ToString())) { detailItem.Quantity = Convert.ToDecimal(string.Empty); }
                if (string.IsNullOrEmpty(detailItem.Remark.ToString())) { detailItem.Remark = string.Empty; }
                feeData.Rate = detailItem.Rate;
                feeData.Quantity = Convert.ToDecimal(detailItem.Quantity.ToString("0.00"));//数量
                feeData.Remark = detailItem.Remark;
                feeData.Amount = Convert.ToDecimal(detailItem.Amount.ToString("0.00"));//单价=金额
                feeData.FeeItemName = detailItem.ChargingCode;
                feeData.TotalAmount = detailItem.Amount * detailItem.Quantity;//总金额(数量*金额)
                feeData.unitPrice = Convert.ToDecimal(detailItem.Amount.ToString("0.00"));//单价=金额
                invoiceReportData.VietnamInvoiceReportFeeData.Add(feeData);
                GrandTotal += Convert.ToDecimal(feeData.TotalAmount.ToString("0.00"));
            }
            /*******越南发票费用信息*************/

            /*******越南发票基本信息*************/
            if (string.IsNullOrEmpty(CurrentRow.InvoiceDate.ToString())) { CurrentRow.InvoiceDate = Convert.ToDateTime(DBNull.Value); }
            if (string.IsNullOrEmpty(CurrentRow.CreateByName)) { CurrentRow.CreateByName = string.Empty; }
            if (string.IsNullOrEmpty(CurrentRow.CustomerName)) { CurrentRow.CustomerName = string.Empty; }
            if (string.IsNullOrEmpty(CurrentRow.InvoiceNo)) { CurrentRow.InvoiceNo = string.Empty; }
            invoiceReportData.VietnamInvoiceReportData.InvoiceDate = CurrentRow.InvoiceDate.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
            invoiceReportData.VietnamInvoiceReportData.CreateBy = CurrentRow.CreateByName.ToString();
            invoiceReportData.VietnamInvoiceReportData.CustomerName = CurrentRow.CustomerName.ToString();
            invoiceReportData.VietnamInvoiceReportData.No = CurrentRow.InvoiceNo.ToString();
            invoiceReportData.VietnamInvoiceReportData.GrandTotal = GrandTotal.ToString();
            if (string.IsNullOrEmpty(CurrentRow.CustomerName))
            {
                invoiceReportData.VietnamInvoiceReportData.CustomerName = string.Empty;
                invoiceReportData.VietnamInvoiceReportData.CustomerAddress = string.Empty;
                invoiceReportData.VietnamInvoiceReportData.Fax = string.Empty;
                invoiceReportData.VietnamInvoiceReportData.Tel = string.Empty;
                invoiceReportData.VietnamInvoiceReportData.Type = string.Empty;
            }
            else
            {
                CustomerInvoiceInfo customer = FinanceService.GetCustomerInfo(CurrentRow.CustomerName, LocalData.IsEnglish);
                if (string.IsNullOrEmpty(customer.CustomerAddress)) { customer.CustomerAddress = string.Empty; }
                if (string.IsNullOrEmpty(customer.CustomerFax)) { customer.CustomerFax = string.Empty; }
                if (string.IsNullOrEmpty(customer.CustomerTel)) { customer.CustomerTel = string.Empty; }
                if (string.IsNullOrEmpty(customer.CustomerType)) { customer.CustomerType = string.Empty; }
                invoiceReportData.VietnamInvoiceReportData.CustomerAddress = customer.CustomerAddress.ToString();
                invoiceReportData.VietnamInvoiceReportData.Fax = customer.CustomerFax.ToString();
                invoiceReportData.VietnamInvoiceReportData.Tel = customer.CustomerTel.ToString();
                invoiceReportData.VietnamInvoiceReportData.Type = customer.CustomerType.ToString();
            }
            /*******越南发票基本信息*************/
            invoiceReportData.VietnamInvoiceReportData.BankAccountNo = invoice.BankAccountNo;//银行账号
            string fileName = Application.StartupPath + "\\Reports\\FAM\\Invoice\\Vientam.frx";
            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Vientam" : "Vientam", (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);
            Dictionary<string, object> reportSource = new Dictionary<string, object>();
            reportSource.Add("VietnamInvoiceReportData", invoiceReportData.VietnamInvoiceReportData);
            reportSource.Add("VietnamInvoiceReportFeeData", invoiceReportData.VietnamInvoiceReportFeeData);
            viewer.BindData(fileName, reportSource, null);
        }


        /// <summary>
        /// 发票统计
        /// </summary>
        [CommandHandler(InvoiceCommandConstants.Command_InvoiceCount)]
        public void Command_InvoiceCount(object sender, EventArgs e)
        {
            InvoiceCountPart countPart = Workitem.Items.AddNew<InvoiceCountPart>();
            string title = LocalData.IsEnglish ? "Invoice Count" : "发票统计";
            countPart.ReportType = InvoiceReportType.InvoiceCount;
            PartLoader.ShowDialog(countPart, title);
        }
        /// <summary>
        /// 免税收入明细
        /// </summary>
        [CommandHandler(InvoiceCommandConstants.Command_DutyFreeDetail)]
        public void Command_DutyFreeDetail(object sender, EventArgs e)
        {
            InvoiceCountPart countPart = Workitem.Items.AddNew<InvoiceCountPart>();
            string title = LocalData.IsEnglish ? "免税收入明细" : "免税收入明细";
            countPart.ReportType = InvoiceReportType.DutyFreeDetail;
            PartLoader.ShowDialog(countPart, title);
        }
        /// <summary>
        /// 开票统计
        /// </summary>
        [CommandHandler(InvoiceCommandConstants.Command_OperationInvoice)]
        public void Command_OperationInvoice(object sender, EventArgs e)
        {
            InvoiceCountPart countPart = Workitem.Items.AddNew<InvoiceCountPart>();
            string title = LocalData.IsEnglish ? "开票统计" : "开票统计";
            countPart.ReportType = InvoiceReportType.OperationInvoice;
            PartLoader.ShowDialog(countPart, title);
        }
        #endregion

        #region 刷新数据
        public void RefreshData()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    List<InvoiceList> blList = DataSource as List<InvoiceList>;
                    if (blList == null || blList.Count == 0) return;

                    List<Guid> ids = new List<Guid>();
                    foreach (var item in blList) ids.Add(item.ID);

                    PageList list = FinanceService.GetInvoiceListByIds(ids.ToArray(), _dataPageInfo);
                    DataSource = list;
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); }
            }
        }

        #endregion

        #region 列表事件

        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            InvoiceList list = gvMain.GetRow(e.RowHandle) as InvoiceList;
            if (list == null) return;

            if (list.IsValid == false)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Disabled);
            }

        }
        #region 列头排序
        private void gvMain_CustomerSorting(object sender, SortingCancelEventArgs e)
        {
            List<InvoiceList> blList = DataSource as List<InvoiceList>;
            if (blList == null || blList.Count == 0) return;

            if (InvokeGetData != null)
            {
                e.Cancel = true;
                _dataPageInfo.SortByName = e.Column.FieldName;
                if (e.ColumnSortOrder == ColumnSortOrder.Ascending ||
                    e.ColumnSortOrder == ColumnSortOrder.None)
                {
                    _dataPageInfo.SortOrderType = SortOrderType.Desc;
                }
                else
                {
                    _dataPageInfo.SortOrderType = SortOrderType.Asc;
                }
                InvokeGetData(this, _dataPageInfo);
            }

        }
        #endregion
        #region 翻页
        private void pageControl1_PageChanged(object sender, PageChangedEventArgs e)
        {
            List<InvoiceList> blList = DataSource as List<InvoiceList>;
            if (blList == null || blList.Count == 0) return;

            if (InvokeGetData != null)
            {
                _dataPageInfo.CurrentPage = e.CurrentPage;
                InvokeGetData(this, _dataPageInfo);
            }

        }
        #endregion
        #endregion
        
        #region GV Events
        private void gvMain_BeforeLeaveRow(object sender, RowAllowEventArgs e)
        {
            if (CurrentChanging != null)
            {
                CancelEventArgs ce = new CancelEventArgs();
                CurrentChanging(this, ce);
                e.Allow = !ce.Cancel;
            }
        }
        public new event KeyEventHandler KeyDown;
        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CurrentRow != null)
            {
                Workitem.Commands[InvoiceCommandConstants.Command_InvoiceEdit].Execute();
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
                Workitem.Commands[InvoiceCommandConstants.Command_InvoiceShowSearch].Execute();
            }
        }

        private void gvMain_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (CurrentChanged != null)
                {
                    Workitem.Commands[InvoiceCommandConstants.Command_InvoiceEdit].Execute();
                }
            }
        }
        #endregion
    }
}
