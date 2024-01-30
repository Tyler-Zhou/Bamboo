using ICP.Common.ServiceInterface.CompositeObjects;
using ICP.FAM.ServiceComponent.JSONObjects;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.CompositeObjects;
using ICP.FAM.ServiceInterface.DataObjects;
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

namespace ICP.FAM.ServiceComponent
{
    /// <summary>
    /// CSP财务服务
    /// </summary>
    public partial class FinanceService
    {
        #region 保存映射信息
        /// <summary>
        /// 保存映射信息
        /// </summary>
        /// <param name="saveRequest">保存对象</param>
        /// <returns></returns>
        public SingleResult SaveMappingInfo(SaveRequestFinanceMapping saveRequest)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.ID, "ID");
            ArgumentHelper.AssertStringNotEmpty(saveRequest.MapType, "MapType");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.SaveBy, "SaveBy");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[csp].[uspSaveFinanceMappingData]");
                //设置60秒超时
                dbCommand.CommandTimeout = 60;

                #region 构建参数
                db.AddInParameter(dbCommand, "@ID", DbType.Guid, saveRequest.ID);
                db.AddInParameter(dbCommand, "@MapID", DbType.Int32, saveRequest.MapID);
                db.AddInParameter(dbCommand, "@MapType", DbType.String, saveRequest.MapType);
                db.AddInParameter(dbCommand, "@IsRemove", DbType.Boolean, saveRequest.IsRemove);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.SaveBy);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, saveRequest.UpdateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                #endregion

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID" });
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

        #region 保存CSP账单
        /// <summary>
        /// 保存CSP账单
        /// </summary>
        /// <param name="saveRequest"></param>
        private void SaveBillForCSP(SaveRequestBill saveRequest)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.SaveByID, "SaveBy");
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetCSPBillMapping");

                db.AddInParameter(dbCommand, "@BillID", DbType.Guid, saveRequest.ID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count <= 0)
                {
                    return;
                }
                
                if(saveRequest.IsRemove)
                {
                    BillStatusForCSPAPI saveRequestForAPI = (from item in ds.Tables[0].AsEnumerable()
                                                             select new BillStatusForCSPAPI
                                                       {
                                                           id = item.Column<int>("BillMapID"),
                                                           newStatus = CSP_BILL_STATUS.Voided,
                                                       }).SingleOrDefault();
                    SaveBillStatusForCSPAPI(saveRequestForAPI);
                    SaveMappingInfo(new SaveRequestFinanceMapping() { ID = saveRequest.ID.Value, MapID = saveRequestForAPI.id, MapType = "fam.Bills", IsRemove = saveRequest.IsRemove, SaveBy = saveRequest.SaveByID, UpdateDate = DateTime.Now });
                }else
                {
                    BillForCSPAPI saveRequestForAPI = (from item in ds.Tables[0].AsEnumerable()
                                                       select new BillForCSPAPI
                                                       {
                                                           id = item.Column<int>("BillMapID"),
                                                           customerId = item.Column<int>("CustomerMapID"),
                                                           shipmentId = item.Column<int>("OperationMapID"),
                                                           shipmentItemId = item.Column<int>("BLMapID"),
                                                           billNo = item.Column<string>("BillNo"),
                                                           issuedTime = item.Column<DateTime>("AccountDate").ToString("yyyy-MM-dd HH:mm:ss"),
                                                           dueTime = item.Column<DateTime>("DueDate").ToString("yyyy-MM-dd HH:mm:ss"),
                                                           description = item.Column<string>("Remark"),
                                                       }).SingleOrDefault();
                    BillForCSPAPI responseResult = SaveBillForCSPAPI(saveRequestForAPI);
                    SaveMappingInfo(new SaveRequestFinanceMapping() { ID = saveRequest.ID.Value, MapID = responseResult.id, MapType = "fam.Bills", IsRemove = saveRequest.IsRemove, SaveBy = saveRequest.SaveByID, UpdateDate = DateTime.Now });
                }
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

        #region 保存CSP账单费用
        /// <summary>
        /// 保存CSP账单费用
        /// </summary>
        /// <param name="saveRequest"></param>
        private void SaveBillFeeForCSP(SaveRequestBill saveRequest)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.SaveByID, "SaveBy");
            try
            {
                if(saveRequest.FeeIDs.Any(fItem=>fItem.IsNullOrEmpty()))
                {
                    return;
                }
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fam].[uspGetCSPBillFeeMapping]");

                db.AddInParameter(dbCommand, "@BillID", DbType.Guid, saveRequest.ID);
                db.AddInParameter(dbCommand, "@FeeIDs", DbType.String, saveRequest.FeeIDs.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count <= 0)
                {
                    return;
                }

                List<ChargeItemForCSPAPI> chargeItems = (from item in ds.Tables[0].AsEnumerable()
                               select new ChargeItemForCSPAPI
                               {
                                   id = item.Column<int>("FeeMapID"),
                                   FeeID = item.Column<Guid>("FeeID"),
                                   billId = item.Column<int>("BillMapID"),
                                   customerId = item.Column<int>("CustomerMapID"),
                                   chargeType = (FeeWay)item.Column<byte>("FeeType"),
                                   chargingCodeId = item.Column<int>("ChargingCodeMapID"),
                                   unitPrice = item.Column<decimal>("UnitPrice"),
                                   quantity = item.Column<decimal>("Quantity"),
                                   currencyId = item.Column<int>("CurrencyMapID"),
                                   description = item.Column<string>("FeeDescription"),
                                   unitId = item.Column<int>("UnitMapID"),
                               }).ToList();
                foreach (ChargeItemForCSPAPI chargeItem in chargeItems)
                {
                    ChargeItemForCSPAPI responseResult = SaveChargeItemForCSPAPI(chargeItem);
                    SaveMappingInfo(new SaveRequestFinanceMapping() { ID = chargeItem.FeeID, MapID = responseResult.id, MapType = "fam.Charges", SaveBy = saveRequest.SaveByID, UpdateDate = DateTime.Now });
                }
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

        #region 保存CSP账单支付记录
        /// <summary>
        /// 保存CSP账单支付记录
        /// </summary>
        /// <param name="saveRequest"></param>
        private void SaveBillPaymentRecordForCSP(SaveRequestCheck saveRequest)
        {
            try
            {
                if (saveRequest.ID.IsNullOrEmpty())
                {
                    return;
                }
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fam].[uspGetCSPBillPaymentRecordMapping]");

                db.AddInParameter(dbCommand, "@CheckID", DbType.Guid, saveRequest.ID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count <= 0)
                {
                    return;
                }

                List<PaymentRecordForCSPAPI> paymentRecords = (from item in ds.Tables[0].AsEnumerable()
                               select new PaymentRecordForCSPAPI
                               {
                                   id = item.Column<int>("CheckMapID"),
                                   CheckItemID = item.Column<Guid>("CheckItemID"),
                                   customerId = item.Column<int>("CustomerMapID"),
                                   chargeItemId = item.Column<int>("ChargeItemMapID"),
                                   currencyId = item.Column<int>("CurrencyMapID"),
                                   payAmount = item.Column<decimal>("PayAmount"),
                                   checkerId = item.Column<int>("CheckerMapID"),
                                   bankDate = item.Column<DateTime>("BankDate").ToString("yyyy-MM-dd HH:mm:ss"),
                               }).ToList();
                foreach (PaymentRecordForCSPAPI paymentRecord in paymentRecords)
                {
                    PaymentRecordForCSPAPI responseResult = SavePaymentRecordForCSPAPI(paymentRecord);
                    SaveMappingInfo(new SaveRequestFinanceMapping() { ID = paymentRecord.CheckItemID, MapID = responseResult.id, MapType = "fam.CheckItems", SaveBy = saveRequest.CheckBy, UpdateDate = DateTime.Now });
                }
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

        #region API Method
        /// <summary>
        /// 更新舱单(Create)
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        private BillForCSPAPI SaveBillForCSPAPI(BillForCSPAPI saveRequest)
        {
            string apiMethod = "/CSP/Billing/CreateOrUpdateBill";
            HTTPMETHOD httpMethod = HTTPMETHOD.POST;
            string apiParameter = JSONSerializerHelper.SerializeToJson(saveRequest);
            string result = PlatformAPIHelper.Execute(CSPUserID, apiMethod, httpMethod, apiParameter);
            BillForCSPAPIItem cspItem = JSONSerializerHelper.DeserializeFromJson<BillForCSPAPIItem>(result);
            if (cspItem == null || cspItem.Result == null)
                throw new Exception("获取结果集失败");
            return cspItem.Result;
        }

        /// <summary>
        /// 保存账单状态
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        private void SaveBillStatusForCSPAPI(BillStatusForCSPAPI saveRequest)
        {
            string apiMethod = "/CSP/Billing/ChangeBillStatus";
            HTTPMETHOD httpMethod = HTTPMETHOD.POST;
            string apiParameter = JSONSerializerHelper.SerializeToJson(saveRequest);
            PlatformAPIHelper.Execute(CSPUserID, apiMethod, httpMethod, apiParameter);
        }

        /// <summary>
        /// 更新舱单(Create)
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        private ChargeItemForCSPAPI SaveChargeItemForCSPAPI(ChargeItemForCSPAPI saveRequest)
        {
            string apiMethod = "/CSP/Billing/CreateOrUpdateChargeItem";
            HTTPMETHOD httpMethod = HTTPMETHOD.POST;
            string apiParameter = JSONSerializerHelper.SerializeToJson(saveRequest);
            string result = PlatformAPIHelper.Execute(CSPUserID, apiMethod, httpMethod, apiParameter);
            ChargeItemForCSPAPIItem cspItem = JSONSerializerHelper.DeserializeFromJson<ChargeItemForCSPAPIItem>(result);
            if (cspItem == null || cspItem.Result == null)
                throw new Exception("获取结果集失败");
            return cspItem.Result;
        }

        /// <summary>
        /// 保存支付记录
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        private PaymentRecordForCSPAPI SavePaymentRecordForCSPAPI(PaymentRecordForCSPAPI saveRequest)
        {
            string apiMethod = "/CSP/Billing/CreateOrUpdatePaymentRecord";
            HTTPMETHOD httpMethod = HTTPMETHOD.POST;
            string apiParameter = JSONSerializerHelper.SerializeToJson(saveRequest);
            string result = PlatformAPIHelper.Execute(CSPUserID, apiMethod, httpMethod, apiParameter);
            PaymentRecordForCSPAPIItem cspItem = JSONSerializerHelper.DeserializeFromJson<PaymentRecordForCSPAPIItem>(result);
            if (cspItem == null || cspItem.Result == null)
                throw new Exception("获取结果集失败");
            return cspItem.Result;
        }

        #endregion
    }
}
