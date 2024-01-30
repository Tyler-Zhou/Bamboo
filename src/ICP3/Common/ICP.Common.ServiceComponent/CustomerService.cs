//-----------------------------------------------------------------------
// <copyright file="CustomerService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using ICP.Common.ServiceInterface.CompositeObjects;
using ICP.Framework.CommonLibrary.Server;

namespace ICP.Common.ServiceComponent
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using ServiceInterface;
    using ServiceInterface.DataObjects;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Framework.CommonLibrary.Common;
    using Framework.CommonLibrary.Helper;

    /// <summary>
    /// 公共客户管理服务
    /// </summary>
    public class CustomerService : ICustomerService
    {
        /// <summary>
        /// 是否英文环境
        /// </summary>
        private bool IsEnglish
        {
            get
            {
                try
                {
                    return ApplicationContext.Current.IsEnglish;
                }
                catch
                {
                    return false;
                }
            }
        }

        ISessionService _sessionService = null;

        #region 构造函数
        /// <summary>
        /// 公共客户管理服务
        /// </summary>
        /// <param name="sessionService"></param>
        public CustomerService(ISessionService sessionService)
        {
            _sessionService = sessionService;
        } 
        #endregion

        #region 获得客户列表(客户列表查询使用)
        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">全称</param>
        /// <param name="address">地址</param>
        /// <param name="tel">电话</param>
        /// <param name="fax">传真</param>
        /// <param name="eMail">邮件</param>
        /// <param name="countryID">国家</param>
        /// <param name="provinceID">省份</param>
        /// <param name="customerState">客户状态</param>
        /// <param name="customerType">客户类型</param>
        /// <param name="isAgentOfCarrier">是否承运人（客户类型必须是货代才可以设置这个值）</param>
        /// <param name="codeApplyState">客户审核状态</param>
        /// <param name="areaID">区域ID</param>
        /// <param name="applyTimeFrom">申请时间-开始</param>
        /// <param name="applyTimeTo">申请时间-结束</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回客户列表</returns>
        public List<CustomerList> GetCustomerListByList(
            string code,
            string name,
            string address,
            string tel,
            string fax,
            string eMail,
            Guid? countryID,
            Guid? provinceID,
            CustomerStateType? customerState,
            CustomerType? customerType,
            bool? isAgentOfCarrier,
            CustomerCodeApplyState? codeApplyState,
            Guid? areaID,
            DateTime? applyTimeFrom,
            DateTime? applyTimeTo,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCustomerList");

                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@Name", DbType.String, name);
                db.AddInParameter(dbCommand, "@Address", DbType.String, address);
                db.AddInParameter(dbCommand, "@Tel", DbType.String, tel);
                db.AddInParameter(dbCommand, "@Fax", DbType.String, fax);
                db.AddInParameter(dbCommand, "@EMail", DbType.String, eMail);
                db.AddInParameter(dbCommand, "@CountryID", DbType.Guid, countryID);
                db.AddInParameter(dbCommand, "@ProvinceID", DbType.Guid, provinceID);
                db.AddInParameter(dbCommand, "@CustomerState", DbType.Int16, customerState);
                db.AddInParameter(dbCommand, "@CustomerType", DbType.Int16, customerType);
                db.AddInParameter(dbCommand, "@IsAgentOfCarrier", DbType.Boolean, isAgentOfCarrier);
                db.AddInParameter(dbCommand, "@CodeApplyState", DbType.Int16, codeApplyState);
                db.AddInParameter(dbCommand, "@CodeApplicantCompanyID", DbType.Guid, areaID);
                db.AddInParameter(dbCommand, "@ApplyTimeFrom", DbType.DateTime, applyTimeFrom);
                db.AddInParameter(dbCommand, "@ApplyTimeTo", DbType.DateTime, applyTimeTo);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CustomerList> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new CustomerList
                                              {
                                                  Type = (CustomerType)b.Field<byte>("Type"),
                                                  IsAgentOfCarrier = b.Field<bool>("IsAgentOfCarrier"),
                                                  CAddress = b.Field<string>("CAddress"),
                                                  CheckedState = b.IsNull("CheckedState") ? (CustomerCodeApplyState?)null : (CustomerCodeApplyState)b.Field<byte>("CheckedState"),
                                                  CName = b.Field<string>("CName"),
                                                  Code = b.Field<string>("Code"),
                                                  CountryProvinceName = b.Field<string>("CountryProvinceName"),
                                                  CreateByName = b.Field<string>("CreateByName"),
                                                  updatebyname = b.Field<string>("updatebyname"),
                                                  CreateDate = b.Field<DateTime>("CreateDate"),
                                                  CShortName = b.Field<string>("CShortName"),
                                                  EAddress = b.Field<string>("EAddress"),
                                                  EMail = b.Field<string>("EMail"),
                                                  EName = b.Field<string>("EName"),
                                                  EShortName = b.Field<string>("EShortName"),
                                                  Fax = b.Field<string>("Fax"),
                                                  State = (CustomerStateType)b.Field<byte>("State"),
                                                  ID = b.Field<Guid>("ID"),
                                                  KeyWord = b.Field<string>("KeyWord"),
                                                  UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                  Tel1 = b.Field<string>("Tel1"),
                                                  Tel2 = b.Field<string>("Tel2"),
                                                  Term = b.Field<int>("Term"),
                                                  IsDangerous = b.Field<bool>("IsDangerous"),
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

        #region 获取例外客户
        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">全称</param>
        /// <param name="address">地址</param>
        /// <param name="tel">电话</param>
        /// <param name="fax">传真</param>
        /// <param name="eMail">邮件</param>
        /// <param name="countryID">国家</param>
        /// <param name="provinceID">省份</param>
        /// <param name="customerState">客户状态</param>
        /// <param name="customerType">客户类型</param>
        /// <param name="companyid">公司ID</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回客户列表</returns>
        public List<CustomerList> GetExCustomerListByList(
            string code,
            string name,
            string address,
            string tel,
            string fax,
            string eMail,
            Guid? countryID,
            Guid? provinceID,
            CustomerStateType? customerState,
            CustomerType? customerType,
            Guid companyid,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetExCustomerList");

                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@Name", DbType.String, name);
                db.AddInParameter(dbCommand, "@Address", DbType.String, address);
                db.AddInParameter(dbCommand, "@Tel", DbType.String, tel);
                db.AddInParameter(dbCommand, "@Fax", DbType.String, fax);
                db.AddInParameter(dbCommand, "@EMail", DbType.String, eMail);
                db.AddInParameter(dbCommand, "@CountryID", DbType.Guid, countryID);
                db.AddInParameter(dbCommand, "@ProvinceID", DbType.Guid, provinceID);
                db.AddInParameter(dbCommand, "@CustomerState", DbType.Int16, customerState);
                db.AddInParameter(dbCommand, "@CustomerType", DbType.Int16, customerType);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyid);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CustomerList> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new CustomerList
                                              {
                                                  Type = (CustomerType)b.Field<byte>("Type"),
                                                  IsAgentOfCarrier = b.Field<bool>("IsAgentOfCarrier"),
                                                  CAddress = b.Field<string>("CAddress"),
                                                  CheckedState = b.IsNull("CheckedState") ? (CustomerCodeApplyState?)null : (CustomerCodeApplyState)b.Field<byte>("CheckedState"),
                                                  CName = b.Field<string>("CName"),
                                                  Code = b.Field<string>("Code"),
                                                  CountryProvinceName = b.Field<string>("CountryProvinceName"),
                                                  CreateByName = b.Field<string>("CreateByName"),
                                                  updatebyname = b.Field<string>("updatebyname"),
                                                  CreateDate = b.Field<DateTime>("CreateDate"),
                                                  CShortName = b.Field<string>("CShortName"),
                                                  EAddress = b.Field<string>("EAddress"),
                                                  EMail = b.Field<string>("EMail"),
                                                  EName = b.Field<string>("EName"),
                                                  EShortName = b.Field<string>("EShortName"),
                                                  Fax = b.Field<string>("Fax"),
                                                  State = (CustomerStateType)b.Field<byte>("State"),
                                                  ID = b.Field<Guid>("ID"),
                                                  KeyWord = b.Field<string>("KeyWord"),
                                                  UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                  Tel1 = b.Field<string>("Tel1"),
                                                  Tel2 = b.Field<string>("Tel2"),
                                                  Term = b.Field<int>("Term"),
                                                  IsDangerous = b.Field<bool>("IsDangerous")
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

        #region 获得所有公司对应的客户列表
        /// <summary>
        /// 获得所有公司对应的客户列表
        /// </summary>
        /// <returns></returns>
        public List<CustomerList> GetCustomerListCompany()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCustomerListByCompany");

                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CustomerList> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new CustomerList
                                              {
                                                  Type = (CustomerType)b.Field<byte>("Type"),
                                                  IsAgentOfCarrier = b.Field<bool>("IsAgentOfCarrier"),
                                                  CAddress = b.Field<string>("CAddress"),
                                                  CheckedState = b.IsNull("CheckedState") ? (CustomerCodeApplyState?)null : (CustomerCodeApplyState)b.Field<byte>("CheckedState"),
                                                  CName = b.Field<string>("CName"),
                                                  Code = b.Field<string>("Code"),
                                                  CreateByName = b.Field<string>("CreateByName"),
                                                  CreateDate = b.Field<DateTime>("CreateDate"),
                                                  CShortName = b.Field<string>("CShortName"),
                                                  EAddress = b.Field<string>("EAddress"),
                                                  EName = b.Field<string>("EName"),
                                                  EShortName = b.Field<string>("EShortName"),
                                                  Fax = b.Field<string>("Fax"),
                                                  State = (CustomerStateType)b.Field<byte>("State"),
                                                  ID = b.Field<Guid>("ID"),
                                                  KeyWord = b.Field<string>("KeyWord"),
                                                  UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                  Tel1 = b.Field<string>("Tel1"),
                                                  Tel2 = b.Field<string>("Tel2"),
                                                  Term = b.Field<int>("Term"),
                                                  IsDangerous = b.Field<bool>("IsDangerous"),
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

        #region 获得客户列表(绑定下拉框时使用)
        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <param name="customerState">客户状态</param>
        /// <param name="isAgentOfCarrier">是否为货代</param>
        /// <param name="customerTypes">客户类型集合</param>
        /// <param name="codeApplyState">客户审核状态</param>
        /// <returns>返回客户列表</returns>
        public List<CustomerList> GetCustomerListByCombox(
                CustomerType[] customerTypes,
                CustomerCodeApplyState? codeApplyState,
                bool? isAgentOfCarrier,
                CustomerStateType? customerState)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCustomerListByCustomerType");

                db.AddInParameter(dbCommand, "@CustomerState", DbType.Int16, customerState);
                db.AddInParameter(dbCommand, "@CustomerTypes", DbType.String, customerTypes.Join());
                db.AddInParameter(dbCommand, "@IsAgentOfCarrier", DbType.Boolean, isAgentOfCarrier);
                db.AddInParameter(dbCommand, "@CodeApplyState", DbType.Int16, codeApplyState);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CustomerList> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new CustomerList
                                              {
                                                  Code = b.Field<string>("Code"),
                                                  CName = b.Field<string>("CName"),
                                                  EName = b.Field<string>("EName"),
                                                  CShortName = b.Field<string>("CShortName"),
                                                  EShortName = b.Field<string>("EShortName"),
                                                  ID = b.Field<Guid>("ID"),
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

        #region 获取客户列表(搜索器用)
        /// <summary>
        /// 获取客户列表(搜索器用)
        /// </summary>
        /// <param name="codeOrName">代码或全称</param>
        /// <param name="address">地址</param>
        /// <param name="tel">电话</param>
        /// <param name="fax">传真</param>
        /// <param name="eMail">邮件</param>
        /// <param name="countryID">国家</param>
        /// <param name="provinceID">省份</param>
        /// <param name="customerState">客户状态</param>
        /// <param name="customerTypes">客户类型</param>
        /// <param name="isAgentOfCarrier">是否承运人（客户类型必须是货代才可以设置这个值）</param>
        /// <param name="codeApplyState">客户审核状态</param>
        /// <param name="codeApplicantCompanyID">申请人所在公司</param>
        /// <param name="agentCustomerSolutionID">用于查找指定解决方案下的代理客户</param>
        /// <param name="applyTimeFrom">申请时间-开始</param>
        /// <param name="applyTimeTo">申请时间-结束</param>
        /// <param name="curruntUserID"></param>
        /// <param name="maxRecords">最大记录数</param>
        /// <param name="isFromOrder"></param>
        /// <returns>返回客户列表</returns>
        public List<CustomerInfo> GetCustomerListBySearch(
            string codeOrName,
            string address,
            string tel,
            string fax,
            string eMail,
            Guid? countryID,
            Guid? provinceID,
            CustomerStateType? customerState,
            CustomerType[] customerTypes,
            bool? isAgentOfCarrier,
            CustomerCodeApplyState? codeApplyState,
            Guid? codeApplicantCompanyID,
            Guid? agentCustomerSolutionID,
            DateTime? applyTimeFrom,
            DateTime? applyTimeTo,
            bool isFromOrder,
            Guid? curruntUserID,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCustomerListForFinder");
                string customerTypesString = string.Empty;
                if (customerTypes != null)
                {
                    customerTypesString = customerTypes.Join<CustomerType>();
                }

                db.AddInParameter(dbCommand, "@NameOrCode", DbType.String, codeOrName);
                db.AddInParameter(dbCommand, "@Address", DbType.String, address);
                db.AddInParameter(dbCommand, "@Tel", DbType.String, tel);
                db.AddInParameter(dbCommand, "@Fax", DbType.String, fax);
                db.AddInParameter(dbCommand, "@EMail", DbType.String, eMail);
                db.AddInParameter(dbCommand, "@CountryID", DbType.Guid, countryID);
                db.AddInParameter(dbCommand, "@ProvinceID", DbType.Guid, provinceID);
                db.AddInParameter(dbCommand, "@CustomerState", DbType.Int16, customerState);
                db.AddInParameter(dbCommand, "@CustomerType", DbType.String, customerTypesString);
                db.AddInParameter(dbCommand, "@IsAgentOfCarrier", DbType.Boolean, isAgentOfCarrier);
                db.AddInParameter(dbCommand, "@CodeApplyState", DbType.Int16, codeApplyState);
                db.AddInParameter(dbCommand, "@CodeApplicantCompanyID", DbType.Guid, codeApplicantCompanyID);

                db.AddInParameter(dbCommand, "@AgentCustomerSolutionID", DbType.Guid, agentCustomerSolutionID);
                db.AddInParameter(dbCommand, "@ApplyTimeFrom", DbType.DateTime, applyTimeFrom);
                db.AddInParameter(dbCommand, "@ApplyTimeTo", DbType.DateTime, applyTimeTo);
                db.AddInParameter(dbCommand, "@IsFromOrder", DbType.Boolean, isFromOrder);
                db.AddInParameter(dbCommand, "@CurruntUserID", DbType.Guid, curruntUserID);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CustomerInfo> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new CustomerInfo
                                              {
                                                  Type = (CustomerType)b.Field<byte>("Type"),
                                                  IsAgentOfCarrier = b.Field<bool>("IsAgentOfCarrier"),
                                                  CAddress = b.Field<string>("CAddress"),
                                                  CheckedState = b.IsNull("CheckedState") ? (CustomerCodeApplyState?)null : (CustomerCodeApplyState)b.Field<byte>("CheckedState"),
                                                  CName = b.Field<string>("CName"),
                                                  Code = b.Field<string>("Code"),
                                                  CountryProvinceName = b.Field<string>("CountryProvinceName"),
                                                  CreateByName = b.Field<string>("CreateByName"),
                                                  CreateDate = b.Field<DateTime>("CreateDate"),
                                                  CShortName = b.Field<string>("CShortName"),
                                                  EAddress = b.Field<string>("EAddress"),
                                                  EName = b.Field<string>("EName"),
                                                  EShortName = b.Field<string>("EShortName"),
                                                  Fax = b.Field<string>("Fax"),
                                                  EMail = b.Field<string>("EMail"),
                                                  Term = b.Field<int>("Term"),
                                                  State = (CustomerStateType)b.Field<byte>("State"),
                                                  ID = b.Field<Guid>("ID"),
                                                  KeyWord = b.Field<string>("KeyWord"),
                                                  UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                  Tel1 = b.Field<string>("Tel1"),
                                                  Tel2 = b.Field<string>("Tel2"),
                                                  IsDangerous = b.Field<bool>("IsDangerous"),
                                                  TradeTermID = b.IsNull("TradeTermID") ? Guid.Empty : b.Field<Guid>("TradeTermID"),
                                                  TradeTermName = b.Field<string>("TradeTermName"),
                                                  BankAccountNo = b.Field<string>("BankAccountNo"),
                                                  TaxIdNo = b.Field<string>("TaxIdNo")
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

        #region 获取客户列表(客户名称检查时用)
        /// <summary>
        /// 获取客户列表(客户名称检查时用)
        /// </summary>
        /// <param name="name">全称</param>       
        /// <returns>返回客户列表</returns>
        public List<CustomerList> GetCustomerListForName(string name)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCustomerListForName");

                db.AddInParameter(dbCommand, "@Name", DbType.String, name);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CustomerList> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new CustomerList
                                              {
                                                  Type = (CustomerType)b.Field<byte>("Type"),
                                                  CAddress = b.Field<string>("CAddress"),
                                                  CheckedState = b.IsNull("CheckedState") ? (CustomerCodeApplyState?)null : (CustomerCodeApplyState)b.Field<byte>("CheckedState"),
                                                  CName = b.Field<string>("CName"),
                                                  Code = b.Field<string>("Code"),
                                                  EnterpriseCodeType = b.Field<string>("EnterpriseCodeType"),
                                                  EnterpriseCode = b.Field<string>("EnterpriseCode"),
                                                  CountryEName = b.Field<string>("CountryEName"),
                                                  CountryName = b.Field<string>("CountryName"),
                                                  CountryProvinceName = b.Field<string>("CountryProvinceName"),
                                                  CreateByName = b.Field<string>("CreateByName"),
                                                  CreateDate = b.Field<DateTime>("CreateDate"),
                                                  CShortName = b.Field<string>("CShortName"),
                                                  EAddress = b.Field<string>("EAddress"),
                                                  EName = b.Field<string>("EName"),
                                                  EShortName = b.Field<string>("EShortName"),
                                                  Fax = b.Field<string>("Fax"),
                                                  State = (CustomerStateType)b.Field<byte>("State"),
                                                  KeyWord = b.Field<string>("KeyWord"),
                                                  Tel1 = b.Field<string>("Tel1"),
                                                  Tel2 = b.Field<string>("Tel2"),
                                                  Term = b.Field<int>("Term"),
                                                  IsDangerous = b.Field<bool>("IsDangerous"),
                                                  Remark = b.Field<string>("Remark"),
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

        /// <summary>
        /// 查询客户
        /// </summary>
        /// <param name="name"></param>
        /// <param name="enterprisecode"></param>
        /// <remarks>通过客户名、企业编码查询；用于宁波导入预配信息时判断是否需要新增客户</remarks>
        /// <returns></returns>
        public List<CustomerInfo> GetCustomerInfoBySearch(string name, string enterprisecode)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCustomerInfoForSearch");

                db.AddInParameter(dbCommand, "@EName", DbType.String, name);
                db.AddInParameter(dbCommand, "@EnterpriseCode", DbType.String, enterprisecode);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CustomerInfo> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new CustomerInfo
                                              {
                                                  ID = b.Field<Guid>("ID"),
                                                  Code = b.Field<string>("Code"),
                                                  KeyWord = b.Field<string>("KeyWord"),
                                                  CShortName = b.Field<string>("CShortName"),
                                                  EShortName = b.Field<string>("EShortName"),
                                                  CName = b.Field<string>("CName"),
                                                  EName = b.Field<string>("EName"),
                                                  CAddress = b.Field<string>("CAddress"),
                                                  EAddress = b.Field<string>("EAddress"),
                                                  EnterpriseCodeType = b.Field<string>("EnterpriseCodeType"),
                                                  EnterpriseCode = b.Field<string>("EnterpriseCode"),
                                                  CountryEName = b.Field<string>("CountryEName"),
                                                  CountryName = b.Field<string>("CountryName"),
                                                  Tel1 = b.Field<string>("Tel1"),
                                                  Fax = b.Field<string>("Fax"),
                                                  Remark = b.Field<string>("Remark"),
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

        #region 获取客户信息
        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回客户信息</returns>
        public CustomerInfo GetCustomerInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCustomerInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                CustomerInfo result = (from b in ds.Tables[0].AsEnumerable()
                                       select new CustomerInfo
                                       {
                                           CAddress = b.Field<string>("CAddress"),
                                           CBillName = b.Field<string>("CBillName"),
                                           CityID = b.IsNull("CityID") ? Guid.Empty : b.Field<Guid>("CityID"),
                                           CityName = b.Field<string>("CityName"),
                                           CName = b.Field<string>("CName"),
                                           Code = b.Field<string>("Code"),
                                           CountryID = b.Field<Guid>("CountryID"),
                                           CountryName = b.Field<string>("CountryName"),
                                           CountryEName = b.Field<string>("CountryEName"),
                                           CreateByID = b.Field<Guid>("CreateByID"),
                                           CreateByName = b.Field<string>("CreateByName"),
                                           CreateDate = b.Field<DateTime>("CreateDate"),
                                           CreditLimit = b.Field<decimal>("CreditLimit"),
                                           CShortName = b.Field<string>("CShortName"),
                                           EAddress = b.Field<string>("EAddress"),
                                           EBillName = b.Field<string>("EBillName"),
                                           EMail = b.Field<string>("EMail"),
                                           EName = b.Field<string>("EName"),
                                           EShortName = b.Field<string>("EShortName"),
                                           Fax = b.Field<string>("Fax"),
                                           Homepage = b.Field<string>("Homepage"),
                                           ID = b.Field<Guid>("ID"),
                                           IsAgentOfCarrier = b.Field<bool>("IsAgentOfCarrier"),
                                           FIRMCODE = b.Field<string>("FIRMCODE"),
                                           KeyWord = b.Field<string>("KeyWord"),
                                           PaymentTypeID = b.IsNull("PaymentTermID") ? (Guid?)null : b.Field<Guid>("PaymentTermID"),
                                           PaymentTypeName = b.Field<string>("PaymentTermName"),
                                           PostCode = b.Field<string>("PostCode"),
                                           ProvinceID = b.IsNull("ProvinceID") ? (Guid?)null : b.Field<Guid>("ProvinceID"),
                                           ProvinceName = b.Field<string>("ProvinceName"),
                                           Remark = b.Field<string>("Remark"),
                                           UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                           State = (CustomerStateType)b.Field<byte>("State"),
                                           TaxIdNo = b.Field<string>("TaxIdNo"),
                                           BankAccountNo = b.Field<string>("BankAccountNo"),
                                           TaxIdType = b.IsNull("TaxIdType") ? (TaxType?)null : (TaxType)b.Field<byte>("TaxIdType"),
                                           Tel1 = b.Field<string>("Tel1"),
                                           Tel2 = b.Field<string>("Tel2"),
                                           Term = b.Field<int>("Term"),
                                           TradeTermID = b.IsNull("TradeTermID") ? (Guid?)null : b.Field<Guid>("TradeTermID"),
                                           TradeTermName = b.Field<string>("TradeTermName"),
                                           Type = (CustomerType)b.Field<byte>("Type"),
                                           IsCompany = b.Field<bool>("IsCompanyCargo"),
                                           EnterpriseCodeType = b.Field<string>("EnterpriseCodeType"),
                                           EnterpriseCode = b.Field<string>("EnterpriseCode"),
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

        #region 通过客户ID和公司ID得到客户的月结信息
        /// <summary>
        /// 通过客户ID和公司ID得到客户的月结信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companyID"></param>
        /// <returns></returns>
        [Obsolete("IMonthlyClosingEntryService GetMonthlyClosingCustomer")]
        public CustomerInfo GetMonthlyClosingEntriesForCustomer(Guid id, Guid companyID)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(id, "companyID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCustomerForMonthlyClosingEntries");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                CustomerInfo result = (from b in ds.Tables[0].AsEnumerable()
                                       select new CustomerInfo
                                       {
                                           ID = b.Field<Guid>("ID"),
                                           IsAgentOfCarrier = b.Field<Boolean>("IsAgentOfCarrier"),
                                           Term = b.IsNull("CreditDate") ? 0 : b.Field<int>("CreditDate")
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

        #region 检测客户代码是否存在
        /// <summary>
        /// 检测客户代码是否存在
        /// </summary>
        /// <param name="customerID">客户</param>
        /// <param name="code">代码</param>
        /// <returns>存在返回true,否则返回fasle</returns>
        public bool CheckCustomerCodeExist(
            Guid customerID,
            string code)
        {
            ArgumentHelper.AssertGuidNotEmpty(customerID, "customerID");
            ArgumentHelper.AssertStringNotEmpty(code, "code");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspCheckCustomerCodeExist");

                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                db.AddOutParameter(dbCommand, "@IsExist", DbType.Boolean, 2);
                db.ExecuteNonQuery(dbCommand);

                bool tempIsExist = (bool)db.GetParameterValue(dbCommand, "@IsExist");
                return tempIsExist;
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

        #region 保存客户信息
        /// <summary>
        /// 保存客户信息
        /// </summary>
        /// <param name="cisaveRequest">客户保存对象</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData SaveCustomerInfo(CustomerInfoSaveRequest cisaveRequest)
        {
            return SaveCustomerInfo(cisaveRequest.id, cisaveRequest.code, cisaveRequest.keyword, cisaveRequest.eshortname,
                cisaveRequest.cshortname, cisaveRequest.cname, cisaveRequest.ename
                , cisaveRequest.ebillname, cisaveRequest.cbillname, cisaveRequest.caddress, cisaveRequest.eaddress,
                cisaveRequest.countryid, cisaveRequest.provinceid, cisaveRequest.cityid
                , cisaveRequest.enterprisecodetype, cisaveRequest.enterprisecode, cisaveRequest.postcode, cisaveRequest.tel1,
                cisaveRequest.tel2, cisaveRequest.fax, cisaveRequest.email
                , cisaveRequest.homepage, cisaveRequest.taxidtype, cisaveRequest.taxidno, cisaveRequest.bankaccountno,
                cisaveRequest.creditlimit, cisaveRequest.term, cisaveRequest.tradetermid
                , cisaveRequest.paymentTypeid, cisaveRequest.customerype, cisaveRequest.isagentOfcarrier, cisaveRequest.firmcode,
                cisaveRequest.remark, cisaveRequest.savebyid
                , cisaveRequest.iscompany, cisaveRequest.updatedate);
        }

        /// <summary>
        /// 保存客户信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="code">代码</param>
        /// <param name="keyWord">关键字</param>
        /// <param name="cShortName">中文简称</param>
        /// <param name="eShortName">英文简称</param>
        /// <param name="cName">中文名</param>
        /// <param name="eName">英文名</param>
        /// <param name="cBillName">中文账单名称</param>
        /// <param name="eBillName">英文账单名称</param>
        /// <param name="cAddress">中文地址</param>
        /// <param name="eAddress">英文地址</param>
        /// <param name="countryID">国家</param>
        /// <param name="provinceID">省/州</param>
        /// <param name="cityID">城市ID</param>
        /// <param name="enterprisecodetype">企业代码类型</param>
        /// <param name="enterprisecode">企业代码</param>
        /// <param name="postCode">邮政编码</param>
        /// <param name="tel1">电话1</param>
        /// <param name="tel2">电话2</param>
        /// <param name="fax">传真</param>
        /// <param name="eMail">邮件地址</param>
        /// <param name="homepage">主页</param>
        /// <param name="taxIdType">税务登记类型</param>
        /// <param name="taxIdNo">税务登记号</param>
        /// <param name="bankAccountNo"></param>
        /// <param name="creditLimit">信用限额</param>
        /// <param name="term">信用期限</param>
        /// <param name="tradeTermID">贸易条款</param>
        /// <param name="paymentTypeID">付款方式</param>
        /// <param name="type">类型（0船东（Carrier）、1航空公司(Airline)、2货代（Forwarding）、3直客（DirectClient）、4拖车行（Trucker）、5报关行（Broker）、6仓储（Warehouse）、7铁路（Railway）、8快递（Express））</param>
        /// <param name="isAgentOfCarrier">是否承运人（客户类型必须是货代才可以设置这个值）</param>
        /// <param name="fIRMCODE"></param>
        /// <param name="remark">备注</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="isCompany"></param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData SaveCustomerInfo(
            Guid? id,
            string code,
            string keyWord,
            string cShortName,
            string eShortName,
            string cName,
            string eName,
            string cBillName,
            string eBillName,
            string cAddress,
            string eAddress,
            Guid countryID,
            Guid? provinceID,
            Guid? cityID,
            string enterprisecodetype,
            string enterprisecode,
            string postCode,
            string tel1,
            string tel2,
            string fax,
            string eMail,
            string homepage,
            TaxType? taxIdType,
            string taxIdNo,
            string bankAccountNo,
            decimal creditLimit,
            int term,
            Guid? tradeTermID,
            Guid? paymentTypeID,
            CustomerType type,
            bool isAgentOfCarrier,
            string fIRMCODE,
            string remark,
            Guid saveByID,
            bool isCompany,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertStringNotEmpty(cShortName, "cShortName");
            ArgumentHelper.AssertStringNotEmpty(eShortName, "eShortName");
            ArgumentHelper.AssertStringNotEmpty(cName, "cName");
            ArgumentHelper.AssertStringNotEmpty(eName, "eName");
            ArgumentHelper.AssertStringNotEmpty(cAddress, "cAddress");
            ArgumentHelper.AssertStringNotEmpty(eAddress, "eAddress");
            ArgumentHelper.AssertGuidNotEmpty(countryID, "countryID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            if (!string.IsNullOrEmpty(bankAccountNo))
            {
                bankAccountNo = bankAccountNo.Replace(GlobalConstants.ShowDividedSymbol, GlobalConstants.DividedSymbol);
            }
            if (!string.IsNullOrEmpty(taxIdNo))
            {
                taxIdNo = taxIdNo.Replace(GlobalConstants.ShowDividedSymbol, GlobalConstants.DividedSymbol);
            }

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveCustomerInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@KeyWord", DbType.String, keyWord);
                db.AddInParameter(dbCommand, "@CShortName", DbType.String, cShortName);
                db.AddInParameter(dbCommand, "@EShortName", DbType.String, eShortName);
                db.AddInParameter(dbCommand, "@CName", DbType.String, cName);
                db.AddInParameter(dbCommand, "@EName", DbType.String, eName);
                db.AddInParameter(dbCommand, "@CBillName", DbType.String, cBillName);
                db.AddInParameter(dbCommand, "@EBillName", DbType.String, eBillName);
                db.AddInParameter(dbCommand, "@CAddress", DbType.String, cAddress);
                db.AddInParameter(dbCommand, "@EAddress", DbType.String, eAddress);
                db.AddInParameter(dbCommand, "@CountryID", DbType.Guid, countryID);
                db.AddInParameter(dbCommand, "@ProvinceID", DbType.Guid, provinceID);
                db.AddInParameter(dbCommand, "@CityID", DbType.Guid, cityID);
                db.AddInParameter(dbCommand, "@EnterpriseCodeType", DbType.String, enterprisecodetype);
                db.AddInParameter(dbCommand, "@EnterpriseCode", DbType.String, enterprisecode);
                db.AddInParameter(dbCommand, "@PostCode", DbType.String, postCode);
                db.AddInParameter(dbCommand, "@Tel1", DbType.String, tel1);
                db.AddInParameter(dbCommand, "@Tel2", DbType.String, tel2);
                db.AddInParameter(dbCommand, "@Fax", DbType.String, fax);
                db.AddInParameter(dbCommand, "@EMail", DbType.String, eMail);
                db.AddInParameter(dbCommand, "@Homepage", DbType.String, homepage);
                db.AddInParameter(dbCommand, "@TaxIdType", DbType.Int16, (short?)taxIdType);
                db.AddInParameter(dbCommand, "@TaxIdNo", DbType.String, taxIdNo);
                db.AddInParameter(dbCommand, "@BankAccountNo", DbType.String, bankAccountNo);
                db.AddInParameter(dbCommand, "@CreditLimit", DbType.String, creditLimit);
                db.AddInParameter(dbCommand, "@Term", DbType.Int32, term);
                db.AddInParameter(dbCommand, "@TradeTermID", DbType.Guid, tradeTermID);
                db.AddInParameter(dbCommand, "@PaymentTermID", DbType.Guid, paymentTypeID);
                db.AddInParameter(dbCommand, "@Type", DbType.Int16, (short)type);
                db.AddInParameter(dbCommand, "@IsAgentOfCarrier", DbType.Boolean, isAgentOfCarrier);
                db.AddInParameter(dbCommand, "@FIRMCODE", DbType.String, fIRMCODE);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, remark);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsCompanyCargo", DbType.Boolean, isCompany);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleResultData result = db.SingleResult(dbCommand);
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

        #region 保存例外客户
        /// <summary>
        /// 保存例外客户
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="Customers">客户集合</param>
        /// <param name="savebyid">保存人</param>
        /// <param name="isEnglish">是否英文</param>
        public void SaveExCustomerInfo(Guid companyid, Guid[] Customers, Guid savebyid, bool isEnglish)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveExCustomer");

            string tempCustomers = Customers.Join();

            db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyid);
            db.AddInParameter(dbCommand, "@Customers", DbType.String, tempCustomers);
            db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, savebyid);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

            db.ExecuteNonQuery(dbCommand);

        } 
        #endregion

        #region 检查是否是例外客户
        /// <summary>
        /// 检查是否是例外客户
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="customerid"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        public List<ExCustomer> CheckExCustomer(Guid companyid, Guid customerid, bool isEnglish)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspCheckInExCustomerList");

            db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyid);
            db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerid);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

            DataSet ds = db.ExecuteDataSet(dbCommand);
            if (ds == null || ds.Tables.Count < 1)
            {
                return null;
            }
            List<ExCustomer> results = (from b in ds.Tables[0].AsEnumerable()
                                        select new ExCustomer
                                                {
                                                    ID = b.Field<Guid>("ID"),
                                                    CompanyID = b.Field<Guid>("CompanyID"),
                                                    CustomerID = b.Field<Guid>("CustomerID"),
                                                    CreateBy = b.Field<Guid>("CreateBy"),
                                                    CreateDate = b.Field<DateTime>("CreateDate"),
                                                    UpdateBy = b.Field<Guid?>("UpdateBy"),
                                                    UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                    IsValid = b.Field<bool>("IsValid"),
                                                }).ToList();

            return results;
        } 
        #endregion

        #region 改变客户状态
        /// <summary>
        /// 改变客户状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="state">状态</param>
        /// <param name="changeByID">更改人</param>
        /// <param name="memoContent">备注内容</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ChangeCustomerState(
            Guid id,
            CustomerStateType state,
            Guid changeByID,
            string memoContent,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspChangeCustomerState");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@State", DbType.Int16, state);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@MemoContent", DbType.String, memoContent);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleResultData result = db.SingleResult(dbCommand);
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

        #region 获取客户合并列表
        /// <summary>
        /// 获取客户合并列表 
        /// </summary>
        /// <param name="mainCustomerID">主客户ID</param>
        /// <returns>返回</returns>
        public List<CustomerCombineList> GetCustomerCombineList(Guid mainCustomerID)
        {
            ArgumentHelper.AssertGuidNotEmpty(mainCustomerID, "mainCustomerID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetMergedCustomerList");

                db.AddInParameter(dbCommand, "@MainID", DbType.Guid, mainCustomerID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<CustomerCombineList>();
                }

                List<CustomerCombineList> results = (from b in ds.Tables[0].AsEnumerable()
                                                     select new CustomerCombineList
                                                     {
                                                         ID = b.Field<Guid>("ID"),
                                                         Code = b.Field<String>("Code"),
                                                         KeyWord = b.Field<String>("KeyWord"),
                                                         CShortName = b.Field<String>("CShortName"),
                                                         EShortName = b.Field<String>("EShortName"),
                                                         CName = b.Field<String>("CName"),
                                                         EName = b.Field<String>("EName"),
                                                         CAddress = b.Field<String>("CAddress"),
                                                         EAddress = b.Field<String>("EAddress"),
                                                         CountryProvinceName = b.Field<String>("CountryProvinceName"),
                                                         Tel1 = b.Field<String>("Tel1"),
                                                         Tel2 = b.Field<String>("Tel2"),
                                                         Fax = b.Field<String>("Fax"),
                                                         State = (CustomerStateType)b.Field<Byte>("State"),
                                                         CreateByName = b.Field<String>("CreateByName"),
                                                         CreateDate = b.Field<DateTime>("CreateDate"),
                                                         UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                         IsMain = b.Field<bool>("IsMain"),
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

        #region 获取合并客户ID列表
        /// <summary>
        /// 获取合并客户ID列表 
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <returns>返回</returns>
        public List<Guid> GetCombineCustomerIDList(Guid customerID)
        {
            ArgumentHelper.AssertGuidNotEmpty(customerID, "customerID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetMergedCustomerList");

                db.AddInParameter(dbCommand, "@MainID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<Guid>();
                }

                List<Guid> results = (from b in ds.Tables[0].AsEnumerable() select b.Field<Guid>("ID")).ToList();
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

        #region 合并客户
        /// <summary>
        /// 合并客户
        /// </summary>
        /// <param name="mainCustomerID">主客户</param>
        /// <param name="customerIDs">要被合并的客户</param>
        /// <param name="combineByID">合并人</param>
        /// <param name="updateDates">被何必客户的数据版本</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData CombineCustomers(
            Guid mainCustomerID,
            Guid[] customerIDs,
            Guid combineByID,
            DateTime?[] updateDates)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspMergeCustomer");

                string tempIDs = customerIDs.Join();
                string tempUpdateDates = updateDates.Join();

                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIDs);
                db.AddInParameter(dbCommand, "@PreservedID", DbType.Guid, mainCustomerID);
                db.AddInParameter(dbCommand, "@MergeByID", DbType.Guid, combineByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                ManyResultData result = db.ManyResult(dbCommand);
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

        #region 取消合并客户
        /// <summary>
        /// 取消合并客户
        /// </summary>
        /// <param name="customerIDs">取消客户列表</param>
        /// <param name="cancelByID">取消人</param>
        /// <param name="updateDates">取消客户的数据版本</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData CancelCombineCustomers(
            Guid[] customerIDs,
            Guid cancelByID,
            DateTime?[] updateDates)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspCancelMergeCustomer");

                string tempIDs = customerIDs.Join();
                string tempUpdateDates = updateDates.Join();

                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIDs);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@CancelByID", DbType.Guid, cancelByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                ManyResultData result = db.ManyResult(dbCommand);
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

        #region 取消例外客户
        /// <summary>
        /// 取消例外客户
        /// </summary>
        /// <param name="customerIDs">取消客户列表</param>
        /// <param name="companyid">所属公司</param>
        /// <param name="cancelByID">取消人</param>
        /// <param name="isEnglish">是否英文</param>
        public void CancelExCustomer(
            Guid[] customerIDs,
            Guid companyid,
            Guid cancelByID,
            bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspCancelExCustomer");

                string tempIDs = customerIDs.Join();

                db.AddInParameter(dbCommand, "@CustomerIDs", DbType.String, tempIDs);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyid);
                db.AddInParameter(dbCommand, "@UpdateByID", DbType.Guid, cancelByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

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

        #region 获取联系人列表
        /// <summary>
        /// 获取联系人列表
        /// </summary>
        /// <param name="customerID">客户</param>
        /// <returns>返回联系人列表</returns>
        public List<CustomerContactList> GetCustomerContactList(Guid customerID)
        {
            ArgumentHelper.AssertGuidNotEmpty(customerID, "customerID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCustomerContactList");

                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CustomerContactList> results = (from b in ds.Tables[0].AsEnumerable()
                                                     select new CustomerContactList
                                                     {
                                                         CName = b.Field<string>("CName"),
                                                         CreateByID = b.Field<Guid>("CreateByID"),
                                                         CreateByName = b.Field<string>("CreateByName"),
                                                         CreateDate = b.Field<DateTime>("CreateDate"),
                                                         CustomerID = b.Field<Guid>("CustomerID"),
                                                         CustomerName = b.Field<string>("CustomerName"),
                                                         Department = b.Field<string>("Department"),
                                                         EMail = b.Field<string>("EMail"),
                                                         EName = b.Field<string>("EName"),
                                                         Fax = b.Field<string>("Fax"),
                                                         ID = b.Field<Guid>("ID"),
                                                         Mobile = b.Field<string>("Mobile"),
                                                         Position = b.Field<string>("Position"),
                                                         Remark = b.Field<string>("Remark"),
                                                         UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                         IsValid = b.Field<bool>("IsValid"),
                                                         Tel = b.Field<string>("Tel"),
                                                         Type = (CustomerContactType)b.Field<byte>("Type"),
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

        #region 保存客户联系人信息
        /// <summary>
        /// 保存客户联系人信息
        /// </summary>
        /// <param name="customerID">客户</param>
        /// <param name="ids">ID列表</param>
        /// <param name="cNames">中文名列表</param>
        /// <param name="eNames">英文名列表</param>
        /// <param name="departments">部门列表</param>
        /// <param name="positions">职位列表</param>
        /// <param name="tels">电话列表</param>
        /// <param name="faxs">传真列表</param>
        /// <param name="mobiles">手机列表</param>
        /// <param name="eMails">邮件列表</param>
        /// <param name="remarks">批注列表</param>
        /// <param name="types">类型列表</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData SaveCustomerContactInfo(
            Guid customerID,
            Guid?[] ids,
            string[] cNames,
            string[] eNames,
            string[] departments,
            string[] positions,
            string[] tels,
            string[] faxs,
            string[] mobiles,
            string[] eMails,
            string[] remarks,
            CustomerContactType[] types,
            Guid saveByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(customerID, "customerID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");
            ArgumentHelper.AssertArrayLengthMatch(ids, cNames, eNames, departments, positions, tels, faxs, mobiles, eMails, remarks, updateDates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveCustomerContactInfo");

                string tempIds = ids.Join();
                string tempCNames = cNames.Join();
                string tempENames = eNames.Join();
                string tempDepartments = departments.Join();
                string tempPositions = positions.Join();
                string tempTels = tels.Join();
                string tempFaxs = faxs.Join();
                string tempMobiles = mobiles.Join();
                string tempEMails = eMails.Join();
                string tempContactTypes = types.Join<CustomerContactType>();
                string tempRemarks = remarks.Join();
                string tempDataVersions = updateDates.Join();

                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@CNames", DbType.String, tempCNames);
                db.AddInParameter(dbCommand, "@ENames", DbType.String, tempENames);
                db.AddInParameter(dbCommand, "@Departments", DbType.String, tempDepartments);
                db.AddInParameter(dbCommand, "@Positions", DbType.String, tempPositions);
                db.AddInParameter(dbCommand, "@Tels", DbType.String, tempTels);
                db.AddInParameter(dbCommand, "@Faxs", DbType.String, tempFaxs);
                db.AddInParameter(dbCommand, "@Mobiles", DbType.String, tempMobiles);
                db.AddInParameter(dbCommand, "@EMails", DbType.String, tempEMails);
                db.AddInParameter(dbCommand, "@Remarks", DbType.String, tempRemarks);
                db.AddInParameter(dbCommand, "@Types", DbType.String, tempContactTypes);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempDataVersions);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                ManyResultData result = db.ManyResult(dbCommand);
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

        /// <summary>
        /// 更改客户联系人有效状态

        /// </summary>
        /// <param name="id">ID列表</param>
        /// <param name="isValid">是否有效列表</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData ChangeCustomerContactState(
            Guid id,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspChangeCustomerContactState");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                ManyResultData result = db.ManyResult(dbCommand);
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
        /// 获取合作伙伴列表
        /// </summary>
        /// <param name="customerID">客户</param>
        /// <returns>返回合作伙伴列表</returns>
        public List<CustomerPartnerList> GetCustomerPartnerList(Guid customerID)
        {
            ArgumentHelper.AssertGuidNotEmpty(customerID, "customerID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCustomerPartnerList");

                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CustomerPartnerList> results = (from b in ds.Tables[0].AsEnumerable()
                                                     select new CustomerPartnerList
                                                     {
                                                         CreateByID = b.Field<Guid>("CreateByID"),
                                                         CreateByName = b.Field<string>("CreateByName"),
                                                         CreateDate = b.Field<DateTime>("CreateDate"),
                                                         CustomerID = b.Field<Guid>("CustomerID"),
                                                         CustomerName = b.Field<string>("CustomerName"),
                                                         ID = b.Field<Guid>("ID"),
                                                         PartnerAddress = b.Field<string>("PartnerAddress"),
                                                         PartnerCode = b.Field<string>("PartnerCode"),
                                                         PartnerID = b.Field<Guid>("PartnerID"),
                                                         PartnerKeyword = b.Field<string>("PartnerKeyword"),
                                                         PartnerName = b.Field<string>("PartnerName"),
                                                         PartnerTel = b.Field<string>("PartnerTel"),
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

        /// <summary>
        /// 保存合作伙伴信息
        /// </summary>
        /// <param name="customerID">客户</param>
        /// <param name="ids">ID列表</param>
        /// <param name="partnerIDs">合作伙伴列表</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData SaveCustomerPartnerInfo(
            Guid customerID,
            Guid?[] ids,
            Guid[] partnerIDs,
            Guid saveByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(customerID, "customerID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");
            ArgumentHelper.AssertArrayLengthMatch(partnerIDs, ids, updateDates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveCustomerPartnerInfo");

                string tempIDs = ids.Join();
                string tempPartnerIDs = partnerIDs.Join();
                string tempDataVersions = updateDates.Join();

                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempIDs);
                db.AddInParameter(dbCommand, "@PartnerIDs", DbType.String, tempPartnerIDs);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempDataVersions);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                ManyResultData result = db.ManyResult(dbCommand);
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
        /// 删除合作伙伴
        /// </summary>
        /// <param name="ids">合作伙伴列表</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDates">数据版本</param>
        public void RemoveCustomerPartnerInfo(
            Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");
            ArgumentHelper.AssertArrayLengthMatch(ids, updateDates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspRemoveCustomerPartnerInfo");

                string tempParterIDs = ids.Join();
                string tempDataVersions = updateDates.Join();

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempParterIDs);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempDataVersions);
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

        /// <summary>
        /// 获取客户审核列表
        /// </summary>
        /// <param name="customerCode">客户代码</param>
        /// <param name="customerName">客户名称</param>
        /// <param name="applicantID">申请人</param>
        /// <param name="apllyDateFrom">申请时间-开始</param>
        /// <param name="applyDateTo">申请时间-结束</param>
        /// <param name="confirmorID">审批人</param>
        /// <param name="confirmDateFrom">审批时间-开始</param>
        /// <param name="confirmDateTo">审批时间-结束</param>
        /// <param name="state">状态</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回户审核列表</returns>
        public List<CustomerConfirmList> GetCustomerConfirmList(
            string customerCode,
            string customerName,
            Guid? applicantID,
            DateTime? apllyDateFrom,
            DateTime? applyDateTo,
            Guid? confirmorID,
            DateTime? confirmDateFrom,
            DateTime? confirmDateTo,
            CustomerCodeApplyState? state,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCustomerConfirmList");

                db.AddInParameter(dbCommand, "@CustomerCode", DbType.String, customerCode);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@ApplicantID", DbType.Guid, applicantID);
                db.AddInParameter(dbCommand, "@ApllyDateFrom", DbType.DateTime, apllyDateFrom);
                db.AddInParameter(dbCommand, "@ApplyDateTo", DbType.DateTime, applyDateTo);
                db.AddInParameter(dbCommand, "@ConfirmorID", DbType.Guid, confirmorID);
                db.AddInParameter(dbCommand, "@ConfirmDateFrom", DbType.DateTime, confirmDateFrom);
                db.AddInParameter(dbCommand, "@ConfirmDateTo", DbType.DateTime, confirmDateTo);
                db.AddInParameter(dbCommand, "@State", DbType.Int16, state);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CustomerConfirmList> results = (from b in ds.Tables[0].AsEnumerable()
                                                     select new CustomerConfirmList
                                                     {
                                                         ApplicantName = b.Field<string>("ApplicantName"),
                                                         ApplicantRemark = b.Field<string>("ApplicantRemark"),
                                                         ApplyDate = b.Field<DateTime>("ApplyDate"),
                                                         ConfirmDate = b.Field<DateTime>("ConfirmDate"),
                                                         ConfirmorName = b.Field<string>("ConfirmorName"),
                                                         ConfirmorRemark = b.Field<string>("ConfirmorRemark"),
                                                         CustomerID = b.Field<Guid>("CustomerID"),
                                                         CustomerName = b.Field<string>("CustomerName"),
                                                         ID = b.Field<Guid>("ID"),
                                                         UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                         State = (CustomerCodeApplyState)b.Field<byte>("State"),
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
        /// 获取最近的客户申请信息
        /// </summary>
        /// <param name="customerID">客户</param>
        /// <returns>返回客户审核信息</returns>
        public CustomerConfirmInfo GetLatelyCustomerConfirmInfo(Guid customerID)
        {
            ArgumentHelper.AssertGuidNotEmpty(customerID, "customerID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetLatelyCustomerConfirmInfo");

                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                CustomerConfirmInfo result = (from b in ds.Tables[0].AsEnumerable()
                                              select new CustomerConfirmInfo
                                              {
                                                  ApplicantID = b.Field<Guid>("ApplicantID"),
                                                  ConfirmorID = b.Field<Guid?>("ConfirmorID"),
                                                  ApplicantName = b.Field<string>("ApplicantName"),
                                                  ApplicantRemark = b.Field<string>("ApplicantRemark"),
                                                  ApplyDate = b.Field<DateTime>("ApplyDate"),
                                                  ConfirmDate = b.Field<DateTime?>("ConfirmDate"),
                                                  ConfirmorName = b.Field<string>("ConfirmorName"),
                                                  ConfirmorRemark = b.Field<string>("ConfirmorRemark"),
                                                  CustomerID = b.Field<Guid>("CustomerID"),
                                                  CustomerName = b.Field<string>("CustomerName"),
                                                  ID = b.Field<Guid>("ID"),
                                                  State = (CustomerCodeApplyState)b.Field<byte>("State"),
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
        /// 申请客户信息
        /// </summary>
        /// <param name="customerID">客户</param>
        /// <param name="applicantID">申请人</param>
        /// <param name="applicantRemark">申请批注</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ApplyCustomerCode(
            Guid customerID,
            Guid applicantID,
            string applicantRemark,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(customerID, "customerID");
            ArgumentHelper.AssertGuidNotEmpty(applicantID, "applicantID");
            //ArgumentHelper.AssertStringNotEmpty(applicantRemark, "applicantRemark");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspApplyCustomerCode");

                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@ApplicantID", DbType.Guid, applicantID);
                db.AddInParameter(dbCommand, "@ApplicantRemark", DbType.String, applicantRemark);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleResultData result = db.SingleResult(dbCommand);
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
        /// 审核客户信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="customerCode">客户代码</param>
        /// <param name="state">状态</param>
        /// <param name="confirmorID">审核人</param>
        /// <param name="confirmorRemark">审核批注</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ConfirmCustomerInfo(
            Guid id,
            string customerCode,
            CustomerCodeApplyState state,
            Guid confirmorID,
            string confirmorRemark,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            //ArgumentHelper.AssertStringNotEmpty(customerCode, "customerCode");
            ArgumentHelper.AssertGuidNotEmpty(confirmorID, "confirmorID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspConfirmCustomerInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@CustomerCode", DbType.String, customerCode);
                db.AddInParameter(dbCommand, "@State", DbType.Int16, state);
                db.AddInParameter(dbCommand, "@ConfirmorID", DbType.Guid, confirmorID);
                db.AddInParameter(dbCommand, "@ConfirmorRemark", DbType.String, confirmorRemark);
                //db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleResultData result = db.SingleResult(dbCommand);
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
        /// 获取客户备注列表
        /// </summary>
        /// <param name="customerID">客户</param>
        /// <returns>返回客户备注列表</returns>
        public List<CustomerMemoList> GetCustomerMemoList(Guid customerID)
        {
            ArgumentHelper.AssertGuidNotEmpty(customerID, "customerID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCustomerMemoList");

                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CustomerMemoList> results = (from b in ds.Tables[0].AsEnumerable()
                                                  select new CustomerMemoList
                                                  {
                                                      ID = b.Field<Guid>("ID"),
                                                      CustomerID = b.Field<Guid>("CustomerID"),
                                                      MemoID = b.Field<Guid>("MemoID"),
                                                      Subject = b.Field<String>("Subject"),
                                                      Content = b.Field<String>("Content"),
                                                      IsShowCustomer = b.Field<bool>("IsShowCustomer"),
                                                      IsShowUser = b.Field<bool>("IsShowUser"),
                                                      Priority = (MemoPriority)b.Field<Byte>("Priority"),
                                                      Type = (MemoType)b.Field<Byte>("Type"),
                                                      CreateByID = b.Field<Guid>("CreateByID"),
                                                      CreateByName = b.Field<String>("CreateByName"),
                                                      CreateDate = b.Field<DateTime>("CreateDate"),
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

        /// <summary>
        /// 获取客户备注信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回备注信息</returns>
        public CustomerMemoInfo GetCustomerMemoInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetCustomerMemoInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                CustomerMemoInfo result = (from b in ds.Tables[0].AsEnumerable()
                                           select new CustomerMemoInfo
                                           {
                                               Content = b.Field<string>("Content"),
                                               CreateByID = b.Field<Guid>("CreateByID"),
                                               CreateByName = b.Field<string>("CreateByName"),
                                               CreateDate = b.Field<DateTime>("CreateDate"),
                                               CustomerID = b.Field<Guid>("CustomerID"),
                                               ID = b.Field<Guid>("ID"),
                                               MemoID = b.Field<Guid>("MemoID"),
                                               UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                               Subject = b.Field<string>("Subject"),
                                               Type = (MemoType)b.Field<byte>("Type"),
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
        /// 保存客户备注
        /// </summary>
        /// <param name="customerID">客户</param>
        /// <param name="ids">ID</param>
        /// <param name="subjects">主题</param>
        /// <param name="contents">内容</param>
        /// <param name="types">类型</param>
        /// <param name="prioritys">优先级</param>
        /// <param name="isShowCustomers"></param>
        /// <param name="isShowUsers">显示用户</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData SaveCustomerMemoInfo(
            Guid customerID,
            Guid?[] ids,
            string[] subjects,
            string[] contents,
            MemoType[] types,
            MemoPriority[] prioritys,
            bool[] isShowCustomers,
            bool[] isShowUsers,
            Guid saveByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(customerID, "customerID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSaveCustomerMemoInfo");

                string tempIds = ids.Join();
                string tempSubjects = subjects.Join();
                string tempContents = contents.Join();
                string tempTypes = types.Join<MemoType>();
                string tempUpdateDates = updateDates.Join();
                string tempIsShowUsers = isShowUsers.Join();
                string tempIsShowCustomers = isShowCustomers.Join();
                string tempPrioeritys = prioritys.Join<MemoPriority>();

                db.AddInParameter(dbCommand, "@Ids", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@Subjects", DbType.String, tempSubjects);
                db.AddInParameter(dbCommand, "@Contents", DbType.String, tempContents);
                db.AddInParameter(dbCommand, "@Types", DbType.String, tempTypes);
                db.AddInParameter(dbCommand, "@Prioritys", DbType.String, tempPrioeritys);
                db.AddInParameter(dbCommand, "@IsShowUsers", DbType.String, tempIsShowUsers);
                db.AddInParameter(dbCommand, "@IsShowCustomers", DbType.String, tempIsShowCustomers);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                ManyResultData result = db.ManyResult(dbCommand);
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
        /// 删除备注
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人ID</param>
        /// <param name="updateDate">数据版本</param>
        public void RemoveCustomerMemoInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspRemoveCustomerMemoInfo");

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

        /// <summary>
        /// 设置/取消客户为危险客户
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isDangerous">是否危险客户</param>
        /// <param name="changeByID">更改人</param>
        /// <param name="memoContent">备注内容</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData SetCustomerIsDangerous(
            Guid id,
            bool isDangerous,
            Guid changeByID,
            string memoContent,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspSetCustomerIsDangerous");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsDangerous", DbType.Boolean, isDangerous);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@MemoContent", DbType.String, memoContent);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleResultData result = db.SingleResult(dbCommand);
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

        #region 获得客户发票台头信息
        /// <summary>
        /// 获得客户发票抬头列表
        /// </summary>
        /// <param name="customerID">顾客ID</param>
        /// <param name="createBy">创建人</param>
        /// <returns></returns>
        public List<CustomerInvoiceTitleInfo> GetCustomerInvoiceTitleList(Guid customerID, Guid companyID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("pub.uspGetCustomerInvoiceTitleList");

                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CustomerInvoiceTitleInfo> results = (from b in ds.Tables[0].AsEnumerable()
                                                          select new CustomerInvoiceTitleInfo
                                                          {
                                                              ID = b.Field<Guid>("ID"),
                                                              Code = b.Field<String>("Code"),
                                                              CustomerID = b.Field<Guid>("CustomerID"),
                                                              CompanyID = b.Field<Guid>("CompanyID"),
                                                              InvoiceType = (CustomerInvoiceType)b.Field<Byte>("Type"),
                                                              Name = b.Field<String>("Name"),
                                                              TaxNo = b.Field<String>("TaxNo"),
                                                              AddressTel = b.Field<String>("AddressTel"),
                                                              BankAccountNo = b.Field<String>("BankAccountNo"),
                                                              IsValid = b.Field<Boolean>("IsValid"),
                                                              UpdateBy = b.Field<Guid?>("UpdateBy"),
                                                              CreateByName = b.Field<String>("CreateBy"),
                                                              CreateDate = b.Field<DateTime>("CreateDate"),
                                                              UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                              LastUseDate = b.Field<DateTime?>("LastUseDate"),
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
        /// 获得客户发票抬头列表
        /// </summary>
        /// <param name="customerID">顾客ID</param>
        /// <param name="invoiceTitle">发票抬头</param>
        /// <param name="createBy">创建人</param>
        /// <returns></returns>
        public List<CustomerInvoiceTitleInfo> GetCustomerInvoiceTitleList(Guid customerID, string invoiceTitle, Guid companyID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("pub.uspGetCustomerInvoiceTitle");

                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@InvoiceTitle", DbType.String, invoiceTitle);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CustomerInvoiceTitleInfo> results = (from b in ds.Tables[0].AsEnumerable()
                                                          select new CustomerInvoiceTitleInfo
                                                          {
                                                              ID = b.Field<Guid>("ID"),
                                                              Code = b.Field<String>("Code"),
                                                              CustomerID = b.Field<Guid>("CustomerID"),
                                                              CompanyID = b.Field<Guid>("CompanyID"),
                                                              InvoiceType = (CustomerInvoiceType)b.Field<Byte>("Type"),
                                                              Name = b.Field<String>("Name"),
                                                              TaxNo = b.Field<String>("TaxNo"),
                                                              AddressTel = b.Field<String>("AddressTel"),
                                                              BankAccountNo = b.Field<String>("BankAccountNo"),
                                                              IsValid = b.Field<Boolean>("IsValid"),
                                                              UpdateBy = b.Field<Guid?>("UpdateBy"),
                                                              CreateByName = b.Field<String>("CreateBy"),
                                                              CreateDate = b.Field<DateTime>("CreateDate"),
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

        /// <summary>
        /// 获得不重复（客户名称唯一）的客户发票抬头列表
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="updateTime">更新时间</param>
        /// <returns></returns>
        public List<CustomerInvoiceTitleInfo> GetUNICustomerInvoiceTitleList(Guid companyID, DateTime updateTime)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("pub.uspGetUNICustomerInvoiceTitleList");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@UpdateTime", DbType.DateTime, updateTime);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CustomerInvoiceTitleInfo> results = (from b in ds.Tables[0].AsEnumerable()
                                                          select new CustomerInvoiceTitleInfo
                                                          {
                                                              ID = b.Field<Guid>("ID"),
                                                              Code = b.Field<String>("Code"),
                                                              CustomerID = b.Field<Guid>("CustomerID"),
                                                              CompanyID = b.Field<Guid>("CompanyID"),
                                                              InvoiceType = (CustomerInvoiceType)b.Field<Byte>("Type"),
                                                              Name = b.Field<String>("Name"),
                                                              TaxNo = b.Field<String>("TaxNo"),
                                                              AddressTel = b.Field<String>("AddressTel"),
                                                              BankAccountNo = b.Field<String>("BankAccountNo"),
                                                              IsValid = b.Field<Boolean>("IsValid"),
                                                              CreateByID = b.Field<Guid>("CreateBy"),
                                                              CreateDate = b.Field<DateTime>("CreateDate"),
                                                              UpdateBy = b.Field<Guid?>("UpdateBy"),
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

        /// <summary>
        /// 获得导入的客户发票抬头信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public List<CustomerInvoiceTitleInfo> GetCustomerInvoiceTitleListForFinder(string taxNo, string name, string companyName)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("pub.uspGetTaxCustomerInvoiceTitleListForFinder");

                db.AddInParameter(dbCommand, "@TaxNo", DbType.String, taxNo);
                db.AddInParameter(dbCommand, "@Name", DbType.String, name);
                db.AddInParameter(dbCommand, "@CreateBy", DbType.String, companyName);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CustomerInvoiceTitleInfo> results = (from b in ds.Tables[0].AsEnumerable()
                                                          select new CustomerInvoiceTitleInfo
                                                          {
                                                              ID = b.Field<Guid>("ID"),
                                                              Code = b.Field<String>("Code"),
                                                              InvoiceType = (CustomerInvoiceType)b.Field<Byte>("Type"),
                                                              Name = b.Field<String>("Name"),
                                                              TaxNo = b.Field<String>("TaxNo"),
                                                              AddressTel = b.Field<String>("AddressTel"),
                                                              BankAccountNo = b.Field<String>("BankAccountNo"),
                                                              CreateByName = b.Field<String>("CreateBy")
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
        /// 获得客户发票信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public CustomerInvoiceTitleInfo GetCustomerInvoiceTitleInfo(Guid id)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("pub.uspGetCustomerInvoiceTitleInfo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                CustomerInvoiceTitleInfo results = (from b in ds.Tables[0].AsEnumerable()
                                                    select new CustomerInvoiceTitleInfo
                                                    {
                                                        ID = b.Field<Guid>("ID"),
                                                        Code = b.Field<String>("Code"),
                                                        CustomerID = b.Field<Guid>("CustomerID"),
                                                        CompanyID = b.Field<Guid>("CompanyID"),
                                                        InvoiceType = (CustomerInvoiceType)b.Field<Byte>("Type"),
                                                        Name = b.Field<String>("Name"),
                                                        TaxNo = b.Field<String>("TaxNo"),
                                                        AddressTel = b.Field<String>("AddressTel"),
                                                        BankAccountNo = b.Field<String>("BankAccountNo"),
                                                        IsValid = b.Field<Boolean>("IsValid"),
                                                        UpdateBy = b.Field<Guid>("UpdateBy"),
                                                        CreateByName = b.Field<String>("CreateBy"),
                                                        CreateDate = b.Field<DateTime>("CreateDate"),
                                                        UpdateDate = b.Field<DateTime?>("UpdateDate"),
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

        #region 保存客户发票台头信息
        /// <summary>
        /// 保存客户发票抬头信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public SingleResultData SaveCustomerInvoiceTitleInfo(
                    Guid customerID,
                    Guid companyID,
                    Guid id,
                    string code,
                    CustomerInvoiceType invoiceType,
                    string name,
                    string taxNo,
                    string addressTel,
                    string bankAccountNo,
                    bool isValid,
                    Guid saveByID,
                    DateTime? updateDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("pub.uspSaveCustomerInvoiceTitle");

                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@Type", DbType.Byte, invoiceType);
                db.AddInParameter(dbCommand, "@Name", DbType.String, name);
                db.AddInParameter(dbCommand, "@TaxNo", DbType.String, taxNo);
                db.AddInParameter(dbCommand, "@AddressTel", DbType.String, addressTel);
                db.AddInParameter(dbCommand, "@BankAccountNo", DbType.String, bankAccountNo);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);


                SingleResultData results = db.SingleResult(dbCommand);

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
