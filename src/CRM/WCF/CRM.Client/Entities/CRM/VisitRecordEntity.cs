using SqlSugar;
using System;

namespace CRM.Client.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("CRM_VisitRecords")]
    public partial class VisitRecordEntity : BaseEntity
    {
           public VisitRecordEntity(){


           }

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

    }
}
