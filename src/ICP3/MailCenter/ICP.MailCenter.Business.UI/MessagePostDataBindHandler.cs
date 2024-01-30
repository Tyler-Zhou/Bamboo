using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using ICP.Common.CommandHandler.ServiceInterface;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using ICP.Operation.Common.UI;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using ICP.FCM.Common.ServiceInterface;
namespace ICP.MailCenter.Business.UI
{

    /// <summary>
    /// 邮件中心业务面板数据绑定后处理器
    /// <remarks>处理邮件关联</remarks>
    /// </summary>
    public class MessagePostDataBindHandler : IPostDataBindHandler
    {

        private object objLock = new object();
        public WorkItem RootWorkItem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }
        }

        [ServiceDependency]
        public IClientBusinessOperationService ClientBusinessOperationService { get; set; }

        /// <summary>
        /// 消息标题单号匹配的列表列获取器
        /// </summary>
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ConditionColumnGetter ConditionColumnGetter
        {
            get;
            set;
        }
        private ListBaseBusinessPart currentBasePart;
        private string isAssociatedColumnName = "IsAssociated";
        private string businessStyleName = "BusinessStyle";
        string operatonNoColumnName = ICP.Operation.Common.ServiceInterface.Constants.OperationNOFieldName;
        string operationIDColumnName = ICP.Operation.Common.ServiceInterface.Constants.OperationIDFieldName;

        #region IPostDataBindHandler 成员
        /// <summary>
        /// 数据绑定后的处理
        /// </summary>
        /// <param name="businessPart"></param>
        /// <param name="data"></param>
        /// <param name="parameter"></param>
        public void PostHandle(IBaseBusinessPart_New businessPart, object data, object parameter)
        {
            currentBasePart = businessPart as ListBaseBusinessPart;
            if (currentBasePart == null)
                return;
            //邮件中心未知业务面板不能自动关联业务，也不需要高亮度显示匹配行
            //if (currentBasePart.TemplateCode.Equals(ListFormType.MailLink4Unknown.ToString()))
            //{
            //    this.currentBasePart.ClearStyle(businessStyleName);
            //    return;
            //}
            BusinessQueryResult result = data as BusinessQueryResult;
            if (result != null && result.Dt != null && result.Dt.Rows.Count > 0)
            {
                ICP.Message.ServiceInterface.Message message = parameter as ICP.Message.ServiceInterface.Message;
                ProcessMessageRelation(result, message);
            }
            //回调刷新的情况下，只获取了业务信息，此时需要主动去获取
            else
            {
                DataTable dt = data as DataTable;
                if (dt == null)
                    return;


            }

        }


        #endregion
        private void ProcessMessageRelation(BusinessQueryResult result, ICP.Message.ServiceInterface.Message message)
        {
            if (result.Relations != null && result.Relations.Count > 0)
            {
                //父邮件已关联业务，当前这封邮件是回复的邮件，此时要关联到业务
                //如果发现父邮件已关联业务，则不应该去修改父邮件与业务的关联信息，而应该新建一条当前邮件和业务的关联信息，
                string messageReference = message.UserProperties == null ? string.Empty : message.UserProperties.Reference;
                if (IsParentMessage(result.Relations[0].MessageId, message.MessageId, messageReference))
                {
                    WaitCallback objCallBack = (data) =>
                        {
                            lock (objLock)
                            {
                                var messageRelations = data as List<OperationMessageRelation>; ICP.Message.ServiceInterface.Message currentMessage = null;
                                Guid iMessageID = Guid.Empty;
                                try
                                {
                                    //控制保存邮件并发
                                    var savedMessage = ClientBusinessOperationService.GetOperationMessageRelationByMessageID(message.MessageId);//根据邮件ID获取邮件关联信息
                                    if (!savedMessage.HasData)
                                    {
                                        currentMessage = MailHelper.GetMessageInfo();
                                        //需要新增一个主键ID，可以控制并发保存
                                        iMessageID = currentMessage.Id = Guid.NewGuid();
                                    }
                                    else
                                        iMessageID = savedMessage.IMessageId;

                                    messageRelations = SaveOperationMessages(savedMessage.HasData, currentMessage, messageRelations, message.MessageId, iMessageID);
                                }
                                catch (Exception ex)
                                {
                                    ICP.Framework.CommonLibrary.Logger.Log.Error(
                                        ICP.Framework.CommonLibrary.Common.CommonHelper.BuildExceptionString(ex));
                                    LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex.Message);
                                }

                                UIHelper.CurrentMessageRelation = messageRelations;//全局缓存当前邮件的关联信息
                                //如果当前邮件已关联业务，则更改关联业务行的显示样式，将业务行放到第一行位置
                                ChangeBusinessStyle(messageRelations);

                                //更改邮件类别图标为绿色
                                RootWorkItem.Commands["Command_ChangeEmailCategories"].Execute();
                            }
                        };
                    ThreadPool.QueueUserWorkItem(objCallBack, result.Relations);
                }
                else
                    //如果当前邮件已关联业务，则更改关联业务行的显示样式，将业务行放到第一行位置
                    ChangeBusinessStyle(result.Relations);

                SaveStopwatchTime();
                return;
            }
            else
            {
                SaveStopwatchTime();
                //取得系统新增的默认关联信息
                var messageRelations = UIHelper.CurrentMessageRelation;
                if (messageRelations != null && messageRelations.Count > 0)
                {
                    ChangeBusinessStyle(messageRelations);
                    return;
                }
            }

            SaveStopwatchTime();
            //数据源为空
            DataTable dt = this.currentBasePart.CurrentDataSource;
            if (dt == null || dt.Rows.Count <= 0)
                return;
            //没有识别出主题单号
            List<string> nos = UIHelper.MatchArray(message.Subject);
            if (nos == null || nos.Count <= 0 || string.IsNullOrEmpty(message.MessageId))
            {
                this.currentBasePart.ClearStyle(businessStyleName);
                return;
            }
            //拼接数据源过滤表达式
            List<string> conditionColumnNames = ConditionColumnGetter.Get(this.currentBasePart.TemplateCode);
            string filterExpression = DataCacheUtility.GetExpression(nos, conditionColumnNames, true);
            if (string.IsNullOrEmpty(filterExpression))
            {
                this.currentBasePart.ClearStyle(businessStyleName);
                return;
            }
            //如果数据源中有主题识别单号
            DataRow[] rows = dt.Select(filterExpression);
            if (rows == null || rows.Length <= 0)
            {
                this.currentBasePart.ClearStyle(businessStyleName);
                return;
            }
            //邮件所有的联系人都是内部联系人，需要清除高亮度显示            
            if (FCMInterfaceUtility.ExsitsInternalContact(message.SendFrom))
            {
                this.currentBasePart.ClearStyle(businessStyleName);
                return;
            }

            //string operationNo = dt.Columns.Contains(operatonNoColumnName) ? rows[0].Field<string>(operatonNoColumnName) : string.Empty;
            //Guid operationId = rows[0].Field<Guid>(operationIDColumnName);

            //message.UserProperties = CreateMessageUserPropertiesObjectInfo(operationId,
            //                                                               this.currentBasePart.OperationType, operationNo,
            //                                                                  Guid.Empty,
            //                                                                  FormType.Unknown,
            //                                                                  Guid.Empty,
            //                                                                  null);

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
        /// 保存计时器日志
        /// </summary>
        private void SaveStopwatchTime()
        {
            //var entrySearchStopwatch =
            //    RootWorkItem.State["MailCenterEntrySearchStopwatch"] as System.Diagnostics.Stopwatch;
            //if (entrySearchStopwatch != null)
            //{
            //    StopwatchHelper.EndStopwatch(entrySearchStopwatch, DateTime.Now,"SEARCH", "ICPMailCenter.exe",
            //                                 (LocalData.IsEnglish ? "Keyword entry search" : "关键字搜索"));
            //    RootWorkItem.State["MailCenterEntrySearchStopwatch"] = null;
            //}
        }

        /// <summary>
        /// 保存邮件与业务的关联信息(发邮件有关联信息，表示父邮件实体和业务联系人已保存)
        /// </summary>
        /// <param name="hasSavedMessage"></param>
        /// <param name="messageRelations"></param>
        /// <param name="messageId"></param>
        /// <param name="iMessageid"></param>
        /// <returns></returns>
        private List<OperationMessageRelation> SaveOperationMessages(bool hasSavedMessage, Message.ServiceInterface.Message messageInfo, List<OperationMessageRelation> messageRelations, string messageId, Guid iMessageid)
        {
            string xmlMessageInfo = null;
            //List<CustomerCarrierObjects> mailContacts = null;
            List<OperationContactParameters> contactParameterses = null;
            List<String> mails = null;
            if (!hasSavedMessage)
            {
                contactParameterses = new List<OperationContactParameters>();
                var mailContactInfos = MailContactInfo.GetAllExternalContacts(messageInfo);
                mails = (from m in mailContactInfos select m.EmailAddress).ToList();

                //mailContacts = ICP.FCM.Common.ServiceInterface.Utility.ConvertContacts(mailContactInfos);
                xmlMessageInfo = MailHelper.GetMessageInfo(messageInfo).GetXmlDataNode(false);
            }

            int count = messageRelations.Count;//关联业务的数量
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
                item.CreateDate = DateTime.SpecifyKind(DateTime.Now,
                                                       DateTimeKind.Unspecified);
                item.Contacts = null;//ConvertContactsToXmlString(hasSavedMessage, mailContacts, item.OperationID, item.OperationType);
                if (i == 0)
                    item.XmlMessageInfo = xmlMessageInfo;
                else
                    item.XmlMessageInfo = null;

                item.CreateBy = LocalData.UserInfo.LoginID;
                item.MessageId = messageId;
                item.IMessageId = iMessageid;
                item.RelationType = MessageRelationType.Auto;
                item.UpdateDataType = UpdateDataType.MainForMessageID;
                item.UploadServer = false;
                item.UpdateDate = null;
            }
            //本地缓存数据库保存关联信息
            ClientBusinessOperationService.SaveLocalOperationMessageRelation(
                messageRelations.ToArray());
            //保存业务联系人
            ClientBusinessOperationService.SaveLocalOperationContactMail(contactParameterses);
            return messageRelations;
        }

        /// <summary>
        /// 将邮件所有的业务联系人转换成Xml
        /// </summary>
        /// <param name="hasSavedMessageRelation"></param>
        /// <param name="mailContacts"></param>
        /// <param name="operationID"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        private string ConvertContactsToXmlString(bool hasSavedMessageRelation, List<CustomerCarrierObjects> mailContacts, Guid operationID, OperationType operationType)
        {
            string xmlContacts = null;
            if (!hasSavedMessageRelation && mailContacts != null)
            {
                mailContacts.ForEach(c =>
                    {
                        c.OceanBookingID = operationID;
                        c.OperationType = operationType;
                    });
                xmlContacts = FCMInterfaceUtility.ConvertContactsToXml(mailContacts);
            }

            return xmlContacts;
        }


        private MessageUserPropertiesObject CreateMessageUserPropertiesObjectInfo(Guid operationId,
            ICP.Framework.CommonLibrary.Common.OperationType operationType,
            string operationNo,
            Guid? formID,
            FormType? formType,
            Guid messageRelationID,
            DateTime? updateDate
            )
        {
            MessageUserPropertiesObject info = new MessageUserPropertiesObject();
            info.OperationId = operationId;
            info.OperationType = operationType;
            info.OperationNO = operationNo;
            info.OperationRelationID = messageRelationID;

            info["UpdateDate"] = updateDate;
            return info;
        }

        private delegate void ChangeBusinessStyleDelegate(List<OperationMessageRelation> relations);
        private void InnerChangeBusinessStyle(List<OperationMessageRelation> relations)
        {
            //为了防止死循环
            if (relations.Count > 20)
            {
                relations = relations.Take(20).ToList();
            }
            StringBuilder strBuf = new StringBuilder();
            relations.ForEach(item => strBuf.Append(string.Format(" {0}='{1}' OR", operatonNoColumnName, item.OperationNo)));
            string expression = strBuf.ToString(0, strBuf.Length - 2).Trim();
            ChangeDataRowPosition(expression);
        }

        private void ChangeBusinessStyle(List<OperationMessageRelation> relations)
        {
            var changeStyleDelegate = new ChangeBusinessStyleDelegate(InnerChangeBusinessStyle);
            this.currentBasePart.Invoke(changeStyleDelegate, relations);
        }

        private void ChangeDataRowPosition(string filterExpression)
        {
            this.currentBasePart.SetFilterRowCellValue(filterExpression, isAssociatedColumnName, 1);
            this.currentBasePart.SetFocusedRowHandle(0);
            this.currentBasePart.AcceptChanges();
        }


    }
}
