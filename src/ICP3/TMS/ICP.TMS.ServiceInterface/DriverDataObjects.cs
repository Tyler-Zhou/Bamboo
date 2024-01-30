using System;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.TMS.ServiceInterface
{
    /// <summary>
    /// 司机信息
    /// </summary>
    [Serializable]
    public class DriversDataList : BaseDataObject
    {

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
                    this.NotifyPropertyChanged(o => o.ID);
                }
            }
        }
        #endregion

        #region 名字
        string name;
        /// <summary>
        /// 名字
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                    this.NotifyPropertyChanged(o => o.Name);
                }
            }
        }
        #endregion

        #region 地址
        string _adress;
        /// <summary>
        /// 地址
        /// </summary>
        public string Adress
        {
            get
            {
                return _adress;
            }
            set
            {
                if (_adress != value)
                {
                    _adress = value;
                    this.NotifyPropertyChanged(o => o.Adress);
                }
            }
        }
        #endregion

        #region 手机
        string _mobile;
        /// <summary>
        /// 手机 
        /// </summary>
        public string Mobile
        {
            get
            {
                return _mobile;
            }
            set
            {
                if (_mobile != value)
                {
                    _mobile = value;
                    this.NotifyPropertyChanged(o => o.Mobile);
                }
            }
        }
        #endregion

        #region 身份ID
        string _cardno;
        /// <summary>
        /// 身份ID
        /// </summary>
        public string CardNo
        {
            get
            {
                return _cardno;
            }
            set
            {
                if (_cardno != value)
                {
                    _cardno = value;
                    this.NotifyPropertyChanged(o => o.CardNo);
                }
            }
        }
        #endregion

        #region 国家
        private Guid? countryID;
        /// <summary>
        /// 国家
        /// </summary>
        public Guid? CountryID
        {
            get
            {
                return countryID;
            }
            set
            {
                if (countryID != value)
                {
                    countryID = value;
                    this.NotifyPropertyChanged(o=>o.CountryID);
                }
            }
        }
        /// <summary>
        /// 国家名称
        /// </summary>
        public string CountryName
        {
            get;
            set;
        }
        #endregion

        #region 省份
  

        Guid? _provinceid;
        /// <summary>
        /// 省份
        /// </summary>
        public Guid? ProvinceID
        {
            get
            {
                return _provinceid;
            }
            set
            {
                if (_provinceid != value)
                {
                    _provinceid = value;
                    this.NotifyPropertyChanged(o => o.ProvinceID);
                }
            }
        }
        private string provinceName;
        /// <summary>
        /// 省份名称
        /// </summary>
        public string ProvinceName
        {
            get
            {
                return provinceName;
            }
            set
            {
                provinceName = value;
            }
        }
        private string countryAndProvince;
        /// <summary>
        /// 国家.省份
        /// </summary>
        public string CountryAndProvince
        {
            get
            {
                if (string.IsNullOrEmpty(countryAndProvince))
                {
                    if (!string.IsNullOrEmpty(CountryName) && !string.IsNullOrEmpty(ProvinceName))
                    {
                        countryAndProvince = CountryName + " " + ProvinceName;
                    }
                    else if (string.IsNullOrEmpty(CountryName) && !string.IsNullOrEmpty(ProvinceName))
                    {
                        countryAndProvince= ProvinceName;
                    }
                    else if (!string.IsNullOrEmpty(CountryName) && string.IsNullOrEmpty(ProvinceName))
                    {
                        countryAndProvince= CountryName;
                    }
                    else
                    {
                        return countryAndProvince=string.Empty;
                    }
                    return countryAndProvince;
                }
                else
                {
                    return countryAndProvince;
                }
            }
            set
            {
                countryAndProvince = value;
            }
        }

        #endregion

        #region 城市
        Guid? _cityid;
        /// <summary>
        /// 城市
        /// </summary>
        public Guid? CityID
        {
            get
            {
                return _cityid;
            }
            set
            {
                if (_cityid != value)
                {
                    _cityid = value;
                    this.NotifyPropertyChanged(o => o.CityID);
                }
            }
        }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName
        {
            get;
            set;
        }
        #endregion

        #region 拖车
        Guid? _truckid;
        /// <summary>
        /// 默认拖车
        /// </summary>
        public Guid? TruckID
        {
            get
            {
                return _truckid;
            }
            set
            {
                if (_truckid != value)
                {
                    _truckid = value;
                    this.NotifyPropertyChanged(o => o.TruckID);
                }
            }
        }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string TruckNo
        {
            get;
            set;
        }
        #endregion

        #region 是否有效
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid
        {
            get;
            set;
        }
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
                    this.NotifyPropertyChanged(o => o.Remark);
                }
            }
        }
        #endregion

        #region 创建时间
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate
        {
            get;
            set;
        }
        #endregion

        #region 创建人
        Guid _createby;
        /// <summary>
        /// 创建人
        /// </summary>
        public Guid CreateBy
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
                    this.NotifyPropertyChanged(o => o.UpdateDate);
                }
            }
        }
        #endregion

    }
}
