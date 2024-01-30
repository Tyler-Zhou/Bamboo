using System.ComponentModel.DataAnnotations;

namespace SnakeERP.Model
{
    /// <summary>
    /// 交付状态
    /// </summary>
    public enum EnumDeliveryStatus
    {
        /// <summary>
        /// 未发
        /// </summary>
        [Display(Name = "未发")]
        NotDelivery = 1,

        /// <summary>
        /// 已发
        /// </summary>
        [Display(Name = "已发")]
        Delivery = 3,
    }
}
