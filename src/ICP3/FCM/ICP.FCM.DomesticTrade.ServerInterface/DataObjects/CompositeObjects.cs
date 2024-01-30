using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.DomesticTrade.ServiceInterface.DataObjects
{
    /// <summary>
    /// 组合数据对象
    /// 用于返回订舱单的箱信息
    /// </summary>
    [Serializable]
    public class CompositeContainerObjects : BaseDataObject
    {
        /// <summary>
        /// 订舱单的某些信息是需要的
        /// </summary>
        public DTBookingInfo BookingInfo { get; set; }

        /// <summary>
        /// 箱列表
        /// </summary>
        public List<DTContainerList> ContainerList { get; set; }

        /// <summary>
        /// 货物列表
        /// </summary>
        public List<DTContainerCargoList> CargoList { get; set; }

    }
}
