using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Platform;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICP.FCM.Common.ServiceComponent.JSONObjects
{
    #region CSP舱单委托
    /// <summary>
    /// CSP舱单委托
    /// </summary>
    [Serializable]
    public class BookingInfoForCSPAPI : BaseOrderInfoForCSPAPI
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid ItemID { get; set; }
        /// <summary>
        /// 订舱单号
        /// </summary>
        public int bookingId { get; set; }
        /// <summary>
        /// 订舱单号
        /// </summary>
        public string bookingNo { get; set; }
        /// <summary>
        /// 下单时间/委托时间
        /// </summary>
        public DateTime creationTime { get; set; }
        /// <summary>
        /// 服务商默认业务员用户Id
        /// </summary>
        public int serviceBusinessUserId { get; set; }
        /// <summary>
        /// 服务商默认业务员用户全名
        /// </summary>
        public string serviceBusinessUserFullName { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string creatorUserFullName { get; set; }
        /// <summary>
        /// 柜型列表
        /// </summary>
        public string Containers
        {
            get
            {
                List<BookingContainerForCSPAPI> containers = new List<BookingContainerForCSPAPI>();
                if (!string.IsNullOrEmpty(containerType))
                    containers = JSONSerializerHelper.DeserializeFromJson<List<BookingContainerForCSPAPI>>(containerType);
                StringBuilder sbValue = new StringBuilder();
                foreach (BookingContainerForCSPAPI item in containers)
                {
                    if (Int32.Parse(item.Value)>0)
                        sbValue.AppendFormat("{0} * {1}, ", item.Value, item.Name);
                }
                return sbValue.ToString();
            }
        }

        
        /// <summary>
        /// 渠道
        /// </summary>
        public int? channelId { get; set; }
        /// <summary>
        /// 渠道
        /// </summary>
        public string channelString { get; set; }
        /// <summary>
        /// 申报币种
        /// </summary>
        public int? declareCurrencyId { get; set; }
        /// <summary>
        /// 申报币种
        /// </summary>
        public string declareCurrencyString { get; set; }
        /// <summary>
        /// 单位切换枚举
        /// </summary>
        public int? unitConvertType { get; set; }




        /// <summary>
        /// 
        /// </summary>
        public string serviceCompanyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string consigneeLocation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string consigneePartnerName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shipperLocation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shipperPartnerName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cancelReason { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cancelRemark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isContainsSpecialGoods { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string containsSpecialGoodsTypes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string specialInstructions { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shipmentNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string customsDeclarationDocumentId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string contactName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string purchaseOrderIds { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string deliveryWarehouse { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string consigneeLocationId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string consigneePartnerId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int shipperLocationId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shipperPartnerId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string destinationIsRequireTruck { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isClearance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isTaxIncluded { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? contactId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? deliveryMethodType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? deliveryWarehouseId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int creatorUserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lastModificationTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lastModifierUserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string deletionTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string deleterUserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isDeleted { get; set; }
    } 
    #endregion

    #region CSP舱单-单个
    /// <summary>
    /// CSP舱单-单个
    /// </summary>
    [Serializable]
    public class BookingInfoForCSPAPIItem : PlatformResponseContent<BookingInfoForCSPAPI>
    {
    }
    #endregion

    #region CSP舱单-列表
    /// <summary>
    /// CSP舱单-列表
    /// </summary>
    [Serializable]
    public class BookingInfoForCSPAPIList : PlatformResponseBaseList<List<BookingInfoForCSPAPI>>
    {

    } 
    #endregion
}
