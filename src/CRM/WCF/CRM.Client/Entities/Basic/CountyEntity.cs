using SqlSugar;

namespace CRM.Client.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Basic_County")]
    public partial class CountyEntity : BaseEntity
    {
        public CountyEntity()
        {


        }
        /// <summary>
        /// Desc:地点-市ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(ColumnDescription = "地点-市ID")]
        public long CityID { get; set; }

        /// <summary>
        /// Desc:代码
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(ColumnDescription = "代码")]
        public string Code { get; set; } = "";

        /// <summary>
        /// Desc:名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(ColumnDescription = "名称")]
        public string Name { get; set; } = "";
    }
}
