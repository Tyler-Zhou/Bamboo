using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bamboo.Server.Models
{
    /// <summary>
    /// 章节实体
    /// </summary>
    [Table("tb_Book_Chapter", Schema = "dbo")]
    public class ChapterEntity : BaseEntity
    {
        ///// <summary>
        ///// BookId
        ///// </summary>
        //[Required]
        //[ForeignKey("iBookId")]
        //public BookEntity BookEntity { get; set; }

        /// <summary>
        /// 书籍标识键
        /// </summary>
        [Column("sBookKey")]
        [MaxLength(50)]
        [Required]
        [DefaultValue("")]
        public string BookKey { get; set; } = "";

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
        /// 内容
        /// </summary>
        [Column("sContent",TypeName ="TEXT")]
        [Required]
        [DefaultValue("")]
        public string Content { get; set; } = "";
        /// <summary>
        /// 链接
        /// </summary>
        [Column("sLink")]
        [MaxLength(200)]
        [Required]
        [DefaultValue("")]
        public string Link { get; set; } = "";
        /// <summary>
        /// 排序索引
        /// </summary>
        [Column("iOrderIndex")]
        [MaxLength(200)]
        [Required]
        [DefaultValue(0)]
        public int OrderIndex { get; set; } = 0;
    }
}
