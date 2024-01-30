namespace ICP.FCM.Common.ServiceComponent
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using CommonData = ICP.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Framework.CommonLibrary.Helper;
    using System.Data;
    using System.Linq;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using ICP.Framework.CommonLibrary.Client;

    public partial class FCMCommonService
    {
        #region Document

        /// <summary>
        /// 获取单证列表
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">业务类型(0:海运出口,1:海运进口,2:空运出口,3:空运进口,4:其他出口,5:其它进口)</param>
        /// <param name="ownerID">所有者ID</param>
        /// <returns>返回单证列表</returns>
        public List<CommonDocumentList> GetDocumentList(
            Guid operationID,
            OperationType operationType,
            Guid? ownerID)
        {
            return new List<CommonDocumentList>();
        }

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
        /// <returns>返回ManyResult</returns>
        public ManyResult SaveDocumentInfo(
            Guid ownerID,
            OperationType ownerSource,
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
            DateTime? updateDate)
        {
            return new ManyResult();
        }

        /// <summary>
        /// 获取单证附件内容
        /// </summary>
        /// <param name="documentID">单证ID</param>
        /// <param name="attachName">附件名</param>
        /// <returns>返回附件内容</returns>
        public byte[] GeDocumentAttachmentContent(
            Guid documentID,
            string attachName)
        {
            return null;
        }

        /// <summary>
        /// 删除单证信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        public void RemoveDocumentInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate)
        {
        }

        #endregion
    }
}
