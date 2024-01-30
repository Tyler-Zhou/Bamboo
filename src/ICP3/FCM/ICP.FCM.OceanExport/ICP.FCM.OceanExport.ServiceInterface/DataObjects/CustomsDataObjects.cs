using System;
using ICP.Framework.CommonLibrary.Common;
using System.Data.Linq.Mapping;

namespace ICP.FCM.OceanExport.ServiceInterface.DataObjects
{
    #region OceanCustoms
    /// <summary>
    /// 报关委托实体
    /// </summary>
    [Serializable]
    [Table(Name = "fcm.OceanCustoms")]
    public partial class OceanCustoms : BaseDataObject
    {
        #region 唯一键/ID

        Guid _id;
        /// <summary>
        /// 唯一键
        /// </summary>
        [Column(Storage = "_id", IsPrimaryKey = true)]
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

        Guid _oceanBookingID;
        [Column(Storage = "_oceanBookingID")]
        public Guid OceanBookingID
        {
            get
            {
                return _oceanBookingID;
            }
            set
            {
                if (_oceanBookingID != value)
                {
                    _oceanBookingID = value;
                    base.OnPropertyChanged("OceanBookingID", value);
                }
            }
        }

        Guid _oceanContainerID;
        [Column(Storage = "_oceanContainerID")]
        public Guid OceanContainerID
        {
            get
            {
                return _oceanContainerID;
            }
            set
            {
                if (_oceanContainerID != value)
                {
                    _oceanContainerID = value;
                    base.OnPropertyChanged("OceanContainerID", value);
                }
            }
        }

        string _oceanContainerNo;
        [Column(Storage = "_oceanContainerNo")]
        public String OceanContainerNo
        {
            get
            {
                return _oceanContainerNo;
            }
            set
            {
                if (_oceanContainerNo != value)
                {
                    _oceanContainerNo = value;
                    base.OnPropertyChanged("OceanContainerNo", value);
                }
            }
        }

        Guid _customsID;
        [Column(Storage = "_customsID")]
        public Guid CustomsID
        {
            get
            {
                return _customsID;
            }
            set
            {
                if (_customsID != value)
                {
                    _customsID = value;
                    base.OnPropertyChanged("CustomsID", value);
                }
            }
        }

        String _customsName;
        [Column(Storage = "_customsName")]
        public String CustomsName
        {
            get
            {
                return _customsName;
            }
            set
            {
                if (_customsName != value)
                {
                    _customsName = value;
                    base.OnPropertyChanged("CustomsName", value);
                }
            }
        }

        string _no;

        [Column(Storage = "_no", CanBeNull = false)]
        public string NO
        {
            get { return _no; }
            set
            {
                if (_no != value)
                {
                    _no = value;
                    base.OnPropertyChanged("NO", value);
                }
            }
        }

        string _title;
        [Column(Storage = "_title", CanBeNull = false)]
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    base.OnPropertyChanged("Title", value);
                }
            }
        }

        string _portToCustoms;
        [Column(Storage = "_portToCustoms", CanBeNull = false)]
        public string PortToCustoms
        {
            get { return _portToCustoms; }
            set
            {
                if (_portToCustoms != value)
                {
                    _portToCustoms = value;
                    base.OnPropertyChanged("PortToCustoms", value);
                }
            }
        }

        int _way;
        [Column(Storage = "_way")]
        public Int32 Way
        {
            get { return _way; }
            set
            {
                if (_way != value)
                {
                    _way = value;
                    base.OnPropertyChanged("Way", value);
                }
            }
        }

        string _remark;
        [Column(Storage = "_remark", CanBeNull = false)]
        public string Remark
        {
            get { return _remark; }
            set
            {
                if (_remark != value)
                {
                    _remark = value;
                    base.OnPropertyChanged("Remark", value);
                }
            }
        }
        private System.Guid _createBy;

        private System.DateTime _CreateDate;

        private System.Guid _UpdateBy;

        private System.DateTime? _UpdateDate;

        [Column(Storage = "_createBy", CanBeNull = false)]
        public System.Guid CreateBy
        {
            get
            {
                return this._createBy;
            }
            set
            {
                if ((this._createBy != value))
                {

                    this._createBy = value;
                    base.OnPropertyChanged("CreateBy", value);
                }
            }
        }

        [Column(Storage = "_CreateDate")]
        public System.DateTime CreateDate
        {
            get
            {
                return this._CreateDate;
            }
            set
            {
                if ((this._CreateDate != value))
                {
                    this._CreateDate = value;
                    base.OnPropertyChanged("CreateDate", value);
                }
            }
        }

        [Column(Storage = "_UpdateBy",CanBeNull=true)]
        public System.Guid UpdateBy
        {
            get
            {
                return this._UpdateBy;
            }
            set
            {
                if ((this._UpdateBy != value))
                {
                    this._UpdateBy = value;
                    base.OnPropertyChanged("UpdateBy", value);
                }
            }
        }

        [Column(Storage = "_UpdateDate",CanBeNull=true)]
        public System.DateTime? UpdateDate
        {
            get
            {
                return this._UpdateDate;
            }
            set
            {
                if ((this._UpdateDate != value))
                {
                    this._UpdateDate = value;
                    base.OnPropertyChanged("UpdateDate", value);
                }
            }
        }
    }

    #endregion
}
