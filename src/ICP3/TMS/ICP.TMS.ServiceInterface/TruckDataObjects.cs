using System;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.TMS.ServiceInterface
{
    /// <summary>
    /// 拖车信息
    /// </summary>
    [Serializable]
    public class TruckDataList : BaseDataObject
    {
        #region ID
        private Guid id;
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID
        {
            get
            {
                return id;
            }
            set
            {
                if (id != value)
                {
                    id = value;
                    this.NotifyPropertyChanged(o => o.ID);
                }
            }
        }
        #endregion

        #region NO
        private String truckNo;
        /// <summary>
        /// 车牌号
        /// </summary>
        public String TruckNo
        {
            get
            {
                return truckNo;
            }
            set
            {
                if (truckNo != value)
                {
                    truckNo = value;
                    this.NotifyPropertyChanged(o => o.TruckNo);
                }
            }
        }
        #endregion

        #region 型号
        private string typeName;
        /// <summary>
        /// 型号
        /// </summary>
        public string TypeName
        {
            get
            {
                return typeName;
            }
            set
            {
                if (typeName != value)
                {
                    typeName = value;
                    this.NotifyPropertyChanged(o=>o.TypeName);
                }
            }
        }
        #endregion

        #region 购买日期
        private DateTime buyDate;
        /// <summary>
        /// 购买日期
        /// </summary>
        public DateTime BuyDate
        {
            get
            {
                return buyDate;
            }
            set
            {
                if (buyDate != value)
                {
                    buyDate = value;
                    this.NotifyPropertyChanged(o=>o.BuyDate);
                }
            }
        }
        #endregion

        #region 创建人ID
        /// <summary>
        /// 创建人ID
        /// </summary>
        public Guid CreateByID
        {
            get;
            set;
        }

        #endregion

        #region 创建人名称
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreateByName
        {
            get;
            set;
        }

        #endregion

        #region 创建时间
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateByDate
        {
            get;
            set;
        }
        #endregion

        #region 最后更新时间
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime? UpdateDate
        {
            get;
            set;
        }

        #endregion

        #region 备注
        private string remark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get
            {
                return remark;
            }
            set
            {
                if (remark != value)
                {
                    remark = value;
                    this.NotifyPropertyChanged(o=>o.Remark);
                }
            }
        }
        #endregion

        /// <summary>
        /// 有效性
        /// </summary>
        public bool IsValid
        {
            get;
            set;
        }


    }
}
