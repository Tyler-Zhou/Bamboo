using System;
using System.Collections.Generic;
using System.Data;
using ICP.Framework.CommonLibrary.Attributes;
using System.ServiceModel;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.Common.ServiceInterface
{
    /// <summary>
    /// 员工列表数据接口
    /// </summary>
    [ServiceInfomation("员工列表数据接口")]
    [ServiceContract]
    public interface IStaffService
    {
        /// <summary>
        /// 获取员工列表数据
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<StaffObjects> GetStaffList(Guid operationID);
        /// <summary>
        /// 获取参与者信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="operationID"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        StaffObjects GetStaffInfo(Guid userID, Guid operationID, OperationType operationType);

        /// <summary>
        /// 保存业务参与人
        /// </summary>
        /// <param name="staff"></param>
        [FunctionInfomation]
        [OperationContract]
        void SaveAssistantInfo(StaffObjects staff);

        /// <summary>
        /// 获取海运业务固定角色
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        DataTable GetOceanFixedRoles();
        /// <summary>
        /// 获取参与人列表
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="operationID"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<StaffObjects> GetAssistantList(Guid userID, Guid operationID, OperationType operationType);
    }
}
