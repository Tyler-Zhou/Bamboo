using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace CRM.Client.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Basic_Customer")]
    public partial class Basic_Customer
    {
           public Basic_Customer(){


           }
           /// <summary>
           /// Desc:主键
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public long ID {get;set;}

           /// <summary>
           /// Desc:代码
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Code {get;set;}

           /// <summary>
           /// Desc:名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Name {get;set;}

           /// <summary>
           /// Desc:联系人名
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string ContactName {get;set;}

           /// <summary>
           /// Desc:手机号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string PhoneNumber {get;set;}

           /// <summary>
           /// Desc:地点-省份ID
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public long ProvinceID {get;set;}

           /// <summary>
           /// Desc:地点-市ID
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public long CityID {get;set;}

           /// <summary>
           /// Desc:地点-县ID
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public long CountyID {get;set;}

           /// <summary>
           /// Desc:地点-镇ID
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public long TownID {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Address {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Remark {get;set;}

           /// <summary>
           /// Desc:销售员ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? SalesID {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:DateTime.Now
           /// Nullable:False
           /// </summary>           
           public DateTime CreateTime {get;set;}

           /// <summary>
           /// Desc:创建人ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long CreateUserID {get;set;}

           /// <summary>
           /// Desc:修改时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? ModifyTime {get;set;}

           /// <summary>
           /// Desc:修改人ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? ModifyUserID {get;set;}

           /// <summary>
           /// Desc:删除时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? DeleteTime {get;set;}

           /// <summary>
           /// Desc:删除人ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? DeleteUserID {get;set;}

    }
}
