#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/9 17:24:10
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using ICP.Monitor.Model;
using ICP.Monitor.Model.Framework;

namespace ICP.Monitor.Interface.Framework
{
    public interface IOperationLogService: IBLLBaseService
    {
        EResultInfo GetOperationLogs(OperationLogSearchParam olSearchParam);
    }
}
