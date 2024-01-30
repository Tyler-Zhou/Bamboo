using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using System.ServiceModel;
using ICP.FCM.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FileSystem.ServiceInterface;

namespace ICP.FCM.OceanImport.ServiceInterface
{
    /// <summary>
    /// 海运进口业务服务类
    /// </summary>
    [ServiceInfomation("海运进口业务服务")]
    [ServiceContract]
    public interface IOIBusinessService
    {

        #region 获得业务数据
        /// <summary>
        /// 获得业务数据列表 --列表上的刷新用到
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<OceanBusinessList> GetBusinessListByIds(Guid[] ids);

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
        List<OceanBusinessList> GetBusinessListByFastSearch(
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
        List<OceanBusinessList> GetBusinessList(
                                          Guid[] companyIDs,
                                          string no,
                                          string blNo,
                                          string containerNo,
                                          string telexNo,
                                          string clearanceNo,
                                          string customerName,
                                          string agent,
                                          string consignee,
                                          string shipper,
                                          string notifyPart,
                                          string pol,
                                          string pod,
                                          string placeOfDelivery,
                                          string vesselName,
                                          string voyageNo,
                                          Guid? customerServiceID,
                                          Guid? filerID,
                                          Guid? salesID,
                                          OIOrderState? state,
                                          bool? isValid,
                                          int maxRecords,
                                          OIBusinessDateSearchType dateSearchType,
                                          DateTime? beginTime,
                                          DateTime? endTime,
                                          ReleaseBLSearchStatue releasestate,
                                          ReceiveRCSearchStatue receivercstate,
                                          ApplyRCSearchStatue applyrcstate,
                                          ReleaseRCSearchStatue releasercstate,
                                          URBSearchStatue urbstate,
                                          UDNSearchStatue udnstate,
                                          ARCSearchStatue arcstate
            );


        /// <summary>
        /// 获得业务详细信息--编辑界面使用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        OceanBusinessInfo GetBusinessInfo(Guid id);

        /// <summary>
        /// 获得业务详细信息(包含MBL、HBL、集装箱、费用、PO)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        OceanBusinessInfo GetBusinessInfoByEdit(Guid id);

        #endregion

        #region  下载
        /// <summary>
        /// 获得下载清单列表
        /// </summary>
        /// <param name="bLNo">提单号</param>
        /// <param name="containerNo">箱号</param>
        /// <param name="vesselName">船名</param>
        /// <param name="voyageNo">航次 </param>
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
        List<OceanBusinessDownLoadList> GetOceanExportList(
                     string bLNo,
                     string containerNo,
                     string vesselName,
                     string voyageNo,
                     DateType? dateType,
                     DateTime? beginDate,
                     DateTime? endDate,
                     string polName,
                     string podName,
                     string placeOfDeliveryName,
                     string consigneeName,
                     Guid? podCompanyID,
                     string companyIDs,
                     DocumentState? docState,
                     OEBLState? state,
                     bool? isValid,
                     ReleaseCarogoState? isreleaseBL,
                     int maxRecords);

        /// <summary>
        /// 通过出口分文件产生进口业务
        /// </summary>
        /// <param name="mblIDs">MBLID集合</param>
        /// <param name="consigneeIDs">收货人集合</param>
        /// <param name="consigneeIDs">DETA集合</param>
        /// <param name="hblIDs">HBLID集合</param>
        /// <param name="saveByID">保存人</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        OIAfterDownLoadRerurnData DownLoadBusiness(
                    Guid[] mblIDs,
                    Guid[] consigneeIDs,
                    DateTime?[] detas,
                    string[] hblIDs,
                    Guid saveByID,
                    Guid? agentID);


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
        OIAfterDownLoadRerurnData DownLoadBusinessFromDispatchFile(
                    Guid[] mblIDs,
                    Guid[] consigneeIDs,
                    DateTime?[] detas,
                    string[] hblIDs,
                    Guid saveByID,
                    Guid? agentID);

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
        SingleResult CancelOceanBusiness(
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
        List<OceanImportTruckInfo> GetOceanTruckServiceList(Guid businessID);

        /// <summary>
        /// 保存派车信息
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveOceanTruckServiceInfo(TruckSaveRequest saveRequest);

        /// <summary>
        /// 删除派车信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveOceanTruckServiceInfo(
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
        SingleResult GetRecentlyOITruckInfoList(Guid companyID, Guid customerID);

        #endregion

        #region 保存
        /// <summary>
        /// 保存业务信息
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveOIBusinessInfo(BusinessSaveRequest saveRequest);

        #endregion

        #region HBL
        /// <summary>
        /// 获取HBL信息
        /// </summary>
        /// <param name="oiBookingID">业务单ID</param>
        /// <returns>返回派车信息</returns>
        [FunctionInfomation]
        [OperationContract]
        List<OceanBusinessHBLList> GetOIBookingHBLList(Guid oiBookingID);

        /// <summary>
        /// 获得分文件HBL信息
        /// </summary>
        /// <param name="OperationID">业务单ID</param>
        /// <param name="DispatchFileLogID">分文档日志ID</param>
        /// <returns>返回派车信息</returns>
        [FunctionInfomation]
        [OperationContract]
        List<OceanBusinessHBLList> GetDispatchHBLInfo(Guid OperationID, Guid DispatchFileLogID);


        /// <summary>
        /// 保存HBL信息
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult SaveOIBookingHBLInfo(HBLInfoSaveRequest saveRequest);

        /// <summary>
        /// 删除HBL信息
        /// </summary>
        /// <param name="ids">删除ID的集合</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveOIBookingHBLInfo(
                            Guid id,
                            Guid removeByID,
                            DateTime? updateDate);
        /// <summary>
        /// 获得海进分文件签收比较时的海出HBL信息
        /// author：joe
        /// initial date:2013-07-08
        /// </summary>
        /// <param name="oiBookingID">业务单ID</param>
        /// <returns>返回派车信息</returns>
        [FunctionInfomation]
        [OperationContract]
        List<OceanBusinessHBLList> GetOceanCompareHBLList(Guid oiBookingID);

        #endregion

        #region MBL


        /// <summary>
        /// 获得MBL信息
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<OceanBusinessMBLList> GetOIMBLList();



        /// <summary>
        /// 获得MBL信息
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        OceanBusinessMBLList GetOIMBLInfo(Guid id);


        /// <summary>
        /// 获取分文档MBL信息
        /// </summary>
        /// <param name="id">MBLID</param>
        /// <param name="DispatchFileLogID">分文档日志ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        OceanBusinessMBLList GetDispatchMBLInfo(Guid id, Guid DispatchFileLogID);




        /// <summary>
        /// 获得海进分文件签收比较时的海出MBL信息
        /// author：joe
        /// initial date:2013-07-08
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        OceanBusinessMBLList GetOECompareMBLInfo(Guid id);

        /// <summary>
        /// 保存MBL信息
        /// </summary>
        /// <param name="saveRequest"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveOIMBLInfo(MBLInfoSaveRequest saveRequest);
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
        [OperationContract]
        SingleResult SetOIReleaseData(
                        Guid oiBookingID,
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
        List<OceanImportFeeList> GetOceanExportFeeList(Guid orderID);

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
        [OperationContract]
        Dictionary<Guid, SaveResponse> SaveOIBusinessWithTrans(
                       BusinessSaveRequest businessSaveRequest,
                       MBLInfoSaveRequest mblSaveRequest,
                       List<HBLInfoSaveRequest> hblList,
                       List<ContainerSaveRequest> containerList,
                       List<FeeSaveRequest> feeList,
                       List<POSaveRequest> poList);
        #endregion

        #region 以事务的方式保存派车、集装箱列表、关联信息
        /// <summary>
        /// 以事务的方式保存派车、集装箱列表、关联信息
        /// </summary>
        /// <param name="truckSave">派车信息</param>
        /// <param name="boxList">集装箱列表</param>
        /// <returns></returns>
        [OperationContract(Name = "SaveOIOrderWithTransByTrack")]
        Dictionary<Guid, SaveResponse> SaveOIOrderWithTrans(
                            Guid mBLID,
                            TruckSaveRequest truckSave,
                            List<ContainerSaveRequest> boxList);
        #endregion

        #region 修改放货信息

        /// <summary>
        /// 修改放货信息
        /// </summary>
        /// <param name="oibookingid">业务id</param>
        /// <param name="rblrcv">接收放单</param>
        /// <param name="urb">催港前放单</param>
        /// <param name="blrca">申请放货</param>
        /// <param name="blrc">已放货</param>
        /// <param name="changebyid">操作人</param>
        /// <param name="udn">已催客户进行付款</param>
        /// <param name="agreeRC">同意放货</param>
        [FunctionInfomation]
        [OperationContract(Name = "ChangeOITrackingInfo")]
        void ChangeOITrackingInfo(Guid oibookingid, bool rblrcv, bool urb, bool blrca, bool blrc, bool udn, bool agreeRC,
            Guid changebyid);

        /// <summary>
        /// 修改放货信息
        /// </summary>
        /// <param name="oibookingid">业务id</param>
        /// <param name="rblrcv">接收放单</param>
        /// <param name="urb">催港前放单</param>
        /// <param name="blrca">申请放货</param>
        /// <param name="blrc">已放货</param>
        /// <param name="changebyid">操作人</param>
        /// <param name="udn">已催客户进行付款</param>
        /// <param name="agreeRC">同意放货</param>
        /// <param name="omblrcved">收到MBL正本</param>
        /// <param name="maildmbl">财务寄出MBL</param>
        [FunctionInfomation]
        [OperationContract(Name = "ChangeOITrackingInfo1")]
        void ChangeOITrackingInfo(Guid oibookingid, bool rblrcv, bool urb, bool blrca, bool blrc, bool udn, bool agreeRC, bool omblrcved, bool maildmbl, Guid changebyid);

        /// <summary>
        /// 修改放货信息
        /// </summary>
        /// <param name="oibookingid">业务id</param>
        /// <param name="values">values</param>
        /// <param name="changebyid">更改人</param>
        [FunctionInfomation]
        [OperationContract(Name = "ChangeOITrackingInfo2")]
        void ChangeOITrackingInfo(Guid oibookingid, IDictionary<string, object> values, Guid changebyid);

        /// <summary>
        /// 第三方代理放单
        /// </summary>
        /// <param name="oibookingid">业务ID</param>
        /// <param name="rbld">放单</param>
        /// <param name="changebyid">放单人</param>
        [FunctionInfomation]
        [OperationContract]
        void ReleaseOIBL(Guid oibookingid, bool rbld, Guid changebyid);

        /// <summary>
        /// 是否第3方代理
        /// </summary>
        /// <param name="agentid"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        bool CheckIsInternalAgent(Guid? agentid);

        /// <summary>
        /// 催港前操作放单
        /// </summary>
        /// <param name="oibookingid"></param>
        /// <returns>返回港前操作的Email</returns>
        [FunctionInfomation]
        [OperationContract]
        string GetPOLOperatorEmail(Guid oibookingid);

        #endregion

        /// <summary>
        ///  获得海进分文件比较业务时海出业务详细信息
        ///  2013-07-10 joe
        /// </summary>
        /// <param name="OEBookingID">海出业务ID</param>
        /// <param name="OIBookingID">海进业务ID</param>
        /// <param name="DETA">最终到港日</param>
        /// <param name="ConsigneeID">收货人ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        OceanBusinessInfo GetCompareBusinessInfo(Guid OEBookingID, Guid OIBookingID, DateTime? DETA, Guid ConsigneeID);
        /// <summary>
        /// 获得分文档业务详细信息
        /// </summary>
        /// <param name="OperationID">海出业务ID</param>
        /// <param name="DispatchFileLogID">分文档日志ID</param>
        /// <param name="SaveByID">执行人</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        OceanBusinessInfo GetDispatchBookingInfo(Guid OperationID, Guid DispatchFileLogID, Guid SaveByID);


        /// <summary>
        /// 以事务的方式签收同步海出业务、HBL、集装箱、账单费用
        /// joe 2013-07-17
        /// </summary>
        /// <param name="businessSaveRequest">业务信息</param>
        /// <param name="mblSaveRequest">MBL信息</param>
        ///<param name="OIBookingID">海进业务ID</param>
        ///<param name="userID">当前用户ID</param>
        ///<param name="IsSaveHBL">是否需要保存HBL信息</param>
        ///<param name="IsSaveContainer">是否需要保存集装箱信息</param>
        ///<param name="IsSaveBill">是否需要保存账单费用信息</param>
        /// <returns></returns>

        [FunctionInfomation]
        [OperationContract]
        void AcceptDispatchWithTrans(
                       BusinessSaveRequest businessSaveRequest,
                       MBLInfoSaveRequest mblSaveRequest,
                       Guid OIBookingID, Guid userID, bool IsSaveHBL, bool IsSaveContainer, bool IsSaveBill);

        

        
        

        

        /// <summary>
        /// 根据业务ID获取业务转移所需的邮件信息
        /// </summary>
        /// <param name="OIBookingID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        MailOIBusinessDataObjects GetTranserEmailInfoByID(Guid OIBookingID);

        

        

        /// <summary>
        ///   港前业务的账单的所有代理账单的修改人邮箱（用";"分开）
        ///  2013-08-12 joe 
        /// </summary>
        /// <param name="OEBookingID">海出业务ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        string GetBillUpdateUserEmails(Guid OEBookingID);

        /// <summary>
        ///  保存签收数据到海海进历史数据
        ///  2013-08-30 joe 
        /// </summary>
        /// <param name="OEBookingIDs">海出业务iD</param>
        /// <param name="RefIDs">签收记录ID</param>
        /// <param name="Types">签收类型（1为签收前，2为签收后）</param>
        /// <param name="userID">保存ID</param>
        /// <param name="isEnglish">是否中英文</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        string SaveOIInfoToHistory(string OEBookingIDs, string RefIDs, string Types, Guid userID, bool isEnglish);

        
        /// <summary>
        /// 根据份文件记录去签收分发的记录
        /// </summary>
        /// <param name="CurrentOperationID">当前打开的业务ID</param>
        /// <param name="DispatchFileLogID">分发的文件版本记录ID</param>
        /// <param name="userID">执行人</param>
        /// <param name="isEnglish">语言</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        void AcceptDispatchFiles(Guid CurrentOperationID, Guid DispatchFileLogID, Guid userID, bool isEnglish);

        /// <summary>
        /// 获得历史海进业务详细信息
        /// </summary>
        /// <param name="OIBookID">海进业务ID</param>
        /// <param name="ApplyID">签收记录ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        OceanBusinessInfo GetHistoryBusinessInfo(Guid OIBookID, Guid ApplyID);

        /// <summary>
        /// 获得历史账单比较账详细信息
        /// </summary>
        /// <param name="OIBookID">海进业务ID</param>
        /// <param name="BeforeApplyID">上次签收记录ID</param>
        /// <param name="AfterApplyID">本次签收记录ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<Fee> GetHistoryBillAndCharages(Guid OIBookID, Guid BeforeApplyID, Guid AfterApplyID);

        /// <summary>
        /// 获得海出签收海进账单修订历史比较账详细信息
        /// </summary>
        /// <param name="OEBookingID">海出业务ID</param>
        /// <param name="BeforeApplyID">上次签收记录ID</param>
        /// <param name="AfterApplyID">本次签收记录ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<Fee> uspGetHistoryCompareReviseBillAndCharge(Guid OEBookingID, Guid BeforeApplyID, Guid AfterApplyID);

        /// <summary>
        /// 得到内部代理信息
        /// </summary>
        /// <param name="OIBookID">海进业务ID</param>
        /// <param name="ApplyID">签收记录ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<Guid> GetInnerCustomers();

        /// <summary>
        /// 通过客户ID得到最近一票报关行信息
        /// </summary>
        /// <param name="customerid"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        CustomerInfo GetCustomsBrokerForCustomerID(Guid customerid);

        /// <summary>
        /// 返回符合发送催款邮件的业务id集合
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<Guid> GetPayNtOperationId();

        [FunctionInfomation]
        [OperationContract]
        void ChangeOIMBLforCarrier(Guid operationid, Guid carrierid);


        /// <summary>
        /// 返回催港前放单所需要的邮箱地址（收件人，CC 地址）
        /// </summary>
        /// <param name="oiBookingId">业务ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<string> GetNoticeReleaseEmail(Guid oiBookingId);


        /// <summary>
        /// 返回拖车公司的邮件地址
        /// </summary>
        /// <param name="operationid"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        string GetTruckCustomersEmail(Guid operationid);

        #region 获得分文件比较详细信息
        /////// <summary>
        ///////  获得分文件比较详细信息
        /////// </summary>
        /////// <param name="OEBookingID">出口业务ID</param>
        /////// <param name="OIBookingID">进口业务ID</param>
        /////// <param name="DispatchFileLogID">分文档日志ID</param>
        /////// <param name="OperationType">业务类型</param>
        /////// <returns></returns>
        ////[FunctionInfomation]
        ////[OperationContract]
        ////List<Fee> DispatchCompareBillAndCharge(Guid OEBookingID, Guid OIBookingID, Guid DispatchFileLogID, OperationType OperationType); 
        #endregion
    }
}
