using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.OA.ServiceInterface.DataObjects;
using System.ServiceModel;

namespace ICP.OA.ServiceInterface
{
    /// <summary>
    /// IBulletinService
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IBulletinService
    {

        /// <summary>
        /// 编辑公告
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="subject">subject</param>
        /// <param name="content">content</param>
        /// <param name="fromTime">fromTime</param>
        /// <param name="toTime">toTime</param>
        /// <param name="departmentIds">departmentIds</param>
        /// <param name="departmentName">departmentName</param>
        /// <param name="userId">userId</param>
        /// <param name="priority">priority</param>
        /// <param name="bulletinType">bulletinType</param>
        /// <returns>Guid</returns>
        [FunctionInfomation]  
        [OperationContract(Name="Notify")]
        Guid SaveBulletin(Guid? id, string subject, string content, DateTime fromTime, DateTime toTime, Guid[] departmentIds, string departmentName, Guid userId, BulletinPriority priority, Guid bulletinType);

        /// <summary>
        /// 搜索公告
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="subject">subject</param>
        /// <param name="content">content</param>
        /// <param name="departmentName">departmentName</param>
        /// <param name="dateTimeFrom">dateTimeFrom</param>
        /// <param name="dataTimeTo">dataTimeTo</param>
        /// <param name="maxResults">maxResults</param>
        /// <returns>BulletinData</returns>
        [FunctionInfomation]  [OperationContract]
        List<BulletinData> GetBulletins(Guid? userId, string subject, string content, string departmentName, DateTime? dateTimeFrom, DateTime? dataTimeTo, int maxResults);
        /// <summary>
        /// 根据创建时间搜索公告
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
       // [OperationContract(Name="GetBulletinsByCreateTime")]
       // List<BulletinData> GetBulletins(Guid userId, DateTime fromTime, DateTime toTime); 
        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="ID">ID</param>
        [FunctionInfomation]  [OperationContract]
        void DeleteBulletinByID(Guid ID);

        /// <summary>
        /// 获取2.0中的BulletinType
        /// </summary>
        /// <returns>BulletinTypeData</returns>
        [FunctionInfomation]  [OperationContract]
        List<BulletinTypeData> GetBulletinTypeDatas();

        /// <summary>
        /// 获取2.0中的组织结构树
        /// </summary>
        [FunctionInfomation]  [OperationContract]
        List<OrganizationTreeData> GetOrganizationTreeData();
    }
}
