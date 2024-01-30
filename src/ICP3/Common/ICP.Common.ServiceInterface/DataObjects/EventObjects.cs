////#region Comment
/////*
//// * Create By:Taylor Zhou
//// * Create On:2018/7/19 星期四 14:43:27
//// *
//// * Description:
//// *         ->
//// *
//// * History:
//// *         ->
//// */
////#endregion

////using System;
////using System.Linq;
////using System.Xml.Linq;
////using System.Xml.Schema;
////using ICP.Framework.CommonLibrary.Common;
////using ICP.Common.ServiceInterface.DataObjects;
////using System.Xml.Serialization;
////using System.Xml;
////using System.Reflection;
////using System.IO;

////namespace ICP.Common.ServiceInterface
////{
////    /// <summary>
////    /// 事件列表实体类
////    /// </summary>
////    [Serializable]
////    public class EventObjects : IXmlSerializable
////    {
////        /// <summary>
////        /// 
////        /// </summary>
////        public EventObjects()
////        {

////        }
////        public Guid Id
////        {
////            get;
////            set;
////        }

////        /// <summary>
////        /// 业务ID
////        /// </summary>
////        public Guid OperationID
////        {
////            get;
////            set;
////        }

////        /// <summary>
////        /// 业务类型
////        /// </summary>
////        public OperationType OperationType
////        {
////            get;
////            set;
////        }

////        /// <summary>
////        /// FormID
////        /// </summary>
////        public Guid FormID
////        {
////            get;
////            set;
////        }

////        /// <summary>
////        ///Message
////        /// </summary>
////        public Guid MessageID
////        {
////            get;
////            set;
////        }

////        /// <summary>
////        /// 表单类型
////        /// </summary>
////        public FormType FormType
////        {
////            get;
////            set;
////        }

////        /// <summary>
////        /// 代码
////        /// </summary>
////        public string Code
////        {
////            get;
////            set;
////        }
////        /// <summary>
////        /// 事件CodeID
////        /// </summary>
////        public Guid? EventID
////        {
////            get;
////            set;
////        }

////        /// <summary>
////        /// 事件描述
////        /// </summary>
////        public string Description
////        {
////            get;
////            set;
////        }

////        /// <summary>
////        /// 主题
////        /// </summary>
////        public string Subject
////        {
////            get;
////            set;
////        }
////        /// <summary>
////        /// 优先级别
////        /// </summary>
////        public MemoPriority Priority
////        {
////            get;
////            set;
////        }
////        /// <summary>
////        /// 创建时间
////        /// </summary>
////        public DateTime? CreateDate
////        {
////            get;
////            set;
////        }
////        /// <summary>
////        /// 
////        /// </summary>
////        public DateTime? UpdateDate
////        {
////            get;
////            set;
////        }
////        /// <summary>
////        /// 所有人
////        /// </summary>
////        public string Owner
////        {
////            get;
////            set;
////        }

////        /// <summary>
////        /// 更新人
////        /// </summary>
////        public Guid UpdateBy
////        {
////            get;
////            set;
////        }
////        /// <summary>
////        /// 分组名称
////        /// </summary>
////        public string CategoryName
////        { get; set; }
////        /// <summary>
////        /// 是否显示给代理
////        /// </summary>
////        public bool IsShowAgent
////        {
////            get;
////            set;
////        }
////        /// <summary>
////        ///是否显示给客户
////        /// </summary>
////        public bool IsShowCustomer
////        {
////            get;
////            set;
////        }

////        /// <summary>
////        /// 沟通类型
////        /// </summary>
////        public MemoType Type
////        {
////            get;
////            set;
////        }
////        /// <summary>
////        /// 图片是否显示
////        /// </summary>
////        public int ShowImage { get; set; }

////        /// <summary>
////        /// 发生时间
////        /// </summary>
////        public DateTime? OccurrenceTime { get; set; }
////        /// <summary>
////        /// 任务中心点击列表时需要改变的值
////        /// </summary>
////        public string ModifyValue { get; set; }
////        # region 表示当前是否为必须完成事件
////        /// <summary>
////        /// 是否已记录事件 
////        /// </summary>
////        public bool Logged { get; set; }
////        /// <summary>
////        /// 当前事件是否为重要事件
////        /// </summary>
////        public bool Important { get; set; }
////        /// <summary>
////        /// 事件列表排序字段
////        /// </summary>
////        public int EventIndex { get; set; }
////        /// <summary>
////        /// 手动设置当前事件是否为重要事件
////        /// </summary>
////        public bool ManualImportant { get; set; }
////        /// <summary>
////        /// 当前是否为重要事件
////        /// </summary>
////        public bool Required { get; set; }

////        #endregion
////        /// <summary>
////        /// 页面排序字段
////        /// </summary>
////        public int UIIndex { get; set; }
////        /// <summary>
////        /// 邮件的MessageID
////        /// </summary>
////        public string MailMsgID { get; set; }
////        private PropertyInfo[] propertyInfos = null;
////        /// <summary>
////        /// 
////        /// </summary>
////        /// <returns></returns>
////        public PropertyInfo[] GetPropertyInfos()
////        {
////            if (propertyInfos == null)
////            {
////                propertyInfos = GetType().GetProperties();
////            }
////            return propertyInfos;
////        }

////        #region IXmlSerializable Members
////        /// <summary>
////        /// 
////        /// </summary>
////        /// <returns></returns>
////        public override string ToString()
////        {
////            return CheckPath().ToSerializerXml(this);
////        }
////        /// <summary>
////        /// 
////        /// </summary>
////        /// <returns></returns>
////        private string CheckPath()
////        {
////            string xmlPath = Path.GetTempPath();
////            if (!Directory.Exists(xmlPath))
////            {
////                Directory.CreateDirectory(xmlPath);
////            }
////            return string.Format(@"{0}{1}{2}", xmlPath, Guid.NewGuid().ToString(), "EventEntity.xml");
////        }
////        /// <summary>
////        /// 
////        /// </summary>
////        /// <returns></returns>
////        public XmlSchema GetSchema()
////        {

////            return null;

////        }

////        /// <summary>
////        /// 
////        /// </summary>
////        /// <param name="reader"></param>
////        public void ReadXml(XmlReader reader)
////        {

////            string id = reader.GetAttribute("Id");
////            if (!string.IsNullOrEmpty(id))
////            {
////                Id = new Guid(id);
////            }
////            Code = reader.GetAttribute("Code");
////            CreateDate = string.IsNullOrEmpty(reader.GetAttribute("CreateDate")) ? (DateTime?)null : DateTime.Parse(reader.GetAttribute("CreateDate"));
////            Description = reader.GetAttribute("Description");
////            if (!string.IsNullOrEmpty(reader.GetAttribute("EventID")))
////            {
////                EventID = new Guid(reader.GetAttribute("EventID"));
////            }
////            FormID = string.IsNullOrEmpty(reader.GetAttribute("FormID")) ? Guid.Empty : new Guid(reader.GetAttribute("FormID"));
////            FormType = (FormType)Enum.Parse(typeof(FormType), reader.GetAttribute("FormType"));
////            IsShowAgent = bool.Parse(reader.GetAttribute("IsShowAgent"));
////            IsShowCustomer = bool.Parse(reader.GetAttribute("IsShowCustomer"));
////            OperationID = new Guid(reader.GetAttribute("OperationID"));
////            OperationType = (OperationType)Enum.Parse(typeof(OperationType), reader.GetAttribute("OperationType"));
////            Owner = reader.GetAttribute("Owner");
////            Priority = (MemoPriority)Enum.Parse(typeof(MemoPriority), reader.GetAttribute("Priority"));
////            Subject = reader.GetAttribute("Subject");
////            Type = (MemoType)Enum.Parse(typeof(MemoType), reader.GetAttribute("Type"));
////            UpdateBy = new Guid(reader.GetAttribute("UpdateBy"));
////            MessageID = new Guid(reader.GetAttribute("MessageID"));
////            ShowImage = int.Parse(reader.GetAttribute("ShowImage"));
////            ModifyValue = reader.GetAttribute("ModifyValue");
////            if (!string.IsNullOrEmpty(reader.GetAttribute("UpdateDate")))
////            {
////                UpdateDate = string.IsNullOrEmpty(reader.GetAttribute("UpdateDate")) ? (DateTime?)null : DateTime.Parse(reader.GetAttribute("UpdateDate"));
////            }
////            if (!string.IsNullOrEmpty(reader.GetAttribute("OccurrenceTime")))
////            {
////                OccurrenceTime = string.IsNullOrEmpty(reader.GetAttribute("OccurrenceTime")) ? (DateTime?)null : DateTime.Parse(reader.GetAttribute("OccurrenceTime"));
////            }
////            Logged = bool.Parse(reader.GetAttribute("Logged"));
////            Important = bool.Parse(reader.GetAttribute("Important"));
////            EventIndex = int.Parse(reader.GetAttribute("EventIndex"));
////            Required = bool.Parse(reader.GetAttribute("Required"));
////            MailMsgID = reader.GetAttribute("MailMsgID");
////            if (string.IsNullOrEmpty(reader.GetAttribute("CategoryName")) && Required == false)
////            {
////                CategoryName = "Other";
////            }
////            else
////            {
////                CategoryName = reader.GetAttribute("CategoryName");
////            }
////            //手动添加的事件
////            if (!string.IsNullOrEmpty(CategoryName) && string.IsNullOrEmpty(Code))
////            {
////                UIIndex = 1;
////            }
////            //系统自有事件
////            else if (!string.IsNullOrEmpty(CategoryName) && !string.IsNullOrEmpty(Code))
////            {
////                UIIndex = 2;
////            }
////            //必须完成事件
////            else if (string.IsNullOrEmpty(CategoryName) && !string.IsNullOrEmpty(Code))
////            {
////                UIIndex = 3;
////            }
////            ManualImportant = bool.Parse(reader.GetAttribute("ManualImportant"));
////            CategoryName = UIIndex + CategoryName;
////            reader.Skip();

////        }
////        /// <summary>
////        /// 
////        /// </summary>
////        /// <param name="writer"></param>
////        public void WriteXml(XmlWriter writer)
////        {
////            PropertyInfo[] propertyInfos = GetPropertyInfos();
////            foreach (PropertyInfo property in propertyInfos)
////            {
////                object value = property.GetValue(this, null);
////                writer.WriteAttributeString(property.Name, value == null ? string.Empty : value.ToString());
////            }
////        }
////        /// <summary>
////        /// 
////        /// </summary>
////        /// <returns></returns>
////        public XElement GetXmlDataNode()
////        {
////            PropertyInfo[] propertyInfos = GetPropertyInfos();
////            bool emptyOperationID = propertyInfos.Any(item => item.Name.Equals("OperationID") && new Guid(item.GetValue(this, null).ToString()) == Guid.Empty);
////            if (emptyOperationID)
////            {
////                return null;
////            }
////            else
////            {
////                XElement element = new XElement("eventinfo");
////                Array.ForEach(propertyInfos, property =>
////                {
////                    object value = property.GetValue(this, null);
////                    if (value != null)
////                    {
////                        if (property.PropertyType == typeof(OperationType))
////                            value = OperationType.GetHashCode();
////                        else if (property.PropertyType == typeof(FormType))
////                            value = FormType.GetHashCode();
////                        else if (property.PropertyType == typeof(MemoPriority))
////                            value = Priority.GetHashCode();
////                        else if (property.PropertyType == typeof(MemoType))
////                            value = Type.GetHashCode();

////                        XAttribute nameAttribute = new XAttribute(property.Name, value);
////                        element.Add(nameAttribute);
////                    }
////                });

////                return element;
////            }
////        }
////        #endregion
////    }
////}
