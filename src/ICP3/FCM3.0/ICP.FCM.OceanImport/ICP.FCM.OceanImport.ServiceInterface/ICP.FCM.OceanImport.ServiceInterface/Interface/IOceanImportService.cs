//-----------------------------------------------------------------------
// <copyright file="IOceanImportService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.OceanImport.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.FCM.OceanImport.ServiceInterface;
    using ICP.Framework;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using System.ServiceModel;

    /// <summary>
    /// 海运进口服务
    /// </summary>
    [ServiceInfomation("海运进口服务")]
    [ServiceContract]
    public interface IOceanImportService : IOceanImportOrderService,
                                           IOceanImportBusinessService,
                                           IOceanImportFeeService,
                                           IOceanImportPOItemService,
                                           IOceanImportContainerService
    {

    }
}
