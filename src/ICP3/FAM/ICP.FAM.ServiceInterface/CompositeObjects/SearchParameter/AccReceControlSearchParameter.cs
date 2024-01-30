using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using System;

namespace ICP.FAM.ServiceInterface
{
    /// <summary>
    /// 查询请求
    /// </summary>
    public class AccReceControlSearchParameter
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public Guid[] CompanyIDs { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid? CustomerID { get; set; }
        /// <summary>
        /// 账单类型
        /// </summary>
        public BillType[] BillTypes { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public OperationType[] OperationTypes { get; set; }
        /// <summary>
        /// Term客户类型
        /// </summary>
        public TermType TermType { get; set; }
        /// <summary>
        /// 投保客户类型
        /// </summary>
        public InsuredType InsuredType { get; set; }
        /// <summary>
        /// 业务日期OR账单日期
        /// </summary>
        public short SearchType { get; set; }
        /// <summary>
        /// 截止日期
        /// </summary>
        public DateTime EndingDate { get; set; }
        /// <summary>
        /// 帐龄范围
        /// </summary>
        public AgingDateState AgingDateState { get; set; }
        /// <summary>
        /// 超期范围
        /// </summary>
        public DayRange PastDueRange { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool OnlyOverPaid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Currency { get; set; }
        
        
    }
}
