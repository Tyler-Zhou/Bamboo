using System;
using System.Collections.Generic;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.ReportCenter.ServiceInterface.DataObjects
{

    #region Report
    /// <summary>
    /// 报表的业务类型
    /// </summary>
    [Serializable]
    public class ReportOperationType
    {
        public List<string> ValueList { get; set; }
        public string Value{ get; set; }
        public string EName { get; set; }
        public string CName { get; set; }
        public Guid ID { get; set; }
        public Guid? ParentID { get; set; }

        public bool HasContainer { get; set; }
        public bool HasShipLine { get; set; }
    }

    /// <summary>
    /// 报表分组类型
    /// </summary>
    [Serializable]
    public class ReportGroupType
    {
        public List<string> UnContainsTypes { get; set; }
        public Guid  ID { get; set; }
        public string EName { get; set; }
        public string CName { get; set; }
    }
    #endregion

    #region 外币式科目余额表
    /// <summary>
    /// 外币式科目余额信息
    /// </summary>
    [Serializable]
    public class FCGLBalanceData
    {
        public Guid? ParentID { get; set; }
        /// <summary>
        /// 科目ID
        /// </summary>
        public Guid? GLID
        {
            get;
            set;
        }

        /// <summary>
        ///  科目代码
        /// </summary>
        public string GLCode
        {
            get;
            set;
        }
        /// <summary>
        /// 科目名称
        /// </summary>
        public string GLName
        {
            get;
            set;
        }
        /// <summary>
        /// 科目类型
        /// </summary>
        public GLCodeType GLCodeType
        {
            get;
            set;
        }
        /// <summary>
        /// 科目类型名称
        /// </summary>
        public string GLCodeTypeName
        {
            get;
            set;
        }
        /// <summary>
        /// 科目级别
        /// </summary>
        public int LevelCode { get; set; }

        #region 期初数据

        /// <summary>
        /// 期初方向
        /// </summary>
        public string BeginningDir
        {
            get;
            set;
        }

        /// <summary>
        /// 期初外币金额
        /// </summary>
        public decimal? BeginningOrgAmt
        {
            get;
            set;
        }

        /// <summary>
        /// 期初金额
        /// </summary>
        public decimal? BeginningBalance
        {
            get;
            set;
        }
        #endregion

        #region 本期发生借方
        /// <summary>
        /// 本期发生借方
        /// </summary>
        public decimal? Debit
        {
            get;
            set;
        }

        /// <summary>
        /// 本期发生借方外币
        /// </summary>
        public decimal? DebitOrgAmt
        {
            get;
            set;
        }
        #endregion

        #region 本期发生贷方
        /// <summary>
        /// 本期发生贷方
        /// </summary>
        public decimal? Credit
        {
            get;
            set;
        }

        /// <summary>
        /// 本期发生贷方外币
        /// </summary>
        public decimal? CreditOrgAmt
        {
            get;
            set;
        }
        #endregion

        #region 期末数据

        /// <summary>
        /// 期末方向
        /// </summary>
        public string TermEndDir
        {
            get;
            set;
        }

        /// <summary>
        /// 期末外币金额
        /// </summary>
        public decimal? TermEndOrgAmt
        {
            get;
            set;
        }

        /// <summary>
        /// 期末金额
        /// </summary>
        public decimal? TermEndBalance
        {
            get;
            set;
        }

        #endregion

        private string indexNo;
        /// <summary>
        /// 排序字段
        /// </summary>
        public string IndexNo
        {
            get
            {
                if (string.IsNullOrEmpty(indexNo))
                {
                    indexNo = GLCode;
                }
                return indexNo;
            }
            set
            {
                indexNo = value;
            }
        }
    }
    #endregion

    #region 外币式科目明细表
    /// <summary>
    /// 外币式科目明细明细信息
    /// </summary>
    [Serializable]
    public class FCGLDetailData
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid? ID
        {
            get;
            set;
        }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? Date
        {
            get;
            set;
        }
        /// <summary>
        /// 月份
        /// </summary>
        public string Month
        {
            get
            {
                if (Date == null)
                {
                    return null;
                }
                else
                {
                    return Date.Value.Year.ToString() + Date.Value.Month.ToString();
                }
            }
        }

        /// <summary>
        /// 科目ID
        /// </summary>
        public Guid? GLID
        {
            get;
            set;
        }
        /// <summary>
        /// 单号
        /// </summary>
        public string VoucherNo
        {
            get;
            set;
        }
        /// <summary>
        /// 摘要
        /// </summary>
        public string Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 币种/汇率
        /// </summary>
        public string FC_Rate
        {
            get;
            set;
        }

        /// <summary>
        /// 方向(1借方、2贷方)
        /// </summary>
        public GLCodeProperty Direction
        {
            get;
            set;
        }
        /// <summary>
        /// 方向描述
        /// </summary>
        public string DirectionName
        {
            get
            {
                string text = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<GLCodeProperty>(Direction, LocalData.IsEnglish);
                if (!string.IsNullOrEmpty(text))
                {
                    text = text.Replace("方", "");
                }
                return text;
            }
        }
        /// <summary>
        /// 借方外币
        /// </summary>
        public decimal? DebitOrgAmt
        {
            get;
            set;
        }
        /// <summary>
        /// 借方
        /// </summary>
        public decimal? Debit
        {
            get;
            set;
        }
        /// <summary>
        /// 贷方外币
        /// </summary>
        public decimal? CreditOrgAmt
        {
            get;
            set;
        }
        /// <summary>
        /// 贷方
        /// </summary>
        public decimal? Credit
        {
            get;
            set;
        }
        /// <summary>
        /// 余额外币
        /// </summary>
        public decimal? BalanceOrgAmt
        {
            get;
            set;
        }
        /// <summary>
        /// 余额汇率
        /// </summary>
        public decimal? BalanceRate
        {
            get;
            set;
        }
        /// <summary>
        /// 余额
        /// </summary>
        public decimal? Balance
        {
            get;
            set;
        }

    }
    #endregion

    #region 资产负债表
    /// <summary>
    /// 资产负债表
    /// </summary>
    [Serializable]
    public class BalanceSheet
    {
        public decimal? BeginAmount1 { get; set; }
        public decimal? Amount1 { get; set; }
        public decimal? BeginAmount6 { get; set; }
        public decimal? Amount6 { get; set; }
        public decimal? BeginAmount7 { get; set; }
        public decimal? Amount7 { get; set; }
        public decimal? BeginAmount8 { get; set; }
        public decimal? Amount8 { get; set; }
        public decimal? BeginAmount11 { get; set; }
        public decimal? Amount11 { get; set; }
        public decimal? BeginAmount31 { get; set; }
        public decimal? Amount31 { get; set; }
        public decimal? BeginAmount39 { get; set; }
        public decimal? Amount39 { get; set; }
        public decimal? BeginAmount40 { get; set; }
        public decimal? Amount40 { get; set; }
        public decimal? BeginAmount41 { get; set; }
        public decimal? Amount41 { get; set; }
        public decimal? BeginAmount50 { get; set; }
        public decimal? Amount50 { get; set; }
        public decimal? BeginAmount51 { get; set; }
        public decimal? Amount51 { get; set; }
        public decimal? BeginAmount60 { get; set; }
        public decimal? Amount60 { get; set; }
        public decimal? BeginAmount67 { get; set; }
        public decimal? Amount67 { get; set; }
        public decimal? BeginAmount70 { get; set; }
        public decimal? Amount70 { get; set; }
        public decimal? BeginAmount71 { get; set; }
        public decimal? Amount71 { get; set; }
        public decimal? BeginAmount72 { get; set; }
        public decimal? Amount72 { get; set; }
        public decimal? BeginAmount73 { get; set; }
        public decimal? Amount73 { get; set; }
        public decimal? BeginAmount74 { get; set; }
        public decimal? Amount74 { get; set; }
        public decimal? BeginAmount76 { get; set; }
        public decimal? Amount76 { get; set; }
        public decimal? BeginAmount78 { get; set; }
        public decimal? Amount78 { get; set; }
        public decimal? BeginAmount100 { get; set; }
        public decimal? Amount100 { get; set; }
        public decimal? BeginAmount111 { get; set; }
        public decimal? Amount111 { get; set; }
        public decimal? BeginAmount115 { get; set; }
        public decimal? Amount115 { get; set; }
        public decimal? BeginAmount121 { get; set; }
        public decimal? Amount121 { get; set; }
        public decimal? BeginAmount122 { get; set; }
        public decimal? Amount122 { get; set; }
        public decimal? BeginAmount135 { get; set; }
        public decimal? Amount135 { get; set; }

        public decimal? BeginAmount2 { get; set; }
        public decimal? Amount2 { get; set; }
        public decimal? BeginAmount3 { get; set; }
        public decimal? Amount3 { get; set; }
        public decimal? BeginAmount4 { get; set; }
        public decimal? Amount4 { get; set; }
        public decimal? BeginAmount5 { get; set; }
        public decimal? Amount5 { get; set; }
        public decimal? BeginAmount9 { get; set; }
        public decimal? Amount9 { get; set; }
        public decimal? BeginAmount10 { get; set; }
        public decimal? Amount10 { get; set; }
        public decimal? BeginAmount21 { get; set; }
        public decimal? Amount21 { get; set; }
        public decimal? BeginAmount24 { get; set; }
        public decimal? Amount24 { get; set; }
        public decimal? BeginAmount32 { get; set; }
        public decimal? Amount32 { get; set; }
        public decimal? BeginAmount34 { get; set; }
        public decimal? Amount34 { get; set; }
        public decimal? BeginAmount38 { get; set; }
        public decimal? Amount38 { get; set; }
        public decimal? BeginAmount44 { get; set; }
        public decimal? Amount44 { get; set; }
        public decimal? BeginAmount45 { get; set; }
        public decimal? Amount45 { get; set; }
        public decimal? BeginAmount46 { get; set; }
        public decimal? Amount46 { get; set; }
        public decimal? BeginAmount52 { get; set; }
        public decimal? Amount52 { get; set; }
        public decimal? BeginAmount53 { get; set; }
        public decimal? Amount53 { get; set; }
        public decimal? BeginAmount68 { get; set; }
        public decimal? Amount68 { get; set; }
        public decimal? BeginAmount69 { get; set; }
        public decimal? Amount69 { get; set; }
        public decimal? BeginAmount75 { get; set; }
        public decimal? Amount75 { get; set; }
        public decimal? BeginAmount77 { get; set; }
        public decimal? Amount77 { get; set; }
        public decimal? BeginAmount79 { get; set; }
        public decimal? Amount79 { get; set; }
        public decimal? BeginAmount80 { get; set; }
        public decimal? Amount80 { get; set; }
        public decimal? BeginAmount101 { get; set; }
        public decimal? Amount101 { get; set; }
        public decimal? BeginAmount102 { get; set; }
        public decimal? Amount102 { get; set; }
        public decimal? BeginAmount103 { get; set; }
        public decimal? Amount103 { get; set; }
        public decimal? BeginAmount106 { get; set; }
        public decimal? Amount106 { get; set; }
        public decimal? BeginAmount108 { get; set; }
        public decimal? Amount108 { get; set; }
        public decimal? BeginAmount110 { get; set; }
        public decimal? Amount110 { get; set; }
        public decimal? BeginAmount116 { get; set; }
        public decimal? Amount116 { get; set; }
        public decimal? BeginAmount117 { get; set; }
        public decimal? Amount117 { get; set; }
        public decimal? BeginAmount118 { get; set; }
        public decimal? Amount118 { get; set; }
        public decimal? BeginAmount119 { get; set; }
        public decimal? Amount119 { get; set; }
        public decimal? BeginAmount120 { get; set; }
        public decimal? Amount120 { get; set; }
    }
    #endregion

    #region 资产负债表(公司汇总)
    /// <summary>
    /// 资产负债表（公司汇总）
    /// </summary>
    [Serializable]
    public class CompanyBalanceSheet
    {

        /// <summary>
        /// 科目名称
        /// </summary>
        public string GLName
        {
            get;
            set;
        }
        /// <summary>
        /// 科目类型
        /// </summary>
        public int GLType
        {
            get;
            set;
        }

        public int IndexNo { get; set; }
 

        #region 公司期末数

        public decimal? YDAmount
        {
            get;
            set;
        }

        public decimal? SZAmount
        {
            get;
            set;
        }

        public decimal? GZAmount
        {
            get;
            set;
        }

        public decimal? XMAmount
        {
            get;
            set;
        }

        public decimal? SHAmount
        {
            get;
            set;
        }

        public decimal? NBAmount
        {
            get;
            set;
        }

        public decimal? CQAmount
        {
            get;
            set;
        }

        public decimal? TJAmount
        {
            get;
            set;
        }

        public decimal? QDAmount
        {
            get;
            set;
        }

        public decimal? DLAmount
        {
            get;
            set;
        }
        #endregion
    }
    #endregion

    #region 资产负债表(集团汇总)
    /// <summary>
    /// 资产负债表（集团汇总）
    /// </summary>
    [Serializable]
    public class CompanyBalanceSheetAll
    {

        /// <summary>
        /// TopCname
        /// </summary>
        public string TopCname
        {
            get;
            set;
        }
        /// <summary>
        /// TopEname
        /// </summary>
        public string TopEname
        {
            get;
            set;
        }

        public int IndexNo { get; set; }

        /// <summary>
        /// ParentCname
        /// </summary>
        public string ParentCname
        {
            get;
            set;
        }

        /// <summary>
        /// ParentEname
        /// </summary>
        public string ParentEname
        {
            get;
            set;
        }

        /// <summary>
        /// GroupCname
        /// </summary>
        public string GroupCname
        {
            get;
            set;
        }

        /// <summary>
        /// GroupEname
        /// </summary>
        public string GroupEname
        {
            get;
            set;
        }

        /// <summary>
        /// GroupID
        /// </summary>
        public string GroupID
        {
            get;
            set;
        }

        /// <summary>
        /// GroupParentID
        /// </summary>
        public string GroupParentID
        {
            get;
            set;
        }

        /// <summary>
        /// CompanyId
        /// </summary>
        public string CompanyId
        {
            get;
            set;
        }

        /// <summary>
        /// CompanyName
        /// </summary>
        public string CompanyName
        {
            get;
            set;
        }

        /// <summary>
        /// ShortCode
        /// </summary>
        public string ShortCode
        {
            get;
            set;
        }

        /// <summary>
        /// BalanceAmount
        /// </summary>
        public decimal? BalanceAmount
        {
            get;
            set;
        }

        /// <summary>
        /// DistrictName
        /// </summary>
        public string DistrictName
        {
            get;
            set;
        }

        /// <summary>
        /// CurrencyID
        /// </summary>
        public string CurrencyID
        {
            get;
            set;
        }

        /// <summary>
        /// CurrencyCode
        /// </summary>
        public string CurrencyCode
        {
            get;
            set;
        }
    }
    #endregion

    #region 费用分析表
    /// <summary>
    /// 费用分析表
    /// </summary>
    [Serializable]
    public class ExpenseAnalysisSheet
    {
        /// <summary>
        /// 科目ID
        /// </summary>
        public Guid? GLID
        {
            get;
            set;
        }

        /// <summary>
        ///  科目代码
        /// </summary>
        public string GLCode
        {
            get;
            set;
        }
        /// <summary>
        /// 科目名称
        /// </summary>
        public string GLName
        {
            get;
            set;
        }
        /// <summary>
        /// 月初数
        /// </summary>
        public decimal? LastMonth
        {
            get;
            set;
        }
        /// <summary>
        /// 本月发生额
        /// </summary>
        public decimal? ThisMonth
        {
            get;
            set;
        }
        /// <summary>
        /// 月累计数
        /// </summary>
        public decimal? MonthsOf
        {
            get;
            set;
        }
        //科目级别
        public int LevelCode
        {
            get;
            set;
        }
        /// <summary>
        /// 月预算值
        /// </summary>
        public decimal? MonthsBudget
        {
            get;
            set;
        }
        /// <summary>
        /// 月增长值
        /// </summary>
        public decimal? MonthsIncrease
        {
            get;
            set;
        }
        /// <summary>
        /// 月增长比例
        /// </summary>
        public decimal? MonthsScale
        {
            get;
            set;
        }

    }
    #endregion

    #region 费用分析表(公司汇总)
    /// <summary>
    /// 费用分析表(公司汇总)
    /// </summary>
    [Serializable]
    public class CompanyExpenseAnalysisSheet
    {
        /// <summary>
        /// 科目ID
        /// </summary>
        public Guid? GLID
        {
            get;
            set;
        }

        /// <summary>
        ///  科目代码
        /// </summary>
        public string GLCode
        {
            get;
            set;
        }
        /// <summary>
        /// 科目名称
        /// </summary>
        public string GLName
        {
            get;
            set;
        }
        /// <summary>
        /// 月初数
        /// </summary>
        public decimal? LastMonth
        {
            get;
            set;
        }
        /// <summary>
        /// 本月发生额
        /// </summary>
        public decimal? ThisMonth
        {
            get;
            set;
        }
        /// <summary>
        /// 月累计数
        /// </summary>
        public decimal? MonthsOf
        {
            get;
            set;
        }
        /// <summary>
        /// 科目级别
        /// </summary>
        public int LevelCode
        {
            get;
            set;
        }


        #region 公司月发生数

        public decimal? 远东区办公室
        {
            get;
            set;
        }

        public decimal? 深圳公司
        {
            get;
            set;
        }

        public decimal? 广州公司
        {
            get;
            set;
        }

        public decimal? 厦门公司
        {
            get;
            set;
        }

        public decimal? 上海公司
        {
            get;
            set;
        }

        public decimal? 宁波公司
        {
            get;
            set;
        }

        public decimal? 重庆公司
        {
            get;
            set;
        }

        public decimal? 天津公司
        {
            get;
            set;
        }

        public decimal? 青岛公司
        {
            get;
            set;
        }

        public decimal? 大连公司
        {
            get;
            set;
        }

        public decimal? 连云港公司
        {
            get;
            set;
        }
        #endregion
    }
    #endregion

    #region 周日期对象
    /// <summary>
    /// 周日期对象
    /// </summary>
    public class DateWeekly
    {
        /// <summary>
        /// 顺序
        /// </summary>
        public Int32 Index
        {
            get;
            set;
        }
        /// <summary>
        /// 开始日期 
        /// </summary>
        public DateTime StartDate
        {
            get;
            set;
        }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate
        {
            get;
            set;
        }
        /// <summary>
        /// 年份
        /// </summary>
        public Int32 Year
        {
            get;
            set;
        }
        /// <summary>
        /// 周数
        /// </summary>
        public Int32 Weekly
        {
            get;
            set;
        }
        private string itemID;
        /// <summary>
        /// 当前项ID
        /// </summary>
        public string ItemID
        {
            get
            {
                if (!string.IsNullOrEmpty(itemID))
                {
                    return itemID;
                }
                return "#" + Year + "-" + Weekly;
            }
            set
            {
                itemID = value;
            }

        }
        private string itemName;
        /// <summary>
        /// 显示名称
        /// </summary>
        public string ItemName
        {
            get
            {
                if (!string.IsNullOrEmpty(itemName))
                {
                    return itemName;
                }
                return "#" + Year + "-" + Weekly + "( " + StartDate.ToShortDateString() + "~" + EndDate.ToShortDateString() + " )";
            }
            set
            {
                itemName = value;
            }
        }

    }
    #endregion

    #region  利润表
    /// <summary>
    /// 利润表(公司)
    /// </summary>
    [Serializable]
    public class ProfitDetailReport
    {
        public string GLType { get; set; }
        public string GLName { get; set; }
        public int IndexNo { get; set; }
        public decimal? Amount { get; set; }
        public decimal? USDAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public decimal? BalanceUSDAmount { get; set; }
    }
    public class ProfitTotalReport
    {

        /// <summary>
        /// 科目名称
        /// </summary>
        public string GLName
        {
            get;
            set;
        }
        /// <summary>
        /// 科目类型
        /// </summary>
        public string GLType
        {
            get;
            set;
        }

        #region 本月数

        public decimal? MYDAmount
        {
            get;
            set;
        }

        public decimal? MSZAmount
        {
            get;
            set;
        }

        public decimal? MGZAmount
        {
            get;
            set;
        }

        public decimal? MXMAmount
        {
            get;
            set;
        }

        public decimal? MSHAmount
        {
            get;
            set;
        }

        public decimal? MNBAmount
        {
            get;
            set;
        }

        public decimal? MCQAmount
        {
            get;
            set;
        }

        public decimal? MTJAmount
        {
            get;
            set;
        }

        public decimal? MQDAmount
        {
            get;
            set;
        }

        public decimal? MDLAmount
        {
            get;
            set;
        }
        #endregion

        #region 本年数

        public decimal? YYDAmount
        {
            get;
            set;
        }

        public decimal? YSZAmount
        {
            get;
            set;
        }

        public decimal? YGZAmount
        {
            get;
            set;
        }

        public decimal? YXMAmount
        {
            get;
            set;
        }

        public decimal? YSHAmount
        {
            get;
            set;
        }

        public decimal? YNBAmount
        {
            get;
            set;
        }

        public decimal? YCQAmount
        {
            get;
            set;
        }

        public decimal? YTJAmount
        {
            get;
            set;
        }

        public decimal? YQDAmount
        {
            get;
            set;
        }

        public decimal? YDLAmount
        {
            get;
            set;
        }
        #endregion
    }
    #endregion

    #region 利润分配表
    /// <summary>
    /// 利润分配(公司)
    /// </summary>
    [Serializable]
    public class ProfitAllocationDetailReport
    {
        public string GLName { get; set; }
        public string IndexNo { get; set; }
        public decimal? YearAmount { get; set; }
        public decimal? LastYearAmount { get; set; }
    }
    #endregion

    
}
