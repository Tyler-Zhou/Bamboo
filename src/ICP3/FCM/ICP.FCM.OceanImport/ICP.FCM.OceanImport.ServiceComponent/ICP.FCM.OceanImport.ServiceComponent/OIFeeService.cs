using System;
using System.Collections.Generic;
using System.Linq;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FAM.ServiceInterface.DataObjects;

namespace ICP.FCM.OceanImport.ServiceComponent
{
    partial class OceanImportService
    {
        #region 根据订单ID获得订单费用列表
        /// <summary>
        /// 获取订单费用列表
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <returns>返回订单费用列表</returns>
        public List<OceanImportFeeList> GetOIOrderFeeList(Guid orderID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOIOrderFeeList");

                db.AddInParameter(dbCommand, "@OrderID", DbType.Guid, orderID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                return ConvertTableToOceanImportFeeList(ds.Tables[0]);
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
        /// 获取订单费用列表
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="companyID">订单口岸ID</param>
        /// <returns>返回订单费用列表</returns>
        public List<OceanImportFeeList> GetOIOrderFeeList(Guid orderID,Guid companyID)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyID, "companyID");
            try
            {
                bool isDefaultDB = CompanyHelper.IsDefaultServer(companyID, _FrameworkInitializeService);
                Database db = DatabaseFactoryHelper.CreateDatabase(isDefaultDB);
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOIOrderFeeList");

                db.AddInParameter(dbCommand, "@OrderID", DbType.Guid, orderID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                return ConvertTableToOceanImportFeeList(ds.Tables[0]);
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

        private List<OceanImportFeeList> ConvertTableToOceanImportFeeList(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
            {
                return new List<OceanImportFeeList>();
            }
            List<OceanImportFeeList> list = (from b in dt.AsEnumerable()
                                             select new OceanImportFeeList
                                             {
                                                 ID = b.Field<Guid>("ID"),
                                                 BookingFeeID = b.Field<Guid>("BookingFeeID"),
                                                 OIBookingID = b.Field<Guid>("OIBookingID"),
                                                 CustomerID = b.Field<Guid>("CustomerID"),
                                                 CustomerName = b.Field<String>("CustomerName"),
                                                 ChargingCodeID = b.Field<Guid>("ChargingCodeID"),
                                                 ChargingCodeName = b.Field<String>("ChargingCodeName"),
                                                 CurrencyID = b.Field<Guid>("CurrencyID"),
                                                 Currency = b.Field<String>("Currency"),
                                                 Quantity = b.Field<Decimal>("Quantity"),
                                                 UnitPrice = b.Field<Decimal>("UnitPrice"),
                                                 Way = (FeeWay)b.Field<Byte>("Way"),
                                                 Amount = b.Field<Decimal>("Amount"),
                                                 Remark = b.Field<String>("Remark"),
                                                 CreateByID = b.Field<Guid>("CreateByID"),
                                                 CreateByName = b.Field<String>("CreateByName"),
                                                 CreateDate = b.Field<DateTime>("CreateDate"),
                                                 UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                             }).ToList<OceanImportFeeList>();
            return list;
        }
        
        #endregion

        #region 保存费用信息
        /// <summary>
        /// 保存费用信息
        /// </summary>
        /// <param name="feeInfo">费用信息</param>
        /// <returns></returns>
        public ManyResult SaveOIOrderFeeList(FeeSaveRequest feeInfo)
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
                Database db = null;
                if (feeInfo.companyID == Guid.Empty)
                {
                    db = DatabaseFactory.CreateDatabase();
                }
                else
                {
                    bool isDefaultDB = CompanyHelper.IsDefaultServer(feeInfo.companyID, _FrameworkInitializeService);
                    db = DatabaseFactoryHelper.CreateDatabase(isDefaultDB);
                }
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveOIOrderFeeList");

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
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.isEnglish);

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
        #endregion

        #region 删除费用信息
        /// <summary>
        /// 删除订单费用
        /// </summary>
        /// <param name="ids">ID 列表</param>
        /// <param name="removeByID">删除人 </param>
        /// <param name="updateDates">更新时间-做数据版本用</param>
        public void RemoveOIOrderFeeList(Guid[] ids,Guid removeByID,DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveOIOrderFeeList");

                string tempIds = ids.Join();
                string tempUpdateDates = updateDates.Join();

                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

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

        public void RemoveOIOrderFeeList(Guid[] ids,Guid companyID, Guid removeByID, DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");
            ArgumentHelper.AssertGuidNotEmpty(companyID, "companyID");

            try
            {
                bool isDefaultDB = CompanyHelper.IsDefaultServer(companyID, _FrameworkInitializeService);
                Database db = DatabaseFactoryHelper.CreateDatabase(isDefaultDB);
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveOIOrderFeeList");

                string tempIds = ids.Join();
                string tempUpdateDates = updateDates.Join();

                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

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
        #endregion
     }
}
