using ICP.Framework.CommonLibrary.Common;
using System;
using EnumContactStage = ICP.Framework.CommonLibrary.Common.ContactStage;

namespace ICP.Message.ServiceInterface
{
    /// <summary>
    /// 操作日志实体类
    /// </summary>
    [Serializable]
    public partial class OperationMessageRelation
    {
        public OperationMessageRelation()
        {
            HasData = true;
            UpdateDataType = UpdateDataType.AddNew;
            ContactStage = EnumContactStage.Unknown;
            UploadServer = true;
            BackupMail = false;
        }
        /// <summary>
        /// 操作日志ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public OperationType OperationType { get; set; }
        /// <summary>
        /// 表单ID
        /// </summary>
        public Guid? FormID { get; set; }
        /// <summary>
        /// 表单类型
        /// </summary>
        public FormType? FormType { get; set; }
        /// <summary>
        /// 传真或邮件日志ID
        /// </summary>
        public Guid IMessageId { get; set; }
        /// <summary>
        /// 邮件内部的MessageId
        /// </summary>
        public string MessageId { get; set; }
        
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate { get; set; }

        public DateTime? CreateDate { get; set; }
        public Guid? UpdateBy { get; set; }
        public Guid CreateBy { get; set; }

        /// <summary>
        /// 是否从数据库中获取了有效记录
        /// </summary>
        public bool HasData { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNo { get; set; }
        /// <summary>
        /// 邮件跟业务的沟通阶段
        /// </summary>
        public EnumContactStage? ContactStage { get; set; }
        /// <summary>
        /// 自动关联或者手动关联
        /// </summary>
        public MessageRelationType RelationType { get; set; }
        /// <summary>
        /// 更改数据类型（累加，覆盖）
        /// </summary>
        public UpdateDataType UpdateDataType { get; set; }

        #region 邮件中心本地缓存表添加栏位

        
        /// <summary>
        /// 邮件所有外部联系人地址 (F1)
        /// </summary>
        public string Contacts { get; set; }
        /// <summary>
        /// 将邮件Message对象序列化成Xml(F2)        
        /// </summary>
        public string XmlMessageInfo { get; set; }
        /// <summary>
        /// 独立存放邮件的HtmlBody（F3）
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 默认从服务端下载下来，即UploadServer=True,对应本地缓存数据库中OperationMessages表栏位(F4)
        /// </summary>
        public bool UploadServer { get; set; }
        /// <summary>
        /// 邮件备份：邮件是否已备份到服务器，对应本地缓存数据库中OperationMessages表栏位(F5)
        /// </summary>
        public bool BackupMail { get; set; }
        /// <summary>
        /// 邮件的EntryID(F8)
        /// </summary>
        public string EntryID { get; set; }

        #endregion
    }
}
