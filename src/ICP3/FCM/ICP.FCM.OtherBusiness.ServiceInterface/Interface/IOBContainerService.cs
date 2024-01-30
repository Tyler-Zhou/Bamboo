using ICP.FCM.OtherBusiness.ServiceInterface.CompositeObjects;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ICP.FCM.OtherBusiness.ServiceInterface
{
    /// <summary>
    /// 其他业务箱信息服务
    /// </summary>
    [ServiceInfomation("其他业务箱信息服务")]
    [ServiceContract]
    public interface IOBContainerService
    {
        #region 获取箱列表
        /// <summary>
        /// 获取箱列表
        /// </summary>
        /// <param name="BookingID">业务ID</param>
        /// <returns></returns>
        [OperationContract(Name = "GetOtherContainerList1")]
        List<OBContainerList> GetOtherContainerList(Guid BookingID);

        /// <summary>
        /// 获取箱列表
        /// </summary>
        /// <param name="BookingID">订单ID</param>
        /// <param name="companyID">订单口岸ID</param>
        /// <returns></returns>
        [OperationContract(Name = "GetOtherContainerList2")]
        List<OBContainerList> GetOtherContainerList(Guid BookingID, Guid companyID); 
        #endregion

        #region 保存箱信息
        /// <summary>
        /// 保存箱信息
        /// </summary>
        /// <param name="ContainerInfo">箱信息</param>
        /// <returns></returns>
        [OperationContract]
        ManyResult SaveOtherContanerList(ContainerSaveRequest ContainerInfo); 
        #endregion

        #region 删除箱信息
        /// <summary>
        /// 删除箱信息
        /// </summary>
        /// <param name="ids">箱ID</param>
        /// <param name="removeByID">操作人</param>
        /// <param name="updateDates">更新时间</param>
        [OperationContract(Name = "RemoveOtherContainerList1")]
        void RemoveOtherContainerList(Guid?[] ids, Guid removeByID, DateTime?[] updateDates);

        /// <summary>
        /// 删除箱信息
        /// </summary>
        /// <param name="ids">箱ID</param>
        /// <param name="companyID">订单口岸ID</param>
        /// <param name="removeByID">操作人</param>
        /// <param name="updateDates">更新时间</param>
        [OperationContract(Name = "RemoveOtherContainerList2")]
        void RemoveOtherContainerList(Guid?[] ids,Guid companyID, Guid removeByID, DateTime?[] updateDates); 
        #endregion
    }
}
