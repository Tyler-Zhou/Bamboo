using System.Data;

namespace ICP.FCM.Common.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using DataObjects;
    using Framework.CommonLibrary.Attributes;
    using Framework.CommonLibrary.Common;
    using CommonData = ICP.Common.ServiceInterface.DataObjects;
    using System.ServiceModel;

    /// <summary>
    /// 备注接口
    /// </summary>
    [ServiceInfomation("备注接口")]
    [ServiceContract]
    public interface IMemoService
    {
        #region Memo


        /// <summary>
        /// 获取已完成的事件
        /// </summary>
        /// <param name="operationID"></param>
        /// <param name="ownerID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract(Name = "GetMemoListComplete")]
        List<EventObjects> GetMemoList(Guid operationID,Guid? ownerID);


        /// <summary>
        /// 获取已完成的事件
        /// </summary>
        /// <param name="operationID"></param>
        /// <param name="eventType">事件类型</param>
        /// <param name="ownerID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract(Name = "GetMemoListComplete2")]
        List<EventObjects> GetMemoList(Guid operationID, DataSearchType eventType, Guid? ownerID);


        /// <summary>
        /// 保存备注信息
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType"></param>
        /// <param name="OperationEventCodes"></param>
        /// <param name="ids">ID</param>
        /// <param name="FormIDs">FORM id</param>
        /// <param name="FormTypes">FORM 类型</param>
        /// <param name="IsShowAgents">是否显示给代理</param>
        /// <param name="IsShowCustomers">是否显示给客户</param>
        /// <param name="Subjects">主题集合</param>
        /// <param name="Contents">内容集合</param>
        /// <param name="Types">类型集合</param>
        /// <param name="prioritys"></param>
        /// <param name="Categorys">分类</param>
        /// <param name="Owner">拥有人</param>
        /// <param name="updateDates"></param>
        /// <param name="saveByID"></param>
        /// <param name="ReturnResult"></param>
        /// <param name="occurrenceTime">发生时间</param>
        /// <param name="ManualImportant">当前事件是否手动标识为重要事件</param>
        /// <param name="MessageIDs"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SaveMemoList(
             Guid operationID,
            OperationType operationType,
            EventObjects eventObjects,
            string[] OperationEventCodes,
            string[] ids,
            string[] FormIDs,
            string[] FormTypes,
            string[] IsShowAgents,
            string[] IsShowCustomers,
            string[] Subjects,
            string[] Contents,
            MemoType[] Types,
            MemoPriority[] prioritys,
            string[] Categorys,
            string Owner,
            DateTime?[] updateDates,
            Guid saveByID,
            bool ReturnResult,
              string occurrenceTime, bool ManualImportant,
            string[] MessageIDs
            );


        /// <summary>
        /// 获取备注附件内容
        /// </summary>
        /// <param name="memoID">备注ID</param>
        /// <param name="attachName">附件名</param>
        /// <returns>返回附件内容</returns>
        [FunctionInfomation]
        [OperationContract]
        byte[] GetMemoAttachmentContent(
            Guid memoID,
            string attachName);

        /// <summary>
        /// 保存备注信息Info
        /// </summary>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveMemoInfo(EventObjects eventObjects);


        /// <summary>
        /// 删除备注信息
        /// </summary>
        /// <param name="memoId"></param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        void RemoveMemoInfo(
            Guid memoId,  //是memo.id
            Guid removeByID,
            DateTime? updateDate);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationType"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<EventCode> GetEventCodeList(OperationType operationType);

        /// <summary>
        /// 根据业务的ID和代码ID返回最新的备注记录
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="operationEventId">代码ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        EventObjects GetMemoFirst(Guid operationId, Guid operationEventId);


        /// <summary>
        /// 获取OperationMemos表的ID
        /// </summary>
        /// <param name="operationId">FROMID</param>
        /// <param name="code">事件的CODE</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        DataTable GetOperationMemosID(Guid operationId, string code);

        #endregion
    }
}
