namespace ICP.FCM.Common.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using CommonData = ICP.Common.ServiceInterface.DataObjects;
    using ICP.Common.ServiceInterface.DataObjects;
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
        /// 获取备注列表
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">业务类型(0:海运出口,1:海运进口,2:空运出口,3:空运进口,4:其他出口,5:其它进口)</param>
        /// <param name="ownerID">所有者ID</param>
        /// <returns>返回备注列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<CommonMemoList> GetMemoList(
            Guid operationID,
            //ICP.Framework.CommonLibrary.Client.OperationType operationType,
            Guid? ownerID);

        /// <summary>
        /// 获取备注信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回备注信</returns>
        [FunctionInfomation]
        [OperationContract]
        CommonMemoInfo GetMemoInfo(Guid id);

        /// <summary>
        /// 保存备注信息
        /// </summary>
        /// <param name="ownerID">所有者ID</param>
        /// <param name="ownerSource">所属业务(0:海运出口,1:海运进口,2:空运出口,3:空运进口,4:其他出口,5:其它进口)</param>
        /// <param name="id">ID</param>
        /// <param name="subject">主题</param>
        /// <param name="content">内容</param>
        /// <param name="attachName">附件名</param>
        /// <param name="attachment">附件内容</param>
        /// <param name="memoType">备注类型</param>
        /// <param name="keyID">字典ID</param>
        /// <param name="isShowAgent">是否代理可见</param>
        /// <param name="isShowCustomer">是否客户可见</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SaveMemoList(
             Guid operationID,
            ICP.Framework.CommonLibrary.Client.OperationType operationType,
            Guid?[] ids,
            Guid?[] formIDs,
            FormType[] formTypes,
            bool[] isShowAgents,
            bool[] isShowCustomers,
            string[] subjects,
            string[] contents,
            CommonData.MemoType[] memoTypes,
            MemoPriority[] prioritys,
            //string[] attachName,
            //byte[][] attachment,        
            //Guid?[] keyID,
            Guid saveByID,
            DateTime?[] updateDates);

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
        /// 删除备注信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        void RemoveMemoInfo(
            Guid memoId,  //是memo.id
            Guid removeByID,
            DateTime? updateDate);

        #endregion
    }
}
