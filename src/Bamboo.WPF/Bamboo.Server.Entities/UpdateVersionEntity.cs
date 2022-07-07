using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bamboo.Server.Entities
{
    /// <summary>
    /// 
    /// </summary>
    [Table("tb_SystemVersion", Schema = "dbo")]
    public class UpdateVersionEntity : BaseEntity
    {
        [Column("sFileMD5")]
        [MaxLength(32)]
        [Required]
        [DefaultValue("")]
        public string MD5
        {
            get;
            set;
        }
        [Column("iPubTime")]
        [Required]
        public long PubTime
        {
            get;
            set;
        }
        [Column("sVersion")]
        [MaxLength(20)]
        [Required]
        [DefaultValue("")]
        public string Version
        {
            get;
            set;
        }
        [Column("sUrl")]
        [MaxLength(255)]
        [Required]
        [DefaultValue("")]
        public string Url
        {
            get;
            set;
        }
        [Column("sName")]
        [MaxLength(64)]
        [Required]
        [DefaultValue("")]
        public string Name
        {
            get;
            set;
        }
    }
}
