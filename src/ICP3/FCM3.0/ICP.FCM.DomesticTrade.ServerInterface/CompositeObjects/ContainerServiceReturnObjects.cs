using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FCM.DomesticTrade.ServiceInterface.CompositeObjects
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ContainerServiceReturnObjects
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ContainerSaveRequest> containers;

        /// <summary>
        /// 
        /// </summary>
        public List<CargoSaveRequest> cargos;

        /// <summary>
        /// 
        /// </summary>
        public List<ContainerPOSaveRequest> pos;
    }
}
