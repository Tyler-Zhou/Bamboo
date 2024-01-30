using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Attributes;
using System.ServiceModel;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;

namespace ICP.FCM.OceanExport.ServiceInterface
{
    /// <summary>
    /// 海运出口报关委托服务接口
    /// </summary>
    [ServiceContract]
    public interface IOceanExportCustomsService
    {
        /// <summary>
        /// 获取报关信息
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns>返回报关信息</returns>
        [FunctionInfomation]
        [OperationContract]
        OceanCustoms GetOceanCustomsInfo(Guid id);

        /// 判断抬头是否存在
        /// </summary>
        /// <param name="oceanBookingID">订单ID</param>
        /// <param name="oceanContainerID">箱号ID</param>
        /// <param name="title">抬头</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        Boolean IsExisted(Guid oceanBookingID, Guid oceanContainerID, String title);

        /// <summary>
        /// 获取报关信息
        /// </summary>
        /// <param name="oceanBookingID">订单ID</param>
        /// <param name="oceanContainerID">箱号ID</param>
        /// <returns>返回报关信息</returns>
        [FunctionInfomation]
        [OperationContract]
        List<OceanCustoms> GetOceanCustomsServiceList(Guid oceanBookingID, Guid oceanContainerID);

        /// <summary>
        /// 保存报关委托信息
        /// </summary>
        /// <param name="obj">报关对象</param>
        /// <returns></returns>        
        [FunctionInfomation]
        [OperationContract]
        Boolean SaveOceanCustomsInfo(OceanCustoms obj);

        /// <summary>
        /// 删除报关委托信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract]
        Boolean RemoveOceanCustomsInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate);
    }
}
