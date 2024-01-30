using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WalkingTec.Mvvm.Core;

namespace SnakeERP.Model
{
    /// <summary>
    /// 基础资料-物流点
    /// </summary>
    [Table("BasicLogisticsPositions")]
    public class BasicLogisticsPosition : BasePoco
    {
        /// <summary>
        /// 物流Id
        /// </summary>
        [Display(Name = "物流公司")]
        public Guid? LogisticsID { get; set; }

        /// <summary>
        /// 物流
        /// </summary>
        [Display(Name = "物流")]
        [JsonIgnore]
        public BasicLogistics Logistics { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = "物流点{0}是必填项")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string Name { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [Display(Name = "联系人")]
        [Required(ErrorMessage = "物流点{0}是必填项")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string Contact { get; set; }

        /// <summary>
        /// 电话1
        /// </summary>
        [Display(Name = "电话1")]
        [RegularExpression("0\\d{2,3}-\\d{7,8}|\\(?0\\d{2,3}[)-]?\\d{7,8}|\\(?0\\d{2,3}[)-]*\\d{7,8}", ErrorMessage = "Validate.{0}formaterror")]
        public string Phone1 { get; set; }

        /// <summary>
        /// 电话2
        /// </summary>
        [Display(Name = "电话2")]
        [StringLength(30, ErrorMessage = "Validate.{0}stringmax{1}")]
        [RegularExpression("^[1][3-9]\\d{9}$", ErrorMessage = "Validate.{0}formaterror")]
        public string Phone2 { get; set; }

        /// <summary>
        /// 电话3
        /// </summary>
        [Display(Name = "电话3")]
        [StringLength(30, ErrorMessage = "Validate.{0}stringmax{1}")]
        [RegularExpression("^[1][3-9]\\d{9}$", ErrorMessage = "Validate.{0}formaterror")]
        public string Phone3 { get; set; }

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
        /// 地址
        /// </summary>
        [Display(Name = "地址")]
        [StringLength(200, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string Address { get; set; }
    }
}
