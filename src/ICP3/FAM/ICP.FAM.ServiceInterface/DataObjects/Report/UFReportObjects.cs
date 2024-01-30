using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FAM.ServiceInterface.DataObjects
{
    #region 科目余额表
    /// <summary>
    /// 科目余额信息
    /// </summary>
    [Serializable]
    public class GLBalanceData
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
        /// 期初金额
        /// </summary>
        public decimal? BeginningDebit
        {
            get;
            set;
        }

        /// <summary>
        /// 期初金额
        /// </summary>
        public decimal? BeginningCredit
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
        /// 期末金额
        /// </summary>
        public decimal? TermEndDebit
        {
            get;
            set;
        }

        /// <summary>
        /// 期末金额
        /// </summary>
        public decimal? TermEndCredit
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
        /// <summary>
        /// 子节点数量(用来判断是否为未级科目)
        /// </summary>
        public int ChildCount
        {
            get;
            set;
        }
        public bool IsFCTotal { get; set; }
    }
    #endregion

    #region 科目明细表
    /// <summary>
    /// 科目明细明细信息
    /// </summary>
    [Serializable]
    public class GLDetailData
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid? ID
        {
            get;
            set;
        }
        public string GLCode
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
                    return Date.Value.Year.ToString("0000")+Date.Value.Month.ToString("00");
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
                string text= ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<GLCodeProperty>(Direction,LocalData.IsEnglish);
                if(!string.IsNullOrEmpty(text))
                {
                    text=text.Replace("方","");
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

    #region 客户科目余额表
    /// <summary>
    /// 客户科目余额表
    /// </summary>
    [Serializable]
    public class CustomerGLBalance
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
        /// 科目代码
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
        /// 客户ID
        /// </summary>
        public Guid? CustomerID
        {
            get;
            set;
        }
        /// <summary>
        /// 客户代码
        /// </summary>
        public string CustomerCode
        {
            get;
            set;
        }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName
        {
            get;
            set;
        }
        /// <summary>
        /// 期初方向(0平、1借方、2贷方)
        /// </summary>
        public GLCodeProperty BeginningDirection
        {
            get;
            set;
        }
        /// <summary>
        /// 期初方向描述
        /// </summary>
        public string BeginningDirectionName
        {
            get
            {
                string text = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<GLCodeProperty>(BeginningDirection, LocalData.IsEnglish);
                if (!string.IsNullOrEmpty(text))
                {
                    text = text.Replace("方", "");
                }
                return text;
            }
        }

        /// <summary>
        /// 期初外币余额
        /// </summary>
        public decimal? BeginningOrgAmt
        {
            get;
            set;
        }

        /// <summary>
        /// 期初余额
        /// </summary>
        public decimal? BeginningBalance
        {
            get;
            set;
        }
        /// <summary>
        /// 借方
        /// </summary>
        public decimal? PeriodDebit
        {
            get;
            set;
        }
        /// <summary>
        /// 贷方
        /// </summary>
        public decimal? PeriodCredit
        {
            get;
            set;
        }
        /// <summary>
        /// 借方外币
        /// </summary>
        public decimal? PeriodDebitOrgAmt
        {
            get;
            set;
        }
        /// <summary>
        /// 贷方外币
        /// </summary>
        public decimal? PeriodCreditOrgAmt
        {
            get;
            set;
        }
        /// <summary>
        /// 期末方向(0平、1借方、2贷方)
        /// </summary>
        public GLCodeProperty PeriodEndDirection
        {
            get;
            set;
        }
        /// <summary>
        /// 期末方向名称
        /// </summary>
        public string PeriodEndDirectionName
        {
            get
            {
                string text = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<GLCodeProperty>(PeriodEndDirection, LocalData.IsEnglish);
                if (!string.IsNullOrEmpty(text))
                {
                    text = text.Replace("方", "");
                }
                return text;
            }
        }
        /// <summary>
        /// 期未外币余额
        /// </summary>
        public decimal? PeriodEndOrgAmt
        {
            get;
            set;
        }
        /// <summary>
        /// 期未余额
        /// </summary>
        public decimal? PeriodEndBalance
        {
            get;
            set;
        }


    }

    #endregion

    #region 客户三栏余额表
    /// <summary>
    /// 客户三栏余额表
    /// </summary>
    [Serializable]
    public class Customer3ColumnGLBalance
    {
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
        /// 日期
        /// </summary>
        public DateTime? Date
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
        /// 借方
        /// </summary>
        public decimal? Debit
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
        /// 期末方向(0平、1借方、2贷方)
        /// </summary>
        public GLCodeProperty? PeriodEndDirection
        {
            get;
            set;
        }
        /// <summary>
        /// 期末方向名称
        /// </summary>
        public string PeriodEndDirectionName
        {
            get
            {
                if (PeriodEndDirection == null)
                {
                    return string.Empty;
                }
                string text = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<GLCodeProperty>(PeriodEndDirection.Value, LocalData.IsEnglish);
                if (!string.IsNullOrEmpty(text))
                {
                    text = text.Replace("方", "");
                }
                return text;
            }
        }
        /// <summary>
        /// 期未余额
        /// </summary>
        public decimal? PeriodEndBalance
        {
            get;
            set;
        }
    }
    #endregion

    #region 客户科目明细帐
    /// <summary>
    /// 客户科目明细帐
    /// </summary>
    [Serializable]
    public class CustomerGLDetail
    {
        /// <summary>
        /// 凭证ID
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
        /// 凭证号
        /// </summary>
        public string VoucherNo
        {
            get;
            set;
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
        /// 科目代码
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
        /// 客户ID
        /// </summary>
        public Guid? CustomerID
        {
            get;
            set;
        }
        /// <summary>
        /// 客户代码
        /// </summary>
        public string CustomerCode
        {
            get;
            set;
        }
        /// <summary>
        /// 客户名称 
        /// </summary>
        public string CustomerName
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
        /// 期末方向(0平、1借方、2贷方)
        /// </summary>
        public GLCodeProperty PeriodEndDirection
        {
            get;
            set;
        }
        /// <summary>
        /// 期末方向名称
        /// </summary>
        public string PeriodEndDirectionName
        {
            get
            {
                string text = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<GLCodeProperty>(PeriodEndDirection, LocalData.IsEnglish);
                if (!string.IsNullOrEmpty(text))
                {
                    text = text.Replace("方", "");
                }
                return text;
            }
        }
        /// <summary>
        /// 期未外币余额
        /// </summary>
        public decimal? PeriodEndOrgAmt
        {
            get;
            set;
        }
        /// <summary>
        /// 期未余额
        /// </summary>
        public decimal? PeriodEndBalance
        {
            get;
            set;
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int IndexNo
        {
            get;
            set;
        }
        /// <summary>
        /// 组： 1期初、2本期、3小计、4合计
        /// </summary>
        public string GroupNo
        {
            get;
            set;
        }
        /// <summary>
        /// 是否添加到数据结果的列表中
        /// </summary>
        public bool IsAddList
        {
            get;
            set;
        }

    }
    #endregion

    #region 个人科目余额表
    /// <summary>
    /// 个人科目余额表
    /// </summary>
    [Serializable]
    public class PersonalGLBalance
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
        /// 科目代码
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
        /// 部门ID
        /// </summary>
        public Guid? DepartmentID
        {
            get;
            set;
        }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName
        {
            get;
            set;
        }
        /// <summary>
        /// 个人ID
        /// </summary>
        public Guid? PersonalID
        {
            get;
            set;
        }
        /// <summary>
        /// 个人名称
        /// </summary>
        public string PersonalName
        {
            get;
            set;
        }
        /// <summary>
        /// 期初方向(0平、1借方、2贷方)
        /// </summary>
        public GLCodeProperty? BeginningDirection
        {
            get;
            set;
        }
        /// <summary>
        /// 期初方向描述
        /// </summary>
        public string BeginningDirectionName
        {
            get
            {
                if (BeginningDirection == null)
                {
                    return string.Empty;
                }
                string text = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<GLCodeProperty>(BeginningDirection.Value, LocalData.IsEnglish);
                if (!string.IsNullOrEmpty(text))
                {
                    text = text.Replace("方", "");
                }
                return text;
            }
        }
        /// <summary>
        /// 期初外币余额
        /// </summary>
        public decimal? BeginningOrgAmt
        {
            get;
            set;
        }
        /// <summary>
        /// 期初余额
        /// </summary>
        public decimal? BeginningBalance
        {
            get;
            set;
        }
        /// <summary>
        /// 借方外币
        /// </summary>
        public decimal? PeriodDebitOrgAmt
        {
            get;
            set;
        }
        /// <summary>
        /// 借方
        /// </summary>
        public decimal? PeriodDebit
        {
            get;
            set;
        }
        /// <summary>
        /// 贷方外币
        /// </summary>
        public decimal? PeriodCreditOrgAmt
        {
            get;
            set;
        }
        /// <summary>
        /// 贷方
        /// </summary>
        public decimal? PeriodCredit
        {
            get;
            set;
        }
        /// <summary>
        /// 期末方向(0平、1借方、2贷方)
        /// </summary>
        public GLCodeProperty? PeriodEndDirection
        {
            get;
            set;
        }
        /// <summary>
        /// 期末方向名称
        /// </summary>
        public string PeriodEndDirectionName
        {
            get
            {
                if (PeriodEndDirection == null)
                {
                    return string.Empty;
                }
                string text = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<GLCodeProperty>(PeriodEndDirection.Value, LocalData.IsEnglish);
                if (!string.IsNullOrEmpty(text))
                {
                    text = text.Replace("方", "");
                }
                return text;
            }
        }
        /// <summary>
        /// 期未外币余额
        /// </summary>
        public decimal? PeriodEndOrgAmt
        {
            get;
            set;
        }
        /// <summary>
        /// 期未余额
        /// </summary>
        public decimal? PeriodEndBalance
        {
            get;
            set;
        }

    }
    #endregion

    #region 个人三栏余额表
    [Serializable]
    public class Personal3ColumnGLBalance
    {
        /// <summary>
        /// 月份
        /// </summary>
        public string Month
        {
            get
            {
                if (Remark == "期初余额" )
                {
                    return "";
                }
                if (Date == null)
                {
                    return null;
                }
                else
                {
                    return Date.Value.ToString("yyyyMM");
                }
            }
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
        /// 摘要
        /// </summary>
        public string Remark
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
        /// 贷方
        /// </summary>
        public decimal? Credit
        {
            get;
            set;
        }
        /// <summary>
        /// 期末方向(0平、1借方、2贷方)
        /// </summary>
        public GLCodeProperty? Direction
        {
            get;
            set;
        }
        /// <summary>
        /// 期末方向名称
        /// </summary>
        public string DirectionName
        {
            get
            {
                if (Direction == null)
                {
                    return string.Empty;
                }
                string text = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<GLCodeProperty>(Direction.Value, LocalData.IsEnglish);
                if (!string.IsNullOrEmpty(text))
                {
                    text = text.Replace("方", "");
                }
                return text;
            }
        }
        /// <summary>
        /// 期未余额
        /// </summary>
        public decimal? Balance
        {
            get;
            set;
        }
    }
    #endregion

    #region 个人科目明细表
    /// <summary>
    /// 个人科目明细表
    /// </summary>
    [Serializable]
    public class PersonalGLDetail
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
        /// 凭证号
        /// </summary>
        public string VoucherNo
        {
            get;
            set;
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
        /// 科目代码
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
        /// 部门ID
        /// </summary>
        public Guid? DepartmentID
        {
            get;
            set;
        }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName
        {
            get;
            set;
        }
        /// <summary>
        /// 个人名称
        /// </summary>
        public string PersonalName
        {
            get;
            set;
        }
        /// <summary>
        /// 个人ID
        /// </summary>
        public Guid? PersonalID
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
        /// 期末方向(0平、1借方、2贷方)
        /// </summary>
        public GLCodeProperty PeriodEndDirection
        {
            get;
            set;
        }
        /// <summary>
        /// 期末方向名称
        /// </summary>
        public string PeriodEndDirectionName
        {
            get
            {
                string text = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<GLCodeProperty>(PeriodEndDirection, LocalData.IsEnglish);
                if (!string.IsNullOrEmpty(text))
                {
                    text = text.Replace("方", "");
                }
                return text;
            }
        }
        /// <summary>
        /// 期未外币余额
        /// </summary>
        public decimal? PeriodEndOrgAmt
        {
            get;
            set;
        }
        /// <summary>
        /// 期未余额
        /// </summary>
        public decimal? PeriodEndBalance
        {
            get;
            set;
        }
        /// <summary>
        /// 分组代码1期初、2本期、3个人小计、4部门小计、5合计
        /// </summary>
        public string GroupNo
        {
            get;
            set;
        }
        /// <summary>
        /// 排序号
        /// </summary>
        public int IndexNo
        {
            get;
            set;
        }
        /// <summary>
        /// 是否添加到数据结果的列表中
        /// </summary>
        public bool IsAddList
        {
            get;
            set;
        }
    }
    #endregion
}
