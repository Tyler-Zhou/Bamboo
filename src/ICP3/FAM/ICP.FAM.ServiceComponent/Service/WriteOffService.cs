using System;
using System.Collections.Generic;
using System.Linq;
using ICP.FAM.ServiceInterface.DataObjects;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using System.Data.SqlClient;
using ICP.FAM.ServiceInterface.CompositeObjects;
using System.Transactions;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;



namespace ICP.FAM.ServiceComponent
{
    /// <summary>
    /// 销账
    /// 创建时间：2011-07-11 15:50
    /// 作者：熊中方
    /// </summary>
    public partial class FinanceService
    {
        #region 获得核销列表
        /// <summary>
        /// 获取销账单列表
        /// </summary>
        /// <param name="searchParameter">查询参数</param>
        /// <returns></returns>
        public PageList GetWriteOffListByList(WriteOffSearchParameter searchParameter)
        {

            try
            {
                string companyIDList = searchParameter.CompanyID.Join();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetWriteOffList");

                db.AddInParameter(dbCommand, "@FeeWay", DbType.Int16, searchParameter.FeeWay);
                db.AddInParameter(dbCommand, "@CurrentUserID", DbType.Guid, searchParameter.CurrentUserID);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.String, companyIDList);
                db.AddInParameter(dbCommand, "@BankAccountID", DbType.Guid, searchParameter.BankAccountID);
                db.AddInParameter(dbCommand, "@CheckNo", DbType.String, searchParameter.CheckNo);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, searchParameter.CustomerName);
                db.AddInParameter(dbCommand, "@RealName", DbType.String, searchParameter.RealName);
                db.AddInParameter(dbCommand, "@OperationNo", DbType.String, searchParameter.OperationNo);
                db.AddInParameter(dbCommand, "@CustomerRefNo", DbType.String, searchParameter.CustomerRefNo);
                db.AddInParameter(dbCommand, "@CertificateNo", DbType.String, searchParameter.CertificateNo);
                db.AddInParameter(dbCommand, "@AuditorState", DbType.Int16, searchParameter.AuditorState);
                db.AddInParameter(dbCommand, "@OtherIDs", DbType.String, searchParameter.OtherIDs);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, searchParameter.IsValid);
                db.AddInParameter(dbCommand, "@IsReached", DbType.Boolean, searchParameter.IsReached);
                db.AddInParameter(dbCommand, "@CurrentPage", DbType.Int32, searchParameter.DataPageInfo.CurrentPage);
                db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, searchParameter.DataPageInfo.PageSize);
                db.AddInParameter(dbCommand, "@OrderByName", DbType.String, searchParameter.DataPageInfo.SortByName + " " + searchParameter.DataPageInfo.SortOrderType.ToString());
                db.AddInParameter(dbCommand, "@AmountMin", DbType.Decimal, searchParameter.AmountMin);
                db.AddInParameter(dbCommand, "@AmountMax", DbType.Decimal, searchParameter.AmountMax);
                db.AddInParameter(dbCommand, "@DateType", DbType.Int16, searchParameter.DateType);
                db.AddInParameter(dbCommand, "@StartDate", DbType.DateTime, searchParameter.StartDate);
                db.AddInParameter(dbCommand, "@EndDate", DbType.DateTime, searchParameter.EndDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, searchParameter.Remark);
                db.AddInParameter(dbCommand, "@IsDateLess", DbType.Boolean, searchParameter.IsDateLess);


                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<WriteOffItemList> results = (from b in ds.Tables[0].AsEnumerable()
                                                  select new WriteOffItemList
                                                  {
                                                      ID = b.Field<Guid>("ID"),
                                                      CheckID = b.Field<Guid>("CheckID"),
                                                      IsMultCurrency = b.Field<Boolean>("IsMultCurrency"),
                                                      CompanyID = b.Field<Guid>("CompanyID"),
                                                      Type = (FeeWay)b.Field<Byte>("Type"),
                                                      No = b.Field<String>("No"),
                                                      CheckNo = b.Field<String>("CheckNo"),
                                                      CustomerID = b.Field<Guid>("CustomerID"),
                                                      CustomerName = b.Field<String>("CustomerName"),
                                                      BankAccountID = b.Field<Guid>("BankAccountID"),
                                                      BankAccount = b.Field<String>("BankAccount"),
                                                      CurrencyID = b.Field<Guid>("CurrencyID"),
                                                      Currency = b.Field<String>("Currency"),
                                                      Amount = b.Field<Decimal>("Amount"),
                                                      WriteOffDate = b.Field<DateTime>("WriteOffDate"),
                                                      ReachedDate = b.Field<DateTime?>("ReachedDate"),
                                                      VoucherSeqNo = b.Field<String>("VoucherSeqNo"),
                                                      AuditByID = b.Field<Guid?>("AuditByID"),
                                                      ApprovalByName = b.Field<String>("ApprovalByName"),
                                                      ReachedByID = b.Field<Guid?>("ReachedByID"),
                                                      BankByName = b.Field<String>("BankByName"),
                                                      CreatedByName = b.Field<String>("CreatedByName"),
                                                      CreatedByID = b.Field<Guid>("CreatedByID"),
                                                      CreateDate = b.Field<DateTime>("CreateDate"),
                                                      Remark = b.Field<String>("Remark"),
                                                      UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                      CheckUpdateDate = b.Field<DateTime?>("CheckUpdateDate"),
                                                      IsValid = b.Field<bool>("IsValid"),
                                                      IsDirty = false,
                                                      VoidDate = b.Field<DateTime?>("VoidDate"),
                                                  }).ToList();

               searchParameter.DataPageInfo.TotalCount = ((from c in ds.Tables[1].AsEnumerable() select c.Column<Int32>("TotalCount")).SingleOrDefault());

               PageList list = PageList.Create<WriteOffItemList>(results, searchParameter.DataPageInfo);

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

        #region 获得核销的账单/费用 列表
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writeOffId"></param>
        /// <returns></returns>
        public List<WriteOffBill> GetWriteOffBillsByIds(Guid writeOffId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetWriteOffBills");

                db.AddInParameter(dbCommand, "@CheckID", DbType.Guid, writeOffId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<WriteOffBill> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new WriteOffBill
                                              {
                                                  ChargeID = b.Field<Guid>("ChargeID"),
                                                  ChargingCodeID = b.Field<Guid>("ChargingCodeID"),
                                                  ChargeName = b.Field<String>("ChargeName"),
                                                  BillID = b.Field<Guid>("BillID"),
                                                  BillNo = b.Field<String>("BillNo"),
                                                  CurrencyID = b.Field<Guid>("CurrencyID"),
                                                  CurrencyName = b.Field<String>("CurrencyName"),
                                                  BillAmount = b.Field<Decimal>("BillAmount"),
                                                  Amount = b.Field<Decimal>("ChargeAmount"),
                                                  StandardCurrencyRate = b.Field<Decimal>("StandardCurrencyRate"),
                                                  StandardCurrencyAmount = b.Field<Decimal>("StandardCurrencyAmount"),
                                                  AvailbeWriteOffAmount = b.Field<Decimal>("AvailbeWriteOffAmount"),
                                                  WriteOffAmount = b.Field<Decimal>("Amount"),
                                                  ExchangeRate = b.Field<Decimal>("WriteOffRate"),
                                                  FinalAmount = b.Field<Decimal>("FinalAmount"),
                                                  IsCommission = b.Field<Boolean>("IsCommission"),
                                                  ChargeUpdateDate = b.Field<DateTime?>("ChargeUpdateDate"),
                                                  BillUpdateDate = b.Field<DateTime?>("BillUpdateDate"),
                                                  UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                  Way = (FeeWay)b.Field<Byte>("Way")
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

        #region 获得核销其他项目

        /// <summary>
        /// 获得核销其他项目
        /// </summary>
        /// <param name="writeOffId"></param>
        /// <returns></returns>
        public List<WriteOffCharge> GetWriteOffCharges(Guid writeOffId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetWriteOffExpenses");

                db.AddInParameter(dbCommand, "@CheckID", DbType.Guid, writeOffId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<WriteOffCharge> results = (from b in ds.Tables[0].AsEnumerable()
                                                select new WriteOffCharge
                                                {
                                                    ID = b.Field<Guid>("ID"),
                                                    Way = (FeeWay)b.Field<Byte>("Way"),
                                                    BillNo = b.Field<String>("BillNo"),
                                                    CustomerID = b.Field<Guid?>("CustomerID"),
                                                    CustomerName = b.Field<String>("CustomerName"),
                                                    GLID = b.Field<Guid>("GLID"),
                                                    GLDescription = b.Field<String>("GLDescription"),
                                                    GLFullName = b.Field<String>("GLFullName"),
                                                    CurrencyID = b.Field<Guid>("CurrencyID"),
                                                    CurrencyName = b.Field<String>("CurrencyName"),
                                                    Amount = b.Field<Decimal>("Amount"),
                                                    StandardCurrencyRate = b.Field<Decimal>("StandardCurrencyRate"),
                                                    StandardCurrencyAmount = b.Field<Decimal>("StandardCurrencyAmount"),
                                                    ExchangeRate = b.Field<Decimal>("Rate"),
                                                    Remark = b.Field<String>("Remark"),
                                                    UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                    RefID=b.Field<Guid?>("RefID")
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
        
        #region 保存销账信息
        /// <summary>
        /// 保存销账信息
        /// </summary>
        /// <param name="saveRequest">销账信息实体</param>
        /// <param name="writeOffByID">销账人</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        public Dictionary<Guid, SaveResponse> SaveWriteOffInfo(SaveRequestCheck saveRequest, Guid writeOffByID, bool isEnglish)
        {
            string saveInfo = string.Empty;

            if (saveRequest != null)
            {
                System.Xml.Linq.XDocument doc = new System.Xml.Linq.XDocument();
                System.Xml.XmlWriter w = doc.CreateWriter();
                System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(saveRequest.GetType());

                s.Serialize(w, saveRequest);
                w.Flush();
                w.Close();
                saveInfo = doc.ToString();
            }

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspWriteOff");
                //销大账时，可能需要长时间等待。
                dbCommand.CommandTimeout = 600;
                db.AddInParameter(dbCommand, "@XMLContent", DbType.Xml, saveInfo);
                db.AddInParameter(dbCommand, "@WriteOffByID", DbType.Guid, writeOffByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);


                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                Dictionary<Guid, SaveResponse> result = new Dictionary<Guid, SaveResponse>();

                #region 获得销账信息

                DataRow woRow = ds.Tables[0].Rows[0];

                SingleResult woSingleResult = new SingleResult();
                woSingleResult.Add("ID", woRow.Field<Guid>("ID"));
                woSingleResult.Add("No", woRow.Field<String>("No"));
                woSingleResult.Add("InvoiceNo", woRow.Field<String>("InvoiceNo"));
                woSingleResult.Add("UpdateDate", woRow.Field<DateTime?>("UpdateDate"));
                saveRequest.ID = woRow.Field<Guid>("ID");
                result.Add(saveRequest.RequestId, new SaveResponse { RequestId = saveRequest.RequestId, SingleResult = woSingleResult });

                #endregion

                #region 获得币种信息
                EnumerableRowCollection<DataRow> currencyRows = ds.Tables[1].AsEnumerable();
                ManyResult currencyManyResult = new ManyResult();
                foreach (DataRow row in currencyRows)
                {
                    SingleResult currencyResult = new SingleResult();
                    currencyResult.Add("ID", row.Field<object>("ID"));
                    currencyResult.Add("UpdateDate", row.Field<DateTime?>("UpdateDate"));
                    currencyManyResult.Items.Add(currencyResult);
                }
                result.Add(saveRequest.CurrencyListRequestID, new SaveResponse { RequestId = saveRequest.CurrencyListRequestID, ManyResult = currencyManyResult });
                #endregion

                #region 获得其他项目费用信息
                EnumerableRowCollection<DataRow> expenseRows = ds.Tables[2].AsEnumerable();
                ManyResult expenseManyResult = new ManyResult();
                foreach (DataRow row in expenseRows)
                {
                    SingleResult expenseResult = new SingleResult();
                    expenseResult.Add("ID", row.Field<object>("ID"));
                    expenseResult.Add("UpdateDate", row.Field<DateTime?>("UpdateDate"));

                    expenseManyResult.Items.Add(expenseResult);
                }
                result.Add(saveRequest.ExpenseListRequestID, new SaveResponse { RequestId = saveRequest.ExpenseListRequestID, ManyResult = expenseManyResult });
                #endregion

                #region 获得账单/费用信息
                EnumerableRowCollection<DataRow> billFeeRows = ds.Tables[3].AsEnumerable();
                ManyResult billFeeManyResult = new ManyResult();
                foreach (DataRow row in billFeeRows)
                {
                    SingleResult billFeeResult = new SingleResult();
                    billFeeResult.Add("ID", row.Field<object>("ID"));
                    billFeeResult.Add("UpdateDate", row.Field<DateTime?>("UpdateDate"));
                    billFeeResult.Add("ChargeUpdateDate", row.Field<DateTime?>("ChargeUpdateDate"));
                    billFeeResult.Add("BillUpdateDate", row.Field<DateTime?>("BillUpdateDate"));

                    billFeeManyResult.Items.Add(billFeeResult);
                }
                result.Add(saveRequest.BillFeeListRequestID, new SaveResponse { RequestId = saveRequest.ExpenseListRequestID, ManyResult = billFeeManyResult });
                #endregion

                #region 异常信息
                if (ds.Tables.Count > 4)
                {
                    SendCheckErrorEmail(ds.Tables[4],woRow.Field<String>("No"));
                }
                #endregion
                SaveBillPaymentRecordForCSP(saveRequest);
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
        /// 发送销账异常的邮件给放货
        /// </summary>
        /// <param name="dt"></param>
        private void SendCheckErrorEmail(DataTable dt,string checkNo)
        {
            try
            {
                EnumerableRowCollection<DataRow> errorRows = dt.AsEnumerable();
                string messageList = string.Empty;
                foreach (DataRow row in errorRows)
                {
                    string no = row.Field<string>("No");
                    string errorMessage = row.Field<string>("ErrorMessage");

                    messageList = messageList + System.Environment.NewLine + "<" + no + "> " + errorMessage;
                }
                if (!string.IsNullOrEmpty(messageList))
                {
                    string subject = "以下业务在销账时放货异常";
                    if (string.IsNullOrEmpty(checkNo))
                    {
                        messageList = "删除销账单:"  + System.Environment.NewLine + messageList;
                    }
                    else
                    {
                        messageList = "销账单号:" + checkNo + System.Environment.NewLine + messageList;
                    }

                    try
                    {
                        //得到该公司的放货人员


                        //以系统管理员的帐号发给该公司的放货人员
                        ICP.Message.ServiceInterface.Message message = new Message.ServiceInterface.Message();
                        message.CreateBy = DataTypeHelper.GetGuid("4047CFAD-ECC8-E111-9D0D-0026551CA87B");
                        message.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                        message.HasAttachment = false;
                        message.SendFrom = "icpsystem@cityocean.com";
                        message.SendTo = "cindy@cityocean.net";
                        message.CC = "itservice@cityocean.com";
                        message.Subject = subject;
                        message.Body = messageList;
                        message.Type = ICP.Message.ServiceInterface.MessageType.Email;
                        message.BodyFormat = ICP.Message.ServiceInterface.BodyFormat.olFormatPlain;

                        _emailService.Send(message);
                    }
                    catch (Exception ex)
                    {
                        ICP.Framework.CommonLibrary.LogHelper.SaveLog(ex.Message + ex.StackTrace);
                    }

                }
                    
            }
            catch (Exception ex)
            {
                string sendTo = "itservice@cityocean.com";
                string subject = checkNo + "销账异常发送邮件时出现错误";
                SendEmail(sendTo, subject, ex.Message);
            }
        }

  
        #endregion

        #region 获得销账详细信息
        /// <summary>
        /// 获得销账详细信息
        /// </summary>
        /// <param name="id">销账ID</param>
        /// <returns></returns>
        public WriteOffItemInfo GetWriteOffItemInfo(Guid id)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetCheckInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                WriteOffItemInfo result = (from b in ds.Tables[0].AsEnumerable()
                                           select new WriteOffItemInfo
                                           {
                                               ID = b.Field<Guid>("ID"),
                                               No = b.Field<String>("No"),
                                               Way = (FeeWay)b.Field<Byte>("Type"),
                                               CompanyID = b.Field<Guid>("CompanyID"),
                                               CompanyName = b.Field<String>("CompanyName"),
                                               CheckNo = b.Field<String>("CheckNo"),
                                               InvoiceNo = b.Field<String>("InvoiceNo"),
                                               IsMultCurrency = b.Field<Boolean>("IsMultCurrency"),
                                               AuditorID = b.Field<Guid?>("ApprovalBy"),
                                               CustomerID = b.Field<Guid>("CustomerID"),
                                               CustomerName = b.Field<String>("CustomerName"),
                                               CheckDate = b.Field<DateTime>("CheckDate"),
                                               PayCustomerName = b.Field<String>("PayCustomer"),
                                               PayBankAccountNo = b.Field<string>("PayBankAccountNo"),
                                               PayBankBranchName = b.Field<string>("PayBankBranchName"),
                                               PayBankName = b.Field<string>("PayBankName"),
                                               PayBankNumber = b.Field<string>("PayBankNumber"),
                                               UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                               Remark = b.Field<String>("Remark"),
                                               IsValid = b.Field<Boolean>("IsValid"),
                                               IsPublic = b.Field<Boolean>("IsPublic"),
                                               CheckMode = (CheckMode)b.Field<Byte>("CheckMode"),
                                               BankReceiptID = b.IsNull("BankReceiptID") ? Guid.Empty : b.Field<Guid>("BankReceiptID"),
                                               BankReceiptNO = b.Field<string>("BankReceiptNO"),
                                               CreateByName = b.Field<String>("CreateByName"),
                                               CreateID = b.Field<Guid>("CreateID")
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

        #region 获得币种信息
        /// <summary>
        /// 获得币种信息
        /// </summary>
        /// <param name="id">销账ID</param>
        /// <returns></returns>
        public List<OperationCurrencyAmountList> GetOperationCurrencyAmountList(Guid id)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetCheckCurrencyAmountList");

                db.AddInParameter(dbCommand, "@CheckID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<OperationCurrencyAmountList> results = (from b in ds.Tables[0].AsEnumerable()
                                                             select new OperationCurrencyAmountList
                                                             {
                                                                 ID = b.Field<Guid>("ID"),
                                                                 CurrencyID = b.Field<Guid>("CurrencyID"),
                                                                 CurrencyName = b.Field<String>("CurrencyName"),
                                                                 TotalAmount = b.Field<Decimal>("Amount"),
                                                                 StandardCurrencyAmount = b.Field<Decimal>("StandardCurrencyAmount"),
                                                                 BankAccountID = b.Field<Guid>("BankAccountID"),
                                                                 BankName = b.Field<String>("BankName"),
                                                                 TotalBillAmount = b.Field<Decimal>("BillAmount"),
                                                                 StandardCurrencyBillAmount = b.Field<Decimal>("StandardCurrencyBillAmount"),
                                                                 TotalOtherAmount = b.Field<Decimal>("ExpensesAmount"),
                                                                 StandardCurrencyOtherAmount = b.Field<Decimal>("StandardCurrencyExpensesAmount"),
                                                                 BankByID = b.Field<Guid?>("BankBy"),
                                                                 BankDate = b.Field<DateTime?>("BankDate"),
                                                                 IsReached = !(b.Field<Guid?>("BankBy") == null),
                                                                 VoucherSeqNo = b.Field<String>("VoucherSeqNo"),
                                                                 BankTransactionID =b.Field<Guid?>("BankTransactionID"),
                                                                 BankTransactionNO = b.Field<string>("BankTransactionNO"),
                                                                 AssociationType = (BTAWType)b.Field<byte>("AssociationType"),
                                                                 IsSupportDirectBank = b.Field<bool>("IsSupportDirectBank"),
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

        #endregion

        #region 作废销帐信息
        /// <summary>
        /// 作废销帐信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isValid"></param>
        /// <param name="changeByID"></param>
        /// <param name="updateDate"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        public SingleResult VoidCheckData(Guid id, bool isValid, Guid changeByID, DateTime? updateDate, bool isEnglish)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "Id");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspChangeCheckState");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = db.ExecuteDataSet(dbCommand);
                SingleResult result = new SingleResult();
                result.Add("ID", ds.Tables[0].Rows[0]["ID"]);
                result.Add("No", ds.Tables[0].Rows[0]["No"]);
                result.Add("UpdateDate", ds.Tables[0].Rows[0]["UpdateDate"]);

                if (ds.Tables.Count > 1)
                {
                    SendCheckErrorEmail(ds.Tables[1],DataTypeHelper.GetString(ds.Tables[0].Rows[0]["No"]));
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

        #region 删除销账信息
        /// <summary>
        /// 删除销账信息
        /// </summary>
        /// <param name="id">销账单ID</param>
        /// <param name="removeByID">删除人ID</param>
        /// <param name="updateDate">更新时间</param>
        public void RemoveWriterOff(Guid id, Guid removeByID, DateTime? updateDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspRemoveCheckInfo");

                db.AddInParameter(dbCommand, "@CheckId", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);


                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables.Count > 0)
                {
                    SendCheckErrorEmail(ds.Tables[0],string.Empty);
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

        #region 删除销账币种信息
        /// <summary>
        /// 删除销账信息
        /// </summary>
        /// <param name="id">销账单币种ID</param>
        /// <param name="removeByID">删除人ID</param>
        /// <param name="updateDate">更新时间</param>
        public void RemoveWriterCurrency(Guid id, Guid removeByID, DateTime? updateDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspRemoveCheckCurrencyInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables.Count > 0)
                {
                    SendCheckErrorEmail(ds.Tables[0], string.Empty);
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

        #region 审核/取消审核 销账信息
        /// <summary>
        /// 审核/取消审核 销账单
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="updateDates">更新时间集合</param>
        /// <param name="auditorById">操作人</param>
        /// <param name="isCheck">是否审核:True为审核,False为取消审核</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        public ManyResult AuditorWriterOff(
            Guid[] ids,
            DateTime?[] updateDates,
            Guid auditorById,
            bool isCheck)
        {
            try
            {
                string idList = ids.Join();
                string updateList = updateDates.Join();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspAuditorCheck");

                db.AddInParameter(dbCommand, "@Ids", DbType.String, idList);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateList);
                db.AddInParameter(dbCommand, "@AuditorById", DbType.Guid, auditorById);
                db.AddInParameter(dbCommand, "@IsCheck", DbType.Boolean, isCheck);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);


                ManyResult result = db.ManyResult(dbCommand, new string[] { "CheckID", "CheckUpdateDate", "ID", "VoucherSeqNo", "UpdateDate" });

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

        #region 检查是否有重复的:到帐时间，实收/付金额和银行都相同的已确认到帐的销帐列表
        /// <summary>
        /// 检查是否有重复的:到帐时间，实收/付金额和银行都相同的已确认到帐的销帐列表
        /// </summary>
        /// <param name="checkId"></param>
        /// <param name="receivedAmts"></param>
        /// <param name="bankDates"></param>
        /// <param name="currencys"></param>
        /// <returns></returns>
        public string CheckExistBankReceived(Guid[] checkIds, DateTime?[] bankDates, decimal[] receivedAmts, Guid?[] bankAccountIDs)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspCheckExistBankReceived");

                db.AddInParameter(dbCommand, "@CheckIds", DbType.String, checkIds.Join());
                db.AddInParameter(dbCommand, "@ReceivedAmts", DbType.String, receivedAmts.Join());
                db.AddInParameter(dbCommand, "@BankDates", DbType.String, bankDates.Join());
                db.AddInParameter(dbCommand, "@BankAccountIDs", DbType.String, bankAccountIDs.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }
                string message = ds.Tables[0].Rows[0][0] == null ? string.Empty : ds.Tables[0].Rows[0][0].ToString();
                return message;
                //SingleResult result = db.SingleResult(dbCommand, new string[] { "Message" });
                ////if (result == null)
                ////{
                ////    return string.Empty; 
                ////}
                //return result.GetValue<string>("Message");
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

        #region 到账
        /// <summary>
        /// 到帐
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="reachedDate">到账时间集合</param>
        /// <param name="amounts">金额集合</param>
        /// <param name="accountIDs">银行账号ID集合</param>
        /// <param name="updateDates">更新时间集合</param>
        /// <param name="chargeByID">更改人ID</param>
        /// <returns></returns>
        public ManyResult WriteOffReachedByCheck(
                             Guid[] ids,
                             DateTime?[] reachedDates,
                             decimal[] amounts,
                             Guid?[] accountIDs,
                             DateTime?[] updateDates,
                             Guid chargeByID)
        {
            try
            {


                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSetBankInfoForCheck");

                db.AddInParameter(dbCommand, "@Ids", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@BankDates", DbType.String, reachedDates.Join());
                db.AddInParameter(dbCommand, "@Amounts", DbType.String, amounts.Join());
                db.AddInParameter(dbCommand, "@AccountIDs", DbType.String, accountIDs.Join());
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.Join());
                db.AddInParameter(dbCommand, "@ChargeByID", DbType.Guid, chargeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);


                ManyResult results = db.ManyResult(dbCommand, new string[] { "ID", "UpdateDate", "BankDate" });

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

        #region 取消到帐
        /// <summary>
        /// 取消到帐
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="remark">备注信息</param>
        /// <param name="chargeByID">操作人</param>
        /// <param name="updateDates">最后更新时间集合</param>
        /// <returns></returns>
        public ManyResult CancelReached(
                            Guid[] ids,
                            string remark,
                            Guid chargeByID,
                            DateTime?[] updateDates)
        {
            try
            {

                string idList = ids.Join();
                string updateList = updateDates.Join();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspCancelBankInfo");

                db.AddInParameter(dbCommand, "@Ids", DbType.String, idList);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateList);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, remark);
                db.AddInParameter(dbCommand, "@ChargeByID", DbType.Guid, chargeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);


                ManyResult results = db.ManyResult(dbCommand, new string[] { "ID", "UpdateDate" });

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

        #region 凭证明细

        /// <summary>
        /// 根据核销单的ID获得凭证明细
        /// </summary>
        /// <param name="writeOffID"></param>
        /// <returns></returns>
        public List<CredentialsDetailList> GetCredentialsDetailList(Guid writeOffID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetCredentialsDetailList");

                db.AddInParameter(dbCommand, "@WriteOffID", DbType.Guid, writeOffID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CredentialsDetailList> results = (from b in ds.Tables[0].AsEnumerable()
                                                       select new CredentialsDetailList
                                                       {
                                                           ID = b.Field<Guid>("ID"),
                                                           WriteOffID = b.Field<Guid>("WriteOffID"),
                                                           //GLID = b.Field<Guid>("GLID"),
                                                           GL = b.Field<String>("GLName"),
                                                           Remark = b.Field<String>("Remark"),
                                                           OrgDebit = b.Field<Decimal>("OrgDebit"),
                                                           OrgCredit = b.Field<Decimal>("OrgCredit"),
                                                           Rate = b.Field<Decimal>("Rate"),
                                                           Debit = b.Field<Decimal>("Debit"),
                                                           Credit = b.Field<Decimal>("Credit"),
                                                           //CustomerID = b.Field<Guid>("CustomerID"),
                                                           CustomerName = b.Field<String>("CustomerName"),
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

        #endregion

        #region 自动生成凭证

        /// <summary>
        /// 自动生成凭证
        /// </summary>
        /// <param name="writeOffID"></param>
        /// <param name="saveByID"></param>
        public void BuildRPLedgers(
           Guid writeOffID,
           Guid saveByID)
        {
            ArgumentHelper.AssertGuidNotEmpty(writeOffID, "writeOffID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("FAM.uspBuildRPLedgersForAsy");

                db.AddInParameter(dbCommand, "@CheckIDS", DbType.Guid, writeOffID);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                //db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

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

        #region 删除凭证明细
        /// <summary>
        /// 删除凭证明细
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDate"></param>
        /// <param name="removeByID"></param>
        public void RemoveCredentialsDetail(
            Guid id,
            DateTime? updateDate,
            Guid removeByID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspRemoveCredentialsDetail");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
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

        #region 保存凭证明细

        /// <summary>
        /// 保存凭证明细
        /// </summary>
        /// <param name="writeOffID">销账ID</param>
        /// <param name="ids">ID集合</param>
        /// <param name="glIDs">会计科目ID集合</param>
        /// <param name="remarks">Remark集合</param>
        /// <param name="orgDebigs">orgDebig集合</param>
        /// <param name="orgCredits">orgCredit集合</param>
        /// <param name="rates">汇率集合</param>
        /// <param name="debigs">debig集合</param>
        /// <param name="credits">credit集合</param>
        /// <param name="customers">customer集合</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">最后更新时间集合</param>
        /// <returns></returns>
        public ManyResult SaveCredentialsDetail(
                Guid writeOffID,
                Guid?[] ids,
                Guid[] glIDs,
                string[] remarks,
                decimal[] orgDebigs,
                decimal[] orgCredits,
                decimal[] rates,
                decimal[] debigs,
                decimal[] credits,
                Guid[] customers,
                Guid saveByID,
                DateTime?[] updateDates)
        {
            try
            {
                string idList = ids.Join();
                string glIDList = glIDs.Join();
                string remarkList = remarks.Join();
                string orgDebigList = orgDebigs.Join();
                string orgCreditList = orgCredits.Join();
                string rateList = rates.Join();
                string debigList = debigs.Join();
                string creditList = credits.Join();
                string customerList = customers.Join();
                string updateDateList = updateDates.Join();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveCredentialsDetail");

                db.AddInParameter(dbCommand, "@WriteOffID", DbType.Guid, writeOffID);
                db.AddInParameter(dbCommand, "@Ids", DbType.String, idList);
                db.AddInParameter(dbCommand, "@GlIDs", DbType.String, glIDList);
                db.AddInParameter(dbCommand, "@Remarks", DbType.String, remarkList);
                db.AddInParameter(dbCommand, "@OrgDebigs", DbType.String, orgDebigList);
                db.AddInParameter(dbCommand, "@OrgCredits", DbType.String, orgCreditList);
                db.AddInParameter(dbCommand, "@Rates", DbType.String, rateList);
                db.AddInParameter(dbCommand, "@Debigs", DbType.String, debigList);
                db.AddInParameter(dbCommand, "@Credits", DbType.String, creditList);
                db.AddInParameter(dbCommand, "@Customers", DbType.String, customerList);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDateList);
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

        #region 根据币种账单信息获得费用信息并转换为销账信息
        /// <summary>
        /// 根据币种账单信息获得费用信息并转换为销账信息
        /// </summary>
        /// <param name="billIDs">账单ID集合</param>
        /// <param name="currencyIDs">币种ID集合</param>
        /// <param name="feeWays">收付方向集合</param>
        /// <param name="isCommissions">是否佣金集合</param>
        /// <returns></returns>
        public List<WriteOffBill> GetWriteOffBills(List<CurrencyBillList> currencyList, bool isEnglish)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetChargeListForCheck");

            List<Guid> billIDs = new List<Guid>();
            List<Guid> currencyIDs = new List<Guid>();
            List<string> Ways = new List<string>();
            List<string> isCommissions = new List<string>();
            foreach (var sub in currencyList)
            {
                billIDs.Add(sub.ID);
                currencyIDs.Add(sub.CurrencyID);
                Ways.Add(((int)sub.Way).ToString());
                int com = sub.IsCommission == true ? 1 : 0;
                isCommissions.Add(com.ToString());
            }

            db.AddInParameter(dbCommand, "@BillIDS", DbType.String, billIDs.ToArray().Join());
            db.AddInParameter(dbCommand, "@CurrencyIDs", DbType.String, currencyIDs.ToArray().Join());
            db.AddInParameter(dbCommand, "@Ways", DbType.String, Ways.ToArray().Join());
            db.AddInParameter(dbCommand, "@IsCommissions", DbType.String, isCommissions.ToArray().Join());
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

            DataSet ds = null;
            ds = db.ExecuteDataSet(dbCommand);
            if (ds == null || ds.Tables.Count < 1)
            {
                return null;
            }

            List<WriteOffBill> results = (from b in ds.Tables[0].AsEnumerable()
                                          select new WriteOffBill
                                          {
                                              Way = (FeeWay)b.Field<Byte>("Way"),
                                              IsCommission = b.Field<Boolean>("IsCommission"),
                                              ChargeID = b.Field<Guid>("ID"),
                                              ChargingCodeID = b.Field<Guid>("ChargingCodeID"),
                                              ChargeName = b.Field<string>("ChargingName"),
                                              ChargeUpdateDate = b.Field<DateTime?>("UpdateDate"),
                                              BillID = b.Field<Guid>("BillID"),
                                              BillNo = b.Field<string>("BillNo"),
                                              CurrencyID = b.Field<Guid>("CurrencyID"),
                                              CurrencyName = b.Field<string>("CurrencyName"),
                                              BillAmount = b.Field<decimal>("BillAmount"),
                                              Amount = b.Field<decimal>("Amount"),
                                              AvailbeWriteOffAmount = b.Field<decimal>("AvailbeWriteOffAmount"),
                                              WriteOffAmount = b.Field<decimal>("WriteOffAmount"),
                                              ExchangeRate = 0.00M,
                                              BillUpdateDate = b.Field<DateTime?>("BillUpdateDate"),
                                              FinalAmount = 0.00M,
                                              PreCurrencyID = b.Field<Guid?>("PreCurrencyID"),
                                              PreAmount = b.Field<Decimal?>("PreAmount"),
                                              PrepaymentID = b.Field<Guid?>("PrepaymentID"),
                                          }).ToList();

            return results;

            ////获得账单的所有费用信息
            //List<Guid> billIDs = (from d in currencyList group d by d.ID into g select g.Key).ToList();
            //List<ChargeList> chargeList = this.GetChargeList(billIDs.ToArray());

            //List<WriteOffBill> List = new List<WriteOffBill>();

            ////取提匹配的信息
            //foreach (CurrencyBillList item in currencyList)
            //{
            //    List<WriteOffBill> billCharge = (from d in chargeList
            //                                     where
            //                                     d.BillID == item.ID &&
            //                                     d.Way == item.Way &&
            //                                     d.CurrencyID == item.CurrencyID &&
            //                                     d.IsCommission == item.IsCommission &&
            //                                     d.Amount != d.PayAmount
            //                                     select new WriteOffBill
            //                                     {
            //                                         Way = d.Way,
            //                                         IsCommission = d.IsCommission,
            //                                         ChargeID = d.ID,
            //                                         ChargingCodeID = d.ChargingCodeID,
            //                                         ChargeName = d.ChargingCode,
            //                                         ChargeUpdateDate = d.UpdateDate,
            //                                         BillID = d.BillID,
            //                                         BillNo = item.BillNO,
            //                                         CurrencyID = d.CurrencyID,
            //                                         CurrencyName = d.CurrencyName,
            //                                         BillAmount= d.BillAmount,
            //                                         Amount = d.Amount,
            //                                         AvailbeWriteOffAmount = d.Amount - d.PayAmount,
            //                                         WriteOffAmount = d.Amount - d.PayAmount,
            //                                         ExchangeRate = 0.00M,
            //                                         //FinalAmount = ((d.Amount - d.PayAmount) * d.Rate) 
            //                                         BillUpdateDate = d.BillUpdateDate,
            //                                         FinalAmount = 0.00M
            //                                     }).ToList();


            //    foreach (WriteOffBill bill in billCharge)
            //    {
            //        List.Add(bill);
            //    }
            //}


            //return List;

        }
        #endregion

        #region 获得核销列表
        /// <summary>
        /// 获取销账单列表
        /// </summary>
        /// <param name="checkID">销账单ID</param>
        /// <returns></returns>
        public List<WriteOffItemList> GetWriteOffListByIds(Guid[] checkID)
        {

            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetWriteOffListByCheckID");

                db.AddInParameter(dbCommand, "@CheckID", DbType.String, checkID.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<WriteOffItemList> results = (from b in ds.Tables[0].AsEnumerable()
                                                  select new WriteOffItemList
                                                  {
                                                      ID = b.Field<Guid>("ID"),
                                                      CheckID = b.Field<Guid>("CheckID"),
                                                      IsMultCurrency = b.Field<Boolean>("IsMultCurrency"),
                                                      CompanyID = b.Field<Guid>("CompanyID"),
                                                      Type = (FeeWay)b.Field<Byte>("Type"),
                                                      No = b.Field<String>("No"),
                                                      CheckNo = b.Field<String>("CheckNo"),
                                                      CustomerID = b.Field<Guid>("CustomerID"),
                                                      CustomerName = b.Field<String>("CustomerName"),
                                                      BankAccountID = b.Field<Guid>("BankAccountID"),
                                                      BankAccount = b.Field<String>("BankAccount"),
                                                      CurrencyID = b.Field<Guid>("CurrencyID"),
                                                      Currency = b.Field<String>("Currency"),
                                                      Amount = b.Field<Decimal>("Amount"),
                                                      WriteOffDate = b.Field<DateTime>("WriteOffDate"),
                                                      ReachedDate = b.Field<DateTime?>("ReachedDate"),
                                                      VoucherSeqNo = b.Field<String>("VoucherSeqNo"),
                                                      AuditByID = b.Field<Guid?>("AuditByID"),
                                                      ApprovalByName = b.Field<String>("ApprovalByName"),
                                                      ReachedByID = b.Field<Guid?>("ReachedByID"),
                                                      BankByName = b.Field<String>("BankByName"),
                                                      CreatedByName = b.Field<String>("CreatedByName"),
                                                      CreatedByID = b.Field<Guid>("CreatedByID"),
                                                      CreateDate = b.Field<DateTime>("CreateDate"),
                                                      Remark = b.Field<String>("Remark"),
                                                      UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                      CheckUpdateDate = b.Field<DateTime?>("CheckUpdateDate"),
                                                      IsValid = b.Field<bool>("IsValid"),
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

        #region 根据费用ID获得发票列表
        /// <summary>
        /// 获得发票列表
        /// </summary>
        /// <param name="feeIds"></param>
        /// <returns></returns>
        public List<InvoiceList> GetInvoiceListByFeeID(Guid[] chargeIDs)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetInvoiceListByChargeID");


                db.AddInParameter(dbCommand, "@ChargeIDs", DbType.String, chargeIDs.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<InvoiceList> results = (from b in ds.Tables[0].AsEnumerable()
                                             select new InvoiceList
                                             {
                                                 ID = b.Column<Guid>("ID"),
                                                 InvoiceNo = b.Column<string>("InvoiceNo"),
                                                 InvoiceDate = b.Column<DateTime>("InvoiceDate"),
                                                 CustomerName = b.Column<string>("CustomerName"),
                                                 BillNo = b.Column<string>("BillNo"),
                                                 Amounts = b.Column<String>("Amounts"),
                                                 CreateByName = b.Column<string>("CreateByName"),
                                                 CreateByID = b.Column<Guid>("CreateByID"),
                                                 CreateDate = b.Column<DateTime>("CreateDate"),
                                                 IsValid = b.Column<bool>("IsValid"),
                                                 UpdateDate = b.Column<DateTime?>("UpdateDate"),
                                                 BLNo = b.Column<String>("BLNo"),
                                                 ETD = b.Column<DateTime?>("ETD"),
                                                 ExpressNo = b.Column<String>("ExpressNo"),
                                                 ExpressDate = b.Column<DateTime?>("ExpressDate"),
                                                 Remark = b.Column<String>("Remark"),
                                                 Selected = false,
                                                 IsDirty = false,
                                             }).ToList();

                return results;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region 保存其他项目(差异)
        public ManyResult SaveExpenseInfo(
                     Guid[] CheckIDs,
                     Guid?[] CustomerIDs,
                     String[] BillNos,
                     Guid[] GLIDs,
                     Guid[] CurrencyIDs,
                     Decimal[] Rates,
                     Decimal[] Amounts,
                     String[] Remarks,
                     Int32[] Ways,
                     Guid saveByID)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveExpenseInfo");

                db.AddInParameter(dbCommand, "@CheckIDs", DbType.String, CheckIDs.Join());
                db.AddInParameter(dbCommand, "@CustomerIDs", DbType.String, CustomerIDs.Join());
                db.AddInParameter(dbCommand, "@BillNos", DbType.String, BillNos.Join());
                db.AddInParameter(dbCommand, "@GLIDs", DbType.String, GLIDs.Join());
                db.AddInParameter(dbCommand, "@CurrencyIDs", DbType.String, CurrencyIDs.Join());
                db.AddInParameter(dbCommand, "@Rates", DbType.String, Rates.Join());
                db.AddInParameter(dbCommand, "@Amounts", DbType.String, Amounts.Join());
                db.AddInParameter(dbCommand, "@Remarks", DbType.String, Remarks.Join());
                db.AddInParameter(dbCommand, "@Ways", DbType.String, Ways.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);



                ManyResult results = db.ManyResult(dbCommand, new string[] { "ID", "UpdateDate" });

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

        #region 以事务的方式保存到账与差异信息
        /// <summary>
        /// 以事务的方式保存到账与差异信息
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="reachedDates"></param>
        /// <param name="amounts"></param>
        /// <param name="accountIDs"></param>
        /// <param name="updateDates"></param>
        /// <param name="chargeByID"></param>
        /// <param name="saveExpense"></param>
        /// <returns></returns>
        public ManyResult WriteOffReached(
                             Guid[] ids,
                             DateTime?[] reachedDates,
                             decimal[] amounts,
                             Guid?[] accountIDs,
                             DateTime?[] updateDates,
                             Guid chargeByID,
                             SaveExpenseList saveExpense)
        {
            TransactionOptions option = new TransactionOptions();
            option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                //保存差异
                ManyResult expensesList = this.SaveExpenseInfo(saveExpense.CheckIDs,
                                saveExpense.CustomerIDs,
                                saveExpense.BillNos,
                                saveExpense.GLIDs,
                                saveExpense.CurrencyIDs,
                                saveExpense.Rates,
                                saveExpense.Amounts,
                                saveExpense.Remarks,
                                saveExpense.Ways,
                                chargeByID);

                //更新fam.CheckAmounts的UpdateDate
                foreach (SingleResult item in expensesList.Items)
                {
                    Guid id = item.GetValue<Guid>("ID");
                    DateTime? updateDate = item.GetValue<DateTime?>("UpdateDate");

                    for (int i = 0; i <= ids.Length - 1; i++)
                    {
                        if (ids[i] == id)
                        {
                            updateDates[i] = updateDate;
                        }
                    }

                }


                //保存到账
                ManyResult result = this.WriteOffReachedByCheck(ids, reachedDates, amounts, accountIDs, updateDates, chargeByID);

                scope.Complete();

                return result;

            }
        }
        #endregion

        #region 获得多收多付列表
        public List<PrepaymentList> GetPrepaymentList(Guid companyID, Guid customerID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetPrepaymentList");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<PrepaymentList> results = (from b in ds.Tables[0].AsEnumerable()
                                                select new PrepaymentList
                                                {
                                                    ID = b.Field<Guid>("ID"),
                                                    RefID=b.Field<Guid>("RefID"),
                                                    CheckID = b.Field<Guid>("CheckID"),
                                                    CheckNo = b.Field<String>("CheckNo"),
                                                    CheckDate = b.Field<DateTime>("CheckDate"),
                                                    BillNo = b.Field<String>("BillNo"),
                                                    Way = (FeeWay)b.Field<Byte>("Way"),
                                                    CurrencyID = b.Field<Guid>("CurrencyID"),
                                                    Amount = b.Field<Decimal>("Amount"),
                                                    PayAmount = b.Field<Decimal>("PayAmount"),
                                                    CreateBy = b.Field<String>("CreateBy"),
                                                    CreateDate = b.Field<DateTime>("CreateDate")
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

        #region 解锁销账单
        public List<UntieLockCheckResult> UntieLockChecks(Guid[] checkIds, DateTime?[] checkUpdates, Guid[] checkAmountIDs, DateTime?[] checkAmountUpdates, Guid chargeByID)
        {
            TransactionOptions option = new TransactionOptions();
            option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                List<UntieLockCheckResult> list = new List<UntieLockCheckResult>();

                //插入解锁数据
                _systemService.SaveUntieLockInfo(UntieLockType.Check, checkIds, chargeByID);

                //取消审核
                ManyResult result1=AuditorWriterOff(checkIds, checkUpdates, chargeByID, false);
                foreach (SingleResult item in result1.Items)
                {
                    UntieLockCheckResult newList = new UntieLockCheckResult();
                    newList.Type = 1;
                    newList.ID = item.GetValue<Guid>("CheckID");
                    newList.UpdateDate = item.GetValue<DateTime?>("CheckUpdateDate");
                    list.Add(newList);

                    //取消审核时，会更新fam.CheckAmount中的数据，所以要将fam.CheckAmount.UpdateDate集合重新更新一下
                    Guid id = item.GetValue<Guid>("ID");
                    DateTime? updateDate = item.GetValue<DateTime?>("UpdateDate");
                    int m = 0;
                    foreach (Guid checkAmountID in checkAmountIDs)
                    {
                        if (checkAmountID == id)
                        {
                            checkAmountUpdates[m] = updateDate;
                        }
                        m++;
                    }
                }
                
                //取消到帐
                ManyResult result2=CancelReached(checkAmountIDs,string.Empty,chargeByID,checkAmountUpdates);

              
                scope.Complete();
                foreach (SingleResult item in result2.Items)
                {
                    UntieLockCheckResult newList = new UntieLockCheckResult();
                    newList.Type = 2;
                    newList.ID = item.GetValue<Guid>("ID");
                    newList.UpdateDate = item.GetValue<DateTime?>("UpdateDate");
                    list.Add(newList);
                }
                return list;
            }
        }
        #endregion

        #region 支付客户信息
        /// <summary>
        /// 支付客户信息
        /// </summary>
        /// <param name="searchParameter"></param>
        /// <returns></returns>
        public List<CustomerBankInfo> AllCustomerBanks(CustomerBankInfoSearchParameter searchParameter)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCustomerBankList");

                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, searchParameter.CustomerID);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, searchParameter.CustomerName);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CustomerBankInfo> results = (from b in ds.Tables[0].AsEnumerable()
                                                  select new CustomerBankInfo
                                                  {

                                                      CustomerID = b.Field<Guid>("CustomerID"),
                                                      AccountName = b.Field<string>("AccountName"),
                                                      AccountNO = b.Field<string>("AccountNO"),
                                                      BranchName = b.Field<string>("BranchName"),
                                                      BankNumber = b.Field<string>("BankNumber"),
                                                      BankName = b.Field<string>("BankName"),
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
    }
}

