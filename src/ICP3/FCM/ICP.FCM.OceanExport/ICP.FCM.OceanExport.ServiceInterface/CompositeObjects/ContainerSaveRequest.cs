using System;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.OceanExport.ServiceInterface.CompositeObjects
{
    /// <summary>
    /// 用于保存箱的对象
    /// </summary>
    [Serializable]
    public class ContainerSaveRequest : SaveRequest
    {
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid oceanBookingID;
        /// <summary>
        /// ShippingOrderNo
        /// </summary>
        public string[] containerShippingOrderNos;
        /// <summary>
        /// 箱ID列表箱ID列表
        /// </summary>
        public Guid?[] containerIDs;
        /// <summary>
        /// 箱号列表
        /// </summary>
        public string[] containerNos;
        /// <summary>
        /// 箱主代码
        /// </summary>
        public string[] containerCTNOpers;
        /// <summary>
        /// 箱型列表
        /// </summary>
        public Guid[] containerTypeIDs;
        /// <summary>
        /// 封条号列表
        /// </summary>
        public string[] containerSealNos;
        /// <summary>
        /// VGM 重量
        /// </summary>
        public decimal[] containerVGMCrossWeights;
        /// <summary>
        /// VGM 称重方式
        /// </summary>
        public string[] containerVGMMethods;
        /// <summary>
        /// 是否客户自有箱
        /// </summary>
        public bool[] containerIsSOCs;
        /// <summary>
        /// 是否一个柜子出两套或两套以上的提单
        /// </summary>
        public bool[] containerIsPartOfs;
        /// <summary>
        /// 出发日
        /// </summary>
        public DateTime?[] deliveryDates;
        /// <summary>
        /// 到达日
        /// </summary>
        public DateTime?[] arriveDates;
        /// <summary>
        /// 还柜日
        /// </summary>
        public DateTime?[] returnDates;
        /// <summary>
        /// 司机名
        /// </summary>
        public string[] driverNames;
        /// <summary>
        /// 车牌
        /// </summary>
        public string[] carNos;
        /// <summary>
        /// 保存人
        /// </summary>
        public Guid saveByID;
        /// <summary>
        /// 更新时间-做数据版本用
        /// </summary>
        public DateTime?[] containerUpdateDates;
        /// <summary>
        /// 是否英文
        /// </summary>
        public bool isEnglish;
    }
}
