using System;
using System.Collections.Generic;
using System.Linq;
using ICP.Framework.CommonLibrary.Client;
using ICP.Message.ServiceInterface;
using System.Threading;
using System.Data;
using ICP.DataCache.ServiceInterface;
using Microsoft.Practices.ObjectBuilder;
using ICP.MailCenter.Business.ServiceInterface;

namespace ICP.Common.Business.ServiceInterface
{
    class EmailCenterOperation : AbstractBussinessPartOperation
    {
        #region IBussinessPartOperation 成员

        /// <summary>
        ///  构造函数
        /// </summary>
        /// <param name="baseBusinessPart">当前业务面板</param>
        /// <param name="operationSourceType">当前业务面板来源</param>
        public EmailCenterOperation(BaseBusinessPart baseBusinessPart)
        {
            this.BaseBusinessPart = baseBusinessPart;
            this.BaseBusinessPart.OperationSourceType = OperationSourceType.EmailCenterOperation;
            this.BaseBusinessPart.AbstractBussinessPartOperation = this;
        }

        public void InitEmail(ICP.Message.ServiceInterface.Message email)
        {
            BaseBusinessPart.CurrentMessage = email;
            BaseBusinessPart.RegisterExtensionSite();
        }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public MailInfoGetter MailInfoGetter { get; set; }

        /// <summary>
        /// 业务面板初始化入口
        /// 动作:1.解析邮件信息
        ///      2.注册工具栏，右键菜单栏拓展点
        ///      3.获取工具栏命令实体，构造工具栏
        ///      4.获取列实体列表，构造列表列
        ///      5.挂接命令处理
        /// </summary>
        /// <param name="initObj"></param>
        protected override void init()
        {
            MailInfoGetter.ExtractMailInfo(BaseBusinessPart.CurrentMessage, this.GetType().Name);
            BaseBusinessPart.SourceType = MailInfoGetter.SourceType;
            BaseBusinessPart.Nos = MailInfoGetter.Nos;
            BaseBusinessPart.SenderEmailAddress = MailInfoGetter.EmailAddress;
        }

    
        /// <summary>
        /// 绑定预处理
        /// </summary>
        /// <param name="mail"></param>
        public override void PreBindData()
        {
            init();
            BaseBusinessPart.CurrentMessageRelation = null;
            BaseBusinessPart.ResetMessageRelationRecord();
        }

        //public override void QueryAndBindData(bool mergeAdvanceQueryString)
        //{
        //    BusinessQueryCriteria criteria = null;
        //    if (BaseBusinessPart.NeedBindData)
        //    {
        //        criteria = GetQueryCriteria(mergeAdvanceQueryString);
        //    }
        //    WaitCallback callback = (data) =>
        //    {
        //        ICP.Common.Business.ServiceInterface.BaseBusinessPart.BindParameter temp = data as ICP.Common.Business.ServiceInterface.BaseBusinessPart.BindParameter;
        //        //查询数据
        //        BusinessQueryResult result = GetData(temp.QueryCriteria);
        //        BaseBusinessPart.AddCustomColumn(result.Dt);
        //        temp.Dt = result.Dt;
        //        InnerBindData(result, temp);
        //    };
        //    ICP.Common.Business.ServiceInterface.BaseBusinessPart.BindParameter parameter = new ICP.Common.Business.ServiceInterface.BaseBusinessPart.BindParameter { Message = BaseBusinessPart.CurrentMessage, NeedBindData = BaseBusinessPart.NeedBindData, Nos = BaseBusinessPart.Nos, QueryCriteria = criteria };
        //    ThreadPool.QueueUserWorkItem(callback, parameter);
        //}


        /// <summary>
        /// 获取数据查询条件
        /// </summary>
        /// <returns></returns>
        protected override BusinessQueryCriteria GetQueryCriteria(bool mergeAdvanceQueryString)
        {
            BusinessQueryCriteria criteria = new BusinessQueryCriteria();
            criteria.companyIDs = string.IsNullOrEmpty(BaseBusinessPart.selectedCompanyIds) ? null : BaseBusinessPart.selectedCompanyIds.Split(',').Select(id => new Guid(id)).ToArray();
            criteria.TemplateCode = BaseBusinessPart.TemplateCode;
            if (mergeAdvanceQueryString)
            {
                criteria.AdvanceQueryString = BaseBusinessPart.AdvanceQueryString;
            }
            return criteria;
        }

       
        protected override    void InnerBindData(BusinessQueryResult result, BindParameter parameter)
        {
            if (IsSameAsCurrentMessage(parameter.Message))
            {
                BaseBusinessPart.Invoke(new DataBindDelegate(SetBindingSource), result);
                BaseBusinessPart.ResetQueryRecord();
                if (parameter.NeedBindData)
                {

                    ProcessMessageRelation(result, parameter);
                }
            }

        }
        private void ProcessMessageRelation(BusinessQueryResult result, BindParameter parameter)
        {
            if (!IsSameAsCurrentMessage(parameter.Message))
                return;
            BaseBusinessPart.dicMessageRelation.Clear();
            BaseBusinessPart.CurrentMessageRelation = null;
            //如果当前邮件已关联业务，则更改关联业务行的显示样式，将业务行放到第一行位置
            if (result.Relation.HasData)
            {
                BaseBusinessPart.ChangeBusinessStyle(result.Relation, parameter.Message);
                return;
            }
            DataTable dt = parameter.Dt;
            List<string> nos = parameter.Nos;
            if (dt == null || dt.Rows.Count <= 0)
                return;
            if (nos == null || nos.Count <= 0)
            {
               BaseBusinessPart. ClearBusinessStyleFormatCondition();
                return;
            }
            string filterExpression = DataCacheUtility.GetExpression(nos, BaseBusinessPart.ConditionColumnNames, true);
            DataRow[] rows = dt.Select(filterExpression);
            if (rows == null || rows.Length <= 0)
            {
                BaseBusinessPart.ClearBusinessStyleFormatCondition();
                return;
            }
            string operationNo = rows[0].Field<string>("NO");
            Guid operationId = rows[0].Field<Guid>("ID");
            ICP.Message.ServiceInterface.Message message = parameter.Message;
            message.UserProperties = new MessageUserPropertiesObject();
            message.UserProperties.OperationId = operationId;
            message.UserProperties.OperationType = BaseBusinessPart.GetOperationType();
            if (!IsSameAsCurrentMessage(parameter.Message))
                return;
            message.UserProperties["OperationNo"] = operationNo;
            WaitCallback callback = (data) =>
            {

                ICP.Message.ServiceInterface.Message temp = data as ICP.Message.ServiceInterface.Message;
                if (!IsSameAsCurrentMessage(parameter.Message))
                    return;

                ServiceClient.GetService<IClientMessageService>().Save(message);
                if (!IsSameAsCurrentMessage(message))
                    return;

                OperationMessageRelation relation = new OperationMessageRelation { HasData = true, OperationID = operationId, IMessageId = message.Id, ID = Guid.NewGuid(), OperationNo = operationNo, MessageId = message.MessageId };
                lock (BaseBusinessPart.syncObj)
                {
                    if (IsSameAsCurrentMessage(message))
                    {
                       BaseBusinessPart. ChangeBusinessStyle(relation, message);
                    }
                }
            };
            ThreadPool.QueueUserWorkItem(callback, message);
        }

        /// <summary>
        /// 比较两消息是否相等，当前仅比较了MessageId和发件人
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual bool IsSameAsCurrentMessage(ICP.Message.ServiceInterface.Message message)
        {
            return BaseBusinessPart.CurrentMessage.Id == message.Id;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="criteria">如果不需要查询实际数据，则传递Null值</param>
        /// <returns></returns>
        protected override  BusinessQueryResult GetData(BusinessQueryCriteria criteria)
        {
            BusinessQueryResult result = new BusinessQueryResult();
            result.Dt = new DataTable();
            result.Relation = new OperationMessageRelation { HasData = false };
            if (criteria != null)
            {
                result = ServiceClient.GetService<IBusinessQueryService>().Get(criteria, BaseBusinessPart.CurrentMessage);
                if (result.Relation == null)
                {
                    result.Relation = new OperationMessageRelation { HasData = false };
                }

            }
            return result;
        }



        #endregion
    }
}
