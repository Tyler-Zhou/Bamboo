﻿//-----------------------------------------------------------------------
// <copyright file="IAirImportService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.AirImport.ServiceInterface
{
    using ICP.Framework.CommonLibrary.Attributes;
    using System.ServiceModel;

    /// <summary>
    /// 空运进口服务
    /// </summary>
    [ServiceInfomation("空运进口服务")]
    [ServiceContract]
    public interface IAirImportService : IAirImportOrderService,
                                           IAirImportBusinessService,
                                           IAirImportFeeService
    {

    }
}
