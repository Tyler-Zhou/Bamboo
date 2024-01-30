using ICP.Framework.CommonLibrary.Client;
using ICP.Operation.Common.ServiceInterface;
using ICP.TaskCenter.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using System.Data;

namespace ICP.TaskCenter.UI
{
    /// <summary>
    /// 业务查询
    /// </summary>
    public class TaskCenterQueryServiceGetter : IBusinessQueryServiceGetter
    {
        #region IBusinessQueryServiceGetter 成员
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="criteria">业务查询信息实体</param>
        /// <param name="parameter">传入参数</param>
        /// <returns>结果集:DataTable</returns>
        public object Query(BusinessQueryCriteria criteria, object parameter)
        {
            DataTable result = new DataTable();
            if (criteria != null)
            {
                // 根据节点的所有人进行查询操作
                var node = (NodeInfo)parameter;
                criteria.LockCompanyIDs = node.LockCompanyIDs;
                criteria.UserId = node.UserID;
                //当节点的高级查询条件在 但是上下文在构造中未取到高级查询时，进行赋值操作
                if (!string.IsNullOrEmpty(node.AdvanceQueryString) && string.IsNullOrEmpty(criteria.AdvanceQueryString))
                {
                    criteria.AdvanceQueryString = node.AdvanceQueryString;
                }
                criteria.TopCount = node.TopCount;
                criteria.OperationType = node.OperationType;
                result = ServiceClient.GetService<IBusinessQueryService>().Get(criteria);
            }
            return result;
        }

        /// <summary>
        /// 查询
        /// 处理完回调刷新面板时候的查询条件调用Query
        /// </summary>
        /// <param name="criteria">业务查询信息实体</param>
        /// <param name="parameter">传入参数</param>
        /// <returns>结果集:DataTable</returns>
        public object SingleQuery(BusinessQueryCriteria criteria, object parameter)
        {
            //处理回调刷新面板时候的查询条件
            if (ServiceClient.GetClientService<WorkItem>().State["columnQueryString"] != null && string.IsNullOrEmpty(criteria.AdvanceQueryString))
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
