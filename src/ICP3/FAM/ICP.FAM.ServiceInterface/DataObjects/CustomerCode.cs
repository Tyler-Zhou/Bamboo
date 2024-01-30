using System;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FAM.ServiceInterface.DataObjects
{
    /// <summary>
    /// kp客户编码表
    /// </summary>
    [Serializable]
    public class CustomerCode:BaseDataObject
    {
        Int32 _rowNumber;
        /// <summary>
        /// 自增行号
        /// </summary>
        public Int32 RowNumber
        {
            get
            {
                return _rowNumber;
            }
            set
            {
                if (_rowNumber != value)
                {
                    _rowNumber = value;
                    base.OnPropertyChanged("RowNumber", value);
                }
            }
        }

        string _code;
        /// <summary>
        /// 编码
        /// </summary>
        public String 编码
        {
            get
            {
                return _code;
            }
            set
            {
                if (_code != value)
                {
                    _code = value;
                    base.OnPropertyChanged("编码", value);
                }
            }
        }

        string _name;
        /// <summary>
        /// 名称
        /// </summary>
        public String 名称
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    base.OnPropertyChanged("名称", value);
                }
            }
        }

        string _initialism;
        /// <summary>
        /// 简码
        /// </summary>
        public String 简码
        {
            get
            {
                return _initialism;
            }
            set
            {
                if (_initialism != value)
                {
                    _initialism = value;
                    base.OnPropertyChanged("简码", value);
                }
            }
        }

        string _taxNo;
        /// <summary>
        /// 税号
        /// </summary>
        public String 税号
        {
            get
            {
                return _taxNo;
            }
            set
            {
                if (_taxNo != value)
                {
                    _taxNo = value;
                    base.OnPropertyChanged("税号", value);
                }
            }
        }

        string _addrTel;
        /// <summary>
        /// 地址电话
        /// </summary>
        public String 地址电话
        {
            get
            {
                return _addrTel;
            }
            set
            {
                if (_addrTel != value)
                {
                    _addrTel = value;
                    base.OnPropertyChanged("地址电话", value);
                }
            }
        }

        string _bankNo;
        /// <summary>
        /// 银行帐号
        /// </summary>
        public String 银行帐号
        {
            get
            {
                return _bankNo;
            }
            set
            {
                if (_bankNo != value)
                {
                    _bankNo = value;
                    base.OnPropertyChanged("银行帐号", value);
                }
            }
        }

        string _email;
        /// <summary>
        /// 邮件地址
        /// </summary>
        public String 邮件地址
        {
            get
            {
                return _email;
            }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    base.OnPropertyChanged("邮件地址", value);
                }
            }
        }

        string _remark;
        /// <summary>
        /// 备注
        /// </summary>
        public String 备注
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
                    base.OnPropertyChanged("备注", value);
                }
            }
        }

        string _isCheck;
        /// <summary>
        /// 身份证校验
        /// </summary>
        public String 身份证校验
        {
            get
            {
                return _isCheck;
            }
            set
            {
                if (_isCheck != value)
                {
                    _isCheck = value;
                    base.OnPropertyChanged("身份证校验", value);
                }
            }
        }
    }
}
