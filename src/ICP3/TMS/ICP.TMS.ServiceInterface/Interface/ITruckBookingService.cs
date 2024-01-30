using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;

namespace ICP.TMS.ServiceInterface
{
    /// <summary>
    /// 拖车服务类
    /// </summary>
    [ServiceInfomation("拖车服务类")]
    [ServiceContract]
    public interface ITruckBookingService
    {
        #region 拖车业务

        #region 获得拖车业务列表
        /// <summary>
        /// 获得拖车业务列表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="no">业务事情</param>
        /// <param name="containerNo">箱号</param>
        /// <param name="mblNo">提单号</param>
        /// <param name="customerRefNo">客户参考单号</param>
        /// <param name="customerName">客户名称</param>
        /// <param name="truckType">业务类型</param>
        /// <param name="state">状态</param>
        /// <param name="maxRowCount">最大行数</param>
        /// <param name="dateSearchType">时间查询类型</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]  [OperationContract]
        List<TruckBookingsList>  GetTruckBookingsList(
            Guid[] companyIDs,
            string no,
            string containerNo,
            string mblNo,
            string customerRefNo,
            string customerName,
            SearchTruckBookingType truckType,
            Int32 state,
            bool? valid,
            int maxRowCount,
            TruckBusinessDateSeachType dateSearchType,
            DateTime? beginDate,
            DateTime? endDate,
            bool isEnglish);
        #endregion

        #region 获得拖车业务列表
        /// <summary>
        /// 获得拖车列表
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        [FunctionInfomation]  [OperationContract]
        List<TruckBookingsList> GetTruckBookingsListByIds(Guid[] ids, bool isEnglish);
        #endregion

        #region 获得拖车业务详细信息
        /// <summary>
        /// 获得拖车业务详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        [FunctionInfomation]  [OperationContract]
        TruckBookingsInfo GetTruckBookingsInfo(Guid id, bool isEnglish);

        #endregion

        #region 作废/激活拖车业务
        /// <summary>
        /// 作废/激活拖车业务
        /// </summary>
        /// <param name="id">业务ID</param>
        /// <param name="isCancel">是否作废,True为作废;False为激活</param>
        /// <param name="changeByID">更改人</param>
        /// <param name="updateDate">最后更新时间</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]  [OperationContract]
        SingleResult CancelTruckBookings(Guid id,
               bool isCancel,
               Guid changeByID,
               DateTime? updateDate,
               bool isEnglish);

        #endregion

        #region 保存业务信息
        /// <summary>
        /// 保存业务信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="no">业务号</param>
        /// <param name="truckType">业务类型</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="customerID">客户ID</param>
        /// <param name="customerRefNo">客户参考号</param>
        /// <param name="salesID">揽货人</param>
        /// <param name="salesTypeID">揽货类型</param>
        /// <param name="bookingMode">委托类型</param>
        /// <param name="bookingDate">委托日期</param>
        /// <param name="mblNo">提单号</param>
        /// <param name="carrierID">船东</param>
        /// <param name="vesselName">船名</param>
        /// <param name="voyageNo">航次</param>
        /// <param name="containerDescription">箱需求</param>
        /// <param name="remark">备注</param>
        /// <param name="pickUpAtID">提柜地</param>
        /// <param name="pickUpAtDescription">提柜地描述</param>
        /// <param name="pickUpAtDate">提柜时间</param>
        /// <param name="deliveryAtID">交货地</param>
        /// <param name="deliveryAtDescription">交货地描述</param>
        /// <param name="deliveryAtDate">交货时间</param>
        /// <param name="returnLocationID">还柜地</param>
        /// <param name="returnLocationDescription">还柜地描述</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDate">更新时间</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]  [OperationContract]
        SingleResult SaveTruckBookings(TruckBookingSaveRequest truckBooking);
        #endregion

        #region 获得派车箱列表
        /// <summary>
        /// 获得派车箱列表
        /// </summary>
        /// <param name="bookingID">拖车业务ID</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]  [OperationContract]
        List<TruckContainersList> GetTruckContainersList(Guid bookingID,bool isEnglish);
        
        #endregion
        
        #region 保存派车箱列表

        /// <summary>
        /// 保存派车箱列表信息
        /// </summary>
        /// <param name="bookingID">业务ID</param>
        /// <param name="indexNos">序号集合</param>
        /// <param name="ids">ID集合</param>
        /// <param name="nos">箱号集合</param>
        /// <param name="states">状态集合</param>
        /// <param name="typeIDs">箱型ID集合</param>
        /// <param name="trayNos">托盘号集合</param>
        /// <param name="truckDates">派车时间集合</param>
        /// <param name="truckPlaces">地点集合</param>
        /// <param name="lastFreeDates">免堆日集合</param>
        /// <param name="pickUpAtDates">提柜日集合</param>
        /// <param name="deliveryDates">交货日集合</param>
        /// <param name="returnDates">还柜日集合</param>
        /// <param name="driverIDs">司机ID集合</param>
        /// <param name="carIDs">车辆ID集合</param>
        /// <param name="remarks">备注集合</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">更新时间集合</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]  [OperationContract]
        ManyResult SaveContainersList(TruckContainersSaveRequest containers);
        #endregion

        #region 删除派车箱列表
        /// <summary>
        /// 删除箱列表
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="bookingID">业务ID</param>
        /// <param name="updateDates">更新时间集合</param>
        /// <param name="deleteByID">删除人</param>
        /// <param name="isEnglish">是否英文版本</param>
        [FunctionInfomation]  [OperationContract]
        void DeleteContainer(Guid[] ids,Guid bookingID,DateTime?[] updateDates, Guid deleteByID, bool isEnglish);
        #endregion

        #region 以事务的方式保存拖车业务信息跟箱信息
        /// <summary>
        /// 以事务的方式保存拖车业务信息跟箱信息
        /// </summary>
        /// <param name="truckBooking">业务保存实体</param>
        /// <param name="containerList">箱列表</param>
        /// <returns></returns>
        [FunctionInfomation]  [OperationContract]
        Dictionary<Guid, SaveResponse> SaveTruckBookingWithTrans(TruckBookingSaveRequest truckBooking, List<TruckContainersSaveRequest> containerList);
        #endregion

        #region 下载拖车业务
        /// <summary>
        /// 下载拖车业务
        /// </summary>
        /// <param name="types">类型集合</param>
        /// <param name="ids">ID集合</param>
        /// <param name="companyID">公司</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]  [OperationContract]
        List<TruckBookingsList> DownLoadTruckList(
                                TruckBookingType[] types, 
                                Guid[] ids,
                                Guid companyID,
                                Guid saveByID,
                                bool isEnglish);
       
        #endregion

        #region 获得下载列表
        /// <summary>
        /// 获得下载列表
        /// </summary>
        /// <param name="truckType">类型</param>
        /// <param name="companyID">操作口岸ID</param>
        /// <param name="customerRefNo">客户参考单号</param>
        /// <param name="containerNo">箱号</param>
        /// <param name="vesselName">船名</param>
        /// <param name="voyageNo">航次</param>
        /// <param name="state">状态</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="maxRecords">返回行数</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<DownLoadOceanBusinessList> GetOceandBusinessList(
                                 TruckBookingType? truckType,
                                 Guid companyID,
                                 string customerRefNo,
                                 string containerNo,
                                 string vesselName,
                                 string voyageNo,
                                 Int32 state,
                                 DateTime? beginDate,
                                 DateTime? endDate,
                                 int maxRecords,
                                 Guid UserID,
                                 bool isEnglish);
        #endregion

        #endregion

        #region 拖车资料

        #region 获得拖车列表
        /// <summary>
        /// 获得拖车资料列表
        /// </summary>
        /// <param name="no">车牌号</param>
        /// <param name="carTypeName">型号</param>
        /// <param name="dateSerachType">时间查询类型(1为创建时间;2为购买时间)</param>
        /// <param name="isvalid">有效性</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="isEnglish">英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]  [OperationContract]
        List<TruckDataList> GetTruckDataList(
                            string no,
                            string carTypeName,
                            TruckDateSeachType dateSerachType,
                            bool? isvalid,
                            DateTime? beginTime,
                            DateTime? endTime,
                            bool isEnglish);
        #endregion

        #region 保存拖车资料
        /// <summary>
        /// 保存拖车资料
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="no">车牌号</param>
        /// <param name="typeName">型号</param>
        /// <param name="buyDate">购买日期</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDate">更新日期</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]  [OperationContract]
        SingleResult SaveCarInfo(
                            Guid id,
                            string no,
                            string typeName,
                            DateTime buyDate,
                            string remark,
                            Guid saveByID,
                            DateTime? updateDate,
                            bool isEnglish);

        #endregion

        #region 作废/激活拖车资料
        /// <summary>
        /// 作废/激活司机资料
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isCancel">是否为作废，True为作废,False为激活</param>
        /// <param name="changeByID">更改人</param>
        /// <param name="updateDate">更新时间</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]  [OperationContract]
        SingleResult CancelTruck(
                     Guid id,
                     bool isCancel,
                     Guid changeByID,
                     DateTime? updateDate,
                     bool isEnglish);
        #endregion

        #endregion

        #region 司机资料

        #region 查询司机列表
        /// <summary>
        /// 获得拖车列表
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="moblie">手机</param>
        /// <param name="carID">身份ID</param>
        /// <param name="isvalid">有效性</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]  [OperationContract]
        List<DriversDataList> GeteDriverList(
                                string name,
                                string moblie,
                                string carID,
                                bool? isvalid,
                                DateTime? beginTime,
                                DateTime? endTime,
                                bool isEnglish);
        #endregion

        #region 保存司机资料
        /// <summary>
        /// 保存司机资料
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">姓名</param>
        /// <param name="mobile">手机</param>
        /// <param name="address">地址</param>
        /// <param name="cardID">符合ID</param>
        /// <param name="provinceID">省份</param>
        /// <param name="cityID">城市</param>
        /// <param name="carID">默认拖车</param>
        /// <param name="remark">备注</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDate">更新时间</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]  [OperationContract]
        SingleResult SaveDriverInfo(
                            Guid id,
                            string name,
                            string  mobile,
                            string address,
                            string cardID,
                            Guid? countryID,
                            Guid? provinceID,
                            Guid? cityID,
                            Guid? carID,
                            string remark,
                            Guid saveByID,
                            DateTime? updateDate,
                            bool isEnglish);
        #endregion

        #region 作废/激活司机资料
        /// <summary>
        /// 作废/激活司机资料
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isCancel">是否为作废，True为作废,False为激活</param>
        /// <param name="changeByID">更改人</param>
        /// <param name="updateDate">更新时间</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]  [OperationContract]
        SingleResult CancelDriver(
                     Guid id, 
                     bool isCancel, 
                     Guid changeByID,
                     DateTime? updateDate,
                     bool isEnglish);
        #endregion

        #endregion

    }
}
