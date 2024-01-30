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
using System.Net;
using System.Text;
using System.Transactions;

namespace ICP.FAM.ServiceComponent
{
    /// <summary>
    /// 银企直连服务实现
    /// </summary>
    public partial class FinanceService
    {
        #region 获得指定公司下所有的银行账号信息
        /// <summary>
        /// 获取某个公司下的所有银行号信息
        /// 返回两列，一列ID是AccountID，一列CurrencyName“银行-币种”
        /// </summary>
        /// <param name="searchParameter"></param>
        /// <returns></returns>
        public List<BankAccountList> DirectBankAccountList(DirectBankSearchParameter searchParameter)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetDirectBankAccounts");

                db.AddInParameter(dbCommand, "@CompanyId", DbType.Guid, searchParameter.CompanyID);
                db.AddInParameter(dbCommand, "@OnlyDirectBank", DbType.Boolean, searchParameter.OnlyDirectBank);
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
                                                     BankName = b.Column<string>("BankName"),
                                                     CurrencyID = b.Column<Guid>("CurrencyID"),
                                                     CurrencyName = b.Column<string>("CurrencyName").Replace("\r\n", "-"),
                                                     CurrencyEName = b.Column<string>("CurrencyEName"),
                                                     IsInvoiceAccount = b.Column<bool>("IsInvoiceAccount"),
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

        #region 根据销账ID获取支付信息
        /// <summary>
        /// 根据销账ID获取支付信息
        /// </summary>
        /// <param name="searchParameter">查询对象</param>
        /// <returns></returns>
        public APIPaymentInfo GetSinglePaymentInfo(PaymentSearchParameter searchParameter)
        {
            try
            {
                List<APIPaymentInfo> list = GetBatchPaymentInfo(searchParameter);
                if(list.Count>1)
                {
                    throw new Exception("不支持多币种销账");
                }
                return list.SingleOrDefault();
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
        /// <param name="searchParameter">查询对象</param>
        /// <returns></returns>
        public List<APIPaymentInfo> GetBatchPaymentInfo(PaymentSearchParameter searchParameter)
        {
            try
            {
                List<APIPaymentInfo> result = new List<APIPaymentInfo>();
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetPaymentInfoList");
                db.AddInParameter(dbCommand, "@CheckAmountIDs", DbType.String, searchParameter.CheckAmountIDs.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds != null && ds.Tables.Count > 0)
                {
                    result = (from b in ds.Tables[0].AsEnumerable()
                              select new APIPaymentInfo
                              {
                                  CompanyID = b.Field<Guid>("CompanyID"),
                                  BusinessID = b.Field<Guid>("BusinessID"),
                                  CustomerID = b.Field<Guid>("CustomerID"),
                                  BusinessNO = b.Field<string>("BusinessNO"),
                                  BankAccountID = b.Field<Guid>("BankAccountID"),
                                  BankAccountNO = b.Field<string>("BankAccountNo"),
                                  BankAccountName = b.Field<string>("BankAccountName"),
                                  RelativeBankCode = (BANKCODE)b.Field<byte>("RelativeBankCode"),
                                  RelativeAccountNO = b.Field<string>("RelativeAccountNO"),
                                  RelativeAccountName = b.Field<string>("RelativeAccountName"),
                                  RelativeBankName = b.Field<string>("RelativeBankName"),
                                  RelativeBankNumber = b.Field<string>("RelativeBankhNumber"),
                                  RelativeBranchName = b.Field<string>("RelativeBranchName"),
                                  Amount = b.Field<decimal>("Amount"),
                                  CurrencyName = b.Field<string>("CurrencyName"),
                                  Remark = b.Field<string>("Remark"),
                              }).ToList();
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
        /// 根据销账ID获取支付信息
        /// </summary>
        /// <param name="searchParameter">查询对象</param>
        /// <returns></returns>
        public List<APIPaymentInfo> GetBankDirectPaymentInfoList(BatchPaymentSearchParameter searchParameter)
        {
            try
            {
                List<APIPaymentInfo> result = null;
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetBankDirectPaymentInfoList");
                db.AddInParameter(dbCommand, "@WriteOffIDs", DbType.String, searchParameter.WriteOffIDs.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds != null && ds.Tables.Count > 0)
                {
                    result = (from b in ds.Tables[0].AsEnumerable()
                              select new APIPaymentInfo
                              {
                                  BusinessID = b.Field<Guid>("BusinessID"),
                                  BusinessNO = b.Field<string>("BusinessNO"),
                                  BankAccountID = b.Field<Guid>("BankAccountID"),
                                  BankAccountNO = b.Field<string>("BankAccountName"),
                                  RelativeBankCode = (BANKCODE)b.Field<byte>("RelativeBankCode"),
                                  RelativeAccountNO = b.Field<string>("RelativeAccountNO"),
                                  RelativeAccountName = b.Field<string>("RelativeAccountName"),
                                  RelativeBranchName = b.Field<string>("RelativeBankName"),
                                  Amount = b.Field<decimal>("Amount"),
                                  CurrencyName = b.Field<string>("CurrencyName"),
                                  Remark = b.Field<string>("Remark"),
                              }).ToList();
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

        #region 获取支付验证码
        /// <summary>
        /// 获取支付验证码
        /// </summary>
        /// <param name="saveRequest">保存请求对象</param>
        /// <returns></returns>
        public void GetPaymentValidCode(SinglePaymentSaveRequest saveRequest)
        {
            try
            {
                GetPaymentValidCodeByAPI(saveRequest);
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

        #region 保存单个支付
        /// <summary>
        /// 保存单个支付
        /// </summary>
        /// <param name="saveRequest">保存请求对象</param>
        /// <returns></returns>
        public bool SaveSinglePaymentInfo(SinglePaymentSaveRequest saveRequest)
        {
            try
            {
                if (SinglePaymentByAPI(saveRequest))
                {
                    UpdatePaymentInfoByWriteOffID(saveRequest);
                }
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
        #endregion

        #region 保存批量支付
        /// <summary>
        /// 保存批量支付
        /// </summary>
        /// <param name="saveRequest">保存请求对象</param>
        /// <returns></returns>
        public bool SaveBatchPaymentInfo(BatchPaymentSaveRequest saveRequest)
        {
            try
            {
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
        #endregion

        #region 通过销账ID支付
        /// <summary>
        /// 通过销账ID支付
        /// </summary>
        /// <param name="writeOffID"></param>
        /// <param name="saveBy"></param>
        private void UpdatePaymentInfoByWriteOffID(SinglePaymentSaveRequest saveRequest)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspUpdatePaymentInfo");
                db.AddInParameter(dbCommand, "@CheckAmountID", DbType.Guid, saveRequest.BusinessID);
                db.AddInParameter(dbCommand, "@RelativeAccountNO", DbType.String, saveRequest.RelativeAccountNO);
                db.AddInParameter(dbCommand, "@RelativeAccountName", DbType.String, saveRequest.RelativeAccountName);
                db.AddInParameter(dbCommand, "@RelativeBankName", DbType.String, saveRequest.RelativeBankName);
                db.AddInParameter(dbCommand, "@RelativeBankNumber", DbType.String, saveRequest.RelativeBankNumber);
                db.AddInParameter(dbCommand, "@RelativeBranchName", DbType.String, saveRequest.RelativeBranchName);
                db.AddInParameter(dbCommand, "@SaveBy", DbType.Guid, saveRequest.SaveByID);
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

        #region 关联银行流水到销账数据
        /// <summary>
        /// 关联银行流水到销账数据
        /// </summary>
        /// <param name="saveRequest">保存请求对象</param>
        /// <returns></returns>
        public BankTransaction2Checks AssociationTransactionCheck(AssociationSaveRequest saveRequest)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetAssociationTransactionCheck");
                db.AddInParameter(dbCommand, "@BankTransactionID", DbType.Guid, saveRequest.BankTransactionID);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, saveRequest.CompanyID);
                db.AddInParameter(dbCommand, "@WriteOffNO", DbType.String, saveRequest.WriteOffNO);
                db.AddInParameter(dbCommand, "@SaveBy", DbType.Guid, saveRequest.SaveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new BankTransaction2Checks();
                }
                BankTransaction2Checks results = (from b in ds.Tables[0].AsEnumerable()
                                                 select new BankTransaction2Checks
                                                 {
                                                     ChecksID = b.Column<Guid>("ChecksID"),
                                                     ChecksNO = b.Column<string>("ChecksNO"),
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

        #region 关联银行流水到销账数据
        /// <summary>
        /// 关联银行流水到销账数据
        /// </summary>
        /// <param name="saveRequest">保存请求对象</param>
        /// <returns></returns>
        public bool AssociationTransactionToWriteOff(AssociationSaveRequest saveRequest)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspAssociationWriteOffForBankTransaction");
                db.AddInParameter(dbCommand, "@BankTransactionID", DbType.Guid, saveRequest.BankTransactionID);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, saveRequest.CompanyID);
                db.AddInParameter(dbCommand, "@WriteOffNO", DbType.String, saveRequest.WriteOffNO);
                db.AddInParameter(dbCommand, "@SaveBy", DbType.Guid, saveRequest.SaveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                db.ExecuteNonQuery(dbCommand);
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
        #endregion

        #region 关联银行流水到销账数据
        /// <summary>
        /// 关联银行流水到销账数据
        /// </summary>
        /// <param name="searchParameter">查询对象</param>
        /// <returns></returns>
        public AssociationInfo GetAssociationInfo(AssociationSearchParameter searchParameter)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetAssociationInfo");
                db.AddInParameter(dbCommand, "@BankTransactionID", DbType.Guid, searchParameter.BankTransactionID);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, searchParameter.CompanyID);
                db.AddInParameter(dbCommand, "@WriteOffNO", DbType.String, searchParameter.WriteOffNO);
                db.AddInParameter(dbCommand, "@SaveBy", DbType.Guid, searchParameter.SaveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                AssociationInfo results = (from b in ds.Tables[0].AsEnumerable()
                                           select new AssociationInfo
                                                           {
                                                               BankTransactionID = b.Column<Guid>("BankTransactionID"),
                                                               TransactionAmount = b.Column<decimal>("TransactionAmount"),
                                                               AssociationWriteOffNO = b.Column<string>("AssociationWriteOffNO"),
                                                               AssociationWriteOffDate = b.Column<DateTime?>("AssociationWriteOffDate"),
                                                               WriteOffNO = b.Column<string>("WriteOffNO"),
                                                               WriteOffAmount = b.Column<decimal>("WriteOffAmount"),
                                                               WriteOffDate = b.Column<DateTime>("WriteOffDate"),
                                                               ChargingClosingDate = b.Column<DateTime>("ChargingClosingDate"),
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

        #region 获取银行流水报表信息
        /// <summary>
        /// 获取银行流水报表信息
        /// </summary>
        /// <param name="searchParameter">查询参数</param>
        /// <returns></returns>
        public List<BankTransactionReportData> ReportDataForBankTransaction(BankTransactionReportSearchParameter searchParameter)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetReportForBankTransaction");

                db.AddInParameter(dbCommand, "@BankTransactionIDs", DbType.String, searchParameter.BankTransactionIDs.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<BankTransactionReportData>();
                }

                List<BankTransactionReportData> results = (from b in ds.Tables[0].AsEnumerable()
                                                      select new BankTransactionReportData
                                                 {
                                                     TransactionAmount = b.Column<string>("TransactionAmount"),
                                                     TransactionCurrency = b.Column<string>("TransactionCurrency"),
                                                     UseDescription = b.Column<string>("UseDescription"),
                                                     TransactionStatus = b.Column<string>("TransactionStatus"),
                                                     OperationDate = b.Column<string>("OperationDate"),
                                                     PaymentAccountNO = b.Column<string>("PaymentAccountNO"),
                                                     PaymentCurrency = b.Column<string>("PaymentCurrency"),
                                                     PaymentAccountName = b.Column<string>("PaymentAccountName"),
                                                     PaymentBranchName = b.Column<string>("PaymentBranchName"),
                                                     ReceiveAccountNO = b.Column<string>("ReceiveAccountNO"),
                                                     ReceiveCurrency = b.Column<string>("ReceiveCurrency"),
                                                     ReceiveAccountName = b.Column<string>("ReceiveAccountName"),
                                                     ReceiveBranchName = b.Column<string>("ReceiveBranchName"),
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

        #region API调用
        /// <summary>
        /// 通过API获取银行流水
        /// </summary>
        /// <param name="searchParameter"></param>
        /// <returns></returns>
        private bool GetTransactionListByAPI(BankTransactionSearchParameter searchParameter)
        {
            try
            {
                ConfigureInfo configureInfo = _configureService.GetCompanyConfigureInfo(searchParameter.CompanyID);
                if (configureInfo.CMBNetComUserID.IsNullOrEmpty())
                {
                    throw new Exception("未配置招商银行一网通用户ID,请在公司配置界面配置后再操作");
                }
                string beginDate = searchParameter.BeginDate == null ? DateTime.Now.ToString("yyyy-MM-dd") : searchParameter.BeginDate.Value.ToString("yyyy-MM-dd");
                string endDate = searchParameter.EndDate == null ? DateTime.Now.ToString("yyyy-MM-dd") : searchParameter.EndDate.Value.ToString("yyyy-MM-dd");

                BnakDirectRequest CMBRequest = new BnakDirectRequest()
                {
                    userid = searchParameter.Queryer,
                    bankcode = "" + searchParameter.BankCode.GetHashCode(),
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                };

                if (searchParameter.BankCode == BANKCODE.CMB)
                {
                    TransactionSearchData data = new TransactionSearchData()
                    {
                        UserID = configureInfo.CMBNetComUserID,
                        AccountNo = searchParameter.BankAccountNO,
                        CurrencyCode = searchParameter.CurrentName,
                        Content = new TransactionSearchDataDetail()
                        {
                            beginDate = beginDate,
                            endDate = endDate,
                        }
                    };
                    CMBRequest.data = JSONSerializerHelper.SerializeToJson(data);
                }

                string apiUrl = ClientConfig.Current.GetValue("CMBTransactionURL");
                string postData = JSONSerializerHelper.SerializeToJson(CMBRequest);
                HttpHelper http = new HttpHelper();
                HttpItem item = new HttpItem()
                {
                    URL = apiUrl,
                    Encoding = Encoding.UTF8,
                    Method = "POST",
                    Accept = "application/json",
                    ContentType = "application/json",
                    SecurityProtocolType = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls,
                    Postdata = postData,
                };
                HttpResult result = http.GetHtml(item);
                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(result.Html);
                }
                ResponseResult responseObj = JSONSerializerHelper.DeserializeFromJson<ResponseResult>(("" + result.Html).Trim());
                if (responseObj.success)
                {
                    return true;
                }
                throw new Exception(responseObj.message);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("调用招行服务发生异常:{0}", ex.Message));
            }
        }

        /// <summary>
        /// 通过API获取银行流水
        /// </summary>
        /// <param name="saveRequest">保存请求</param>
        /// <returns></returns>
        private void GetPaymentValidCodeByAPI(SinglePaymentSaveRequest saveRequest)
        {
            try
            {
                ConfigureInfo configureInfo = _configureService.GetCompanyConfigureInfo(saveRequest.CompanyID);
                if (configureInfo.CMBNetComUserID.IsNullOrEmpty())
                {
                    throw new Exception("未配置招商银行一网通用户ID,请在公司配置界面配置后再操作");
                }
                PaymentValidData data = new PaymentValidData()
                {
                    UserID = configureInfo.CMBNetComUserID,
                    AccountNo = saveRequest.AccountNO,
                    CurrencyCode = saveRequest.CurrencyName,
                    Content = new PaymentValidDataDetail()
                    {
                        WriteOffID = saveRequest.BusinessID,
                        WriteOffNO = saveRequest.BusinessNO,
                        WriteOffAmount = "" + saveRequest.Amount,
                        PayerID = saveRequest.SaveByID,
                        Payer = saveRequest.SaveBy,
                    }
                };
                Encoding edcoding = Encoding.UTF8;
                BnakDirectRequest CMBRequest = new BnakDirectRequest()
                {
                    userid = saveRequest.SaveByID,
                    bankcode = "" + saveRequest.BankCode.GetHashCode(),
                    date = saveRequest.UpdateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    data = Cryptography.Encrypt3DES(JSONSerializerHelper.SerializeToJson(data), ClientConstants.EncryptKey, edcoding),
                };
                string apiUrl = ClientConfig.Current.GetValue("CMBValidCode");
                string postData = JSONSerializerHelper.SerializeToJson(CMBRequest);
                HttpHelper http = new HttpHelper();
                HttpItem item = new HttpItem()
                {
                    URL = apiUrl,
                    Encoding = edcoding,
                    Method = "POST",
                    Accept = "application/json",
                    ContentType = "application/json",
                    SecurityProtocolType = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls,
                    Postdata = postData,
                };
                //得到新的HTML代码
                HttpResult result = http.GetHtml(item);
                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(result.Html);
                }
                else
                {
                    ResponseResult responseObj = JSONSerializerHelper.DeserializeFromJson<ResponseResult>(("" + result.Html).Trim());
                    if (responseObj.success)
                    {
                    }
                    else
                    {
                        throw new Exception(responseObj.message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("调用招行服务发生异常:{0}", ex.Message));
            }
        }

        /// <summary>
        /// 通过API支付
        /// </summary>
        /// <param name="saveRequest">保存请求</param>
        /// <returns></returns>
        private bool SinglePaymentByAPI(SinglePaymentSaveRequest saveRequest)
        {
            try
            {
                ConfigureInfo configureInfo = _configureService.GetCompanyConfigureInfo(saveRequest.CompanyID);
                if (configureInfo.CMBNetComUserID.IsNullOrEmpty())
                {
                    throw new Exception("未配置招商银行一网通用户ID,请在公司配置界面配置后再操作");
                }
                SinglePaymentData data = new SinglePaymentData()
                {
                    UserID = configureInfo.CMBNetComUserID,
                    AccountNo = saveRequest.AccountNO,
                    CurrencyCode = saveRequest.CurrencyName,
                    Content = new SinglePaymentDataDetail()
                    {
                        ValidCode = saveRequest.ValidCode,
                        PermissionMode = saveRequest.PermissionMode,
                        BusinessReferenceID = saveRequest.BusinessID,
                        BusinessReferenceNO = saveRequest.BusinessNO,
                        RelativeAccountNO = saveRequest.RelativeAccountNO,
                        RelativeAccountName = saveRequest.RelativeAccountName,
                        RelativeBranchName = saveRequest.RelativeBranchName,
                        RelativeBankName = saveRequest.RelativeBankName,
                        RelativeBankNumber = saveRequest.RelativeBankNumber,
                        SettlementMethod = saveRequest.SettlementMethod,
                        TransactionAmount = saveRequest.Amount,
                        TransactionRemark = saveRequest.Remark,
                        UseDescription = saveRequest.UseDescription,
                        PayerID = saveRequest.SaveByID,
                    }
                };
                Encoding edcoding = Encoding.UTF8;
                BnakDirectRequest CMBRequest = new BnakDirectRequest()
                {
                    userid = saveRequest.SaveByID,
                    bankcode = "" + saveRequest.BankCode.GetHashCode(),
                    date = saveRequest.UpdateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    data = Cryptography.Encrypt3DES(JSONSerializerHelper.SerializeToJson(data), ClientConstants.EncryptKey, edcoding),
                };
                string apiUrl = ClientConfig.Current.GetValue("CMBPaymentURL");
                string postData = JSONSerializerHelper.SerializeToJson(CMBRequest);
                HttpHelper http = new HttpHelper();
                HttpItem item = new HttpItem()
                {
                    URL = apiUrl,
                    Encoding = edcoding,
                    Method = "POST",
                    Accept = "application/json",
                    ContentType = "application/json",
                    SecurityProtocolType = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls,
                    Postdata = postData,
                };
                //得到新的HTML代码
                HttpResult result = http.GetHtml(item);
                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(result.Html);
                }
                else
                {
                    ResponseResult responseObj = JSONSerializerHelper.DeserializeFromJson<ResponseResult>(("" + result.Html).Trim());
                    if (responseObj.success)
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception(responseObj.message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("调用招行服务发生异常:{0}", ex.Message));
            }
        }
        #endregion
    }
}
