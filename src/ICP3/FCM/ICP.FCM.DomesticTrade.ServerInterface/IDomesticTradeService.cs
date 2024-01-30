//-----------------------------------------------------------------------
// <copyright file="IDomesticTradeService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using ICP.Framework.CommonLibrary.Attributes;
using System.ServiceModel;

namespace ICP.FCM.DomesticTrade.ServiceInterface
{
    /// <summary>
    /// 内贸业务服务
    /// </summary>
    [ServiceInfomation("内贸业务服务")]
    [ServiceContract]
    public interface IDomesticTradeService : IDomesticTradeBookingService
                                        , IDomesticTradeContainerService
                                        , IDomesticTradeOrderService
                                        , IDomesticTradeOtherService
                                        , IDomesticTradeTruckService
                                        , IDomesticTradePOService
                                        , IDomesticTradeFeeService
    {

    }
}
