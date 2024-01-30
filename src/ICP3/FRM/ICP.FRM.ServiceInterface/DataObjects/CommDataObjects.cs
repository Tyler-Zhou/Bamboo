namespace ICP.FRM.ServiceInterface.DataObjects
{
    using System;
    using System.Collections.Generic;
    using ICP.Framework.CommonLibrary.Common;

    /// <summary>
    /// FrmUnitRateList
    /// </summary>
    [Serializable]
    public class FrmUnitRateList : BaseDataObject
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
                    base.OnPropertyChanged("ID", value);
                }
            }
        }

        Guid _parentID;
        /// <summary>
        /// 运价ID
        /// </summary>
        public Guid ParentID
        {
            get
            {
                return _parentID;
            }
            set
            {
                if (_parentID != value)
                {
                    _parentID = value;
                    base.OnPropertyChanged("ParentID", value);
                }
            }
        }

        Guid _unitid;
        /// <summary>
        /// 运价ID
        /// </summary>
        public Guid UnitID
        {
            get
            {
                return _unitid;
            }
            set
            {
                if (_unitid != value)
                {
                    _unitid = value;
                    base.OnPropertyChanged("UnitID", value);
                }
            }
        }

        string _unitname;
        /// <summary>
        /// 运价单位名
        /// </summary>
        public string UnitName
        {
            get
            {
                return _unitname;
            }
            set
            {
                if (_unitname != value)
                {
                    _unitname = value;
                    base.OnPropertyChanged("UnitName", value);
                }
            }
        }
        /// <summary>
        /// 币种
        /// </summary>
        private string _Currency;
        public string Currency
        {
            get { return _Currency; }
            set
            {
                if (_Currency != value)
                {
                    _Currency = value;
                    base.OnPropertyChanged("Currency", value);
                }
            }
        }

        decimal _rate;
        /// <summary>
        /// 行位置
        /// </summary>
        public decimal Rate
        {
            get
            {
                return _rate;
            }
            set
            {
                if (_rate != value)
                {
                    _rate = value;
                    base.OnPropertyChanged("Rate", value);
                }
            }
        }
    }

    /// <summary>
    /// 查价中的底价和卖价
    /// </summary>
    [Serializable]
    public class FrmUnitRateInfo : FrmUnitRateList
    {
        decimal _ReserveRate;
        /// <summary>
        /// 底价
        /// </summary>
        public decimal ReserveRate
        {
            get
            {
                return _ReserveRate;
            }
            set
            {
                if (_ReserveRate != value)
                {
                    _ReserveRate = value;
                    base.OnPropertyChanged("ReserveRate", value);
                }
            }
        }

        decimal _SalesRate;
        /// <summary>
        /// 卖价
        /// </summary>
        public decimal SalesRate
        {
            get
            {
                return _SalesRate;
            }
            set
            {
                if (_SalesRate != value)
                {
                    _SalesRate = value;
                    base.OnPropertyChanged("SalesRate", value);
                }
            }
        }
    }

    /// <summary>
    /// 箱型列表
    /// </summary>
    [Serializable]
    public class FRMContainerInfo
    {
        static List<string> UnitList = new List<string>();
        FRMContainerInfo()
        {
            UnitList.Add("45FR");
            UnitList.Add("40RF");
            UnitList.Add("45HT");
            UnitList.Add("20RF");
            UnitList.Add("20HQ");
            UnitList.Add("20TK");
            UnitList.Add("20GP");
            UnitList.Add("40TK");
            UnitList.Add("40OT");
            UnitList.Add("20FR");
            UnitList.Add("45GP");
            UnitList.Add("40GP");
            UnitList.Add("45RF");
            UnitList.Add("20RH");
            UnitList.Add("45OT");
            UnitList.Add("40NOR");
            UnitList.Add("40FR");
            UnitList.Add("20OT");
            UnitList.Add("45TK");
            UnitList.Add("20NOR");
            UnitList.Add("40HT");
            UnitList.Add("40RH");
            UnitList.Add("45RH");
            UnitList.Add("45HQ");
            UnitList.Add("20HT");
            UnitList.Add("40HQ");
            UnitList.Add("53HQ");
        }

        /// <summary>
        /// 验证某个值是在箱列表中
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Contains(string value)
        {
            return UnitList.Contains(value);
        }

        public static List<string> GetList()
        {
            return UnitList;
        }

    }

}
