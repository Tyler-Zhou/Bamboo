using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
namespace ICP.Message.ServiceInterface
{
    /// <summary>
    /// 消息实体类
    /// </summary>
    [XmlInclude(typeof(MessageEDILogRelation))]
    [XmlInclude(typeof(EventObjects))]
    [Serializable]
    public  class Message
    {
        public static Message messageInfo;
        private static bool createInstance = false;

        public Message()
        {
            Flag = MessageFlag.UnRead;
            Attachments = new List<AttachmentContent>();
            Way = MessageWay.Send;
            Priority = MessagePriority.Normal;
            BodyFormat = BodyFormat.olFormatPlain;
            State = MessageState.Sending;
            CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            Type = MessageType.Email;
            messageInfo = this;
            createInstance = true;
            BackupMailState = false;
            IsAssociated = false;
        }

        public static Message CreateInstance()
        {
            if (!createInstance || messageInfo == null)
                return new Message();
            else
                return messageInfo;
        }

        public PropertyInfo[] GetPropertyInfos()
        {
            return GetType().GetProperties();

        }

        public string GetXmlDataNode(bool createUserPropertiesElement)
        {
            XElement element = new XElement("row");
            PropertyInfo[] propertyInfos = GetPropertyInfos();
            XElement userPropertiesElement = null;
            XCData bodyXC = null;
            string htmlBody = string.Empty;
            foreach (PropertyInfo p in propertyInfos)
            {
                if(p.Name.Equals("IsMailItem"))
                    continue;
                object value = p.GetValue(this, null);
                if (value != null)
                {
                    if (p.PropertyType == typeof(MessageType))
                        value = Type.GetHashCode();
                    else if (p.PropertyType == typeof(MessageFlag))
                        value = Flag.GetHashCode();
                    else if (p.PropertyType == typeof(MessageWay))
                        value = Way.GetHashCode();
                    else if (p.PropertyType == typeof(MessageState))
                        value = State.GetHashCode();
                    else if (p.PropertyType == typeof(BodyFormat))
                        value = BodyFormat.GetHashCode();
                    else if (p.PropertyType == typeof(MessageRelationType))
                        value = RelationType.GetHashCode();
                    else if (p.PropertyType == typeof(MessagePriority))
                        value = Priority.GetHashCode();
                    else if (p.PropertyType == typeof(List<AttachmentContent>))
                        continue;
                    else if (p.PropertyType == typeof(MessageUserPropertiesObject))
                    {
                        if (createUserPropertiesElement)
                            userPropertiesElement = UserProperties.GetXmlDataNode();
                        continue;
                    }
                    if (p.Name.Equals("Body"))
                    {
                        //bodyXC = new XCData(Body);                        
                        htmlBody = Body.ReplaceHexCharacter(string.Empty);
                        continue;
                    }
                    if (p.Name.Equals("SentDateTimeZone"))
                    {
                        value = SentDateTimeZone.ToDateTimeOffsetUTC();
                    }
                    XAttribute nameAttribute = new XAttribute(p.Name, value);
                    element.Add(nameAttribute);
                }
            }
            Guid newID = Guid.NewGuid();
            string flag = string.Format("{0}{1}{2}", "{", newID, "}");
            element.SetValue(flag);
            if (userPropertiesElement != null)
                element.Add(userPropertiesElement);

            string xml = element.ToString().Replace(flag, "<![CDATA[" + htmlBody + "]]>");

            return xml;
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 消息类型（传真，EDI，邮件）
        /// </summary>
        public MessageType Type { get; set; }
        /// <summary>
        /// 是否包含附件
        /// </summary>
        public bool HasAttachment { get; set; }
        /// <summary>
        /// 消息状态（草稿，已发送，发送成功，发送失败，发送成功，发送中）
        /// </summary>
        public MessageState State { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public String Subject { get; set; }
        /// <summary>
        /// 接收人
        /// </summary>
        public String SendTo { get; set; }
        /// <summary>
        /// 发件人
        /// </summary>
        public String SendFrom { get; set; }
        /// <summary>
        /// 消息正文
        /// </summary>
        public String Body { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        /// <summary>
        /// 针对属于那个文件夹下的传真
        /// </summary>
        public Guid FolderId { get; set; }
        public string FolderName { get; set; }
        public String CC { get; set; }
        /// <summary>
        /// 正文格式（html，PainText,Plain）
        /// </summary>
        public BodyFormat BodyFormat { get; set; }
        /// <summary>
        /// 附件内容
        /// </summary>
        public List<AttachmentContent> Attachments { get; set; }
        public String CreatorName { get; set; }
        public Guid CreateBy { get; set; }
        /// <summary>
        /// 针对邮件，查看本地outlook某一封邮件有个MessageID
        /// </summary>
        public string MessageId { get; set; }
        /// <summary>
        /// 消息状态值
        /// </summary>
        public String StateDescription { get; set; }
        public long Size { get; set; }
        /// <summary>
        /// 重要性
        /// </summary>
        public MessagePriority Priority { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public MessageFlag Flag { get; set; }
        /// <summary>
        /// 消息方向（发送，接收）
        /// </summary>
        public MessageWay Way { get; set; }
        /// <summary>
        /// 沟通阶段(SO,SI等)
        /// </summary>
        public string ContactStage { get; set; }
        /// <summary>
        /// 邮件自定义信息
        /// </summary>
        public MessageUserPropertiesObject UserProperties { get; set; }
        /// <summary>
        /// 发件人名称列表
        /// </summary>
        public String FromName { get; set; }
        /// <summary>
        /// 接收人名称列表
        /// </summary>
        public string ToName { get; set; }
        /// <summary>
        /// CC人名称列表
        /// </summary>
        public string CCName { get; set; }

        public string BCC { get; set; }
        public string BCCName { get; set; }

        /// <summary>
        /// 邮件的EntryID
        /// </summary>
        public string EntryID { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime? Sendtime { get; set; }

        /// <summary>
        /// 发送时间(带时区格式)
        /// 邮件按当地时间排序
        /// </summary>
        public DateTimeOffset SentDateTimeZone{ get; set; }
        /// <summary>
        /// 接收时间
        /// </summary>
        public DateTime? ReceivingTime { get; set; }
        /// <summary>
        /// 是邮件实体还是回执实体 只用在UI        
        /// </summary>
        public bool IsMailItem { get; set; }

        /// <summary>
        /// 邮件备份状态
        /// </summary>
        public bool BackupMailState { get; set; }

        /// <summary>
        /// 关联类型
        /// </summary>
        public MessageRelationType RelationType { get; set; }

        /// <summary>
        /// 关联状态
        /// </summary>
        public bool IsAssociated { get; set; }

    }
}
