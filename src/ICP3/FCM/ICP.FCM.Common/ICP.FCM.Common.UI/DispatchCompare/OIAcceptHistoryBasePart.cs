using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Common;
using ICP.FileSystem.ServiceInterface;

namespace ICP.FCM.Common.UI.DispatchCompare
{
    [SmartPart]
    public partial class OIAcceptHistoryBasePart : ICP.FCM.Common.UI.Document.DispatchDocumentCompareBase
    {   
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        public Guid OperationID
        {
            get;
            set;
        }
        public Guid BeforeApplyID
        {
            get;
            set;
        }
        public Guid AfterApplyID
        {
            get;
            set;
        }


        public OIAcceptHistoryBasePart()
        {
            CurrDipatchCompareType = DipatchCompareType.OIAcceptHestory;
            InitializeComponent();
            this.Disposed += delegate {
                this.dispatchCompareObject = null;
                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            profitComparePart1.Visible = false;

        }
        /// <summary>
        ///  绑定数据
        /// </summary>
        public override void InnerInitData()
        {

            dispatchCompareObject.OIOceanBusinessInfo = oiService.GetBusinessInfoByEdit(CurrentSimpleBusinnessInfo.OIBusinessID);
            _businessInfo = dispatchCompareObject.OIOceanBusinessInfo;

            dispatchCompareObject.OIBillInfo = finService.GetBillInfos(CurrentSimpleBusinnessInfo.OIBusinessID);
            dispatchCompareObject.OEBillInfo = finService.GetBillInfos(CurrentSimpleBusinnessInfo.OEBusinessID);




            dispatchCompareObject.OEBookingInfo = oiService.GetCompareBusinessInfo(CurrentSimpleBusinnessInfo.OEBusinessID, CurrentSimpleBusinnessInfo.OIBusinessID, null, Guid.Empty);

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

            //#region 文件
            //if (oiInfo.FilerId != oeInfo.FilerId)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "Filer" : "文件";
            //    oldValue = oiInfo.FilerName;
            //    newValue = oeInfo.FilerName;
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion

            //#region 操作口岸
            //if (oiInfo.CompanyID != oeInfo.CompanyID)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "Operate Company" : "操作口岸";
            //    oldValue = oiInfo.CompanyName;
            //    newValue = oeInfo.CompanyName;
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion



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


            //#region 客户/参考号 有待确认
            //if (oiInfo.CustomerID != oeInfo.CustomerID)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "Cust/Ref.No" : "客户/参考号";
            //    oldValue = oiInfo.CustomerName;
            //    newValue = oeInfo.CustomerName;
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion


            //#region 港前客服
            //if (oiInfo.POLFilerName != oeInfo.POLFilerName)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "OE filer Name" : "港前客服";
            //    oldValue = oiInfo.POLFilerName;
            //    newValue = oeInfo.POLFilerName;
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion


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

            //#region 揽货类型
            //if (oiInfo.SalesTypeID != oeInfo.SalesTypeID)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "Sales Type" : "揽货类型";
            //    oldValue = oiInfo.SalesTypeName;
            //    newValue = oeInfo.SalesTypeName;
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion

            //#region  委托日期
            //if (oiInfo.BookingDate != oeInfo.BookingDate)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "Bkg Date" : "委托日期";
            //    oldValue = oiInfo.BookingDate.ToShortDateString();
            //    newValue = oeInfo.BookingDate.ToShortDateString();
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion

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

            ////#region  货物描述
            ////if (oiInfo.CargoDescription.Type != oeInfo.CargoDescription.Type)
            ////{
            ////    fieldDbName = "1";
            ////    fieldName = LocalData.IsEnglish ? "Cargo Type" : "货物类型";

            ////    oldValue = (oiInfo.CargoDescription.Type == null) ? "" : oiInfo.CargoDescription.Type;

            ////    newValue = (oeInfo.CargoDescription.Type == null) ? "" : oeInfo.CargoDescription.Type;
            ////    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            ////}
            ////#endregion

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

            //#region  离港日
            //if (oiInfo.ETD != oeInfo.ETD)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "ETD" : "离港日";
            //    oldValue = oiInfo.ETD.ToString();
            //    newValue = oeInfo.ETD.ToString();
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion

            //#region 卸货港
            //if (oiInfo.PODID != oeInfo.PODID)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "POD" : "卸货港";
            //    oldValue = oiInfo.PODName;
            //    newValue = oeInfo.PODName;
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion

            //#region 到港日
            //if (oiInfo.ETA != oeInfo.ETA)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "ETA" : "到港日";
            //    oldValue = oiInfo.ETA.ToString();
            //    newValue = oeInfo.ETA.ToString();
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion

            //#region 交货地
            //if (oiInfo.PlaceOfDeliveryID != oeInfo.PlaceOfDeliveryID)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "Delivery" : "交货地";
            //    oldValue = oiInfo.PlaceOfDeliveryName;
            //    newValue = oeInfo.PlaceOfDeliveryName;
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion

            //#region DETA
            //if (oiInfo.DETA != oeInfo.DETA)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "DETA" : "DETA";
            //    oldValue = oiInfo.DETA.ToString();
            //    newValue = oeInfo.DETA.ToString();
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion
            //#region 最终目的地
            //if (oiInfo.FinalDestinationID != oeInfo.FinalDestinationID)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "Final Dest" : "最终目的地";
            //    oldValue = oiInfo.FinalDestinationName;
            //    newValue = oeInfo.FinalDestinationName;
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion


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

            //#region 是否报关
            //if (oiInfo.IsCustoms != oeInfo.IsCustoms)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "Is Customs" : "是否报关";
            //    oldValue = oiInfo.IsCustoms ? "√" : " ×";
            //    newValue = oeInfo.IsCustoms ? "√" : " ×";
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion

            //#region 商检

            //if (oiInfo.IsCommodityInspection != oeInfo.IsCommodityInspection)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "Commodity Inspection" : "商检";
            //    oldValue = oiInfo.IsCommodityInspection ? "√" : " ×";
            //    newValue = oeInfo.IsCommodityInspection ? "√" : " ×";
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion

            //#region 质检
            //if (oiInfo.IsQuarantineInspection != oeInfo.IsQuarantineInspection)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "Quarantine Inspection" : "质检";
            //    oldValue = oiInfo.IsQuarantineInspection ? "√" : " ×";
            //    newValue = oeInfo.IsQuarantineInspection ? "√" : " ×";
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion

            //#region 是否仓储
            //if (oiInfo.IsWareHouse != oeInfo.IsWareHouse)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "Is Warehouse" : "仓储";
            //    oldValue = oiInfo.IsWareHouse ? "√" : " ×";
            //    newValue = oeInfo.IsWareHouse ? "√" : " ×";
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion

            //#region 运输
            //if (oiInfo.IsTruck != oeInfo.IsTruck)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "Truck" : "运输";
            //    oldValue = oiInfo.IsTruck ? "√" : " ×";
            //    newValue = oeInfo.IsTruck ? "√" : " ×";
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion


            //#region 备注
            //if (oiInfo.Remark != oeInfo.Remark)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "Remark" : "备注";
            //    oldValue = oiInfo.Remark;
            //    newValue = oeInfo.Remark;
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion

            #endregion 委托信息

            #region MBL信息
            OceanBusinessMBLList oiMbl = oiInfo.MBLInfo;
            OceanBusinessMBLList oeMbl = oeInfo.MBLInfo;
            if (oiMbl == null || oeMbl == null) return;

            //#region 主提单号
            //if (oiMbl.MBLNo != oeMbl.MBLNo)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "MBL No" : "主提单号";
            //    oldValue = oiMbl.MBLNo;
            //    newValue = oeMbl.MBLNo;
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion

            //#region MBL 分提单号
            //if (oiMbl.SubNo != oeMbl.SubNo)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "MBL No" : "MBL 分提单号";
            //    oldValue = oiMbl.SubNo;
            //    newValue = oeMbl.SubNo;
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion

            //#region 提货地
            //if (oiMbl.FinalWareHouseName != oeMbl.FinalDestinationName)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "MBL Final WareHouse" : "MBL提货地";
            //    oldValue = oiMbl.FinalWareHouseName;
            //    newValue = oeMbl.FinalDestinationName;
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion

            //#region 船公司
            //if (oiMbl.CarrierID != oeMbl.CarrierID)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "MBL Carrier" : "MBL船公司";
            //    oldValue = oiMbl.CarrierName;
            //    newValue = oeMbl.CarrierName;
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion

            //#region 转关号
            //if (oiMbl.ReturnLocationID != oeMbl.r)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "MBL No" : "主提单号";
            //    oldValue = oiMbl.MBLNo;
            //    newValue = oeMbl.No;
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion



            //#region 转关日
            //if (oiMbl.ReturnLocationID != oeMbl.r)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "MBL No" : "主提单号";
            //    oldValue = oiMbl.MBLNo;
            //    newValue = oeMbl.No;
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion

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

            //#region 主提单号
            //if (oiMbl.ITNO != oeMbl.BLTitleID)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "MBL No" : "主提单号";
            //    oldValue = oiMbl.MBLNo;
            //    newValue = oeMbl.No;
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion

            //#region 大船 有问题
            //if (oiMbl.VoyageID != oeMbl.VoyageID)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "Voyage" : "大船";
            //    oldValue = oiMbl.VoyageName;
            //    newValue = oeMbl.VoyageName;
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion

            //#region 驳船  有问题
            //if (oiMbl.PreVoyageID != oeMbl.PreVoyageID)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "Pre-Voyage" : "驳船";
            //    oldValue = oiMbl.PreVoyageName;
            //    newValue = oeMbl.PreVoyageName;
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion

            //#region 放货类型
            //if (oiMbl.ReleaseType != oeMbl.ReleaseType)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "Release Type" : "放货类型";
            //    oldValue = EnumHelper.GetDescription<FCMReleaseType>(oiMbl.ReleaseType, LocalData.IsEnglish);
            //    newValue = EnumHelper.GetDescription<FCMReleaseType>(oeMbl.ReleaseType, LocalData.IsEnglish);
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion

            //#region 运输条款
            //if (oiMbl.MBLTransportClauseID != oeMbl.MBLTransportClauseID)
            //{
            //    fieldDbName = "1";
            //    fieldName = LocalData.IsEnglish ? "Trans Clause" : "运输条款";
            //    oldValue = oiMbl.MBLTransportClauseName;
            //    newValue = oeMbl.MBLTransportClauseName;
            //    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            //}
            //#endregion
            #endregion

            #region HBL信息
            List<OceanBusinessHBLList> oiHblList = oiInfo.HBLList;
            List<OceanBusinessHBLList> oeHblList = oiInfo.HBLList;
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
                    AddBusiness("2", LocalData.IsEnglish ? "MBL Information" : "分提单信息", "", "", Hbls, null);
                    isHblChange = true;

                }

            }
            #endregion

            #region 集装箱信息
            List<OIBusinessContainerList> oiContainerList = oiInfo.ContainerList;
            List<OIBusinessContainerList> oeContainerList = oeInfo.ContainerList;
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

            CurrBill = FCMCommonService.GetCompareBillAndChargeInfo(CurrentSimpleBusinnessInfo.OIBusinessID, OperationType.OceanExport);
            List<DocumentInfo> beforeDocumentList = FCMCommonService.GetHistoryFileInfo(OperationID, BeforeApplyID);
            List<DocumentInfo> afterDocumentList = FCMCommonService.GetHistoryFileInfo(OperationID, AfterApplyID);
            ucDocumentHistoryPart.BindingData(afterDocumentList, beforeDocumentList);

        }

        public override bool SaveData()
        {
            return true;
        }
    }
}
