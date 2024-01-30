using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary;
using ICP.OA.ServiceInterface.DataObjects;
using System.Data.SqlClient;
using System.Data;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.OA.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using sm = ICP.Sys.ServiceInterface;
using System.ServiceModel;
using ICP.SubscriptionPublish.ServiceInterface;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Net;
namespace ICP.OA.ServiceComponent
{
    /// <summary>
    /// 公告服务
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class BulletinService : PublishService<ISubscriptionPublishNotifyService>, IBulletinService
    {
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

        /// <summary>
        /// 通知公告发布
        /// <param name="data">公告数据</param>
        /// <param name="notifyDepartmentIds"></param>
        /// </summary>
        public void Notify(BulletinData data, List<Guid> notifyDepartmentIds)
        {
            string methodName = "Notify";
            try
            {
                NoticefyCorpBulletin(data, notifyDepartmentIds);
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog("CSPNotify", CommonHelper.BuildExceptionString(ex));
            }
            FireEvent(methodName, notifyDepartmentIds, data);
        }
        #region 触发微信企业号公告

        /// <summary>
        /// 触发微信企业号公告
        /// </summary>
        /// <param name="data"></param>
        /// <param name="notifyDepartmentIds"></param>
        private void NoticefyCorpBulletin(BulletinData data, List<Guid> notifyDepartmentIds)
        {
            try
            {
                string url = INIConfig.IniReadValue(FrameworkCommandConstants.SECTION_CSPAPI, OACommandConstants.CSPAPI_SYNCCORPBULLETIN);// AppSettingReader.Current.GetAppSettingValue("SyncCorpBulletin");
                if (string.IsNullOrEmpty(url))
                {
                    throw new Exception("微信企业号公告接口地址为空[SyncCorpBulletin]");
                }
                //触发微信企业号公告
                string DataString = "";
                DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(BulletinData));
                using (MemoryStream ms = new MemoryStream())
                {
                    json.WriteObject(ms, data);
                    DataString = Encoding.UTF8.GetString(ms.ToArray());
                }
                DataString += "_*#&_";
                DataContractJsonSerializer jsonList = new DataContractJsonSerializer(typeof(List<Guid>));
                using (MemoryStream ms = new MemoryStream())
                {
                    jsonList.WriteObject(ms, notifyDepartmentIds);
                    DataString = DataString + Encoding.UTF8.GetString(ms.ToArray());
                }
                //post 
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                myHttpWebRequest.Method = "POST";//设置请求的方法
                myHttpWebRequest.Accept = "*/*";//设置Accept标头的值
                myHttpWebRequest.Timeout = 3000;
                byte[] ByteData = Encoding.UTF8.GetBytes(DataString);
                using (Stream stream = myHttpWebRequest.GetRequestStream())
                {
                    stream.Write(ByteData, 0, ByteData.Length);
                }
                RequestState myRequestState = new RequestState();
                myRequestState.request = myHttpWebRequest;
                IAsyncResult result =
                (IAsyncResult)myHttpWebRequest.BeginGetResponse(
                    new AsyncCallback(RespCallback), myRequestState);
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog(
                    ex.Message + "\r\n" + data.CreateTime.ToString(CultureInfo.InvariantCulture)
                    + "FromTime" + data.FromTime.ToString(CultureInfo.InvariantCulture)
                    + "ToTime" + data.ToTime.ToString(CultureInfo.InvariantCulture) + "\r\n"
                    + ex.StackTrace);
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
        #endregion

        #region 初始化 和本地方法

        /// <summary>
        /// Spl的连接字符串
        /// </summary>
        string _SqlConnectionString = string.Empty;
        sm.IUserService userService;
        public BulletinService(string sqlConnectionString, sm.IUserService _userService)
        {
            _SqlConnectionString = sqlConnectionString;
            userService = _userService;
        }
        #endregion

        /// <summary>
        /// 编辑公告
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="subject">subject</param>
        /// <param name="content">content</param>
        /// <param name="fromTime">fromTime</param>
        /// <param name="toTime">toTime</param>
        /// <param name="departmentIds">departmentIds</param>
        /// <param name="departmentName">departmentName</param>
        /// <param name="userId">userId</param>
        /// <param name="priority">priority</param>
        /// <param name="createTime">createTime</param>
        /// <param name="bulletinType">bulletinType</param>
        /// <returns>Guid</returns>
        public Guid SaveBulletin(Guid? id
                            , string subject
                            , string content
                            , DateTime fromTime
                            , DateTime toTime
                            , Guid[] departmentIds
                            , string departmentName
                            , Guid userId
                            , BulletinPriority priority
                            , Guid bulletinType)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(_SqlConnectionString))
                {
                    string tempDepartmentIds = string.Empty;
                    foreach (var item in departmentIds)
                    {

                        if (tempDepartmentIds.Length > 0) tempDepartmentIds += ";";
                        tempDepartmentIds += item.ToString();
                    }

                    conn.Open();

                    SqlCommand myCommand = new SqlCommand("dbo.uspSaveBulletin", conn);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    //添加输入查询参数、赋予值
                    myCommand.Parameters.Add("@ID", SqlDbType.UniqueIdentifier);
                    myCommand.Parameters.Add("@Subject", SqlDbType.NVarChar);
                    myCommand.Parameters.Add("@Content", SqlDbType.NVarChar);
                    myCommand.Parameters.Add("@DepartmentIds", SqlDbType.NVarChar);
                    myCommand.Parameters.Add("@DepartmentName", SqlDbType.NVarChar);
                    myCommand.Parameters.Add("@FromDate", SqlDbType.DateTime);
                    myCommand.Parameters.Add("@ToDate", SqlDbType.DateTime);
                    myCommand.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier);
                    myCommand.Parameters.Add("@Priority", SqlDbType.Int);
                    myCommand.Parameters.Add("@BulletinType", SqlDbType.UniqueIdentifier);

                    myCommand.Parameters["@ID"].Value = id;
                    myCommand.Parameters["@Subject"].Value = subject;
                    myCommand.Parameters["@Content"].Value = content;
                    myCommand.Parameters["@DepartmentIds"].Value = tempDepartmentIds;
                    myCommand.Parameters["@DepartmentName"].Value = departmentName;
                    myCommand.Parameters["@FromDate"].Value = fromTime;
                    myCommand.Parameters["@ToDate"].Value = toTime;
                    myCommand.Parameters["@UserId"].Value = userId;
                    myCommand.Parameters["@Priority"].Value = (short)priority;
                    myCommand.Parameters["@BulletinType"].Value = bulletinType;

                    DataSet ds = new DataSet();
                    SqlDataAdapter DataAdapter = new SqlDataAdapter();
                    DataAdapter.SelectCommand = myCommand;
                    DataAdapter.Fill(ds, "MainTable");


                    //myCommand.ExecuteNonQuery();

                    if (DataAdapter == null)
                    {
                        return Guid.Empty;
                    }


                    conn.Close();

                    if (ds == null || ds.Tables.Count == 0) return Guid.Empty;

                    Guid result = new Guid(ds.Tables[0].Rows[0][0].ToString());
                    UserInfo userInfo = userService.GetUserInfo(userId);
                    BulletinData data = new BulletinData()
                    {
                        ID = result,
                        BulletinType = bulletinType,
                        Content = content,
                        Subject = subject,
                        Departments = departmentIds.ToList<Guid>(),
                        Priority = priority,
                        PublisherCName = userInfo.CName,
                        PublisherEName = userInfo.EName,
                        CreateTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified),
                        FromTime = fromTime,
                        ToTime = toTime
                    };
                    InnerDoNotify(data, departmentIds.ToList<Guid>());
                    try { }
                    catch (Exception ex)
                    {
                    }
                    return result;
                }

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }
        /// <summary>
        /// 处理公告通知
        /// </summary>
        /// <param name="data"></param>
        /// <param name="notifyDepartmentIds"></param>
        private void InnerDoNotify(BulletinData data, List<Guid> notifyDepartmentIds)
        {
            // WaitCallback fire = (_data) =>
            //      {
            //Notify(_data as BulletinData);
            // };
            // ThreadPool.QueueUserWorkItem(fire, data);
            Notify(data, notifyDepartmentIds);
        }

        /// <summary>
        /// 搜索公告
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="subject">subject</param>
        /// <param name="content">content</param>
        /// <param name="departmentName">departmentName</param>
        /// <param name="dateTimeFrom">dateTimeFrom</param>
        /// <param name="dataTimeTo">dataTimeTo</param>
        /// <param name="maxResults">maxResults</param>
        /// <returns>BulletinData</returns>
        public List<BulletinData> GetBulletins(Guid? userId
            , string subject
            , string content
            , string departmentName
            , DateTime? dateTimeFrom
            , DateTime? dataTimeTo
            , int maxResults)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_SqlConnectionString))
                {
                    StringBuilder command = new StringBuilder();
                    command.Append("exec dbo.uspGetBulletins ");
                    command.Append(string.Format("@subject=\'{0}\' ", subject));
                    command.Append(string.Format(",@content=\'{0}\' ", content));
                    command.Append(string.Format(",@departmentName=\'{0}\' ", departmentName));
                    command.Append(string.Format(",@FromDate=\'{0}\' ", dateTimeFrom));
                    command.Append(string.Format(",@ToDate=\'{0}\' ", dataTimeTo));
                    command.Append(string.Format(",@maxResults=\'{0}\' ", maxResults));
                    command.Append(string.Format(",@IsEnglish=\'{0}\' ", ApplicationContext.Current.IsEnglish));

                    if (userId != null && userId != Guid.Empty)
                    {
                        command.Append(string.Format(",@UserId=\'{0}\' ", userId));
                    }
                    conn.Open();
                    SqlDataAdapter myAdapter = new SqlDataAdapter(command.ToString(), conn);
                    DataSet ds = new DataSet();
                    myAdapter.Fill(ds);
                    conn.Close();

                    if (ds == null || ds.Tables.Count == 0) return null;

                    #region linq

                    List<BulletinData> results = (from b in ds.Tables[0].AsEnumerable()
                                                  select new BulletinData
                                                  {
                                                      Content = b.Field<String>("Body"),
                                                      BulletinType = b.Field<Guid>("BulletinType"),
                                                      BulletinTypeCName = b.Field<String>("BulletinTypeCName"),
                                                      BulletinTypeEName = b.Field<String>("BulletinTypeEName"),
                                                      CreateTime = b.Field<DateTime>("CreateTime"),
                                                      FromTime = b.Field<DateTime>("FromTime"),
                                                      ID = b.Field<Guid>("ID"),
                                                      Priority = (BulletinPriority)b.Field<int>("Priority"),
                                                      StructureNodeCName = b.Field<string>("ReaderDescription"),
                                                      //StructureNodeEName = b.StructureNode == null ? string.Empty : b.StructureNode.EName,
                                                      //StructureNodeID = b.StructureNodeID,
                                                      Subject = b.Field<string>("Subject"),
                                                      ToTime = b.Field<DateTime>("ToTime"),
                                                      Publisher = b.Field<string>("Publisher"),
                                                      Departments = (from r in ds.Tables[1].AsEnumerable()
                                                                     where r.Field<Guid>("BulletinID") == b.Field<Guid>("ID")
                                                                     select r.Field<Guid>("DepartmentID")
                                                                 ).ToList()
                                                  }).ToList();

                    #endregion


                    return results;
                }

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="ID">ID</param>
        public void DeleteBulletinByID(Guid ID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_SqlConnectionString))
                {

                    StringBuilder command = new StringBuilder();
                    command.Append("exec dbo.uspRemoveBulletins ");
                    command.Append(string.Format("@ID=\'{0}\' ", ID));
                    conn.Open();
                    SqlCommand myCommand = new SqlCommand(command.ToString(), conn);
                    myCommand.ExecuteNonQuery();

                    conn.Close();
                }

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }


        #region 特定的2.0基础数据实现

        /// <summary>
        /// 获取2.0中的BulletinType
        /// </summary>
        /// <returns>BulletinTypeData</returns>
        public List<BulletinTypeData> GetBulletinTypeDatas()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_SqlConnectionString))
                {

                    string command = "select Id,Code,CName ,EName from dbo.T_PUB_DictCode where CodeType=33";
                    conn.Open();
                    SqlDataAdapter myAdapter = new SqlDataAdapter(command, conn);
                    DataSet ds = new DataSet();
                    myAdapter.Fill(ds);
                    conn.Close();

                    if (ds == null || ds.Tables.Count == 0) return null;

                    #region linq

                    List<BulletinTypeData> results = (from b in ds.Tables[0].AsEnumerable()
                                                      select new BulletinTypeData
                                                      {
                                                          ID = b.Field<Guid>("ID"),
                                                          CName = b.Field<string>("CName"),
                                                          Code = b.Field<string>("Code"),
                                                          EName = b.Field<string>("EName"),
                                                      }).ToList();

                    #endregion


                    return results;
                }

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 获取2.0中的组织结构树
        /// </summary>
        public List<OrganizationTreeData> GetOrganizationTreeData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_SqlConnectionString))
                {

                    string command = "select Id,ParentID,Name as CName,EName from dbo.T_SYS_StructureNode  where IsValid=1";
                    conn.Open();
                    SqlDataAdapter myAdapter = new SqlDataAdapter(command, conn);
                    DataSet ds = new DataSet();
                    myAdapter.Fill(ds);
                    conn.Close();

                    if (ds == null || ds.Tables.Count == 0) return null;

                    #region linq

                    List<OrganizationTreeData> results = (from b in ds.Tables[0].AsEnumerable()
                                                          select new OrganizationTreeData
                                                          {
                                                              ID = b.Field<Guid>("ID"),
                                                              CName = b.Field<string>("CName"),
                                                              ParentID = b.Field<Guid?>("ParentID"),
                                                              EName = b.Field<string>("EName"),
                                                              HasPermission = true
                                                          }).ToList();

                    #endregion

                    OrganizationTreeData tager = results.Find(d => d.ID == d.ParentID);
                    if (tager != null) tager.ParentID = null;

                    return results;
                }

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        #endregion
    }
}
