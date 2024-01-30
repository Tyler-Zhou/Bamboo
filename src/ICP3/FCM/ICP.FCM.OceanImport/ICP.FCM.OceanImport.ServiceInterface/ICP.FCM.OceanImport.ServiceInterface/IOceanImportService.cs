//-----------------------------------------------------------------------
// <copyright file="IOceanImportService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.OceanImport.ServiceInterface
{
    using ICP.Framework.CommonLibrary.Attributes;
    using System.ServiceModel;

    /// <summary>
    /// 海运进口服务
    /// </summary>
    [ServiceInfomation("海运进口服务")]
    [ServiceContract]
    public interface IOceanImportService : IOIOrderService,
                                           IOIBusinessService,
                                           IOIFeeService,
                                           IOIPOItemService,
                                           IOIContainerService
    {

       
    }
}
