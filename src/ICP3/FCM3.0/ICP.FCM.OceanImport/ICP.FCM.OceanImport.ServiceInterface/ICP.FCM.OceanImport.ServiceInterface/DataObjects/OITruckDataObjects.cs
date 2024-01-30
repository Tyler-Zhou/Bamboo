using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Common; 
using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.OceanImport.ServiceInterface
{
    #region 提货通知书
    /// <summary>
    /// 提货通知书列表
    /// </summary>
    [Serializable]
    public partial class OceanImportTruckList : BaseDataObject
    {
        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

        #region ID
        Guid _id;
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    base.OnPropertyChanged("ID", value);
                }
            }
        }
        #endregion

        #region 派车单号
        string _no;
        /// <summary>
        /// 派车单号 
        /// </summary>
        public string NO
        {
            get
            {
                return _no;
            }
            set
            {
                if (_no != value)
                {
                    _no = value;
                    base.OnPropertyChanged("NO", value);
                }
            }
        }
        #endregion

        #region 业务单ID
        Guid _oibookingid;
        /// <summary>
        /// 分提单ID
        /// </summary>
        public Guid OIBookingID
        {
            get
            {
                return _oibookingid;
            }
            set
            {
                if (_oibookingid != value)
                {
                    _oibookingid = value;
                    base.OnPropertyChanged("OIBookingID", value);
                }
            }
        }
        #endregion 

        #region 拖车公司名称

        string _truckerName;
        /// <summary>
        /// 拖车公司ID
        /// </summary>
        public string TruckerName
        {
            get
            {
                return _truckerName;
            }
            set
            {
                if (_truckerName != value)
                {
                    _truckerName = value;
                    base.OnPropertyChanged("TruckerName", value);
                }
            }
        }
        #endregion

        #region 提货地点名称
        string _pickupatName;
        /// <summary>
        /// 提货地点名称（关联客户）
        /// </summary>
        public string PickUpAtName
        {
            get
            {
                return _pickupatName;
            }
            set
            {
                if (_pickupatName != value)
                {
                    _pickupatName = value;
                    base.OnPropertyChanged("PickUpAtName", value);
                }
            }
        }
        #endregion

        #region 提货时间
        DateTime? _pickupdate;
        /// <summary>
        /// 提货时间
        /// </summary>
        public DateTime? PickUpDate
        {
            get
            {
                return _pickupdate;
            }
            set
            {
                if (_pickupdate != value)
                {
                    _pickupdate = value;
                    base.OnPropertyChanged("PickUpDate", value);
                }
            }
        }

        #endregion

        #region 交货地点
        string _deliveryatName;
        /// <summary>
        /// 交货地点ID（关联客户）
        /// </summary>
        public string DeliveryAtName
        {
            get
            {
                return _deliveryatName;
            }
            set
            {
                if (_deliveryatName != value)
                {
                    _deliveryatName = value;
                    base.OnPropertyChanged("DeliveryAtName", value);
                }
            }
        }
        #endregion

        #region 交货时间
        DateTime? _deliverydate;
        /// <summary>
        /// 交货时间
        /// </summary>
        public DateTime? DeliveryDate
        {
            get
            {
                return _deliverydate;
            }
            set
            {
                if (_deliverydate != value)
                {
                    _deliverydate = value;
                    base.OnPropertyChanged("DeliveryDate", value);
                }
            }
        }
        #endregion

        #region 创建人
        string _createbyName;
        /// <summary>
        /// 建立人
        /// </summary>
        public string CreateByName
        {
            get
            {
                return _createbyName;
            }
            set
            {
                if (_createbyName != value)
                {
                    _createbyName = value;
                    base.OnPropertyChanged("CreateByName", value);
                }
            }
        }
        #endregion

        #region 创建时间
        DateTime? _createdate;
        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime? CreateDate
        {
            get
            {
                return _createdate;
            }
            set
            {
                if (_createdate != value)
                {
                    _createdate = value;
                    base.OnPropertyChanged("CreateDate", value);
                }
            }
        }

        #endregion

    }

    #endregion


    #region 提货通知书详细信息
    /// <summary>
    /// 提货通知书详细信息
    /// </summary>
    [Serializable]
    public partial class OceanImportTruckInfo : OceanImportTruckList
    {

        #region 拖车公司

        Guid _truckerid;
        /// <summary>
        /// 拖车公司ID
        /// </summary>
        [GuidRequired(CMessage = "拖车行",EMessage="Trucker")]
        public Guid TruckerID
        {
            get
            {
                return _truckerid;
            }
            set
            {
                if (_truckerid != value)
                {
                    _truckerid = value;
                    base.OnPropertyChanged("TruckerID", value);
                }
            }
        }

        #region 拖车公司描述
        CustomerDescription _truckerDescription;
        /// <summary>
        /// 拖车公司描述
        /// </summary>
        public CustomerDescription TruckerDescription
        {
            get
            {
                return _truckerDescription;
            }
            set
            {
                if (_truckerDescription != value)
                {
                    _truckerDescription = value;
                    base.OnPropertyChanged("TruckerDescription", value);
                }
            }
        }
        #endregion

        #endregion   

        #region 提货地点
        Guid _pickupatid;
        /// <summary>
        /// 提货地点ID（关联客户）
        /// </summary>
        [GuidRequired(CMessage = "提货地",EMessage="PickUpAt")]
        public Guid PickUpAtID
        {
            get
            {
                return _pickupatid;
            }
            set
            {
                if (_pickupatid != value)
                {
                    _pickupatid = value;
                    base.OnPropertyChanged("PickUpAtID", value);
                }
            }
        }

        #region 提货地描述
        CustomerDescription _pickUpAtDescription;
        /// <summary>
        /// 提货地描述
        /// </summary>
        public CustomerDescription PickUpAtDescription
        {
            get
            {
                return _pickUpAtDescription;
            }
            set
            {
                if (_pickUpAtDescription != value)
                {
                    _pickUpAtDescription = value;
                    base.OnPropertyChanged("PickUpAtDescription", value);
                }
            }
        }
        #endregion

        #endregion

        #region 交货地点

        Guid _deliveryatid;
        /// <summary>
        /// 交货地点ID（关联客户）
        /// </summary>
        [GuidRequired(CMessage = "交货地",EMessage="DeliveryAt")]
        public Guid DeliveryAtID
        {
            get
            {
                return _deliveryatid;
            }
            set
            {
                if (_deliveryatid != value)
                {
                    _deliveryatid = value;
                    base.OnPropertyChanged("DeliveryAtID", value);
                }
            }
        }

        #region 交货地描述
        CustomerDescription _deliveryAtDescription;
        /// <summary>
        /// 交货地描述
        /// </summary>
        public CustomerDescription DeliveryAtDescription
        {
            get
            {
                return _deliveryAtDescription;
            }
            set
            {
                if (_deliveryAtDescription != value)
                {
                    _deliveryAtDescription = value;
                    base.OnPropertyChanged("DeliveryAtDescription", value);
                }
            }
        }
        #endregion

        #endregion

        #region 是否需要司机本
        bool _isdrivinglicence;
        /// <summary>
        /// 是否需要司机本
        /// </summary>
        public bool IsDrivingLicence
        {
            get
            {
                return _isdrivinglicence;
            }
            set
            {
                if (_isdrivinglicence != value)
                {
                    _isdrivinglicence = value;
                    base.OnPropertyChanged("IsDrivingLicence", value);
                }
            }
        }
        #endregion

        #region 帐单寄送

        Guid _billtoid;
        /// <summary>
        /// 账单寄送ID
        /// </summary>
        public Guid BillToID
        {
            get
            {
                return _billtoid;
            }
            set
            {
                if (_billtoid != value)
                {
                    _billtoid = value;
                    base.OnPropertyChanged("BillToID", value);
                }
            }
        }

        string billToName;
        /// <summary>
        /// 帐单寄送名称
        /// </summary>
        public string BillToName
        {
            get
            {
                return billToName;
            }
            set
            {
                if (billToName != value)
                {
                    billToName = value;
                    base.OnPropertyChanged("BillToName", value);
                }
            }
        }

        #region 帐单寄送描述
        CustomerDescription _billToDescription;
        /// <summary>
        /// 帐单寄送描述
        /// </summary>
        public CustomerDescription BillToDescription
        {
            get
            {
                return _billToDescription;
            }
            set
            {
                if (_billToDescription != value)
                {
                    _billToDescription = value;
                    base.OnPropertyChanged("BillToDescription", value);
                }
            }
        }
        #endregion

        #endregion

        #region 备注
        string _remark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get
            {
                return _remark;
            }
            set
            {
                if (_remark != value)
                {
                    _remark = value;
                    base.OnPropertyChanged("Remark", value);
                }
            }
        }
        #endregion

        #region 品名
        string _commodity;
        /// <summary>
        /// 品名
        /// </summary>
        [StringLength(MaximumLength=500,CMessage="品名",EMessage="Commdity")]
        public string Commodity
        {
            get
            {
                return _commodity;
            }
            set
            {
                if (_commodity != value)
                {
                    _commodity = value;
                    base.OnPropertyChanged("Commodity", value);
                }
            }
        }

      #endregion

        #region 发送提货单日
        DateTime? _pickupsenddate;
        /// <summary>
        /// 发送提货单日
        /// </summary>
        public DateTime? PickUpSendDate
        {
            get
            {
                return _pickupsenddate;
            }
            set
            {
                if (_pickupsenddate != value)
                {
                    _pickupsenddate = value;
                    base.OnPropertyChanged("PickUpSendDate", value);
                }
            }
        }

        #endregion

        #region 创建人
        Guid _createby;
        /// <summary>
        /// 建立人
        /// </summary>
        public Guid CreateBy
        {
            get
            {
                return _createby;
            }
            set
            {
                if (_createby != value)
                {
                    _createby = value;
                    base.OnPropertyChanged("CreateBy", value);
                }
            }
        }
        #endregion

        #region 更新人
        Guid? _updateby;
        /// <summary>
        /// 更新人
        /// </summary>
        public Guid? UpdateBy
        {
            get
            {
                return _updateby;
            }
            set
            {
                if (_updateby != value)
                {
                    _updateby = value;
                    base.OnPropertyChanged("UpdateBy", value);
                }
            }
        }
        #endregion

        #region 更新时间
        DateTime? _updatedate;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate
        {
            get
            {
                return _updatedate;
            }
            set
            {
                if (_updatedate != value)
                {
                    _updatedate = value;
                    base.OnPropertyChanged("UpdateDate", value);
                }
            }
        }
        #endregion

        #region 关联的集装箱ID列表
        /// <summary>
        /// 关联的集装箱列表
        /// </summary>
        public List<Guid> ContainerIDList
        {
            get;
            set;
        }
        #endregion

    }

    #endregion 

}
