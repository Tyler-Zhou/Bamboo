using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DevExpress.XtraEditors;
using ICP.Common.ServiceInterface.Client;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects.ReportObjects;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Message.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.DataObjects;
using System.Windows.Forms;
using ICP.Common.ServiceInterface;
using System.Xml;
using System.IO;
using System.Text;
using System.Data;
using BillTotalInfo = ICP.FCM.OceanExport.ServiceInterface.DataObjects.BillTotalInfo;
using ProfitReportData = ICP.FCM.OceanImport.ServiceInterface.ProfitReportData;
using ICP.FileSystem.ServiceInterface;
using ICP.Framework.ClientComponents;

namespace ICP.FCM.OceanExport.UI
{

    public class OceanExportPrintHelper : Controller
    {
        #region Services


        public WorkItem Workitem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();

            }

        }
        /// <summary>
        /// 
        /// </summary>
        public IOEReportDataService OEReportSrvice
        {
            get { return ServiceClient.GetService<IOEReportDataService>(); }

        }
        public IReportViewService ReportViewService
        {
            get { return ServiceClient.GetClientService<IReportViewService>(); }
        }

        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }

        /// <summary>
        /// 公共客户管理服务
        /// </summary>
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

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
            chkPrevoyage.Checked = !ArgumentHelper.GuidIsNullOrEmpty(prevoyageId);
            chkVoyage.Checked = !ArgumentHelper.GuidIsNullOrEmpty(voyageId);

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

        //public void ShowTruckEdit(Guid id, OceanBookingList CurrentRow, WorkItem workItem, IOceanExportTruckService oeService, string no)
        //{
        //    List<OceanTruckInfo> truckList = oeService.GetOceanTruckServiceList(id);
        //    SingleResult recentData = oeService.GetTruckRecentData(id);

        //    Dictionary<string, object> stateValues = new Dictionary<string, object>();
        //    stateValues.Add("Booking", CurrentRow);

        //    if (recentData != null)
        //    {
        //        stateValues.Add("RecentTruckerID", recentData.GetValue<Guid?>("TruckerID"));
        //        stateValues.Add("RecentShipperID", recentData.GetValue<Guid?>("ShipperID"));
        //        stateValues.Add("ReturnLocationID", recentData.GetValue<Guid?>("ReturnLocationID"));
        //        stateValues.Add("ContainerDescription", SerializerHelper.DeserializeFromString<ContainerDescription>(typeof(ContainerDescription), recentData.GetValue<string>("ContainerDescription")));
        //        stateValues.Add("CustomsBrokerID", recentData.GetValue<Guid?>("CustomsBrokerID"));
        //        stateValues.Add("IsDrivingLicence", recentData.GetValue<bool?>("IsDrivingLicence"));
        //        stateValues.Add("Remark", recentData.GetValue<string>("Remark"));
        //    }

        //    string title = LocalData.IsEnglish ? "Truck Service" : "拖车服务";
        //    PartLoader.ShowEditPart<Booking.OceanTruckEditPart>(workItem, truckList, stateValues, title + no, null,
        //        Booking.OEBookingCommandConstants.Command_Truck + id.ToString());
        //}

        /// <summary>
        /// 获取海运出口报表路径 System.Windows.Forms.Application.StartupPath + "\\Reports\\OceanExport\\
        /// </summary>
        public string GetOEReportPath()
        {
            return Application.StartupPath + "\\Reports\\OceanExport\\";
        }

        /// <summary>
        /// 打印操作联系单
        /// </summary>
        /// <param name="orderID">orderID</param>
        /// <returns>返回IReportViewer</returns>
        public void PrintOEOrder(Guid orderID, Guid comanyID)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    OEOrderReportData data = OEReportSrvice.GetOEOrderReportData(orderID, comanyID);

                    if (data == null) return;

                    IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Order" : "打印操作联系单", (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);

                    string fileName = GetOEReportPath();
                    if (LocalData.IsEnglish) fileName += "OE_OrderInfo_EN.frx";
                    else fileName += "OE_OrderInfo_CN.frx";

                    Dictionary<string, object> reportSource = new Dictionary<string, object>();

                    reportSource.Add("ReportSource", data);
                    reportSource.Add("OrderFee", data.Fees);

                    Message.ServiceInterface.Message message = new Message.ServiceInterface.Message
                    {
                        UserProperties =
                            new MessageUserPropertiesObject
                            {
                                OperationType = OperationType.OceanExport,
                                OperationId = orderID,
                                FormType = FormType.ShippingOrder,
                                FormId = orderID
                            }
                    };
                    viewer.BindData(fileName, reportSource, null, message);
                    return;
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex.Message);
                    return;
                }
            }
        }


        /// <summary>
        /// 打印订舱确认书
        /// </summary>
        /// <param name="bookingID">bookingID</param>
        /// <param name="messages"></param>
        /// <returns>返回IReportViewer</returns>
        public void PrintOEBookingConfirmation(Guid bookingID, Message.ServiceInterface.Message messages)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    BookingConfirmationReportData data = OEReportSrvice.GetBookingConfirmationReportData(bookingID);
                    if (data == null)
                        return;

                    if (!String.IsNullOrEmpty(data.CarrierCode))
                    {
                        data.ShippingLineRemark = OEUtility.GetXmlValueForKey(data.CarrierCode.Trim());
                    }

                    IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Booking Confirmation" : "打印订舱确认书", Workitem.Workspaces[ClientConstants.MainWorkspace]);

                    string fileName = GetOEReportPath() + "OE_BookingConfirmation.frx";
                    Dictionary<string, object> reportSource = new Dictionary<string, object>
                    {
                        {"ReportSource", data},
                        {"ContainerSource", data.ContainerInfoList}
                    };


                    if (messages == null)
                    {
                        CustomerInfo customerinfo = GetCustomerInfo(bookingID);

                        if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.Fax))
                        {
                            reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerFaxAddressKey, customerinfo.Fax);
                        }

                        if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.EMail))
                        {
                            reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerEmailAddressKey, customerinfo.EMail);
                        }

                        Message.ServiceInterface.Message message = new Message.ServiceInterface.Message();
                        message.UserProperties = new MessageUserPropertiesObject();
                        message.UserProperties.OperationType = OperationType.OceanExport;
                        message.UserProperties.OperationId = bookingID;
                        message.UserProperties.FormType = FormType.Booking;
                        message.UserProperties.FormId = bookingID;
                        messages = message;
                    }


                    reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.DocumentNameKey, "BookingConfirmation");
                    reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.DocumentTypeKey, DocumentType.ASO);
                    viewer.BindData(fileName, reportSource, null, messages);
                    return;
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex.Message);
                    return;
                }
            }
        }

        /// <summary>
        /// 订舱确认书(宁波)
        /// </summary>
        /// <param name="operationID"></param>
        public void PrintOEBookingConfirmation4NB(Guid operationID)
        {
            try
            {
                BookingConfirmation4NBReportData reportData = OEReportSrvice.GetBookingConfirmation4NBReportData(operationID);
                if (reportData == null)
                    return;
                IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Booking Receipts" : "打印订舱确认书(宁波)", Workitem.Workspaces[ClientConstants.MainWorkspace]);

                string fileName = GetOEReportPath() + "OE_BookingConfirmation4NB.frx";
                Dictionary<string, object> reportSource = new Dictionary<string, object>
                    {
                        {"ReportSource", reportData},
                        {"BLInfoSource", reportData.BLInfoList},
                        {"ContainerInfoSource", reportData.ContainerInfoList},
                    };
                viewer.BindData(fileName, reportSource, null);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex.Message);
            }
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

            ProfitContainerObjects data = OceanExportService.GetOceanProfitReportData(_oceanBookList.ID);

            //账单币种
            string DefaultCurrencyName = string.Empty;
            List<ConfigureInfo> configureInfo = data.ConfigureInfo;
            if (configureInfo != null && configureInfo.Count > 0)
            {
                DefaultCurrencyName = configureInfo[0].DefaultCurrency;
            }

            ProfitReportData reportData = new ProfitReportData
            {
                BaseReportData =
                    new ProfitBaseReportData
                    {
                        PrintDate = DateTime.Now.ToString("MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo),
                        DefaultCurrency = DefaultCurrencyName,
                        ReferenceNo = _oceanBookList.No,
                        MasterBLNo = mblString,
                        HouseBLNo = hblString,
                        AgentName = _oceanBookList.AgentName,
                        VesselVoyageNo = vesselVoyageString,
                        LoadPortName = _oceanBookList.POLName,
                        ETD =
                            _oceanBookList.ETD == null
                                ? string.Empty
                                : _oceanBookList.ETD.Value.ToString("MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo),
                        DiscPortName = _oceanBookList.PODName,
                        ETA =
                            _oceanBookList.ETA == null
                                ? string.Empty
                                : _oceanBookList.ETA.Value.ToString("MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo)
                    }
            };

            //账单信息
            List<BillTotalInfo> billList = data.BillInfoList;
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
                    feeItem.PostDate = bill.DueDate.ToString("MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo);
                    feeItem.company = bill.CustomerName;
                    feeItem.ChargeItemDescription = bill.PayAmountDescription;
                    decimal money = 0m; //保存换算后的金额

                    switch (bill.Type)
                    {
                        case BillType.AR:
                            money = bill.Amount;
                            feeItem.Revenue = bill.Way == FeeWay.AR ? money.ToString("n") : (-money).ToString("n");
                            feeItem.Cost = 0.00m.ToString("n");
                            feeItem.agent = 0.00m.ToString("n");
                            totalRevenue += bill.Way == FeeWay.AR ? money : -money;
                            break;
                        case BillType.AP:
                            money = bill.Amount;
                            feeItem.Revenue = 0.00m.ToString("n");
                            feeItem.Cost = bill.Way == FeeWay.AR ? money.ToString("n") : (-money).ToString("n");
                            feeItem.agent = 0.00m.ToString("n");
                            totalCost += bill.Way == FeeWay.AR ? money : -money;
                            break;
                        case BillType.DC:
                            money = bill.Amount;
                            feeItem.Revenue = 0.00m.ToString("n");
                            feeItem.Cost = 0.00m.ToString("n");
                            feeItem.agent = bill.Way == FeeWay.AR ? money.ToString("n") : (-money).ToString("n");
                            totalAgent += bill.Way == FeeWay.AR ? money : -money;
                            break;
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

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Profit Print" : "利润打印", (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);

            //清空
            data = null; configureInfo.Clear(); billList.Clear();

            Message.ServiceInterface.Message message = new Message.ServiceInterface.Message();
            message.UserProperties = new MessageUserPropertiesObject();
            message.UserProperties.OperationType = OperationType.OceanExport;
            message.UserProperties.OperationId = oceanBookList.ID;
            message.UserProperties.FormType = FormType.Booking;
            message.UserProperties.FormId = oceanBookList.ID;
            viewer.BindData(fileName, reportSource, null, message);

            return viewer;
        }

        /// <summary>
        /// 打印装箱单
        /// </summary>
        /// <param name="containerID">containerID</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintOELoadContainer(Guid containerID, Guid operationId)
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
                        data.BLNo += item.BLNo + Environment.NewLine + "NPC" + Environment.NewLine + Environment.NewLine;
                    }

                    data.QuantityUnit += "CTNS" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                    data.GoodsDescription += item.Commodity + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                    data.WeightUnit += "KGS" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                    data.MeasurementUnit += "CBM" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                }
            }

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Load Container" : "打印装箱单", (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);
            string fileName = GetOEReportPath() + "OE_BL_LoadCtn.frx";
            Dictionary<string, object> reportSource = new Dictionary<string, object>();
            reportSource.Add("ReportSource", data);
            CustomerInfo customerinfo = GetCustomerInfo(operationId);

            if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.Fax))
            {
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerFaxAddressKey, customerinfo.Fax);
            }

            if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.EMail))
            {
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerEmailAddressKey, customerinfo.EMail);
            }

            Message.ServiceInterface.Message message = new Message.ServiceInterface.Message();
            message.UserProperties = new MessageUserPropertiesObject();
            message.UserProperties.OperationType = OperationType.OceanExport;
            message.UserProperties.OperationId = operationId;
            message.UserProperties.FormType = FormType.Container;
            message.UserProperties.FormId = containerID;
            viewer.BindData(fileName, reportSource, null, message);
            return viewer;
        }
        public IReportViewer PrintOELoadGoods(string operationNo, string mblNo)
        {

            ContainerPackingReportData reportData = new ContainerPackingReportData();
            OceanMBLInfo mbl = OceanExportService.GetOceanMBLInfo(operationNo, mblNo);

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

            reportData.BLNo = mbl.SONO;
            reportData.ContainerType = mbl.Container;
            string[] srr = new string[2];

            if (!mbl.VesselVoyage.IsNullOrEmpty())
            {
                srr = mbl.VesselVoyage.Split('/');
                reportData.Vessel = srr[0];
                reportData.Voyage = srr[1];
            }

            reportData.POL = mbl.POLName;
            reportData.POD = mbl.NBPODCode.IsNullOrEmpty() ? mbl.PODName : mbl.NBPODCode;
            reportData.PlaceOfDelivery = mbl.PlaceOfDeliveryName;
            reportData.ContainerQtyDescription = mbl.ContainerDescription;
            reportData.Marks = mbl.Marks;
            reportData.Quantity = mbl.Quantity;
            reportData.QuantityUnit = mbl.QuantityUnitName;

            if (string.IsNullOrEmpty(mbl.ContainerQtyDescription))
                reportData.GoodsDescription = "SHIPPER'S LOAD COUNT & SEAL(0*0) CONTAINER S.T.C.";
            else
                reportData.GoodsDescription = "SHIPPER'S LOAD COUNT & SEAL(" + mbl.ContainerQtyDescription + ") CONTAINER S.T.C.";

            if (!mbl.TransportClauseName.IsNullOrEmpty()) 
                reportData.GoodsDescription += "\r\n" + mbl.TransportClauseName + "\r\n\r\n" + mbl.GoodsDescription;

            reportData.Weight = mbl.Weight.ToString("F2");
            reportData.WeightUnit = mbl.WeightUnitName;
            reportData.Measurement = mbl.Measurement.ToString("F2");
            reportData.MeasurementUnit = mbl.MeasurementUnitName;
            reportData.ContainerInfo = mbl.CtnQtyInfo;

            //ShippingOrderReportData data = OEReportSrvice.GetShippingOrderReportData(blID);
            //if (data == null) return null;

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Load Goods" : "打印装货单", (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);
            string fileName = GetOEReportPath() + "OE_LoadGoods.frx";

            Dictionary<string, object> reportSource = new Dictionary<string, object>();

            reportSource.Add("ReportSource", reportData);
            CustomerInfo customerinfo = GetCustomerInfo(mbl.OceanBookingID);

            if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.Fax))
            {
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerFaxAddressKey, customerinfo.Fax);
            }

            if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.EMail))
            {
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerEmailAddressKey, customerinfo.EMail);
            }


            Message.ServiceInterface.Message message = new Message.ServiceInterface.Message();
            message.UserProperties = new MessageUserPropertiesObject
            {
                OperationType = OperationType.OceanExport,
                OperationId = mbl.OceanBookingID,
                FormType = FormType.MBL,
                FormId = mbl.ID
            };
            viewer.BindData(fileName, reportSource, null, message);

            return viewer;
        }

        /// <summary>
        /// 打印装货单
        /// </summary>
        /// <param name="blID">提单ID</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintOELoadGoods(OceanBLList bl)
        {
            return PrintOELoadGoods(bl.RefNo, bl.No);
        }

        /// <summary>
        /// 打印装箱单
        /// </summary>
        /// <param name="bl">提单列表</param>
        /// <param name="iscopy">是否副本</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintOELoadContainers(OceanBLList bl, bool iscopy)
        {
            List<ContainerPackingReportData> reportDatas = new List<ContainerPackingReportData>();

            BusinessOperationContext context = new BusinessOperationContext();
            context.OperationID = bl.OceanBookingID;
            context.FormType = FormType.MBL;

            List<OceanBLList> mbls = OceanExportService.GetOceanBLListByOperationInfo(context);
            if (mbls == null || mbls.Find(r => r.BLType == FCMBLType.MBL) == null)
            {
                return null;
            }

            foreach (OceanBLList m in mbls)
            {
                OceanMBLInfo mbl = OceanExportService.GetOceanMBLInfo(m.ID);
                ContainerPackingReportData reportData = new ContainerPackingReportData {BLNo = mbl.No};
                string[] srr = new string[2];

                if (mbl.VesselVoyage != null)
                {
                    srr = mbl.VesselVoyage.Split('/');
                    reportData.Vessel = srr[0];
                    reportData.Voyage = srr[1];
                }
                //TODO:箱型添加
                reportData.ContainerType = "";
                reportData.POL = mbl.POLName;
                reportData.POD = mbl.NBPODCode.IsNullOrEmpty() ? mbl.PODCode : mbl.NBPODCode;
                reportData.PlaceOfDelivery = mbl.PlaceOfDeliveryName;
                reportData.Marks = mbl.Marks;
                reportData.Quantity = mbl.Quantity;
                reportData.QuantityUnit = mbl.QuantityUnitName;
                reportData.Weight = mbl.Weight.ToString("F2") + " " + mbl.WeightUnitName; 
                reportData.WeightUnit = mbl.WeightUnitName;
                reportData.Measurement = mbl.Measurement.ToString("F2") + " " + mbl.MeasurementUnitName;
                reportData.MeasurementUnit = mbl.MeasurementUnitName;
                reportData.GoodsDescription = mbl.GoodsDescription;
                string[] Containers = {};
                if (mbl.ContainerQtyDescription.IsNullOrEmpty())
                {
                    OceanBookingInfo obi = OceanExportService.GetOceanBookingInfo(bl.OceanBookingID);
                    Containers = obi.ContainerDescription.ToString().Split(new[] { "*" }, StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    Containers = mbl.ContainerQtyDescription.Split(new[] { "*" }, StringSplitOptions.RemoveEmptyEntries);
                }
                string Container = Containers.Where((t, i) => i%2 != 0).Aggregate(string.Empty, (current, t) => current + t);

                if (Container.IsNullOrEmpty())
                {
                    OceanBookingInfo obi=OceanExportService.GetOceanBookingInfo(bl.OceanBookingID);
                    reportData.ContainerType = obi.ContainerDescription.ToString();
                }
                reportData.ContainerInfo = Container;
                reportDatas.Add(reportData);
            }


            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Packing list" : "打印装箱单", (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);
            string fileName = string.Empty;
            if (iscopy)
            {
                fileName = GetOEReportPath() + "ConLoad.frx";
            }
            else
            {
                fileName = GetOEReportPath() + "OE_LoadContainer.frx";
            }

            Dictionary<string, object> reportSource = new Dictionary<string, object>();

            reportSource.Add("ReportSource", reportDatas);
            CustomerInfo customerinfo = GetCustomerInfo(bl.OceanBookingID);

            if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.Fax))
            {
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerFaxAddressKey, customerinfo.Fax);
            }

            if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.EMail))
            {
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerEmailAddressKey, customerinfo.EMail);
            }


            Message.ServiceInterface.Message message = new Message.ServiceInterface.Message();
            message.UserProperties = new MessageUserPropertiesObject();
            message.UserProperties.OperationType = OperationType.OceanExport;
            message.UserProperties.OperationId = bl.OceanBookingID;
            message.UserProperties.FormType = FormType.MBL;
            message.UserProperties.FormId = bl.ID;
            viewer.BindData(fileName, reportSource, null, message);

            return viewer;
        }


        /// <summary>
        /// 打印派车国内报表
        /// </summary>
        /// <param name="truckID">truckID</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintPickupCN(Guid truckID, Guid operationId)
        {
            PickupCNReportData data = OEReportSrvice.GetPickupCNReportData(truckID);
            if (data == null) return null;
            data.From = LocalData.UserInfo.LoginName;
            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Pickup" : "打印派车单", (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);

            string fileName = GetOEReportPath() + "OE_PickupDelivery_CN.frx";

            Dictionary<string, object> reportSource = new Dictionary<string, object>();

            reportSource.Add("ReportSource", data);
            CustomerInfo customerinfo = GetCustomerInfo(operationId);

            if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.Fax))
            {
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerFaxAddressKey, customerinfo.Fax);
            }

            if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.EMail))
            {
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerEmailAddressKey, customerinfo.EMail);
            }

            Message.ServiceInterface.Message message = new Message.ServiceInterface.Message();
            message.UserProperties = new MessageUserPropertiesObject();
            message.UserProperties.OperationType = OperationType.OceanExport;
            message.UserProperties.OperationId = operationId;
            message.UserProperties.FormType = FormType.Customs;
            message.UserProperties.FormId = truckID;
            viewer.BindData(fileName, reportSource, null, message);
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
                                                                    , (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);
            string fileName = GetOEReportPath() + (LocalData.IsEnglish ? "OE_BusinessInfo_EN.frx" : "OE_BusinessInfo_CN.frx");
            Dictionary<string, object> reportSource = new Dictionary<string, object>();
            reportSource.Add("OEBusinessReportData", data);
            reportSource.Add("BLListReportData", data.blListReportDatas);
            CustomerInfo customerinfo = GetCustomerInfo(operationID);

            if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.Fax))
            {
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerFaxAddressKey, customerinfo.Fax);
            }

            if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.EMail))
            {
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerEmailAddressKey, customerinfo.EMail);
            }
            Message.ServiceInterface.Message message = new Message.ServiceInterface.Message();
            message.UserProperties = new MessageUserPropertiesObject();
            message.UserProperties.OperationType = OperationType.OceanExport;
            message.UserProperties.OperationId = operationID;
            message.UserProperties.FormType = FormType.Booking;
            message.UserProperties.FormId = operationID;
            viewer.BindData(fileName, reportSource, null, message);
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
            BL.BLReportClientData blReportData = new BL.BLReportClientData();
            OEUtility.CopyToValue(data, blReportData, typeof(BL.BLReportClientData));
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
                blReportData.ETDString = blReportData.ETD.Value.ToString("MMM,dd.yyyy", DateTimeFormatInfo.InvariantInfo);
            }
            #endregion

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? " BL Info" : "提单信息"
                                                                    , (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);

            string fileName = GetOEReportPath() + "OE_BL_TR_Report.frx";
            Dictionary<string, object> reportSource = new Dictionary<string, object>();
            reportSource.Add("ReportSource", blReportData);

            CustomerInfo customerinfo = GetCustomerInfo(operationID);

            if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.Fax))
            {
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerFaxAddressKey, customerinfo.Fax);
            }

            if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.EMail))
            {
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerEmailAddressKey, customerinfo.EMail);
            }
            Message.ServiceInterface.Message message = new Message.ServiceInterface.Message();
            message.UserProperties = new MessageUserPropertiesObject();
            message.UserProperties.OperationType = OperationType.OceanExport;
            message.UserProperties.OperationId = operationID;
            message.UserProperties.FormType = FormType.Booking;
            message.UserProperties.FormId = operationID;
            viewer.BindData(fileName, reportSource, null, message);

            return viewer;
        }

        /// <summary>
        /// 打印报关委托国内报表
        /// </summary>
        /// <param name="truckID">报关单ID</param>
        /// <param name="operationID">操作ID</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintCustomsCN(Guid customsID, Guid operationId)
        {
            CustomsCNReportData data = OEReportSrvice.GetCustomsCNReportData(customsID);
            if (data == null) return null;
            data.From = LocalData.UserInfo.LoginName;
            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Customs" : "打印报关委托单", (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);

            string fileName = GetOEReportPath() + "OE_Customs_CN.frx";

            Dictionary<string, object> reportSource = new Dictionary<string, object>();

            reportSource.Add("ReportSource", data);
            CustomerInfo customerinfo = GetCustomerInfo(operationId);

            if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.Fax))
            {
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerFaxAddressKey, customerinfo.Fax);
            }

            if (customerinfo != null && !string.IsNullOrEmpty(customerinfo.EMail))
            {
                reportSource.Add(ICP.Common.ServiceInterface.CommonConstants.CustomerEmailAddressKey, customerinfo.EMail);
            }

            Message.ServiceInterface.Message message = new Message.ServiceInterface.Message();
            message.UserProperties = new MessageUserPropertiesObject();
            message.UserProperties.OperationType = OperationType.OceanExport;
            message.UserProperties.OperationId = operationId;
            message.UserProperties.FormType = FormType.Customs;
            message.UserProperties.FormId = customsID;
            viewer.BindData(fileName, reportSource, null, message);
            return viewer;
        }

        /// <summary>
        ///业务ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private CustomerInfo GetCustomerInfo(Guid operationID)
        {
            OceanBookingInfo info = OceanExportService.GetOceanBookingInfo(operationID);
            CustomerInfo customerInfo = CustomerService.GetCustomerInfo(info.CustomerID);
            return customerInfo;
        }

        /// <summary>
        /// Email订舱报表
        /// </summary>
        /// <param name="bookingID">订舱ID</param>
        /// <param name="carrierID">船东ID</param>
        /// <param name="otherField">其它字段（数据库不存在的）</param>
        public void EmailBooking(Guid bookingID, Guid? carrierID, OtherEmailBookingField otherField)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    if (bookingID == Guid.Empty || carrierID == Guid.Empty || carrierID == null)
                    {
                        MessageBoxService.ShowInfo("订舱ID/船东ID不能为空！");
                        return;
                    }
                    //报表模板列表
                    List<EmailBookingSIConfig> configList = OceanExportService.GetEmailBookingSIConfig(EDIMode.Booking);

                    if (configList == null)
                    {
                        MessageBoxService.ShowInfo("未获取到配置文件！");
                        return;
                    }
                    EmailBookingSIConfig config = configList.Find(delegate(EmailBookingSIConfig c) { return c.CompanyID == LocalData.UserInfo.DefaultCompanyID && c.CarrierID == carrierID; });
                    if (config == null)
                    {
                        MessageBoxService.ShowInfo("未找到Email订舱配置信息！");
                        return;
                    }
                    //报表文件路径 \\Reports\\OceanExport\\
                    string filePath = GetOEReportPath() + config.ReportName;
                    //显示报表面板
                    IReportViewer viewer = ReportViewService.ShowReportViewer("Email Booking", (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);

                    //报表数据源
                    Dictionary<string, object> reportSource = new Dictionary<string, object>();

                    //获取数据源
                    DataSet ds = OceanExportService.GetEmailBookingDataSetByBookingID(bookingID, LocalData.UserInfo.LoginID);
                    //处理数据                   
                    EmailBookingReport data = GetDataSourceByDataSet(ds, otherField, config.EmailBookingSICode);

                    reportSource.Add("ReportSource", data);

                    Message.ServiceInterface.Message message = new Message.ServiceInterface.Message();
                    message.UserProperties = new MessageUserPropertiesObject();
                    message.UserProperties.OperationType = OperationType.OceanExport;
                    message.UserProperties.OperationId = bookingID;
                    message.UserProperties.FormType = FormType.Booking;
                    message.UserProperties.FormId = bookingID;
                    viewer.Workitem.State["ExportExcelFile"] = true;
                    //Workitem.State["ExportExcelFile"] = true;
                    viewer.BindData(filePath, reportSource, null, message);
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex.Message);
                    return;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="otherField">其它数据，临时的（数据库不存在的）</param>
        /// <param name="code"></param>
        /// <returns></returns>
        private EmailBookingReport GetDataSourceByDataSet(DataSet ds, OtherEmailBookingField otherField, EmailBookingSICode code)
        {
            EmailBookingReport e = new EmailBookingReport();

            foreach (DataRow dr in ds.Tables["Table1"].Rows)
            {
                if (code == EmailBookingSICode.SZCSCLBooking)
                {
                    e.Shipper = dr["SendByTel"].ToString();
                }
                else
                    e.Shipper = GetXMLNodeText(dr["ShipperInfo"].ToString(), "CustomerDescription/Name");
                e.Consignee = GetXMLNodeText(dr["ConisgneeInfo"].ToString(), "CustomerDescription/Name");
                e.Notify = GetXMLNodeText(dr["NotifyInfo"].ToString(), "CustomerDescription/Name");
                e.SONO = dr["SoNo"].ToString();
                e.ContractNO = dr["ContractNO"].ToString();
                e.VesselVoyage = dr["Vessel"].ToString() + "/" + dr["Voyage"].ToString();
                e.PlaceOfReceipt = dr["ReceiveLocCode"].ToString();

                e.POL = dr["PolCode"].ToString();
                e.POD = dr["PodCode"].ToString();
                e.Delivery = dr["DeliveryCode"].ToString();
                e.FinalDestination = dr["FinalDestination"].ToString();
                e.PaymentSettledAtCN = otherField.PaymentSettledAtCN;
                e.PaymentSettledAtHK = !otherField.PaymentSettledAtCN;
                e.CYCY = dr["DeliveryTerm"].ToString() == "CY-CY" ? true : false;
                e.CYDR = dr["DeliveryTerm"].ToString() == "CY-DOOR" ? true : false;
                e.DRDR = dr["DeliveryTerm"].ToString() == "DOOR-DOOR" ? true : false;

                e.DRCY = dr["DeliveryTerm"].ToString() == "DOOR-CY" ? true : false;
                e.CYFO = dr["DeliveryTerm"].ToString() == "CY-FO" ? true : false;
                e.CYLO = dr["DeliveryTerm"].ToString() == "CY-LO" ? true : false;
                e.OriginalBL = Convert.ToInt32(dr["ReleaseType"]) == 1 ? true : false;
                e.TelexRelease = Convert.ToInt32(dr["ReleaseType"]) == 2 ? true : false;
                e.SeaWayBill = Convert.ToInt32(dr["ReleaseType"]) == 3 ? true : false;
                e.ShippingMarks = dr["Marks"].ToString();
                e.NumberOfGoods = Convert.ToInt32(dr["Qty"]) <= 0 ? "" : dr["Qty"].ToString();
                e.PackageType = dr["QtyUnit"].ToString();
                e.DescriptionOfGoods = dr["GoodsInfo"].ToString();
                e.GrossWeight = Convert.ToDecimal(dr["WEIGHT"]) <= 0 ? "" : dr["WEIGHT"].ToString();
                e.Measurements = Convert.ToDecimal(dr["Volume"]) <= 0 ? "" : dr["Volume"].ToString();
                e.FreightCollect = dr["PaymentTerm"].ToString() == "CC" ? true : false;
                e.FreightPrepaid = !e.FreightCollect;
            }
            foreach (DataRow dr in ds.Tables["Table2"].Rows)
            {
                if (dr["ContainerType"].ToString() == "20GP")
                {
                    e.GP20 = Convert.ToInt32(dr["Qty"]) <= 0 ? "" : dr["Qty"].ToString();
                }
                else if (dr["ContainerType"].ToString() == "40GP")
                {
                    e.GP40 = Convert.ToInt32(dr["Qty"]) <= 0 ? "" : dr["Qty"].ToString();
                }
                else if (dr["ContainerType"].ToString() == "40HQ")
                {
                    e.HQ40 = Convert.ToInt32(dr["Qty"]) <= 0 ? "" : dr["Qty"].ToString();
                }
                else if (dr["ContainerType"].ToString() == "45HQ")
                {
                    e.HQ45 = Convert.ToInt32(dr["Qty"]) <= 0 ? "" : dr["Qty"].ToString();
                }
                else
                {
                    e.Anothers = dr["Qty"].ToString() + "*" + dr["ContainerType"].ToString();
                }

            }
            return e;
        }
        /// <summary>
        /// 获取指定节点的值
        /// </summary>
        /// <param name="xmlStr">xml字符串</param>
        /// <param name="nodeText">节点（例如：CustomerDescription/Name）</param>
        /// <returns></returns>
        private string GetXMLNodeText(string xmlStr, string nodeText)
        {
            string res = string.Empty;
            if (!string.IsNullOrEmpty(xmlStr))
            {
                XmlDocument xmlDoc = new XmlDocument();
                byte[] bs = Encoding.UTF8.GetBytes(xmlStr);
                MemoryStream ms = new MemoryStream(bs);
                xmlDoc.Load(ms); //加载XML文档   
                res = xmlDoc.SelectSingleNode(nodeText).InnerText;
            }
            return res;
        }
    }
}
