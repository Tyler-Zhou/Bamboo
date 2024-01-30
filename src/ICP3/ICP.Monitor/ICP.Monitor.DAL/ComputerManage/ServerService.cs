#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/22 16:45:14
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using ICP.Monitor.Model;
using ICP.Monitor.Model.ComputerManage;

namespace ICP.Monitor.DAL.ComputerManage
{
    public class ServerService : DALBaseService
    {
        EWindowsServiceInfo[] services = {
                                  new EWindowsServiceInfo {ServiceName = "ICPWindowsService", ServiceState = "",Choosed=false},
                                  new EWindowsServiceInfo {ServiceName = "ICPFileSystemService", ServiceState = "",Choosed=false}
                              };
        #region Server
        /// <summary>
        /// 获取服务器信息
        /// </summary>
        /// <returns></returns>
        public EResultInfo GetServerInfo()
        {
            EResultInfo re = new EResultInfo();

            try
            {
                EServerInfo si = new EServerInfo
                {
                    ServerName = ComputerHelper.GetMachineName(),
                    CPU = ComputerHelper.GetCPUInfo(),
                    Memory = ComputerHelper.GetMemoryInfo(),
                    Drives = ComputerHelper.GetHardDisks()
                };
                re.Data = si;
                return re;
            }
            catch (Exception ex)
            {
                re.Exception = HandlingException(ex);
                return re;
            }
        } 
        #endregion

        #region Windows Service
        /// <summary>
        /// 获取所有服务
        /// </summary>
        /// <returns></returns>
        public EResultInfo GetServices()
        {
            EResultInfo re = new EResultInfo { Data = new List<EWindowsServiceInfo>() };

            try
            {
                List<EWindowsServiceInfo> sis = new List<EWindowsServiceInfo>();
                foreach (EWindowsServiceInfo item in services)
                {
                    if (WindowsServiceIsExisted(item.ServiceName))
                    {
                        using (ServiceController serviceItem = new ServiceController(item.ServiceName))
                        {
                            item.ServiceState = GetWindowsServiceState(serviceItem);
                        }
                        sis.Add(item);
                    }
                    else
                        item.ServiceState = "服务未安装";

                }
                re.Data = sis;
                return re;
            }
            catch (Exception ex)
            {
                re.Exception = HandlingException(ex);
                return re;
            }
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public EResultInfo StartService(string serviceName)
        {
            EResultInfo re = new EResultInfo { Data = false };

            try
            {
                if (WindowsServiceIsExisted(serviceName))
                {
                    using (ServiceController serviceItem = new ServiceController(serviceName))
                    {
                        re.Data = StartWindowsService(serviceItem);
                    }
                }
                return re;
            }
            catch (Exception ex)
            {
                re.Exception = HandlingException(ex);
                return re;
            }
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public EResultInfo StopService(string serviceName)
        {
            EResultInfo re = new EResultInfo { Data = false };

            try
            {
                if (WindowsServiceIsExisted(serviceName))
                {
                    using (ServiceController serviceItem = new ServiceController(serviceName))
                    {
                        re.Data = StopWindowsService(serviceItem);
                    }
                }
                return re;
            }
            catch (Exception ex)
            {
                re.Exception = HandlingException(ex);
                return re;
            }
        }

        /// <summary>
        /// 重启服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public EResultInfo ReStartService(string serviceName)
        {
            EResultInfo re = new EResultInfo { Data = false };

            try
            {
                if (WindowsServiceIsExisted(serviceName))
                {
                    using (ServiceController serviceItem = new ServiceController(serviceName))
                    {
                        re.Data = ReStartWindowsWService(serviceItem);
                    }
                }
                return re;
            }
            catch (Exception ex)
            {
                re.Exception = HandlingException(ex);
                return re;
            }
        }

        /// <summary>
        /// 通过服务名称获取服务信息
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <returns></returns>
        public EResultInfo GetServiceInfoByName(string serviceName)
        {
            EResultInfo re = new EResultInfo { Data = new EWindowsServiceInfo() };

            try
            {
                re.Data = BulidServieInfo(serviceName);
                return re;
            }
            catch (Exception ex)
            {
                re.Exception = HandlingException(ex);
                return re;
            }
        }
        /// <summary>
        /// 构建Windows服务信息
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        EWindowsServiceInfo BulidServieInfo(string serviceName)
        {
            EWindowsServiceInfo si = new EWindowsServiceInfo();
            if (WindowsServiceIsExisted(serviceName))
            {
                using (ServiceController sc = new ServiceController(serviceName))
                {
                    si.ServiceName = serviceName;
                    si.ServiceState = GetWindowsServiceState(sc);
                }
            }
            return si;
        }
        /// <summary>
        /// 判断window服务是否存在
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        bool WindowsServiceIsExisted(string serviceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            return services.Any(s => s.ServiceName == serviceName);
        }
        /// <summary>
        /// 获取服务状态
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        string GetWindowsServiceState(ServiceController service)
        {
            string returnValue = string.Empty;
            string state = service.Status.ToString();
            switch (state)
            {
                case "Stopped":
                    returnValue = "服务已停止";
                    break;
                case "Running":
                    returnValue = "服务运行中";
                    break;
                case "Paused":
                    returnValue = "服务已暂停";
                    break;
                case "StartPending":
                    returnValue = "服务正在启动";
                    break;
                case "StopPending":
                    returnValue = "服务正在停止";
                    break;
                case "ContinuePending":
                    returnValue = "服务即将继续";
                    break;
                case "PausePending":
                    returnValue = "服务即将暂停";
                    break;
            }
            return returnValue;
        }
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        bool StartWindowsService(ServiceController service)
        {
            service.Start();
            //等待服务到达运行状态
            service.WaitForStatus(ServiceControllerStatus.Running);
            return true;
        }
        /// <summary>
        /// 停止服务
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        bool StopWindowsService(ServiceController service)
        {
            service.Stop();
            //等待服务到达运行状态
            service.WaitForStatus(ServiceControllerStatus.Stopped);
            return true;
        }

        /// <summary>
        /// 重启服务
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        bool ReStartWindowsWService(ServiceController service)
        {
            //停止服务
            service.Stop();
            service.WaitForStatus(ServiceControllerStatus.Stopped);
            //启动服务
            service.Start();
            service.WaitForStatus(ServiceControllerStatus.Running);
            return true;
        } 
        #endregion
    }
}
