#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/6 14:18:08
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using ICP.Monitor.Interface.Framework;
using ICP.Monitor.Model;
using ICP.Monitor.Model.Framework;

namespace ICP.Monitor.BLL.Framework
{
    /// <summary>
    /// 操作日志服务
    /// </summary>
    public class OperationLogService : IOperationLogService
    {
        /// <summary>
        /// DAL OeprationLog
        /// </summary>
        DAL.Framework.OperationLogService dal = new DAL.Framework.OperationLogService();

        private WebAppContext _WebAppContext;
        /// <summary>
        /// Web上下文
        /// </summary>
        public WebAppContext WebAppContext
        {
            get
            {
                return _WebAppContext;
            }
            set
            {
                _WebAppContext = value;
                dal.WebAppContext = _WebAppContext;
            }
        }

        /// <summary>
        /// 获取操作日志
        /// </summary>
        /// <returns></returns>
        public EResultInfo GetOperationLogs(OperationLogSearchParam olSearchParam)
        {
            return dal.GetOperationLogs(olSearchParam);
        }
    }
}
