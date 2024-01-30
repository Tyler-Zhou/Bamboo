#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/7/10 星期二 15:58:54
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using ICP.Framework.CommonLibrary.Common;
using System;

namespace ICP.FCM.Common.ServiceInterface
{
    #region 用于保存报价单的对象
    /// <summary>
    /// 用于保存报价单的对象
    /// </summary>
    [Serializable]
    public class QPOrderSaveRequest : SaveRequest
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid? id{get;set;}
        /// <summary>
        /// 业务号
        /// </summary>
        public string no{get;set;}
        /// <summary>
        /// 报价目标类型
        /// </summary>
        public QPTargetType TargetType{get;set;}
        /// <summary>
        /// 客户
        /// </summary>
        public Guid? customerid{get;set;}
        /// <summary>
        /// 客户描述
        /// </summary>
        public CustomerDescription customerDescription{get;set;}
        /// <summary>
        /// 运输条款
        /// </summary>
        public Guid? transportClauseID{get;set;}
        /// <summary>
        /// 品名
        /// </summary>
        public string commodity{get;set;}
        /// <summary>
        /// 报价人
        /// </summary>
        public Guid quoteBy{get;set;}
        /// <summary>
        /// 付款类型
        /// </summary>
        public QPPaymentType PaymentType { get; set; }
        /// <summary>
        /// 报价开始时间
        /// </summary>
        public DateTime fromDate{get;set;}
        /// <summary>
        /// 报价结束时间
        /// </summary>
        public DateTime toDate{get;set;}
        /// <summary>
        /// 报价Remark
        /// </summary>
        public string remark{get;set;}
        /// <summary>
        /// 保存人
        /// </summary>
        public Guid saveByID { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? updateDate { get; set; }
    } 
    #endregion

    #region 用于保存报价价格的对象
    /// <summary>
    /// 用于保存报价价格的对象
    /// </summary>
    [Serializable]
    public class QPRatesSaveRequest : SaveRequest
    {
        /// <summary>
        /// ID列表
        /// </summary>
        public Guid?[] ids{get;set;}
        /// <summary>
        /// 报价单ID
        /// </summary>
        public Guid qpOrderID{get;set;}
        /// <summary>
        /// 装货港
        /// </summary>
        public Guid[] polIDs{get;set;}
        /// <summary>
        /// 卸货港
        /// </summary>
        public Guid[] podIDs{get;set;}
        /// <summary>
        /// 收货地
        /// </summary>
        public Guid?[] placeOfReceiptIDs{get;set;}
        /// <summary>
        /// 交货地
        /// </summary>
        public Guid[] placeOfDeliveryIDs{get;set;}
        /// <summary>
        /// 船东集合
        /// </summary>
        public string[] carriers{get;set;}
        /// <summary>
        /// TT
        /// </summary>
        public short[] tts{get;set;}
        /// <summary>
        /// Unit 20
        /// </summary>
        public int[] unit20{get;set;}
        /// <summary>
        /// Unit 40
        /// </summary>
        public int[] unit40{get;set;}
        /// <summary>
        /// Unit 40HQ
        /// </summary>
        public int[] unit40HQ{get;set;}
        /// <summary>
        /// Unit 45
        /// </summary>
        public int[] unit45{get;set;}
        /// <summary>
        /// 附加费描述
        /// </summary>
        public string[] surcharges { get; set; }
        /// <summary>
        /// 保存人
        /// </summary>
        public Guid saveByID{get;set;}
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime?[] updateDates{get;set;}
    } 
    #endregion
}
