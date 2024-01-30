
namespace ICP.FCM.OceanExport.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.Framework.CommonLibrary.Attributes;
    using System.ServiceModel;
    using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
    using ICP.Common.ServiceInterface.DataObjects;

    /// <summary>
    /// AMSACIISF接口
    /// </summary>
    [ServiceInfomation("AMSACIISF")]
    [ServiceContract]
    public interface IOceanHBL2AmsAciIsfService
    {
        /// <summary>
        /// 获取HBL下的AMS列表
        /// </summary>
        /// <param name="HBLID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<OceanHBL2AmsAciIsf> GetAmsAciIsfOjbectsList(Guid HBLID,bool isEnglish);

        /// <summary>
        /// 查询相同Customer HBL上次ams列表
        /// </summary>
        /// <param name="HBLID"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<OceanHBL2AmsAciIsf> GetLastAmsAciIsfOjbectsList(Guid HBLID, bool isEnglish);


        [FunctionInfomation]
        [OperationContract]
        void SaveAmsAciIsfOjbects(List<OceanHBL2AmsAciIsf> oceanHBL2AmsAciIsf, Guid hblID, Guid saveBy, AMSEntryType amsEntryType);

        /// <summary>
        /// 根据HBLID查询所有箱信息
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<ContainerForAMS> GetContainerNumByHBLID(Guid hblid);

        /// <summary>
        /// 客户名称获取列表
        /// </summary>
        /// <param name="ShipperName"></param>
        /// <param name="ConsigneeName"></param>
        /// <param name="SellerName"></param>
        /// <param name="BuyerName"></param>
        /// <param name="ManufacturerName"></param>
        /// <param name="StuffingName"></param>
        /// <param name="ConsolidatorName"></param>
        /// <param name="ShipToName"></param>
        /// <param name="BookingPartyName"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<OceanHBL2AmsAciIsf> GetAmsListByCustomerNames(string ShipperName, string ConsigneeName, string SellerName, string BuyerName, string ManufacturerName, string StuffingName, string ConsolidatorName, string ShipToName, string BookingPartyName);

    }
}
