

//-----------------------------------------------------------------------
// <copyright file="IFCMCommonService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.Common.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using CommonData = ICP.Common.ServiceInterface.DataObjects;
    using System.ServiceModel;

    /// <summary>
    /// FCM公共服务
    /// </summary>
    [ServiceInfomation("FCM公共服务")]
    [ServiceContract]
    public interface IFCMCommonService :
        IAgentRequestService, IDocumentService, IFreightRateRequestService, IMailFaxLogService, IMemoService, IOperationCommonService, IBusinessService
    {
        
    }
}
