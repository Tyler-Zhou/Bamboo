using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WalkingTec.Mvvm.Core;

namespace SnakeERP.Model
{
    /// <summary>
    /// 基础资料-市
    /// </summary>
    [Table("BasicCitys")]
    public class BasicCity : BasePoco
    {
        /// <summary>
        /// 编码
        /// </summary>
        [Display(Name = "编码")]
        [Required(ErrorMessage = "市{0}是必填项")]
        [RegularExpression("^[0-9]{6,6}$", ErrorMessage = "Validate.{0}formaterror")]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = "市{0}是必填项")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string Name { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        [Display(Name = "省")]
        public Guid ProvinceID { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        [Display(Name = "省")]
        [JsonIgnore]
        public BasicProvince Province { get; set; }
    }
}
