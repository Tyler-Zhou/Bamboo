using ICP.FCM.Common.ServiceInterface.CompositeObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.ServiceInterface.CompositeObjects;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using CustomerSearchType = ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.CustomerSearchType;
using DateSearchType = ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.DateSearchType;
using NoSearchType = ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.NoSearchType;
using PortSearchType = ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.PortSearchType;

namespace ICP.FCM.OtherBusiness.ServiceInterface
{
    /// <summary>
    /// 其他业务服务
    /// </summary>
    [ServiceInfomation("其他业务服务")]
    [ServiceContract]
    public interface IOBBookingService
    {
        #region 以事务方式保存订单信息、费用信息，箱信息
        /// <summary>
        /// 以事务方式保存订单信息、费用信息，箱信息
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <param name="fees"></param>
        /// <param name="container"></param>
        /// <param name="delegates"></param>
        /// <returns></returns>
        [OperationContract]
        Dictionary<Guid, SaveResponse> SaveOtherBusinessWithTrans(OtherBusinessSaveRequest saveRequest,
             List<FeeSaveRequest> fees, List<ContainerSaveRequest> container, List<SaveRequestBookingDelegate> delegates); 
        #endregion

        #region 获取订单列表

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="companyIDs">公司列表</param>
        /// <param name="departIDs">部门列表</param>
        /// <param name="otOperationTypes">其他业务类型列表(1:本地业务;2:代订舱;3:其他业务;4:指定货;5:FBA空派;6:FBA海派)</param>
        /// <param name="no">业务号</param>
        /// <param name="customerName">客户名</param>
        /// <param name="polName">装货港名</param>
        /// <param name="podName">卸货港名</param>
        /// <param name="placeOfDeliveryName">交货地名</param>
        /// <param name="VesselName">船名</param>
        /// <param name="VoyageNo">航次</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="NotifyPart"></param>
        /// <param name="orderState">订单状态()</param>
        /// <param name="salesID">业务员</param>
        /// <param name="overseasFilerID">海外客服</param>
        /// <param name="dateSearchType">日期搜索类型()</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="ContainerNo">箱号</param>
        /// <param name="MBLNo">MBL</param>
        /// <param name="HBLNo">HBL</param>
        /// <param name="Consignee">发货人</param>
        /// <param name="Shipper">收货人</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回订单列表</returns>
        [OperationContract]
        List<OtherBusinessList> GetOtherBusinessList(
           Guid[] companyIDs,
           Guid[] departIDs,
            int[] otOperationTypes,
           string no,
           string MBLNo,
           string HBLNo,
           string ContainerNo,
           string Consignee,
           string Shipper,
           string NotifyPart,
           string customerName,
           string polName,
           string podName,
           string placeOfDeliveryName,
           string VesselName,
           string VoyageNo,
           bool? isValid,
           OBOrderState? orderState,
           Guid? salesID,
           Guid? overseasFilerID,
           DateSearchType dateSearchType,
           DateTime? beginTime,
           DateTime? endTime,
           int maxRecords);
        #endregion

        #region 快速查询订单列表
        /// <summary>
        /// 快速查询订单列表
        /// </summary>
        /// <param name="companyIDs">公司列表</param>
        /// <param name="otOperationTypes">其他业务类型列表(1:本地业务;2:代订舱;3:其他业务;4:指定货;5:FBA空派;6:FBA海派)</param>
        /// <param name="noSearchType">单号搜索类型(0:全部,1:业务号,2:提单号,3:箱号,4:订舱号,5:合约号)</param>
        /// <param name="no">业务号</param>
        /// <param name="customerSearchType">客户搜索类型(0:全部,1:客户,2:船东,3:承运人,4:发货人,5:收货人,6:通知人,7:对单人)</param>
        /// <param name="customerName">客户名</param>
        /// <param name="portSearchType">港口搜索类型(0:全部,1:收货地,2:装货港,3:卸货港,4:交货地,5:最终目的地)</param>
        /// <param name="portName">港口名</param>
        /// <param name="dateSearchType">日期搜索类型(0:全部,1:离港日,2:到港日,3:订舱日,4:创建日,)</param>
        /// <param name="salesId">揽货人</param>
        /// <param name="OperatorID">操作人</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回其他业务列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<OtherBusinessList> GetOtherBusinessListForFaster(
            Guid[] companyIDs,
            int[] otOperationTypes,
            NoSearchType noSearchType,
            string no,
            CustomerSearchType customerSearchType,
            string customerName,
            PortSearchType portSearchType,
            string portName,
            DateSearchType dateSearchType,
            Guid salesId,
            Guid? OperatorID,
            DateTime? beginTime,
            DateTime? endTime,
            bool? isValid,
            int maxRecords); 
        #endregion

        #region 刷新订单列表

        /// <summary>
        /// 刷新订单列表
        /// </summary>
        /// <param name="orderIDs">订单ID集合</param>
        /// <param name="companyIDs">口岸ID集合</param>
        /// <returns>返回订单列表</returns>
        [OperationContract]
        List<OtherBusinessList> GetOtherBusinessListById(Guid[] orderIDs, Guid[] companyIDs); 
        #endregion

        #region 获取订单信息

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="companyID">口岸ID</param>
        /// <returns>返回业务信息</returns>
        [OperationContract]
        OtherBusinessInfo GetOtherBusinessInfo(Guid id,Guid companyID); 
        #endregion

        #region 业务作废
        /// <summary>
        /// 业务作废
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="isCancel">是否取消(true为取消,false为激活)</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        [OperationContract(Name = "CancelOtherBusiness1")]
        SingleResult CancelOtherBusiness(Guid orderID, bool isCancel, Guid changeByID, DateTime? updateDate);

        /// <summary>
        /// 业务作废
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="companyID">口岸ID</param>
        /// <param name="isCancel">是否取消(true为取消,false为激活)</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        [OperationContract(Name = "CancelOtherBusiness2")]
        SingleResult CancelOtherBusiness(Guid orderID,Guid companyID, bool isCancel, Guid changeByID, DateTime? updateDate); 
        #endregion

        #region 保存业务信息
        /// <summary>
        /// 保存业务信息
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        [OperationContract]
        SingleResult SaveOtherBusinessInfo(OtherBusinessSaveRequest saveRequest); 
        #endregion

        #region 获取利润表信息
        /// <summary>
        /// 获取利润表信息
        /// </summary>
        /// <param name="OperationID">业务ID</param>
        /// <returns></returns>
        /// 
        [OperationContract(Name = "GetOtherProfitReportData1")]
        ProfitContainerObjects GetOtherProfitReportData(Guid OperationID);

        /// <summary>
        /// 获取利润表信息
        /// </summary>
        /// <param name="OperationID">业务ID</param>
        /// <param name="companyID">口岸ID</param>
        /// <returns></returns>
        /// 
        [OperationContract(Name = "GetOtherProfitReportData2")]
        ProfitContainerObjects GetOtherProfitReportData(Guid OperationID, Guid companyID); 
        #endregion
    }
}
