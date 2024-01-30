using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.DataCache.ServiceInterface;
using System.Xml.Serialization;
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
       public EmailSourceType? SourceType 
       { 
           get; 
           set; 
       }
       public OperationType? OperationType 
       { 
           get; 
           set; 
       }
        /// <summary>
        /// 模板代码
        /// </summary>
       public string TemplateCode { get; set; }
      /// <summary>
      /// 业务号
      /// </summary>
       public   string OperationNo{get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
       public   string Customer {get; set; }
        /// <summary>
        /// 发货人
        /// </summary>
       public   string Shipper  {get; set; }
        /// <summary>
        /// 装货港名称
        /// </summary>
       public string POLName {get; set; }
        /// <summary>
        /// 卸货港名称
        /// </summary>
       public string PODName {get; set; }
        /// <summary>
        /// 交货地
        /// </summary>
        public string Delivery {get; set; }
        /// <summary>
        /// 订舱号
        /// </summary>
        public string SONO {get; set; }
        /// <summary>
        /// 承运人
        /// </summary>
        public  string Carrier {get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public  string Consinee {get; set; }
        /// <summary>
        /// 箱号
        /// </summary>
        public string  ContainerNO{get; set; }  
        /// <summary>
        /// 提货单
        /// </summary>
         public    string BLNo{get; set; }
        /// <summary>
        /// 船名航次
        /// </summary>
         public    string Vessel{get; set; }
        /// <summary>
        /// 文件员
        /// </summary>
         public    Guid?  DocBy{get; set; }
        /// <summary>
        /// 离港日
        /// </summary>
       public DateTime? EtdTime{get; set; }
        /// <summary>
        /// 到港日
        /// </summary>
       public   DateTime? EtaTime {get; set; }
        /// <summary>
        /// 海外客服
        /// </summary>
       public    Guid? OverseasCs{get; set; }
        /// <summary>
        /// 订舱员 
        /// </summary>
       public   Guid? SoBy{get; set; }
        /// <summary>
        /// 日期搜索类型
        /// </summary>
       public   DateSearchType dateSearchType{get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public    DateTime? FromTime{get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public       DateTime? ToTime{get; set; }
        /// <summary>
        /// 操作口岸
        /// </summary>
        public  Guid[] companyIDs{get; set; }
        /// <summary>
        /// 揽货部门
        /// </summary>
        public       Guid[] SalesBrach{get; set; }
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
    }
}
