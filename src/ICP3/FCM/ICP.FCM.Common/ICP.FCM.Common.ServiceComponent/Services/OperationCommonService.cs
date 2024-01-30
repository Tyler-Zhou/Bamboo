using System;
using System.Collections.Generic;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using CommonData = ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using System.Data;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace ICP.FCM.Common.ServiceComponent
{
    /// <summary>
    /// 业务公共信息接口
    /// </summary>
    public partial class FCMCommonService
    {
        /// <summary>
        /// 获取业务公共信息
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">表单类型</param>
        /// <returns>返回业务公共信息</returns>
        public OperationCommonInfo GetOperationCommonInfo(Guid operationID, OperationType operationType)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationCommonInfo");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
                db.AddInParameter(dbCommand, "@OperationType", DbType.Int16, operationType);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                OperationCommonInfo result = (from b in ds.Tables[0].AsEnumerable()
                                              select new OperationCommonInfo
                                              {
                                                  OperationID = b.Field<Guid>("OperationID"),
                                                  OperationNo = b.Field<string>("OperationNo"),
                                                  FreightID = b.Field<Guid?>("FreightID"),
                                                  OperationDate = b.Field<DateTime>("OperationDate"),
                                                  ETD = b.Field<DateTime>("ETD"),
                                                  ETA = b.Field<DateTime>("ETA"),
                                                  CompanyID = b.Field<Guid>("CompanyID"),
                                                  OperationType = operationType,
                                                  IsAPCCfm =b.Field<Boolean>("IsAPCCfm"),
                                                  AgentID = b.Field<Guid?>("AgentID"),
                                                  AgentOfCarrierID = b.Field<Guid?>("AgentOfCarrierID"),
                                                  AdjustmentAmount = b.Field<decimal>("AdjustmentAmount"),
                                                  AdjustmentCurrencyID = b.Field<Guid?>("AdjustmentCurrencyID"),
                                                  AdjustmentCurrencyName = b.Field<string>("AdjustmentCurrencyName"),
                                                  ECommerceAmount = b.Field<decimal>("ECommerceAmount"),
                                                  ECommerceCurrencyID = b.Field<Guid?>("ECommerceCurrencyID"),
                                                  ECommerceCurrencyName = b.Field<string>("ECommerceCurrencyName"),

                                                  Forms = (from f in ds.Tables[1].AsEnumerable()
                                                           select new FormData
                                                           {
                                                               ID = f.Field<Guid>("FormID"),
                                                               No = f.Field<string>("No"),
                                                               Type = (FormType)f.Field<byte>("FormType"),
                                                               CntQty = f.Field<Int32>("ContainerQty")
                                                           }).ToList(),
                                                  TradeCustomers = (from c in ds.Tables[2].AsEnumerable()
                                                                    select new OperationCustomer
                                                                    {
                                                                        ID = c.Field<Guid>("ID"),
                                                                        CName = c.Field<String>("CName"),
                                                                        EName = c.Field<String>("EName"),
                                                                        Term = c.Field<int>("Term"),
                                                                        Type = (CommonData.CustomerType)c.Field<byte>("CustomerType"),
                                                                        IsAgent = c.Field<bool>("IsAgent"),
                                                                        IsCustomer = c.Field<bool>("IsCustomer")
                                                                    }).ToList()
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

        /// <summary>
        /// 获取业务公共信息列表
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="accountDate">计费日期</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="operationType">业务类型</param>
        /// <returns>返回业务公共信息列表</returns>
        public List<OperationCommonInfo> GetOperationCommonInfoList(DateTime? beginTime, DateTime? endTime
            , DateTime accountDate, string operationNo, OperationType operationType)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationCommonInfoList");

                db.AddInParameter(dbCommand, "@BeginTime", DbType.DateTime, beginTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.String, endTime);
                db.AddInParameter(dbCommand, "@AccountDate", DbType.String, accountDate);
                db.AddInParameter(dbCommand, "@OperationNo", DbType.String, operationNo);
                db.AddInParameter(dbCommand, "@OperationType", DbType.Int16, operationType);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                
                List<OperationCommonInfo> result = (from b in ds.Tables[0].AsEnumerable()
                                              select new OperationCommonInfo
                                              {
                                                  OperationID = b.Field<Guid>("OperationID"),
                                                  OperationNo = b.Field<string>("OperationNo"),
                                                  OperationDate = b.Field<DateTime>("OperationDate"),
                                                  CompanyID = b.Field<Guid>("CompanyID"),
                                                  OperationType = operationType,
                                                  Forms = (from f in ds.Tables[1].AsEnumerable()
                                                           where b.Field<Guid>("OperationID").Equals(f.Field<Guid>("OperationID"))
                                                           select new FormData
                                                           {
                                                               OperationID = f.Field<Guid>("OperationID"),
                                                               ID = f.Field<Guid>("FormID"),
                                                               No = f.Field<string>("No"),
                                                               Type = (FormType)f.Field<byte>("FormType"),
                                                               CntQty = f.Field<Int32>("ContainerQty")
                                                           }).ToList()
                                              }).ToList();

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
        /// 获取公司配置客户信息
        /// </summary>
        /// <param name="companyID">口岸ID</param>
        /// <returns></returns>
        public List<ConfigureCustomerInfo> GetConfigureCustomers(Guid companyID)
        {

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetConfigureCustomers");
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<ConfigureCustomerInfo> results = (from b in ds.Tables[0].AsEnumerable()
                                                       select new ConfigureCustomerInfo
                                              {
                                                  CustomerID = b.Field<Guid>("CustomerID"),
                                                  CustomerCname = b.Field<String>("CustomerCname"),
                                                  CustomerEname = b.Field<String>("CustomerEname"),
                                                  BLTitle = b.Field<String>("BLTitle"),
                                                  CAScacCode = b.Field<String>("CAScacCode"),
                                                  USScacCode = b.Field<String>("USScacCode"),
                                                  IsDefault = b.Field<bool>("IsDefault"),

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
    }
}
