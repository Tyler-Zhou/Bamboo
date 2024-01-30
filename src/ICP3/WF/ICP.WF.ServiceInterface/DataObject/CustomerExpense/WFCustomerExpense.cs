using System;

namespace ICP.WF.ServiceInterface
{
    /// <summary>
    ///CRM客户列表
    /// </summary>
    [Serializable]
    public class WFCECRMCustomerList
    {
        public Guid ID
        {
            get;
            set;
        }
        public string Code
        {
            get;
            set;
        }
        public string KeyWord
        {
            get;
            set;
        }
        public string  CName
        {
            get;
            set;
        }
        public string  EName
        {
            get;
            set;
        }
        public string  Country
        {
            get;
            set;
        }

    }

    /// <summary>
    /// CRM客户跟进纪录
    /// </summary>
    [Serializable]
    public class WFCECRMCustomerTouchLogList
    {
        public Guid ID { get; set; }
        public Guid CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 业务费用报销申请纪录
    /// </summary>
    [Serializable]
    public class WFCustomerExpenseLogList
    {
        public string WorkName{get;set;}
        public string WorkNo{get;set;}
        public decimal? Money{get;set;}
        public string  CreateBy{get;set;}
        public DateTime CreateDate { get; set; }
    }


}
