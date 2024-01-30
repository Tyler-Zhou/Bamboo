using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Attributes;
using System.ServiceModel;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using System.Data;
using ICP.FCM.Common.ServiceInterface.CompositeObjects;

namespace ICP.FCM.Common.ServiceInterface
{
    /// <summary>
    /// 员工列表数据接口
    /// </summary>
    [ServiceInfomation("ICargoTrackingService服务")]
    [ServiceContract]
    public interface ICargoTrackingService
    {
        /// <summary>
        /// 获取箱列表
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        CargoTrackingInfo GetOperationContainersInfo(Guid operationID, OperationType operationType);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        CargoTrackingInfo GetCargoTrackingInfo(Guid operationID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cargoTrackingSaveRequest"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveOperationContainersInfo(CargoTrackingSaveRequest cargoTrackingSaveRequest);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cargoTracking"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveCargoTrackingInfo(CargoTrackingInfo cargoTracking);

        /// <summary>
        /// 保存海关放行/进场日期
        /// </summary>
        [FunctionInfomation]
        [OperationContract]
        void SaveCustomsAndGateIn(List<CargoTrackingContainerInfo> saveList,Guid saveBy);
        /// <summary>
        /// 保存开截港
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="openPort"></param>
        /// <param name="closePort"></param>
        /// <param name="portDesc"></param>
        /// <param name="isEnglish"></param>
        /// <param name="saveBy"></param>
        /// <param name="updateDate"></param>
        /// <param name="netETD"></param>
        /// <param name="Dock"></param>
        [FunctionInfomation]
        [OperationContract]
        void SaveOpenAndClosePort(Guid? Id, DateTime? openPort, DateTime? closePort, string portDesc, bool isEnglish, Guid saveBy, DateTime? updateDate, DateTime? netETD, string Dock);
        /// <summary>
        ///  保存/更新箱动态的事件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveCarrierContainerEvent(List<SaveCarrierContainerEventInput>  input);
        /// <summary>
        /// 删除箱动态事件
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="RemoveByID"></param>
        /// <param name="IsEnglish"></param>
        [FunctionInfomation]
        [OperationContract]
        void DeleteCarrierContainerEvent(Guid Id, Guid RemoveByID);
    }
}
