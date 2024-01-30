using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.DataObjects;

namespace ICP.FCM.OceanExport.ServiceInterface.DataObjects
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
        public OceanBookingInfo BookingInfo { get; set; }

        /// <summary>
        /// 箱列表
        /// </summary>
        public List<OceanContainerList> ContainerList { get; set; }

        /// <summary>
        /// 货物列表
        /// </summary>
        public List<OceanContainerCargoList> CargoList { get; set; }

        /// <summary>
        /// MBL列表
        /// </summary>
        public List<OceanMBLInfo> MBLs { get; set; }
        /// <summary>
        /// HBL列表
        /// </summary>
        public List<OceanHBLInfo> HBLs { get; set; }
    }

    /// <summary>
    /// 利润表信息
    /// </summary>
    [Serializable]
    public partial class ProfitContainerObjects : BaseDataObject
    {
        //业务账单列表
        public List<BillTotalInfo> BillInfoList
        {
            get;

            set;
        }

        //公司配置信息
        public List<ConfigureInfo> ConfigureInfo
        {
            get;
            set;
        }
    }

    [Serializable]

    public partial class BillTotalInfo : BillInfo
    {
        //总额
        public decimal Amount
        {
            get;
            set;
        }

        //方向（0:应收,1:应付）
        public FeeWay Way
        {
            get;
            set;
        }
    }
}
