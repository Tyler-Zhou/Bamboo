using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Agilelabs.Framework.Service;
using LongWin.OrganizationStructure.ServiceInterface;
using LongWin.OrganizationStructure.ServiceInterface.DataObjects;
using LongWin.Framework.Server;
using LongWin.ReportCenter.ServiceInterface;
using System.Data.Sql;

namespace LongWin.ReportCenter.ServiceComponent
{
    /// <summary>
    /// 提供业务信息详细给CRM
    /// </summary>
    public class REPPUBDataService : IREPPUBDataService
    {
        IDatabaseFactory _databaseFactory;//获取数据服务
        ISessionService _sessionService;//会话服务
        IOrganizationService _organizationService;
        IUserStationService _userStationService;
        public REPPUBDataService(IDatabaseFactory databaseFactory,
            ISessionService sessionService,
            IUserStationService userStationService,
            IOrganizationService organizationService)
        {
            this._userStationService = userStationService;
            this._databaseFactory = databaseFactory;
            this._sessionService = sessionService;
            this._organizationService = organizationService;
        }

        Guid CurrentUserId
        {
            get
            {
                return new Guid(_sessionService.CurrentSession[ServerVariables.CURRENT_USER].ToString());
            }
        }

        public JobInfoSet.JobInfoDataTable GetJobInformation(DateTime BeginTime, DateTime EndTime, Guid CustomerId, Guid SaleId)
        {
            IDatabase dataBase = this._databaseFactory.CreateDatabase();
            try
            {
                string condition = "";

                condition = " (JobInfo.EtdSecond>='" + BeginTime.ToShortDateString() + "'"
                + " and JobInfo.EtdSecond<'" + EndTime.ToShortDateString() + "')"
                + " and JobInfo.ConsignerId='" + CustomerId.ToString() + "'";

                if (SaleId != Guid.Empty)
                {
                    condition += " and JobInfo.SalesId='" + SaleId.ToString() + "'";
                }

                string sqlCommand = string.Format(@"SELECT distinct 
                    JobInfo.Id as ConsignId
	                ,JobInfo.ConsignNo AS JobCode
	                ,IsNull(SumFee.LocalRecoupFlag,0) AS IsVerifyed
	                ,JobInfo.HBLNO as BLNO
	                ,JobInfo.ContainerNo
	                ,JobInfo.DiscPortName
	                ,JobInfo.LoadPortname 
	                ,JobInfo.DestinationName
	                ,JobInfo.ConsignerName
	                ,Agent.CShortName AS AgentName
					,IsNull(SumFee.AmountUSDByDR,0) as AmountUSDByDR
					,IsNull(SumFee.AmountUSDByCR,0) as AmountUSDByCR
	                ,IsNull(SumFee.ProfitUSD,0) AS ProfitByUSD
	                ,IsNull(BoxupCount.TEU,0) as TEU
	                ,JobInfo.EtdSecond AS ETD
	                ,JobInfo.ETA
	                ,JobInfo.Operator AS OperatorName
	                ,JobInfo.VesselVoyage
	                ,JobInfo.SalesName
                    ,JobInfo.ConsignerId
                    FROM ICP2005.ICP2005.DBO.IV_REP_JobInfo JobInfo 
                    left JOIN ICP2005.ICP2005.DBO.IV_REP_SumFee sumFee on JobInfo.ID=sumFee.ConsignID 
                    left join T_PUB_Customer Agent on Agent.ID=JobInfo.AgentId 
                    left join ICP2005.ICP2005.DBO.IV_REP_BoxUpCount BoxupCount on BoxupCount.ConsignId=JobInfo.id 
                    LEFT JOIN T_SYS_StructureNode StructNode ON JobInfo.SalesDeptID = StructNode.Id "

                + " where " + condition);
                JobInfoSet.JobInfoDataTable otable = new JobInfoSet.JobInfoDataTable();
                DataSet ds = dataBase.ExecuteDataset(CommandType.Text, sqlCommand);
                otable.Merge(ds.Tables[0]);

                return otable;
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        public JobInfoSet.JobInfoDataTable GetJobInformation(DateTime BeginTime, DateTime EndTime, Guid[] CustomerIds, Guid SaleId)
        {
            string strcustomerids = "";
            foreach (Guid customerId in CustomerIds)
            {
                strcustomerids += customerId.ToString() + ",";
            }
            if (strcustomerids.EndsWith(","))
            {
                strcustomerids = strcustomerids.Substring(0, strcustomerids.Length - 1);
            }
            strcustomerids = strcustomerids.Replace(",", "'',''");
            strcustomerids = "''" + strcustomerids + "''";


            IDatabase dataBase = this._databaseFactory.CreateDatabase();
            try
            {
                string condition = "";

                if (SaleId != Guid.Empty)
                {
                    condition = " and JobInfo.SalesId='" + SaleId.ToString() + "'";
                }

                string sqlCommand = string.Format(@"SELECT distinct 
                    JobInfo.Id as ConsignId
	                ,JobInfo.ConsignNo AS JobCode
	                ,IsNull(SumFee.LocalRecoupFlag,0) AS IsVerifyed
	                ,JobInfo.HBLNO as BLNO
	                ,JobInfo.ContainerNo
	                ,JobInfo.DiscPortName
	                ,JobInfo.LoadPortname 
	                ,JobInfo.DestinationName
	                ,JobInfo.ConsignerName
	                ,Agent.CShortName AS AgentName
					,IsNull(SumFee.AmountUSDByDR,0) as AmountUSDByDR
					,IsNull(SumFee.AmountUSDByCR,0) as AmountUSDByCR
	                ,IsNull(SumFee.ProfitUSD,0) AS ProfitByUSD
	                ,IsNull(BoxupCount.TEU,0) as TEU
	                ,JobInfo.EtdSecond AS ETD
	                ,JobInfo.ETA
	                ,JobInfo.Operator AS OperatorName
	                ,JobInfo.VesselVoyage
	                ,JobInfo.SalesName
                    ,JobInfo.ConsignerId
                    FROM ICP2005.ICP2005.DBO.IV_REP_JobInfo JobInfo 
                    left JOIN ICP2005.ICP2005.DBO.IV_REP_SumFee sumFee on JobInfo.ID=sumFee.ConsignID 
                    left join T_PUB_Customer Agent on Agent.ID=JobInfo.AgentId 
                    left join ICP2005.ICP2005.DBO.IV_REP_BoxUpCount BoxupCount on BoxupCount.ConsignId=JobInfo.id 
                    LEFT JOIN T_SYS_StructureNode StructNode ON JobInfo.SalesDeptID = StructNode.Id 
                 where (JobInfo.EtdSecond>='" + BeginTime.ToShortDateString() + @"' 
                and JobInfo.EtdSecond<'" + EndTime.ToShortDateString() + "')"
                + condition
                + " and (CHARINDEX(CAST(JobInfo.ConsignerId AS Varchar(36)), '" + strcustomerids + "') > 0 OR LEN(LTRIM(RTRIM('" + strcustomerids + "'))) = 0) ");

                JobInfoSet.JobInfoDataTable otable = new JobInfoSet.JobInfoDataTable();
                DataSet ds = dataBase.ExecuteDataset(CommandType.Text, sqlCommand);
                otable.Merge(ds.Tables[0]);

                return otable;
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public JobInfoSet.FeeOfJobDataTable GetFeesOfJob(Guid consignId)
        {
            IDatabase dataBase = this._databaseFactory.CreateDatabase();
            try
            {
                string sqlCommand = string.Format("SELECT "
            + "Fee.ShipperName as CustomerName"
            + ",fee.Amount"
            + ",fee.Rate"
            + ",cast(IsNull(DcNote.IsPaid,0 ) as bit) as IsPaid"
            + ",cast(IsNull(DcNote.RecoupFlag,0) as bit) as RecoupFlag"
            + ",cast(fee.DrCrFlag as smallint) as DrCrFlag"
            + ",Fee.BillingCodeName as FeeName"
            + ",CurrencyName as CurrencyEName"

            + " from ICP2005.ICP2005.DBO.V_REP_FeeInfo fee"
            + " left join dbo.T_FCM_DcNoteFees DcNoteFees on DcNoteFees.FeeID=fee.ID"
            + " left join dbo.T_FCM_DcNote DcNote on DcNote.ID=DcNoteFees.DcNoteID"

            + " where fee.ConsignId = '" + consignId.ToString() + "'");

                JobInfoSet.FeeOfJobDataTable otable = new JobInfoSet.FeeOfJobDataTable();
                DataSet ds = dataBase.ExecuteDataset(CommandType.Text, sqlCommand);
                otable.Merge(ds.Tables[0]);

                return otable;
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public bool GetIsViewShipper()
        {
            List<LongWin.OrganizationStructure.ServiceInterface.DataObjects.UserStationData> userStations = this._userStationService.GetUserStations(CurrentUserId);
            bool isViewShipper = false;
            foreach (LongWin.OrganizationStructure.ServiceInterface.DataObjects.UserStationData row in userStations)
            {
                if (row.RoleName.IndexOf("经理") > -1 ||
                    row.RoleName.IndexOf("董事长") > -1
                    || row.RoleName.IndexOf("主管") > -1)
                {
                    isViewShipper = true;
                    break;
                }
            }
            return isViewShipper;
        }



        #region 通过部门获取员工

        /// <summary>
        /// 通过部门获取员工
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public DataSet GetStafferByCompanyID(Guid CompanyID)
        {
            IDatabase dataBase = this._databaseFactory.CreateDatabase();

            string sql = string.Format(@"   SELECT	 DISTINCT 
		                                             Staffer.ID
		                                            ,Staffer.Name
		                                                                                       
                                            FROM	T_SYS_UserStation INNER JOIN 
		                                            T_HR_StafferBaseInfo Staffer ON T_SYS_UserStation.UserID = Staffer.ID INNER JOIN 
		                                            t_sys_structurenode ON T_SYS_UserStation.StructureNodeId = t_sys_structurenode.id
                                            WHERE T_SYS_UserStation.StructureNodeId IN 
                                            (
	                                            SELECT ID
	                                            FROM T_SYS_StructureNode,
		                                            (SELECT RTRIM(LTRIM(NodeCode)) AS nodecode  
					                                               FROM T_SYS_StructureNode 
					                                               WHERE ID='{0}' ) e
	                                            WHERE e.NodeCode=LEFT(RTRIM(LTRIM(T_SYS_StructureNode.nodecode)),LEN(e.NodeCOde))
                                            )
                                            AND IsDimission = 1", CompanyID.ToString());
            return dataBase.ExecuteDataset(CommandType.Text, sql);

        }

        #endregion
    }
}
