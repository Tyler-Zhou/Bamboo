using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ICP.Common.CommandHandler.ServiceInterface;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;

namespace ICP.MailCenter.Business.UI
{
    /// <summary>
    /// 邮件中心查询数据管理者
    /// </summary>
    public abstract class CommonQueryController : ICommonQuery
    {
        private object objSync = new object();
        //public ManualResetEvent manualResetEvent;

        static CommonQueryController()
        {
            ServiceClient.GetService<IBusinessContactService>();
            //允许多个等待线程，默认信号为不发送状态
            //manualResetEvent = new ManualResetEvent(false);
        }

        #region 服务

        public WorkItem RoowWorkItem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }
        }

        public Microsoft.Practices.CompositeUI.Collections.ManagedObjectCollection<object> Items
        {
            get { return RoowWorkItem.Items; }
        }

        /// <summary>
        /// 海出命令处理类
        /// </summary>
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public CommonCommandHandler commandHandler
        {
            get;
            set;
        }

        public IBusinessQueryService QueryService
        {
            get { return ServiceClient.GetService<IBusinessQueryService>(); }
        }


        public IClientBusinessOperationService ClientBusinessOperationService
        {
            get
            {
                return ServiceClient.GetClientService<IClientBusinessOperationService>();
            }
        }

        public ICP.FCM.Common.ServiceInterface.IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        public IClientBusinessContactService ClientBusinessContactService
        {
            get { return ServiceClient.GetClientService<IClientBusinessContactService>(); }
        }

        #endregion

        #region 属性
        public const string operationIdFieldName = "OceanBookingID";
        public const string operationTypeFiledName = "OperationType";
        public const string advanceSearchCompanyIDsKey = "AdvanceSearchCompanyIDs";
        /// <summary>
        /// 是否自动等待关联, true: 等待，false:不需要等待
        /// </summary>
        public bool AutoWaitRelation { get; set; }

        /// <summary>
        /// 数据集合
        /// </summary>
        public DataTable dataList { get; set; }

        /// <summary>
        /// 高级查询SQL
        /// </summary>
        public string AdvanceQueryString { get; set; }

        /// <summary>
        /// 消息实体
        /// </summary>
        public Message.ServiceInterface.Message messageEntity { get; set; }
        /// <summary>
        /// 业务查询参数
        /// </summary>
        public Operation.Common.ServiceInterface.BusinessQueryCriteria Criteria { get; set; }

        /// <summary>
        /// 关联业务实体
        /// </summary>
        public List<OperationMessageRelation> MessageRelations { get; set; }

        /// <summary>
        /// 邮件是否存在MessageID
        /// </summary>
        public bool HasMessageID { get; set; }

        public string Messageid { get; set; }

        public string MessageReference { get; set; }

        public List<string> ExternalEmails { get; set; }
        /// <summary>
        //邮件发件人类型
        /// </summary>
        public EmailSourceType[] SourceTypes { get; set; }

        #endregion

        #region IMailCenterCommonQuery 成员

        public virtual DataTable GetLocalOperationViewInfo(Guid operationID,
                                                      ICP.Framework.CommonLibrary.Common.OperationType operationType)
        {
            return ClientBusinessOperationService.GetLocalOperationViewInfo(operationID, operationType);
        }

        public virtual DataTable GetOperationViewInfo(Guid operationID, OperationType operationType)
        {
            DataTable dt = null;
            if (operationID != Guid.Empty)
                dt = QueryService.GetOperationInfo(new List<Guid>(1) { operationID }, operationType);

            return dt;
        }
        public virtual System.Data.DataTable GetOperationListBySubjectInNO(BusinessQueryCriteria criteria, ICP.Message.ServiceInterface.Message messageInfo)
        {
            return null;
        }


        public virtual bool IsUnknownBusinessPart()
        {
            return false;
        }

        public virtual DataTable GetOperationViewList(
            ICP.Operation.Common.ServiceInterface.BusinessQueryCriteria criteria)
        {
            return null;
        }

        public virtual DataTable GetOperationListByContact(BusinessQueryCriteria criteria)
        {
            return null;
        }

        public virtual DataTable GetOperationListByMessageRelations(BusinessQueryCriteria criteria, OperationMessageRelation[] messageRelations)
        {
            return null;
        }

        public virtual DataTable GetOperationListByKeyWord(BusinessQueryCriteria criteria)
        {
            return null;
        }

        /// <summary>
        /// 获取业务数据共用方法
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public BusinessQueryResult GetData(ICP.Operation.Common.ServiceInterface.BusinessQueryCriteria criteria,
                                           ICP.Message.ServiceInterface.Message message)
        {
            BusinessQueryResult result = BusinessQueryResult.CreateInstance();

            MessageReference = message.UserProperties == null ? string.Empty : message.UserProperties.Reference;
            Messageid = message.MessageId;
            //高级搜索直接从服务器上搜索
            if (criteria.SearchType == SearchActionType.Advance)
            {
                // if (criteria.TemplateCode.Equals(ListFormType.MailLink4Unknown.ToString()))
                //    criteria.TemplateCode = ListFormType.MailLink4Customer.ToString();
                criteria.OperationType = OperationType.Unknown;
                if (RoowWorkItem.State[advanceSearchCompanyIDsKey] != null)
                {
                    criteria.companyIDs = RoowWorkItem.State[advanceSearchCompanyIDsKey] as System.Guid[];
                    RoowWorkItem.State[advanceSearchCompanyIDsKey] = null;
                }
                result = QueryService.Get(criteria, Messageid, MessageReference);
                result.Dt = MergeDataTable(result.Dt, UIHelper.MessageRelationOperationList);
                return result;
            }

            SourceTypes = null;
            messageEntity = message;
            Criteria = criteria;
            dataList = result.Dt;
            AdvanceQueryString = criteria.AdvanceQueryString;
            HasMessageID = !string.IsNullOrEmpty(Messageid);
            //输入单号或客户名称搜索
            if (criteria.SearchType == SearchActionType.KeyWord)
            {
                this.dataList = GetOperationListByKeyWord(criteria);
                //将关联的业务列表合并显示                
                MergeDataTable(UIHelper.MessageRelationOperationList);
            }
            else//系统默认查找数据
            {
                //步骤1：
                //获取该邮件的发件人的业务                
                InnerGetSenderOperationList();
                //获取邮件的关联信息或主题单号查找业务        
                InnerGetMailOperationList();
            }

            #region invalidate
            //if (!MessageRelation.HasData)
            //{
            //    //承运人面板和客户面板查找业务数据
            //    if (!IsUnknownBusinessPart())
            //    {
            //        GetOperationViewList(criteria);
            //        //客户面板和承运人面板关键字查询不需要再从主题或者暗码中去查询
            //        if (criteria.SearchType != SearchType.KeyWordSearch)
            //            GetSpeciallyOperationData(criteria);
            //    }
            //    else
            //        GetSpeciallyOperationData(criteria);
            //}
            //else
            //{
            //    //参与者业务面板直接将数据返回
            //    if (IsUnknownBusinessPart())
            //    {
            //        //未知业务面板，首先从本地查找，没有的话从服务端查找
            //        DataTable dt = this.GetLocalOperationViewInfo(MessageRelation.OperationID,
            //                                                 MessageRelation.OperationType);
            //        //如果关联信息中没有找到业务数据，就需要从邮件暗码和主题查找
            //        if (dt == null || dt.Rows.Count == 0 || criteria.SearchType == SearchType.KeyWordSearch)
            //            GetSpeciallyOperationData(criteria);
            //    }
            //    //承运人面板或客户面板需要关联邮件业务联系人
            //    else
            //    {
            //        criteria.OperationType = MessageRelation.OperationType;
            //        //如果不存在,需要根据邮件暗码去找到业务,自动关联，并且将业务ContactMail保存
            //        bool isRelation = IsRelationCurrentMail(criteria, MessageRelation.OperationID,
            //                                                MessageRelation.OperationType);
            //        if (!isRelation || criteria.SearchType == SearchType.KeyWordSearch)
            //            GetSpeciallyOperationData(criteria);
            //    }
            //}

            #endregion

            result.Dt = dataList;
            result.Relations = MessageRelations;
            return result;
        }

        /// <summary>
        /// 是否是外部邮件
        /// </summary>
        /// <returns></returns>
        protected bool IsExternalMail()
        {
            return ExternalEmails!=null && ExternalEmails.Count > 0;
        }
        /// <summary>
        /// 所有外部联系人都有被存储到数据库
        /// </summary>
        /// <returns></returns>
        protected bool IsAllContactExsit()
        {
            if (SourceTypes == null || SourceTypes.Length <= 0)
                return false;

            return !SourceTypes.Any(item => item == EmailSourceType.Unknown);
        }
        /// <summary>
        ///获取该邮件的发件人的业务   
        /// </summary>
        private void InnerGetSenderOperationList()
        {
            //判断发件人是否是外部联系人
            if (!string.IsNullOrEmpty(messageEntity.SendFrom))
            {
                ExternalEmails = MailContactInfo.GetAllExternalMails(messageEntity);//获取所有外部联系人地址
                if (IsExternalMail())//是否是外部邮件
                {
                    //判断发件人是否存在于联系人列表
                    SourceTypes = ClientBusinessContactService.GetEmailTypes(ExternalEmails);//根据所有外部邮件获取联系人信息
                    if (IsAllContactExsit())// 是否所有外部联系人都有被存储到数据库
                    {
                        //获取发件人的业务列表
                        this.dataList = GetOperationListByContact(Criteria);
                        if (dataList != null)
                        {
                            int rowCount = dataList.Rows.Count;
                            if (rowCount > 5)
                            {
                                //Top 50
                                this.dataList = dataList.AsEnumerable().Take(5).CopyToDataTable();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 程序自动从本地缓存查找(根据邮件或主题单号)
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="sendFrom"></param>
        /// <param name="messageID"></param>
        /// <param name="messageReference"></param>
        /// <returns></returns>
        private void InnerGetMailOperationList()
        {
            UIHelper.MessageRelationOperationList = null;
            //步骤2：
            //查找关联信息            
            MessageRelations = ClientBusinessOperationService.GetLocalOperationMessageRelationByMessageIdAndReference
                (Messageid, MessageReference);
            UIHelper.CurrentMessageRelation = MessageRelations;
            //如果找到，返回业务数据
            if (MessageRelations != null && MessageRelations.Count > 0)
            {
                //设置该邮件存储在数据库中的IMessageID，方便后续关联业务使用
                messageEntity.Id = MessageRelations[0].IMessageId;

                DataTable dt = GetOperationListByMessageRelations(Criteria, MessageRelations.ToArray());
                //缓存系统自动默认搜索的关联业务集合
                UIHelper.MessageRelationOperationList = dt;
                MergeDataTable(dt);
            }
            else
            {
                //关联信息没有找到，再根据主题单号查找
                if (!string.IsNullOrEmpty(AdvanceQueryString))
                {
                    Criteria.AdvanceQueryString = AdvanceQueryString;
                    DataTable dt = GetOperationListBySubjectInNO(Criteria, messageEntity);
                    MergeDataTable(dt);
                    if (dataList == null || dataList.Rows.Count <= 0)
                    {
                        //没有找到，根据主题识别列表获取业务列表
                    }
                }
            }
        }
        /// <summary>
        ///合并数据 
        /// </summary>
        /// <param name="tableNew"></param>
        public void MergeDataTable(DataTable tableNew)
        {
            if (this.dataList == null || this.dataList.Rows.Count == 0)
                this.dataList = tableNew;
            else
            {
                if (tableNew != null && tableNew.Rows.Count > 0)
                {
                    this.dataList.Merge(tableNew);
                    //合并后，将OceanBookingID相同的行去除 
                    this.dataList = dataList.DefaultView.ToTable(true, (from DataColumn col in dataList.Columns
                                                                        select col.ColumnName).ToArray( ));
                }
            }
        }
        /// <summary>
        /// 合并数据集
        /// </summary>
        /// <param name="oldTable"></param>
        /// <param name="newTable"></param>
        /// <returns></returns>
        public static DataTable MergeDataTable(DataTable oldTable, DataTable newTable)
        {
            if (oldTable == null || oldTable.Rows.Count <= 0)
                return newTable;
            else
            {
                if (newTable != null && newTable.Rows.Count > 0)
                {
                    oldTable.Merge(newTable);
                    oldTable = oldTable.DefaultView.ToTable(true, (from DataColumn col in oldTable.Columns
                                                                   select col.ColumnName).ToArray());
                    return oldTable;
                }

                return newTable;
            }
        }

        #endregion
    }
}
