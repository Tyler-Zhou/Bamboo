using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Server;
using ICP.FCM.Common.UI.Document;

namespace ICP.FCM.Common.UI.DispatchCompare
{
    public partial class OIDispachCompareBasePartNew : DispatchDocumentCompareNew
    {
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        /// <summary>
        /// 系统错误日志服务
        /// </summary>
        public ISystemErrorLogService SystemErrorLogService
        {
            get { return ServiceClient.GetService<ISystemErrorLogService>(); }
        }
        public OIDispachCompareBasePartNew()
        {
            InitializeComponent();

            this.Load += delegate
            {

            };
            this.Disposed += delegate
            {
                try
                {
                    this.dispatchCompareObject = null;

                    if (this.Workitem != null)
                    {
                        this.Workitem.Items.Remove(this);
                        this.Workitem = null;
                    }
                }
                catch (Exception ex)
                {
                    string exceptionstr = "OIDispachCompareBasePartNew():Disposed\r\n" + ex.Message;
                    SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                        LocalData.SessionId, new byte[0], exceptionstr,
                        DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
                }
            };
        }

        /// <summary>
        ///  绑定数据
        /// </summary>
        public override void InnerInitData()
        {
            string result = FCMCommonService.GetDispatchNewLogID(NewOperationID);
            Guid fileLog = JSONSerializerHelper.DeserializeFromJson<Guid>(result);
            dispatchCompareObject.OIOceanBusinessInfo = oiService.GetBusinessInfoByEdit(OldOperationID);
            _businessInfo = dispatchCompareObject.OIOceanBusinessInfo;
            dispatchCompareObject.OIBillInfo = finService.GetBillInfos(OldOperationID);
            dispatchCompareObject.OEBillInfo = finService.GetBillInfos(NewOperationID);

            //dispatchCompareObject.OEBookingInfo = oiService.GetCompareBusinessInfo(NewOperationID, OldOperationID, null, Guid.Empty);
            dispatchCompareObject.OEBookingInfo = oiService.GetDispatchBookingInfo(NewOperationID, fileLog, LocalData.UserInfo.LoginID);

            //dispatchCompareObject.OEMBLInfo = oiService.GetOECompareMBLInfo(dispatchCompareObject.OEBookingInfo.MBLID.Value);
            dispatchCompareObject.OEMBLInfo = oiService.GetDispatchMBLInfo(dispatchCompareObject.OEBookingInfo.MBLID.Value, fileLog);

            //dispatchCompareObject.OEHBLList = oiService.GetOceanCompareHBLList(NewOperationID);
            dispatchCompareObject.OEHBLList = oiService.GetDispatchHBLInfo(NewOperationID, fileLog);

            //dispatchCompareObject.OEContainerList = oiService.GetOICompareContainerList(OldOperationID);
            dispatchCompareObject.OEContainerList = oiService.GetDispatchContainerInfo(NewOperationID, fileLog);
            OceanBusinessInfo oiInfo = dispatchCompareObject.OIOceanBusinessInfo;
            OceanBusinessInfo oeInfo = dispatchCompareObject.OEBookingInfo;
            if (oiInfo == null || oeInfo == null) return;

            string fieldDbName = "1";
            string fieldName = "";
            string oldValue = "";
            string newValue = "";
            CurrBusinessUpdate.Clear();

            #region 基本信息

            #region 运输条款
            if (oiInfo.TransportClauseID != oeInfo.TransportClauseID)
            {
                fieldDbName = "1";
                fieldName = LocalData.IsEnglish ? "Trans Clause" : "运输条款";
                oldValue = oiInfo.TransportClauseName;
                newValue = oeInfo.TransportClauseName;
                AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            }
            #endregion


            #region 业务类型

            if (oiInfo.OIOperationType != oeInfo.OIOperationType)
            {
                fieldDbName = "1";
                fieldName = LocalData.IsEnglish ? "Type" : "业务类型";
                oldValue = EnumHelper.GetDescription<FCMOperationType>(oiInfo.OIOperationType, LocalData.IsEnglish);
                newValue = EnumHelper.GetDescription<FCMOperationType>(oeInfo.OIOperationType, LocalData.IsEnglish);
                AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            }

            #endregion

            #region 付款方式
            if (oiInfo.PaymentTermID != oeInfo.PaymentTermID)
            {
                fieldDbName = "1";
                fieldName = LocalData.IsEnglish ? "Payt Term" : "付款方式";
                oldValue = oiInfo.PaymentTermName;
                newValue = oeInfo.PaymentTermName;
                AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            }
            #endregion

            #region 委托方式
            if (oiInfo.BookingMode != oeInfo.BookingMode)
            {
                fieldDbName = "1";
                fieldName = LocalData.IsEnglish ? "Bkg Type" : "委托方式";
                oldValue = EnumHelper.GetDescription<FCMBookingMode>(oiInfo.BookingMode, LocalData.IsEnglish);
                newValue = EnumHelper.GetDescription<FCMBookingMode>(oeInfo.BookingMode, LocalData.IsEnglish);
                AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            }
            #endregion


            #region 贸易条款
            if (oiInfo.TradeTermID != oeInfo.TradeTermID)
            {
                fieldDbName = "1";
                fieldName = LocalData.IsEnglish ? "Trade Term" : "贸易条款";
                oldValue = oiInfo.TradeTermName;
                newValue = oeInfo.TradeTermName;
                AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            }
            #endregion

            #region 海外部客服
            if (oiInfo.OverSeasFilerId != oeInfo.OverSeasFilerId)
            {
                fieldDbName = "1";
                fieldName = LocalData.IsEnglish ? "OverSeas Filer" : "海外部客服";
                oldValue = oiInfo.OverSeasFilerName;
                newValue = oeInfo.OverSeasFilerName;
                AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            }
            #endregion

            #region 揽货人
            if (oiInfo.SalesID != oeInfo.SalesID)
            {
                fieldDbName = "1";
                fieldName = LocalData.IsEnglish ? "Sales" : "揽货人";
                oldValue = oiInfo.SalesName;
                newValue = oeInfo.SalesName;
                AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            }
            #endregion

            #region 揽货部门
            if (oiInfo.SalesDepartmentID != oeInfo.SalesDepartmentID)
            {
                fieldDbName = "1";
                fieldName = LocalData.IsEnglish ? "Sales Dept" : "揽货部门";
                oldValue = oiInfo.SalesDepartmentName;
                newValue = oeInfo.SalesDepartmentName;
                AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            }
            #endregion


            #endregion 基本信息

            #region 委托信息

            #region 代理/参考号
            if (oiInfo.AgentID != oeInfo.AgentID)
            {
                fieldDbName = "1";
                fieldName = LocalData.IsEnglish ? "Agent/No" : "代理/参考号";
                oldValue = oiInfo.AgentName;
                newValue = oeInfo.AgentName;
                AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            }
            #endregion

            #region 品名
            if (oiInfo.Commodity != oeInfo.Commodity)
            {
                fieldDbName = "1";
                fieldName = LocalData.IsEnglish ? "Commodity" : "品名";
                oldValue = oiInfo.Commodity;
                newValue = oeInfo.Commodity.Replace("\r", "").Replace("\n", "");
                AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            }
            #endregion

            #region 发货人
            if (oiInfo.ShipperID != oeInfo.ShipperID)
            {
                fieldDbName = "1";
                fieldName = LocalData.IsEnglish ? "Shipper" : "发货人";
                oldValue = oiInfo.ShipperName;
                newValue = oeInfo.ShipperName;
                AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            }
            #endregion

            #region 数量
            if (oiInfo.Quantity != oeInfo.Quantity)
            {
                fieldDbName = "1";
                fieldName = LocalData.IsEnglish ? "QTY" : "数量";
                oldValue = oiInfo.Quantity.ToString();
                newValue = oeInfo.Quantity.ToString();
                AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            }
            #endregion


            #region 数量单位
            if (oiInfo.QuantityUnitID != oeInfo.QuantityUnitID)
            {
                fieldDbName = "1";
                fieldName = LocalData.IsEnglish ? "QTY Unit" : "数量单位";
                oldValue = oiInfo.QuantityUnitName;
                newValue = oeInfo.QuantityUnitName;
                AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            }
            #endregion

            #region 收货人
            if (oiInfo.ConsigneeID != oeInfo.ConsigneeID)
            {
                fieldDbName = "1";
                fieldName = LocalData.IsEnglish ? "Consignee" : "收货人";
                oldValue = oiInfo.ConsigneeName;
                newValue = oeInfo.ConsigneeName;
                AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            }
            #endregion

            #region 重量
            if (oiInfo.Weight != oeInfo.Weight)
            {
                fieldDbName = "1";
                fieldName = LocalData.IsEnglish ? "Weight" : "重量";
                oldValue = oiInfo.Weight.ToString();
                newValue = oeInfo.Weight.ToString();
                AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            }
            #endregion

            #region 重量单位
            if (oiInfo.WeightUnitID != oeInfo.WeightUnitID)
            {
                fieldDbName = "1";
                fieldName = LocalData.IsEnglish ? "Weight Unit" : "重量单位";
                oldValue = oiInfo.WeightUnitName;
                newValue = oeInfo.WeightUnitName;
                AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            }
            #endregion

            #region  通知人
            if (oiInfo.NotifyPartyID != oeInfo.NotifyPartyID)
            {
                fieldDbName = "1";
                fieldName = LocalData.IsEnglish ? "Notify Party" : "通知人";
                oldValue = oiInfo.NotifyPartyName;
                newValue = oeInfo.NotifyPartyName;
                AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            }
            #endregion

            #region 体积
            if (oiInfo.Measurement != oeInfo.Measurement)
            {
                fieldDbName = "1";
                fieldName = LocalData.IsEnglish ? "Measurement" : "体积";
                oldValue = oiInfo.Measurement.ToString();
                newValue = oeInfo.Measurement.ToString();
                AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            }
            #endregion

            #region 体积单位
            if (oiInfo.MeasurementUnitID != oeInfo.MeasurementUnitID)
            {
                fieldDbName = "1";
                fieldName = LocalData.IsEnglish ? "Measurement Unit" : "体积单位";
                oldValue = oiInfo.MeasurementUnitName;
                newValue = oeInfo.MeasurementUnitName;
                AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            }
            #endregion

            #region 收货地
            if (oiInfo.PlaceOfReceiptID != oeInfo.PlaceOfReceiptID)
            {
                fieldDbName = "1";
                fieldName = LocalData.IsEnglish ? "Delivery" : "收货地";
                oldValue = oiInfo.PlaceOfReceiptName;
                newValue = oeInfo.PlaceOfReceiptName;
                AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            }
            #endregion


            #region 装货港
            if (oiInfo.POLID != oeInfo.POLID)
            {
                fieldDbName = "1";
                fieldName = LocalData.IsEnglish ? "POL" : "装货港";
                oldValue = oiInfo.POLName;
                newValue = oeInfo.POLName;
                AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            }
            #endregion


            #region 仓库
            if (oiInfo.WareHouseID != oeInfo.WareHouseID)
            {
                fieldDbName = "1";
                fieldName = LocalData.IsEnglish ? "WareHouse" : "仓库";
                oldValue = oiInfo.WareHouseName;
                newValue = oeInfo.WareHouseName;
                AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            }
            #endregion

            #region FETA
            //if (oiInfo.FETA != oeInfo.FETA)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "FETA" : "FETA";
            //    oldValue = oiInfo.FETA.ToString();
            //    newValue = oeInfo.FETA.ToString();
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            #endregion

            #region 报关行
            if (oiInfo.CustomsID != oeInfo.CustomsID)
            {
                fieldDbName = "1";
                fieldName = LocalData.IsEnglish ? "WareHouse" : "报关行";
                oldValue = oiInfo.CustomsName;
                newValue = oeInfo.CustomsName;
                AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            }
            #endregion



            #endregion 委托信息

            #region MBL信息
            OceanBusinessMBLList oiMbl = oiInfo.MBLInfo;
            OceanBusinessMBLList oeMbl = dispatchCompareObject.OEMBLInfo;
            if (oiMbl == null || oeMbl == null) return;


            #region 承运人
            if (oiMbl.AgentOfCarrierID != oeMbl.AgentOfCarrierID)
            {
                fieldDbName = "1";
                fieldName = LocalData.IsEnglish ? "Agent Of Carrier" : "承运人";
                oldValue = oiMbl.AgentOfCarrierName;
                newValue = oeMbl.AgentOfCarrierName;
                AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            }
            #endregion

            #endregion

            #region HBL信息
            List<OceanBusinessHBLList> oiHblList = oiInfo.HBLList;
            List<OceanBusinessHBLList> oeHblList = dispatchCompareObject.OEHBLList;
            if (oiHblList != null)
            {
                List<HBLInfo> Hbls = new List<HBLInfo>();
                foreach (OceanBusinessHBLList hbl in oiHblList)
                {
                    HBLInfo hblinfo = new HBLInfo();
                    hblinfo.No = hbl.HBLNo;
                    hblinfo.OldISFNo = hbl.ISFNo;
                    hblinfo.OldAMSNo = hbl.AMSNo;
                    // hblinfo.OldDescriptionOfGood = hbl.GoodsInfo;
                    // hblinfo.OldMeasurement = hbl.Measurement.ToString();
                    // hblinfo.OldQuantity = hbl.Qty.ToString();
                    // hblinfo.OldReceiveOBLDate = hbl.ReceiveOBLDate.ToString();
                    hblinfo.OldShipperID = hbl.ShipperName;
                    //  hblinfo.OldWeight = hbl.Weight.ToString();

                    var tmp = oeHblList.Find((OceanBusinessHBLList ss) => { return ss.HBLNo == hbl.HBLNo; });
                    if (tmp != null)
                    {
                        hblinfo.NewISFNo = tmp.ISFNo;
                        hblinfo.NewAMSNo = tmp.AMSNo;
                        hblinfo.NewShipperID = tmp.ShipperName;
                    }
                    else
                    {
                        hblinfo.NewISFNo = "";
                        hblinfo.NewAMSNo = "";
                        hblinfo.NewShipperID = "";
                    }
                    if (hblinfo.NewISFNo != hblinfo.OldISFNo || hblinfo.NewAMSNo != hblinfo.OldAMSNo || hblinfo.NewShipperID != hblinfo.OldShipperID)
                    {
                        Hbls.Add(hblinfo);
                    }

                }
                if (Hbls.Count > 0)
                {
                    AddBusiness("2", LocalData.IsEnglish ? "HBL Information" : "分提单信息", "", "", Hbls, null);
                    isHblChange = true;

                }

            }
            #endregion

            #region 集装箱信息
            List<OIBusinessContainerList> oiContainerList = oiInfo.ContainerList;
            List<OIBusinessContainerList> oeContainerList = dispatchCompareObject.OEContainerList;
            if (oiHblList != null)
            {
                List<ContainerInfo> containers = new List<ContainerInfo>();
                foreach (OIBusinessContainerList box in oiContainerList)
                {
                    ContainerInfo containerInfo = new ContainerInfo();
                    containerInfo.No = box.No;
                    containerInfo.OldContainerType = box.ContainerTypeName;
                    containerInfo.OldSealNo = box.SealNo;
                    containerInfo.OldIsPartOf = box.IsPartOf ? "1" : "0";
                    containerInfo.OldMeasurement = box.Measurement.ToString();
                    containerInfo.OldQuantity = box.Quantity.ToString();
                    containerInfo.OldWeight = box.Weight.ToString();

                    // containerInfo.HBLNo = box.BLNo;
                    //containerInfo.OldAvailableDate = box.AvailableDate.ToString();
                    //containerInfo.OldGODate = box.GODate.ToString();   
                    //containerInfo.OldLastFreeDate = box.LFDate.ToString();
                    //containerInfo.OldLocation = box.Location;
                    // containerInfo.OldPickUpNo = box.PickUpDate.ToString();

                    var tmp = oeContainerList.Find((OIBusinessContainerList ss) => { return ss.No == box.No; });
                    if (tmp != null)
                    {
                        containerInfo.NewSealNo = tmp.SealNo;
                        containerInfo.NewContainerType = tmp.ContainerTypeName;
                        containerInfo.NewIsPartOf = tmp.IsPartOf ? "1" : "0";
                        containerInfo.NewMeasurement = tmp.Measurement.ToString();
                        containerInfo.NewQuantity = tmp.Quantity.ToString();
                        containerInfo.NewWeight = tmp.Weight.ToString();
                    }
                    else
                    {
                        containerInfo.NewSealNo = "";
                        containerInfo.NewIsPartOf = "";
                        containerInfo.NewMeasurement = "";
                        containerInfo.NewQuantity = "";
                        containerInfo.NewWeight = "";
                    }
                    if (containerInfo.NewSealNo != containerInfo.OldSealNo || containerInfo.NewIsPartOf != containerInfo.OldIsPartOf
                           || containerInfo.NewMeasurement != containerInfo.OldMeasurement || containerInfo.NewQuantity != containerInfo.OldQuantity
                             || containerInfo.NewWeight != containerInfo.OldWeight)
                    {
                        containers.Add(containerInfo);
                    }

                }
                if (containers.Count > 0)
                {
                    AddBusiness("2", LocalData.IsEnglish ? "Container Information" : "集装箱信息", "", "", null, containers);
                    isContainerChange = true;
                }

            }
            #endregion

            string OEProfit;
            string OIProfit;
            CurrBill = FCMCommonService.DispatchCompareBillAndCharge(NewOperationID, OldOperationID, fileLog, OperationType.OceanExport);
            //lblNewProfitValue.Text = OEProfit;
            //lblOldProfitValue.Text = OIProfit;


        }

        public override bool SaveData()
        {
            return base.SaveData();
        }
    }
}
