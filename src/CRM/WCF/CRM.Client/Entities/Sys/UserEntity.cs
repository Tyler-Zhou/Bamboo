using SqlSugar;

namespace CRM.Client.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Sys_User")]
    public partial class UserEntity : BaseEntity
    {
        public UserEntity()
        {


        }

        /// <summary>
        /// Desc:代码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Code { get; set; } = "";

        /// <summary>
        /// Desc:名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Name { get; set; } = "";

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Password { get; set; } = "";

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public byte[]? Avatar { get; set; }
    }
}
