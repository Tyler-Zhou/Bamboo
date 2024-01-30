using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors;
using ICP.Common.ServiceInterface.Client;
using ICP.FAM.ServiceInterface;
using ICP.FAM.UI.BatchBill;
using ICP.FAM.UI.CustomerBill.Print;
using ICP.FAM.UI.TelexApply;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.UI.CustomerBill;
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FAM.UI.WriteOff;
using ICP.FCM.Common.ServiceInterface;
using System.Windows.Forms;
using System.Diagnostics;
using ICP.DataCache.ServiceInterface;
using System.Reflection;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.UI.Bill;

namespace ICP.FAM.UI
{
    public class FinanceClientService : IFinanceClientService
    {

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IOperationAgentService
             OperationAgentService
        {
            get { return ServiceClient.GetService<IOperationAgentService>(); }
        }

        /// <summary>
        /// 
        /// </summary>
        IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        /// <summary>
        /// 账单报表服务
        /// </summary>
        IFinanceReportService FinanceReportService
        {
            get
            {
                return ServiceClient.GetService<IFinanceReportService>();
            }
        }

        /// <summary>
        /// 报表服务
        /// </summary>
        public IReportViewService ReportViewService
        {
            get { return ServiceClient.GetClientService<IReportViewService>(); }
        }

        #region ShowBillList

        /// <summary>
        /// 显示帐单列表
        /// </summary>
        /// <param name="blCommonInfo">BillCommonInfo</param>
        /// <param name="workspaceName">workspaceName,传入空值以一个对话框形式打开
        public void ShowBillList(OperationCommonInfo operationCommonInfo, string workspaceName)
        {
            //没有参考号无法为维护帐单
            if (operationCommonInfo == null || operationCommonInfo.Forms == null || operationCommonInfo.Forms.Count == 0)
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "TO DO" : "此业务没有参考单号.无法维护帐单.");
                return;
            }
            if (OperationAgentService.GetIsAcceptDispatch(operationCommonInfo.OperationID))
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "You could not open the page Account Info until the agent dispatch docs." : "在代理分发文件之前，您不能打开账单页面。");
                return;
            }

            //if (operationCommonInfo.OperationType == Framework.CommonLibrary.Common.OperationType.OceanImport)
            //{
            //    try
            //    {
            //        DataSet dst = OperationAgentService.CompareOceanBookingCheckSum4LACo(operationCommonInfo.OperationID, false);
            //        if (dst != null)
            //        {
            //            string strerr = LocalData.IsEnglish ? "Accepting Docs is failed due to a sync error occourred. Please contact IT dept." : "数据没有同步无法签收，请联系管理员！";
            //            ICP.Framework.ClientComponents.Controls.Utility.ShowMessage(strerr);
            //            return;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        ICP.Framework.ClientComponents.Controls.Utility.ShowMessage(LocalData.IsEnglish ? "Network problems, please try again later." : "网络问题，请稍后再试！");
            //        return;
            //    }
            //}
            int state = 0;
            if (operationCommonInfo.OperationType == OperationType.OceanExport)
            {
                bool flag = false;
                List<SimpleBusinnessInfo> result = FCMCommonService.GetOEIDByOIID(operationCommonInfo.OperationID);
                if (result != null && result.Count > 0)
                {
                    for (int i = 0; i < result.Count; i++)
                    {
                        state = OperationAgentService.UspGetDispatchLogState(result[i].OIBusinessID);
                        if (state != 0 && state != 1 && state != 6)
                        {
                            flag = true;
                        }
                    }
                }

                if (flag)
                {
                    DialogResult diaResult = XtraMessageBox.Show(LocalData.IsEnglish ?
                        "D/C Fees have been revised by the agent, you must accept the revised fees first. \n\t Clicks Yes to enter the page [Accept Revised D/C Fees From The Agent] \n\t Clicks No to continue to enter the page [Account Info]."
                        : "代理已经修订了代理账单费用，您必须先签收此次修订。\n\t 单击[Yes]进入签收修订页面。\n\t 单击[NO]继续打开账单页面。"
                        , "", MessageBoxButtons.YesNo);

                    if (diaResult == DialogResult.Yes)
                    {
                        FCM.Common.UI.FCMUIUtility.ShowReviseAccepteNew(Workitem, operationCommonInfo.OperationID);
                        return;
                    }
                }
            }
            else if (operationCommonInfo.OperationType == OperationType.OceanImport)
            {
                List<SimpleBusinnessInfo> result = FCMCommonService.GetOEIDByOIID(operationCommonInfo.OperationID);
                if (result != null && result.Count> 0)
                {
                    state = OperationAgentService.UspGetDispatchLogState(result[0].OEBusinessID);
                }
                if (state != 0 && state != 1 && state != 6)
                {
                    DialogResult diaResult = XtraMessageBox.Show(LocalData.IsEnglish ? "The agent have re-dispatched Docs, you must accept the dispatching first. \t\n Clicks Yes to enter the page [Accept Dispatched Docs From The Agent] \t\n Clicks No to continue to enter the page [Account Info]. "
                    : "代理已经重新分发了文档，您必须先签收此次分发。\t\n 单击[Yes]进入签收代理分发文档页面。\t\n 单击[NO]继续打开账单页面。",
                    "", MessageBoxButtons.YesNo);
                    if (diaResult == DialogResult.Yes)
                    {
                        FCM.Common.UI.FCMUIUtility.ShowAcceptedDocumentCompareNew(Workitem, Guid.Empty, operationCommonInfo.OperationID, false);
                        return;
                    }
                }
            }
            else if (operationCommonInfo.OperationType == OperationType.AirExport)
            {
                bool flag = false;
                List<SimpleBusinnessInfo> result = FCMCommonService.GetOEIDByOIID(operationCommonInfo.OperationID);
                if (result != null && result.Count > 0)
                {
                    for (int i = 0; i < result.Count; i++)
                    {
                        state = OperationAgentService.UspGetDispatchLogState(result[i].OIBusinessID);
                        if (state != 0 && state != 1 && state != 6)
                        {
                            flag = true;
                        }
                    }
                }

                if (flag)
                {
                    DialogResult diaResult = XtraMessageBox.Show(LocalData.IsEnglish ?
                        "D/C Fees have been revised by the agent, you must accept the revised fees first. \n\t Clicks Yes to enter the page [Accept Revised D/C Fees From The Agent] \n\t Clicks No to continue to enter the page [Account Info]."
                        : "代理已经修订了代理账单费用，您必须先签收此次修订。\n\t 单击[Yes]进入签收修订页面。\n\t 单击[NO]继续打开账单页面。"
                        , "", MessageBoxButtons.YesNo);

                    if (diaResult == DialogResult.Yes)
                    {
                        FCM.Common.UI.FCMUIUtility.ShowReviseAccepteNew(Workitem, operationCommonInfo.OperationID);
                        return;
                    }
                }
            }
            else if (operationCommonInfo.OperationType == OperationType.AirImport)
            {
                List<SimpleBusinnessInfo> result = FCMCommonService.GetOEIDByOIID(operationCommonInfo.OperationID);
                if (result != null && result.Count > 0)
                {
                    state = OperationAgentService.UspGetDispatchLogState(result[0].OEBusinessID);
                }
                if (state != 0 && state != 1 && state != 6)
                {
                    DialogResult diaResult = XtraMessageBox.Show(LocalData.IsEnglish ? "The agent have re-dispatched Docs, you must accept the dispatching first. \t\n Clicks Yes to enter the page [Accept Dispatched Docs From The Agent] \t\n Clicks No to continue to enter the page [Account Info]. "
                    : "代理已经重新分发了文档，您必须先签收此次分发。\t\n 单击[Yes]进入签收代理分发文档页面。\t\n 单击[NO]继续打开账单页面。",
                    "", MessageBoxButtons.YesNo);
                    if (diaResult == DialogResult.Yes)
                    {
                        FCM.Common.UI.FCMUIUtility.ShowAcceptedDocumentCompareNew(Workitem, Guid.Empty, operationCommonInfo.OperationID, false);
                        return;
                    }
                }
            }

            

            Stopwatch stopwatch = StopwatchHelper.StartStopwatch();
            CustomerBillWorkitem tempWorkitem;

            tempWorkitem = Workitem.WorkItems.AddNew<CustomerBillWorkitem>("CustomerBillList" + Guid.NewGuid().ToString());

            tempWorkitem.Run();
            tempWorkitem.Show(operationCommonInfo);

            MethodBase method = MethodBase.GetCurrentMethod();
            StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName, "VIEW-BILL", string.Format("显示帐单列表;OperationID[{0}]", operationCommonInfo.OperationID));
        }

        /// <summary>
        /// 显示财务账单列表
        /// </summary>
        /// <param name="criteria"></param>
        public void ShowBillList(BillListQueryCriteria criteria)
        {
            BillWorkitem billlistWork = Workitem.WorkItems.AddNew<BillWorkitem>();
            billlistWork.Show(criteria);
        }

        #endregion

        #region ShowWriteOffEditor

        public void ShowWriteOffEditor(string title, Dictionary<string, object> dicList, string workspaceName)
        {
            PartLoader.ShowEditPart<WriteOffEditPart>(Workitem, null, dicList, title, null, string.Empty);

        }

        #endregion

        #region ShowSingleBusinessTelexApply

        public void ShowSingleBusinessTelexApply(Guid businessId, string refNo)
        {
            TelexApplyEditPart editor = new TelexApplyEditPart();

            PartLoader.ShowDialog(editor, "电放申请单");
        }

        #endregion

        #region 显示凭证信息
        /// <summary>
        /// 显示凭证信息
        /// </summary>
        /// <param name="masterID"></param>
        public void ShowLedgerInfo(Guid masterID)
        {
            ReportViewLedgerInfo editor = new ReportViewLedgerInfo();
            editor.ID = masterID;
            editor.ShowDialog();
        }

        #endregion

        #region 显示管理费用月预算费用
        public void ShowFeeYearMonthBudgetPart()
        {
            FeeYearMonthBudgetPart part = new FeeYearMonthBudgetPart();
            part.ShowDialog();
        }
        #endregion

        #region 批量新增账单

        /// <summary>
        /// 批量新增账单
        /// </summary>
        /// <param name="editPartSaved">编辑完成后事件</param>
        public void BatchAddBill(PartDelegate.EditPartSaved editPartSaved)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                var title = LocalData.IsEnglish ? "Batch CustomerBill Edit" : "批量编辑帐单";
                PartLoader.ShowEditPart<BatchCustomerBillEditPart>(Workitem, null, EditMode.New, null, title, editPartSaved, title);
            }
        }
        #endregion

        #region 打印批量账单
        /// <summary>
        /// 打印批量账单
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="billIDs">账单列表</param>
        /// <param name="userID">打印人</param>
        public void PrintBatchBill(Guid customerID, Guid companyID, Guid[] billIDs, Guid userID)
        {
            string title = LocalData.IsEnglish ? "Print Batch Bill" : "打印批量账单";
            BatchBillReportData reportData = FinanceReportService.GetBatchBillReportData(customerID, companyID,billIDs.ToArray(), userID);
            Dictionary<string, FeeTotalInfo> feeTotals = new Dictionary<string, FeeTotalInfo>();
            foreach (var item in reportData.ManifestList)
            {
                if (feeTotals.ContainsKey(item.CurrencyName) == false) feeTotals.Add(item.CurrencyName, new FeeTotalInfo()); 
                if (item.ChargeWay == FeeWay.AR)
                {
                    feeTotals[item.CurrencyName].Debit += item.ChargeAmount;
                }
                else
                {
                    feeTotals[item.CurrencyName].Credit += item.ChargeAmount;
                    item.ChargeAmount = 0m - item.ChargeAmount;
                }
            }
            foreach (var item in feeTotals)
            {
                reportData.TotalAmount += item.Key + ":" + (item.Value.Debit - item.Value.Credit).ToString("N") + " ";
            }
            Dictionary<string, object> reportSource = new Dictionary<string, object>
                {
                    {"ReportSource", reportData},
                    {"ManifestListSource", reportData.ManifestList},
                };
            string fileName = Application.StartupPath + "\\Reports\\FAM\\";
            fileName += "BatchBillReport.frx";
            IReportViewer viewer = ReportViewService.ShowReportViewer(title, Workitem.Workspaces[ClientConstants.MainWorkspace]);
            viewer.BindData(fileName, reportSource, null, null);
        } 
        #endregion
    }
}
