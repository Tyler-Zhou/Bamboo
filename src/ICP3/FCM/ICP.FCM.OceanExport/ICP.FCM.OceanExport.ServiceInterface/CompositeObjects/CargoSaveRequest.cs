using ICP.Framework.CommonLibrary.Common;
using System;

namespace ICP.FCM.OceanExport.ServiceInterface.CompositeObjects
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
        public Guid transactionId{get;set;}
        /// <summary>
        /// 箱ID
        /// </summary>
        public Guid containerID{get;set;}
        /// <summary>
        /// 要建立关联的MBL ID
        /// </summary>
        public Guid[] mblIDs{get;set;}
        /// <summary>
        /// MBLNO 如果输入的主提单号不存在，那么根据订舱信息和装箱信息新建主提单
        /// </summary>
        public string[] mblNos{get;set;}
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid oceanBookingID{get;set;}
        /// <summary>
        /// 主提单ID
        /// </summary>
        public Guid MBLID{get;set;}
        /// <summary>
        /// 主提单号
        /// </summary>
        public string MBLNo{get;set;}
        /// <summary>
        /// 分提单ID
        /// </summary>
        public Guid[] hblids{get;set;}
        /// <summary>
        /// 分提单号
        /// </summary>
        public string[] hblnos{get;set;}
        /// <summary>
        /// 箱ID集合
        /// </summary>
        public Guid[] containerids { get; set; }
        /// <summary>
        /// 箱号集合
        /// </summary>
        public string[] containernos { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        public Guid?[] ids{get;set;}
        /// <summary>
        /// 唛头
        /// </summary>
        public string[] marks{get;set;}
        /// <summary>
        /// 品名
        /// </summary>
        public string[] commodities{get;set;}
        /// <summary>
        /// HSCode
        /// </summary>
        public string[] hscodes{get;set;}
        /// <summary>
        /// 数量
        /// </summary>
        public int[] quantities{get;set;}
        /// <summary>
        /// 数量单位
        /// </summary>
        public Guid[] quantityUnitIDs{get;set;}
        /// <summary>
        /// 重量
        /// </summary>
        public decimal[] weights{get;set;}
        /// <summary>
        /// 重量单位
        /// </summary>
        public Guid[] weightUnitIDs{get;set;}
        /// <summary>
        /// 体积
        /// </summary>
        public decimal[] measurements{get;set;}
        /// <summary>
        /// 体积单位
        /// </summary>
        public Guid[] measurementUnitIDs{get;set;}
        /// <summary>
        /// 保存人
        /// </summary>
        public Guid saveByID{get;set;}
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime?[] updateDates{get;set;}
    }
}
