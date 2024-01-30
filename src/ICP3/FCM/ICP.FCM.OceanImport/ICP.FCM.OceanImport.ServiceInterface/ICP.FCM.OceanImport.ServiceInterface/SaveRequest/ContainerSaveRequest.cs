using System;

namespace ICP.FCM.OceanImport.ServiceInterface
{
    /// <summary>
    /// 业务保存类
    /// </summary>
    [Serializable]
    public class ContainerSaveRequest : ICP.Framework.CommonLibrary.Common.SaveRequest
    {
        #region ID
        /// <summary>
        /// ID
        /// </summary>
        public Guid?[] IDs;
        #endregion

        #region MBLID
        /// <summary>
        /// MBLID
        /// </summary>
        public Guid MBLID;
        #endregion

        #region 是否关联
        public bool[] IsRelations;
        #endregion

        #region 与业务关联的箱列表
        public Guid[] RelatedContainerIDs;
        #endregion

        #region NO
        public string[] Nos;

        #endregion

        #region 箱型
        /// <summary>
        /// 箱型ID
        /// </summary>
        public Guid[] ContainerTypeIDs;
        #endregion

        #region 封条号
        /// <summary>
        /// 封条号
        /// </summary>
        public string[] SealNos;
        #endregion

        #region 数量
        /// <summary>
        /// 数量
        /// </summary>
        public int?[] Quantitys;
        #endregion

        #region 重量
        /// <summary>
        /// 重量
        /// </summary>
        public  decimal?[] Weights;
        #endregion

        #region 体积
        /// <summary>
        /// 体积
        /// </summary>
        public decimal?[] Measurements;
        #endregion

        #region 提货单号
      /// <summary>
        /// 提货单号
        /// </summary>
        public string[] BLNos;
        #endregion

        #region G.O.Date
        /// <summary>
        /// G.O.Date
        /// </summary>
        public DateTime?[] GODates;
        #endregion

        #region LFDate
        /// <summary>
        /// L.F.Date
        /// </summary>
        public DateTime?[] LFDates;
        #endregion

        #region 有效日期
        /// <summary>
        /// 有效日期
        /// </summary>
        public DateTime?[] ValidDates;
        #endregion

        #region 运送日期
        /// <summary>
        /// 有效日期
        /// </summary>
        public DateTime?[] TruckDates;
        #endregion

        #region 地点
        /// <summary>
        /// 地点
        /// </summary>
        public string[] Locations;
        #endregion

        #region 备注

        /// <summary>
        /// 备注
        /// </summary>
        public string[] Remarks;
        #endregion

        #region 保存人
        /// <summary>
        /// 保存人
        /// </summary>
        public Guid saveByID;
        #endregion

        #region 更新时间
        /// <summary>
        /// 更新时间-做数据版本用
        /// </summary>
        public DateTime?[] UpdateDates;
        #endregion

        #region 是否英文
        /// <summary>
        /// 是否英文
        /// </summary>
        public bool isEnglish;
        #endregion

        #region 分单
        /// <summary>
        /// 分单
        /// </summary>
        public bool[] IsPartOfs;
        #endregion

        #region 提柜时间
        /// <summary>
        /// 提柜时间
        /// </summary>
        public DateTime?[] PickUpdates;
        #endregion

        #region 还空时间
        /// <summary>
        /// 还空时间
        /// </summary>
        public DateTime?[] Returndates;
        #endregion

        #region DeliveryTimes

        public DateTime?[] DeliveryTimes;

        #endregion
    }
}
