using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WalkingTec.Mvvm.Core;

namespace SnakeERP.Model
{
    /// <summary>
    /// 基础资料-车辆信息
    /// </summary>
    [Table("BasicVehicleInfos")]
    public class BasicVehicleInfo : BasePoco
    {
        /// <summary>
        /// 号码
        /// </summary>
        [Display(Name = "号码")]
        [Required(ErrorMessage = "车辆{0}是必填项")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string Number { get; set; }

        /// <summary>
        /// 车辆类型ID
        /// </summary>
        [Display(Name = "类型")]
        [Required(ErrorMessage = "车辆{0}是必填项")]
        public Guid VehicleTypeID { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        [Display(Name = "车辆类型")]
        [JsonIgnore]
        public BasicVehicleType VehicleType { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        [Display(Name = "品牌")]
        [Required(ErrorMessage = "车辆{0}是必填项")]
        public Guid VehicleBrandID { get; set; }

        /// <summary>
        /// 车辆品牌
        /// </summary>
        [Display(Name = "车辆品牌")]
        [JsonIgnore]
        public BasicVehicleBrand VehicleBrand { get; set; }

        /// <summary>
        /// 是否新能源
        /// </summary>
        [Display(Name = "是否新能源")]
        public bool NewEnergy { get; set; } = false;

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        public EnumVehicleStatus Status { get; set; }
    }

    /// <summary>
    /// 车辆状态
    /// </summary>
    public enum EnumVehicleStatus
    {
        /// <summary>
        /// 可用
        /// </summary>
        [Display(Name = "可用")]
        Available = 1,

        /// <summary>
        /// 维修
        /// </summary>
        [Display(Name = "维修")]
        Maintenance = 2,

        /// <summary>
        /// 其他
        /// </summary>
        [Display(Name = "其他")]
        Other = 3,
    }
}
