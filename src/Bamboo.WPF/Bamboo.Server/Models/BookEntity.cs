using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bamboo.Server.Models
{
    /// <summary>
    /// 书籍实体
    /// </summary>
    [Table("tb_Book", Schema = "dbo")]
    public class BookEntity : BaseEntity
    {
        /// <summary>
        /// 标识键
        /// </summary>
        [Column("sKey")]
        [MaxLength(50)]
        [Required]
        [DefaultValue("")]
        public string Key { get; set; } = "";
        /// <summary>
        /// 名称
        /// </summary>
        [Column("sName")]
        [MaxLength(50)]
        [Required]
        [DefaultValue("")]
        public string Name { get; set; } = "";
        /// <summary>
        /// 链接
        /// </summary>
        [Column("sLink")]
        [MaxLength(200)]
        [Required]
        [DefaultValue("")]
        public string Link { get; set; } = "";
        /// <summary>
        /// 作者
        /// </summary>
        [Column("sAuthor")]
        [MaxLength(50)]
        [Required]
        [DefaultValue("")]
        public string Author { get; set; } = "";
        /// <summary>
        /// 标签
        /// </summary>
        [Column("sTag")]
        [MaxLength(50)]
        [Required]
        [DefaultValue("")]
        public string Tag { get; set; } = "";
        /// <summary>
        /// 简介
        /// </summary>
        [Column("sIntroduction", TypeName = "TEXT")]
        [Required]
        [DefaultValue("")]
        public string Introduction { get; set; } = "";
        /// <summary>
        /// 完本状态
        /// </summary>
        [Column("tStatus", TypeName = "TINYINT")]
        [Required]
        [DefaultValue(0)]
        public int Status { get; set; } = 0;
    }
}
