using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Server;
using ICP.ReportCenter.ServiceInterface;
using ICP.ReportCenter.ServiceInterface.DataObjects;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface;
using ICP.ReportCenter.ServiceComponent.Common;

namespace ICP.ReportCenter.ServiceComponent
{
    public class ReportCenterService : ICP.ReportCenter.ServiceInterface.IReportCenterService
    {

        #region 初始化
        string _reportUrl = string.Empty;
        string _reportUser = string.Empty;
        string _reportUserPSW = string.Empty;
        public ReportCenterService()
        {
        }

        public ReportCenterService(
           string reportServerUrl,
           string reportServerUser,
           string reportServerPSW)
        {
            _reportUrl = reportServerUrl;
            _reportUser = reportServerUser;
            _reportUserPSW = reportServerPSW;
        }

        bool IsEnglish = false;

        #endregion

        #region IReportCenterService 成员

        public ICP.ReportCenter.ServiceInterface.ReportServerInfo GetReportServerUrl()
        {
            ReportServerInfo reportServerInfo = new ReportServerInfo();
            reportServerInfo.ReportUrl = _reportUrl;
            reportServerInfo.ReportUser = _reportUser;
            reportServerInfo.ReportUserPSW = _reportUserPSW;
            return reportServerInfo;
        }

        /// <summary>
        /// 查询用户所属公司所有的部门
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="isEnglish">中英文</param>
        /// <returns></returns>
        public List<OrganizationList> GetOrganizationListForReport(Guid userID, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetUserOrganizationListForReport");

                db.AddInParameter(command, "@UserID", DbType.Guid, userID);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<OrganizationList>();
                }

                List<OrganizationList> results = (from b in ds.Tables[0].AsEnumerable()
                                                  select new OrganizationList
                                                  {
                                                      ID = b.Field<Guid>("ID"),
                                                      Code = b.Field<string>("Code"),
                                                      CShortName = b.Field<string>("CName"),
                                                      EShortName = b.Field<string>("EName"),
                                                      Type = (OrganizationType)b.Field<byte>("Type"),
                                                      ParentID = b.Field<Guid?>("ParentID"),
                                                      IsValid = b.Field<bool>("IsValid"),
                                                      FullName = b.Field<string>("FullName"),
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

        /// <summary>
        /// 查询用户所属公司所有的部门
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="isEnglish">中英文</param>
        /// <returns></returns>
        public List<OrganizationList> GetOrganizationListForCRMReport(Guid userID, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetUserOrganizationListForCRMReport");

                db.AddInParameter(command, "@UserID", DbType.Guid, userID);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<OrganizationList>();
                }

                List<OrganizationList> results = (from b in ds.Tables[0].AsEnumerable()
                                                  select new OrganizationList
                                                  {
                                                      ID = b.Field<Guid>("ID"),
                                                      Code = b.Field<string>("Code"),
                                                      CShortName = b.Field<string>("CName"),
                                                      EShortName = b.Field<string>("EName"),
                                                      Type = (OrganizationType)b.Field<byte>("Type"),
                                                      ParentID = b.Field<Guid?>("ParentID"),
                                                      IsValid = b.Field<bool>("IsValid"),
                                                      FullName = b.Field<string>("FullName"),
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


        public List<ReportOperationType> GetReportOperationType(bool getContainerTypeOnly)
        {

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetDWReportOperationType");

                db.AddInParameter(dbCommand, "@GetContainerTypeOnly", DbType.Boolean, getContainerTypeOnly);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<ReportOperationType> results = (from b in ds.Tables[0].AsEnumerable()
                                                     select new ReportOperationType
                                                    {
                                                        ID = b.Field<Guid>("ID"),
                                                        ParentID = b.Field<Guid?>("ParentID"),
                                                        CName = b.Field<string>("CName"),
                                                        EName = b.Field<string>("EName"),
                                                        HasContainer = b.Field<bool>("HasContainer"),
                                                        HasShipLine = b.Field<bool>("HasShipLine"),
                                                        Value = b.Field<string>("Value")
                                                    }).ToList();


                List<ReportOperationType> parents = results.FindAll(a => a.ParentID == null);
                foreach (var item in parents)
                {
                    List<ReportOperationType> childs = results.FindAll(a => a.ParentID == item.ID);
                    item.ValueList = new List<string>();
                    if (!string.IsNullOrEmpty(item.Value))
                    {
                        item.ValueList.Add(item.Value);
                    }
                    foreach (var c in childs)
                    {
                        item.ValueList.Add(c.Value);
                    }
                }

                return results;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        public List<ReportGroupType> GetReportGroupType()
        {

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetDWReportGroupType");

                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<ReportGroupType> results = (from b in ds.Tables[0].AsEnumerable()
                                                 select new ReportGroupType
                                                {
                                                    ID = b.Field<Guid>("ID"),
                                                    CName = b.Field<string>("CName"),
                                                    EName = b.Field<string>("EName"),
                                                    UnContainsTypes = (from v in (b.Field<string>("UnContainsTypes")).Split
                                                                 (
                                                                 new string[] { ICP.Framework.CommonLibrary.Common.GlobalConstants.DividedSymbol }
                                                                 , StringSplitOptions.None
                                                                 )
                                                                       where v != string.Empty
                                                                       select v).ToList()
                                                }).ToList();


                return results;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        public void CheckLedgersForBill(DateTime @fromDate, DateTime @toDate, string @companyIDS)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspCheckLedgersForBill");
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, @fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, @toDate);
                db.AddInParameter(dbCommand, "@CompanyIDS", DbType.String, @companyIDS);
                db.ExecuteDataSet(dbCommand);
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
        /// 科目余额明细表
        /// </summary>
        /// <param name="glID">科目ID</param>
        /// <param name="fromDate">开始日期</param>
        /// <param name="toDate">结束日期</param>
        /// <returns></returns>
        public List<GLDetailData> GetGLDetailBalanceList(Guid glID,
                                                      DateTime fromDate,
                                                      DateTime toDate)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetGLDetailBalanceList");

                db.AddInParameter(dbCommand, "@GlID", DbType.Guid, glID);
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<GLDetailData> list = (from d in ds.Tables[0].AsEnumerable()
                                           select new GLDetailData
                                           {
                                               ID = d.Field<Guid?>("ID"),
                                               GLID = d.Field<Guid?>("GLID"),
                                               Date = d.Field<DateTime?>("Date"),
                                               VoucherNo = d.Field<String>("VoucherNo"),
                                               Remark = d.Field<String>("Remark"),
                                               Debit = d.Field<Decimal?>("Debit"),
                                               Credit = d.Field<Decimal?>("Credit"),
                                               Direction = (GLCodeProperty)d.Field<Byte>("Direction"),
                                               Balance = d.Field<Decimal?>("Balance")
                                           }).ToList();
                return list;

            }
            catch (Exception ex) { throw ex; }
        }

        #region 外币金额式科目余额表
        /// <summary>
        /// 外币金额式科目余额表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="fromGLCode">开始科目</param>
        /// <param name="toGLCode">结束科目</param>
        /// <param name="glCodeType">科目类型(1资产;2负债;3权益;4成本;5损益)</param>
        /// <param name="fromGLLevel">开始级别</param>
        /// <param name="toGLLevel">结束级别</param>
        /// <param name="showEndLevel">只显示未级科目</param>
        /// <param name="fromDate">开始日期</param>
        /// <param name="toDate">结束日期</param>
        /// <param name="showCumulation">本期无发生额，累计有发生显示</param>
        /// <returns></returns>
        public List<FCGLBalanceData> GetFCGLBalanceDataList(Guid[] companyIDs,
                                                        int? fromGLCode,
                                                        int? toGLCode,
                                                        GLCodeType glCodeType,
                                                        int fromGLLevel,
                                                        int toGLLevel,
                                                        bool showEndLevel,
                                                        DateTime fromDate,
                                                        DateTime toDate,
                                                        bool showCumulation)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetFCGLBalanceList");

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@FromGLCode", DbType.Int32, fromGLCode);
                db.AddInParameter(dbCommand, "@ToGLCode", DbType.Int32, toGLCode);
                db.AddInParameter(dbCommand, "@GlCodeType", DbType.Byte, glCodeType);
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@ShowCumulation", DbType.Boolean, showCumulation);
                db.AddInParameter(dbCommand, "@IsLeaf", DbType.Boolean, showEndLevel);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                //科目详细数据
                List<FCGLBalanceData> list = (from d in ds.Tables[0].AsEnumerable()
                                              select new FCGLBalanceData
                                              {
                                                  GLID = d.Field<Guid?>("GLID"),
                                                  GLCode = d.Field<String>("GLCode"),
                                                  GLName = d.Field<String>("GLName"),
                                                  ParentID = d.Field<Guid?>("ParentID"),
                                                  GLCodeType = (GLCodeType)d.Field<Byte>("GLCodeType"),
                                                  GLCodeTypeName = d.Field<String>("GLCodeTypeName"),
                                                  LevelCode = d.Field<int>("LevelCode"),
                                                  BeginningDir = EnumHelper.GetDescription<GLCodeProperty>((GLCodeProperty)d.Field<Byte>("BeginningDir"), LocalData.IsEnglish),
                                                  BeginningOrgAmt = d.Field<Decimal?>("BeginningOrgAmt"),
                                                  BeginningBalance = d.Field<Decimal?>("BeginningBalance"),
                                                  Debit = d.Field<Decimal?>("Debit"),
                                                  Credit = d.Field<Decimal?>("Credit"),
                                                  DebitOrgAmt = d.Field<Decimal?>("DebitOrgAmt"),
                                                  CreditOrgAmt = d.Field<Decimal?>("CreditOrgAmt"),
                                                  TermEndDir = EnumHelper.GetDescription<GLCodeProperty>((GLCodeProperty)d.Field<Byte>("TermEndDir"), LocalData.IsEnglish),
                                                  TermEndOrgAmt = d.Field<Decimal?>("TermEndOrgAmt"),
                                                  TermEndBalance = d.Field<Decimal?>("TermEndBalance"),
                                              }).ToList();

                return list;
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region 外币式科目明细表
        /// <summary>
        /// 外币式科目明细表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="glID">科目ID</param>
        /// <param name="fromDate">开始日期</param>
        /// <param name="toDate">结束日期</param>
        /// <returns></returns>
        public List<FCGLDetailData> GetFCGLDetailBalanceList(Guid[] companyIDs,
                                                      Guid glID,
                                                      DateTime fromDate,
                                                      DateTime toDate)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetFCGLDetailBalanceList");

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@GlID", DbType.Guid, glID);
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<FCGLDetailData> list = (from d in ds.Tables[0].AsEnumerable()
                                             select new FCGLDetailData
                                             {
                                                 ID = d.Field<Guid?>("ID"),
                                                 GLID = d.Field<Guid?>("GLID"),
                                                 Date = d.Field<DateTime?>("Date"),
                                                 VoucherNo = d.Field<String>("VoucherNo"),
                                                 Remark = d.Field<String>("Remark"),
                                                 FC_Rate = d.Field<String>("FC_Rate"),
                                                 DebitOrgAmt = d.Field<Decimal?>("DebitOrgAmt"),
                                                 Debit = d.Field<Decimal?>("Debit"),
                                                 CreditOrgAmt = d.Field<Decimal?>("CreditOrgAmt"),
                                                 Credit = d.Field<Decimal?>("Credit"),
                                                 Direction = (GLCodeProperty)d.Field<Byte>("Direction"),
                                                 BalanceOrgAmt = d.Field<Decimal?>("BalanceOrgAmt"),
                                                 BalanceRate = d.Field<Decimal?>("BalanceRate"),
                                                 Balance = d.Field<Decimal?>("Balance")
                                             }).ToList();

                return list;

            }
            catch (Exception ex) { throw ex; }
        }



        #endregion

        #region 外币式客户余额表
        /// <summary>
        /// 外币式客户余额表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="customerIDs">客户ID集合</param>
        /// <param name="fromDate">开始日期</param>
        /// <param name="toDate">结束日期</param>
        /// <param name="propertys">余额方向(1借方,2贷方)</param>
        /// <returns></returns>
        public List<CustomerGLBalance> GetCustomerFCGLBalanceList(Guid[] companyIDs,
                                                   Guid[] customerIDs,
                                                   Guid[] glIDs,
                                                   DateTime fromDate,
                                                   DateTime toDate,
                                                   GLCodeProperty[] propertys)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetCustomerFCGLBalanceList");

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@CustomerIDs", DbType.String, customerIDs.Join());
                db.AddInParameter(dbCommand, "@GLIDs", DbType.String, glIDs.Join());
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CustomerGLBalance> list = (from d in ds.Tables[0].AsEnumerable()
                                                select new CustomerGLBalance
                                                {
                                                    GLID = d.Field<Guid?>("GLID"),
                                                    GLCode = d.Field<String>("GLCode"),
                                                    GLName = d.Field<String>("GLName"),
                                                    CustomerID = d.Field<Guid?>("CustomerID"),
                                                    CustomerCode = d.Field<String>("CustomerCode"),
                                                    CustomerName = d.Field<String>("CustomerName"),
                                                    BeginningDirection = (GLCodeProperty)d.Field<Byte>("BeginningDirection"),
                                                    BeginningOrgAmt = d.Field<Decimal?>("BeginningOrgAmt"),
                                                    BeginningBalance = d.Field<Decimal?>("BeginningBalance"),
                                                    PeriodCredit = d.Field<Decimal?>("PeriodCredit"),
                                                    PeriodDebit = d.Field<Decimal?>("PeriodDebit"),
                                                    PeriodCreditOrgAmt = d.Field<Decimal?>("PeriodCreditOrgAmt"),
                                                    PeriodDebitOrgAmt = d.Field<Decimal?>("PeriodDebitOrgAmt"),
                                                    PeriodEndDirection = (GLCodeProperty)d.Field<Byte>("PeriodEndDirection"),
                                                    PeriodEndOrgAmt = d.Field<Decimal?>("PeriodEndOrgAmt"),
                                                    PeriodEndBalance = d.Field<Decimal?>("PeriodEndBalance")
                                                }).ToList();
                return list;
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region 外币式客户科目明细表
        /// <summary>
        /// 外币式客户科目明细表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="customerIDs">客户ID集合</param>
        /// <param name="glIDs">科目ID集合</param>
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <returns></returns>
        public List<CustomerGLDetail> GetCustomerFCGLDetailList(Guid[] companyIDs,
                                                 Guid[] customerIDs,
                                                 Guid[] glIDs,
                                                 DateTime fromDate,
                                                 DateTime toDate)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetCustomerFCGLDetailList");

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@CustomerIDs", DbType.String, customerIDs.Join());
                db.AddInParameter(dbCommand, "@GLIDs", DbType.String, glIDs.Join());
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CustomerGLDetail> list = (from d in ds.Tables[0].AsEnumerable()
                                               select new CustomerGLDetail
                                               {
                                                   ID = d.Field<Guid?>("ID"),
                                                   GLID = d.Field<Guid?>("GLID"),
                                                   GLCode = d.Field<String>("GLCode"),
                                                   GLName = d.Field<String>("GLName"),
                                                   Date = d.Field<DateTime?>("Date"),
                                                   VoucherNo = d.Field<String>("VoucherNo"),
                                                   CustomerID = d.Field<Guid?>("CustomerID"),
                                                   CustomerCode = d.Field<String>("CustomerCode"),
                                                   CustomerName = d.Field<String>("CustomerName"),
                                                   Remark = d.Field<String>("Remark"),
                                                   FC_Rate = d.Field<String>("FC_Rate"),
                                                   DebitOrgAmt = d.Field<Decimal?>("Debit"),
                                                   Debit = d.Field<Decimal?>("Debit"),
                                                   CreditOrgAmt = d.Field<Decimal?>("CreditOrgAmt"),
                                                   Credit = d.Field<Decimal?>("Credit"),
                                                   PeriodEndDirection = (GLCodeProperty)d.Field<Byte>("PeriodEndDirection"),
                                                   PeriodEndOrgAmt = d.Field<Decimal?>("PeriodEndOrgAmt"),
                                                   PeriodEndBalance = d.Field<Decimal?>("PeriodEndBalance")
                                               }).ToList();

                return list;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region 外币式个人科目余额表
        /// <summary>
        /// 外币式个人科目余额表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="departmentIDs">部门ID集合</param>
        /// <param name="personalIDs">个人ID集合</param>
        /// <param name="glIds">科目ID集合</param>
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <param name="property">统计方向(0全部;1借方;2贷方)</param>
        /// <returns></returns>
        public List<PersonalGLBalance> GetPersonalFCGLBalanceList(Guid[] companyIDs,
                                                   Guid[] departmentIDs,
                                                   Guid[] personalIDs,
                                                   Guid[] glIds,
                                                   DateTime fromDate,
                                                   DateTime toDate,
                                                   GLCodeProperty property)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetPersonalFCGLBalanceList");

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@DepartmentIDs", DbType.String, departmentIDs.Join());
                db.AddInParameter(dbCommand, "@PersonalIDs", DbType.String, personalIDs.Join());
                db.AddInParameter(dbCommand, "@GLIDs", DbType.String, glIds.Join());
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<PersonalGLBalance> list = (from d in ds.Tables[0].AsEnumerable()
                                                select new PersonalGLBalance
                                                {
                                                    GLID = d.Field<Guid?>("GLID"),
                                                    GLCode = d.Field<String>("GLCode"),
                                                    GLName = d.Field<String>("GLName"),
                                                    DepartmentID = d.Field<Guid?>("DepartmentID"),
                                                    DepartmentName = d.Field<String>("DepartmentName"),
                                                    PersonalID = d.Field<Guid?>("PersonalID"),
                                                    PersonalName = d.Field<String>("PersonalName"),
                                                    BeginningDirection = d.Field<Byte?>("BeginningDirection") == null ? null : (GLCodeProperty?)d.Field<Byte?>("BeginningDirection"),
                                                    BeginningOrgAmt = d.Field<Decimal?>("BeginningOrgAmt"),
                                                    BeginningBalance = d.Field<Decimal?>("BeginningBalance"),
                                                    PeriodDebitOrgAmt = d.Field<Decimal?>("PeriodDebitOrgAmt"),
                                                    PeriodDebit = d.Field<Decimal?>("PeriodDebit"),
                                                    PeriodCreditOrgAmt = d.Field<Decimal?>("PeriodCreditOrgAmt"),
                                                    PeriodCredit = d.Field<Decimal?>("PeriodCredit"),
                                                    PeriodEndDirection = (GLCodeProperty?)d.Field<Byte?>("PeriodEndDirection") == null ? null : (GLCodeProperty?)d.Field<Byte?>("PeriodEndDirection"),
                                                    PeriodEndOrgAmt = d.Field<Decimal?>("PeriodEndOrgAmt"),
                                                    PeriodEndBalance = d.Field<Decimal?>("PeriodEndBalance")
                                                }).ToList();
                return list;
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region 外币式个人科目明细表
        /// <summary>
        /// 外币式个人科目明细表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="departmentIDs">部门ID集合</param>
        /// <param name="personalIDs">个人ID集合</param>
        /// <param name="glIds">科目ID集合</param>
        /// <param name="fromDate">开始日期</param>
        /// <param name="toDate">结束日期</param>
        /// <param name="noAccounting">包含未记账凭证</param>
        /// <param name="orderByDebit">借方在前,贷方在后</param>
        /// <returns></returns>
        public List<PersonalGLDetail> GetPersonalFCGLDetailList(Guid[] companyIDs,
                                                 Guid[] departmentIDs,
                                                 Guid[] personalIDs,
                                                 Guid[] glIds,
                                                 DateTime fromDate,
                                                 DateTime toDate,
                                                 bool orderByDebit)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetPersonalFCGLDetailList");

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@DepartmentIDs", DbType.String, departmentIDs.Join());
                db.AddInParameter(dbCommand, "@PersonalIDs", DbType.String, personalIDs.Join());
                db.AddInParameter(dbCommand, "@GLIDs", DbType.String, glIds.Join());
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                //db.AddInParameter(dbCommand, "@OrderByDebit", DbType.Boolean, orderByDebit);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<PersonalGLDetail> list = (from d in ds.Tables[0].AsEnumerable()
                                               select new PersonalGLDetail
                                               {
                                                   ID = d.Field<Guid?>("ID"),
                                                   GLID = d.Field<Guid?>("GLID"),
                                                   GLCode = d.Field<String>("GLCode"),
                                                   GLName = d.Field<String>("GLName"),
                                                   Date = d.Field<DateTime?>("Date"),
                                                   VoucherNo = d.Field<String>("VoucherNo"),
                                                   DepartmentID = d.Field<Guid?>("DepartmentID"),
                                                   DepartmentName = d.Field<String>("DepartmentName"),
                                                   PersonalID = d.Field<Guid?>("PersonalID"),
                                                   PersonalName = d.Field<String>("PersonalName"),
                                                   Remark = d.Field<String>("Remark"),
                                                   FC_Rate = d.Field<String>("FC_Rate"),
                                                   DebitOrgAmt = d.Field<Decimal?>("DebitOrgAmt"),
                                                   Debit = d.Field<Decimal?>("Debit"),
                                                   CreditOrgAmt = d.Field<Decimal?>("CreditOrgAmt"),
                                                   Credit = d.Field<Decimal?>("Credit"),
                                                   PeriodEndDirection = (GLCodeProperty)d.Field<Byte>("PeriodEndDirection"),
                                                   PeriodEndOrgAmt = d.Field<Decimal?>("PeriodEndOrgAmt"),
                                                   PeriodEndBalance = d.Field<Decimal?>("PeriodEndBalance")
                                               }).ToList();

                return list;
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region 资产负债表（口岸）
        /// <summary>
        /// 资产负债表（口岸）
        /// </summary>
        /// <param name="fromDate">开始日期</param>
        /// <param name="toDate">结束日期</param>        
        /// <param name="queryDate">所属期</param>        
        /// <returns></returns>
        public List<BalanceSheet> GetCompanyBalanceSheetDetail(Guid[] companyIDs, DateTime fromDate, DateTime toDate)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetCompanyBalanceSheetlDetail");

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
              
                List<BalanceSheet> list = (from d in ds.Tables[0].AsEnumerable()
                                           select new BalanceSheet
                                             {
                                                    BeginAmount1=d.Field<Decimal?>("BeginAmount1"),
                                                    Amount1=d.Field<Decimal?>("Amount1"),
                                                    BeginAmount6=d.Field<Decimal?>("BeginAmount6"),
                                                    Amount6=d.Field<Decimal?>("Amount6"),
                                                    BeginAmount7=d.Field<Decimal?>("BeginAmount7"),
                                                    Amount7=d.Field<Decimal?>("Amount7"),
                                                    BeginAmount8=d.Field<Decimal?>("BeginAmount8"),
                                                    Amount8=d.Field<Decimal?>("Amount8"),
                                                    BeginAmount11=d.Field<Decimal?>("BeginAmount11"),
                                                    Amount11=d.Field<Decimal?>("Amount11"),
                                                    BeginAmount31=d.Field<Decimal?>("BeginAmount31"),
                                                    Amount31=d.Field<Decimal?>("Amount31"),
                                                    BeginAmount39=d.Field<Decimal?>("BeginAmount39"),
                                                    Amount39=d.Field<Decimal?>("Amount39"),
                                                    BeginAmount40=d.Field<Decimal?>("BeginAmount40"),
                                                    Amount40=d.Field<Decimal?>("Amount40"),
                                                    BeginAmount41 = d.Field<Decimal?>("BeginAmount41"),
                                                    Amount41 = d.Field<Decimal?>("Amount41"),
                                                    BeginAmount50=d.Field<Decimal?>("BeginAmount50"),
                                                    Amount50=d.Field<Decimal?>("Amount50"),
                                                    BeginAmount51=d.Field<Decimal?>("BeginAmount51"),
                                                    Amount51=d.Field<Decimal?>("Amount51"),
                                                    BeginAmount60 = d.Field<Decimal?>("BeginAmount60"),
                                                    Amount60 = d.Field<Decimal?>("Amount60"),
                                                    BeginAmount67=d.Field<Decimal?>("BeginAmount67"),
                                                    Amount67=d.Field<Decimal?>("Amount67"),
                                                    BeginAmount70=d.Field<Decimal?>("BeginAmount70"),
                                                    Amount70=d.Field<Decimal?>("Amount70"),
                                                    BeginAmount71=d.Field<Decimal?>("BeginAmount71"),
                                                    Amount71=d.Field<Decimal?>("Amount71"),
                                                    BeginAmount72=d.Field<Decimal?>("BeginAmount72"),
                                                    Amount72=d.Field<Decimal?>("Amount72"),
                                                    BeginAmount73=d.Field<Decimal?>("BeginAmount73"),
                                                    Amount73=d.Field<Decimal?>("Amount73"),
                                                    BeginAmount74=d.Field<Decimal?>("BeginAmount74"),
                                                    Amount74=d.Field<Decimal?>("Amount74"),
                                                    BeginAmount76=d.Field<Decimal?>("BeginAmount76"),
                                                    Amount76 =d.Field<Decimal?>("Amount76"),
                                                    BeginAmount78=d.Field<Decimal?>("BeginAmount78"),
                                                    Amount78=d.Field<Decimal?>("Amount78"),
                                                    BeginAmount100=d.Field<Decimal?>("BeginAmount100"),
                                                    Amount100=d.Field<Decimal?>("Amount100"),
                                                    BeginAmount111 = d.Field<Decimal?>("BeginAmount111"),
                                                    Amount111 = d.Field<Decimal?>("Amount111"),
                                                    BeginAmount115=d.Field<Decimal?>("BeginAmount115"),
                                                    Amount115=d.Field<Decimal?>("Amount115"),
                                                    BeginAmount121=d.Field<Decimal?>("BeginAmount121"),
                                                    Amount121=d.Field<Decimal?>("Amount121"),
                                                    BeginAmount122 = d.Field<Decimal?>("BeginAmount122"),
                                                    Amount122 = d.Field<Decimal?>("Amount122"),
                                                    BeginAmount135=d.Field<Decimal?>("BeginAmount135"),
                                                    Amount135 = d.Field<Decimal?>("Amount135"),

                                                    BeginAmount2 = d.Field<Decimal?>("BeginAmount2"),
                                                    Amount2 = d.Field<Decimal?>("Amount2"),
                                                    BeginAmount3 = d.Field<Decimal?>("BeginAmount3"),
                                                    Amount3 = d.Field<Decimal?>("Amount3"),
                                                    BeginAmount4 = d.Field<Decimal?>("BeginAmount4"),
                                                    Amount4 = d.Field<Decimal?>("Amount4"),
                                                    BeginAmount5 = d.Field<Decimal?>("BeginAmount5"),
                                                    Amount5 = d.Field<Decimal?>("Amount5"),
                                                    BeginAmount9 = d.Field<Decimal?>("BeginAmount9"),
                                                    Amount9 = d.Field<Decimal?>("Amount9"),
                                                    BeginAmount10 = d.Field<Decimal?>("BeginAmount10"),
                                                    Amount10 = d.Field<Decimal?>("Amount10"),
                                                    BeginAmount21 = d.Field<Decimal?>("BeginAmount21"),
                                                    Amount21 = d.Field<Decimal?>("Amount21"),
                                                    BeginAmount24 = d.Field<Decimal?>("BeginAmount24"),
                                                    Amount24 = d.Field<Decimal?>("Amount24"),
                                                    BeginAmount32 = d.Field<Decimal?>("BeginAmount32"),
                                                    Amount32 = d.Field<Decimal?>("Amount32"),
                                                    BeginAmount34 = d.Field<Decimal?>("BeginAmount34"),
                                                    Amount34 = d.Field<Decimal?>("Amount34"),
                                                    BeginAmount38 = d.Field<Decimal?>("BeginAmount38"),
                                                    Amount38 = d.Field<Decimal?>("Amount38"),
                                                    BeginAmount44 = d.Field<Decimal?>("BeginAmount44"),
                                                    Amount44 = d.Field<Decimal?>("Amount44"),
                                                    BeginAmount45 = d.Field<Decimal?>("BeginAmount45"),
                                                    Amount45 = d.Field<Decimal?>("Amount45"),
                                                    BeginAmount46 = d.Field<Decimal?>("BeginAmount46"),
                                                    Amount46 = d.Field<Decimal?>("Amount46"),
                                                    BeginAmount52 = d.Field<Decimal?>("BeginAmount52"),
                                                    Amount52 = d.Field<Decimal?>("Amount52"),
                                                    BeginAmount53 = d.Field<Decimal?>("BeginAmount53"),
                                                    Amount53 = d.Field<Decimal?>("Amount53"),
                                                    BeginAmount68 = d.Field<Decimal?>("BeginAmount68"),
                                                    Amount68 = d.Field<Decimal?>("Amount68"),
                                                    BeginAmount69 = d.Field<Decimal?>("BeginAmount69"),
                                                    Amount69 = d.Field<Decimal?>("Amount69"),
                                                    BeginAmount75 = d.Field<Decimal?>("BeginAmount75"),
                                                    Amount75 = d.Field<Decimal?>("Amount75"),
                                                    BeginAmount77 = d.Field<Decimal?>("BeginAmount77"),
                                                    Amount77 = d.Field<Decimal?>("Amount77"),
                                                    BeginAmount79 = d.Field<Decimal?>("BeginAmount79"),
                                                    Amount79 = d.Field<Decimal?>("Amount79"),
                                                    BeginAmount80 = d.Field<Decimal?>("BeginAmount80"),
                                                    Amount80 = d.Field<Decimal?>("Amount80"),
                                                    BeginAmount101 = d.Field<Decimal?>("BeginAmount101"),
                                                    Amount101 = d.Field<Decimal?>("Amount101"),
                                                    BeginAmount102 = d.Field<Decimal?>("BeginAmount102"),
                                                    Amount102 = d.Field<Decimal?>("Amount102"),
                                                    BeginAmount103 = d.Field<Decimal?>("BeginAmount103"),
                                                    Amount103 = d.Field<Decimal?>("Amount103"),
                                                    BeginAmount106 = d.Field<Decimal?>("BeginAmount106"),
                                                    Amount106 = d.Field<Decimal?>("Amount106"),
                                                    BeginAmount108 = d.Field<Decimal?>("BeginAmount108"),
                                                    Amount108 = d.Field<Decimal?>("Amount108"),
                                                    BeginAmount110 = d.Field<Decimal?>("BeginAmount110"),
                                                    Amount110 = d.Field<Decimal?>("Amount110"),
                                                    BeginAmount116 = d.Field<Decimal?>("BeginAmount116"),
                                                    Amount116 = d.Field<Decimal?>("Amount116"),
                                                    BeginAmount117 = d.Field<Decimal?>("BeginAmount117"),
                                                    Amount117 = d.Field<Decimal?>("Amount117"),
                                                    BeginAmount118 = d.Field<Decimal?>("BeginAmount118"),
                                                    Amount118 = d.Field<Decimal?>("Amount118"),
                                                    BeginAmount119 = d.Field<Decimal?>("BeginAmount119"),
                                                    Amount119 = d.Field<Decimal?>("Amount119"),
                                                    BeginAmount120 = d.Field<Decimal?>("BeginAmount120"),
                                                    Amount120 = d.Field<Decimal?>("Amount120"),
                                             }).ToList();        
                return list;

            }
            catch (Exception ex) { throw ex; }
        }



        #endregion

        #region 资产负债表汇总表
        /// <summary>
        /// 资产负债表（汇总表）
        /// </summary>       
        /// <param name="fromDate">开始日期</param>
        /// <param name="toDate">结束日期</param>     
        /// <returns></returns>
        public List<CompanyBalanceSheet> GetCompanyBalanceSheet(DateTime fromDate, DateTime toDate)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetCompanyBalanceSheetl");

                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<CompanyBalanceSheet> list = (from d in ds.Tables[0].AsEnumerable()
                                                  select new CompanyBalanceSheet
                                                  {
                                                      GLName = d.Field<String>("GLName"),
                                                      GLType=d.Field<int>("GLType"),
                                                      YDAmount = d.Field<Decimal?>("YDAmount"),
                                                      SZAmount = d.Field<Decimal?>("SZAmount"),
                                                      GZAmount = d.Field<Decimal?>("GZAmount"),
                                                      XMAmount = d.Field<Decimal?>("XMAmount"),
                                                      SHAmount = d.Field<Decimal?>("SHAmount"),
                                                      NBAmount = d.Field<Decimal?>("NBAmount"),
                                                      CQAmount = d.Field<Decimal?>("CQAmount"),
                                                      TJAmount = d.Field<Decimal?>("TJAmount"),
                                                      QDAmount = d.Field<Decimal?>("QDAmount"),
                                                      DLAmount = d.Field<Decimal?>("DLAmount"),
                                                  }).ToList();
 
                return list;

            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 资产负债表（汇总表）
        /// </summary>       
        /// <param name="fromDate">开始日期</param>
        /// <param name="toDate">结束日期</param>     
        /// <returns></returns>
        public List<CompanyBalanceSheetAll> GetCompanyBalanceSheetAll(DateTime fromDate, DateTime toDate, List<Guid> companyIDs)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetCompanyBalanceSheetTotal");

                db.AddInParameter(dbCommand, "@StartDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@CompanyIDS", DbType.String, companyIDs.ToArray().Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<CompanyBalanceSheetAll> list = (from d in ds.Tables[0].AsEnumerable()
                                                     select new CompanyBalanceSheetAll
                                                  {
                                                      BalanceAmount = d.Field<Decimal?>("BalanceAmount"),
                                                      CompanyId = d.Field<Guid>("CompanyId").ToString(),
                                                      CompanyName = d.Field<string>("CompanyName"),
                                                      CurrencyCode = d.Field<string>("CurrencyCode"),
                                                      CurrencyID = d.Field<Guid>("CurrencyID").ToString(),
                                                      DistrictName = d.Field<string>("DistrictName"),
                                                      GroupCname = d.Field<string>("GroupCname"),
                                                      GroupEname = d.Field<string>("GroupEname"),
                                                      GroupID = d.Field<Guid>("GroupID").ToString(),
                                                      GroupParentID = d.Field<Guid>("GroupParentID").ToString(),
                                                      IndexNo = d.Field<int>("IndexNo"),
                                                      ParentCname = d.Field<string>("ParentCname"),
                                                      ParentEname = d.Field<string>("ParentEname"),
                                                      ShortCode = d.Field<string>("ShortCode"),
                                                      TopCname = d.Field<string>("TopCname"),
                                                      TopEname = d.Field<string>("TopEname"),
                                                  }).ToList();

                return list;

            }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region 费用分析表
        /// <summary>
        /// 费用分析表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param> 
        /// <param name="expenseType">费用类型(1.管理费用,2.财务费用)</param>         
        /// <param name="fromDate">开始日期</param>
        /// <param name="toDate">结束日期</param>
        /// <returns></returns>
        public List<ExpenseAnalysisSheet> GetExpenseAnalysisSheetDetail(Guid[] companyIDs,
                                                        ExpenseType expenseType,
                                                        DateTime fromDate,
                                                        DateTime toDate, 
                                                        bool isCheckGL)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetExpenseAnalysisSheetDetail");

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@ExpenseType", DbType.Int32, (Int32)expenseType);
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<ExpenseAnalysisSheet> list = (from d in ds.Tables[0].AsEnumerable()
                                                   select new ExpenseAnalysisSheet
                                              {
                                                  GLID = d.Field<Guid?>("GLID"),
                                                  GLCode = d.Field<String>("GLCode"),
                                                  GLName = d.Field<String>("GLName"),
                                                  LastMonth = d.Field<Decimal?>("LastMonth"),
                                                  ThisMonth = d.Field<Decimal?>("ThisMonth"),
                                                  MonthsOf = d.Field<Decimal?>("MonthsOf"),
                                                  LevelCode = d.Field<int>("LevelCode"),
                                                  MonthsBudget=d.Field<Decimal?>("MonthsBudget"),
                                                  MonthsIncrease = d.Field<Decimal?>("MonthsIncrease"),
                                                  MonthsScale = d.Field<Decimal?>("MonthsScale"),
                                              }).ToList();

                if (isCheckGL)
                {
                    list = (from d in list where d.LevelCode == 2 select d).ToList();
                }

                return list;

            }
            catch (Exception ex) { throw ex; }
        }



        #endregion

        #region 费用分析表汇总表
        /// <summary>
        /// 费用分析表汇总表
        /// </summary>
        /// <param name="expenseType">费用类型(1.管理费用,2.财务费用)</param>         
        /// <param name="fromDate">开始日期</param>
        /// <param name="toDate">结束日期</param>
        /// <returns></returns>
        public List<CompanyExpenseAnalysisSheet> GetExpenseAnalysisSheet(ExpenseType expenseType,
                                                    ExpenseHappenType happenType,
                                                    DateTime fromDate, 
                                                    DateTime toDate, 
                                                    bool isCheckGL)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetExpenseAnalysisSheet");

                db.AddInParameter(dbCommand, "@ExpenseType", DbType.Int32, (Int32)expenseType);
                db.AddInParameter(dbCommand, "@HappenType", DbType.Int32, (Int32)happenType);
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<CompanyExpenseAnalysisSheet> data=IListDataSet.DataSetToIList<CompanyExpenseAnalysisSheet>(ds, 0).ToList();

                List<CompanyExpenseAnalysisSheet> list = new List<CompanyExpenseAnalysisSheet>();
                foreach (CompanyExpenseAnalysisSheet item in data)
                {
                    if (isCheckGL && item.LevelCode != 2)
                    {
                        continue;
                    }
                    if (item.LevelCode > 2)
                    {
                        item.GLCode = GetSPACE(item.LevelCode) + item.GLCode;
                        item.GLName = GetSPACE(item.LevelCode) + item.GLName;
                    }

                    list.Add(item);
                }
               
                return list;

            }
            catch (Exception ex) { throw ex; }
        }
        private string GetSPACE(int count)
        {
            string str = string.Empty;
            for (int n = 2; n <= count;n++ )
            {
                str += " ";
            }
            return str;
        }



        #endregion

        #region 根据航线统计箱量
        /// <summary>
        /// 根据航线统计箱量
        /// </summary>
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <param name="customerids">客户集合</param>
        /// <param name="shipperlines">航线集合</param>
        /// <returns></returns>
        public List<ReportContainerVolumeForShipperLine> GetContainerVolumeForShipperLine(DateTime fromDate, DateTime toDate, Guid[] customerids, Guid[] shipperlines, Guid[] salesids, Guid[] companyids)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.GetContainerVolumeForShipperLineReport");

                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@customerids", DbType.String, customerids.Join());
                db.AddInParameter(dbCommand, "@SalesIDs", DbType.String, salesids.Join());
                db.AddInParameter(dbCommand, "@CompanyIDS", DbType.String, companyids.Join());

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<string> colNames = new List<string>();

                foreach (DataColumn col in ds.Tables[0].Columns)
                {
                    colNames.Add(col.ColumnName);
                }

                List<ReportContainerVolumeForShipperLine> list = (from d in ds.Tables[0].AsEnumerable()
                                                                  select new ReportContainerVolumeForShipperLine
                                               {
                                                   CustomerName = d.Field<String>("CustomerName"),
                                                   CompanyName = d.Field<String>("CompanyName"),
                                                   POL = d.Field<String>("POL"),
                                                   POD = d.Field<String>("POD"),
                                                   PODVolume = d.IsNull("PODVolume") ? 0 : Convert.ToInt32(d.Field<decimal>("PODVolume")),
                                                   CSAVVolunm = colNames.Contains("CSAV") ? (d.IsNull("CSAV") ? 0 : Convert.ToInt32(d.Field<decimal>("CSAV"))) : 0,
                                                   CSCLVolumn = colNames.Contains("CSCL") ? (d.IsNull("CSCL") ? 0 : Convert.ToInt32(d.Field<decimal>("CSCL"))) : 0,
                                                   ZIMVolunm = colNames.Contains("ZIM") ? (d.IsNull("ZIM") ? 0 : Convert.ToInt32(d.Field<decimal>("ZIM"))) : 0,
                                                   COSCOVolunm = colNames.Contains("COSCO") ? (d.IsNull("COSCO") ? 0 : Convert.ToInt32(d.Field<decimal>("COSCO"))) : 0,
                                                   PILVolunm = colNames.Contains("PIL") ? (d.IsNull("PIL") ? 0 : Convert.ToInt32(d.Field<decimal>("PIL"))) : 0,
                                                   OthersVolunm = colNames.Contains("Other") ? (d.IsNull("Other") ? 0 : Convert.ToInt32(d.Field<decimal>("Other"))) : 0,
                                                   SalesName = d.Field<string>("Ename")
                                               }).ToList();

                return list;

            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region 凭证备份
        /// <summary>
        /// 备份凭证
        /// </summary>
        public void BackupLedger(string yearMonth, Guid[] companyIds, Guid SaveById) 
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspBackupLedgers");
                db.AddInParameter(dbCommand, "@YearMonth ", DbType.String, yearMonth);
                db.AddInParameter(dbCommand, "@CompanyIDS", DbType.String, companyIds.Join());
                db.AddInParameter(dbCommand, "@SaveByID ", DbType.Guid, SaveById);
                db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            { throw ex; }
        }
        #endregion

        #region  利润表
        /// <summary>
        /// 利润表
        /// </summary>
        /// <param name="companyIDs"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public List<ProfitDetailReport> GetProfitDetailList(Guid[] companyIDs, DateTime fromDate, DateTime toDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetProfitDetailList");

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<ProfitDetailReport> list = (from d in ds.Tables[0].AsEnumerable()
                                                 select new ProfitDetailReport
                                                   {
                                                        GLType=d.Field<String>("GLType"),
                                                        IndexNo=d.Field<int>("IndexNo"),
                                                        GLName=d.Field<String>("GLName"),
                                                        Amount=d.Field<Decimal?>("Amount"),
                                                        USDAmount=d.Field<Decimal?>("USDAmount"),
                                                        BalanceAmount = d.Field<Decimal?>("BalanceAmount"),
                                                        BalanceUSDAmount = d.Field<Decimal?>("BalanceUSDAmount")
                                                   }).ToList();

            

                return list;

            }
            catch (Exception ex) { throw ex; }
        }

       
        #endregion

        #region 利润汇总表
        /// <summary>
        /// 利润汇总表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public List<ProfitTotalReport> GetProfitTotalList(DateTime fromDate, DateTime toDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetProfitTotalList");

                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<ProfitTotalReport> list = (from d in ds.Tables[0].AsEnumerable()
                                                select new ProfitTotalReport
                                                 {
                                                     GLName = d.Field<String>("GLName"),
                                                     GLType=d.Field<String>("GLType"),
                                                     MYDAmount = d.Field<Decimal?>("MYDAmount"),
                                                     MSZAmount = d.Field<Decimal?>("MSZAmount"),
                                                     MGZAmount = d.Field<Decimal?>("MGZAmount"),
                                                     MXMAmount = d.Field<Decimal?>("MXMAmount"),
                                                     MSHAmount = d.Field<Decimal?>("MSHAmount"),
                                                     MNBAmount = d.Field<Decimal?>("MNBAmount"),
                                                     MCQAmount = d.Field<Decimal?>("MCQAmount"),
                                                     MQDAmount = d.Field<Decimal?>("MQDAmount"),
                                                     MDLAmount = d.Field<Decimal?>("MDLAmount"),
                                                     MTJAmount = d.Field<Decimal?>("MTJAmount"),
                                                     YYDAmount = d.Field<Decimal?>("YYDAmount"),
                                                     YSZAmount = d.Field<Decimal?>("YSZAmount"),
                                                     YGZAmount = d.Field<Decimal?>("YGZAmount"),
                                                     YXMAmount = d.Field<Decimal?>("YXMAmount"),
                                                     YSHAmount = d.Field<Decimal?>("YSHAmount"),
                                                     YNBAmount = d.Field<Decimal?>("YNBAmount"),
                                                     YCQAmount = d.Field<Decimal?>("YCQAmount"),
                                                     YQDAmount = d.Field<Decimal?>("YQDAmount"),
                                                     YDLAmount = d.Field<Decimal?>("YDLAmount"),
                                                     YTJAmount = d.Field<Decimal?>("YTJAmount")
                                                 }).ToList();





                return list;

            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region 利润分配表
        /// <summary>
        /// 利润分配
        /// </summary>
        /// <param name="companyIDs"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public List<ProfitAllocationDetailReport> GetProfitAllocationDetailList(Guid[] companyIDs, DateTime fromDate, DateTime toDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetProfitAllocationDetailList");

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, companyIDs.Join());
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<ProfitAllocationDetailReport> list = (from d in ds.Tables[0].AsEnumerable()
                                                           select new ProfitAllocationDetailReport
                                                             {
                                                                 IndexNo = d.Field<String>("IndexNo"),
                                                                 GLName = d.Field<String>("GLName"),
                                                                 YearAmount = d.Field<Decimal?>("YearAmount"),
                                                                 LastYearAmount = d.Field<Decimal?>("LastYearAmount")                                                 
                                                             }).ToList();



                return list;

            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region 利润分配汇总表
        /// <summary>
        /// 利润分配汇总表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>

        public List<CompanyBalanceSheet> GetProfitAllocationTotalList(DateTime fromDate, DateTime toDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fam.uspGetProfitAllocationTotalList");

                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<CompanyBalanceSheet> list = (from d in ds.Tables[0].AsEnumerable()
                                                  select new CompanyBalanceSheet
                                                {
                                                    GLName = d.Field<String>("GLName"),
                                                    GLType = d.Field<int>("GLType"),
                                                    YDAmount = d.Field<Decimal?>("YDAmount"),
                                                    SZAmount = d.Field<Decimal?>("SZAmount"),
                                                    GZAmount = d.Field<Decimal?>("GZAmount"),
                                                    XMAmount = d.Field<Decimal?>("XMAmount"),
                                                    SHAmount = d.Field<Decimal?>("SHAmount"),
                                                    NBAmount = d.Field<Decimal?>("NBAmount"),
                                                    CQAmount = d.Field<Decimal?>("CQAmount"),
                                                    QDAmount = d.Field<Decimal?>("QDAmount"),
                                                    DLAmount = d.Field<Decimal?>("DLAmount"),
                                                    TJAmount = d.Field<Decimal?>("TJAmount")
                                                }).ToList();





                return list;

            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region 进口箱列表
        /// <summary>
        /// 进口箱列表
        /// </summary>
        /// <param name="CompanyIDs">公司列表</param>
        /// <param name="FreightLocationIDs">提柜地列表</param>
        /// <param name="BeginTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <returns></returns>
        public OIContainerReportData GetOIContaierList(string CompanyIDs, string FreightLocationIDs, DateTime BeginTime, DateTime EndTime)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOIContaierList");

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, CompanyIDs);
                db.AddInParameter(dbCommand, "@FreightLocationIDs", DbType.String, FreightLocationIDs);
                db.AddInParameter(dbCommand, "@BeginTime", DbType.DateTime, BeginTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, EndTime);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<OIContaierList> list = (from d in ds.Tables[0].AsEnumerable()
                                             select new OIContaierList
                                                  {
                                                      ContainerNo = d.Field<String>("ContainerNo"),
                                                      ContainerTypeName = d.Field<string>("ContainerTypeName"),
                                                      FreightLocationID = d.Field<Guid?>("FreightLocationID"),
                                                      FreightLocationName = d.Field<string>("FreightLocationName"),
                                                      No = d.Field<string>("No"),
                                                      TransportName = d.Field<string>("TransportName"),
                                                      VeVoNo = d.Field<string>("VeVoNo"),
                                                  }).ToList();
                List<OIContainerTypeCount> coutlist = (from d in ds.Tables[1].AsEnumerable()
                                                       select new OIContainerTypeCount
                                             {
                                                 ContainerTypeName = d.Field<string>("ContainerTypeName"),
                                                 FreightLocationName = d.Field<string>("FreightLocationName"),
                                                 VeVoNo = d.Field<string>("VeVoNo"),
                                                 Num = d.Field<int>("Num"),
                                             }).ToList();

                OIContainerReportData returndata = new OIContainerReportData();
                returndata.OIContaierList = list;
                returndata.OIContainerTypeCount = coutlist;

                return returndata;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 进口箱量统计表
        /// </summary>
        /// <param name="queryParameter">查询条件</param>
        /// <returns></returns>
        public OIContainerReportData GetOIContainerVolumeTotal(QueryCriteria4OIContainerVolumeTotal queryParameter)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOIContaierList");

                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, queryParameter.CompanyIDs);
                db.AddInParameter(dbCommand, "@FreightLocationIDs", DbType.String, queryParameter.FreightLocationIDs);
                db.AddInParameter(dbCommand, "@VesselIDs", DbType.String, queryParameter.VesselIDs);
                db.AddInParameter(dbCommand, "@Voyages", DbType.String, queryParameter.VoyageIDs);
                db.AddInParameter(dbCommand, "@ContainerTypeIDs", DbType.String, queryParameter.ContainerTypeIDs);
                db.AddInParameter(dbCommand, "@BeginTime", DbType.DateTime, queryParameter.BeginTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, queryParameter.EndTime);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<OIContaierList> list = (from d in ds.Tables[0].AsEnumerable()
                                             select new OIContaierList
                                             {
                                                 ContainerNo = d.Field<String>("ContainerNo"),
                                                 ContainerTypeName = d.Field<string>("ContainerTypeName"),
                                                 FreightLocationID = d.Field<Guid?>("FreightLocationID"),
                                                 FreightLocationName = d.Field<string>("FreightLocationName"),
                                                 No = d.Field<string>("No"),
                                                 TransportName = d.Field<string>("TransportName"),
                                                 VeVoNo = d.Field<string>("VeVoNo"),
                                             }).ToList();
                List<OIContainerTypeCount> coutlist = (from d in ds.Tables[1].AsEnumerable()
                                                       select new OIContainerTypeCount
                                                       {
                                                           ContainerTypeName = d.Field<string>("ContainerTypeName"),
                                                           FreightLocationName = d.Field<string>("FreightLocationName"),
                                                           VeVoNo = d.Field<string>("VeVoNo"),
                                                           Num = d.Field<int>("Num"),
                                                       }).ToList();

                OIContainerReportData returndata = new OIContainerReportData();
                returndata.OIContaierList = list;
                returndata.OIContainerTypeCount = coutlist;

                return returndata;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #endregion
    }
}
