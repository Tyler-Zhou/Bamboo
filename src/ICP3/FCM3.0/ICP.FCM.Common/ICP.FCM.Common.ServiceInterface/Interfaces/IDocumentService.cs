namespace ICP.FCM.Common.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using CommonData = ICP.Common.ServiceInterface.DataObjects;
    using System.ServiceModel;

    /// <summary>
    /// 文档接口
    /// </summary>
    [ServiceInfomation("文档接口")]
    [ServiceContract]
    public interface IDocumentService
    {
        #region Document

        /// <summary>
        /// 获取单证列表
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">业务类型(0:海运出口,1:海运进口,2:空运出口,3:空运进口,4:其他出口,5:其它进口)</param>
        /// <param name="ownerID">所有者ID</param>
        /// <returns>返回单证列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<CommonDocumentList> GetDocumentList(
            Guid operationID,
            ICP.Framework.CommonLibrary.Client.OperationType operationType,
            Guid? ownerID);

        /// <summary>
        /// 保存单证信息
        /// </summary>
        /// <param name="ownerID">所有者</param>
        /// <param name="ownerSource">所属业务(0:海运出口,1:海运进口,2:空运出口,3:空运进口,4:其他出口,5:其它进口)</param>
        /// <param name="id">ID</param>
        /// <param name="documentNo">单证号</param>
        /// <param name="documentType">单证类型（0核销单，1提单等）</param>
        /// <param name="numberOfOriginal">正本份数</param>
        /// <param name="numberOfCopies">副本份数</param>
        /// <param name="receivedDate">接收日期</param>
        /// <param name="releaseDate">放单日期</param>
        /// <param name="releaseMode">放单方式（0快递，1当面交接）</param>
        /// <param name="returnDate">退回日期</param>
        /// <param name="trackingNo">快递单号</param>
        /// <param name="attachmentName">附件名称</param>
        /// <param name="attachmentContent">附件</param>
        /// <param name="remark">备注</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult SaveDocumentInfo(
             Guid ownerID,
            ICP.Framework.CommonLibrary.Client.OperationType ownerSource,
            Guid?[] id,
            string[] documentNo,
            DocumentType[] documentType,
            short[] numberOfOriginal,
            short[] numberOfCopies,
            DateTime[] receivedDate,
            DateTime?[] releaseDate,
            DocumentReleaseMode[] releaseMode,
            DateTime?[] returnDate,
            string[] trackingNo,
            string[] attachmentName,
            byte[][] attachmentContent,
            string[] remark,
            Guid saveByID,
            DateTime? updateDate);

        /// <summary>
        /// 获取单证附件内容
        /// </summary>
        /// <param name="documentID">单证ID</param>
        /// <param name="attachName">附件名</param>
        /// <returns>返回附件内容</returns>
        [FunctionInfomation]
        [OperationContract]
        byte[] GeDocumentAttachmentContent(
            Guid documentID,
            string attachName);

        /// <summary>
        /// 删除单证信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveDocumentInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate);

        #endregion
    }
}
