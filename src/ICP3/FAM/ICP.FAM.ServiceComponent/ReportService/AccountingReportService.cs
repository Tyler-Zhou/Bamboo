namespace ICP.FAM.ServiceComponent
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using ICP.FAM.ServiceInterface;
    using ICP.FAM.ServiceInterface.DataObjects;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Framework.CommonLibrary.Helper;
    using ICP.Common.ServiceInterface.DataObjects;


    /// <summary>
    /// 财务服务
    /// </summary>
    public partial class FinanceReportService : IFinanceReportService
    {
        #region 获取会计科目
        /// <summary>
        /// GetGLDetail
        /// </summary>
        /// <param name="glCode">glCode</param>
        /// <param name="fromDate">fromDate</param>
        /// <param name="toDate">toDate</param>
        /// <param name="companyIDs">companyIDs</param>
        /// <param name="billTypes">billTypes</param>
        /// <returns>LedgerData</returns>
        public List<LedgerData> GetGLDetail(string glCode, DateTime fromDate, DateTime toDate, Guid[] companyIDs, ReportBillType[] billTypes)
        {
            try
            {
                #region 未知的2.0逻辑
                List<ReportBillType> types = new List<ReportBillType>();
                foreach (var item in billTypes) types.Add(item);

                //if (billTypes.Contains(ReportBillType.Check) || billTypes.Contains(ReportBillType.Deposit))
                //    types.Add(ReportBillType.Clearance);不需要 -YGY

                #endregion

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetGLDetail");

                db.AddInParameter(dbCommand, "@glCode", DbType.String, glCode);
                db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@companyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@billTypes", DbType.String, types.ToArray().Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                #region linq
                List<LedgerData> result = (from b in ds.Tables[0].AsEnumerable()
                                           select new LedgerData
                                            {
                                                Id = b.Field<Guid>("Id"),
                                                GLId = b.Field<Guid>("GLId"),
                                                GLCode = b.Field<string>("GLCode"),
                                                GLDescription = b.Field<string>("GLDescription"),
                                                Date = b.Field<DateTime>("Date"),
                                                //BeginBalance = b.Field<decimal>("BeginBalance"),
                                                CrAmt = b.Field<decimal>("CrAmt"),
                                                DrAmt = b.Field<decimal>("DrAmt"),
                                                //Balance = b.Field<decimal>("Balance"),
                                                OrgAmt = b.Field<decimal>("OrgAmt"),
                                                Rate = b.Field<decimal>("Rate"),
                                                Remark = b.Field<string>("Remark"),
                                                BillType = (ReportBillType)b.Field<Byte>("Type"),
                                                BillId = b.Field<Guid>("BillId"),
                                                BillNo = b.Field<string>("BillNo"),
                                                CustomerFinanceCode = b.Field<string>("CustomerFinanceCode"),
                                                OperationNo = b.Field<string>("OperationNo"),
                                                CustomerName = b.Field<string>("CustomerName"),
                                                CustomerID = b.Field<Guid>("CustomerID")
                                            }).ToList();
                #endregion

                return result;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region GetGLSummary
        /// <summary>
        /// GetGLSummary
        /// </summary>
        /// <param name="fromDate">fromDate</param>
        /// <param name="toDate">toDate</param>
        /// <param name="companyIDs">companyIDs</param>
        /// <param name="billTypes">billTypes</param>
        /// <returns>LedgerData</returns>
        public List<GLData> GetGLSummary(DateTime fromDate, DateTime toDate, Guid[] companyIDs, ReportBillType[] billTypes)
        {
            try
            {
                #region 未知的2.0逻辑
                List<ReportBillType> types = new List<ReportBillType>();
                foreach (var item in billTypes) types.Add(item);

                //if (billTypes.Contains(ReportBillType.Check) || billTypes.Contains(ReportBillType.Deposit))
                //    types.Add(ReportBillType.Clearance); 不需要 -YGY

                #endregion

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetGLSummary");

                db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@companyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@billTypes", DbType.String, types.ToArray().Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                #region linq
                List<GLData> result = (from b in ds.Tables[0].AsEnumerable()
                                           select new GLData
                                           {
                                               Id = b.Field<Guid>("Id"),
                                               Code = b.Field<string>("Code"),
                                               Description = b.Field<string>("Description"),
                                               CategoryType = (ChargingGroupType)b.Field<Byte>("CategoryType"),
                                               //Category = b.Field<string>("GLDescription"),
                                               BeginBalance = b.Field<decimal>("BeginBalance"),
                                               Debit = b.Field<decimal>("Debit"),
                                               Credit = b.Field<decimal>("Credit"),
                                               Balance = b.Field<decimal>("Balance"),
                                           }).ToList();
                #endregion


                return result;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region GetTrialBalance
        /// <summary>
        /// GetGLSummary
        /// </summary>
        /// <param name="glCode">glCode</param>
        /// <param name="toDate">toDate</param>
        /// <param name="companyIDs">companyIDs</param>
        /// <param name="billTypes">billTypes</param>
        /// <returns>LedgerData</returns>
        public List<GLData> GetTrialBalance(DateTime toDate, Guid[] companyIDs, ReportBillType[] billTypes)
        {
            try
            {
                #region 未知的2.0逻辑
                List<ReportBillType> types = new List<ReportBillType>();
                foreach (var item in billTypes) types.Add(item);

                //if (billTypes.Contains(ReportBillType.Check) || billTypes.Contains(ReportBillType.Deposit))
                //    types.Add(ReportBillType.Clearance);不需要 -YGY

                #endregion

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetTrialBalance");

                db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@companyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@billTypes", DbType.String, types.ToArray().Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                #region linq
                List<GLData> result = (from b in ds.Tables[0].AsEnumerable()
                                       select new GLData
                                       {
                                           Id = b.Field<Guid>("Id"),
                                           Code = b.Field<string>("Code"),
                                           Description = b.Field<string>("Description"),
                                           CategoryType = (ChargingGroupType)b.Field<Byte>("CategoryType"),
                                           Debit = b.Field<decimal>("Debit"),
                                           Credit = b.Field<decimal>("Credit"),
                                           Balance = b.Field<decimal>("Balance"),
                                       }).ToList();
                #endregion


                return result;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region GetBalanceSheet
        /// <summary>
        /// GetBalanceSheet
        /// </summary>
        /// <param name="glCode">glCode</param>
        /// <param name="fromDate">fromDate</param>
        /// <param name="toDate">toDate</param>
        /// <param name="companyIDs">companyIDs</param>
        /// <returns>LedgerData</returns>
        public GLDataList GetBalanceSheet(DateTime fromDate, DateTime toDate, Guid[] companyIDs)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBalanceSheet");

                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) 
                { 
                    return null;
                }

                List<GLData> lieqList=new List<GLData>();
                List<GLData> assetsList=new List<GLData>();
                #region linq
                lieqList = (from b in ds.Tables[0].AsEnumerable()
                                       select new GLData
                                       {
                                           Id = b.Field<Guid>("Id"),
                                           Code = b.Field<string>("Code"),
                                           Description = b.Field<string>("Description"),
                                           CategoryType = (ChargingGroupType)b.Field<Byte>("CategoryType"),
                                           CategoryTypeCName = b.Field<string>("CategoryTypeCName"),
                                           CategoryTypeEName = b.Field<string>("CategoryTypeEName"),
                                           Balance = b.Field<decimal>("Balance"),
                                       }).ToList();

                if (ds.Tables.Count>=2)
                {
                  assetsList = (from b in ds.Tables[1].AsEnumerable()
                                         select new GLData
                                         {
                                             Id = b.Field<Guid>("Id"),
                                             Code = b.Field<string>("Code"),
                                             Description = b.Field<string>("Description"),
                                             CategoryType = (ChargingGroupType)b.Field<Byte>("CategoryType"),
                                             CategoryTypeCName = b.Field<string>("CategoryTypeCName"),
                                             CategoryTypeEName = b.Field<string>("CategoryTypeEName"),
                                             Balance = b.Field<decimal>("Balance"),
                                         }).ToList();
                 }
                #endregion

                if (assetsList == null)
                {
                    assetsList = new List<GLData>();
                }
                if (lieqList == null)
                {
                    lieqList = new List<GLData>();
                }

                GLDataList items = new GLDataList();
                items.AssetsList = assetsList;
                items.LieqList = lieqList;

                return items;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region GetIncome
        /// <summary>
        /// GetBalanceSheet
        /// </summary>
        /// <param name="glCode">glCode</param>
        /// <param name="fromDate">fromDate</param>
        /// <param name="toDate">toDate</param>
        /// <param name="companyIDs">companyIDs</param>
        /// <returns>LedgerData</returns>
        public GLDataAndTotalInfo GetIncome(DateTime fromDate, DateTime toDate, Guid[] companyIDs,bool isIncomeOrder)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetIncome");

                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@IsIncomeOrder", DbType.Boolean, isIncomeOrder);                
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                #region linq
                List<GLData> result = (from b in ds.Tables[0].AsEnumerable()
                                       select new GLData
                                       {
                                           Id = b.Field<Guid>("Id"),
                                           Code = b.Field<string>("Code"),
                                           Description = b.Field<string>("Description"),
                                           CategoryType = (ChargingGroupType)b.Field<Byte>("CategoryType"),
                                           CategoryTypeCName = b.Field<string>("CategoryTypeCName"),
                                           CategoryTypeEName = b.Field<string>("CategoryTypeEName"),
                                           //Category = b.Field<string>("GLDescription"),
                                           Balance = b.Field<decimal>("Balance"),
                                       }).ToList();
                #endregion


                GLDataTotal total = new GLDataTotal();
                if (ds.Tables.Count >= 2)
                {
                    total = (from d in ds.Tables[1].AsEnumerable()
                             select new GLDataTotal
                             {
                                 IncomeAmount = d.Field<decimal>("IncomeAmount"),
                                 CostAmount = d.Field<decimal>("CostAmount"),
                                 CrossIncome = d.Field<decimal>("CrossIncome"),
                                 OtherAmount = d.Field<decimal>("OtherAmount")
                             }).SingleOrDefault();
                }

                GLDataAndTotalInfo list = new GLDataAndTotalInfo();

                if (result == null)
                {
                    result = new List<GLData>();
                }
                if (total == null)
                {
                    total = new GLDataTotal();
                }

                list.GLDataList = result;
                list.DataTottal = total;


                return list;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region  GetCheckData
        /// <summary>
        /// GetCheckDatas
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>PaymentCheckData</returns>
        public CheckData GetCheckData(Guid id)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetCheckData");

                db.AddInParameter(dbCommand, "@id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                #region linq
                CheckData result = (from b in ds.Tables[0].AsEnumerable()
                                    select new CheckData
                                       {
                                           Id = b.Field<Guid>("Id"),
                                           Amount = b.Field<decimal>("Amount"),
                                           BankAccountId = b.Field<Guid>("BankAccountId"),
                                           BankDate = b.Field<DateTime?>("BankDate"),
                                           BankAccountDescription = b.Field<string>("BankName") +"-" + b.Field<string>("BankCurrency"),
                                           CheckNo = b.Field<string>("CheckNo"),
                                           CompanyId = b.Field<Guid>("CompanyId"),
                                           CompanyName = b.Field<string>("CompanyName"),
                                           CustomerId = b.Field<Guid>("CustomerId"),
                                           CustomerName = b.Field<string>("CustomerName"),
                                           No = b.Field<string>("No"),
                                           CheckDate = b.Field<DateTime>("CheckDate"),
                                           Remark = b.Field<string>("Remark"),
                                       }).SingleOrDefault();
                #endregion


                return result;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region  GetReportBillData
        /// <summary>
        /// GetReportBillData
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>ReportBillData</returns>
        public ReportBillData GetReportBillData(Guid id)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetReportBillData");

                db.AddInParameter(dbCommand, "@id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                #region linq
                ReportBillData result = (from b in ds.Tables[0].AsEnumerable()
                                         select new ReportBillData
                                    {
                                        Id = b.Field<Guid>("Id"),
                                        AccountDate = b.Field<DateTime>("AccountDate"),
                                        CheckUserName = b.Field<string>("CheckUserName"),
                                        CompanyName = b.Field<string>("CompanyName"),
                                        CustomerName = b.Field<string>("CustomerName"),
                                        No = b.Field<string>("No"),
                                        OperationNo = b.Field<string>("OperationNo"),
                                        RefNo = b.Field<string>("RefNo"),
                                        Fees = (from f in ds.Tables[1].AsEnumerable()
                                                select new ChargeList
                                                {
                                                    ID = f.Field<Guid>("ID"),
                                                    ChargingCodeID = f.Field<Guid>("ChargingCodeID"),
                                                    ChargingCode = f.Field<String>("ChargingCode"),
                                                    CurrencyID = f.Field<Guid>("CurrencyID"),
                                                    CurrencyName = f.Field<String>("CurrencyName"),
                                                    Rate = f.Field<Decimal>("Rate"),
                                                    UnitPrice = f.Field<Decimal>("UnitPrice"),
                                                    Quantity = f.Field<Decimal>("Quantity"),
                                                    Amount = f.Field<Decimal>("Amount"),
                                                    IsAgent =f.Field<bool>("IsAgent"),
                                                    Way = (FeeWay)f.Field<Byte>("Way"),
                                                    IsDirty = false,
                                                }).ToList(),
                                    }).SingleOrDefault();

                #endregion


                return result;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region GetBankOutStandingDataTotal
        /// <summary>
        /// GetBankOutStandingDataTotal
        /// </summary>
        /// <param name="fromDate">fromDate</param>
        /// <param name="toDate">toDate</param>
        /// <param name="companyIDs">companyIDs</param>
        /// <param name="billTypes">billTypes</param>
        /// <returns>LedgerData</returns>
        public List<BankOutStandingData> GetBankOutStandingDataTotal(DateTime toDate, Guid[] companyIDs, bool hasBankDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBankOutStandingDataTotal");

                db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@HasNotBankDate", DbType.Boolean, hasBankDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                #region linq
                List<BankOutStandingData> result = (from b in ds.Tables[0].AsEnumerable()
                                              select new BankOutStandingData
                                         {
                                             BankId = b.Field<Guid>("BankId"),
                                             BankName = b.Field<string>("BankName"),
                                             BankAccountId = b.Field<Guid>("BankAccountId"),
                                             BankAccountNo = b.Field<string>("BankAccountNo"),
                                             Deposit = b.Field<decimal>("Deposit"),
                                             CheckPaid = b.Field<decimal>("CheckPaid"),
                                         }).ToList();

                #endregion

                return result;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }


        #endregion

        #region GetBankOutStandingDataTotal
        /// <summary>
        /// GetBankOutStandingDataTotal
        /// </summary>
        /// <param name="toDate">toDate</param>
        /// <param name="companyIDs">companyIDs</param>
        /// <param name="hasBankDate">hasBankDate</param>
        /// <returns>LedgerData</returns>
        public List<BankOutStandingDetailData> GetBankOutStandingDataDetail(Guid BankAccountId, DateTime toDate, Guid[] companyIDs, bool hasBankDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBankOutStandingDataDetail");

                db.AddInParameter(dbCommand, "@BankAccountId", DbType.Guid, BankAccountId);
                db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@companyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@HasNoBankDate", DbType.Boolean, hasBankDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                #region linq
                List<BankOutStandingDetailData> result = (from b in ds.Tables[0].AsEnumerable()
                                                          select new BankOutStandingDetailData
                                                    {
                                                        CheckDate = b.Field<DateTime>("CheckDate"),
                                                        CheckNo = b.Field<string>("CheckNo"),
                                                        CustomerName = b.Field<string>("CustomerName"),
                                                        CheckPaid = b.Field<decimal>("CheckPaid"),
                                                        Deposit = b.Field<decimal>("Deposit"),
                                                        BankDate = b.Field<DateTime?>("BankDate"),
                                                    }).ToList();

                #endregion

                return result;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region 获取日记帐列表
        /// <summary>
        /// 获取日记帐列表
        /// </summary>
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <param name="minAmount">minAmount</param>
        /// <param name="maxAmount">maxAmount</param>
        /// <param name="companyIDs">companyIDs</param>
        /// <returns>JournalList</returns>
        public List<JournalDetailReportData> GetJournalReportData(
            DateTime? fromDate, DateTime? toDate
            , decimal? minAmount, decimal? maxAmount, Guid[] companyIDs)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetJournalReportData");


                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@MinAmount", DbType.Decimal, minAmount);
                db.AddInParameter(dbCommand, "@MaxAmount", DbType.Decimal, maxAmount);
                db.AddInParameter(dbCommand, "@companyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                #region linq
                List<JournalDetailReportData> result = (from c in ds.Tables[0].AsEnumerable()
                                                        select new JournalDetailReportData
                                              {
                                                  ID = c.Field<Guid>("ID"),
                                                  GLID = c.Field<Guid>("ID"),
                                                  CurrencyID = c.Field<Guid>("CurrencyID"),
                                                  CurrencyName = c.Field<string>("CurrencyName"),
                                                  GLDescription = c.Field<string>("GLDescription"),
                                                  JournalPostDate = c.Field<DateTime>("JournalPostDate"),
                                                  JournalID = c.Field<Guid>("JournalID"),
                                                  DRAmount = c.Field<decimal>("DRAmount"),
                                                  CRAmount = c.Field<decimal>("CRAmount"),
                                                  Remark = c.Field<string>("Remark"),
                                              }).ToList();

                #endregion

                return result;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region 获取凭证数据

        /// <summary>
        /// 获取凭证数据
        /// </summary>
        /// <param name="fromDate">fromDate</param>
        /// <param name="toDate">toDate</param>
        /// <param name="companys">companys</param>
        /// <param name="vType">vType</param>
        /// <returns>LedgerData</returns>
        public List<VoucherLedgerData> GetLedgerData(DateTime fromDate, DateTime toDate, string companys, short vType)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("FAM.uspGetVoucherForExport");

                db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.String, companys);
                db.AddInParameter(dbCommand, "@VoucherType", DbType.Int16, vType);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<VoucherLedgerData> results = (from b in ds.Tables[0].AsEnumerable()
                                                   select new VoucherLedgerData
                                                   {
                                                       Id = b.Field<Guid>("Id"),
                                                       GLId = b.Field<Guid>("GLId"),
                                                       GLCode = b.Field<string>("GLCode"),
                                                       GLDescription = b.Field<string>("GLDescription"),
                                                       MakeDate = b.Field<DateTime>("MakeDate"),
                                                       Date = b.Field<DateTime>("Date"),
                                                       CompanyID=b.Field<Guid>("CompanyID"),
                                                       BeginBalance = b.Field<decimal>("BeginBalance"),
                                                       CrAmt = b.Field<decimal>("CrAmt"),
                                                       DrAmt = b.Field<decimal>("DrAmt"),
                                                       Balance = b.Field<decimal>("Balance"),
                                                       OrgAmt = b.Field<decimal>("OrgAmt"),
                                                       Rate = b.Field<decimal>("Rate"),
                                                       Remark = b.Field<string>("Remark"),
                                                       BillType = b.Field<Byte>("BillType"),
                                                       BillId = b.Field<Guid>("BillId"),
                                                       BillNo = b.Field<string>("BillNo"),
                                                       CustomerFinanceCode = b.Field<string>("CustomerFinanceCode"),
                                                       OperationNo = b.Field<string>("OperationNo"),
                                                       CustomerName = b.Field<string>("CustomerName"),
                                                       CustomerShortName = b.Field<string>("CustomerShortName"),
                                                       DepartmentName = b.Field<string>("DepartmentName"),
                                                       EmployeeName = b.Field<string>("EmployeeName"),
                                                       VoucherSeqNo = b.Field<string>("VoucherSeqNo"),
                                                       OrgAmtIsZero = b.Field<bool>("OrgAmtIsZero")
                                                   }).ToList();


                return results;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region Aging

        #region GetAgingReportForTotal
        /// <summary>
        /// GetAgingReportForTotal
        /// </summary>
        /// <param name="endingDate">endingDate</param>
        /// <param name="companyIDs">companyIDs</param>
        /// <param name="billTypes">billTypes</param>
        /// <param name="operationTypes">operationTypes</param>
        /// <param name="customerId">customerId</param>
        /// <param name="SearchType">SearchType</param>
        /// <param name="onlyOverPaid">onlyOverPaid</param>
        /// <returns>AgingReportData</returns>
        public List<AgingReportData> GetAgingReportForTotal(DateTime endingDate
                                                            , Guid[] companyIDs
                                                            , BillType[] billTypes
                                                            , OperationType[] operationTypes
                                                            , Guid? customerId
                                                            , short SearchType
                                                            , bool onlyOverPaid
                                                            ,TermType termType
                                                            , AgingDateState agingDateState
                                                            )
        {
            try
            {
                if (customerId == Guid.Empty) customerId = null;

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("FAM.uspGetAgingReportForTotal");
                //设置超时时限为300
                dbCommand.CommandTimeout = 300;

                db.AddInParameter(dbCommand, "@endingDate", DbType.DateTime, endingDate);
                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@billTypes", DbType.String, billTypes.Join());
                db.AddInParameter(dbCommand, "@operationTypes", DbType.String, operationTypes.Join());
                db.AddInParameter(dbCommand, "@customerId", DbType.Guid, customerId);
                db.AddInParameter(dbCommand, "@SearchType", DbType.Int16, SearchType);
                db.AddInParameter(dbCommand, "@onlyOverPaid", DbType.Boolean, onlyOverPaid);
                db.AddInParameter(dbCommand, "@AgingDateState", DbType.Int16, agingDateState);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.AddInParameter(dbCommand, "@DisplayTermOrBuyCustomer", DbType.Int16,termType);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<AgingReportData> results = (from b in ds.Tables[0].AsEnumerable()
                                                 select new AgingReportData
                                                   {

                                                       Balance = b.Field<decimal>("Balance"),
                                                       Currency = b.Field<string>("Currency"),
                                                       Current = 0m,
                                                       CustomerID = b.Field<Guid>("CustomerID"),
                                                       CustomerName = b.Field<string>("CustomerName"),
                                                       CreditLimit = b.Field<decimal>("CreditLimit"),
                                                       Terms = b.Field<int>("Term"),
                                                       PastDue = b.Field<decimal>("PastDue"),
                                                       Less30 = b.Field<decimal>("Less30"),
                                                       Over30 = b.Field<decimal>("Over30"),
                                                       Over45 = b.Field<decimal>("Over45"),
                                                       Over60 = b.Field<decimal>("Over60"),
                                                       Over90 = b.Field<decimal>("Over90"),
                                                       MainCurBalance = b.Field<decimal>("MainCurBalance"),

                                                   }).ToList();


                return results;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region GetAgingReportForDetail
        /// <summary>
        /// GetAgingReportForDetail
        /// </summary>
        /// <param name="endingDate">endingDate</param>
        /// <param name="companyIDs">companyIDs</param>
        /// <param name="billTypes">billTypes</param>
        /// <param name="operationTypes">operationTypes</param>
        /// <param name="customerId">customerId</param>
        /// <param name="currencyID">currencyID</param>
        /// <param name="SearchType">SearchType</param>
        /// <param name="onlyOverPaid">onlyOverPaid</param>
        /// <returns>AgingReportDetailData</returns>
        public List<AgingReportDetailData> GetAgingReportForDetail(DateTime endingDate
            , Guid[] companyIDs
            , BillType[] billTypes
            , OperationType[] operationTypes
            , Guid? customerId
            , string currency
            , short SearchType
            , bool onlyOverPaid
            ,AgingDateState agingDateState
            , TermType termType
            )
        {
            try
            {
                if (customerId == Guid.Empty) customerId = null;

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("FAM.uspGetAgingReportForDetail");
                //设置超时时限为300
                dbCommand.CommandTimeout = 300;

                db.AddInParameter(dbCommand, "@endingDate", DbType.DateTime, endingDate);
                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@billTypes", DbType.String, billTypes.Join());
                db.AddInParameter(dbCommand, "@operationTypes", DbType.String, operationTypes.Join());
                db.AddInParameter(dbCommand, "@customerId", DbType.Guid, customerId);
                db.AddInParameter(dbCommand, "@SearchType", DbType.Int16, SearchType);
                db.AddInParameter(dbCommand, "@Currency", DbType.String, currency);
                db.AddInParameter(dbCommand, "@onlyOverPaid", DbType.Boolean, onlyOverPaid);
                db.AddInParameter(dbCommand, "@AgingDateState", DbType.Int16, agingDateState);
                db.AddInParameter(dbCommand, "@DisplayTermOrBuyCustomer", DbType.Int16, termType);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<AgingReportDetailData> results = (from b in ds.Tables[0].AsEnumerable()
                                                       select new AgingReportDetailData
                                                 {
                                                     Balance = b.Field<decimal>("Balance"),
                                                     BillId = b.Field<Guid>("BillId"),
                                                     BillNo = b.Field<string>("BillNo"),
                                                     CRAmt = 0m,
                                                     Currency = b.Field<string>("CurrencyName"),
                                                     Current = 0m,
                                                     CustomerID = b.Field<Guid>("CustomerID"),
                                                     CustomerName = b.Field<string>("CustomerName"),
                                                     DestAmount = 0m,
                                                     DRAmt = 0m,
                                                     FianceDate = b.Field<DateTime>("FinanceDate"),
                                                     PastDue = b.Field<decimal>("PastDue"),
                                                     Less30 = b.Field<decimal>("Less30"),
                                                     Over30 = b.Field<decimal>("Over30"),
                                                     Over45 = b.Field<decimal>("Over45"),
                                                     Over60 = b.Field<decimal>("Over60"),
                                                     Over90 = b.Field<decimal>("Over90"),
                                                     MainCurBalance = b.Field<decimal>("MainCurBalance"),
                                                     RefNo = b.Field<string>("RefNo"),
                                                     SalesName = b.Field<string>("SalesName"),
                                                     CustomerServiceName= b.Field<string>("CustomerServiceName"),
                                                 }).ToList();

                return results;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region GetAgingReportForFeeDetail
        /// <summary>
        /// GetAgingReportForFeeDetail
        /// </summary>
        /// <param name="BillId">BillId</param>
        /// <returns>AgingReportFeeData</returns>
        public List<AgingReportFeeData> GetAgingReportForFeeDetail(Guid BillId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetAgingReportForFeeDetail");
                dbCommand.CommandTimeout = 300;

                db.AddInParameter(dbCommand, "@BillId", DbType.Guid, BillId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<AgingReportFeeData> results = (from b in ds.Tables[0].AsEnumerable()
                                            select new AgingReportFeeData
                                                       {
                                                           FeeItemName =b.Field<string>("FeeItemName"),
                                                           CrAmt = b.Field<decimal>("CrAmt"),
                                                           DrAmt = b.Field<decimal>("DrAmt"),
                                                           Amount = b.Field<decimal>("Amount"),
                                                           Currency = b.Field<string>("CurrencyName"),
                                                       }).ToList();

                return results;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region GetAgingReportFormCA
        public List<AgingReportDetailData> GetAgingReportForCA(
            DateTime begingDate,
            DateTime endDate
            , Guid[] companyIDs
            , BillType[] billTypes
            , OperationType[] operationTypes
            , Guid? customerId
            , string currency
            , AgingDateState agingDateState
            , TermType termType
            , bool onlyOverPaid)
        {
            try
            {
                if (customerId == Guid.Empty) customerId = null;

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("FAM.uspGetAgingReportForDetailForCA");
                dbCommand.CommandTimeout = 300;

                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, begingDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, endDate);
                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@billTypes", DbType.String, billTypes.Join());
                db.AddInParameter(dbCommand, "@operationTypes", DbType.String, operationTypes.Join());
                db.AddInParameter(dbCommand, "@customerId", DbType.Guid, customerId);
                db.AddInParameter(dbCommand, "@ToCurrency", DbType.String, currency);
                db.AddInParameter(dbCommand, "@onlyOverPaid", DbType.Boolean, onlyOverPaid);
                db.AddInParameter(dbCommand, "@AgingDateState", DbType.Int16, agingDateState);
                db.AddInParameter(dbCommand, "@DisplayTermOrBuyCustomer", DbType.Int16, termType);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<AgingReportDetailData> results = (from b in ds.Tables[0].AsEnumerable()
                                                       select new AgingReportDetailData
                                                       {
                                                           CustomerID = b.Field<Guid>("CustomerID"),
                                                           CustomerName = b.Field<string>("CustomerName"),
                                                           RefNo = b.Field<string>("RefNo"),
                                                           Currency = b.Field<string>("CurrencyName"),
                                                           DRAmt = b.Field<decimal>("DRAmt"),
                                                           CRAmt = b.Field<decimal>("CRAmt"),
                                                           DestAmount = b.Field<decimal>("DestAmount"),
                                                           Balance = b.Field<decimal>("Balance"),               
                                                           FianceDate = b.Field<DateTime>("FinanceDate"),
                                                          
                                                       }).ToList();


                return results;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #endregion

        #region 收支报表

        #region GetCheckListReportData
        /// <summary>
        /// GetCheckListReportData
        /// </summary>
        /// <param name="checkType"></param>
        /// <param name="dateType">0:CreateDate,1:BankDate;2:CheckDate</param>
        /// <param name="from">from</param>
        /// <param name="to">to</param>
        /// <param name="groupBy">0:BillTo;  1:BankName	2:Date</param>
        /// <param name="companyId">companyId</param>
        /// <returns></returns>
        public List<RepCheckData> GetCheckListReportData(CheckType checkType, short dateType
            , DateTime? from, DateTime? to
            , short groupBy, Guid[] companyId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetCheckListReportData");
                dbCommand.CommandTimeout = 300;

                db.AddInParameter(dbCommand, "@checkType", DbType.Int16, checkType);
                db.AddInParameter(dbCommand, "@dateType", DbType.Int16, dateType);
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, from);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, to);
                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyId.Join());
                db.AddInParameter(dbCommand, "@GroupBy", DbType.Int16, groupBy);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<RepCheckData> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new RepCheckData
                                                       {
                                                           GroupId = b.Field<string>("GroupId"),
                                                           GroupName = b.Field<string>("GroupName"),
                                                           GroupId1 = b.Field<string>("GroupId1"),
                                                           GroupName1 = b.Field<string>("GroupName1"),
                                                           Deposit = b.Field<decimal>("Deposit"),
                                                           CheckPaid = b.Field<decimal>("CheckPaid"),
                                                           Total = b.Field<decimal>("Total"),
                                                       }).ToList();

                return results;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region 获取收支详细
        /// <summary>
        /// 获取收支详细
        /// </summary>
        /// <param name="checkID">checkID</param>
        /// <param name="checkType"></param>
        /// <param name="groupBy">0:BillTo;  1:BankName	2:Date</param>
        /// <returns>RepCheckDetailData</returns>
        public List<RepCheckDetailData> GetCheckDetailReportData(
                                    CheckType checkType 
                                  , short dateType
                                  , DateTime? from
                                  , DateTime? to
                                  , short groupBy
                                  , Guid guoupByID
                                  , Guid[] companyId
                    )
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetCheckDetailReportData");
                //设置超时时限为300
                dbCommand.CommandTimeout = 300;

                db.AddInParameter(dbCommand, "@CheckType", DbType.Int16, checkType);
                db.AddInParameter(dbCommand, "@DateType", DbType.Int16, dateType);
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, from);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, to);
                db.AddInParameter(dbCommand, "@GroupBy", DbType.Int16, groupBy);
                db.AddInParameter(dbCommand, "@GroupByID", DbType.Guid, guoupByID);
                db.AddInParameter(dbCommand, "@CompanyIds", DbType.String, companyId.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<RepCheckDetailData> results = (from b in ds.Tables[0].AsEnumerable()
                                                    select new RepCheckDetailData
                                              {
                                                  CheckPaid = b.Field<decimal>("CheckPaid"),
                                                  Deposit = b.Field<decimal>("Deposit"),
                                                  //GroupId = b.Field<string>("GroupId"),
                                                  //GroupName = b.Field<string>("GroupName"),
                                                  //GroupId1 = b.Field<string>("GroupId1"),
                                                  //GroupName1 = b.Field<string>("GroupName1"),
                                                  BankName = b.Field<string>("BankName"),
                                                  CheckNo = b.Field<string>("CheckNo"),
                                                  Date = b.Field<DateTime>("Date"),
                                              }).ToList();


                return results;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region 获取收支CA

        /// <summary>
        /// 获取收支CA
        /// </summary>
        /// <param name="checkType">checkType</param>
        /// <param name="dateType">0:CreateDate,1:BankDate;2:CheckDate</param>
        /// <param name="from">from</param>
        /// <param name="to">to</param>
        /// <param name="sortBy">0:BillTo;  1:BankName	2:Date</param>
        /// <param name="companyId">companyId</param>
        /// <returns>RepCACheckDepositData</returns>
        public List<RepCACheckDepositData> GetCheckListReportDataCA(CheckType checkType
            , short dateType
            , DateTime? from, DateTime? to
            , short sortBy, Guid[] companyId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetCheckListReportDataCA");
                dbCommand.CommandTimeout = 300;

                db.AddInParameter(dbCommand, "@checkType", DbType.Int16, checkType);
                db.AddInParameter(dbCommand, "@dateType", DbType.Int16, dateType);
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, from);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, to);
                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyId.Join());
                db.AddInParameter(dbCommand, "@SortBy", DbType.Int16, sortBy);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<RepCACheckDepositData> results = (from b in ds.Tables[0].AsEnumerable()
                                                       select new RepCACheckDepositData
                                              {

                                                    Amount = b.Field<decimal>("Amount"),
                                                    Balance = b.Field<decimal>("Balance"),
                                                    BankName = b.Field<string>("BankName"),
                                                    BillCurrency =b.Field<string>("BillCurrency"),
                                                    BillNo = b.Field<string>("BillNo"),
                                                    BillRefNo = b.Field<string>("BillRefNo"),
                                                    BillToId =b.Field<Guid>("BillToId"),
                                                    BillToName = b.Field<string>("BillToName"),
                                                    CheckAmount = b.Field<decimal>("CheckAmount"),
                                                    CheckID =b.Field<Guid>("CheckID"),
                                                    CheckNo =b.Field<string>("CheckNo"),
                                                    CheckWriteOffAmt = b.Field<decimal>("CheckWriteOffAmt"),
                                                    Currency =b.Field<string>("Currency"),
                                                    DateTime =b.Field<DateTime >("DateTime"),
                                                    OperationNo =b.Field<string>("OperationNo"),
                                                    //PaidAmount = i.CurrencyBill.PaidAmt == null ? 0m : i.CurrencyBill.PaidAmt.Value,
                                                    Remark = b.Field<string >("Remark"),
                                              }).ToList();

                return results;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region 获取预收预付列表

        /// <summary>
        /// 获取预收预付列表
        /// </summary>
        /// <param name="from">开始时间</param>
        /// <param name="to">结束时间</param>
        /// <param name="companyIds">公司ID</param>
        /// <param name="CustomerID">客户ID</param>
        /// <param name="GLID">科目ID</param>
        /// <returns></returns>
        public List<PrepaidInAdvanceData> GetPrepaidInAdvanceData(
              DateTime? from, DateTime? to
            , Guid[] companyIds, Guid? CustomerID
            , Guid? GLID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetGLReportForCheck");
                dbCommand.CommandTimeout = 300;

                db.AddInParameter(dbCommand, "@StartDate", DbType.DateTime, from);
                db.AddInParameter(dbCommand, "@EndingDate", DbType.DateTime, to);
                db.AddInParameter(dbCommand, "@CompanyIds", DbType.String, companyIds.Join());
                db.AddInParameter(dbCommand, "@CustomerId", DbType.Guid, CustomerID);
                db.AddInParameter(dbCommand, "@GLID", DbType.Guid, GLID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<PrepaidInAdvanceData> results = (from b in ds.Tables[0].AsEnumerable()
                                                       select new PrepaidInAdvanceData
                                                       {
                                                           ID = b.Field<Guid?>("ID"),
                                                           No = b.Field<string>("NO"),
                                                           CustomerName = b.Field<string>("CustomerName"),
                                                           CustomerID = b.Field<Guid?>("CustomerID"),
                                                           CheckDate = b.Field<DateTime>("CheckDate"),
                                                           GLName = b.Field<string>("GLName"),
                                                           Amount = b.Field<decimal>("Amount"),
                                                       }).ToList();

                return results;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region 获取预收预付余额清单

        /// <summary>
        /// 获取预收预付余额清单
        /// </summary>
        /// <param name="from">开始时间</param>
        /// <param name="to">结束时间</param>
        /// <param name="companyIds">公司ID</param>
        /// <param name="CustomerID">客户ID</param>
        /// <param name="GLID">科目ID</param>
        /// <returns></returns>
        public List<GLCheckBalanceData> GetGLCheckBalanceData(
              DateTime? from, DateTime? to
            , Guid[] companyIds, Guid? CustomerID
            , Guid? GLID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetGLBalanceReportForCheck");
                dbCommand.CommandTimeout = 300;

                //db.AddInParameter(dbCommand, "@StartDate", DbType.DateTime, from);
                db.AddInParameter(dbCommand, "@EndingDate", DbType.DateTime, to);
                db.AddInParameter(dbCommand, "@CompanyIds", DbType.String, companyIds.Join());
                db.AddInParameter(dbCommand, "@CustomerId", DbType.Guid, CustomerID);
                db.AddInParameter(dbCommand, "@GLID", DbType.Guid, GLID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<GLCheckBalanceData> results = (from b in ds.Tables[0].AsEnumerable()
                                                    select new GLCheckBalanceData
                                                      {
                                                          BillNo = b.Field<string>("BillNo"),
                                                          BillType = b.Field<string>("BillType"),
                                                          Date = b.Field<DateTime?>("Date"),
                                                          EShortName = b.Field<string>("EShortName"),
                                                          Amount = b.Field<decimal>("Amount"),
                                                      }).ToList();

                return results;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #endregion

        #region 进口代理对帐 

           /// <summary>
       /// 获取代理对帐单统计表
       /// </summary>
       /// <param name="userCompanyID">用户公司ID</param>
       /// <param name="customerID">客户ID</param>
       /// <param name="agentCompanyIDs">代理公司ID集合</param>
       /// <param name="operactioType">业务类型</param>
       /// <param name="DateType">日期类型(0:BillDate,1:ETD)</param>
       /// <param name="currencyID">折合币种</param>
       /// <param name="fromDate">开始日期</param>
       /// <param name="toDate">结束日期</param>
       /// <param name="orderByName">排序字段</param>
       /// <param name="billType">帐单类型(0:All全部,1:Open未完全付 2:Paid已付)</param>
       /// <param name="isShowPaidStatus">是否显示付款状态</param>
       /// <param name="isAttached">是否显示明细</param>
       /// <returns></returns>
        public AgentStatementReportDateTotal GetAgentStatementReportDate(
                                         Guid userCompanyID,
                                         Guid? customerID,
                                         Guid[] agentCompanyIDs,
                                         string operactioType ,
                                         Int16 dateType,
                                         Guid? currencyID,
                                         DateTime? fromDate,
                                         DateTime? toDate,
                                         AgentStatementSortByEnum orderByName,
                                         Int16 billType, 
                                         bool isShowPaidStatus,
                                         bool isAttached)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("FAM.uspGetAgentStatementTotal");
                dbCommand.CommandTimeout = 300;

                string stragentCompanyIDList = agentCompanyIDs.Join();

                db.AddInParameter(dbCommand, "@UserCompanyID", DbType.Guid, userCompanyID);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@AgentCompanyIDs", DbType.String, stragentCompanyIDList);
                db.AddInParameter(dbCommand, "@OperactioType", DbType.String, operactioType);
                db.AddInParameter(dbCommand, "@DateType", DbType.Int16, dateType);
                db.AddInParameter(dbCommand, "@CurrencyID", DbType.Guid, currencyID);
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@OrderByName", DbType.Int16, orderByName);
                db.AddInParameter(dbCommand, "@BillType", DbType.Int16, billType);
                db.AddInParameter(dbCommand, "@IsShowPaidStatus", DbType.Boolean, isShowPaidStatus);
                db.AddInParameter(dbCommand, "@IsShowDetail", DbType.Boolean, isAttached);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                #region 公司信息
                CompanyInfo companyInfo = (from b in ds.Tables[0].AsEnumerable()
                                       select new CompanyInfo
                                       {
                                           CompanyAddress = b.Field<String>("UserCompanyAddress"),
                                           CompanyName = b.Field<String>("UserCompanyName"),
                                           CompanyTel = b.Field<String>("UserCompanyTel"),
                                           CompanyFax = b.Field<String>("UserCompanyFax")
                                       }).SingleOrDefault();
                #endregion
                List<AgentStatementReportDate> MasterList = new List<AgentStatementReportDate>();
                List<AgentStatementReportMasterAndDetailData> MasterDetailDataList = new List<AgentStatementReportMasterAndDetailData>();

                
                #region 主数据列表
                    MasterList = (from b in ds.Tables[1].AsEnumerable()
                                  select new AgentStatementReportDate
                                  {
                                      BillToID= b.Field<Guid>("BillToId"),
                                      BillToName = b.Field<String>("BillToName"),
                                      BillToAddress = b.Field<String>("BillToAddress"),
                                      BillToTelFax = b.Field<String>("BillToTelFax"),
                                      BillNo = b.Field<String>("BillNo"),
                                      OperationType = (OperationType)b.Field<Byte>("OperationType"),
                                      Date = b.Field<DateTime>("Date"),
                                      OurRefNo = b.Field<String>("OurRefNo"),
                                      YourRefNo = b.Field<String>("YourRefNo"),
                                      HBLNo = b.Field<String>("HBLNo"),
                                      DRAmount = b.Field<Decimal>("DRAmount"),
                                      CRAmount = b.Field<Decimal>("CRAmount"),
                                      PaidAmount = b.Field<Decimal>("PaidAmount"),
                                      Balance = b.Field<Decimal>("BalanceAmount"),
                                      IsPaid = b.Field<Boolean>("IsPaid"),
                                      BillID = b.Field<Guid>("BillId"),
                                      CompanyID = b.Field<Guid>("CompanyId"),
                                      CompanyEName = b.Field<String>("CompanyEName"),
                                  }).ToList();
                #endregion

               if (isAttached)
                {   
                    #region 主表&明细列表数据
                    MasterDetailDataList = (from b in ds.Tables[2].AsEnumerable()
                                            select new AgentStatementReportMasterAndDetailData
                                                                    {
                                                                        BillToName = b.Field<String>("BillToName"),
                                                                        BillToAddress = b.Field<String>("BillToAddress"),
                                                                        BillToTelFax = b.Field<String>("BillToTelFax"),
                                                                        BillNo = b.Field<String>("BillNo"),
                                                                        OperationType = (OperationType)b.Field<Byte>("OperationType"),
                                                                        Date = b.Field<DateTime>("Date"),
                                                                        OurRefNo = b.Field<String>("OurRefNo"),
                                                                        YourRefNo = b.Field<String>("YourRefNo"),
                                                                        HBLNo = b.Field<String>("HBLNo"),
                                                                        DRAmount = b.Field<Decimal>("DRAmount"),
                                                                        CRAmount = b.Field<Decimal>("CRAmount"),
                                                                        IsPaid = b.Field<Boolean>("IsPaid"),
                                                                        CompanyID = b.Field<Guid>("CompanyId"),
                                                                        CompanyEName = b.Field<String>("CompanyEName"),
                                                                        BillID = b.Field<Guid>("BillID"),
                                                                        BillDate = b.Field<DateTime>("BillDate"),
                                                                        AgentRefNo = b.Field<String>("AgentRefNo"),
                                                                        MBLNo = b.Field<String>("MBLNo"),
                                                                        Commodity = b.Field<String>("Commodity"),
                                                                        POLAndETD = b.Field<String>("POLAndETD"),
                                                                        PODAndETA = b.Field<String>("PODANDETA"),
                                                                        FDeatAndFETA = b.Field<String>("FDeatAndFETA"),
                                                                        KGSAndLBS = b.Field<String>("KGsAndLBS"),
                                                                        CBMAndCFT = b.Field<String>("CMBAndCFT"),
                                                                        VesselName = b.Field<String>("VesselName"),
                                                                        PKGS = b.Field<String>("PKGS"),
                                                                        FeeItemName = b.Field<String>("FeeItemName"),
                                                                        Payment = b.Field<String>("PayMent"),
                                                                        Revenue = b.Field<Decimal>("Revenue"),
                                                                        Cost = b.Field<Decimal>("Cost"),
                                                                        DueDate = b.Field<DateTime>("DueDate"),
                                                                        BillSource = b.Field<String>("BillSource"),
                                                                        Terms = b.Field<Int32>("Terms"),
                                                                        Currency = b.Field<String>("Currency"),
                                                                    }).ToList();
                     #endregion
                }
               

                AgentStatementReportDateTotal results = new AgentStatementReportDateTotal();
                results.MasterDataList = MasterList;
                results.CompanyInfo = companyInfo;
                results.MasterAndDetailDataList = MasterDetailDataList;

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
        /// 获取代理对帐单详细
        /// </summary>
        /// <param name="billNo">billNo</param>
        /// <returns>AgentStatementReportDate</returns>
        public List<AgentStatementReportDetailDate> GetAgentStatementReportDetailDate(Guid billID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetAgentStatementDetail");
                dbCommand.CommandTimeout = 300;

                db.AddInParameter(dbCommand, "@BillID", DbType.Guid, billID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<AgentStatementReportDetailDate> results = (from b in ds.Tables[0].AsEnumerable()
                                                                select new AgentStatementReportDetailDate
                                                                {
                                                                    BillID = b.Field<Guid>("BillID"),
                                                                    BillToID = b.Field<Guid>("BillToID"),
                                                                    BillToName = b.Field<String>("BillToName"),
                                                                    BillToAddress = b.Field<String>("BillToAddress"),
                                                                    BillToTelFax = b.Field<String>("BillToTelFax"),
                                                                    BillNo = b.Field<String>("BillNo"),
                                                                    Date = b.Field<DateTime>("Date"),
                                                                    BillDate=b.Field<DateTime>("BillDate"),
                                                                    OurRefNo = b.Field<String>("OurRefNo"),
                                                                    AgentRefNo = b.Field<String>("AgentRefNo"),
                                                                    MBLNo = b.Field<String>("MBLNo"),
                                                                    HBLNo = b.Field<String>("HBLNo"),
                                                                    Commodity = b.Field<String>("Commodity"),
                                                                    POLAndETD = b.Field<String>("POLAndETD"),
                                                                    PODAndETA = b.Field<String>("PODANDETA"),
                                                                    FDeatAndFETA = b.Field<String>("FDeatAndFETA"),
                                                                    KGSAndLBS = b.Field<String>("KGsAndLBS"),
                                                                    CBMAndCFT = b.Field<String>("CMBAndCFT"),
                                                                    VesselName = b.Field<String>("VesselName"),
                                                                    PKGS = b.Field<String>("PKGS"),
                                                                    FeeItemName = b.Field<String>("FeeItemName"),
                                                                    DRAmount = b.Field<Decimal>("DRAmount"),
                                                                    CRAmount = b.Field<Decimal>("CRAmount"),
                                                                    Payment = b.Field<String>("PayMent"),
                                                                    Revenue = b.Field<Decimal>("Revenue"),
                                                                    Cost = b.Field<Decimal>("Cost"),
                                                                    DueDate = b.Field<DateTime>("DueDate"),
                                                                    BillSource = b.Field<String>("BillSource"),
                                                                    Terms = b.Field<Int32>("Terms"),
                                                                    Currency = b.Field<String>("Currency"),
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

        #region

        /// <summary>
        /// GetLocalStatementReportData
        /// </summary>
        /// <param name="billToId">billToId</param>
        /// <param name="createFromDate">createFromDate</param>
        /// <param name="createToDate">createToDate</param>
        /// <param name="orderBy">orderBy</param>
        /// <param name="billType">billType</param>
        /// <param name="billState">billState</param>
        /// <param name="userCompanyId">userCompanyId</param>
        /// <param name="ETAFrom">ETAFrom</param>
        /// <param name="ETATo">ETATo</param>
        /// <param name="ETDFrom">ETDFrom</param>
        /// <param name="ETDTo">ETDTo</param>
        /// <returns></returns>
        public List<LocalStatementReportData> GetLocalStatementReportData(
            Guid? billToId
            , DateTime? createFromDate, DateTime? createToDate
            , LocalStatementOrderByEnum orderBy
            , CheckType billType, StatementBillStateEnum billState
            , Guid[] companyIds
            , DateTime? ETAFrom, DateTime? ETATo
            , DateTime? ETDFrom, DateTime? ETDTo)
        {
            try
            {
                
                

                Database db = DatabaseFactory.CreateDatabase();
                //SPR_FIN_GetAgentStatementByTotal
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetLocalStatementReportDate");
                //设置超时时限为300
                dbCommand.CommandTimeout = 300;

                db.AddInParameter(dbCommand, "@BillToId", DbType.Guid, billToId);
                db.AddInParameter(dbCommand, "@CreateFromDate", DbType.DateTime, createFromDate);
                db.AddInParameter(dbCommand, "@CreateToDate", DbType.DateTime, createToDate);
                db.AddInParameter(dbCommand, "@BillType", DbType.Int16, billType);
                db.AddInParameter(dbCommand, "@BillState", DbType.Int16, billState);
                db.AddInParameter(dbCommand, "@OrderBy", DbType.Int16, orderBy);
                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIds.Join());
                db.AddInParameter(dbCommand, "@ETAFromDate", DbType.DateTime, ETAFrom);
                db.AddInParameter(dbCommand, "@ETAToDate", DbType.DateTime, ETATo);
                db.AddInParameter(dbCommand, "@ETDFromDate", DbType.DateTime, ETDFrom);
                db.AddInParameter(dbCommand, "@ETDToDate", DbType.DateTime, ETDTo);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }
                List<LocalStatementReportData> results = (from b in ds.Tables[0].AsEnumerable()
                                                          select new LocalStatementReportData
                                                       {
                                                           BillToId = b.Field<Guid>("BillToId"),
                                                           BillToName = b.Field<string>("BillToName"),
                                                           BillToAddress = b.Field<string>("BillToAddress"),
                                                           BillToTelFax = b.Field<string>("BillToTelFax"),
                                                           InvoiceDate = b.Field<DateTime>("InvoiceDate"),
                                                           InvoiceNo = b.Field<string>("InvoiceNo"),
                                                           RefNo = b.Field<string>("RefNo"),
                                                           HBLNo = b.Field<string>("HBLNo"),
                                                           CustomerRefNo = b.Field<string>("CustomerRefNo"),
                                                           Currency = b.Field<string>("Currency"),
                                                           Amount = b.Field<decimal>("Amount"),
                                                           PaidAmount = b.Field<decimal>("PaidAmount"),
                                                           PayAmount = b.Field<decimal>("PaidAmount"),
                                                           Balance = b.Field<decimal>("Balance"),
                                                           CheckNo = b.Field<string>("CheckNo"),
                                                           BillToCode = b.Field<string>("BillToCode"),
                                                           DueDate = b.Field<DateTime>("DuDate"),
                                                           ContainerNO = b.Field<string>("ContainerNO"),
                                                           BillID = b.Field<Guid>("BillID"),
                                                           ETD = b.Field<DateTime>("ETD"),
                                                           ETA = b.Field<DateTime>("ETA"),
                                                       }).ToList();

                return results;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// GetLocalStatementReportData
        /// </summary>
        /// <param name="billToId">billToId</param>
        /// <param name="createFromDate">createFromDate</param>
        /// <param name="createToDate">createToDate</param>
        /// <param name="orderBy">orderBy</param>
        /// <param name="billType">billType</param>
        /// <param name="billState">billState</param>
        /// <param name="userCompanyId">userCompanyId</param>
        /// <param name="ETAFrom">ETAFrom</param>
        /// <param name="ETATo">ETATo</param>
        /// <param name="ETDFrom">ETDFrom</param>
        /// <param name="ETDTo">ETDTo</param>
        /// <returns></returns>
        public List<LocalStatementReportDetailData> GetLocalStatementReportDetailData(
            Guid? billToId
            , DateTime? createFromDate, DateTime? createToDate
            , LocalStatementOrderByEnum orderBy
            , CheckType billType, StatementBillStateEnum billState
            , Guid[] companyIds
            , DateTime? ETAFrom, DateTime? ETATo
            , DateTime? ETDFrom, DateTime? ETDTo)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                //SPR_FIN_GetAgentStatementByTotal
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetLocalStatementReportDetailData");
                //设置超时时限为300
                dbCommand.CommandTimeout = 300;

                db.AddInParameter(dbCommand, "@BillToId", DbType.Guid, billToId);
                db.AddInParameter(dbCommand, "@CreateFromDate", DbType.DateTime, createFromDate);
                db.AddInParameter(dbCommand, "@CreateToDate", DbType.DateTime, createToDate);
                db.AddInParameter(dbCommand, "@BillType", DbType.Int16, billType);
                db.AddInParameter(dbCommand, "@BillState", DbType.Int16, billState);
                db.AddInParameter(dbCommand, "@OrderBy", DbType.Int16, orderBy);
                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIds.Join());
                db.AddInParameter(dbCommand, "@ETAFromDate", DbType.DateTime, ETAFrom);
                db.AddInParameter(dbCommand, "@ETAToDate", DbType.DateTime, ETATo);
                db.AddInParameter(dbCommand, "@ETDFromDate", DbType.DateTime, ETDFrom);
                db.AddInParameter(dbCommand, "@ETDToDate", DbType.DateTime, ETDTo);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }
                List<LocalStatementReportDetailData> results = (from b in ds.Tables[0].AsEnumerable()
                                                                select new LocalStatementReportDetailData
                                                          {
                                                              BillToId = b.Field<Guid>("BillToId"),
                                                              BillToName = b.Field<string>("BillToName"),
                                                              BillToAddress = b.Field<string>("BillToAddress"),
                                                              BillToTelFax = b.Field<string>("BillToTelFax"),
                                                              BillToCode = b.Field<string>("BillToCode"),
                                                              BillToAttn = b.Field<string>("BillToAttn"),
                                                              MBLNo= b.Field<string>("MBLNo"),
                                                              HBLNo = b.Field<string>("HBLNo"),
                                                              InvoiceNo = b.Field<string>("InvoiceNo"),
                                                              InvoiceDate = b.Field<DateTime>("InvoiceDate"),
                                                              RefNo = b.Field<string>("RefNo"),
                                                              CustomerRefNo = b.Field<string>("CustomerRefNo"),
                                                              Shipper = b.Field<string>("Shipper"),
                                                              Consignee = b.Field<string>("Consignee"),
                                                              Notify = b.Field<string>("Notify"),
                                                              Carrier = b.Field<string>("Carrier"),
                                                              VesselNo = b.Field<string>("VesselNo"),
                                                              POLAndETD = b.Field<string>("POLAndETD"),
                                                              PODAndETA = b.Field<string>("PODAndETA"),
                                                              FDestAndETA = b.Field<string>("FDestAndETA"),
                                                              Commodity = b.Field<string>("Commodity"),
                                                              PKGS = b.Field<string>("PKGS"),
                                                              KGSAndLBS = b.Field<string>("KGSAndLBS"),
                                                              CBMAndCFT = b.Field<string>("CBMAndCFT"),
                                                              CarrierBook = b.Field<string>("CarrierBook"),
                                                              FeeItemName = b.Field<string>("FeeItemName"),
                                                              Rate = b.Field<decimal>("Rate"),
                                                              Qty = b.Field<decimal>("Qty"),
                                                              Price = b.Field<decimal>("UnitPrice"),
                                                              Amount = b.Field<decimal>("Amount"),
                                                              DueDate = b.Field<DateTime>("DueDate"),
                                                              Terms = b.Field<int>("Terms").ToString(),
                                                              BillId = b.Field<Guid>("BillId"),
                                                              TotalAmount = b.Field<string>("TotalAmount"),
                                                              PaidAmount = b.Field<string>("PaidAmount"),
                                                              FinAmount = b.Field<string>("FinAmount"),
                                                              Currency = b.Field<string>("Currency"),
                                                              Remark = b.Field<string>("Remark"),
                                                              PublicMemo = b.Field<string>("PublicMemo"),
                                                              ShipToName = b.Field<string>("ShipToName"),
                                                              ShipToAddress = b.Field<string>("ShipToAddress"),
                                                              ShipToTelFax = b.Field<string>("ShipToTelFax"),
                                                              ContainerNo = b.Field<string>("ContainerNo"),
                                                              CompanyName = b.Field<string>("CompanyName"),
                                                              CompanyAddress = b.Field<string>("CompanyAddress"),
                                                              CompanyTel = b.Field<string>("CompanyTel"),
                                                              CompanyFax = b.Field<string>("CompanyFax"),
                                                          }).ToList();

                return results;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        #endregion

    }
}
