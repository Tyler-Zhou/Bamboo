#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/22 17:07:12
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using ICP.Monitor.Interface.ComputerManage;
using ICP.Monitor.Model;
using ICP.Monitor.Model.Framework;

namespace ICP.Monitor.BLL.ComputerManage
{
    public class ServerService : IServerService
    {
        #region Fields & Property
        /// <summary>
        /// DAL OeprationLog
        /// </summary>
        DAL.ComputerManage.ServerService dal = new DAL.ComputerManage.ServerService();

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
        #endregion

        #region Server
        /// <summary>
        /// 获取服务器信息
        /// </summary>
        /// <returns></returns>
        public EResultInfo GetServerInfo()
        {
            return dal.GetServerInfo();
        } 
        #endregion

        #region Windows Service
        /// <summary>
        /// 获取所有服务
        /// </summary>
        /// <returns></returns>
        public EResultInfo GetServices()
        {
            return dal.GetServices();
        }
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public EResultInfo StartService(string serviceName)
        {
            return dal.StartService(serviceName);
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public EResultInfo StopService(string serviceName)
        {
            return dal.StopService(serviceName);
        }

        /// <summary>
        /// 重启服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public EResultInfo ReStartService(string serviceName)
        {
            return dal.ReStartService(serviceName);
        }

        /// <summary>
        /// 通过服务名称获取服务信息
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <returns></returns>
        public EResultInfo GetServiceInfoByName(string serviceName)
        {
            return dal.GetServiceInfoByName(serviceName);
        } 
        #endregion
    }
}
