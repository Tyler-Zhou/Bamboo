using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.AirImport.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using System.ServiceModel;

namespace ICP.FCM.AirImport.ServiceInterface
{
    /// <summary>
    /// 空运进口业务服务类
    /// </summary>
    [ServiceInfomation("空运进口业务服务")]
    [ServiceContract]
    public interface IAirImportBusinessService
    {

        #region 获得业务数据
        /// <summary>
        /// 获得业务数据列表 --列表上的刷新用到
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<AirBusinessList> GetBusinessListByIds(Guid[] ids);

        /// <summary>
        /// 获得业务数据列表--列表上的快速查询使用
        /// </summary>
        /// <param name="companyIDs">公司集合ID</param>
        /// <param name="noSearchType">单号搜索类型(0:全部、1:业务号、2:MBLNo、3:HBLNo、4:箱号、5:转关号)</param>
        /// <param name="no">单号</param>
        /// <param name="customerSearchType">客户搜索类型(0:全部、1:客户、2:代理、3:发货人、4:收货人、5:通知人、6:船公司、7:仓库、8:报关行)</param>
        /// <param name="customerName">客户名称</param>
        /// <param name="portSearchType">地点搜索类型(0:全部、1:装货港、2:卸货港、3:交货地、4:最终目的地、5:提货地、6:还柜地)</param>
        /// <param name="portName">地点</param>
        /// <param name="dateSearchType">时间搜索类型(0:全部、1:创建日、2:F.ETA 、3:离港日、4:到港日、5:收单日期、6:放单日期)</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">返回最大行数</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<AirBusinessList> GetBusinessListByFastSearch(
                                  Guid[] companyIDs,
                                  OIBusinessNoSearchType noSearchType,
                                  string no,
                                  OIBusinessCustomerSearchType customerSearchType,
                                  string customerName,
                                  OIBusinessPortSearchType portSearchType,
                                  string portName,
                                  OIBusinessDateSearchType dateSearchType,
                                  DateTime? beginTime,
                                  DateTime? endTime,
                                  Guid? filerID,
                                  int maxRecords);


        /// <summary>
        ///  获得业务列表--左边的查询使用
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="no">业务号</param>
        /// <param name="blNo">提单号</param>
        /// <param name="containerNo">箱号</param>
        /// <param name="customerName">客户名称</param>
        /// <param name="agent">代理</param>
        /// <param name="consignee">收货人</param>
        /// <param name="shipper">发货人</param>
        /// <param name="notifyPart">通知人</param>
        /// <param name="pol">装货港</param>
        /// <param name="pod">卸货港</param>
        /// <param name="placeOfDelivery">交货地</param>
        /// <param name="vesselName">船名</param>
        /// <param name="voyageNo">航次</param>
        /// <param name="salesID">揽货人</param>
        /// <param name="state">状态</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">返回最大行数</param>
        /// <param name="dateSearchType">时间搜索类型(0:全部、1:创建日、2:F.ETA 、3:离港日、4:到港日、5:收单日期、6:放单日期)</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<AirBusinessList> GetBusinessList(
                                          Guid[] companyIDs,
                                          string no,
                                          string blNo,
                                          string customerName,
                                          string agent,
                                          string consignee,
                                          string shipper,
                                          string notifyPart,
                                          string pol,
                                          string pod,
                                          string placeOfDelivery,
                                          string flightNo,
                                          Guid? customerServiceID,
                                          Guid? filerID,
                                          Guid? salesID,
                                          AIOrderState? state,
                                          bool? isValid,
                                          int maxRecords,
                                          OIBusinessDateSearchType dateSearchType,
                                          DateTime? beginTime,
                                          DateTime? endTime);


        /// <summary>
        /// 获得业务详细信息--编辑界面使用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        AirBusinessInfo GetBusinessInfo(Guid id);

        /// <summary>
        /// 获得业务详细信息(包含MBL、HBL、集装箱、费用、PO)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        AirBusinessInfo GetBusinessInfoByEdit(Guid id);

        #endregion

        #region  下载

        /// <summary>
        /// 获得下载清单列表
        /// </summary>
        /// <param name="bLNo">提单号</param>
        /// <param name="vesselName">船名</param>
        /// <param name="eta">离港日</param>
        /// <param name="etd">到港日</param>
        /// <param name="polName">装货港</param>
        /// <param name="podName">卸货港</param>
        /// <param name="placeOfDeliveryName">交货地</param>
        /// <param name="consigneeName">收货人</param>
        /// <param name="podCompanyID">代理公司</param>
        /// <param name="companyID">操作公司</param>
        /// <param name="state">状态</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">返回最大行数</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<AirBusinessDownLoadList> GetAirExportList(
                      string bLNo,
                      string flightNo,
                      DateType? dateType,
                      DateTime? beginDate,
                      DateTime? endDate,
                      string polName,
                      string podName,
                      string placeOfDelivery,
                      string consigneeName,
                      Guid? polCompanyID,
                      string companyIDs,
                      AIBLState? state,
                      bool? isValid,
                      int maxRecords);

        /// <summary>
        /// 下载业务单
        /// </summary>
        /// <param name="mblIDs">MBLID集合</param>
        /// <param name="consigneeIDs">收货人集合</param>
        /// <param name="hblIDs">HBLID集合</param>
        /// <param name="saveByID">保存人</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<AirBusinessList> DownLoadBusiness(
                    Guid[] mblIDs,
                    Guid[] consigneeIDs,
                    DateTime?[] detas,
                    string[] hblIDs,
                    Guid saveByID);


        #endregion

        #region 取消业务

        /// <summary>
        /// 取消业务单
        /// </summary>
        /// <param name="businessID">业务单ID</param>
        /// <param name="isCancel">是否取消(True为取消;Flase为激活)</param>
        /// <param name="changeByID">更改人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult CancelAirBusiness(
                    Guid businessID,
                    bool isCancel,
                    Guid changeByID,
                    DateTime? updateDate);


        #endregion

        #region 业务转移
        /// <summary>
        /// 业务转移
        /// </summary>
        /// <param name="BusinessID">业务ID</param>
        /// <param name="newCompanyID">新公司</param>
        /// <param name="changeByID">操作人</param>
        /// <param name="updateDate">更新时间</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult TransferBusiness(
                    Guid businessID,
                    Guid newCompanyID,
                    Guid changeByID,
                    DateTime? updateDate);

        #endregion

        #region 派车
        /// <summary>
        /// 获取派车信息
        /// </summary>
        /// <param name="businessID">业务单ID</param>
        /// <returns>返回派车信息</returns>
        [FunctionInfomation]
        [OperationContract]
        List<AirImportTruckInfo> GetAirTruckServiceList(Guid businessID);

        /// <summary>
        /// 保存派车信息
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveAirTruckServiceInfo(TruckSaveRequest saveRequest);

        /// <summary>
        /// 删除派车信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveAirTruckServiceInfo(
                     Guid id,
                     Guid removeByID,
                     DateTime? updateDate);

        /// <summary>
        /// 获取指定客户的最近提货通知书下的拖车行和交货地
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="customerID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult GetRecentlyAITruckInfoList(Guid companyID, Guid customerID);

        #endregion

        #region 保存
        /// <summary>
        /// 保存业务信息
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveAIBusinessInfo(BusinessSaveRequest saveRequest);

        #endregion

        #region HBL
        /// <summary>
        /// 获取HBL信息
        /// </summary>
        /// <param name="oiBookingID">业务单ID</param>
        /// <returns>返回派车信息</returns>
        [FunctionInfomation]
        [OperationContract]
        List<AirBusinessHBLList> GetAIBookingHBLList(Guid oiBookingID);

        /// <summary>
        /// 保存HBL信息
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult SaveAIBookingHBLInfo(HBLInfoSaveRequest saveRequest);

        /// <summary>
        /// 删除HBL信息
        /// </summary>
        /// <param name="ids">删除ID的集合</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveAIBookingHBLInfo(
                            Guid id,
                            Guid removeByID,
                            DateTime? updateDate);


        #endregion

        #region MBL


        /// <summary>
        /// 获得MBL信息
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<AirBusinessMBLList> GetAIMBLList();

        /// <summary>
        /// 获得MBL信息
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        AirBusinessMBLList GetAIMBLInfo(Guid id);

        /// <summary>
        /// 保存MBL信息
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveAIMBLInfo(MBLInfoSaveRequest saveRequest);
        #endregion

        #region 确认放单
        /// <summary>
        /// 确认放单
        /// </summary>
        /// <param name="oiBookingID"></param>
        /// <param name="releaseDate"></param>
        /// <param name="updateTime"></param>
        /// <param name="saveByID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SetAIReleaseData(
                        Guid oiBookingID,
                        FCMReleaseType releaseType,
                        DateTime releaseDate,
                        DateTime? updateTime,
                        Guid saveByID);

        #endregion

        #region 获得出口业务的费用列表
        /// <summary>
        /// 获取订单费用列表
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <returns>返回订单费用列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<AirImportFeeList> GetAirExportFeeList(Guid orderID);

        #endregion

        #region 以事务的方式保存业务信息

        /// <summary>
        /// 以事务的方式保存业务、HBL、集装箱、费用、PO信息
        /// </summary>
        /// <param name="businessSaveRequest">业务数据</param>
        /// <param name="mblSaveRequest">MBL信息</param>
        /// <param name="hblList">HBL List</param>
        /// <param name="containerList">集装箱 List</param>
        /// <param name="feeList">费用 List</param>
        /// <param name="poList">PO List</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        Dictionary<Guid, SaveResponse> SaveAIBusinessWithTrans(
                       BusinessSaveRequest businessSaveRequest,
                       MBLInfoSaveRequest mblSaveRequest,
                       List<HBLInfoSaveRequest> hblList,
                       List<FeeSaveRequest> feeList);
        #endregion

        #region 以事务的方式保存派车、集装箱列表、关联信息
        /// <summary>
        /// 以事务的方式保存派车、集装箱列表、关联信息
        /// </summary>
        /// <param name="truckSave">派车信息</param>
        /// <param name="boxList">集装箱列表</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract(Name = "SaveAIOrderWithTransByTruck")]
        Dictionary<Guid, SaveResponse> SaveAIOrderWithTrans(
                            Guid mBLID,
                            TruckSaveRequest truckSave);
        #endregion
    }
}
