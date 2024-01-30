using System;
using System.Collections.Generic;
using System.Linq;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.FAM.ServiceInterface.DataObjects.SaveRequests;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FAM.ServiceInterface;

namespace ICP.FAM.ServiceComponent
{
    public partial class FinanceService
    {

        #region 获得月结协议列表
        /// <summary>
        /// 获得月结协议列表
        /// </summary>
        /// <param name="customerName"></param>
        /// <param name="applicantName"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public List<MonthlyClosingEntryList> GetMonthlyClosingEntryLists(
            Guid[] companyIds,
            string customerName,
            string applicantName,
            DateTime? from,
            DateTime? to,
            DateTime? Profitfrom,
            DateTime? Profitto,
            int totalRecords)
        {
            List<MonthlyClosingEntryList> result = new List<MonthlyClosingEntryList>();

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetMonthlyClosingEntryLists");

                string companyIdList = companyIds.Join();

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIdList);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@ApplicantName", DbType.String, applicantName);
                db.AddInParameter(dbCommand, "@FromTime", DbType.DateTime, from);
                db.AddInParameter(dbCommand, "@ToTime", DbType.DateTime, to);
                db.AddInParameter(dbCommand, "@ProfitFromTime", DbType.DateTime, Profitfrom);
                db.AddInParameter(dbCommand, "@ProfitToTime", DbType.DateTime, Profitto);
                db.AddInParameter(dbCommand, "@TotalRecords", DbType.Int32, totalRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<MonthlyClosingEntryList> results = (from b in ds.Tables[0].AsEnumerable()
                                                         select new MonthlyClosingEntryList
                                                {
                                                    ID = b.Field<Guid>("ID"),
                                                    IsValid = b.Field<bool>("IsValid"),
                                                    CustomerName = b.Field<string>("CustomerName"),
                                                    ApplyByName = b.Field<string>("ApplyByName"),
                                                    ValidDate = b.Field<DateTime?>("ValidDate"),
                                                    Profit = b.Field<decimal>("Profit"),
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

        /// <summary>
        /// 获得月结协议列表
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <returns></returns>
        public List<MonthlyClosingEntryList> GetMonthlyClosingEntryLists(MonthlyClosingEntrySearchParameter param)
        {
            List<MonthlyClosingEntryList> result = new List<MonthlyClosingEntryList>();

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetMonthlyClosingEntryLists");

                string companyIdList = param.CompanyIDs.Join();

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIdList);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, param.CustomerName);
                db.AddInParameter(dbCommand, "@ApplicantName", DbType.String, param.ApplicantName);
                db.AddInParameter(dbCommand, "@FromTime", DbType.DateTime, param.From);
                db.AddInParameter(dbCommand, "@ToTime", DbType.DateTime, param.To);
                db.AddInParameter(dbCommand, "@ProfitFromTime", DbType.DateTime, param.ProfitFrom);
                db.AddInParameter(dbCommand, "@ProfitToTime", DbType.DateTime, param.ProfitTo);
                db.AddInParameter(dbCommand, "@IsInsured", DbType.Boolean, param.IsInsured);
                db.AddInParameter(dbCommand, "@TotalRecords", DbType.Int32, param.TotalRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<MonthlyClosingEntryList> results = (from b in ds.Tables[0].AsEnumerable()
                                                         select new MonthlyClosingEntryList
                                                         {
                                                             ID = b.Field<Guid>("ID"),
                                                             IsValid = b.Field<bool>("IsValid"),
                                                             CustomerName = b.Field<string>("CustomerName"),
                                                             ApplyByName = b.Field<string>("ApplyByName"),
                                                             ValidDate = b.Field<DateTime?>("ValidDate"),
                                                             Profit = b.Field<decimal>("Profit"),
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

        #region 获得月结协议详细信息
        /// <summary>
        /// 获得月结协议详细信息
        /// </summary>
        /// <param name="entryId"></param>
        /// <returns></returns>
        public MonthlyClosingAgreement GetMonthlyClosingEntryInfo(Guid entryId)
        {
            ArgumentHelper.AssertGuidNotEmpty(entryId, "entryId");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetMonthlyClosingEntryInfo");

                db.AddInParameter(dbCommand, "@EntryId", DbType.Guid, entryId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
          
                var result = (from b in ds.Tables[0].AsEnumerable()
                              select new MonthlyClosingAgreement
                              {
                                  ID = b.Field<Guid>("ID"),                           
                                  CustomerId = b.Field<Guid>("CustomerId"),
                                  CustomerTypes = b.Field<string>("CustomerTypes"),
                                  IsInsured = b.Field<bool>("IsInsured"),
                                  RiskRating = (RiskRatingLevel)b.Field<byte>("RiskRating"),
                                  InsuredAmount = b.Field<decimal>("InsuredAmount"),
                                  InsuredDate = b.Field<DateTime?>("InsuredDate"),
                                  UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                  CustomerName = b.Field<string>("CustomerName"),
                              }).SingleOrDefault();
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    result.Option2Company = (from b in ds.Tables[1].AsEnumerable()
                                             select new MonthlyClosingAgreement2Company
                                             {
                                                 ID = b.Field<Guid>("ID"),
                                                 MonthlyClosingEntryID = b.Field<Guid>("MonthlyClosingEntryID"),
                                                 CompanyId = b.Field<Guid>("CompanyID"),
                                                 UserID = b.Field<Guid?>("UserID"),
                                                 CalculateTermType = (CalculateTermType)b.Field<byte>("CalculateTermType"),
                                                 PaymentDate = b.Field<int>("PaymentDate"),
                                                 CreditDate = b.Field<int>("CreditDate"),
                                                 CreditAmount = b.Field<decimal?>("CreditAmount"),
                                                 Estimatedvalue = b.Field<decimal?>("Estimatedvalue"),
                                                 ApplyByID = b.Field<Guid>("ApplyByID"),
                                                 ValidDate = b.Field<DateTime?>("ValidDate"),
                                                 ApplyTime = b.Field<DateTime?>("ApplyTime"),
                                                 Remark = b.Field<string>("Remark"),
                                                 IsValid = b.Field<bool>("IsValid"),
                                                 UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                 CompanyName = b.Field<string>("CompanyName"),
                                                 ApplyByName = b.Field<string>("ApplyByName"),
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

        #region 改变月结协议的状态
        /// <summary>
        /// 改变月结协议的状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isCancel"></param>
        /// <param name="saveById"></param>
        /// <param name="updateDate"></param>
        /// <returns></returns>
        public SingleResult ChangeMonthlyClosingEntryValidity(Guid id, bool isCancel, Guid saveById, DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "Id");
            ArgumentHelper.AssertGuidNotEmpty(saveById, "saveById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.ChangeMonthlyClosingEntryValidity");

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

        #region 保存月结协议信息
        /// <summary>
        /// 保存月结协议信息
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        public SingleResult SaveMonthlyClosingEntryInfo(MonthlyClosingEntrySaveRequest saveRequest)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.CustomerId, "CustomerId");
            ArgumentHelper.AssertGuidNotEmpty(saveRequest.CreateById, "CreateById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveMonthlyClosingEntryInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, saveRequest.Id);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, saveRequest.CustomerId);
                db.AddInParameter(dbCommand, "@ApplyTimes", DbType.String, saveRequest.ApplyTimes.ToArray().Join());
                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, saveRequest.monthlyCompanyIDs.ToArray().Join());
                db.AddInParameter(dbCommand, "@CustomerTypes", DbType.String, saveRequest.CustomerTypes);
                db.AddInParameter(dbCommand, "@IsInsured", DbType.Boolean, saveRequest.IsInsured);
                db.AddInParameter(dbCommand, "@RiskRating", DbType.Int16, saveRequest.RiskRating);
                db.AddInParameter(dbCommand, "@InsuredAmount", DbType.Decimal, saveRequest.InsuredAmount);
                db.AddInParameter(dbCommand, "@InsuredDate", DbType.DateTime, saveRequest.InsuredDate);


                db.AddInParameter(dbCommand, "@SaveById", DbType.Guid, saveRequest.CreateById);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, saveRequest.UpdateDate);
                db.AddInParameter(dbCommand, "@ValidDates", DbType.String, saveRequest.ValidDates.ToArray().Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.AddInParameter(dbCommand, "@IsValids", DbType.String, saveRequest.IsValids.ToArray().Join());
                db.AddInParameter(dbCommand, "@Remarks", DbType.String, saveRequest.Remarks.ToArray().Join());
                db.AddInParameter(dbCommand, "@ApplyBys", DbType.String, saveRequest.ApplyByIDs.ToArray().Join());
                db.AddInParameter(dbCommand, "@UserIDs", DbType.String, saveRequest.UserIDs.ToArray().Join());
                db.AddInParameter(dbCommand, "@CalculateTermTypes", DbType.String, saveRequest.CalculateTermTypes.ToArray().Join());
                db.AddInParameter(dbCommand, "@PaymentDates", DbType.String, saveRequest.PaymentDates.ToArray().Join());
                db.AddInParameter(dbCommand, "@CreditDates", DbType.String, saveRequest.CreditDate.ToArray().Join());
                db.AddInParameter(dbCommand, "@CreditAmounts", DbType.String, saveRequest.CreditAmount.ToArray().Join());
                db.AddInParameter(dbCommand, "@Estimatedvalues", DbType.String, saveRequest.Estimatedvalue.ToArray().Join());
                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "CustomerID", "CustomerName", "UpdateDate" });

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

        #region 查询新增客户已存在月结协议
        public bool IsExistMonthlyClosingEntry(Guid customerid)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetSqlStringCommand("SELECT TOP  1  CustomerID FROM [fam].[MonthlyClosingEntries] WHERE CustomerID=@CustomerID");

                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerid);

                object results = db.ExecuteScalar(dbCommand);
                if (results == null)
                {
                    return false;
                }
                else 
                {
                    return true;
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

        #region 通过客户ID和公司ID得到客户的月结信息
        /// <summary>
        /// 通过客户ID和公司ID得到客户的月结信息
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="companyID">公司ID</param>
        /// <returns></returns>
        public MonthlyClosingCustomer GetMonthlyClosingCustomer(Guid customerID, Guid companyID)
        {
            ArgumentHelper.AssertGuidNotEmpty(customerID, "customerID");
            ArgumentHelper.AssertGuidNotEmpty(companyID, "companyID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCustomerForMonthlyClosingEntries");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                MonthlyClosingCustomer result = (from b in ds.Tables[0].AsEnumerable()
                                       select new MonthlyClosingCustomer
                                       {
                                           CustomerID = b.Field<Guid>("ID"),
                                           IsAgentOfCarrier = b.Field<Boolean>("IsAgentOfCarrier"),
                                           CalculateTermType = (CalculateTermType)b.Field<byte>("CalculateTermType"),
                                           CreditTerm = b.Field<int>("CreditDate"),
                                           PaymentDate = b.Field<int>("PaymentDate")
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

        #region 保存客户账单偏好设置
        public void SaveCustomerPreferencesInfo(CustomerPreferencesSaveRequest CustomerPreference)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspSaveCustomerPreferencesInfo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, CustomerPreference.Id);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, CustomerPreference.CustomerId);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, CustomerPreference.CompanyId);
                db.AddInParameter(dbCommand, "@NotifyPaymentDay", DbType.Int32, CustomerPreference.NotifyPaymentDay);
                db.AddInParameter(dbCommand, "@Tue", DbType.Decimal, CustomerPreference.Tue);
                db.AddInParameter(dbCommand, "@NotifyMail", DbType.Xml, CustomerPreference.NotifyMail);
                db.AddInParameter(dbCommand, "@InvoiceTitle", DbType.String, CustomerPreference.InvoiceTitle);
                db.AddInParameter(dbCommand, "@NotifyContact", DbType.String, CustomerPreference.NotifyContact);
                db.AddInParameter(dbCommand, "@ShipTo", DbType.String, CustomerPreference.ShipTo);
                db.AddInParameter(dbCommand, "@PdfAssembled", DbType.Byte, CustomerPreference.PdfAssembled);
                db.AddInParameter(dbCommand, "@OtherAttachments", DbType.Byte, CustomerPreference.OtherAttachments);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.AddInParameter(dbCommand, "@IsNeedPO", DbType.Boolean, CustomerPreference.IsNeedPO);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, CustomerPreference.UpdateDate);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, CustomerPreference.SaveByID);

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

        #region 获取客户账单偏好设置
        public List<CustomerPreferencesInfo> GetCustomerPreferencesInfo(Guid? id, Guid? CustomerId, Guid? CompanyId)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetCustomerPreferencesInfo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, CustomerId);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, CompanyId);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);


                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                var result = (from b in ds.Tables[0].AsEnumerable()
                              select new CustomerPreferencesInfo
                              {
                                  ID = b.Column<Guid>("ID"),
                                  CompanyID = b.Column<Guid>("CompanyId"),
                                  CompanyName = b.Column<string>("CompanyName"),
                                  CustomerID = b.Column<Guid>("CustomerId"),
                                  CustomerName = b.Column<string>("CustomerName"),
                                  InvoiceTitle = b.Column<string>("InvoiceTitle"),
                                  NotifyPaymentDay = b.Column<int>("NotifyPaymentDay"),
                                  Tue = b.Column<decimal>("Tue"),
                                  NotifyMail = b.Column<string>("NotifyMail"),
                                  NotifyContact = b.Column<string>("NotifyContact"),
                                  ShipTo = b.Column<string>("ShipTo"),
                                  PdfAssembled = b.Column<byte>("PdfAssembled"),
                                  OtherAttachments = b.Column<byte>("OtherAttachments"),
                                  IsNeedPO = b.Column<bool>("IsNeedPO"),
                                  CreateBy = b.Column<Guid>("CreateBy"),
                                  CreateByName = b.Column<string>("CreateByName"),
                                  CreateDate = b.Column<DateTime>("CreateDate"),
                                  UpdateBy = b.Column<Guid?>("UpdateBy"),
                                  UpdateByName = b.Column<string>("UpdateByName"),
                                  UpdateDate = b.Column<DateTime?>("UpdateDate"),
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
        #endregion
    }
}
