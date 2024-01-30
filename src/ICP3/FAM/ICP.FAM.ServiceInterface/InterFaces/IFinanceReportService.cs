namespace ICP.FAM.ServiceInterface
{
    using ICP.Framework.CommonLibrary.Attributes;
    using System.ServiceModel;
    using ICP.FAM.ServiceInterface.DataObjects;
    using System;
    using ICP.Common.ServiceInterface.DataObjects;

    /// <summary>
    /// 财务报表服务
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IFinanceReportService :  ICustomerBillReportService 
                                             ,IAccountingReportService
                                             ,IUFReportService
    {





    }
}
