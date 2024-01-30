using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace SnakeERP.Model
{
    /// <summary>
    /// 车辆品牌
    /// </summary>
    [Table("BasicVehicleBrands")]
    public class BasicVehicleBrand : BasePoco
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = "车辆品牌{0}是必填项")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string Name { get; set; }
    }
}
