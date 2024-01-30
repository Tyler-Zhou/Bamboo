using ICP.Framework.CommonLibrary.Attributes;
using ICP.Sys.ServiceInterface.DataObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ICP.FCM.OceanExport.ServiceInterface
{
    /// <summary>
    /// 海运出口其它服务
    /// </summary>
    [ServiceInfomation("海运出口其它服务")]
    [ServiceContract]
    public interface IOceanExportOtherService
    {
        #region Other

        /// <summary>
        /// 根据销售客户和口岸公司获取揽货方式
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="companyId">口岸公司</param>
        /// <returns>返回揽货方式</returns>
        [FunctionInfomation]
        [OperationContract]
        ICP.Common.ServiceInterface.DataObjects.DataDictionaryInfo GetSalesType(
            Guid customerID,
            Guid companyId);


        /// <summary>
        /// 判断客户和公司是否在同一个国家
        /// </summary>
        /// <param name="customerId">客户ID( 用cusomterId获得客户的国家ID)</param>
        /// <param name="companyId">口岸公司ID(compnyId用于在获取配置里对应的客户的国家ID)</param>
        /// <returns>是否在同一个国家</returns>
        [FunctionInfomation]
        [OperationContract]
        bool IsCustomerAndCompanySameCountry(Guid customerId, Guid companyId);

        /// <summary>
        /// 船公司是否存在于该订舱单的口岸公司的电子订舱EDI配置
        /// </summary>
        /// <param name="carrierId">船公司GUID</param>
        /// <param name="companyId">口岸公司GUID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        bool IsCarrierInCompanyEdiBooking(Guid carrierId, Guid companyId);

        /// <summary>
        /// 判断港口的国家是否存在公司配置中的客户的国家
        /// </summary>
        /// <param name="portId">港口ID</param>
        /// <param name="companyId">口岸公司ID</param>
        /// <returns>是否存在</returns>
        [FunctionInfomation]
        [OperationContract]
        bool IsPortCountryExistCompanyConfig(Guid portId, Guid? companyId);

        /// <summary>
        /// 获取指定口岸公司的海运订舱单中所有揽货人(默认是有职位的用户)
        /// </summary>
        /// <param name="organizationIDs">部门ID(包括子部门的业务)</param>
        [FunctionInfomation]
        [OperationContract]
        List<UserList> GetOrderSalesByCompanyIDs(
            Guid[] organizationIDs,bool isEnglish);

        #endregion
    }
}