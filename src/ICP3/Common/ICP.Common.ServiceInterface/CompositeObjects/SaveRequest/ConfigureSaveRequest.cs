using ICP.Framework.CommonLibrary.Common;
using System;

namespace ICP.Common.ServiceInterface.CompositeObjects
{
    /// <summary>
    /// 配置保存对象
    /// </summary>
    public class ConfigureSaveRequest: SaveRequest
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid? ID { get; set; }
        /// <summary>
        /// 操作口岸ID
        /// </summary>
        public  Guid CompanyID { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public  Guid CustomerID { get; set; }
        /// <summary>
        /// 本位币ID
        /// </summary>
        public  Guid StandardCurrencyID { get; set; }
        /// <summary>
        /// 默认币ID
        /// </summary>
        public  Guid DefaultCurrencyID { get; set; }
        /// <summary>
        /// 解决方案ID
        /// </summary>
        public  Guid SolutionID { get; set; }
        /// <summary>
        /// 签发地ID
        /// </summary>
        public  Guid? IssuePlaceID { get; set; }
        /// <summary>
        /// 商务关账日
        /// </summary>
        public DateTime? BusinessClosingDate { get; set; }
        /// <summary>
        /// 计费关账日
        /// </summary>
        public  DateTime? ChargingClosingDate { get; set; }
        /// <summary>
        /// 财务关账日
        /// </summary>
        public  DateTime? AccountingClosingDate { get; set; }
        /// <summary>
        /// 公司代码
        /// </summary>
        public  string ShortCode { get; set; }
        /// <summary>
        /// 默认代理描述
        /// </summary>
        public  string DefaultAgentDescription { get; set; }
        /// <summary>
        /// 提单抬头ID
        /// </summary>
        public  Guid BLTitleID { get; set; }
        /// <summary>
        /// 是否增值税发票
        /// </summary>
        public  bool IsVATinvoice { get; set; }
        /// <summary>
        /// 增值税费用名称
        /// </summary>
        public  Guid? VATFEEID { get; set; }
        /// <summary>
        /// 应收增值税率
        /// </summary>
        public  decimal? VATrateAP { get; set; }
        /// <summary>
        /// 招商银行一网通用户ID
        /// </summary>
        public  string CMBNetComUserID { get; set; }
        /// <summary>
        /// 保存人
        /// </summary>
        public Guid SaveByID { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate { get; set; }
    }
}
