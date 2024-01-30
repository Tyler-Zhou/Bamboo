using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;

namespace ICP.FCM.OceanImport.ServiceInterface
{
    /// <summary>
    /// 海运进口箱信息服务类
    /// </summary>
    [ServiceInfomation("海运进口箱信息服务类")]
    [ServiceContract]
    public interface IOIContainerService
    {
        /// <summary>
        /// 获得集装箱列表
        /// </summary>
        /// <param name="ioBookingID">业务单ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<OIBusinessContainerList> GetOIContainerList(Guid ioBookingID);


        /// <summary>
        /// 获得集装箱列表
        /// </summary>
        /// <param name="mblID">MBLID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<OIBusinessContainerList> GetOIContainerListByMBL(Guid mblID);

        /// <summary>
        /// 保存集装箱信息
        /// </summary>
        /// <param name="IDs">ID</param>
        /// <param name="ContainerTypeIDs"> 箱型ID</param>
        /// <param name="SealNos">封条号</param>
        /// <param name="Quantitys">数量</param>
        /// <param name="BLNos">提单</param>
        /// <param name="GODates">G.O.Date</param>
        /// <param name="LFDates">L.F.Date</param>
        /// <param name="ValidDates">有效日期</param>
        /// <param name="TruckDates">运送日期</param>
        /// <param name="Addresss">地点</param>
        /// <param name="Remarks">备注</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="containerUpdateDates">新时间</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult SaveOIContainerInfo(ContainerSaveRequest saveRequest);

        
        /// <summary>
        /// 删除集装箱信息
        /// </summary>
        /// <param name="ids">ID 列表</param>
        /// <param name="removeByID">删除人 </param>
        /// <param name="updateDates">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveOIContainerInfo(
            Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates);

        /// <summary>
        /// 根据业务号获得集装箱号列表
        /// </summary>
        /// <param name="bookingID">业务单ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<Guid> GetOIContainerIdsByBooking(Guid bookingID);


        /// <summary>
        /// 根据派车号获得集装箱号列表
        /// </summary>
        /// <param name="truckID">派车ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<Guid> GetOIContainerIdsByTruck(Guid truckID);


        /// <summary>
        /// 保存箱号与业务的关联
        /// </summary>
        /// <param name="bookingID">业务号</param>
        /// <param name="containerIDs">箱ID集合</param>
        /// <param name="saveById">保存人</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult SaveOIContainerAndBusiness(
                        Guid bookingID, 
                        Guid[] containerIDs,
                        Guid saveById);


        /// <summary>
        /// 保存派车与业务的关联
        /// </summary>
        /// <param name="truckID">派车号</param>
        /// <param name="containerIDs">箱ID集合</param>
        /// <param name="saveById">保存人</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult SaveOIContainerAndTruck(
                        Guid truckID,
                        Guid[] containerIDs,
                        Guid saveById);

        /// <summary>
        /// 获得海进HBL与箱信息关联列表
        /// </summary>
        /// <param name="hblID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract] 
        List<OIBusinessContainerList> GetOIHBL2Container(Guid hblID);

        /// <summary>
        /// 保存海进HBL与箱信息关联列表
        /// </summary>
        /// <param name="hblID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult SaveOIHBL2Container(Guid hblID,Guid[] ctnIDs,Guid saveByID);

         /// <summary>
        /// 获得海进文件比较时海出业务的集装箱列表
        /// 2013-07-08 joe
        /// </summary>
        /// <param name="ioBookingID">业务单ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<OIBusinessContainerList> GetOICompareContainerList(Guid ioBookingID);

        /// <summary>
        /// 获取分文件集装箱列表
        /// </summary>
        /// <param name="OperationID">业务单ID</param>
        /// <param name="DispatchFileLogID">分文档日志ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<OIBusinessContainerList> GetDispatchContainerInfo(Guid OperationID, Guid DispatchFileLogID);
    }
}
