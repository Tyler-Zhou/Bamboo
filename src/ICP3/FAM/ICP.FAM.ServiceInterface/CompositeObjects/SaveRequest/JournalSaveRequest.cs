using System;

namespace ICP.FAM.ServiceInterface.DataObjects.SaveRequests
{
    /// <summary>
    /// JournalSaveRequest
    /// </summary>
    [Serializable]
    public class JournalSaveRequest : ICP.Framework.CommonLibrary.Common.SaveRequest
    {


        /// <summary>
        /// ID
        /// </summary>
        public Guid ID;
        /// <summary>
        /// 单号
        /// </summary>
        public string No;
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime PostDate;
        /// <summary>
        /// 公司ID
        /// </summary>
        public Guid CompanyID;
        /// <summary>
        /// 保存人ID
        /// </summary>
        public Guid SaveByID;
        ///// <summary>
        ///// 更新人
        ///// </summary>
        //public Guid UpdateBy;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate;
        /// <summary>
        /// 是否英文版本
        /// </summary>
        public bool IsEnglish;
    }



    /// <summary>
    /// JournalDetailSaveRequest
    /// </summary>
    [Serializable]
    public class JournalDetailSaveRequest : ICP.Framework.CommonLibrary.Common.SaveRequest
    {
        /// <summary>
        /// ID集合
        /// </summary>
        public Guid?[] IDs;

        /// <summary>
        /// 日记帐ID
        /// </summary>
        public Guid JournalID;

        /// <summary>
        /// 会计科目ID
        /// </summary>
        public Guid[] GLIDs;


        /// <summary>
        /// 会计科目ID
        /// </summary>
        public Guid?[] Customers;

        /// <summary>
        /// 币种ID集合
        /// </summary>
        public Guid[] CurrencyIDs;

        /// <summary>
        /// 应收金额 
        /// </summary>
        public decimal[] DRAmounts;

        /// <summary>
        /// 应会金额集合
        /// </summary>
        public decimal[] CRAmounts;

        /// <summary>
        /// 备注集合
        /// </summary>
        public string[] Remarks;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime?[] UpdateDates;
      
        /// <summary>
        /// 保存人
        /// </summary>
        public Guid  SaveByID;
        /// <summary>
        /// 是否英文版本
        /// </summary>
        public bool IsEnglish;
    }


    /// <summary>
    /// LedgerSaveRequest
    /// </summary>
    [Serializable]
    public class LedgerSaveRequest : ICP.Framework.CommonLibrary.Common.SaveRequest
    {
        public Guid? ID;
        public Int32? ReceiptQty;
        public LedgerMasterType Type;
        public Guid CompanyID;
        public Guid? AccountingID;
        public Guid? CashierID;
        public DateTime? DATE;
        public DateTime? UpdateDate;
        public Guid?[] DetailIDS;
        public Guid?[] CustomerIDs;
        public Guid[] GLIDs;
        public Guid?[] RefIDs;
        public string[] RefNos;
        public string[] OperationNos;
        public LedgerDetailType?[] DetailTypes;
        public DateTime[] Dates;
        public Decimal[] CRAmts;
        public Decimal[] DRAmts;
        public string[] Remarks;
        public Guid?[] CompanyIds;
        public Decimal[] OrgAmts;
        public Decimal?[] Rates;
        public Guid?[] DepIDs;
        public Guid?[] UserIDs;
        public DateTime?[] UpdateDates;
        public Guid SaveBy;
        public Boolean IsEnglish;
        public Boolean IsCarryOver;
    }

    public class BeginBalanceSaveRequest : ICP.Framework.CommonLibrary.Common.SaveRequest
    {
        public Guid CompanyID;
        public Int32 Year;
        public Guid SaveBy;
        public Boolean IsEnglish; 
        public Guid?[] DetailIDS;
        public Guid?[] CustomerIDs;
        public Guid[] GLIDs;
        public Decimal[] CRAmts;
        public Decimal[] DRAmts;
        public Decimal[] OrgAmts;
        public Decimal?[] Rates;
        public Guid?[] DepIDs;
        public Guid?[] UserIDs;
        public DateTime?[] UpdateDates;
 
    }

}
