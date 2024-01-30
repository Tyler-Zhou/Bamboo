using ICP.Framework.CommonLibrary.Attributes;
using System;

namespace ICP.FileSystem.ServiceInterface
{
    #region 文件上传状态
    /// <summary>
    /// 文件上传状态
    /// </summary>
    [Flags]
    [Serializable]
    public enum UploadState
    {
        /// <summary>
        /// 本地处理中
        /// </summary>
        [MemberDescription("本地处理中", "Local Processing")]
        LocalProcessing,
        /// <summary>
        /// 本地保存中
        /// </summary>
        [MemberDescription("本地保存中", "Local Saving")]
        LocalSaving,
        /// <summary>
        /// 本地保存完成
        /// </summary>
        [MemberDescription("本地保存完成", "Local Saved")]
        LocalSaved,
        /// <summary>
        /// 开始上传到服务器
        /// </summary>
        [MemberDescription("开始上传到服务器", "Uploading")]
        Uploading,
        /// <summary>
        /// 上传成功
        /// </summary>
        [MemberDescription("上传成功", "Successed")]
        Successed,
        /// <summary>
        /// 上传失败
        /// </summary>
        [MemberDescription("上传失败", "Failed")]
        Failed
    } 
    #endregion

    #region 文件来源
    /// <summary>
    /// 文件来源
    /// </summary>
    [Flags]
    [Serializable]
    public enum FileSource
    {
        /// <summary>
        /// 正常上载
        /// </summary>
        [MemberDescription("正常上载", "From DocumentList")]
        FDocument = 1,
        /// <summary>
        /// 来自分文件
        /// </summary>
        [MemberDescription("来自分文件", "From Dispatch")]
        FDispatch = 2
    } 
    #endregion

    #region 文档状态
    /// <summary>
    /// 文档状态DocS/DocH/DCRev/DCRcv
    /// </summary>
    [Flags]
    [Serializable]
    public enum DocumentState
    {
        /// <summary>
        /// 正常
        /// </summary>
        [MemberDescription("Pending", "Pending")]
        Pending = 0,
        /// <summary>
        /// 已分发
        /// </summary>
        [MemberDescription("DocS", "DocS")]
        Dispatched = 1,
        /// <summary>
        /// 接收
        /// </summary>
        [MemberDescription("DocH", "DocH")]
        Accepted = 2,
        /// <summary>
        /// 已申请修订
        /// </summary>
        [MemberDescription("DCRev", "DCRev")]
        Reviseing = 3,

        /// <summary>
        /// 已审批修订
        /// </summary>
        [MemberDescription("DCRcv", "DCRcv")]
        Revised = 4,
    }
    #endregion

    #region 文档类型
    /// <summary>
    /// 文档类型
    /// </summary>
    [Flags]
    [Serializable]
    public enum DocumentType
    {
        /// <summary>
        /// 原始的SO副本
        /// </summary>
        [MemberDescription("SOD", "OSO - 原始的SO副本", "OSO - Original SO Copy")]
        OSO = 1,
        /// <summary>
        /// 装柜信息
        /// </summary>
        [MemberDescription("TRK - 装柜信息", "TRK - Truck Loading Info")]
        TRK = 2,
        /// <summary>
        /// 报关单副本
        /// </summary>
        [MemberDescription("CF - 报关单副本", "CF - Customs Declaration Copy")]
        CF = 3,
        /// <summary>
        /// 补料信息
        /// </summary>
        [MemberDescription("SIR", "SI - 补料信息", "SI - SI Staff")]
        SI = 4,
        /// <summary>
        /// 客户账单要求
        /// </summary>
        [MemberDescription("ARR -客户账单要求", "ARR -Accounts Receivable Requirement")]
        ARR = 5,
        /// <summary>
        /// MBL副本
        /// </summary>
        [MemberDescription("MBLR", "MBL - MBL副本", "MBL - Master BL Copy")]
        MBL = 6,
        /// <summary>
        /// HBL副本
        /// </summary>
        [MemberDescription("HBL - HBL副本", "HBL - House BL Copy")]
        HBL = 7,
        /// <summary>
        /// 客户确认补料
        /// </summary>
        [MemberDescription("SID - 客户确认补料", "SID - Customer Confirmed SI")]
        SID = 8,
        /// <summary>
        /// 进口安全申报
        /// </summary>
        [MemberDescription("ISF - 进口安全申报", "ISF - Importer Security Filing")]
        ISF = 9,
        /// <summary>
        /// 其它
        /// </summary>
        [MemberDescription("其它", "Other")]
        Other = 10,
        /// <summary>
        /// 应收账单
        /// </summary>
        [MemberDescription("AR - 应收账单", "AR - Accounts Receivable")]
        AR = 11,
        /// <summary>
        /// 应付账单
        /// </summary>
        [MemberDescription("APR", "AP - 应付账单", "AP - Accounts Payable")]
        AP = 12,
        /// <summary>
        /// 代理账单
        /// </summary>
        [MemberDescription("DC - 代理账单", "DC - Debit Credit")]
        DC = 13,
        /// <summary>
        /// 调整的SO副本
        /// </summary>
        [MemberDescription("ASO - 调整的SO副本", "ASO - Adjusted SO Copy")]
        ASO = 14,
        /// <summary>
        /// 客户订舱单
        /// </summary>
        [MemberDescription("BKG - 客户订舱单", "BKG - Customer Booking")]
        BKG = 15,
        /// <summary>
        /// 电放保函
        /// </summary>
        [MemberDescription("LGTLX - 电放保函", "LGTLX - Letter of Telex Guarantee")]
        LGTLX = 16,
        /// <summary>
        /// 包装固定保函
        /// </summary>
        [MemberDescription("LGPKG - 包装固定保函", "LGPKG - Letter of Package Guarantee")]
        LGPKG = 17,
        /// <summary>
        /// 非危险品保函
        /// </summary>
        [MemberDescription("LGDC - 非危险品保函", "LGDC - Letter of Non Dangerous Cargo Guarantee")]
        LGDC = 18,
        /// <summary>
        /// 压单保函
        /// </summary>
        [MemberDescription("LGPBL - 压单保函", "LGPBL - Letter of Pledge BL Guarantee")]
        LGPBL = 19,
        /// <summary>
        /// 改单保函
        /// </summary>
        [MemberDescription("LGABL - 改单保函", "LGABL - Letter of Alter BL Guarantee")]
        LGABL = 20,
        /// <summary>
        /// 并单保函
        /// </summary>
        [MemberDescription("LGMBL - 并单保函", "LGMBL - Letter of Merge BL Guarantee")]
        LGMBL = 21,
        /// <summary>
        /// 包装固定保函(CO)
        /// </summary>
        [MemberDescription("LGPKG1 - 包装固定保函(CO)", "LGPKG1 - (CO)Letter of Package Guarantee")]
        LGPKG1 = 22,
        /// <summary>
        /// 非危险品保函(CO)
        /// </summary>
        [MemberDescription("LGDC1 - 非危险品保函(CO)", "LGDC1 - (CO)Letter of Non Dangerous Cargo Guarantee")]
        LGDC1 = 23,
        /// <summary>
        /// 改单保函(CO)
        /// </summary>
        [MemberDescription("LGABL1 - 改单保函(CO)", "LGABL1 - (CO)Letter of Alter BL Guarantee")]
        LGABL1 = 24,
        /// <summary>
        /// 并单保函(CO)
        /// </summary>
        [MemberDescription("LGMBL1 - 并单保函(CO)", "LGMBL1 - (CO)Letter of Merge BL Guarantee")]
        LGMBL1 = 25,
        /// <summary>
        /// 电放保函(CO)
        /// </summary>
        [MemberDescription("LGTLX1 - 电放保函(CO)", "LGTLX1 - (CO)Letter of Telex Guarantee")]
        LGTLX1 = 26,
        /// <summary>
        /// 美国舱单系统
        /// </summary>
        [MemberDescription("AMS - 美国舱单系统", "AMS - America Manifest System")]
        AMS = 27,
        /// <summary>
        /// 到港通知书(To Customer)
        /// </summary>
        [MemberDescription("AN - 到港通知书(To Customer)", "AN - Arrival Notice(To Customer)")]
        AN = 28,
        /// <summary>
        /// SIMBL - MBL副本
        /// </summary>
        [MemberDescription("SIMBL - MBL副本", "SIMBL - MBL Copy")]
        SIMBL = 29,
        /// <summary>
        /// SIHBL - HBL副本
        /// </summary>
        [MemberDescription("SIHBL - HBL副本", "SIHBL - HBL Copy")]
        SIHBL = 30,
        /// <summary>
        /// 到港通知书(From Carrier)
        /// </summary>
        [MemberDescription("AN_C - 到港通知书(From Carrier)", "AN_C - Arrival Notice(From Carrier)")]
        AN_C = 31,
        /// <summary>
        /// NRAS
        /// </summary>
        [MemberDescription("NRAS", "NRAS")]
        NRAS = 32,
        /// <summary>
        /// 报价单
        /// </summary>
        [MemberDescription("QuotedPrice", "报价单")]
        QuotedPrice = 33,
        /// <summary>
        /// 客户收货签收单
        /// </summary>
        [MemberDescription("POD - 客户收货签收单", "POD - Customer Receipt")]
        POD = 34,
        /// <summary>
        /// 账款控制
        /// </summary>
        [MemberDescription("应收账款控制", "Accounts Control")]
        AC = 35,
        /// <summary>
        /// 水单
        /// </summary>
        [MemberDescription("水单列表", "BankReceipt")]
        BR = 36,
        /// <summary>
        /// 流程附件
        /// </summary>
        [MemberDescription("流程附件", "WorkFlowFile")]
        WFF = 37,
        /// <summary>
        /// 报关清单
        /// </summary>
        [MemberDescription("报关清单", "CustomsInvoice")]
        CI = 38,

        /// <summary>
        /// 打包清单
        /// </summary>
        [MemberDescription("打包清单", "PackingList")]
        PL = 39,

        /// <summary>
        /// 装货单
        /// </summary>
        [MemberDescription("装货单", "PurchaseOrder")]
        PO = 40,

        /// <summary>
        /// 申报材料
        /// </summary>
        [MemberDescription("申报材料", "DeclarationMaterial")]
        DM = 41,

    } 
    #endregion
}
