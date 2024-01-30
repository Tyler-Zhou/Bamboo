using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace SnakeERP.Model
{
    /// <summary>
    /// 发货-订单信息
    /// </summary>
    [Table("DGOrderInfos")]
    public class DGOrderInfo : BasePoco
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = "订单{0}是必填项")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string Name { get; set; }

        /// <summary>
        /// 发货时间
        /// </summary>
        [Display(Name = "发货时间")]
        [Required(ErrorMessage = "订单{0}是必填项")]
        public DateTime ShipDateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 发货公司
        /// </summary>
        [Display(Name = "发货公司")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string DeliveryCompany { get; set; } = "";

        /// <summary>
        /// 发货电话
        /// </summary>
        [Display(Name = "发货公司电话")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string DeliveryCompanyPhone { get; set; } = "";

        /// <summary>
        /// 送货人
        /// </summary>
        [Display(Name = "发货人")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string DeliveryMan { get; set; } = "";

        /// <summary>
        /// 车牌
        /// </summary>
        [Display(Name = "车牌")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string LicensePlate { get; set; } = "";
        
    }
}
