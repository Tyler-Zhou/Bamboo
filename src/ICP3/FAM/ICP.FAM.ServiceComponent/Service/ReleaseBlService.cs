using ICP.Common.ServiceInterface;

namespace ICP.FAM.ServiceComponent
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using ServiceInterface.DataObjects;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Framework.CommonLibrary;
    using Framework.CommonLibrary.Common;
    using Framework.CommonLibrary.Helper;
    using Framework.CommonLibrary.Client;
    using FCM.Common.ServiceInterface.DataObjects;
    using Message.ServiceInterface;
    using Sys.ServiceInterface.DataObjects;
    using FCM.OceanExport.ServiceInterface.DataObjects;
    using System.Text;
    using System.Diagnostics;
    using DataCache.ServiceInterface;
    using System.Globalization;
    using System.IO;
    using ICP.Common.ServiceInterface.DataObjects;

    /// <summary>
    /// 放单
    /// </summary>
    public partial class FinanceService
    {

        #region GetList

        /// <summary>
        /// 获取放单列表
        /// </summary>
        /// <param name="CompanyIds"></param>
        /// <param name="releaseBLSearchStatue"></param>
        /// <param name="releaseRCSearchStatue"></param>
        /// <param name="applyReleaseSearchStatue"></param>
        /// <param name="receiveRCSearchStatue"></param>
        /// <param name="ReceiveOBL"></param>
        /// <param name="releaseType">放单状态(正本 1 ,电放 2)可为空</param>
        /// <param name="blNo">提单号</param>
        /// <param name="soNo"></param>
        /// <param name="ctnNo">箱号</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="customerName">客户</param>
        /// <param name="vessel">船名</param>
        /// <param name="voyageNo">航次</param>
        /// <param name="ChangedReleaseType"></param>
        /// <param name="applyBeginTime"></param>
        /// <param name="applyEndTime"></param>
        /// <param name="releaseBeginTime">申请放单时间</param>
        /// <param name="releaseEndTime">申请放单时间</param>
        /// <param name="etdBeginTime">ETD</param>
        /// <param name="etdEndTime">ETD</param>
        /// <param name="acceptBeginDate"></param>
        /// <param name="acceptEndDate"></param>
        /// <param name="etaBeginTime"></param>
        /// <param name="etaEndTime"></param>
        /// <param name="isMonthlyPay"></param>
        /// <param name="isTrustRelease"></param>
        /// <param name="isSWBRelease"></param>
        /// <param name="isOverSeaMBL"></param>
        /// <param name="isAirandOther"></param>
        /// <param name="dataPageInfo">包含了 当前页码数 每页显示行数 排序名</param>
        /// <returns></returns>
        public ReleasePageList GetReleaseBLListByList(Guid[] CompanyIds
                                            , ReleaseBLSearchStatue releaseBLSearchStatue
                                            , ReleaseRCSearchStatue releaseRCSearchStatue
                                            , ApplyReleaseSearchStatue applyReleaseSearchStatue
                                            , ReceiveRCSearchStatue receiveRCSearchStatue
                                            , int ReceiveOBL
                                            , ReleaseType? releaseType
                                            , string blNo
                                            , string soNo
                                            , string ctnNo
                                            , string operationNo
                                            , string customerName
                                            , string vessel
                                            , string voyageNo
                                            , int ChangedReleaseType
                                            , DateTime? applyBeginTime
                                            , DateTime? applyEndTime
                                            , DateTime? releaseBeginTime
                                            , DateTime? releaseEndTime
                                            , DateTime? etdBeginTime
                                            , DateTime? etdEndTime
                                            , DateTime? acceptBeginDate
                                            , DateTime? acceptEndDate
                                            , DateTime? etaBeginTime
                                            , DateTime? etaEndTime
                                            , bool isMonthlyPay
                                            , bool isTrustRelease
                                            , bool isSWBRelease
                                            , bool isOverSeaMBL
                                            , bool isAirandOther
                                            , DataPageInfo dataPageInfo)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetReleaseBLList");

                string CompanyIdList = CompanyIds.Join();

                db.AddInParameter(dbCommand, "@CompanyIds", DbType.String, CompanyIdList);
                db.AddInParameter(dbCommand, "@ReleaseBLSearchStatue", DbType.Byte, releaseBLSearchStatue);
                db.AddInParameter(dbCommand, "@ReleaseRCSearchStatue", DbType.Byte, releaseRCSearchStatue);
                db.AddInParameter(dbCommand, "@ApplyReleaseSearchStatue", DbType.Byte, applyReleaseSearchStatue);
                db.AddInParameter(dbCommand, "@ReceiveRCSearchStatue", DbType.Byte, receiveRCSearchStatue);
                db.AddInParameter(dbCommand, "@ReceiveOBL", DbType.Byte, (byte)ReceiveOBL);
                db.AddInParameter(dbCommand, "@ReleaseType", DbType.Byte, releaseType);
                db.AddInParameter(dbCommand, "@BlNo", DbType.String, blNo);
                db.AddInParameter(dbCommand, "@SoNo", DbType.String, soNo);
                db.AddInParameter(dbCommand, "@ctnNo", DbType.String, ctnNo);
                db.AddInParameter(dbCommand, "@operationNo", DbType.String, operationNo);
                db.AddInParameter(dbCommand, "@customerName", DbType.String, customerName);
                db.AddInParameter(dbCommand, "@vessel", DbType.String, vessel);
                db.AddInParameter(dbCommand, "@voyageNo", DbType.String, voyageNo);
                db.AddInParameter(dbCommand, "@ChangedReleaseType", DbType.Int32, ChangedReleaseType);
                db.AddInParameter(dbCommand, "@applyBeginTime", DbType.DateTime, applyBeginTime);
                db.AddInParameter(dbCommand, "@applyEndTime", DbType.DateTime, applyEndTime);
                db.AddInParameter(dbCommand, "@releaseBeginTime", DbType.DateTime, releaseBeginTime);
                db.AddInParameter(dbCommand, "@releaseEndTime", DbType.DateTime, releaseEndTime);
                db.AddInParameter(dbCommand, "@etdBeginTime", DbType.DateTime, etdBeginTime);
                db.AddInParameter(dbCommand, "@etdEndTime", DbType.DateTime, etdEndTime);
                db.AddInParameter(dbCommand, "@AcceptBeginDate", DbType.DateTime, acceptBeginDate);
                db.AddInParameter(dbCommand, "@AcceptEndDate", DbType.DateTime, acceptEndDate);

                db.AddInParameter(dbCommand, "@pageSize", DbType.Int32, dataPageInfo.PageSize);
                db.AddInParameter(dbCommand, "@currentPage", DbType.Int32, dataPageInfo.CurrentPage);
                db.AddInParameter(dbCommand, "@OrderByName", DbType.String, dataPageInfo.SortByName + " " + dataPageInfo.SortOrderType.ToString());

                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                db.AddInParameter(dbCommand, "@ETABeginTime", DbType.DateTime, etaBeginTime);
                db.AddInParameter(dbCommand, "@ETAEndTime", DbType.DateTime, etaEndTime);

                db.AddInParameter(dbCommand, "@IsMonthlyPay", DbType.Boolean, isMonthlyPay);
                db.AddInParameter(dbCommand, "@IsTrustRelease", DbType.Boolean, isTrustRelease);
                db.AddInParameter(dbCommand, "@IsSWBRelease", DbType.Boolean, isSWBRelease);
                db.AddInParameter(dbCommand, "@IsOverSeaMBL", DbType.Boolean, isOverSeaMBL);
                db.AddInParameter(dbCommand, "@IsAirandOther", DbType.Boolean, isAirandOther);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1) { return null; }

                List<ReleaseBLList> results = BulidReleaseBLListByDataSet(ds);
                dataPageInfo.TotalCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
                ReleasePageList PageList = ReleasePageList.Create<ReleaseBLList>(results, dataPageInfo);
                PageList.CreatedCount = int.Parse(ds.Tables[1].Rows[0][1].ToString());
                PageList.IssueCount = int.Parse(ds.Tables[1].Rows[0][2].ToString());


                return PageList;
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

        private List<ReleaseBLList> BulidReleaseBLListByDataSet(DataSet ds)
        {
            List<ReleaseBLList> results = (from b in ds.Tables[0].AsEnumerable()
                                           select new ReleaseBLList
                                          {
                                              ID = b.Field<Guid>("ID"),
                                              State = (ReleaseBLState)b.Field<byte>("State"),
                                              ETD = b.Field<DateTime?>("ETD"),
                                              BlNo = b.Field<string>("BlNo"),
                                              BLID = b.Field<Guid?>("BLID"),
                                              AgentName = b.Field<string>("AgentName"),
                                              FormType = (FormType)b.Field<byte>("FormType"),
                                              ContainerNos = b.Field<string>("ContainerNos"),
                                              VesselVoyage = b.Field<string>("VesselVoyage"),
                                              CustomerCName = b.Field<string>("CustomerCName"),
                                              CustomerEName = b.Field<string>("CustomerEName"),
                                              CustomerContact = b.Field<string>("CustomerContact"),
                                              ETA = b.Field<DateTime?>("ETA"),
                                              ReleaseType = (ReleaseType)b.Field<byte>("ReleaseType"),
                                              PaymentTerm = b.Field<string>("PaymentTerm"),
                                              IsMonthlyStatement = b.Field<bool>("IsMonthlyStatement"),
                                              IsAlwayTelex = b.Field<bool>("IsAlwayTelex"),
                                              IsAlwaySWB = b.Field<bool>("IsAlwaySWB"),
                                              IsWriteOff = b.Field<bool>("IsWriteOff"),
                                              IsPaid = b.Field<bool>("IsPaid"),
                                              ReleaseDate = b.Field<DateTime?>("ReleaseDate"),
                                              ReleaseBy = b.Field<string>("ReleaseBy"),
                                              HasNoticedTelex = b.Field<bool>("HasNoticedTelex"),
                                              AgentReceivedTelex = b.Field<bool>("AgentReceivedTelex"),
                                              TelexNo = b.Field<string>("TelexNo"),
                                              HasSentOriginal = b.Field<bool>("HasSentOriginal"),
                                              ExpressOrderNo = b.Field<string>("ExpressOrderNo"),
                                              Remark = b.Field<string>("Remark"),
                                              OperationType = b["OperationType"] == DBNull.Value ? OperationType.Unknown : (OperationType)b.Field<byte>("OperationType"),
                                              OperationID = b.Field<Guid>("OperationID"),
                                              CustomerID = b.Field<Guid>("CustomerID"),
                                              CreateByID = b.Field<Guid>("CreateByID"),
                                              CreateByName = b.Field<string>("CreateByName"),
                                              CreateDate = b.Field<DateTime>("CreateDate"),
                                              UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                              BLUpdateDate = b.Field<DateTime?>("BLUpdateDate"),
                                              ApplyDate = b.Field<DateTime?>("ApplyedDate"),
                                              IsApplyTelex = b.Field<DateTime?>("ApplyedDate") != null,
                                              IsDirty = false,
                                              ConsigneeCName = b.Field<string>("ConsigneeCName"),
                                              ConsigneeEName = b.Field<string>("ConsigneeEName"),
                                              OperationNo = b.Field<string>("OperationNo"),
                                              MBLID = b.Field<Guid?>("MBLID"),
                                              AgentID = b.Field<Guid?>("AgentID"),
                                              FilerEmail = b.Field<string>("FilerEmail"),
                                              POLFilerEmail = b.Field<string>("PODFilerEmail"),
                                              RBLA = b.Field<bool>("RBLA"),
                                              RBLD = b.Field<bool>("RBLD"),
                                              RBLRcv = b.Field<bool>("RBLRcv"),
                                              BLRC = b.Field<bool>("BLRC"),
                                              IsMBL = b.Field<bool>("IsMBL"),
                                              IsCRelease = b.Field<bool>("IsCRelease"),
                                              IsOverSeaMBL = b.Field<bool>("IsOverSeaMBL"),
                                              IsNotReForHbl = b.Field<bool>("IsNotReForHbl"),
                                              OBLRec = b.Field<bool>("OBLRec"),
                                              ArEmail = b.Field<string>("ArEmail"),
                                              IsExRelease = b.Field<bool>("IsExRelease"),
                                          }).ToList();
            return results;
        }

        /// <summary>
        /// 获取还未放单列表
        /// </summary>
        /// <returns>ReleaseAndArList</returns>
        public List<ReleaseAndArList> GetReleaseAndArList(Guid? OperationID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationReleaseAndArList");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, OperationID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<ReleaseAndArList> results = BulidReleaseAndArListByDataSet(ds);

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

        private List<ReleaseAndArList> BulidReleaseAndArListByDataSet(DataSet ds)
        {
            List<ReleaseAndArList> results = new List<ReleaseAndArList>();
            List<ReleaseAndArList> releaseList = null;
            List<ReleaseAndArList> arlist = null;
            if (ds.Tables[0] != null)
            {
                releaseList = (from b in ds.Tables[0].AsEnumerable()
                               select new ReleaseAndArList
                               {
                                   ID = b.Field<Guid>("ID"),
                                   OperationID = b.Field<Guid>("OperationID"),
                                   CompanyID = b.Field<Guid>("CompanyID"),
                                   CompanyName = b.Field<string>("CompanyName"),
                                   No = b.Field<string>("No"),
                                   SONO = b.Field<string>("SONO"),
                                   SalesID = b.Field<Guid?>("SalesID"),
                                   SalesName = b.Field<string>("SalesName"),
                                   SalesEmail = b.Field<string>("SalesEmail"),
                                   BookingerID = b.Field<Guid?>("BookingerID"),
                                   BookingerName = b.Field<string>("BookingerName"),
                                   BookingerEmail = b.Field<string>("BookingerEmail"),
                                   Days = b.Field<int>("Days"),
                                   ETA = b.Field<DateTime>("ETA"),
                                   PODID = b.Field<Guid?>("PODID"),
                                   CustomerID = b.Field<Guid?>("CustomerID"),
                                   PODName = b.Field<string>("PODName")
                               }).ToList();
            }

            if (releaseList != null)
                results.AddRange(releaseList);
            if (arlist != null)
                results.AddRange(arlist);

            return results;
        }

        /// <summary>
        /// 获取放单列表
        /// </summary>
        /// <param name="ids">IDs</param>
        /// <returns>ReleaseBLList</returns>
        public List<ReleaseBLList> GetReleaseBLListByIds(Guid[] ids)
        {
            ArgumentHelper.AssertGuidNotEmpty(ids, "ids");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetReleaseBLListByIDs");

                string tempIds = ids.Join();

                db.AddInParameter(dbCommand, "@IDs", DbType.String, tempIds);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<ReleaseBLList> results = BulidReleaseBLListByDataSet(ds);

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

        /// <summary>
        /// 根据客户从历史放单找出客户联系人
        /// </summary>
        /// <param name="customerID">customerID</param>
        /// <returns>ContactID</returns>
        public string GetReleaseBLRecentlyCustomerContact(Guid customerID)
        {
            ArgumentHelper.AssertGuidNotEmpty(customerID, "customerID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetReleaseBLRecentlyCustomerContact");

                db.AddInParameter(dbCommand, "@customerID", DbType.Guid, customerID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count == 0) { return string.Empty; }

                string contact = ds.Tables[0].Rows[0][0] == null ? string.Empty : ds.Tables[0].Rows[0][0].ToString();
                return contact;

            }
            catch (SqlException sqlException) { throw new ApplicationException(sqlException.Message); }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 保存放单
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="customerId">客户</param>
        /// <param name="customerContact">客户联系人</param>
        /// <param name="type">类型(正本 1 ,电放 2)</param>
        /// <param name="state">状态(已创建=1、已签收=2、已放单=3、已接收=4)</param>
        /// <param name="telexNo">电放号</param>
        /// <param name="expressOrderNo">快递单号</param>
        /// <param name="remark">备注</param>
        /// <param name="savebyID">保存人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <param name="blUpdateDate">提单更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public SingleResult SaveReleaseBLInfo(
            Guid id,
            Guid customerId,
            string customerContact,
            ReleaseType type,
            ReleaseBLState state,
            string telexNo,
            string expressOrderNo,
            string remark,
            Guid savebyID,
            DateTime? updateDate,
            DateTime? blUpdateDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveReleaseBLInfo");

                #region 传递参数

                db.AddInParameter(dbCommand, "@id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@customerId", DbType.Guid, customerId);
                db.AddInParameter(dbCommand, "@customerContact", DbType.String, customerContact);
                db.AddInParameter(dbCommand, "@type", DbType.Int16, type);
                db.AddInParameter(dbCommand, "@state", DbType.Int16, state);

                db.AddInParameter(dbCommand, "@telexNo", DbType.String, telexNo);
                db.AddInParameter(dbCommand, "@expressOrderNo", DbType.String, expressOrderNo);
                db.AddInParameter(dbCommand, "@remark", DbType.String, remark);

                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, savebyID);
                db.AddInParameter(dbCommand, "@updateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@blUpdateDate", DbType.DateTime, blUpdateDate);

                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                #endregion

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate", "BLUpdateDate" });
                return result;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                string strerror = ClientHelper.GetErrorMessageForNetwork(ex);
                throw new ApplicationException(strerror);
            }
        }

        /// <summary>
        /// 改变放单状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="no">电放号或快递号</param>
        /// <param name="state">状态(已创建=1、已签收=2、已放单=3、已接收=4)</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <param name="blUpdateDate">提单更新时间-做数据版本用</param>
        /// <param name="agentId">发邮件用：代理ID</param>
        /// <param name="blNo">发邮件用：提单号</param>
        /// <param name="name">发邮件用：当前用户名</param>
        /// <param name="filerEmail">发邮件用：客服Email</param>
        /// <returns>返回SingleResult</returns>
        public SingleResult ChangeReleaseBLState(
            Guid id,
            ReleaseBLState state,
            bool rbld,
            string no,
            Guid changeByID,
            DateTime? updateDate,
            DateTime? blUpdateDate,
            bool isCancelRelease,
            Guid? agentId,
            string blNo,
            string name,
            string filerEmail,
            string podFilerEmail,
            bool isCRelease,
            bool isOverSeaMBL,
            Guid operationID
            )
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");
            ArgumentHelper.AssertGuidNotEmpty(operationID, "changeByID");
            //string mac = getMacAddr_Local();

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspChangeReleaseBLState");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@State", DbType.Int16, state);
                db.AddInParameter(dbCommand, "@RBLD", DbType.Boolean, rbld);
                db.AddInParameter(dbCommand, "@TelexNo", DbType.String, no);
                db.AddInParameter(dbCommand, "@Mac", DbType.String, ApplicationContext.Current.MacAddress);
                db.AddInParameter(dbCommand, "@updateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@blUpdateDate", DbType.DateTime, blUpdateDate);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                db.AddInParameter(dbCommand, "@IsCancelRelease", DbType.Boolean, isCancelRelease);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate", "BLUpdateDate", "ReleaseType" });

                ReleaseType releasetype = ReleaseType.Unknown;
                releasetype = (ReleaseType)result.GetValue<byte>("ReleaseType");

                #region 邮件通知

                ContactList contactlist = GetContactListOfAgent(agentId);

                OceanBookingInfo bookinginfo = _oceanexportservice.GetOceanBookingInfo(operationID);

                string sendTo = string.Empty;
                //邮件通知代理联系人
                if (contactlist != null)
                {
                    sendTo = contactlist.ContactEmail;
                }
                else
                {
                    sendTo = filerEmail;
                }
                if (!string.IsNullOrEmpty(sendTo))
                {
                    string subject = "";
                    string content = "";
                    if (rbld)
                    {
                        if (contactlist == null || isCRelease)
                        {
                            if (isCRelease)
                            {
                                content = subject = LocalData.IsEnglish ? "Again Release BL! Please get the request of Release BL, " + blNo + " which is sent from " + name + "at" + DateTime.Now + ",and ReleaseType is " + releasetype + ",Thanks !" : "重新放单！请接收这个放单请求,提单为：" + blNo + " 业务号为：" + bookinginfo.No + " 收货人为:" + bookinginfo.ConsigneeName + "，放单类型为：[" + releasetype + "]，是在" + DateTime.Now + "发自" + name + ",谢谢!";
                            }
                            else
                            {
                                content = subject = LocalData.IsEnglish ? "Please get the request of Release BL, " + blNo + " which is sent from " + name + "at" + DateTime.Now + ",Thanks !" : "请接收这个放单请求,提单为：" + blNo + " 业务号为：" + bookinginfo.No + " 收货人为" + bookinginfo.ConsigneeName + " ，是在" + DateTime.Now + "发自" + name + ",谢谢!";
                            }
                            if (releasetype == ReleaseType.Telex || releasetype == ReleaseType.seaway || isCRelease || isOverSeaMBL)
                            {
                                SendEmail(sendTo, subject, content, operationID);
                            }
                        }
                    }
                    else
                    {
                        content = subject = LocalData.IsEnglish ? "Please cancel the Release BL, " + blNo + " which is sent from " + name + "at" + DateTime.Now + ",Thanks !" : "请取消这个提单为：" + blNo + " 业务号为：" + bookinginfo.No + "的放单指令，是在" + DateTime.Now + "发自" + name + ",谢谢!";
                        SendEmail(sendTo, subject, content, operationID);
                    }

                }
                #endregion

                return result;
            }

            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                string strerror = ClientHelper.GetErrorMessageForNetwork(ex);
                throw new ApplicationException(strerror);
            }
        }

        /// <summary>
        /// 发送Email
        /// </summary>
        /// <param name="sendTo">接收人</param>
        /// <param name="subject">主题</param>
        /// <param name="content">内容</param>
        public void SendEmail(string sendTo, string subject, string content)
        {
            try
            {
                UserInfo userInfo = new UserInfo();
                userInfo = userService.GetUserInfo(ApplicationContext.Current.UserId);
                Message message = new Message();
                message.CreateBy = ApplicationContext.Current.UserId;
                message.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                message.HasAttachment = false;
                message.SendFrom = userInfo.EMail;
                message.SendTo = sendTo;
                message.CC = userInfo.EMail;
                message.Subject = subject;
                message.Body = content;
                message.Type = MessageType.Email;
                message.BodyFormat = BodyFormat.olFormatPlain;

                _emailService.Send(message);
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog(ex.Message + ex.StackTrace);
            }
        }


        private void SendEmail(string sendTo, string subject, string content, Guid operationID)
        {
            try
            {
                UserInfo userInfo = new UserInfo();
                userInfo = userService.GetUserInfo(ApplicationContext.Current.UserId);
                Message message = new Message();
                message.CreateBy = ApplicationContext.Current.UserId;
                message.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                message.HasAttachment = false;
                message.SendFrom = userInfo.EMail;
                message.SendTo = sendTo;
                message.CC = userInfo.EMail;
                message.Subject = subject;
                message.Body = content;
                message.Type = MessageType.Email;
                message.BodyFormat = BodyFormat.olFormatPlain;

                message.UserProperties = new MessageUserPropertiesObject();
                MessageUserPropertiesObject userProperties = message.UserProperties;
                userProperties.FormId = operationID;
                userProperties.FormType = FormType.Booking;
                userProperties.OperationType = OperationType.OceanExport;
                userProperties.OperationId = operationID;

                _emailService.Send(message);
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog(ex.Message + ex.StackTrace);
            }
        }


        /// <summary>
        /// 改变放单类型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="type">状态(正本 1 ,电放 2)</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <param name="blUpdateDate">提单更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public SingleResult ChangeReleaseBLType(
            Guid id,
            ReleaseType type,
            Guid changeByID,
            DateTime? updateDate,
            DateTime? blUpdateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspChangeReleaseBLType");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@Type", DbType.Int16, type);
                db.AddInParameter(dbCommand, "@updateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@blUpdateDate", DbType.DateTime, blUpdateDate);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate", "BLUpdateDate" });
                return result;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 改变放单类型(放单管理专用)
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="type">状态(正本 1 ,电放 2)</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <param name="blUpdateDate">提单更新时间-做数据版本用</param>
        /// <param name="state">发邮件用：状态</param>
        /// <param name="agentId">发邮件用：代理ID</param>
        /// <param name="blNo">发邮件用：提单号</param>
        /// <param name="name">发邮件用：当前用户名</param>
        /// <returns>返回SingleResult</returns>
        public SingleResult ChangeReleaseTypeToTel(
            Guid id,
            ReleaseType type,
            Guid changeByID,
            DateTime? updateDate,
            DateTime? blUpdateDate,
            ReleaseBLState state,
            string filerEmail,
            Guid? agentId,
            string blNo,
            string name,
            Guid operationID)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspChangeReleaseTypeToTel");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@Type", DbType.Int16, type);
                db.AddInParameter(dbCommand, "@updateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@blUpdateDate", DbType.DateTime, blUpdateDate);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@Mac", DbType.String, ApplicationContext.Current.MacAddress);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate", "BLUpdateDate", "TelNo" });

                #region 邮件通知
                ////如果在放货中状态为已接收时，则邮件通知代理联系人
                //if (rblrcv)
                //{
                //格式：A在2013-1-24将提单<BL123>的放单方式由<AAA>改为<BBB>，请知悉
                ContactList contactlist = GetContactListOfAgent(agentId);
                string sendto = contactlist != null ? contactlist.ContactEmail : filerEmail;
                if (contactlist != null)
                {
                    string newType = type == ReleaseType.Original ? LocalData.IsEnglish ? "Original" : "正本" : LocalData.IsEnglish ? "Telex" : "电放";
                    string oldType = type == ReleaseType.Original ? LocalData.IsEnglish ? "Telex" : "电放" : LocalData.IsEnglish ? "Original" : "正本";

                    string subject = LocalData.IsEnglish ? name + " in the " + DateTime.Now.ToString() + " will bill of lading number of [" + blNo + "] put single way from the [" + oldType + "] to [" + newType + "], please know" :
                        name + "在" + DateTime.Now.ToString() + "将提单[" + blNo + "]的放单方式由[" + oldType + "]改为[" + newType + "]，请知悉";
                    string content = subject;
                    SendEmail(sendto, subject, content, operationID);
                    //  }
                }
                #endregion

                return result;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 申请放单
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="applyDate">applyDate</param>
        /// <param name="changeByID">changeByID</param>
        /// <param name="updateDate">updateDate</param>
        /// <returns>返回SingleResult  "ID", "UpdateDate"</returns>
        public SingleResult ApplyReleaseBL(Guid id
                                        , DateTime? applyDate
                                        , Guid changeByID
                                        , DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(changeByID, "changeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspApplyReleaseBL");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@ApplyedDate", DbType.DateTime, applyDate);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeByID);
                db.AddInParameter(dbCommand, "@updateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate" });
                return result;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 获取代理指定的联系人
        /// </summary>
        /// <param name="AgentID">AgentID</param>  
        public ContactList GetContactListOfAgent(Guid? AgentID)
        {
            // ArgumentHelper.AssertGuidNotEmpty(AgentID, "id"); 

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetAgentContactEmail");

                db.AddInParameter(dbCommand, "@AgentID", DbType.Guid, AgentID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new ContactList();
                }

                ContactList result = (from b in ds.Tables[0].AsEnumerable()
                                      select new ContactList
                                      {
                                          ID = b.Field<Guid>("ID"),
                                          AgentID = b.Field<Guid>("AgentID"),
                                          ContactEmail = b.Field<string>("ContactEmail"),
                                          EmailSendTime = b.Field<int>("EmailSendTime"),
                                      }).SingleOrDefault();

                return result;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 保存联系人邮箱
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="agentID">代理ID</param>
        /// <param name="contactEmail">邮箱</param>
        /// <param name="saveByID"></param>
        public void SaveContactListOfAgent(Guid id
                                               , Guid? agentID
                                               , string contactEmail
                                               , int emailsendtime
                                                , Guid saveByID)
        {
            //ArgumentHelper.AssertGuidNotEmpty(agentID, "agentID");
            //ArgumentHelper.AssertStringNotEmpty(contactEmail, "contactEmail");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("pub.uspAgentContactEmail");

                db.AddInParameter(command, "@Id", DbType.Guid, id);
                db.AddInParameter(command, "@AgentId", DbType.Guid, agentID);
                db.AddInParameter(command, "@ContactEmail", DbType.String, contactEmail);
                db.AddInParameter(command, "@EmailSendTime", DbType.Int32, emailsendtime);
                db.AddInParameter(command, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, IsEnglish);
                db.ManyResult(command);
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
        /// 保存特殊放单
        /// </summary>
        /// <param name="releaseid">放单ID</param>
        /// <param name="saveByID"></param>
        public void SaveExRelease(Guid releaseid, Guid savebyid)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("fam.uspSaveExRelease");

                db.AddInParameter(command, "@ReleaseID", DbType.Guid, releaseid);
                db.AddInParameter(command, "@SaveByID", DbType.Guid, savebyid);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, IsEnglish);
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
        /// 检查特殊放单
        /// </summary>
        /// <param name="releaseid">放单ID</param>
        public List<ExRelease> CheckExRelease(Guid releaseid)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("fam.uspCheckInExReleaseList");

                db.AddInParameter(command, "@ReleaseID", DbType.Guid, releaseid);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, IsEnglish);
                DataSet ds = db.ExecuteDataSet(command);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<ExRelease> results = (from b in ds.Tables[0].AsEnumerable()
                                           select new ExRelease
                                            {
                                                ID = b.Field<Guid>("ID"),
                                                ReleaseID = b.Field<Guid>("ReleaseID"),
                                                CreateBy = b.Field<Guid>("CreateBy"),
                                                CreateDate = b.Field<DateTime>("CreateDate"),
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

        /// </summary>
        /// 保存特殊放单
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="agentID">代理ID</param>
        /// <param name="contactEmail">邮箱</param>
        /// <param name="saveByID"></param>
        public void CheckExRelease(Guid releaseid, Guid savebyid)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("pub.uspSaveExRelease");

                db.AddInParameter(command, "@ReleaseID", DbType.Guid, releaseid);
                db.AddInParameter(command, "@SaveByID", DbType.Guid, savebyid);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, IsEnglish);
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

        /// <summary>
        /// 更改接收正本状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="state">0 未收到 1 收到</param>
        /// <param name="saveByID">执行人</param>
        public void ChangeRecState(Guid id, int state, Guid saveByID, DateTime? UpdateDate)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand command = db.GetStoredProcCommandWithTimeout("fcm.uspChangeOBLRec");

                db.AddInParameter(command, "@Id", DbType.Guid, id);
                db.AddInParameter(command, "@State", DbType.Byte, state);
                db.AddInParameter(command, "@SaveByID", DbType.Guid, saveByID);
                db.AddInParameter(command, "@UpdateDate", DbType.DateTime, UpdateDate);
                db.AddInParameter(command, "@IsEnglish", DbType.Boolean, IsEnglish);
                db.ManyResult(command);
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
        /// 发送催款邮件
        /// </summary>
        /// <param name="waitsend">催款实体类</param>
        /// <param name="iscc">是否抄送</param>
        /// <param name="isAuto">是否自动发送</param>
        public string SendAREmail(ReleaseAndArList waitsend, bool iscc, bool isAuto)
        {
            //自动发送需要检查是否已经发送过当前批次  手工方式发送不检查
            if (isAuto)
            {
                if (CheckArLogInfo(waitsend))
                {
                    return string.Empty;
                }

                ////非深圳公司不自动发送
                //if (waitsend.CompanyID.ToString().ToUpper() != "41D7D3FE-183A-41CD-A725-EB6F728541EC")
                //{
                //    return string.Empty;
                //}
            }



            ConfigureInfo config = _configureService.GetCompanyConfigureInfo(waitsend.CompanyID);
            if (string.IsNullOrEmpty(config.ReleaserEmail))
            {
                return "发送失败，没有找到公司配置的放货负责人邮箱！";
            }

            UserInfoEmail uie = _userService.GetUserInfoByEmailAddress(config.ReleaserEmail);
            string subject = "B/L： " + waitsend.No + " SO: " + waitsend.SONO + " 请安排放货或取单";

            StringBuilder content = new StringBuilder("DEAR" + Environment.NewLine);
            content.Append("你好，贵司委托我司承运的B/L[" + waitsend.No + "]预计将于[" + waitsend.ETA.ToShortDateString() + "]到达[" + waitsend.PODName + "]，烦请贵司尽早安排放货或取单事宜，以避免不必要的费用产生!" + Environment.NewLine);
            content.Append("如有海运费或港前费用未付的请及时安排支付（需要开税务发票的请提供开票资料到invoice@cityocean.com邮箱）" + Environment.NewLine);
            content.Append("可以安排电放的货请提供盖章的电放申请表。" + Environment.NewLine);
            content.Append("如需咨询请回邮件或致电。" + Environment.NewLine);
            content.Append("由于贵司原因导致放货延误产生的额外费用和其他后果，我司概不负责。" + Environment.NewLine + Environment.NewLine);

            List<EmailAccountSignature> siglist = _userService.GetEmailAccountSignature(null, uie.EMail);
            if (siglist != null && siglist.Find(r => r.Type == 0) != null)
            {
                content.Append(siglist.Find(r => r.Type == 0).Signature);
            }
            else
            {
                content.Append("B.rgds," + Environment.NewLine);
                content.Append(uie.EName + Environment.NewLine);
                content.Append(waitsend.CompanyName + Environment.NewLine);
                content.Append("Tel: " + uie.Tel + Environment.NewLine);
                content.Append("Customer Complaint: 400-622-0122 " + Environment.NewLine);
                content.Append("Mobile: " + uie.Mobile + Environment.NewLine);
                content.Append("E-mail: " + uie.EMail + Environment.NewLine);
                content.Append("http://www.cityocean.com");
            }


            Stopwatch stopwatchSendR = StopwatchHelper.StartStopwatch();
            try
            {
                List<ReleaseAndArContact> contactList = _fcmCommonService.GetReleaseAndArContactList(waitsend.OperationID);
                if (contactList == null || contactList.Count < 1)
                {
                    return "发送失败，没有找到对应催款联系人邮箱！";
                }

                List<ReleaseAndArContact> sendTos = contactList.FindAll(r => r.IsAR && r.CustomerID == waitsend.CustomerID);
                if (sendTos == null || sendTos.Count < 1)
                {
                    return "发送失败，没有找到对应催款联系人邮箱！";
                }


                Message message = new Message();
                message.CreateBy = uie.UserID;
                message.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                message.HasAttachment = false;
                message.SendFrom = string.IsNullOrEmpty(config.ReleaserEmail) ? "icpsystem@cityocean.com" : config.ReleaserEmail;
                string sendto = string.Empty;
                sendTos.ForEach(delegate(ReleaseAndArContact r)
                {
                    if (string.IsNullOrEmpty(sendto))
                    {
                        sendto = r.Mail;
                    }
                    else
                    {
                        sendto += ";" + r.Mail;

                    }
                });
                message.SendTo = sendto;

                //message.SendFrom = "icpsystem@cityocean.com";
                //message.SendTo = "moremo@cityocean.com";

                if (iscc)
                {
                    string cc = waitsend.SalesEmail;
                    if (string.IsNullOrEmpty(cc))
                    {
                        cc = waitsend.BookingerEmail;
                    }
                    else
                    {
                        cc += (";" + waitsend.BookingerEmail);
                    }

                    message.CC = cc;
                }

                if (string.IsNullOrEmpty(message.CC))
                {
                    message.CC = config.ReleaserEmail;
                }
                else
                {
                    message.CC += (";" + config.ReleaserEmail);
                }

                message.Subject = subject;
                message.Body = content.ToString();
                message.Type = MessageType.Email;
                message.State = MessageState.Transmitted;
                message.BodyFormat = BodyFormat.olFormatPlain;

                message.UserProperties = new MessageUserPropertiesObject();
                MessageUserPropertiesObject userProperties = message.UserProperties;
                userProperties.FormId = waitsend.OperationID;
                userProperties.FormType = FormType.Booking;
                userProperties.OperationType = OperationType.OceanExport;
                userProperties.OperationId = waitsend.OperationID;
                _emailService.Send(message);

                SendReleaseList.Add(waitsend);

                EventObjects EventObjects = new EventObjects
                {
                    Id = Guid.Empty,
                    Code = "",
                    OccurrenceTime = DateTime.Now,
                    OperationType = OperationType.OceanExport,
                    FormID = waitsend.OperationID,
                    OperationID = waitsend.OperationID,
                    Description = "Send a reminder message at" + DateTime.Now.ToShortDateString(),
                    Subject = "Send a reminder message",
                    ModifyValue = "",
                    Type = MemoType.EmailLog,
                    UpdateBy = new Guid("4047CFAD-ECC8-E111-9D0D-0026551CA87B"),
                    UpdateDate = DateTime.Now,
                };

                _fcmCommonService.SaveMemoInfo(EventObjects);

                //string hostName = Dns.GetHostName();//本机名   
                //string ip = string.Empty;
                //System.Net.IPAddress[] addressList = Dns.GetHostAddresses(hostName);//会返回所有地址，包括IPv4和IPv6   
                //foreach (IPAddress ipa in addressList)
                //{
                //    if (ipa.AddressFamily == AddressFamily.InterNetwork)
                //    {
                //        ip = ipa.ToString();
                //    }
                //}

                //存储服务端日志 内容提单ID(BLID),业务ID(OperationID),发送时距离ETA天数(Days)
                string savelog = "BLID:[" + waitsend.ID + "],OperationID:[" + waitsend.OperationID + "],Days:[" + waitsend.Days.ToString() + "],IsAuto[" + isAuto.ToString() + "]";
                SaveArLogInfo(savelog);
                OperationLogService.Add(new Guid("4047CFAD-ECC8-E111-9D0D-0026551CA87B"), "", "", "", DateTime.Now, "", "SEND-ARMAIL-DB"
                           , stopwatchSendR.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture)
                           , string.Format("发送完成;OceanBookingID[{0}]", waitsend.OperationID.ToString()), IsEnglish);
                return string.Empty;
            }
            catch (Exception ex)
            {
                OperationLogService.Add(new Guid("4047CFAD-ECC8-E111-9D0D-0026551CA87B"), "", "", "", DateTime.Now, "", "SEND-ARMAIL-DB-Faild"
                           , stopwatchSendR.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture)
                           , string.Format("发送失败;OceanBookingID[{0}] 失败原因：" + ex.Message, waitsend.OperationID.ToString()), IsEnglish);
                SaveArErrorLogInfo(ex.Data.ToString() + ":" + ex.Message);
                throw ex;
            }
        }



        /// <summary>
        /// 查询是否发送过当次的催款邮件
        /// </summary>
        /// <param name="waitsend"></param>
        /// <returns></returns>
        public bool CheckArLogInfo(ReleaseAndArList waitsend)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\EmailLog\\ARLogs\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            if (File.Exists(path))
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader sr = new StreamReader(fs, Encoding.Default);
                string str, invoiceNo = string.Empty;
                while ((str = sr.ReadLine()) != null)
                {
                    if (str.Contains(waitsend.OperationID.ToString()) && str.Contains(waitsend.Days.ToString()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }


    }
}
