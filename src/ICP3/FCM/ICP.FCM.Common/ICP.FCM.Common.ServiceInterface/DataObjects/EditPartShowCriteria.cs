using System;

namespace ICP.FCM.Common.ServiceInterface.DataObjects
{  
    /// <summary>
    /// 编辑界面数据源条件类
    /// </summary>
   public sealed class EditPartShowCriteria  
    {
       /// <summary>
       /// 业务ID
       /// </summary>
       public Guid OperationID { get; set; }
       /// <summary>
       /// 业务号
       /// </summary>
       public string OperationNo { get; set; }
       /// <summary>
       /// 口岸ID
       /// </summary>
       public Guid CompanyID { get; set; }
       /// <summary>
       /// 具体的单证号(如MBL No.,HBL NO.,SO No.)
       /// </summary>
       public object BillNo { get; set; }
       
       /// <summary>
       /// 无参构造函数
       /// </summary>
       public EditPartShowCriteria() { }
       /// <summary>
       /// 构造函数
       /// </summary>
       /// <param name="operationNo"></param>
       /// <param name="billNo"></param>
       public EditPartShowCriteria(string operationNo, object billNo)
       {
           OperationNo = operationNo;
           BillNo = billNo;
       }
    }
}
