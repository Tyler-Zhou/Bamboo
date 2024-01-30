using System;
using System.Linq;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Operation.Common.ServiceInterface;
using ICP.Operation.Common.UI;
using Microsoft.Practices.CompositeUI;

namespace ICP.MailCenter.Business.UI
{
    /// <summary>
    /// 关联邮件业务信息查询条件获取器
    /// </summary>
    public class MessageQueryCriteriaGetter : IQueryCriteriaGetter
    {
        public WorkItem rootWorkItem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }
        }

        public BusinessQueryCriteria Get(IBaseBusinessPart_New basePart, object parameter, bool mergeAdvanceQueryString)
        {
            BusinessQueryCriteria criteria = new BusinessQueryCriteria();
            criteria.TemplateCode = basePart.TemplateCode;
            criteria.OperationType = basePart.OperationType;
            criteria.SearchType = basePart.SearchType;
            criteria.NeedSearchInSQLServer = basePart.NeedSearchInSQLServer;
            criteria.ServerQueryString = basePart.ServerQueryString;
            if (basePart is ListBaseBusinessPart)
            {
                ListBaseBusinessPart messagePart = basePart as ListBaseBusinessPart;
                criteria.companyIDs = string.IsNullOrEmpty(messagePart.SelectedCompanyIds) ? null : messagePart.SelectedCompanyIds.Split(',').Select(id => new Guid(id)).ToArray();
                //criteria.ScopeItem = string.IsNullOrEmpty(messagePart.SelectedScope)?"All":messagePart.SelectedScope;
                ICP.Message.ServiceInterface.Message message = parameter as ICP.Message.ServiceInterface.Message;
                //高级搜索不需要传入发件人
                criteria.EmailAddress = basePart.SearchType == SearchActionType.Advance ? "" : message.SendFrom;
            }

            if (mergeAdvanceQueryString)
            {
                criteria.AdvanceQueryString = basePart.AdvanceQueryString;
            }
            return criteria;
        }

        public BusinessQueryCriteria GetEntity(IBaseBusinessPart_New basePart, object parameter, bool mergeAdvanceQueryString,
                                        BusinessOperationParameter businessOperationParameter)
        {
            ////区分是否要连接本地缓存数据库还是数据库,1表示连接数据库，0表示连接本地缓存数据            
            //如果从本地缓存数据库里没有找到数据，就需要到SQL Server 去查询            
            BusinessQueryCriteria criteria = new BusinessQueryCriteria();
            criteria.SearchType = SearchActionType.Auto;
            criteria.NeedSearchInSQLServer = true;
            criteria.TemplateCode = businessOperationParameter.Context["TemplateCode"].ToString();
            criteria.OperationType = businessOperationParameter.Context.OperationType;
            if (basePart is ListBaseBusinessPart)
            {
                ListBaseBusinessPart messagePart = basePart as ListBaseBusinessPart;
                messagePart.AdvanceQueryString = " AND " + ICP.Operation.Common.ServiceInterface.Constants.OperationNOFieldName + "='" + businessOperationParameter.Context.OperationNO + "'";
                criteria.ServerQueryString = string.Format(" 1=1 AND $@NO@ LIKE '%{0}%'", businessOperationParameter.Context.OperationNO);
                criteria.companyIDs = string.IsNullOrEmpty(messagePart.SelectedCompanyIds) ? null : messagePart.SelectedCompanyIds.Split(',').Select(id => new Guid(id)).ToArray();
                ICP.Message.ServiceInterface.Message message = parameter as ICP.Message.ServiceInterface.Message;
                criteria.EmailAddress = criteria.TemplateCode.Equals(ListFormType.MailLink4Unknown.ToString()) ? string.Empty : message.SendFrom;
            }

            if (mergeAdvanceQueryString)
            {
                criteria.AdvanceQueryString = basePart.AdvanceQueryString;
            }

            return criteria;
        }
    }
}
