using System;
using System.Xml.Serialization;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.MailCenter.ServiceInterface.DataObjects
{
    /// <summary>
    /// 上下文实体类
    /// </summary>
    /// 
    [Serializable]
    [XmlIncludeAttribute(typeof(ToolMenuItemObject))]
    public partial class ToolMenuItemObject
    {
        /// <summary>
        /// 邮件文件夹操作类型
        /// </summary>
        public OperationFileType MailFolderType
        {
            get;
            set;
        }

        /// <summary>
        /// 业务操作类型
        /// </summary>
        public ICP.Framework.CommonLibrary.Common.OperationType OperationType
        {
            get;
            set;
        }

        /// <summary>
        /// 纯文件名称
        /// </summary>
        public String SafeFilePath
        {
            get;
            set;
        }

        /// <summary>
        /// 文件完成路径
        /// </summary>
        public String FullFilePath
        {
            get;
            set;
        }
        /// <summary>
        /// 发件人
        /// </summary>
        public String Sender
        {
            get;
            set;
        }

        /// <summary>
        /// 表单类型
        /// </summary>
        public FormType FormType
        {
            get;
            set;
        }

        /// <summary>
        /// 可以用各种类型的属性
        /// </summary>
        public object Id
        {
            get;
            set;
        }

        /// <summary>
        ///SO号 
        /// </summary>
        public String SO
        {
            get;
            set;
        }
        /// <summary>
        /// 业务号
        /// </summary>
        public String OperationNO
        {
            get;
            set;
        }
        /// <summary>
        /// 客户名称
        /// </summary>
        public String CustomerName
        {
            get;
            set;
        }

        /// <summary>
        /// 卸货港
        /// </summary>
        public String POD
        {
            get;
            set;
        }

        /// <summary>
        /// 商品
        /// </summary>
        public String Commodity
        {
            get;
            set;
        }

    }
}
