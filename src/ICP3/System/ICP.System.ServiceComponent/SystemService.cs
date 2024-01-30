//-----------------------------------------------------------------------
// <copyright file="SystemService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.Sys.ServiceComponent
{


    /// <summary>
    /// 系统管理服务
    /// </summary>
    public class SystemService : ICP.Sys.ServiceInterface.ISystemService
    {
        ICP.Framework.CommonLibrary.Server.ISessionService _sessionService = null;
        public SystemService(ICP.Framework.CommonLibrary.Server.ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

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
        /// 获取网卡认证信息列表
        /// </summary>
        /// <param name="code">网卡地址</param>
        /// <param name="senderName">请求认证人</param>
        /// <param name="approverName">审批人</param>
        /// <param name="sendDateFrom">请求认证时间(开始）</param>
        /// <param name="sendDateTo">请求认证时间(结束)</param>
        /// <param name="approveDateFrom">审批时间(开始)</param>
        /// <param name="approveDateTo">审批时间 （结束）</param>
        /// <param name="state">状态</param>
        /// <param name="maxRecords">最大记录数(0:不限制,否则按指定记录返回)</param>
        /// <returns>返回网卡认证信息列表</returns>
        public List<AuthcodeList> GetAuthCodeList(
            string code,
            string senderName,
            string approverName,
            DateTime? sendDateFrom,
            DateTime? sendDateTo,
            DateTime? approveDateFrom,
            DateTime? approveDateTo,
            bool? state,
            int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetAuthCodeList");

                db.AddInParameter(command, "@AuthCode", DbType.String, code);
                db.AddInParameter(command, "@SenderName", DbType.String, senderName);
                db.AddInParameter(command, "@ApproverName", DbType.String, approverName);
                db.AddInParameter(command, "@SendDateFrom", DbType.DateTime, sendDateFrom);
                db.AddInParameter(command, "@SendDateTo", DbType.DateTime, sendDateTo);
                db.AddInParameter(command, "@ApproveDateFrom", DbType.DateTime, approveDateFrom);
                db.AddInParameter(command, "@ApproveDateTo", DbType.DateTime, approveDateTo);
                db.AddInParameter(command, "@State", DbType.Byte, state);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.AddInParameter(command, "@MaxRecords", DbType.Int32, maxRecords);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<AuthcodeList>();
                }

                List<AuthcodeList> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new AuthcodeList
                                              {
                                                  ID = b.Field<Guid>("ID"),
                                                  AuthCode = b.Field<string>("AuthCode"),
                                                  SenderName = b.Field<string>("SenderName"),
                                                  SendDate = b.Field<DateTime>("SendDate"),
                                                  ApproverName = b.Field<string>("ApproverName"),
                                                  ApproveDate = b.Field<DateTime>("ApproveDate"),
                                                  State = b.Field<bool>("State"),
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
        /// 获取认证详细信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回认证详细信息</returns>
        public AuthcodeInfo GetAuthCodeInfo(Guid id)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspGetAuthCodeInfo");

                db.AddInParameter(command, "@ID", DbType.Guid, id);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new AuthcodeInfo();
                }

                AuthcodeInfo result = (from b in ds.Tables[0].AsEnumerable()
                                       select new AuthcodeInfo
                                       {
                                           ID = b.Field<Guid>("ID"),
                                           AuthCode = b.Field<string>("AuthCode"),
                                           SenderName = b.Field<string>("SenderName"),
                                           SendDate = b.Field<DateTime>("SendDate"),
                                           SenderRemark = b.Field<string>("SenderRemark"),
                                           ApproverName = b.Field<string>("ApproverName"),
                                           ApproveDate = b.Field<DateTime>("ApproveDate"),
                                           ApproverRemark = b.Field<string>("ApproverRemark"),
                                           State = b.Field<bool>("State"),
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
        /// 申请网卡认证信息
        /// </summary>
        /// <param name="authCode">网卡地址</param>
        /// <param name="senderId">发送人</param>
        /// <param name="sendDate">发送时间</param>
        /// <param name="senderRemark">申请人备注</param>
        /// <param name="updateDate">版本控制</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData RegisterAuthCodeInfo(
            string authCode,
            Guid senderId,
            DateTime sendDate,
            string senderRemark,
            DateTime? updateDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspSaveAuthCodeInfo");

                db.AddInParameter(command, "@ID", DbType.Guid, null);
                db.AddInParameter(command, "@AuthCode", DbType.String, authCode);
                db.AddInParameter(command, "@SenderID", DbType.Guid, senderId);
                db.AddInParameter(command, "@SendDate", DbType.DateTime, sendDate);
                db.AddInParameter(command, "@SenderRemark", DbType.String, senderRemark);
                db.AddInParameter(command, "@ApproverID", DbType.Guid, null);
                db.AddInParameter(command, "@ApproveDate", DbType.DateTime, null);
                db.AddInParameter(command, "@ApproverRemark", DbType.String, string.Empty);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);
                db.AddInParameter(command, "@UpdateDate", DbType.DateTime, updateDate);

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
        /// 改变状态

        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="remark">备注</param>
        /// <param name="changeByID">修改人</param>
        /// <param name="updateDate">版本控制</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ChangeAuthCodeState(
            Guid id,
            bool isValid,
            string remark,
            Guid changeByID,
            DateTime? updateDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspChangeAuthCodeState");

                db.AddInParameter(command, "@ID", DbType.Guid, id);
                db.AddInParameter(command, "@State", DbType.Byte, isValid);
                db.AddInParameter(command, "@Remark", DbType.String, remark);
                db.AddInParameter(command, "@ChangeByID", DbType.Guid, changeByID);
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
        /// 获取错误日志列表
        /// </summary>
        /// <param name="projectName">项目名</param>
        /// <param name="senderName">发送人</param>
        /// <param name="approveName">审批人</param>
        /// <param name="sendDateFrom">发送时间-开始</param>
        /// <param name="sendDateTo">发送时间-结束</param>
        /// <param name="approveDateFrom">审批时间-开始</param>
        /// <param name="approveDateTo">审批时间-结束</param>
        /// <param name="maxRecords">最大记录数(0:不限制,否则按指定记录返回)</param>
        /// <returns>返回错误日志列表</returns>
        public List<ErrorLogList> GetErrorLogList(
            string projectName,
            string senderName,
            string approveName,
            DateTime? sendDateFrom,
            DateTime? sendDateTo,
            DateTime? approveDateFrom,
            DateTime? approveDateTo,
            int maxRecords)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取错误日志详细信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回错误日志详细信息</returns>
        public ErrorLogInfo GetErrorLogInfo(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 向TFS添加反馈。
        /// </summary>
        /// <param name="title"></param>
        /// <param name="desc"></param>
        /// <param name="area"></param>
        /// <param name="level"></param>
        /// <param name="mail"></param>
        /// <param name="attachmentID"></param>
        /// <param name="attachment"></param>
        public void AddFeedBack(
            string title, 
            string desc,
            string area,
            string level,
            string mail,
            Guid? attachmentID,
            string attachmentName,
            byte[] attachment
            )
        {
            title = title.Replace("'", "''");
            desc = desc.Replace("'", "''");

            //sql
            TFSDB db = new TFSDB();
            try
            {
                string sql = string.Empty;
                if (attachmentID == null)
                {
                    sql = db.SQLCmd.Replace(db.SQLCmdMark_Title, title);
                    sql = sql.Replace(db.SQLCmdMark_Level, level);
                    sql = sql.Replace(db.SQLCmdMark_Desc, desc);
                    sql = sql.Replace(db.SQLCmdMark_Mail, mail);
                    db.ExecuteNonQuery(sql);
                }
                else
                {
                    sql = db.SQLCmd_With_Attchment.Replace(db.SQLCmdMark_Title, title);
                    sql = sql.Replace(db.SQLCmdMark_Level, level);
                    sql = sql.Replace(db.SQLCmdMark_Desc, desc);
                    sql = sql.Replace(db.SQLCmdMark_Mail, mail);
                    sql = sql.Replace(db.SQLCmdMark_Attachment, attachmentName);
                    sql = sql.Replace(db.SQLCmdMark_AttachmentGUID, attachmentID.Value.ToString());
                    db.ExecuteNonQuery(sql);

                    db.SaveAttachment(attachmentID.Value, attachment);
                }
                db.Commit();
            }
            catch
            {
                db.Rollback();
                throw;
            }
        }

        /// <summary>
        /// 保存错误日志详细信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="projectName">项目模块名</param>
        /// <param name="errorContenxt">错误日志内容</param>
        /// <param name="senderId">发送人ID</param>
        /// <param name="sendDate">发送日期</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData SaveErrorLogInfo(
            Guid? id,
            string projectName,
            string errorContenxt,
            Guid senderId,
            DateTime sendDate,
            DateTime? updateDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspSaveErrorLogInfo");

                db.AddInParameter(command, "@ID", DbType.Guid, id);
                db.AddInParameter(command, "@ProjectName", DbType.String, projectName);
                db.AddInParameter(command, "@ErrorContenxt", DbType.String, errorContenxt);
                db.AddInParameter(command, "@SenderId", DbType.Guid, senderId);
                db.AddInParameter(command, "@SendDate", DbType.DateTime, sendDate);
                db.AddInParameter(command, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new SingleResultData();
                }

                SingleResultData result = (from b in ds.Tables[0].AsEnumerable()
                                           select new SingleResultData
                                            {
                                                ID = b.Field<Guid>("ID"),
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
        /// 记录错误日志
        /// </summary>
        /// <param name="projectName">项目模块名</param>
        /// <param name="errorContenxt">错误日志内容</param>
        /// <param name="senderId">发送人ID</param>
        /// <param name="sendDate">发送日期</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData WriteErrorLog(
            string projectName,
            string errorContenxt,
            Guid senderId,
            DateTime sendDate,
            DateTime? updateDate)
        {
            return this.SaveErrorLogInfo(null, projectName, errorContenxt, senderId, sendDate, updateDate);
        }

        /// <summary>
        /// 改变状态

        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="remark">备注</param>
        /// <param name="changeById">修改人ID</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>返回SingleResultData</returns>
        public SingleResultData ChangeErrorLogState(
            Guid id,
            bool isValid,
            string remark,
            Guid changeById,
            DateTime? updateDate)
        {
            throw new NotImplementedException();
        }

        public void SaveUntieLockInfo(
               UntieLockType type,
               Guid[] refIDs,
               Guid saveByID) 
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("sm.uspSaveUntieLockInfo");

                db.AddInParameter(command, "@RefIDs", DbType.String, refIDs.Join());
                db.AddInParameter(command, "@Type", DbType.Byte, (int)type);
                db.AddInParameter(command, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(command);
               
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


        public DataSet GetDataSet(string sql)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetSqlStringCommand(sql);

                DataSet ds = db.ExecuteDataSet(command);

                return ds;
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


        public void ExecSql(string sql)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetSqlStringCommand(sql);

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



    }

    public class TFSDB
    {
        public string SQLCmdMark_Level = "--%Level%--";
        public string SQLCmdMark_Title = "--%Title%--";
        public string SQLCmdMark_Attachment = "--%Attachment%--";
        public string SQLCmdMark_AttachmentGUID = "--%AttachmentGUID%--";
        public string SQLCmdMark_Desc = "--%Desc%--";
        public string SQLCmdMark_Mail = "--%Mail%--";
        public string SQLCmdMark_DescLine = @"<P>{0}</P>
";

        public string SQLCmd_With_Attchment
        {
            get
            {
                return @"exec sp_executesql N'set nocount on
declare @CheckBackend as int exec @CheckBackend = dbo.CheckBackendUserMode  if @CheckBackend <> 0 return
declare @fRollback as bit; set @fRollback=0;
declare @ForceRollbackError as int; set @ForceRollbackError=0;
declare @fVerbose as bit; set @fVerbose=0;
declare @NowUtc as datetime; set @NowUtc=getutcdate()
select @NowUtc
declare @projectId as int;
set @projectId=dbo.GetProjectID(@P2);
if @projectId is null begin raiserror (600150,11,1) with seterror, nowait; return; end; 
select  [CSSNodeId] from dbo.TreeNodes where [ID]=@P2

select [Name],[CSSNodeId] from dbo.TreeNodes where [ID]=@projectId and [TypeID]=-42
declare @PersonId as int
declare @rebuildOK as int
declare @PersonName as nvarchar(255)declare @userSid as nvarchar(256)
set @userSid=@P1
exec @rebuildOK=dbo.RebuildCallersViews @PersonId output,@P3,0,@fVerbose,@projectId,@P1
if @rebuildOK<>0 return

select @PersonName = DisplayPart from dbo.Constants where SID = @P1
declare @bulkUpdateIdList as nvarchar(4000); set @bulkUpdateIdList='''';declare @missingOrUpdatedIdList as nvarchar(4000);set @missingOrUpdatedIdList='''';
select Dbo.GetDbStamp2(default) as DbStamp
declare @RowVer0 as binary(8) set @RowVer0=@P27
declare @RowVer1 as binary(8) set @RowVer1=@P28
declare @RowVer2 as binary(8) set @RowVer2=@P29
declare @RowVer3 as binary(8) set @RowVer3=@P30
declare @RowVer4 as binary(8) set @RowVer4=@P31
declare @RowVer5 as binary(8) set @RowVer5=@P32
declare @RowVer6 as binary(8) set @RowVer6=@P33
declare @RowVer7 as binary(8) set @RowVer7=@P34
declare @RowVer8 as binary(8) set @RowVer8=@P35
declare @RowVer9 as binary(8) set @RowVer9=@P36
exec dbo.GetAdminData default,@RowVer0,@RowVer1,@RowVer2,@RowVer3,@RowVer4,@RowVer5,@RowVer6,@RowVer7,@RowVer8,@RowVer9
set xact_abort on;set implicit_transactions off;set transaction isolation level serializable;begin transaction
declare @O1 as int; insert into dbo.[WorkItemsInsert] ([System.PersonId],[System.ChangedDate],[System.AreaId],[System.IterationId],[System.AssignedTo],[System.CreatedDate],[System.CreatedBy],[System.ChangedBy],[System.Title],[System.State],[System.Reason],[System.WorkItemType],[Microsoft.VSTS.Common.Issue],[Microsoft.VSTS.Common.StateChangeDate],[Microsoft.VSTS.Common.ActivatedDate],[Microsoft.VSTS.Common.ActivatedBy],[Microsoft.VSTS.Common.Priority],[Microsoft.VSTS.Common.Triage],[Microsoft.VSTS.Common.Severity],[Microsoft.VSTS.CMMI.Blocked],[Microsoft.VSTS.CMMI.FoundInEnvironment],[Microsoft.VSTS.CMMI.RootCause]) values (@PersonId,@NowUtc,@P4,@P5,@P6,@NowUtc,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@NowUtc,@NowUtc,@P14,@P15,@P16,@P17,@P18,@P19,@P20)
set @O1=scope_identity(); select @O1
set @bulkUpdateIdList=isnull(@bulkUpdateIdList,'''')+cast(@O1 as nvarchar) + '';''
exec dbo.[WorkItemAddWords] @NowUtc,@O1,@P21,@P22,1
exec dbo.[WorkItemAddWords] @NowUtc,@O1,@P23,@P24,1
declare @FileId1 as int
exec dbo.[WorkItemAddFile] @NowUtc,@O1,''System.AttachedFiles'',''--%AttachmentGUID%--'',@P25,@FileId1 output,default,''2012-10-19T08:29:29.203'',''2012-10-19T08:30:13.939'',3363; select @FileId1
exec dbo.[WorkItemAuthorizeChanges] @PersonId,@NowUtc,@fVerbose,@fRollback output,1,@O1,@projectId,@userSid
if (@fRollback = 1)
begin 
 exec dbo.GetForceRollbackErrorCode @PersonId,@NowUtc,@ForceRollbackError output
end
exec dbo.ForceRollback @fRollback, @ForceRollbackError
if @@trancount=0 return
exec dbo.[WorkItemApplyChanges] @PersonId,@NowUtc,@O1
set transaction isolation level read committed;commit transaction
exec dbo.[WorkItemGetComputedColumns] @O1,@NowUtc,@P26,default,1,null,@userSid
set nocount off',N'@P1 nvarchar(45),@P2 int,@P3 nvarchar(8),@P4 int,@P5 int,@P6 nvarchar(3),@P7 nvarchar(7),@P8 nvarchar(7),@P9 nvarchar(200),@P10 nvarchar(3),@P11 nvarchar(3),@P12 nvarchar(3),@P13 nvarchar(1),@P14 nvarchar(7),@P15 int,@P16 nvarchar(2),@P17 nvarchar(2),@P18 nvarchar(1),@P19 nvarchar(50),@P20 nvarchar(4),@P21 nvarchar(27),@P22 nvarchar(2),@P23 nvarchar(36),@P24 nvarchar(4000),@P25 nvarchar(19),@P26 nvarchar(846),@P27 binary(8),@P28 binary(8),@P29 binary(8),@P30 binary(8),@P31 binary(8),@P32 binary(8),@P33 binary(8),@P34 binary(8),@P35 binary(8),@P36 binary(8)',@P1=N'S-1-5-21-2189582008-2937641874-499621481-1035',@P2=282,@P3=N'WorkItem',@P4=282,@P5=282,@P6=N'zly',@P7=N'icpuser',@P8=N'icpuser',@P9=N'--%Title%--',@P10=N'活动的',@P11=N'已批准',@P12=N'Bug',@P13=N'否',@P14=N'icpuser',@P15=3,@P16=N'挂起',@P17=N'--%Level%--',@P18=N'否',@P19=N'--%Mail%--',@P20=N'编码错误',@P21=N'Microsoft.VSTS.CMMI.Symptom',@P22=N'--',@P23=N'Microsoft.VSTS.CMMI.StepsToReproduce',@P24=N'--%Desc%--',@P25=N'--%Attachment%--',@P26=N'<?xml version=""1.0"" encoding=""utf-16""?><columns><c>System.Id</c><c>System.AreaPath</c><c>System.Title</c><c>System.State</c><c>System.Reason</c><c>System.Rev</c><c>System.AssignedTo</c><c>System.WorkItemType</c><c>System.ChangedBy</c><c>System.ChangedDate</c><c>System.CreatedBy</c><c>System.CreatedDate</c><c>System.AreaId</c><c>System.AuthorizedAs</c><c>System.IterationPath</c><c>System.RevisedDate</c><c>System.PersonId</c><c>Microsoft.VSTS.Common.StateChangeDate</c><c>Microsoft.VSTS.Common.ActivatedDate</c><c>System.IterationId</c><c>Microsoft.VSTS.Common.Issue</c><c>Microsoft.VSTS.Common.ActivatedBy</c><c>Microsoft.VSTS.Common.Priority</c><c>Microsoft.VSTS.Common.Triage</c><c>Microsoft.VSTS.Common.Severity</c><c>Microsoft.VSTS.CMMI.Blocked</c><c>Microsoft.VSTS.CMMI.FoundInEnvironment</c><c>Microsoft.VSTS.CMMI.RootCause</c></columns>',@P27=0x0000000000026584,@P28=0x0000000000008396,@P29=0x0000000000025A75,@P30=0x0000000000025A72,@P31=0x0000000000025B90,@P32=0x000000000002631F,@P33=0x0000000000008397,@P34=0x0000000000025A77,@P35=0x0000000000025A9D,@P36=0x0000000000025901";
            }
        }

        public string SQLCmd
        {
            get
            {
                return @"exec sp_executesql N'set nocount on
declare @CheckBackend as int exec @CheckBackend = dbo.CheckBackendUserMode  if @CheckBackend <> 0 return
declare @fRollback as bit; set @fRollback=0;
declare @ForceRollbackError as int; set @ForceRollbackError=0;
declare @fVerbose as bit; set @fVerbose=0;
declare @NowUtc as datetime; set @NowUtc=getutcdate()
select @NowUtc
declare @projectId as int;
set @projectId=dbo.GetProjectID(@P2);
if @projectId is null begin raiserror (600150,11,1) with seterror, nowait; return; end; 
select  [CSSNodeId] from dbo.TreeNodes where [ID]=@P2

select [Name],[CSSNodeId] from dbo.TreeNodes where [ID]=@projectId and [TypeID]=-42
declare @PersonId as int
declare @rebuildOK as int
declare @PersonName as nvarchar(255)declare @userSid as nvarchar(256)
set @userSid=@P1
exec @rebuildOK=dbo.RebuildCallersViews @PersonId output,@P3,0,@fVerbose,@projectId,@P1
if @rebuildOK<>0 return

select @PersonName = DisplayPart from dbo.Constants where SID = @P1
declare @bulkUpdateIdList as nvarchar(4000); set @bulkUpdateIdList='''';declare @missingOrUpdatedIdList as nvarchar(4000);set @missingOrUpdatedIdList='''';
select Dbo.GetDbStamp2(default) as DbStamp
declare @RowVer0 as binary(8) set @RowVer0=@P26
declare @RowVer1 as binary(8) set @RowVer1=@P27
declare @RowVer2 as binary(8) set @RowVer2=@P28
declare @RowVer3 as binary(8) set @RowVer3=@P29
declare @RowVer4 as binary(8) set @RowVer4=@P30
declare @RowVer5 as binary(8) set @RowVer5=@P31
declare @RowVer6 as binary(8) set @RowVer6=@P32
declare @RowVer7 as binary(8) set @RowVer7=@P33
declare @RowVer8 as binary(8) set @RowVer8=@P34
declare @RowVer9 as binary(8) set @RowVer9=@P35
exec dbo.GetAdminData default,@RowVer0,@RowVer1,@RowVer2,@RowVer3,@RowVer4,@RowVer5,@RowVer6,@RowVer7,@RowVer8,@RowVer9
declare @O1 as int; insert into dbo.[WorkItemsInsert] ([System.PersonId],[System.ChangedDate],[System.AreaId],[System.IterationId],[System.AssignedTo],[System.CreatedDate],[System.CreatedBy],[System.ChangedBy],[System.Title],[System.State],[System.Reason],[System.WorkItemType],[Microsoft.VSTS.Common.Issue],[Microsoft.VSTS.Common.StateChangeDate],[Microsoft.VSTS.Common.ActivatedDate],[Microsoft.VSTS.Common.ActivatedBy],[Microsoft.VSTS.Common.Priority],[Microsoft.VSTS.Common.Triage],[Microsoft.VSTS.Common.Severity],[Microsoft.VSTS.CMMI.Blocked],[Microsoft.VSTS.CMMI.FoundInEnvironment],[Microsoft.VSTS.CMMI.RootCause]) values (@PersonId,@NowUtc,@P4,@P5,@P6,@NowUtc,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@NowUtc,@NowUtc,@P14,@P15,@P16,@P17,@P18,@P19,@P20)
set @O1=scope_identity(); select @O1
set @bulkUpdateIdList=isnull(@bulkUpdateIdList,'''')+cast(@O1 as nvarchar) + '';''
exec dbo.[WorkItemAddWords] @NowUtc,@O1,@P21,@P22,1
exec dbo.[WorkItemAddWords] @NowUtc,@O1,@P23,@P24,1
exec dbo.[WorkItemAuthorizeChanges] @PersonId,@NowUtc,@fVerbose,@fRollback output,1,@O1,@projectId,@userSid
exec dbo.[WorkItemApplyChanges] @PersonId,@NowUtc,@O1
exec dbo.[WorkItemGetComputedColumns] @O1,@NowUtc,@P25,default,1,null,@userSid
set nocount off',N'@P1 nvarchar(45),@P2 int,@P3 nvarchar(8),@P4 int,@P5 int,@P6 nvarchar(3),@P7 nvarchar(3),@P8 nvarchar(3),@P9 nvarchar(100),@P10 nvarchar(3),@P11 nvarchar(3),@P12 nvarchar(3),@P13 nvarchar(1),@P14 nvarchar(3),@P15 int,@P16 nvarchar(2),@P17 nvarchar(2),@P18 nvarchar(1),@P19 nvarchar(50),@P20 nvarchar(4),@P21 nvarchar(27),@P22 nvarchar(9),@P23 nvarchar(36),@P24 nvarchar(4000),@P25 nvarchar(846),@P26 binary(8),@P27 binary(8),@P28 binary(8),@P29 binary(8),@P30 binary(8),@P31 binary(8),@P32 binary(8),@P33 binary(8),@P34 binary(8),@P35 binary(8)',@P1=N'S-1-5-21-2189582008-2937641874-499621481-1027',@P2=282,@P3=N'WorkItem',@P4=282,@P5=282,@P6=N'zly',@P7=N'icpuser',@P8=N'icpuser',@P9=N'--%Title%--',@P10=N'活动的',@P11=N'已批准',@P12=N'Bug',@P13=N'否',@P14=N'icpuser',@P15=3,@P16=N'挂起',@P17=N'--%Level%--',@P18=N'否',@P19=N'--%Mail%--',@P20=N'编码错误',@P21=N'Microsoft.VSTS.CMMI.Symptom',@P22=N'<P>--</P>',@P23=N'Microsoft.VSTS.CMMI.StepsToReproduce',@P24=N'--%Desc%--',@P25=N'<?xml version=""1.0"" encoding=""utf-16""?><columns><c>System.Id</c><c>System.AreaPath</c><c>System.Title</c><c>System.State</c><c>System.Reason</c><c>System.Rev</c><c>System.AssignedTo</c><c>System.WorkItemType</c><c>System.ChangedBy</c><c>System.ChangedDate</c><c>System.CreatedBy</c><c>System.CreatedDate</c><c>System.AreaId</c><c>System.AuthorizedAs</c><c>System.IterationPath</c><c>System.RevisedDate</c><c>System.PersonId</c><c>Microsoft.VSTS.Common.StateChangeDate</c><c>Microsoft.VSTS.Common.ActivatedDate</c><c>System.IterationId</c><c>Microsoft.VSTS.Common.Issue</c><c>Microsoft.VSTS.Common.ActivatedBy</c><c>Microsoft.VSTS.Common.Priority</c><c>Microsoft.VSTS.Common.Triage</c><c>Microsoft.VSTS.Common.Severity</c><c>Microsoft.VSTS.CMMI.Blocked</c><c>Microsoft.VSTS.CMMI.FoundInEnvironment</c><c>Microsoft.VSTS.CMMI.RootCause</c></columns>',@P26=0x0000000000026584,@P27=0x0000000000008396,@P28=0x0000000000025A75,@P29=0x0000000000025A72,@P30=0x0000000000025B90,@P31=0x000000000002631F,@P32=0x0000000000008397,@P33=0x0000000000025A77,@P34=0x0000000000025A9D,@P35=0x0000000000025901";
            }
        }

        string connectionString = @"Data Source=feedback.cityocean.com,1433;Initial Catalog=TfsWorkItemTracking;Persist Security Info=True;User ID=icp3;Password=longwin";

        SqlConnection conn;
        SqlTransaction Tran;
        public TFSDB()
        {
            conn = new SqlConnection(connectionString);
            conn.Open();
            Tran = conn.BeginTransaction("FeedBack");
        }

        public void Commit()
        {
            if (Tran != null)
                Tran.Commit();

            //if (conn != null)
            //    conn.Close();
        }

        public void Rollback()
        {
            if (Tran != null)
                Tran.Rollback();

            //if (conn != null)
            //    conn.Close();
        }

        public void ExecuteNonQuery(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Transaction = Tran;
            cmd.ExecuteNonQuery();
        }

        public void SaveAttachment(Guid id, byte[] fileContent)
        {
            string sql = @"insert into TfsWorkItemTrackingAttachments.dbo.[Attachments] (fileGuid, fileContent, creationDate, projectUri, IdentityName) 
values (@FileGUID, @FileContent, getutcdate() ,N'vstfs:///Classification/TeamProject/4a47cb35-18c2-4b30-9a6b-7045d0839207', N'LWDEVSRV\icpuser')";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Transaction = Tran;
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@FileGUID", DbType = DbType.Guid, Value = id });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@FileContent", DbType = DbType.Binary, Value = fileContent, Size = fileContent.Length });
            cmd.ExecuteNonQuery();
        }
    }
}