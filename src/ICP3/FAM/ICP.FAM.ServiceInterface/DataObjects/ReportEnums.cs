
namespace ICP.FAM.ServiceInterface.DataObjects
{
    /// <summary>
    /// ReportBillType 1:应收 2:应付 3:代理账单 4:实收 5:实付 6:JOURNAL 7:报销
    /// </summary>
    public enum ReportBillType  
    {
        /// <summary>
        /// 应收
        /// </summary>
        AR=1,
        /// <summary>
        /// 应付
        /// </summary>
        AP=2,
        /// <summary>
        /// 代理账单
        /// </summary>
        DRCR=3,
        /// <summary>
        /// 实收
        /// </summary>
        Deposit=4,
        /// <summary>
        /// 实付
        /// </summary>
        Check=5,
        /// <summary>
        /// JOURNAL
        /// </summary>
        Journal=6,
        /// <summary>
        /// 报销
        /// </summary>
        Clearance=7
    }
}
