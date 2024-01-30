using ICP.Framework.CommonLibrary.Attributes;
using System;

namespace ICP.FAM.ServiceInterface
{
    #region Day Range
    /// <summary>
    /// 天数范围(Day Range)
    /// </summary>
    [Flags]
    [Serializable]
    public enum DayRange
    {
        /// <summary>
        /// All
        /// </summary>
        [MemberDescription("所有", "ALL")]
        All = 0,
        /// <summary>
        /// Over 15
        /// </summary>
        [MemberDescription("15", "15")]
        Day15 = 15,
        /// <summary>
        /// Over 30
        /// </summary>
        [MemberDescription("30", "30")]
        Day30 = 30,
        /// <summary>
        /// Over 40
        /// </summary>
        [MemberDescription("40", "40")]
        Day40 = 40,
        /// <summary>
        /// Over 60
        /// </summary>
        [MemberDescription("60", "60")]
        Day60 = 60,
        /// <summary>
        /// Over 90
        /// </summary>
        [MemberDescription("90", "90")]
        Day90 = 90,
        /// <summary>
        /// Over 180
        /// </summary>
        [MemberDescription("180", "180")]
        Day180 = 180,
    }
    #endregion

    #region Risk Rating Level
    /// <summary>
    /// 风险评分等级
    /// </summary>
    [Flags]
    [Serializable]
    public enum RiskRatingLevel
    {
        /// <summary>
        /// Level 1
        /// </summary>
        [MemberDescription("1", "1")]
        Level1 = 1,
        /// <summary>
        /// Level 2
        /// </summary>
        [MemberDescription("2", "2")]
        Level2 = 2,
        /// <summary>
        /// Level 3
        /// </summary>
        [MemberDescription("3", "3")]
        Level3 = 3,
        /// <summary>
        /// Level 4
        /// </summary>
        [MemberDescription("4", "4")]
        Level4 = 4,
        /// <summary>
        /// Level 5
        /// </summary>
        [MemberDescription("5", "5")]
        Level5 = 5,
        /// <summary>
        /// Level 6
        /// </summary>
        [MemberDescription("6", "6")]
        Level6 = 6,
        /// <summary>
        /// Level 7
        /// </summary>
        [MemberDescription("7", "7")]
        Level7 = 7,
    }
    #endregion

    #region 银行类型
    /// <summary>
    /// 银行类型
    /// </summary>
    [Flags]
    [Serializable]
    public enum BANKCODE
    {
        /// <summary>
        /// 招商银行
        /// </summary>
        [MemberDescription("招商银行", "CMB")]
        CMB = 1,
    }
    #endregion

    #region 银行流水关联销账类型
    /// <summary>
    /// 银行流水关联销账类型(Bank Transaction Association Write-Off)
    /// </summary>
    [Flags]
    [Serializable]
    public enum BTAWType
    {
        /// <summary>
        /// 未知
        /// </summary>
        [MemberDescription("未知", "Unknown")]
        Unknown = 0,
        /// <summary>
        /// 手动
        /// </summary>
        [MemberDescription("手动", "Manual")]
        Manual = 1,
        /// <summary>
        /// 自动
        /// </summary>
        [MemberDescription("自动", "Automatic")]
        Automatic = 2,
    }
    #endregion

    #region 招商银行币种
    /// <summary>
    /// 招商银行币种
    /// </summary>
    [Flags]
    [Serializable]
    public enum CMBCurrencyType
    {
        /// <summary>
        /// 人民币
        /// </summary>
        [MemberDescription("人民币", "RMB")]
        RMB = 10,
        /// <summary>
        /// 港币
        /// </summary>
        [MemberDescription("港币", "HKD")]
        HKD = 21,
        /// <summary>
        /// 澳元
        /// </summary>
        [MemberDescription("澳元", "AUD")]
        AUD = 29,
        /// <summary>
        /// 美元
        /// </summary>
        [MemberDescription("美元", "USD")]
        USD = 32,
        /// <summary>
        /// 欧元
        /// </summary>
        [MemberDescription("欧元", "EUR")]
        EUR = 35,
        /// <summary>
        /// 加拿大元
        /// </summary>
        [MemberDescription("加拿大元", "CAD")]
        CAD = 39,
        /// <summary>
        /// 英镑
        /// </summary>
        [MemberDescription("英镑", "GBP")]
        GBP = 43,
        /// <summary>
        /// 日元
        /// </summary>
        [MemberDescription("日元", "JPY")]
        JPY = 65,
        /// <summary>
        /// 新加坡元
        /// </summary>
        [MemberDescription("新加坡元", "SGD")]
        SGD = 69,
        /// <summary>
        /// 挪威克朗
        /// </summary>
        [MemberDescription("挪威克朗", "NOK")]
        NOK = 83,
        /// <summary>
        /// 丹麦克朗
        /// </summary>
        [MemberDescription("丹麦克朗", "DKK")]
        DKK = 85,
        /// <summary>
        /// 瑞士法郎
        /// </summary>
        [MemberDescription("瑞士法郎", "CHF")]
        CHF = 87,
        /// <summary>
        /// 瑞典克朗
        /// </summary>
        [MemberDescription("瑞典克朗", "SEK")]
        SEK = 88,

    } 
    #endregion

    #region CSP账单状态
    /// <summary>
    /// CSP账单状态
    /// </summary>
    [Flags]
    [Serializable]
    public enum CSP_BILL_STATUS
    {
        /// <summary>
        /// 未支付
        /// </summary>
        [MemberDescription("未支付", "Outstanding")]
        Outstanding = 1,

        /// <summary>
        /// 已支付
        /// </summary>
        [MemberDescription("已支付", "Paid")]
        Paid = 2,

        /// <summary>
        /// 逾期
        /// </summary>
        [MemberDescription("逾期", "Overdue")]
        Overdue = 3,

        /// <summary>
        /// 作废
        /// </summary>
        [MemberDescription("作废", "Voided")]
        Voided = 4,

        /// <summary>
        /// 已确认
        /// </summary>
        [MemberDescription("已确认", "Confirmed")]
        Confirmed = 5,

        /// <summary>
        /// 部分支付
        /// </summary>
        [MemberDescription("部分支付", "PartialPaid")]
        PartialPaid = 6,
    }
    #endregion

}
