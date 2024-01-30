using System;
using System.Linq;
using System.Data.Linq;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Agilelabs.Framework.Service;
using LongWin.Framework.Server;
using LongWin.DataWarehouseReport.ServiceInterface;
using LongWin.OrganizationStructure.ServiceInterface;
using LongWin.OrganizationStructure.ServiceInterface.DataObjects;

namespace LongWin.DataWarehouseReport.ServiceComponent
{
    public class REPBaseDataService : IREPBaseDataService
    {
        IDatabaseFactory _databaseFactory;//获取数据服务
        ISessionService _sessionService;//会话服务
        IUserStationService _userStationService;//用户服务
        IOrganizationService _organizationService;//组织结构服务
        IInitializeService _iinitializeService;//获取当前用户信息服务

        string _reportUrl;
        string _reportUser;
        string _reportUserPSW;
         IDataContextService _dcService;
        public REPBaseDataService(IDatabaseFactory databaseFactory
            , ISessionService sessionService,
            IUserStationService userStationService
            , IOrganizationService organizationService
            , IInitializeService iinitializeService
            , IDataContextService dcService
            , string reportUrl
            , string reportUser
            , string reportUserPSW
            )
        {
            _reportUrl = reportUrl;
            _reportUser = reportUser;
            _reportUserPSW = reportUserPSW;
            _dcService = dcService;
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

        /// <summary>
        /// 报表服务器地址
        /// </summary>
        /// <returns></returns>
        public ReportServerInfo GetReportServerUrl()
        {
            return  new ReportServerInfo(this._reportUrl,this._reportUser,this._reportUserPSW);
        }


        /// <summary>
        /// 根据当前用户返回可以选择组织结构
        /// </summary>
        /// <returns></returns>
        public DataSet GetStructureNodesByCurrentUser()
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
                    //DataSet companyids = this._organizationService.GetCompanyInfo(us.DepartmentId));
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

                i += 1;
            }
                       
              StructureNodes = this._organizationService.GetStructureNodes(structureNodeIds, true,true).ToDataSet();
            //StructureNodeData
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

                if (( us.CompanyId.ToString().ToUpper() == "1D95B3C1-CB53-4719-9ED3-B8D2B98CA8A5".ToUpper()
                || us.CompanyId.ToString().ToUpper() == "3C20304C-6804-401F-B96C-0ADA22970358".ToUpper()
                    )&&
                    (us.RoleName.IndexOf("计费") > -1
                    || us.RoleName.IndexOf("商务") > -1))
                {
                    structureNodeIds[i] = new Guid("701ACD43-D49B-422B-83A9-ACB56B696995");
                }//远东区办公室 

                i += 1;
            }

            StructureNodes = this._organizationService.GetStructureNodes(structureNodeIds, true, true).ToDataSet();

            DataTable dt = userStations.ToTable();
            dt.TableName = "UserStation";
            StructureNodes.Tables.Add(dt);

            return StructureNodes;
        }

        public bool IsAgentDev()
        {
            //IDatabase dataBase = this._databaseFactory.CreateDatabase();
            List<UserStationData> userStations = this._userStationService.GetUserStations(this.CurrentUserId);
            bool isDevAgent = false;

            foreach (UserStationData us in userStations)
            {
                if ((us.RoleName.IndexOf("经理") > -1
                    && us.DepartmentFullName.IndexOf("海外部") == -1)
                   || (us.RoleName.IndexOf("主管") > -1
                   && us.DepartmentFullName.IndexOf("海外部") == -1)
                   || us.RoleName.IndexOf("董事长") > -1)
                {
                    isDevAgent = false;
                }
                else if (us.RoleName.IndexOf("海外拓展") > -1)
                {
                    isDevAgent = true;
                }
            }
            return isDevAgent;
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
                    || row.RoleName.IndexOf("财务") > -1
                    || row.RoleName.IndexOf("计费") > -1
                    || row.RoleName.IndexOf("操作") > -1
                    || row.RoleName.IndexOf("定舱") > -1
                    || row.RoleName.IndexOf("客服") > -1
                    || row.RoleName.IndexOf("总") > -1
                    || row.RoleName.IndexOf("文件") > -1)
                {
                    isViewShipper = true;
                    break;
                }
            }
            return isViewShipper;
        }


        public List<LedgerData> GetLedgerData(DateTime fromDate, DateTime toDate, string companys, short vType)
        {
            string sql = string.Format(@" EXEC SPD_FIN_GetVoucherForExport '{0}','{1}','{2}','{3}'", fromDate, toDate, companys, vType.ToString());

            using (DataContext dc = _dcService.GetDataContext(false))
            { 
                List<LedgerData> list = new List<LedgerData>(dc.ExecuteQuery<LedgerData>(sql, new object[] { }));
                return list;
            }
            
        }




    }
}
