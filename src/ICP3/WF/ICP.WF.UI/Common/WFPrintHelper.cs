using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.Client;
using ICP.FAM.ServiceInterface.DataObjects.Report;
using ICP.WF.ServiceInterface;
using ICP.FAM.ServiceInterface;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.WF.UI.Common
{
    /// <summary>
    /// WF报表打印服务类
    /// </summary>
    public class WFPrintHelper
    {
        #region Services
        public WorkItem Workitem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();

            }
        }

        public IReportViewService ReportViewService
        {
            get { return ServiceClient.GetClientService<IReportViewService>(); }
        }
        #endregion

        #region 打印应付支票信息
        /// <summary>
        /// 打印应付支票信息
        /// </summary>
        public void PrintCheckAP(Guid workflowId)
        {
            string fileName = string.Empty;
            string titleString = string.Empty;
            CashReportData reportData = new CashReportData();
            Dictionary<string, object> reportSource = new Dictionary<string, object>();
            try
            {
                reportData = ServiceClient.GetService<IWorkFlowDataService>().GetCheckReportData(workflowId);
                if (reportData == null)
                {
                    return;
                }
                reportData.BaseReportData.Total = reportData.BaseReportData.Amount;
                //reportData.BaseReportData.Total = ReportHelper.GetText(Math.Abs(Convert.ToDecimal(reportData.BaseReportData.Amount)));
                fileName = System.Windows.Forms.Application.StartupPath + "\\Reports\\FAM\\";
                fileName += "RPT_Check_PacgranAP.frx";
                reportSource.Add("BillListReportData", reportData.BillList);
                reportSource.Add("BaseReportData", reportData.BaseReportData);
                IReportViewer viewer = ReportViewService.ShowReportViewer(titleString, (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);
                viewer.BindData(fileName, reportSource, null);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex.Message);
                return;
            }
        }
        #endregion
    }
}
