using ICP.Common.ServiceInterface.DataObjects;
using ICP.FAM.ServiceComponent.Common;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.CompositeObjects;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.DataObjects.SaveRequests;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;

namespace ICP.FAM.ServiceComponent
{
    public partial class FinanceService
    {
        #region 获得指定公司下所有的银行账号信息
        /// <summary>
        /// 获取某个公司下的所有银行号信息
        /// 返回两列，一列ID是AccountID，一列CurrencyName“银行-币种”
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        public List<BankAccountList> GetCompanyBankAccounts(Guid companyId, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetCompanyBankAccounts");

                db.AddInParameter(dbCommand, "@CompanyId", DbType.Guid, companyId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<BankAccountList>();
                }

                List<BankAccountList> results = (from b in ds.Tables[0].AsEnumerable()
                                                 select new BankAccountList
                                          {
                                              ID = b.Column<Guid>("ID"),
                                              BankID = b.Column<Guid>("BankID"),
                                              BankName =b.Column<string>("BankName"),
                                              CurrencyID = b.Column<Guid>("CurrencyID"),
                                              CurrencyName = b.Column<string>("CurrencyName").Replace("\r\n", "-"),
                                              CurrencyEName = b.Column<string>("CurrencyEName"),
                                              IsInvoiceAccount=b.Column<bool>("IsInvoiceAccount"),
                                              AccountNo = b.Column<string>("AccountNo"),
                                              IsSupportDirectBank = b.Column<bool>("IsSupportDirectBank"),
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

        #region 通过银行账号获取账号明细及其公司信息
        /// <summary>
        /// 通过银行账号获取账号明细及其公司信息
        /// </summary>
        /// <param name="accountNO">公司ID</param>
        /// <returns></returns>
        public BankAccountList GetBankAccountByNO(string accountNO)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBankAccountByNO");

                db.AddInParameter(dbCommand, "@AccountNO", DbType.String, accountNO);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1||ds.Tables[0].Rows.Count!=1)
                {
                    return new BankAccountList();
                }

                BankAccountList results = (from b in ds.Tables[0].AsEnumerable()
                                                 select new BankAccountList
                                                 {
                                                     ID = b.Column<Guid>("ID"),
                                                     CurrencyID = b.Column<Guid>("CurrencyID"),
                                                     CurrencyName = b.Column<string>("CurrencyName").Replace("\r\n", "-"),
                                                     CurrencyEName = b.Column<String>("CurrencyEName"),
                                                     CompanyID = b.Column<Guid>("CompanyID"),
                                                     IsSupportDirectBank = b.Column<bool>("IsSupportDirectBank"),
                                                 }).SingleOrDefault();
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

        #region 获取银行账号列表，用户常用的排最前面
        /// <summary>
        /// 获取银行账号列表，用户常用的排最前面
        /// 返回两列，一列ID是AccountID，一列CurrencyName“银行-币种”
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        public List<BankAccountList> GetBankAccountsOrderByRecentUsedFirst(Guid companyId, Guid userId, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBankAccountsOrderByRecentUsedFirst");

                db.AddInParameter(dbCommand, "@CompanyId", DbType.Guid, companyId);
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<BankAccountList>();
                }

                List<BankAccountList> results = (from b in ds.Tables[0].AsEnumerable()
                                                 select new BankAccountList
                                                 {
                                                     ID = b.Column<Guid>("ID"),
                                                     CurrencyName = b.Column<string>("CurrencyName").Replace("\r\n", "-"),
                                                     AccountCurrencyName = b.Column<string>("AccountCurrencyName"),
                                                     AccountNo = b.Column<string>("AccountNo"),
                                                     BankName=b.Column<string>("BankName")
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

        #region 保存银行与公司的关联
        /// <summary>
        /// 保存银行与公司的关联
        /// </summary>
        /// <param name="bankID">银行ID</param>
        /// <param name="companyIds">公司ID集合</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        public SingleResult SaveBankAndCompany(Guid bankID, Guid[] companyIds, bool isEnglish,Guid saveByID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveBankAndCompany");

                string companyIdList = companyIds.Join();

                db.AddInParameter(dbCommand, "@BankID", DbType.Guid, bankID);
                db.AddInParameter(dbCommand, "@CompanyIds", DbType.String, companyIdList);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                SingleResult result = db.SingleResult(dbCommand,new string[]{"ID","UpdateDate"});

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

        #region 获得银行关联的公司ID集合
        /// <summary>
        /// 获得银行关联的公司ID集合
        /// </summary>
        /// <param name="bankID"></param>
        /// <returns></returns>
        public List<Guid> GetBankAndCompany(Guid bankID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBankAndCompany");

                db.AddInParameter(dbCommand, "@BankID", DbType.Guid, bankID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, false);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<Guid> list = (from b in ds.Tables[0].AsEnumerable() select b.Field<Guid>("CompanyID")).ToList();

                return list;
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

        #region 获得银行列表
        /// <summary>
        /// 获得银行列表
        /// </summary>
        /// <param name="companyIds">公司ID集合</param>
        /// <param name="simpleName">银行简称</param>
        /// <param name="cnName">银行中文名称</param>
        /// <param name="enName">英文名称</param>
        /// <param name="isValid">有效性</param>
        /// <param name="dataPageInfo">dataPageInfo</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        public PageList GetBankList(
            Guid[] companyIds, 
            string simpleName, 
            string cnName, 
            string enName, 
            bool? isValid,
            DataPageInfo dataPageInfo,
            bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBankList");

                db.AddInParameter(dbCommand, "@CompanyIds", DbType.String, companyIds.Join());
                db.AddInParameter(dbCommand, "@SimpleName", DbType.String, simpleName);
                db.AddInParameter(dbCommand, "@CnName", DbType.String, cnName);
                db.AddInParameter(dbCommand, "@EnName", DbType.String, enName);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, dataPageInfo.PageSize);
                db.AddInParameter(dbCommand, "@CurrentPage ", DbType.Int32, dataPageInfo.CurrentPage);
                db.AddInParameter(dbCommand, "@OrderByName", DbType.String, dataPageInfo.SortByName + " "+dataPageInfo.SortOrderType.ToString());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<BankList> results = (from b in ds.Tables[0].AsEnumerable()
                                          select new BankList
                                          {
                                              CompanyName = b.Column<string>("CompanyName"),
                                              CName = b.Column<string>("CName"),
                                              EName = b.Column<string>("EName"),
                                              CShortName = b.Column<string>("CShortName"),
                                              EShortName = b.Column<string>("EShortName"),
                                              ID = b.Column<Guid>("ID"),
                                              CreateByName = b.Column<string>("CreateByName"),
                                              CreateByID = b.Column<Guid>("CreateByID"),
                                              CreateDate = b.Column<DateTime>("CreateDate"),
                                              IsValid = b.Column<bool>("IsValid"),
                                              UpdateDate = b.Column<DateTime?>("UpdateDate"),
                                              Fax = b.Column<String>("Fax"),
                                              Tel1 = b.Column<String>("Tel1"),
                                              Contact = b.Column<String>("Contact")
                                          }).ToList();

                dataPageInfo.TotalCount = ((from c in ds.Tables[1].AsEnumerable() select c.Column<Int32>("TotalCount")).SingleOrDefault());

                PageList list = PageList.Create<BankList>(results, dataPageInfo);

                return list;
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

        #region 获得银行账号列表
        /// <summary>
        /// 获得银行账号列表
        /// </summary>
        /// <param name="bankID">银行ID</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        public  List<BankAccountList> GetBankAccountList(object bankID, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBankAccountList");

                db.AddInParameter(dbCommand, "@BankID", DbType.Guid, bankID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }


                List<BankAccountList> list = (from d in ds.Tables[0].AsEnumerable()
                                              select new BankAccountList { 
                                                    ID=d.Field<Guid>("ID"),
                                                    BankID = d.Field<Guid>("BankID"),
                                                    AccountNo = d.Field<String>("AccountNo"),
                                                    CurrencyName = d.Field<String>("Currency"),
                                                    GLCName = d.Field<String>("GLCName"),
                                                    GLEName = d.Field<String>("GLEName"),
                                                    GLFullName = d.Field<String>("GLFullName"),
                                                    IsValid = d.Field<Boolean>("IsValid"),
                                                    CreateByName = d.Field<String>("CreateByName"),
                                                    CreateDate = d.Field<DateTime>("CreateDate"),
                                                    Remark = d.Field<String>("Remark"),
                                                    GLName = isEnglish ? d.Field<String>("GLEName") : d.Field<String>("GLCName"),
                                                    UpdateDate = d.Field<DateTime?>("UpdateDate")
                                              }).ToList();

                return list;
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

        #region 获得银行详细信息
        /// <summary>
        /// 获得银行详细信息
        /// </summary>
        /// <param name="bankId">银行ID</param>
        /// <param name="isEnglish">是事英文版本</param>
        /// <returns></returns>
        public BankInfo GetBankInfo(Guid bankId, bool isEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(bankId, "requestId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBankInfo");

                db.AddInParameter(dbCommand, "@BankId", DbType.Guid, bankId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                if (ds.Tables[0].Rows.Count > 1)
                {
                    throw new Exception("Found more than 1 booking orders. It's impossible!");
                }

                BankInfo result = (from b in ds.Tables[0].AsEnumerable()
                                   select new BankInfo
                                         {
                                             CName = b.Column<string>("CName"),
                                             EName = b.Column<string>("EName"),
                                             CShortName = b.Column<string>("CShortName"),
                                             EShortName = b.Column<string>("EShortName"),
                                             ID = b.Column<Guid>("ID"),
                                             CreateByID = b.Column<Guid>("CreateBy"),
                                             CreateByName = b.Column<string>("CreateByName"),
                                             CustomerId = b.Column<Guid?>("CustomerId"),
                                             CustomerName = b.Column<string>("CustomerName"),
                                             CompanyId = b.Column<Guid>("CompanyId"),
                                             CompanyName = b.Column<string>("CompanyName"),
                                             PostCode = b.Column<string>("PostCode"),
                                             CAddress = b.Column<string>("CAddress"),
                                             EAddress = b.Column<string>("EAddress"),
                                             Contact = b.Column<string>("Contact"),
                                             Fax = b.Column<string>("Fax"),
                                             Remark = b.Column<string>("Remark"),
                                             UpdateDate = b.Column<DateTime?>("UpdateDate"),
                                             CreateDate = b.Column<DateTime>("CreateDate"),
                                             IsValid = b.Column<bool>("IsValid"),
                                             Tel1 = b.Column<string>("Tel"),
                                             BankAccountInfoList = (from h in ds.Tables[1].AsEnumerable()
                                                                    select new BankAccountInfo
                                                                    {
                                                                        ID = h.Column<Guid>("ID"),
                                                                        CreateByID = h.Column<Guid>("CreateBy"),
                                                                        UpdateDate = h.Column<DateTime?>("UpdateDate"),
                                                                        CreateByName = h.Column<string>("CreateByName"),
                                                                        BankID = h.Column<Guid>("BankID"),
                                                                        Remark = h.Column<string>("Remark"),
                                                                        IsValid = h.Column<bool>("IsValid"),
                                                                        IsShowInInvoiceBankList = h.Column<bool>("IsShowInInvoiceBankList"),
                                                                        IsSupportDirectBank = h.Column<bool>("IsSupportDirectBank"),
                                                                        IsOpen = h.Column<bool>("IsOpen"),
                                                                        GLID = h.Column<Guid>("GLID"),
                                                                        CurrencyID = h.Column<Guid>("CurrencyID"),
                                                                        GLCName = h.Column<string>("GLCName"),
                                                                        GLEName = h.Column<string>("GLEName"),
                                                                        GLFullName=h.Column<String>("GLFullName"),
                                                                        AccountNo = h.Column<string>("AccountNo"),
                                                                        CurrencyName = h.Column<string>("Currency"),
                                                                        CreateDate=h.Column<DateTime>("CreateDate")
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
        #endregion

        #region 保存银行账号信息
        /// <summary>
        /// 保存银行账号信息
        /// </summary>
        /// <param name="saveRequest">保存实休</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        public SingleResult SaveBankAccountInfo(BankAccountSaveRequest saveRequest, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveBankAccountInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, saveRequest.Id);
                db.AddInParameter(dbCommand, "@AccountNo", DbType.String, saveRequest.AcccountNo);
                db.AddInParameter(dbCommand, "@BankId", DbType.Guid, saveRequest.BankId);
                db.AddInParameter(dbCommand, "@CurrencyId", DbType.Guid, saveRequest.CurrencyId);
                db.AddInParameter(dbCommand, "@GlId", DbType.Guid, saveRequest.GlId);

                db.AddInParameter(dbCommand, "@IsValid", DbType.String, saveRequest.IsValid);
                db.AddInParameter(dbCommand, "@IsShowInInvoiceBankList", DbType.String, saveRequest.IsShowInInvoiceBankList);
                db.AddInParameter(dbCommand, "@IsOpen", DbType.Boolean, saveRequest.IsOpen);
                db.AddInParameter(dbCommand, "@IsSupportDirectBank", DbType.Boolean, saveRequest.IsSupportDirectBank);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, saveRequest.Remark);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                db.AddInParameter(dbCommand, "@SaveById", DbType.Guid, saveRequest.SaveById);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, saveRequest.UpdateDate);

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

        #region 保存银行信息
        /// <summary>
        /// 保存银行信息
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <param name="accounts"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        public SingleResult SaveBankInfo(BankSaveRequest saveRequest, BankAccountSaveRequest[] accounts, bool isEnglish)
        {
            try
            {
                TransactionOptions option = new TransactionOptions();
                option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    Database db = DatabaseFactory.CreateDatabase();
                    DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveBankInfo");

                    db.AddInParameter(dbCommand, "@Id", DbType.Guid, saveRequest.Id);
                    db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, saveRequest.CustomerId);
                    db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, saveRequest.CompanyId);
                    db.AddInParameter(dbCommand, "@SaveById", DbType.Guid, saveRequest.CreateById);
                    db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, saveRequest.UpdateDate);
                    db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                    db.AddInParameter(dbCommand, "@CShortName", DbType.String, saveRequest.CShortName);
                    db.AddInParameter(dbCommand, "@EShortName", DbType.String, saveRequest.EShortName);
                    db.AddInParameter(dbCommand, "@CName", DbType.String, saveRequest.CName);
                    db.AddInParameter(dbCommand, "@EName", DbType.String, saveRequest.EName);
                    db.AddInParameter(dbCommand, "@CAddress", DbType.String, saveRequest.CAddress);
                    db.AddInParameter(dbCommand, "@EAddress", DbType.String, saveRequest.EAddress);
                    db.AddInParameter(dbCommand, "@PostCode", DbType.String, saveRequest.PostalCode);
                    db.AddInParameter(dbCommand, "@Tel1", DbType.String, saveRequest.Tel);
                    db.AddInParameter(dbCommand, "@Tel2", DbType.String, string.Empty);
                    db.AddInParameter(dbCommand, "@Fax", DbType.String, saveRequest.Fax);
                    db.AddInParameter(dbCommand, "@HomePage", DbType.String, string.Empty);
                    db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, saveRequest.IsValid);
                    db.AddInParameter(dbCommand, "@Email", DbType.String, string.Empty);
                    db.AddInParameter(dbCommand, "@Contact", DbType.String, saveRequest.Contact);
                    db.AddInParameter(dbCommand, "@Remark", DbType.String, saveRequest.Remark);

                    SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate" });

                    Guid id = result.GetValue<Guid>("ID");

                    foreach (BankAccountSaveRequest accountRequest in accounts)
                    {
                        accountRequest.BankId = id;
                        SaveBankAccountInfo(accountRequest, isEnglish);
                    }

                    scope.Complete();

                    return result;
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

        #region 更新银行的有效性
        /// <summary>
        /// 更改银行的有效性
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isCancel">是否为作废(True为作废，False为激活)</param>
        /// <param name="saveById">更改人</param>
        /// <param name="updateDate">最后更新时间</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        public SingleResult ChangeBankValidity(Guid id, bool isCancel, Guid saveById, DateTime? updateDate, bool isEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "Id");
            ArgumentHelper.AssertGuidNotEmpty(saveById, "saveById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.ChangeBankValidity");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@isCancel", DbType.Boolean, isCancel);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, saveById);
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

        #region 更新账号的有效性
        /// <summary>
        /// 更新账号的有效性
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isCancel">是否为作废(True为作废，False为激活)</param>
        /// <param name="saveById">更改人</param>
        /// <param name="updateDate">最后更新时间</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        public SingleResult ChangeBankAccountValidity(Guid id, bool isCancel, Guid saveById, DateTime? updateDate, bool isEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "Id");
            ArgumentHelper.AssertGuidNotEmpty(saveById, "saveById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.ChangeBankAccountValidity");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@isCancel", DbType.Boolean, isCancel);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, saveById);
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

        #region 获得账号的详细信息
        /// <summary>
        /// 获得账号详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        public BankAccountInfo GetBankAccountInfo(Guid id, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBankAccountInfo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid,id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }


                BankAccountInfo info = (from d in ds.Tables[0].AsEnumerable()
                                        select new BankAccountInfo
                                              {
                                                  ID = d.Field<Guid>("ID"),
                                                  BankID = d.Field<Guid>("BankID"),
                                                  AccountNo = d.Field<String>("AccountNo"),
                                                  CurrencyID=d.Field<Guid>("CurrencyID"),
                                                  CurrencyName = d.Field<String>("Currency"),
                                                  GLCName = d.Field<String>("GLCName"),
                                                  GLEName = d.Field<String>("GLEName"),
                                                  IsValid = d.Field<Boolean>("IsValid"),
                                                  Remark = d.Field<String>("Remark"),
                                                  GLName = isEnglish ? d.Field<String>("GLEName") : d.Field<String>("GLCName"),
                                                  IsSupportDirectBank = d.Field<bool>("IsSupportDirectBank"),
                                                  UpdateDate = d.Field<DateTime?>("UpdateDate"),
                                              }).SingleOrDefault();

                return info;
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

        #region 银行流水
        /// <summary>
        /// 通过销账单号支付(API)
        /// </summary>
        /// <param name="writeOffID">销账ID</param>
        /// <param name="saveBy">保存人</param>
        public void PluginPaymentByWriteOffID(Guid writeOffID,Guid saveBy)
        {
            try
            {
                //APIPaymentInfo paymentInfo = GetPaymentInfoByWriteOffID(writeOffID);
                //if (paymentInfo!=null && paymentInfo.RelativeBankCode==BANKCODE.CMB)
                //{
                //    if(paymentInfo.NetComUserID.IsNullOrEmpty())
                //    {
                //        throw new Exception("未配置招商银行一网通用户ID,请在公司配置界面配置后再操作");
                //    }
                //    string apiUrl = ClientConfig.Current.GetValue("CMBPaymentURL");
                //    string requestURL = string.Format(apiUrl
                //        , paymentInfo.NetComUserID, paymentInfo.BusinessNO, paymentInfo.RelativeAccountNO, paymentInfo.RelativeAccountName, paymentInfo.Amount, paymentInfo.Remark);
                //    HttpHelper http = new HttpHelper();
                //    HttpItem item = new HttpItem()
                //    {
                //        URL = requestURL,
                //        Encoding = Encoding.UTF8,
                //        Method = "GET",
                //        ContentType = "application/json",
                //    };
                //    //得到新的HTML代码
                //    HttpResult result = http.GetHtml(item);
                //    if (result.StatusCode != System.Net.HttpStatusCode.OK)
                //    {
                //        Framework.CommonLibrary.LogHelper.SaveLog(string.Format("调用服务发生异常:{0}", result.Html));
                //    }
                //    else
                //    {
                //        ResponseResult responseObj = JSONSerializerHelper.DeserializeFromJson<ResponseResult>(result.Html);
                //        if (responseObj.success)
                //        {
                //            UpdatePaymentInfoByWriteOffID(writeOffID, saveBy);
                //        }
                //        else
                //        {
                //            throw new Exception(responseObj.message);
                //        }
                //    }
                //}
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
        /// 根据销账ID获取支付信息
        /// </summary>
        /// <param name="writeOffID"></param>
        /// <returns></returns>
        public APIPaymentInfo GetPaymentInfoByWriteOffID(Guid writeOffID)
        {
            try
            {
                APIPaymentInfo result = null;
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetPaymentInfoByWriteOffID");
                db.AddInParameter(dbCommand, "@WriteOffID", DbType.Guid, writeOffID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds != null && ds.Tables.Count > 0)
                {
                    result = (from b in ds.Tables[0].AsEnumerable()
                              select new APIPaymentInfo
                              {
                                  CompanyID = b.Field<Guid>("CompanyID"),
                                  CustomerID = b.Field<Guid>("CustomerID"),
                                  BusinessID = b.Field<Guid>("BusinessID"),
                                  BusinessNO = b.Field<string>("BusinessNO"),
                                  RelativeBankCode = (BANKCODE)b.Field<byte>("BankType"),
                                  RelativeAccountNO = b.Field<string>("AccountNO"),
                                  RelativeAccountName = b.Field<string>("AccountName"),
                                  Amount = b.Field<decimal>("Amount"),
                                  CurrencyName = b.Field<string>("CurrencyName"),
                                  Remark = b.Field<string>("Remark"),
                              }).SingleOrDefault();
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
        /// 获取交易流水
        /// </summary>
        /// <param name="requestParameter"></param>
        /// <returns></returns>
        public List<BankTransactionInfo> GetTransList(BankTransactionSearchParameter requestParameter)
        {
            try
            {
                GetTransactionListByAPI(requestParameter);
                return SearchTransList(requestParameter);
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
        /// 获取银行/销账关联数据
        /// </summary>
        /// <param name="searchParameter"></param>
        /// <returns></returns>
        public List<BankTransaction2Checks> GetBankTransaction2Checks(BankTransaction2ChecksSearchParameter searchParameter)
        {
            try
            {
                List<BankTransaction2Checks> results = null;
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBankTransaction2Checks");
                db.AddInParameter(dbCommand, "@BankTransactionID", DbType.Guid, searchParameter.BankTransactionID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                else
                {
                    results = (from b in ds.Tables[0].AsEnumerable()
                               select new BankTransaction2Checks
                               {
                                   BankTransactionID = b.Field <Guid>("BankTransactionID"),
                                   ChecksID = b.Field<Guid>("ChecksID"),
                                   ChecksNO = b.Field<string>("ChecksNO"),
                                   ChecksWay = (FeeWay)b.Field<byte>("ChecksWay"),
                                   ChecksAmount = b.Field<decimal>("ChecksAmount"),
                                   CustomerName = b.Field<string>("CustomerName"),
                                   BankAccountName = b.Field<string>("BankAccountName"),
                                   CurrencyName = b.Field<string>("CurrencyName"),
                                   Checkser = b.Field<string>("Checkser"),
                                   ChecksDate = b.Field<DateTime>("ChecksDate"),
                               }).ToList();
                }

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
        /// 关联银行流水到销账数据
        /// </summary>
        /// <param name="saveRequest">保存对象</param>
        /// <returns></returns>
        public bool BankTransactionAssociationChecks(SaveRequestBankTransaction2Checks saveRequest)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveBankTransaction2Checks");
                #region 构建参数
                db.AddInParameter(dbCommand, "@BankTransactionID", DbType.String, saveRequest.BankTransactionID);
                db.AddInParameter(dbCommand, "@ChecksIDs", DbType.String, saveRequest.ChecksIDs.Join());
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveRequest.SaveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, saveRequest.UpdateDate);
                #endregion
                db.ManyResult(dbCommand, new[] { "ID" });
                return true;
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

        private List<BankTransactionInfo> SearchTransList(BankTransactionSearchParameter requestParameter)
        {
            try
            {
                List<BankTransactionInfo> results = null;
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBankTransactionList");
                db.AddInParameter(dbCommand, "@BusinessNO", DbType.String, requestParameter.BusinessNO);
                db.AddInParameter(dbCommand, "@FlowWaterNO", DbType.String, requestParameter.FlowWaterNO);
                db.AddInParameter(dbCommand, "@BankAccountID", DbType.Guid, requestParameter.BankAccountID);
                db.AddInParameter(dbCommand, "@RelativeAccountName", DbType.String, requestParameter.RelativeAccountName);
                db.AddInParameter(dbCommand, "@BeginDate", DbType.DateTime, requestParameter.BeginDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.DateTime, requestParameter.EndDate);
                db.AddInParameter(dbCommand, "@DebitCreditFlag", DbType.String, requestParameter.DebitCreditFlag);
                db.AddInParameter(dbCommand, "@MinimumAmount", DbType.Decimal, requestParameter.MinimumAmount);
                db.AddInParameter(dbCommand, "@MaximumAmount", DbType.Decimal, requestParameter.MaximumAmount);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<BankTransactionInfo>();
                }
                else
                {
                    results = (from b in ds.Tables[0].AsEnumerable()
                               select new BankTransactionInfo
                               {
                                   CompanyID = requestParameter.CompanyID,
                                   ID = b.Field<Guid>("ID"),
                                   BusinessNO = b.Field<string>("BusinessNO"),
                                   FlowWaterNO = b.Field<string>("FlowWaterNO"),
                                   AccountNO = b.Field<string>("AccountNO"),
                                   RelativeAccountNo = b.Field<string>("RelativeAccountNo"),
                                   RelativeAccountName = b.Field<string>("RelativeAccountName"),
                                   RelativeBankName = b.Field<string>("RelativeBankName"),
                                   CurrencyName = b.Field<string>("CurrencyName"),
                                   TransactionAmount = b.Field<decimal>("TransactionAmount"),
                                   OperationDateTime = b.Field<DateTime>("OperationDateTime"),
                                   DebitCreditFlag = b.Field<string>("DebitCreditFlag"),
                                   Remark = b.Field<string>("Remark"),
                               }).ToList();
                }

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



    }
}
