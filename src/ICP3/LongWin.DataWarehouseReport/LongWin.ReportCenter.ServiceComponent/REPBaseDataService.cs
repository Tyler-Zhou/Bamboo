using System;
using System.Collections.Generic;
using System.Data;
using Agilelabs.Framework.Service;
using LongWin.Framework.Server;
using LongWin.OrganizationStructure.ServiceInterface;
using LongWin.OrganizationStructure.ServiceInterface.DataObjects;
using LongWin.ReportCenter.ServiceInterface;

namespace LongWin.ReportCenter.ServiceComponent
{
    public class REPBaseDataService:IREPBaseDataService
    {
        IDatabaseFactory _databaseFactory;//获取数据服务
        ISessionService _sessionService;//会话服务
        IUserStationService _userStationService;//用户服务
        IOrganizationService _organizationService;//组织结构服务
        IInitializeService _iinitializeService;//获取当前用户信息服务
        ISystemRoleService _systemRoleService;
        string _reportUrl;
        string _reportUser;
        string _reportUserPSW;

        public REPBaseDataService(IDatabaseFactory databaseFactory
            , ISessionService sessionService,
             IUserStationService userStationService
            , IOrganizationService organizationService
            , ISystemRoleService systemRoleService
            , IInitializeService iinitializeService
            , string reportUrl
            , string reportUser
            , string reportUserPSW
            )
        {
            _reportUrl = reportUrl;
            _reportUser = reportUser;
            _reportUserPSW = reportUserPSW;

            _systemRoleService = systemRoleService;
            _reportUrl = reportUrl;

            this._databaseFactory = databaseFactory;
            this._sessionService = sessionService;
            this._userStationService = userStationService;
            this._organizationService = organizationService;
            this._iinitializeService = iinitializeService;
        }
        /// <summary>
        /// 当前用户
        /// </summary> 
        Guid CurrentUserId
        {
            get
            {
                return new Guid(_sessionService.CurrentSession[ServerVariables.CURRENT_USER].ToString());
            }
        }


        #region IREPBaseDataService Members
        /// <summary>
        /// 报表服务器地址
        /// </summary>
        /// <returns></returns>
        public ReportServerInfo GetReportServerUrl()
        {
            return new ReportServerInfo(this._reportUrl, this._reportUser, this._reportUserPSW);
        }
        public System.Data.DataSet GetUserSetByCurrentUser()
        {
            //获取该用户的身份/部门/用户的集合
            List<UserStationData> userStations = this._userStationService.GetUserStations(this.CurrentUserId);
            DataSet users = new DataSet();
            //对每一个部门的身份进行处理
            foreach (UserStationData row in userStations)
            {
                if (row.RoleName.IndexOf("经理") > -1 ||
                    row.RoleName.IndexOf("董事长") > -1
                    || row.RoleName.IndexOf("主管") > -1)
                {
                    //如果是经理

                    if (users.Tables.Count == 0)
                    {
                        users.Tables.Add(this._userStationService.GetUsersByStructureNode(row.DepartmentId,true).ToTable());
                        
                    }
                    else
                    {
                        users.Tables[0].Merge(this._userStationService.GetUsersByStructureNode(row.DepartmentId,true).ToTable());
                    }
                }
                if (row.RoleName.IndexOf("财务") > -1
                    || row.RoleName.IndexOf("计费") > -1
                    || row.RoleName.IndexOf("操作") > -1
                    || row.RoleName.IndexOf("定舱") > -1
                    || row.RoleName.IndexOf("客服") > -1
                    || row.RoleName.IndexOf("文件") > -1)
                {
                    //Guid companyId ;
                    //if (this._organizationService.GetCompanyInfo(new Guid(row["StructureNodeId"].ToString())) != null
                    //    && this._organizationService.GetCompanyInfo(new Guid(row["StructureNodeId"].ToString())).Tables.Count>0
                    //    && this._organizationService.GetCompanyInfo(new Guid(row["StructureNodeId"].ToString())).Tables[0].Rows.Count>0)
                    //{
                    //    companyId = new Guid(this._organizationService.GetCompanyInfo(new Guid(row["StructureNodeId"].ToString())).Tables[0].Rows[0]["Id"].ToString());
                    //}
                    //else
                    //{
                    //    companyId = new Guid(row["StructureNodeId"].ToString());
                    //}
                    //if (users.Tables.Count == 0)
                    //{
                    //    users.Tables.Add(this._organizationService.GetUsersByStructureNode(companyId, true, false));
                    //}
                    //else
                    //{
                    //    users.Tables[0].Merge(this._organizationService.GetUsersByStructureNode(companyId, true, false));
                    //}
                    users = this._userStationService.GetAllUsers(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,true, -1).ToDataSet();
                    break;
                }
            }
            if (users == null || users.Tables.Count == 0 || users.Tables[0].Rows.Count == 0)
            {
                UserInfo user = this._iinitializeService.GetUserInfo();
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(Guid));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Code", typeof(string));
                
                dt.Rows.Add(new object[] { this.CurrentUserId, user.DispalyName, user.GetValue("Code").ToString() });

                users.Tables.Clear();
                users.Tables.Add(dt);
            }

            return users;
        }

        public System.Data.DataSet GetUserSetForAgentDcNoteByCurrentUser()
        {
            //获取该用户的身份/部门/用户的集合

            List<UserStationData> userStations = this._userStationService.GetUserStations(this.CurrentUserId);
            DataSet users = new DataSet();
            //对每一个部门的身份进行处理
            foreach (UserStationData row in userStations)
            {
                if (row.RoleName.IndexOf("经理") > -1 ||
                    row.RoleName.IndexOf("董事长") > -1
                    || row.RoleName.IndexOf("主管") > -1)
                {
                    //如果是经理

                    if (users.Tables.Count == 0)
                    {
                        users.Tables.Add(this._userStationService.GetUsersByStructureNode(row.DepartmentId,true).ToTable());

                    }
                    else
                    {
                        users.Tables[0].Merge(this._userStationService.GetUsersByStructureNode(row.DepartmentId,true).ToTable());
                    }
                }
                if (row.RoleName.IndexOf("财务") > -1
                    || row.RoleName.IndexOf("计费") > -1
                    || row.RoleName.IndexOf("操作") > -1
                    || row.RoleName.IndexOf("定舱") > -1
                    || row.RoleName.IndexOf("客服") > -1
                    || row.RoleName.IndexOf("文件") > -1
                    || row.RoleName.IndexOf("海外拓展") > -1)
                {
                    users = this._userStationService.GetAllUsers(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true, -1).ToDataSet();
                    break;
                }
            }
            if (users == null || users.Tables.Count == 0 || users.Tables[0].Rows.Count == 0)
            {
                UserInfo user = this._iinitializeService.GetUserInfo();
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(Guid));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Code", typeof(string));

                dt.Rows.Add(new object[] { this.CurrentUserId, user.DispalyName, user.GetValue("Code").ToString() });

                users.Tables.Clear();
                users.Tables.Add(dt);
            }
            return users;
        }

        public DataSet GetDevelopersByCurrentUser()
        {
            IDatabase dataBase = this._databaseFactory.CreateDatabase();
            try
            {
                DataSet ds=new DataSet();
                List<UserStationData> userStations = this._userStationService.GetUserStations(this.CurrentUserId);
                string sqlCommand ="";
                foreach (UserStationData row in userStations)
                {
                    sqlCommand =" select distinct UserId,Username,UserCode from IV_REP_DevSales where DeptNodeCode like rtrim('"+row.DepartmentFullCode.Trim()+")%'";
                    ds.Merge(dataBase.ExecuteDataset(CommandType.Text,sqlCommand));
                }
            return ds;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public DataSet GetMKSalesByCurrentUser()
        {
            IDatabase dataBase = this._databaseFactory.CreateDatabase();
            try
            {
                DataSet ds=new DataSet();
                List<UserStationData> userStations = this._userStationService.GetUserStations(this.CurrentUserId);
                string sqlCommand ="";
                foreach (UserStationData row in userStations)
                {
                    sqlCommand = " select distinct UserId,Username,UserCode from IV_REP_MKSales where DeptNodeCode like rtrim('" + row.DepartmentFullCode.Trim() + ")%'";
                    ds.Merge(dataBase.ExecuteDataset(CommandType.Text,sqlCommand));
                }
            return ds;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public DataSet GetCurrentUserStations()
        {
            return  this._userStationService.GetUserStations(this.CurrentUserId).ToDataSet();
        }


        public DataSet GetStructureNodesByCurrentUser()
        {
            List<UserStationData> userStations = this._userStationService.GetUserStations(this.CurrentUserId);
            DataSet StructureNodes = new DataSet();

            Guid[] structureNodeIds = new Guid[userStations.Count]; 

            int i = 0;
            foreach(UserStationData us in userStations)
            {
                if (us.RoleName.IndexOf("财务") > -1
                    || us.RoleName.IndexOf("计费") > -1
                    || us.RoleName.IndexOf("操作") > -1
                    || us.RoleName.IndexOf("定舱") > -1
                    || us.RoleName.IndexOf("客服") > -1
                    || us.RoleName.IndexOf("文件") > -1)
                {
                    //DataSet companyids = this._organizationService.GetCompanyInfo(us.DepartmentId));
                    //if (companyids != null && companyids.Tables[0].Rows.Count > 0)
                    //{
                    //    structureNodeIds[i] = new Guid(this._organizationService.GetCompanyInfo(new Guid(userStations.Tables[0].Rows[i]["StructureNodeId"].ToString())).Tables[0].Rows[0]["Id"].ToString());
                    //}
                    //else
                    //{
                    //    structureNodeIds[i] = new Guid(userStations.Tables[0].Rows[i]["StructureNodeId"].ToString());
                    //}

                    structureNodeIds[i]=us.CompanyId;
                }
                else
                {
                    structureNodeIds[i] = us.DepartmentId;
                }

                i += 1;
            }

            StructureNodes = this._organizationService.GetStructureNodes(structureNodeIds, true, true).ToDataSet();

            DataTable dt = userStations.ToTable().Copy();
            dt.TableName = "UserStation";
            StructureNodes.Tables.Add(dt);

            return StructureNodes;
        }

        public DataSet GetStructureNodesForAgentByCurrentUser()
        {
            List<UserStationData> userStations = this._userStationService.GetUserStations(this.CurrentUserId);
            DataSet StructureNodes = new DataSet();

            Guid[] structureNodeIds = new Guid[userStations.Count];

            int i = 0;
            foreach (UserStationData us in userStations)
            {
                if (us.RoleName.IndexOf("财务") > -1
                    || us.RoleName.IndexOf("计费") > -1
                    || us.RoleName.IndexOf("操作") > -1
                    || us.RoleName.IndexOf("定舱") > -1
                    || us.RoleName.IndexOf("客服") > -1
                    || us.RoleName.IndexOf("文件") > -1)
                {
                    //DataSet companyids = this._organizationService.GetCompanyInfo(new Guid(userStations.Tables[0].Rows[i]["StructureNodeId"].ToString()));
                    //if (companyids != null && companyids.Tables[0].Rows.Count > 0)
                    //{
                    //    structureNodeIds[i] = new Guid(this._organizationService.GetCompanyInfo(new Guid(userStations.Tables[0].Rows[i]["StructureNodeId"].ToString())).Tables[0].Rows[0]["Id"].ToString());
                    //}
                    //else
                    //{
                    //    structureNodeIds[i] = new Guid(userStations.Tables[0].Rows[i]["StructureNodeId"].ToString());
                    //}
                    structureNodeIds[i] = us.CompanyId;
                }
                else if (us.RoleName.IndexOf("海外拓展") > -1)
                {
                    structureNodeIds[i] = new Guid("701ACD43-D49B-422B-83A9-ACB56B696995");
                }
                else
                {
                    structureNodeIds[i] = us.DepartmentId;
                }

                i += 1;
            }

            StructureNodes = this._organizationService.GetStructureNodes(structureNodeIds, true,true).ToDataSet();

            DataTable dt = userStations.ToTable();
            dt.TableName = "UserStation";
            StructureNodes.Tables.Add(dt);

            return StructureNodes;
        }

       
        public DataSet GetCustomerByCondition(string condition)
        {
            //return this._customerService.GetCustomerList(condition);
            condition = " select ID,CName from T_PUB_Customer where IsValid<>0 and IsValid<>6 and " + condition;
            return this._databaseFactory.CreateDatabase().ExecuteDataset(CommandType.Text, condition);
        }
        public DataSet GetRolesByCondition()
        {
            return this._systemRoleService.GetAllSystemRoles().ToDataSet();
        }
        

        public bool GetUserIsManange()
        {
            List<UserStationData> userStations = this._userStationService.GetUserStations(this.CurrentUserId);
            bool isViewShipper = false;
            foreach (UserStationData row in userStations)
            {
                if (row.RoleName.IndexOf("经理") > -1 ||
                    row.RoleName.IndexOf("董事长") > -1
                    || row.RoleName.IndexOf("主管") > -1
                    ||row.RoleName.IndexOf("财务") > -1
                    || row.RoleName.IndexOf("计费") > -1
                    || row.RoleName.IndexOf("操作") > -1
                    || row.RoleName.IndexOf("定舱") > -1
                    || row.RoleName.IndexOf("客服") > -1
                    || row.RoleName.IndexOf("文件") > -1)
                {
                    isViewShipper = true;
                    break;
                }
            }
            return isViewShipper;
        }

        public ReportInfo.TitleInfoDataTable GetReportTitleInfo(Guid structureNodeId)
        {
            //DataSet ds=this._organizationService.GetCompanyInfo(structureNodeId);

            DataSet ds = new DataSet();
            IDatabase dataBase = this._databaseFactory.CreateDatabase();
            ds = dataBase.ExecuteDataset(CommandType.Text, "Select Name=description,EName,Fax,Tel,Address,EAddress FROM T_SYS_StructureNode where id='" + structureNodeId.ToString() + "'");

            ReportInfo.TitleInfoDataTable dt = new ReportInfo.TitleInfoDataTable();
            ReportInfo.TitleInfoRow row = dt.NewTitleInfoRow();
            if (ds.Tables.Count > 0&&ds.Tables[0].Rows.Count > 0)
            {
                row.CompanyName = ds.Tables[0].Rows[0]["Name"].ToString();
                row.CompanyEName = ds.Tables[0].Rows[0]["EName"].ToString();
                row.TelOrFax ="Tel:"+ ds.Tables[0].Rows[0]["Tel"].ToString() + ", Fax:" + ds.Tables[0].Rows[0]["Fax"].ToString();
                row.CompanyAddress = ds.Tables[0].Rows[0]["Address"].ToString();
                row.CompanyEAddress = ds.Tables[0].Rows[0]["EAddress"].ToString();
            }
            else
            {
                row.CompanyName = "深圳市鹏城海物流有限公司";
                row.CompanyEName = "CITY OCEAN LOGISTICS CO.,LTD";
                row.TelOrFax = "Tel:86-755-33339933;Fax:86-755-33958201";
                row.CompanyAddress = "深圳市福田区深南大道4019号航天大厦22F";
               row.CompanyEAddress = "22F.,A BLDG.,AEROSPACE SKYSCRAPER,NO.4019 SHENNAN ROAD,FUTIAN DISTRICT,SHENZHEN,CHINA";
            }
            dt.Rows.Add(row);
            return dt;

        }

        public DataSet GetDeveloperSales()
        {
            IDatabase dataBase = this._databaseFactory.CreateDatabase();
            List<UserStationData> userStations = this._userStationService.GetUserStations(this.CurrentUserId);

            DataSet oset=new DataSet();

            DataTable dt = new DataTable();

            DataColumn column;
            column = new DataColumn("UserId", typeof(Guid));
            dt.Columns.Add(column);
            dt.PrimaryKey = new DataColumn[] { column};

            column = new DataColumn("UserName", typeof(string));
            dt.Columns.Add(column);

            column = new DataColumn("UserCode", typeof(string));
            dt.Columns.Add(column);
            oset.Tables.Add(dt);
            
            
            
            string sqlCommand = "";
            foreach (UserStationData us in userStations)
            {
                if (us.RoleName.IndexOf("经理") > -1
                   || us.RoleName.IndexOf("主管") > -1
                   || us.RoleName.IndexOf("董事长") > -1)
                {
                    sqlCommand = " select distinct UserId,SalesCname AS Username,SalesEname AS UserCode from [IV_REP_CRMSales] where DeptNodeCode like (select rtrim(NodeCode)+'%' from T_SYS_StructureNode where Id='" + us.DepartmentId + "')";
                    oset.Merge( dataBase.ExecuteDataset(CommandType.Text, sqlCommand));
                }
            }
            if (oset.Tables.Count == 0 || oset.Tables[0].Rows.Count == 0)
            {
                oset = dataBase.ExecuteDataset(CommandType.Text, "select SalesEname as UserCode,SalesCname as UserName,UserId from [IV_REP_CRMSales] where UserId='" + this.CurrentUserId.ToString() + "'");
            }
            return oset;
        }

        //public DataSet GetMarketSales()
        //{
        //    IDatabase dataBase = this._databaseFactory.CreateDatabase();
        //    List<UserStationData> userStations = this._userStationService.GetUserStations(this.CurrentUserId);

        //    DataSet oset = new DataSet();

        //    DataTable dt = new DataTable();

        //    DataColumn column;
        //    column = new DataColumn("UserId", typeof(Guid));
        //    dt.Columns.Add(column);
        //    dt.PrimaryKey = new DataColumn[] { column };

        //    column = new DataColumn("UserName", typeof(string));
        //    dt.Columns.Add(column);

        //    column = new DataColumn("UserCode", typeof(string));
        //    dt.Columns.Add(column);
        //    oset.Tables.Add(dt);

        //    string sqlCommand = "";
        //    foreach(UserStationData us in userStations)
        //    {
        //        if (us.RoleName.IndexOf("经理") > -1
        //            || us.RoleName.IndexOf("主管") > -1
        //            || us.RoleName.IndexOf("董事长") > -1)
        //        {
        //            sqlCommand = " select distinct UserId,Username,UserCode from IV_REP_MKSales where DeptNodeCode like (select rtrim(NodeCode)+'%' from T_SYS_StructureNode where Id='" + us.DepartmentId + "')";
        //            oset.Merge(dataBase.ExecuteDataset(CommandType.Text, sqlCommand));
        //        }
        //    }
        //    if (oset.Tables.Count == 0 || oset.Tables[0].Rows.Count == 0)
        //    {
        //        oset = dataBase.ExecuteDataset(CommandType.Text, "select UserCode,UserName,UserId from IV_REP_MKSales where UserId='" + this.CurrentUserId.ToString() + "'");
        //    }
        //    return oset;
        //}

        public DataSet GetMarketAccountByCondition(string Condition)
        {
            return null;//this._customerGroupService.GetAccountByCondition(Condition);
        }

        public DataSet GetAllUserSet()
        {
            return this._userStationService.GetAllUsers(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true, -1).ToDataSet();
        }
       
        public DataSet GetUserSetForCostByCurrentUser()
        {
            //获取该用户的身份/部门/用户的集合

            List<UserStationData> userStations = this._userStationService.GetUserStations(this.CurrentUserId);
            DataSet users = new DataSet();
            //对每一个部门的身份进行处理
            foreach (UserStationData row in userStations)
            {
                if (row.RoleName.IndexOf("经理") > -1 ||
                    row.RoleName.IndexOf("董事长") > -1
                    || row.RoleName.IndexOf("主管") > -1)
                {
                    //如果是经理

                    if (users.Tables.Count == 0)
                    {
                        users.Tables.Add(this._userStationService.GetUsersByStructureNode(row.DepartmentId, true).ToTable());
                    }
                    else
                    {
                        users.Tables[0].Merge(this._userStationService.GetUsersByStructureNode(row.DepartmentId, true).ToTable());
                    }
                }
            }
            if (users == null || users.Tables.Count == 0 || users.Tables[0].Rows.Count == 0)
            {
                UserInfo user = this._iinitializeService.GetUserInfo();
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(Guid));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Code", typeof(string));

                dt.Rows.Add(new object[] { this.CurrentUserId, user.DispalyName, user.GetValue("Code").ToString() });

                users.Tables.Clear();
                users.Tables.Add(dt);
            }
            return users;
        }
        public DataSet GetUserSetForAGTCRM()
        {
            List<UserStationData> userStations = this._userStationService.GetUserStations(this.CurrentUserId);
            DataSet users = new DataSet();
            //对每一个部门的身份进行处理
            foreach (UserStationData row in userStations)
            {
                if (row.RoleName.IndexOf("经理") > -1 ||
                    row.RoleName.IndexOf("董事长") > -1
                    || row.RoleName.IndexOf("主管") > -1)
                {
                    //如果是经理

                   users = this._databaseFactory.CreateDatabase().ExecuteDataset(CommandType.Text, @"SELECT DISTINCT dbo.T_SYS_UserStation.UserId as ID,Users.Code,Users.Name
                                                                                FROM          dbo.T_SYS_StructureNode INNER JOIN
                                                                                dbo.T_SYS_UserStation ON dbo.T_SYS_StructureNode.Id = dbo.T_SYS_UserStation.StructureNodeId
                                                                                    Inner join T_SYS_Users Users on Users.ID=T_SYS_UserStation.UserID
                                                                                WHERE      (dbo.T_SYS_StructureNode.Name = '海外部')");
                }
            }
            if (users == null || users.Tables.Count == 0 || users.Tables[0].Rows.Count == 0)
            {
                UserInfo user = this._iinitializeService.GetUserInfo();
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(Guid));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Code", typeof(string));

                dt.Rows.Add(new object[] { this.CurrentUserId, user.DispalyName, user.GetValue("Code").ToString() });

                users.Tables.Clear();
                users.Tables.Add(dt);
            }
            return users;
        }

        public DataSet GetStructureNodesForCostByCurrentUser()
        {
            List<UserStationData> userStations = this._userStationService.GetUserStations(this.CurrentUserId);
            DataSet StructureNodes = new DataSet();

            Guid[] structureNodeIds = new Guid[userStations.Count];
            bool IsOperate = false;
            bool IsManage = false;

            int i=0;
            foreach (UserStationData us in userStations)
            {
                structureNodeIds[i] = us.DepartmentId;
                i+=1;
            }

            StructureNodes = this._organizationService.GetStructureNodes(structureNodeIds, true,true).ToDataSet();

            DataTable dt = userStations.ToTable();
            dt.TableName = "UserStation";
            StructureNodes.Tables.Add(dt);

            return StructureNodes;
        }

        public bool IsAgentDev()
        {
            IDatabase dataBase = this._databaseFactory.CreateDatabase();
            List<UserStationData> userStations = this._userStationService.GetUserStations(this.CurrentUserId);
            bool isDevAgent = false;

            foreach(UserStationData us in userStations)
            {
                if ((us.RoleName.IndexOf("经理") > -1
                    && us.DepartmentFullName.IndexOf("海外部") == -1)
                   || (us.RoleName.IndexOf("主管") > -1
                   && us.DepartmentFullName.IndexOf("海外部") == -1)
                   || us.RoleName.IndexOf("董事长") > -1)
                {
                    isDevAgent=false;
                }
                else if (us.RoleName.IndexOf("海外拓展")>-1)
                {
                    isDevAgent = true;
                }
            }
            return isDevAgent;
        }
       
        #endregion

        #region IREPBaseDataService 成员

        public DataSet GetAllExams()
        {
            string sql = " select ID,ExamTitle from T_TR_Exam  ";
            return this._databaseFactory.CreateDatabase().ExecuteDataset(CommandType.Text, sql);
        }

        public DataSet GetStructureNodesByExamUser()
        {
            List<UserStationData> userStations = this._userStationService.GetUserStations(this.CurrentUserId);
            DataSet StructureNodes = new DataSet();

            Guid[] structureNodeIds = new Guid[userStations.Count];
            int i = 0;
            foreach(UserStationData us in userStations)
            {
                if (us.RoleName.IndexOf("HR经理") > -1
                    || us.RoleName.IndexOf("远东HR") > -1
                    || us.RoleName.IndexOf("董事长") > -1
                    || ((us.RoleName.IndexOf("总经理") > -1)
                     && (us.RoleName.IndexOf("分公司") == -1))
                    )
                {
                    structureNodeIds[i] = new Guid("701ACD43-D49B-422B-83A9-ACB56B696995");
                }
                else if (us.RoleName.IndexOf("HR") > -1
                    || us.RoleName.IndexOf("经理") > -1
                    || us.RoleName.IndexOf("主管") > -1
                    )
                {
                    //DataSet companyids = this._organizationService.GetCompanyInfo(new Guid(userStations.Tables[0].Rows[i]["StructureNodeId"].ToString()));
                    //if (companyids != null && companyids.Tables[0].Rows.Count > 0)
                    //{
                    //    structureNodeIds[i] = new Guid(this._organizationService.GetCompanyInfo(new Guid(userStations.Tables[0].Rows[i]["StructureNodeId"].ToString())).Tables[0].Rows[0]["Id"].ToString());
                    //}
                    //else
                    //{
                    //    structureNodeIds[i] = new Guid(userStations.Tables[0].Rows[i]["StructureNodeId"].ToString());
                    //}

                    structureNodeIds[i] = us.CompanyId;
                }
                else
                {
                    structureNodeIds[i] = us.DepartmentId;
                }

                i++;
            }
            StructureNodes = this._organizationService.GetStructureNodes(structureNodeIds, true,false).ToDataSet();
           
            DataTable tb = StructureNodes.Tables[0];
            for (int r = tb.Rows.Count - 1; r >= 0; r--)
            {
                if (tb.Rows[r]["type"].ToString() == "3")
                {
                    tb.Rows[r].Delete();
                }
            }
            StructureNodes.AcceptChanges();

            DataTable dt = userStations.ToTable();
            dt.TableName = "UserStation";
            StructureNodes.Tables.Add(dt);

            return StructureNodes;
        }

        public DataSet GetAllProject()
        {
            string sql = " select ID,ProjectName from T_MBO_Project  ";
            return this._databaseFactory.CreateDatabase().ExecuteDataset(CommandType.Text, sql);
        }


        public DataSet GetUserSetByCurrentBMOUser()
        {
            //获取该用户的身份/部门/用户的集合

            List<UserStationData> userStations = this._userStationService.GetUserStations(this.CurrentUserId);
            DataSet users = new DataSet();
            //对每一个部门的身份进行处理
            foreach (UserStationData row in userStations)
            {
                if (row.RoleName.IndexOf("经理") > -1 ||
                    row.RoleName.IndexOf("董事长") > -1
                    || row.RoleName.IndexOf("主管") > -1)
                {
                    //如果是经理


                    if (users.Tables.Count == 0)
                    {
                        users.Tables.Add(this._userStationService.GetUsersByStructureNode(row.DepartmentId, true).ToTable());

                    }
                    else
                    {
                        users.Tables[0].Merge(this._userStationService.GetUsersByStructureNode(row.DepartmentId, true).ToTable());
                    }
                }
                if (row.RoleName.IndexOf("财务") > -1
                    || row.RoleName.IndexOf("计费") > -1
                    || row.RoleName.IndexOf("操作") > -1
                    || row.RoleName.IndexOf("定舱") > -1
                    || row.RoleName.IndexOf("客服") > -1
                    || row.RoleName.IndexOf("文件") > -1)
                {
                    //Guid companyId ;
                    //if (this._organizationService.GetCompanyInfo(new Guid(row["StructureNodeId"].ToString())) != null
                    //    && this._organizationService.GetCompanyInfo(new Guid(row["StructureNodeId"].ToString())).Tables.Count>0
                    //    && this._organizationService.GetCompanyInfo(new Guid(row["StructureNodeId"].ToString())).Tables[0].Rows.Count>0)
                    //{
                    //    companyId = new Guid(this._organizationService.GetCompanyInfo(new Guid(row["StructureNodeId"].ToString())).Tables[0].Rows[0]["Id"].ToString());
                    //}
                    //else
                    //{
                    //    companyId = new Guid(row["StructureNodeId"].ToString());
                    //}
                    //if (users.Tables.Count == 0)
                    //{
                    //    users.Tables.Add(this._organizationService.GetUsersByStructureNode(companyId, true, false));
                    //}
                    //else
                    //{
                    //    users.Tables[0].Merge(this._organizationService.GetUsersByStructureNode(companyId, true, false));
                    //}
                    users = this._userStationService.GetAllUsers(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true, -1).ToDataSet();
                    break;
                }
            }
            if (users == null || users.Tables.Count == 0 || users.Tables[0].Rows.Count == 0)
            {
                UserInfo user = this._iinitializeService.GetUserInfo();
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(Guid));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Code", typeof(string));

                dt.Rows.Add(new object[] { this.CurrentUserId, user.DispalyName, user.GetValue("Code").ToString() });

                users.Tables.Clear();
                users.Tables.Add(dt);
            }
            return users;
        }

        #endregion
 
    }
}
