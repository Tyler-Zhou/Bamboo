using System;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Common.ServiceInterface.DataObjects
{
    public partial class ShippingPortInfo : BaseDataObject
    {
        Guid? _id;
        /// <summary>
        /// 唯一键

        /// </summary>
        public Guid? ID
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


        Guid _shippingLineID;
        /// <summary>
        /// 航线ID
        /// </summary>
        public Guid ShippingLineID
        {
            get
            {
                return _shippingLineID;
            }
            set
            {
                if (_shippingLineID != value)
                {
                    _shippingLineID = value;
                    base.OnPropertyChanged("ShippingLineID", value);
                }
            }
        }

        Guid _portID;
        /// <summary>
        /// 航线ID
        /// </summary>
        public Guid PortID
        {
            get
            {
                return _portID;
            }
            set
            {
                if (_portID != value)
                {
                    _portID = value;
                    base.OnPropertyChanged("PortID", value);
                }
            }
        }

        string _portName;
        /// <summary>
        /// 国家港口名字
        /// </summary>
        public string PortName
        {
            get
            {
                return _portName;
            }
            set
            {
                if (_portName != value)
                {
                    _portName = value;
                    base.OnPropertyChanged("PortName", value);
                }
            }
        }


        string _createbyname;
        /// <summary>
        /// 创建人
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
                    base.OnPropertyChanged("CreateByName", value);
                }
            }
        }


        DateTime _createdate;
        /// <summary>
        /// 创建时间
        /// </summary>
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
                    base.OnPropertyChanged("CreateDate", value);
                }
            }
        }


        DateTime? _updateDate;
        /// <summary>
        /// 行版本
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
                    base.OnPropertyChanged("UpdateDate", value);
                }
            }
        }
    }
}
