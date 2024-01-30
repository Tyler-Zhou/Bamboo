

namespace ICP.WF.ServiceComponent
{
    using System;
    using System.Collections.Generic;
    using ICP.Common.ServiceInterface;
    using ICP.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Server;
    using ICP.Sys.ServiceInterface;
    using ICP.Sys.ServiceInterface.DataObjects;
    using ICP.WF.ServiceInterface;
    using sys = ICP.Sys.ServiceInterface.DataObjects;
    using ICP.WF.ServiceInterface.DataObject;
    using ICP.Framework.CommonLibrary.Client;
    using System.Linq;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System.Data;
    using System.Data.Common;
    using ICP.Framework.CommonLibrary.Helper;
    using System.Data.SqlClient;
    using ICP.TMS.ServiceInterface;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.FAM.ServiceInterface;
    using ICP.FAM.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary;

    /// <summary>
    /// 流程扩展服务(用于与其他模块服务的代理)
    /// </summary>
    public class WorkFlowExtendService : IWorkFlowExtendService
    {
        #region 构造函数

        private ITransportFoundationService _transportFoundationService;
        private IOrganizationService _organizationService;
        private ICustomerService _customerService;
        private IUserService _userService;
        private IJobService _jobService;
        private IWorkFlowConfigService _workConfigService;
        private IConfigureService _configureService;
        private ISessionService _sessionService;
        public ITruckBookingService _truckBookingService;
        public IFinanceService _financeService;

        string _ftpHost = string.Empty;
        string _ftpUser = string.Empty;
        string _ftpPassword = string.Empty;
        string _ftpBasePath = "WF";

        public WorkFlowExtendService(
              ITransportFoundationService transportFoundationService
            , ICustomerService customerService
            , IOrganizationService organizationService
            , ISessionService sessionService
            , IJobService jobService
            , IUserService userService
            , IWorkFlowConfigService workConfigService
            , IConfigureService configureService
            , ITruckBookingService truckBookingService
            , IFinanceService financeService
            , string ftpHost
            , string ftpPath
            , string ftpUser
            , string ftpPassword)
        {
            _transportFoundationService = transportFoundationService;
            _customerService = customerService;
            _organizationService = organizationService;
            _jobService = jobService;
            _userService = userService;
            _workConfigService = workConfigService;
            _configureService = configureService;
            _sessionService = sessionService;
            _truckBookingService = truckBookingService;
            _financeService = financeService;

            _ftpHost = ftpHost;
            _ftpPassword = ftpPassword;
            _ftpBasePath = ftpPath;
            _ftpUser = ftpUser;

        }

        #endregion

        #region 本地变量

        /// <summary>
        /// 判断是否英文环境
        /// </summary>
        bool IsEnglish
        {
            get
            {
                try
                {
                    return ICP.Framework.CommonLibrary.Common.ApplicationContext.Current.IsEnglish;
                }
                catch
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 当前用户名
        /// </summary>
        Guid CurrentUserID
        {
            get
            {
                try
                {
                    return ICP.Framework.CommonLibrary.Common.ApplicationContext.Current.UserId;
                }
                catch
                {
                    return Guid.Empty;
                }
            }
        }


        #endregion

        #region IWorkFlowExtendService接口成员



        /// <summary>
        /// 根据获组织机构得用户ID集合
        /// </summary>
        /// <param name="ids">部门ID集合</param>
        /// <param name="names">部门名称集合</param>
        /// <param name="codes">部门代码集合</param>
        /// <param name="containsDescendants">是否包含下属单位</param>
        /// <param name="organizationType">机构类型</param>
        /// <returns></returns>
        public Guid[] GetUserByOrganization(Guid[] ids, string[] names, string[] codes, bool containsDescendants, OrganizationType organizationType)
        {

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("sm.uspGetUserByOrganization");


                string strID = ids == null ? string.Empty : ids.Join();
                string strName = names == null ? string.Empty : names.Join();
                string strCode = codes == null ? string.Empty : codes.Join();


                db.AddInParameter(dbCommand, "@Ids", DbType.String, strID);
                db.AddInParameter(dbCommand, "@Names", DbType.String, strName);
                db.AddInParameter(dbCommand, "@Codes", DbType.String, strCode);
                db.AddInParameter(dbCommand, "@ContainsDescendants", DbType.Boolean, containsDescendants);
                db.AddInParameter(dbCommand, "@OrganizationType", DbType.Byte, organizationType);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<Guid> results = (from b in ds.Tables[0].AsEnumerable()
                                      select b.Field<Guid>("ID")).ToList();

                if (results == null)
                {
                    results = new List<Guid>();
                }
                return results.ToArray();
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
        /// 获得指定职位与部门下的用户
        /// </summary>
        /// <param name="orgID">部门名称</param>
        /// <param name="jobIds">职位名称</param>
        /// <param name="containsDescendants">包含子节点</param>
        /// <param name="organizationType">部门类型</param>
        /// <returns></returns>
        public Guid[] GetUsersByJobAndOrganization(string orgID, string[] jobIds, bool containsDescendants, OrganizationType organizationType)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("sm.uspGetUsersByJobAndOrganization");

                string strJobID = jobIds == null ? string.Empty : jobIds.Join();


                db.AddInParameter(dbCommand, "@OrganizationID", DbType.String, orgID);
                db.AddInParameter(dbCommand, "@JobIDs", DbType.String, strJobID);
                db.AddInParameter(dbCommand, "@ContainsDescendants", DbType.Boolean, containsDescendants);
                db.AddInParameter(dbCommand, "@OrganizationType", DbType.Byte, organizationType);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<Guid> results = (from b in ds.Tables[0].AsEnumerable()
                                      select b.Field<Guid>("ID")).ToList();

                if (results == null)
                {
                    results = new List<Guid>();
                }

                return results.ToArray();
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
        /// 根据角色获得用户
        /// </summary>
        /// <param name="jobIds">职位ID集合</param>
        /// <param name="jobNames">职位名称集合</param>
        /// <returns></returns>
        public Guid[] GetUsersByJob(Guid[] jobIds, string[] jobNames)
        {

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("sm.uspGetUsersByJob");

                string strJobID = jobIds == null ? string.Empty : jobIds.Join();

                string strJobName = jobNames == null ? string.Empty : jobNames.Join();

                db.AddInParameter(dbCommand, "@JobIds", DbType.String, strJobID);
                db.AddInParameter(dbCommand, "@JobNames", DbType.String, strJobName);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<Guid> results = (from b in ds.Tables[0].AsEnumerable()
                                      select b.Field<Guid>("ID")).ToList();

                if (results == null)
                {
                    results = new List<Guid>();
                }
                return results.ToArray();
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
        /// 获得用户
        /// </summary>
        /// <param name="userIds">用户ID集合</param>
        /// <param name="userNames">用户名称集合</param>
        /// <param name="userCodes">用户代码集合</param>
        /// <returns></returns>
        public Guid[] GetUsersByIDs(Guid[] userIds, string[] userNames, string[] userCodes)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("sm.uspGetUsers");

                string strUserID = userIds == null ? string.Empty : userIds.Join();
                string strUserName = userNames == null ? string.Empty : userNames.Join();
                string strUserCode = userCodes == null ? string.Empty : userCodes.Join();



                db.AddInParameter(dbCommand, "@UserIds", DbType.String, strUserID);
                db.AddInParameter(dbCommand, "@UserNames", DbType.String, strUserName);
                db.AddInParameter(dbCommand, "@UserCodes", DbType.String, strUserCode);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<Guid> results = (from b in ds.Tables[0].AsEnumerable()
                                      select b.Field<Guid>("ID")).ToList();

                if (results == null)
                {
                    results = new List<Guid>();
                }
                return results.ToArray();
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
        /// 获得部门列表
        /// </summary>
        /// <param name="code"></param>
        /// <param name="cName"></param>
        /// <param name="eName"></param>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public List<OrganizationList> GetStructures(string code, string cName, string eName, string fullName)
        {
            List<OrganizationList> OrgList = new List<OrganizationList>();
            if (!string.IsNullOrEmpty(cName))
            {
                OrgList = _organizationService.GetOrganizationList(code, cName, true, 0);

            }
            else if (!string.IsNullOrEmpty(eName))
            {
                OrgList = _organizationService.GetOrganizationList(code, eName, true, 0);
            }
            else
            {
                OrgList = _organizationService.GetOrganizationList(code, string.Empty, true, 0);
            }

            if (!string.IsNullOrEmpty(fullName))
            {
                return OrgList.FindAll(delegate(OrganizationList o)
                {
                    return o.FullName == fullName;
                });
            }
            else
            {
                return OrgList;
            }
        }
        /// <summary>
        /// 获得用户所在的部门列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<UserOrganizationTreeList> GetStructuresByUser(Guid userId)
        {
            return _userService.GetUserOrganizationTreeList(userId);
        }



        /// <summary>
        /// 获取指定部门的职位列表
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public List<JobList> GetJobListByOrg(Guid departmentId)
        {
            List<sys.Organization2JobList> orgJobList = _jobService.GetOrganization2JobList(departmentId, true);
            List<JobList> jobList = new List<JobList>();

            jobList = (from oj in orgJobList
                       select new JobList
                                    {
                                        ID = oj.ID,
                                        CName = oj.Name,
                                        EName = oj.Name,
                                        ParentID = oj.ParentID,
                                        Code = oj.Code,
                                    }).ToList();

            return jobList;
        }



        /// <summary>
        /// 获得指定用户的明细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ICP.Sys.ServiceInterface.DataObjects.UserInfo GetUserInfoById(Guid id)
        {
            return _userService.GetUserInfo(id);

        }
        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="returnProperty"></param>
        /// <returns></returns>
        public string GetUserInfoByIDProperty(Guid id, string returnProperty)
        {
            UserInfo data = GetUserInfoById(id);
            if (data == null) return string.Empty;

            if (returnProperty.ToLower().Equals("code"))
            {
                return data.Code;
            }
            else if (returnProperty.ToLower().Equals("name"))
            {
                if (IsEnglish)
                {
                    return data.EName;
                }
                else
                {
                    return data.CName;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取指定用户列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public List<UserList> GetUserList(string code, string name, ICP.Sys.ServiceInterface.DataObjects.GenderType? sex, Guid? jobId, Guid? roleId, Guid? organizationId, bool? isValid, int maxRecords)
        {
            return _userService.GetUserListByList(code, name, sex, jobId, roleId, organizationId, null, isValid, maxRecords);
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="isValid">是否有效</param>
        /// <<param name="maxRecords">返回最大行数</param>
        /// <returns></returns>
        public List<JobList> GetJobList(string code, string name, bool? isValid, int maxRecords)
        {
            return _jobService.GetJobList(code, name, isValid, maxRecords);
        }

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="isValid">是否有效</param>
        /// <<param name="maxRecords">返回最大行数</param>
        /// <returns></returns>
        public List<OrganizationList> GetOrganizationList(string code, string name, bool? isValid, int maxRecords)
        {
            return _organizationService.GetOrganizationList(code, name, isValid, maxRecords);
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ICP.Sys.ServiceInterface.DataObjects.UserInfo GetUserInfo(string code)
        {
            return _userService.GetUserInfo(new Guid(code));
        }

        /// <summary>
        /// 获取部门详细信息
        /// </summary>
        /// <param name="ID">部门ID</param>
        /// <returns></returns>
        public OrganizationInfo GetOrganizationInfo(string ID)
        {
            return _organizationService.GetOrganizationInfo(new Guid(ID));
        }

        /// <summary>
        /// 获取职位详细信息
        /// </summary>
        /// <param name="code">职位代码</param>
        /// <returns></returns>
        public JobInfo GetJobInfo(string code)
        {
            return _jobService.GetJobInfo(new Guid(code));
        }
        #endregion

        #region 获得车辆列表
        /// <summary>
        /// 获得车辆列表
        /// </summary>
        /// <returns></returns>
        public List<TruckDataList> GetTruckList()
        {
            return _truckBookingService.GetTruckDataList(null, null, TruckDateSeachType.ALL, true, null, null, IsEnglish);
        }

        #endregion

        #region 获得全部的公司

        /// <summary>
        /// 获得全部的公司信息
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        public List<OrganizationList> GetWFAllOffice()
        {
            return _organizationService.GetOfficeList();
        }
        #endregion

        #region  获得表单Key集合
        /// <summary>
        /// 获得表单Key集合
        /// </summary>
        /// <returns></returns>
        public List<string> GetFormProfileKeyList()
        {
            List<FormProfileList> formProfileList = _workConfigService.GetFormProfileList(null, null, ApplicationContext.Current.IsEnglish);
            List<string> keyList = new List<string>();

            foreach (FormProfileList fp in formProfileList)
            {
                if (!keyList.Contains(fp.KeyWord))
                {
                    keyList.Add(fp.KeyWord);
                }
            }
            return keyList;


        }

        #endregion

        #region 发送消息
        /// <summary>
        /// 向指定人群发送消息
        /// </summary>
        /// <param name="receivers">接收人列表</param>
        /// <param name="sender">发送人</param>
        /// <param name="message">消息内容</param>
        public void SendMessage(Guid[] receivers, Guid? sender, string message)
        {

        }
        #endregion

        #region 发送邮件
        /// <summary>
        /// 向指定人群发送邮件
        /// </summary>
        /// <param name="sender">发送人</param>
        /// <param name="receivers">接收人列表</param>
        /// <param name="subject">消息主题</param>
        /// <param name="conent">消息内容</param>
        public void SendEMail(Guid sender, Guid[] receivers, string subject, string conent)
        {

        }
        #endregion

        #region 获得币种列表
        /// <summary>
        /// 获取币种列表
        /// </summary>
        /// <returns></returns>
        public List<CurrencyList> GetCurrencys()
        {
            List<CurrencyList> list = new List<CurrencyList>();
            List<CurrencyList> searchList = _configureService.GetCurrencyList(null, null, null, true, 0);
            if (searchList != null)
            {
                CurrencyList RMBCurrrency = searchList.Find(delegate(CurrencyList item) { return item.Code == "RMB"; });
                CurrencyList USDCurrrency = searchList.Find(delegate(CurrencyList item) { return item.Code == "USD"; });
                CurrencyList HKDCurrrency = searchList.Find(delegate(CurrencyList item) { return item.Code == "HKD"; });

                foreach (CurrencyList currency in searchList)
                {
                    if (currency != RMBCurrrency &&
                        currency != USDCurrrency &&
                        currency != HKDCurrrency)
                    {
                        list.Add(currency);
                    }
                }
                if (HKDCurrrency != null)
                {
                    list.Insert(0, HKDCurrrency);
                }
                if (USDCurrrency != null)
                {
                    list.Insert(0, USDCurrrency);
                }
                if (RMBCurrrency != null)
                {
                    list.Insert(0, RMBCurrrency);
                }
            }

            return list;

        }
        #endregion

        #region 获得客户信息
        /// <summary>
        /// 获得客户信息
        /// </summary>
        /// <param name="id">客户ID</param>
        /// <param name="returnProperty">返回的字段</param>
        /// <returns></returns>
        public string GetCustomerInfo(Guid id, string returnProperty)
        {
            CustomerInfo customer = _customerService.GetCustomerInfo(id);

            if (returnProperty.ToLower().Equals("code"))
            {
                return customer.Code;
            }
            else if (returnProperty.ToLower().Equals("cname"))
            {
                return customer.CName;
            }
            else if (returnProperty.ToLower().Equals("ename"))
            {
                return customer.EName;
            }
            return string.Empty;

        }
        #endregion

        #region 获得费用项目列表
        /// <summary>
        /// 获取可用费用项目列表
        /// </summary>
        /// <returns></returns>
        public List<ChargingCodeList> GetChargeCodeList(Guid? groupID)
        {
            return _configureService.GetChargingCodeListBySearch(null, null, groupID, null, true, 0);
        }
        #endregion

        #region 获得报销费用科目

        /// <summary>
        /// 获得报销费用科目
        /// </summary>
        /// <returns></returns>
        public List<CostItemData> GetCostItemList(Guid companyID)
        {
            List<SolutionGLCodeList> glcodelist = new List<SolutionGLCodeList>();
            List<CostItemData> costdata = new List<CostItemData>();
            glcodelist = _configureService.GetFeeGLCodeList(companyID, this.IsEnglish);
            if (glcodelist == null || glcodelist.Count == 0)
            {
                return costdata;
            }
            foreach (var item in glcodelist)
            {
                CostItemData cost = new CostItemData();
                cost.ID = item.ID;
                cost.ParentID = item.ParentID;
                cost.CName = item.CName;
                cost.EName = item.EName;
                cost.EFullName = item.EfullName;
                cost.FullName = item.FullName;
                cost.GroupID = item.GroupByID;
                costdata.Add(cost);
            }
            return costdata;
            //return _transportFoundationService.GetAllCostItems();
        }


        /// <summary>
        /// 获得影视公司的生产成本费用
        /// </summary>
        /// <returns></returns>
        public List<CostItemData> GetMovieCostList()
        {
            //佳辉
            Guid jhID = new Guid("7E08ECB1-71C1-4A6B-AD8E-022B28D54588");

            List<Guid> companyIDs = new List<Guid>();
            companyIDs.Add(jhID);

            List<SolutionGLCodeList> glcodelist = new List<SolutionGLCodeList>();
            List<CostItemData> costdata = new List<CostItemData>();

            glcodelist = _configureService.GetSolutionGLCodeListNew(DataTypeHelper.GetGuid("B6E4DDED-4359-456A-B835-E8401C910FD0"), companyIDs.ToArray(), "5001", string.Empty, GLCodeType.Unknown, true, LocalData.IsEnglish);
            if (glcodelist == null || glcodelist.Count == 0)
            {
                return costdata;
            }
            foreach (var item in glcodelist)
            {
                CostItemData cost = new CostItemData();
                cost.ID = item.ID;
                cost.ParentID = item.ParentID;
                cost.CName = item.CName;
                cost.EName = item.EName;
                cost.EFullName = item.EfullName;
                cost.FullName = item.FullName;
                cost.GroupID = item.GroupByID;
                costdata.Add(cost);
            }
            return costdata;
        }
        /// <summary>
        /// 获得所有的费用项目
        /// </summary>
        /// <returns></returns>
        public List<CostItemData> GetAllCostItemList()
        {
            return _transportFoundationService.GetAllCostItems();
        }

        #endregion

        #region 退佣

        /// <summary>
        /// 获得退佣日志
        /// </summary>
        /// <param name="operationID"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        public List<WFCommissionLogList> GetCommissionLogList(Guid[] operationIDs, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspGetCommissionLog");

                db.AddInParameter(dbCommand, "@OperationIDs", DbType.String, operationIDs.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<WFCommissionLogList> results = (from b in ds.Tables[0].AsEnumerable()
                                                     select new WFCommissionLogList
                                                {
                                                    CreateDate = b.Field<DateTime>("CreateDate"),
                                                    CreateName = b.Field<string>("CreateName"),
                                                    ID = b.Field<Guid>("ID"),
                                                    WorkFlowNo = b.Field<string>("WorkFlowNo"),
                                                    WorkName = b.Field<string>("WorkName"),
                                                    OperactioNo = b.Field<string>("OperationNo")

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
        /// 获得退佣业务纪录
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="blNo">提单号</param>
        /// <param name="containerNo">箱号</param>
        /// <param name="customerName">客户名称</param>
        /// <param name="isApply">是否已申请</param>
        /// <param name="dataPage">分布集合</param>
        /// <param name="isEnglish">是否为英文版本</param>
        /// <returns></returns>
        public PageList GetCommissionBusinessList(
                            Guid userID,
                            Guid[] companyIDs,
                            string operationNo,
                            string blNo,
                            string containerNo,
                            string customerName,
                            bool? isApply,
                            DataPageInfo dataPage,
                            bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspGetCommissionBusinessList");

                string tempCompanyIDs = companyIDs.Join();

                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@CompanyIDs", DbType.String, tempCompanyIDs);
                db.AddInParameter(dbCommand, "@OperationNo", DbType.String, operationNo);
                db.AddInParameter(dbCommand, "@BlNo", DbType.String, blNo);
                db.AddInParameter(dbCommand, "@ContainerNo", DbType.String, containerNo);
                db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@IsApply", DbType.Boolean, isApply);
                db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, dataPage.PageSize);
                db.AddInParameter(dbCommand, "@CurrentPage", DbType.Int32, dataPage.CurrentPage);
                db.AddInParameter(dbCommand, "@OrderByName", DbType.String, dataPage.SortByName + " " + dataPage.SortOrderType.ToString());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<WFBusinessList> results = (from b in ds.Tables[0].AsEnumerable()
                                                select new WFBusinessList
                                                     {
                                                         ID = b.Field<Guid>("ID"),
                                                         CompanyID = b.Field<Guid>("CompanyID"),
                                                         CustomerName = b.Field<string>("CustomerName"),
                                                         DefaultCurrencyID = b.Field<Guid>("DefaultCurrencyID"),
                                                         DefaultCurrencyName = b.Field<string>("DefaultCurrencyName"),
                                                         AP = b.Field<decimal>("CR"),
                                                         AR = b.Field<decimal>("DR"),
                                                         CommissionAmount = b.Field<decimal>("CommissionAmount"),
                                                         ECCost = b.Field<decimal>("ECCost"),
                                                         Ratio = b.Field<decimal>("Ratio"),
                                                         Profit = b.Field<decimal>("Profit"),
                                                         APDescription = b.Field<string>("DefaultCurrencyName") + ":" + b.Field<decimal>("CR").ToString(),
                                                         ARDescription = b.Field<string>("DefaultCurrencyName") + ":" + b.Field<decimal>("DR").ToString(),
                                                         CommissionAmountDescription = b.Field<string>("CommissionCurrencyName") + ":" + b.Field<decimal>("CommissionAmount").ToString(),
                                                         ProfitDescription = b.Field<string>("DefaultCurrencyName") + ":" + (b.Field<decimal>("Profit")).ToString(),
                                                         OperationDescription = b.Field<string>("OperationDescription"),
                                                         OperationNO = b.Field<string>("OperationNO"),
                                                         OperationType = (OperationType)b.Field<byte>("OperationType"),
                                                         RefNO = b.Field<string>("RefNO"),
                                                         SolutionID = b.Field<Guid>("SolutionID"),
                                                         StateDescription = GetStateDescription((OperationType)b.Field<byte>("OperationType"), b.Field<byte>("State")),
                                                         CompanyName = b.Field<string>("CompanyName"),
                                                         SalesName = b.Field<string>("SalesName"),
                                                         CustomerID = b.Field<Guid>("CustomerID"),
                                                         GoodsNmae = b.Field<string>("GoodsNmae"),
                                                         CommissionCurrencyID = b.Field<Guid>("CommissionCurrencyID"),
                                                         CommissionPayAmount = b.Field<Decimal>("CommissionPayAmount"),
                                                         PaidStatus = (PaidStatus)b.Field<Byte>("PaidStatus"),
                                                     }).ToList();

                dataPage.TotalCount = ((from c in ds.Tables[1].AsEnumerable() select c.Column<Int32>("TotalCount")).SingleOrDefault());

                PageList list = PageList.Create<WFBusinessList>(results, dataPage);
                return list;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 获得业务单号
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public string GetCommissionBusinessNos(string[] ids)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspGetCommissionBusinessNos");

                db.AddInParameter(dbCommand, "@OperationIDs", DbType.String, ids.Join());

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                if (ds.Tables[0].Rows[0][0] != null)
                {
                    return ds.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    return string.Empty;
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


        /// <summary>
        /// 获得退佣数据
        /// </summary>
        /// <param name="billID">账单ID集合</param>
        /// <param name="customersID">客户ID</param>
        /// <param name="operateRate">手续费比例</param>
        /// <returns></returns>
        public ComissionData GetWFComissionDataByCodition(List<Guid> operationIDList, List<Guid> currencyIDList, Guid customersID, bool isEnglish)
        {
            return _financeService.GetComissionDataByWF(operationIDList, currencyIDList, customersID, isEnglish);
        }


        /// <summary>
        /// 状态描述
        /// </summary>
        /// <param name="operationType"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        private string GetStateDescription(OperationType operationType, byte state)
        {
            if (operationType == OperationType.OceanExport)
            {
                return ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<OEBLState>((OEBLState)state, this.IsEnglish);
            }
            else if (operationType == OperationType.OceanImport)
            {
                return ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<OIOrderState>((OIOrderState)state, this.IsEnglish);
            }
            else if (operationType == OperationType.AirExport)
            {
                return ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<AEOrderState>((AEOrderState)state, this.IsEnglish);
            }
            else if (operationType == OperationType.AirImport)
            {
                return ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<AIOrderState>((AIOrderState)state, this.IsEnglish);
            }
            else if (operationType == OperationType.Other)
            {
                return ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<OBBLState>((OBBLState)state, this.IsEnglish);
            }
            else if (operationType == OperationType.Internal)
            {
                return ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<DTBLState>((DTBLState)state, this.IsEnglish);
            }
            else if (operationType == OperationType.Truck)
            {
                return ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<TruckBusinessState>((TruckBusinessState)state, this.IsEnglish);
            }


            else
            {
                return "未找到描述.请在 BusinessService.StateDescription方法中增加该描述的获取实现.";
            }
        }
        #endregion

        #region 业务费用报销
        /// <summary>
        /// 获得业务费用报销日志列表
        /// </summary>
        /// <param name="customerTouchIDs">业务跟进纪录ID集合</param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        public List<WFCustomerExpenseLogList> GetWFCustomerExpenseLogList(Guid[] customerTouchIDs, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspGetExpenseLogList ");

                db.AddInParameter(dbCommand, "@CustomerTouchIDs", DbType.String, customerTouchIDs.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<WFCustomerExpenseLogList> results = (from b in ds.Tables[0].AsEnumerable()
                                                          select new WFCustomerExpenseLogList
                                                          {
                                                              CreateBy = b.Field<string>("CreateBy"),
                                                              CreateDate = b.Field<DateTime>("CreateDate"),
                                                              WorkNo = b.Field<string>("WorkNo"),
                                                              WorkName = b.Field<string>("WorkName")
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
        /// 获得当前用户受益的CRM客户列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="code">代码</param>
        /// <param name="keyWord">关键字</param>
        /// <param name="cnmae">中文名称</param>
        /// <param name="ename">英文名称</param>
        /// <param name="contact">联系人</param>
        /// <param name="email">邮箱</param>
        /// <param name="country">国家</param>
        /// <param name="maxrow">最大行数</param>
        /// <param name="isenglish">是否英文版本</param>
        /// <returns></returns>
        public List<WFCECRMCustomerList> GetWFCRMCustomerList(Guid userID, string code, string keyWord, string cnmae, string ename, string contact, string email, string country, int maxrow, bool isenglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspGetCRMCustomerList");

                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                db.AddInParameter(dbCommand, "@KeyWord", DbType.String, keyWord);
                db.AddInParameter(dbCommand, "@CName", DbType.String, cnmae);
                db.AddInParameter(dbCommand, "@EName", DbType.String, ename);
                db.AddInParameter(dbCommand, "@Contact", DbType.String, contact);
                db.AddInParameter(dbCommand, "@EMail", DbType.String, email);
                db.AddInParameter(dbCommand, "@Country", DbType.String, country);
                db.AddInParameter(dbCommand, "@MaxRecord", DbType.Int32, maxrow);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isenglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<WFCECRMCustomerList> results = (from b in ds.Tables[0].AsEnumerable()
                                                     select new WFCECRMCustomerList
                                                      {
                                                          ID = b.Field<Guid>("ID"),
                                                          CName = b.Field<string>("CName"),
                                                          EName = b.Field<string>("EName"),
                                                          Code = b.Field<string>("Code"),
                                                          KeyWord = b.Field<string>("KeyWord"),
                                                          Country = b.Field<string>("Country")
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
        /// 获得CRM客户信息
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="isenglish">是否英文版本</param>
        /// <returns></returns>
        public WFCECRMCustomerList GetCRMCustomerInfo(Guid customerID, bool isenglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspGetCRMCustomerInfo");

                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, isenglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                WFCECRMCustomerList results = (from b in ds.Tables[0].AsEnumerable()
                                                     select new WFCECRMCustomerList
                                                     {
                                                         ID = b.Field<Guid>("ID"),
                                                         CName = b.Field<string>("CName"),
                                                         EName = b.Field<string>("EName"),
                                                         Code = b.Field<string>("Code"),
                                                         KeyWord = b.Field<string>("KeyWord"),
                                                         Country = b.Field<string>("Country")
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

        /// <summary>
        /// 获得客户跟进纪录列表
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        public List<WFCECRMCustomerTouchLogList> GetCRMCustomerTouchLogList(Guid customerID, bool isEnglish)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspGetCRMExpenseTouchLogList ");

                db.AddInParameter(dbCommand, "@CustomerID", DbType.Guid, customerID);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<WFCECRMCustomerTouchLogList> results = (from b in ds.Tables[0].AsEnumerable()
                                                             select new WFCECRMCustomerTouchLogList
                                                          {
                                                              ID = b.Field<Guid>("ID"),
                                                              CustomerID = customerID,
                                                              Content = b.Field<string>("Content"),
                                                              Subject = b.Field<string>("Subject"),
                                                              CreateTime = b.Field<DateTime>("CreateTime")
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

        #region 部门&公司


        /// <summary>
        /// 获得用户的部门树列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<OrganizationList> GetOrganizationListByDepartment(Guid userID)
        {
            List<UserOrganizationTreeList> treeList = _userService.GetUserOrganizationTreeList(userID);
            List<OrganizationList> list = (from d in treeList
                                           select new OrganizationList
                                           {
                                               ID = d.ID,
                                               Code = d.Code,
                                               CreateBy = d.CreateBy,
                                               CreateDate = d.CreateDate,
                                               CShortName = d.CShortName,
                                               EShortName = d.EShortName,
                                               FullName = d.FullName,
                                               HierarchyCode = d.HierarchyCode,
                                               IsDefault = d.IsDefault,
                                               IsValid = d.IsValid,
                                               ParentID = d.ParentID,
                                               Type = d.Type
                                           }).ToList();

            return list;

        }

        /// <summary>
        /// 获得用户的公司树列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<OrganizationList> GetOrganizationListByUserCompany(Guid userID)
        {
            List<OrganizationList> treeList = _userService.GetUserCompanyList(userID, null);
            List<OrganizationList> list = (from d in treeList
                                           where d.Type != OrganizationType.Department
                                           select new OrganizationList
                                           {
                                               ID = d.ID,
                                               Code = d.Code,
                                               CreateBy = d.CreateBy,
                                               CreateDate = d.CreateDate,
                                               CShortName = d.CShortName,
                                               EShortName = d.EShortName,
                                               FullName = d.FullName,
                                               HierarchyCode = d.HierarchyCode,
                                               IsDefault = d.IsDefault,
                                               IsValid = d.IsValid,
                                               ParentID = d.ParentID,
                                               Type = d.Type
                                           }).ToList();

            return list;
        }

        /// <summary>
        /// 获得所有的公司树列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<OrganizationList> GetOrganizationListByCompany()
        {
            List<OrganizationList> itemList = _organizationService.GetOrganizationList(string.Empty, string.Empty, true, 0);
            List<OrganizationList> list = (from d in itemList
                                           where d.Type != OrganizationType.Department
                                           select new OrganizationList
                                           {
                                               ID = d.ID,
                                               Code = d.Code,
                                               CreateBy = d.CreateBy,
                                               CreateDate = d.CreateDate,
                                               CShortName = d.CShortName,
                                               EShortName = d.EShortName,
                                               FullName = d.FullName,
                                               HierarchyCode = d.HierarchyCode,
                                               IsDefault = d.IsDefault,
                                               IsValid = d.IsValid,
                                               ParentID = d.ParentID,
                                               Type = d.Type
                                           }).ToList();

            return list;


        }
        #endregion

        #region
        /// <summary>
        /// 获取FTP服务器信息
        /// </summary>
        /// <returns>返回</returns>
        public ICP.OA.ServiceInterface.DataObjects.FTPServerConfig GetFTPServerConfig()
        {

            return new ICP.OA.ServiceInterface.DataObjects.FTPServerConfig(
                _ftpHost,
                _ftpUser,
                _ftpPassword,
                _ftpBasePath);
        }
        #endregion

        #region 获得银行帐号
        /// <summary>
        /// 获得银行帐号列表
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        public List<BankAccountList> GetBankAccountNoLis(Guid companyID)
        {
            List<BankAccountList> list = _financeService.GetCompanyBankAccounts(companyID, IsEnglish); ;
            list = (from d in list where d.IsInvoiceAccount orderby d.CurrencyName select d).ToList();
            return list;
        }

        public List<SolutionGLCodeList> GetSolutionGLCodeList(Guid solutionID)
        {
            return _configureService.GetSolutionGLCodeListNew(solutionID,new Guid[0]{}, string.Empty, string.Empty, GLCodeType.Unknown, true, LocalData.IsEnglish);
        }

        public List<DataDictionaryList> GetDataDictionaryList(DataDictionaryType type)
        {
            return _transportFoundationService.GetDataDictionaryList(string.Empty, string.Empty, type, true, 0); ;
        }

        #endregion

        #region 判断部门是否属于指定部门的子节点 
        public Boolean CheckOrganizationIDIsChildList(Guid checkID, Guid[] orgIds)
        {
           List<OrganizationList> list=_organizationService.GetOrganizationAndChildList(orgIds);

           bool isContains = false;

            foreach(OrganizationList item in list)
            {
                if (item.ID == checkID)
                {
                    return true;
                    break;
                }
            }
            return isContains;
        }
        #endregion
    }
}
