using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;
using System;
using System.Collections.Generic;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 邮件关联参数
    /// </summary>
    [Serializable]
    public class MessageRelationParameter
    {
        // Fields
        private static MessageRelationParameter instance = null;

        // Methods
        public static MessageRelationParameter CreateInstance(AssociateType? associateType, MessageRelationType relationType
            , UpdateDataType updateDataType, List<MailContactInfo> mailContactInfos, List<CustomerCarrierObjects> contacts
            , string messageid, Guid[] operationIds, OperationType[] operationTypes, string[] operationNOs)
        {
            instance = new MessageRelationParameter
            {
                AssociateType = associateType,
                RelationType = relationType,
                UpdateDataType = updateDataType,
                MailContactInfos = mailContactInfos,
                MessageID = messageid,
                OperationIDs = operationIds,
                OperationTypes = operationTypes,
                OperationNOs = operationNOs
            };

            return instance;
        }
        public static MessageRelationParameter CreateInstance(AssociateType? associateType, MessageRelationType relationType
            , UpdateDataType updateDataType, List<MailContactInfo> mailContactInfos, List<CustomerCarrierObjects> contacts
            , string messageid, Guid[] operationIds, OperationType[] operationTypes, string[] operationNOs
            , Message.ServiceInterface.Message message)
        {
            instance = new MessageRelationParameter
            {
                AssociateType = associateType,
                RelationType = relationType,
                UpdateDataType = updateDataType,
                MailContactInfos = mailContactInfos,
                MessageID = messageid,
                OperationIDs = operationIds,
                OperationTypes = operationTypes,
                OperationNOs = operationNOs,
                MessageInfo = message
            };

            return instance;
        }

        public static MessageRelationParameter CreateInstance(AssociateType? associateType, MessageRelationType relationType
            , UpdateDataType updateDataType, List<MailContactInfo> mailContactInfos, List<CustomerCarrierObjects> contacts
            , string messageid, Guid[] operationIds, OperationType[] operationTypes, string[] operationNOs
            , Message.ServiceInterface.Message message,int businessContactType)
        {
            instance = new MessageRelationParameter
            {
                AssociateType = associateType,
                RelationType = relationType,
                UpdateDataType = updateDataType,
                MailContactInfos = mailContactInfos,
                MessageID = messageid,
                OperationIDs = operationIds,
                OperationTypes = operationTypes,
                OperationNOs = operationNOs,
                MessageInfo=message,
                BusinessContactType=businessContactType
            };

            return instance;
        }
        /// <summary>
        /// 关联邮件的阶段
        /// </summary>
        public AssociateType? AssociateType { get; set; }
        /// <summary>
        /// 邮件的所有外部联系人集合
        /// </summary>
        public List<MailContactInfo> MailContactInfos { get; set; }
        /// <summary>
        /// 邮件的MessageID
        /// </summary>
        public string MessageID { get; set; }
        /// <summary>
        /// 业务ID集合
        /// </summary>
        public Guid[] OperationIDs { get; set; }
        /// <summary>
        /// 业务类型集合
        /// </summary>
        public OperationType[] OperationTypes { get; set; }
        /// <summary>
        /// 业务号集合
        /// </summary>
        public string[] OperationNOs { get; set; }

        /// <summary>
        /// 邮件关联类型
        /// </summary>
        public MessageRelationType RelationType { get; set; }
        /// <summary>
        /// 更改数据类型
        /// </summary>
        public UpdateDataType UpdateDataType { get; set; }
        /// <summary>
        /// 邮件实体
        /// </summary>
        public Message.ServiceInterface.Message MessageInfo { get; set; }
        /// <summary>
        /// 业务联系人类型
        /// </summary>
        public int BusinessContactType { get; set; }
    }
}
