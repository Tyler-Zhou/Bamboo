using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace CRM.Client.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("CRM_VisitRecords")]
    public partial class CRM_VisitRecords
    {
           public CRM_VisitRecords(){


           }
           /// <summary>
           /// Desc:主键
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public long ID {get;set;}

           /// <summary>
           /// Desc:客户ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long CustomerID {get;set;}

           /// <summary>
           /// Desc:销售员ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long SalesID {get;set;}

           /// <summary>
           /// Desc:拜访时间
           /// Default:DateTime.Now
           /// Nullable:False
           /// </summary>           
           public DateTime VisitTime {get;set;}

           /// <summary>
           /// Desc:笔记
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Notes {get;set;}

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
