using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.Client;
using ICP.FCM.AirImport.ServiceInterface;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.AirImport.ServiceInterface.DataObjects;

namespace ICP.FCM.AirImport.UI.Common
{
   public class AirImportPrintHelper
    {
        #region Services

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IAIReportDataService OIReportSrvice { get; set; }

        [ServiceDependency]
        public IReportViewService ReportViewService { get; set; }

        #endregion

        /// <summary>
        /// 获取空运进口报表路径 System.Windows.Forms.Application.StartupPath + "\\Reports\\AirImport\\
        /// </summary>
        public string GetOIReportPath()
        {
            return System.Windows.Forms.Application.StartupPath + "\\Reports\\AirImport\\";
        }

        /// <summary>
        /// 打印进口操作联系单
        /// </summary>
        /// <param name="orderID">orderID</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintOIOrder(Guid orderID)
        {
            OIOrderReportData data = OIReportSrvice.GetAIOrderReportData(orderID);                     
            if (data == null) return null;
            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Order" : "打印操作联系单", (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);
            string fileName = GetOIReportPath();
            if (LocalData.IsEnglish) fileName += "AI_OrderInfo_EN.frx";
            else fileName += "AI_OrderInfo_CN.frx";
          
            Dictionary<string, object> reportSource = new Dictionary<string, object>();
            reportSource.Add("ReportSource", data);
            reportSource.Add("OrderFee", data.Fees);

            viewer.BindData(fileName, reportSource, null);
            return viewer;
        }

        /// <summary>
        /// 打印利润表
        /// </summary>
        /// <param name="orderID">orderID</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintProfit(Guid businessID)
        {
            ProfitReportData data = OIReportSrvice.GetProfitReportData(businessID);
            if (data == null) return null;
            data.PrintDate = DateTime.Now.ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            if (data.Fees != null && data.Fees.Count > 0)
            {
                data.TotalRevenue = 0.0M;
                data.TotalCost = 0.0M;
                data.TotalAgent = 0.0M;
                data.Profit = 0.0M;
                foreach (var item in data.Fees)
                {
                    data.TotalRevenue += (item.Revenue != null? item.Revenue: 0.0M);
                    data.TotalCost += (item.Cost != null ? item.Cost : 0.0M);
                    data.TotalAgent += (item.agent != null ? item.agent : 0.0M);
                }

                data.Profit = data.TotalRevenue - data.TotalCost + data.TotalAgent;
                //=Sum(fields!Revenue.Value)- Sum(fields!Cost.Value) + Sum(fields!agent.Value)
            }

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Profit Report" : "打印利润表", (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);
            string fileName = GetOIReportPath();
            fileName += "RptProfit.frx";          

            Dictionary<string, object> reportSource = new Dictionary<string, object>();
            reportSource.Add("Common_ProfitReportData", data);
            reportSource.Add("Common_ProfitReportFeeData", data.Fees);

            viewer.BindData(fileName, reportSource, null);
            return viewer;                   
        }

        /// <summary>
        /// 打印派车国内报表
        /// </summary>
        /// <param name="truckID">truckID</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintPickupCN(Guid truckID)
        {
            PickupCNReportData data = OIReportSrvice.GetPickupCNReportData(truckID);
            if (data == null) return null;
       
            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Pickup" : "打印派车单", (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);
            string fileName = GetOIReportPath() + "AI_PickupDelivery_CN.frx";

            Dictionary<string, object> reportSource = new Dictionary<string, object>();
            reportSource.Add("ReportSource", data);
            viewer.BindData(fileName, reportSource, null);
            return viewer;        
        }

        /// <summary>
        /// 打印业务信息报表
        /// </summary>
        /// <param name="operationID">operationID</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintBusinessInfo(Guid operationID)
        {
            OIBusinessReportData data = OIReportSrvice.GetAIBusinessReportData(operationID);
            if (data == null) return null;

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? " BusinessInfo" : "业务信息"
                                                                    , (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);
            string fileName = GetOIReportPath() + (LocalData.IsEnglish ? "AI_BusinessInfo_EN.frx" : "AI_BusinessInfo_CN.frx");
            Dictionary<string, object> reportSource = new Dictionary<string, object>();
            reportSource.Add("OIBusinessReportData", data);
            reportSource.Add("BLListReportData", data.blListReportDatas);
            viewer.BindData(fileName, reportSource, null);
            return viewer;
        }

        ///// <summary>
        ///// 打印业务信息报表
        ///// </summary>
        ///// <param name="blID">blID</param>
        ///// <returns>返回IReportViewer</returns>
        //public IReportViewer PrintBusinessInfoCopy(Guid blID)
        //{
        //    #region 数据源

        //    OIBLReportData data = OIReportSrvice.GetOIBLReportData(blID);

        //    BLReportClientData blReportData = new BLReportClientData();
        //    Utility.CopyToValue(data, blReportData, typeof(BLReportClientData));

        //    if (string.IsNullOrEmpty(blReportData.MBLNo) == false)
        //    {
        //        blReportData.MBLNo = "MBLNO:" + blReportData.MBLNo;
        //    }
        //    if (blReportData.ETD != null)
        //    {
        //        blReportData.ETDString = blReportData.ETD.Value.ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        //    }

        //    #endregion

        //    IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? " BL Info" : "提单信息"
        //                                                            , (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);

        //    string fileName = GetOIReportPath() + "OI_BL_TR_Report.frx";
        //    Dictionary<string, object> reportSource = new Dictionary<string, object>();
        //    reportSource.Add("ReportSource", blReportData);
        //    viewer.BindData(fileName, reportSource, null);
        //    return viewer;
        //}
    }
}
