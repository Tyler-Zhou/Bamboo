using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FAM.ServiceInterface.DataObjects
{
    /// <summary>
    /// 总电放
    /// </summary>
    [Serializable]
    public class TelexApplyList : BaseDataObject
    {
        /// <summary>
        /// 根据主键判断是否新增数据
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }


        #region ID

        Guid _id;

        /// <summary>
        /// ID
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

                    this.NotifyPropertyChanged(o => o.ID);
                }
            }
        }

        #endregion

        #region 所属公司

        string _companyName;
        /// <summary>
        /// 所属公司
        /// </summary>
        public string CompanyName
        {
            get
            {
                return _companyName;
            }
            set
            {
                if (_companyName != value)
                {
                    _companyName = value;

                    this.NotifyPropertyChanged(o => o.CompanyName);
                }
            }
        }

        #endregion

        #region 客户中英文名称

        string _customerCName;

        /// <summary>
        /// 客户中文名称
        /// </summary>
        public string CustomerCName
        {
            get
            {
                return _customerCName;
            }
            set
            {
                if (_customerCName != value)
                {
                    _customerCName = value;

                    this.NotifyPropertyChanged(o => o.CustomerCName);
                }
            }
        }

        string _customerEName;

        /// <summary>
        /// 客户英文名称
        /// </summary>
        public string CustomerEName
        {
            get
            {
                return _customerEName;
            }
            set
            {
                if (_customerEName != value)
                {
                    _customerEName = value;

                    this.NotifyPropertyChanged(o => o.CustomerEName);
                }
            }
        }

        #endregion

        #region 客户的ID

        Guid _customerId;

        /// <summary>
        /// 客户的ID
        /// </summary>
        [GuidRequired(CMessage = "客户",EMessage="Customer")]
        public Guid CustomerId
        {
            get
            {
                return _customerId;
            }
            set
            {
                if (_customerId != value)
                {
                    _customerId = value;

                    this.NotifyPropertyChanged(o => o.CustomerId);
                }
            }
        }

        #endregion

        #region 收货人
        /// <summary>
        /// 收货人名称的拼装数据
        /// </summary>
        public string ConsigneeName { get; set; } 
        #endregion

        #region 电放类型
        /// <summary>
        /// 
        /// </summary>
        private TelexType _telextype;
        /// <summary>
        /// 电放类型:0所有;1电放;2.SWB
        /// </summary>
        public TelexType TelexType {
            get { return _telextype; }
            set
            {
                if (_telextype != value)
                {
                    _telextype = value;

                    this.NotifyPropertyChanged(o => o.TelexType);
                }
            } } 
        #endregion

        #region 协议的有效期

        DateTime _validDate;

        /// <summary>
        /// 协议的有效期（截止日期）
        /// </summary>
        [Required(CMessage = "月结协议的有效截止日期",EMessage="ValidDate")]
        public DateTime ValidDate
        {
            get
            {
                return _validDate;
            }
            set
            {
                if (_validDate != value)
                {
                    _validDate = value;

                    this.NotifyPropertyChanged(o => o.ValidDate);
                }
            }
        }

        #endregion

        #region 申请时间

        DateTime _applyTime;

        /// <summary>
        /// 申请时间
        /// </summary>
        [Required(CMessage = "申请时间",EMessage="ApplyTime")]
        public DateTime ApplyTime
        {
            get
            {
                return _applyTime;
            }
            set
            {
                if (_applyTime != value)
                {
                    _applyTime = value;

                    this.NotifyPropertyChanged(o => o.ApplyTime);
                }
            }
        }

        #endregion

        #region 建立人

        Guid _createbyid;
        /// <summary>
        /// 建立人
        /// </summary>
        public Guid CreateByID
        {
            get
            {
                return _createbyid;
            }
            set
            {
                if (_createbyid != value)
                {
                    _createbyid = value;
                    this.NotifyPropertyChanged(o => o.CreateByID);
                }
            }
        }

        string _createbyname;
        /// <summary>
        /// 建立人
        /// </summary>
        public string CreateByName
        {
            get
            {
                return _createbyname;
            }
            set
            {
                if (_createbyname != value)
                {
                    _createbyname = value;
                    this.NotifyPropertyChanged(o => o.CreateByName);
                }
            }
        }

        #endregion

        #region 更新时间-做数据版本控制用

        DateTime? _updateDate;
        /// <summary>
        /// 更新时间-做数据版本控制用
        /// </summary>
        public DateTime? UpdateDate
        {
            get
            {
                return _updateDate;
            }
            set
            {
                if (_updateDate != value)
                {
                    _updateDate = value;
                    this.NotifyPropertyChanged(o => o.UpdateDate);
                }
            }
        }

        #endregion

        #region 备注

        string _remark;

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(MaximumLength=500,CMessage="备注",EMessage="Remark")]
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

                    this.NotifyPropertyChanged(o => o.Remark);
                }
            }
        }

        #endregion

        #region 有效性

        bool _isValid;
        /// <summary>
        /// 有效性
        /// </summary>
        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                if (_isValid != value)
                {
                    _isValid = value;

                    this.NotifyPropertyChanged(o => o.IsValid);
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// 总总电放单对应的联系人的实体
    /// 一对多
    /// </summary>
    [Serializable]
    public class TelexConsignee : BaseDataObject
    {
        Guid _consigneeId;

        /// <summary>
        /// 收货人ID
        /// 如果叫ConsigneeId，则无法很好的配合网格列搜索器
        /// 故此处叫CustomerId
        /// </summary>
        public Guid CustomerId
        {
            get
            {
                return _consigneeId;
            }
            set
            {
                if (_consigneeId != value)
                {
                    _consigneeId = value;

                    this.NotifyPropertyChanged(o => o.CustomerId);
                }
            }
        }

        string _consigneeName;

        /// <summary>
        /// 收货人名称
        /// </summary>
        public string CustomerName
        {
            get
            {
                return _consigneeName;
            }
            set
            {
                if (_consigneeName != value)
                {
                    _consigneeName = value;

                    this.NotifyPropertyChanged(o => o.CustomerName);
                }
            }
        }
    }

    /// <summary>
    /// 总总电放单的详细信息
    /// </summary>
    [Serializable]
    public class TelexApplyInfo : TelexApplyList
    {
        string _customerDescription;

        /// <summary>
        /// 客户描述
        /// </summary>
        public string CustomerDescription
        {
            get
            {
                return _customerDescription;
            }
            set
            {
                if (_customerDescription != value)
                {
                    _customerDescription = value;

                    this.NotifyPropertyChanged(o => o.CustomerDescription);
                }
            }
        }

        /// <summary>
        /// 初始化总总电放单详细信息
        /// </summary>
        public TelexApplyInfo()
        {
            this.Consignees = new List<TelexConsignee>();
        }

        Guid _companyId;

        /// <summary>
        /// 所属公司ID
        /// </summary>
        public Guid CompanyId
        {
            get
            {
                return _companyId;
            }
            set
            {
                if (_companyId != value)
                {
                    _companyId = value;

                    this.NotifyPropertyChanged(o => o.CompanyId);
                }
            }
        }

        /// <summary>
        /// 绑定数据源的时候，决定界面上CheckBox的Enabled属性
        /// </summary>
        public bool HasConsignees
        {
            get
            {
                return this.Consignees.Count == 0;
            }
        }

        /// <summary>
        /// 收货人列表
        /// </summary>
        public List<TelexConsignee> Consignees { get; set; }
    }
}
