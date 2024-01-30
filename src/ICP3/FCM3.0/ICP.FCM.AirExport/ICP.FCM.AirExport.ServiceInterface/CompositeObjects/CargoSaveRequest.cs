using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.AirExport.ServiceInterface.CompositeObjects
{
    /// <summary>
    ///用于保存货物的对象
    /// </summary>
    [Serializable]
    public class CargoSaveRequest : SaveRequest
    {
        /// <summary>
        /// 事务的Id
        /// </summary>
        public Guid transactionId;
        /// <summary>
        /// 箱ID
        /// </summary>
        public Guid containerID;
        /// <summary>
        /// 要建立关联的MBL ID
        /// </summary>
        public Guid[] mblIDs;
        /// <summary>
        /// MBLNO 如果输入的主提单号不存在，那么根据订舱信息和装箱信息新建主提单
        /// </summary>
        public string[] mblNos;
        /// <summary>
        /// ID
        /// </summary>
        public Guid?[] ids;
        /// <summary>
        /// 唛头
        /// </summary>
        public string[] marks;
        /// <summary>
        /// 品名
        /// </summary>
        public string[] commodities;
        /// <summary>
        /// 数量
        /// </summary>
        public int[] quantities;
        /// <summary>
        /// 数量单位
        /// </summary>
        public Guid[] quantityUnitIDs;
        /// <summary>
        /// 重量
        /// </summary>
        public decimal[] weights;
        /// <summary>
        /// 重量单位
        /// </summary>
        public Guid[] weightUnitIDs;
        /// <summary>
        /// 体积
        /// </summary>
        public decimal[] measurements;
        /// <summary>
        /// 体积单位
        /// </summary>
        public Guid[] measurementUnitIDs;
        /// <summary>
        /// 保存人
        /// </summary>
        public Guid saveByID;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime?[] updateDates;
    }
}
