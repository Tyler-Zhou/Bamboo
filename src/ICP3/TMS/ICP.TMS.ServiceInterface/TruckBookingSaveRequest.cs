using System;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.TMS.ServiceInterface
{

    /// <summary>
    /// 拖车业务信息保存实体
    /// </summary>
    [Serializable]
    public class TruckBookingSaveRequest : ICP.Framework.CommonLibrary.Common.SaveRequest 
    {
        #region ID
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID
        {
            get;
            set;
        }
        #endregion

        #region NO
        /// <summary>
        /// No
        /// </summary>
        public String No
        {
            get;
            set;
        }
        #endregion

        #region 业务类型
        /// <summary>
        /// 业务类型
        /// </summary>
        public TruckBookingType TruckType
        {
            get;
            set;
        }
        #endregion

        #region 公司ID
        /// <summary>
        /// 公司ID
        /// </summary>
        public Guid CompanyID
        {
            get;
            set;
        }
        #endregion

        #region 客户ID
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid CustomerID
        {
            get;
            set;
        }
        #endregion

        #region 客户参考号
        /// <summary>
        /// 客户参考号
        /// </summary>
        public string CustomerRefNo
        {
            get;
            set;
        }
        #endregion

        #region 提单号
        /// <summary>
        /// 提单号
        /// </summary>
        public string MBLNo
        {
            get;
            set;
        }

        #endregion

        #region 揽货人ID
        /// <summary>
        /// 揽货人ID
        /// </summary>
        public Guid SalesID
        {
            get;
            set;            
        }
        #endregion

        #region 揽货方式ID
        /// <summary>
        /// 揽货方式
        /// </summary>
        public Guid SalesTypeID
        {
            get;
            set;
        }
        #endregion

        #region 委托方式
        /// <summary>
        /// 委托方式
        /// </summary>
        public BookingMode Bookingmode
        {
            get;
            set;
        }
        #endregion

        #region 委托日期
        /// <summary>
        /// 委托日期
        /// </summary>
        public DateTime BookingDate
        {
            get;
            set;

        }
        #endregion

        #region 船东ID
        /// <summary>
        /// 船东ID
        /// </summary>
        public Guid? CarrierID
        {
            get;
            set;

        }
        #endregion

        #region 船名
        /// <summary>
        /// 船名
        /// </summary>
        public string VesselName
        {
            get;
            set;
        }
        #endregion

        #region 航次
        /// <summary>
        /// 航次
        /// </summary>
        public string VoyageNo
        {
            get;
            set;
        }
        #endregion

        #region 箱信息(集装箱描述)

        /// <summary>
        /// 箱信息(集装箱描述)
        /// </summary>
        public ContainerDescription ContainerDescription
        {
            get;
            set;
        }

        #endregion

        #region 备注
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get;
            set;
        }
        #endregion

        #region 提货地ID
        /// <summary>
        /// 提货地ID
        /// </summary>
        public Guid? PickUpAtID
        {
            get;
            set;
        }
        #endregion

        #region 提柜地描述
        /// <summary>
        /// 提柜地描述
        /// </summary>
        public CustomerDescription PickUpAtDescription
        {
            get;
            set;
        }
        #endregion

        #region 提柜时间
        /// <summary>
        /// 提柜时间
        /// </summary>
        public DateTime? PickUpAtDate
        {
            get;
            set;
        }
        #endregion

        #region 交货地ID
        /// <summary>
        /// 交货地ID
        /// </summary>
        public Guid? DeliveryAtID
        {
            get;
            set;
        }
        #endregion

        #region 交货地描述
        /// <summary>
        /// 提柜地描述
        /// </summary>
        public CustomerDescription DeliveryAtDescription
        {
            get;
            set;
        }
        #endregion

        #region 交货时间
        /// <summary>
        /// 交货时间
        /// </summary>
        public DateTime? DeliveryDate
        {
            get;
            set;
        }
        #endregion

        #region 还柜地ID
        /// <summary>
        /// 还柜地ID
        /// </summary>
        public Guid? ReturnLocationID
        {
            get;
            set;
        }
        #endregion

        #region 还柜地描述
        /// <summary>
        /// 还柜地描述
        /// </summary>
        public CustomerDescription ReturnLocationDescription
        {
            get;
            set;
        }
        #endregion

        #region 保存人ID
        /// <summary>
        /// 保存人ID
        /// </summary>
        public Guid SaveByID
        {
            get;
            set;
        }

        #endregion

        #region 最后更新时间
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime? UpdateDate
        {
            get;
            set;
        }

        #endregion

        public bool IsEnglish
        {
            get;
            set;
        }

    }

    /// <summary>
    /// 拖车业务派车信息保存实体
    /// </summary>
    [Serializable]
    public class TruckContainersSaveRequest : ICP.Framework.CommonLibrary.Common.SaveRequest
    {
        #region ID
        /// <summary>
        /// ID
        /// </summary>
        public Guid[] IDs
        {
            get;
            set;
        }
        #endregion

        #region 业务ID
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid TruckBookingID
        {
            get;
            set;
        }
        #endregion

        #region 序号
        /// <summary>
        /// 序号
        /// </summary>
        public String[] IndexNos
        {
            get;
            set;
        }
        #endregion

        #region 状态
        /// <summary>
        /// 状态
        /// </summary>
        public TruckBusinessState[] States
        {
            get;
            set;
        }
        #endregion

        #region 箱号
        /// <summary>
        /// No
        /// </summary>
        public String[] Nos
        {
            get;
            set;
        }
        #endregion

        #region 箱型
        /// <summary>
        /// 箱型
        /// </summary>
        public Guid?[] TypeIDs
        {
            get;
            set;
        }

        #endregion

        #region 托盘号
        /// <summary>
        /// 托盘号
        /// </summary>
        public String[] TrayNos
        {
            get;
            set;
        }
        #endregion

        #region 派车日期
        /// <summary>
        /// 派车日期
        /// </summary>
        public DateTime?[] TruckDates
        {
            get;
            set;
        }
        #endregion

        #region 派车地点
        /// <summary>
        /// 派车地点
        /// </summary>
        public String[] TruckPlaces
        {
            get;
            set;
        }
        #endregion

        #region 免堆日
        /// <summary>
        /// 派车日期
        /// </summary>
        public DateTime?[] LastFreeDates
        {
            get;
            set;
        }
        #endregion

        #region 提柜日期
        /// <summary>
        /// 提柜日期
        /// </summary>
        public DateTime?[] PickUpAtDates
        {
            get;
            set;
        }
        #endregion

        #region 交货日期
        /// <summary>
        /// 交货日期
        /// </summary>
        public DateTime?[] DeliveryDates
        {
            get;
            set;
        }
        #endregion

        #region 还柜日
        /// <summary>
        /// 还柜日
        /// </summary>
        public DateTime?[] ReturnDates
        {
            get;
            set;
        }
        #endregion

        #region 司机ID
        /// <summary>
        /// 司机ID
        /// </summary>
        public Guid?[] DriverIDs
        {
            get;
            set;
        }
        #endregion

        #region 车辆
        /// <summary>
        /// 车辆ID
        /// </summary>
        public Guid?[] CarIDs
        {
            get;
            set;
        }
        #endregion

        #region 备注
        /// <summary>
        /// 备注
        /// </summary>
        public String[] Remarks
        {
            get;
            set;
        }
        #endregion

        #region 保存人ID
        /// <summary>
        /// 创建人ID
        /// </summary>
        public Guid SaveByID
        {
            get;
            set;
        }

        #endregion

        #region 最后更新时间
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime?[] UpdateDates
        {
            get;
            set;
        }

        #endregion


        public bool IsEnglish { get; set; }
    }

}
