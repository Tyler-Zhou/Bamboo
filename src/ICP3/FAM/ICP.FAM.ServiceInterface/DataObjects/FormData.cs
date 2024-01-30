

namespace ICP.FAM.ServiceInterface.DataObjects
{
    using System;

    /// <summary>
    /// 币种汇率对象
    /// </summary>
    [Serializable]
    public class CurrencyRateData
    {
        /// <summary>
        /// 币种ID
        /// </summary>
        public Guid CurrencyID { get; set; }

        /// <summary>
        /// 币种名
        /// </summary>
        public string CurrencyName { get; set; }

        /// <summary>
        /// 汇率
        /// </summary>
        public decimal Rate { get; set; }
    }

    /// <summary>
    /// 币种金额对象
    /// </summary>
    [Serializable]
    public class CurrencyAmountData
    {
        /// <summary>
        /// 币种ID
        /// </summary>
        public Guid CurrencyID { get; set; }

        /// <summary>
        /// 币种名
        /// </summary>
        public string CurrencyName { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }
    }
}
