using SqlSugar;

namespace CRM.Client.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Basic_City")]
    public partial class CityEntity : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public CityEntity()
        {


        }

        /// <summary>
        /// Desc:地点-省份ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(ColumnDescription = "地点-省份ID")]
        public long ProvinceID { get; set; }

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
