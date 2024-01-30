using ICP.FCM.Common.ServiceInterface.CompositeObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;
using ICP.FCM.Common.ServiceInterface.Interfaces;
using System.Collections.Generic;

namespace ICP.FCM.Common.ServiceInterface
{
    /// <summary>
    /// CSP业务服务
    /// </summary>
    [ServiceInfomation("CSP业务服务")]
    [ServiceContract]
    public interface ICSPBusinessService
    {
        #region 保存CSP业务映射信息
        /// <summary>
        /// 保存CSP业务映射信息
        /// </summary>
        /// <param name="saveRequest">保存对象</param>
        /// <returns></returns>
        [FunctionInfomation("保存CSP业务映射信息")]
        [OperationContract]
        SingleResult SaveMappingInfo(SaveRequestBusinessMapping saveRequest); 
        #endregion

        #region 保存CSP舱单
        /// <summary>
        /// 保存CSP舱单
        /// </summary>
        /// <param name="saveRequest">保存对象</param>
        /// <returns>返回ManyResult</returns>
        [FunctionInfomation("保存CSP舱单")]
        [OperationContract]
        void SaveBookingInfoForCSP(SaveRequestBookingDelegate saveRequest);

        #endregion

        #region 保存CSP运单
        /// <summary>
        /// 保存CSP运单
        /// </summary>
        /// <param name="saveRequest">保存对象</param>
        /// <returns>返回ManyResult</returns>
        [FunctionInfomation("保存CSP运单")]
        [OperationContract]
        void SaveShipmentInfoForCSP(SaveRequestShipmentInfoForCSP saveRequest);
        #endregion

        #region 保存CSP运单箱
        /// <summary>
        /// 保存CSP运单箱
        /// </summary>
        /// <param name="saveRequest">保存对象</param>
        /// <returns>返回ManyResult</returns>
        [FunctionInfomation("保存CSP运单箱")]
        [OperationContract]
        void SaveShipmentContainerForCSP(SaveRequestShipmentContainerForCSP saveRequest);
        #endregion

        #region 保存CSP运单提单
        /// <summary>
        /// 保存CSP运单提单
        /// </summary>
        /// <param name="saveRequest">保存对象</param>
        /// <returns>返回ManyResult</returns>
        [FunctionInfomation("保存CSP运单提单")]
        [OperationContract]
        void SaveShipmentItemForCSP(SaveRequestShipmentItemForCSP saveRequest);
        #endregion

        #region 保存CSP运单提单箱
        /// <summary>
        /// 保存CSP运单提单箱
        /// </summary>
        /// <param name="saveRequest">保存对象</param>
        /// <returns>返回ManyResult</returns>
        [FunctionInfomation("保存CSP运单提单箱")]
        [OperationContract]
        void SaveShipmentItemContainerForCSP(SaveRequestShipmentItemContainerForCSP saveRequest);
        #endregion
    }
}
