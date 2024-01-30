using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WalkingTec.Mvvm.Core;

namespace SnakeERP.Model
{
    /// <summary>
    /// 基础资料客户
    /// </summary>
    [Table("BasicCustomers")]
    public class BasicCustomer : BasePoco
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
        /// 省Id
        /// </summary>
        [Display(Name = "省")]
        public Guid? ProvinceID { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        [Display(Name = "省")]
        [JsonIgnore]
        public BasicProvince Province { get; set; }

        /// <summary>
        /// 市Id
        /// </summary>
        [Display(Name = "市")]
        public Guid? CityID { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        [Display(Name = "市")]
        [JsonIgnore]
        public BasicCity City { get; set; }

        /// <summary>
        /// 县Id
        /// </summary>
        [Display(Name = "县")]
        public Guid? CountyID { get; set; }

        /// <summary>
        /// 县
        /// </summary>
        [Display(Name = "县")]
        [JsonIgnore]
        public BasicCounty County { get; set; }
        
        /// <summary>
        /// 手机
        /// </summary>
        [Display(Name = "手机")]
        [RegularExpression("^[1][3-9]\\d{9}$", ErrorMessage = "Validate.{0}formaterror")]
        public string CellPhone { get; set; }

        /// <summary>
        /// 座机
        /// </summary>
        [Display(Name = "座机")]
        [StringLength(30, ErrorMessage = "Validate.{0}stringmax{1}")]
        [RegularExpression("^[-0-9\\s]{8,30}$", ErrorMessage = "Validate.{0}formaterror")]
        public string HomePhone { get; set; }

        /// <summary>
        /// 销售员Id
        /// </summary>
        [Display(Name = "销售员")]
        public Guid? SalesID { get; set; }

        /// <summary>
        /// 销售员
        /// </summary>
        [Display(Name = "销售员")]
        [JsonIgnore]
        public FrameworkUser Sales { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [Display(Name = "地址")]
        [StringLength(200, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string Address { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        [Display(Name = "区域")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string Region { get; set; }
        
    }
}
