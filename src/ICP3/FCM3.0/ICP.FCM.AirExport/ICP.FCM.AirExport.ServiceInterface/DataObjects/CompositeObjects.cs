using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.AirExport.ServiceInterface.DataObjects
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
        public AirBookingInfo BookingInfo { get; set; }

        /// <summary>
        /// 箱列表
        /// </summary>
        public List<AirContainerList> ContainerList { get; set; }

        /// <summary>
        /// 货物列表
        /// </summary>
        public List<AirContainerCargoList> CargoList { get; set; }

        /// <summary>
        /// MBL列表
        /// </summary>
        public List<AirMBLInfo> MBLs { get; set; }
    }
}
