using System;

namespace ICP.FRM.ServiceInterface
{
    /// <summary>
    /// 商务周报表保存实体
    /// </summary>
    [Serializable]
    public class BusinessWeeklyReportSaveRequest : ICP.Framework.CommonLibrary.Common.SaveRequest
    {
        /// <summary>
        /// ID集合
        /// </summary>
        public Guid?[] IDs;

        /// <summary>
        /// 周日期
        /// </summary>
        public String WeeklyDate;

        /// <summary>
        /// 口岸ID集合
        /// </summary>
        public Guid[] DivisionIDs;

        /// <summary>
        /// 航线ID集合
        /// </summary>
        public Guid?[] ShiplineIDs;

        /// <summary>
        /// 船公司ID集合
        /// </summary>
        public Guid?[] CarrierIDs;

        /// <summary>
        /// Rates
        /// </summary>
        public String[] Rates;

        /// <summary>
        /// ShippingSpace
        /// </summary>
        public String[] ShippingSpace;

        /// <summary>
        /// Description
        /// </summary>
        public String[] Descriptions;

        /// <summary>
        /// 更新时间 
        /// </summary>
        public DateTime?[] UpdateDates;

        /// <summary>
        /// 创建人
        /// </summary>
        public Guid CreateByID;


        /// <summary>
        /// 是否英文版本
        /// </summary>
        public bool isEnglish;

    }

    /// <summary>
    /// 商务周报表经理批注保存实体
    /// </summary>
    [Serializable]
    public class BusinessWeeklyReport_ManagerSaveRequest : ICP.Framework.CommonLibrary.Common.SaveRequest
    {
        /// <summary>
        /// ID集合
        /// </summary>
        public Guid?[] IDs;

        /// <summary>
        /// 月
        /// </summary>
        public string MonthDate;

        /// <summary>
        /// 周期
        /// </summary>
        public String WeeklyDate;

        /// <summary>
        /// 口岸ID集合
        /// </summary>
        public Guid[] DivisionIDs;

        /// <summary>
        /// 贸易区ID集合
        /// </summary>
        public Guid[] ShiplineIDs;

        /// <summary>
        /// 职位ID集合
        /// </summary>
        public Guid[] JobIDs;

        /// <summary>
        /// 备注
        /// </summary>
        public String[] Descriptions;

        /// <summary>
        /// 更新时间 
        /// </summary>
        public DateTime?[] UpdateDates;

        /// <summary>
        /// 创建人
        /// </summary>
        public Guid CreateByID;

        /// <summary>
        /// 是否英文版本
        /// </summary>
        public bool isEnglish;

    }



}
