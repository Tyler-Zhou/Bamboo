using ICP.Framework.CommonLibrary.Client;
using ICP.OA.ServiceInterface;
using ICP.OA.ServiceInterface.DataObjects;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using System.ServiceModel;
using ICP.Message.ServiceInterface;
using ICP.DataCache.ServiceInterface;
using ICP.SubscriptionPublish.ServiceInterface;
using System;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Common;
using System.Collections.Generic;
using System.Linq;
using ICP.FileSystem.ServiceInterface;

namespace ICP.SubscriptionPublish.Client
{
    /// <summary>
    /// 回调方法的具体实现
    /// </summary>
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SubscriptionPublishNotifyClientService : PublishService<IBusinessOperationCallbackService>, ISubscriptionPublishNotifyService
    {


        public IMessageNotifyService MessageNotfyService
        {
            get
            {
                return ServiceClient.GetClientService<IMessageNotifyService>();
            }
        }


        public IBulletinNotifyService BulletinNotifyService
        {
            get
            {
                return ServiceClient.GetClientService<IBulletinNotifyService>();
            }
        }

        public IDocumentNotifyService DocumentNotifyService
        {
            get
            {
                return ServiceClient.GetClientService<IDocumentNotifyService>();
            }
        }

        public IMessageSentCallbackService MessageSentCallbackService
        {
            get { return ServiceClient.GetClientService<IMessageSentCallbackService>(); }
        }

        public ICP.Business.Common.ServiceInterface.IICPCommonOperationService ICPCommonOperationService
        {
            get
            {
                return ServiceClient.GetClientService<ICP.Business.Common.ServiceInterface.IICPCommonOperationService>();
            }
        }

        /// <summary>
        /// 消息回调实现
        /// </summary>
        /// <param name="messages"></param>
        /// <param name="type"></param>
        public void ChangeState(ICP.Message.ServiceInterface.Message[] messages, MessageType type)
        {
            try
            {
                MessageNotfyService.ChangeState(messages, type);
            }
            catch (Exception ex)
            {
                SaveLog(ex);
            }

        }
        /// <summary>
        /// 公告回调实现
        /// </summary>
        /// <param name="data"></param>
        public void Notify(BulletinData data)
        {
            try
            {
                BulletinNotifyService.Notify(data);
            }
            catch (Exception ex)
            {
                SaveLog(ex);
            }
        }
        /// <summary>
        /// 文档上传回调实现
        /// </summary>
        /// <param name="type"></param>
        /// <param name="data"></param>
        /// <param name="generateIds"></param>
        public void Upload(NotifyType type, object data, object generateIds)
        {
            try
            {
                DocumentNotifyService.Upload(type, data, generateIds);
                FireEvent("Upload", type, data, generateIds);
                UpdateLocalBusiness(type, data, generateIds);
            }
            catch (Exception ex)
            {
                SaveLog(ex);
            }
        }
        private void UpdateLocalBusiness(NotifyType type, object data, object generateIDs)
        {
            if (type != NotifyType.Sucessed && type != NotifyType.Delete)
            {
                return;
            }
            System.Threading.WaitCallback callback = (temp =>
                {
                    try
                    {
                        List<DocumentInfo> documentList = new List<DocumentInfo>();
                        if (type == NotifyType.Sucessed)
                        {
                            documentList = (data as DocumentInfo[]).ToList();
                        }
                        else if (type == NotifyType.Delete)
                        {
                            ManyResult result = (ManyResult)generateIDs;
                            for (int i = 0; i < result.Items.Count; i++)
                            {
                                DocumentInfo documentInfo = new DocumentInfo();
                                documentInfo.OperationID = result.Items[i].GetValue<Guid>("OperationID");
                                documentInfo.Type = (OperationType)result.Items[i].GetValue<Byte>("OperationType");
                                documentList.Add(documentInfo);
                            }
                        }

                        var operationInfoGroups = (from document in documentList
                                                   group document by new { document.OperationID, document.Type } into groups
                                                   select new { Key = groups.Key }).ToList();
                        foreach (var group in operationInfoGroups)
                        {
                            ICPCommonOperationService.UpdateLocalBusinessData(group.Key.OperationID, group.Key.Type);
                        }
                    }
                    catch (Exception ex)
                    {
                        ICP.Framework.CommonLibrary.Logger.Log.Error(ex.Message, ex);
                    }
                });
            System.Threading.ThreadPool.QueueUserWorkItem(callback);

        }
        private void SaveLog(Exception ex)
        {

            LogHelper.SaveLog(CommonHelper.BuildExceptionString(ex));
        }
        /// <summary>
        /// 处理邮件发送成功以后，回调刷新任务中心列表
        /// </summary>
        /// <param name="operationId">业务的ID</param>
        /// <param name="operationType">业务的类别</param>
        public void SendMessage(Guid[] operationIds, OperationType[] operationTypes)
        {
            try
            {
                for (int i = 0; i < operationIds.Length; i++)
                {
                    MessageSentCallbackService.HandleMessageSent(operationIds[i], operationTypes[i]);
                }
            }
            catch (Exception ex)
            {
                SaveLog(ex);
            }
        }
    }

}