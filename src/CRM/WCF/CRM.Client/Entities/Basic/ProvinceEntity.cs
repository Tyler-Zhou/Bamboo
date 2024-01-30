using SqlSugar;

namespace CRM.Client.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Basic_Province")]
    public partial class ProvinceEntity : BaseEntity
    {
        public ProvinceEntity()
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
    }
}
