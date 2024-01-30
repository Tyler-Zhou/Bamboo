using System;
using System.Collections.Generic;
using System.Text;
using ICP.Framework.CommonLibrary.Common;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using ICP.Framework.CommonLibrary.Helper;
using DevExpress.XtraEditors.DXErrorProvider;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.TMS.ServiceInterface
{
    #region 拖车列表
    /// <summary>
    /// 拖车列表数据
    /// </summary>
    [Serializable]
    public class TruckBookingsList : BaseDataObject
    {
        private Guid id;
        /// <summary>
        /// 树状结构显示用
        /// </summary>
        public Guid ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid BookingID
        {
            get;
            set;
        }
        /// <summary>
        /// 派车ID
        /// </summary>
        public Guid? ContainerID
        {
            get;
            set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public TruckBusinessState State
        {
            get;
            set;
        }

        private string stateDescription;
        public string StateDescription
        {
            get
            {
                if (string.IsNullOrEmpty(stateDescription))
                {
                    try
                    {
                        stateDescription = EnumHelper.GetDescription<TruckBusinessState>(State, ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish);
                    }
                    catch
                    {
                        stateDescription = string.Empty;
                    }
                }
                return stateDescription;
            }
            set
            {
                stateDescription = value;
            }
        }
        /// <summary>
        /// 业务号
        /// </summary>
        public string No
        {
            get;
            set;
        }
        /// <summary>
        /// 业务类型
        /// </summary>
        public TruckBookingType Type
        {
            get;
            set;
        }

        private string typeDescription;
        public string TypeDescription
        {
            get
            {
                if (string.IsNullOrEmpty(typeDescription))
                {
                    try
                    {
                        typeDescription = EnumHelper.GetDescription<TruckBookingType>(Type, ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish);
                    }
                    catch
                    {
                        typeDescription = string.Empty;
                    }
                }
                return typeDescription;
            }
            set
            {
                typeDescription = value;
            }
        }

        /// <summary>
        /// 箱号
        /// </summary>
        public string ContainerNo
        {
            get;
            set;
        }
        /// <summary>
        /// 箱型
        /// </summary>
        public string ContainerType
        {
            get;
            set;
        }
        /// <summary>
        /// 托盘号
        /// </summary>
        public string TrayNo
        {
            get;
            set;
        }
        /// <summary>
        /// 派车日期
        /// </summary>
        public DateTime? TruckDate
        {
            get;
            set;
        }
        /// <summary>
        /// 派车地点
        /// </summary>
        public string TruckPlace
        {
            get;
            set;
        }
        /// <summary>
        /// 免堆日
        /// </summary>
        public DateTime? LastFreeDate
        {
            get;
            set;
        }
        /// <summary>
        /// 司机名称
        /// </summary>
        public string DriverName
        {
            get;
            set;
        }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string TruckNo
        {
            get;
            set;
        }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName
        {
            get;
            set;
        }
        /// <summary>
        /// 客户参考号
        /// </summary>
        public string CustomerRefNo
        {
            get;
            set;
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateByName
        {
            get;
            set;
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate
        {
            get;
            set;
        }
        /// <summary>
        /// 箱最后更新时间
        /// </summary>
        public DateTime? ContainerUpdateDate
        {
            get;
            set;
        }
        /// <summary>
        /// 船东提单号
        /// </summary>
        public string MBLNo
        {
            get;
            set;
        }
        /// <summary>
        /// 业务最后更新时间
        /// </summary>
        public DateTime? BookingUpdateDate
        {
            get;
            set;
        }

        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdateByName
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get;
            set;
        }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid
        {
            get;
            set;
        }
        /// <summary>
        /// 编辑类型
        /// </summary>
        public TruckBookingEditType EditType
        {
            get;
            set;
        }


        public EditMode TruckEditMode
        {
            get;
            set;
        }



    }

    #endregion

    #region 下载海运业务列表
    /// <summary>
    /// 下载海运业务列表
    /// </summary>
    [Serializable]
    public class DownLoadOceanBusinessList
    {
        /// <summary>
        /// 选择
        /// </summary> 
        public bool IsSelect { get; set; }

        /// <summary>
        /// OperationID
        /// </summary>
        public Guid ID { get; set; }

        ///// <summary>
        ///// 操作口岸ID
        ///// </summary>
        //public Guid CompanyID { get; set; }

        /// <summary>
        /// 拖车业务号
        /// </summary>
        public String RefNo { get; set; }

        /// <summary>
        /// 下载状态
        /// </summary>
        public DownLoadState DownloadState { get; set; }
        /// <summary>
        /// 状态描述
        /// </summary>
        public string StateDescription
        {
            get
            {
                try
                {
                    return EnumHelper.GetDescription<DownLoadState>(DownloadState, ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish);
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 业务类型
        /// </summary>
        public TruckBookingType BusinessType { get; set; }
        /// <summary>
        /// 类型描述
        /// </summary>
        public string TypeDescripttion
        {
            get
            {
                try
                {
                    return EnumHelper.GetDescription<TruckBookingType>(BusinessType, ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish);
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 客户名称
        /// </summary>
        public String CustomerName { get; set; }
        /// <summary>
        /// 客户参考号(业务号)
        /// </summary>
        public String CustomerRefNo { get; set; }

        /// <summary>
        /// 箱号
        /// </summary>
        public String ContainerNo { get; set; }
        /// <summary>
        /// 港口名称
        /// </summary>
        public String PortName { get; set; }
        /// <summary>
        /// 港口日期
        /// </summary>
        public DateTime? PortDate { get; set; }
        /// <summary>
        /// 船名航次
        /// </summary>
        public String VesselVoyage { get; set; }

    }
    #endregion

    #region 拖车业务信息
    /// <summary>
    /// 派车业务详细信息
    /// </summary>
    [Serializable]
    public class TruckBookingsInfo : BaseDataObject
    {
        #region ID
        private Guid id;
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID
        {
            get
            {
                return id;
            }
            set
            {
                if (id != value)
                {
                    id = value;
                    this.NotifyPropertyChanged(o => o.ID);
                }
            }
        }
        #endregion

        #region NO
        private String no;
        /// <summary>
        /// No
        /// </summary>
        public String No
        {
            get
            {
                return no;
            }
            set
            {
                if (no != value)
                {
                    no = value;
                    this.NotifyPropertyChanged(o => o.No);
                }
            }
        }
        #endregion

        #region 业务类型
        private TruckBookingType truckType;
        /// <summary>
        /// 业务类型
        /// </summary>
        public TruckBookingType TruckType
        {
            get
            {
                return truckType;
            }
            set
            {
                if (truckType != value)
                {
                    truckType = value;
                    this.NotifyPropertyChanged(o => o.TruckType);
                }
            }
        }
        #endregion

        #region 公司ID
        private Guid companyID;
        /// <summary>
        /// 公司ID
        /// </summary>
        public Guid CompanyID
        {
            get
            {
                return companyID;
            }
            set
            {
                if (companyID != value)
                {
                    companyID = value;
                    this.NotifyPropertyChanged(o => o.CompanyID);
                }
            }
        }
        #endregion

        #region 公司名称
        private string companyName;
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            get
            {
                return companyName;
            }
            set
            {
                if (companyName != value)
                {
                    companyName = value;
                    this.NotifyPropertyChanged(o => o.CompanyName);
                }
            }
        }
        #endregion

        #region 客户ID
        private Guid customerID;
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid CustomerID
        {
            get
            {
                return customerID;
            }
            set
            {
                if (customerID != value)
                {
                    customerID = value;
                    this.NotifyPropertyChanged(o => o.CustomerID);
                }
            }
        }
        #endregion

        #region 客户名称
        private string customerName;
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName
        {
            get
            {
                return customerName;
            }
            set
            {
                if (customerName != value)
                {
                    customerName = value;
                    this.NotifyPropertyChanged(o => o.CustomerName);
                }
            }
        }
        #endregion

        #region 客户参考号
        private string customerRefNo;
        /// <summary>
        /// 客户参考号
        /// </summary>
        public string CustomerRefNo
        {
            get
            {
                return customerRefNo;
            }
            set
            {
                if (customerRefNo != value)
                {
                    customerRefNo = value;
                    this.NotifyPropertyChanged(o => o.CustomerRefNo);
                }
            }
        }
        #endregion

        #region 提单号
        private string mblNo;
        /// <summary>
        /// 提单号
        /// </summary>
        public string MBLNo
        {
            get
            {
                return mblNo;
            }
            set
            {
                if (mblNo != value)
                {
                    mblNo = value;
                    this.NotifyPropertyChanged(o => o.MBLNo);
                }
            }
        }

        #endregion

        #region 揽货人ID
        private Guid salesID;
        /// <summary>
        /// 揽货人ID
        /// </summary>
        public Guid SalesID
        {
            get
            {
                return salesID;
            }
            set
            {
                if (salesID != value)
                {
                    salesID = value;
                    this.NotifyPropertyChanged(o => o.SalesID);
                }
            }
        }
        #endregion

        #region 揽货人名称
        private string salesName;
        /// <summary>
        /// 公司名称
        /// </summary>
        public string SalesName
        {
            get
            {
                return salesName;
            }
            set
            {
                if (salesName != value)
                {
                    salesName = value;
                    this.NotifyPropertyChanged(o => o.SalesName);
                }
            }
        }
        #endregion

        #region 揽货方式ID
        private Guid salesTypeID;
        /// <summary>
        /// 揽货方式
        /// </summary>
        public Guid SalesTypeID
        {
            get
            {
                return salesTypeID;
            }
            set
            {
                if (salesTypeID != value)
                {
                    salesTypeID = value;
                    this.NotifyPropertyChanged(o => o.SalesTypeID);
                }
            }
        }
        #endregion

        #region 揽货方式名称
        private String salesTypeName;
        /// <summary>
        /// 揽货方式名称
        /// </summary>
        public String SalesTypeName
        {
            get
            {
                return salesTypeName;
            }
            set
            {
                if (salesTypeName != value)
                {
                    salesTypeName = value;
                    this.NotifyPropertyChanged(o => o.SalesTypeName);
                }
            }
        }
        #endregion

        #region 委托方式
        private BookingMode bookingMode;
        /// <summary>
        /// 委托方式
        /// </summary>
        public BookingMode Bookingmode
        {
            get
            {
                return bookingMode;
            }
            set
            {
                if (bookingMode != value)
                {
                    bookingMode = value;
                    this.NotifyPropertyChanged(o => o.Bookingmode);
                }
            }
        }
        #endregion

        #region 委托日期
        private DateTime bookingDate;
        /// <summary>
        /// 委托日期
        /// </summary>
        public DateTime BookingDate
        {
            get
            {
                return bookingDate;
            }
            set
            {
                if (bookingDate != value)
                {
                    bookingDate = value;
                    this.NotifyPropertyChanged(o => o.BookingDate);
                }
            }

        }
        #endregion

        #region 船东ID
        private Guid? carrierID;
        /// <summary>
        /// 船东ID
        /// </summary>
        public Guid? CarrierID
        {
            get
            {
                return carrierID;
            }
            set
            {
                if (carrierID != value)
                {
                    carrierID = value;
                    this.NotifyPropertyChanged(o => o.CarrierID);
                }
            }


        }
        #endregion

        #region 船东名称
        private String carrierName;
        /// <summary>
        /// 船东名称
        /// </summary>
        public String CarrierName
        {
            get
            {
                return carrierName;
            }
            set
            {
                if (carrierName != value)
                {
                    carrierName = value;
                    this.NotifyPropertyChanged(o => o.CarrierName);
                }
            }


        }
        #endregion

        #region 船名
        private string vesselName;
        /// <summary>
        /// 船名
        /// </summary>
        public string VesselName
        {
            get
            {
                return vesselName;
            }
            set
            {
                if (vesselName != value)
                {
                    vesselName = value;
                    this.NotifyPropertyChanged(o => o.VesselName);
                }
            }
        }
        #endregion

        #region 航次
        private string voyageNo;
        /// <summary>
        /// 航次
        /// </summary>
        public string VoyageNo
        {
            get
            {
                return voyageNo;
            }
            set
            {
                if (voyageNo != value)
                {
                    voyageNo = value;
                    this.NotifyPropertyChanged(o => o.VoyageNo);
                }
            }
        }
        #endregion

        #region 箱信息(集装箱描述)

        ContainerDescription _containerdescription;
        /// <summary>
        /// 箱信息(集装箱描述)
        /// </summary>
        public ContainerDescription ContainerDescription
        {
            get
            {
                return _containerdescription;
            }
            set
            {
                if (_containerdescription != value)
                {
                    _containerdescription = value;
                    this.NotifyPropertyChanged(o => o.ContainerDescription);
                }
            }
        }

        #endregion

        #region 备注
        private string remark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get
            {
                return remark;
            }
            set
            {
                if (remark != value)
                {
                    remark = value;
                    this.NotifyPropertyChanged(o => o.Remark);
                }
            }
        }
        #endregion

        #region 提货地ID
        private Guid? pickUpAtID;
        /// <summary>
        /// 提货地ID
        /// </summary>
        public Guid? PickUpAtID
        {
            get
            {
                return pickUpAtID;
            }
            set
            {
                if (pickUpAtID != value)
                {
                    pickUpAtID = value;
                    this.NotifyPropertyChanged(o => o.PickUpAtID);
                }
            }
        }
        #endregion

        #region 提柜地名称
        private String pickUpAtName;
        /// <summary>
        /// 提柜地名称
        /// </summary>
        public String PickUpAtName
        {
            get
            {
                return pickUpAtName;
            }
            set
            {
                if (pickUpAtName != value)
                {
                    pickUpAtName = value;
                    this.NotifyPropertyChanged(o => o.PickUpAtName);
                }
            }
        }
        #endregion

        #region 提柜地描述
        CustomerDescription pickUpAtDescription;
        /// <summary>
        /// 提柜地描述
        /// </summary>
        public CustomerDescription PickUpAtDescription
        {
            get
            {
                return pickUpAtDescription;
            }
            set
            {
                if (pickUpAtDescription != value)
                {
                    pickUpAtDescription = value;
                    this.NotifyPropertyChanged(o => o.PickUpAtDescription);
                }
            }
        }
        #endregion

        #region 提柜时间
        private DateTime? pickUpAtDate;
        /// <summary>
        /// 提柜时间
        /// </summary>
        public DateTime? PickUpAtDate
        {
            get
            {
                return pickUpAtDate;
            }
            set
            {
                if (pickUpAtDate != value)
                {
                    pickUpAtDate = value;
                    this.NotifyPropertyChanged(o => o.PickUpAtDate);
                }
            }
        }
        #endregion

        #region 交货地ID
        private Guid? deliveryAtID;
        /// <summary>
        /// 交货地ID
        /// </summary>
        public Guid? DeliveryAtID
        {
            get
            {
                return deliveryAtID;
            }
            set
            {
                if (deliveryAtID != value)
                {
                    deliveryAtID = value;
                    this.NotifyPropertyChanged(o => o.DeliveryAtID);
                }
            }
        }
        #endregion

        #region 交货地名称
        private String deliveryAtName;
        /// <summary>
        /// 交货地名称
        /// </summary>
        public String DeliveryAtName
        {
            get
            {
                return deliveryAtName;
            }
            set
            {
                if (deliveryAtName != value)
                {
                    deliveryAtName = value;
                    this.NotifyPropertyChanged(o => o.DeliveryAtName);
                }
            }
        }
        #endregion

        #region 交货地描述
        CustomerDescription deliveryAtDescription;
        /// <summary>
        /// 提柜地描述
        /// </summary>
        public CustomerDescription DeliveryAtDescription
        {
            get
            {
                return deliveryAtDescription;
            }
            set
            {
                if (deliveryAtDescription != value)
                {
                    deliveryAtDescription = value;
                    this.NotifyPropertyChanged(o => o.DeliveryAtDescription);
                }
            }
        }
        #endregion

        #region 交货时间
        private DateTime? deliveryDate;
        /// <summary>
        /// 交货时间
        /// </summary>
        public DateTime? DeliveryDate
        {
            get
            {
                return deliveryDate;
            }
            set
            {
                if (deliveryDate != value)
                {
                    deliveryDate = value;
                    this.NotifyPropertyChanged(o => o.DeliveryDate);
                }
            }
        }
        #endregion

        #region 还柜地ID
        private Guid? returnLocationID;
        /// <summary>
        /// 还柜地ID
        /// </summary>
        public Guid? ReturnLocationID
        {
            get
            {
                return returnLocationID;
            }
            set
            {
                if (returnLocationID != value)
                {
                    returnLocationID = value;
                    this.NotifyPropertyChanged(o => o.ReturnLocationID);
                }
            }
        }
        #endregion

        #region 还柜地名称
        private String returnLocationName;
        /// <summary>
        /// 还柜地名称
        /// </summary>
        public String ReturnLocationName
        {
            get
            {
                return returnLocationName;
            }
            set
            {
                if (returnLocationName != value)
                {
                    returnLocationName = value;
                    this.NotifyPropertyChanged(o => o.ReturnLocationName);
                }
            }
        }
        #endregion

        #region 还柜地描述
        CustomerDescription returnLocationDescription;
        /// <summary>
        /// 还柜地描述
        /// </summary>
        public CustomerDescription ReturnLocationDescription
        {
            get
            {
                return returnLocationDescription;
            }
            set
            {
                if (returnLocationDescription != value)
                {
                    returnLocationDescription = value;
                    this.NotifyPropertyChanged(o => o.ReturnLocationDescription);
                }
            }
        }
        #endregion

        #region 创建人ID
        /// <summary>
        /// 创建人ID
        /// </summary>
        public Guid CreateByID
        {
            get;
            set;
        }

        #endregion

        #region 创建人名称
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreateByName
        {
            get;
            set;
        }

        #endregion

        #region 创建时间
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateByDate
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

        #region 拖车列表
        public List<TruckContainersList> TruckContainersList
        {
            get;
            set;
        }
        #endregion

        public bool IsValid
        {
            get;
            set;
        }
    }

    #endregion

    #region 拖车业务派车信息
    /// <summary>
    /// 拖车业务派车列表
    /// </summary>
    [Serializable]
    public partial class TruckContainersList : BaseDataObject
    {
        #region ID
        private Guid id;
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID
        {
            get
            {
                return id;
            }
            set
            {
                if (id != value)
                {
                    id = value;
                    this.NotifyPropertyChanged(o => o.ID);
                }
            }

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
        private String indexNo;
        /// <summary>
        /// 序号
        /// </summary>
        public String IndexNo
        {
            get
            {
                return indexNo;
            }
            set
            {
                if (indexNo != value)
                {
                    indexNo = value;
                    this.NotifyPropertyChanged(o => o.IndexNo);
                }
            }
        }
        #endregion

        #region 状态
        private TruckBusinessState state;
        /// <summary>
        /// 状态
        /// </summary>
        public TruckBusinessState State
        {
            get
            {
                return state;
            }
            set
            {
                if (state != value)
                {
                    state = value;
                    this.NotifyPropertyChanged(o => o.State);
                }
            }
        }
        #endregion

        #region 箱号
        private String no;
        /// <summary>
        /// No
        /// </summary>
        public String No
        {
            get
            {
                return no;
            }
            set
            {
                if (no != value)
                {
                    no = value;
                    this.NotifyPropertyChanged(o => o.No);
                }
            }
        }
        #endregion

        #region 箱型
        private Guid typeID;
        /// <summary>
        /// 箱型
        /// </summary>
        public Guid TypeID
        {
            get
            {
                return typeID;
            }
            set
            {
                if (typeID != value)
                {
                    typeID = value;
                    this.NotifyPropertyChanged(o => o.TypeID);
                }
            }
        }
        /// <summary>
        /// 箱型名称
        /// </summary>
        public String ContainerTypeName
        {
            get;
            set;
        }
        #endregion

        #region 托盘号
        private string trayNo;
        /// <summary>
        /// 托盘号
        /// </summary>
        public string TrayNo
        {
            get
            {
                return trayNo;
            }
            set
            {
                if (trayNo != value)
                {
                    trayNo = value;
                    this.NotifyPropertyChanged(o => o.TrayNo);
                }
            }
        }
        #endregion

        #region 派车日期
        private DateTime? truckDate;
        /// <summary>
        /// 派车日期
        /// </summary>
        public DateTime? TruckDate
        {
            get
            {
                return truckDate;
            }
            set
            {
                if (truckDate != value)
                {
                    truckDate = value;
                    this.NotifyPropertyChanged(o => o.TruckDate);
                }
            }
        }
        #endregion

        #region 派车地点
        private string truckPlace;
        /// <summary>
        /// 派车地点
        /// </summary>
        public string TruckPlace
        {
            get
            {
                return truckPlace;
            }
            set
            {
                if (truckPlace != value)
                {
                    truckPlace = value;
                    this.NotifyPropertyChanged(o => o.TruckPlace);
                }
            }
        }
        #endregion

        #region 免堆日
        private DateTime? tastFreeDate;
        /// <summary>
        /// 派车日期
        /// </summary>
        public DateTime? LastFreeDate
        {
            get
            {
                return tastFreeDate;
            }
            set
            {
                if (tastFreeDate != value)
                {
                    tastFreeDate = value;
                    this.NotifyPropertyChanged(o => o.LastFreeDate);
                }
            }
        }
        #endregion

        #region 提柜日期
        private DateTime? pickUpAtDate;
        /// <summary>
        /// 提柜日期
        /// </summary>
        public DateTime? PickUpAtDate
        {
            get
            {
                return pickUpAtDate;
            }
            set
            {
                if (pickUpAtDate != value)
                {
                    pickUpAtDate = value;
                    this.NotifyPropertyChanged(o => o.PickUpAtDate);
                }
            }
        }
        #endregion

        #region 交货日期
        private DateTime? deliveryDate;
        /// <summary>
        /// 交货日期
        /// </summary>
        public DateTime? DeliveryDate
        {
            get
            {
                return deliveryDate;
            }
            set
            {
                if (deliveryDate != value)
                {
                    deliveryDate = value;
                    this.NotifyPropertyChanged(o => o.DeliveryDate);
                }
            }
        }
        #endregion

        #region 还柜日
        private DateTime? returnDate;
        /// <summary>
        /// 还柜日
        /// </summary>
        public DateTime? ReturnDate
        {
            get
            {
                return returnDate;
            }
            set
            {
                if (returnDate != value)
                {
                    returnDate = value;
                    this.NotifyPropertyChanged(o => o.ReturnDate);
                }
            }
        }
        #endregion

        #region 司机ID
        private Guid? driverID;
        /// <summary>
        /// 司机ID
        /// </summary>
        public Guid? DriverID
        {
            get
            {
                return driverID;
            }
            set
            {
                if (driverID != value)
                {
                    driverID = value;
                    this.NotifyPropertyChanged(o => o.DriverID);
                }
            }
        }
        /// <summary>
        /// 司机名称
        /// </summary>
        public String DriverName
        {
            get;
            set;
        }
        #endregion

        #region 车辆
        private Guid? carID;
        /// <summary>
        /// 车辆ID
        /// </summary>
        public Guid? CarID
        {
            get
            {
                return carID;
            }
            set
            {
                if (carID != value)
                {
                    carID = value;
                    this.NotifyPropertyChanged(o => o.CarID);
                }
            }
        }
        /// <summary>
        /// 车牌号
        /// </summary>
        public String CarNo
        {
            get;
            set;
        }
        #endregion

        #region 备注
        private string remark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get
            {
                return remark;
            }
            set
            {
                if (remark != value)
                {
                    remark = value;
                    this.NotifyPropertyChanged(o => o.Remark);
                }
            }
        }
        #endregion

        #region 创建人ID
        /// <summary>
        /// 创建人ID
        /// </summary>
        public Guid CreateByID
        {
            get;
            set;
        }

        #endregion

        #region 创建时间
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateByDate
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


    }

    /// <summary>
    /// 箱列表对象
    /// </summary>
    public partial class TruckContainersList : IDXDataErrorInfo
    {
        public TruckContainersList()
        {

        }

        private string _error = "";

        #region IDXDataErrorInfo Members

        void IDXDataErrorInfo.GetPropertyError(string propertyName, ErrorInfo info)
        {
            switch (propertyName)
            {
                case "No":
                    if (string.IsNullOrEmpty(this.No))
                    {
                        string message = LocalData.IsEnglish ? "Container No Must Input." : "箱号必须填写.";
                        SetErrorInfo(info, message, ErrorType.Warning);

                        if (string.IsNullOrEmpty(_error) == false)
                        {
                            _error += System.Environment.NewLine;
                        }
                        _error += message;
                    }
                    else
                    {
                        string errorInfo = ValidateContainerHelper.CheckContainerNo(this.No);
                        if (string.IsNullOrEmpty(errorInfo) == false)
                        {
                            SetErrorInfo(info, errorInfo, ErrorType.Information);

                            if (string.IsNullOrEmpty(_error) == false)
                            {
                                _error += System.Environment.NewLine;
                            }
                            _error += errorInfo;
                        }
                    }
                    break;

                case "TypeID":
                    if (this.TypeID == System.Guid.Empty)
                    {
                        string typeMessage = LocalData.IsEnglish ? "Container Type Must Input." : "箱箱必须填写.";
                        SetErrorInfo(info, typeMessage, ErrorType.Warning);

                        if (string.IsNullOrEmpty(_error) == false)
                        {
                            _error += System.Environment.NewLine;
                        }

                        _error += typeMessage;
                    }

                    break;
            }
        }
        void IDXDataErrorInfo.GetError(ErrorInfo info) { }
        //</gridControl1>
        #endregion

        private void SetErrorInfo(ErrorInfo info, string errorText, ErrorType errorType)
        {
            info.ErrorText = errorText;
            info.ErrorType = errorType;
        }

    }



    /// <summary>
    /// 验证箱号帮助类
    /// </summary>
    public class ValidateContainerHelper
    {
        /// <summary>
        /// 核对数：用于计算机核对箱主号与顺序号记录的正确性。核对号一般于顺序号之后，用一位阿拉伯数字表示，并加方框以醒目。
        /// 核对号是由箱主代码的四位字母和顺序号的六位数字通过以下方式换算而得。具体换算步骤如下：
        /// 首先，将表示箱主代码的四位字母转化成相应的等效数字，字母和等效数字的对应关系见下表
        /// A--10;B--12;C--13;D--14;....J--20;K--21;L--23;M--24;N--25;.....T--31;U--32;V--34;W--35;X--36;Y--37;Z—38
        /// 从表中可以看出，去掉了11及其倍数的数字，这是因为后面的计算将把11作为模数。
        /// 然后，将前四位字母对应的等效数字和后面的顺序号的数字（共计10位）采用加权系数法进行计算求和。
        /// </summary>
        /// <param name="containerChar">验证字串符</param>
        /// <returns></returns>
        private static int ContainerCheckDigitCharToInt(char containerChar)
        {
            int result = Convert.ToInt32(containerChar);
            int returnResult = 0;
            if (result > 54 && result < 91)
            {
                returnResult = result - 55;
                returnResult += returnResult / 11;
            }

            return returnResult;

            //int returnVal = 0;
            //switch (containerChar)
            //{
            //    case 'A':
            //        returnVal = 10;
            //        break;
            //    case 'B':
            //        returnVal = 12;
            //        break;
            //    case 'C':
            //        returnVal = 13;
            //        break;
            //    case 'D':
            //        returnVal = 14;
            //        break;
            //    case 'E':
            //        returnVal = 15;
            //        break;
            //    case 'F':
            //        returnVal = 16;
            //        break;
            //    case 'G':
            //        returnVal = 17;
            //        break;
            //    case 'H':
            //        returnVal = 18;
            //        break;
            //    case 'I':
            //        returnVal = 19;
            //        break;
            //    case 'J':
            //        returnVal = 20;
            //        break;
            //    case 'K':
            //        returnVal = 21;
            //        break;
            //    case 'L':
            //        returnVal = 23;
            //        break;
            //    case 'M':
            //        returnVal = 24;
            //        break;
            //    case 'N':
            //        returnVal = 25;
            //        break;
            //    case 'O':
            //        returnVal = 26;
            //        break;
            //    case 'P':
            //        returnVal = 27;
            //        break;
            //    case 'Q':
            //        returnVal = 28;
            //        break;
            //    case 'R':
            //        returnVal = 29;
            //        break;
            //    case 'S':
            //        returnVal = 30;
            //        break;
            //    case 'T':
            //        returnVal = 31;
            //        break;
            //    case 'U':
            //        returnVal = 32;
            //        break;
            //    case 'V':
            //        returnVal = 34;
            //        break;
            //    case 'W':
            //        returnVal = 35;
            //        break;
            //    case 'X':
            //        returnVal = 36;
            //        break;
            //    case 'Y':
            //        returnVal = 37;
            //        break;
            //    case 'Z':
            //        returnVal = 38;
            //        break;
            //    default:
            //        returnVal = 0;
            //        break;
            //}
            //return returnVal;
        }

        /// <summary>
        /// 箱号规则验证
        /// </summary>
        /// <remarks>
        /// 标准集装箱箱号由11位编码组成，包括三个部分：
        /// 1、 第一部分由4位英文大写字母组成。前三位代码 (Owner Code) 主要说明箱主、经营人，第四位代码说明集装箱的类型(U)。列如CBHU 开头的标准集装箱是表明箱主和经营人为中远集运。
        /// 2、 第二部分由6位数字组成。是箱体注册码（Registration Code）, 用于一个集装箱箱体持有的唯一标识。
        /// 3、 第三部分为校验码（Check Digit）由前4位字母和6位数字经过校验规则运算得到，用于识别在校验时是否发生错误。即第11位数字。
        ///     (核对数：用于计算机核对箱主号与顺序号记录的正确性。核对号一般于顺序号之后，用一位阿拉伯数字表示，并加方框以醒目。核对号是由箱主代码的四位字母和顺序号的六位数字通过以下方式换算而得。具体换算步骤如下：
        ///              首先，将表示箱主代码的四位字母转化成相应的等效数字，字母和等效数字的对应关系见下表
        ///              A--10;B--12;C--13;D--14;....J--20;K--21;L--23;M--24;N--25;.....T--31;U--32;V--34;W--35;X--36;Y--37;Z—38
        ///              从表中可以看出，去掉了11及其倍数的数字，这是因为后面的计算将把11作为模数。然后，将前四位字母对应的等效数字和后面的顺序号的数字（共计10位）采用加权系数法进行计算求和。
        /// </remarks>
        /// <param name="containerNo">箱号</param>
        /// <returns></returns>
        public static string CheckContainerNo(string containerNo)
        {
            StringBuilder errorInfo = new StringBuilder();
            string message = string.Empty;
            //箱号必填规则验证
            if (string.IsNullOrEmpty(containerNo))
            {
                message = LocalData.IsEnglish ? "Container No Must Input." : "箱号必须录入.";
                errorInfo.Append(message);
                return errorInfo.ToString();
            }


            char[] noChars = containerNo.ToCharArray();
            int noLength = noChars.Length;

            //箱号长度11位规则验证
            if (noLength > 11)
            {
                message = LocalData.IsEnglish ? "Container No Length {0}Is Greater Than 11." : "箱号长度({0})大于11位.";
                errorInfo.AppendFormat(message, noLength);
                errorInfo.AppendLine();
            }
            if (noChars.Length < 11)
            {
                message = LocalData.IsEnglish ? "Container No Length {0}Is Less  Than 11." : "箱号长度({0})小于11位.";
                errorInfo.AppendFormat("箱号长度({0})小于11位.", noLength);
                errorInfo.AppendLine();
            }

            double sum = 0;     //前四位字母对应的等效数字和后面的顺序号的数字（共计10位）采用加权系数法进行计算求和


            //前三位大写字母规则验证
            for (int index = 0; index < noLength; index++)
            {
                char tempChar = noChars[index];
                if (index == 3)
                {
                    //第四位代码说明集装箱的类型(U)规则验证
                    if (tempChar != 'U')
                    {
                        errorInfo.AppendFormat("箱号第{0}位的'{1}',标准集装箱箱号第四位集装箱的类型代码应该是\"U\".", index + 1, tempChar);
                        errorInfo.AppendLine();
                    }
                }

                if (index < 4)
                {
                    //箱号前4位由4位英文大写字母,规则验证
                    if (Char.IsUpper(tempChar) == false)
                    {
                        errorInfo.AppendFormat("箱号第{0}位的'{1}',违背标准集装箱箱号前四位由四位英文大写字母的规则.", index + 1, tempChar);
                        errorInfo.AppendLine();
                    }

                    sum = sum + (ContainerCheckDigitCharToInt(tempChar) * Math.Pow(2, index));
                }
                else if (index > 3 && index < 10)
                {
                    //第二部分由6位数字组成规则验证.
                    if (Char.IsNumber(tempChar) == false)
                    {
                        if (Char.IsUpper(tempChar) == false)
                        {
                            errorInfo.AppendFormat("箱号第{0}位的'{1}',违背标准集装箱箱号第五位至第十位由数字组成的规则.", index + 1, tempChar);
                            errorInfo.AppendLine();
                        }
                    }
                    sum = sum + ((int)tempChar - 48) * Math.Pow(2, index);
                }

            }

            //第三部分为校验码（Check Digit）由前4位字母和6位数字经过校验规则运算得到，用于识别在校验时是否发生错误
            int tempComputeResult = (int)(sum % 11.0);
            if (tempComputeResult == 10)
            {
                tempComputeResult = 0;
            }

            if (noLength == 11)
            {
                int tempCheckDigit = (int)noChars[10] - 48;
                if (tempComputeResult != tempCheckDigit)
                {
                    errorInfo.AppendFormat("箱号第11位的{0},违背标准集装箱箱号校验码的规则.", noChars[10]);
                    errorInfo.AppendLine();
                }
            }

            return errorInfo.ToString();
        }
    }
    #endregion

    #region 箱描述
    /// <summary>
    /// 箱描述
    /// </summary>
    [Serializable]
    public class ContainerDescription
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ContainerDescription()
        {
            this.Containers = new List<Container>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="containerInfo">箱信息(2*20GP,3*40HQ)</param>
        public ContainerDescription(string containerInfo)
        {
            this.Containers = this.BuildContainer(containerInfo);
        }

        /// <summary>
        /// 箱列表
        /// </summary>
        [XmlElement("Container", typeof(Container))]
        public List<Container> Containers { get; set; }

        /// <summary>
        /// 转为字符串描述
        /// </summary>
        /// <returns>返回字符串描述</returns>
        public override string ToString()
        {
            StringBuilder sbValue = new StringBuilder();
            foreach (Container c in this.Containers)
            {
                if (sbValue.Length > 0)
                {
                    sbValue.Append(",");
                }

                sbValue.Append(c.ToString());
            }

            return sbValue.ToString();
        }

        /// <summary>
        /// 根据箱描述串生成箱列表
        /// </summary>
        /// <param name="containerInfo">箱信息描述</param>
        /// <returns>返回箱信息列表</returns>
        private List<Container> BuildContainer(string containerInfo)
        {
            if (string.IsNullOrEmpty(containerInfo))
            {
                return new List<Container>();
            }

            List<Container> results = new List<Container>();
            //sapce
            string sapces = @"[\x20]*";
            //Count
            string pQty = @"(?<Qty>[\d]+)";
            //Size
            string psize = @"(?<SizeB>(?<Size>20|40|45)[\']*)";
            //Type 
            string ptype = @"(?<Type>(GP|HQ|FR|HT|OT|RF|RF|TK|TF|RH|NOR))";
            //spe
            string pspae = @"[\x20\,]*";
            //
            string patten = @"[\x20]*(?<box>" + pQty + sapces + @"[xX\xD7\*]" + sapces + psize + sapces + ptype + @")" + pspae;
            //
            MatchCollection matchs = Regex.Matches(containerInfo.ToUpper(), patten, RegexOptions.IgnoreCase);

            Dictionary<string, int> containerQtyDictionary = new Dictionary<string, int>();
            Dictionary<string, string> containerSizeDictionary = new Dictionary<string, string>();
            Dictionary<string, string> containerTypeDictionary = new Dictionary<string, string>();

            foreach (Match match in matchs)
            {
                int qty = int.Parse(match.Groups["Qty"].Value);
                string size = match.Groups["SizeB"].Value;
                string type = match.Groups["Type"].Value;

                string key = string.Format("{0} {1}", size, type);
                if (containerQtyDictionary.ContainsKey(key))
                {
                    containerQtyDictionary[key] = int.Parse(containerQtyDictionary[key].ToString()) + 1;
                }
                else
                {
                    containerQtyDictionary.Add(key, qty);
                    containerSizeDictionary.Add(key, size);
                    containerTypeDictionary.Add(key, type);
                }
            }

            foreach (KeyValuePair<string, int> e in containerQtyDictionary)
            {
                Container container = new Container();
                container.Quantity = e.Value;
                container.Weight = 0;
                container.WeightUnit = string.Empty;
                container.Size = int.Parse(containerSizeDictionary[e.Key].Trim());
                container.Type = containerTypeDictionary[e.Key];
                results.Add(container);
            }
            return results;
        }


        /// <summary>
        /// 箱信息
        /// </summary>
        [Serializable]
        public class Container
        {
            /// <summary>
            /// 构造函数
            /// </summary>
            public Container()
            {
                this.Size = 0;
                this.Type = string.Empty;
                this.Quantity = 0;
                this.Weight = 0;
                this.WeightUnit = string.Empty;
            }

            /// <summary>
            /// 箱尺寸
            /// </summary>
            [XmlElement("Size")]
            public int Size { get; set; }

            /// <summary>
            /// 箱型
            /// </summary>
            [XmlElement("Type")]
            public string Type { get; set; }

            /// <summary>
            /// 数量
            /// </summary>
            [XmlElement("Quantity")]
            public int Quantity { get; set; }

            /// <summary>
            /// 重量
            /// </summary>
            [XmlElement("Weight")]
            public decimal? Weight { get; set; }

            /// <summary>
            /// 重量单位
            /// </summary>
            [XmlElement("WeightUnit")]
            public string WeightUnit { get; set; }

            /// <summary>
            /// 转为字符串描述
            /// </summary>
            /// <returns>返回字符串描述</returns>
            public override string ToString()
            {
                return this.Quantity.ToString()
                    + " * "
                    + this.Size
                    + " "
                    + this.Type;
            }
        }
    }
    #endregion


}
