namespace ICP.FCM.OceanExport.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Sys.ServiceInterface.DataObjects;
    using System.ServiceModel;

    /// <summary>
    /// 海运出口提单服务
    /// </summary>
    [ServiceInfomation("海运出口提单服务")]
    [ServiceContract]
    public interface IOceanExportBLService
    {
        #region BL

      
        /// <summary>
        /// 获取提单列表
        /// </summary>
        /// <param name="blIDs">blIDs</param>
        /// <returns>返回订单列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<OceanBLList> GetOceanBLListByIds(
            Guid[] blIDs);


        /// <summary>
        /// 验证MBLNo是否存在
        /// </summary>
        /// <param name="mblID"></param>
        /// <param name="mblNo"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        bool IsExistsMBLNo(Guid? mblID, string mblNo);


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
        /// <param name="consigneeName">收货人</param>
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
        List<OceanBLList> GetOceanBLList(
            Guid[] companyIDs,
            string operationNo,
            string blNo,
            string containerNo,
            string shippingOrderNo,
            string scno,
            string customerName,
            string carrierName,
            string agentOfCarrierName,
            string vesselName,
            string voyageNo,
            string polName,
            string podName,
            string placeOfDeliveryName,
            string consigneeName,
            Guid? salesID,
            Guid? filerID,
            Guid? overseasFilerID,
            OEBLState? state,
            DateSearchType dateSearchType,
            DateTime? beginTime,
            DateTime? endTime,
            int maxRecords);

        /// <summary>
        /// 获取提单列表
        /// </summary>
        /// <param name="companyIDs">公司列表</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="blNo">提单号</param>
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
        /// <param name="state">状态()</param>
        /// <param name="dateSearchType">日期搜索类型()</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="ownerID">是否自己的单</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回订舱列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<OceanBLList> GetOceanMBLList(
            Guid[] companyIDs,
            string operationNo,
            string blNo,
            string shippingOrderNo,
            string scno,
            string customerName,
            string carrierName,
            string agentOfCarrierName,
            string vesselName,
            string voyageNo,
            string polName,
            string podName,
            string placeOfDeliveryName,
            Guid? salesID,
            OEBLState? state,
            DateSearchType dateSearchType,
            DateTime? beginTime,
            DateTime? endTime,
            Guid? ownerID,
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
        List<OceanBLList> GetOceanBLListForFaster(
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
        OceanMBLInfo GetOceanMBLInfo(Guid id);

        /// <summary>
        /// 删除MBL提单
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人ID</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveOceanMBLInfo(
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
        SingleResult ChangeOceanMBLState(
            Guid id,
            OEBLState state,
            Guid changeByID,
            DateTime? updateDate);

        /// <summary>
        /// 确认装船类型
        /// </summary>
        /// <param name="shippingOrderID">ShippingOrderID</param>
        /// <param name="preVoyageID">preVoyageID</param>
        /// <param name="voyageID">voyageID</param>
        /// <param name="onBoardType">确认装船类型（0不确定，1前程确定，2确定）</param>
        /// <param name="confirmByID">确认人</param>
        /// <param name="shippingOrderUpdateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult ConfirmOnBoardType(
            Guid shippingOrderID,
            Guid? preVoyageID,
            Guid? voyageID,
            ConfirmOnBoardType onBoardType,
            Guid confirmByID,
            DateTime? shippingOrderUpdateDate);

        /// <summary>
        /// 获取HBL提单信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回HBL提单信息</returns>
        [FunctionInfomation]
        [OperationContract]
        OceanHBLInfo GetOceanHBLInfo(Guid id);

        /// <summary>
        /// 删除HBL提单
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人ID</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveOceanHBLInfo(
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
        SingleResult ChangeOceanHBLState(
            Guid id,
            OEBLState state,
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
        SingleResult MergeOceanBL(Guid id, Guid[] blID, Guid saveByID);

        /// <summary>
        /// 分单
        /// </summary>
        /// <param name="xml">特定的XML结构</param>
        /// <param name="saveById">保存人</param>
        /// <returns>ManyResult { "ID" }</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult SplitOceanBL(string xml, Guid saveById);

        #endregion

        #region BLContainer

        /// <summary>
        /// 获取MBL单的箱列表,现行是获取MBL的业务下所有的箱，然后在Relation(bool)中体现是否和这票MBL关联
        /// </summary>
        /// <param name="mblID">MBL ID</param>
        /// <returns>返回MBL箱列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<OceanBLContainerList> GetOceanMBLContainerList(Guid mblID);

        /// <summary>
        /// 保存MBL的箱信息
        /// </summary>
        /// <param name="oceanBookingID">订舱ID</param>
        /// <param name="mblID">MBL ID</param>
        /// <param name="relation">关联</param>
        /// <param name="ids">OceanContainers箱ID列表</param>
        /// <param name="cargoIds">OceanBLContainersID列表</param>
        /// <param name="containerNos">箱号列表</param>
        /// <param name="containerSOs">装货单号列表</param>
        /// <param name="containerTypeIDs">箱型列表</param>
        /// <param name="containerSealNos">封条号列表</param>
        /// <param name="containerMarks">唛头列表</param>
        /// <param name="containerCommoditys">品名列表</param>
        /// <param name="containerQuantitys">数量列表</param>
        /// <param name="containerWeights">箱重量列表</param>
        /// <param name="containerMeasurements">箱体积列表</param>
        /// <param name="containerIsSOCs">是否客户自有箱列表</param>
        /// <param name="containerIsPartOfs">是否一个柜子出两套或两套以上的提单（这里仅仅只整箱情况）</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">OceanContainers更新时间-做数据版本用</param>
        /// <param name="cargoUpdateDates">货物更新时间-做数据版本用</param>
        /// <returns>返回ManyResult</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult SaveOceanMBLContainerInfo(
            Guid oceanBookingID,
            Guid mblID,
            bool[] relation,
            Guid?[] ids,
            Guid?[] cargoIds,
            string[] containerNos,
            string[] containerSOs,
            Guid[] containerTypeIDs,
            string[] containerSealNos,
            string[] containerMarks,
            string[] containerCommoditys,
            int[] containerQuantitys,
            decimal[] containerWeights,
            decimal[] containerMeasurements,
            bool[] containerIsSOCs,
            bool[] containerIsPartOfs,
            Guid saveByID,
            DateTime?[] updateDates,
            DateTime?[] cargoUpdateDates,
            bool IsSynToHBL
            );

        /// <summary>
        /// 获取HBL单的箱列表,现行是获取HBL的业务下所有的箱，然后在Relation(bool)中体现是否和这票HBL关联
        /// </summary>
        /// <param name="hblID">HBL ID</param>
        /// <returns>返回箱列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<OceanBLContainerList> GetOceanHBLContainerList(Guid hblID);

        /// <summary>
        /// 保存HBL的箱信息
        /// </summary>
        /// <param name="oceanBookingID">订舱ID</param>
        /// <param name="hblID">HBL ID</param>
        /// <param name="relation">关联</param>
        /// <param name="ids">OceanContainers箱ID列表</param>
        /// <param name="cargoIds">OceanBLContainersID列表</param>
        /// <param name="containerNos">箱号列表</param>
        /// <param name="containerSOs">装货单号列表</param>
        /// <param name="containerTypeIDs">箱型列表</param>
        /// <param name="containerSealNos">封条号列表</param>
        /// <param name="containerMarks">埋头列表</param>
        /// <param name="containerCommoditys">品名列表</param>
        /// <param name="containerQuantitys">数量列表</param>
        /// <param name="containerWeights">箱重量列表</param>
        /// <param name="containerMeasurements">箱体积列表</param>
        /// <param name="containerIsSOCs">是否客户自有箱列表</param>
        /// <param name="containerIsPartOfs">是否一个柜子出两套或两套以上的提单（这里仅仅只整箱情况）</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">OceanContainers更新时间-做数据版本用</param>
        /// <param name="cargoUpdateDates">货物更新时间-做数据版本用</param>
        /// <returns>返回ManyResult</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult SaveOceanHBLContainerInfo(
            Guid oceanBookingID,
            Guid hblID,
            bool[] relation,
            Guid?[] ids,
            Guid?[] cargoIds,
            string[] containerNos,
            string[] containerSOs,
            Guid[] containerTypeIDs,
            string[] containerSealNos,
            string[] containerMarks,
            string[] containerCommoditys,
            int[] containerQuantitys,
            decimal[] containerWeights,
            decimal[] containerMeasurements,
            bool[] containerIsSOCs,
            bool[] containerIsPartOfs,
            Guid saveByID,
            DateTime?[] updateDates,
            DateTime?[] cargoUpdateDates,
            bool IsSynToMBL
            );

        /// <summary>
        /// 获取提单的货物信息列表
        /// </summary>
        /// <param name="blIDs">提单ID</param>
        /// <returns>计量信息列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<OceanContainerCargoList> GetOceanBLMeasureInfoList(Guid[] blIDs);

        #endregion

        #region 需要以事务方式保存方法

        /// <summary>
        /// 保存MBL和箱
        /// </summary>
        /// <param name="blParameter">MBL参数</param>
        /// <param name="ctnParameter">箱参数</param>
        /// <returns>返回 BLResult,ContainerResult</returns>
        [FunctionInfomation]
        [OperationContract(Name = "SaveOceanExportMBLAndContainerWithTrans")]
        SingleResult SaveOceanMBLAndContainerWithTrans(SaveMBLInfoParameter blParameter, SaveBLContainerParameter ctnParameter, List<Guid> ctnIDList, List<DateTime?> ctnDateList,bool IsSynToHBL);
        /// <summary>
        /// 保存HBL和箱
        /// </summary>
        /// <param name="blParameter">MBL参数</param>
        /// <param name="ctnParameter">箱参数</param>
        /// <returns>返回 BLResult,ContainerResult</returns>
        [FunctionInfomation]
        [OperationContract(Name = "SaveOceanExportHBLAndContainerWithTrans")]
        SingleResult SaveOceanHBLAndContainerWithTrans(SaveHBLInfoParameter blParameter, SaveBLContainerParameter ctnParameter, List<Guid> ctnIDList, List<DateTime?> ctnDateList, bool IsSynToMBL);

        /// <summary>
        /// 保存MBL信息
        /// </summary>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveOceanMBLInfo(SaveMBLInfoParameter parameter);

        /// <summary>
        /// 保存HBL信息
        /// </summary>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveOceanHBLInfo(SaveHBLInfoParameter parameter, bool IsSynToMBL);

        #endregion
    }
}