using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ICP.TaskCenter.ServiceInterface
{
    /// <summary>
    /// 操作视图服务类
    /// </summary>
    [ServiceContract]
    public interface IOperationViewService
    {
        /// <summary>
        /// 获取用户操作视图列表
        /// </summary>
        /// <param name="parentId">父节点Id</param>
        /// <returns>节点集合</returns>
        [OperationContract]
        List<NodeInfo> GetOperationViewList(Guid? parentId);


        /// <summary>
        /// 获取用户视图空间列表
        /// </summary>
        /// <param name="parentId">父节点Id</param>
        /// <returns>节点集合</returns>
        [OperationContract]
        List<NodeInfo> GetUserWorkSpaceList(Guid? parentId);

        /// <summary>
        /// 获取视图空间中的操作视图列表
        /// </summary>
        /// <param name="parentId">父节点Id</param>
        /// <param name="workSpaceId">视图ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>节点集合</returns>
        [OperationContract]
        List<NodeInfo> GetWorkSpaceOperationViewList(Guid? parentId, Guid? workSpaceId, Guid userId);

        /// <summary>
        ///  获得用户下属的列表
        /// </summary>
        /// <returns>节点集合</returns>
        [OperationContract]
        List<NodeInfo> GetSubordinateUserList();

        /// <summary>
        ///  获得用户指定部门下属的列表
        /// </summary>
        /// <param name="depID">上级UserId</param>
        /// <returns>节点集合</returns>
        [OperationContract]
        List<NodeInfo> GetDepartmentUserList(Guid depID);
        /// <summary>
        /// 获取当前登录用户的顶级操作视图节点
        /// </summary>
        /// <returns>节点集合</returns>
        [OperationContract]
        List<NodeInfo> GetUserRootOperationViewList();

        /// <summary>
        /// 获取子节点信息
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns>节点集合</returns>
        [OperationContract]
        List<NodeInfo> GetWorkSpaceSonViewList(Guid parentId, Guid? viewparentId);
        /// <summary>
        ///  返回替换以后的客户名称
        /// </summary>
        /// <param name="codeName">需要替换的客户名称</param>
        /// <returns>客户名称</returns>
        [OperationContract]
        string GetDeleteMarkerForInputStr(string codeName);

        /// <summary>
        /// 保存协助同事信息
        /// </summary>
        /// <param name="userAssists">协助同事实体类</param>
        /// <returns>是否保存成功</returns>
        [OperationContract]
        int UserAssistsSave(UserAssistsType userAssists);

        /// <summary>
        /// 返回协助同事列表信息
        /// </summary>
        /// <param name="userId">当前用户</param>
        /// <param name="date">当前时间</param>
        /// <returns>协助同事列表信息</returns>
        [OperationContract]
        List<UserAssistsType> GetUserAssistsList(Guid userId, DateTime date);
        /// <summary>
        /// 获得协助同事列表
        /// </summary>
        /// <returns>协助同事列表</returns>
        [OperationContract]
        List<NodeInfo> GetSubordinateUserAssistsList();
    }
}
