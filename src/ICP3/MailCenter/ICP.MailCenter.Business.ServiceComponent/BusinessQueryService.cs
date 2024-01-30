using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Server;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ICP.Sys.ServiceInterface;

namespace ICP.MailCenter.Business.ServiceComponent
{
    /// <summary>
    /// 业务数据查询服务
    /// </summary>
    public class BusinessQueryService : IBusinessQueryService
    {
        /// <summary>
        /// 业务联系人呢服务
        /// </summary>
        IBusinessContactService BusinessContactService;
        /// <summary>
        /// 操作日志服务
        /// </summary>
        IOperationLogService OperationLogService;
        /// <summary>
        /// 关联信息服务
        /// </summary>
        IOperationMessageRelationService OperationMessageRelationService;
        /// <summary>
        /// 用户服务
        /// </summary>
        IUserService UserService;

        static Random random = new Random(10);
        public bool IsEnglish
        {
            get
            {
                return ICP.Framework.CommonLibrary.Common.ApplicationContext.Current.IsEnglish;
            }
        }
        public Guid UserId
        {
            get
            {
                return ICP.Framework.CommonLibrary.Common.ApplicationContext.Current.UserId;
            }
        }
        /// <summary>
        /// 用户默认所属的公司Id
        /// </summary>
        public Guid DefaultCompanyId
        {
            get
            {
                return ICP.Framework.CommonLibrary.Common.ApplicationContext.Current.DefaultCompanyId;
            }
        }
        /// <summary>
        /// 构造函数:初始化服务
        /// </summary>
        /// <param name="contactService">业务联系人信息服务接口</param>
        /// <param name="messageRelationService">关联信息服务</param>
        /// <param name="userService">用户服务</param>
        /// <param name="operationLogService">操作日志服务</param>
        public BusinessQueryService(IBusinessContactService contactService
            , IOperationMessageRelationService messageRelationService
            ,IUserService userService
            , IOperationLogService operationLogService)
        {
            this.BusinessContactService = contactService;
            this.OperationMessageRelationService = messageRelationService;
            OperationLogService = operationLogService;
            UserService = userService;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="criteria">业务查询信息实体类</param>
        /// <returns>DataTable</returns>
        public DataTable Get(BusinessQueryCriteria criteria)
        {
            Stopwatch stopwatchTotal = StopwatchHelper.StartStopwatch();

            //如果查询参数中包含所属公司，则以查询参数中的公司为准，否则取默认公司
            string companyIds = DefaultCompanyId.ToString();
            //针对上级对下级工作空间查看时，对公司赋值的操作
            if (criteria.UserId != Guid.Empty && criteria.UserId != UserId)
            {
                List<OrganizationList> usersTreeLists = UserService.GetUserCompanyList(criteria.UserId,
                    OrganizationType.Company);
                companyIds = usersTreeLists.Where(n => n.IsDefault).Select(n => n.ID).FirstOrDefault().ToString();
            }else if (!string.IsNullOrEmpty(criteria.LockCompanyIDs))
            {
                companyIds = criteria.LockCompanyIDs;
            }
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetOceanOperationList]");
            db.AddInParameter(dbCommand, "@IsQueryColumnMappingOnly", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
            db.AddInParameter(dbCommand, "@OperationViewCode", DbType.String, criteria.TemplateCode);
            db.AddInParameter(dbCommand, "@OperationType", DbType.Int32, criteria.OperationType == null ? 0 : (int)criteria.OperationType);
            db.AddInParameter(dbCommand, "@MailSender", DbType.String, criteria.EmailAddress);
            db.AddInParameter(dbCommand, "@AdvancedCriteria", DbType.String, string.IsNullOrEmpty(criteria.AdvanceQueryString) ? null : criteria.AdvanceQueryString);
            db.AddInParameter(dbCommand, "@TopCount", DbType.Int16, criteria.TopCount);
            if (string.IsNullOrEmpty(criteria.AdvanceQueryString))
            {
                db.AddInParameter(dbCommand, "@CurrentUserID", DbType.Guid, criteria.UserId == Guid.Empty ? this.UserId : criteria.UserId);
                db.AddInParameter(dbCommand, "@CurrentCompanyIDs", DbType.String, companyIds);
            }
            try
            {
                DataSet dtSet = db.ExecuteDataSet(dbCommand);
                OperationLogService.Add(DateTime.Now, "SEARCH"
                ,  string.IsNullOrEmpty(criteria.AdvanceQueryString) ? ("节点查询:[" + criteria.TemplateCode + "]") : ("高级查询：["+criteria.AdvanceQueryString+"]")
                , stopwatchTotal.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
                if (dtSet.Tables == null || dtSet.Tables[0] == null)
                {
                    DataTable dt = new DataTable();
                    dt.TableName = "Table";
                    return dt;
                }
                dtSet.RemotingFormat = SerializationFormat.Binary;

                return dtSet.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="messageID">邮件的MessageID特性</param>
        /// <param name="messageReference">邮件的Reference特性值</param>
        /// <returns></returns>
        public BusinessQueryResult Get(BusinessQueryCriteria criteria, string messageID, string messageReference)
        {

            BusinessQueryResult result = new BusinessQueryResult();
            try
            {
                result.Dt = Get(criteria);
                result.Relations = OperationMessageRelationService.GetByMessageIdAndReference(messageID, messageReference);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataTable Save(List<BusinessSaveParameter> parameters)
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspCommonUpdateOpertionData]");
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
            db.AddInParameter(dbCommand, "@UpdateXml", DbType.String, UIHelper.GetXMLParameters(parameters));
            db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, this.UserId);

            try
            {
                DataSet dtSet = db.ExecuteDataSet(dbCommand);
                return dtSet.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据业务ID和业务类型批量获取
        /// </summary>
        /// <param name="operationIDs"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        public DataTable GetOperationInfo(List<Guid> operationIDs, OperationType operationType)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetOperationViewCache]");
            db.AddInParameter(dbCommand, "@OperationIDS", DbType.String, operationIDs.ToArray().Join());
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
            db.AddInParameter(dbCommand, "@OperationType", DbType.Int16, operationType.GetHashCode());
            try
            {
                DataSet dtSet = db.ExecuteDataSet(dbCommand);
                if (dtSet.Tables == null || dtSet.Tables[0] == null)
                {
                    DataTable dt = new DataTable();
                    dt.TableName = "Table";
                    return dt;
                }
                dtSet.RemotingFormat = SerializationFormat.Binary;
                return dtSet.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 返回联系人的邮箱地址信息
        /// </summary>
        /// <param name="operationId">业务号</param>
        /// <param name="values">搜索范围</param>
        /// <returns></returns>
        public string GetCustomerMailList(Guid operationId, string values)
        {
            string Mail = string.Empty;
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetCustomerMailList]");
            db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationId);
            db.AddInParameter(dbCommand, "@Values", DbType.String, values);
            try
            {
                DataSet dtSet = db.ExecuteDataSet(dbCommand);
                if (dtSet.Tables == null || dtSet.Tables[0] == null)
                {
                    return string.Empty;
                }
                else
                {
                    for (int i = 0; i < dtSet.Tables[0].Rows.Count; i++)
                    {
                        if (string.IsNullOrEmpty(Mail))
                        {
                            Mail = dtSet.Tables[0].Rows[i]["Mail"].ToString();
                        }
                        else
                        {
                            Mail = Mail + "," + dtSet.Tables[0].Rows[i]["Mail"];
                        }
                    }
                    return Mail;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
