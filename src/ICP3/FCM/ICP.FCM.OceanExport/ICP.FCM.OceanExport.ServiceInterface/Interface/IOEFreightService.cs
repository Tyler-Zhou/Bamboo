using System;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using System.ServiceModel;

namespace ICP.FCM.OceanExport.ServiceInterface
{
    [ServiceInfomation("海运出口拖车服务")]
    [ServiceContract]
    public interface IOEFreightService
    {
        [OperationContract]
        FreightDataList GetFright(string ContractNo,
            Guid? CarrierID,
            Guid? PlaceOfReceiptID,
            Guid? POLID,
            Guid? PODID,
            Guid? FinalDestinationID,
            string Comm,
            DateTime? FromDate,
            DateTime? ToDate);
        /// <summary>
        /// 获取合约列表
        /// </summary>
        /// <param name="ContractNo">合约号</param>
        /// <param name="CarrierID">船东</param>
        /// <param name="POLID">POL</param>
        /// <param name="PODID">POD</param>
        /// <param name="PlaceOfReceiptID">POR</param>
        /// <param name="Comm">箱型</param>
        /// <param name="FromDate">fromdate</param>
        /// <param name="ToDate">todate</param>
        /// <param name="freightId">已有的合约明细ID</param>
        /// <returns></returns>
        [OperationContract(Name = "GetFreightsWithFreightId")]
        FreightDataList GetFreight(string ContractNo,
          Guid? CarrierID,
          Guid? PlaceOfReceiptID,
          Guid? POLID,
          Guid? PODID,
          Guid? FinalDestinationID,
          string Comm,
          DateTime? FromDate,
          DateTime? ToDate,
          Guid? freightId, SelectType type);


        /// <summary>
        /// 获得合约ByID
        /// </summary>
        /// <param name="FreightRateID"></param>
        /// <returns></returns>
        [OperationContract]
        FreightDataList GetFrightByID(Guid FreightRateID);
    }
}
