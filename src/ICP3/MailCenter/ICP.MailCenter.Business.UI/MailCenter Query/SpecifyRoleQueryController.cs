using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;

namespace ICP.MailCenter.Business.UI
{
    /// <summary>
    /// 客户和承运人业务面板查询数据控制器
    /// </summary>
    public class SpecifyRoleQueryController : CommonQueryController
    {

        #region IMailCenterCommonQuery 成员

        public override DataTable GetOperationListByMessageRelations(BusinessQueryCriteria criteria, OperationMessageRelation[] messageRelations)
        {
            criteria.SearchType = SearchActionType.MessageRelation;
            criteria.OperationIDs = (from mr in messageRelations
                                     select mr.OperationID).ToArray();
            return GetOperationViewList(criteria);
        }

        public override DataTable GetOperationListByContact(BusinessQueryCriteria criteria)
        {
            criteria.SearchType = SearchActionType.Contact;
            return GetOperationViewList(criteria);
        }

        public override DataTable GetOperationListByKeyWord(BusinessQueryCriteria criteria)
        {
            criteria.SearchType = SearchActionType.KeyWord;
            return GetOperationViewList(criteria);
        }

        /// <summary>
        /// 本地缓存根据邮件主题匹配单号查找到业务
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public override System.Data.DataTable GetOperationListBySubjectInNO(BusinessQueryCriteria criteria, Message.ServiceInterface.Message messageInfo)
        {
            criteria.SearchType = SearchActionType.SubjectInNO;
            criteria.AdvanceQueryString = AdvanceQueryString;
            DataTable dt = ClientBusinessOperationService.GetLocalOperationViewListBySubjectKeyWord(criteria);//根据邮件主题从本地缓存获取业务信息
            if (dt != null && dt.Rows.Count > 0)
            {
                //如果模糊匹配单号查询结果大于5条，就要过滤掉
                dt = dt.AsEnumerable().Take(5).CopyToDataTable();      
        
                //等待保存上次预览邮件的关联，再执行保存当前选择的邮件关联
                //if (AutoWaitRelation)
                    //manualResetEvent.WaitOne();
                //使用队列
                //AsyncEnumerator ae = new AsyncEnumerator();
                //ae.BeginExecute(InnerMessageRelation(dt, criteria, messageInfo), null);
                //只能将邮件的实体关联， (回执..不能关联)
                if(messageInfo.IsMailItem)
                InnerMessageRelation(dt, criteria, messageInfo);

                #region Invalidate Code

                //{
                //    if (IsAllContactExsit())
                //    {
                //        int count = dt.Rows.Count;
                //        Guid[] operationIDs = new Guid[count];
                //        OperationType[] operationTypes = new OperationType[count];
                //        string[] operationNos = new string[count];
                //        DateTime?[] updateDates = new DateTime?[count];

                //        for (int i = 0; i < count; i++)
                //        {
                //            operationIDs[i] = dt.Rows[i].Field<Guid>(operationIdFieldName);
                //            operationTypes[i] = (OperationType)dt.Rows[i].Field<byte>("OperationType");
                //            operationNos[i] = dt.Rows[i].Field<string>("NO");
                //            updateDates[i] = dt.Rows[i].Field<DateTime?>("UpdateDate");
                //        }

                //        //外部联系人，需要保存联系人与业务的关联，和邮件与业务的关联
                //        commandHandler.SaveOperationMessageRelation(null, MessageRelationType.Auto,
                //                                                    UpdateDataType.AddNew, criteria.EmailAddress,
                //                                                    ContactType.Customer, criteria.EmailAddress,
                //                                                    messageEntity.MessageId, operationIDs,
                //                                                    operationTypes, operationNos,
                //                                                    updateDates);
                //    }
                //}
                //else
                //{
                //    //当前用户属于业务参与者     
                //    MatchRelation(dt.Rows[0], criteria);
                //}

                //如果根据主题只找到一票业务就默认给关联
                //if (dt.Rows.Count == 1)
                //{
                // messageRelation.OperationID = dt.Rows[0].Field<Guid>(operationIdFieldName);
                //messageRelation.OperationType = (OperationType)dt.Rows[0].Field<byte>(operationTypeFiledName);
                //由于除了客户面板，其他面板模板未配置业务类型列，所以业务类型客户端不传，由数据库统一处理
                //messageRelation.OperationType = (OperationType)dt.Rows[0].Field<byte>("OperationType");
                //SaveMessageRelationAndContactMail(messageRelation, dt.Rows[0].Field<DateTime?>("UpdateDate"), true);
                //}

                #endregion
            }
            return dt;
        }

        private void InnerMessageRelation(DataTable dt, BusinessQueryCriteria criteria, Message.ServiceInterface.Message messageInfo)
        {
            //邮件所有外部联系人是否都有保存
            if (IsExternalMail())
            {
                if (IsAllContactExsit())
                {
                    AutoWaitRelation = true;
                    int count = dt.Rows.Count;
                    Guid[] operationIDs = new Guid[count];
                    OperationType[] operationTypes = new OperationType[count];
                    string[] operationNos = new string[count];
                    int i = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        operationIDs[i] = row.Field<Guid>("OceanBookingID");
                        operationTypes[i] = (OperationType)row.Field<byte>("OperationType");
                        operationNos[i] = row.Field<string>("NO");
                        i++;
                    }

                    //如果保存过关联信息，就不需要在保存了
                    commandHandler.SaveOperationMessageRelation(
                        MessageRelationParameter.CreateInstance(null,
                                                                MessageRelationType.Auto,
                                                                UpdateDataType.AddNew,
                                                                MailContactInfo.GetAllExternalContacts(
                                                                    messageInfo),
                                                                null,
                                                                messageInfo.MessageId, operationIDs,
                                                                operationTypes,
                                                                operationNos));
                    //manualResetEvent.Set();
                    AutoWaitRelation = false;
                }
            }

            ////IF 是外部邮件？ AND发件人存在于联系人列表？ THEN
            //// 当前邮件自动关联到匹配的业务列表 (关联的逻辑，请参照[x.x.2.2关联Associate]的定义)
            ////ELSE IF不是外部邮件? THEN 
            //// 如果当前用户已属于业务.参与者，则当前邮件自动关联到匹配的业务列表的第1票业务。
            //// 关联的逻辑，请参照[x.x.2.2关联Associate]的定义。
            //// 如果当前用户不属于业务.参与者，则限制业务列表的快捷菜单功能。
            ////ELSE (发件人未知)
            ////	无动作
            ////END IF
            else
            {
                //当前用户属于业务参与者     
                SaveSingleRelation(dt.Rows[0], messageInfo.MessageId, criteria);
            }
        }

        /// <summary>
        ///本地缓存业务列表中都是关于当前登录用户的业务，除了搜索栏和高级搜索中搜索业务后，
        /// 关联该票业务，那么该票业务不属于当前参与者。
        /// 但根据主题单号查询业务列表条件无关
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="criteria"></param>
        private void SaveSingleRelation(DataRow dataRow, string messageId, BusinessQueryCriteria criteria)
        {
            //当前用户属于业务参与者     
            //只能默认关联一票业务或者第一漂业务
            //在内部联系人的情况下，如果登录用户属于业务的参与者，则不需要保存联系人和业务的关联，只需保存邮件和业务的关联
            AutoWaitRelation = true;
            commandHandler.SaveOperationMessageRelation(
                MessageRelationParameter.CreateInstance(AssociateType.Normal,
                                                        MessageRelationType.Auto,
                                                        UpdateDataType.AddNew,
                                                        GetMailContactInfo(criteria.EmailAddress,
                                                                           MailContactType.olOriginator),
                                                        null,
                                                        messageId,
                                                        new Guid[1] { dataRow.Field<Guid>(operationIdFieldName) },
                                                        new OperationType[1]
                                                            {
                                                                (OperationType) dataRow.Field<byte>(operationTypeFiledName)
                                                            },
                                                        new string[1] { dataRow.Field<string>("NO") }))
;
            //manualResetEvent.Set();
            AutoWaitRelation = false;
        }


        private List<MailContactInfo> GetMailContactInfo(string emailAddress, MailContactType contactType)
        {
            return new List<MailContactInfo>(1) { new MailContactInfo() { ContactType = contactType, EmailAddress = emailAddress } };
        }

        public override DataTable GetOperationViewList(BusinessQueryCriteria criteria)
        {
            criteria.AdvanceQueryString = criteria.SearchType == SearchActionType.KeyWord ? base.AdvanceQueryString : string.Empty;
            DataTable dt;
            //flag=2表示根据keyword来查询，本地查询不到，到服务端去查找
            if (criteria.SearchType == SearchActionType.KeyWord)
            {
                dt = ClientBusinessOperationService.GetLocalOperationViewListBySubjectKeyWord(criteria);
                criteria.AdvanceQueryString = criteria.ServerQueryString;
                criteria.OperationType = OperationType.Unknown;
                if (dt == null || dt.Rows.Count <= 0)
                    dt = QueryService.Get(criteria);
            }
            else
                dt = ClientBusinessOperationService.GetLocalOperationViewList(criteria);

            return dt;
        }

        public override bool IsUnknownBusinessPart()
        {
            return false;
        }


        #endregion
    }
}
