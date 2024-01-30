using System;
using System.Collections.Generic;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;

namespace ICP.FRM.ServiceInterface
{
    /// <summary>
    /// 询价的接口
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IInquireRatesService
    {
        /// <summary>
        /// 获取海运询价列表
        /// </summary>
        /// <param name="pol">POL</param>
        /// <param name="delivery">Delivery</param>
        /// <param name="pod">POD</param>
        /// <param name="commodity">Commodity</param>
        /// <param name="inquireOrRespondBy">InquireBy OR RespondBy</param>
        /// <param name="isUnReply">isUnReply</param>
        /// <param name="durationFrom">Duration From</param>
        /// <param name="durationTo">Duration To</param>
        /// <param name="currentUserID">Current UserID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        InquierOceanRatesResult GetInquireOceanRateList(
           string pol,
           string delivery,
           string pod,
           string commodity,
           Guid? inquireOrRespondBy,
           bool isUnReply,
           DateTime? durationFrom,
           DateTime? durationTo,
           Guid currentUserID);

        /// <summary>
        /// 获取海运询价列表
        /// </summary>
        /// <param name="no">NO</param>
        /// <param name="pol">POL</param>
        /// <param name="delivery">Delivery</param>
        /// <param name="pod">POD</param>
        /// <param name="commodity">Commodity</param>
        /// <param name="inquireOrRespondBy">InquireBy OR RespondBy</param>
        /// <param name="isUnReply">isUnReply</param>
        /// <param name="durationFrom">Duration From</param>
        /// <param name="durationTo">Duration To</param>
        /// <param name="StrQuery"></param>
        /// <param name="currentUserID">Current UserID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract(Name = "GetInquireOceanRateList1")]
        InquierOceanRatesResult GetInquireOceanRateList(
            string no,
           string pol,
           string delivery,
           string pod,
           string commodity,
           Guid? inquireOrRespondBy,
           bool? isUnReply,
           DateTime? durationFrom,
           DateTime? durationTo, string StrQuery, 
           Guid currentUserID);

        /// <summary>
        /// 获取海运历史询价列表
        /// </summary>
        /// <param name="inquirePriceID">历史记录不包含自身ID，排除</param>
        /// <param name="parentID">历史记录不包含自身的parentID，排除</param>
        /// <param name="polID"></param>
        /// <param name="podID"></param>
        /// <param name="deliveryID"></param>
        /// <param name="carrierID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        InquierOceanRatesResult GetInquireOceanRateHistoryList(Guid inquirePriceID, Guid? parentID, Guid? polID, Guid? podID, Guid? deliveryID, Guid? carrierID);

        [FunctionInfomation]
        [OperationContract]
        InquierAirRatesResult GetInquireAirRateList(
           string pol,
           string delivery,
           string pod,
           string commodity,
           Guid? inquireOrRespondBy,
           bool? isUnReply,
           DateTime? durationFrom,
           DateTime? durationTo, string StrQuery, 
           Guid currentUserID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="no">Inquire Trucking No</param>
        /// <param name="pol"></param>
        /// <param name="delivery"></param>
        /// <param name="pod"></param>
        /// <param name="commodity"></param>
        /// <param name="inquireOrRespondBy"></param>
        /// <param name="isUnReply"></param>
        /// <param name="durationFrom"></param>
        /// <param name="durationTo"></param>
        /// <param name="currentUserID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        InquierTruckingRatesResult GetInquireTruckingRateList(
            string no,
           string pol,
           string delivery,
           string pod,
           string commodity,
           Guid? inquireOrRespondBy,
           bool? isUnReply,
           DateTime? durationFrom,
           DateTime? durationTo, string StrQuery, 
           Guid currentUserID);

        /// <summary>
        /// 获取拖车历史询价列表
        /// </summary>
        /// <param name="inquirePriceID">历史记录不包含自身ID，排除</param>
        /// <param name="parentID">历史记录不包含自身的parentID，排除</param>
        /// <param name="polID"></param>
        /// <param name="podID"></param>
        /// <param name="deliveryID"></param>
        /// <param name="carrierID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        InquierTruckingRatesResult GetInquireTruckingRateHistoryList(Guid inquirePriceID, Guid? parentID, Guid? polID,
            Guid? podID, Guid? deliveryID, Guid? carrierID);

        /// <summary>
        /// 获取General Info面板的数据
        /// </summary>
        /// <param name="inquireRateID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        InquierOceanRate GetInquierOceanRateInfoForInquireBy(Guid inquireRateID, Guid userID);

        [FunctionInfomation]
        [OperationContract]
        InquierAirRate GetInquierAirRateInfoForInquireBy(Guid inquireRateID, Guid userID);

        [FunctionInfomation]
        [OperationContract]
        InquierTruckingRate GetInquierTruckingRateInfoForInquireBy(Guid inquireRateID, Guid userID);

        /// <summary>
        /// 获取Discussing列表
        /// </summary>
        /// <param name="inquireOceanRateID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<InquireDiscussing> GetInquireRateDiscussingList(Guid inquireRateID, Guid userID);

        /// <summary>
        /// 保存询价期望信息
        /// </summary>
        /// <param name="Type">InquierType</param>
        /// <param name="UnitIDs">箱型的ID</param>
        /// <param name="ExpCarrierID">ExpCarrierID</param>
        /// <param name="CustomerID">CustomerID</param>
        /// <param name="ExpTermID">ExpTermID</param>
        /// <param name="CargoVolume">CargoVolume</param>
        /// <param name="CargoWeightMeasurement">CargoWeightMeasurement</param>
        /// <param name="ExpComm">ExpComm</param>
        /// <param name="POLID">POLID</param>
        /// <param name="PODID">PODID</param>
        /// <param name="DeliveryID">DeliveryID</param>
        /// <param name="Discussing">Discussing</param>
        /// <param name="RespondByID">RespondByID</param>
        /// <param name="SaveBy">SaveBy</param>
        /// <returns>返回新询价的 "ID","No"</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveInquireRateInfo(
               Guid id,string no,
               InquierType type,
               Guid[] rateIDs,
               Guid[] rateUnitIDs,
               decimal[] rateRates,
               Guid? shippingLineID,
               Guid? customerID,
               string expCarrier,
               Guid? polID,
               Guid? podID,
               Guid? placeOfDeliveryID,
               string expCommodity,
               Guid? expTransportClauseID,
               string cargoWeight,
               string measurement,
               string cargoReady,
               string cartonsOrPallets,
               string mAWB,
               string hAWB,
               string zipCode,
               string estimateTimeOfDelivery,
               bool isWillBooking,
               string expPrice,
               string discussingWhenNew,
               Guid? respondByID,
               Guid? inquireByID,
               DateTimeOffset sentTime,
               DateTime? updateDate,
               Guid companyID, Guid currentUserID);

        /// <summary>
        /// 用事务的方式保存海运同一个询价列表
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="sentTime"></param>
        /// <param name="companyID"></param>
        /// <param name="currentUserID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SaveOceanInquireRateWithTrans(List<InquierOceanRate> datas, DateTimeOffset sentTime, Guid companyID,Guid currentUserID);

        [FunctionInfomation]
        [OperationContract]
        ManyResultData SaveAirInquireRateWithTrans(List<InquierAirRate> datas, DateTimeOffset sentTime, Guid companyID);

        [FunctionInfomation]
        [OperationContract]
        ManyResultData SaveTruckingInquireRateWithTrans(List<InquierTruckingRate> datas, DateTimeOffset sentTime, Guid companyID, Guid currentUserID);

        /// <summary>
        /// 增加和删除箱型(海运，空运，拖车询价通用)
        /// </summary>
        /// <param name="inquireRateID"></param>
        /// <param name="unitIDs"></param>
        /// <param name="changeByID"></param>
        /// <param name="updateDate"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeRateUnit(Guid inquireRateID,
          Guid[] unitIDs,
          Guid changeByID,
          DateTime? updateDate);

        /// <summary>
        /// 把该条询价指定人的未读消息改成已读(海运，空运，拖车询价通用)
        /// </summary>
        /// <param name="inquireOceanRateID"></param>
        /// <param name="userID">指定人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract]
        void ChangeDiscussingToHadRead(
            Guid inquireRateID,
            Guid userID);

        /// <summary>
        /// 删除询价(海运，空运，拖车询价通用)
        /// </summary>
        /// <param name="ids">ID 列表</param>
        /// <param name="removeByID">删除人 </param>
        /// <param name="updateDates">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveInquireRate(
            Guid[] inquireRateIDs,
            DateTime?[] updateDates,
            Guid removeByID);

        ///// <summary>
        /////  发送消息
        ///// </summary>
        ///// <param name="InquierPriceID">询价ID</param>
        ///// <param name="Content">内容</param>
        ///// <param name="SaveByID">发起人</param>
        ///// <param name="InquierPriceUpdateDate">询价的更新日期</param>
        ///// <returns>返回询价的 "UpdateDate"  返回服务器的当前日期(因为有可能在发起此类消息时客户端的日期不对,需刷新页面)"SaveDate"</returns>
        //[FunctionInfomation]
        //SingleResult SendInquierMessage(Guid InquierPriceID, string Content, Guid SaveByID, DateTime? InquierPriceUpdateDate);

        /// <summary>
        /// 新增发送Discussings(海运，空运，拖车询价通用)
        /// </summary>
        /// <returns>返回询价的 "UpdateDate"  返回服务器的当前日期(因为有可能在发起此类消息时客户端的日期不对,需刷新页面)"SaveDate"</returns>
        [FunctionInfomation]
        [OperationContract]
        void SendDiscussings(Guid id,
            Guid inquireRateID,
            Guid fromID,
            Guid? toID,
            DateTimeOffset sentTime,
            string content);


        ///// <summary>
        ///// 转给其它商务员
        ///// </summary>
        ///// <param name="InquierPriceID">InquierPriceID</param>
        ///// <param name="RespondID">RespondID</param>
        ///// <param name="SaveByID">SaveByID</param>
        ///// <param name="InquierPriceUpdateDate">InquierPriceUpdateDate</param>
        ///// <returns>返回询价的 "UpdateDate"</returns>
        //[FunctionInfomation]
        //SingleResult TransitRespond(Guid InquierPriceID, Guid RespondID, Guid SaveByID, DateTime? InquierPriceUpdateDate);

        /// <summary>
        /// 更改Respond by，商务员将此inquire rate 移交给另一个商务员(海运，空运，拖车询价通用)
        /// </summary>
        /// <param name="inquireRateID"></param>
        /// <param name="newRespondByID"></param>
        /// <param name="postscript"></param>
        /// <param name="saveByID"></param>
        /// <param name="inquireRateUpdateDate"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        void TransitRespondMan(Guid inquireRateID,
            Guid? inquireByID,
            Guid? oldRespondByID,
            Guid newRespondByID,
            string postscript,
            DateTimeOffset sentTime,
            Guid saveByID);

        /// <summary>
        /// 批量修改拖车询价的FUEL(燃料)
        /// </summary>
        /// <param name="inquireRateIDs"></param>
        /// <param name="updateDates"></param>
        /// <param name="fuel"></param>
        /// <param name="saveByID"></param>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData BatchUpdateChargeFuelForInquirePrices(
            Guid[] inquireRateIDs,
            DateTime?[] updateDates,
            Decimal fuel,
            Guid saveByID);

        /// <summary>
        /// 检查是否存在相同港口而且没有过期的拖车询价
        /// </summary>
        /// <param name="fromID"></param>
        /// <param name="toID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        bool CheckExistTruckingRateSamePort(Guid fromID, Guid toID);

        /// <summary>
        /// 获取询价询问人列表
        /// </summary>
        /// <param name="inquirePriceID"></param>
        /// <param name="parentID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<InquirePriceInquireBys> GetInquirePriceInquireBys(Guid inquirePriceID, Guid? parentID);

        /// <summary>
        /// 处理询价信息
        /// </summary>
        /// <param name="ID">询价人信息ID</param>
        /// <param name="Handled">询价人询价状态</param>
        /// <returns>处理结果</returns>
        [FunctionInfomation]
        [OperationContract]
        void HandledInquirePriceInquireBys(Guid ID, Boolean Handled);

        /// <summary>
        /// 询价替换
        /// </summary>
        /// <param name="oldID">待替换询价ID</param>
        /// <param name="newID">新询价ID</param>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ReplaceInquirePrice(Guid oldID, Guid newID);

        /// <summary>
        /// 确认业务询价
        /// </summary>
        /// <param name="oceanBookingID">业务ID</param>
        /// <param name="userID">当前用户ID</param>
        /// <param name="isConfirm">true确认，false取消确认</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ConfirmInquirePriceToShipment(Guid oceanBookingID, Guid userID, Boolean isConfirm);
    }
}
