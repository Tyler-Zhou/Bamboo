using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Common.ServiceInterface.Client;
using ICP.FCM.DomesticTrade.ServiceInterface;
using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.ReportObjects;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.DomesticTrade.UI
{
    public class DomesticTradePrintHelper
    {
        #region Services

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IDTReportDataService DTReportDataSrvice
        {
            get
            {
                return ServiceClient.GetService<IDTReportDataService>();
            }
        }
        public IReportViewService ReportViewService
        {
            get
            {
                return ServiceClient.GetClientService<IReportViewService>();
            }
        }

        #endregion

        /// <summary>
        /// 获取内贸业务报表路径 System.Windows.Forms.Application.StartupPath + "\\Reports\\OceanExport\\
        /// </summary>
        public string GetOEReportPath()
        {
            return Application.StartupPath + "\\Reports\\Domestic\\";
        }

        /// <summary>
        /// 打印操作联系单
        /// </summary>
        /// <param name="orderID">orderID</param>
        /// <returns>返回IReportViewer</returns>
        public  IReportViewer PrintOEOrder(Guid orderID,Guid companyID)
        {
            DTOrderReportData data = DTReportDataSrvice.GetDTOrderReportData(orderID, companyID);
            if (data == null) return null;

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Order" : "打印操作联系单", (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);

            string fileName = GetOEReportPath();
            if (LocalData.IsEnglish) fileName += "DT_OrderInfo_EN.frx";
            else fileName += "DT_OrderInfo_CN.frx";

            Dictionary<string, object> reportSource = new Dictionary<string, object>();

            reportSource.Add("ReportSource", data);
            reportSource.Add("OrderFee", data.Fees);

            viewer.BindData(fileName, reportSource, null);
            return viewer;
        }

        /// <summary>
        /// 打印订舱确认书
        /// </summary>
        /// <param name="blID">bookingID</param>
        /// <returns>返回IReportViewer</returns>
        public  IReportViewer PrintOEBookingConfirmation(Guid bookingID)
        {
            BookingConfirmationReportData data = DTReportDataSrvice.GetBookingConfirmationReportData(bookingID);
            if (data == null) return null;

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Booking Confirmation" : "打印订舱确认书", (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);

            string fileName = GetOEReportPath() + "DT_BookingConfirmation.frx";
            Dictionary<string, object> reportSource = new Dictionary<string, object>();

            reportSource.Add("ReportSource", data);
            viewer.BindData(fileName, reportSource, null);
            return viewer;
        }

        /// <summary>
        /// 打印装箱单
        /// </summary>
        /// <param name="containerID">containerID</param>
        /// <returns>返回IReportViewer</returns>
        public  IReportViewer PrintOELoadContainer(Guid containerID)
        {
            ContainerPackingReportData data = DTReportDataSrvice.GetContainerPackingReportData(containerID);
            if (data == null) return null;

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Load Container" : "打印装箱单", (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);

            string fileName = GetOEReportPath()+"OE_BL_LoadCtn.frx";

            Dictionary<string, object> reportSource = new Dictionary<string, object>();

            reportSource.Add("ReportSource", data);

            viewer.BindData(fileName, reportSource, null);
            return viewer;
        }

        /// <summary>
        /// 打印装货单
        /// </summary>
        /// <param name="blID">提单ID</param>
        /// <returns>返回IReportViewer</returns>
        public  IReportViewer PrintOELoadGoods(Guid blID)
        {
            ShippingOrderReportData data = DTReportDataSrvice.GetShippingOrderReportData(blID);
            if (data == null) return null;

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Load Goods" : "打印装货单", (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);

            string fileName = GetOEReportPath()+"OE_LoadGoods.frx";

            Dictionary<string, object> reportSource = new Dictionary<string, object>();

            reportSource.Add("ReportSource", data);

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
            PickupCNReportData data = DTReportDataSrvice.GetPickupCNReportData(truckID);
            if (data == null) return null;

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Pickup" : "打印派车单", (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);

            string fileName = GetOEReportPath() + "OE_PickupDelivery_CN.frx";

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
            //DTBusinessReportData data = DTReportSrvice.GetOEBusinessReportData(operationID);
            //if (data == null) return null;

            //IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? " BusinessInfo" : "业务信息"
            //                                                        , (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);
            //string fileName = GetOEReportPath() +  (LocalData.IsEnglish ?"OE_BusinessInfo_EN.frx":"OE_BusinessInfo_CN.frx");
            //Dictionary<string, object> reportSource = new Dictionary<string, object>();
            //reportSource.Add("OEBusinessReportData", data);
            //reportSource.Add("BLListReportData", data.blListReportDatas);
            //viewer.BindData(fileName, reportSource, null);
            //return viewer;
            return null;
        }

        /// <summary>
        /// 打印业务信息报表
        /// </summary>
        /// <param name="operationID">operationID</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintBusinessInfoCopy(Guid operationID, FCMBLType blType)
        {

            //BLReportData data = OEReportSrvice.GetBLReportData(operationID, blType);
            //ICP.FCM.DomesticTrade.UI.BL.BLReportClientData blReportData = new ICP.FCM.DomesticTrade.UI.BL.BLReportClientData();
            //Utility.CopyToValue(data, blReportData, typeof(ICP.FCM.DomesticTrade.UI.BL.BLReportClientData));
            //if (blReportData.BLType == BLType.MBL)
            //{
            //    blReportData.MBLNo = string.Empty;
            //}
            //else if (string.IsNullOrEmpty(blReportData.MBLNo) == false)
            //{
            //    blReportData.MBLNo = "MBLNO:" + blReportData.MBLNo;
            //}

            //if (blReportData.ETD != null)
            //{
            //    blReportData.ETDString = blReportData.ETD.Value.ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            //}
            //#endregion

            //IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? " BL Info" : "提单信息"
            //                                                        , (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);

            //string fileName = GetOEReportPath() + "OE_BL_TR_Report.frx";
            //Dictionary<string, object> reportSource = new Dictionary<string, object>();
            //reportSource.Add("ReportSource", blReportData);
            //viewer.BindData(fileName, reportSource, null);
            //return viewer;
            return null;
        }
    }
}
