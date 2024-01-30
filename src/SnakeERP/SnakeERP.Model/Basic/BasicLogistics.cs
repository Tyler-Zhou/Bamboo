using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace SnakeERP.Model
{
    /// <summary>
    /// 基础资料-物流
    /// </summary>
    [Table("BasicLogisticss")]
    public class BasicLogistics : BasePoco
    {
        /// <summary>
        /// 编码
        /// </summary>
        [Display(Name = "编码")]
        [Required(ErrorMessage = "物流{0}是必填项")]
        [RegularExpression("^[0-9]{6,6}$", ErrorMessage = "Validate.{0}formaterror")]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = "物流{0}是必填项")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string Name { get; set; } = "";

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "别名")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string Alias { get; set; } = "";

        /// <summary>
        /// 手机
        /// </summary>
        [Display(Name = "手机")]
        [RegularExpression("^[1][3-9]\\d{9}$", ErrorMessage = "Validate.{0}formaterror")]
        public string CellPhone { get; set; } = "";

        /// <summary>
        /// 地址
        /// </summary>
        [Display(Name = "地址")]
        [StringLength(200, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string Address { get; set; } = "";

        /// <summary>
        /// 车牌
        /// </summary>
        [Display(Name = "车牌")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string LicensePlate { get; set; } = "";
    }
}
