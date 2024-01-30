    using System;
    using ICP.Framework.CommonLibrary.Attributes;
    using System.ServiceModel;


namespace ICP.FCM.OceanExport.ServiceInterface
{
    /// <summary>
    /// 海运出口签收账单修订服务
    /// 2013-08-08 joe initial
    /// </summary>
    [ServiceContract]
   public   interface IAcceptBillReviseService
   {

       #region 签收海进修订

       /// <summary>
        ///  通过海出订单ID签收海进修订账单信息
        ///  2013-07-22 joe 
        /// </summary>
        /// <param name="OEBookingID">海出业务ID</param>
        /// <param name="remark">签收备注</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        int AcceptBillRevise(Guid OEBookingID, string remark,Guid UserID);
       
        #endregion
    }
}
