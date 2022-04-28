using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bamboo.Server.Entities
{
    /// <summary>
    /// 用户
    /// </summary>
    [Table("sys_User", Schema = "dbo")]
    public class UserEntity : BaseEntity
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Column("sAccount")]
        [MaxLength(50)]
        [Required]
        [DefaultValue("")]
        public string Account { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Column("sUserName")]
        [MaxLength(50)]
        [Required]
        [DefaultValue("")]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Column("sPassword")]
        [MaxLength(50)]
        [Required]
        [DefaultValue("")]
        public string Password { get; set; }
    }
}
