/*************
 *类说明：      发送邮件使用的类
 *创建时间：    2013-12-10
 *创建人:       wlj
 * *********************/
using System;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.OceanImport.ServiceInterface
{
    public class MailOIBusinessDataObjects : BaseDataObject
    {
        #region ETA
        DateTime? eta;
        /// <summary>
        /// 到港日
        /// </summary>
        public DateTime? ETA
        {
            get
            {
                return eta;
            }
            set
            {
                if (eta != value)
                {
                    eta = value;
                    base.OnPropertyChanged("ETA", value);
                }
            }
        }
        #endregion

        #region 提单号
        string _hblno;
        /// <summary>
        /// 提单号
        /// </summary>
        [Required(CMessage = "分提单号", EMessage = "HBLNo")]
        public string HBLNo
        {
            get
            {
                return _hblno;
            }
            set
            {
                if (_hblno != value)
                {
                    _hblno = value;
                    base.OnPropertyChanged("HBLNo", value);
                }
            }
        }
        #endregion

        #region 装货港

        Guid _polid;
        /// <summary>
        /// 装货港ID
        /// </summary>
        [GuidRequired(CMessage = "装货港", EMessage = "POL")]
        public Guid POLID
        {
            get
            {
                return _polid;
            }
            set
            {
                if (_polid != value)
                {
                    _polid = value;
                    base.OnPropertyChanged("POLID", value);
                }
            }
        }

        string _polname;
        /// <summary>
        /// 装货港名称
        /// </summary>
        public string POLName
        {
            get
            {
                return _polname;
            }
            set
            {
                if (_polname != value)
                {
                    _polname = value;
                    base.OnPropertyChanged("POLName", value);
                }
            }
        }

        #endregion

        #region 卸货港
        Guid _podid;
        /// <summary>
        /// 卸货港ID
        /// </summary>
        [GuidRequired(CMessage = "卸货港", EMessage = "POD")]
        public Guid PODID
        {
            get
            {
                return _podid;
            }
            set
            {
                if (_podid != value)
                {
                    _podid = value;
                    base.OnPropertyChanged("PODID", value);
                }
            }
        }

        string _podname;
        /// <summary>
        /// 卸货港名称
        /// </summary>
        public string PODName
        {
            get
            {
                return _podname;
            }
            set
            {
                if (_podname != value)
                {
                    _podname = value;
                    base.OnPropertyChanged("PODName", value);
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
                    base.OnPropertyChanged("ContainerDescription", value);
                }
            }
        }

        #endregion

        #region 客户

        Guid _customerid;
        /// <summary>
        /// 客户ID
        /// </summary>
        [GuidRequired(CMessage = "客户", EMessage = "Customer")]
        public Guid CustomerID
        {
            get
            {
                return _customerid;
            }
            set
            {
                if (_customerid != value)
                {
                    _customerid = value;
                    base.OnPropertyChanged("CustomerID", value);
                }
            }
        }

        string _customername;
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName
        {
            get
            {
                return _customername;
            }
            set
            {
                if (_customername != value)
                {
                    _customername = value;
                    base.OnPropertyChanged("CustomerName", value);
                }
            }
        }

        #endregion

        #region 业务号
        string _no;
        /// <summary>
        /// 编号
        /// </summary>
        public string No
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
                    base.OnPropertyChanged("No", value);
                }
            }
        }

        #endregion

        #region 箱号
        string _containerno;
        /// <summary>
        /// 箱号
        /// </summary>
        public string ContainerNo
        {
            get
            {
                return _containerno;
            }
            set
            {
                if (_containerno != value)
                {
                    _containerno = value;
                    base.OnPropertyChanged("ContainerNo", value);
                }
            }
        }

        #endregion

        #region LFDate
        DateTime? lfDate;
        /// <summary>
        /// 最后免堆日期
        /// </summary>
        public DateTime? LFDate
        {
            get
            {
                return lfDate;
            }
            set
            {
                if (lfDate != value)
                {
                    lfDate = value;
                    base.OnPropertyChanged("LFDate", value);
                }
            }
        }
        #endregion

        #region 港前业务号 & 港前放单的通知联系人 & 海出业务 .客服 & 海出业务.所有海进业务.文件和客服 & 海出业务.所有海进业务.提单号 & 目标代理
       
        string _operationNo;
        /// <summary>
        /// 港前业务号
        /// </summary>
        public string OperationNo
        {
            get
            {
                return _operationNo;
            }
            set
            {
                if (_operationNo != value)
                {
                    _operationNo = value;
                    base.OnPropertyChanged("OperationNo", value);
                }
            }
        }

        public Guid _oibookingID;
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OIBookingID
        {
            get
            {
                return _oibookingID;
            }
            set
            {
                if (_oibookingID != value)
                {
                    _oibookingID = value;
                    base.OnPropertyChanged("OIBookingID", value);
                }
            }
        }

        string _releaseEmail;
        /// <summary>
        /// 港前放单的通知联系人
        /// </summary>
        public string ReleaseEmail
        {
            get
            {
                return _releaseEmail;
            }
            set
            {
                if (_releaseEmail != value)
                {
                    _releaseEmail = value;
                    base.OnPropertyChanged("ReleaseEmail", value);
                }
            }
        }

        string _polFiler;
        /// <summary>
        /// 海出业务 .客服
        /// </summary>
        public string POLFiler
        {
            get
            {
                return _polFiler;
            }
            set
            {
                if (_polFiler != value)
                {
                    _polFiler = value;
                    base.OnPropertyChanged("POLFiler", value);
                }
            }
        }

        string _allfiler;
        /// <summary>
        /// 所有海进业务.文件和客服
        /// </summary>
        public string AllFiler
        {
            get
            {
                return _allfiler;
            }
            set
            {
                if (_allfiler != value)
                {
                    _allfiler = value;
                    base.OnPropertyChanged("AllFiler", value);
                }
            }
        } 
            
        string _blnos;
        /// <summary>
        /// 所有海进业务.提单号
        /// </summary>
        public string BLNos
        {
            get
            {
                return _blnos;
            }
            set
            {
                if (_blnos != value)
                {
                    _blnos = value;
                    base.OnPropertyChanged("BLNos", value);
                }
            }
        }

        string _agent;
        /// <summary>
        /// 目标代理,转移后的公司
        /// </summary>
        public string Agent
        {
            get
            {
                return _agent;
            }
            set
            {
                if (_agent != value)
                {
                    _agent = value;
                    base.OnPropertyChanged("Agent", value);
                }
            }
        }

        #endregion

    }
}
