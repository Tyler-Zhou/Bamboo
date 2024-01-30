using ICP.Common.ServiceInterface.CompositeObjects;
using ICP.FCM.Common.ServiceComponent.JSONObjects;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.CompositeObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Platform;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ICP.FCM.Common.ServiceComponent
{
    /// <summary>
    /// 采购单服务
    /// </summary>
    public partial class FCMCommonService
    {
        /// <summary>
        ///  保存导入的采购单
        /// </summary>
        /// <param name="saveRequests">保存集合</param>
        /// <returns></returns>
        public ManyResult SaveImportPurchaseOrderItem(List<SaveRequestPurchaseOrderItem> saveRequests)
        {
            ManyResult results = new ManyResult();
            foreach (SaveRequestPurchaseOrderItem saveRequest in saveRequests)
            {
                if (saveRequest != null)
                {
                    List<PurchaseOrderItemForCSPAPI> poItems = SavePurchaseOrderItemForCSP(saveRequest);
                    foreach (PurchaseOrderItemForCSPAPI item in poItems)
                    {
                        SingleResult result = new SingleResult();
                        result.Add("PurchaseOrderID", item.orderId);
                        result.Add("PurchaseOrderItemID", item.itemId);
                        result.Add("StockKeepingUnit", item.sku);
                        results.Items.Add(result);
                    }
                    
                }
            }
            return results;
        }

        /// <summary>
        ///  保存导入的采购单
        /// </summary>
        /// <param name="searchParameter">保存集合</param>
        /// <returns></returns>
        public List<PurchaseOrderItem> SearchPurchaseOrderItemByShipmentInfo(SearchParameterOrderItemByShipment searchParameter)
        {
            List<PurchaseOrderItemForCSPAPI> results = SearchOrderItemByShipmentInfo(searchParameter);
            return GetCSPPOItemAPIMappingData(results);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        private List<PurchaseOrderItem> GetCSPPOItemAPIMappingData(List<PurchaseOrderItemForCSPAPI> datas)
        {
            try
            {
                if (datas == null || datas.Count() <= 0)
                    return new List<PurchaseOrderItem>();
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetCSPPOItemListForJSON]");
                dbCommand.CommandTimeout = 10 * 60;
                db.AddInParameter(dbCommand, "@ShipmentItemContainerIds", DbType.String, datas.Select(fItem => fItem.shipmentItemContainerId).ToArray().Join());
                db.AddInParameter(dbCommand, "@OrderIds", DbType.String, datas.Select(fItem => fItem.orderId).ToArray().Join());
                db.AddInParameter(dbCommand, "@ItemIds", DbType.String, datas.Select(fItem => fItem.itemId).ToArray().Join());
                db.AddInParameter(dbCommand, "@OrderNumbers", DbType.String, datas.Select(fItem => fItem.orderNumber).ToArray().Join());
                db.AddInParameter(dbCommand, "@ProductNames", DbType.String, datas.Select(fItem => fItem.productName).ToArray().Join());
                db.AddInParameter(dbCommand, "@Skus", DbType.String, datas.Select(fItem => fItem.sku).ToArray().Join());
                db.AddInParameter(dbCommand, "@Mpns", DbType.String, datas.Select(fItem => fItem.mpn).ToArray().Join());
                db.AddInParameter(dbCommand, "@Cartons", DbType.String, datas.Select(fItem => fItem.cartons).ToArray().Join());
                db.AddInParameter(dbCommand, "@Units", DbType.String, datas.Select(fItem => fItem.units).ToArray().Join());
                db.AddInParameter(dbCommand, "@UnitCosts", DbType.String, datas.Select(fItem => fItem.unitCost).ToArray().Join());
                db.AddInParameter(dbCommand, "@Weights", DbType.String, datas.Select(fItem => fItem.weight).ToArray().Join());
                db.AddInParameter(dbCommand, "@Volumes", DbType.String, datas.Select(fItem => fItem.volume).ToArray().Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                List<PurchaseOrderItem> result = new List<PurchaseOrderItem>();
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count <= 0)
                {
                    return result;
                }
                result = ConvertCSPPOItemForCSPByTable(ds.Tables[0]);
                return result;

            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<PurchaseOrderItem> ConvertCSPPOItemForCSPByTable(DataTable dt)
        {
            List<PurchaseOrderItem> result = (from item in dt.AsEnumerable()
                                              select new PurchaseOrderItem
                                            {
                                                ID = item.Column<int>("BillOfLadingNO"),
                                                BillOfLadingNO = item.Column<string>("BillOfLadingNO"),
                                                ContainerNO = item.Column<string>("ContainerNO"),
                                                ManufacturerPartNumber = item.Column<string>("ManufacturerPartNumber"),
                                                PurchaseOrderID = item.Column<int>("PurchaseOrderID"),
                                                PurchaseOrderNO = item.Column<string>("PurchaseOrderNO"),
                                                ProductName = item.Column<string>("ProductName"),
                                                StockKeepingUnit = item.Column<string>("StockKeepingUnit"),
                                                CartonCount = item.Column<int>("CartonCount"),
                                                Quantity = item.Column<decimal>("Quantity"),
                                                UnitCostPrice = item.Column<decimal>("UnitCostPrice"),
                                                Weight = item.Column<decimal>("Weight"),
                                                Volume = item.Column<decimal>("Volume"),
                                            }).ToList();
            return result;
        }

    }
}
