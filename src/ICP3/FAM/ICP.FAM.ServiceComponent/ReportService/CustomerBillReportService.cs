using ICP.Common.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.DataObjects.Report;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
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
    /// 财务服务
    /// </summary>
    public partial class FinanceReportService : IFinanceReportService
    {
        /// <summary>
        /// 获取费用列表报表数据
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <returns>费用清单的报表对象</returns>
        public FeeListReportData GetFeeListReportData(Guid operationID)
        {
            ArgumentHelper.AssertGuidNotEmpty(operationID, "operationID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetFeeListReportData");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                #region linq
                FeeListReportData result = (from b in ds.Tables[0].AsEnumerable()
                                            select new FeeListReportData
                                            {
                                                SONo = b.Field<string>("SONo"),
                                                OperationNo = b.Field<string>("OperationNo"),
                                                BLNo = b.Field<string>("BLNo"),
                                                VesselVoyage = b.Field<string>("VesselVoyage"),
                                                PreClosingDate = b.Field<DateTime?>("PreClosingDate"),
                                                ClosingDate = b.Field<DateTime?>("ClosingDate"),
                                                PreETD = b.Field<DateTime?>("PreETD"),
                                                ETD = b.Field<DateTime?>("ETD"),
                                                ETA = b.Field<DateTime?>("ETA"),
                                                ContainerType = b.Field<string>("ContainerType"),
                                                ContainerNo = b.Field<string>("ContainerNo"),
                                                BillNo = b.Field<string>("BillNo"),
                                                POL = b.Field<string>("POL"),
                                                POD = b.Field<string>("POD"),
                                                Destination = b.Field<string>("Destination"),
                                                FETA = b.Field<DateTime?>("FETA"),
                                                //Assessor = b.Field<string>("Assessor"),
                                                Sales = b.Field<string>("Sales"),
                                                Filer = b.Field<string>("Filer"),
                                                FeeListReportFees = (from c in ds.Tables[1].AsEnumerable()
                                                                     select new FeeListReportFee
                                                                    {
                                                                        ChargeCode = c.Field<string>("ChargeCode"),
                                                                        Way = (FeeWay)c.Field<byte>("Way"),
                                                                        Currency = c.Field<string>("Currency"),
                                                                        Amount = c.Field<decimal>("Amount"),
                                                                        AmountOfUSD = c.Field<decimal>("AmountOfUSD"),
                                                                        BillToName = c.Field<string>("BillToName"),
                                                                    }).ToList(),
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
        /// 获取报表帐单列表
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <returns>返回报表帐单</returns>
        public CommonBillReportData GetCommonBillReportData(Guid operationID)
        {
            ArgumentHelper.AssertGuidNotEmpty(operationID, "operationID");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetCommonBillReportData");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                #region linq
                CommonBillReportData result = (from b in ds.Tables[0].AsEnumerable()
                                               select new CommonBillReportData
                                               {
                                                   ContainerNo = b.Field<string>("ContainerNo"),
                                                   ContainerType = b.Field<string>("ContainerType"),
                                                   Destination = b.Field<string>("Destination"),
                                                   ETA = b.Field<DateTime?>("ETA") == null ? string.Empty : b.Field<DateTime>("ETA").ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                   ETD = b.Field<DateTime?>("ETD") == null ? string.Empty : b.Field<DateTime>("ETD").ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                   FETA = b.Field<DateTime?>("FETA") == null ? string.Empty : b.Field<DateTime>("FETA").ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                   POD = b.Field<string>("POD"),
                                                   POL = b.Field<string>("POL"),
                                                   VesselVoyage = b.Field<string>("VesselVoyage"),
                                                   PreVesselVoyage = b.Field<string>("PreVesselVoyage"),
                                                   SalesInfo = b.Field<string>("SalesInfo"),
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
        /// 获取报表帐单列表
        /// </summary>
        /// <param name="billId">billId</param>
        /// <returns>返回报表帐单</returns>
        public LocalBillReportData GetLocalBillReportData(Guid billId)
        {
            ArgumentHelper.AssertGuidNotEmpty(billId, "billId");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetLocalBillReportData");

                db.AddInParameter(dbCommand, "@billId", DbType.Guid, billId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                #region linq
                LocalBillReportData result = (from b in ds.Tables[0].AsEnumerable()
                                              select new LocalBillReportData
                                               {
                                                   ContainerNo = b.Field<string>("ContainerNo"),
                                                   Destination = b.Field<string>("Destination"),
                                                   ETD = b.Field<DateTime?>("ETD") == null ? string.Empty : b.Field<DateTime>("ETD").ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                   FETA = b.Field<DateTime?>("FETA") == null ? string.Empty : b.Field<DateTime>("FETA").ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                   POD = b.Field<string>("POD"),
                                                   POL = b.Field<string>("POL"),
                                                   VesselVoyage = b.Field<string>("VesselVoyage"),
                                                   AgentRefNo = b.Field<string>("AgentRefNo"),
                                                   Carrier = b.Field<string>("Carrier"),
                                                   Commodity = b.Field<string>("Commodity"),
                                                   Consignee = b.Field<string>("Consignee"),
                                                   ETA = b.Field<DateTime?>("ETA") == null ? string.Empty : b.Field<DateTime>("ETA").ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                   HBLNo = b.Field<string>("HBLNo"),
                                                   PaymentMode = b.Field<string>("PaymentMode"),
                                                   MBLNo = b.Field<string>("MBLNo"),
                                                   Memo = b.Field<string>("Memo"),
                                                   Notify = b.Field<string>("Notify"),
                                                   Measurement = b.Field<string>("Measurement"),
                                                   Weight = b.Field<string>("Weight"),
                                                   Quantity = b.Field<string>("Quantity"),
                                                   Shipper = b.Field<string>("Shipper"),
                                                   truckerDescription = SerializerHelper.DeserializeFromString<CustomerDescription>(b.Field<string>("DeliveryAtDescription")),
                                                   CustomerRefNo = b.Field<string>("CustomerRefNo")
                                               }).SingleOrDefault();
                #endregion

                return result;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        #region  后缀号
        /// <summary>
        /// 获取后缀号And数据库里最大后缀号
        /// </summary>
        /// <param name="billID">帐单ID</param>
        /// <returns>SuffixNo</returns>
        public SingleResult GetSuffixNo(Guid billID)
        {
            ArgumentHelper.AssertGuidNotEmpty(billID, "billID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetSuffixNo");

                db.AddInParameter(dbCommand, "@billID", DbType.Guid, billID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }
            
                SingleResult result = db.SingleResult(dbCommand, new string[] { "SuffixNo", "MaxNo" });
                return result;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 保存后缀号
        /// </summary>
        /// <param name="billID">帐单ID</param>
        /// <param name="suffixNo">suffixNo</param>
        /// <param name="saveByID">saveByID</param>
        public void SaveSuffixNo(Guid billID, string suffixNo, Guid saveByID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveSuffixNo");

                db.AddInParameter(dbCommand, "@BillID", DbType.Guid, billID);
                db.AddInParameter(dbCommand, "@suffixNo", DbType.String, suffixNo);
                db.AddInParameter(dbCommand, "@saveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return; }

                return;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }

        }
        #endregion

        #region 打印用的汇率列表

        /// <summary>
        /// 获取帐单打印用的汇率列表
        /// </summary>
        /// <param name="billID">帐单ID</param>
        /// <returns>SolutionExchangeRateListe</returns>
        public List<SolutionExchangeRateList> GetPrintBillRateList(Guid billID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetPrintBillRateList");

                db.AddInParameter(dbCommand, "@billID", DbType.Guid, billID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }



                #region linq
                List<SolutionExchangeRateList> result = (from b in ds.Tables[0].AsEnumerable()
                                                         select new SolutionExchangeRateList
                                                          {
                                                              ID = b.Field<Guid>("ID"),
                                                              SourceCurrency = b.Field<string>("SourceCurrency"),
                                                              TargetCurrency = b.Field<string>("TargetCurrency"),
                                                              Rate = b.Field<decimal>("Rate"),
                                                              CreateByID = b.Field<Guid>("CreateByID")
                                                          }).ToList();
                #endregion

                return result;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }


        /// <summary>
        /// 保存帐单打印用的汇率列表
        /// </summary>
        /// <param name="billID">帐单ID</param>
        /// <param name="sourceCurrencys">tagerCurrencys</param>
        /// <param name="tagerCurrencys">tagerCurrencys</param>
        /// <param name="saveByID">saveByID</param>
        /// <param name="rates">rates</param>
        public void SavePrintBillRateList(Guid billID, string[] sourceCurrencys, string[] tagerCurrencys, decimal[] rates, Guid saveByID)
        {
            ArgumentHelper.AssertGuidNotEmpty(billID, "billID");
            ArgumentHelper.AssertArrayLengthMatch(sourceCurrencys, tagerCurrencys, rates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSavePrintBillRateList");

                #region 构建 数组string拼装串


                string tempsourceCurrencys = sourceCurrencys.Join();
                string temptagerCurrencys = tagerCurrencys.Join();
                string temprates = rates.Join();

                #endregion

                db.AddInParameter(dbCommand, "@billID", DbType.Guid, billID);
                db.AddInParameter(dbCommand, "@saveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@sourceCurrencys", DbType.String, tempsourceCurrencys);
                db.AddInParameter(dbCommand, "@targetCurrencys", DbType.String, temptagerCurrencys);
                db.AddInParameter(dbCommand, "@rates", DbType.String, temprates);


                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return; }

                return;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region 获得催款单报表数据
        /// <summary>
        /// 获得催款单报表数据
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="userID">用户ID</param>
        /// <param name="bankID">银行ID</param>
        /// <param name="billIDs">帐单ID集合</param>
        /// <returns></returns>
        public BillDunReportData GetBillDunReportData(
                    Guid customerID,
                    Guid companyID,
                    Guid userID,
                    Guid? bankID,
                    Guid[] billIDs)
        {

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBillDunReportData");

                db.AddInParameter(dbCommand, "@CusotmerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@BankID", DbType.Guid, bankID);
                db.AddInParameter(dbCommand, "@BillIDs", DbType.String, billIDs.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }
                
                BillDunReportData reportDate = new BillDunReportData();

                #region 客户信息
                BillDunReportDataCustomerInfo customerInfo = (from b in ds.Tables[0].AsEnumerable()
                                                              select new BillDunReportDataCustomerInfo
                                                               {
                                                                   CustomerName = b.Field<string>("CustomerName"),
                                                                   CustomerAddress = b.Field<string>("CustomerAddress"),
                                                                   CustomerAttn = b.Field<string>("CustomerAttn"),
                                                                   CustomerTel = b.Field<string>("CustomerTel"),
                                                                   CustomerFax = b.Field<string>("CustomerFax")
                                                               }).SingleOrDefault();
                #endregion

                #region 公司信息
                BillDunReportDataCompanyInfo companyInfo = (from b in ds.Tables[1].AsEnumerable()
                                                            select new BillDunReportDataCompanyInfo
                                                              {
                                                                  CompanyName = b.Field<string>("CompanyName"),
                                                                  CompanyAddress = b.Field<string>("CompanyAddress"),
                                                                  UserName = b.Field<string>("UserName"),
                                                                  UserTel = b.Field<string>("UserTel"),
                                                                  UserFax = b.Field<string>("UserFax"),
                                                                  BankName = b.Field<string>("BankName")
                                                              }).SingleOrDefault();
                #endregion

                #region 费用信息-DB
                List<BillDunReportDataCostInfo> dbCostList = (from b in ds.Tables[2].AsEnumerable()
                                                              select new BillDunReportDataCostInfo
                                                                {
                                                                    BillNo = b.Field<string>("BillNo"),
                                                                    ETD = b.Field<DateTime?>("ETD"),
                                                                    ETA = b.Field<DateTime?>("ETA"),
                                                                    BLNo = b.Field<string>("BLNo"),
                                                                    ContainerNos = b.Field<string>("ContainerNos"),
                                                                    PODName = b.Field<string>("PODName"),
                                                                    ChargeName = b.Field<string>("ChargeName"),
                                                                    CurrencyName = b.Field<string>("CurrencyName"),
                                                                    CurrencyID = b.Field<Guid>("CurrencyID"),
                                                                    Amount = b.Field<decimal?>("Amount"),
                                                                    InvoiceNo = b.Field<string>("InvoiceNo")
                                                                }).ToList();
                #endregion

                #region 帐号信息

                List<BillDunReportDataAccountInfo> accountList = (from b in ds.Tables[3].AsEnumerable()
                                                                  select new BillDunReportDataAccountInfo
                                                                  {
                                                                      AccountNo = b.Field<string>("AccountNo"),
                                                                      CurrencyName = b.Field<string>("CurrencyName")
                                                                  }).ToList();

                #endregion

                reportDate.AccountList = accountList;
                reportDate.CompanyInfo = companyInfo;
                reportDate.CustomerInfo = customerInfo;
                reportDate.DBCostList = dbCostList;


                return reportDate;



            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        /// <summary>
        /// 获取报表账龄列表
        /// </summary>
        /// <param name="StructType"></param>
        /// <param name="StructNodeId"></param>
        /// <param name="ETD_Ending_Date"></param>
        /// <param name="ViewType"></param>
        /// <param name="IsEnglish"></param>
        /// <param name="GroupBy"></param>
        /// <returns></returns>
        public List<DebitnoteAgeSumData> GetDebitnoteAgeReportData(byte StructType, string StructNodeId, DateTime ETD_Ending_Date, byte ViewType, string IsEnglish, string GroupBy)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.SPR_REPDW_GetDebitnoteAge_Sum");

                db.AddInParameter(dbCommand, "@StructType", DbType.Byte, StructType);
                db.AddInParameter(dbCommand, "@StructNodeId", DbType.String, StructNodeId);
                db.AddInParameter(dbCommand, "@ETD_Ending_Date", DbType.DateTime, ETD_Ending_Date);
                db.AddInParameter(dbCommand, "@ViewType", DbType.Byte, ViewType);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.String, IsEnglish);
                db.AddInParameter(dbCommand, "@GroupBy", DbType.String, GroupBy);

                DataSet ds = null;
                
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                #region linq
                List<DebitnoteAgeSumData> result = (from b in ds.Tables[0].AsEnumerable()
                                                    select new DebitnoteAgeSumData
                                                    {
                                                        Area = b.Field<string>("Area"),
                                                        Company = b.Field<string>("Company"),
                                                        Period1 = b.Field<decimal>("Period1"),
                                                        Period2 = b.Field<decimal>("Period2"),
                                                        Period3 = b.Field<decimal>("Period3"),
                                                        Period4 = b.Field<decimal>("Period4"),
                                                        USDALLAMOUNT = b.Field<decimal>("USDALLAMOUNT"),
                                                        CusNum = b.Field<int>("CusNum"),
                                                        Monthly = b.Field<int?>("Monthly"),
                                                    }).ToList();
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
        /// 批量账单
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="billIDs">帐单ID集合</param>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public BatchBillReportData GetBatchBillReportData(Guid customerID,
                    Guid companyID, Guid[] billIDs, Guid userID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBatchBillReportData");

                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@BillIDs", DbType.String, billIDs.Join());
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }
                if (ds.Tables[0].Rows.Count > 1)
                {
                    throw new Exception("查询到多个客户信息，禁止打印!");
                }
                BatchBillReportData result = (from b in ds.Tables[0].AsEnumerable()
                                              select new BatchBillReportData
                                            {

                                                CompanyDsc = b.Field<string>("CompanyDsc"),
                                                CustomerDsc = b.Field<string>("CustomerDsc"),
                                                AccountDate = b.Field<DateTime?>("AccountDate") == null ? string.Empty : b.Field<DateTime>("AccountDate").ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                DueDate = b.Field<DateTime?>("DueDate") == null ? string.Empty : b.Field<DateTime>("DueDate").ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                                                Trem = b.Field<int>("Trem"),
                                                CurrentUser = b.Field<string>("CurrentUser"),
                                                ManifestList = (from f in ds.Tables[1].AsEnumerable()
                                                                select new BillManifestInfo
                                                             {
                                                                 OperationNo = f.Field<string>("OperationNo"),
                                                                 ContainerNo = f.Field<string>("ContainerNo"),
                                                                 DescriptionOfCharges = f.Field<string>("DescriptionOfCharges"),
                                                                 CntQty = f.Field<decimal>("CntQty"),
                                                                 ChargeRate = f.Field<decimal>("ChargeRate"),
                                                                 ChargeWay = (FeeWay)f.Field<byte>("ChargeWay"),
                                                                 ChargePrice = f.Field<decimal>("ChargePrice"),
                                                                 ChargeAmount = f.Field<decimal>("ChargeAmount"),
                                                                 CurrencyName = f.Field<string>("CurrencyName"),
                                                                 Remark = f.Field<string>("Remark"),
                                                             }).ToList()
                                            }).SingleOrDefault();
                return result;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }
    }
}
