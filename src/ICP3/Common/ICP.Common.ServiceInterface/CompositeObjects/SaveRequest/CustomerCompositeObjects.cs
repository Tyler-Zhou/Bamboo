#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/7/3 星期二 17:25:16
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using System;

namespace ICP.Common.ServiceInterface.CompositeObjects
{
    /// <summary>
    /// 客户保存对象
    /// </summary>
    [Serializable]
    public class CustomerInfoSaveRequest : SaveRequest
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid? id { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string keyword { get; set; }
        /// <summary>
        /// 简称-中文
        /// </summary>
        public string cshortname { get; set; }
        /// <summary>
        /// 简称-英文
        /// </summary>
        public string eshortname { get; set; }
        /// <summary>
        /// 名称-中文
        /// </summary>
        public string cname { get; set; }
        /// <summary>
        /// 名称-英文
        /// </summary>
        public string ename { get; set; }
        /// <summary>
        /// 账单名-中文
        /// </summary>
        public string cbillname { get; set; }
        /// <summary>
        /// 账单名-英文
        /// </summary>
        public string ebillname { get; set; }
        /// <summary>
        /// 地址-中文
        /// </summary>
        public string caddress { get; set; }
        /// <summary>
        /// 地址-英文
        /// </summary>
        public string eaddress { get; set; }
        /// <summary>
        /// 国家ID
        /// </summary>
        public Guid countryid { get; set; }
        /// <summary>
        /// 省/州ID
        /// </summary>
        public Guid? provinceid { get; set; }
        /// <summary>
        /// 城市ID
        /// </summary>
        public Guid? cityid { get; set; }
        /// <summary>
        /// 企业代码类型
        /// </summary>
        public string enterprisecodetype { get; set; }
        /// <summary>
        /// 企业代码
        /// </summary>
        public string enterprisecode { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string postcode { get; set; }
        /// <summary>
        /// 电话1
        /// </summary>
        public string tel1 { get; set; }
        /// <summary>
        /// 电话2
        /// </summary>
        public string tel2 { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string fax { get; set; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 主页
        /// </summary>
        public string homepage { get; set; }
        /// <summary>
        /// 税务类型
        /// </summary>
        public TaxType? taxidtype { get; set; }
        /// <summary>
        /// 税务登记号
        /// </summary>
        public string taxidno { get; set; }
        /// <summary>
        /// 银行账号
        /// </summary>
        public string bankaccountno { get; set; }
        /// <summary>
        /// 信用限额
        /// </summary>
        public decimal creditlimit { get; set; }
        /// <summary>
        /// 信用期限
        /// </summary>
        public int term { get; set; }
        /// <summary>
        /// 贸易条款
        /// </summary>
        public Guid? tradetermid { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        public Guid? paymentTypeid { get; set; }
        /// <summary>
        /// 客户类型
        /// </summary>
        public CustomerType customerype { get; set; }
        /// <summary>
        /// 承运人
        /// </summary>
        public bool isagentOfcarrier { get; set; }
        /// <summary>
        /// 堆场或码头的代码(国外需求)
        /// </summary>
        public string firmcode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 保存人
        /// </summary>
        public Guid savebyid { get; set; }
        /// <summary>
        /// 是否公司货客户
        /// </summary>
        public bool iscompany { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? updatedate { get; set; }
    }
}
