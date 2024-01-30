using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FCM.OtherBusiness.ServiceInterface.CompositeObjects;
namespace ICP.FCM.OtherBusiness.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using System.ServiceModel;
    using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
    /// <summary>
    /// 其他业务服务
    /// </summary>
    [ServiceInfomation("其他业务服务")]
    [ServiceContract]
    public interface IOtherBusiness
    {
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="companyIDs">公司列表</param>
        /// <param name="noSearchType">单号搜索类型(0:全部,1:业务号,2:提单号,3:箱号,4:订舱号,5:合约号)</param>
        /// <param name="no">号码</param>
        /// <param name="customerSearchType">客户搜索类型(0:全部,1:客户,2:船东,3:承运人,4:发货人,5:收货人,6:通知人,7:对单人)</param>
        /// <param name="customerName">客户名</param>
        /// <param name="portSearchType">港口搜索类型(0:全部,1:收货地,2:装货港,3:卸货港,4:交货地,5:最终目的地)</param>
        /// <param name="portName">港口名</param>
        /// <param name="dateSearchType">日期搜索类型(0:全部,1:离港日,2:到港日,3:订舱日,4:创建日,)</param>
        /// <param name="salesId">揽货人</param>
        /// <param name="isValid"></param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回其他业务列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<OtherBusinessList> GetOtherBusinessListForFaster(
            Guid[] companyIDs,
            ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.NoSearchType noSearchType,
            string no,
            ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.CustomerSearchType customerSearchType,
            string customerName,
            ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.PortSearchType portSearchType,
            string portName,
            ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.DateSearchType dateSearchType,
            Guid salesId,
            Guid? OperatorID,
            DateTime? beginTime,
            DateTime? endTime,
            bool? isValid,
            int maxRecords,
            bool isEnglish);



        /// <summary>
        /// 获取其他业务列表
        /// </summary>
        /// <param name="companyIDs">公司列表</param>
        /// <param name="no">业务号</param>
        /// <param name="customerName">客户名</param>
        /// <param name="polName">装货港名</param>
        /// <param name="podName">卸货港名</param>
        /// <param name="placeOfDeliveryName">交货地名</param>
        /// <param name="carrierName">航空公司</param>
        /// <param name="orderState">订单状态()</param>
        /// <param name="salesID">业务员</param>
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
           Guid[] tempIDs,
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
           ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.DateSearchType dateSearchType,
           DateTime? beginTime,
           DateTime? endTime,
           int maxRecords,
           bool isEnglish);



        /// <summary>
        /// 获取其他业务列表（刷新）
        /// </summary>
        /// <param name="orderIDs">orderIDs</param>
        /// <returns>返回订单列表</returns>
        [OperationContract]
        List<OtherBusinessList> GetOtherBusinessListById(
            Guid[] orderIDs, bool isEnglish);


        /// <summary>
        /// 业务作废
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="isValid">是否取消(true为取消,false为激活)</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        [OperationContract]
        SingleResult CancelOtherBusiness(
             Guid orderID,
             bool isCancel,
             Guid changeByID,
             DateTime? updateDate,
             bool isEnglish);
        /// <summary>
        /// 获取业务信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isEnglish">isEnglish</param>
        /// <returns>返回业务信息</returns>
        [OperationContract]
        OtherBusinessInfo GetOtherBusinessInfo(Guid id, bool isEnglish);
        /// <summary>
        /// 保存业务信息（事物）
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <param name="fees"></param>
        /// <param name="container"></param>
        /// <param name="IsEnglish"></param>
        /// <returns></returns>
        [OperationContract]
        Dictionary<Guid, SaveResponse> SaveOtherBusinessWithTrans(OtherBusinessSaveRequest saveRequest,
             List<FeeSaveRequest> fees, List<ContainerSaveRequest> container, bool IsEnglish);
        /// <summary>
        /// 保存业务信息
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        [OperationContract]
        SingleResult SaveOtherBusinessInfo(OtherBusinessSaveRequest saveRequest);

        /// <summary>
        /// 获取利润表信息
        /// </summary>
        /// <param name="OperationID"></param>
        /// <returns></returns>
        /// 
        [OperationContract]
        ProfitContainerObjects GetOtherProfitReportData(Guid OperationID);

    }


}
