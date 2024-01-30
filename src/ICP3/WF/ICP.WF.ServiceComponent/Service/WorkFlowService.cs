//-----------------------------------------------------------------------
// <copyright file="WorkFlowService.cs" company="ICP">
//     Copyright (c) ICP. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.ServiceComponent
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Workflow.Runtime;
    using System.Workflow.Runtime.Configuration;
    using System.Workflow.Runtime.Hosting;
    using System.Xml;
    using ICP.Framework.CommonLibrary.Helper;
    using ICP.Sys.ServiceInterface;
    using ICP.Sys.ServiceInterface.DataObjects;
    using ICP.WF.ServiceInterface;
    using ICP.WF.ServiceInterface.DataObject;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using ICP.Framework.CommonLibrary.Common;
    using System.Text;
    using System.IO;
    using ICP.Framework.CommonLibrary.Client;
    using ICP.Framework.CommonLibrary.Server;
    using System.ServiceModel;
    using System.Threading;
    using ICP.Message.ServiceInterface;
    using ICP.Framework.CommonLibrary;

    /// <summary>
    /// 工作流管理服务
    /// </summary>
    public class WorkflowService : IWorkflowService
    {
        #region 构造函数

        const string MAINTABLENAME = "MainTable";
        IWorkFlowConfigService _configService;
        IWorkFlowExtendService _extendService;
        IUserService _userService;
        IOrganizationService _organizationService;
        ISessionService _sessionService;
        IBusinessDataExchangeService _businessDataExchangeService;
        IMessageService _messageService;

        string _connectionString;
        string administratorEmail;
        //管理员用户ID
        Guid administratorId = new Guid("4047CFAD-ECC8-E111-9D0D-0026551CA87B");
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configService">流程配置服务</param>
        /// <param name="extendService">流程拓展服务</param>
        /// <param name="userService">用户服务</param>
        /// <param name="organizationService">组织结构服务/param>
        /// <param name="connectionString">连接</param>
        /// <returns></returns>
        public WorkflowService(
            IWorkFlowConfigService configService,
            IWorkFlowExtendService extendService,
            IUserService userService,
            IOrganizationService organizationService,
            ISessionService sessionService,
            IBusinessDataExchangeService businessDataExchangeService,
            IMessageService messageService,
            string connectionString)
        {

            _configService = configService;
            _userService = userService;
            _extendService = extendService;
            _connectionString = connectionString;
            _organizationService = organizationService;
            _sessionService = sessionService;
            _businessDataExchangeService = businessDataExchangeService;
            _messageService = messageService;

            WFCommissionIDList = new List<Guid>();
            WFCommissionIDList.Add(new Guid("928CBC8E-0041-4188-BCD6-812784DE7067"));
            WFCommissionIDList.Add(new Guid("953DAD24-2C60-4CC3-A122-784CA7BBE34A"));

            WFExpenseIDList = new List<Guid>();
            WFExpenseIDList.Add(new Guid("4EE24687-D3B9-47BC-A828-52B649BF3D08"));
            WFExpenseIDList.Add(new Guid("1ACB0832-9118-46FF-93B0-9E5038283D13"));

            UserInfo userInfo = _userService.GetUserInfo(administratorId);
            administratorEmail = userInfo.EMail;

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
                    Guid userId = ICP.Framework.CommonLibrary.Common.ApplicationContext.Current.UserId;
                    //LogHelper.SaveLog(userId.ToString());
                    if (userId == Guid.Empty)
                    {
                        return new Guid("4047CFAD-ECC8-E111-9D0D-0026551CA87B");
                    }

                    return userId;
                }
                catch
                {
                    return Guid.Empty;
                }
            }
        }


        public List<Guid> WFCommissionIDList = new List<Guid>();
        public List<Guid> WFExpenseIDList = new List<Guid>();
        //亏损流程的ID
        public Guid DeficitOperationID = new Guid("DD6BA6A0-65AD-4483-9399-DC7CEAADFCD3");

        #endregion

        #region IWorkFlowService接口实现

        #region 启动工作流
        /// <summary>
        /// 启动工作流
        /// </summary>
        /// <param name="workflowConfigID">流程配置ID</param>
        /// <param name="workName">工作名称</param>
        /// <param name="isGenerateNo">是否生成单号</param>
        /// <param name="data">启动初始化数据</param>
        /// <param name="startByID">发起人</param>
        /// <param name="startDepartmentId">发起部门</param>
        /// <param name="isValidateData">是否需要验证日期</param>
        /// <returns></returns>
        public WorkItemInfo StartWorkflowInternal(
            Guid workflowConfigID,
            string workName,
            bool isGenerateNo,
            DataSet data,
            Guid startByID,
            Guid startDepartmentId,
            bool isValidateData)
        {

            ArgumentHelper.AssertGuidNotEmpty(workflowConfigID, "workflowConfigID");
            ArgumentHelper.AssertGuidNotEmpty(startByID, "startByID");
            ArgumentHelper.AssertGuidNotEmpty(startDepartmentId, "startDepartmentId");

            //取出配置信息,并且检验是否合乎逻辑
            string xomlContent = string.Empty;
            string ruleContent = string.Empty;

            WorkFlowConfigInfo item = _configService.GetWorkFlowConfigInfoByID(workflowConfigID, ApplicationContext.Current.IsEnglish);
            if (item == null)
            {
                throw new ApplicationException(SRHelper.GetString("Config Not Existed", "该配置项不存在"));
            }
            if (isValidateData)
            {
                //验证当天是否可以发起申请此流程
                if (item.Days > 0)
                {
                    DateTime startDate = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"));
                    DateTime endDate = Convert.ToDateTime(DateTime.Now.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1);

                    TimeSpan ts = endDate - startDate;

                    if (item.Days > ts.Days)
                    {
                        throw new ApplicationException(SRHelper.GetString("Today Cannot Start WorkFlow", "今天不能发起流程"));
                    }
                }
            }

            xomlContent = item.WorkFlowFileContent;
            ruleContent = item.RuleFileContent;

            //启动工作流
            WorkflowRuntimeSection wrs = new WorkflowRuntimeSection();
            wrs.EnablePerformanceCounters = false;
            using (WorkflowRuntime workflowRuntime = new WorkflowRuntime(wrs))
            {
                WorkflowInstance instance = null;
                Exception faultException = null;
                try
                {
                    workflowRuntime.WorkflowCreated += delegate (object sender, WorkflowEventArgs e)
                    {
                        SaveWorkflowInfo(
                            null,
                            e.WorkflowInstance.InstanceId,
                            workName,
                            isGenerateNo,
                            startByID,
                            startDepartmentId,
                            DateTime.Now,
                            null,
                            WorkflowState.Activated,
                            workflowConfigID,
                            startByID);
                    };

                    workflowRuntime.WorkflowCompleted += delegate (object sender, WorkflowCompletedEventArgs e)
                    {
                        //标志流程完成
                        ChangeWorkflowState(
                             e.WorkflowInstance.InstanceId,
                            WorkflowState.Finished,
                            startByID);
                    };

                    workflowRuntime.WorkflowTerminated += delegate (object sender, WorkflowTerminatedEventArgs e)
                    {
                        if (e.Exception != null)
                        {
                            faultException = e.Exception;
                        }
                        else
                        {
                            //标志流程完成
                            ChangeWorkflowState(
                             e.WorkflowInstance.InstanceId,
                            WorkflowState.Return,
                            startByID);
                        }
                    };



                    //加入自定义服务
                    workflowRuntime.AddService(this);

                    //加载流程扩展服务
                    workflowRuntime.AddService(_extendService);

                    //加载与业务接口通行的服务
                    workflowRuntime.AddService(_businessDataExchangeService);

                    //加入持久化服务
                    LWSqlWorkflowPersistenceService persistenceService = new LWSqlWorkflowPersistenceService(_connectionString);
                    workflowRuntime.AddService(persistenceService);

                    System.Workflow.Runtime.Hosting.ManualWorkflowSchedulerService manualService = new ManualWorkflowSchedulerService(true);
                    workflowRuntime.AddService(manualService);

                    //启动运行时
                    workflowRuntime.StartRuntime();

                    #region 字典数据

                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    parameters.Add(WWFConstants.WorkFlowTitle, workName);
                    parameters.Add(WWFConstants.Proposer, startByID.ToString());
                    parameters.Add(WWFConstants.ProposerDepartment, startDepartmentId.ToString());

                    Dictionary<string, object> dataCollection = new Dictionary<string, object>();
                    dataCollection.Add(WWFConstants.CurrentDate_E, DateTime.Now.ToShortDateString());
                    dataCollection.Add(WWFConstants.CurrentDate_C, DateTime.Now.ToShortDateString());
                    dataCollection.Add(WWFConstants.MainWorkItemDataSet, data);
                    dataCollection.Add(WWFConstants.RuleFile, ruleContent);

                    parameters.Add(WWFConstants.DataCollection, dataCollection);

                    #endregion

                    using (System.IO.StringReader sr = new System.IO.StringReader(xomlContent))
                    {
                        using (XmlReader reader = XmlReader.Create(sr))
                        {
                            instance = workflowRuntime.CreateWorkflow(
                                reader,
                                null,
                                parameters);
                            instance.Start();

                            manualService.RunWorkflow(instance.InstanceId);
                        }
                    }

                    // waitHandle.WaitOne();

                    if (faultException != null)
                    {
                        //出现异常不持久化流程
                        if (instance != null)
                        {
                            instance.Abort();
                        }

                        throw faultException;
                    }
                    else
                    {
                        //持久化前,,必须调用该方法
                        persistenceService.PreSave();

                        //持久化实例
                        instance.TryUnload();
                    }


                    WorkItemInfo workItemInfo = this.GetMainWorkitemInfo(instance.InstanceId);
                    return workItemInfo;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {

                    if (workflowRuntime != null)
                    {
                        workflowRuntime.StopRuntime();
                    }
                }
            }

        }
        #endregion

        #region 启动工作流
        /// <summary>
        /// 启动工作流
        /// </summary>
        /// <param name="workflowConfigID">流程配置Key</param>
        /// <param name="workName">工作名称</param>
        /// <param name="data">启动初始化数据</param>
        /// <param name="startByID">发起人</param>
        /// <param name="startDepartmentId">发起部门</param>
        /// <returns></returns>
        public WorkItemInfo StartWorkflow(
            string key,
            string workName,
            DataSet data,
            Guid startByID,
            Guid startDepartmentId,
            bool isValidateData)
        {
            WorkFlowConfigInfo wfConfigInfo = _configService.GetWorkFlowConfigInfoByKey(key, ApplicationContext.Current.IsEnglish);

            return StartWorkflowInternal(wfConfigInfo.Id, workName, true, data, startByID, startDepartmentId, isValidateData);
        }
        #endregion

        #region ChangeWorkflowState
        /// <summary>
        /// 改变流程状态 
        /// </summary>
        /// <param name="workflowID">流程ID</param>
        /// <param name="state">状态</param>
        /// <param name="changeByID">更改人</param>
        public void ChangeWorkflowState(
            Guid workflowID,
            WorkflowState state,
            Guid changeByID)
        {
            ArgumentHelper.AssertGuidNotEmpty(workflowID, "workflowID");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");

            try
            {
                WorkItemInfo flow = GetMainWorkitemInfo(workflowID);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspChangeWorkflowState");

                db.AddInParameter(dbCommand, "@WorkflowInstanceID", DbType.Guid, workflowID);
                db.AddInParameter(dbCommand, "@State", DbType.Byte, state.GetHashCode());
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@FinishDate", DbType.DateTime, DateTime.UtcNow); //数据库存UTC时间
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

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

        #region 保存任务信息
        /// <summary>
        /// 保存任务信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="workflowInstanceID">流程实例ID</param>
        /// <param name="name">工作名</param>
        /// <param name="applicantID">申请人</param>
        /// <param name="isGenerateNo">是否需要生成单号</param>
        /// <param name="applyDepartmentID">申请部门</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="finishDate">结束时间</param>
        /// <param name="state">状态</param>
        /// <param name="workflowConfigID">流程配置ID</param>
        /// <param name="saveByID">保存人</param>
        /// <returns>返回</returns>
        public SingleResult SaveWorkflowInfo(
            Guid? id,
            Guid workflowInstanceID,
            string name,
            bool isGenerateNo,
            Guid applicantID,
            Guid applyDepartmentID,
            DateTime? startDate,
            DateTime? finishDate,
            WorkflowState state,
            Guid workflowConfigID,
            Guid saveByID)
        {
            ArgumentHelper.AssertGuidNotEmpty(workflowInstanceID, "workflowInstanceID");
            ArgumentHelper.AssertGuidNotEmpty(applicantID, "applicantID");
            ArgumentHelper.AssertGuidNotEmpty(applyDepartmentID, "applyDepartmentID");
            ArgumentHelper.AssertGuidNotEmpty(workflowConfigID, "workflowConfigID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspSaveWorkInfo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@WorkflowInstanceID", DbType.Guid, workflowInstanceID);
                db.AddInParameter(dbCommand, "@NAME", DbType.String, name);
                db.AddInParameter(dbCommand, "@IsGenerateNo", DbType.String, isGenerateNo);
                db.AddInParameter(dbCommand, "@ApplicantID", DbType.Guid, applicantID);
                db.AddInParameter(dbCommand, "@ApplyDepartmentID", DbType.Guid, applyDepartmentID);
                db.AddInParameter(dbCommand, "@StartDate", DbType.DateTime, startDate == null ? startDate : startDate.Value.ToDateTimeUTC());  //数据库存UTC时间
                db.AddInParameter(dbCommand, "@FinishDate", DbType.DateTime, finishDate == null ? finishDate : finishDate.Value.ToDateTimeUTC());  //数据库存UTC时间
                db.AddInParameter(dbCommand, "@STATE", DbType.Byte, state);
                db.AddInParameter(dbCommand, "@WorkFlowConfigID", DbType.Guid, workflowConfigID);
                db.AddInParameter(dbCommand, "@MainWorkInfoID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "No", "UpdateDate", "WorkflowInstanceID" });
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

        #region 申请任务
        /// <summary>
        /// 申请任务
        /// </summary>
        /// <param name="workflowInstanceID">流程实例ID</param>
        /// <param name="formFile">关联的表单文件</param>
        /// <param name="formCName">任务中文名</param>
        /// <param name="formEName">任务英文名</param>
        /// <param name="data">表单对应的数据源</param>
        /// <param name="applierID">申请人ID</param>
        /// <param name="participants">下一步执行人ID集合</param>
        /// <returns></returns>
        public Guid ApplyTask(
            Guid workflowInstanceID,
            string formFile,
            string formCName,
            string formEName,
            Guid applierID,
            System.Data.DataSet data,
            Guid[] participants)
        {
            ArgumentHelper.AssertGuidNotEmpty(workflowInstanceID, "workflowInstanceID");
            ArgumentHelper.AssertGuidNotEmpty(applierID, "applierID");

            return AddWorkItem(workflowInstanceID, formFile, formCName, formEName, applierID, applierID, true, data, participants);
        }
        #endregion

        #region 添加WorkItem
        /// <summary>
        /// 添加WorkItem
        /// </summary>
        /// <param name="workflowInstanceID"></param>
        /// <param name="formFile"></param>
        /// <param name="formCName"></param>
        /// <param name="formEName"></param>
        /// <param name="executorID"></param>
        /// <param name="applierID"></param>
        /// <param name="isMain"></param>
        /// <param name="data"></param>
        /// <param name="participants"></param>
        /// <returns></returns>
        public Guid AddWorkItem(Guid workflowInstanceID,
            string formFile,
            string formCName,
            string formEName,
            Guid? executorID,
            Guid applierID,
            bool isMain,
            System.Data.DataSet data,
            Guid[] participants)
        {
            try
            {

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspSaveWorkItemInfo");

                FormProfileInfo formProfile = _configService.GetFormProfileInfo(formFile, ApplicationContext.Current.IsEnglish);
                DataSet ds = new DataSet();

                if (formProfile == null)
                {
                    throw new ApplicationException(SRHelper.GetString("NextNotForm", "下一步没有设置对应的表单"));
                }
                using (TextReader tw = new StringReader(formProfile.DataSchame))
                {
                    ds.ReadXmlSchema(tw);
                    tw.Close();
                }



                InitDataSet(ds);
                if (data == null)
                {
                    data = ds;
                }

                WorkItemInfo flow = GetMainWorkitemInfo(workflowInstanceID);

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, null);
                db.AddInParameter(dbCommand, "@WorkflowInstanceID", DbType.Guid, workflowInstanceID);
                db.AddInParameter(dbCommand, "@CName", DbType.String, formCName);
                db.AddInParameter(dbCommand, "@EName", DbType.String, formEName);
                db.AddInParameter(dbCommand, "@ExecutorID", DbType.Guid, executorID);
                db.AddInParameter(dbCommand, "@StartDate", DbType.DateTime, DateTime.UtcNow.AddSeconds(1));  //数据库存UTC时间(加1秒，保证新记录时间大于上一步完成时间)
                db.AddInParameter(dbCommand, "@FinishDate", DbType.DateTime, null);
                db.AddInParameter(dbCommand, "@State", DbType.Byte, WorkItemState.Waiting);
                db.AddInParameter(dbCommand, "@FormProfileID", DbType.Guid, formProfile.Id);
                db.AddInParameter(dbCommand, "@DataContent", DbType.Xml, data.GetXml());
                db.AddInParameter(dbCommand, "@IsMain", DbType.Boolean, isMain);
                db.AddInParameter(dbCommand, "@ParticipantIds", DbType.String, participants.Join());
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, applierID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResultData result = db.SingleResult(dbCommand);
                return result.ID;
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

        #region 取消流程
        /// <summary>
        /// 取消流程
        /// </summary>
        /// <param name="workflowID">流程id</param>
        /// <param name="cancelByID">取消人</param>
        public void CancelWorkflow(
            Guid workflowID,
            Guid cancelByID)
        {
            ArgumentHelper.AssertGuidNotEmpty(workflowID, "workflowID");
            ArgumentHelper.AssertGuidNotEmpty(cancelByID, "cancelByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspCancelWorkFlow");

                db.AddInParameter(dbCommand, "@WorkflowID", DbType.Guid, workflowID);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, null);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, cancelByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                db.ExecuteNonQuery(dbCommand);

                //删除对应的日志
                //退佣
                RemoveCommissionLog(workflowID, cancelByID, true);
                //业务费用报销
                RemoveExpenseLog(workflowID, cancelByID, true);
                //亏损单审批
                RemoveDeficitLog(workflowID, false);

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

        #region 获取流程的详细信息
        /// <summary>
        /// 获取流程的详细信息
        /// </summary>
        /// <param name="id">流程ID</param>
        /// <returns></returns>
        public WorkInfosInfo GetWorkflowInfo(Guid id)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspGetWorkInfo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                WorkInfosInfo result = (from b in ds.Tables[0].AsEnumerable()
                                        select new WorkInfosInfo
                                        {
                                            ID = b.Field<Guid>("ID"),
                                            No = b.Field<String>("No"),
                                            Name = b.Field<String>("Name"),
                                            ApplicantID = b.Field<Guid>("ApplicantID"),
                                            ApplicantName = b.Field<String>("ApplicantName"),
                                            ApplicantDepartmentID = b.Field<Guid>("ApplicantDepartmentID"),
                                            ApplicantDepartmentName = b.Field<String>("ApplicantDepartmentName"),
                                            StartDate = b.Field<DateTime>("StartDate"),
                                            FinishDate = b.Field<DateTime?>("FinishDate"),
                                            State = (WorkflowState)b.Field<Byte>("State"),
                                            WorkFlowConfigID = b.Field<Guid>("WorkFlowConfigID"),
                                            WorkFlowConfigKey = b.Field<String>("WorkFlowConfigKey"),
                                            MainWorkInfoID = b.Field<Guid>("MainWorkInfoID"),
                                            WorkflowInstanceID = b.Field<Guid>("WorkflowInstanceID"),
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

        #region 更改任务状态
        /// <summary>
        /// 更改任务状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <param name="changeByID"></param>
        public void ChangeWorkItemState(
            Guid id,
            WorkItemState state,
            Guid changeByID,
            Guid workflowInstanceID)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");

            try
            {
                WorkItemInfo flow = GetMainWorkitemInfo(workflowInstanceID);

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspChangeWorkItemState");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@State", DbType.Byte, state);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@FinishDate", DbType.DateTime, DateTime.UtcNow);  //数据库存UTC时间
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

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

        #region 完成任务
        /// <summary>
        /// 完成任务
        /// </summary>
        /// <param name="workitemID">任务ID</param>
        /// <param name="finishByID">执行人</param>
        /// <param name="data">表单对应的数据</param>
        /// <param name="enqueueItem">书签信息</param>
        /// <returns>成功完成true,否则返回false.</returns>
        public bool FinishTask(
            Guid workitemID,
            Guid finishByID,
            System.Data.DataSet data,
            EnqueueItem enqueueItem)
        {
            Guid ownerUserId = Guid.Empty;
            Guid createDepartmentId = Guid.Empty;
            Guid workFlowConfigID = Guid.Empty;
            string workName = string.Empty;
            Guid workFlowId = Guid.Empty;
            Guid workflowInstanceID = Guid.Empty;
            string workNo = string.Empty;
            DateTime startDate = DateTime.Now;

            try
            {
                WorkItemInfo workitem = this.GetWorkitemInfo(workitemID);
                if (workitem == null)
                {
                    throw new ApplicationException(SRHelper.GetString("WorkItemNotExisted", "当前任务已经不存在."));
                }

                WorkInfosInfo workflowInfo = this.GetWorkflowInfo(workitem.WorkFlowID);
                if (workflowInfo.State == WorkflowState.Cancel)
                {
                    throw new ApplicationException(SRHelper.GetString("Workflow Canceled", "流程已经被用户取消,无法再完成"));
                }
                if (workflowInfo.State == WorkflowState.Finished)
                {
                    throw new ApplicationException(SRHelper.GetString("Workflow Finished", "流程已经完成,无法再完成"));
                }
                if (workflowInfo.State == WorkflowState.Return)
                {
                    throw new ApplicationException(SRHelper.GetString("Workflow Return", "流程已经被上级打回,无法再完成"));
                }
                workNo = workflowInfo.No;
                startDate = workflowInfo.StartDate;
                //删除其它待办人
                FindshWorkItem(workitemID, finishByID);

                if (workitem.State == WorkItemState.Processing)
                {
                    //标示任务完成
                    ChangeWorkItemState(
                        workitem.ID,
                        WorkItemState.Finished,
                        finishByID,
                        workitem.WorkflowInstanceID);

                    if (enqueueItem.Opinion)
                    {
                        //如果审批通过 ,继续下一步任务
                        ContinueWorkflow(
                            workitem.WorkflowInstanceID,
                            workitemID,
                            enqueueItem,
                            finishByID);


                    }
                    else
                    {
                        //标志任务被userd打回
                        this.ChangeWorkflowState(
                            workitem.WorkflowInstanceID,
                            WorkflowState.Return,
                            finishByID);

                        ownerUserId = workflowInfo.ApplicantID;
                        createDepartmentId = workflowInfo.ApplicantDepartmentID;
                        workFlowConfigID = workflowInfo.WorkFlowConfigID;
                        workName = workflowInfo.Name;
                        workFlowId = workflowInfo.ID;
                        workflowInstanceID = workitem.WorkflowInstanceID;
                    }
                }
                else
                {
                    throw new ApplicationException(SRHelper.GetString("WorkItemHaveNotBeenSigned", "当前工作项已经完成或者尚未被签收， 不能修改数据。"));
                }
            }
            catch (FaultException<WorkflowExecutorNullExceptionInfo> ex)
            {
                if (ex.Detail != null)
                {
                    ex.Detail.WorkitemId = workitemID;
                    ex.Detail.CallerId = finishByID;
                }
                throw ex;
            }
            catch (Exception e)
            {
                throw e;
            }


            if (enqueueItem.Opinion == false)
            {
                //重新开启一个流程
                WorkItemInfo mainWorkItem = this.GetMainWorkitemInfo(workflowInstanceID);
                DataSet ds = null;
                if (mainWorkItem.WorkFlowDataList != null && mainWorkItem.WorkFlowDataList.Count > 0)
                {
                    ds = mainWorkItem.WorkFlowDataList[0].FormData;
                }
                else
                {
                    ds = mainWorkItem.FormData;
                }

                #region 删除对应的日志
                if (WFCommissionIDList.Contains(workFlowConfigID))
                {
                    //退佣
                    RemoveCommissionLog(workFlowId, finishByID, false);
                }
                if (WFExpenseIDList.Contains(workFlowConfigID))
                {
                    //报销
                    RemoveExpenseLog(workFlowId, finishByID, false);
                }
                if (DeficitOperationID == workFlowConfigID)
                {
                    //亏损
                    RemoveDeficitLog(workflowInstanceID, false);
                }
                #endregion

                WorkItemInfo info = StartWorkflowInternal(
                     workFlowConfigID,
                     workName,
                     true,
                     ds,
                     ownerUserId,
                     createDepartmentId,
                     false);

                SetMainWorkflow(
                   info.ID,
                   workFlowId,
                   ownerUserId);

                ReturnWorlEmailInfo eMailInfo = new ReturnWorlEmailInfo();
                eMailInfo.ApplyID = ownerUserId;
                eMailInfo.No = workNo;
                eMailInfo.NewNo = info.WorkNo;
                eMailInfo.StartDate = startDate;
                eMailInfo.ReturnUserID = finishByID;
                eMailInfo.WorkName = workName;
                eMailInfo.ExecutorID = finishByID;
                if (data.Tables[0] != null && data.Tables[0].Columns.Contains("Remark") &&
                    data.Tables[0].Rows.Count > 0 && data.Tables[0].Rows[0]["Remark"] != null)
                {
                    eMailInfo.Remark = data.Tables[0].Rows[0]["Remark"].ToString();
                }

                ReturnWorkFlowSendEMail(eMailInfo);
            }

            return true;

        }
        #endregion

        #region 流程被打回时，发送邮件通知申请人
        /// <summary>
        /// 流程被打回时，发送邮件通知申请人
        /// </summary>
        /// <param name="applyID">申请人ID</param>
        /// <param name="no">单号</param>
        /// <param name="newNo">新流程的单号</param>
        private void ReturnWorkFlowSendEMail(ReturnWorlEmailInfo eMailInfo)
        {
            UserInfo applyUserInfo = _userService.GetUserInfo(eMailInfo.ApplyID);
            if (applyUserInfo == null || string.IsNullOrEmpty(applyUserInfo.EMail))
            {
                return;
            }

            string emailSubject = GetEMailMessage(eMailInfo);
            string title = IsEnglish ? "Work Flow Return Notice" : "流程被打回通知";
            if (string.IsNullOrEmpty(emailSubject))
            {
                return;
            }

            ICP.Message.ServiceInterface.Message message = new Message();
            message.CreateBy = eMailInfo.ExecutorID;
            message.SendTo = applyUserInfo.EMail;
            message.SendFrom = administratorEmail;
            message.Subject = title;
            message.Body = emailSubject;
            message.Type = MessageType.Email;

            _messageService.Send(message);

        }
        /// <summary>
        /// 拼装邮件发送内容
        /// </summary>
        /// <param name="eMailInfo"></param>
        /// <returns></returns>
        private string GetEMailMessage(ReturnWorlEmailInfo eMailInfo)
        {

            UserInfo returnUserInfo = _userService.GetUserInfo(eMailInfo.ReturnUserID);

            string message = string.Empty;
            if (IsEnglish)
            {
                message = "The process <" + eMailInfo.WorkName + "> of your work No.<" + eMailInfo.No + ">,which made in <" + eMailInfo.StartDate.ToShortDateString() + "> was blocked back by <" + returnUserInfo.EName + "> .the system has applied new process for work No.<{" + eMailInfo.NewNo + "}> in the list (Processing)。Pls handle with it asap." + System.Environment.NewLine + "Remark:" + eMailInfo.Remark;
            }
            else
            {
                message = "您于<" + eMailInfo.StartDate.ToShortDateString() + ">发起的单号为<" + eMailInfo.No + ">的<" + eMailInfo.WorkName + ">流程,被<" + returnUserInfo.CName + ">打回了,打回原因：" + eMailInfo.Remark + System.Environment.NewLine + "系统为您重新申请了单号<" + eMailInfo.NewNo + ">的新流程,在<在办>工作列表中,请赶紧处理";
            }

            return message;
        }


        #endregion

        #region 完成流程
        /// <summary>
        /// 删除其它的待办人
        /// </summary>
        /// <returns></returns>
        public bool FindshWorkItem(Guid workitemID,
                                    Guid finishByID)
        {

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspFinishWorkItem");

                db.AddInParameter(dbCommand, "@WorkItemID ", DbType.Guid, workitemID);
                db.AddInParameter(dbCommand, "@UserID ", DbType.Guid, finishByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                db.ExecuteNonQuery(dbCommand);

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
        #endregion

        #region 重新发起新的流程
        /// <summary>
        /// 重新发起新流程
        /// </summary>
        /// <param name="workflowID"></param>
        /// <param name="mainWorkflowID"></param>
        /// <param name="setByID"></param>
        public void SetMainWorkflow(
            Guid workflowID,
            Guid mainWorkflowID,
            Guid setByID)
        {

            ArgumentHelper.AssertGuidNotEmpty(workflowID, "workflowID");
            ArgumentHelper.AssertGuidNotEmpty(mainWorkflowID, "mainWorkflowID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspSetMainWorkInfo");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, workflowID);
                db.AddInParameter(dbCommand, "@MainWorkInfoID", DbType.Guid, mainWorkflowID);
                db.AddInParameter(dbCommand, "@SetByID", DbType.Guid, setByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

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

        #region 下一步任务
        /// <summary>
        /// 下一步任务
        /// </summary>
        /// <param name="workFlowInstanceId">流程实例ID</param>
        /// <param name="currentWorkItemId">当前任务ID</param>
        /// <param name="enqueueData">数据标签</param>
        /// <param name="setByID">保存人</param>
        private void ContinueWorkflow(
            Guid workFlowInstanceId,
            Guid currentWorkItemId,
            EnqueueItem enqueueData,
            Guid setByID)
        {
            //启动工作流
            WorkflowRuntimeSection wrs = new WorkflowRuntimeSection();
            wrs.EnablePerformanceCounters = false;
            using (WorkflowRuntime workflowRuntime = new WorkflowRuntime(wrs))
            {
                //AutoResetEvent waitHandle = new AutoResetEvent(false);
                Exception faultException = null;
                //备份流程信息

                try
                {
                    workflowRuntime.WorkflowCompleted += delegate (object sender, WorkflowCompletedEventArgs e)
                    {
                        //标志流程完成
                        ChangeWorkflowState(
                             e.WorkflowInstance.InstanceId,
                            WorkflowState.Finished,
                            setByID);
                    };

                    workflowRuntime.WorkflowTerminated += delegate (object sender, WorkflowTerminatedEventArgs e)
                    {
                        if (e.Exception != null && !string.IsNullOrEmpty(e.Exception.Message))
                        {
                            faultException = new Exception(e.Exception.Message + "\r\n" + e.Exception.InnerException);
                        }
                        else
                        {
                            //标志流程完成
                            ChangeWorkflowState(
                             e.WorkflowInstance.InstanceId,
                            WorkflowState.Return,
                            setByID);
                        }

                    };



                    //加入自定义服务
                    workflowRuntime.AddService(this);

                    //加载流程扩展服务
                    workflowRuntime.AddService(_extendService);

                    //加载与业务接口通行的服务

                    workflowRuntime.AddService(_businessDataExchangeService);

                    //加入持久化服务

                    LWSqlWorkflowPersistenceService persistenceService = new LWSqlWorkflowPersistenceService(_connectionString);
                    workflowRuntime.AddService(persistenceService);

                    System.Workflow.Runtime.Hosting.ManualWorkflowSchedulerService manualService = new ManualWorkflowSchedulerService(true);
                    workflowRuntime.AddService(manualService);

                    //启动运行时

                    workflowRuntime.StartRuntime();

                    WorkflowInstance instance = workflowRuntime.GetWorkflow(workFlowInstanceId);
                    if (instance != null)
                    {
                        //加载流程
                        instance.Load();

                        //恢复书签。。继续下一个任务

                        instance.EnqueueItem(currentWorkItemId.ToString(), enqueueData, null, null);

                        manualService.RunWorkflow(instance.InstanceId);
                    }

                    //等待所有线程的结束
                    //waitHandle.WaitOne(new TimeSpan(0, 1, 0), true);

                    if (faultException != null)
                    {
                        if (instance != null)
                        {
                            instance.Abort();
                        }
                        throw faultException;
                    }
                    else
                    {
                        persistenceService.PreSave();
                        //持久化实例

                        instance.TryUnload();
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (workflowRuntime != null)
                    {
                        workflowRuntime.StopRuntime();
                    }
                }
            }
        }
        #endregion

        #region 返回任务信息
        /// <summary>
        /// 返回任务信息
        /// </summary>
        /// <param name="workflowInstanceID"></param>
        /// <returns></returns>
        public WorkItemInfo GetMainWorkitemInfo(Guid workflowInstanceID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspGetMainWorkitemInfo");

                db.AddInParameter(dbCommand, "@WorkflowInstanceID", DbType.Guid, workflowInstanceID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                WorkItemInfo result = (from b in ds.Tables[0].AsEnumerable()
                                       select new WorkItemInfo
                                       {
                                           ID = b.Field<Guid>("ID"),
                                           WorkNo = b.Field<String>("No"),
                                           WorkName = b.Field<String>("Name"),
                                           OwnerUserID = b.Field<Guid>("ApplicantID"),
                                           OrganizationID = b.Field<Guid>("ApplyDepartmentID"),
                                           StartTime = b.Field<DateTime>("StartDate").ToLocalTime(),  //获取时将数据库存UTC时间转本地时间                                           
                                           FinishTime = b.Field<DateTime?>("FinishDate") == null ? b.Field<DateTime?>("FinishDate") : b.Field<DateTime?>("FinishDate").Value.ToLocalTime(),
                                           WorkState = (WorkflowState)b.Field<Byte>("State"),
                                           WorkflowConfigID = b.Field<Guid>("WorkFlowConfigID"),
                                           WorkflowInstanceID = b.Field<Guid>("WorkflowInstanceID")
                                       }).SingleOrDefault();

                result.WorkFlowDataList = (from c in ds.Tables[1].AsEnumerable()
                                           select new WorkFlowData
                                           {
                                               Id = c.Field<Guid>("ID"),
                                               FormDataString = c.Field<String>("DataContent"),
                                               State = (WorkflowState)c.Field<Byte>("State"),
                                               ProfileKey = c.Field<string>("ProfileKey"),
                                               FormSchema = c.Field<string>("FormSchema"),
                                               DataSchema = c.Field<string>("DataScheme"),
                                               WebDataSchema = c.Field<string>("WebDataScheme"),
                                               ExecutorID = c.Field<Guid?>("ExecutorID"),
                                               ExecutorName = c.Field<String>("ExecutorName"),
                                               CName = c.Field<String>("WorkItemCName"),
                                               EName = c.Field<String>("WorkItemEName"),
                                               Name = IsEnglish ? c.Field<String>("WorkItemEName") : c.Field<String>("WorkItemCName"),
                                               IsMain = c.Field<Boolean>("IsMain"),
                                           }).ToList();

                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                foreach (WorkFlowData item in result.WorkFlowDataList)  //将xml转成json string
                {
                    if (item.WebDataSchema != null)
                    {
                        doc.LoadXml(item.WebDataSchema);
                        item.WebDataSchema = Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc);
                    }
                }

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

        #region 获取任务的详细信息
        /// <summary>
        /// 获取任务的详细信息
        /// </summary>
        /// <param name="workitemID">任务ID</param>
        /// <returns>返回任务的详细信息</returns>
        public WorkItemInfo GetWorkitemInfo(Guid workItemID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspGetWorkitemInfo");

                db.AddInParameter(dbCommand, "@WorkItemID", DbType.Guid, workItemID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                WorkItemInfo result = (from b in ds.Tables[0].AsEnumerable()
                                       select new WorkItemInfo
                                       {
                                           ID = b.Field<Guid>("ID"),
                                           WorkFlowID = b.Field<Guid>("WorkInfoId"),
                                           FormDataString = b.Field<String>("DataContent"),
                                           Name = b.Field<String>("WorkItemName"),
                                           State = (WorkItemState)b.Field<Byte>("State"),
                                           FormSchema = b.Field<string>("FormSchema"),
                                           ExecutorID = b.Field<Guid?>("ExecutorID"),
                                           ExecutorName = b.Field<String>("ExecutorName"),
                                           WorkflowInstanceID = b.Field<Guid>("WorkFlowInstanceID"),
                                           WorkflowConfigID = b.Field<Guid>("WorkflowConfigID"),
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

        #region 获取流程所有环节的简要数据
        /// <summary>
        /// 获取流程所有环节的简要数据(用于流程图查看)
        /// </summary>
        /// <param name="workInfoID">工作流ID</param>
        /// <returns></returns>
        public FlowChartInfo GetFlowChartInfo(Guid workInfoID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspGetWorkflowChartInfo");

                db.AddInParameter(dbCommand, "@WorkInfoID", DbType.Guid, workInfoID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                FlowChartInfo result = (from b in ds.Tables[0].AsEnumerable()
                                        select new FlowChartInfo
                                        {
                                            WorkflowID = b.Field<Guid>("WorkflowID"),
                                            WorkflowTitle = b.Field<String>("WorkflowTitle"),
                                            WorkflowNo = b.Field<String>("WorkflowNo"),
                                            WorkflowSate = (WorkflowState)b.Field<byte>("WorkflowSate"),
                                            Nodes = (from c in ds.Tables[1].AsEnumerable()
                                                     select new FlowChartNode
                                                     {
                                                         Id = c.Field<Guid>("Id"),
                                                         Name = c.Field<String>("Name"),
                                                         State = (WorkItemState)c.Field<byte>("State"),
                                                         ExcutorName = c.Field<String>("ExcutorName"),
                                                         CreateTime = c.Field<DateTime>("CreateTime").ToLocalTime(),
                                                         StartTime = c.Field<DateTime?>("StartTime") == null ? c.Field<DateTime?>("StartTime") : c.Field<DateTime?>("StartTime").Value.ToLocalTime(),
                                                         FinishedTime = c.Field<DateTime?>("FinishedTime") == null ? c.Field<DateTime?>("FinishedTime") : c.Field<DateTime?>("FinishedTime").Value.ToLocalTime(),
                                                         IsMainWorkItem = c.Field<bool>("IsMainWorkItem"),

                                                         Principals = (from d in ds.Tables[2].AsEnumerable()
                                                                       where c.Field<Guid>("Id") == d.Field<Guid>("WorkItemID")
                                                                       select d.Field<String>("PrincipalName")
                                                                       ).ToList()
                                                     }).ToList()
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

        #region 保存制定任务的数据
        /// <summary>
        /// 保存制定任务的数据
        /// </summary>
        /// <param name="workitemID">任务id</param>
        /// <param name="saveByID">修改人</param>
        /// <param name="data">任务数据</param>
        public void SetWorkItemData(
            Guid workitemID,
            Guid saveByID,
            System.Data.DataSet data)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspSetWorkItemData");

                db.AddInParameter(dbCommand, "@ID", DbType.Guid, workitemID);
                db.AddInParameter(dbCommand, "@DataContent", DbType.Xml, data.GetXml());
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

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

        #region 指派任务
        /// <summary>
        /// 指派任务
        /// </summary>
        /// <param name="workItemId">任务id </param>
        /// <param name="transmitByID">指派人 </param>
        /// <param name="participants">所指派的人列表</param>
        public void TransmitWorkItem(
            Guid workItemId,
            Guid transmitByID,
            Guid[] participants)
        {
        }
        #endregion

        #region 获取流程列表
        /// <summary>
        /// 获取流程列表
        /// </summary>
        /// <param name="userID">当前用户</param>
        /// <param name="workflowName">流程名</param>
        /// <param name="workName">工作名</param>
        /// <param name="no">单号</param>
        /// <param name="applicantorName">申请人</param>
        /// <param name="type">查询类型1为自己创建的，2为我参与的，3为下属创建的</param>
        /// <param name="stateList">状态列表</param>
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <param name="endFromDate">开始时间</param>
        /// <param name="endToDate">开始时间</param>
        /// <param name="maxResult">最大记录数量</param>
        /// <returns>返回流程列表</returns>
        public string GetWorkList(
            Guid userID,
            Guid? depID,
            string workflowName,
            string workName,
            string no,
            string applicantorName,
            WorkListSearchType? type,
            WorkItemSearchStatus[] stateList,
            DateTime? fromDate,
            DateTime? toDate,
            DateTime? endFromDate,
            DateTime? endToDate,
            int maxResult)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspGetWorkList");

                List<WorkItemSearchStatus> list = new List<WorkItemSearchStatus>();
                list.Add(WorkItemSearchStatus.All);
                list.Add(WorkItemSearchStatus.Cancel);
                list.Add(WorkItemSearchStatus.Finished);
                list.Add(WorkItemSearchStatus.OnceProcess);
                list.Add(WorkItemSearchStatus.Processing);
                list.Add(WorkItemSearchStatus.Return);
                list.Add(WorkItemSearchStatus.Waiting);

                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@OrganizationID", DbType.Guid, depID);
                db.AddInParameter(dbCommand, "@WorkflowName", DbType.String, workflowName);
                db.AddInParameter(dbCommand, "@WorkName", DbType.String, workName);
                db.AddInParameter(dbCommand, "@No", DbType.String, no);
                db.AddInParameter(dbCommand, "@WorkSearchType", DbType.Int32, type);
                db.AddInParameter(dbCommand, "@ApplyByName", DbType.String, applicantorName);
                db.AddInParameter(dbCommand, "@MasterStatus", DbType.String, stateList.Length == 0 ? list.ToArray().Join() : stateList.Join());
                db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
                db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, toDate);
                db.AddInParameter(dbCommand, "@EndFromData", DbType.DateTime, endFromDate);
                db.AddInParameter(dbCommand, "@EndToData", DbType.DateTime, endToDate);
                db.AddInParameter(dbCommand, "@MaxResult", DbType.Int32, maxResult);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<WorkFlowData> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new WorkFlowData
                                              {

                                                  Id = b.Field<Guid>("ID"),
                                                  WorkFlowConfigID = b.Field<Guid>("WorkFlowConfigID"),
                                                  Name = b.Field<String>("WorkName"),
                                                  WorkName = b.Field<String>("WorkFlowName"),
                                                  No = b.Field<String>("No"),
                                                  VoucherNo = b.Field<String>("VoucherNo"),
                                                  StartTime = b.Field<DateTime?>("WorkInfoStartDate"),
                                                  EndTime = b.Field<DateTime?>("WorkInfoEndDate"),
                                                  OwnerUserId = b.Field<Guid>("OwnerUserId"),
                                                  OwnerUserName = b.Field<String>("OwnerUserName"),
                                                  DepartmentName = b.Field<String>("ApplyDepartmentName"),
                                                  DepartmentID = b.Field<Guid>("ApplyDepartmentID"),
                                                  State = (WorkflowState)b.Field<Byte>("WorkInfoState"),
                                                  CurrentWorkItemId = b.Field<Guid>("CurrentWorkItemId"),
                                                  CurrentWorkItemName = b.Field<String>("CurrentWorkItemName"),
                                                  CurrentWorkItemExcutorID = b.Field<Guid?>("CurrentWorkItemExcutorID"),
                                                  CurrentWorkItemExcutorName = b.Field<String>("CurrentWorkItemExcutorName"),
                                                  WorkItemState = (WorkItemSearchStatus)b.Field<Byte>("CurrentWorkItemState"),
                                              }).ToList();

                string result = Newtonsoft.Json.JsonConvert.SerializeObject(results);
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

        #region 获得某个流程的数据
        /// <summary>
        /// 获得某个流程的数据(外部接口使用)
        /// </summary>
        /// <param name="workInfoID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public WorkFlowData GetWorkFlowData(Guid workInfoID, Guid userID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspGetWorkListByWFInfoID");

                db.AddInParameter(dbCommand, "@WorkFlowInfoID", DbType.Guid, workInfoID);
                db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);


                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                WorkFlowData results = (from b in ds.Tables[0].AsEnumerable()
                                        select new WorkFlowData
                                        {

                                            Id = b.Field<Guid>("ID"),
                                            Name = b.Field<String>("WorkName"),
                                            WorkName = b.Field<String>("WorkFlowName"),
                                            No = b.Field<String>("No"),
                                            StartTime = b.Field<DateTime?>("WorkInfoStartDate"),
                                            EndTime = b.Field<DateTime?>("WorkInfoEndDate"),
                                            OwnerUserId = b.Field<Guid>("OwnerUserId"),
                                            OwnerUserName = b.Field<String>("OwnerUserName"),
                                            DepartmentName = b.Field<String>("ApplyDepartmentName"),
                                            DepartmentID = b.Field<Guid>("ApplyDepartmentID"),
                                            State = (WorkflowState)b.Field<Byte>("WorkInfoState"),
                                            CurrentWorkItemId = b.Field<Guid>("CurrentWorkItemId"),
                                            CurrentWorkItemName = b.Field<String>("CurrentWorkItemName"),
                                            CurrentWorkItemExcutorID = b.Field<Guid?>("CurrentWorkItemExcutorID"),
                                            CurrentWorkItemExcutorName = b.Field<String>("CurrentWorkItemExcutorName"),
                                            WorkItemState = (WorkItemSearchStatus)b.Field<Byte>("CurrentWorkItemState"),
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

        #region 获取指定流程任务环节列表
        /// <summary>
        /// 获取指定流程任务环节列表
        /// </summary>
        /// <param name="workflowID">流程id</param>
        /// <returns></returns>
        public List<WorkItemInfo> GetWorkItemDetailList(Guid workflowID)
        {
            ArgumentHelper.AssertGuidNotEmpty(workflowID, "workflowID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspGetWorkItemDetailList");

                db.AddInParameter(dbCommand, "@WorkflowID", DbType.Guid, workflowID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<WorkItemInfo> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new WorkItemInfo
                                              {
                                                  ID = b.Field<Guid>("Id"),
                                                  WorkFlowID = b.Field<Guid>("WorkInfoId"),
                                                  WorkflowConfigID = b.Field<Guid>("WorkflowConfigID"),
                                                  FormID = b.Field<Guid>("FormProfileID"),
                                                  ProfileKey = b.Field<string>("ProfileKey"),
                                                  FormDataString = b.Field<string>("DataContent"),
                                                  FormSchema = b.Field<string>("FormData"),
                                                  DataSchema = b.Field<string>("DataScheme"),
                                                  WebDataSchema = b.Field<string>("WebDataScheme"),
                                                  WorkName = b.Field<String>("WorkItemName"),
                                                  State = (WorkItemState)b.Field<Byte>("State"),
                                                  ExecutorID = b.Field<Guid?>("ExecutorId"),
                                                  ExecutorName = b.Field<String>("ExecutorName"),
                                                  WorkflowInstanceID = b.Field<Guid>("WorkflowInstanceID"),
                                                  IsMain = b.Field<Boolean>("IsMain"),
                                                  WorkFlowPrintTitle = b.Field<string>("WorItemPrintTitle"),

                                                  workItemParticipantsList = (from c in ds.Tables[1].AsEnumerable()
                                                                              where b.Field<Guid>("Id") == c.Field<Guid>("WorkItemID")
                                                                              select new WorkItemParticipantsList
                                                                              {
                                                                                  ID = c.Field<Guid>("ID"),
                                                                                  WorkItemID = c.Field<Guid>("WorkItemID"),
                                                                                  ParticipantID = c.Field<Guid>("ParticipantID"),
                                                                              }).ToList(),



                                              }).ToList();

                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                foreach (WorkItemInfo item in results)  //将xml转成json string
                {
                    if (item.WebDataSchema != null)
                    {
                        doc.LoadXml(item.WebDataSchema);
                        item.WebDataSchema = Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc);
                    }
                }

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

        #region 获取指定流程所有数据的字典列表
        /// <summary>
        /// 获取指定流程所有数据的字典列表(与业务交互时候使用)
        /// </summary>
        /// <param name="workflowInstanceId"></param>
        /// <returns></returns>
        public DataCollector GetDataCollect(Guid workflowInstanceId)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();

            WorkItemInfo flow = GetMainWorkitemInfo(workflowInstanceId);

            if (flow != null)
            {

                WorkFlowConfigInfo workFlowConfigInfo = _configService.GetWorkFlowConfigInfoByID(flow.WorkflowConfigID, ApplicationContext.Current.IsEnglish);
                OrganizationInfo orgInfo = _organizationService.GetOrganizationInfo(flow.OrganizationID);

                //全局字典

                //流程ID

                values.Add(WWFConstants.WorkflowIdCode, flow.ID);
                values.Add(WWFConstants.WorkflowId_C, flow.ID);
                values.Add(WWFConstants.WorkflowId_E, flow.ID);

                //工作名
                values.Add(WWFConstants.WorkNameCode, flow.WorkName);
                values.Add(WWFConstants.WorkName_C, flow.WorkName);
                values.Add(WWFConstants.WorkName_E, flow.WorkName);

                //工作单号
                values.Add(WWFConstants.WorkflowNoCode, flow.WorkNo);
                values.Add(WWFConstants.WorkflowNo_C, flow.WorkNo);
                values.Add(WWFConstants.WorkflowNo_E, flow.WorkNo);

                //流程名
                values.Add(WWFConstants.WorkflowNameCode, workFlowConfigInfo.CDescription);
                values.Add(WWFConstants.WorkflowName_C, workFlowConfigInfo.CDescription);
                values.Add(WWFConstants.WorkflowName_E, workFlowConfigInfo.EDescription);

                //流程状态
                values.Add(WWFConstants.WorkFlowState, (short)flow.State);

                //申请人ID
                values.Add(WWFConstants.ProposerIDCode, flow.OwnerUserID.ToString());
                values.Add(WWFConstants.ProposerID_C, flow.OwnerUserID.ToString());
                values.Add(WWFConstants.ProposerID_E, flow.OwnerUserID.ToString());

                UserInfo userInfo = _userService.GetUserInfo(flow.OwnerUserID);
                //申请人名
                if (userInfo != null)
                {
                    values.Add(WWFConstants.ProposerNameCode, userInfo.CName);
                    values.Add(WWFConstants.ProposerName_C, userInfo.CName);
                    values.Add(WWFConstants.ProposerName_E, userInfo.EName);
                }
                //申请人部门名称
                values.Add(WWFConstants.ProposerDepartmentNameCode, orgInfo.CShortName);
                values.Add(WWFConstants.ProposerDepartmentName_C, orgInfo.CShortName);
                values.Add(WWFConstants.ProposerDepartmentName_E, orgInfo.EShortName);

                //申请人部门全称
                values.Add(WWFConstants.ProposerDepartmentFullNameCode, orgInfo.FullName);
                values.Add(WWFConstants.ProposerDepartmentFullName_C, orgInfo.FullName);
                values.Add(WWFConstants.ProposerDepartmentFullName_E, orgInfo.FullName);


                //申请人部门ID
                values.Add(WWFConstants.ProposerDepartmentIDCode, orgInfo.ID);
                values.Add(WWFConstants.ProposerDepartmentId_C, orgInfo.ID);
                values.Add(WWFConstants.ProposerDepartmentId_E, orgInfo.ID);


                //申请人公司信息

                OrganizationInfo companyInfo = _organizationService.GetOrganizationInfo(flow.OrganizationID);

                if (companyInfo != null)
                {
                    Guid companyID = Guid.Empty;
                    string companyCName = string.Empty;
                    string companyEName = string.Empty;

                    if (companyInfo.Type == OrganizationType.Department)
                    {
                        companyID = companyInfo.ParentID.ToGuid();
                        companyCName = companyEName = companyInfo.ParentName;
                    }
                    else
                    {
                        companyID = companyInfo.ID;
                        companyCName = companyInfo.CShortName;
                        companyEName = companyInfo.EShortName;
                    }

                    //公司ID
                    values.Add(WWFConstants.ProposerCompanyIDCode, companyID);
                    values.Add(WWFConstants.ProposerCompanyId_C, companyID);
                    values.Add(WWFConstants.ProposerCompanyId_E, companyID);

                    //公司名称
                    if (IsEnglish)
                    {
                        values.Add(WWFConstants.ProposerCompanyNameCode, companyCName);
                        values.Add(WWFConstants.ProposerCompanyName_C, companyCName);
                        values.Add(WWFConstants.ProposerCompanyName_E, companyCName);
                    }
                    else
                    {
                        values.Add(WWFConstants.ProposerCompanyNameCode, companyEName);
                        values.Add(WWFConstants.ProposerCompanyName_C, companyEName);
                        values.Add(WWFConstants.ProposerCompanyName_E, companyEName);
                    }
                    //公司全称
                    values.Add(WWFConstants.ProposerCompanyFullNameCode, companyInfo.FullName);
                    values.Add(WWFConstants.ProposerCompanyFullName_C, companyInfo.FullName);
                    values.Add(WWFConstants.ProposerCompanyFullName_E, companyInfo.FullName);


                }
                //流程申请时间
                values.Add(WWFConstants.CurrentDate_E, flow.StartTime);
                values.Add(WWFConstants.CurrentDate_C, flow.StartTime);


                //取对应任务的表单数据
                foreach (WorkFlowData item in flow.WorkFlowDataList.OrderBy(o => o.EndTime))
                {
                    if (item.ExecutorID != null)
                    {
                        values.Add(item.CName + "->" + WWFConstants.CurrentExcutorID_C, item.ExecutorID);
                        values.Add(item.EName + "->" + WWFConstants.CurrentExcutorID_E, item.ExecutorID);

                        values.Add(item.CName + "->" + WWFConstants.CurrentExcutor_C, item.ExecutorName);
                        values.Add(item.EName + "->" + WWFConstants.CurrentExcutor_E, item.ExecutorName);
                    }
                    values.Add(item.CName + "->" + WWFConstants.CurrentFinishDate_C, DateTime.Now);
                    values.Add(item.EName + "->" + WWFConstants.CurrentFinishDate_E, DateTime.Now);

                    DataSet ds = item.FormData;
                    if (ds != null)
                    {

                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count > 0)
                        {
                            #region 循环第一个表中的数据
                            foreach (DataColumn c in ds.Tables[0].Columns)
                            {

                                object value = ds.Tables[0].Rows[0][c];
                                if (value == DBNull.Value)
                                {
                                    if (c.DataType == typeof(Guid))
                                    {
                                        value = Guid.Empty;
                                    }
                                    else if (c.DataType == typeof(decimal) || c.DataType == typeof(Decimal))
                                    {
                                        value = 0;
                                    }
                                    else if (c.DataType == typeof(int) || c.DataType == typeof(Int16) || c.DataType == typeof(Int32))
                                    {
                                        value = 0;
                                    }
                                    else if (c.DataType == typeof(bool) || c.DataType == typeof(Boolean))
                                    {
                                        value = false;
                                    }
                                    else if (c.DataType == typeof(string) || c.DataType == typeof(String))
                                    {
                                        value = string.Empty;
                                    }
                                    else if (c.DataType == typeof(DateTime))
                                    {
                                        value = DateTime.Now;
                                    }
                                }

                                #region 中文表名+列标题
                                string tcname = (item.CName + "->" + c.Caption);
                                if (values.ContainsKey(tcname) == false)
                                {
                                    values.Add(tcname, value);
                                }
                                #endregion

                                #region 英文表名+列标题
                                string tename = (item.EName + "->" + c.Caption);
                                if (values.ContainsKey(tename) == false)
                                {
                                    values.Add(tename, value);
                                }
                                #endregion

                                #region 中文表名+列名
                                string tccname = (item.CName + "->" + c.ColumnName);
                                if (values.ContainsKey(tccname) == false)
                                {
                                    values.Add(tccname, value);
                                }
                                #endregion

                                #region 英文表名+列名
                                string tcename = (item.EName + "->" + c.ColumnName);
                                if (values.ContainsKey(tcename) == false)
                                {
                                    values.Add(tcename, value);
                                }
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            foreach (DataTable dt in ds.Tables)
                            {
                                if (dt.TableName.Equals("MainTable") && dt.Rows.Count > 0)
                                {
                                    #region 主表单数据直接取第一行

                                    foreach (DataColumn c in dt.Columns)
                                    {
                                        object value = dt.Rows[0][c];
                                        if (value == DBNull.Value)
                                        {
                                            if (c.DataType == typeof(Guid))
                                            {
                                                value = Guid.Empty;
                                            }
                                            else if (c.DataType == typeof(decimal) || c.DataType == typeof(Decimal))
                                            {
                                                value = 0;
                                            }
                                            else if (c.DataType == typeof(int) || c.DataType == typeof(Int16) || c.DataType == typeof(Int32))
                                            {
                                                value = 0;
                                            }
                                            else if (c.DataType == typeof(bool) || c.DataType == typeof(Boolean))
                                            {
                                                value = false;
                                            }
                                            else if (c.DataType == typeof(DateTime))
                                            {
                                                value = DateTime.Now;
                                            }
                                        }

                                        #region 中文表名+列标题
                                        string tcname = (item.CName + "->" + c.Caption);
                                        if (values.ContainsKey(tcname) == false)
                                        {
                                            values.Add(tcname, value);
                                        }
                                        #endregion

                                        #region 英文表名+列标题
                                        string tename = (item.EName + "->" + c.Caption);
                                        if (values.ContainsKey(tename.ToUpper()) == false)
                                        {
                                            values.Add(tename, value);
                                        }
                                        #endregion

                                        #region 中文表名+列名
                                        string tccname = (item.CName + "->" + c.ColumnName);
                                        if (values.ContainsKey(tccname) == false)
                                        {
                                            values.Add(tccname, value);
                                        }
                                        #endregion

                                        #region 英文表名+列名
                                        string tcename = (item.EName + "->" + c.ColumnName);
                                        if (values.ContainsKey(tcename) == false)
                                        {
                                            values.Add(tcename, value);
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region 子表的
                                    //如果有子表单则转换为数组类型
                                    foreach (DataColumn c in dt.Columns)
                                    {

                                        List<object> os = new List<object>();
                                        foreach (DataRow row in dt.Rows)
                                        {
                                            object value = row[c];
                                            if (value == DBNull.Value)
                                            {
                                                if (c.DataType == typeof(Guid))
                                                {
                                                    value = Guid.Empty;
                                                }
                                                else if (c.DataType == typeof(decimal) || c.DataType == typeof(Decimal))
                                                {
                                                    value = 0;
                                                }
                                                else if (c.DataType == typeof(int) || c.DataType == typeof(Int16) || c.DataType == typeof(Int32))
                                                {
                                                    value = 0;
                                                }
                                                else if (c.DataType == typeof(bool) || c.DataType == typeof(Boolean))
                                                {
                                                    value = false;
                                                }
                                                else if (c.DataType == typeof(string) || c.DataType == typeof(String))
                                                {
                                                    value = string.Empty;
                                                }
                                            }
                                            os.Add(value);
                                        }



                                        #region 中文表名+列标题
                                        string tname = (item.CName + "->" + c.Caption);
                                        if (values.ContainsKey(tname) == false)
                                        {
                                            values.Add(tname, os.ToArray());
                                        }
                                        #endregion

                                        #region 英文表名+列标题
                                        string ename = (item.EName + "->" + c.Caption);
                                        if (values.ContainsKey(ename) == false)
                                        {
                                            values.Add(ename, os.ToArray());
                                        }
                                        #endregion

                                        #region 中文表名+列名
                                        string tccname = (item.CName + "->" + c.ColumnName);
                                        if (values.ContainsKey(tccname) == false)
                                        {
                                            values.Add(tccname, os.ToArray());
                                        }
                                        #endregion

                                        #region 英文表名+列名
                                        string tcename = (item.EName + "->" + c.ColumnName);
                                        if (values.ContainsKey(tcename) == false)
                                        {
                                            values.Add(tcename, os.ToArray());
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                            }
                        }
                    }
                }
            }
            DataCollector collector = new DataCollector();
            collector.DataCollect = values;

            Dictionary<string, object> nullValueList = (from d in values where string.IsNullOrEmpty(d.Value.ToString()) select d).ToDictionary(c => c.Key, c => c.Value);

            return collector;

        }
        #endregion

        #region 获取指定流程所有数据的字典列表
        /// <summary>
        /// 获取指定流程所有数据的字典列表(与业务交互时候使用)
        /// </summary>
        /// <param name="workflowInstanceId"></param>
        /// <returns></returns>
        public string GetDataCollectString(Guid workflowInstanceId)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();

            WorkItemInfo flow = GetMainWorkitemInfo(workflowInstanceId);

            if (flow != null)
            {

                WorkFlowConfigInfo workFlowConfigInfo = _configService.GetWorkFlowConfigInfoByID(flow.WorkflowConfigID, ApplicationContext.Current.IsEnglish);
                OrganizationInfo orgInfo = _organizationService.GetOrganizationInfo(flow.OrganizationID);

                //全局字典

                //流程ID

                values.Add(WWFConstants.WorkflowIdCode, flow.ID);
                values.Add(WWFConstants.WorkflowId_C, flow.ID);
                values.Add(WWFConstants.WorkflowId_E, flow.ID);

                //工作名
                values.Add(WWFConstants.WorkNameCode, flow.WorkName);
                values.Add(WWFConstants.WorkName_C, flow.WorkName);
                values.Add(WWFConstants.WorkName_E, flow.WorkName);

                //工作单号
                values.Add(WWFConstants.WorkflowNoCode, flow.WorkNo);
                values.Add(WWFConstants.WorkflowNo_C, flow.WorkNo);
                values.Add(WWFConstants.WorkflowNo_E, flow.WorkNo);

                //流程名
                values.Add(WWFConstants.WorkflowNameCode, workFlowConfigInfo.CDescription);
                values.Add(WWFConstants.WorkflowName_C, workFlowConfigInfo.CDescription);
                values.Add(WWFConstants.WorkflowName_E, workFlowConfigInfo.EDescription);

                //流程状态
                values.Add(WWFConstants.WorkFlowState, (short)flow.State);

                //申请人ID
                values.Add(WWFConstants.ProposerIDCode, flow.OwnerUserID.ToString());
                values.Add(WWFConstants.ProposerID_C, flow.OwnerUserID.ToString());
                values.Add(WWFConstants.ProposerID_E, flow.OwnerUserID.ToString());

                UserInfo userInfo = _userService.GetUserInfo(flow.OwnerUserID);
                //申请人名
                if (userInfo != null)
                {
                    values.Add(WWFConstants.ProposerNameCode, userInfo.CName);
                    values.Add(WWFConstants.ProposerName_C, userInfo.CName);
                    values.Add(WWFConstants.ProposerName_E, userInfo.EName);
                }
                //申请人部门名称
                values.Add(WWFConstants.ProposerDepartmentNameCode, orgInfo.CShortName);
                values.Add(WWFConstants.ProposerDepartmentName_C, orgInfo.CShortName);
                values.Add(WWFConstants.ProposerDepartmentName_E, orgInfo.EShortName);

                //申请人部门全称
                values.Add(WWFConstants.ProposerDepartmentFullNameCode, orgInfo.FullName);
                values.Add(WWFConstants.ProposerDepartmentFullName_C, orgInfo.FullName);
                values.Add(WWFConstants.ProposerDepartmentFullName_E, orgInfo.FullName);


                //申请人部门ID
                values.Add(WWFConstants.ProposerDepartmentIDCode, orgInfo.ID);
                values.Add(WWFConstants.ProposerDepartmentId_C, orgInfo.ID);
                values.Add(WWFConstants.ProposerDepartmentId_E, orgInfo.ID);


                //申请人公司信息

                OrganizationInfo companyInfo = _organizationService.GetOrganizationInfo(flow.OrganizationID);

                if (companyInfo != null)
                {
                    Guid companyID = Guid.Empty;
                    string companyCName = string.Empty;
                    string companyEName = string.Empty;

                    if (companyInfo.Type == OrganizationType.Department)
                    {
                        companyID = companyInfo.ParentID.ToGuid();
                        companyCName = companyEName = companyInfo.ParentName;
                    }
                    else
                    {
                        companyID = companyInfo.ID;
                        companyCName = companyInfo.CShortName;
                        companyEName = companyInfo.EShortName;
                    }

                    //公司ID
                    values.Add(WWFConstants.ProposerCompanyIDCode, companyID);
                    values.Add(WWFConstants.ProposerCompanyId_C, companyID);
                    values.Add(WWFConstants.ProposerCompanyId_E, companyID);

                    //公司名称
                    if (IsEnglish)
                    {
                        values.Add(WWFConstants.ProposerCompanyNameCode, companyCName);
                        values.Add(WWFConstants.ProposerCompanyName_C, companyCName);
                        values.Add(WWFConstants.ProposerCompanyName_E, companyCName);
                    }
                    else
                    {
                        values.Add(WWFConstants.ProposerCompanyNameCode, companyEName);
                        values.Add(WWFConstants.ProposerCompanyName_C, companyEName);
                        values.Add(WWFConstants.ProposerCompanyName_E, companyEName);
                    }
                    //公司全称
                    values.Add(WWFConstants.ProposerCompanyFullNameCode, companyInfo.FullName);
                    values.Add(WWFConstants.ProposerCompanyFullName_C, companyInfo.FullName);
                    values.Add(WWFConstants.ProposerCompanyFullName_E, companyInfo.FullName);


                }
                //流程申请时间
                values.Add(WWFConstants.CurrentDate_E, flow.StartTime);
                values.Add(WWFConstants.CurrentDate_C, flow.StartTime);


                //取对应任务的表单数据
                foreach (WorkFlowData item in flow.WorkFlowDataList)
                {
                    if (item.ExecutorID != null)
                    {
                        values.Add(item.CName + "->" + WWFConstants.CurrentExcutorID_C, item.ExecutorID);
                        values.Add(item.EName + "->" + WWFConstants.CurrentExcutorID_E, item.ExecutorID);

                        values.Add(item.CName + "->" + WWFConstants.CurrentExcutor_C, item.ExecutorName);
                        values.Add(item.EName + "->" + WWFConstants.CurrentExcutor_E, item.ExecutorName);
                    }

                    values.Add(item.CName + "->" + WWFConstants.CurrentFinishDate_C, DateTime.Now);
                    values.Add(item.EName + "->" + WWFConstants.CurrentFinishDate_E, DateTime.Now);

                    DataSet ds = item.FormData;
                    if (ds != null)
                    {

                        if (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count > 0)
                        {
                            #region 循环第一个表中的数据
                            foreach (DataColumn c in ds.Tables[0].Columns)
                            {

                                object value = ds.Tables[0].Rows[0][c];
                                if (value == DBNull.Value)
                                {
                                    if (c.DataType == typeof(Guid))
                                    {
                                        value = Guid.Empty;
                                    }
                                    else if (c.DataType == typeof(decimal) || c.DataType == typeof(Decimal))
                                    {
                                        value = 0;
                                    }
                                    else if (c.DataType == typeof(int) || c.DataType == typeof(Int16) || c.DataType == typeof(Int32))
                                    {
                                        value = 0;
                                    }
                                    else if (c.DataType == typeof(bool) || c.DataType == typeof(Boolean))
                                    {
                                        value = false;
                                    }
                                    else if (c.DataType == typeof(string) || c.DataType == typeof(String))
                                    {
                                        value = string.Empty;
                                    }
                                    else if (c.DataType == typeof(DateTime))
                                    {
                                        value = DateTime.Now;
                                    }
                                }

                                #region 中文表名+列标题
                                string tcname = (item.CName + "->" + c.Caption);
                                if (values.ContainsKey(tcname) == false)
                                {
                                    values.Add(tcname, value);
                                }
                                #endregion

                                #region 英文表名+列标题
                                string tename = (item.EName + "->" + c.Caption);
                                if (values.ContainsKey(tename) == false)
                                {
                                    values.Add(tename, value);
                                }
                                #endregion

                                #region 中文表名+列名
                                string tccname = (item.CName + "->" + c.ColumnName);
                                if (values.ContainsKey(tccname) == false)
                                {
                                    values.Add(tccname, value);
                                }
                                #endregion

                                #region 英文表名+列名
                                string tcename = (item.EName + "->" + c.ColumnName);
                                if (values.ContainsKey(tcename) == false)
                                {
                                    values.Add(tcename, value);
                                }
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            foreach (DataTable dt in ds.Tables)
                            {
                                if (dt.TableName.Equals("MainTable") && dt.Rows.Count > 0)
                                {
                                    #region 主表单数据直接取第一行

                                    foreach (DataColumn c in dt.Columns)
                                    {
                                        object value = dt.Rows[0][c];
                                        if (value == DBNull.Value)
                                        {
                                            if (c.DataType == typeof(Guid))
                                            {
                                                value = Guid.Empty;
                                            }
                                            else if (c.DataType == typeof(decimal) || c.DataType == typeof(Decimal))
                                            {
                                                value = 0;
                                            }
                                            else if (c.DataType == typeof(int) || c.DataType == typeof(Int16) || c.DataType == typeof(Int32))
                                            {
                                                value = 0;
                                            }
                                            else if (c.DataType == typeof(bool) || c.DataType == typeof(Boolean))
                                            {
                                                value = false;
                                            }
                                            else if (c.DataType == typeof(DateTime))
                                            {
                                                value = DateTime.Now;
                                            }
                                        }

                                        #region 中文表名+列标题
                                        string tcname = (item.CName + "->" + c.Caption);
                                        if (values.ContainsKey(tcname) == false)
                                        {
                                            values.Add(tcname, value);
                                        }
                                        #endregion

                                        #region 英文表名+列标题
                                        string tename = (item.EName + "->" + c.Caption);
                                        if (values.ContainsKey(tename.ToUpper()) == false)
                                        {
                                            values.Add(tename, value);
                                        }
                                        #endregion

                                        #region 中文表名+列名
                                        string tccname = (item.CName + "->" + c.ColumnName);
                                        if (values.ContainsKey(tccname) == false)
                                        {
                                            values.Add(tccname, value);
                                        }
                                        #endregion

                                        #region 英文表名+列名
                                        string tcename = (item.EName + "->" + c.ColumnName);
                                        if (values.ContainsKey(tcename) == false)
                                        {
                                            values.Add(tcename, value);
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region 子表的
                                    //如果有子表单则转换为数组类型
                                    foreach (DataColumn c in dt.Columns)
                                    {

                                        List<object> os = new List<object>();
                                        foreach (DataRow row in dt.Rows)
                                        {
                                            object value = row[c];
                                            if (value == DBNull.Value)
                                            {
                                                if (c.DataType == typeof(Guid))
                                                {
                                                    value = Guid.Empty;
                                                }
                                                else if (c.DataType == typeof(decimal) || c.DataType == typeof(Decimal))
                                                {
                                                    value = 0;
                                                }
                                                else if (c.DataType == typeof(int) || c.DataType == typeof(Int16) || c.DataType == typeof(Int32))
                                                {
                                                    value = 0;
                                                }
                                                else if (c.DataType == typeof(bool) || c.DataType == typeof(Boolean))
                                                {
                                                    value = false;
                                                }
                                                else if (c.DataType == typeof(string) || c.DataType == typeof(String))
                                                {
                                                    value = string.Empty;
                                                }
                                            }
                                            os.Add(value);
                                        }



                                        #region 中文表名+列标题
                                        string tname = (item.CName + "->" + c.Caption);
                                        if (values.ContainsKey(tname) == false)
                                        {
                                            values.Add(tname, os.ToArray());
                                        }
                                        #endregion

                                        #region 英文表名+列标题
                                        string ename = (item.EName + "->" + c.Caption);
                                        if (values.ContainsKey(ename) == false)
                                        {
                                            values.Add(ename, os.ToArray());
                                        }
                                        #endregion

                                        #region 中文表名+列名
                                        string tccname = (item.CName + "->" + c.ColumnName);
                                        if (values.ContainsKey(tccname) == false)
                                        {
                                            values.Add(tccname, os.ToArray());
                                        }
                                        #endregion

                                        #region 英文表名+列名
                                        string tcename = (item.EName + "->" + c.ColumnName);
                                        if (values.ContainsKey(tcename) == false)
                                        {
                                            values.Add(tcename, os.ToArray());
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                            }
                        }
                    }
                }
            }
            DataCollector collector = new DataCollector();
            collector.DataCollect = values;

            Dictionary<string, object> nullValueList = (from d in values where string.IsNullOrEmpty(d.Value.ToString()) select d).ToDictionary(c => c.Key, c => c.Value);

            string results = Newtonsoft.Json.JsonConvert.SerializeObject(collector);
            return results;

        }
        #endregion

        #region 设置表、行的Default值


        /// <summary>
        /// 初始化DateSet
        /// </summary>
        /// <param name="ds"></param>
        void InitDataSet(DataSet ds)
        {
            if (ds == null)
            {
                return;
            }

            foreach (DataTable dt in ds.Tables)
            {
                SetTableDefaultValue(dt);

                if (dt.Rows.Count == 0)
                {
                    DataRow dr = dt.NewRow();
                    dt.Rows.Add(dr);
                }
            }
            ds.AcceptChanges();
        }
        /// <summary>
        /// 设置行的Default值
        /// </summary>
        /// <param name="row"></param>
        private void SetRowDefaultValue(DataRow row)
        {
            foreach (DataColumn col in row.Table.Columns)
            {
                if (row[col] != System.DBNull.Value)
                {
                    continue;
                }

                switch (col.DataType.Name)
                {
                    case "String":
                        row[col] = "";
                        break;
                    case "Int32":
                        row[col] = (int)0;
                        break;
                    case "Int16":
                        row[col] = (short)0;
                        break;
                    case "Boolean":
                        row[col] = false;
                        break;
                    case "Decimal":
                        row[col] = 0.0m;
                        break;
                    case "Guid":
                        row[col] = Guid.Empty;
                        break;
                    case "DateTime":
                        row[col] = DateTime.Now.ToString("yyyy-MM-dd");
                        break;
                    default:
                        row[col] = new object();
                        break;
                }
            }
        }


        /// <summary>
        /// 设置表的Default值
        /// </summary>
        /// <param name="dr"></param>
        private void SetTableDefaultValue(DataTable table)
        {
            if (table == null)
            {
                return;
            }
            foreach (DataColumn col in table.Columns)
            {
                switch (col.DataType.Name)
                {
                    case "String":
                        col.DefaultValue = "";
                        break;
                    case "Int32":
                        col.DefaultValue = (int)0;
                        break;
                    case "Int16":
                        col.DefaultValue = (short)0;
                        break;
                    case "Boolean":
                        col.DefaultValue = false;
                        break;
                    case "Decimal":
                        col.DefaultValue = 0.0m;
                        break;
                    case "Guid":
                        col.DefaultValue = Guid.Empty;
                        break;
                    case "DateTime":
                        col.DefaultValue = DateTime.Now.ToString("yyyy-MM-dd");
                        break;
                    default:
                        col.DefaultValue = "";
                        break;
                }
            }
        }
        #endregion

        #region 更新主表单里面对应字段的数据
        /// <summary>
        /// 更新主表单里面对应字段的数据
        /// </summary>
        /// <param name="workflowInstanceId">工作流实例ID</param>
        /// <param name="fieldNames"></param>
        /// <param name="fieldValues"></param>
        /// <param name="modes"></param>
        public void SaveMainWorkItemData(
            Guid workflowInstanceId,
            string[] fieldNames,
            object[] fieldValues,
            RewriteMode[] modes,
            Guid saveByID)
        {
            ArgumentHelper.AssertGuidNotEmpty(workflowInstanceId, "workflowInstanceId");
            // ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {

                WorkItemInfo workItemInfo = this.GetMainWorkitemInfo(workflowInstanceId);
                if (workItemInfo.WorkFlowDataList == null
                    || workItemInfo.WorkFlowDataList.Count == 0
                    || workItemInfo.WorkFlowDataList[0].FormData.Tables.Count == 0)
                {
                    return;
                }

                string currentUserName = string.Empty;

                UserInfo userInfo = _userService.GetUserInfo(CurrentUserID);


                if (userInfo != null)
                {
                    currentUserName = this.IsEnglish ? userInfo.EName : userInfo.CName;
                }


                DataSet mainDataSet = workItemInfo.WorkFlowDataList[0].FormData.Copy();
                for (int i = 0; i < fieldNames.Length; i++)
                {
                    //查找对应标题对应的列
                    DataColumn col = null;
                    foreach (DataColumn c in mainDataSet.Tables[MAINTABLENAME].Columns)
                    {
                        if (c.ColumnName.Equals(fieldNames[i]))
                        {
                            col = c;
                            break;
                        }
                    }
                    if (col == null)
                    {
                        continue;
                    }

                    //更新行中对应的列
                    if (mainDataSet.Tables[MAINTABLENAME].Rows.Count == 0)
                    {
                        //如果没数据新增一个默认行
                        DataRow dataRow = mainDataSet.Tables[MAINTABLENAME].NewRow();
                        SetRowDefaultValue(dataRow);
                        mainDataSet.Tables[MAINTABLENAME].Rows.Add(dataRow);
                        mainDataSet.Tables[MAINTABLENAME].AcceptChanges();
                    }

                    foreach (DataRow dr in mainDataSet.Tables[MAINTABLENAME].Rows)
                    {
                        if (modes[i] == RewriteMode.AppendAndExcutor)
                        {
                            if (col.DataType == typeof(string))
                            {
                                if (fieldValues[i] != null
                                    && string.IsNullOrEmpty(fieldValues[i].ToString()) == false)
                                {
                                    string oldv = dr[col.ColumnName].ToString();
                                    if (string.IsNullOrEmpty(oldv) == false)
                                    {
                                        oldv = oldv + Environment.NewLine;
                                    }

                                    dr[col.ColumnName] = oldv
                                        + "【" + currentUserName
                                        + ":"
                                        + fieldValues[i]
                                        + "】";
                                }
                            }
                        }
                        else if (modes[i] == RewriteMode.Append)
                        {
                            if (col.DataType == typeof(string))
                            {
                                dr[col.ColumnName] = dr[col.ColumnName]
                                    + Environment.NewLine
                                    + fieldValues[i];
                            }
                            else
                            {
                                dr[col.ColumnName] = fieldValues[i];
                            }
                        }
                        else
                        {
                            if (mainDataSet.Tables[MAINTABLENAME].Columns[col.ColumnName].DataType == typeof(Boolean) ||
                               mainDataSet.Tables[MAINTABLENAME].Columns[col.ColumnName].DataType == typeof(Boolean?))
                            {
                                bool boolValue = false;
                                if (fieldValues[i] != null)
                                {
                                    if (fieldValues[i].ToString() == "0" || fieldValues[i].ToString() == "true")
                                    {
                                        boolValue = true;
                                    }
                                }
                                dr[col.ColumnName] = boolValue;
                            }
                            else
                            {
                                dr[col.ColumnName] = fieldValues[i];
                            }
                        }
                    }
                }

                this.SetWorkItemData(
                    workItemInfo.WorkFlowDataList[0].Id,
                    CurrentUserID,
                    mainDataSet);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 新建任务
        /// <summary>
        /// 新建任务
        /// </summary>
        /// <param name="workInfoID">流程ID</param>
        /// <param name="cname">中文名</param>
        /// <param name="ename">英文名</param>
        /// <param name="formFile">对应的表单</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="participants">签收人</param>
        /// <returns></returns>
        public Guid NewTask
            (Guid workflowInstanceID,
            string formFile,
            string formCName,
            string formEName,
            Guid applierID,
            System.Data.DataSet data,
            Guid[] participants
            )
        {

            return AddWorkItem(workflowInstanceID, formFile, formCName, formEName, null, applierID, false, data, participants);
        }
        #endregion

        #region 获得流程信息
        /// <summary>
        /// 获取流程信息
        /// </summary>
        /// <returns></returns>
        public string GetWorkInfo(Guid userID, Guid id)
        {
            WorkInfosInfo info = GetWorkflowInfo(id);

            string result = GetWorkList(
            userID,
            null,
            null,
            info.Name,
            info.No,
            string.Empty,
            null,
            new List<WorkItemSearchStatus>().ToArray(),
            null,
            null,
            null,
            null,
            1);
            List<WorkFlowData> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<WorkFlowData>>(result);
            if (list != null && list.Count > 0)
            {
                string returns = Newtonsoft.Json.JsonConvert.SerializeObject(list[0]);
                return returns;
            }
            else
            {
                string returns = Newtonsoft.Json.JsonConvert.SerializeObject(new WorkFlowData());
                return returns;
            }

        }
        #endregion

        #region 是否可进行取消操作
        /// <summary>
        /// 是否可以进行取消操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsCancel(Guid id)
        {
            FlowChartInfo info = GetFlowChartInfo(id);
            List<FlowChartNode> items = null;
            if (info != null)
            {
                items = info.Nodes;
                //是否可以进行取消操作
                if (items.Count <= 1)
                {
                    //只有一个任务明细，可以进行取消
                    return true;
                }
                else if (items.Count == 2)
                {
                    //有两个任务明细，而且第二个的执行人为NULL，可以进行取消
                    if (string.IsNullOrEmpty(items[1].ExcutorName))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
        #endregion

        #region 获得主表单的数据
        /// <summary>
        /// 获得主表单的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WorkItemInfo GetMainItem(Guid id)
        {
            List<WorkItemInfo> list = GetWorkItemDetailList(id);

            var items = from i in list where i.IsMain && i.WorkFlowID == id select i;

            foreach (var mainItem in items)
            {
                WorkItemInfo info = mainItem as WorkItemInfo;

                return info;
            }

            return null;

        }
        #endregion

        #region 删除退佣日志
        /// <summary>
        /// 删除退佣日志
        /// </summary>
        /// <param name="workFlowId">流程ID</param>
        /// <param name="userID">用户ID</param>
        /// <param name="isCheckConfig">是否需要验证流程配置</param>
        public void RemoveCommissionLog(Guid workFlowId, Guid userID, bool isCheckConfig)
        {
            try
            {
                if (isCheckConfig)
                {
                    WorkInfosInfo workinfo = GetWorkflowInfo(workFlowId);
                    if (!WFCommissionIDList.Contains(workinfo.WorkFlowConfigID))
                    {
                        return;
                    }
                }

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspRemoveCommissionLog");

                db.AddInParameter(dbCommand, "@WorkFlowId", DbType.Guid, workFlowId);
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
        #endregion

        #region 删除业务费用报销日志
        /// <summary>
        /// 删除业务费用报销
        /// </summary>
        /// <param name="workFlowId">流程ID</param>
        /// <param name="userID">用户ID</param>
        /// <param name="isCheckConfig">是否需要验证流程配置</param>
        public void RemoveExpenseLog(Guid workFlowId, Guid userID, bool isCheckConfig)
        {
            try
            {
                if (isCheckConfig)
                {
                    WorkInfosInfo workinfo = GetWorkflowInfo(workFlowId);
                    if (!WFExpenseIDList.Contains(workinfo.WorkFlowConfigID))
                    {
                        return;
                    }
                }

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspRemoveExpenseLog");

                db.AddInParameter(dbCommand, "@WorkFlowId", DbType.Guid, workFlowId);
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
        #endregion

        #region 删除亏损单日志
        public void RemoveDeficitLog(Guid workFlowId, bool isCheckConfig)
        {
            try
            {
                if (isCheckConfig)
                {
                    WorkInfosInfo workinfo = GetWorkflowInfo(workFlowId);
                    if (DeficitOperationID == workinfo.WorkFlowConfigID)
                    {
                        return;
                    }
                }

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspRemoveOperationWorkFlowLog");

                db.AddInParameter(dbCommand, "@WorkFlowId", DbType.Guid, workFlowId);
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
        #endregion

        #region 审核流程
        /// <summary>
        /// 审核流程
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="isCheck">是否为审核</param>
        /// <param name="isMerger">是否需要合并凭证号</param>
        /// <param name="audirotBy">审核人</param>
        /// <returns></returns>
        public ManyResult AuditorWork(Guid[] ids, bool isMerger, bool isCheck, Guid audirotBy)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspAuditorWorkFlow");

                db.AddInParameter(dbCommand, "@Ids", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@IsMerger", DbType.Boolean, isMerger);
                db.AddInParameter(dbCommand, "@IsCheck", DbType.Boolean, isCheck);
                db.AddInParameter(dbCommand, "@AuditorById", DbType.Guid, audirotBy);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                ManyResult result = db.ManyResult(dbCommand, new string[] { "ID", "VoucherNo" });

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

        #region  批量完成
        public List<CurrentWorkItem> MultiFinishWork(Guid[] ids, Guid finishBy)
        {
            try
            {
                List<Guid> wfIDlist = new List<Guid>();
                List<CurrentWorkItem> list = GetWorkCurrentItemList(ids);

                #region 循环调用保存方法
                foreach (CurrentWorkItem item in list)
                {
                    #region 不符合条件的
                    //1、流程的状态不是活动的、跳过
                    if (item.WorkflowState != WorkflowState.Activated)
                    {
                        continue;
                    }
                    //2、不是LA\NJ的财务部申请的流程、就跳过
                    if (item.ApplyDepartmentID != new Guid("0BD38686-98F1-4788-A2FE-2C2140372FE0") &&
                        item.ApplyDepartmentID != new Guid("B387D64C-68A3-45F3-90F3-ADB5114C6031"))
                    {
                        continue;
                    }
                    //3、当前步的执行人不包含当前用户、跳过
                    if (!item.UserList.Contains(finishBy))
                    {
                        continue;
                    }
                    //4、当前表单不是审核界面的、跳过
                    if (item.CurrentWorkItemFormID != new Guid("B0EBD0F5-48A8-42A0-A70B-1D5FB9A757EA"))
                    {
                        continue;
                    }
                    #endregion

                    #region 构造DataSet EnqueueItem
                    DataSet ds = new DataSet("LWBaseForm");
                    DataTable dt = new DataTable("MainTable");
                    DataColumn dcOpinion = new DataColumn("Opinion", typeof(bool));
                    DataColumn dcRemark = new DataColumn("Remark", typeof(string));
                    dt.Columns.Add(dcOpinion);
                    dt.Columns.Add(dcRemark);

                    DataRow dr = dt.NewRow();
                    dr["Opinion"] = true;
                    dr["Remark"] = "";

                    dt.Rows.Add(dr);
                    ds.Tables.Add(dt);

                    EnqueueItem enqueueItem = new EnqueueItem(item.CurrentWorkItemID, true, null);

                    #endregion
                    //保存
                    SetWorkItemData(item.CurrentWorkItemID, finishBy, ds);
                    //完成
                    FinishTask(item.CurrentWorkItemID, finishBy, ds, enqueueItem);

                    wfIDlist.Add(item.WorkInfoID);
                }
                #endregion 

                List<CurrentWorkItem> returnList = new List<CurrentWorkItem>();
                if (wfIDlist.Count > 0)
                {
                    returnList = GetWorkCurrentItemList(wfIDlist.ToArray());
                }

                return returnList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CurrentWorkItem> GetWorkCurrentItemList(Guid[] ids)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspGetWorkInfoCurrentitemList");

                db.AddInParameter(dbCommand, "@WorkInfoIDs", DbType.String, ids.Join());
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = db.ExecuteDataSet(dbCommand);

                List<CurrentWorkItem> list = (from d in ds.Tables[0].AsEnumerable()
                                              select new CurrentWorkItem
                                              {
                                                  WorkInfoID = d.Field<Guid>("WorkInfoID"),
                                                  ApplyDepartmentID = d.Field<Guid>("ApplyDepartmentID"),
                                                  No = d.Field<String>("No"),
                                                  WorkflowState = (WorkflowState)d.Field<Byte>("State"),
                                                  WorkflowItemState = WorkItemSearchStatus.All,
                                                  FinishDate = d.Field<DateTime?>("FinishDate"),
                                                  CurrentWorkItemID = d.Field<Guid>("WorkItemID"),
                                                  CurrentWorkItemFormID = d.Field<Guid>("FormProfileID"),
                                                  CurrentWorkItemName = d.Field<string>("WorkItemName"),
                                                  UserList = (from b in ds.Tables[1].AsEnumerable()
                                                              where d.Field<Guid>("WorkItemID") == b.Field<Guid>("WorkItemID")
                                                              select b.Field<Guid>("UserID")).ToList()
                                              }).ToList();




                return list;
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

        #region 删除CostFee纪录 
        public void RemoveCostFee(Guid workflowID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("wf.uspRemoveCostFee");

                db.AddInParameter(dbCommand, "@WorkFlowID", DbType.Guid, workflowID);
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
        #endregion

        #endregion

    }


}
