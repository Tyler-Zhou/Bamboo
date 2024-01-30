using ICP.Common.ServiceInterface.DataObjects;
using ICP.DataCache.ServiceInterface;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.CompositeObjects;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.DataObjects.SaveRequests;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Message.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Transactions;

namespace ICP.FAM.ServiceComponent
{
    /// <summary>
    /// 帐单部分
    /// </summary>
    public partial class FinanceService
    {
        #region GetBillList(获取帐单列表)

        /// <summary>
        /// 获取帐单列表
        /// </summary>
        /// <param name="companyIDs">口岸ID列表</param>
        /// <param name="invoiceNo">发票号</param>
        /// <param name="customerName">客户名称</param>
        /// <param name="billState">账单状态</param>
        /// <param name="billType">账单类型</param>
        /// <param name="isValid">是否可用</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">返回最大行行数</param>
        /// <returns>返回帐单列表</returns>
        public List<BillList> GetBillList(Guid[] companyIDs, string invoiceNo, string customerName
            , BillState? billState, BillType? billType, bool? isValid, DateTime? beginTime,
            DateTime? endTime, int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBatchBillList");

                string tempCompanyIDs = companyIDs.Join();
                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, tempCompanyIDs);
                db.AddInParameter(dbCommand, "@InvoiceNo", DbType.String, invoiceNo);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@BillState", DbType.Int16, billState);
                db.AddInParameter(dbCommand, "@BillType", DbType.Int16, billType);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@BeginTime", DbType.DateTime, beginTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, endTime);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<BillList> results = BulidBatchBillListByDataSet(ds);

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
        /// 获取帐单列表
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <returns>返回帐单列表</returns>
        public List<BillList> GetBillListByOperactioID(Guid operationID)
        {
            ArgumentHelper.AssertGuidNotEmpty(operationID, "operationID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBillList");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<BillList> results = BulidBillListByDataSet(ds);

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
        /// 获取帐单列表
        /// </summary>
        /// <param name="ids">帐单IDs</param>
        /// <returns>返回帐单列表</returns>
        public List<BillList> GetBillListByIDs(Guid[] ids)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBillListByIDs");

                string tempIds = ids.Join();

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<BillList> results = BulidBillListByDataSet(ds);

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
        private List<BillList> BulidBatchBillListByDataSet(DataSet ds)
        {
            List<BillList> results = (from b in ds.Tables[0].AsEnumerable()
                                      select new BillList
                                      {
                                          ID = b.Field<Guid>("ID"),
                                          State = (BillState)b.Field<Byte>("State"),
                                          No = b.Field<String>("No"),
                                          OperationID = b.Field<Guid>("OperationID"),
                                          OperationNo = b.Field<String>("OperationNo"),
                                          OperationType = (OperationType)b.Field<Byte>("OperationType"),
                                          FormNo = b.Field<string>("FormNo"),
                                          CustomerID = b.Field<Guid>("CustomerID"),
                                          CustomerName = b.Field<String>("CustomerName"),
                                          Type = (BillType)b.Field<Byte>("Type"),
                                          AccountDate = b.Field<DateTime>("AccountDate"),
                                          DueDate = b.Field<DateTime>("DueDate"),
                                          AmountDescription = b.Field<String>("AmountDescription"),
                                          PayAmountDescription = b.Field<String>("PayAmountDescription"),
                                          BalanceDescription = b.Field<String>("BalanceDescription"),
                                          InvoiceNo = b.Field<string>("InvoiceNo"),
                                          CheckNo = b.Field<string>("CheckNo"),
                                          BankDates = b.Field<string>("BankDates"),
                                          CheckDates = b.Field<string>("CheckDates"),
                                          CompanyID = b.Field<Guid>("CompanyID"),
                                          CompanyName = b.Field<String>("CompanyName"),
                                          CreateByID = b.Field<Guid>("CreateByID"),
                                          CreateByName = b.Field<String>("CreateByName"),
                                          CreateDate = b.Field<DateTime>("CreateDate"),
                                          UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                          VATAmout = b.Field<decimal?>("VAAmount"),
                                          IsDirty = false
                                      }).ToList();
            return results;
        }

        private List<BillList> BulidBillListByDataSet(DataSet ds)
        {
            List<BillList> results = (from b in ds.Tables[0].AsEnumerable()
                                      select new BillList
                                      {
                                          ID = b.Field<Guid>("ID"),
                                          AccountDate = b.Field<DateTime>("AccountDate"),
                                          CheckNo = b.Field<string>("CheckNo"),
                                          BankDates = b.Field<string>("BankDates"),
                                          FormNo = b.Field<string>("FormNo"),
                                          InvoiceNo = b.Field<string>("InvoiceNo"),
                                          No = b.Field<String>("No"),
                                          Type = (BillType)b.Field<Byte>("Type"),
                                          CompanyID = b.Field<Guid>("CompanyID"),
                                          CompanyName = b.Field<String>("CompanyName"),
                                          CustomerName = b.Field<String>("CustomerName"),
                                          DueDate = b.Field<DateTime>("DueDate"),
                                          CreateByID = b.Field<Guid>("CreateByID"),
                                          CreateByName = b.Field<String>("CreateByName"),
                                          CreateDate = b.Field<DateTime>("CreateDate"),
                                          State = (BillState)b.Field<Byte>("State"),
                                          AmountDescription = b.Field<String>("AmountDescription"),
                                          PayAmountDescription = b.Field<String>("PayAmountDescription"),
                                          BalanceDescription = b.Field<String>("BalanceDescription"),
                                          UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                          VATAmout = b.Field<decimal?>("VAAmount"),
                                          CheckDates = b.Field<string>("CheckDates"),
                                          //IsSpecial = b.Field<bool>("IsSpecial"),
                                          IsDirty = false,

                                          //FromType = b.Field<int?>("FromType"),     //joe add 2013-08-21
                                          CurrencyAmounts = (from c in ds.Tables[1].AsEnumerable()
                                                             where c.Field<Guid>("BillID") == b.Field<Guid>("ID")
                                                             select new CurrencyAmountData
                                                             {
                                                                 CurrencyID = c.Field<Guid>("CurrencyID"),
                                                                 CurrencyName = c.Field<string>("CurrencyName"),
                                                                 Amount = c.Field<decimal>("Amount"),
                                                             }).ToList(),
                                      }).ToList();
            return results;
        }

        #endregion

        #region GetBillInfos(获取帐单列表)
        /// <summary>
        /// 获取帐单列表
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <returns>返回帐单列表</returns>
        public List<BillInfo> GetBillInfos(Guid operationID)
        {

            ArgumentHelper.AssertGuidNotEmpty(operationID, "operationID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBillInfos");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                #region linq
                List<BillInfo> results = (from b in ds.Tables[0].AsEnumerable()
                                          select new BillInfo
                                          {
                                              ID = b.Field<Guid>("ID"),
                                              AccountDate = b.Field<DateTime>("AccountDate"),
                                              CheckNo = b.Field<string>("CheckNo"),
                                              FormNo = b.Field<string>("FormNo"),
                                              InvoiceNo = b.Field<string>("InvoiceNo"),
                                              No = b.Field<String>("No"),
                                              Type = (BillType)b.Field<Byte>("Type"),
                                              CompanyID = b.Field<Guid>("CompanyID"),
                                              CompanyName = b.Field<String>("CompanyName"),
                                              CustomerName = b.Field<String>("CustomerName"),
                                              DueDate = b.Field<DateTime>("DueDate"),
                                              CreateByID = b.Field<Guid>("CreateByID"),
                                              CreateByName = b.Field<String>("CreateByName"),
                                              CreateDate = b.Field<DateTime>("CreateDate"),
                                              State = (BillState)b.Field<Byte>("State"),
                                              AmountDescription = b.Field<String>("AmountDescription"),
                                              PayAmountDescription = b.Field<String>("PayAmountDescription"),
                                              BalanceDescription = b.Field<String>("BalanceDescription"),
                                              UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                              CustomerID = b.Field<Guid>("CustomerID"),
                                              CustomerRefNo = b.Field<string>("CustomerRefNo"),
                                              CustomerDescription = SerializerHelper.DeserializeFromString<FAMCustomerDescription>(b.Field<string>("CustomerDescription")),
                                              //VATAmout = b.Field<Decimal?>("VAAmount"),
                                              //IsSpecial = b.Field<bool>("IsSpecial"),
                                              IsDirty = false,
                                              Fees = (from f in ds.Tables[1].AsEnumerable()
                                                      where f.Field<Guid>("BillID") == b.Field<Guid>("ID")
                                                      select new ChargeList
                                                      {
                                                          ID = f.Field<Guid>("ID"),
                                                          BillID = f.Field<Guid>("BillID"),
                                                          ChargingCodeID = f.Field<Guid>("ChargingCodeID"),
                                                          ChargingCode = f.Field<String>("ChargingCode"),
                                                          CurrencyID = f.Field<Guid>("CurrencyID"),
                                                          CurrencyName = f.Field<String>("CurrencyName"),
                                                          Rate = f.Field<Decimal>("Rate"),
                                                          ContainerID = f.Field<Guid?>("ContainerID"),
                                                          UnitID = f.Field<Guid>("UnitID"),
                                                          UnitName = f.Field<String>("UnitName"),
                                                          UnitPrice = f.Field<Decimal>("UnitPrice"),
                                                          Quantity = f.Field<Decimal>("Quantity"),
                                                          Amount = f.Field<Decimal>("Amount"),
                                                          Remark = f.Field<String>("Remark"),
                                                          CreateByID = f.Field<Guid>("CreateByID"),
                                                          CreateByName = f.Field<String>("CreateByName"),
                                                          CreateDate = f.Field<DateTime>("CreateDate"),
                                                          Way = (FeeWay)f.Field<Byte>("Way"),
                                                          Type = (FeeType)f.Field<Byte>("Type"),
                                                          UpdateDate = f.Field<DateTime?>("UpdateDate"),
                                                          IsDirty = false,
                                                          FromType = f.Field<int?>("FromType"),
                                                      }).ToList(),
                                          }).ToList();
                #endregion

                return results;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region GetBillInfo
        /// <summary>
        /// 获取帐单信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回帐单信息</returns>
        public BillInfo GetBillInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBillInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                #region linq
                BillInfo result = (from b in ds.Tables[0].AsEnumerable()
                                   select new BillInfo
                                   {
                                       PayCurrencyId = b.Field<Guid?>("PayCurrencyId"),
                                       IsPadByOnecCurrency = b.Field<Guid?>("PayCurrencyId").HasValue,
                                       CustomerID = b.Field<Guid>("CustomerID"),
                                       CustomerName = b.Field<string>("CustomerName"),
                                       FormID = b.Field<Guid>("FormID"),
                                       FormType = (FormType)b.Field<Byte>("FormType"),
                                       FormNo = b.Field<string>("FormNo"),
                                       CustomerRefNo = b.Field<string>("CustomerRefNo"),
                                       Type = (BillType)b.Field<Byte>("Type"),
                                       CompanyID = b.Field<Guid>("CompanyID"),
                                       CompanyName = b.Field<string>("CompanyName"),
                                       CreateByID = b.Field<Guid>("CreateByID"),
                                       CreateByName = b.Field<string>("CreateByName"),
                                       CreateDate = b.Field<DateTime>("CreateDate"),
                                       UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                       State = (BillState)b.Field<Byte>("State"),
                                       AccountDate = b.Field<DateTime>("AccountDate"),
                                       DueDate = b.Field<DateTime>("DueDate"),
                                       ID = b.Field<Guid>("ID"),
                                       AuditorID = b.Field<Guid?>("CheckById"),
                                       AuditorEmail = b.Field<string>("CheckByEmail"),
                                       Remark = b.Field<string>("Remark"),
                                       IsVATInvoiced = b.IsNull("IsVATInvoiced") ? false : b.Field<bool>("IsVATInvoiced"),
                                       Taxrate = b.Field<Decimal?>("Taxrate"),
                                       IsSend = b.Field<bool>("IsSend"),
                                       No = b.Field<string>("No"),
                                       InvoiceNo = b.Field<string>("InvoiceNo"),
                                       CheckNo = b.Field<string>("CheckNo"),
                                       CustomerDescription = SerializerHelper.DeserializeFromString<FAMCustomerDescription>(b.Field<string>("CustomerDescription")),
                                       IsDirty = false,
                                       ShipToID = b.Field<Guid?>("ShipToID"),
                                       ShipToName = b.Field<string>("ShipToName"),
                                       OperationID = b.Field<Guid>("OperationID"),
                                       FromType = b.Field<int?>("FromType"),
                                       //  AgentFeeIsPayed = ds.Tables[2].AsEnumerable().Sum((DataRow r) => r.Field<int>("AgentFeeIsPayed"))>0?true:false,

                                       CurrencyRates = (from c in ds.Tables[1].AsEnumerable()
                                                        select new CurrencyRateData
                                                        {
                                                            CurrencyID = c.Field<Guid>("CurrencyID"),
                                                            CurrencyName = c.Field<string>("CurrencyName"),
                                                            Rate = c.Field<decimal>("Rate"),
                                                        }).ToList(),
                                       Fees = (from f in ds.Tables[2].AsEnumerable()
                                               select new ChargeList
                                               {
                                                   ID = f.Field<Guid>("ID"),
                                                   BillID = f.Field<Guid>("BillID"),
                                                   ChargingCodeID = f.Field<Guid>("ChargingCodeID"),
                                                   ChargingCode = f.Field<String>("ChargingCode"),
                                                   FeeCode = f.Field<String>("Code"),
                                                   ChargingDescription = f.Field<String>("FeeDescription"),
                                                   CurrencyID = f.Field<Guid>("CurrencyID"),
                                                   CurrencyName = f.Field<String>("CurrencyName"),
                                                   Rate = f.Field<Decimal>("Rate"),
                                                   ContainerID = f.Field<Guid?>("ContainerID"),
                                                   UnitID = f.Field<Guid>("UnitID"),
                                                   UnitName = f.Field<String>("UnitName"),
                                                   UnitPrice = f.Field<Decimal>("UnitPrice"),
                                                   Quantity = f.Field<Decimal>("Quantity"),
                                                   Amount = f.Field<Decimal>("Amount"),
                                                   Remark = f.Field<String>("Remark"),
                                                   CreateByID = f.Field<Guid>("CreateByID"),
                                                   CreateByName = f.Field<String>("CreateByName"),
                                                   CreateDate = f.Field<DateTime>("CreateDate"),
                                                   Way = (FeeWay)f.Field<Byte>("Way"),
                                                   Type = (FeeType)f.Field<Byte>("Type"),
                                                   PayAmount = f.Field<Decimal>("PayAmount"),
                                                   UpdateDate = f.Field<DateTime?>("UpdateDate"),
                                                   IsAgent = f.Field<bool>("IsAgent"),
                                                   IsSecondSale = f.Field<bool>("IsSecondSale"),
                                                   IsVATInvoiced = f.IsNull("IsVATInvoiced") ? false : f.Field<bool>("IsVATInvoiced"),
                                                   IsGST = f.IsNull("IsGst") ? false : f.Field<bool>("IsGst"),
                                                   IsDirty = false,
                                                   FromType = f.Field<int?>("FromType"),
                                                   IsDispatch = f.Field<int>("IsDispatch") > 0 ? true : false,

                                               }).ToList(),
                                       ReviseHistoryFees = (from f in ds.Tables[3].AsEnumerable()
                                                            select new ChargeList
                                                            {
                                                                ID = f.Field<Guid>("ID"),
                                                                BillID = f.Field<Guid>("BillID"),
                                                                ChargingCodeID = f.Field<Guid>("ChargingCodeID"),
                                                                ChargingCode = f.Field<String>("ChargingCode"),
                                                                FeeCode = f.Field<String>("Code"),
                                                                ChargingDescription = f.Field<String>("FeeDescription"),
                                                                CurrencyID = f.Field<Guid>("CurrencyID"),
                                                                CurrencyName = f.Field<String>("CurrencyName"),
                                                                Rate = f.Field<Decimal>("Rate"),
                                                                UnitID = f.Field<Guid>("UnitID"),
                                                                UnitName = f.Field<String>("UnitName"),
                                                                UnitPrice = f.Field<Decimal>("UnitPrice"),
                                                                Quantity = f.Field<Decimal>("Quantity"),
                                                                Amount = f.Field<Decimal>("Amount"),
                                                                Remark = f.Field<String>("Remark"),
                                                                CreateByID = f.Field<Guid>("CreateByID"),
                                                                CreateByName = f.Field<String>("CreateByName"),
                                                                CreateDate = f.Field<DateTime>("CreateDate"),
                                                                Way = (FeeWay)f.Field<Byte>("Way"),
                                                                Type = (FeeType)f.Field<Byte>("Type"),
                                                                PayAmount = f.Field<Decimal>("PayAmount"),
                                                                UpdateDate = f.Field<DateTime?>("UpdateDate"),
                                                                IsAgent = f.Field<bool>("IsAgent"),
                                                                IsSecondSale = f.Field<bool>("IsSecondSale"),
                                                                IsVATInvoiced = f.IsNull("IsVATInvoiced") ? false : f.Field<bool>("IsVATInvoiced"),
                                                                IsDirty = false,
                                                                FromType = b.Field<int?>("FromType"),
                                                            }).ToList(),
                                       //ReviseHistoryFees = (from f in ds.Tables[3].AsEnumerable()
                                       //        select new ChargeList
                                       //        {
                                       //            ID = f.Field<Guid>("ID"),
                                       //            BillID = f.Field<Guid>("BillID"),
                                       //            ChargingCodeID = f.Field<Guid>("ChargingCodeID"),
                                       //            ChargingCode = f.Field<String>("ChargingCode"),
                                       //            FeeCode = f.Field<String>("Code"),
                                       //            ChargingDescription = f.Field<String>("FeeDescription"),
                                       //            CurrencyID = f.Field<Guid>("CurrencyID"),
                                       //            CurrencyName = f.Field<String>("CurrencyName"),
                                       //            Rate = f.Field<Decimal>("Rate"),
                                       //            UnitID = f.Field<Guid>("UnitID"),
                                       //            UnitName = f.Field<String>("UnitName"),
                                       //            UnitPrice = f.Field<Decimal>("UnitPrice"),
                                       //            Quantity = f.Field<Decimal>("Quantity"),
                                       //            Amount = f.Field<Decimal>("Amount"),
                                       //            Remark = f.Field<String>("Remark"),
                                       //            CreateByID = f.Field<Guid>("CreateByID"),
                                       //            CreateByName = f.Field<String>("CreateByName"),
                                       //            CreateDate = f.Field<DateTime>("CreateDate"),
                                       //            Way = (FeeWay)f.Field<Byte>("Way"),
                                       //            Type = (FeeType)f.Field<Byte>("Type"),
                                       //            PayAmount = f.Field<Decimal>("PayAmount"),
                                       //            UpdateDate = f.Field<DateTime?>("UpdateDate"),
                                       //            IsAgent = f.Field<bool>("IsAgent"),
                                       //            IsSecondSale = f.Field<bool>("IsSecondSale"),
                                       //            IsVATInvoiced = f.IsNull("IsVATInvoiced") ? false : f.Field<bool>("IsVATInvoiced"),
                                       //            IsDirty = false,
                                       //            FromType = b.Field<int?>("FromType"),
                                       //         }).ToList(),
                                   }).SingleOrDefault();
                #endregion
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
        /// 获取帐单信息
        /// 2013-7-31 joe initial
        /// </summary>
        /// <param name="operationID">业务ID</param>
        ///<param name="billType">账单类型（1:应收,2:应付,3:代理）</param>
        /// <returns>返回帐单列表</returns>
        public List<BillInfo> GetBillListByOperactioIDAndBillType(Guid operationID, BillType billType)
        {

            ArgumentHelper.AssertGuidNotEmpty(operationID, "operationID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBillInfoByBookIDAndBillType");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
                db.AddInParameter(dbCommand, "@Type", DbType.Int16, billType);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                #region linq
                List<BillInfo> results = (from b in ds.Tables[0].AsEnumerable()
                                          select new BillInfo
                                          {
                                              ID = b.Field<Guid>("ID"),
                                              AccountDate = b.Field<DateTime>("AccountDate"),
                                              CheckNo = b.Field<string>("CheckNo"),
                                              FormNo = b.Field<string>("FormNo"),
                                              InvoiceNo = b.Field<string>("InvoiceNo"),
                                              No = b.Field<String>("No"),
                                              Type = (BillType)b.Field<Byte>("Type"),
                                              CompanyID = b.Field<Guid>("CompanyID"),
                                              CompanyName = b.Field<String>("CompanyName"),
                                              CustomerName = b.Field<String>("CustomerName"),
                                              DueDate = b.Field<DateTime>("DueDate"),
                                              CreateByID = b.Field<Guid>("CreateByID"),
                                              CreateByName = b.Field<String>("CreateByName"),
                                              CreateDate = b.Field<DateTime>("CreateDate"),
                                              State = (BillState)b.Field<Byte>("State"),
                                              AmountDescription = b.Field<String>("AmountDescription"),
                                              PayAmountDescription = b.Field<String>("PayAmountDescription"),
                                              BalanceDescription = b.Field<String>("BalanceDescription"),
                                              UpdateDate = b.Field<DateTime?>("UpdateDate"),

                                              CustomerID = b.Field<Guid>("CustomerID"),
                                              CustomerRefNo = b.Field<string>("CustomerRefNo"),
                                              CustomerDescription = SerializerHelper.DeserializeFromString<FAMCustomerDescription>(b.Field<string>("CustomerDescription")),

                                              //IsSpecial = b.Field<bool>("IsSpecial"),
                                              IsDirty = false,
                                              FromType = b.Field<int?>("FromType"),
                                              Fees = (from f in ds.Tables[1].AsEnumerable()
                                                      where f.Field<Guid>("BillID") == b.Field<Guid>("ID")
                                                      select new ChargeList
                                                      {
                                                          ID = f.Field<Guid>("ID"),
                                                          BillID = f.Field<Guid>("BillID"),
                                                          ChargingCodeID = f.Field<Guid>("ChargingCodeID"),
                                                          ChargingCode = f.Field<String>("ChargingCode"),
                                                          CurrencyID = f.Field<Guid>("CurrencyID"),
                                                          CurrencyName = f.Field<String>("CurrencyName"),
                                                          Rate = f.Field<Decimal>("Rate"),
                                                          UnitID = f.Field<Guid>("UnitID"),
                                                          UnitName = f.Field<String>("UnitName"),
                                                          UnitPrice = f.Field<Decimal>("UnitPrice"),
                                                          Quantity = f.Field<Decimal>("Quantity"),
                                                          Amount = f.Field<Decimal>("Amount"),
                                                          Remark = f.Field<String>("Remark"),
                                                          CreateByID = f.Field<Guid>("CreateByID"),
                                                          CreateByName = f.Field<String>("CreateByName"),
                                                          CreateDate = f.Field<DateTime>("CreateDate"),
                                                          Way = (FeeWay)f.Field<Byte>("Way"),
                                                          Type = (FeeType)f.Field<Byte>("Type"),
                                                          UpdateDate = f.Field<DateTime?>("UpdateDate"),
                                                          IsDirty = false,
                                                          FromType = b.Field<int?>("FromType"),
                                                      }).ToList(),
                                              ReviseHistoryFees = (from f in ds.Tables[3].AsEnumerable()
                                                                   select new ChargeList
                                                                   {
                                                                       ID = f.Field<Guid>("ID"),
                                                                       BillID = f.Field<Guid>("BillID"),
                                                                       ChargingCodeID = f.Field<Guid>("ChargingCodeID"),
                                                                       ChargingCode = f.Field<String>("ChargingCode"),
                                                                       FeeCode = f.Field<String>("Code"),
                                                                       ChargingDescription = f.Field<String>("FeeDescription"),
                                                                       CurrencyID = f.Field<Guid>("CurrencyID"),
                                                                       CurrencyName = f.Field<String>("CurrencyName"),
                                                                       Rate = f.Field<Decimal>("Rate"),
                                                                       UnitID = f.Field<Guid>("UnitID"),
                                                                       UnitName = f.Field<String>("UnitName"),
                                                                       UnitPrice = f.Field<Decimal>("UnitPrice"),
                                                                       Quantity = f.Field<Decimal>("Quantity"),
                                                                       Amount = f.Field<Decimal>("Amount"),
                                                                       Remark = f.Field<String>("Remark"),
                                                                       CreateByID = f.Field<Guid>("CreateByID"),
                                                                       CreateByName = f.Field<String>("CreateByName"),
                                                                       CreateDate = f.Field<DateTime>("CreateDate"),
                                                                       Way = (FeeWay)f.Field<Byte>("Way"),
                                                                       Type = (FeeType)f.Field<Byte>("Type"),
                                                                       PayAmount = f.Field<Decimal>("PayAmount"),
                                                                       UpdateDate = f.Field<DateTime?>("UpdateDate"),
                                                                       IsAgent = f.Field<bool>("IsAgent"),
                                                                       IsSecondSale = f.Field<bool>("IsSecondSale"),
                                                                       IsVATInvoiced = f.IsNull("IsVATInvoiced") ? false : f.Field<bool>("IsVATInvoiced"),
                                                                       IsDirty = false,
                                                                       FromType = b.Field<int?>("FromType"),
                                                                   }).ToList(),
                                          }).ToList();
                #endregion

                return results;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region   GetChargeList
        /// <summary>
        /// 获取帐单下费用列表
        /// </summary>
        /// <param name="billIDs">帐单ID</param>
        /// <returns>返回帐单下费用列表</returns>
        public List<ChargeList> GetChargeList(Guid[] billIDs)
        {
            try
            {
                string billIDList = billIDs.Join();
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetChargeList");

                db.AddInParameter(dbCommand, "@BillIDS", DbType.String, billIDList);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<ChargeList> results = (from b in ds.Tables[0].AsEnumerable()
                                            select new ChargeList
                                            {
                                                ID = b.Field<Guid>("ID"),
                                                BillID = b.Field<Guid>("BillID"),
                                                BillNo = b.Field<String>("BillNo"),
                                                ChargingCodeID = b.Field<Guid>("ChargingCodeID"),
                                                ChargingCode = b.Field<String>("ChargingCode"),
                                                CurrencyID = b.Field<Guid>("CurrencyID"),
                                                CurrencyName = b.Field<String>("CurrencyName"),
                                                Rate = b.Field<Decimal>("Rate"),
                                                ContainerID = b.Field<Guid?>("ContainerID"),
                                                UnitID = b.Field<Guid>("UnitID"),
                                                UnitName = b.Field<String>("UnitName"),
                                                UnitPrice = b.Field<Decimal>("UnitPrice"),
                                                Quantity = b.Field<Decimal>("Quantity"),
                                                BillAmount = b.Field<Decimal>("BillAmount"),
                                                Amount = b.Field<Decimal>("Amount"),
                                                PayAmount = b.Field<Decimal>("PayAmount"),
                                                Remark = b.Field<String>("Remark"),
                                                CreateByID = b.Field<Guid>("CreateByID"),
                                                CreateByName = b.Field<String>("CreateByName"),
                                                CreateDate = b.Field<DateTime>("CreateDate"),
                                                Way = (FeeWay)b.Field<Byte>("Way"),
                                                Type = (FeeType)b.Field<Byte>("Type"),
                                                UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                IsCommission = b.Field<Boolean>("IsCommission"),
                                                BillUpdateDate = b.Field<DateTime?>("BillUpdateDate"),
                                                InvoiceAmount = b.Field<Decimal>("InvoiceAmount"),
                                                IsDirty = false,
                                                FromType = b.Field<int?>("FromType"),
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
        #endregion

        #region   GetChargeListForPrintBill
        /// <summary>
        /// 获取帐单下费用列表
        /// </summary>
        /// <param name="billID">帐单ID</param>
        /// <returns>返回帐单下费用列表</returns>
        public List<ChargeList> GetChargeListForPrintBill(Guid[] billIDs, bool isEN)
        {
            try
            {
                string billIDList = billIDs.Join();
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetChargeList");

                db.AddInParameter(dbCommand, "@BillIDS", DbType.String, billIDList);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEN);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<ChargeList> results = (from b in ds.Tables[0].AsEnumerable()
                                            select new ChargeList
                                            {
                                                ID = b.Field<Guid>("ID"),
                                                BillID = b.Field<Guid>("BillID"),
                                                BillNo = b.Field<String>("BillNo"),
                                                ChargingCodeID = b.Field<Guid>("ChargingCodeID"),
                                                ChargingCode = b.Field<String>("ChargingCode"),
                                                FeeCode = b.Field<String>("Code"),
                                                ChargingDescription = b.Field<String>("FeeDescription"),
                                                CurrencyID = b.Field<Guid>("CurrencyID"),
                                                CurrencyName = b.Field<String>("CurrencyName"),
                                                Rate = b.Field<Decimal>("Rate"),
                                                ContainerID = b.Field<Guid?>("ContainerID"),
                                                UnitID = b.Field<Guid>("UnitID"),
                                                UnitName = b.Field<String>("UnitName"),
                                                UnitPrice = b.Field<Decimal>("UnitPrice"),
                                                Quantity = b.Field<Decimal>("Quantity"),
                                                Amount = b.Field<Decimal>("Amount"),
                                                PayAmount = b.Field<Decimal>("PayAmount"),
                                                Remark = b.Field<String>("Remark"),
                                                CreateByID = b.Field<Guid>("CreateByID"),
                                                CreateByName = b.Field<String>("CreateByName"),
                                                CreateDate = b.Field<DateTime>("CreateDate"),
                                                Way = (FeeWay)b.Field<Byte>("Way"),
                                                Type = (FeeType)b.Field<Byte>("Type"),
                                                UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                IsCommission = b.Field<Boolean>("IsCommission"),
                                                IsVATInvoiced = b.IsNull("IsVATInvoiced") ? false : b.Field<Boolean>("IsVATInvoiced"),
                                                IsGST = b.IsNull("IsGst") ? false : b.Field<Boolean>("IsGst"),
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
        #endregion

        #region RemoveBillInfo
        /// <summary>
        /// 删除帐单
        /// </summary>
        /// <param name="ids">帐单ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDates">数据版本</param>
        public int RemoveBillInfo(
            Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
#if DEBUG
                SaveBillForCSP(new SaveRequestBill() { ID = ids[0], IsRemove = true, SaveByID = removeByID });
#endif
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspRemoveBillInfo");

                string tempIds = ids.Join();
                string tempUpdateDates = updateDates.Join();

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                return (int)db.ExecuteScalar(dbCommand);
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

        #region RemoveChargeInfo
        /// <summary>
        /// 删除费用信息
        /// </summary>
        /// <param name="ids">费用ID列表</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDates">数据版本</param>
        public void RemoveChargeInfo(
            Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspRemoveChargeInfo");

                string tempIds = ids.Join();
                string tempUpdateDates = updateDates.Join();

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempIds);
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

        #region  SaveBillInfo

        /// <summary>
        /// 保存帐单信息
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="formID">表单ID</param>
        /// <param name="formType">表单类型</param>
        /// <param name="id">帐单ID</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="customerID">客户ID</param>
        /// <param name="shipToID">Ship To ID</param>
        /// <param name="customerDescription">客户的描述信息</param>
        /// <param name="CustomerRefNo">客户参考号</param>
        /// <param name="type">账单类型（0:应收,1:应付,2:代理）</param>
        /// <param name="accountDate">账单日</param>
        /// <param name="dueDate">到期日</param>
        /// <param name="payCurrencyId">按一种币种支付.为空时说明不是按一种币种支付的</param>
        /// <param name="auditorId">审核人ID</param>
        /// <param name="auditorEmail">审核人邮件地址</param>
        /// <param name="state">账单状态</param>
        /// <param name="billNo">账单号</param>
        /// <param name="operationDate">业务时间</param>
        /// <param name="billFromType">账单来源类型海出1海进2</param>
        /// <param name="rateCurrencyID">汇率的币种(如果这个列表里没有该币种就按公司配置下默认的)</param>
        /// <param name="rateValue">汇率值(如果这个列表里没有该币种就按公司配置下默认的)</param>
        /// <param name="remark">备注</param>
        /// <param name="isVATInvoiced">是否开增值税发票</param>
        /// <param name="taxrate">税率</param>
        /// <param name="updateDate">帐单版本</param>
        /// <param name="feeIDs">费用-ID列表</param>
        /// <param name="feeWays">费用-方向列表</param>
        /// <param name="feeTypes">费用-类型列表</param>
        /// <param name="feeIsAgents">费用-是否代理费列表</param>
        /// <param name="feeIsSecondSales">费用-是否二次销售列表</param>
        /// <param name="feeIsVATInvoiceds">费用-是否开增值税发票列表</param>
        /// <param name="feeIsGSTs">费用-GSTs列表</param>
        /// <param name="feeChargingCodeIDs">费用-费用代码ID列表</param>
        /// <param name="feeDescriptions">费用-费用描述列表</param>
        /// <param name="feeCurrencyIDs">费用-币种ID列表</param>
        /// <param name="feeRates">费用-汇率列表</param>
        /// <param name="feeContainerIDs">费用-关联柜号ID列表</param>
        /// <param name="feeUnitIDs">费用-费用单位列</param>
        /// <param name="feeUnitPrices">费用-费用单价列表</param>
        /// <param name="feeQuantities">费用-费用数量列表</param>
        /// <param name="feeAmounts">费用-费用金额列表</param>
        /// <param name="feeRemarks">费用-费用备注列表</param>
        /// <param name="feeUpdateDates">费用-数据版本</param>
        /// <param name="feeFromTypes">费用-费用来源类型海出1海进2</param>
        /// <param name="feeIsRevises">费用-是否修改</param>
        /// <param name="IsClosingEdit">是否是关账后的修改</param>
        /// <param name="IsAppcfm">Appcfm </param>
        /// <param name="saveByID">修改人ID</param>
        /// <returns>ManyResult</returns>
        public HierarchyManyResult SaveBillInfo(
            Guid operationID,
            Guid formID,
            FormType formType,
            Guid? id,
            Guid companyID,
            Guid customerID,
            Guid? shipToID,
            FAMCustomerDescription customerDescription,
            string CustomerRefNo,
            BillType type,
            DateTime accountDate,
            DateTime dueDate,
            Guid? payCurrencyId,
            Guid? auditorId,
            string auditorEmail,
            BillState state,
            string billNo,
             DateTime operationDate,
             int? billFromType,
            Guid[] rateCurrencyID,
            decimal[] rateValue,
            string remark,
            bool isVATInvoiced,
            Decimal? taxrate,
            DateTime? updateDate,
            Guid?[] feeIDs,
            FeeWay[] feeWays,
            FeeType[] feeTypes,
            bool[] feeIsAgents,
            bool[] feeIsSecondSales,
            bool[] feeIsVATInvoiceds,
            bool[] feeIsGSTs,
            Guid[] feeChargingCodeIDs,
            string[] feeDescriptions,
            Guid[] feeCurrencyIDs,
            decimal[] feeRates,
            Guid?[] feeContainerIDs,
            Guid[] feeUnitIDs,
            decimal[] feeUnitPrices,
            decimal[] feeQuantities,
            decimal[] feeAmounts,
            string[] feeRemarks,
            DateTime?[] feeUpdateDates,
            int?[] feeFromTypes,
            bool[] feeIsRevises,
            bool IsClosingEdit,
            bool IsAppcfm,
            Guid saveByID)
        {
            #region 验证参数
            ArgumentHelper.AssertGuidNotEmpty(companyID, "companyID");
            ArgumentHelper.AssertGuidNotEmpty(customerID, "customerID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            ArgumentHelper.AssertArrayLengthMatch(
                feeIDs,
                feeWays,
                feeTypes,
                feeChargingCodeIDs,
                feeCurrencyIDs,
                feeRates,
                feeUnitIDs,
                feeUnitPrices,
                feeQuantities,
                feeAmounts,
                feeRemarks,
                feeIsRevises,
                feeUpdateDates);

            #endregion

            try
            {
                Stopwatch stopwatchBooking = StopwatchHelper.StartStopwatch();
                HierarchyManyResult result = null;
                TransactionOptions option = new TransactionOptions();
                option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    Database db = DatabaseFactory.CreateDatabase();
                    DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveBillInfo");

                    dbCommand.CommandTimeout = 60;

                    #region 构建 数组string拼装串

                    string tempCustomerDescription = SerializerHelper.SerializeToString<FAMCustomerDescription>(customerDescription, true, false);

                    string tempRateCurrencyID = payCurrencyId.IsNullOrEmpty() ? string.Empty : rateCurrencyID.Join();
                    string tempRateValue = payCurrencyId.IsNullOrEmpty() ? string.Empty : rateValue.Join();

                    string tempFeeIds = feeIDs.Join();
                    string tempFeeWays = feeWays.Join();
                    string tempFeeTypes = feeTypes.Join();
                    string tempFeeChargingCodeIds = feeChargingCodeIDs.Join();
                    string tempFeeCurrencyIds = feeCurrencyIDs.Join();
                    string tempFeeRates = feeRates.Join();
                    string tempFeeContainerIDs = feeContainerIDs.Join();
                    string tempFeeUnitIds = feeUnitIDs.Join();
                    string tempFeeUnitPrices = feeUnitPrices.Join();
                    string tempFeeQuantities = feeQuantities.Join();
                    string tempFeeAmounts = feeAmounts.Join();
                    string tempFeeRemarks = feeRemarks.Join();
                    string tempFeeUpdateDates = feeUpdateDates.Join();

                    string tempFeeIsAgents = feeIsAgents.Join();
                    string tempFeeIsSecondSales = feeIsSecondSales.Join();
                    string tempFeeIsVATInvoiceds = feeIsVATInvoiceds.Join();
                    string tempFeeIsGsts = feeIsGSTs.Join();
                    string tempFeeFromTypes = feeFromTypes.Join();
                    string tempFeeIsRevises = feeIsRevises.Join();
                    #endregion

                    #region 传递参数

                    db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
                    db.AddInParameter(dbCommand, "@FormID", DbType.Guid, formID);
                    db.AddInParameter(dbCommand, "@FormType", DbType.Byte, formType);
                    db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                    db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                    db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                    db.AddInParameter(dbCommand, "@CustomerDescription", DbType.String, tempCustomerDescription);
                    db.AddInParameter(dbCommand, "@ShipToID", DbType.Guid, shipToID);
                    db.AddInParameter(dbCommand, "@CustomerRefNo", DbType.String, CustomerRefNo);
                    db.AddInParameter(dbCommand, "@Type", DbType.Byte, type);
                    db.AddInParameter(dbCommand, "@AccountDate", DbType.DateTime, accountDate);
                    db.AddInParameter(dbCommand, "@DueDate", DbType.DateTime, dueDate);
                    db.AddInParameter(dbCommand, "@PayCurrencyId", DbType.Guid, payCurrencyId);
                    db.AddInParameter(dbCommand, "@RateCurrencyIDs", DbType.String, tempRateCurrencyID);
                    db.AddInParameter(dbCommand, "@RateValues", DbType.String, tempRateValue);
                    db.AddInParameter(dbCommand, "@Remark", DbType.String, remark);
                    db.AddInParameter(dbCommand, "@IsVATInvoiced", DbType.Boolean, isVATInvoiced);
                    db.AddInParameter(dbCommand, "@Taxrate", DbType.Decimal, taxrate);
                    db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                    db.AddInParameter(dbCommand, "@BillFromType", DbType.Int16, billFromType);
                    db.AddInParameter(dbCommand, "@FeeIDs", DbType.String, tempFeeIds);
                    db.AddInParameter(dbCommand, "@FeeWays", DbType.String, tempFeeWays);
                    db.AddInParameter(dbCommand, "@FeeTypes", DbType.String, tempFeeTypes);
                    db.AddInParameter(dbCommand, "@FeeIsAgents", DbType.String, tempFeeIsAgents);
                    db.AddInParameter(dbCommand, "@FeeIsSecondSales", DbType.String, tempFeeIsSecondSales);
                    db.AddInParameter(dbCommand, "@FeeIsVATInvoiceds", DbType.String, tempFeeIsVATInvoiceds);
                    db.AddInParameter(dbCommand, "@FeeIsGsts", DbType.String, tempFeeIsGsts);
                    db.AddInParameter(dbCommand, "@FeeChargingCodeIDs", DbType.String, tempFeeChargingCodeIds);
                    db.AddInParameter(dbCommand, "@FeeDescriptions", DbType.String, feeDescriptions.Join());
                    db.AddInParameter(dbCommand, "@FeeCurrencyIDs", DbType.String, tempFeeCurrencyIds);
                    db.AddInParameter(dbCommand, "@FeeQuantities", DbType.String, tempFeeQuantities);
                    db.AddInParameter(dbCommand, "@FeeRates", DbType.String, tempFeeRates);
                    db.AddInParameter(dbCommand, "@FeeContainerIDs", DbType.String, tempFeeContainerIDs);
                    db.AddInParameter(dbCommand, "@FeeUnitIDs", DbType.String, tempFeeUnitIds);
                    db.AddInParameter(dbCommand, "@FeeUnitPrices", DbType.String, tempFeeUnitPrices);
                    db.AddInParameter(dbCommand, "@FeeAmounts", DbType.String, tempFeeAmounts);
                    db.AddInParameter(dbCommand, "@FeeRemarks", DbType.String, tempFeeRemarks);
                    db.AddInParameter(dbCommand, "@FeeUpdateDates", DbType.String, tempFeeUpdateDates);
                    db.AddInParameter(dbCommand, "@FeeFromTypes", DbType.String, tempFeeFromTypes);
                    db.AddInParameter(dbCommand, "@FeeIsRevises", DbType.String, tempFeeIsRevises);
                    db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                    db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                    db.AddInParameter(dbCommand, "@IsClosingEdit", DbType.Boolean, IsClosingEdit);
                    db.AddInParameter(dbCommand, "@IsAPPCFM", DbType.Boolean, IsAppcfm);

                    #endregion
                    stopwatchBooking.Start();
                    ManyResult[] results = db.ManyResults(dbCommand, new string[][] { new string[] { "ID", "No","UpdateDate","ApplyState" }, 
                    new string[] { "ID", "UpdateDate" } });



                    if (results == null
                        || results.Length < 2
                        || results[0].Items.Count == 0)
                    {
                        return null;
                    }
                    string tempbillNo = results[0].Items[0].GetValue<string>("No");
                    bool isCreateBill = id == Guid.Empty ? true : false;
                    billNo = isCreateBill ? tempbillNo : billNo;
                    EmailNotifyInfo notifyInfo = new EmailNotifyInfo(isCreateBill, billNo, auditorId, state, companyID, auditorEmail, operationID, operationDate);
                    SendEmail(notifyInfo);
                    result = new HierarchyManyResult(results[0].Items[0].Copy());
                    if (results[1] != null)
                    {
                        foreach (SingleResult s in results[1].Items)
                        {
                            result.Childs.Add(new HierarchyManyResult(s));
                        }
                        feeIDs = results[1].Items.Select(fitem => fitem.GetValue<Guid?>("ID")).ToArray();
                    }
                    OperationLogService.Add(DateTime.Now, "SAVE-BILL-DB", string.Format("数据库保存;Bill ID[{0}] Operation ID[{1}]", result.GetValue<Guid>("ID"), operationID.ToString()), stopwatchBooking.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
                    
#if DEBUG
                    
                    //应收/非退佣费用
                    if (type == BillType.AR)
                    {
                        SaveRequestBill saveRequest = new SaveRequestBill()
                        {
                            ID = result.GetValue<Guid>("ID"),
                            OperationID = operationID,
                            FeeIDs = feeIDs,
                            SaveByID = saveByID,
                            UpdateDate = DateTime.Now,
                        };
                        SaveBillForCSP(saveRequest);
                        SaveBillFeeForCSP(saveRequest);
                    }
#endif
                    scope.Complete();
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
        /// <summary>
        /// 发送邮件给审核人或者操作口岸的所有计费
        /// </summary>
        /// <param name="billId"></param>
        /// <param name="auditorEmail"></param>
        /// <param name="state"></param>
        /// <param name="companyID"></param>
        private void SendEmail(EmailNotifyInfo info)
        {
            WaitCallback fire = (notifyInfo) =>
            {

                EmailNotifyInfo tempInfo = notifyInfo as EmailNotifyInfo;
                if (tempInfo == null)
                {
                    return;
                }
                string emailSubject = GetEmailTemplate(tempInfo);
                List<string> toUsers = new List<string>();
                try
                {
                    ConfigureInfo companyInfo = _configureService.GetCompanyConfigureInfo(tempInfo.CompanyId);
                    if (tempInfo.IsNew && companyInfo.ChargingClosingdate.HasValue && (companyInfo.ChargingClosingdate.Value - tempInfo.OperationDate).Days > 0)
                    {

                        toUsers = GetEmailAddresses(tempInfo.OperationId, tempInfo.CompanyId);

                    }
                    else if (tempInfo.State == BillState.Approved && tempInfo.AuditorId != null && !string.IsNullOrEmpty(tempInfo.AuditorEmail))
                    {
                        toUsers.Add(tempInfo.AuditorEmail);
                    }
                    if (toUsers.Count <= 0)
                        return;
                    //管理员用户ID
                    Guid fromEmailId = new Guid("4047CFAD-ECC8-E111-9D0D-0026551CA87B");

                    ICP.Message.ServiceInterface.Message message = CreateMessageInfo(fromEmailId, toUsers, emailSubject, tempInfo);
                    _emailService.Send(message);
                    //_emailService.SendAndSaveLog(fromEmailId, toUsers.ToArray(), null, emailSubject, null, ICP.OA.ServiceInterface.DataObjects.MailPriority.Normal);

                }
                catch (Exception ex)
                {
                    LogHelper.SaveLog(ex.Message + ex.StackTrace);
                }
            };

            ThreadPool.QueueUserWorkItem(fire, info);
        }

        ICP.Message.ServiceInterface.Message CreateMessageInfo(Guid fromEmailId, List<string> toUsers, string emailSubject, EmailNotifyInfo tempInfo)
        {
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            //邮件内容
            string content = string.Empty;
            message.Subject = emailSubject;
            message.Body = content;
            message.BodyFormat = BodyFormat.olFormatHTML;
            message.Priority = MessagePriority.Normal;


            UserInfo fromInfo = userService.GetUserInfo(fromEmailId);
            if (fromInfo != null && !string.IsNullOrEmpty(fromInfo.EMail))
            {
                message.SendFrom = fromInfo.EMail;
            }

            message.SendTo = string.Join(";", toUsers.ToArray());
            message.UserProperties = new MessageUserPropertiesObject();
            MessageUserPropertiesObject userProperties = message.UserProperties;
            userProperties.OperationId = tempInfo.OperationId;
            userProperties.OperationType = OperationType.Internal;
            userProperties.FormId = tempInfo.OperationId;
            userProperties.FormType = FormType.Booking;

            return message;
        }


        private string GetEmailTemplate(EmailNotifyInfo tempInfo)
        {
            string emailTemplate = IsEnglish ? "User:{0} has modified bill,the bill No. is {1}." : "用户：{0} 已修改账单，账单号：{1}。";
            if (tempInfo.IsNew)
            {
                emailTemplate = IsEnglish ? "User:{0} has created a bill,the bill No. is {1}." : "用户：{0} 已新增账单，账单号：{1}。";
            }
            string emailSubject = string.Format(emailTemplate, ApplicationContext.Current.Username, tempInfo.BillId);
            return emailSubject;
        }
        #region 获取邮件地址


        private List<string> GetEmailAddresses(Guid operationId, Guid companyId)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fam].[uspGetChargingEmailAddress]");
            db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationId);
            db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyId);
            DataSet dtSet = db.ExecuteDataSet(dbCommand);
            if (dtSet == null || dtSet.Tables.Count <= 0 || dtSet.Tables[0].Rows.Count <= 0)
                return new List<string>();
            var result = (from userInfo in dtSet.Tables[0].AsEnumerable()
                          select userInfo.Field<string>("Email")).Distinct().ToList();
            if (result == null || result.Count <= 0 || string.IsNullOrEmpty(result[0]))
                return new List<string>();
            return result;
        }

        #endregion


        /// <summary>
        /// 保存帐单信息
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="billinfos">billinfos</param>
        /// <param name="saveByID">saveByID</param>
        /// <param name="SaveSource">数据来源</param>
        /// <param name="operationDate">业务时间</param>
        /// <returns>HierarchyManyResult</returns>
        public List<HierarchyManyResult> SaveBillInfos(
            Guid operationID,
            List<BillInfo> billinfos,
            Guid saveByID,
            byte SaveSource,
            DateTime operationDate)
        {
            List<HierarchyManyResult> results = new List<HierarchyManyResult>();
            try
            {
                TransactionOptions option = new TransactionOptions();
                option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
                {

                    foreach (var info in billinfos)
                    {
                        #region 构建 费用列表参数
                        List<Guid?> feeIDs = new List<Guid?>();
                        List<FeeType> feeTypes = new List<FeeType>();
                        List<FeeWay> feeWays = new List<FeeWay>();
                        List<Guid> feeChargingCodeIDs = new List<Guid>(), feeCurrencyIDs = new List<Guid>(), feeUnitIDs = new List<Guid>();
                        List<string> feeRemarks = new List<string>();
                        List<decimal> feeRates = new List<decimal>(), feeQuantities = new List<decimal>(), feeUnitPrices = new List<decimal>(), feeAmounts = new List<decimal>();
                        List<string> feeDescriptions = new List<string>();
                        List<Guid?> feeContainerIDs = new List<Guid?>();
                        List<DateTime?> feeUpdateDates = new List<DateTime?>();

                        List<bool> feeIsAgents = new List<bool>();
                        List<bool> feeIsSecondSales = new List<bool>();
                        List<bool> feeIsVATInvoiceds = new List<bool>();
                        List<bool> feeIsGSTs = new List<bool>();
                        List<int?> feeFromTypes = new List<int?>();
                        List<bool> feeIsRevises = new List<bool>();


                        foreach (var item in info.Fees)
                        {
                            feeIDs.Add(item.ID);
                            feeTypes.Add(FeeType.Normal);
                            feeWays.Add(item.Way);
                            feeChargingCodeIDs.Add(item.ChargingCodeID);
                            feeDescriptions.Add(item.ChargingDescription);
                            feeCurrencyIDs.Add(item.CurrencyID);
                            feeContainerIDs.Add(item.ContainerID);
                            feeUnitIDs.Add(item.UnitID);
                            feeRates.Add(item.Rate);
                            feeQuantities.Add(item.Quantity);
                            feeUnitPrices.Add(item.UnitPrice);
                            feeAmounts.Add(item.Amount);
                            feeRemarks.Add(item.Remark);
                            feeIsAgents.Add(item.IsAgent);
                            feeIsSecondSales.Add(item.IsSecondSale);
                            feeIsVATInvoiceds.Add(item.IsVATInvoiced);
                            feeIsGSTs.Add(item.IsGST);
                            feeUpdateDates.Add(item.UpdateDate);
                            feeFromTypes.Add(item.FromType);
                            feeIsRevises.Add(false);

                        }
                        if (info.ReviseHistoryFees != null)
                        {
                            foreach (var item in info.ReviseHistoryFees)
                            {
                                feeIDs.Add(item.ID);
                                feeTypes.Add(FeeType.Normal);
                                feeWays.Add(item.Way);
                                feeChargingCodeIDs.Add(item.ChargingCodeID);
                                feeDescriptions.Add(item.ChargingDescription);
                                feeCurrencyIDs.Add(item.CurrencyID);
                                feeContainerIDs.Add(item.ContainerID);
                                feeUnitIDs.Add(item.UnitID);
                                feeRates.Add(item.Rate);
                                feeQuantities.Add(item.Quantity);
                                feeUnitPrices.Add(item.UnitPrice);
                                feeAmounts.Add(item.Amount);
                                feeRemarks.Add(item.Remark);
                                feeIsAgents.Add(item.IsAgent);
                                feeIsSecondSales.Add(item.IsSecondSale);
                                feeIsVATInvoiceds.Add(item.IsVATInvoiced);
                                feeIsGSTs.Add(item.IsGST);
                                feeUpdateDates.Add(item.UpdateDate);
                                feeFromTypes.Add(item.FromType);
                                feeIsRevises.Add(true);
                            }
                        }
                        #endregion

                        #region 调用服务接口
                        HierarchyManyResult result = SaveBillInfo(operationID
                                                    , info.FormID
                                                    , info.FormType
                                                    , info.ID
                                                    , info.CompanyID
                                                    , info.CustomerID
                                                    , info.ShipToID
                                                    , info.CustomerDescription
                                                    , info.CustomerRefNo
                                                    , info.Type
                                                    , info.AccountDate
                                                    , info.DueDate
                                                    , null
                                                    , info.AuditorID
                                                    , info.AuditorEmail
                                                    , info.State
                                                     , info.No
                                                     , operationDate
                                                     , info.FromType
                                                    , null
                                                    , null
                                                    , info.Remark
                                                    , info.IsVATInvoiced
                                                    , info.Taxrate
                                                    , info.UpdateDate
                                                    , feeIDs.ToArray()
                                                    , feeWays.ToArray()
                                                    , feeTypes.ToArray()
                                                    , feeIsAgents.ToArray()
                                                    , feeIsSecondSales.ToArray()
                                                    , feeIsVATInvoiceds.ToArray()
                                                    , feeIsGSTs.ToArray()
                                                    , feeChargingCodeIDs.ToArray()
                                                    , feeDescriptions.ToArray()
                                                    , feeCurrencyIDs.ToArray()
                                                    , feeRates.ToArray()
                                                    , feeContainerIDs.ToArray()
                                                    , feeUnitIDs.ToArray()
                                                    , feeUnitPrices.ToArray()
                                                    , feeQuantities.ToArray()
                                                    , feeAmounts.ToArray()
                                                    , feeRemarks.ToArray()
                                                    , feeUpdateDates.ToArray()
                                                    , feeFromTypes.ToArray()
                                                    , feeIsRevises.ToArray()
                                                    , false
                                                    , false
                                                    , SaveSource
                                                    , saveByID
                                                    );

                        results.Add(result);
                        #endregion
                    }

                    scope.Complete();

                    return results;
                }
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 保存帐单信息
        /// </summary>
        /// <param name="billinfos">账单列表信息</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="operationDate">操作时间</param>
        /// <returns>HierarchyManyResult</returns>
        public List<HierarchyManyResult> BatchSaveBillInfos(
            List<BillInfo> billinfos,
            Guid saveByID,
            DateTime operationDate)
        {
            List<HierarchyManyResult> results = new List<HierarchyManyResult>();
            try
            {
                TransactionOptions option = new TransactionOptions();
                option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
                {

                    foreach (var info in billinfos)
                    {
                        #region 构建 费用列表参数
                        List<Guid?> feeIDs = new List<Guid?>();
                        List<FeeType> feeTypes = new List<FeeType>();
                        List<FeeWay> feeWays = new List<FeeWay>();
                        List<Guid> feeChargingCodeIDs = new List<Guid>(), feeCurrencyIDs = new List<Guid>(), feeUnitIDs = new List<Guid>();
                        List<string> feeRemarks = new List<string>();
                        List<decimal> feeRates = new List<decimal>(), feeQuantities = new List<decimal>(), feeUnitPrices = new List<decimal>(), feeAmounts = new List<decimal>();
                        List<string> feeDescriptions = new List<string>();
                        List<Guid?> feeContainerIDs = new List<Guid?>();
                        List<DateTime?> feeUpdateDates = new List<DateTime?>();

                        List<bool> feeIsAgents = new List<bool>();
                        List<bool> feeIsSecondSales = new List<bool>();
                        List<bool> feeIsVATInvoiceds = new List<bool>();
                        List<bool> feeIsGSTs = new List<bool>();
                        List<int?> feeFromTypes = new List<int?>();
                        List<bool> feeIsRevises = new List<bool>();


                        foreach (var item in info.Fees)
                        {
                            feeIDs.Add(item.ID);
                            feeTypes.Add(FeeType.Normal);
                            feeWays.Add(item.Way);
                            feeChargingCodeIDs.Add(item.ChargingCodeID);
                            feeDescriptions.Add(item.ChargingDescription);
                            feeCurrencyIDs.Add(item.CurrencyID);
                            feeContainerIDs.Add(item.ContainerID);
                            feeUnitIDs.Add(item.UnitID);
                            feeRates.Add(item.Rate);
                            feeQuantities.Add(item.Quantity);
                            feeUnitPrices.Add(item.UnitPrice);
                            feeAmounts.Add(item.Amount);
                            feeRemarks.Add(item.Remark);
                            feeIsAgents.Add(item.IsAgent);
                            feeIsSecondSales.Add(item.IsSecondSale);
                            feeIsVATInvoiceds.Add(item.IsVATInvoiced);
                            feeIsGSTs.Add(item.IsGST);
                            feeUpdateDates.Add(item.UpdateDate);
                            feeFromTypes.Add(item.FromType);
                            feeIsRevises.Add(false);

                        }
                        if (info.ReviseHistoryFees != null)
                        {
                            foreach (var item in info.ReviseHistoryFees)
                            {
                                feeIDs.Add(item.ID);
                                feeTypes.Add(FeeType.Normal);
                                feeWays.Add(item.Way);
                                feeChargingCodeIDs.Add(item.ChargingCodeID);
                                feeDescriptions.Add(item.ChargingDescription);
                                feeCurrencyIDs.Add(item.CurrencyID);
                                feeContainerIDs.Add(item.ContainerID);
                                feeUnitIDs.Add(item.UnitID);
                                feeRates.Add(item.Rate);
                                feeQuantities.Add(item.Quantity);
                                feeUnitPrices.Add(item.UnitPrice);
                                feeAmounts.Add(item.Amount);
                                feeRemarks.Add(item.Remark);
                                feeIsAgents.Add(item.IsAgent);
                                feeIsSecondSales.Add(item.IsSecondSale);
                                feeIsVATInvoiceds.Add(item.IsVATInvoiced);
                                feeIsGSTs.Add(item.IsGST);
                                feeUpdateDates.Add(item.UpdateDate);
                                feeFromTypes.Add(item.FromType);
                                feeIsRevises.Add(true);
                            }
                        }
                        #endregion

                        #region 调用服务接口
                        HierarchyManyResult result = SaveBillInfo(info.OperationID
                                                    , info.FormID
                                                    , info.FormType
                                                    , info.ID
                                                    , info.CompanyID
                                                    , info.CustomerID
                                                    , info.ShipToID
                                                    , info.CustomerDescription
                                                    , info.CustomerRefNo
                                                    , info.Type
                                                    , info.AccountDate
                                                    , info.DueDate
                                                    , null
                                                    , info.AuditorID
                                                    , info.AuditorEmail
                                                    , info.State
                                                     , info.No
                                                     , operationDate
                                                     , info.FromType
                                                    , null
                                                    , null
                                                    , info.Remark
                                                    , info.IsVATInvoiced
                                                    , info.Taxrate
                                                    , info.UpdateDate
                                                    , feeIDs.ToArray()
                                                    , feeWays.ToArray()
                                                    , feeTypes.ToArray()
                                                    , feeIsAgents.ToArray()
                                                    , feeIsSecondSales.ToArray()
                                                    , feeIsVATInvoiceds.ToArray()
                                                    , feeIsGSTs.ToArray()
                                                    , feeChargingCodeIDs.ToArray()
                                                    , feeDescriptions.ToArray()
                                                    , feeCurrencyIDs.ToArray()
                                                    , feeRates.ToArray()
                                                    , feeContainerIDs.ToArray()
                                                    , feeUnitIDs.ToArray()
                                                    , feeUnitPrices.ToArray()
                                                    , feeQuantities.ToArray()
                                                    , feeAmounts.ToArray()
                                                    , feeRemarks.ToArray()
                                                    , feeUpdateDates.ToArray()
                                                    , feeFromTypes.ToArray()
                                                    , feeIsRevises.ToArray()
                                                    , false
                                                    , false
                                                    , saveByID
                                                    );

                        results.Add(result);
                        #endregion
                    }

                    scope.Complete();

                    return results;
                }
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 保存帐单信息
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="formID">表单ID</param>
        /// <param name="formType">表单类型</param>
        /// <param name="id">帐单ID</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="customerID">客户ID</param>
        /// <param name="shipToID">Ship To ID</param>
        /// <param name="customerDescription">客户的描述信息</param>
        /// <param name="CustomerRefNo">客户参考号</param>
        /// <param name="type">账单类型（0:应收,1:应付,2:代理）</param>
        /// <param name="accountDate">账单日</param>
        /// <param name="dueDate">到期日</param>
        /// <param name="payCurrencyId">按一种币种支付.为空时说明不是按一种币种支付的</param>
        /// <param name="auditorId">审核人ID</param>
        /// <param name="auditorEmail">审核人邮件地址</param>
        /// <param name="state">账单状态</param>
        /// <param name="billNo">账单号</param>
        /// <param name="operationDate">业务时间</param>
        /// <param name="billFromType">账单来源类型海出1海进2</param>
        /// <param name="rateCurrencyID">汇率的币种(如果这个列表里没有该币种就按公司配置下默认的)</param>
        /// <param name="rateValue">汇率值(如果这个列表里没有该币种就按公司配置下默认的)</param>
        /// <param name="remark">备注</param>
        /// <param name="isVATInvoiced">是否开增值税发票</param>
        /// <param name="taxrate">税率</param>
        /// <param name="updateDate">帐单版本</param>
        /// <param name="feeIDs">费用-ID列表</param>
        /// <param name="feeWays">费用-方向列表</param>
        /// <param name="feeTypes">费用-类型列表</param>
        /// <param name="feeIsAgents">费用-是否代理费列表</param>
        /// <param name="feeIsSecondSales">费用-是否二次销售列表</param>
        /// <param name="feeIsVATInvoiceds">费用-是否开增值税发票列表</param>
        /// <param name="feeIsGSTs">费用-GSTs列表</param>
        /// <param name="feeChargingCodeIDs">费用-费用代码ID列表</param>
        /// <param name="feeDescriptions">费用-费用描述列表</param>
        /// <param name="feeCurrencyIDs">费用-币种ID列表</param>
        /// <param name="feeRates">费用-汇率列表</param>
        /// <param name="feeContainerIDs">费用-关联柜号ID列表</param>
        /// <param name="feeUnitIDs">费用-费用单位列</param>
        /// <param name="feeUnitPrices">费用-费用单价列表</param>
        /// <param name="feeQuantities">费用-费用数量列表</param>
        /// <param name="feeAmounts">费用-费用金额列表</param>
        /// <param name="feeRemarks">费用-费用备注列表</param>
        /// <param name="feeUpdateDates">费用-数据版本</param>
        /// <param name="feeFromTypes">费用-费用来源类型海出1海进2</param>
        /// <param name="feeIsRevises">费用-是否修改</param>
        /// <param name="IsClosingEdit">是否是关账后的修改</param>
        /// <param name="IsAppcfm">Appcfm </param>
        /// <param name="IsAppcfm">SaveSource</param>
        /// <param name="saveByID">修改人ID</param>
        /// <returns>ManyResult</returns>
        public HierarchyManyResult SaveBillInfo(
            Guid operationID,
            Guid formID,
            FormType formType,
            Guid? id,
            Guid companyID,
            Guid customerID,
            Guid? shipToID,
            FAMCustomerDescription customerDescription,
            string CustomerRefNo,
            BillType type,
            DateTime accountDate,
            DateTime dueDate,
            Guid? payCurrencyId,
            Guid? auditorId,
            string auditorEmail,
            BillState state,
            string billNo,
             DateTime operationDate,
             int? billFromType,
            Guid[] rateCurrencyID,
            decimal[] rateValue,
            string remark,
            bool isVATInvoiced,
            Decimal? taxrate,
            DateTime? updateDate,
            Guid?[] feeIDs,
            FeeWay[] feeWays,
            FeeType[] feeTypes,
            bool[] feeIsAgents,
            bool[] feeIsSecondSales,
            bool[] feeIsVATInvoiceds,
            bool[] feeIsGSTs,
            Guid[] feeChargingCodeIDs,
            string[] feeDescriptions,
            Guid[] feeCurrencyIDs,
            decimal[] feeRates,
            Guid?[] feeContainerIDs,
            Guid[] feeUnitIDs,
            decimal[] feeUnitPrices,
            decimal[] feeQuantities,
            decimal[] feeAmounts,
            string[] feeRemarks,
            DateTime?[] feeUpdateDates,
            int?[] feeFromTypes,
            bool[] feeIsRevises,
            bool IsClosingEdit,
            bool IsAppcfm,
            byte SaveSource,
            Guid saveByID)
        {
            #region 验证参数
            ArgumentHelper.AssertGuidNotEmpty(companyID, "companyID");
            ArgumentHelper.AssertGuidNotEmpty(customerID, "customerID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            ArgumentHelper.AssertArrayLengthMatch(
                feeIDs,
                feeWays,
                feeTypes,
                feeChargingCodeIDs,
                feeCurrencyIDs,
                feeRates,
                feeUnitIDs,
                feeUnitPrices,
                feeQuantities,
                feeAmounts,
                feeRemarks,
                feeIsRevises,
                feeUpdateDates);

            #endregion

            try
            {
                Stopwatch stopwatchBooking = StopwatchHelper.StartStopwatch();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveBillInfo");

                dbCommand.CommandTimeout = 60;

                #region 构建 数组string拼装串

                string tempCustomerDescription = SerializerHelper.SerializeToString<FAMCustomerDescription>(customerDescription, true, false);

                string tempRateCurrencyID = payCurrencyId.IsNullOrEmpty() ? string.Empty : rateCurrencyID.Join();
                string tempRateValue = payCurrencyId.IsNullOrEmpty() ? string.Empty : rateValue.Join();

                string tempFeeIds = feeIDs.Join();
                string tempFeeWays = feeWays.Join();
                string tempFeeTypes = feeTypes.Join();
                string tempFeeChargingCodeIds = feeChargingCodeIDs.Join();
                string tempFeeCurrencyIds = feeCurrencyIDs.Join();
                string tempFeeRates = feeRates.Join();
                string tempFeeContainerIDs = feeContainerIDs.Join();
                string tempFeeUnitIds = feeUnitIDs.Join();
                string tempFeeUnitPrices = feeUnitPrices.Join();
                string tempFeeQuantities = feeQuantities.Join();
                string tempFeeAmounts = feeAmounts.Join();
                string tempFeeRemarks = feeRemarks.Join();
                string tempFeeUpdateDates = feeUpdateDates.Join();

                string tempFeeIsAgents = feeIsAgents.Join();
                string tempFeeIsSecondSales = feeIsSecondSales.Join();
                string tempFeeIsVATInvoiceds = feeIsVATInvoiceds.Join();
                string tempFeeIsGsts = feeIsGSTs.Join();
                string tempFeeFromTypes = feeFromTypes.Join();
                string tempFeeIsRevises = feeIsRevises.Join();
                #endregion

                #region 传递参数

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
                db.AddInParameter(dbCommand, "@FormID", DbType.Guid, formID);
                db.AddInParameter(dbCommand, "@FormType", DbType.Byte, formType);
                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@CustomerDescription", DbType.String, tempCustomerDescription);
                db.AddInParameter(dbCommand, "@ShipToID", DbType.Guid, shipToID);
                db.AddInParameter(dbCommand, "@CustomerRefNo", DbType.String, CustomerRefNo);
                db.AddInParameter(dbCommand, "@Type", DbType.Byte, type);
                db.AddInParameter(dbCommand, "@AccountDate", DbType.DateTime, accountDate);
                db.AddInParameter(dbCommand, "@DueDate", DbType.DateTime, dueDate);
                db.AddInParameter(dbCommand, "@PayCurrencyId", DbType.Guid, payCurrencyId);
                db.AddInParameter(dbCommand, "@RateCurrencyIDs", DbType.String, tempRateCurrencyID);
                db.AddInParameter(dbCommand, "@RateValues", DbType.String, tempRateValue);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, remark);
                db.AddInParameter(dbCommand, "@IsVATInvoiced", DbType.Boolean, isVATInvoiced);
                db.AddInParameter(dbCommand, "@Taxrate", DbType.Decimal, taxrate);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@BillFromType", DbType.Int16, billFromType);
                db.AddInParameter(dbCommand, "@FeeIDs", DbType.String, tempFeeIds);
                db.AddInParameter(dbCommand, "@FeeWays", DbType.String, tempFeeWays);
                db.AddInParameter(dbCommand, "@FeeTypes", DbType.String, tempFeeTypes);
                db.AddInParameter(dbCommand, "@FeeIsAgents", DbType.String, tempFeeIsAgents);
                db.AddInParameter(dbCommand, "@FeeIsSecondSales", DbType.String, tempFeeIsSecondSales);
                db.AddInParameter(dbCommand, "@FeeIsVATInvoiceds", DbType.String, tempFeeIsVATInvoiceds);
                db.AddInParameter(dbCommand, "@FeeIsGsts", DbType.String, tempFeeIsGsts);
                db.AddInParameter(dbCommand, "@FeeChargingCodeIDs", DbType.String, tempFeeChargingCodeIds);
                db.AddInParameter(dbCommand, "@FeeDescriptions", DbType.String, feeDescriptions.Join());
                db.AddInParameter(dbCommand, "@FeeCurrencyIDs", DbType.String, tempFeeCurrencyIds);
                db.AddInParameter(dbCommand, "@FeeQuantities", DbType.String, tempFeeQuantities);
                db.AddInParameter(dbCommand, "@FeeRates", DbType.String, tempFeeRates);
                db.AddInParameter(dbCommand, "@FeeContainerIDs", DbType.String, tempFeeContainerIDs);
                db.AddInParameter(dbCommand, "@FeeUnitIDs", DbType.String, tempFeeUnitIds);
                db.AddInParameter(dbCommand, "@FeeUnitPrices", DbType.String, tempFeeUnitPrices);
                db.AddInParameter(dbCommand, "@FeeAmounts", DbType.String, tempFeeAmounts);
                db.AddInParameter(dbCommand, "@FeeRemarks", DbType.String, tempFeeRemarks);
                db.AddInParameter(dbCommand, "@FeeUpdateDates", DbType.String, tempFeeUpdateDates);
                db.AddInParameter(dbCommand, "@FeeFromTypes", DbType.String, tempFeeFromTypes);
                db.AddInParameter(dbCommand, "@FeeIsRevises", DbType.String, tempFeeIsRevises);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                db.AddInParameter(dbCommand, "@IsClosingEdit", DbType.Boolean, IsClosingEdit);
                db.AddInParameter(dbCommand, "@SaveSource", DbType.Boolean, SaveSource);
                db.AddInParameter(dbCommand, "@IsAPPCFM", DbType.Boolean, IsAppcfm);

                #endregion
                stopwatchBooking.Start();
                ManyResult[] results = db.ManyResults(dbCommand, new string[][] { new string[] { "ID", "No","UpdateDate","ApplyState" }, 
                    new string[] { "ID", "UpdateDate" } });



                if (results == null
                    || results.Length < 2
                    || results[0].Items.Count == 0)
                {
                    return null;
                }
                string tempbillNo = results[0].Items[0].GetValue<string>("No");
                bool isCreateBill = id == Guid.Empty ? true : false;
                billNo = isCreateBill ? tempbillNo : billNo;
                EmailNotifyInfo notifyInfo = new EmailNotifyInfo(isCreateBill, billNo, auditorId, state, companyID, auditorEmail, operationID, operationDate);
                SendEmail(notifyInfo);
                HierarchyManyResult result = new HierarchyManyResult(results[0].Items[0].Copy());
                if (results[1] != null)
                {
                    foreach (SingleResult s in results[1].Items)
                    {
                        result.Childs.Add(new HierarchyManyResult(s));
                    }
                    feeIDs = results[1].Items.Select(fitem => fitem.GetValue<Guid?>("ID")).ToArray();
                }
                OperationLogService.Add(DateTime.Now, "SAVE-BILL-DB", string.Format("数据库保存;Bill ID[{0}] Operation ID[{1}]", result.GetValue<Guid>("ID"), operationID.ToString()), stopwatchBooking.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
                //应收/非退佣费用
#if DEBUG
                if (type == BillType.AR)
                {
                    SaveRequestBill saveRequest = new SaveRequestBill()
                    {
                        OperationID = operationID,
                        FeeIDs = feeIDs,
                        SaveByID = saveByID,
                        UpdateDate = DateTime.Now,
                    };
                    SaveBillForCSP(saveRequest);
                    SaveBillFeeForCSP(saveRequest);
                }
#endif
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

        #region 保存帐单及其费用列表
        /// <summary>
        /// 保存帐单及其费用列表
        /// </summary>
        /// <param name="saveRequest">保存对象</param>
        /// <returns>ManyResult</returns>
        public HierarchyManyResult SaveBillAndFeeList(SaveRequestBill saveRequest)
        {
            #region 验证参数
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.CompanyID, "companyID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.CustomerID, "customerID");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.SaveByID, "saveByID");

            ArgumentHelper.AssertArrayLengthMatch(
                saveRequest.FeeIDs,
               saveRequest.FeeWays,
               saveRequest.FeeTypes,
               saveRequest.FeeChargingCodeIDs,
               saveRequest.FeeCurrencyIDs,
               saveRequest.FeeRates,
               saveRequest.FeeUnitIDs,
               saveRequest.FeeUnitPrices,
               saveRequest.FeeQuantities,
               saveRequest.FeeAmounts,
               saveRequest.FeeRemarks,
               saveRequest.FeeIsRevises,
               saveRequest.FeeUpdateDates);

            #endregion

            try
            {
                Stopwatch stopwatchBooking = StopwatchHelper.StartStopwatch();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveBillInfo");

                dbCommand.CommandTimeout = 60;

                #region 构建 数组string拼装串

                string tempCustomerDescription = SerializerHelper.SerializeToString<FAMCustomerDescription>(saveRequest.CustomerDescription, true, false);

                string tempRateCurrencyID = saveRequest.PayCurrencyID.IsNullOrEmpty() ? string.Empty : saveRequest.RateCurrencyID.Join();
                string tempRateValue = saveRequest.PayCurrencyID.IsNullOrEmpty() ? string.Empty : saveRequest.RateValue.Join();

                string tempFeeIds = saveRequest.FeeIDs.Join();
                string tempFeeWays = saveRequest.FeeWays.Join();
                string tempFeeTypes = saveRequest.FeeTypes.Join();
                string tempFeeChargingCodeIds = saveRequest.FeeChargingCodeIDs.Join();
                string tempFeeCurrencyIds = saveRequest.FeeCurrencyIDs.Join();
                string tempFeeRates = saveRequest.FeeRates.Join();
                string tempFeeContainerIDs = saveRequest.FeeContainerIDs.Join();
                string tempFeeUnitIds = saveRequest.FeeUnitIDs.Join();
                string tempFeeUnitPrices = saveRequest.FeeUnitPrices.Join();
                string tempFeeQuantities = saveRequest.FeeQuantities.Join();
                string tempFeeAmounts = saveRequest.FeeAmounts.Join();
                string tempFeeRemarks = saveRequest.FeeRemarks.Join();
                string tempFeeUpdateDates = saveRequest.FeeUpdateDates.Join();

                string tempFeeIsAgents = saveRequest.FeeIsAgents.Join();
                string tempFeeIsSecondSales = saveRequest.FeeIsSecondSales.Join();
                string tempFeeIsVATInvoiceds = saveRequest.FeeIsVATInvoiceds.Join();
                string tempFeeIsGsts = saveRequest.FeeIsGSTs.Join();
                string tempFeeFromTypes = saveRequest.FeeFromTypes.Join();
                string tempFeeIsRevises = saveRequest.FeeIsRevises.Join();
                #endregion

                #region 传递参数

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, saveRequest.OperationID);
                db.AddInParameter(dbCommand, "@FormID", DbType.Guid, saveRequest.FormID);
                db.AddInParameter(dbCommand, "@FormType", DbType.Byte, saveRequest.FormType);
                db.AddInParameter(dbCommand, "@Id", DbType.Guid, saveRequest.ID);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, saveRequest.CompanyID);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, saveRequest.CustomerID);
                db.AddInParameter(dbCommand, "@CustomerDescription", DbType.String, tempCustomerDescription);
                db.AddInParameter(dbCommand, "@ShipToID", DbType.Guid, saveRequest.ShipToID);
                db.AddInParameter(dbCommand, "@CustomerRefNo", DbType.String, saveRequest.CustomerRefNo);
                db.AddInParameter(dbCommand, "@Type", DbType.Byte, saveRequest.Type);
                db.AddInParameter(dbCommand, "@AccountDate", DbType.DateTime, saveRequest.AccountDate);
                db.AddInParameter(dbCommand, "@DueDate", DbType.DateTime, saveRequest.DueDate);
                db.AddInParameter(dbCommand, "@PayCurrencyId", DbType.Guid, saveRequest.PayCurrencyID);
                db.AddInParameter(dbCommand, "@RateCurrencyIDs", DbType.String, tempRateCurrencyID);
                db.AddInParameter(dbCommand, "@RateValues", DbType.String, tempRateValue);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, saveRequest.Remark);
                db.AddInParameter(dbCommand, "@IsVATInvoiced", DbType.Boolean, saveRequest.IsVATInvoiced);
                db.AddInParameter(dbCommand, "@Taxrate", DbType.Decimal, saveRequest.TaxRate);
                db.AddInParameter(dbCommand, "@BillFromType", DbType.Int16, saveRequest.BillFromType);
                db.AddInParameter(dbCommand, "@FeeIDs", DbType.String, tempFeeIds);
                db.AddInParameter(dbCommand, "@FeeWays", DbType.String, tempFeeWays);
                db.AddInParameter(dbCommand, "@FeeTypes", DbType.String, tempFeeTypes);
                db.AddInParameter(dbCommand, "@FeeIsAgents", DbType.String, tempFeeIsAgents);
                db.AddInParameter(dbCommand, "@FeeIsSecondSales", DbType.String, tempFeeIsSecondSales);
                db.AddInParameter(dbCommand, "@FeeIsVATInvoiceds", DbType.String, tempFeeIsVATInvoiceds);
                db.AddInParameter(dbCommand, "@FeeIsGsts", DbType.String, tempFeeIsGsts);
                db.AddInParameter(dbCommand, "@FeeChargingCodeIDs", DbType.String, tempFeeChargingCodeIds);
                db.AddInParameter(dbCommand, "@FeeDescriptions", DbType.String, saveRequest.FeeDescriptions.Join());
                db.AddInParameter(dbCommand, "@FeeCurrencyIDs", DbType.String, tempFeeCurrencyIds);
                db.AddInParameter(dbCommand, "@FeeQuantities", DbType.String, tempFeeQuantities);
                db.AddInParameter(dbCommand, "@FeeRates", DbType.String, tempFeeRates);
                db.AddInParameter(dbCommand, "@FeeContainerIDs", DbType.String, tempFeeContainerIDs);
                db.AddInParameter(dbCommand, "@FeeUnitIDs", DbType.String, tempFeeUnitIds);
                db.AddInParameter(dbCommand, "@FeeUnitPrices", DbType.String, tempFeeUnitPrices);
                db.AddInParameter(dbCommand, "@FeeAmounts", DbType.String, tempFeeAmounts);
                db.AddInParameter(dbCommand, "@FeeRemarks", DbType.String, tempFeeRemarks);
                db.AddInParameter(dbCommand, "@FeeUpdateDates", DbType.String, tempFeeUpdateDates);
                db.AddInParameter(dbCommand, "@FeeFromTypes", DbType.String, tempFeeFromTypes);
                db.AddInParameter(dbCommand, "@FeeIsRevises", DbType.String, tempFeeIsRevises);
                db.AddInParameter(dbCommand, "@IsClosingEdit", DbType.Boolean, saveRequest.IsClosingEdit);
                db.AddInParameter(dbCommand, "@IsAPPCFM", DbType.Boolean, saveRequest.IsAPPCFM);
                db.AddInParameter(dbCommand, "@SaveSource", DbType.Boolean, saveRequest.SaveSource);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.SaveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, saveRequest.UpdateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                #endregion
                stopwatchBooking.Start();
                ManyResult[] results = db.ManyResults(dbCommand, new string[][] { new string[] { "ID", "No","UpdateDate","ApplyState" }, 
                    new string[] { "ID", "UpdateDate" } });



                if (results == null
                    || results.Length < 2
                    || results[0].Items.Count == 0)
                {
                    return null;
                }
                string tempbillNo = results[0].Items[0].GetValue<string>("No");
                bool isCreateBill = saveRequest.ID == Guid.Empty ? true : false;
                saveRequest.BillNo = isCreateBill ? tempbillNo : saveRequest.BillNo;
                EmailNotifyInfo notifyInfo = new EmailNotifyInfo(isCreateBill, saveRequest.BillNo, saveRequest.AuditorID, saveRequest.State, saveRequest.CompanyID, saveRequest.AuditorEmail, saveRequest.OperationID, saveRequest.OperationDate);
                SendEmail(notifyInfo);
                HierarchyManyResult result = new HierarchyManyResult(results[0].Items[0].Copy());
                if (results[1] != null)
                {
                    foreach (SingleResult s in results[1].Items)
                    {
                        result.Childs.Add(new HierarchyManyResult(s));
                    }
                    saveRequest.FeeIDs = results[1].Items.Select(fitem => fitem.GetValue<Guid?>("ID")).ToArray();
                }
                OperationLogService.Add(DateTime.Now, "SAVE-BILL-DB", string.Format("数据库保存;Bill ID[{0}] Operation ID[{1}]", result.GetValue<Guid>("ID"), saveRequest.OperationID.ToString()), stopwatchBooking.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
                //应收/非退佣费用
#if DEBUG
                if (saveRequest.Type == BillType.AR)
                {
                    SaveBillForCSP(saveRequest);
                    SaveBillFeeForCSP(saveRequest);
                }
#endif
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

        #region GetCustomerAgingList
        /// <summary>
        /// 账款列表
        /// </summary>
        /// <param name="endingDate"></param>
        /// <param name="companyIDs"></param>
        /// <param name="billTypes"></param>
        /// <param name="operationTypes"></param>
        /// <param name="customerId"></param>
        /// <param name="SearchType"></param>
        /// <param name="onlyOverPaid"></param>
        /// <param name="termType"></param>
        /// <param name="agingDateState"></param>
        /// <returns></returns>
        public List<CustomerAgingList> GetCustomerAgingList(AccReceControlSearchParameter arcSearchParameter)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetCustomerAgingList");

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, arcSearchParameter.CompanyIDs.Join());
                db.AddInParameter(dbCommand, "@CustomerIds", DbType.Guid, arcSearchParameter.CustomerID);
                db.AddInParameter(dbCommand, "@billTypes", DbType.String, arcSearchParameter.BillTypes.Join());
                db.AddInParameter(dbCommand, "@endingDate", DbType.DateTime, arcSearchParameter.EndingDate);
                db.AddInParameter(dbCommand, "@operationTypes", DbType.String, arcSearchParameter.OperationTypes.Join());
                db.AddInParameter(dbCommand, "@DisplayTermOrBuyCustomer", DbType.Int16, arcSearchParameter.TermType);
                db.AddInParameter(dbCommand, "@InsuredType", DbType.Int16, arcSearchParameter.InsuredType);
                db.AddInParameter(dbCommand, "@SearchType", DbType.Int16, arcSearchParameter.SearchType);
                db.AddInParameter(dbCommand, "@AgingDateState", DbType.Int16, arcSearchParameter.AgingDateState);
                db.AddInParameter(dbCommand, "@PastDueRange", DbType.Int16, arcSearchParameter.PastDueRange);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                #region linq
                List<CustomerAgingList> results = (from b in ds.Tables[0].AsEnumerable()
                                                   select new CustomerAgingList
                                                 {
                                                     Balance = b.Field<decimal>("Balance"),
                                                     Currency = b.Field<string>("Currency"),
                                                     Current = 0m,
                                                     CustomerID = b.Field<Guid>("CustomerID"),
                                                     CustomerName = b.Field<string>("CustomerName"),
                                                     CompanyID = b.Field<Guid>("CompanyID"),
                                                     CompanyName = b.Field<string>("CompanyName"),
                                                     CreditLimit = b.Field<decimal>("CreditLimit"),
                                                     Terms = b.Field<int>("Term"),
                                                     Less30 = b.Field<decimal>("Less30"),
                                                     Over30 = b.Field<decimal>("Over30"),
                                                     Over45 = b.Field<decimal>("Over45"),
                                                     Over60 = b.Field<decimal>("Over60"),
                                                     Over90 = b.Field<decimal>("Over90"),
                                                     MainCurBalance = b.Field<decimal>("MainCurBalance"),
                                                     ShptWorth = b.Field<decimal>("ShptWorth"),
                                                     PastDu = b.Field<decimal>("PastDu"),
                                                     PastDueAmount = b.Field<decimal>("PastDueAmount"),
                                                     LastMemo = b.Field<string>("LogLastContent"),
                                                     LastUpdate = b.Field<DateTime?>("LogLastUpdate"),
                                                     LastUpdateBy = b.Field<string>("LogLastUpdateBy"),
                                                 }).ToList();
                #endregion

                return results;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region GetCustomerAgingLogs
        /// <summary>
        /// 获取客户应收账款催收日志
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <returns></returns>
        public string GetCustomerAgingLogs(Guid? customerId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetCustomerAgingLogs");

                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                #region linq
                List<CustomerAgingLogs> Logs = (from b in ds.Tables[0].AsEnumerable()
                                                select new CustomerAgingLogs
                                             {
                                                 ID = b.Field<Guid>("ID"),
                                                 CompanyID = b.Field<Guid>("CompanyID"),
                                                 CustomerID = b.Field<Guid>("CustomerID"),
                                                 CustomerName = b.Field<string>("CustomerName"),
                                                 Content = b.Field<string>("Content"),
                                                 CreateBy = b.Field<Guid>("CreateBy"),
                                                 CreateByName = b.Field<string>("CreateByName"),
                                                 CustomerMark = b.Field<byte>("CustomerMark"),
                                                 MemoTime = b.Field<DateTime>("MemoTime"),
                                                 Priority = b.Field<byte>("Priority"),
                                                 Subject = b.Field<string>("Subject"),
                                                 Type = b.Field<byte>("Type"),
                                             }).ToList();
                if (ds.Tables.Count > 1)
                {
                    List<CustomerAgingLogAtts> LogAtts = (from b in ds.Tables[1].AsEnumerable()
                                                          select new CustomerAgingLogAtts
                                                          {
                                                              ID = b.Field<Guid>("ID"),
                                                              AgingLogID = b.Field<Guid>("AgingLogID"),
                                                              FileID = b.Field<Guid>("FileID"),
                                                              FileName = b.Field<string>("FileName"),
                                                              CreateBy = b.Field<Guid>("CreateBy"),
                                                              CreateByName = b.Field<string>("CreateByName"),
                                                              CreateOn = b.Field<DateTime>("CreateOn"),
                                                              filebyte = b.Field<byte[]>("filebyte"),
                                                          }).ToList();

                    foreach (CustomerAgingLogs log in Logs)
                    {
                        log.logAtts = new List<CustomerAgingLogAtts>();
                        foreach (CustomerAgingLogAtts LogAtt in LogAtts)
                        {
                            if (LogAtt.AgingLogID == log.ID)
                            {
                                log.logAtts.Add(LogAtt);
                            }
                        }
                    }
                }
                #endregion
                string result = Newtonsoft.Json.JsonConvert.SerializeObject(Logs);
                return result;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 保存客户应收账款日志
        /// </summary>
        /// <param name="logInfo"></param>
        public SingleResult SaveCustomerAgingLog(CustomerLogSaveSequest logInfo)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveCustomerAgingLogInfo");

            db.AddInParameter(dbCommand, "@ID", DbType.Guid, logInfo.Id);
            db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, logInfo.CustomerId);
            db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, logInfo.CompanyId);
            db.AddInParameter(dbCommand, "@CustomerMark", DbType.Byte, logInfo.CustomerMark);
            db.AddInParameter(dbCommand, "@Subject", DbType.String, logInfo.Subject);
            db.AddInParameter(dbCommand, "@Content", DbType.String, logInfo.Content);
            db.AddInParameter(dbCommand, "@Priority", DbType.Byte, logInfo.Priority);
            db.AddInParameter(dbCommand, "@Type", DbType.Byte, logInfo.Type);
            db.AddInParameter(dbCommand, "@SaveBy", DbType.Guid, logInfo.SaveBy);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

            SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate" });

            return result;
        }


        /// <summary>
        /// 保存客户应收账款日志文件
        /// </summary>
        /// <param name="AgingID"></param>
        /// <param name="filename"></param>
        /// <param name="file"></param>
        public SingleResult SaveCustomerAgingLogAtts(Guid AgingID, string filename, byte[] file, Guid CreateBy)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveCustomerAgingLogAtts");

            db.AddInParameter(dbCommand, "@ID", DbType.Guid, null);
            db.AddInParameter(dbCommand, "@AgingID", DbType.Guid, AgingID);
            db.AddInParameter(dbCommand, "@Name", DbType.String, filename);
            db.AddInParameter(dbCommand, "@Body", DbType.Binary, file);
            db.AddInParameter(dbCommand, "@CreateBy", DbType.Guid, CreateBy);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

            SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate" });
            return result;
        }

        /// <summary>
        /// 删除客户应收账款文件
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteCustomerAgingLogAtts(Guid ID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspDeleteCustomerAgingLogAtts");

            db.AddInParameter(dbCommand, "@ID", DbType.Guid, ID);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 删除客户应收账款日志
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteCustomerAgingLogs(Guid ID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspDeleteCustomerAgingLog");

            db.AddInParameter(dbCommand, "@ID", DbType.Guid, ID);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

            db.ExecuteNonQuery(dbCommand);
        }


        #endregion

        #region LocalFeeConfigure
        public SingleResult SaveLocalFeeConfigure(LocalFeeConfigureSaveRequest request)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveLoaclFeeConfigure");

                string tempCarrierIDs = request.CarrierIDs.Join();
                string tempShippingLineIDs = request.ShippingLineIDs.Join();
                string tempLocationIDs = request.LocationIDs.Join();
                string tempCompanyIDs = request.CompanyIDs.Join();
                string tempChargeUnits = request.ChargeUnits.Join();
                string tempPrices = request.Prices.Join();

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, request.Id);
                db.AddInParameter(dbCommand, "@IsCarrier", DbType.Boolean, request.IsCarrier);
                db.AddInParameter(dbCommand, "@CarrierIDs", DbType.String, tempCarrierIDs);
                db.AddInParameter(dbCommand, "@IsLocation", DbType.Boolean, request.IsLocation);
                db.AddInParameter(dbCommand, "@LocationIDs", DbType.String, tempLocationIDs);
                db.AddInParameter(dbCommand, "@IsShippingLine", DbType.Boolean, request.IsShippingLine);
                db.AddInParameter(dbCommand, "@ShippingLineIDs", DbType.String, tempShippingLineIDs);
                db.AddInParameter(dbCommand, "@IsCommpany", DbType.Boolean, request.IsCommpany);
                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, tempCompanyIDs);
                db.AddInParameter(dbCommand, "@ChargeID", DbType.Guid, request.ChargeID);
                db.AddInParameter(dbCommand, "@CurrencyID", DbType.Guid, request.CurrencyID);
                db.AddInParameter(dbCommand, "@Prices", DbType.String, tempPrices);
                db.AddInParameter(dbCommand, "@ChargeUnits", DbType.String, tempChargeUnits);
                db.AddInParameter(dbCommand, "@ChargeUnit", DbType.Byte, request.ChargeUnit);
                db.AddInParameter(dbCommand, "@StartDate", DbType.DateTime, request.StartDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.DateTime, request.ENDDate);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, request.IsValid);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, request.SaveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, request.UpdateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

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

        public List<LocalFeeConfigure> GetLoaclFeeConfigureList(Guid[] CarrierIDs, Guid[] LocationIDs, Guid[] ShippingLineIDs, Guid[] CompanyIDs, Guid ChargeID, DateTime StartDate, DateTime EndDate, bool IsValid, bool IsEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetLoaclFeeConfigureList");

                string tempCarrierIDs = CarrierIDs.Join();
                string tempShippingLineIDs = ShippingLineIDs.Join();
                string tempLocationIDs = LocationIDs.Join();
                string tempCompanyIDs = CompanyIDs.Join();

                db.AddInParameter(dbCommand, "@CarrierIDs", DbType.String, tempCarrierIDs);
                db.AddInParameter(dbCommand, "@LocationIDs", DbType.String, tempLocationIDs);
                db.AddInParameter(dbCommand, "@ShippingLineIDs", DbType.String, tempShippingLineIDs);
                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, tempCompanyIDs);
                db.AddInParameter(dbCommand, "@ChargeID", DbType.Guid, ChargeID);
                db.AddInParameter(dbCommand, "@StartDate", DbType.DateTime, StartDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.DateTime, EndDate);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, IsValid);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<LocalFeeConfigure> results = (from b in ds.Tables[0].AsEnumerable()
                                                   select new LocalFeeConfigure
                                                {
                                                    ID = b.Field<Guid>("ID"),
                                                    No = b.Field<int>("No"),
                                                    CarrierIDs = ConvertGuidArray(b.Field<string>("CarrierIDs")),
                                                    CarrierNames = b.Field<string>("CarrierNames"),
                                                    CompanyIDs = ConvertGuidArray(b.Field<string>("CompanyIDs")),
                                                    CompanyNames = b.Field<string>("CompanyNames"),
                                                    LocationIDs = ConvertGuidArray(b.Field<string>("LocationIDs")),
                                                    LocationNames = b.Field<string>("LocationNames"),
                                                    ShippingLineIDs = ConvertGuidArray(b.Field<string>("ShippingLineIDs")),
                                                    ShippingLineNames = b.Field<string>("ShippingLineNames"),
                                                    CurrencyID = ConvertGuidArray(b.Field<string>("CurrencyIDs"))[0],
                                                    CurrencyName = ConvertStringArray(b.Field<string>("CurrencyNames"))[0],
                                                    Prices = ConvertPricesArray(b.Field<string>("ChargeUnitNames"), b.Field<string>("Prices")),
                                                    ChargeID = b.Field<Guid>("ChargeID"),
                                                    ChargeName = b.Field<string>("ChargeName"),
                                                    ChargeUnit = b.Field<byte>("ChargeUnit"),
                                                    ChargeUnitName = b.Field<string>("ChargeUnitName"),
                                                    IsCarrier = b.Field<bool>("IsCarrier"),
                                                    IsCommpany = b.Field<bool>("IsCommpany"),
                                                    IsShippingLine = b.Field<bool>("IsShippingLine"),
                                                    IsLocation = b.Field<bool>("IsLocation"),
                                                    StartDate = b.Field<DateTime>("StartDate"),
                                                    EndDate = b.Field<DateTime>("EndDate"),
                                                    IsValid = b.Field<bool>("IsValid"),
                                                    CreateBy = b.Field<Guid>("CreateBy"),
                                                    CreateByName = b.Field<string>("CreateByName"),
                                                    CreateDate = b.Field<DateTime>("CreateDate"),
                                                    UpdateBy = b.Field<Guid?>("UpdateBy"),
                                                    UpdateByName = b.Field<string>("UpdateByName"),
                                                    UpdateDate = b.Field<DateTime?>("UpdateDate"),
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

        public void DeleteLoaclFeeConfigure(Guid ID, bool IsEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspDeleteLoaclFeeConfigure");
                db.AddInParameter(dbCommand, "@ID", DbType.Guid, ID);
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

        public List<AddLocalFeeList> GetLocalFeeListForOperationID(Guid OperationID, bool IsEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetLocalFeeListForOperationID");
                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, OperationID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<AddLocalFeeList> results = (from b in ds.Tables[0].AsEnumerable()
                                                 select new AddLocalFeeList
                                                   {
                                                       ChargeID = b.Field<Guid>("ChargeID"),
                                                       Code = b.Field<string>("Code"),
                                                       ChargeEname = b.Field<string>("ChargeEname"),
                                                       ChargeCname = b.Field<string>("ChargeCname"),
                                                       CurrencyCode = b.Field<string>("CurrencyCode"),
                                                       CurrencyID = b.Field<Guid>("CurrencyID"),
                                                       Price = b.Field<decimal>("Price"),
                                                       qty = b.Field<int>("qty"),
                                                       CustomerID = b.Field<Guid>("CustomerID"),
                                                       CustomerName = b.Field<string>("CustomerName"),
                                                       Way = b.Field<int>("Way"),
                                                       Amount = b.Field<decimal>("Amount"),
                                                       FeeID = b.Field<Guid?>("FeeID"),
                                                       BillID = b.Field<Guid?>("BillID"),
                                                       BillNo = b.Field<string>("BillNo"),
                                                       BillAmount = b.Field<decimal>("BillAmount"),
                                                       BillWay = b.Field<int>("BillWay"),
                                                       IsSelected = b.Field<bool>("IsSelected"),
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

        private Guid[] ConvertGuidArray(string data)
        {
            if (string.IsNullOrEmpty(data))
                return null;
            else
            {
                string[] strArr = data.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                Guid[] returns = new Guid[strArr.Length];
                for (int i = 0; i < strArr.Length; i++)
                {
                    returns[i] = new Guid(strArr[i]);
                }

                return returns;
            }
        }

        private string[] ConvertStringArray(string data)
        {
            if (string.IsNullOrEmpty(data))
                return null;
            else
            {
                string[] strArr = data.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                return strArr;
            }
        }

        private string ConvertPricesArray(string units, string prices)
        {
            if (string.IsNullOrEmpty(units) || string.IsNullOrEmpty(prices))
                return null;
            else
            {
                string[] unitArr = units.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] priceArr = prices.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string returns = string.Empty;
                if (unitArr.Length == priceArr.Length)
                {
                    for (int i = 0; i < unitArr.Length; i++)
                    {
                        if (string.IsNullOrEmpty(returns))
                        {
                            returns += unitArr[i] + ":" + priceArr[i];
                        }
                        else
                            returns += "," + unitArr[i] + ":" + priceArr[i];
                    }
                }
                return returns;
            }
        }

        #endregion

        #region 获取所属提单的业务的所有应收账单列表
        /// <summary>
        /// 获取所属提单的业务的所有应收账单列表
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <returns>CurrencyBillList</returns>
        public List<CurrencyBillList> GetDRCurrencyBillList(Guid operationId)
        {
            ArgumentHelper.AssertGuidNotEmpty(operationId, "operationID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetDRCurrencyBillList");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CurrencyBillList> results = null;

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

        #region 设置帐单已寄单
        /// <summary>
        /// 设置帐单已寄单
        /// </summary>
        /// <param name="ids">帐单ID</param>
        /// <param name="changeByID">ChangeByID</param>
        /// <param name="updateDates">数据版本</param>
        public ManyResult SetBillIsSend(
            Guid[] ids,
            Guid changeByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertArrayLengthMatch(ids, updateDates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSetBillIsSend");

                string tempIds = ids.Join();
                string tempupdateDates = updateDates.Join();

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@IsSend", DbType.Boolean, true);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempupdateDates);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                db.ExecuteNonQuery(dbCommand);

                ManyResult results = db.ManyResult(dbCommand, new string[] { "ID", "UpdateDate" });
                return results;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        } 
        #endregion

        #region 获取公司的打印汇率列表
        /// <summary>
        /// 获取公司的打印汇率列表
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回公司的汇率列表</returns>
        public List<SolutionExchangeRateList> GetCompanyPrintBillExchangeRateList(Guid companyID)
        {
            ArgumentHelper.AssertGuidNotEmpty(companyID, "companyID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBillPrintRate");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<SolutionExchangeRateList>();
                }

                List<SolutionExchangeRateList> results = (from b in ds.Tables[0].AsEnumerable()
                                                          select new SolutionExchangeRateList
                                                          {
                                                              //ID = b.Field<Guid>("ID"),
                                                              SourceCurrency = b.Field<string>("SourceCurrency"),
                                                              TargetCurrency = b.Field<string>("TargetCurrency"),
                                                              SourceCurrencyID = b.Field<Guid>("SourceCurrencyID"),
                                                              TargetCurrencyID = b.Field<Guid>("TargetCurrencyID"),
                                                              Rate = b.Field<decimal>("Rate"),
                                                              IsValid = b.Field<bool>("IsLeft"),
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

        #endregion

        #region 开始执行新账单的方式时间
        /// <summary>
        /// 开始执行新账单的方式时间。
        /// </summary>
        /// <returns></returns>
        public DateTime? GetStartDateForReviseAgentBill()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetSqlStringCommand("select sm.ufnGetStartDateForReviseAgentBill()");
                object results = db.ExecuteScalar(dbCommand);

                if (results == null)
                {
                    return null;
                }

                return Convert.ToDateTime(results);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        #endregion

        #region 得到你账单可以绑定的参考号
        /// <summary>
        /// 得到你账单可以绑定的参考号ID，NO
        /// </summary>
        /// <param name="operationID">操作ID</param>
        /// <returns></returns>
        public DataTable GetValidReNos(Guid operationID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetSqlStringCommand(" SELECT  om.ID,om.No,3 as FormType FROM fcm.OceanMBLs om WHERE om.OceanBookingID=@OperationID " +
                                                             " AND not EXISTS(SELECT 1 FROM fcm.OceanHBLs oh WHERE oh.OceanMBLID=om.id) " +
                                                             " UNION " +
                                                             " SELECT oh.id,oh.no,4 as FormType FROM fcm.OceanHBLs oh WHERE oh.OceanBookingID=@OperationID ");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
                DataSet ds = db.ExecuteDataSet(dbCommand);

                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        } 
        #endregion

        
        

        #region 邮件通知对象
        /// <summary>
        /// 邮件通知对象
        /// </summary>
        sealed class EmailNotifyInfo
        {
            public string BillId { get; set; }
            public Guid? AuditorId { get; set; }
            public BillState State { get; set; }
            public Guid CompanyId { get; set; }
            public string AuditorEmail { get; set; }
            public Boolean IsNew { get; set; }
            public Guid OperationId { get; set; }
            public DateTime OperationDate { get; set; }
            public EmailNotifyInfo(bool isNew, string billNo, Guid? auditorId, BillState state, Guid companyId, string auditorEmail, Guid operationId, DateTime operationDate)
            {
                BillId = billNo;
                AuditorId = auditorId;
                State = state;
                CompanyId = companyId;
                AuditorEmail = auditorEmail;
                IsNew = isNew;
                OperationId = operationId;
                OperationDate = operationDate;
            }

        } 
        #endregion

        #region 亏损邮件通知对象
        /// <summary>
        /// 亏损邮件通知对象
        /// </summary>
        sealed class DeficitEmailNotifyInfo
        {
            public string OpNos { get; set; }

            public string ToEMail { get; set; }

            public string Subject { get; set; }
        } 
        #endregion
    }
}
