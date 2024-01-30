﻿using ICP.FCM.OceanExport.ServiceInterface.CompositeObjects;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ICP.FCM.OceanExport.ServiceInterface
{
    /// <summary>
    /// 海运出口费用服务
    /// </summary>
    [ServiceInfomation("海运出口费用服务")]
    [ServiceContract]
    public interface IOceanExportFeeService
    {
        #region 获取订单费用列表

        /// <summary>
        /// 获取订单费用列表
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="companyID">口岸ID</param>
        /// <returns>返回订单费用列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<OceanBookingFeeList> GetOceanOrderFeeList(Guid orderID, Guid companyID); 
        #endregion

        #region 保存订单费用
        /// <summary>
        /// 保存订单费用
        /// </summary>
        /// <param name="feeInfo">详见数据对象</param>
        /// <returns>返回ManyResult</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult SaveOceanOrderFeeList(FeeSaveRequest feeInfo); 
        #endregion

        #region 删除订单费用
        /// <summary>
        /// 删除订单费用
        /// </summary>
        /// <param name="ids">ID列表</param>
        /// <param name="companyID">口岸ID</param>
        /// <param name="removeByID">删除人 </param>
        /// <param name="updateDates">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveOceanOrderFeeList(Guid[] ids, Guid companyID, Guid removeByID, DateTime?[] updateDates); 
        #endregion
    }
}
