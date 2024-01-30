#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/7/10 星期二 16:13:00
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Transactions;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using IsolationLevel = System.Transactions.IsolationLevel;

namespace ICP.FCM.Common.ServiceComponent
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FCMCommonService
    {
        #region 以事务方式保存报价单、报价价格信息
        /// <summary>
        /// 以事务方式保存报价单、报价价格信息
        /// </summary>
        /// <param name="saveRequest">OrderSaveRequest</param>
        /// <param name="rates">QPRatesSaveRequest</param>
        /// <returns></returns>
        public Dictionary<Guid, SaveResponse> SaveQuotedPriceWithTrans(QPOrderSaveRequest saveRequest,
            List<QPRatesSaveRequest> rates)
        {
            Stopwatch swQuotedPrice = Stopwatch.StartNew();
            StringBuilder operationLog = new StringBuilder();

            operationLog.Append("事务保存:QuotedPrice;");

            TransactionOptions option = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            };
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                Dictionary<Guid, SaveResponse> result = new Dictionary<Guid, SaveResponse>();
                try
                {
                    Guid newOrderId = Guid.Empty;
                    if (saveRequest != null)
                    {
                        result.Add(saveRequest.RequestId,
                            new SaveResponse
                            {
                                RequestId = saveRequest.RequestId,
                                SingleResult = SaveQuotedPriceOrderInfo(saveRequest)
                            });

                        if (saveRequest.id == Guid.Empty)
                        {
                            newOrderId = result[saveRequest.RequestId].SingleResult.GetValue<Guid>("ID");
                        }
                        operationLog.AppendFormat("{0};QPID{1};[{2}ms];", saveRequest.id == Guid.Empty ? "New" : "Edit",
                            newOrderId, swQuotedPrice.ElapsedMilliseconds);
                    }

                    if (rates != null)
                    {
                        Stopwatch stopwatRates = Stopwatch.StartNew();
                        foreach (QPRatesSaveRequest itemRates in rates)
                        {
                            if (saveRequest != null && saveRequest.id == Guid.Empty)
                            {
                                itemRates.qpOrderID = newOrderId;
                                itemRates.ids = new Guid?[itemRates.ids.Length];
                            }
                            result.Add(itemRates.RequestId,
                                new SaveResponse
                                {
                                    RequestId = itemRates.RequestId,
                                    ManyResult = SaveQuotedPriceRatesList(itemRates)
                                });
                        }
                        operationLog.AppendFormat("Rates[{0}ms];", stopwatRates.ElapsedMilliseconds);
                    }
                    operationLog.Append("Success;");


                    scope.Complete();
                }
                catch (Exception ex)
                {
                    operationLog.Append("Failed;");
                    throw ex;
                }
                finally
                {
                    _OperationLogService.Add(DateTime.Now, "SAVE-QP-DB", operationLog.ToString(),
                        swQuotedPrice.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
                }
                return result;
            }
        }
        #endregion

        #region 保存报价信息
        /// <summary>
        /// 保存报价信息
        /// </summary>
        /// <param name="saveRequest">报价实体对象</param>
        /// <returns></returns>
        public SingleResult SaveQuotedPriceOrderInfo(QPOrderSaveRequest saveRequest)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveQuotedPriceInfo");

                string tempCustomerDescription = SerializerHelper.SerializeToString(saveRequest.customerDescription, true, false);


                db.AddInParameter(dbCommand, "@ID", DbType.Guid, saveRequest.id);
                db.AddInParameter(dbCommand, "@No", DbType.String, saveRequest.no);
                db.AddInParameter(dbCommand, "@TransportClauseID", DbType.Guid, saveRequest.transportClauseID);
                db.AddInParameter(dbCommand, "@TargetType", DbType.Byte, saveRequest.TargetType);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, saveRequest.customerid);
                db.AddInParameter(dbCommand, "@CustomerDescription", DbType.Xml, tempCustomerDescription);
                db.AddInParameter(dbCommand, "@Commodity", DbType.String, saveRequest.commodity);
                db.AddInParameter(dbCommand, "@PaymentType", DbType.Byte, saveRequest.PaymentType);
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, saveRequest.fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, saveRequest.toDate);
                db.AddInParameter(dbCommand, "@QuoteBy", DbType.Guid, saveRequest.quoteBy);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, saveRequest.remark);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.saveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, saveRequest.updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "NO", "UpdateDate" });
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

        #region 作废报价信息
        /// <summary>
        /// 作废报价信息
        /// </summary>
        /// <param name="ID">报价ID</param>
        /// <param name="isCancel">是否作废</param>
        /// <param name="removeByID">作废人</param>
        /// <param name="updateDate">更新时间</param>
        public SingleResult RemoveQuotedPriceOrderInfo(Guid ID, bool isCancel, Guid removeByID, DateTime? updateDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspCancelQuotedPrice");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, ID);
                db.AddInParameter(dbCommand, "@IsCancel", DbType.Boolean, isCancel);
                db.AddInParameter(dbCommand, "@CancelByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate" });

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

        #region 获取报价信息
        /// <summary>
        /// 获取报价信息
        /// </summary>
        /// <param name="quotedPriceID">报价ID</param>
        /// <returns></returns>
        public QuotedPriceOrderInfo GetQuotedPriceOrderInfo(Guid quotedPriceID)
        {
            ArgumentHelper.AssertGuidNotEmpty(quotedPriceID, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetQuotedPriceInfo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, quotedPriceID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                if (ds.Tables[0].Rows.Count > 1)
                {
                    throw new Exception("Found more than 1 Quoted Price. It's impossible!");
                }

                QuotedPriceOrderInfo result = BulidQuotedPriceOrderInfoByDataSet(ds).SingleOrDefault();

                //包含费用子表
                if (ds.Tables.Count > 1)
                {
                    result.RatesList = ConvertTableToQuotedPriceRatesList(ds.Tables[1]);
                }
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

        #region 获取报价列表
        /// <summary>
        /// 获取报价列表
        /// </summary>
        /// <param name="no">业务号</param>
        /// <param name="customerName">客户名</param>
        /// <param name="isSure">是否已经确认</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="quoteBy">报价人</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回报价列表</returns>
        public List<QuotedPriceOrderList> GetQuotedPriceOrderList(string no, string customerName, bool? isSure, bool? isValid, Guid? quoteBy, DateTime? beginTime, DateTime? endTime, int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetQuotedPriceList");

                db.AddInParameter(dbCommand, "@No", DbType.String, no);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@IsSure", DbType.Boolean, isSure);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@QuoteBy", DbType.Guid, quoteBy);
                db.AddInParameter(dbCommand, "@BeginTime", DbType.Date, beginTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.Date, endTime);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<QuotedPriceOrderList> results = BulidQuotedPriceOrderListByDataSet(ds);

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
        #endregion

        #region 报价单快速搜索
        /// <summary>
        /// 报价单快速搜索
        /// </summary>
        /// <param name="noSearchType">查找单号类型</param>
        /// <param name="no">单号</param>
        /// <param name="portSearchType">港口搜索类型</param>
        /// <param name="portName">港口名称</param>
        /// <param name="dateSearchType">日期搜索类型</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录行数</param>
        /// <returns></returns>
        public List<QuotedPriceOrderList> GetQuotedPriceListForFaster(
            QPNoSearchType noSearchType,
            string no,
            QPPortSearchType portSearchType,
            string portName,
            QPDateSearchType dateSearchType,
            DateTime? beginTime,
            DateTime? endTime,
            bool? isValid,
            int maxRecords)
        {
            return new List<QuotedPriceOrderList>();
        }
        #endregion

        #region 获取报价列表：刷新数据
        /// <summary>
        /// 获取报价列表：刷新数据
        /// </summary>
        /// <param name="QuotedPriceIDs">报价ID集合</param>
        /// <returns></returns>
        public List<QuotedPriceOrderList> GetQuotedPriceListByIds(Guid[] QuotedPriceIDs)
        {
            ArgumentHelper.AssertGuidNotEmpty(QuotedPriceIDs, "QuotedPriceIDs");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetQuotedPriceListByIDs");

                string tempQuotedPriceIDs = QuotedPriceIDs.Join();

                db.AddInParameter(dbCommand, "@QuotedPriceIDs", DbType.String, tempQuotedPriceIDs);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<QuotedPriceOrderList> results = BulidQuotedPriceOrderListByDataSet(ds);

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
        #endregion

        #region 获取最近该客户的报价单列表
        /// <summary>
        /// 获取最近该客户的报价单列表
        /// </summary>
        /// <param name="quotedPriceID">报价单ID</param>
        /// <param name="no">单号</param>
        /// <param name="customerID">客户ID</param>
        /// <param name="quoteBy">揽货人(报价人)</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">最大记录</param>
        /// <returns>返回最近该客户的业务数据列表</returns>
        public List<QuotedPricePartInfo> GetRecentlyQuotedPriceList(Guid? quotedPriceID, string no, Guid? customerID, Guid? quoteBy, DateTime? beginTime, DateTime? endTime, int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetRecentlyQuotedPriceList");

                db.AddInParameter(dbCommand, "@QuotedPriceID", DbType.Guid, quotedPriceID);
                db.AddInParameter(dbCommand, "@QuotedPriceNo", DbType.String, no);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@QuoteBy", DbType.Guid, quoteBy);
                db.AddInParameter(dbCommand, "@BeginTime", DbType.Date, beginTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.Date, endTime);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<QuotedPricePartInfo>();
                }

                List<QuotedPricePartInfo> results = BulidQuotedPricePartInfoByDataSet(ds);

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
        #endregion

        #region 将结果集合DataSet转换成报价明细对象
        /// <summary>
        ///将结果集合DataSet转换成报价明细对象
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <returns></returns>
        private List<QuotedPriceOrderInfo> BulidQuotedPriceOrderInfoByDataSet(DataSet ds)
        {
            List<QuotedPriceOrderInfo> results = (from b in ds.Tables[0].AsEnumerable()
                                                  select new QuotedPriceOrderInfo
                                                  {
                                                      ID = b.Field<Guid>("ID"),
                                                      No = b.Field<string>("No"),
                                                      CustomerID = b.Field<Guid>("CustomerID"),
                                                      TargetType = (QPTargetType)b.Field<byte>("TargetType"),
                                                      CustomerName = b.Field<string>("CustomerName"),
                                                      CustomerDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("CustomerDescription")),
                                                      TransportClauseID = b.Field<Guid>("TransportClauseID"),
                                                      TransportClauseName = b.Field<string>("TransportClauseName"),
                                                      Commodity = b.Field<string>("Commodity"),
                                                      IsValid = b.Field<bool>("IsValid"),
                                                      QuoteBy = b.Field<Guid?>("QuoteBy"),
                                                      QuoteByName = b.Field<string>("QuoteByName"),
                                                      PaymentType = (QPPaymentType)b.Field<byte>("PaymentType"),
                                                      FromDate = b.Field<DateTime?>("FromDate"),
                                                      ToDate = b.Field<DateTime?>("ToDate"),
                                                      ConfirmedBy = b.Field<Guid?>("ConfirmedBy"),
                                                      ConfirmedName = b.Field<string>("ConfirmedName"),
                                                      Remark = b.Field<string>("Remark"),
                                                      CreateByID = b.Field<Guid>("CreateByID"),
                                                      CreateByName = b.Field<string>("CreateByName"),
                                                      CreateDate = b.Field<DateTime>("CreateDate"),
                                                      UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                      IsDirty = false
                                                  }).ToList();
            return results;
        }
        #endregion

        #region 将结果集合DataSet转换成报价列表对象
        /// <summary>
        ///将结果集合DataSet转换成报价列表对象
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <returns></returns>
        private List<QuotedPriceOrderList> BulidQuotedPriceOrderListByDataSet(DataSet ds)
        {
            List<QuotedPriceOrderList> results = (from b in ds.Tables[0].AsEnumerable()
                                                  select new QuotedPriceOrderList
                                                  {
                                                      ID = b.Field<Guid>("ID"),
                                                      No = b.Field<string>("No"),
                                                      TargetType = (QPTargetType)b.Field<byte>("TargetType"),
                                                      CustomerID = b.Field<Guid>("CustomerID"),
                                                      CustomerName = b.Field<string>("CustomerName"),
                                                      TransportClauseName = b.Field<string>("TransportClauseName"),
                                                      Commodity = b.Field<string>("Commodity"),
                                                      IsValid = b.Field<bool>("IsValid"),
                                                      PaymentType = (QPPaymentType)b.Field<byte>("PaymentType"),
                                                      FromDate = b.Field<DateTime?>("FromDate"),
                                                      ToDate = b.Field<DateTime?>("ToDate"),
                                                      QuoteByName = b.Field<string>("QuoteByName"),
                                                      ConfirmedName = b.Field<string>("ConfirmedName"),
                                                      CreateByID = b.Field<Guid>("CreateByID"),
                                                      CreateByName = b.Field<string>("CreateByName"),
                                                      CreateDate = b.Field<DateTime>("CreateDate"),
                                                      UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                      IsDirty = false
                                                  }).ToList();
            return results;
        }
        #endregion

        #region 将结果集合DataSet转换成报价面板显示对象
        /// <summary>
        ///将结果集合DataSet转换成报价明细对象
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <returns></returns>
        private List<QuotedPricePartInfo> BulidQuotedPricePartInfoByDataSet(DataSet ds)
        {
            List<QuotedPricePartInfo> results = (from b in ds.Tables[0].AsEnumerable()
                                                 select new QuotedPricePartInfo
                                                 {
                                                     QuotedPriceID = b.Field<Guid>("ID"),
                                                     QuotedPriceNo = b.Field<string>("No"),
                                                     CustomerID = b.Field<Guid>("CustomerID"),
                                                     CustomerName = b.Field<string>("CustomerName"),
                                                     TransportClauseID = b.Field<Guid>("TransportClauseID"),
                                                     TransportClauseName = b.Field<string>("TransportClauseName"),
                                                     Commodity = b.Field<string>("Commodity"),
                                                     POLID = b.Field<Guid>("POLID"),
                                                     POLName = b.Field<string>("POLName"),
                                                     PODID = b.Field<Guid>("PODID"),
                                                     PODName = b.Field<string>("PODName"),
                                                     PlaceOfReceiptID = b.Field<Guid?>("PlaceOfReceiptID"),
                                                     PlaceOfReceiptName = b.Field<string>("PlaceOfReceiptName"),
                                                     PlaceOfDeliveryID = b.Field<Guid>("PlaceOfDeliveryID"),
                                                     PlaceOfDeliveryName = b.Field<string>("PlaceOfDeliveryName")
                                                 }).ToList();
            return results;
        }
        #endregion

        #region 获取报价价格列表

        /// <summary>
        /// 获取报价价格列表
        /// </summary>
        /// <param name="qpOrderID">报价单ID</param>
        /// <returns>返回报价价格列表</returns>
        public List<QuotedPriceRatesList> GetQuotedPriceRatesList(Guid qpOrderID)
        {
            ArgumentHelper.AssertGuidNotEmpty(qpOrderID, "QuotedPriceOrderID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();

                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetQuotedPriceRatesList");

                db.AddInParameter(dbCommand, "@QuotedPriceID", DbType.Guid, qpOrderID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<QuotedPriceRatesList>();
                }

                return ConvertTableToQuotedPriceRatesList(ds.Tables[0]);
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

        #region 保存报价价格
        /// <summary>
        /// 保存报价价格
        /// </summary>
        /// <param name="rates">QPRatesSaveRequest</param>
        /// <returns>返回ManyResult</returns>
        public ManyResult SaveQuotedPriceRatesList(QPRatesSaveRequest rates)
        {
            ArgumentHelper.AssertGuidNotEmpty(rates.qpOrderID, "QuotedPriceOrderID");
            ArgumentHelper.AssertGuidNotEmpty(rates.saveByID, "saveByID");
            ArgumentHelper.AssertArrayLengthMatch(
                rates.ids,
                rates.polIDs,
                rates.podIDs,
                rates.placeOfReceiptIDs,
                rates.placeOfDeliveryIDs,
                rates.tts,
                rates.unit20,
                rates.unit40,
                rates.unit40HQ,
                rates.unit45);

            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveQuotedPriceRatesList");
                dbCommand.CommandTimeout = 60;

                string tempIds = rates.ids.Join();
                string tempPOLIds = rates.polIDs.Join();
                string tempPODIds = rates.podIDs.Join();
                string tempPlaceOfReceiptIDs = rates.placeOfReceiptIDs.Join();
                string tempPlaceOfDeliveryIDs = rates.placeOfDeliveryIDs.Join();
                string tempCarriers = rates.carriers.Join();
                string temptts = rates.tts.Join();
                string tempUnit20 = rates.unit20.Join();
                string tempUnit40 = rates.unit40.Join();
                string tempUnit40HQ = rates.unit40HQ.Join();
                string tempUnit45 = rates.unit45.Join();
                string tempSurcharges = rates.surcharges.Join();

                string tempUpdateDates = rates.updateDates.Join();

                db.AddInParameter(dbCommand, "@QuotedPriceID", DbType.Guid, rates.qpOrderID);
                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@PlaceOfReceiptIDs", DbType.String, tempPlaceOfReceiptIDs);
                db.AddInParameter(dbCommand, "@POLIDs", DbType.String, tempPOLIds);
                db.AddInParameter(dbCommand, "@PODIDs", DbType.String, tempPODIds);
                db.AddInParameter(dbCommand, "@PlaceOfDeliveryIDs", DbType.String, tempPlaceOfDeliveryIDs);
                db.AddInParameter(dbCommand, "@Carriers", DbType.String, tempCarriers);
                db.AddInParameter(dbCommand, "@TTs", DbType.String, temptts);
                db.AddInParameter(dbCommand, "@Unit20s", DbType.String, tempUnit20);
                db.AddInParameter(dbCommand, "@Unit40s", DbType.String, tempUnit40);
                db.AddInParameter(dbCommand, "@Unit40HQs", DbType.String, tempUnit40HQ);
                db.AddInParameter(dbCommand, "@Unit45s", DbType.String, tempUnit45);
                db.AddInParameter(dbCommand, "@Surcharges", DbType.String, tempSurcharges);

                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, rates.saveByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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

        #region 删除报价价格

        /// <summary>
        /// 删除报价价格
        /// </summary>
        /// <param name="ids">ID列表</param>
        /// <param name="removeByID">删除人 </param>
        /// <param name="updateDates">更新时间-做数据版本用</param>
        public void RemoveQuotedPriceRatesList(Guid[] ids, Guid removeByID, DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveQuotedPriceRatesInfo");

                string tempIds = ids.Join();
                string tempUpdateDates = updateDates.Join();

                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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

        #region 将结果集合DataTable转换成报价价格列表对象
        /// <summary>
        /// 将结果集合DataTable转换成报价价格列表对象
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<QuotedPriceRatesList> ConvertTableToQuotedPriceRatesList(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
                return new List<QuotedPriceRatesList>();
            List<QuotedPriceRatesList> results = (from b in dt.AsEnumerable()
                                                  select new QuotedPriceRatesList
                                                  {
                                                      ID = b.Field<Guid>("ID"),
                                                      POLID = b.Field<Guid>("POLID"),
                                                      POLName = b.Field<string>("POLName"),
                                                      PODID = b.Field<Guid>("PODID"),
                                                      PODName = b.Field<string>("PODName"),
                                                      PlaceOfReceiptID = b.Field<Guid?>("PlaceOfReceiptID"),
                                                      PlaceOfReceiptName = b.Field<string>("PlaceOfReceiptName"),
                                                      PlaceOfDeliveryID = b.Field<Guid>("PlaceOfDeliveryID"),
                                                      PlaceOfDeliveryName = b.Field<string>("PlaceOfDeliveryName"),
                                                      Carrier = b.Field<string>("Carrier"),
                                                      TT = b.Field<short>("TT"),
                                                      Unit20 = b.Field<int>("Unit20"),
                                                      Unit40 = b.Field<int>("Unit40"),
                                                      Unit40HQ = b.Field<int>("Unit40HQ"),
                                                      Unit45 = b.Field<int>("Unit45"),
                                                      SurchargeDescription = b.Field<string>("SurchargeDescription"),
                                                      CreateByID = b.Field<Guid>("CreateByID"),
                                                      CreateDate = b.Field<DateTime>("CreateDate"),
                                                      UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                      IsDirty = false,
                                                  }).ToList();

            return results;
        }
        #endregion

        #region 获取订单报表数据
        /// <summary>
        /// 获取订单报表数据
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <returns></returns>
        public QPOrderReportData GetQPOrderReportData(Guid orderID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetQuotedPriceOrderReportData");

                db.AddInParameter(dbCommand, "@QuotedPriceID", DbType.Guid, orderID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                QPOrderReportData result = (from b in ds.Tables[0].AsEnumerable()
                                            select new QPOrderReportData
                                            {
                                                ID = b.Field<Guid>("ID"),
                                                No = b.Field<string>("No"),
                                                TargetTypeName = b.Field<string>("TargetTypeName"),
                                                Customer = b.Field<string>("Customer"),
                                                CustomerDesc = SerializerHelper.DeserializeFromString<CustomerDescription>(typeof(CustomerDescription), b.Field<string>("CustomerDescription")),
                                                TransportClause = b.Field<string>("TransportClause"),
                                                PaymentTypeName = b.Field<string>("PaymentTypeName"),
                                                Commodity = b.Field<string>("Commodity"),
                                                Remark = b.Field<string>("Remark"),
                                                QuoteBy = b.Field<string>("QuoteBy"),
                                                QuoteReferenceNumber = b.Field<string>("QuoteReferenceNumber"),
                                                QuoteTelephone = b.Field<string>("QuoteTelephone"),
                                                QuoteFax = b.Field<string>("QuoteFax"),
                                                QuoteEMail = b.Field<string>("QuoteEMail"),
                                                QuoteByDescription = b.Field<string>("QuoteByDescription"),
                                                Terms = b.Field<string>("Terms"),
                                                EffectiveStartDate = b.Field<string>("EffectiveStartDate"),
                                                ValidityDate = b.Field<string>("ValidityDate"),
                                                RequestDate = b.Field<string>("RequestDate"),
                                                RatesList = (from f in ds.Tables[1].AsEnumerable()
                                                             select new QPRatesReportData
                                                             {
                                                                 ID = f.Field<Guid>("RateID"),
                                                                 PlaceOfReceiptName = f.Field<string>("PlaceOfReceipt"),
                                                                 POLName = f.Field<string>("POLName"),
                                                                 PODName = f.Field<string>("PODName"),
                                                                 PlaceOfDeliveryName = f.Field<string>("PlaceOfDelivery"),
                                                                 Carrier = f.Field<string>("Carrier"),
                                                                 TT = f.Field<string>("TT"),
                                                                 Unit20 = f.Field<string>("Unit20"),
                                                                 Unit40 = f.Field<string>("Unit40"),
                                                                 Unit40HQ = f.Field<string>("Unit40HQ"),
                                                                 Unit45 = f.Field<string>("Unit45"),
                                                                 SurchargeDescription = f.Field<string>("SurchargeDescription"),
                                                             }).ToList(),
                                            }).SingleOrDefault();

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
    }
}
