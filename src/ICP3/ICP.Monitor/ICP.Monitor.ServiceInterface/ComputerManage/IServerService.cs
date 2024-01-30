#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/22 17:06:16
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using ICP.Monitor.Model;

namespace ICP.Monitor.Interface.ComputerManage
{
    public interface IServerService : IBLLBaseService
    {
        #region Server
        /// <summary>
        /// 获取服务器信息
        /// </summary>
        /// <returns></returns>
        EResultInfo GetServerInfo(); 
        #endregion

        #region Windows Service
        /// <summary>
        /// 获取所有服务
        /// </summary>
        /// <returns></returns>
        EResultInfo GetServices();

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        EResultInfo StartService(string serviceName);

        /// <summary>
        /// 停止服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        EResultInfo StopService(string serviceName);

        /// <summary>
        /// 重启服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        EResultInfo ReStartService(string serviceName);

        /// <summary>
        /// 通过服务名称获取服务信息
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <returns></returns>
        EResultInfo GetServiceInfoByName(string serviceName); 
        #endregion
    }
}
