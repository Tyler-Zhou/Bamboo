using System;
using System.Collections.Generic;
using System.Text;

namespace LongWin.BusinessInfo.ServiceInterface.DataObject
{
    [Serializable]
  public  class JobFeeData
    {
        string _CustomerCName;
        /// <summary>
        /// 往来单位名称(中文)
        /// </summary>
        public string CustomerCName
        {
            get { return _CustomerCName; }
            set { _CustomerCName = value; }
        }

        string _CustomerEName;
        /// <summary>
        /// 往来单位名称(英文)
        /// </summary>
        public string CustomerEName
        {
            get { return _CustomerEName; }
            set { _CustomerEName = value; }
        }

        string _CurrencyName;
        /// <summary>
        /// 币种名称
        /// </summary>
        public string CurrencyName
        {
            get { return _CurrencyName; }
            set { _CurrencyName = value; }
        }

        string _FeeEName;
        /// <summary>
        /// 费用名称(英文)
        /// </summary>
        public string FeeEName
        {
            get { return _FeeEName; }
            set { _FeeEName = value; }
        }

        string _FeeCName;
        /// <summary>
        /// 费用名称(中文)
        /// </summary>
        public string FeeCName
        {
            get { return _FeeCName; }
            set { _FeeCName = value; }
        }

        short _DrCrFlag;
        /// <summary>
        /// 收/付标志（0:应收 1:应付）
        /// </summary>
        public short DrCrFlag
        {
            get { return _DrCrFlag; }
            set { _DrCrFlag = value; }
        }

        bool _RecoupFlag;
        /// <summary>
        /// 核销
        /// </summary>
        public bool RecoupFlag
        {
            get { return _RecoupFlag; }
            set { _RecoupFlag = value; }
        }

        bool _IsPaid;
        /// <summary>
        /// 到帐
        /// </summary>
        public bool IsPaid
        {
            get { return _IsPaid; }
            set { _IsPaid = value; }
        }

        decimal _Rate;
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal Rate
        {
            get { return _Rate; }
            set { _Rate = value; }
        }

        decimal _Amount;
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
    }
}
