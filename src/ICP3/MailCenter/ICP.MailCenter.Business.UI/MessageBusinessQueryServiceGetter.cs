using System;
using System.Data;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.MailCenter.Business.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Common.CommandHandler.ServiceInterface;

namespace ICP.MailCenter.Business.UI
{
    /// <summary>
    /// 消息业务面板业务数据查询服务类获取器
    /// </summary>
    public class MessageBusinessQueryServiceGetter : IBusinessQueryServiceGetter
    {
        public WorkItem workItem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }
        }

        public IClientBusinessOperationService ClientBusinessOperationService
        {
            get
            {
                return ServiceClient.GetClientService<IClientBusinessOperationService>();
            }
        }

        public IBusinessQueryService QueryService
        {
            get
            {
                return ServiceClient.GetService<IBusinessQueryService>();
            }
        }

        private MailBusinessPartTemplateData GetTemplateData(string templateCode, ICP.Framework.CommonLibrary.Common.OperationType operationType)
        {
            MailBusinessPartTemplateData templateData = InternalMailFileLoader.Current[new SelectionKey() { TemplateCode = templateCode, Type = operationType }];
            return templateData;
        }
        #region IBusinessQueryServiceGetter 成员

        public object Query(BusinessQueryCriteria criteria, object parameter)
        {
            ICP.Message.ServiceInterface.Message message = parameter as ICP.Message.ServiceInterface.Message;

            if (message == null)
                return null;

            ////如果是内部邮件
            //if (message.UserProperties != null && !string.IsNullOrEmpty(message.UserProperties.Action))
            //{
            //    //还未处理 暂时返回Null
            //    return GetBusinessInfo(message.UserProperties);
            //}
            //else
            //{

            //如果是外部邮件
            MailBusinessPartTemplateData templateData = GetTemplateData(criteria.TemplateCode, (criteria.OperationType == null ? OperationType.OceanExport : criteria.OperationType.Value));
            if (templateData != null && !string.IsNullOrEmpty(templateData.Assmbly))
            {
                ICommonQuery query = QueryControllerFactory.CreateInstance(templateData.Assmbly,
                                                                                               templateData.ControlName,
                                                                                               templateData.TemplateCode);
                if (query != null)
                {
                    return query.GetData(criteria, MailHelper.GetMessageInfo(message));
                }
            }
            else
                throw new NotImplementedException("未能找到对应的数据查询搜索器! " + criteria.TemplateCode + "|" + criteria.OperationType.ToString());

            // }
            return null;
        }
        /// <summary>
        /// 回调刷新邮件中心业务面板时，去查找某票单，如果本地缓存表没有找到，就到服务器数据库找
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public object SingleQuery(BusinessQueryCriteria criteria, object parameter)
        {
            BusinessQueryResult result = new BusinessQueryResult();

            DataTable dt = ClientBusinessOperationService.GetLocalOperationViewList(criteria);
            Message.ServiceInterface.Message messageInfo = parameter as Message.ServiceInterface.Message;
            string messageReference = messageInfo.UserProperties == null ? string.Empty : messageInfo.UserProperties.Reference;
            string messageID = messageInfo.MessageId;
            if (dt == null || dt.Rows.Count == 0)
            {
                criteria.OperationType = OperationType.Unknown;
                //if (criteria.TemplateCode.Equals(ListFormType.MailLink4Unknown.ToString()))
                //    criteria.TemplateCode = ListFormType.MailLink4Customer.ToString();
                criteria.AdvanceQueryString = criteria.ServerQueryString;
                return QueryService.Get(criteria, messageID, messageReference);
            }

            result.Relations = ClientBusinessOperationService.GetOperationMessageRelationByMessageIdAndReference(messageID, messageReference);
            result.Dt = dt;

            return result;
        }
        #endregion
    }
}
