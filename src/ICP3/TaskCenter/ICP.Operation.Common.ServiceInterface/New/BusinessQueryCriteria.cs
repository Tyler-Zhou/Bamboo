using ICP.DataCache.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using System;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 业务查询信息实体类
    /// </summary>
    [Serializable]
    public class BusinessQueryCriteria
    {
        /// <summary>
        /// 发件人Email地址
        /// </summary>
        public string EmailAddress
        {
            get;
            set;
        }
        /// <summary>
        /// 邮件来源类型
        /// </summary>
        public EmailSourceType? SourceType
        {
            get;
            set;
        }
        /// <summary>
        /// 操作类型
        /// </summary>
        public OperationType? OperationType
        {
            get;
            set;
        }
        /// <summary>
        /// 如果本地缓存没有找到们是否需要到服务端数据库中去查找
        /// </summary>
        public bool NeedSearchInSQLServer { get; set; }
        /// <summary>
        /// 搜索类型
        /// </summary>
        public SearchActionType SearchType { get; set; }
        /// <summary>
        /// 服务端查询条件字符串
        /// </summary>
        public string ServerQueryString { get; set; }
        /// <summary>
        /// 模板代码
        /// </summary>
        public string TemplateCode { get; set; }
        /// <summary>
        /// 锁定查询条件--口岸集合
        /// </summary>
        public string LockCompanyIDs { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNo { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string Customer { get; set; }
        /// <summary>
        /// 发货人
        /// </summary>
        public string Shipper { get; set; }
        /// <summary>
        /// 装货港名称
        /// </summary>
        public string POLName { get; set; }
        /// <summary>
        /// 卸货港名称
        /// </summary>
        public string PODName { get; set; }
        /// <summary>
        /// 交货地
        /// </summary>
        public string Delivery { get; set; }
        /// <summary>
        /// 订舱号
        /// </summary>
        public string SONO { get; set; }
        /// <summary>
        /// 承运人
        /// </summary>
        public string Carrier { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public string Consinee { get; set; }
        /// <summary>
        /// 箱号
        /// </summary>
        public string ContainerNO { get; set; }
        /// <summary>
        /// 提货单
        /// </summary>
        public string BLNo { get; set; }
        /// <summary>
        /// 船名航次
        /// </summary>
        public string Vessel { get; set; }
        /// <summary>
        /// 文件员
        /// </summary>
        public Guid? DocBy { get; set; }
        /// <summary>
        /// 离港日
        /// </summary>
        public DateTime? EtdTime { get; set; }
        /// <summary>
        /// 到港日
        /// </summary>
        public DateTime? EtaTime { get; set; }
        /// <summary>
        /// 海外客服
        /// </summary>
        public Guid? OverseasCs { get; set; }
        /// <summary>
        /// 订舱员 
        /// </summary>
        public Guid? SoBy { get; set; }
        /// <summary>
        /// 日期搜索类型
        /// </summary>
        public DateSearchType dateSearchType { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? FromTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? ToTime { get; set; }
        /// <summary>
        /// 操作口岸
        /// </summary>
        public Guid[] companyIDs { get; set; }
        /// <summary>
        /// 揽货部门
        /// </summary>
        public Guid[] SalesBrach { get; set; }
        /// <summary>
        /// 揽货人
        /// </summary>
        public Guid? SalesID { get; set; }
        /// <summary>
        /// 高级查找过滤条件
        /// </summary>
        public string AdvanceQueryString
        {
            get;
            set;
        }
        /// <summary>
        /// 节点所属人
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 电放号
        /// </summary>
        public string TelxNo { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int State { set; get; }
        /// <summary>
        ///是否有效
        /// </summary>
        public bool? IsValid { get; set; }
        /// <summary>
        /// 是否放单,0为全部，1为否，2为是
        /// </summary>
        public int RBLD { get; set; }
        /// <summary>
        /// 是否已签收,0为全部，1为否，2为是
        /// </summary>
        public int RBLRcv { get; set; }
        /// <summary>
        /// 是否已申请,0为全部，1为否，2为是
        /// </summary>
        public int RCA { get; set; }
        /// <summary>
        /// 是否已放货,0为全部，1为否，2为是
        /// </summary>
        public int RC { get; set; }
        /// <summary>
        /// 是否已同意,0为全部，1为否，2为是
        /// </summary>
        public int ARC { get; set; }
        /// <summary>
        /// 是否已催单,0为全部，1为否，2为是
        /// </summary>
        public int URB { get; set; }
        /// <summary>
        /// 是否已催款,0为全部，1为否，2为是
        /// </summary>
        public int UDN { get; set; }
        /// <summary>
        /// 是否已关帐,0为全部，1为否，2为是
        /// </summary>
        public int ACCLOS { get; set; }
        /// <summary>
        /// 日期搜索类型
        /// </summary>
        public OIBusinessDateSearchType OIDateSearchType { get; set; }
        /// <summary>
        /// 通知人
        /// </summary>
        public string NotifyPart { get; set; }
        /// <summary>
        /// 代理
        /// </summary>
        public string Agent { get; set; }
        /// <summary>
        /// 客服
        /// </summary>
        public Guid? CustomerServiceID { set; get; }
        /// <summary>
        /// 航次
        /// </summary>
        public string VoyageNo { set; get; }
        /// <summary>
        /// 范围
        /// </summary>
        public string ScopeItem { get; set; }
        /// <summary>
        /// 邮件关联的业务集合
        /// </summary>
        public Guid[] OperationIDs { get; set; }
        /// <summary>
        /// 返回数据条数
        /// </summary>
        public int TopCount { get; set; }
    }

    /// <summary>
    /// 锁定查询条件
    /// </summary>
    [Serializable]
    public class LockCriteria
    {
        /// <summary>
        /// 口岸ID集合
        /// </summary>
        public string CcompanyIDs { get; set; }
    }

    /// <summary>
    /// 高级查找返回参数类
    /// </summary>
    [Serializable]
    public  class QueryInfo
    {
        public QueryInfo(string advanceQueryString, OperationType operationType)
        {
            AdvanceQueryString = advanceQueryString;
            OperationType = operationType;
        }

        public string AdvanceQueryString { get; set; }
        public OperationType OperationType { get; set; }
    }
}
