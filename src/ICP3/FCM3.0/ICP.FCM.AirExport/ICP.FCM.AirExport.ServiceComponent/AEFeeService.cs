using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.AirExport.ServiceInterface;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.FCM.AirExport.ServiceInterface.CompositeObjects;
using ICP.FAM.ServiceInterface.DataObjects;

namespace ICP.FCM.AirExport.ServiceComponent
{
    partial class AirExportService
    {
        /// <summary>
        /// 获取订单费用列表
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <returns>返回订单费用列表</returns>
        public List<AirBookingFeeList> GetAirOrderFeeList(Guid orderID)
        {
            ArgumentHelper.AssertGuidNotEmpty(orderID, "orderID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAirOrderFeeList");

                db.AddInParameter(dbCommand, "@OrderID", DbType.Guid, orderID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<AirBookingFeeList> results = (from b in ds.Tables[0].AsEnumerable()
                                                     select new AirBookingFeeList
                                                     {
                                                         ID = b.Field<Guid>("ID"),
                                                         BookingFeeID = b.Field<Guid>("BookingFeeID"),
                                                         CurrencyID = b.Field<Guid>("CurrencyID"),
                                                         AirBookingID = b.Field<Guid>("AirBookingID"),
                                                         CustomerID = b.Field<Guid>("CustomerID"),
                                                         CustomerName = b.Field<string>("CustomerName"),
                                                         ChargingCodeID = b.Field<Guid>("ChargingCodeID"),
                                                         ChargingCodeName = b.Field<string>("ChargingCodeName"),
                                                         Currency = b.Field<string>("Currency"),
                                                         Quantity = b.Field<decimal>("Quantity"),
                                                         UnitPrice = b.Field<decimal>("UnitPrice"),
                                                         Way = (FeeWay)b.Field<byte>("Way"),
                                                         Amount = b.Field<decimal>("Amount"),
                                                         Remark = b.Field<string>("Remark"),
                                                         CreateByID = b.Field<Guid>("CreateByID"),
                                                         CreateByName = b.Field<string>("CreateByName"),
                                                         CreateDate = b.Field<DateTime>("CreateDate"),
                                                         UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                         IsDirty = false,
                                                     }).ToList();

                return results;
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

        /// <summary>
        /// 保存订单费用
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="ids">ID列表</param>
        /// <param name="customerIDs">客户ID列表</param>
        /// <param name="chargingCodeIDs">费用代码ID列表</param>
        /// <param name="currencyIDs">币种列表</param>
        /// <param name="quantities">数量列表</param>
        /// <param name="unitPrices">单价列表</param>
        /// <param name="unitPrices">方向列表</param>
        /// <param name="amounts">金额列表</param>
        /// <param name="remarks">备注列表</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">更新时间-做数据版本用</param>
        /// <returns>返回ManyResult</returns>
        public ManyResult SaveAirOrderFeeList(FeeSaveRequest feeInfo)
        {
            ArgumentHelper.AssertGuidNotEmpty(feeInfo.orderID, "orderID");
            ArgumentHelper.AssertGuidNotEmpty(feeInfo.customerIDs, "customerIDs");
            ArgumentHelper.AssertGuidNotEmpty(feeInfo.chargingCodeIDs, "chargeCodeIDs");
            ArgumentHelper.AssertGuidNotEmpty(feeInfo.currencyIDs, "currencieIDs");
            ArgumentHelper.AssertGuidNotEmpty(feeInfo.saveByID, "saveByID");
            ArgumentHelper.AssertArrayLengthMatch(
                feeInfo.ids,
                feeInfo.customerIDs,
                feeInfo.chargingCodeIDs,
                feeInfo.currencyIDs,
                feeInfo.quantities,
                feeInfo.unitPrices,
                feeInfo.amounts,
                feeInfo.ways,
                feeInfo.remarks);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveAirOrderFeeList");

                string tempIds = feeInfo.ids.Join();
                string tempCustomerIds = feeInfo.customerIDs.Join();
                string tempChargeCodeIds = feeInfo.chargingCodeIDs.Join();
                string tempCurrencieIDs = feeInfo.currencyIDs.Join();
                string tempWays = feeInfo.ways.Join<FeeWay>();
                string tempQuantities = feeInfo.quantities.Join(2);
                string tempUnitPrices = feeInfo.unitPrices.Join(2);
                string tempAmounts = feeInfo.amounts.Join(2);
                string tempRemark = feeInfo.remarks.Join();
                string tempUpdateDates = feeInfo.updateDates.Join();

                db.AddInParameter(dbCommand, "@OrderID", DbType.Guid, feeInfo.orderID);
                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@CustomerIDs", DbType.String, tempCustomerIds);
                db.AddInParameter(dbCommand, "@ChargingCodeIDs", DbType.String, tempChargeCodeIds);
                db.AddInParameter(dbCommand, "@CurrencyIDs", DbType.String, tempCurrencieIDs);
                db.AddInParameter(dbCommand, "@Quantities", DbType.String, tempQuantities);
                db.AddInParameter(dbCommand, "@UnitPrices", DbType.String, tempUnitPrices);
                db.AddInParameter(dbCommand, "@Amounts", DbType.String, tempAmounts);
                db.AddInParameter(dbCommand, "@Ways", DbType.String, tempWays);
                db.AddInParameter(dbCommand, "@Remarks", DbType.String, tempRemark);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, feeInfo.saveByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyResult result = db.ManyResult(dbCommand, new string[] { "ID", "UpdateDate" });
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

        /// <summary>
        /// 删除订单费用
        /// </summary>
        /// <param name="ids">ID 列表</param>
        /// <param name="removeByID">删除人 </param>
        /// <param name="updateDates">更新时间-做数据版本用</param>
        public void RemoveAirOrderFeeList(
            Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveAirOrderFeeInfo");

                string tempIds = ids.Join();
                string tempUpdateDates = updateDates.Join();

                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                db.ExecuteNonQuery(dbCommand);
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
    }
}
