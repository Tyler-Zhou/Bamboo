using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace CRM.Client.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Basic_Employee")]
    public partial class EmployeeEntity : BaseEntity
    {
        public EmployeeEntity()
        {


        }
        /// <summary>
        /// Desc:工号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string JobNumber { get; set; } = "";

        /// <summary>
        /// Desc:身份证号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string IDNumber { get; set; } = "";

        /// <summary>
        /// Desc:名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Name { get; set; } = "";

        /// <summary>
        /// Desc:工作手机号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string WorkPhoneNumber { get; set; } = "";

        /// <summary>
        /// Desc:私人手机号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string PersonalPhoneNumber { get; set; } = "";

        /// <summary>
        /// Desc:性别
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public byte Sex { get; set; }

        /// <summary>
        /// Desc:地点-省份ID
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public long ProvinceID { get; set; }

        /// <summary>
        /// Desc:地点-城市(区)ID
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public long CityID { get; set; }

        /// <summary>
        /// Desc:地点-镇(区)ID
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public long CountyID { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Address { get; set; } = "";

        /// <summary>
        /// Desc:备注
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Remark { get; set; } = "";

        /// <summary>
        /// Desc:
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public byte Status { get; set; }
    }
}
