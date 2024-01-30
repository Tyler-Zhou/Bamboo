using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Agilelabs.Framework.Service;
using LongWin.Framework.Server;
using LongWin.OrganizationStructure.ServiceInterface;
using LongWin.BusinessInfo.ServiceInterface;
using LongWin.BusinessInfo.ServiceInterface.DataObject;

namespace LongWin.BusinessInfo.ServiceComponent
{
    public class OTEBusinessInfoService : IOTEBusinessInfoService
    {

        IDatabaseFactory _databaseFactory;//��ȡ���ݷ���
 //       ISessionService _sessionService;//�Ự����
        IOrganizationService _organizationService;
        IUserStationService _userStationService;
        //IPublicFeeService _publicFeeService;
        //IPublicDataService _publicDataService;


        public OTEBusinessInfoService(IOrganizationService organizationService, 
             IUserStationService userStationService,
            IDatabaseFactory databaseFactory)
        {
            this._userStationService = userStationService;
            this._databaseFactory = databaseFactory;
            //this._sessionService = sessionService;
            this._organizationService = organizationService;
            
        }


        /// <summary>
        /// һ���ض��Ŀͻ��Ϳͻ���һ���ض��ĸ�������һ��ʱ���ڵ�ҵ������        
        /// </summary>
        /// <param name="BeginTime">��ʼʱ��(ETD)</param>
        /// <param name="EndTime">����ʱ��(ETD)</param>
        /// <param name="CustomerId">ָ���Ŀͻ�</param>
        /// <param name="SaleId">ָ����ҵ��Ա</param>
        /// <returns></returns>
        public List<JobInfoData>  GetJobInformation(DateTime BeginTime, DateTime EndTime, Guid CustomerId, Guid SaleId)
        {
            IDatabase dataBase = this._databaseFactory.CreateDatabase();
            if (CustomerId == null || CustomerId == Guid.Empty)
            {
                throw new ApplicationException("Customers can not be empty !");
            }
            //if (SaleId == null || SaleId == Guid.Empty)
            //{
            //    throw new ApplicationException("Salesmen can not be empty !");
            //}

            try
            {                

                string sql = @" EXEC SPR_REP_GetJobInformationByCustomerID '" + BeginTime.ToShortDateString() + "','"
                                                                           + EndTime.ToShortDateString() + "','"
                                                                           + CustomerId.ToString() + "','"
                                                                            + SaleId.ToString() + "'";

                DataSet ds = dataBase.ExecuteDataset(CommandType.StoredProcedure, sql);

                List<JobInfoData> jobInfoList = new List<JobInfoData>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in ds.Tables[0].Rows)
                    {
                        JobInfoData job = new JobInfoData();
                        job.AgentName = drow["AgentName"].ToString();
                        job.AmountUSDByCR =ConvertToDecimal(drow["AmountUSDByCR"]);
                        job.AmountUSDByDR = ConvertToDecimal(drow["AmountUSDByDR"]);
                        job.ProfitByUSD =ConvertToDecimal(drow["ProfitByUSD"]);
                        job.BLNO = drow["BLNo"].ToString();           
                        job.ConsignerEName = drow["ConsignerEName"].ToString();
                        job.ConsignerCName = drow["ConsignerCName"].ToString();

                        job.ConsignerId = new Guid(drow["ConsignerId"].ToString());
                        job.ConsignId = new Guid(drow["ConsignId"].ToString());
                        job.ContainerNo = string.Empty;
                        job.DestinationName = drow["DestinationName"].ToString();
                        job.DiscPortName = drow["DiscPortName"].ToString();
                        job.ETD = ConvertToDateTime(drow["ETD"]);
                        job.ETA = ConvertToDateTime(drow["ETA"]);
                        job.ENVerifyedState = (ENVerifyed)short.Parse(drow["IsVerifyed"].ToString());
                        job.CNVerifyedState = (CNVerifyed)short.Parse(drow["IsVerifyed"].ToString());

                        job.JobCode = drow["JobCode"].ToString();
                        job.LoadPortname = drow["LoadPortname"].ToString();
                        job.OperatorName = drow["OperatorName"].ToString();
                        job.SalesName = drow["SalesName"].ToString();
                        job.TEU =ConvertToDecimal(drow["TEU"]);
                        job.Weight = 0.00m;
                        job.VesselVoyage = drow["VesselVoyage"].ToString();
                        job.CommisionAmount = ConvertToDecimal(drow["CommisionAmount"]);
                        jobInfoList.Add(job);
                    }
                }
                return jobInfoList;
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        private bool ConvertToBool(object val)
        {
            try
            {
                return Convert.ToBoolean(val);
            }
            catch
            {
                return false;
            }

        }


        private decimal ConvertToDecimal(object val)
        {
            try
            {
                return Convert.ToDecimal(val);
            }
            catch
            {
                return 0.00m;
            }
        }

        private short ConvertToInt(object val)
        {
            try
            {
                return Convert.ToInt16(val);
            }
            catch
            {
                return 0;
            }
        }

        private DateTime ConvertToDateTime(object val)
        {
            try
            {
                return DateTime.Parse(val.ToString());
            }
            catch
            {
                return new DateTime(1900, 1, 1);
            }
        }
        /// <summary>
        /// ��ȡһ��ί�е����з�����ϸ
        /// </summary>
        /// <param name="consignId">ί�е�Id</param>
        /// <returns></returns>
        public List<JobFeeData>  GetFeesOfJob(Guid consignId)
        {
            IDatabase dataBase = this._databaseFactory.CreateDatabase();
            try
            {

                string sql = @" EXEC SPR_REP_GetFeeDetailInfo '" + consignId.ToString() + "'";

                DataSet ds = dataBase.ExecuteDataset(CommandType.StoredProcedure, sql);
                List<JobFeeData> feeList = new List<JobFeeData>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in ds.Tables[0].Rows)
                    {
                        JobFeeData fee = new JobFeeData();
                        fee.Amount = ConvertToDecimal(drow["Amount"]);
                        fee.CurrencyName = drow["CurrencyEName"].ToString();
                        fee.CustomerEName = drow["CustomerEName"].ToString();
                        fee.CustomerCName = drow["CustomerCName"].ToString();
                        fee.DrCrFlag =ConvertToInt(drow["DrCrFlag"]);
                        fee.FeeCName = drow["FeeCName"].ToString();
                        fee.FeeEName = drow["FeeEName"].ToString();
                        fee.IsPaid =ConvertToBool(drow["IsPaid"]);
                        fee.Rate =ConvertToDecimal(drow["Rate"]);
                        fee.RecoupFlag = ConvertToBool(drow["RecoupFlag"]);
                        feeList.Add(fee);
                    }
                }
                return feeList;
            }
            catch (Exception err)
            {
                throw err;
            }
        }



        /// <summary>
        /// ��ȡһ���û��Ƿ���Բ鿴Ӧ�����õ�������λ
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool GetIsViewShipper(Guid userID)
        {
            List<LongWin.OrganizationStructure.ServiceInterface.DataObjects.UserStationData> userStations = this._userStationService.GetUserStations(userID);
            bool isViewShipper = false;
            foreach (LongWin.OrganizationStructure.ServiceInterface.DataObjects.UserStationData row in userStations)
            {
                if (row.RoleName.IndexOf("����") > -1 ||
                    row.RoleName.IndexOf("���³�") > -1
                    || row.RoleName.IndexOf("����") > -1)
                {
                    isViewShipper = true;
                    break;
                }
            }
            return isViewShipper;
        }

    }
}
