using System;
using System.Collections.Generic;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FAM.UI.CustomerBill.Print
{
    public class BillReportConfigConstants
    {
        public const string CustomerBillConfig = "CustomerBillConfig";

        /// <summary>
        /// 签名栏默认键
        /// </summary>
        public const string Signature = "Signature";

        /// <summary>
        /// 备注第一段
        /// </summary>
        public const string BillBankInfo_CN = "BillBankInfo_CN";
        /// <summary>
        /// 备注第一段
        /// </summary>
        public const string BillBankInfo_EN = "BillBankInfo_EN";

        /// <summary>
        /// 备注第三段
        /// </summary>
        public const string BillRemarkInfo_EN = "BillRemarkInfo_EN";
        /// <summary>
        /// 备注第三段
        /// </summary>
        public const string BillRemarkInfo_CN = "BillRemarkInfo_CN";

        /// <summary>
        /// 是否显示后缀号的Code
        /// </summary>
        public const string ShowSuffix = "ShowSuffix";

        /// <summary>
        /// 备注第二段(汇率信息)是否要隐藏
        /// </summary>
        public const string RateInfoIsHide = "RateInfoIsHide";

        /// <summary>
        /// 更多额外备注，黑色粗体显示在帐单最下方
        /// </summary>
        public const string MoreRemark = "MoreRemark";

        /// <summary>
        /// 公司Loga配置的代码
        /// </summary>
        public const string Logo = "Logo";

        /// <summary>
        /// CRDR_ReportName Code
        /// </summary>
        public const string CR_ReportName = "CR_ReportName";
        /// <summary>
        /// DR_ReportName Code
        /// </summary>
        public const string DR_ReportName = "DR_ReportName";
        /// <summary>
        /// CRDR_ReportName Code
        /// </summary>
        public const string DRCR_ReportName = "DRCR_ReportName";


        /// <summary>
        /// CR_NOTE Code
        /// </summary>
        public const string CR_NOTE = "CR_NOTE";
        /// <summary>
        /// DR_NOTE Code
        /// </summary>
        public const string DR_NOTE = "DR_NOTE";
        /// <summary>
        /// DRCR_NOTE Code
        /// </summary>
        public const string DRCR_NOTE = "DRCR_NOTE";

    }

    /// <summary>
    /// 报表名字
    /// </summary>
    public enum BillReportNameEnum
    {
        BillReport,
        LocalInvoiceDR,
        LocalInvoiceCR,
        LocalInvoiceDRCR,
    }

    public enum BillReportLogaEnum
    {
        CEC,
        CITYOCEAN,
        MANDARIN,
        TOPSHIPPING,
    }

    public class BillReportHelper
    {
        /// <summary>
        /// 根据报表名字枚举取得报表
        /// </summary>
        /// <param name="name">BillReportNameEnum</param>
        /// <returns>ReportName</returns>
        public static string GetReportPathByBillReportNameEnum(BillReportNameEnum name)
        {
            if (name == BillReportNameEnum.BillReport)
            {
                if (LocalData.IsEnglish) return name.ToString() + "_EN.frx";
                else return name.ToString() + "_CN.frx";
            }
            else return name.ToString() + ".frx";
        }
    }


    #region 客户端对象 PrintBillConfigData

    public class PrintBillConfigData
    {
        /// <summary>
        /// BillID
        /// </summary>
        public Guid BillID { get; set; }

        /// <summary>
        /// 表头 "INVOICE", "DEBIT NOTE", "CREDIT NOTE" 三种
        /// </summary>
        public string BillNote { get; set; }
        /// <summary>
        /// 发给BillToDescription
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// 一般是公司名
        /// </summary>
        public string TitelCompanyName { get; set; }
        /// <summary>
        /// 一般是公司地址传真电话等信息
        /// </summary>
        public string TitelCompanyDes { get; set; }
        /// <summary>
        /// 报表最后的 印章
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// 马来西亚用到的序列号
        /// </summary>
        public string SuffixNo { get; set; }

        /// <summary>
        /// 只有海出要用到
        /// </summary>
        public PrintBillType PrintBillType { get; set; }

        /// <summary>
        /// 只有海出要用到
        /// </summary>
        public PrintBillShipType PrintBillShipType { get; set; }
        /// <summary>
        /// 用于确定Logo的文件路径名
        /// </summary>
        public string LogoFileName { get; set; }


        /// <summary>
        /// 用逗号隔开的提单号
        /// </summary>
        public string BLNO { get; set; }

        /// <summary>
        /// 三段拼装的备注1
        /// </summary>
        public string BankInfo { get; set; }

        /// <summary>
        /// 三段拼装的备注2
        /// </summary>
        public string RateInfo { get; set; }

        /// <summary>
        /// 三段拼装的备注3
        /// </summary>
        public string RemarkInfo { get; set; }

        public bool IsShowMainCurrency { get; set; }

        public bool IsShowFETA { get; set; }

        public string PrintData { get; set; }

        /// <summary>
        /// 用于打印配置中更变公司后缓存该公司的信息
        /// </summary>
        public PrintBillCacheInfo PrintBillCacheInfo { get; set; }

        /// <summary>
        /// 标识所选的是否英文
        /// </summary>
        public bool IsEN { get; set; }

        /// <summary>
        /// 是否试算
        /// </summary>
        public bool IsTrial { get; set; }

        /// <summary>
        /// 税率
        /// </summary>
        public Decimal TaxRate { get; set; }

        /// <summary>
        /// 是否分币种
        /// </summary>
        public bool IsSepc { get; set; }
    }

    /// <summary>
    /// 打印帐单的类型(1Normal,2Commission
    /// </summary>
    public enum PrintBillType
    {
        /// <summary>
        /// 普通
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 佣金
        /// </summary>
        Commission = 1
    }

    /// <summary>
    /// 打印帐单要显示的船运类型
    /// </summary>
    public enum PrintBillShipType
    {
        /// <summary>
        /// 大船
        /// </summary>
        Ship,
        /// <summary>
        /// 驳船
        /// </summary>
        PreShip,
    }

    /// <summary>
    /// 用于配置打印帐单缓存
    /// </summary>
    public class PrintBillCacheInfo
    {
        public List<SolutionExchangeRateList> RateList { get; set; }
        public ConfigureInfo ConfigureInfo { get; set; }
        public CompanyReportConfigureList ReportConfigures { get; set; }
        public CustomerInfo Customer { get; set; }
    }


    public class PrintBillTitelConfigData
    {
        public Guid CompanyID { get; set; }
        public string CompanyDsc { get; set; }
        public string CompanyName { get; set; }

        /// <summary>
        /// 口岸银行帐号
        /// </summary>
        public Guid CompanyBank { get; set; }
    }

    #endregion

    #region 客户端报表对象

    /// <summary>
    /// 费用明细
    /// </summary>
    public class ChargeItemReportData
    {
        /// <summary>
        /// 方向
        /// </summary>
        public FeeWay Way { get; set; }

        /// <summary>
        /// 费用名
        /// </summary>
        public string ChargeName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Qty { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal UnitPrice { get; set; }


        /// <summary>
        /// 应收减应付
        /// </summary>
        public decimal Amount { get; set; }

        public decimal TotalAmoint { get; set; }

        /// <summary>
        /// 收
        /// </summary>
        public decimal Debit { get; set; }

        /// <summary>
        /// 应收加GST税费
        /// </summary>
         public decimal DebitGST { get; set; }
        

        public string DebitForPrint
        {
            get
            {
                if (Debit == 0)
                {
                    return string.Empty;
                }

                return Debit.ToString("n");
            }
        }

        /// <summary>
        /// 付
        /// </summary>
        public decimal Credit { get; set; }

        public string CreditForPrint
        {
            get
            {
                if (Credit == 0)
                {
                    return string.Empty;
                }

                return Credit.ToString("n");
            }
        }


        public string ForPrint
        {
            get;
            set;
        }


        /// <summary>
        /// 汇率
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// 税率(马来西亚报表显示使用)
        /// </summary>
        public string TaxRate { get; set; }

        /// <summary>
        /// GST税费
        /// </summary>
        public decimal GstTaxRate { get; set; }
    }

    /// <summary>
    /// 费用合计对象
    /// </summary>
    public class FeeTotalInfo
    {
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// 收
        /// </summary>
        public decimal Debit { get; set; }
        /// <summary>
        /// 付
        /// </summary>
        public decimal Credit { get; set; }

        public decimal SumTotal { get; set; }

        /// <summary>
        /// 已核销金额
        /// </summary>
        public decimal PadAmount { get; set; }

        /// <summary>
        /// GST税费
        /// </summary>
        public decimal GstTaxRate { get; set; }

        /// <summary>
        /// 汇总(包括GST)
        /// </summary>
        public decimal TotalGst { get; set; }

    }

    #endregion

    #region 打印国内帐单的报表对象
    /// <summary>
    /// 打印国内帐单的对象
    /// </summary>
    [Serializable]
    public class CommonBillReportSource : CommonBillReportData
    {
        /// <summary>
        /// 我公司的名称　
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 我公司的地址
        /// </summary>
        public string CompanyDsc { get; set; }

        /// <summary>
        /// 马来西亚公司要的序列号
        /// </summary>
        public string SuffixNo { get; set; }

        /// <summary>
        /// 报表的标题
        /// </summary>
        public string BillNote { get; set; }


        /// <summary>
        /// 发货人描述
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// 帐单号
        /// </summary>
        public string BillNo { get; set; }

        /// <summary>
        /// 制单日期
        /// </summary>
        public string BillDate { get; set; }

        /// <summary>
        /// 提单号
        /// </summary>
        public string BLNo { get; set; }



        /// <summary>
        /// 通用备注
        /// </summary>
        public string CommonRemark { get; set; }

        /// <summary>
        /// 其它当前帐单的附属备注
        /// </summary>
        public string OtherRemark { get; set; }

        /// <summary>
        /// 更多额外备注，黑色粗体显示在帐单最下方
        /// </summary>
        public string MoreRemark { get; set; }

        /// <summary>
        /// 打印人
        /// </summary>
        public string PrintBy { get; set; }

        /// <summary>
        /// 打印人电话
        /// </summary>
        public string PrintByTel { get; set; }

        /// <summary>
        /// 打印人传真
        /// </summary>
        public string PrintByFax { get; set; }


        /// <summary>
        /// ETA的显示(主要是因为海进的ETA要显为FETA,其它为空,这个在应用服务端处理)
        /// </summary>
        public string ETACaption { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public OperationType OperationType { get; set; }

        /// <summary>
        /// 费用列表
        /// </summary>
        public List<ChargeItemReportData> Fees { get; set; }

        /// <summary>
        /// 图标的完全路径
        /// </summary>
        public string LogoPath { get; set; }


        /// <summary>
        /// GST(马来西亚报表显示使用)
        /// </summary>
        public string GST { get; set; }
    }

    #endregion

    #region 打印国外帐单的报表对象

    /// <summary>
    /// 打印国外帐单的报表对象
    /// </summary>
    [Serializable]
    public class LocalBillReportSource : LocalBillReportData
    {
        /// <summary>
        /// 我公司的名称地址电话传真拼装串
        /// </summary>
        public string CompanyDsc { get; set; }
        /// <summary>
        /// BillDate
        /// </summary>
        public string BillDate { get; set; }
        /// <summary>
        /// DueDate
        /// </summary>
        public string DueDate { get; set; }
        /// <summary>
        /// Trem
        /// </summary>
        public int Trem { get; set; }
        /// <summary>
        /// BillToCustomerDsc
        /// </summary>
        public string BillToCustomerDsc { get; set; }
        /// <summary>
        /// BillToCustomerName
        /// </summary>
        public string BillToCustomerName { get; set; }
        /// <summary>
        /// BillToCustomerAdd
        /// </summary>
        public string BillToCustomerAdd { get; set; }

        /// <summary>
        /// 巴西DEBIT NOTE用到
        /// </summary>
        public string BillToCustomerCity { get; set; }

        /// <summary>
        /// 巴西DEBIT NOTE用到
        /// </summary>
        public string BillToCustomerPostCode { get; set; }

        /// <summary>
        /// 巴西DEBIT NOTE用到
        /// </summary>
        public string LogoPathForBrazil { get; set; }

        /// <summary>
        /// CreateDate
        /// </summary>
        public string CreateDate { get; set; }
        /// <summary>
        /// PostDate
        /// </summary>
        public string PostDate { get; set; }
        /// <summary>
        /// ShipperTo
        /// </summary>
        public string ShipperTo { get; set; }
        /// <summary>
        /// BillNo
        /// </summary>
        public string BillNo { get; set; }
        /// <summary>
        /// OperationNo
        /// </summary>
        public string OperationNo { get; set; }

        /// <summary>
        /// 客户参考号
        /// </summary>
        public string CustomerRefNo { get; set; }

        /// <summary>
        /// 应收总金额 分币种
        /// </summary>
        public string DRTotalAmount { get; set; }
        /// <summary>
        /// 应付总金额 分币种
        /// </summary>
        public string CRTotalAmount { get; set; }
        /// <summary>
        /// 应收减应付余额 分币种
        /// </summary>
        public string CrAmount { get; set; }
        /// <summary>
        /// 应付收减应收余额 分币种
        /// </summary>
        public string APAmount { get; set; }
        /// <summary>
        /// 应收总金额 主币种
        /// </summary>
        public string TotalAmount { get; set; }
        /// <summary>
        /// 已核销总金额 分币种
        /// </summary>
        public string PaidAmount { get; set; }

        /// <summary>
        /// 应收减已核销余额 分币种
        /// </summary>
        public string FinAmount { get; set; }

        /// <summary>
        /// 泰国账单用
        /// </summary>
        public string VatTaxAmountForTHAILAND { get; set; }

        /// <summary>
        /// 泰国账单用
        /// </summary>
        public string SubTotalAmountForTHAILAND { get; set; }

        /// <summary>
        /// 泰国账单用
        /// </summary>
        public string TaxAmountForTHAILAND { get; set; }

        /// <summary>
        /// 泰国账单用
        /// </summary>
        public string NetTotalAmountForTHAILAND { get; set; }

        /// <summary>
        /// 泰国账单用,口岸的银行账号
        /// </summary>
        public string CompanyBankAccount { get; set; }

        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// CurrentUser
        /// </summary>
        public string CurrentUser { get; set; }
        /// <summary>
        /// MainCurrency
        /// </summary>
        public string MainCurrency { get; set; }

        /// <summary>
        /// 公司签名
        /// </summary>
        public string CompanySignature { get; set; }
    }

    #endregion
}
