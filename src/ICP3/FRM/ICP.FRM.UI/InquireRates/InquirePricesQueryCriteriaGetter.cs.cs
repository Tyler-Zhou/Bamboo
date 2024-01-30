#region Comment

/*
 * 
 * FileName:    InquirePricesQueryServiceGetter.cs
 * CreatedOn:   
 * CreatedBy:   
 * 
 * 
 * Description：
 *      ->询价高级查询类
 * History：
 *      ->
 * 
 * 
 * 
 */

#endregion

using System;
using System.Linq;
using ICP.Operation.Common.ServiceInterface;

namespace ICP.FRM.UI.InquireRates
{
    public class InquirePricesQueryCriteriaGetter : IQueryCriteriaGetter
    {
        #region IQueryCriteriaGetter 成员

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

        public BusinessQueryCriteria GetEntity(IBaseBusinessPart_New basePart, object parameter, bool mergeAdvanceQueryString, BusinessOperationParameter businessOperationParameter)
        {
            return Get(basePart, parameter, mergeAdvanceQueryString);
        }

        #endregion
    }
}
