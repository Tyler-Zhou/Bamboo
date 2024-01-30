using System;
using System.Collections.Generic;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.DataCache.ServiceInterface;
using ICP.FileSystem.ServiceInterface;

namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    #region OperationCommonInfo

    /// <summary>
    /// 业务公共信息
    /// </summary>
    [Serializable]
    public class OperationCommonInfo
    {
        /// <summary>
        ///  TradeCustomers Forms 
        /// </summary>
        public OperationCommonInfo()
        {
            TradeCustomers = new List<OperationCustomer>();
            Forms = new List<FormData>();
        }

        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID { get; set; }

        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNo { get; set; }

        /// <summary>
        /// 当前表单ID
        /// </summary>
        public Guid CurrentFormID { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
        public Guid CompanyID { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public OperationType OperationType { get; set; }
        

        /// <summary>
        /// 关帐前以业务时间（出口为ETD、进口为ETA、内贸为ETD、其他为创建时间）,关帐后以当天时间设置帐单建立时间
        /// </summary>
        public DateTime OperationDate { get; set; }

        /// <summary>
        /// ETD
        /// </summary>
        public DateTime ETD { get; set; }
        /// <summary>
        /// ETA
        /// </summary>
        public DateTime ETA { get; set; }

        /// <summary>
        /// 帐单参考列表
        /// </summary>
        public List<FormData> Forms { get; set; }

        /// <summary>
        /// 合约ID
        /// </summary>
        public Guid? FreightID { get; set; }

        /// <summary>
        /// 提单的AgentID,ConsigneeID,CarrierID,CustomerID, 在帐单中可以下拉选择
        /// </summary>
        public List<OperationCustomer> TradeCustomers { get; set; }

        /// <summary>
        /// 分文件状态
        /// </summary>
        public DocumentState DocumentState
        {
            get;
            set;
        }

        ///// <summary>
        ///// 是否下载
        ///// </summary>
        //public bool IsDownLoad { get; set; }

        /// <summary>
        /// 代理ID
        /// </summary>
        public Guid? AgentID
        {
            get;
            set;
        }

        /// <summary>
        /// 承运人ID
        /// </summary>
        public Guid? AgentOfCarrierID
        {
            get;
            set;
        }

        /// <summary>
        /// 是否和承运人确认费用
        /// </summary>
        public Boolean IsAPCCfm
        {
            get;
            set;
        }

        /// <summary>
        /// 调整费用金额
        /// </summary>
        public decimal AdjustmentAmount { get; set; }

        /// <summary>
        /// 调整费用币种ID
        /// </summary>
        public Guid? AdjustmentCurrencyID { get; set; }

        /// <summary>
        /// 调整费用币种名称
        /// </summary>
        public string AdjustmentCurrencyName { get; set; }

        /// <summary>
        /// 电商金额
        /// </summary>
        public decimal ECommerceAmount { get; set; }

        /// <summary>
        /// 电商币种ID
        /// </summary>
        public Guid? ECommerceCurrencyID { get; set; }

        /// <summary>
        /// 电商币种名称
        /// </summary>
        public string ECommerceCurrencyName { get; set; }
    }

    /// <summary>
    /// 业务公共信息
    /// </summary>
    [Serializable]
    public class OperationCustomer : CustomerList
    {
        /// <summary>
        /// 是否业务中的代理
        /// </summary>
        public bool IsAgent { get; set; }
        /// <summary>
        /// 是否业务中的客户
        /// </summary>
        public bool IsCustomer { get; set; }
    }

    /// <summary>
    /// 表单数据
    /// </summary>
    [Serializable]
    public class FormData
    {
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID { get; set; }
        /// <summary>
        /// 表单ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 表单类型
        /// </summary>
        public FormType Type { get; set; }

        /// <summary>
        /// 表单号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 箱数量
        /// </summary>
        public Int32 CntQty { get; set; }

        /// <summary>
        /// 是否当前
        /// </summary>
        public bool IsCurrent { get; set; }
    }


    /// <summary>
    /// 业务代理信息实体类
    /// </summary>
    [Serializable]
    public class OperationAgentInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 是否是内部代理
        /// </summary>
        public bool IsBranch { get; set; }

        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID { get; set; }


        /// <summary>
        /// 订舱号
        /// </summary>
        public string SONO { get; set; }

        /// <summary>
        /// 代理ID
        /// </summary>
        public Guid AgentID { get; set; }
    }

    /// <summary>
    /// 文档分发信息
    /// </summary>
    [Serializable]
    public class DocumentDispatchInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        ///提单ID
        /// </summary>
        public Guid OceanBookingID { get; set; }

        /// <summary>
        /// 文档ID
        /// </summary>
        public DocumentState State { get; set; }

        /// <summary>
        /// 代理邮件地址
        /// </summary>
        public String AgentMail { get; set; }

        /// <summary>
        /// 海外客服ID
        /// </summary>
        public Guid? OverseasCSID { get; set; }

        /// <summary>
        /// 海外部客服Name
        /// </summary>
        public string OverseasCSName { get; set; }

        /// <summary>
        /// 文档接收者类型
        /// </summary>
        public RecipientType RecipientType { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否再次分发
        /// </summary>
        public bool IsAgainDispatch { get; set; }

    }


    /// <summary>
    /// 选择的文档列表信息
    /// </summary>
    [Serializable]
    public class DocumentDispatchSelectListInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid? ID { get; set; }

        /// <summary>
        /// 文档ID
        /// </summary>
        public Guid? OperationFileID { get; set; }

        /// <summary>
        /// 文档分发信息ID
        /// </summary>
        public Guid? OceanAgentDispatchID { get; set; }

        /// <summary>
        /// 文档更新时间
        /// </summary>
        public DateTime? UpdateDate { get; set; }
    }

    /// <summary>
    /// 日志类
    /// </summary>
    [Serializable]
    public class DescriptionInfo
    {
        /// <summary>
        /// 日志内容
        /// </summary>
        public String Description { get; set; }
    }

    /// <summary>
    /// 文档分发界面信息
    /// </summary>
    [Serializable]
    public class DocumentDispatchContainerObjects
    {
        /// <summary>
        /// 代理信息
        /// </summary>
        public OperationAgentInfo AgentInfo { get; set; }

        /// <summary>
        /// 选择的文档列表集合
        /// </summary>
        public List<DocumentDispatchSelectListInfo> DocumentListInfo { get; set; }

        /// <summary>
        /// 文档分发信息
        /// </summary>
        public DocumentDispatchInfo DispatchInfo { get; set; }

        /// <summary>
        /// 分文件日志
        /// </summary>
        public List<DescriptionInfo> Description { get; set; }

        /// <summary>
        /// 保存人ID
        /// </summary>
        public Guid SaveByID { get; set; }

    }

    #endregion

    #region BookingCommonInfo

    #endregion
}
