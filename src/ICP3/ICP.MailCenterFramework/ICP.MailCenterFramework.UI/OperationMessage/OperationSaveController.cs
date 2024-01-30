using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Message.ServiceInterface;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Operation.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using Microsoft.Office.Interop.Outlook;
using Exception = System.Exception;

namespace ICP.MailCenterFramework.UI
{
    /// <summary>
    /// 业务数据保存
    /// </summary>
    public class OperationSaveController
    {
        public const string operationIdFieldName = "OceanBookingID";
        public const string operationTypeFiledName = "OperationType";

        /// <summary>
        /// 是否自动等待关联, true: 等待，false:不需要等待
        /// </summary>
        public bool AutoWaitRelation { get; set; }
        /// <summary>
        //邮件所有外部联系人地址
        /// </summary>
        List<string> ExternalEmails { get; set; }

        /// <summary>
        /// 数据库视图CODE的名称
        /// </summary>
        public string TemplateCode { get { return "MailLink4in1"; } } 

        /// <summary>
        /// 缓存数据库服务接口
        /// </summary>
        public IDataCacheOperationService DataCacheOperationService
        {
            get
            {
                IDataCacheOperationService temp = null;
                try
                {
                    temp = ServiceClient.GetService<IDataCacheOperationService>();
                    //temp = null;
                }
                catch (Exception ex)
                {
                    ToolUtility.WriteLog("OperationSaveController DataCacheOperationService", ex);
                    temp = null;
                }
                return temp;
            }
        }

        /// <summary>
        /// [业务与邮件关联]的获取保存类
        /// </summary>
        OperationMessageController OperationMsgCtr
        {
            get { return new OperationMessageController(); }
        }
        /// <summary>
        /// 联系人控制器
        /// </summary>
        OperationContactController OperationCCtr
        {
            get { return  new OperationContactController();}
        }

        /// <summary>
        /// 邮件实体服务
        /// </summary>
        public IMessageService MessageService
        {
            get
            {
                IMessageService temp = null;
                try
                {
                    temp = ServiceClient.GetService<IMessageService>();
                }
                catch (Exception ex)
                {
                    ToolUtility.WriteLog("OperationSaveController IMessageService", ex);
                    temp = null;
                }
                return temp; 
            }
        }

        #region 批量关联
        /// <summary>
        /// 根据邮件查询和自动关联
        /// </summary>
        /// <param name="currentItem">当前邮件或报文对象</param>
        /// <param name="associateResult">关联结果</param>
        public bool AssociateOperationBusiness(object currentItem, ref string associateResult)
        {
            bool resultValue = false;
            DataTable dt = null;
            BusinessQueryCriteria criteria = null;
            Message.ServiceInterface.Message messageEntity = null;
            BusinessQueryResult result = null;
            List<OperationMessageRelation> messageRelations = null;
            try
            {
                criteria = new BusinessQueryCriteria();
                criteria.TemplateCode = TemplateCode;

                #region 1.当前邮件转换成Message对象

                if (currentItem is MailItem) //当前为邮件
                {
                    MailItem currentMail = currentItem as MailItem;
                    if (currentMail.Sent)
                    {
                        messageEntity = OutlookUtility.ConvertMailItemToMessageInfo(currentMail);
                        messageEntity.IsMailItem = true;
                    }
                }
                else if (currentItem is ReportItem) //当前为报文
                {
                    _ReportItem reportItem = currentItem as ReportItem;
                    messageEntity = OutlookUtility.ConvertReportItemToMessageInfo(reportItem);
                    messageEntity.IsMailItem = false;
                }

                #endregion

                if (messageEntity == null)
                {
                    associateResult = "Mail convert message entry failed";
                    return false;
                }

                if (!IsSaveExternalMail(messageEntity))
                {
                    associateResult = "It contains external mail addresses";
                    return false;
                }

                criteria.companyIDs = string.IsNullOrEmpty(GetQueryConditions.SelectedCompanyIds)
                        ? null
                        : GetQueryConditions.SelectedCompanyIds.Split(',').Select(id => new Guid(id)).ToArray();
                string messageReference = string.Empty;
                if (messageEntity.UserProperties != null)
                {
                    messageReference = messageEntity.UserProperties.Reference;
                }
                messageRelations =
                    OperationMsgCtr.GetOperationMessageRelationByMessageIdAndReference(messageEntity.MessageId, messageReference);
                if (messageRelations != null && messageRelations.Count > 0)
                    dt = OperationMsgCtr.GetOperationListByMessageRelations(criteria, messageRelations.ToArray());
                else
                {
                    //关联信息没有找到，再根据主题单号查找
                    criteria.AdvanceQueryString = GetQueryConditions.AppendAdvanceStringToSQL(messageEntity.Subject);
                    if (!string.IsNullOrEmpty(criteria.AdvanceQueryString))
                    {
                        criteria.SearchType = SearchActionType.SubjectInNO;
                        dt = GetOperationListBySubjectInNO(criteria, messageEntity);
                    }
                }

                if (dt == null || dt.Rows.Count <= 0)
                {
                    associateResult = "No business information";
                    return false;
                }

                result = new BusinessQueryResult();
                result.Relations = messageRelations;
                result.Dt = dt;
                PostHandle(result, messageEntity, currentItem);
                resultValue = OutlookUtility.IsRelationOperation(currentItem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                messageEntity = null;
                messageRelations = null;
                criteria = null;
                result = null;
                dt = null;
            }
            return resultValue;
        }
        #endregion

        #region 自动关联

        /// <summary>
        /// 本地缓存根据邮件主题匹配单号查找到业务,并自动关联
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="messageInfo"></param>
        /// <returns></returns>
        public DataTable GetOperationListBySubjectInNO(BusinessQueryCriteria criteria, Message.ServiceInterface.Message messageInfo)
        {
            criteria.SearchType = SearchActionType.SubjectInNO;
            DataTable dt = OperationMsgCtr.GetOperationListBySubjectInNo(criteria, false);//根据邮件主题从本地缓存获取业务信息
            if (dt != null && dt.Rows.Count > 0)
            {
                //如果模糊匹配单号查询结果大于5条，就要过滤掉
                dt = dt.AsEnumerable().Take(5).CopyToDataTable();
                //只能将邮件的实体关联， (回执..不能关联)
                if (messageInfo.IsMailItem && !string.IsNullOrEmpty(messageInfo.MessageId))
                    InnerMessageRelation(dt, criteria, messageInfo);
            }
            return dt;
        }

        /// <summary>
        /// 初始化关联信息：根据主题单号关联邮件
        /// </summary>
        /// <param name="dt">业务数据</param>
        /// <param name="criteria">查询参数</param>
        /// <param name="messageInfo">邮件Message对象</param>
        private void InnerMessageRelation(DataTable dt, BusinessQueryCriteria criteria, Message.ServiceInterface.Message messageInfo)
        {
            //1.为接收邮件且收件人为未保存外部联系人(默认TO、CC、BCC中的未保存外部联系人类型默认与发件人一致保存)
            //2.其他邮件判断所有联系人是否全部保存
            int? type = null;
            if ((messageInfo.Way == MessageWay.Receive
                 && (!FCMInterfaceUtility.ExsitsInternalContact(messageInfo.SendFrom))))
            {
                type = GetContactPersonType(messageInfo.SendFrom);
            }
            //是否存在外部联系人
            if (IsExternalMail(messageInfo))
            {
                if (type != null || IsAllContactExsit())
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
                    SaveOperationMessageRelation(
                        MessageRelationParameter.CreateInstance(null,
                                                                MessageRelationType.Auto,
                                                                UpdateDataType.AddNew,
                                                                MailContactInfo.GetAllExternalContacts(messageInfo),
                                                                null,
                                                                messageInfo.MessageId, operationIDs,
                                                                operationTypes,
                                                                operationNos,
                                                                messageInfo, (type == null ? 1 : type.Value)));
                    AutoWaitRelation = false;
                    ResetMessageRelationOperationList(messageInfo);
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
                SaveSingleRelation(dt.Rows[0], messageInfo, criteria, (type == null ? 1 : type.Value));
                ResetMessageRelationOperationList(messageInfo);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendEmails"></param>
        /// <returns></returns>
        public int? GetContactPersonType(string sendEmails)
        {
            List<OperationContactInfo> listResult = OperationCCtr.GetOperationContactListByMailAddress(sendEmails);
            if (listResult.Count > 0)
            {
                if (listResult[0].Customer && listResult[0].Carrier)
                    return 3;
                if (listResult[0].Customer)
                    return 1;
                if (listResult[0].Carrier)
                    return 2;
            }
            return null;   
        }

        ///  <summary>
        /// 本地缓存业务列表中都是关于当前登录用户的业务，除了搜索栏和高级搜索中搜索业务后，
        ///  关联该票业务，那么该票业务不属于当前参与者。
        ///  但根据主题单号查询业务列表条件无关
        ///  </summary>
        /// <param name="dataRow">网格数据行</param>
        /// <param name="messageInfo">邮件Message对象</param>
        /// <param name="criteria">高级查询</param>
        /// <param name="businessContactType"></param>
        private void SaveSingleRelation(DataRow dataRow, Message.ServiceInterface.Message messageInfo, BusinessQueryCriteria criteria, int businessContactType)
        {
            //当前用户属于业务参与者     
            //只能默认关联一票业务或者第一漂业务
            //在内部联系人的情况下，如果登录用户属于业务的参与者，则不需要保存联系人和业务的关联，只需保存邮件和业务的关联
            AutoWaitRelation = true;
            SaveOperationMessageRelation(
                MessageRelationParameter.CreateInstance(AssociateType.Normal,
                                                        MessageRelationType.Auto,
                                                        UpdateDataType.AddNew,
                                                        GetMailContactInfo(criteria.EmailAddress,
                                                                           MailContactType.olOriginator),
                                                        null,
                                                        messageInfo.MessageId,
                                                        new Guid[1] { dataRow.Field<Guid>(operationIdFieldName) },
                                                        new OperationType[1]
                                                            {
                                                                (OperationType) dataRow.Field<byte>(operationTypeFiledName)
                                                            },
                                                        new string[1] { dataRow.Field<string>("NO") },
                                                        messageInfo, businessContactType));
            AutoWaitRelation = false;
        }


        /// <summary>
        /// 缓存关联列表（绑定dataGrid加粗关联信息用到）
        /// </summary>
        public void ResetMessageRelationOperationList(Message.ServiceInterface.Message messageInfo)
        {
            string messageReference = null;
            List<OperationMessageRelation> MessageRelations;
            DataTable dtR = null;
            if (messageInfo.UserProperties != null)
            {
                messageReference = messageInfo.UserProperties.Reference;
            }
            MessageRelations = OperationMsgCtr.GetOperationMessageRelationByMessageIdAndReference(messageInfo.MessageId, messageReference);//查找关联信息
            //如果找到关联信息，返回业务数据
            if (MessageRelations != null && MessageRelations.Count > 0)
            {
                //设置该邮件存储在数据库中的IMessageID，方便后续关联业务使用
                //Message.Id = MessageRelations[0].IMessageId;
                BusinessQueryCriteria Criteria = new BusinessQueryCriteria();
                Criteria.TemplateCode = "MailLink4in1";
                Criteria.companyIDs = string.IsNullOrEmpty(GetQueryConditions.SelectedCompanyIds) ? null : GetQueryConditions.SelectedCompanyIds.Split(',').Select(id => new Guid(id)).ToArray();
                dtR = OperationMsgCtr.GetOperationListByMessageRelations(Criteria, MessageRelations.ToArray());
                //缓存系统自动默认搜索的关联业务集合
                if (dtR != null)
                {
                    UIHelper.MessageRelationOperationList = dtR.Copy();//高级搜索用到
                }
            }
        }

        /// <summary>
        /// 构建业务联系人
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="contactType"></param>
        /// <returns></returns>
        private List<MailContactInfo> GetMailContactInfo(string emailAddress, MailContactType contactType)
        {
            return new List<MailContactInfo>(1) { new MailContactInfo() { ContactType = contactType, EmailAddress = emailAddress } };
        }
        #endregion

        #region 数据绑定后的处理

        /// <summary>
        /// 数据绑定后的处理
        /// </summary>
        /// <param name="data"></param>
        /// <param name="messageEntry">Message Entry</param>
        /// <param name="mailEntry">MailItem Entry</param>
        public void PostHandle(object data, object messageEntry,object mailEntry)
        {
            BusinessQueryResult result = data as BusinessQueryResult;
            if (result != null && result.Dt != null && result.Dt.Rows.Count > 0)
            {
                 ProcessMessageRelation(result, messageEntry, mailEntry);
            }
        }
        /// <summary>
        /// 处理关联信息
        /// </summary>
        /// <param name="result">Message Entry</param>
        /// <param name="messageEntry">Message Entry</param>
        /// <param name="mailEntry">MailItem Entry</param>
        private void ProcessMessageRelation(BusinessQueryResult result, object messageEntry, object mailEntry)
        {
            if (result.Relations != null && result.Relations.Count > 0)
            {
                Message.ServiceInterface.Message message = messageEntry as Message.ServiceInterface.Message ?? HelpMailStore.msg;
                //父邮件已关联业务，当前这封邮件是回复的邮件，此时要关联到业务
                //如果发现父邮件已关联业务，则不应该去修改父邮件与业务的关联信息，而应该新建一条当前邮件和业务的关联信息，
                string messageReference = message.UserProperties == null ? string.Empty : message.UserProperties.Reference;
                if (IsParentMessage(result.Relations[0].MessageId, message.MessageId, messageReference))
                {
                    var messageRelations = result.Relations;
                    Guid iMessageID = Guid.Empty;
                    //控制保存邮件并发
                    var savedMessage = OperationMsgCtr.GetOperationMessageRelationSingleByMessageID(message.MessageId);
                        //根据当前邮件ID获取邮件关联信息
                    if (!savedMessage.HasData) //如果当前邮件没有关联信息，就保存关联信息
                    {
                        //需要新增一个主键ID，可以控制并发保存
                        iMessageID = message.Id = Guid.NewGuid();
                    }
                    else
                        iMessageID = savedMessage.IMessageId;
                    messageRelations = SaveOperationMessages(savedMessage.HasData, message, messageRelations,
                        message.MessageId, iMessageID);
                    if (messageEntry == null)
                    {
                        HelpMailStore.msg.IsAssociated = true;
                    }
                }
            }
        }

        /// <summary>
        /// 根据邮件关联信息的MessageID和当前邮件MessageID对比，如果不一致，表示是父邮件（Reference）
        /// </summary>
        /// <param name="relationMessageID"></param>
        /// <param name="messageId"></param>
        /// <param name="referenceId"></param>
        /// <returns></returns>
        private bool IsParentMessage(string relationMessageID, string messageId, string referenceId)
        {
            bool isParent = false;
            //关联信息中MessageID和当前邮件ID对比
            if (string.Compare(relationMessageID, messageId, StringComparison.OrdinalIgnoreCase) != 0)
            {
                //关联信息中MessageID和Parent邮件的MessageID对比
                if (string.Compare(relationMessageID, referenceId, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    isParent = true;
                }
            }

            return isParent;
        }

        /// <summary>
        /// 保存邮件与业务的关联信息(发邮件有关联信息，表示父邮件实体和业务联系人已保存)
        /// </summary>
        /// <param name="hasSavedMessage"></param>
        /// <param name="messageInfo"></param>
        /// <param name="messageRelations"></param>
        /// <param name="messageId"></param>
        /// <param name="iMessageid"></param>
        /// <returns></returns>
        private List<OperationMessageRelation> SaveOperationMessages(bool hasSavedMessage, Message.ServiceInterface.Message messageInfo
            , List<OperationMessageRelation> messageRelations, string messageId, Guid iMessageid)
        {
            string xmlMessageInfo = null;
            List<OperationContactParameters> contactParameterses = null;
            List<String> mails = null;
                if (!hasSavedMessage)
                {
                    contactParameterses = new List<OperationContactParameters>();
                    mails = MailContactInfo.GetAllExternalMails(messageInfo);
                    xmlMessageInfo = messageInfo.GetXmlDataNode(false);
                }
                int count = messageRelations.Count; //关联业务的数量
                for (int i = 0; i < count; i++)
                {
                    OperationMessageRelation item = messageRelations[i];

                    if (!hasSavedMessage)
                    {
                        contactParameterses.Add(new OperationContactParameters()
                        {
                            OceanBookingID = item.OperationID,
                            OperationType = item.OperationType,
                            Mails = mails
                        });
                    }

                    item.ID = Guid.NewGuid();
                    item.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    item.Contacts = null;
                    item.XmlMessageInfo = i == 0 ? xmlMessageInfo : null;
                    item.CreateBy = LocalData.UserInfo.LoginID;
                    item.MessageId = messageId;
                    item.EntryID = messageInfo.EntryID;
                    item.IMessageId = iMessageid;
                    item.RelationType = MessageRelationType.Auto;
                    item.UpdateDataType = UpdateDataType.MainForMessageID;
                    item.UploadServer = false;
                    item.BackupMail = true;
                    //首条关联信息记录保存为未备份上传
                    if (i == 0)
                    {
                        item.BackupMail = messageInfo.BackupMailState;
                    }
                    item.UpdateDate = null;
                }
                //本地缓存数据库保存关联信息
                SaveLocalOperationMessageRelation(messageRelations.ToArray());
                //DataCacheOperationService.SaveLocalOperationMessageRelation(messageRelations.ToArray());
                //保存业务联系人
                SaveLocalOperationContactMail(contactParameterses);
                //DataCacheOperationService.SaveLocalOperationContactMail(contactParameterses);
            return messageRelations;
        }
        #endregion

        #region 保存联系人到内存
        /// <summary>
        /// 保存外部联系人到邮件源(内存变量)
        /// </summary>
        /// <param name="_AssociateTypes">业务ID</param>
        /// <param name="messageInfo"></param>
        public void SaveContactToMailStore(AssociateType _AssociateTypes, Message.ServiceInterface.Message messageInfo)
        {
            List<MailContactInfo> mailContactList = MailContactInfo.GetAllExternalContacts(messageInfo);

            foreach (MailContactInfo contactItem in mailContactList)
            {
                if (string.IsNullOrEmpty(contactItem.EmailAddress)) continue;
                OperationContactInfo contactResult =
                    OperationCCtr.GetOperationContactSingleByMailAddress(contactItem.EmailAddress);

                if (contactResult != null)
                {
                    //contactResult.Mail = contactItem.EmailAddress;
                    //已设置为客户或承运人则不更改值
                    contactResult.Customer = contactResult.Customer ? contactResult.Customer : _AssociateTypes == AssociateType.AsCustomer;
                    contactResult.Carrier = contactResult.Carrier ? contactResult.Carrier : _AssociateTypes == AssociateType.AsCarrier;
                }
                else
                {
                    OperationContactInfo operationContact = new OperationContactInfo
                    {
                        Customer = _AssociateTypes == AssociateType.AsCustomer,
                        Carrier = _AssociateTypes == AssociateType.AsCarrier,
                        UpdateDate = DateTime.Now,
                        Mail = contactItem.EmailAddress
                    };
                    HelpMailStore.TableOperationContact.Add(operationContact);
                }
            }
        }
        
        #endregion

        #region 所有联系人已保存情况下：点击关联
        /// <summary>
        /// 保存关联信息（手动关联时）
        /// </summary>
        /// <param name="messageRelationParameter">关联参数</param>
        /// <param name="bussOperationContexts"></param>
        public void InnerSaveOperationMessageRelation(MessageRelationParameter messageRelationParameter
            , List<BusinessOperationContext> bussOperationContexts)
        {
            List<Guid> OperationIDs = new List<Guid>();
            List<string> OperationNOs = new List<string>();
            List<OperationType> OperationTypes = new List<OperationType>();
            foreach (var item in bussOperationContexts)
            {
                OperationIDs.Add(item.OperationID);
                OperationNOs.Add(item.OperationNO);
                OperationTypes.Add(item.OperationType);
            }
            messageRelationParameter.OperationIDs = OperationIDs.ToArray();
            messageRelationParameter.OperationTypes = OperationTypes.ToArray();
            messageRelationParameter.OperationNOs = OperationNOs.ToArray();
            SaveOperationMessageRelation(messageRelationParameter);
        }
        
        /// <summary>
        /// 保存业务信息
        /// </summary>
        /// <param name="iMessageId"></param>
        /// <param name="messageRelationParameter"></param>
        private void SaveMessageRelation(MessageRelationParameter messageRelationParameter)
        {
            List<OperationContactParameters> contactParameters = null;
            // 设置当前关联信息集合
            List<OperationMessageRelation> messageRelations = null;
            //关联和同步业务数据到本地缓存
            ManyResult manyResult = null;
            try
            {
                messageRelations = BuildOperationMessageRelations(messageRelationParameter, ref contactParameters);
                if (messageRelations != null && messageRelations.Count > 0)
                {
                    if (messageRelationParameter.RelationType == MessageRelationType.Hand)
                    {
                        messageRelations.ForEach(item =>
                        {
                            item.Contacts = null;
                            item.XmlMessageInfo = null;
                        });
                        //关联和同步业务数据到本地缓存
                        SaveLocalOperationMessageRelation(messageRelations.ToArray());
                        manyResult =
                            DataCacheOperationService.SaveOperationMessageRelation(messageRelations.ToArray());
                        if (manyResult != null && manyResult.Items.Count > 0)
                        {
                            int count = manyResult.Items.Count;
                            for (int r = 0; r < count; r++)
                            {
                                messageRelations[r].ID = manyResult.Items[r].GetValue<Guid>("ID");
                                messageRelations[r].UpdateDate = manyResult.Items[r].GetValue<DateTime?>("UpdateDate");
                            }
                        }
                        
                    }
                    else
                    {
                        //保存本地业务关联信息
                        SaveLocalOperationMessageRelation(messageRelations.ToArray());
                        //DataCacheOperationService.SaveLocalOperationMessageRelation(messageRelations.ToArray());
                    }
                    //保存本地业务联系人
                    SaveLocalOperationContactMail(contactParameters);
                    //DataCacheOperationService.SaveLocalOperationContactMail(contactParameters);
                }
            }
            finally
            {
                messageRelations = null;
                manyResult = null;
            }
        }

        /// <summary>
        /// 更新内存关联信息并保存到本地文件
        /// </summary>
        /// <param name="relations"></param>
        /// <returns></returns>
        public void SaveLocalOperationMessageRelation(OperationMessageRelation[] relations)
        {
            if (relations == null || relations.Length <= 0)
                return;
            try
            {
                switch (relations[0].UpdateDataType)
                {
                    case UpdateDataType.AddNew:
                        if (!relations[0].UploadServer)
                        {
                            //if (relations.Length > 5)
                            //{
                            //    //数据库存在邮件且关联MessageID一致则不需重复保存
                            //    Message.ServiceInterface.Message messageInfo =
                            //        ServiceClient.GetService<IMessageService>().Get(relations[0].IMessageId);
                            //    if (messageInfo != null && !messageInfo.MessageId.Equals(relations[0].MessageId))
                            //        return;
                            //}
                        }
                        break;
                    case UpdateDataType.MainForMessageID:
                        foreach (var itemRelation in relations)
                        {
                            //itemRelation.ExecuteType = 3;
                            HelpMailStore.TableMessageRelation.RemoveAll(item => itemRelation.MessageId.Equals(item.MessageId));
                        }
                        break;
                    case UpdateDataType.MainForOperationID:
                        foreach (var itemRelation in relations)
                        {
                            //itemRelation.ExecuteType = 3;
                            HelpMailStore.TableMessageRelation.RemoveAll(item => itemRelation.MessageId.Equals(item.MessageId));
                        }
                        break;
                }

                //过滤重复的关联信息
                List<String> tempMessageIDs = new List<string>();
                string arrText = string.Empty;
                foreach (var item in relations)
                {
                    arrText = string.Format("{0}{1}", item.OperationID, item.MessageId).ToLower();
                    if (!tempMessageIDs.Contains(arrText))
                    {
                        tempMessageIDs.Add(arrText);
                        SaveOperationMessageRelation2File(item);
                    }
                    else
                    {
                        //item.ExecuteType = 3;
                        HelpMailStore.TableMessageRelation.RemoveAll(itemID => item.ID.Equals(itemID.ID));
                    }
                }
                tempMessageIDs = null;
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("OperationSaveController SaveLocalOperationMessageRelation", ex);
            }
        }
        /// <summary>
        /// 保存关联业务信息到本地文件
        /// </summary>
        /// <param name="omr"></param>
        public void SaveOperationMessageRelation2File(OperationMessageRelation omr)
        {
            List<OperationMessageRelation> tempList =
                HelpMailStore.TableMessageRelation.Where(item => omr.ID.Equals(item.ID)).ToList();
            if (tempList.Count > 0)
            {
                //omr.ExecuteType = 2;
                tempList[0] = omr;
            }
            else
            {
                //omr.ExecuteType = 1;
                HelpMailStore.TableMessageRelation.Add(omr);
            }
            string filePath = ICPPathUtility.CombineDirectory4FileName(ICPPathUtility.TempPathMessageRelation(), omr.ID + ".bak");
            if (File.Exists(filePath))
                File.Delete(filePath);
            SerializationUtility.Serialize2File(filePath, omr);
        }

        public void SaveLocalOperationContactMail(List<OperationContactParameters> ContactParameters)
        {
            try
            {
                if (ContactParameters == null || ContactParameters.Count <= 0)
                    return;

                foreach (var item in ContactParameters)
                {
                    List<DataRow> tempRow = HelpMailStore.TableBusiness.AsEnumerable()
                        .Where(itemBusiness => item.OceanBookingID.Equals(itemBusiness.Field<Guid>("ID"))).ToList();
                    if ((tempRow.Count > 0))
                    {
                        string contactMail = tempRow[0].Field<string>("ContactMail");
                        if (string.IsNullOrEmpty(contactMail))
                        {
                            contactMail = string.Join(";", item.Mails.ToArray());
                        }
                        else
                        {
                            foreach (string mail in item.Mails)
                            {
                                if (!string.IsNullOrEmpty(mail))
                                {
                                    if (!contactMail.Contains(mail))
                                    {
                                        contactMail = string.Format("{0};{1}", contactMail, mail);
                                    }
                                }
                            }
                        }
                        tempRow[0]["ContactMail"] = contactMail;
                        HelpMailStore.TableBusiness.AcceptChanges();
                    }


                    string filePath = ICPPathUtility.CombineDirectory4FileName(ICPPathUtility.TempPathContactMail(), item.OceanBookingID + ".bak");
                    if (File.Exists(filePath))
                        File.Delete(filePath);
                    SerializationUtility.Serialize2File(filePath, item);
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("OperationSaveController SaveLocalOperationContactMail", ex);
            }
        }

        

        /// <summary>
        /// 保存邮件
        /// </summary>
        /// <param name="messageRelationParameter"></param>
        /// <param name="imessageID"></param>
        private void SaveMailInfo(MessageRelationParameter messageRelationParameter)
        {
            //手动关联,需要确保邮件是否保存到服务端数据库
            if (messageRelationParameter.RelationType != MessageRelationType.Auto)
            {
                Message.ServiceInterface.Message messageInfo = ServiceClient.GetService<IMessageService>().GetMessageByMessageId(messageRelationParameter.MessageID);
                if (messageInfo == null)
                {
                    //克隆实体，移除附件
                    Message.ServiceInterface.Message message = messageRelationParameter.MessageInfo;
                    message.Id = Guid.NewGuid();
                    messageRelationParameter.MessageInfo.Id = MessageService.Save(message)[0].Items[0].GetValue<Guid>("ID");
                }else
                    messageRelationParameter.MessageInfo.Id = messageInfo.Id;
            }
        }
        /// <summary>
        /// 设置当前关联信息集合
        /// </summary>
        /// <returns></returns>
        private List<OperationMessageRelation> BuildOperationMessageRelations(MessageRelationParameter messageRelationParameter
            , ref List<OperationContactParameters> contactParameterses)
        {
            List<OperationMessageRelation> newMessageRelationslList = null;
            bool directReturn = true;
            List<OperationMessageRelation> currentRelations = new List<OperationMessageRelation>();
            currentRelations = GetCurrentMailRelations(messageRelationParameter, ref directReturn);
            messageRelationParameter.MessageInfo.IsAssociated = directReturn;
            if (!directReturn)
                return currentRelations;
            //手动关联时，删除需要移除关联信息
            RemoveUnnecessaryOperationMessages(messageRelationParameter.OperationIDs, currentRelations);
            //自动关联时，获取邮件所有外部邮件的业务联系人
            List<string> mails = null;
            List<CustomerCarrierObjects> contacts = GetContacts(messageRelationParameter, ref mails);
            List<OperationMessageRelation> newMessageRelations = new List<OperationMessageRelation>();
            int length = messageRelationParameter.OperationIDs.Length;
            contactParameterses = new List<OperationContactParameters>(length);
            for (int i = 0; i < length; i++)
            {
                OperationMessageRelation relationInfo = currentRelations.Find(item => item.OperationID == messageRelationParameter.OperationIDs[i]);
                if (relationInfo == null)
                {
                    OperationMessageRelation newMessageRelation = FCMInterfaceUtility.CreateOperationMessageRelationInfo(Guid.NewGuid(), null
                        , messageRelationParameter.OperationIDs[i], messageRelationParameter.OperationTypes[i], messageRelationParameter.MessageID
                        , messageRelationParameter.MessageInfo.Id, ContactStage.Unknown, messageRelationParameter.MessageInfo.EntryID);
                    newMessageRelation.EntryID = messageRelationParameter.MessageInfo.EntryID;
                    newMessageRelation.BackupMail = true;
                    newMessageRelation.UpdateDataType = messageRelationParameter.UpdateDataType;
                    newMessageRelation.RelationType = messageRelationParameter.RelationType;
                    newMessageRelation.Contacts = ConvertOceanContactsToXml(messageRelationParameter.RelationType, contacts, messageRelationParameter.OperationIDs[i], messageRelationParameter.OperationTypes[i]); ;
                    //邮件只需要保存一次即可
                    if (i == 0)
                    {
                        newMessageRelation.XmlMessageInfo = ConvertMessageInfoToXml(messageRelationParameter);
                        //是否备份邮件
                        newMessageRelation.BackupMail = messageRelationParameter.MessageInfo.BackupMailState;
                    }
                    else
                        newMessageRelation.XmlMessageInfo = null;
                    newMessageRelation.UploadServer = false;
                    newMessageRelation.OperationNo = messageRelationParameter.OperationNOs[i];
                    newMessageRelations.Add(newMessageRelation);
                }
                else
                {
                    newMessageRelations.Add(relationInfo);
                }

                if (messageRelationParameter.RelationType == MessageRelationType.Auto)
                {
                    if (mails != null && mails.Count > 0)
                    {
                        contactParameterses.Add(new OperationContactParameters()
                        {
                            OceanBookingID = messageRelationParameter.OperationIDs[i],
                            OperationType = messageRelationParameter.OperationTypes[i],
                            Mails = mails
                        });
                    }
                }
            }
            newMessageRelationslList = newMessageRelations;
            return newMessageRelationslList;
        }

        /// <summary>
        /// 获取当前邮件的关联信息
        /// </summary>
        /// <param name="messageRelationParameter"></param>
        /// <param name="directReturn"></param>
        /// <returns></returns>
        private List<OperationMessageRelation> GetCurrentMailRelations(MessageRelationParameter messageRelationParameter, ref bool directReturn)
        {
            //手动关联时可连接服务器查询关联数据
            List<OperationMessageRelation> cuOperationMessageRelations = HelpMailStore.CurrentMessageRelation;
            if (cuOperationMessageRelations.Count > 0)
            {
                //MesasgeID相同：为当前邮件关联信息
                if (
                    !cuOperationMessageRelations.Any(
                        item => item.MessageId.ToLower().Equals(messageRelationParameter.MessageID.ToLower())))
                {
                    directReturn = false;
                    return cuOperationMessageRelations;
                }
                cuOperationMessageRelations.ForEach(delegate(OperationMessageRelation item)
                {
                    item.IMessageId = messageRelationParameter.MessageInfo.Id;
                    item.RelationType = messageRelationParameter.RelationType;
                    item.UpdateDataType = messageRelationParameter.UpdateDataType;
                    item.ContactStage = ContactStage.Unknown; //
                    item.UpdateBy = LocalData.UserInfo.LoginID;
                    item.BackupMail = false;
                    item.UpdateDate = null;
                });
            }
            else
                cuOperationMessageRelations = new List<OperationMessageRelation>();
            return cuOperationMessageRelations;
        }
        /// <summary>
        /// 移除多余的服务端数据库关联信息
        /// </summary>
        private void RemoveUnnecessaryOperationMessages(Guid[] operationIDs, List<OperationMessageRelation> operationMessageRelations)
        {
            List<Guid> messageRelationIds = new List<Guid>();
            List<DateTime?> updateDates = new List<DateTime?>();
            try
            {
                int totalCount = operationMessageRelations.Count;
                for (int i = 0; i < totalCount; i++)
                {
                    if (!operationIDs.Contains(operationMessageRelations[i].OperationID))
                    {
                        messageRelationIds.Add(operationMessageRelations[i].ID);
                        updateDates.Add(operationMessageRelations[i].UpdateDate);
                    }
                }
                if (messageRelationIds.Count > 0)
                    DataCacheOperationService.RemoveAndSyncOperationMessageRelations(messageRelationIds.ToArray(), updateDates.ToArray());
            }
            finally
            {
                messageRelationIds = null;
                updateDates = null;
            }
        }
        /// <summary>
        /// 获取邮件所有外部业务联系人
        /// </summary>
        /// <param name="messageRelationParameter"></param>
        /// <returns></returns>
        private List<CustomerCarrierObjects> GetContacts(MessageRelationParameter messageRelationParameter, ref List<string> mails)
        {
            List<CustomerCarrierObjects> customerCarrier = null;
            List<CustomerCarrierObjects> contacts = null;
            if (messageRelationParameter.RelationType == MessageRelationType.Auto)
            {
                contacts = FCMInterfaceUtility.ConvertCustomerCarrierObjectsFromContacts(messageRelationParameter.MailContactInfos, messageRelationParameter.BusinessContactType);

                if (contacts != null && contacts.Count > 0)
                {
                    mails = (from c in contacts select c.Mail).ToList();
                }
            }
            customerCarrier = contacts;
            return customerCarrier;
        }
        /// <summary>
        /// 将业务联系人实体转换成Xml
        /// </summary>
        /// <param name="relationType"></param>
        /// <param name="contacts"></param>
        /// <param name="operationID"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        private string ConvertOceanContactsToXml(MessageRelationType relationType, List<CustomerCarrierObjects> contacts, Guid operationID, OperationType operationType)
        {

            if (relationType == MessageRelationType.Auto && contacts != null)
            {
                contacts.ForEach(delegate(CustomerCarrierObjects item)
                {
                    item.OceanBookingID = operationID;
                    item.OperationType = operationType;
                });
                return FCMInterfaceUtility.ConvertContactsToXml(contacts);
            }
            else
                return null;
        }

        /// <summary>
        /// 将邮件实体转换成Xml形式
        /// </summary>
        /// <param name="relationType"></param>
        /// <param name="messageRelationParameter"></param>
        /// <returns></returns>
        private string ConvertMessageInfoToXml(MessageRelationParameter messageRelationParameter)
        {
            if (messageRelationParameter.RelationType == MessageRelationType.Auto)
            {
                Message.ServiceInterface.Message messageInfo = messageRelationParameter.MessageInfo;
                return messageInfo.GetXmlDataNode(false);
            }
            else
                return null;
        }
        #endregion

        #region Common
        /// <summary>
        /// 是否已保存所有外部联系人
        /// </summary>
        /// <param name="messageInfo"></param>
        /// <returns></returns>
        public bool IsSaveExternalMail(Message.ServiceInterface.Message messageInfo)
        {
            return !IsExternalMail(messageInfo) || IsAllContactExsit();
        }

        /// <summary>
        /// 是否是外部邮件
        /// </summary>
        /// <returns></returns>
        public bool IsExternalMail(Message.ServiceInterface.Message messageInfo)
        {
            ExternalEmails = MailContactInfo.GetAllExternalMails(messageInfo);//获取所有外部联系人地址
            return ExternalEmails != null && ExternalEmails.Count > 0;
        }

        /// <summary>
        /// 所有外部联系人都有被存储到数据库
        /// </summary>
        /// <returns></returns>
        public bool IsAllContactExsit()
        {
            foreach (OperationContactInfo ocItem in HelpMailStore.TableOperationContact)
            {
                if (string.IsNullOrEmpty(ocItem.Mail))
                {
                    
                }
            }
            return Convert.ToBoolean(ExternalEmails.Count(mailItem => Convert.ToBoolean(HelpMailStore.TableOperationContact.Count(item => mailItem.ToUpper().Equals((string.IsNullOrEmpty(item.Mail) ? "" : item.Mail).ToUpper())))));
        }

        /// <summary>
        /// 保存关联信息（手动，自动）
        /// </summary>
        public void SaveOperationMessageRelation(MessageRelationParameter messageRelationParameter)
        {
            if (messageRelationParameter.OperationIDs == null || messageRelationParameter.OperationIDs.Length <= 0)
                return;
            SaveMailInfo(messageRelationParameter);
            SaveMessageRelation(messageRelationParameter);
        }
        #endregion

        #region 另存邮件
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strEntryID"></param>
        /// <param name="backupMailState"></param>
        /// <param name="item"></param>
        public void SaveAsMail(string strEntryID,bool backupMailState, object item)
        {
            if (backupMailState)
                return;
            if (!LocalData.NeedBackUpMail)
                return;
            //总时间
            if (string.IsNullOrEmpty(strEntryID))
                return;
            if (item == null)
                return;
            //StringBuilder strException = new StringBuilder();
            try
            {
                System.Diagnostics.Stopwatch stopwatchFindmail = new System.Diagnostics.Stopwatch();
                stopwatchFindmail.Start();
                string saveAsPath = ICPPathUtility.CombineDirectory4FileName(ICPPathUtility.TempPathMailEntity(), strEntryID + ".msg");
                OutlookUtility.SaveMailItemAs(item, saveAsPath);
                stopwatchFindmail.Stop();
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("OperationSaveController SaveAsMail", ex);
                //strException.AppendFormat("Message:{0}\r\n", ex.Message);
                //strException.AppendFormat("StackTrace:{0}\r\n", ex.StackTrace);
            }
        }
        #endregion

        #region 获取联系人阶段
        /// <summary>
        /// 获取联系人阶段
        /// </summary>
        /// <param name="associateType"></param>
        /// <returns></returns>
        private ContactStage GetContactStage(AssociateType? associateType)
        {
            if (!associateType.HasValue)
                return ContactStage.Unknown;
            if (associateType == AssociateType.WithStageSI)
                return ContactStage.SI;
            else if (associateType == AssociateType.WithStageSO)
                return ContactStage.SO;
            else
                return ContactStage.Unknown;
        } 
        #endregion
    }
}