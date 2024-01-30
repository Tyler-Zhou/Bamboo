using System;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using ICP.Common.ServiceInterface.Client;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects.ReportObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanImport.ServiceInterface;



namespace ICP.FCM.OceanExport.UI
{

    public class OceanExportPrintHelper : Controller
    {
        #region Services

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IOEReportDataService OEReportSrvice { get; set; }

        [ServiceDependency]
        public IReportViewService ReportViewService { get; set; }

        [ServiceDependency]
        public IOceanExportService oeService { get; set; }

        [ServiceDependency]
        public IConfigureService configureService { get; set; }

        [ServiceDependency]
        public IFinanceService FinanceService { get; set; }

        #endregion
        /// <summary>
        /// 根据船名航次设置对应打印勾选框的勾选状态
        /// </summary>
        /// <param name="prevoyageId"></param>
        /// <param name="voyageId"></param>
        /// <param name="chkPrevoyage"></param>
        /// <param name="chkVoyage"></param>
        public void SetPrintCheckByVoyageType(Guid? prevoyageId, Guid? voyageId, CheckEdit chkPrevoyage, CheckEdit chkVoyage)
        {
            chkPrevoyage.Checked = !Utility.GuidIsNullOrEmpty(prevoyageId);
            chkVoyage.Checked = !Utility.GuidIsNullOrEmpty(voyageId);
           
        }
        /// <summary>
        /// 根据装船类型设置chk控件的Checked
        /// </summary>
        /// <param name="voyageShowType">装船类型</param>
        /// <param name="chkVoyage">显示大船</param>
        /// <param name="chkPreVoyage">显示驳船</param>
        public void SetVoyageCheckByVoyageShowType(VoyageShowType voyageShowType, CheckEdit chkVoyage, CheckEdit chkPreVoyage, string arrVeyage, string arrPreVoyage)
        {
            if (voyageShowType == VoyageShowType.Unknown)
            {
                chkVoyage.Checked = chkPreVoyage.Checked = false;
            }
            else if (voyageShowType == VoyageShowType.PreConfirm)
            {
                chkPreVoyage.Checked = true;
                chkVoyage.Checked = false;
            }
            else if (voyageShowType == VoyageShowType.Confirm)
            {
                chkPreVoyage.Checked = false;
                chkVoyage.Checked = true;
            }
            else if (voyageShowType == VoyageShowType.All)
            {
                //驳船和大船都没有
                if (string.IsNullOrEmpty(arrVeyage) && string.IsNullOrEmpty(arrPreVoyage))
                {
                    chkVoyage.Checked = chkPreVoyage.Checked = false;
                }
                //有驳船，但没有大船
                if (string.IsNullOrEmpty(arrVeyage) && !string.IsNullOrEmpty(arrPreVoyage))
                {
                    chkPreVoyage.Checked = true; chkVoyage.Checked = false;
                }
                //有大船，但没有驳船
                if (!string.IsNullOrEmpty(arrVeyage) && string.IsNullOrEmpty(arrPreVoyage))
                {
                    chkPreVoyage.Checked = false; chkVoyage.Checked = true;
                }
                //驳船和大船都有
                if (!string.IsNullOrEmpty(arrVeyage) && !string.IsNullOrEmpty(arrPreVoyage))
                {
                    chkPreVoyage.Checked = true; chkVoyage.Checked = true;
                }

                //chkVoyage.Checked = chkPreVoyage.Checked = true;
            }
        }

        /// <summary>
        /// 根椐chk获得VoyageShowType
        /// </summary>
        /// <param name="chkVoyage"></param>
        /// <param name="chkPreVoyage"></param>
        /// <returns>VoyageShowType</returns>
        public VoyageShowType GetVoyageShowTypeByVoyageCheck(CheckEdit chkPreVoyage, CheckEdit chkVoyage)
        {
            if (chkVoyage.Checked && chkPreVoyage.Checked)
            {
                return VoyageShowType.All;
            }
            else if (chkVoyage.Checked == false && chkPreVoyage.Checked == false)
            {
                return VoyageShowType.Unknown;
            }
            else if (chkVoyage.Checked == false && chkPreVoyage.Checked == true)
            {
                return VoyageShowType.PreConfirm;
            }
            else if (chkVoyage.Checked == true && chkPreVoyage.Checked == false)
            {
                return VoyageShowType.Confirm;
            }

            return VoyageShowType.All;
        }

        public void ShowTruckEdit(Guid id, OceanBookingList CurrentRow, WorkItem workItem, IOceanExportTruckService oeService, string no)
        {
            List<OceanTruckInfo> truckList = oeService.GetOceanTruckServiceList(id);
            SingleResult recentData = oeService.GetTruckRecentData(id);

            Dictionary<string, object> stateValues = new Dictionary<string, object>();
            stateValues.Add("Booking", CurrentRow);

            if (recentData != null)
            {
                stateValues.Add("RecentTruckerID", recentData.GetValue<Guid?>("TruckerID"));
                stateValues.Add("RecentShipperID", recentData.GetValue<Guid?>("ShipperID"));
                stateValues.Add("ReturnLocationID", recentData.GetValue<Guid?>("ReturnLocationID"));
                stateValues.Add("ContainerDescription", SerializerHelper.DeserializeFromString<ContainerDescription>(typeof(ContainerDescription), recentData.GetValue<string>("ContainerDescription")));
                stateValues.Add("CustomsBrokerID", recentData.GetValue<Guid?>("CustomsBrokerID"));
                stateValues.Add("IsDrivingLicence", recentData.GetValue<bool?>("IsDrivingLicence"));
                stateValues.Add("Remark", recentData.GetValue<string>("Remark"));
            }

            string title = LocalData.IsEnglish ? "Truck Service" : "拖车服务";
            PartLoader.ShowEditPart<Booking.OceanTruckEditPart>(workItem, truckList, stateValues, title + no, null,
                Booking.OEBookingCommandConstants.Command_Truck + id.ToString());
        }

        /// <summary>
        /// 获取海运出口报表路径 System.Windows.Forms.Application.StartupPath + "\\Reports\\OceanExport\\
        /// </summary>
        public string GetOEReportPath()
        {
            return System.Windows.Forms.Application.StartupPath + "\\Reports\\OceanExport\\";
        }

        /// <summary>
        /// 打印操作联系单
        /// </summary>
        /// <param name="orderID">orderID</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintOEOrder(Guid orderID)
        {
            OEOrderReportData data = OEReportSrvice.GetOEOrderReportData(orderID);
            if (data == null) return null;

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Order" : "打印操作联系单", (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);

            string fileName = GetOEReportPath();
            if (LocalData.IsEnglish) fileName += "OE_OrderInfo_EN.frx";
            else fileName += "OE_OrderInfo_CN.frx";

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
        public IReportViewer PrintOEBookingConfirmation(Guid bookingID)
        {
            BookingConfirmationReportData data = OEReportSrvice.GetBookingConfirmationReportData(bookingID);
            if (data == null) return null;

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Booking Confirmation" : "打印订舱确认书", (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);

            string fileName = GetOEReportPath() + "OE_BookingConfirmation.frx";
            Dictionary<string, object> reportSource = new Dictionary<string, object>();

            reportSource.Add("ReportSource", data);
            viewer.BindData(fileName, reportSource, null);
            return viewer;
        }

        /// <summary>
        /// 打印利润表
        /// </summary>
        /// <param name="bookingID">bookingID</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintOEBookingProfit(OceanBookingList oceanBookList)
        {
            OceanBookingList _oceanBookList = oceanBookList;
            string mblString = _oceanBookList.MBLNo;
            string hblString = _oceanBookList.HBLNo;
            string vesselVoyageString = _oceanBookList.VesselVoyage;

            ProfitContainerObjects data = oeService.GetOceanProfitReportData(_oceanBookList.ID);

            //账单币种
            string DefaultCurrencyName = string.Empty;
            List<ConfigureInfo> configureInfo = data.ConfigureInfo;
            if (configureInfo != null && configureInfo.Count > 0)
            {
                DefaultCurrencyName = configureInfo[0].DefaultCurrency;
            }

            ICP.FCM.OceanImport.ServiceInterface.ProfitReportData reportData = new ICP.FCM.OceanImport.ServiceInterface.ProfitReportData();
            reportData.BaseReportData = new ICP.FCM.OceanImport.ServiceInterface.ProfitBaseReportData();
            reportData.BaseReportData.PrintDate = DateTime.Now.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            reportData.BaseReportData.DefaultCurrency = DefaultCurrencyName;
            reportData.BaseReportData.ReferenceNo = _oceanBookList.No;
            reportData.BaseReportData.MasterBLNo = mblString;
            reportData.BaseReportData.HouseBLNo = hblString;
            reportData.BaseReportData.AgentName = _oceanBookList.AgentName;
            reportData.BaseReportData.VesselVoyageNo = vesselVoyageString;
            reportData.BaseReportData.LoadPortName = _oceanBookList.POLName;
            reportData.BaseReportData.ETD = _oceanBookList.ETD == null ? string.Empty : _oceanBookList.ETD.Value.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            reportData.BaseReportData.DiscPortName = _oceanBookList.PODName;
            reportData.BaseReportData.ETA = _oceanBookList.ETA == null ? string.Empty : _oceanBookList.ETA.Value.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);

            //账单信息
            List<ICP.FCM.OceanExport.ServiceInterface.DataObjects.BillTotalInfo> billList = data.BillInfoList;
            if (billList != null && billList.Count > 0)
            {
                reportData.Fees = new List<ProfitReportFeeData>();
                decimal totalRevenue = 0m;
                decimal totalCost = 0m;
                decimal totalAgent = 0m;

                foreach (var bill in billList)
                {
                    ProfitReportFeeData feeItem = new ProfitReportFeeData();
                    feeItem.InvNo = bill.No;
                    feeItem.PostDate = bill.DueDate.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                    feeItem.company = bill.CustomerName;
                    feeItem.ChargeItemDescription = bill.PayAmountDescription;
                    decimal money = 0m; //保存换算后的金额

                    if (bill.Type == BillType.AR)
                    {
                        money = bill.Amount;
                        feeItem.Revenue = bill.Way == FeeWay.AR ? money.ToString("n") : (-money).ToString("n");
                        feeItem.Cost = 0.00m.ToString("n");
                        feeItem.agent = 0.00m.ToString("n");
                        totalRevenue += bill.Way == FeeWay.AR ? money : -money;
                    }
                    else if (bill.Type == BillType.AP)
                    {
                        money = bill.Amount;
                        feeItem.Revenue = 0.00m.ToString("n");
                        feeItem.Cost = bill.Way == FeeWay.AR ? money.ToString("n") : (-money).ToString("n");
                        feeItem.agent = 0.00m.ToString("n");
                        totalCost += bill.Way == FeeWay.AR ? money : -money;
                    }
                    else if (bill.Type == BillType.DC)
                    {
                        money = bill.Amount;
                        feeItem.Revenue = 0.00m.ToString("n");
                        feeItem.Cost = 0.00m.ToString("n");
                        feeItem.agent = bill.Way == FeeWay.AR ? money.ToString("n") : (-money).ToString("n");
                        totalAgent += bill.Way == FeeWay.AR ? money : -money;
                    }

                    reportData.Fees.Add(feeItem);
                }

                reportData.BaseReportData.TotalRevenue = totalRevenue.ToString("n");
                reportData.BaseReportData.TotalCost = totalCost.ToString("n");
                reportData.BaseReportData.TotalAgent = totalAgent.ToString("n");
                reportData.BaseReportData.Profit = (totalRevenue + totalCost + totalAgent).ToString("n");
            }

            string fileName = GetOEReportPath();
            fileName += "OE_Profit.frx";
            Dictionary<string, object> reportSource = new Dictionary<string, object>();
            reportSource.Add("ReportSource", reportData.BaseReportData);
            if (reportData.Fees != null && reportData.Fees.Count > 0)
            {
                reportSource.Add("FeeListReportSource", reportData.Fees);
            }

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Profit Print" : "利润打印", (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);
            viewer.BindData(fileName, reportSource, null);
            //清空
            data = null; configureInfo.Clear(); billList.Clear();
            return viewer;
        }

        /// <summary>
        /// 打印装箱单
        /// </summary>
        /// <param name="containerID">containerID</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintOELoadContainer(Guid containerID)
        {
            ContainerPackingReportData data = OEReportSrvice.GetContainerPackingReportData(containerID);
            if (data == null) return null;
            data.BLNo = string.Empty;
            data.QuantityUnit = string.Empty;
            data.GoodsDescription = string.Empty;
            data.WeightUnit = string.Empty;
            data.MeasurementUnit = string.Empty;
            if (data.GoodsList != null && data.GoodsList.Count > 0)
            {
                foreach (var item in data.GoodsList)
                {
                    if (!string.IsNullOrEmpty(item.BLNo))
                    {
                        data.BLNo += item.BLNo + System.Environment.NewLine + "NPC" + System.Environment.NewLine + System.Environment.NewLine;
                    }

                    data.QuantityUnit += "CTNS" + System.Environment.NewLine + System.Environment.NewLine + System.Environment.NewLine;
                    data.GoodsDescription += item.Commodity + System.Environment.NewLine + System.Environment.NewLine + System.Environment.NewLine;
                    data.WeightUnit += "KGS" + System.Environment.NewLine + System.Environment.NewLine + System.Environment.NewLine;
                    data.MeasurementUnit += "CBM" + System.Environment.NewLine + System.Environment.NewLine + System.Environment.NewLine;
                }
            }

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Load Container" : "打印装箱单", (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);
            string fileName = GetOEReportPath() + "OE_BL_LoadCtn.frx";
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
        public IReportViewer PrintOELoadGoods(OceanBLList bl)
        {
            ContainerPackingReportData reportData = new ContainerPackingReportData();
            OceanMBLInfo mbl = oeService.GetOceanMBLInfo(bl.ID);

            if (mbl.ShipperDescription != null)
            {
                reportData.ShipperDescription = mbl.ShipperDescription.ToString(LocalData.IsEnglish);
            }

            if (mbl.ConsigneeDescription != null)
            {
                reportData.ConsigneeDescription = mbl.ConsigneeDescription.ToString(LocalData.IsEnglish);
            }
            if (mbl.NotifyPartyDescription != null)
            {
                reportData.NotifyPartyDescription = mbl.NotifyPartyDescription.ToString(LocalData.IsEnglish);
            }

            reportData.BLNo = bl.SONO;
            string[] srr = new string[2];

            if (mbl.VesselVoyage != null)
            {
                srr = mbl.VesselVoyage.Split('/');
                reportData.Vessel = srr[0];
                reportData.Voyage = srr[1];
            }

            reportData.POL = mbl.POLName;
            reportData.POD = mbl.PODName;
            reportData.PlaceOfDelivery = mbl.PlaceOfDeliveryName;
            reportData.ContainerQtyDescription = mbl.ContainerDescription;
            reportData.Marks = mbl.Marks;
            reportData.Quantity = mbl.Quantity;
            reportData.QuantityUnit = mbl.QuantityUnitName;

            if (string.IsNullOrEmpty(mbl.ContainerQtyDescription))
                reportData.GoodsDescription = "SHIPPER'S LOAD COUNT & SEAL(0*0) CONTAINER S.T.C.";
            else
                reportData.GoodsDescription = "SHIPPER'S LOAD COUNT & SEAL(" + mbl.ContainerQtyDescription + ") CONTAINER S.T.C.";

            if (string.IsNullOrEmpty(mbl.TransportClauseName) == false) reportData.GoodsDescription += "\r\n" + mbl.TransportClauseName;

            reportData.Weight = mbl.Weight.ToString("F2");
            reportData.WeightUnit = mbl.WeightUnitName;
            reportData.Measurement = mbl.Measurement.ToString("F2");
            reportData.MeasurementUnit = mbl.MeasurementUnitName;
            reportData.ContainerInfo = mbl.CtnQtyInfo;

            //ShippingOrderReportData data = OEReportSrvice.GetShippingOrderReportData(blID);
            //if (data == null) return null;

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Load Goods" : "打印装货单", (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);
            string fileName = GetOEReportPath() + "OE_LoadGoods.frx";

            Dictionary<string, object> reportSource = new Dictionary<string, object>();

            reportSource.Add("ReportSource", reportData);

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
            PickupCNReportData data = OEReportSrvice.GetPickupCNReportData(truckID);
            if (data == null) return null;
            data.From = LocalData.UserInfo.LoginName;
            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Pickup" : "打印派车单", (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);

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
            OEBusinessReportData data = OEReportSrvice.GetOEBusinessReportData(operationID);
            if (data == null) return null;

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? " BusinessInfo" : "业务信息"
                                                                    , (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);
            string fileName = GetOEReportPath() + (LocalData.IsEnglish ? "OE_BusinessInfo_EN.frx" : "OE_BusinessInfo_CN.frx");
            Dictionary<string, object> reportSource = new Dictionary<string, object>();
            reportSource.Add("OEBusinessReportData", data);
            reportSource.Add("BLListReportData", data.blListReportDatas);
            viewer.BindData(fileName, reportSource, null);
            return viewer;
        }

        /// <summary>
        /// 打印业务信息报表
        /// </summary>
        /// <param name="operationID">operationID</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintBusinessInfoCopy(Guid operationID, FCMBLType blType)
        {
            #region 数据源

            BLReportData data = OEReportSrvice.GetBLReportData(operationID, blType);
            ICP.FCM.OceanExport.UI.BL.BLReportClientData blReportData = new ICP.FCM.OceanExport.UI.BL.BLReportClientData();
            Utility.CopyToValue(data, blReportData, typeof(ICP.FCM.OceanExport.UI.BL.BLReportClientData));
            if (blReportData.BLType == FCMBLType.MBL)
            {
                blReportData.MBLNo = string.Empty;
            }
            else if (string.IsNullOrEmpty(blReportData.MBLNo) == false)
            {
                blReportData.MBLNo = "MBLNO:" + blReportData.MBLNo;
            }

            if (blReportData.ETD != null)
            {
                blReportData.ETDString = blReportData.ETD.Value.ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            }
            #endregion

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? " BL Info" : "提单信息"
                                                                    , (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);

            string fileName = GetOEReportPath() + "OE_BL_TR_Report.frx";
            Dictionary<string, object> reportSource = new Dictionary<string, object>();
            reportSource.Add("ReportSource", blReportData);
            viewer.BindData(fileName, reportSource, null);
            return viewer;
        }



    }
}
