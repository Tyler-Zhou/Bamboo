using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace CRM.Client.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Sys_User")]
    public partial class Sys_User
    {
           public Sys_User(){


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
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Password {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public byte[] Avatar {get;set;}

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
