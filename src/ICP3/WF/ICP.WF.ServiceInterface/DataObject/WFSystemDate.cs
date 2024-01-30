using System;
using ICP.Sys.ServiceInterface.DataObjects;
using System.Collections.Generic;

namespace ICP.WF.ServiceInterface.DataObject
{
    /// <summary>
    /// 组织结构部门信息
    /// </summary>
    [Serializable]
    public class WFOrganizationList : OrganizationList
    {
        public string WFID
        {
            get;
            set;
        }

        public string WFParentID
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 用户信息
    /// </summary>
    [Serializable]
    public class WFUserList : UserList
    {
        public string WFID
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 职位列表
    /// </summary>
    [Serializable]
    public class WFJobList : JobList
    {
        public string WFID
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 凭证信息
    /// </summary>
    [Serializable]
    public class WFLedgerMaster
    {
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark 
        { 
            get; 
            set; 
        }
        /// <summary>
        /// 意见
        /// </summary>
        public string Opinion
        {
            get;
            set;
        }
        /// <summary>
        /// 附单据数
        /// </summary>
        public string ReceiptQty
        {
            get;
            set;
        }
        /// <summary>
        /// 费用生成日期
        /// </summary>
        public string FeeDate
        {
            get;
            set;
        }
        /// <summary>
        /// 凭证列表
        /// </summary>
        public List<WFLedgerList> LedgerList
        {
            get;
            set;
        }

    }

    /// <summary>
    /// 流程凭证信息
    /// </summary>
    [Serializable]
    public class WFLedgerList
    {
        /// <summary>
        /// 会计科目ID
        /// </summary>
        public Guid GLID
        {
            get;
            set;
        }
        /// <summary>
        /// 会计科目全称
        /// </summary>
        public string GLFullName
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
        /// 客户名称
        /// </summary>
        public string CustomerName
        {
            get;
            set;
        }
        /// <summary>
        /// 部门ID
        /// </summary>
        public Guid? DeptID
        {
            get;
            set;
        }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName
        {
            get;
            set;
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid? UserID
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
        /// 原币金额
        /// </summary>
        public decimal? OrgAmt
        {
            get;
            set;
        }
        /// <summary>
        /// 借方金额
        /// </summary>
        public decimal? DRAmt
        {
            get;
            set;
        }
        /// <summary>
        /// 贷方金额
        /// </summary>
        public decimal? CRAmt
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
        /// 币种ID
        /// </summary>
        public Guid? CurrencyID
        {
            get;
            set;
        }


    }


}
