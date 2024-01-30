using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using System.Windows.Forms;
using ICP.Common.ServiceInterface.Client;
using ICP.FAM.ServiceInterface;
using ICP.Common.ServiceInterface;
using System.Collections;
using ISearchPart = ICP.Framework.ClientComponents.UIFramework.ISearchPart;

namespace ICP.FAM.UI
{
    class LedgerListWorkItem : WorkItem
    {
        protected override void OnRunStarted()
        {
            base.OnRunStarted();

            Show();
        }

        private void Show()
        {
            LedgerListMainWorkSpace mainSpace = SmartParts.Get<LedgerListMainWorkSpace>("LedgerListMainWorkSpace");
            if (mainSpace == null)
            {
                mainSpace = SmartParts.AddNew<LedgerListMainWorkSpace>("LedgerListMainWorkSpace");

                #region AddPart

                LedgerListToolPart toolBar = SmartParts.AddNew<LedgerListToolPart>();
                IWorkspace toolBarWorkspace = (IWorkspace)Workspaces[LedgerListWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                LedgerListPart listPart = SmartParts.AddNew<LedgerListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[LedgerListWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                LedgerListSearchPart searchPart = SmartParts.AddNew<LedgerListSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)Workspaces[LedgerListWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);

                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Ledger List" : "凭证列表";
                mainWorkspace.Show(mainSpace, smartPartInfo);

                LedgerListUIAdapter bookingAdapter = new LedgerListUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);

                bookingAdapter.Init(dic);
            }
            else
                Workspaces[ClientConstants.MainWorkspace].Activate(mainSpace);
        }
    }

    /// <summary>
    /// 命令常量
    /// </summary>
    public class LedgerListCommandConstants
    {
        public const string Command_Add = "Command_Add";
        public const string Command_Edit = "Command_Edit";
        public const string Command_Cancel = "Command_Cancel";
        public const string Command_Cashier = "Command_Cashier";
        public const string Command_FM = "Command_FM";
        public const string Command_Aduitor = "Command_Aduitor";
        public const string Command_KeepAccounts = "Command_KeepAccounts";
        public const string Command_Print = "Command_Print";
        public const string Command_BulkPrint = "Command_BulkPrint";
        public const string Command_BillVoucher = "Command_BillVoucher";
        public const string Command_Reorganize = "Command_Reorganize";
        public const string CommandRefreshData = "Command_RefreshData";
        public const string CommandShowSearch = "Command_ShowSearch";
        public const string Command_AdjustRate = "Command_AdjustRate";
    }

    /// <summary>
    /// WorkSpace常量
    /// </summary>
    public class LedgerListWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";

    }

    public class LedgerListUIAdapter:IDisposable
    {
        #region Service

        [ServiceDependency]
        public WorkItem WorkItem { get; set; }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }


        #endregion

        #region Parts

        IToolBar toolBar;
        ISearchPart searchPart;
        LedgerListPart mainListPart;

        #endregion

        #region Interface

        public void Init(Dictionary<string, object> controls)
        {
            toolBar = (IToolBar)controls[typeof(LedgerListToolPart).Name];
            searchPart = (ISearchPart)controls[typeof(LedgerListSearchPart).Name];
            mainListPart = (LedgerListPart)controls[typeof(LedgerListPart).Name];
            RefreshBarEnabled(toolBar, null);
            #region Connection

            #region mainListPart.CurrentChanged

            mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                if (sender is GridView)
                {
                    int selectionRow = (int)data;
                    if (selectionRow > 0)
                        toolBar.SetEnable("barBulkPrint", true);
                    else
                        toolBar.SetEnable("barBulkPrint", false);
                }
                else
                {
                    LedgerListInfo listData = data as LedgerListInfo;
                    RefreshBarEnabled(toolBar, listData);
                }
            };

            mainListPart.KeyDown += new KeyEventHandler(mainListPart_KeyDown);

            #endregion

            #region MyRegion

            searchPart.OnSearched += delegate(object sender, object results)
            {
                mainListPart.DataSource = results;
            };

            #endregion

            #endregion
        }

        void mainListPart_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender != null)
            {
                Dictionary<string, object> keyValue = sender as Dictionary<string, object>;
                if (keyValue != null)
                {
                    searchPart.Init(keyValue);
                    searchPart.RaiseSearched();
                }
            }
        }

        private void DisableToolBar()
        {
            toolBar.SetEnable("barDelete", false);
            toolBar.SetEnable("btnCashierChecked", false);
            toolBar.SetEnable("btnCashierUnChecked", false);
            toolBar.SetEnable("btnFMChecked", false);
            toolBar.SetEnable("btnFMUnChecked", false);
            toolBar.SetEnable("btnAduited", false);
            toolBar.SetEnable("btnUnAduited", false);
            toolBar.SetEnable("btnCancelAccounts", false);
        }

        private void RefreshBarEnabled(IToolBar toolBar, LedgerListInfo listData)
        {

            DisableToolBar();
            if (listData != null && listData.IsNew == false)
            {
                switch (listData.Status)
                {
                    case LedgerMasterStatus.Unknown:
                    case LedgerMasterStatus.CreateBy:
                        toolBar.SetEnable("barEdit", true);
                        toolBar.SetText("barEdit", "编辑(&E)");
                        toolBar.SetEnable("barDelete", true);
                        toolBar.SetEnable("btnCashierChecked", true);
                        break;
                    case LedgerMasterStatus.CashierChecked:
                        toolBar.SetEnable("btnCashierUnChecked", true);
                        toolBar.SetEnable("btnFMChecked", true);
                        toolBar.SetEnable("btnAduited", true);
                        break;
                    case LedgerMasterStatus.FinanceManagerChecked:
                        toolBar.SetEnable("btnFMUnChecked", true);
                        toolBar.SetEnable("btnAduited", true);
                        break;
                    case LedgerMasterStatus.Auditor:;
                        toolBar.SetEnable("btnUnAduited", true);
                        break;
                    case LedgerMasterStatus.CloseAccounts:
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            mainListPart.KeyDown -= mainListPart_KeyDown;
            mainListPart = null;
            searchPart = null;
            toolBar = null;
            
        }

        #endregion
    }

    /// <summary>
    /// 凭证打印
    /// </summary>
    public class LedgerPrint
    {
        public static void Print(WorkItem workItem, IFinanceService financeService, IReportViewService ReportViewService, Guid id)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                //凭证数据源
                PrintLedgerMasterReports hdObj = new PrintLedgerMasterReports();

                #region  获得数据

                hdObj = financeService.GetPrintLedgerReportDate(id, LocalData.IsEnglish);
                //大写金额
                decimal amount = 0;
                int i = 0;
                foreach (Ledgers dtl in hdObj.DetailList)
                {
                    amount += dtl.DRAmt;
                    //dtl.Page = i / 8 + (i % 8 > 0 ? 1 : 0);
                }
                //hdObj.TotalPages = hdObj.DetailList.Count / 8 + (hdObj.DetailList.Count % 8 > 0 ? 1 : 0); //8行记录占一页
                hdObj.FiguresAmount = FAMUtility.MoneyToString(amount);

                #endregion

                if (hdObj == null) return;
                IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Credentials Print" : "打印凭证",
                    (IWorkspace)workItem.Workspaces[ClientConstants.MainWorkspace]);

                string fileName = Application.StartupPath + "\\Reports\\FAM\\RptCredentialsList_CN.frx";
                Dictionary<string, object> reportSource = new Dictionary<string, object>();

                reportSource.Add("HdReportSource", hdObj);
                reportSource.Add("DtlReportSource", hdObj.DetailList);
                viewer.BindData(fileName, reportSource, null);
            }
        }

        public static void Print(WorkItem workItem, IReportViewService ReportViewService, List<PrintLedgerMasterReports> hdList)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                ////凭证数据源
                //List<PrintLedgerMasterReports> hdList = new List<PrintLedgerMasterReports>();
                #region  获得数据

                //hdList = financeService.GetBulkPrintLedgerReportDate(ids.ToArray(), LocalData.IsEnglish);

                //bool isCostFee = true;  //报销生成凭证
                //if (hdList.Exists(o => o.Type == LedgerMasterType.Account
                //    || o.Type == LedgerMasterType.Billing
                //    || o.Type == LedgerMasterType.Unknown))
                //    isCostFee = false;

                int i = 1;
                Hashtable ht = new Hashtable();
                //hdList的排序方式必须和报表模板RptBulkLedger_CN的排序方式一致，才能保证页码hdObj.Page正确
                hdList = hdList.OrderBy(o => o.No).OrderBy(o=>o.Remark).ThenByDescending(o => Math.Abs(o.DRAmt)).ThenByDescending(o => Math.Abs(o.CRAmt)).ToList();
                foreach (PrintLedgerMasterReports hdObj in hdList)
                {
                    var query = from hd in hdList
                                group hd by hd.No into l
                                select new { l.Key, CRAmt = l.Sum(o => o.CRAmt), DRAmt = l.Sum(o => o.DRAmt), Count = l.Count() };
                    var obj = query.Where(o => o.Key == hdObj.No).FirstOrDefault();
                    hdObj.FiguresAmount = FAMUtility.MoneyToString(obj.DRAmt);
                    hdObj.TotalCRAmt = obj.CRAmt;
                    hdObj.TotalDRAmt = obj.DRAmt;

                    if (ht[hdObj.No] == null)
                        ht.Add(hdObj.No, 1);
                    else
                        ht[hdObj.No] = Convert.ToInt32(ht[hdObj.No]) + 1;
                    i = Convert.ToInt32(ht[hdObj.No]);
                    hdObj.TotalPages = obj.Count / 6 + (obj.Count % 6 > 0 ? 1 : 0); //6行记录占一页
                    hdObj.Page = (i / 6) + (i % 6 > 0 ? 1 : 0);
                    //System.Diagnostics.Trace.WriteLine(string.Format("Row:{0} Desc:{1} Page:{2} DR:{3} CR:{4}",i,hdObj.Remark,hdObj.Page,hdObj.DRAmt,hdObj.CRAmt));
                }

                #endregion
                //System.Data.DataSet ds = ICP.FAM.UI.Comm.IListDataSet.ToDataSet<PrintLedgerMasterReports>(hdList);
                if (hdList == null || hdList.Count == 0) return;
                IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Credentials Print" : "打印凭证",
                    (IWorkspace)workItem.Workspaces[ClientConstants.MainWorkspace]);

                string fileName = Application.StartupPath + "\\Reports\\FAM\\RptBulkLedger_CN.frx";
                Dictionary<string, object> reportSource = new Dictionary<string, object>();
                reportSource.Add("HdReportSource", hdList);
                viewer.BindData(fileName, reportSource, null);
                
            }
        }
    }
}
