using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WalkingTec.Mvvm.Core;

namespace SnakeERP.Model
{
    /// <summary>
    /// 发货明细
    /// </summary>
    [Table("DGOrderDetails")]
    public class DGOrderDetail : BasePoco
    {
        /// <summary>
        /// 发货单
        /// </summary>
        [Display(Name = "发货单")]
        public Guid? OrderInfoID { get; set; }

        /// <summary>
        /// 发货单
        /// </summary>
        [Display(Name = "发货单")]
        [JsonIgnore]
        public DGOrderInfo OrderInfo { get; set; }


        /// <summary>
        /// 发货人
        /// </summary>
        [Display(Name = "发货人")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string DeliveryMan { get; set; }

        /// <summary>
        /// 发货电话
        /// </summary>
        [Display(Name = "发货电话")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string DeliveryPhone { get; set; }


        /// <summary>
        /// 发货地址
        /// </summary>
        [Display(Name = "发货地址")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string DeliveryAddress { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        [Display(Name = "收货人")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string ReceivingMan { get; set; }

        /// <summary>
        /// 收货电话
        /// </summary>
        [Display(Name = "收货电话")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string ReceivingPhone { get; set; }


        /// <summary>
        /// 收货地址
        /// </summary>
        [Display(Name = "收货地址")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string ReceivingAddress { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        [Display(Name = "订单编号")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string OrderNO { get; set; }

        /// <summary>
        /// 门数量
        /// </summary>
        [Display(Name = "门数量")]
        public int DoorQuantity { get; set; }

        /// <summary>
        /// 套数量
        /// </summary>
        [Display(Name = "套数量")]
        public int SleeveQuantity { get; set; }

        /// <summary>
        /// 线条数量
        /// </summary>
        [Display(Name = "线条数量")]
        public int LinesQuantity { get; set; }

        /// <summary>
        /// 其他数量
        /// </summary>
        [Display(Name = "其他数量")]
        public int OtherQuantity { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        [StringLength(200, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string Remark { get; set; } = "";

        /// <summary>
        /// 交付状态
        /// </summary>
        [Display(Name = "交付状态")]
        public EnumDeliveryStatus? DeliveryStatus { get; set; }
    }
}
