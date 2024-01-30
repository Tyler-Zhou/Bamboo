using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DevExpress.Utils;
using ICP.Business.Common.ServiceInterface;
using ICP.Common.CommandHandler.ServiceInterface;
using ICP.Common.ServiceInterface;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Operation.Common.ServiceInterface;
using ICP.Operation.Common.UI;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.ObjectBuilder;
using ICP.Message.ServiceInterface;

namespace ICP.MailCenter.Business.UI
{
    public class MessageSpecialConditioner : IBusinessSpecialConditioner
    {

        public object objLock = new object();
        /// <summary>
        /// 海出命令处理类
        /// </summary>
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public CommonCommandHandler commandHandler
        {
            get;
            set;
        }
        public WorkItem RootWorkItem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }
        }
        public IFCMCommonService FcmCommonService
        {
            get { return ServiceClient.GetService<IFCMCommonService>(); }
        }

        public IBusinessQueryService BusinessQueryService
        {
            get
            {
                return ServiceClient.GetService<IBusinessQueryService>();
            }
        }

        public IICPCommonOperationService commonOperationService
        {
            get { return ServiceClient.GetService<IICPCommonOperationService>(); }
        }

        public IClientBusinessOperationService ClientBusinessOperationService
        {
            get { return ServiceClient.GetClientService<IClientBusinessOperationService>(); }
        }

        public IClientBusinessContactService ClientBusinessContactService
        {
            get { return ServiceClient.GetClientService<IClientBusinessContactService>(); }
        }


        private ListBaseBusinessPart businessPart;
        public bool QueryDataFired(IBaseBusinessPart_New baseBusinessPart, string templateCode, string queryString)
        {
            if (templateCode.Equals(ListFormType.MailLink4Carrier.ToString()))
                return false;
            else
                return true;
        }
        public void SaveOperationMemo(string templateCode, List<Guid> operationIDs, OperationType operationType, Guid formID)
        {
            if (templateCode.Equals(ListFormType.MailLink4CarrierSO.ToString()))
            {
                SaveEventMemo(operationIDs, operationType, formID, "SOD", "SO", 1, (LocalData.IsEnglish ? "SO is Done" : "订舱成功"), (LocalData.IsEnglish ? "SO is Done" : "订舱成功"), true);

            }
            else if (templateCode.Equals(ListFormType.MailLink4CarrierMBL.ToString()))
            {
                SaveEventMemo(operationIDs, operationType, formID, "MBLR", "SI", 3, (LocalData.IsEnglish ? "Received MBL Copy" : "收到MBL附件"), (LocalData.IsEnglish ? "MBL Copy is received from the carrier" : "承运人已收到MBL附件"), false);
            }
            else if (templateCode.Equals(ListFormType.MailLink4CarrierAP.ToString()))
            {
                SaveEventMemo(operationIDs, operationType, formID, "APR", "Finance", 3, (LocalData.IsEnglish ? "Received AP" : "收到应收账单"), (LocalData.IsEnglish ? "Credit Note is received from the carrier" : "承运人已收到应收账单"), false);
            }
        }

        public void SaveEventMemo(List<Guid> operationIDs, OperationType operationType,
        Guid formID,
        string code,
        string categoryName,
        int value,
        string subject,
        string description,
        bool isSendMail)
        {
            if (operationIDs == null || operationIDs.Count == 0)
                return;

            try
            {
                foreach (Guid id in operationIDs)
                {
                    FcmCommonService.SaveMemoInfo(CreateEventInfo(operationType, formID, id, code, categoryName, subject, description));
                    UpdateEventCode(id, code, value);
                    if (SOSetting.Current.NotfiyCS)
                    {
                    }
                    //commonOperationService.MailCustomerServiceForSODown(id);
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(CommonHelper.BuildExceptionString(ex));
                LocalCommonServices.ErrorTrace.SetErrorInfo(null, CommonHelper.BuildExceptionString(ex));
            }
        }
        private void UpdateEventCode(Guid operationID, string code, int value)
        {
            List<BusinessSaveParameter> parameters = new List<BusinessSaveParameter>();
            BusinessSaveParameter parameter = new BusinessSaveParameter();
            parameter["OceanBookingID"] = operationID;
            //DataTable dt = this.RootWorkItem.State["BaseDataSource"] as DataTable;
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    DataRow[] rows = dt.Select(string.Format("ID='{0}'", operationID));
            //    if (rows != null && rows.Length > 0)
            //    {
            //        parameter["UpdateDate"] = rows[0].Field<DateTime?>("UpdateDate");
            //    }
            //    else
            //    {
            //        parameter["UpdateDate"] = DateTime.Now;
            //    }
            //}

            parameter[code] = value;
            parameters.Add(parameter);
            BusinessQueryService.Save(parameters);
        }

        private EventObjects CreateEventInfo(OperationType operationType, Guid formID, Guid operationID, string code, string categoryName, string subject, string description)
        {
            return new EventObjects()
            {
                OperationID = operationID,
                OperationType = operationType,
                FormID = formID,
                Description = description,
                UpdateBy = LocalData.UserInfo.LoginID,
                CreateDate = DateTime.Now,
                Code = code,
                CategoryName = categoryName,
                Subject = subject
            };
        }




        public bool IsAutoFileSONO()
        {
            return SOSetting.Current.AutoFillSO;
        }
        /// <summary>
        /// 默认关联邮件与业务的关联， 和联系人与业务的关联
        /// </summary>
        public void DefaultMessageRelation(BusinessQueryCriteria criteria, object parameter, string advanceQueryString)
        {
            ////在高级查找和根据主题单号查找，不需要默认关联邮件
            //if (criteria.SearchType == SearchActionType.Advance || criteria.SearchType == SearchActionType.KeyWord)
            //    return;

            //RootWorkItem.State[Constants.CurrentMessageRelationKey] = null;
            //RootWorkItem.State["SubjectInKeyWordOperationList"] = null;
            //var messageInfo = parameter as Message.ServiceInterface.Message;
            //string messageId = messageInfo.MessageId;
            ////查找邮件所有外部联系人地址
            //var externalContacts = MailContactInfo.GetAllExternalContacts(messageInfo);
            //if (externalContacts != null && externalContacts.Count > 0)
            //{
            //    string messageReference = messageInfo.UserProperties == null
            //                                  ? string.Empty
            //                                  : messageInfo.UserProperties.Reference;
            //    //获取邮件的关联信息
            //    var mailOperationMessages = GetOperationMessageRelation(messageId, messageReference);
            //    if (!HasOperationMessageRelation(mailOperationMessages))
            //    {
            //        DataTable dtList = GetLocalOperationViewListBySubjectKeyWord(criteria, advanceQueryString);
            //        if (dtList != null && dtList.Rows.Count > 0)
            //        {
            //            //邮件所有外部联系人是否都有保存
            //            var emailSourceTypes =
            //                ClientBusinessContactService.GetEmailTypes(GetExternalMails(externalContacts));
            //            if (IsAllContactExsits(emailSourceTypes))
            //            {
            //                int count = dtList.Rows.Count;
            //                Guid[] operationIDs = new Guid[count];
            //                OperationType[] operationTypes = new OperationType[count];
            //                string[] operationNos = new string[count];
            //                for (int i = 0; i < count; i++)
            //                {
            //                    operationIDs[i] = dtList.Rows[i].Field<Guid>("OceanBookingID");
            //                    operationTypes[i] = (OperationType)dtList.Rows[i].Field<byte>("OperationType");
            //                    operationNos[i] = dtList.Rows[i].Field<string>("NO");
            //                }

            //                //如果保存过关联信息，就不需要在保存了
            //                var operationMessages = ClientBusinessOperationService.GetOperationMessages(messageId,
            //                                                                                            operationIDs);
            //                if (operationMessages == null || operationMessages.Count != count)
            //                {

            //                    commandHandler.SaveOperationMessageRelation(
            //                        MessageRelationParameter.CreateInstance(null,
            //                                                                MessageRelationType.Auto,
            //                                                                UpdateDataType.AddNew,
            //                                                                externalContacts,
            //                                                                null,
            //                                                                messageId, operationIDs,
            //                                                                operationTypes,
            //                                                                operationNos));
            //                }
            //            }
            //        }
            //    }
            //}
        }

        private List<string> GetExternalMails(List<MailContactInfo> mailContacts)
        {
            return (from c in mailContacts
                    select c.EmailAddress).ToList();
        }

        /// <summary>
        /// 是否存在业务关联信息
        /// </summary>
        /// <param name="operationMessageRelations"></param>
        /// <returns></returns>
        private bool HasOperationMessageRelation(List<OperationMessageRelation> operationMessageRelations)
        {
            if (operationMessageRelations != null && operationMessageRelations.Count > 0)
                return true;

            return false;
        }

        /// <summary>
        /// 根据主题单号查找关联信息
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="advanceQueryString"></param>
        /// <returns></returns>
        private DataTable GetLocalOperationViewListBySubjectKeyWord(BusinessQueryCriteria criteria, string advanceQueryString)
        {
            DataTable dtList = null;
            lock (criteria)
            {
                criteria.SearchType = SearchActionType.SubjectInNO;
                criteria.AdvanceQueryString = advanceQueryString;
                //根据主题单号查找业务
                dtList = ClientBusinessOperationService.GetLocalOperationViewListBySubjectKeyWord(criteria);
                //如果模糊匹配单号查询结果大于10条，就要过滤掉
                if (dtList != null && dtList.Rows.Count > 10)
                    dtList = dtList.AsEnumerable().Take(10).CopyToDataTable();

                RootWorkItem.State["OperationListBySubjectInKeyWord"] = dtList;
            }
            return dtList;
        }

        /// <summary>
        /// 根据邮件，获取关联信息
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="messageReference"></param>
        /// <returns></returns>
        private List<OperationMessageRelation> GetOperationMessageRelation(string messageId, string messageReference)
        {
            List<OperationMessageRelation> mailOperationMessages = null;
            mailOperationMessages = ClientBusinessOperationService.GetLocalOperationMessageRelationByMessageIdAndReference
                (messageId, messageReference);
           UIHelper.CurrentMessageRelation = mailOperationMessages;

            return mailOperationMessages;
        }
        /// <summary>
        /// 邮件里的所有外部邮件是否都有保存到服务端数据库
        /// </summary>
        /// <param name="emailSourceTypes"></param>
        /// <returns></returns>
        private bool IsAllContactExsits(EmailSourceType[] emailSourceTypes)
        {
            if (emailSourceTypes != null && emailSourceTypes.Length > 0)
                return !emailSourceTypes.Any(item => item == EmailSourceType.Unknown);

            return false;
        }



        #region IBusinessSpecialConditioner 成员


        public void SetGridRowStyle(AppearanceObject appearance, Guid operationID, bool isValid)
        {
            var currentMessageRelation = UIHelper.CurrentMessageRelation;
            if (currentMessageRelation == null || currentMessageRelation.Count <= 0)
            {
                if (!isValid)
                    GridHelper.SetColorStyle(appearance,
                                                                                  PresenceStyle.Disabled);
            }
            else
            {
                if (currentMessageRelation.Any(item => item.OperationID == operationID))
                {
                    if (!isValid)
                        GridHelper.SetColorStyle(appearance,
                                                                                    PresenceStyle.BoldAndDisabled);
                    else
                        GridHelper.SetColorStyle(appearance,
                                                                                    PresenceStyle.Bold);
                }
                else
                {
                    if (!isValid)
                        GridHelper.SetColorStyle(appearance, PresenceStyle.Disabled);

                }
            }

        }

        #endregion
    }
}
