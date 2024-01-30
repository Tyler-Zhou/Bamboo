using ICP.Operation.Common.ServiceInterface;
using System;
using System.Linq;

namespace ICP.TaskCenter.UI
{
    /// <summary>
    /// 查询条件获取器
    /// </summary>
    public class TaskCenterQueryCriteriaGetter : IQueryCriteriaGetter
    {

        #region IQueryCriteriaGetter 成员

        /// <summary>
        /// 获取业务查询信息实体
        /// </summary>
        /// <param name="basePart">基础业务面板接口</param>
        /// <param name="parameter">传入参数</param>
        /// <param name="mergeAdvanceQueryString">是否高级查询</param>
        /// <returns>业务查询信息实体</returns>
        public BusinessQueryCriteria Get(IBaseBusinessPart_New basePart, object parameter, bool mergeAdvanceQueryString)
        {
            BusinessQueryCriteria criteria = new BusinessQueryCriteria();
            criteria.companyIDs = string.IsNullOrEmpty(basePart.SelectedCompanyIds) ? null : basePart.SelectedCompanyIds.Split(',').Select(id => new Guid(id)).ToArray();
            criteria.TemplateCode = basePart.TemplateCode;
            if (mergeAdvanceQueryString)
            {
                criteria.AdvanceQueryString = basePart.AdvanceQueryString;
            }
            return criteria;
        }

        /// <summary>
        /// 获取业务查询信息实体
        /// </summary>
        /// <param name="basePart">基础业务面板接口</param>
        /// <param name="parameter">传入参数</param>
        /// <param name="mergeAdvanceQueryString">是否高级查询</param>
        /// <param name="businessOperationParameter">邮件业务关联参数</param>
        /// <returns>业务查询信息实体</returns>
        public BusinessQueryCriteria GetEntity(IBaseBusinessPart_New basePart, object parameter,
                                                bool mergeAdvanceQueryString,
            BusinessOperationParameter businessOperationParameter)
        {
            return Get(basePart, parameter, mergeAdvanceQueryString);
        }

        #endregion
    }
}
