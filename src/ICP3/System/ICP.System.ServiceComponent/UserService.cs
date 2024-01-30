//-----------------------------------------------------------------------
// <copyright file="UserService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using ICP.Framework.CommonLibrary;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using System.Net;
using System.IO;
using System.Text;
using System.Threading;

namespace ICP.Sys.ServiceComponent
{

    /// <summary>
    /// 用户管理服务
    /// </summary>
    public class UserService : IUserService
    {

        #region 初始化

        ICP.Framework.CommonLibrary.Server.ISessionService _sessionService = null;

        string _ftpHost = string.Empty;
        string _ftpUser = string.Empty;
        string _ftpPassword = string.Empty;
        string _ftpBasePath = "\\UserPhoto";

        

        public UserService(string ftpHost,
                            string ftpPath,
                            string ftpUser,
                            string ftpPassword,
            ICP.Framework.CommonLibrary.Server.ISessionService sessionService)
        {
            _ftpHost = ftpHost;
            _ftpPassword = ftpPassword;
            _ftpBasePath = ftpPath;
            _ftpUser = ftpUser;
            _sessionService = sessionService;
        }


        #endregion

        #region 同步微信通讯录
        public class SyncContactsService
        {
            public void SyncCorpContacts(string type)
            {
                HttpGet(type);
            }
            public ManualResetEvent allDone = new ManualResetEvent(false);
            const int BUFFER_SIZE = 1024;
            const int DefaultTimeout = 1 * 60 * 1000; // 1 minutes timeout

            INIHelper iniConfig;
            /// <summary>
            /// INI 配置文件
            /// </summary>
            INIHelper INIConfig
            {
                get
                {
                    return iniConfig ?? (iniConfig = new INIHelper(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FrameworkCommandConstants.CONFIG_ICPSERVICE)));
                }
            }
            public class RequestState
            {
                // This class stores the State of the request.
                const int BUFFER_SIZE = 1024;
                public StringBuilder requestData;
                public byte[] BufferRead;
                public HttpWebRequest request;
                public HttpWebResponse response;
                public Stream streamResponse;
                public RequestState()
                {
                    BufferRead = new byte[BUFFER_SIZE];
                    requestData = new StringBuilder("");
                    request = null;
                    streamResponse = null;
                }
            }

            private void RespCallback(IAsyncResult asynchronousResult)
            {
                return;

            }
            private void ReadCallBack(IAsyncResult asyncResult)
            {
                return;
            }
            /// <summary>
            /// Http请求
            /// </summary>
            /// <param name="type"></param>
            private void HttpGet(string type)
            {
                try
                {
                    string url = INIConfig.IniReadValue(FrameworkCommandConstants.SECTION_CSPAPI, SystemCommandConstants.CSPAPI_SYNCCORPWEBSITE);
                    //string url = AppSettingReader.Current.GetAppSettingValue("SyncCorpWebsit");
                    if (url.Contains("?"))
                    {
                        url += "&type=" + type;
                    }
                    else
                    {
                        url += "?type=" + type;
                    }
                    if (string.IsNullOrEmpty(url)) { return; };
                    HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                    myHttpWebRequest.Method = "GET";//设置请求的方法
                    myHttpWebRequest.Accept = "*/*";//设置Accept标头的值
                    myHttpWebRequest.Timeout = 5000;
                    RequestState myRequestState = new RequestState();
                    myRequestState.request = myHttpWebRequest;
                    IAsyncResult result =
                    (IAsyncResult)myHttpWebRequest.BeginGetResponse(
                        new AsyncCallback(RespCallback), myRequestState);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        SyncContactsService syncContactsService = new SyncContactsService();
        #endregion

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

        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="identityCard">身份证</param>
        /// <returns>bool</returns>
        public bool IsExistUser(
            string code,
            string identityCard)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspIsExistUser");

                db.AddInParameter(command, "@Code", DbType.String, code);
                db.AddInParameter(command, "@IdentityCard", DbType.String, identityCard);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables[0].Rows.Count < 1)
                {
                    return false;
                }
                else
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
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="sex">性别</param>
        /// <param name="jobId">职位ID</param>
        /// <param name="roleId">角色ID</param>
        /// <param name="organizationId">部门ID</param>
        /// <param name="hasJob">是否有职位</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数(0:不限制,否则按指定记录返回)</param>
        /// <returns>返回用户列表</returns>
        public List<UserList> GetUserListByList(
            string code,
            string name,
            GenderType? sex,
            Guid? jobId,
            Guid? roleId,
            Guid? organizationId,
            bool? hasJob,
            bool? isValid,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetUserList");

                db.AddInParameter(command, "@Code", DbType.String, code);
                db.AddInParameter(command, "@Name", DbType.String, name);
                db.AddInParameter(command, "@Sex", DbType.Boolean, sex);
                db.AddInParameter(command, "@OrganizationID", DbType.Guid, organizationId);
                db.AddInParameter(command, "@JobID", DbType.Guid, jobId);
                db.AddInParameter(command, "@RoleID", DbType.Guid, roleId);
                db.AddInParameter(command, "@HasJob", DbType.Boolean, hasJob);
                db.AddInParameter(command, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.AddInParameter(command, "@MaxRecords", DbType.Int32, maxRecords);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<UserList>();
                }

                List<UserList> results = BulidUserListByDataSet(ds);

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
        /// 获得指定用户的下属列表
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="isValid"></param>
        /// <returns></returns>

        public List<UserList> GetSubordinateUserList(
            Guid userID,
            bool? isValid)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand("sm.uspGetSubordinateUserList");

                db.AddInParameter(command, "@UserID", DbType.Guid, userID);
                db.AddInParameter(command, "@IsValid", DbType.Boolean, isValid);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<UserList>();
                }

                List<UserList> results = (from d in ds.Tables[0].AsEnumerable()
                                          select new UserList
                                          {
                                              ID = d.Field<Guid>("ID"),
                                              Code = d.Field<string>("Code"),
                                              CName = d.Field<string>("CName"),
                                              EName = d.Field<string>("EName"),
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

        private static List<UserList> BulidUserListByDataSet(DataSet ds)
        {
            List<UserList> results = (from b in ds.Tables[0].AsEnumerable()
                                      select new UserList
                                      {
                                          ID = b.Field<Guid>("ID"),
                                          Code = b.Field<string>("Code"),
                                          CName = b.Field<string>("CName"),
                                          EName = b.Field<string>("EName"),
                                          Gender = b.Field<bool>("Sex") ? GenderType.Male : GenderType.Female,
                                          EMail = b.Field<string>("Email"),
                                          Tel = b.Field<string>("Tel"),
                                          Fax = b.Field<string>("Fax"),
                                          Mobile = b.Field<string>("Mobile"),
                                          IsValid = b.Field<bool>("IsValid"),
                                          OrganizationName = b.Field<string>("OrganizationName"),
                                          JobName = b.Field<string>("JobName"),
                                          CreateBy = b.Field<string>("CreateBy"),
                                          CreateDate = b.Field<DateTime>("CreateDate"),
                                          UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                      }).ToList();
            return results;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="organizationId">部门Id</param>
        /// <param name="roleName">角色名</param>
        /// <param name="jobName">职位名</param>
        /// <param name="hasJob">是否有职位</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回用户列表</returns>
        public List<UserList> GetUserListBySearch(
            Guid organizationId,
            string roleName,
            string jobName,
            bool? hasJob,
            bool? isValid,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetUserListForFinder");

                db.AddInParameter(command, "@OrganizationID", DbType.Guid, organizationId);
                db.AddInParameter(command, "@JobName", DbType.String, jobName);
                db.AddInParameter(command, "@RoleName", DbType.String, roleName);
                db.AddInParameter(command, "@HasJob", DbType.Boolean, hasJob);
                db.AddInParameter(command, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.AddInParameter(command, "@MaxRecords", DbType.Int32, maxRecords);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<UserList>();
                }

                List<UserList> results = BulidUserListByDataSet(ds);

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
        /// 获取指定部门下职位的下属用户列表(默认是有职位的用户)
        /// </summary>
        /// <param name="organizationIDs">部门ID</param>
        /// <param name="jobNames">职位名</param>
        /// <param name="roleNames">角色名</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回用户列表</returns>
        public List<UserList> GetUnderlingUserList(
            Guid[] organizationIDs,
            string[] jobNames,
            string[] roleNames,
            bool? isValid)
        {
            //ArgumentHelper.AssertGuidNotEmpty(organizationIDs, "organizationIDs");  

            try
            {
                string tempOrganizationIDs = organizationIDs == null ? string.Empty : organizationIDs.Join();
                string tempJobNames = jobNames == null ? string.Empty : jobNames.Join();
                string tempRoleNames = roleNames == null ? string.Empty : roleNames.Join();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetUnderlingUserList");

                db.AddInParameter(command, "@organizationIDs", DbType.String, tempOrganizationIDs);
                db.AddInParameter(command, "@JobNames", DbType.String, tempJobNames);
                db.AddInParameter(command, "@RoleNames", DbType.String, tempRoleNames);
                db.AddInParameter(command, "@IsValid", DbType.Boolean, isValid);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<UserList>();
                }

                List<UserList> results = BulidUserListByDataSet(ds);

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
        /// 获取组织架构下的用户列表(通过部门IDs、职位、角色)
        /// </summary>
        /// <param name="organizationIDs">部门ID</param>
        /// <param name="jobNames">职位名</param>
        /// <param name="roleNames">角色名</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回用户列表</returns>
        public List<UserList> GetOrganizationJobNameUserList(
            Guid[] organizationIDs,
            string[] jobNames,
            string[] roleNames,
            bool? isValid)
        {
            try
            {
                string tempOrganizationIDs = organizationIDs == null ? string.Empty : organizationIDs.Join();
                string tempJobNames = jobNames == null ? string.Empty : jobNames.Join();
                string tempRoleNames = roleNames == null ? string.Empty : roleNames.Join();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetOrganizationJobNameUserList");

                db.AddInParameter(command, "@organizationIDs", DbType.String, tempOrganizationIDs);
                db.AddInParameter(command, "@JobNames", DbType.String, tempJobNames);
                db.AddInParameter(command, "@RoleNames", DbType.String, tempRoleNames);
                db.AddInParameter(command, "@IsValid", DbType.Boolean, isValid);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<UserList>();
                }

                List<UserList> results = BulidUserListByDataSet(ds);

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
        /// 获取用户详细信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回用户详细信息</returns>
        public UserInfo GetUserInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetUserInfo");

                db.AddInParameter(command, "@ID", DbType.Guid, id);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                UserInfo result = (from b in ds.Tables[0].AsEnumerable()
                                   select new UserInfo
                                   {
                                       ID = b.Field<Guid>("ID"),
                                       Code = b.Field<string>("Code"),
                                       CName = b.Field<string>("CName"),
                                       EName = b.Field<string>("EName"),
                                       Gender = b.Field<bool>("Sex") ? GenderType.Male : GenderType.Female,
                                       EMail = b.Field<string>("Email"),
                                       Tel = b.Field<string>("Tel"),
                                       Fax = b.Field<string>("Fax"),
                                       Mobile = b.Field<string>("Mobile"),
                                       IsValid = b.Field<bool>("IsValid"),
                                       CreateById = b.Field<Guid>("CreateByID"),
                                       CreateBy = b.Field<string>("CreateBy"),
                                       CreateDate = b.Field<DateTime>("CreateDate"),
                                       UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                       //MailAccounts = (from m in ds.Tables[1].AsEnumerable()
                                       //               select new UserMailAccountList
                                       //               {
                                       //                   Email = b.Field<string>("Email"),
                                       //                   FriendlyName = b.Field<string>("FriendlyName"),
                                       //                   GetMailAtLogin = b.Field<bool>("GetMailAtLogin"),
                                       //                   ID = b.Field<Guid>("ID"),
                                       //                   IsDefault = b.Field<bool>("IsDefault"),
                                       //                   IsValid = b.Field<bool>("IsValid"),
                                       //                   MailIncomingHost = b.Field<string>("MailIncomingHost"),
                                       //                   MailIncomingLogin = b.Field<string>("MailIncomingLogin"),
                                       //                   MailIncomingPassword = b.Field<string>("MailIncomingPassword"),
                                       //                   MailIncomingPort = b.Field<short>("MailIncomingPort"),
                                       //                   MailIncomingProtocol = (MailProtocol) b.Field<short>("MailIncomingProtocol"),
                                       //                   MailOutgoingPassword =  b.Field<string>("MailIOutgoingPassword"),
                                       //                   MailOutgoingHost = b.Field<string>("MailOutgoingHost"),
                                       //                   MailOutgoingLogin = b.Field<string>("MailOutgoingLogin"),
                                       //                   MailOutgoingPort = b.Field<short>("MailOutgoingPort"),
                                       //                   MailOutgoingProtocol = (MailProtocol)b.Field<short>("MailOutgoingProtocol"),
                                       //                   UserID = b.Field<Guid>("UserID")
                                       //               }).ToList()
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
        /// 通过邮箱获取用户详细信息
        /// </summary>
        /// <param name="address">address</param>
        /// <returns>返回用户邮箱详细信息</returns>
        public UserInfoEmail GetUserInfoByEmailAddress(string address)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetUserInfoByEmailAddress");

                db.AddInParameter(command, "@EmailAddress", DbType.String, address);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                UserInfoEmail result = (from b in ds.Tables[0].AsEnumerable()
                                        select new UserInfoEmail
                                   {
                                       UserID = b.Field<Guid>("UserID"),
                                       CName = b.Field<string>("CName"),
                                       EName = b.Field<string>("EName"),
                                       EMail = b.Field<string>("Email"),
                                       Tel = b.Field<string>("Tel"),
                                       Mobile = b.Field<string>("Mobile"),
                                       MailIncomingHost = b.Field<string>("MailIncomingHost"),
                                       MailIncomingPassword = b.Field<string>("MailIncomingPassword"),
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
        /// 保存用户信息
        /// </summary>
        /// <param name="id">id==null或则id==Guid.Empty则新增数据,否则修改对应键的信息</param>
        /// <param name="code">代码</param>
        /// <param name="cname">中文名称</param>
        /// <param name="ename">英文名称</param>
        /// <param name="sex">性别</param>
        /// <param name="tel">电话</param>
        /// <param name="fax">传真</param>
        /// <param name="mobile">移动电话</param>
        /// <param name="saveById">修改人Id</param>
        /// <param name="emails">邮箱列表</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>/返回SingleResultData </returns>
        public SingleResultData SaveUserInfo(
            Guid? id,
            string code,
            string cname,
            string ename,
            GenderType sex,
            string tel,
            string fax,
            string mobile,
            Guid saveById,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(saveById, "saveById");
            ArgumentHelper.AssertStringNotEmpty(code, "code");
            ArgumentHelper.AssertStringNotEmpty(cname, "cname");
            ArgumentHelper.AssertStringNotEmpty(ename, "ename");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspSaveUserInfo");

                db.AddInParameter(command, "@ID", DbType.Guid, id);
                db.AddInParameter(command, "@Code", DbType.String, code);
                db.AddInParameter(command, "@CName", DbType.String, cname);
                db.AddInParameter(command, "@EName", DbType.String, ename);
                db.AddInParameter(command, "@Sex", DbType.Boolean, sex);
                db.AddInParameter(command, "@Tel", DbType.String, tel);
                db.AddInParameter(command, "@Fax", DbType.String, fax);
                db.AddInParameter(command, "@Mobile", DbType.String, mobile);
                db.AddInParameter(command, "@SaveByID", DbType.Guid, saveById);
                db.AddInParameter(command, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResultData result = db.SingleResult(command);
                syncContactsService.SyncCorpContacts("user_SaveUserInfo");
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
        /// 获取用户详细信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回用户详细信息</returns>
        public UserDetailInfo GetUserDetailInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetUserDetailInfo");

                db.AddInParameter(command, "@ID", DbType.Guid, id);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                UserDetailInfo result =
                    (from b in ds.Tables[0].AsEnumerable()
                     select new UserDetailInfo
                        {
                            ID = b.Field<Guid>("ID"),
                            Code = b.Field<string>("Code"),
                            CName = b.Field<string>("CName"),
                            EName = b.Field<string>("EName"),
                            Gender = b.Field<bool>("Sex") ? GenderType.Male : GenderType.Female,
                            EMail = b.Field<string>("Email"),
                            Tel = b.Field<string>("Tel"),
                            Fax = b.Field<string>("Fax"),
                            Mobile = b.Field<string>("Mobile"),
                            IsValid = b.Field<bool>("IsValid"),
                            CreateById = b.Field<Guid>("CreateByID"),
                            CreateBy = b.Field<string>("CreateBy"),
                            CreateDate = b.Field<DateTime>("CreateDate"),
                            UpdateDate = b.Field<DateTime?>("UpdateDate"),
                            Remark = b.Field<string>("Remark"),
                            Address = b.Field<string>("Address"),
                            EmailPassword = b.Field<string>("EmailPassword"),
                            Birthday = b.Field<DateTime?>("Birthday"),
                            DepartmentID = b.Field<Guid>("DepartmentID"),
                            DepartmentName = b.Field<String>("DepartmentName"),
                            Department = b.Field<string>("Department"),
                            JobID = b.Field<Guid>("JobID"),
                            JobName = b.Field<String>("JobName"),
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
        /// 获取所有用户详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ICP.ReportCenter.ServiceInterface.DataObjects.UserDetailInfo> GetAllUsersDetailInfo(Guid id)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetUserDetailInfo");

                db.AddInParameter(command, "@ID", DbType.Guid, id);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<ICP.ReportCenter.ServiceInterface.DataObjects.UserDetailInfo> result
                    = (from b in ds.Tables[0].AsEnumerable()
                       select new ICP.ReportCenter.ServiceInterface.DataObjects.UserDetailInfo
                        {
                            ID = b.Field<Guid>("ID"),
                            Code = b.Field<string>("Code"),
                            CName = b.Field<string>("CName"),
                            EName = b.Field<string>("EName"),
                            Gender = b.Field<bool>("Sex") ? "Man" : "Woman",
                            EMail = b.Field<string>("Email"),
                            Tel = b.Field<string>("Tel"),
                            Fax = b.Field<string>("Fax"),
                            Mobile = b.Field<string>("Mobile"),
                            IsValid = b.Field<bool>("IsValid"),
                            CreateById = b.Field<Guid>("CreateByID"),
                            CreateBy = b.Field<string>("CreateBy"),
                            CreateDate = b.Field<DateTime>("CreateDate"),
                            UpdateDate = b.Field<DateTime?>("UpdateDate"),
                            Remark = b.Field<string>("Remark"),
                            Address = b.Field<string>("Address"),
                            EmailPassword = b.Field<string>("EmailPassword"),
                            Birthday = b.Field<DateTime?>("Birthday"),
                            //DepartmentID = b.Field<Guid>("DepartmentID"),
                            Department = b.Field<string>("Department"),
                            RoleName = b.Field<string>("RoleName"),
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
        /// <summary>
        /// 获取所有用户详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<UserDetailInfo> GetAllUsersDetailInfo_UserService(Guid id)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetUserDetailInfo");

                db.AddInParameter(command, "@ID", DbType.Guid, id);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<UserDetailInfo> result
                    = (from b in ds.Tables[0].AsEnumerable()
                       select new UserDetailInfo
                       {
                           ID = b.Field<Guid>("ID"),
                           Code = b.Field<string>("Code"),
                           CName = b.Field<string>("CName"),
                           EName = b.Field<string>("EName"),
                           Gender = b.Field<bool>("Sex") ? GenderType.Male : GenderType.Female,
                           EMail = b.Field<string>("Email"),
                           Tel = b.Field<string>("Tel"),
                           Fax = b.Field<string>("Fax"),
                           Mobile = b.Field<string>("Mobile"),
                           IsValid = b.Field<bool>("IsValid"),
                           CreateById = b.Field<Guid>("CreateByID"),
                           CreateBy = b.Field<string>("CreateBy"),
                           CreateDate = b.Field<DateTime>("CreateDate"),
                           UpdateDate = b.Field<DateTime?>("UpdateDate"),
                           Remark = b.Field<string>("Remark"),
                           Address = b.Field<string>("Address"),
                           EmailPassword = b.Field<string>("EmailPassword"),
                           Birthday = b.Field<DateTime?>("Birthday"),
                           DepartmentID = b.Field<Guid>("DepartmentID"),
                           DepartmentName = b.Field<String>("DepartmentName"),
                           Department = b.Field<string>("Department"),
                           JobID = b.Field<Guid>("JobID"),
                           JobName = b.Field<String>("JobName"),
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
        /// <summary>
        /// 获取通讯录用户信息列表
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public ICP.ReportCenter.ServiceInterface.DataObjects.ContactObject GetAllContactList(Guid organizationID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetUserContactInfo");
                db.AddInParameter(command, "@OrganizationID", DbType.Guid, organizationID);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                ICP.ReportCenter.ServiceInterface.DataObjects.ContactObject result = new ICP.ReportCenter.ServiceInterface.DataObjects.ContactObject();

                result.FullDepartmentList = (from d in ds.Tables[0].AsEnumerable()
                                             select new ICP.ReportCenter.ServiceInterface.DataObjects.FullDepartmentObject
                                                             {
                                                                 ID = d.Field<Guid>("ID"),
                                                                 ParentID = d.Field<Guid>("ParentID"),
                                                                 Type = d.Field<byte>("Type"),
                                                                 FullName = d.Field<string>("FullCName"),
                                                                 CompanyAddress = d.Field<string>("CompanyAddress"),
                                                                 CompanyFax = d.Field<string>("CompanyFax"),
                                                                 CompanyTel = d.Field<string>("CompanyTel"),

                                                             }).ToList();

                result.UserDetailInfoList = (from u in ds.Tables[1].AsEnumerable()
                                             select new ICP.ReportCenter.ServiceInterface.DataObjects.UserDetailInfo
                                                  {
                                                      ID = u.Field<Guid>("ID"),
                                                      CName = u.Field<string>("CName"),
                                                      EName = u.Field<string>("EName"),
                                                      EMail = u.Field<string>("Email"),
                                                      Tel = u.Field<string>("Tel"),
                                                      //Fax = u.Field<string>("Fax"),
                                                      //Address = u.Field<string>("Address"),
                                                      ParentID = u.Field<Guid>("ParentID"),
                                                      Mobile = u.Field<string>("Mobile"),
                                                      RoleName = u.Field<string>("RoleName")

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
        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="id">id==null或则id==Guid.Empty则新增数据,否则修改对应键的信息</param>
        /// <param name="code">代码</param>
        /// <param name="cname">中文名称</param>
        /// <param name="ename">英文名称</param>
        /// <param name="sex">性别</param>
        /// <param name="tel">电话</param>
        /// <param name="fax">传真</param>
        /// <param name="mobile">移动电话</param>
        /// <param name="saveById">修改人Id</param>
        /// <param name="address">地址</param>
        /// <param name="birthday">birthday</param>
        /// <param name="remark">remark</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>/返回SingleResultData </returns>
        public SingleResultData SaveUserDetailInfo(
            Guid id,
            string cname,
            string ename,
            GenderType sex,
            string tel,
            string fax,
            string mobile,

            string address,
            string remark,
            DateTime? birthday,

            Guid saveById,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(saveById, "saveById");
            ArgumentHelper.AssertStringNotEmpty(cname, "cname");
            ArgumentHelper.AssertStringNotEmpty(ename, "ename");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspSaveUserDetailInfo");

                db.AddInParameter(command, "@ID", DbType.Guid, id);
                db.AddInParameter(command, "@CName", DbType.String, cname);
                db.AddInParameter(command, "@EName", DbType.String, ename);
                db.AddInParameter(command, "@Sex", DbType.Boolean, sex);
                db.AddInParameter(command, "@Tel", DbType.String, tel);
                db.AddInParameter(command, "@Fax", DbType.String, fax);
                db.AddInParameter(command, "@Mobile", DbType.String, mobile);
                db.AddInParameter(command, "@Address", DbType.String, address);
                db.AddInParameter(command, "@Birthday", DbType.DateTime, birthday);
                db.AddInParameter(command, "@Remark", DbType.String, remark);
                db.AddInParameter(command, "@SaveByID", DbType.Guid, saveById);
                db.AddInParameter(command, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResultData result = db.SingleResult(command);
                syncContactsService.SyncCorpContacts("user_SaveUserDetailInfo");
                return result;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 获取FTP服务器信息
        /// </summary>
        /// <returns>返回</returns>
        public FTPServerConfig GetFTPServerConfig()
        {
            return new FTPServerConfig(
                _ftpHost,
                _ftpUser,
                _ftpPassword,
                _ftpBasePath);
        }


        /// <summary>
        /// ChangedUserPassword
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="oldPassword">oldPassword</param>
        /// <param name="newPassword">newPassword</param>
        /// <param name="updateDate">updateDate</param>
        public SingleResultData ChangedUserPassword(Guid id, string oldPassword, string newPassword, DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            //ArgumentHelper.AssertStringNotEmpty(oldPassword, "oldPassword");
            ArgumentHelper.AssertStringNotEmpty(newPassword, "newPassword");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspChangedUserPassword");

                if (string.IsNullOrEmpty(oldPassword) == false) oldPassword = Cryptography.Encrypt3DES(oldPassword, id.ToString());

                newPassword = Cryptography.Encrypt3DES(newPassword, id.ToString());

                db.AddInParameter(command, "@ID", DbType.Guid, id);
                db.AddInParameter(command, "@oldPassword", DbType.String, oldPassword);
                db.AddInParameter(command, "@newPassword", DbType.String, newPassword);
                db.AddInParameter(command, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                SingleResultData result = db.SingleResult(command);
                return result;
            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }

        }

        /// <summary>
        /// 设置用户默认邮件帐号
        /// </summary>
        /// <param name="userMailAccountID">邮件帐号ID</param>
        /// <param name="setById">设置人ID</param>
        /// <returns>返回SingleResultData</returns>
        public ManyResultData SetUserDefaultMailAccount(
            Guid userMailAccountID,
            Guid setById)
        {
            ArgumentHelper.AssertGuidNotEmpty(userMailAccountID, "userMailAccountID");
            ArgumentHelper.AssertGuidNotEmpty(setById, "setById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspSetUserDefaultMailAccount");

                db.AddInParameter(command, "@ID", DbType.Guid, userMailAccountID);
                db.AddInParameter(command, "@SetByID", DbType.Guid, setById);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyResultData result = db.ManyResult(command);
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
        /// 保存用户邮件帐号
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="ids">邮件帐号ID</param>
        /// <param name="emails">邮件地址</param>
        /// <param name="mailIncomingProtocols">接收邮件服务器协议</param>
        /// <param name="mailIncomingHosts">接收邮件服务器</param>
        /// <param name="mailIncomingPorts">接收邮件服务器端口</param>
        /// <param name="mailIncomingLogins">接收邮件用户名</param>
        /// <param name="mailIncomingPasswords">接收邮件用户密码</param>
        /// <param name="mailOutgoingProtocols">发送邮件服务器协议</param>
        /// <param name="mailOutgoingHosts">发送邮件服务器</param>
        /// <param name="mailOutgoingPorts">发送邮件服务器端口</param>
        /// <param name="mailOutgoingLogins">发送邮件用户名</param>
        /// <param name="mailOutgoingPasswords">发送邮件用户密码</param>
        /// <param name="friendlyNames">用户昵称</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="getMailAtLogins">是否登录后就接收邮件</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData SaveUserMailAccountInfo(
            Guid userID,
            Guid[] ids,
            string[] emails,
            MailProtocol[] mailIncomingProtocols,
            string[] mailIncomingHosts,
            int[] mailIncomingPorts,
            string[] mailIncomingLogins,
            string[] mailIncomingPasswords,
            MailProtocol[] mailOutgoingProtocols,
            string[] mailOutgoingHosts,
            int[] mailOutgoingPorts,
            string[] mailOutgoingLogins,
            string[] mailOutgoingPasswords,
            string[] friendlyNames,
            bool[] getMailAtLogins,
            Guid saveByID,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(userID, "userID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("sm.uspSaveUserMailAccountInfo");

                String tempIDs = ids.Join();
                String tempEmails = emails.Join();
                String tempMailIncomingProtocols = mailIncomingProtocols.Join<MailProtocol>();
                String tempMailIncomingHosts = mailIncomingHosts.Join();
                String tempMailIncomingPorts = mailIncomingPorts.Join();
                String tempMailIncomingLogins = mailIncomingLogins.Join();
                String tempMailIncomingPasswords = mailIncomingPasswords.Join();
                String tempMailOutgoingProtocols = mailOutgoingProtocols.Join<MailProtocol>();
                String tempMailOutgoingHosts = mailOutgoingHosts.Join();
                String tempMailOutgoingPorts = mailOutgoingPorts.Join();
                String tempMailOutgoingLogins = mailOutgoingLogins.Join();
                String tempMailOutgoingPasswords = mailOutgoingPasswords.Join();
                String tempFriendlyNames = friendlyNames.Join();
                String tempGetMailAtLogins = getMailAtLogins.Join();
                String tempUpdateDates = updateDates.Join();

                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempIDs);
                db.AddInParameter(dbCommand, "@Emails", DbType.String, tempEmails);
                db.AddInParameter(dbCommand, "@MailIncomingProtocols", DbType.String, tempMailIncomingProtocols);
                db.AddInParameter(dbCommand, "@MailIncomingHosts", DbType.String, tempMailIncomingHosts);
                db.AddInParameter(dbCommand, "@MailIncomingPorts", DbType.String, tempMailIncomingPorts);
                db.AddInParameter(dbCommand, "@MailIncomingLogins", DbType.String, tempMailIncomingLogins);
                db.AddInParameter(dbCommand, "@MailIncomingPasswords", DbType.String, tempMailIncomingPasswords);
                db.AddInParameter(dbCommand, "@MailOutgoingProtocols", DbType.String, tempMailOutgoingProtocols);
                db.AddInParameter(dbCommand, "@MailOutgoingHosts", DbType.String, tempMailOutgoingHosts);
                db.AddInParameter(dbCommand, "@MailOutgoingPorts", DbType.String, tempMailOutgoingPorts);
                db.AddInParameter(dbCommand, "@MailOutgoingLogins", DbType.String, tempMailOutgoingLogins);
                db.AddInParameter(dbCommand, "@MailOutgoingPasswords", DbType.String, tempMailOutgoingPasswords);
                db.AddInParameter(dbCommand, "@FriendlyNames", DbType.String, tempFriendlyNames);
                db.AddInParameter(dbCommand, "@GetMailAtLogins", DbType.String, tempGetMailAtLogins);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyResultData result = db.ManyResult(dbCommand);
                syncContactsService.SyncCorpContacts("user_SaveUserMailAccountInfo");
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
        /// 删除邮件帐号
        /// </summary>
        /// <param name="ids">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDates">数据版本</param>
        public void RemoveUserMailAccount(
            Guid[] ids,
            DateTime?[] updateDates,
            Guid removeByID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("sm.uspRemoveUserMailAccount");

                string tempIDs = ids.Join();
                string tempUpdateDates = updateDates.Join();

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempIDs);
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                db.ExecuteNonQuery(dbCommand);
                syncContactsService.SyncCorpContacts("user_RemoveUserMailAccount");
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

        public List<MailAccount> GetMailAccountList(
            Guid? organizationID,
            Guid? jobID,
            Guid? roleID,
            string userName,
            string email,
            bool? isValid,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("sm.uspGetMailAccountList");

                db.AddInParameter(dbCommand, "@OrganizationID", DbType.Guid, organizationID);
                db.AddInParameter(dbCommand, "@JobID", DbType.Guid, jobID);
                db.AddInParameter(dbCommand, "@RoleID", DbType.Guid, roleID);
                db.AddInParameter(dbCommand, "@UserName", DbType.String, userName);
                db.AddInParameter(dbCommand, "@EMail", DbType.String, email);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int16, maxRecords);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) return new List<MailAccount>();

                List<MailAccount> results = (from b in ds.Tables[0].AsEnumerable()
                                             select new MailAccount
                                             {
                                                 ID = b.Field<Guid>("ID"),
                                                 EMail = b.Field<String>("Email"),
                                                 UserName = b.Field<String>("UserName"),
                                                 UserID = b.Field<Guid>("UserID"),
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
        /// 获取用户的邮件帐号列表
        /// </summary>
        /// <param name="userIDs">用户ID</param>
        /// <returns>返回用户的邮件帐号列表</returns>
        public List<UserMailAccountList> GetUserMailAccountList(Guid[] userIDs)
        {
            ArgumentHelper.AssertGuidNotEmpty(userIDs, "userIDs");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("sm.uspGetUserMailAccountList");

                string tempUserIDs = userIDs.Join();
                db.AddInParameter(dbCommand, "@UserIDs", DbType.String, tempUserIDs);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) return new List<UserMailAccountList>();

                List<UserMailAccountList> results = (from b in ds.Tables[0].AsEnumerable()
                                                     select new UserMailAccountList
                                                     {
                                                         ID = b.Field<Guid>("ID"),
                                                         Email = b.Field<String>("Email"),
                                                         FriendlyName = b.Field<String>("FriendlyName"),
                                                         GetMailAtLogin = b.Field<Boolean>("GetMailAtLogin"),
                                                         IsDefault = b.Field<Boolean>("IsDefault"),
                                                         IsValid = b.Field<Boolean>("IsValid"),
                                                         MailIncomingHost = b.Field<String>("MailIncomingHost"),
                                                         MailIncomingLogin = b.Field<String>("MailIncomingLogin"),
                                                         MailIncomingPassword = b.Field<String>("MailIncomingPassword"),
                                                         MailIncomingPort = b.Field<int>("MailIncomingPort"),
                                                         MailIncomingProtocol = (MailProtocol)b.Field<byte>("MailIncomingProtocol"),
                                                         MailOutgoingPassword = b.Field<String>("MailOutgoingPassword"),
                                                         MailOutgoingHost = b.Field<String>("MailOutgoingHost"),
                                                         MailOutgoingLogin = b.Field<String>("MailOutgoingLogin"),
                                                         MailOutgoingPort = b.Field<int>("MailOutgoingPort"),
                                                         MailOutgoingProtocol = (MailProtocol)b.Field<byte>("MailOutgoingProtocol"),
                                                         UserID = b.Field<Guid>("UserID"),
                                                         UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                         CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified)
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
        /// 更改用户状态
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeById">修改人ID</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>/返回SingleResultData</returns>
        public SingleResultData ChangeUserState(
            Guid userId,
            bool isValid,
            Guid changeById,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(userId, "userId");
            ArgumentHelper.AssertGuidNotEmpty(changeById, "changeById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspChangeUserState");

                db.AddInParameter(command, "@ID", DbType.Guid, userId);
                db.AddInParameter(command, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(command, "@ChangeByID", DbType.Guid, changeById);
                db.AddInParameter(command, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResultData result = db.SingleResult(command);
                syncContactsService.SyncCorpContacts("user_ChangeUserState");
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
        /// 根据保留用户ID获取被合并用户的日志列表
        /// </summary>
        /// <param name="preserveUserId">保留用户ID</param>
        /// <returns>获取被合并用户的日志列表</returns>
        public List<UserList> GetMergeUserList(Guid preserveUserId)
        {
            ArgumentHelper.AssertGuidNotEmpty(preserveUserId, "preserveUserId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetMergeUserList");

                db.AddInParameter(command, "@PreserveUserID", DbType.Guid, preserveUserId);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<UserList>();
                }

                List<UserList> results = (from b in ds.Tables[0].AsEnumerable()
                                          select new UserList
                                          {
                                              ID = b.Field<Guid>("ID"),
                                              Code = b.Field<string>("Code"),
                                              CName = b.Field<string>("CName"),
                                              EName = b.Field<string>("EName"),
                                              Gender = b.Field<bool>("Sex") ? GenderType.Male : GenderType.Female,
                                              EMail = b.Field<string>("Email"),
                                              Tel = b.Field<string>("Tel"),
                                              Fax = b.Field<string>("Fax"),
                                              Mobile = b.Field<string>("Mobile"),
                                              IsValid = b.Field<bool>("IsValid"),
                                              CreateBy = b.Field<string>("CreateBy"),
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
        /// 合并用户
        /// </summary>
        /// <param name="mergeUserIds">要被合并的用户列表ID</param>
        /// <param name="preserveUserId">保留的用户Id</param>
        /// <param name="mergeById">合并人ID</param>
        /// <param name="updateDates">版本(控制并发冲突)</param>
        /// <returns>/返回ManyResultData</returns>
        public ManyResultData MergeUser(
            Guid[] mergeUserIds,
            Guid preserveUserId,
            Guid mergeById,
            DateTime?[] updateDates)
        {
            ArgumentHelper.AssertGuidNotEmpty(mergeUserIds, "mergeUserIds");
            ArgumentHelper.AssertGuidNotEmpty(preserveUserId, "preserveUserId");
            ArgumentHelper.AssertGuidNotEmpty(mergeById, "mergeById");

            try
            {


                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspMergeUser");

                string tempMergeUserIds = mergeUserIds.Join();
                string tempUpdateDates = updateDates.Join();

                db.AddInParameter(command, "@IDs", DbType.String, tempMergeUserIds);
                db.AddInParameter(command, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(command, "@PreservedID", DbType.Guid, preserveUserId);
                db.AddInParameter(command, "@MergeByID", DbType.Guid, mergeById);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyResultData result = db.ManyResult(command);
                syncContactsService.SyncCorpContacts("user_MergeUser");
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
        /// 取消合并用户
        /// </summary>
        /// <param name="ids">用户ID列表</param>
        /// <param name="cancelById">取消人ID</param>
        /// <param name="updateDates">版本(控制并发冲突)</param>
        /// <returns>/返回SingleResultData</returns>
        public SingleResultData CancelMergeUser(
            Guid[] ids,
            DateTime?[] updateDates,
            Guid cancelById)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");
            ArgumentHelper.AssertGuidNotEmpty(cancelById, "cancelById");
            ArgumentHelper.AssertArrayLengthMatch(ids, updateDates);

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspCancelMergeUser");

                string tempIds = ids.Join();
                string tempUpdateDates = updateDates.Join();

                db.AddInParameter(command, "@IDs", DbType.String, tempIds);
                db.AddInParameter(command, "@CancelByID", DbType.Guid, cancelById);
                db.AddInParameter(command, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResultData result = db.SingleResult(command);
                syncContactsService.SyncCorpContacts("user_CancelMergeUser");
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
        /// 获取用户所挂公司列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="type">类型</param>
        /// <returns>返回用户所挂公司列表</returns>
        public List<OrganizationList> GetUserCompanyList(Guid userId, OrganizationType? type)
        {
            ArgumentHelper.AssertGuidNotEmpty(userId, "userId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetUserOrganizationList");

                db.AddInParameter(command, "@UserID", DbType.Guid, userId);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.AddInParameter(command, "@Type", DbType.Int16, type);

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
                                                      CreateBy = b.Field<string>("CreateBy"),
                                                      CreateDate = b.Field<DateTime>("CreateDate"),
                                                      UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                      HierarchyCode = b.Field<string>("HierarchyCode"),
                                                      FullName = b.Field<string>("FullName"),
                                                      IsDefault = b.Field<bool>("IsDefault"),
                                                      IsDirty = false
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
        /// 获取用户所挂公司树形列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>返回用户所挂公司树形列表</returns>
        public List<UserOrganizationTreeList> GetUserOrganizationTreeList(Guid userId)
        {
            ArgumentHelper.AssertGuidNotEmpty(userId, "userId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetUserOrganizationTreeList");

                db.AddInParameter(command, "@UserID", DbType.Guid, userId);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<UserOrganizationTreeList>();
                }

                List<UserOrganizationTreeList> results = (from b in ds.Tables[0].AsEnumerable()
                                                          select new UserOrganizationTreeList
                                                          {
                                                              ID = b.Field<Guid>("ID"),
                                                              Code = b.Field<string>("Code"),
                                                              CShortName = b.Field<string>("CName"),
                                                              EShortName = b.Field<string>("EName"),
                                                              Type = (OrganizationType)b.Field<byte>("Type"),
                                                              ParentID = b.Field<Guid?>("ParentID"),
                                                              IsValid = b.Field<bool>("IsValid"),
                                                              CreateBy = b.Field<string>("CreateBy"),
                                                              CreateDate = b.Field<DateTime>("CreateDate"),
                                                              UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                              HierarchyCode = b.Field<string>("HierarchyCode"),
                                                              FullName = b.Field<string>("FullName"),
                                                              IsDefault = b.Field<bool>("IsDefault"),
                                                              HasPermission = b.Field<bool>("HasPermission"),
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


        /// <summary>
        /// 获取用户职位列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>返回用户职位列表</returns>
        public List<User2OrganizationJobList> GetUser2OrganizationJobList(Guid userId)
        {
            ArgumentHelper.AssertGuidNotEmpty(userId, "userId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetUserOrganizationJobList");

                db.AddInParameter(command, "@UserID", DbType.Guid, userId);
                db.AddInParameter(command, "@IsValid", DbType.Boolean, true);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<User2OrganizationJobList>();
                }

                List<User2OrganizationJobList> results = (from b in ds.Tables[0].AsEnumerable()
                                                          select new User2OrganizationJobList
                                                          {
                                                              ID = b.Field<Guid>("ID"),
                                                              UserID = userId,
                                                              OrganizationJobID = b.Field<Guid>("OrganizationJobID"),
                                                              OrganizationJobName = b.Field<string>("OrganizationJobName"),
                                                              IsDefault = b.Field<bool>("IsDefault"),
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
        /// 设置用户职位
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="ids">用户职位关系列表</param>
        /// <param name="organizationJobIds">组织结构列表</param>
        /// <param name="updateDates">版本(控制并发冲突)</param>
        /// <param name="setById">修改人ID </param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData SetUserOrganizationJob(
            Guid userId,
            Guid[] ids,
            Guid[] organizationJobIds,
            bool[] isDefaults,
            Guid setById)
        {
            ArgumentHelper.AssertGuidNotEmpty(userId, "userId");
            ArgumentHelper.AssertGuidNotEmpty(setById, "setById");

            try
            {
                string tempIDs = ids.Join();
                string tempOrganizationJobIDs = organizationJobIds.Join();
                string tempIsDefaults = isDefaults.Join();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspSetUserOrganizationJob");

                db.AddInParameter(command, "@UserID", DbType.Guid, userId);
                db.AddInParameter(command, "@IDs", DbType.String, tempIDs);
                db.AddInParameter(command, "@OrganizationJobIDs", DbType.String, tempOrganizationJobIDs);
                db.AddInParameter(command, "@IsDefaults", DbType.String, tempIsDefaults);
                db.AddInParameter(command, "@SetByID", DbType.Guid, setById);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyResultData result = db.ManyResult(command);
                syncContactsService.SyncCorpContacts("user_SetUserOrganizationJob");
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
        /// 设置默认职位
        /// </summary>
        /// <param name="userOrganizationJobId">用户职位关系ID</param>
        /// <param name="setById">设置人ID</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>返回ManyResultData</returns>
        public ManyResultData SetUserDefaultOrganizationJob(
            Guid userOrganizationJobId,
            Guid setById,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(userOrganizationJobId, "userId");
            ArgumentHelper.AssertGuidNotEmpty(setById, "setById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspSetUserDefaultOrganizationJob");

                db.AddInParameter(command, "@ID", DbType.Guid, userOrganizationJobId);
                db.AddInParameter(command, "@SetByID", DbType.Guid, setById);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                ManyResultData result = db.ManyResult(command);
                syncContactsService.SyncCorpContacts("user_SetUserDefaultOrganizationJob");
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
        /// 删除用户职位
        /// </summary>
        /// <param name="userOrganizationJobIds">用户职位关系列表</param>
        /// <param name="updateDates">版本(控制并发冲突)</param>
        /// <param name="removeById">删除人ID</param>
        /// <returns>返回SingleResultData</returns>
        public void RemoveUserOrganizationJob(
            Guid[] userOrganizationJobIds,
            DateTime?[] updateDates,
             Guid removeById)
        {
            ArgumentHelper.AssertGuidNotEmpty(userOrganizationJobIds, "userOrganizationJobIds");
            ArgumentHelper.AssertGuidNotEmpty(removeById, "removeById");
            ArgumentHelper.AssertArrayLengthMatch(userOrganizationJobIds, updateDates);

            try
            {
                string tempIDs = userOrganizationJobIds.Join();
                string tempUpdateDates = updateDates.Join();

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspRemoveUserOrganizationJob");

                db.AddInParameter(command, "@IDs", DbType.String, tempIDs);
                db.AddInParameter(command, "@RemoveByID", DbType.Guid, removeById);
                db.AddInParameter(command, "@UpdateDates", DbType.String, tempUpdateDates);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                db.ExecuteNonQuery(command);
                syncContactsService.SyncCorpContacts("user_RemoveUserOrganizationJob");
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
        /// 获取用户交接列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="isEnglsih">是否英文</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回用户交接列表</returns>
        public List<UserConnectionList> GetUserConnectionList(
            Guid userId,
            DateTime fromDate,
            DateTime toDate,
            bool? isValid,
            bool isEnglsih,
            int maxRecords)
        {
            ArgumentHelper.AssertGuidNotEmpty(userId, "userId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetUserConnectionList");

                db.AddInParameter(command, "@UserID", DbType.Guid, userId);
                db.AddInParameter(command, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(command, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(command, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, isEnglsih);
                db.AddInParameter(command, "@MaxRecords", DbType.Int32, maxRecords);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<UserConnectionList>();
                }

                List<UserConnectionList> results = (from b in ds.Tables[0].AsEnumerable()
                                                    select new UserConnectionList
                                                    {
                                                        ID = b.Field<Guid>("ID"),
                                                        UserName = b.Field<string>("UserName"),
                                                        ConnectUserName = b.Field<string>("ConnectUserName"),
                                                        IsValid = b.Field<bool>("IsValid"),
                                                        CreateBy = b.Field<string>("CreateBy"),
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
        /// 获取接交日志详细信息
        /// </summary>
        /// <param name="userConnectionLogId">交接日志ID</param>
        /// <returns>返回交接日志列表</returns>
        public UserConnectionInfo GetUserConnectionInfo(Guid userConnectionLogId)
        {
            ArgumentHelper.AssertGuidNotEmpty(userConnectionLogId, "userConnectionLogId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetUserConnectionInfo");

                db.AddInParameter(command, "@ID", DbType.Guid, userConnectionLogId);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new UserConnectionInfo();
                }

                UserConnectionInfo result = (from b in ds.Tables[0].AsEnumerable()
                                             select new UserConnectionInfo
                                             {
                                                 ID = b.Field<Guid>("ID"),
                                                 UserID = b.Field<Guid>("UserID"),
                                                 UserName = b.Field<string>("UserName"),
                                                 ConnectUserID = b.Field<Guid>("ConnectUserID"),
                                                 ConnectUserName = b.Field<string>("ConnectUserName"),
                                                 IsValid = b.Field<bool>("IsValid"),
                                                 CreateById = b.Field<Guid>("CreateByID"),
                                                 CreateBy = b.Field<string>("CreateBy"),
                                                 CreateDate = b.Field<DateTime>("CreateDate"),
                                                 UpdateDate = b.Field<DateTime?>("UpdateDate"),
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
        /// 交接用户工作
        /// </summary>
        /// <param name="id">如果id==null或则id==Guid.Empty则新增，否则修改</param>
        /// <param name="fromUserId">交接人</param>
        /// <param name="toUserId">接替工作人</param>
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <param name="setById">设置人</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>/返回SingleResultData </returns>
        public SingleResultData UserConnection(
            Guid? id,
            Guid fromUserId,
            Guid toUserId,
            DateTime fromDate,
            DateTime toDate,
            Guid setById,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(fromUserId, "fromUserId");
            ArgumentHelper.AssertGuidNotEmpty(toUserId, "toUserId");
            ArgumentHelper.AssertGuidNotEmpty(setById, "setById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspUserConnection");

                db.AddInParameter(command, "@ID", DbType.Guid, id);
                db.AddInParameter(command, "@FromUserID", DbType.Guid, fromUserId);
                db.AddInParameter(command, "@ToUserID", DbType.Guid, toUserId);
                db.AddInParameter(command, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(command, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(command, "@SetByID", DbType.Guid, setById);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResultData result = db.SingleResult(command);
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
        /// 改变交接状态

        /// </summary>
        /// <param name="userConnectionId">交接日志ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeById">改变人ID</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>/返回SingleResultData </returns>
        public SingleResultData ChangeUserConnectionState(
            Guid userConnectionId,
            bool isValid,
            Guid changeById,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(userConnectionId, "userConnectionId");
            ArgumentHelper.AssertGuidNotEmpty(changeById, "changeById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspChangeUserConnectionState");

                db.AddInParameter(command, "@UserConnectionID", DbType.Guid, userConnectionId);
                db.AddInParameter(command, "@IsValid", DbType.Boolean, isValid);
                db.AddInParameter(command, "@ChangeByID", DbType.Guid, changeById);
                db.AddInParameter(command, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResultData result = db.SingleResult(command);
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
        /// 判断两个用户是否上下属关系
        /// </summary>
        /// <param name="underlingUserId">下属用户ID</param>
        /// <param name="seniorUserId">上司用户ID</param>
        /// <returns>如果是上下属关系返回true，否则返回fale</returns>
        public bool IsUnderlingUser(
            Guid underlingUserId,
            Guid seniorUserId)
        {
            ArgumentHelper.AssertGuidNotEmpty(underlingUserId, "underlingUserId");
            ArgumentHelper.AssertGuidNotEmpty(seniorUserId, "seniorUserId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspIsUnderlingUser");

                db.AddInParameter(command, "@UnderlingUserId", DbType.Guid, underlingUserId);
                db.AddInParameter(command, "@SeniorUserId", DbType.Guid, seniorUserId);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.AddOutParameter(command, "@IsUnderlingUser", DbType.Boolean, 2);
                db.ExecuteNonQuery(command);

                bool tempIsUnderlingUser = (bool)db.GetParameterValue(command, "@IsUnderlingUser");
                return tempIsUnderlingUser;
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
        ///    检测指定用户的账号是否被其它用户使用
        /// </summary>
        /// <param name="userId">   用户ID</param>
        /// <param name="code">    账号</param>
        /// <returns>   如果存在返回true,否则返回false</returns>
        public bool CheckUserCodeExsit(
            Guid userId,
            string code)
        {
            ArgumentHelper.AssertGuidNotEmpty(userId, "userId");
            ArgumentHelper.AssertStringNotEmpty(code, "code");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspCheckUserCodeExist");

                db.AddInParameter(command, "@ID", DbType.Guid, userId);
                db.AddInParameter(command, "@Code", DbType.String, code);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.AddOutParameter(command, "@IsExist", DbType.Boolean, 2);
                db.ExecuteNonQuery(command);

                bool tempIsExist = (bool)db.GetParameterValue(command, "@IsExist");
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

        /// <summary>
        /// 更改用户密码
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        /// <param name="changeById">更改人ID</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ChangeUserPassword(
            Guid userId,
            string oldPassword,
            string newPassword,
            Guid changeById,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(userId, "userId");
            ArgumentHelper.AssertGuidNotEmpty(changeById, "changeById");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspChangeUserPassword");

                db.AddInParameter(command, "@ID", DbType.Guid, userId);
                db.AddInParameter(command, "@OldPassword", DbType.String, oldPassword);
                db.AddInParameter(command, "@NewPassword", DbType.String, newPassword);
                db.AddInParameter(command, "@ChangeByID", DbType.Guid, changeById);
                db.AddInParameter(command, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResultData result = db.SingleResult(command);
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
        /// 更改用户邮箱密码
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="newPassword">新密码</param>
        public SingleResultData ChangedUserMailPassword(Guid userid, string newPassword)
        {
            ArgumentHelper.AssertGuidNotEmpty(userid, "userId");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommand("sm.uspChangedUserMailPassword");

                db.AddInParameter(command, "@UserID", DbType.Guid, userid);
                db.AddInParameter(command, "@Password", DbType.String, newPassword);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                SingleResultData result = db.SingleResult(command);
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
        /// 工作流新增用户
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="sex">性别</param>
        /// <param name="organizationID">部门</param>
        /// <param name="jobID">职位</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <param name="birthday">生日</param>
        /// <param name="culture">学历</param>
        /// <param name="emolumentDate">入职计薪日期</param>
        /// <param name="familyAddress">户口所在地</param>
        /// <param name="nativePlace">籍贯</param>
        /// <param name="identityCard">身份证号</param>
        /// <param name="specialty">专业</param>
        /// <param name="email">邮箱</param>
        /// <param name="ename">英文名</param>
        /// <param name="phone">手机</param>
        /// <param name="tel">电话</param>
        /// <returns></returns>
        public SingleResultData SaveUserInfoForWF(
                     string name,
                     string ename,
                     string tel,
                     string phone,
                     string email,
                     string sex,
                     Guid organizationID,
                     Guid jobID,
                     DateTime? birthday,
                     string nativePlace,
                     string familyAddress,
                     string culture,
                     string specialty,
                     string identityCard,
                     DateTime? emolumentDate,
                     Guid saveByID)
        {


            Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("sm.uspSaveUserForWF");

            bool isSex = false;

            if (sex.Contains("男") || sex.ToLower().Contains("man") || sex.ToLower().Contains("0"))
            {
                isSex = true;
            }


            db.AddInParameter(command, "@Name", DbType.String, name);
            db.AddInParameter(command, "@EName", DbType.String, ename);
            db.AddInParameter(command, "@Tel", DbType.String, tel);
            db.AddInParameter(command, "@Phone", DbType.String, phone);
            db.AddInParameter(command, "@EMail", DbType.String, email);
            db.AddInParameter(command, "@Sex", DbType.Boolean, isSex);
            db.AddInParameter(command, "@OrganizationID", DbType.Guid, organizationID);
            db.AddInParameter(command, "@JobID", DbType.Guid, jobID);
            db.AddInParameter(command, "@Birthday", DbType.DateTime, birthday);
            db.AddInParameter(command, "@NativePlace", DbType.String, nativePlace);
            db.AddInParameter(command, "@FamilyAddress", DbType.String, familyAddress);
            db.AddInParameter(command, "@Culture", DbType.String, culture);
            db.AddInParameter(command, "@Specialty", DbType.String, specialty);
            db.AddInParameter(command, "@IdentityCard", DbType.String, identityCard);
            db.AddInParameter(command, "@EmolumentDate", DbType.DateTime, emolumentDate);
            db.AddInParameter(command, "@SaveByID", DbType.Guid, saveByID);
            db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);


            try
            {
                db.ExecuteNonQuery(command);

                SingleResultData result = new SingleResultData();
                syncContactsService.SyncCorpContacts("user_SaveUserInfoForWF");
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
        public string GetUserEmailPassword(string emailAddress)
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("sm.uspGetUserEmailPassword");
            db.AddInParameter(command, "@EmailAddress", DbType.String, emailAddress);

            db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);
            try
            {
                return db.ExecuteScalar(command).ToString();
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
        public ManyResult GetUserNamesByID(Guid[] userIds)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("sm.uspGetUserNameByID");
            db.AddInParameter(dbCommand, "@UserIds", DbType.String, userIds.Join());
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
            try
            {
                return db.ManyResult(dbCommand, new String[] { "ID", "CName", "EName" });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 得到海外客服信息列表
        /// </summary>
        /// <param name="OperationId"></param>
        /// <returns></returns>
        public List<UserList> GetOverseasSalesList(string roleNames)
        {
            List<UserList> userList = GetUnderlingUserList(null, new string[] { roleNames }, null, true);
            return userList;
        }

        /// <summary>
        /// 返回当前人员的上级
        /// </summary>
        /// <param name="UserID">当前人员</param>
        /// <returns></returns>
        public List<UserDetailInfo> GetSuperior(Guid UserID)
        {
            List<UserDetailInfo> userList = new List<UserDetailInfo>();

            string sql = "SELECT USERID  FROM sm.VOrganizationJobUsers";
            sql = sql + " WHERE JobID IN (SELECT J.ID FROM sm.Jobs AS j ";
            sql = sql + " WHERE j.ID IN (SELECT O.JobID FROM sm.Organization2Jobs AS o WHERE O.OrganizationID=(SELECT V.OrganizationID FROM sm.VOrganizationJobUsers AS V ";
            sql = sql + " WHERE V.UserID='" + UserID + "' AND V.IsDefault=1 AND j.CName LIKE '%经理%')))";
            sql = sql + " AND OrganizationID=(SELECT V.OrganizationID FROM sm.VOrganizationJobUsers AS V ";
            sql = sql + " WHERE V.UserID='" + UserID + "' AND V.IsDefault=1) AND UserIsValid=1";
            Database db = DatabaseFactory.CreateDatabase();
            DataSet ds = db.ExecuteDataSet(CommandType.Text, sql);
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Guid userid = new Guid(dt.Rows[i]["USERID"].ToString());
                    UserDetailInfo user = GetUserDetailInfo(userid);
                    if (userList.Any())
                    {
                        //去除重复项的操作
                        if (userList.FirstOrDefault(n => n.ID == user.ID) == null)
                        {
                            userList.Add(user);
                        }
                    }
                    else
                    {
                        userList.Add(user);
                    }
                }
            }
            return userList;
        }

        /// </summary>
        /// 保存邮件签名
        /// </summary>
        /// <param name="accountID">邮箱ID</param>
        /// <param name="type">签名类型</param>
        /// <param name="signature">签名</param>
        public void SaveEmailAccountSignature(Guid accountID, int type, string signature, Guid savebyid)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspSaveEmailAccountSignature");

                db.AddInParameter(command, "@AccountID", DbType.Guid, accountID);
                db.AddInParameter(command, "@Type", DbType.Int32, type);
                db.AddInParameter(command, "@Signature", DbType.String, signature);
                db.AddInParameter(command, "@SaveByID", DbType.Guid, savebyid);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.ExecuteNonQuery(command);
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


        /// </summary>
        /// 获取邮箱签名
        /// </summary>
        /// <param name="releaseid">邮箱ID</param>
        /// <param name="EmailAddress">邮箱地址</param>
        public List<EmailAccountSignature> GetEmailAccountSignature(Guid? AccountID, string EmailAddress)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetEmailAccountSignature");

                db.AddInParameter(command, "@AccountID", DbType.Guid, AccountID);
                db.AddInParameter(command, "@EmailAddress", DbType.String, EmailAddress);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                DataSet ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<EmailAccountSignature> results = (from b in ds.Tables[0].AsEnumerable()
                                                       select new EmailAccountSignature
                                           {
                                               Id = b.Field<Guid>("ID"),
                                               AccountID = b.Field<Guid>("AccountID"),
                                               Type = b.Field<int>("type"),
                                               Signature = b.Field<string>("Signature"),
                                               CreateById = b.Field<Guid>("CreateBy"),
                                               CreateByDate = b.Field<DateTime>("CreateDate"),
                                               UpdateById = b.Field<Guid?>("UpdateBy"),
                                               UpdateByDate = b.Field<DateTime?>("UpdateDate"),
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

    }
}
