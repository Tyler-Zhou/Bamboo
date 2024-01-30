#region Comment

/*
 * 
 * FileName:    Enum.cs
 * CreatedOn:   2014/5/20 星期二 18:03:37
 * CreatedBy:   taylor
 * 
 * Description：
 *      ->程序枚举类型定义
 *      ->1.DocumentType：文档类型
 * History：
 * 
 * 
 * 
 * 
 */

#endregion


namespace ICP.Document
{
    /// <summary>
    /// 文档类型
    /// </summary>
    public enum DocumentType
    {
        [MemberDescription("SOD", "OSO - 原始的SO副本", "OSO - Original SO Copy")]
        OSO = 1,
        [MemberDescription("TRK - 装柜信息", "TRK - Truck Loading Info")]
        TRK,
        [MemberDescription("CF - 报关单副本", "CF - Customs Declaration Copy")]
        CF,
        [MemberDescription("SIR", "SI - 补料信息", "SI - SI Staff")]
        SI,
        [MemberDescription("ARR -客户账单要求", "ARR -Accounts Receivable Requirement")]
        ARR,
        [MemberDescription("MBLR", "MBL - MBL副本", "MBL - Master BL Copy")]
        MBL,
        [MemberDescription("HBL - HBL副本", "HBL - House BL Copy")]
        HBL,
        [MemberDescription("SID - 客户确认补料", "SID - Customer Confirmed SI")]
        SID,
        [MemberDescription("ISF - 进口安全申报", "ISF - Importer Security Filing")]
        ISF,
        [MemberDescription("其它", "Other")]
        Other,
        [MemberDescription("AR - 应收账单", "AR - Accounts Receivable")]
        AR,
        [MemberDescription("APR", "AP - 应付账单", "AP - Accounts Payable")]
        AP,
        [MemberDescription("DC - 代理账单", "DC - Debit Credit")]
        DC,
        [MemberDescription("ASO - 调整的SO副本", "ASO - Adjusted SO Copy")]
        ASO,
        [MemberDescription("BKG - 客户订舱单", "BKG - Customer Booking")]
        BKG,
        [MemberDescription("LGTLX - 电放保函", "LGTLX - Letter of Telex Guarantee")]
        LGTLX,
        [MemberDescription("LGPKG - 包装固定保函", "LGPKG - Letter of Package Guarantee")]
        LGPKG,
        [MemberDescription("LGDC - 非危险品保函", "LGDC - Letter of Non Dangerous Cargo Guarantee")]
        LGDC,
        [MemberDescription("LGPBL - 压单保函", "LGPBL - Letter of Pledge BL Guarantee")]
        LGPBL,
        [MemberDescription("LGABL - 改单保函", "LGABL - Letter of Alter BL Guarantee")]
        LGABL,
        [MemberDescription("LGMBL - 并单保函", "LGMBL - Letter of Merge BL Guarantee")]
        LGMBL,
        [MemberDescription("LGPKG1 - 包装固定保函(CO)", "LGPKG1 - (CO)Letter of Package Guarantee")]
        LGPKG1,
        [MemberDescription("LGDC1 - 非危险品保函(CO)", "LGDC1 - (CO)Letter of Non Dangerous Cargo Guarantee")]
        LGDC1,
        [MemberDescription("LGABL1 - 改单保函(CO)", "LGABL1 - (CO)Letter of Alter BL Guarantee")]
        LGABL1,
        [MemberDescription("LGMBL1 - 并单保函(CO)", "LGMBL1 - (CO)Letter of Merge BL Guarantee")]
        LGMBL1,
        [MemberDescription("LGTLX1 - 电放保函(CO)", "LGTLX1 - (CO)Letter of Telex Guarantee")]
        LGTLX1,
        [MemberDescription("AMS - 美国舱单系统", "AMS - America Manifest System")]
        AMS,
        [MemberDescription("ANRC", "AN - 到港通知书", "AN - Arrival Notice")]
        AN,
        [MemberDescription("SIMBL - MBL副本", "SIMBL - MBL Copy")]
        SIMBL,
        [MemberDescription("SIHBL - HBL副本", "SIHBL - HBL Copy")]
        SIHBL
    }

    /// <summary>
    /// 异常类型
    /// </summary>
    public enum ExceptionType
    {
        /// <summary>
        /// 进程异常
        /// </summary>
        ThreadEx=1,
        /// <summary>
        /// 未处理异常
        /// </summary>
        UnhandledEx=2
    }
}
