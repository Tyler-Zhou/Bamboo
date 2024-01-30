using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.Client;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.FCM.AirExport.ServiceInterface.DataObjects.ReportObjects;
using ICP.FCM.AirExport.ServiceInterface;

namespace ICP.FCM.AirExport.UI.Common
{
    public class AirExportPrintHelper : WorkItem
    {
        #region services

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IReportViewService ReportViewService { get; set; }

        [ServiceDependency]
        public IAirExportService airReportSrvice { get; set; }

        #endregion

        /// <summary>
        /// 获取空运出口报表路径 System.Windows.Forms.Application.StartupPath + "\\Reports\\OceanImport\\
        /// </summary>
        public string GetAEReportPath()
        {
            return System.Windows.Forms.Application.StartupPath + "\\Reports\\AirExport\\";
        }


        public IReportViewer PrintAEOrder(Guid bookID)
        {
            AEOrderReportData data = airReportSrvice.GetAEOrderReportData(bookID);
            if (data == null) return null;

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Order" : "打印操作联系单", (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);

            string fileName = GetAEReportPath();
            if (LocalData.IsEnglish) fileName += "AE_OrderInfo_EN.frx";
            else fileName += "AE_OrderInfo_CN.frx";

            Dictionary<string, object> reportSource = new Dictionary<string, object>();

            reportSource.Add("ReportSource", data);
            reportSource.Add("OrderFee", data.Fees);

            viewer.BindData(fileName, reportSource, null);
            return viewer;
        }
    }
}
