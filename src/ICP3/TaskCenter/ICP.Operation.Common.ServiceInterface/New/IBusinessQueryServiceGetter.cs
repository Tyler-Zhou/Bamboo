
namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 业务查询接口
    /// </summary>
   public interface IBusinessQueryServiceGetter
    {
       /// <summary>
       /// 查询
       /// </summary>
        /// <param name="criteria">业务查询信息实体</param>
       /// <param name="parameter">传入参数</param>
        /// <returns>结果集:DataTable</returns>
       object Query(BusinessQueryCriteria criteria,object parameter);
       /// <summary>
       /// 查询
       /// 处理完回调刷新面板时候的查询条件调用Query
       /// </summary>
       /// <param name="criteria">业务查询信息实体</param>
       /// <param name="parameter">传入参数</param>
       /// <returns>结果集:DataTable</returns>
       object SingleQuery(BusinessQueryCriteria criteria, object parameter);
    }
}
