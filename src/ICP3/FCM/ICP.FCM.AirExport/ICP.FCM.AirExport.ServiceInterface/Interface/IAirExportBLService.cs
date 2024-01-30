namespace ICP.FCM.AirExport.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.FCM.AirExport.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using System.ServiceModel;

    /// <summary>
    /// 空运出口提单服务
    /// </summary>
    [ServiceInfomation("空运出口提单服务")]
    [ServiceContract]
    public interface IAirExportBLService
    {
        #region BL

        /// <summary>
        /// 获取提单列表
        /// </summary>
        /// <param name="blIDs">blIDs</param>
        /// <returns>返回订单列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<AirBLList> GetAirBLListByIDs(
            Guid[] blIDs);

        /// <summary>
        /// 获取提单列表
        /// </summary>
        /// <param name="companyIDs">公司列表</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="blNo">提单号</param>
        /// <param name="containerNo">箱号</param>
        /// <param name="shippingOrderNo">订舱号</param>
        /// <param name="scno">合约号</param>
        /// <param name="customerName">客户名</param>
        /// <param name="carrierName">船东名</param>
        /// <param name="agentOfCarrierName">承运人名</param>
        /// <param name="vesselName">船名</param>
        /// <param name="voyageNo">航次</param>
        /// <param name="polName">装货港名</param>
        /// <param name="podName">卸货港名</param>
        /// <param name="placeOfDeliveryName">目的港名</param>
        /// <param name="salesID">业务员</param>
        /// <param name="filerID">文件</param>
        /// <param name="state">状态()</param>
        /// <param name="dateSearchType">日期搜索类型()</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回提单列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<AirBLList> GetAirBLList(
            Guid[] companyIDs,
            string operationNo,
            string blNo,
            string filightNo,
            string scno,
            string customerName,
            string checkerName,
            string airCompanyName,
            string agentOfCarrierName,
            string departureName,
            string detinationName,
            string placeOfDelivery,
            Guid? salesID,
            Guid? filerID,
            AEBLState? state,
            DateSearchType dateSearchType,
            DateTime? beginTime,
            DateTime? endTime,
            int maxRecords);

        /// <summary>
        /// 获取提单列表
        /// </summary>
        /// <param name="companyIDs">公司列表</param>
        /// <param name="noSearchType">单号搜索类型(0:全部,1:业务号,2:提单号,3:箱号,4:订舱号,5:合约号)</param>
        /// <param name="no">号码</param>
        /// <param name="customerSearchType">客户搜索类型(0:全部,1:客户,2:船东,3:承运人,4:发货人,5:收货人,6:通知人,7:对单人)</param>
        /// <param name="customerName">客户名</param>
        /// <param name="portSearchType">港口搜索类型(0:全部,1:收货地,2:装货港,3:卸货港,4:交货地,5:最终目的地)</param>
        /// <param name="portName">港口名</param>
        /// <param name="dateSearchType">日期搜索类型(0:全部,1:离港日,2:到港日,3:订舱日,4:创建日,)</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回提单列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<AirBLList> GetAirBLListForFaster(
            Guid[] companyIDs,
            NoSearchType noSearchType,
            string no,
            CustomerSearchType customerSearchType,
            string customerName,
            PortSearchType portSearchType,
            string portName,
            DateSearchType dateSearchType,
            DateTime? beginTime,
            DateTime? endTime,
            int maxRecords);

        /// <summary>
        /// 获取MBL提单信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回MBL提单信息</returns>
        [FunctionInfomation]
        [OperationContract]
        AirMBLInfo GetAirMBLInfo(Guid id);

        /// <summary>
        /// 删除MBL提单
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人ID</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveAirMBLInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate);

        /// <summary>
        /// 改变MBL状态
        /// </summary>
        /// <param name="id">MBL ID</param>
        /// <param name="state">状态（0草稿、1对单中、2、对单确认、3已完成）</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult ChangeAirMBLState(
            Guid id,
            AEBLState state,
            Guid changeByID,
            DateTime? updateDate); 

        /// <summary>
        /// 更改订单的状态
        /// </summary>
        /// <param name="id">MBL ID</param>
        /// <param name="state">状态（0草稿、1对单中、2、对单确认、3已完成）</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        [OperationContract]
        SingleResult ChangeBLAirOrderState(
            Guid orderID,
            AEOrderState state,
            string reason,
            Guid changeByID,
            DateTime? updateDate);

        /// <summary>
        /// 获取HBL提单信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回HBL提单信息</returns>
        [FunctionInfomation]
        [OperationContract]
        AirHBLInfo GetAirHBLInfo(Guid id);

        /// <summary>
        /// 删除HBL提单
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人ID</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveAirHBLInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate);

        /// <summary>
        /// 改变HBL状态
        /// </summary>
        /// <param name="id">HBL ID</param>
        /// <param name="state">状态（0草稿、1对单中、2、对单确认、3已完成）</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回true</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult ChangeAirHBLState(
            Guid id,
            AEBLState state,
            Guid changeByID,
            DateTime? updateDate);

        /// <summary>
        /// 合单
        /// </summary>
        /// <param name="id">保留的提单</param>
        /// <param name="blID">要合并的提单</param>
        /// <param name="saveByID">saveByID</param>
        /// <returns>返回保留的提单的ID,更新时间</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult MergeAirBL(Guid id, Guid[] blID,Guid saveByID);

        /// <summary>
        /// 分单
        /// </summary>
        /// <param name="xml">特定的XML结构</param>
        /// <param name="saveById">保存人</param>
        /// <returns>ManyResult { "ID" }</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult SplitAirBL(string xml,Guid saveById);

        #endregion

        #region BLContainer       

        /// <summary>
        /// 获取提单的货物信息列表
        /// </summary>
        /// <param name="blIDs">提单ID</param>
        /// <returns>计量信息列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<AirContainerCargoList> GetAirBLMeasureInfoList(Guid[] blIDs);

        #endregion

        #region 需要以事务方式保存方法
       
        /// <summary>
        /// 保存MBL信息
        /// </summary>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveAirMBLInfo(SaveMBLInfoParameter parameter);

        /// <summary>
        /// 保存HBL信息
        /// </summary>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveAirHBLInfo(SaveHBLInfoParameter parameter);

        #endregion
    }
}