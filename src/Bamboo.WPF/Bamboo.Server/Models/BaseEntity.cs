using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bamboo.Server.Models
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        [Column("iId")]
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("tCreateDate", TypeName = "DateTime")]
        [Required]
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("tUpdateDate",TypeName ="DateTime")]
        public DateTime? UpdateDate { get; set; }
    }
}
