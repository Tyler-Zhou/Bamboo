using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using ICP.EDIManager.ServiceInterface;
using System.Timers;

namespace ICP.EDIManager.ServiceComponent
{
    public class EDIManagerServiceHost : ICP.EDIManager.ServiceInterface.IEDIManagerServiceHost
    {
        /// <summary>
        /// 
        /// </summary>
        private ServiceHost _myServiceHost;

        private EDIManagerService _myService = new EDIManagerService();

        /// <summary>
        /// 
        /// </summary>
        public void StartService()
        {
            _myServiceHost = new ServiceHost(typeof(EDIManagerService));//实例化WCF服务对象
            _myServiceHost.Open();

            _myService.Download();

        }

 

        /// <summary>
        /// 
        /// </summary>
        public void StopService()
        {
            if (_myServiceHost.State != CommunicationState.Closed)
                _myServiceHost.Close();
            _myService.Dispose();
        }
    }
}
