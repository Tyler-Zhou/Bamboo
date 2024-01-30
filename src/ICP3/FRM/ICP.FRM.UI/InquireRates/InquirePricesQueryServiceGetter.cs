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

using ICP.Operation.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using System.Data;

namespace ICP.FRM.UI.InquireRates
{
    /// <summary>
    /// 询价高级查询类
    ///     业务数据查询
    /// </summary>
    public class InquirePricesQueryServiceGetter : IBusinessQueryServiceGetter
    {
        #region IBusinessQueryServiceGetter 成员
        /// <summary>
        /// 查询业务数据
        /// </summary>
        /// <param name="criteria">业务查询信息实体类</param>
        /// <param name="parameter">参数</param>
        /// <returns>查询结果</returns>
        public object Query(BusinessQueryCriteria criteria, object parameter)
        {
            DataTable result = new DataTable();
            if (criteria != null)
            {
                try
                {
                    var data = (ClientInquierOceanRate)parameter;
                    if (data == null)
                        return null;
                    criteria.OperationType = data.OperationType;
                    criteria.UserId = LocalData.UserInfo.LoginID;
                    criteria.AdvanceQueryString = data.AdvanceQueryString;
                    criteria.TopCount = data.TopCount;
                    criteria.TemplateCode = data.ViewCode;
                    result = ServiceClient.GetService<IBusinessQueryService>().Get(criteria);
                }
                catch
                {
                    result = null;
                }
            }
            return result;
        }
        /// <summary>
        /// 查询业务数据
        /// </summary>
        /// <param name="criteria">业务查询信息实体类</param>
        /// <param name="parameter">参数</param>
        /// <returns>查询结果</returns>
        public object SingleQuery(BusinessQueryCriteria criteria, object parameter)
        {
            //处理回调刷新面板时候的查询条件
            if (ServiceClient.GetClientService<WorkItem>().State["columnQueryString"] != null)
            {
                criteria.AdvanceQueryString =
                    ServiceClient.GetClientService<WorkItem>().State["columnQueryString"].ToString();
                ServiceClient.GetClientService<WorkItem>().State["columnQueryString"] = null;
            }
            return Query(criteria, parameter);
        }

        #endregion
    }
}
