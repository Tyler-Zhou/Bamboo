//-----------------------------------------------------------------------
// <copyright file="IOceanExportService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.DomesticTrade.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Sys.ServiceInterface.DataObjects;
    using System.ServiceModel;


    /// <summary>
    /// 内贸业务服务
    /// </summary>
    [ServiceInfomation("内贸业务服务")]
    [ServiceContract]
    public interface IDomesticTradeService :IDomesticTradeBookingService
                                         ,IDomesticTradeContainerService
                                         , IDomesticTradeOrderService
                                         , IDomesticTradeOtherService
                                         , IDomesticTradeTruckService
                                            , IDomesticTradeFeeService
    {

    }
}
