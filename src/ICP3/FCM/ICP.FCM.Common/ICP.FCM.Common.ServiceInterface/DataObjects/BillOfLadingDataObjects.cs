#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/7/10 星期二 15:32:06
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using System;

namespace ICP.FCM.Common.ServiceInterface
{
    /// <summary>
    /// 提单信息(Bill Of Lading)
    /// </summary>
    [Serializable]
    public class BillOfLadingList : BaseDataObject
    {
        #region 提单ID(唯一键)
        Guid _ID;
        /// <summary>
        /// 提单ID(唯一键)
        /// </summary>
        [GuidRequired(CMessage = "提单唯一键", EMessage = "B/L Guid")]
        public Guid ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    base.OnPropertyChanged("ID", value);
                }
            }
        }

        #endregion

        #region 提单号
        string _No;
        /// <summary>
        /// 提单号
        /// </summary>
        [StringLength(CMessage = "提单号", EMessage = "B/L NO.")]
        public string No
        {
            get
            {
                return _No;
            }
            set
            {
                if (_No != value)
                {
                    _No = value;
                    base.OnPropertyChanged("No", value);
                }
            }
        }
        #endregion

        #region  主提单ID
        Guid _ParentID;
        /// <summary>
        /// 主提单ID
        /// </summary>
        [GuidRequired(CMessage = "主提单唯一键", EMessage = "Master B/L Guid")]
        public Guid ParentID
        {
            get
            {
                return _ParentID;
            }
            set
            {
                if (_ParentID != value)
                {
                    _ParentID = value;
                    base.OnPropertyChanged("ParentID", value);
                }
            }
        }

        #endregion

        #region 业务ID
        Guid _OperationID;
        /// <summary>
        /// 业务ID
        /// </summary>
        [GuidRequired(CMessage = "业务唯一键", EMessage = "Operation Guid")]
        public Guid OperationID
        {
            get
            {
                return _OperationID;
            }
            set
            {
                if (_OperationID != value)
                {
                    _OperationID = value;
                    base.OnPropertyChanged("OperationID", value);
                }
            }
        }
        #endregion

        #region 业务号
        string _OperationNo;
        /// <summary>
        /// 业务号
        /// </summary>
        [StringLength(CMessage ="业务号",EMessage ="Operation NO.")]
        public string OperationNo
        {
            get
            {
                return _OperationNo;
            }
            set
            {
                if (_OperationNo != value)
                {
                    _OperationNo = value;
                    base.OnPropertyChanged("OperationNo", value);
                }
            }
        }
        #endregion

        #region 海运提单类型
        BillOfLadingType _BLType=BillOfLadingType.Unknown;
        /// <summary>
        /// 海运提单类型
        /// </summary>
        [Required(CMessage = "提单类型", EMessage = "Bill Of Lading Type")]
        public BillOfLadingType BLType
        {
            get
            {
                return _BLType;
            }
            set
            {
                if (_BLType != value)
                {
                    _BLType = value;
                    base.OnPropertyChanged("BLType", value);
                }
            }
        }

        /// <summary>
        /// 海运提单类型名(只读,根据状态和中英文环境返回字串)
        /// </summary>
        public string BLTypeName
        {
            get
            {
                return EnumHelper.GetDescription(BLType, true);
            }
        }
        #endregion

        #region 发货人
        Guid _shipperid;
        /// <summary>
        /// 发货人ID
        /// </summary>
        [GuidRequired(CMessage = "发货人唯一键", EMessage = "Shipper Guid")]
        public Guid ShipperID
        {
            get
            {
                return _shipperid;
            }
            set
            {
                if (_shipperid != value)
                {
                    _shipperid = value;
                    base.OnPropertyChanged("ShipperID", value);
                }
            }
        }
        string _shippername;
        /// <summary>
        /// 发货人
        /// </summary>
        [StringLength(CMessage = "发货人", EMessage = "Shipper Name", MaximumLength =500)]
        public string ShipperName
        {
            get
            {
                return _shippername;
            }
            set
            {
                if (_shippername != value)
                {
                    _shippername = value;
                    base.OnPropertyChanged("ShipperName", value);
                }
            }
        }

        #endregion

        #region 收货人
        Guid _ConsigneeID;
        /// <summary>
        /// 收货人
        /// </summary>
        [GuidRequired(CMessage = "收货人唯一键", EMessage = "Consignee Guid")]
        public Guid ConsigneeID
        {
            get
            {
                return _ConsigneeID;
            }
            set
            {
                if (_ConsigneeID != value)
                {
                    _ConsigneeID = value;
                    base.OnPropertyChanged("ConsigneeID", value);
                }
            }
        }
        string _ConsigneeName;
        /// <summary>
        /// 收货人
        /// </summary>
        [StringLength(CMessage = "收货人", EMessage = "Consignee Name", MaximumLength =500)]
        public string ConsigneeName
        {
            get
            {
                return _ConsigneeName;
            }
            set
            {
                if (_ConsigneeName != value)
                {
                    _ConsigneeName = value;
                    base.OnPropertyChanged("ConsigneeName", value);
                }
            }
        }

        #endregion

        #region 通知人
        Guid _NotifyPartyID;
        /// <summary>
        /// 通知人
        /// </summary>
        [GuidRequired(CMessage = "通知人唯一键", EMessage = "Notify Party Guid")]
        public Guid NotifyPartyID
        {
            get
            {
                return _NotifyPartyID;
            }
            set
            {
                if (_NotifyPartyID != value)
                {
                    _NotifyPartyID = value;
                    base.OnPropertyChanged("NotifyPartyID", value);
                }
            }
        }
        /// <summary>
        /// 通知人
        /// </summary>
        [StringLength(CMessage = "通知人", EMessage = "Notify Party Name", MaximumLength =500)]
        public string NotifyPartyName { get; set; }
        #endregion

        #region 放单状态
        OceanReleaseState _ReleaseState = OceanReleaseState.Unknown;
        /// <summary>
        /// 放单状态
        /// </summary>
        public OceanReleaseState ReleaseState
        {
            get
            {
                return _ReleaseState;
            }
            set
            {
                if (_ReleaseState != value)
                {
                    _ReleaseState = value;
                    base.OnPropertyChanged("ReleaseState", value);
                }
            }
        }
        #endregion

        #region 放单状态名称
        /// <summary>
        /// 放单状态(只读,根据电放类型和中英文环境返回字串)
        /// </summary>
        public string ReleaseStateName
        {
            get
            {
                return EnumHelper.GetDescription(ReleaseState, ApplicationContext.Current.IsEnglish);
            }
        }
        #endregion

        #region 放单类型
        OceanReleaseType _ReleaseType = OceanReleaseType.Unknown;
        /// <summary>
        /// 放单类型
        /// </summary>
        public OceanReleaseType ReleaseType
        {
            get
            {
                return _ReleaseType;
            }
            set
            {
                if (_ReleaseType != value)
                {
                    _ReleaseType = value;
                    base.OnPropertyChanged("ReleaseType", value);
                }
            }
        }
        #endregion

        #region 放单类型名称
        /// <summary>
        /// 放单类型(只读,根据电放类型和中英文环境返回字串)
        /// </summary>
        public string ReleaseTypeName
        {
            get
            {
                return EnumHelper.GetDescription(ReleaseType, ApplicationContext.Current.IsEnglish);
            }
        }
        #endregion

        #region 电放号
        string _TelexNo;
        /// <summary>
        /// 电放号
        /// </summary>
        public string TelexNo
        {
            get
            {
                return _TelexNo;
            }
            set
            {
                if (_TelexNo != value)
                {
                    _TelexNo = value;
                    base.OnPropertyChanged("TelexNo", value);
                }
            }
        }
        #endregion

        #region 已发送BL Copy让客户确认
        /// <summary>
        /// 已发送BL Copy让客户确认
        /// </summary>
        [Required(CMessage = "已发送BL Copy让客户确认", EMessage = "Customer has confirmed BL")]
        public bool BLCFM { get; set; }
        #endregion

        #region 已向承运人补料
        /// <summary>
        /// 已向承运人补料
        /// </summary>
        [Required(CMessage = "已向承运人补料", EMessage = "SI is sent to the carrier")]
        public bool MBLD { get; set; }
        #endregion

        #region 申请放单
        /// <summary>
        /// 申请放单
        /// </summary>
        [Required(CMessage = "申请放单", EMessage = "Applied release BL")]
        public bool RBLA { get; set; }
        #endregion

        #region 发生HOLD货
        /// <summary>
        /// 发生HOLD货
        /// </summary>
        [Required(CMessage = "发生HOLD货", EMessage = "Release BL is holding")]
        public bool RBLH { get; set; }
        #endregion

        #region 已放单
        /// <summary>
        /// 已放单
        /// </summary>
        [Required(CMessage = "已放单", EMessage = "Is released")]
        public bool RBLD { get; set; }
        #endregion

        #region 代理已收到放单通知
        /// <summary>
        ///  代理已收到放单通知
        /// </summary>
        [Required(CMessage = "代理已收到放单通知", EMessage = "CS/Agent has received the notice of the releasing BL.")]
        public bool RBLRcv { get; set; }
        #endregion

        #region BL 已放货
        /// <summary>
        ///  BL 已放货
        /// </summary>
        [Required(CMessage = "BL 已放货", EMessage = "The cargo of BL is released")]
        public bool BLRC { get; set; }
        #endregion

        #region AMS
        /// <summary>
        /// 美国反恐舱单系统(Automated Manifest System)
        /// </summary>
        [Required(CMessage = "AMS已完成", EMessage = "AMS is done")]
        public bool AMS { get; set; }
        #endregion

        #region ISF
        /// <summary>
        /// 进口安全申报(Importer Security Filing)
        /// </summary>
        [Required(CMessage = "进口安全申报已完成", EMessage = "ISF is done")]
        public bool ISF { get; set; }
        #endregion

        #region  操作口岸ID(唯一键)
        Guid _CompanyID;
        /// <summary>
        /// 操作口岸ID(唯一键)
        /// </summary>
        [GuidRequired(CMessage = "操作口岸唯一键", EMessage = "Operation Company Guid")]
        public Guid CompanyID
        {
            get
            {
                return _CompanyID;
            }
            set
            {
                if (_CompanyID != value)
                {
                    _CompanyID = value;
                    base.OnPropertyChanged("CompanyID", value);
                }
            }
        }
        #endregion
    }
}
