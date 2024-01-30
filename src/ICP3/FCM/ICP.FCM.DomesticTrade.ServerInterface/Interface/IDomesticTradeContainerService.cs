namespace ICP.FCM.DomesticTrade.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using DataObjects;
    using Framework.CommonLibrary.Attributes;
    using Framework.CommonLibrary.Common;
    using CompositeObjects;
    using System.ServiceModel;

    /// <summary>
    /// 内贸业务集装箱服务
    /// </summary>
    [ServiceInfomation("内贸业务集装箱服务")]
    [ServiceContract]
    public interface IDomesticTradeContainerService
    {
        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="transactionId"></param>
         [FunctionInfomation] 
        [OperationContract]
        void CommitTransaction(Guid transactionId);

        /// <summary>
        /// 以事务的方式保存货物列表
        /// </summary>
        /// <param name="containers">箱列表</param>
        /// <param name="cargos">货物列表</param>
        /// <param name="pos">PO列表及其Item的信息</param>
        /// <returns>组合对象结果集</returns>
         [FunctionInfomation] 
        [OperationContract]
         Dictionary<Guid, SaveResponse> SaveContainerCargos(List<ContainerSaveRequest> containers,
            List<CargoSaveRequest> cargos, List<POSaveRequest> pos,PoItemContainersRelationSaveRequest relations,bool IsEnglish);

        #region Container

        /// <summary>
        /// 获取订舱单箱信息
        /// </summary>
        /// <param name="bookingID">订舱单ID</param>
        /// <returns>包含3个表的组合对象</returns>
         [FunctionInfomation]
        [OperationContract]
        CompositeContainerObjects GetCompositeContainerObjects(Guid bookingID,bool IsEnglish);

        /// <summary>
        /// 获取海运箱信息
        /// </summary>
        /// <param name="bookingID">订舱单ID</param>
        /// <returns>返回海运箱信息</returns>
        [FunctionInfomation]
        [OperationContract]
        List<DTContainerList> GetDTContainerList(Guid bookingID, bool IsEnglish);

        /// <summary>
        /// 保存箱信息
        /// </summary>
        /// <param name="containerSaveRequest">详见对象</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult SaveDTContainerInfo(ContainerSaveRequest containerSaveRequest, bool IsEnglish);

        /// <summary>
        /// 删除业务下箱信息(自动删除箱下的货物和PO)
        /// </summary>
        /// <param name="ids">ID列表</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDates">更新时间-做数据版本用</param>
        /// <returns>返回ManyResult</returns>
         [FunctionInfomation] 
        [OperationContract]
        void RemoveDTContaierInfo(
           Guid[] ids,
           Guid removeByID,
            DateTime?[] updateDates , 
            bool IsEnglish);

        #endregion

        #region Cargo

        /// <summary>
        /// 删除箱下货物信息
        /// </summary>
        /// <param name="cargoID">货物ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
         [FunctionInfomation] [OperationContract]
        void RemoveDTContainerCargoInfo(
            Guid cargoID,
            Guid removeByID,
            DateTime? updateDate, 
            bool IsEnglish);

        /// <summary>
        /// 获取箱下的货物列表
        /// </summary>
        /// <param name="containerID">箱ID</param>
        /// <returns>返回该提单箱下的货物列表</returns>
         [FunctionInfomation] [OperationContract]
        List<DTContainerCargoList> GetDTContainerCargoList(Guid containerID,bool IsEnglish);

         /// <summary>
        /// 保存箱的MBL货物信息
        /// </summary>
        /// <param name="cargoSaveRequest">详见数据对象</param>
        /// <returns>返回ManyResult</returns>
         [FunctionInfomation] [OperationContract]
        ManyResult SaveDTMBLContainerCargoInfo(CargoSaveRequest cargoSaveRequest, bool IsEnglish);

        #endregion

        #region PO

        /// <summary>
        /// 获取箱下的PO列表
        /// </summary>
        /// <param name="containerID">箱ID</param>
        /// <param name="bookingId">订舱单Id</param>
        /// <returns>返回箱下的PO列表</returns>
         [FunctionInfomation] [OperationContract]
        CompositePoAndItems GetDTContainerPOList(Guid bookingId, bool IsEnglish);

        /// <summary>
        /// 保存箱下的PO信息
        /// </summary>
        /// <param name="containerPOSaveRequest">详见数据对象</param>
        /// <returns>返回ManyResult</returns>
         [FunctionInfomation] [OperationContract]
        HierarchyManyResult SaveDTContainerPOInfo(ContainerPOSaveRequest containerPOSaveRequest, bool IsEnglish);

        /// <summary>
        /// 删除PO
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
         [FunctionInfomation] [OperationContract]
        void RemoveDTContainerPOInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate, 
            bool IsEnglish);

        #endregion

        /// <summary>
        /// 保存PO和箱子的多对多关联关系
        /// </summary>
        /// <param name="ContainerIds"></param>
        /// <param name="poIds"></param>
        /// <param name="saveById"></param>
         [FunctionInfomation]
         [OperationContract]
        void SaveDTContainerPOInfoByIds(Guid[] containerIds, Guid[] poIds, Guid saveById, bool IsEnglish);

        /// <summary>
        /// 保存Item和箱子的多对多关联关系
        /// </summary>
        /// <param name="containerIds"></param>
        /// <param name="itemIds"></param>
        /// <param name="saveById"></param>
         [FunctionInfomation]
         [OperationContract]
        void SaveDTContainerPOItemInfo(Guid[] containerIds, Guid[] itemIds, Guid saveById, bool IsEnglish);
    }
}