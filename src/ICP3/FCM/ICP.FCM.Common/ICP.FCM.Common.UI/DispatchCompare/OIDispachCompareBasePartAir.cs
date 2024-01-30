using System;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.OceanImport.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.Common.UI.Document;
using ICP.FCM.AirImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.FCM.Common.UI.DispatchCompare
{
    /// <summary>
    /// 
    /// </summary>
    public partial class OIDispachCompareBasePartAir : DispatchDocumentCompareNew
    {
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public IAirImportService aiService
        {
            get
            {
                return ServiceClient.GetService<IAirImportService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public OIDispachCompareBasePartAir()
        {
            InitializeComponent();

            this.Load += delegate
            {

            };
            this.Disposed += delegate
            {
                this.dispatchCompareObject = null;

                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
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
            AirBusinessInfo oiInfo = aiService.GetBusinessInfo(OldOperationID); 
            dispatchCompareObject.OIBillInfo = finService.GetBillInfos(OldOperationID);
            dispatchCompareObject.OEBillInfo = finService.GetBillInfos(NewOperationID);

            dispatchCompareObject.OEBookingInfo = oiService.GetDispatchBookingInfo(NewOperationID, fileLog, LocalData.UserInfo.LoginID);

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

            #region 品名
            if (!string.IsNullOrEmpty(oiInfo.Commodity) && !string.IsNullOrEmpty(oeInfo.Commodity))
            {
                if (oiInfo.Commodity != oeInfo.Commodity)
                {
                    fieldDbName = "1";
                    fieldName = LocalData.IsEnglish ? "Commodity" : "品名";
                    oldValue = oiInfo.Commodity;
                    newValue = oeInfo.Commodity.Replace("\r", "").Replace("\n", "");
                    AddBusiness(fieldDbName, fieldName, oldValue, newValue);
                }
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

            #region 交货地
            if (oiInfo.PlaceOfDeliveryID != oeInfo.PlaceOfDeliveryID)
            {
                fieldDbName = "1";
                fieldName = LocalData.IsEnglish ? "Delivery" : "交货地";
                oldValue = oiInfo.PlaceOfDeliveryName;
                newValue = oeInfo.PlaceOfDeliveryName;
                AddBusiness(fieldDbName, fieldName, oldValue, newValue);
            }
            #endregion


            #endregion

            CurrBill = FCMCommonService.DispatchCompareBillAndCharge(NewOperationID, OldOperationID, fileLog, OperationType.AirExport);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool SaveData()
        {
            return base.SaveData();
        }
    }
}
