using System; 
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Common.ServiceInterface.DataObjects
{
    #region 核销单数据对象
    /// <summary>
    /// 核销单数据对象
    /// </summary>
    [Serializable]
    public class VerificationSheet : BaseDataObject
    {
        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return ID == Guid.Empty; } }

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
                    this.NotifyPropertyChanged(o => o.ID);
                }
            }
        }

        string _sheetNo;
        /// <summary>
        /// 核销单号
        /// </summary>
        [StringLength(MaximumLength=20,CMessage="核销单号",EMessage="SheetNo")]
        [Required(CMessage = "核销单号", EMessage = "SheetNo")]
        public string SheetNo
        {
            get
            {
                return _sheetNo;
            }
            set
            {
                if (_sheetNo != value)
                {
                    _sheetNo = value;
                    this.NotifyPropertyChanged(o => o.SheetNo);
                }
            }
        }

        Guid _operationId;
        /// <summary>
        /// 业务ID
        /// </summary>
        [GuidRequired(CMessage = "业务号",EMessage="OperationNo")]
        public Guid OperationId
        {
            get
            {
                return _operationId;
            }
            set
            {
                if (_operationId != value)
                {
                    _operationId = value;
                    this.NotifyPropertyChanged(o => o.OperationId);
                }
            }
        }

        string _operationNo;
        /// <summary>
        /// 业务号
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
                    this.NotifyPropertyChanged(o => o.OperationNo);
                }
            }
        }

        Guid? _customerid;
        /// <summary>
        /// 经营单位ID
        /// </summary>
        //[GuidRequired(ErrorMessage = "经营单位必须填写")]
        public Guid? CustomerId
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
                    this.NotifyPropertyChanged(o => o.CustomerId);
                }
            }
        }

        string _customerName;
        /// <summary>
        /// 经营单位name
        /// </summary>
        public string CustomerName
        {
            get
            {
                return _customerName;
            }
            set
            {
                if (_customerName != value)
                {
                    _customerName = value;
                    this.NotifyPropertyChanged(o => o.CustomerName);
                }
            }
        }

        DateTime? _receiptDate;
        /// <summary>
        /// 收到日期
        /// </summary>
        public DateTime? ReceiptDate
        {
            get
            {
                return _receiptDate;
            }
            set
            {
                if (_receiptDate != value)
                {
                    _receiptDate = value;
                    this.NotifyPropertyChanged(o => o.ReceiptDate);
                }
            }
        }

        DateTime? _returnDate;
        /// <summary>
        /// 寄还日期
        /// </summary>
        public DateTime? ReturnDate
        {
            get
            {
                return _returnDate;
            }
            set
            {
                if (_returnDate != value)
                {
                    _returnDate = value;
                    this.NotifyPropertyChanged(o => o.ReturnDate);
                }
            }
        }

        string _expressNO;
        /// <summary>
        /// 快递单号
        /// </summary>
        [StringLength(MaximumLength=50,CMessage="快递单号",EMessage="ExpressNo")]
        public string ExpressNO
        {
            get
            {
                return _expressNO;
            }
            set
            {
                if (_expressNO != value)
                {
                    _expressNO = value;
                    this.NotifyPropertyChanged(o => o.ExpressNO);
                }
            }
        }

        bool _isFreightArrive;
        /// <summary>
        /// 运费是否到帐
        /// </summary>
        public bool IsFreightArrive
        {
            get
            {
                return _isFreightArrive;
            }
            set
            {
                if (_isFreightArrive != value)
                {
                    _isFreightArrive = value;
                    this.NotifyPropertyChanged(o => o.IsFreightArrive);
                }
            }
        }

        string _remark;
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(MaximumLength=100,CMessage="备注",EMessage="Remark")]
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

        DateTime _createdate;
        /// <summary>
        /// 建立日期
        /// </summary>
        [Required(CMessage = "建立日期",EMessage="CreateDate")]
        public DateTime CreateDate
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
                    this.NotifyPropertyChanged(o => o.CreateDate);
                }
            }
        }

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
    }

    #endregion
}