using System;
using System.Collections.Generic;

namespace ICP.FAM.ServiceInterface.DataObjects
{
    /// <summary>
    /// 催款单报表数据
    /// </summary>
    [Serializable]
    public class BillDunReportData
    {
        /// <summary>
        /// 客户信息
        /// </summary>
        public BillDunReportDataCustomerInfo CustomerInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 公司信息
        /// </summary>
        public BillDunReportDataCompanyInfo CompanyInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 数据库中的费用信息
        /// </summary>
        public List<BillDunReportDataCostInfo> DBCostList
        {
            get;
            set;
        }
        /// <summary>
        /// 报表中的费用信息
        /// </summary>
        public List<BillDunReportDataChargeInfo> ReportChargeList
        {
            get;
            set;
        }
        /// <summary>
        /// 帐号信息
        /// </summary>
        public List<BillDunReportDataAccountInfo> AccountList
        {
            get;
            set;
        }
        /// <summary>
        /// 分组方式
        /// </summary>
        public string DunGroupType
        {
            get;
            set;
        }

        /// <summary>
        /// 目标币种名称
        /// </summary>
        public string ToCurrencyName
        {
            get;
            set;
        }
        /// <summary>
        /// 目标币种ID
        /// </summary>
        public Guid ToCurrencyID
        {
            get;
            set;
        }


    }

    [Serializable]
    public class BaseInfo
    {
        public string DunGroupType
        { 
            get;
            set; 
        }
        public string ToCurrencyName
        {
            get;
            set;
        }

        /// <summary>
        /// 美金金额
        /// </summary>
        public decimal TotalUSDAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 人民币金额
        /// </summary>
        public decimal TotalRMBAmount
        {
            get;
            set;
        }
        /// <summary>
        /// HKD金额
        /// </summary>
        public decimal TotalHKDAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 折合金额
        /// </summary>
        public decimal TotalToAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 美金金额
        /// </summary>
        public decimal TotalNotPaidUSDAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 人民币金额
        /// </summary>
        public decimal TotalNotPaidRMBAmount
        {
            get;
            set;
        }
        /// <summary>
        /// HKD金额
        /// </summary>
        public decimal TotalNotPaidHKDAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 折合金额
        /// </summary>
        public decimal TotalNotPaidToAmount
        {
            get;
            set;
        }
    }


    /// <summary>
    /// 客户信息
    /// </summary>
    [Serializable]
    public class BillDunReportDataCustomerInfo
    {
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName
        {
            get;
            set;
        }
        /// <summary>
        /// 客户地址
        /// </summary>
        public string CustomerAddress
        {
            get;
            set;
        }
        /// <summary>
        /// 客户联系人
        /// </summary>
        public string CustomerAttn
        {
            get;
            set;
        }
        /// <summary>
        /// 客户电话
        /// </summary>
        public string CustomerTel
        {
            get;
            set;
        }
        /// <summary>
        /// 客户传真
        /// </summary>
        public string CustomerFax
        {
            get;
            set;
        }

    }

    /// <summary>
    /// 公司信息
    /// </summary>
    [Serializable]
    public class BillDunReportDataCompanyInfo
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            get;
            set;
        }
        /// <summary>
        /// 公司地址
        /// </summary>
        public string CompanyAddress
        {
            get;
            set;
        }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
            get;
            set;
        }
        /// <summary>
        /// 用户电话
        /// </summary>
        public string UserTel
        {
            get;
            set;
        }
        /// <summary>
        /// 用户传真
        /// </summary>
        public string UserFax
        {
            get;
            set;
        }
        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName
        {
            get;
            set;
        }

    }

    /// <summary>
    /// 费用信息--接受数据库返回值
    /// </summary>
    [Serializable]
    public class BillDunReportDataCostInfo
    {
        /// <summary>
        /// 账单号
        /// </summary>
        public string BillNo
        {
            get;
            set;
        }
        /// <summary>
        /// 离港日
        /// </summary>
        public DateTime? ETD
        {
            get;
            set;
        }
        /// <summary>
        /// 到港日
        /// </summary>
        public DateTime? ETA
        {
            get;
            set;
        }
        /// <summary>
        /// 提单号
        /// </summary>
        public string BLNo
        {
            get;
            set;
        }
        /// <summary>
        /// 箱号
        /// </summary>
        public string ContainerNos
        {
            get;
            set;
        }
        /// <summary>
        /// 目的港
        /// </summary>
        public string PODName
        {
            get;
            set;
        }
        /// <summary>
        /// 费用名称
        /// </summary>
        public string ChargeName
        {
            get;
            set;
        }
        /// <summary>
        /// 币种ID
        /// </summary>
        public Guid CurrencyID
        {
            get;
            set;
        }
        /// <summary>
        /// 币种名称
        /// </summary>
        public string CurrencyName
        {
            get;
            set;
        }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal? Amount
        {
            get;
            set;
        }

        /// <summary>
        /// 未核销金额
        /// </summary>
        public decimal? UnPaidAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceNo
        {
            get;
            set;
        }

    }

    /// <summary>
    /// 帐单/费用信息-统计后的
    /// </summary>
    [Serializable]
    public class BillDunReportDataChargeInfo
    {
        /// <summary>
        /// 账单号
        /// </summary>
        public string BillNo
        {
            get;
            set;
        }
        /// <summary>
        /// 离港日
        /// </summary>
        public string ETD
        {
            get;
            set;
        }
        /// <summary>
        /// 到港日
        /// </summary>
        public string ETA
        {
            get;
            set;
        }
        /// <summary>
        /// 提单号
        /// </summary>
        public string BLNo
        {
            get;
            set;
        }
        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceNo
        {
            get;
            set;
        }
        /// <summary>
        /// 箱号
        /// </summary>
        public string ContainerNos
        {
            get;
            set;
        }
        /// <summary>
        /// 目的港
        /// </summary>
        public string PODName
        {
            get;
            set;
        }
        /// <summary>
        /// 费用名称
        /// </summary>
        public string ChargeName
        {
            get;
            set;
        }
        /// <summary>
        /// 美金金额
        /// </summary>
        public decimal? USDAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 人民币金额
        /// </summary>
        public decimal? RMBAmount
        {
            get;
            set;
        }
        /// <summary>
        /// HKD金额
        /// </summary>
        public decimal? HKDAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 折合金额
        /// </summary>
        public decimal? ToAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 美金金额(未核销)
        /// </summary>
        public decimal? USDUnpaidAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 人民币金额(未核销)
        /// </summary>
        public decimal? RMBUnpaidAmount
        {
            get;
            set;
        }
        /// <summary>
        /// HKD金额(未核销)
        /// </summary>
        public decimal? HKDUnpaidAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 折合金额(未核销)
        /// </summary>
        public decimal? ToUnpaidAmount
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 账号信息
    /// </summary>
    [Serializable]
    public class BillDunReportDataAccountInfo
    {
        /// <summary>
        /// 币种名称
        /// </summary>
        public string CurrencyName
        {
            get;
            set;
        }
        /// <summary>
        /// 账号
        /// </summary>
        public string AccountNo
        {
            get;
            set;
        }
    }



}
