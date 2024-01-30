
namespace ICP.OA.ServiceInterface.DataObjects
{
    using System;
    using System.Collections.Generic; 
    using ICP.Framework.CommonLibrary.Common;
    using System.Xml.Serialization;
    using System.Runtime.Serialization;
    using ICP.Message.ServiceInterface;

    #region Mail
    


    [Serializable]
    public partial class MailFolderInfo : MessageFolderList
    {
        Guid _createbyid;
        /// <summary>
        /// 创建人ID
        /// </summary>
        public Guid CreateById
        {
            get
            {
                return _createbyid;
            }
            set
            {
                if (_createbyid != value)
                {
                    _createbyid = value;
                    base.OnPropertyChanged("CreateById", value);
                }
            }
        }


        string _createby;
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy
        {
            get
            {
                return _createby;
            }
            set
            {
                if (_createby != value)
                {
                    _createby = value;
                    base.OnPropertyChanged("CreateBy", value);
                }
            }
        }


        DateTime _createdate;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate
        {
            get
            {
                return _createdate;
            }
            set
            {
                if (_createdate != value)
                {
                    _createdate = value;
                    base.OnPropertyChanged("CreateDate", value);
                }
            }
        }


    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [System.Xml.Serialization.XmlType]
    public partial class MailMessageList : BaseDataObject
    {
        public MailMessageList()
        {
            this.MailAttachments = new List<MailAttachmentList>();
        }
        public override bool IsNew { get { return ID == Guid.Empty; } }

        Guid _id;
        /// <summary>
        /// 唯一键
        /// </summary>
        [XmlElement("ID")]
        public Guid ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    base.OnPropertyChanged("ID", value);
                }
            }
        }


        Guid _usermailaccountid;
        /// <summary>
        /// 用户邮件账号ID
        /// </summary>
        [XmlElement("UserMailAccountID")]
        public Guid UserMailAccountID
        {
            get
            {
                return _usermailaccountid;
            }
            set
            {
                if (_usermailaccountid != value)
                {
                    _usermailaccountid = value;
                    base.OnPropertyChanged("UserMailAccountID", value);
                }
            }
        }


        string _mailfrom;
        /// <summary>
        /// 发件人
        /// </summary>
        [StringLength(MaximumLength = 100, CMessage = "发件人", EMessage = "mailfrom")]
        [Required(CMessage = "发件人", EMessage = "mailfrom")]
        [XmlElement("MailFrom")]
        public string MailFrom
        {
            get
            {
                return _mailfrom;
            }
            set
            {
                if (_mailfrom != value)
                {
                    _mailfrom = value;
                    base.OnPropertyChanged("MailFrom", value);
                }
            }
        }


        string _mailto;
        /// <summary>
        /// 收件人
        /// </summary>
        [Required(CMessage = "收件件人", EMessage = "mailto")]
        [XmlElement("MailTo")]
        public string MailTo
        {
            get
            {
                return _mailto;
            }
            set
            {
                if (_mailto != value)
                {
                    _mailto = value;
                    base.OnPropertyChanged("MailTo", value);
                }
            }
        }


        string _mailcc;
        /// <summary>
        /// 抄送人
        /// </summary>
        [XmlElement("MailCC")]
        public string MailCC
        {
            get
            {
                return _mailcc;
            }
            set
            {
                if (_mailcc != value)
                {
                    _mailcc = value;
                    base.OnPropertyChanged("MailCC", value);
                }
            }
        }


        Guid _folderid;
        /// <summary>
        /// 文件ID
        /// </summary>
        [Required(CMessage = "文件",EMessage="Folder")]
        [XmlElement("FolderID")]
        public Guid FolderID
        {
            get
            {
                return _folderid;
            }
            set
            {
                if (_folderid != value)
                {
                    _folderid = value;
                    base.OnPropertyChanged("FolderID", value);
                }
            }
        }


        string _foldername;
        /// <summary>
        /// 文件夹
        /// </summary>
        [XmlElement("FolderName")]
        public string FolderName
        {
            get
            {
                return _foldername;
            }
            set
            {
                if (_foldername != value)
                {
                    _foldername = value;
                    base.OnPropertyChanged("FolderName", value);
                }
            }
        }


        string _subject;
        /// <summary>
        /// 邮件主题
        /// </summary>
        [StringLength(MaximumLength=500,CMessage="邮件主题",EMessage="Subject")]
        [Required(CMessage = "邮件主题", EMessage = "Subject")]
        [XmlElement("Subject")]
        public string Subject
        {
            get
            {
                return _subject;
            }
            set
            {
                if (_subject != value)
                {
                    _subject = value;
                    base.OnPropertyChanged("Subject", value);
                }
            }
        }

      


        MessagePriority _priority;
        /// <summary>
        /// 重要性
        /// </summary>
        [XmlElement("Priority")]
        public MessagePriority Priority
        {
            get
            {
                return _priority;
            }
            set
            {
                if (_priority != value)
                {
                    _priority = value;
                    base.OnPropertyChanged("Priority", value);
                }
            }
        }


        MessageFlag _flag;
        /// <summary>
        /// 状态（1未读、2已读、3答复,4转发）
        /// </summary>
        [XmlElement("Flag")]
        public MessageFlag Flag
        {
            get
            {
                return _flag;
            }
            set
            {
                if (_flag != value)
                {
                    _flag = value;
                    base.OnPropertyChanged("Flag", value);
                }
            }
        }


        bool _isfax;
        /// <summary>
        /// 是否发送传真
        /// </summary>
        [Required(CMessage = "是否发送传真", EMessage = "isfax")]
        [XmlElement("IsFax")]
        public bool IsFax
        {
            get
            {
                return _isfax;
            }
            set
            {
                if (_isfax != value)
                {
                    _isfax = value;
                    base.OnPropertyChanged("IsFax", value);
                }
            }
        }

        /// <summary>
        /// 包含附件
        /// </summary>
        [XmlElement("IsIncludeAttachment")]
        public bool IsIncludeAttachment{get;set;}


        long _size;
        [XmlElement("Size")]
        public long Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }



        int _rowIndex;
        [XmlElement("RowIndex")]
        public int RowIndex
        {
            get
            {
                return _rowIndex;
            }
            set
            {
                _rowIndex = value;
            }
        }

        List<MailAttachmentList> _mailattachments;
        /// <summary>
        /// 附件名列表
        /// </summary>
        [XmlArray("MailAttachments")]
        [XmlArrayItem("MailAttachment")]
        public List<MailAttachmentList> MailAttachments
        {
            get
            {
                return _mailattachments;
            }
            set
            {
                if (_mailattachments != value)
                {
                    _mailattachments = value;
                    base.OnPropertyChanged("MailAttachmentNames", value);
                }
            }
        }

     

        DateTime _createdate;
        /// <summary>
        /// 创建时间
        /// </summary>
        [XmlElement(ElementName = "CreateDate")]
        public DateTime CreateDate
        {
            get
            {
                return _createdate;
            }
            set
            {
                if (_createdate != value)
                {
                    _createdate = value;
                    base.OnPropertyChanged("CreateDate", value);
                }
            }
        }

        DateTime? _updateDate;
        /// <summary>
        /// 建立时间
        /// </summary>
        [XmlIgnore]
        public DateTime? UpdateDate
        {
            get
            {
                return _updateDate;
            }
            set
            {
                if (_updateDate != value)
                {
                    _updateDate = value;
                    base.OnPropertyChanged("UpdateDate", value);
                }
            }
        }

        public override bool Equals(object obj)
        {
            MailMessageList newObj = obj as MailMessageList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
    }


    /// <summary>
    /// 邮件消息日志集合
    /// <example>
    /// 
    /// </example>
    /// </summary>
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute()]
    [System.Xml.Serialization.XmlRootAttribute(IsNullable = false,  ElementName = "MailMessages")]
    public partial class MailMessageCollect
    {
        public MailMessageCollect()
        {
            this.MailMessages = new List<MailMessageList>();
        }

        [System.Xml.Serialization.XmlElement("MailMessage")]
        public List<MailMessageList> MailMessages { get; set; }
    }


    [Serializable]
    public partial class MailMessagesInfo : MailMessageList
    {
        byte[] _mailcontent;
        /// <summary>
        /// 邮件内容
        /// </summary>
        public byte[] MailContent
        {
            get
            {
                return _mailcontent;
            }
            set
            {
                if (_mailcontent != value)
                {
                    _mailcontent = value;
                    base.OnPropertyChanged("MailContent", value);
                }
            }
        }


        Guid _createbyid;
        /// <summary>
        /// 创建人ID
        /// </summary>
        public Guid CreateById
        {
            get
            {
                return _createbyid;
            }
            set
            {
                if (_createbyid != value)
                {
                    _createbyid = value;
                    base.OnPropertyChanged("CreateById", value);
                }
            }
        }


        string _createby;
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy
        {
            get
            {
                return _createby;
            }
            set
            {
                if (_createby != value)
                {
                    _createby = value;
                    base.OnPropertyChanged("CreateBy", value);
                }
            }
        }


      


    }


    [Serializable]
    [System.Xml.Serialization.XmlType]
    public partial class MailAttachmentList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty; } }

        Guid _id;
        /// <summary>
        /// 唯一键
        /// </summary>
        [XmlElement("ID")]
        public Guid ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    base.OnPropertyChanged("ID", value);
                }
            }
        }


        Guid _mailmessageid;
        /// <summary>
        /// 邮件ID
        /// </summary>
        [Required(CMessage = "邮件",EMessage="MailMessage")]
        [XmlElement("MailMessageID")]
        public Guid MailMessageID
        {
            get
            {
                return _mailmessageid;
            }
            set
            {
                if (_mailmessageid != value)
                {
                    _mailmessageid = value;
                    base.OnPropertyChanged("MailMessageID", value);
                }
            }
        }


        string _attachmentname;
        /// <summary>
        /// 附件名称
        /// </summary>
        [StringLength(MaximumLength=200,CMessage="附件名",EMessage="AttachmentName")]
        [XmlElement("AttachmentName")]
        public string AttachmentName
        {
            get
            {
                return _attachmentname;
            }
            set
            {
                if (_attachmentname != value)
                {
                    _attachmentname = value;
                    base.OnPropertyChanged("AttachmentName", value);
                }
            }
        }


        /// <summary>
        /// 附件大小
        /// </summary>
        [XmlElement("Size")]
        public long Size { get; set; }

        public override bool Equals(object obj)
        {
            MailAttachmentList newObj = obj as MailAttachmentList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
    }

    /// <summary>
    /// 邮件和日志附件类
    /// </summary>
    [Serializable]
    [KnownType(typeof(MailAttachmentClientInfo))]
    public partial class MailAttachmentInfo : MailAttachmentList
    {
        byte[] _attachment;
        /// <summary>
        /// 附件内容
        /// </summary>
        public byte[] Attachment
        {
            get
            {
                return _attachment;
            }
            set
            {
                if (_attachment != value)
                {
                    _attachment = value;
                    base.OnPropertyChanged("Attachment", value);
                }
            }
        }


        DateTime _createdate;
        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime CreateDate
        {
            get
            {
                return _createdate;
            }
            set
            {
                if (_createdate != value)
                {
                    _createdate = value;
                    base.OnPropertyChanged("CreateDate", value);
                }
            }
        }


    }

    #endregion

    #region Document

    [Serializable]
    public partial class DocumentFolderFileList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty; } }

        Guid _id;
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    base.OnPropertyChanged("ID", value);
                }
            }
        }


        Guid _parentid;
        /// <summary>
        /// 文件夹ID
        /// </summary>
        [Required(CMessage = "文件夹",EMessage="ParentFiler")]

        public Guid ParentID
        {
            get
            {
                return _parentid;
            }
            set
            {
                if (_parentid != value)
                {
                    _parentid = value;
                    base.OnPropertyChanged("ParentID", value);
                }
            }
        }


        string _parentname;
        /// <summary>
        /// 父名称
        /// </summary>
        public string ParentName
        {
            get
            {
                return _parentname;
            }
            set
            {
                if (_parentname != value)
                {
                    _parentname = value;
                    base.OnPropertyChanged("ParentName", value);
                }
            }
        }


        string _hierarchycode;
        /// <summary>
        /// 级联代码
        /// </summary>
        public string HierarchyCode
        {
            get
            {
                return _hierarchycode;
            }
            set
            {
                if (_hierarchycode != value)
                {
                    _hierarchycode = value;
                    base.OnPropertyChanged("HierarchyCode", value);
                }
            }
        }


        string _name;
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(MaximumLength=100,CMessage="名称",EMessage="Name")]

        [Required(CMessage = "名称", EMessage = "Name")]

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    base.OnPropertyChanged("Name", value);
                }
            }
        }

        DocuentPermission _Permission;
        /// <summary>
        /// 是否管理员
        /// </summary>
        public DocuentPermission Permission
        {
            get
            {
                return _Permission;
            }
            set
            {
                if (_Permission != value)
                {
                    _Permission = value;
                    base.OnPropertyChanged("Permission", value);
                }
            }
        }

        string _description;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    base.OnPropertyChanged("Description", value);
                }
            }
        }


        OADocumentType _documenttype;
        /// <summary>
        /// 类型
        /// </summary>
        public OADocumentType DocumentType
        {
            get
            {
                return _documenttype;
            }
            set
            {
                if (_documenttype != value)
                {
                    _documenttype = value;
                    base.OnPropertyChanged("OADocumentType", value);
                }
            }
        }


        Guid _createbyid;
        /// <summary>
        /// 建立人
        /// </summary>
        public Guid CreateByID
        {
            get
            {
                return _createbyid;
            }
            set
            {
                if (_createbyid != value)
                {
                    _createbyid = value;
                    base.OnPropertyChanged("CreateByID", value);
                }
            }
        }


        string _createbyname;
        /// <summary>
        /// 建立人
        /// </summary>
        public string CreateByName
        {
            get
            {
                return _createbyname;
            }
            set
            {
                if (_createbyname != value)
                {
                    _createbyname = value;
                    base.OnPropertyChanged("CreateByName", value);
                }
            }
        }


        DateTime _createdate;
        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime CreateDate
        {
            get
            {
                return _createdate;
            }
            set
            {
                if (_createdate != value)
                {
                    _createdate = value;
                    base.OnPropertyChanged("CreateDate", value);
                }
            }
        }

        DateTime? _updateDate;
        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime? UpdateDate
        {
            get
            {
                return _updateDate;
            }
            set
            {
                if (_updateDate != value)
                {
                    _updateDate = value;
                    base.OnPropertyChanged("UpdateDate", value);
                }
            }
        }

        ///// <summary>
        ///// 权限
        ///// </summary>
        //public short DocuentPermission { get; set; }


        ///// <summary>
        ///// 是否有权限
        ///// </summary>
        ///// <param name="permission">权限</param>
        ///// <returns>如果有返回true,否则返回false</returns>
        //public bool HasRight(FilePermission permission)
        //{
        //    return (this.Rights & (short)permission)>0;
        //}

        ///// <summary>
        ///// 是否有权限
        ///// </summary>
        ///// <param name="permission">权限</param>
        ///// <returns>如果有返回true,否则返回false</returns>
        //public bool HasRight(FolderPermission permission)
        //{
        //    return (this.Rights & (short)permission)>0;
        //}

        public override bool Equals(object obj)
        {
            DocumentFolderFileList newObj = obj as DocumentFolderFileList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
    }

    [Serializable]
    public partial class DocumentFileList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty; } }

        Guid _id;
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    base.OnPropertyChanged("ID", value);
                }
            }
        }


        Guid _folderid;
        /// <summary>
        /// 文件夹ID
        /// </summary>
        [Required(CMessage = "文件夹",EMessage="Folder")]

        public Guid FolderID
        {
            get
            {
                return _folderid;
            }
            set
            {
                if (_folderid != value)
                {
                    _folderid = value;
                    base.OnPropertyChanged("FolderID", value);
                }
            }
        }


        string _foldername;
        /// <summary>
        /// 文件夹名称
        /// </summary>
        public string FolderName
        {
            get
            {
                return _foldername;
            }
            set
            {
                if (_foldername != value)
                {
                    _foldername = value;
                    base.OnPropertyChanged("FolderName", value);
                }
            }
        }


        string _filename;
        /// <summary>
        /// 文件名称
        /// </summary>
        [StringLength(MaximumLength=100,CMessage="文件名称",EMessage="FileName")]

        [Required(CMessage = "文件名称",EMessage="FileName")]

        public string FileName
        {
            get
            {
                return _filename;
            }
            set
            {
                if (_filename != value)
                {
                    _filename = value;
                    base.OnPropertyChanged("FileName", value);
                }
            }
        }


        Guid _createbyid;
        /// <summary>
        /// 建立人
        /// </summary>
        public Guid CreateByID
        {
            get
            {
                return _createbyid;
            }
            set
            {
                if (_createbyid != value)
                {
                    _createbyid = value;
                    base.OnPropertyChanged("CreateByID", value);
                }
            }
        }


        string _createbyname;
        /// <summary>
        /// 建立人
        /// </summary>
        public string CreateByName
        {
            get
            {
                return _createbyname;
            }
            set
            {
                if (_createbyname != value)
                {
                    _createbyname = value;
                    base.OnPropertyChanged("CreateByName", value);
                }
            }
        }


        DateTime _createdate;
        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime CreateDate
        {
            get
            {
                return _createdate;
            }
            set
            {
                if (_createdate != value)
                {
                    _createdate = value;
                    base.OnPropertyChanged("CreateDate", value);
                }
            }
        }

        DateTime? _updateDate;
        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime? UpdateDate
        {
            get
            {
                return _updateDate;
            }
            set
            {
                if (_updateDate != value)
                {
                    _updateDate = value;
                    base.OnPropertyChanged("UpdateDate", value);
                }
            }
        }

        string _filedescription;
        /// <summary>
        /// 描述（便于查询使用）
        /// </summary>
        [StringLength(MaximumLength=200,CMessage="描述",EMessage="FileDescription")]

        public string FileDescription
        {
            get
            {
                return _filedescription;
            }
            set
            {
                if (_filedescription != value)
                {
                    _filedescription = value;
                    base.OnPropertyChanged("FileDescription", value);
                }
            }
        }


        public override bool Equals(object obj)
        {
            DocumentFileList newObj = obj as DocumentFileList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
    }


    [Serializable]
    public partial class DocumentFileInfo : DocumentFileList
    {
        public byte[] Content { get; set; }
        /// <summary>
        /// StreamID
        /// </summary>
        public Guid? StreamID { get; set; }
    }


    [Serializable]
    public partial class DocumentFilePermissionList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty; } }

        Guid _id;
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    base.OnPropertyChanged("ID", value);
                }
            }
        }


        Guid _fileid;
        /// <summary>
        /// 文件夹ID
        /// </summary>
        [Required(CMessage = "文件夹",EMessage="File")]

        public Guid FileID
        {
            get
            {
                return _fileid;
            }
            set
            {
                if (_fileid != value)
                {
                    _fileid = value;
                    base.OnPropertyChanged("FileID", value);
                }
            }
        }


        string _filename;
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName
        {
            get
            {
                return _filename;
            }
            set
            {
                if (_filename != value)
                {
                    _filename = value;
                    base.OnPropertyChanged("FileName", value);
                }
            }
        }


        Guid? _organizationid;
        /// <summary>
        /// 组织架构ID
        /// </summary>
        public Guid? OrganizationID
        {
            get
            {
                return _organizationid;
            }
            set
            {
                if (_organizationid != value)
                {
                    _organizationid = value;
                    base.OnPropertyChanged("OrganizationID", value);
                }
            }
        }


        string _organizationname;
        /// <summary>
        /// 组织架构
        /// </summary>
        public string OrganizationName
        {
            get
            {
                return _organizationname;
            }
            set
            {
                if (_organizationname != value)
                {
                    _organizationname = value;
                    base.OnPropertyChanged("OrganizationName", value);
                }
            }
        }


        Guid? _useobjectid;
        /// <summary>
        /// 使用对象ID（对应岗位或用户）
        /// </summary>
        public Guid? UseObjectID
        {
            get
            {
                return _useobjectid;
            }
            set
            {
                if (_useobjectid != value)
                {
                    _useobjectid = value;
                    base.OnPropertyChanged("UseObjectID", value);
                }
            }
        }


        string _useobjectname;
        /// <summary>
        /// 使用对象
        /// </summary>
        public string UseObjectName
        {
            get
            {
                return _useobjectname;
            }
            set
            {
                if (_useobjectname != value)
                {
                    _useobjectname = value;
                    base.OnPropertyChanged("UseObjectName", value);
                }
            }
        }


        bool _ismanager;
        /// <summary>
        /// 是否管理员
        /// </summary>
        [Required(CMessage = "是否管理员",EMessage="IsManager")]

        public bool IsManager
        {
            get
            {
                return _ismanager;
            }
            set
            {
                if (_ismanager != value)
                {
                    _ismanager = value;
                    base.OnPropertyChanged("IsManager", value);
                }
            }
        }


        bool _isview;
        /// <summary>
        /// 是否可以查看
        /// </summary>
        [Required(CMessage = "是否可以查看",EMessage="IsView")]

        public bool IsView
        {
            get
            {
                return _isview;
            }
            set
            {
                if (_isview != value)
                {
                    _isview = value;
                    base.OnPropertyChanged("IsView", value);
                }
            }
        }


        bool _isedit;
        /// <summary>
        /// 是否可以编辑
        /// </summary>
        [Required(CMessage = "是否可以编辑",EMessage="IsEdit")]

        public bool IsEdit
        {
            get
            {
                return _isedit;
            }
            set
            {
                if (_isedit != value)
                {
                    _isedit = value;
                    base.OnPropertyChanged("IsEdit", value);
                }
            }
        }


        bool _isdelete;
        /// <summary>
        /// 是否可以删除
        /// </summary>
        [Required(CMessage = "是否可以删除",EMessage="IsDelete")]

        public bool IsDelete
        {
            get
            {
                return _isdelete;
            }
            set
            {
                if (_isdelete != value)
                {
                    _isdelete = value;
                    base.OnPropertyChanged("IsDelete", value);
                }
            }
        }


        Guid _createbyid;
        /// <summary>
        /// 建立人
        /// </summary>
        public Guid CreateByID
        {
            get
            {
                return _createbyid;
            }
            set
            {
                if (_createbyid != value)
                {
                    _createbyid = value;
                    base.OnPropertyChanged("CreateByID", value);
                }
            }
        }


        string _createbyname;
        /// <summary>
        /// 建立人
        /// </summary>
        public string CreateByName
        {
            get
            {
                return _createbyname;
            }
            set
            {
                if (_createbyname != value)
                {
                    _createbyname = value;
                    base.OnPropertyChanged("CreateByName", value);
                }
            }
        }


        DateTime _createdate;
        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime CreateDate
        {
            get
            {
                return _createdate;
            }
            set
            {
                if (_createdate != value)
                {
                    _createdate = value;
                    base.OnPropertyChanged("CreateDate", value);
                }
            }
        }

        DateTime? _updateDate;
        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime? UpdateDate
        {
            get
            {
                return _updateDate;
            }
            set
            {
                if (_updateDate != value)
                {
                    _updateDate = value;
                    base.OnPropertyChanged("UpdateDate", value);
                }
            }
        }

        public override bool Equals(object obj)
        {
            DocumentFilePermissionList newObj = obj as DocumentFilePermissionList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
    }


    [Serializable]
    public partial class DocumentFilePermissionInfo : DocumentFilePermissionList
    {
    }


    [Serializable]
    public partial class DocumentFolderList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty; } }

        Guid _id;
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    base.OnPropertyChanged("ID", value);
                }
            }
        }


        string _cname;
        /// <summary>
        /// 中文名称
        /// </summary>
        [StringLength(MaximumLength=200,CMessage="中文名称",EMessage="ChineseName")]
        [Required(CMessage = "中文名称", EMessage = "ChineseName")]
        public string Name
        {
            get
            {
                return _cname;
            }
            set
            {
                if (_cname != value)
                {
                    _cname = value;
                    base.OnPropertyChanged("CName", value);
                }
            }
        }

        /// <summary>
        /// 权限
        /// </summary>
        public DocuentPermission Permission
        {
            get;
            set;
        }


        FolderType _type;
        /// <summary>
        /// 类型（1:个人,2:公共,3:邮件）
        /// </summary>
        [Required(CMessage = "类型",EMessage="Type")]

        public FolderType Type
        {
            get
            {
                return _type;
            }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    base.OnPropertyChanged("Type", value);
                }
            }
        }


        string _parentname;
        /// <summary>
        /// 父名
        /// </summary>
        public string ParentName
        {
            get
            {
                return _parentname;
            }
            set
            {
                if (_parentname != value)
                {
                    _parentname = value;
                    base.OnPropertyChanged("ParentName", value);
                }
            }
        }


        Guid _parentid;
        /// <summary>
        /// 父ID
        /// </summary>
        public Guid ParentID
        {
            get
            {
                return _parentid;
            }
            set
            {
                if (_parentid != value)
                {
                    _parentid = value;
                    base.OnPropertyChanged("ParentID", value);
                }
            }
        }


        string _hierarchycode;
        /// <summary>
        /// 层次结构
        /// </summary>
        public string HierarchyCode
        {
            get
            {
                return _hierarchycode;
            }
            set
            {
                if (_hierarchycode != value)
                {
                    _hierarchycode = value;
                    base.OnPropertyChanged("HierarchyCode", value);
                }
            }
        }


        Guid _createbyid;
        /// <summary>
        /// 建立人
        /// </summary>
        public Guid CreateByID
        {
            get
            {
                return _createbyid;
            }
            set
            {
                if (_createbyid != value)
                {
                    _createbyid = value;
                    base.OnPropertyChanged("CreateByID", value);
                }
            }
        }


        string _createbyname;
        /// <summary>
        /// 建立人
        /// </summary>
        public string CreateByName
        {
            get
            {
                return _createbyname;
            }
            set
            {
                if (_createbyname != value)
                {
                    _createbyname = value;
                    base.OnPropertyChanged("CreateByName", value);
                }
            }
        }


        DateTime _createdate;
        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime CreateDate
        {
            get
            {
                return _createdate;
            }
            set
            {
                if (_createdate != value)
                {
                    _createdate = value;
                    base.OnPropertyChanged("CreateDate", value);
                }
            }
        }

        DateTime? _updateDate;
        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime? UpdateDate
        {
            get
            {
                return _updateDate;
            }
            set
            {
                if (_updateDate != value)
                {
                    _updateDate = value;
                    base.OnPropertyChanged("UpdateDate", value);
                }
            }
        }

        public override bool Equals(object obj)
        {
            DocumentFolderList newObj = obj as DocumentFolderList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
    }


    [Serializable]
    public partial class DocumentFolderInfo : DocumentFolderList
    {
    }


    [Serializable]
    public partial class DocumentPermissionList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty; } }

        Guid _id;
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    base.OnPropertyChanged("ID", value);
                }
            }
        }


        //Guid _folderid;
        ///// <summary>
        ///// 文件夹ID
        ///// </summary>
        //[Required(ErrorMessage = "文件夹必须填写")]

        //public Guid FolderID
        //{
        //    get
        //    {
        //        return _folderid;
        //    }
        //    set
        //    {
        //        if (_folderid != value)
        //        {
        //            _folderid = value;
        //            base.OnPropertyChanged("FolderID", value);
        //        }
        //    }
        //}


        string _foldername;
        /// <summary>
        /// 文件夹名称
        /// </summary>
        public string FolderName
        {
            get
            {
                return _foldername;
            }
            set
            {
                if (_foldername != value)
                {
                    _foldername = value;
                    base.OnPropertyChanged("FolderName", value);
                }
            }
        }


        DocuentPermission _Permission;
        /// <summary>
        /// 是否管理员
        /// </summary>
        public DocuentPermission Permission
        {
            get
            {
                return _Permission;
            }
            set
            {
                if (_Permission != value)
                {
                    _Permission = value;
                    base.OnPropertyChanged("Permission", value);
                }
            }
        }


        public bool _isParentPermission;
        public bool IsParentPermission
        { 
             get
                {
                    return _isParentPermission;
                }
                set
                {
                    if (_isParentPermission != value)
                    {
                        _isParentPermission = value;
                        base.OnPropertyChanged("IsParentPermission", value);
                    }
                }
        }

        Guid _createbyid;
        /// <summary>
        /// 建立人
        /// </summary>
        public Guid CreateByID
        {
            get
            {
                return _createbyid;
            }
            set
            {
                if (_createbyid != value)
                {
                    _createbyid = value;
                    base.OnPropertyChanged("CreateByID", value);
                }
            }
        }


        string _createbyname;
        /// <summary>
        /// 建立人
        /// </summary>
        public string CreateByName
        {
            get
            {
                return _createbyname;
            }
            set
            {
                if (_createbyname != value)
                {
                    _createbyname = value;
                    base.OnPropertyChanged("CreateByName", value);
                }
            }
        }


        DateTime _createdate;
        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime CreateDate
        {
            get
            {
                return _createdate;
            }
            set
            {
                if (_createdate != value)
                {
                    _createdate = value;
                    base.OnPropertyChanged("CreateDate", value);
                }
            }
        }

        DateTime? _updateDate;
        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime? UpdateDate
        {
            get
            {
                return _updateDate;
            }
            set
            {
                if (_updateDate != value)
                {
                    _updateDate = value;
                    base.OnPropertyChanged("UpdateDate", value);
                }
            }
        }

        public override bool Equals(object obj)
        {
            DocumentPermissionList newObj = obj as DocumentPermissionList;
            if (newObj == null) return false;
            return newObj.ID == this.ID;
        }
    }

    [Serializable]
    public partial class DocumentUserPermissionList : DocumentPermissionList
    {
        

        Guid? _userid;
        /// <summary>
        /// 使用对象ID
        /// </summary>
        public Guid? UserID
        {
            get
            {
                return _userid;
            }
            set
            {
                if (_userid != value)
                {
                    _userid = value;
                    base.OnPropertyChanged("UserID", value);
                }
            }
        }


        string _username;
        /// <summary>
        /// 使用对象
        /// </summary>
        public string UserName
        {
            get
            {
                return _username;
            }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    base.OnPropertyChanged("UserName", value);
                }
            }
        }

    }
    [Serializable]
    public class MailAttachmentClientInfo : MailAttachmentInfo
    {
        public string FileName { get; set; }
    }

    [Serializable]
    public partial class DocumentOrganizationJobPermissionList : DocumentPermissionList
    {
        Guid? _organizationid;
        /// <summary>
        /// 
        /// </summary>
        public Guid? OrganizationID
        {
            get
            {
                return _organizationid;
            }
            set
            {
                if (_organizationid != value)
                {
                    _organizationid = value;
                    base.OnPropertyChanged("OrganizationID", value);
                }
            }
        }


        string _organizationname;
        /// <summary>
        /// 组织架构
        /// </summary>
        public string OrganizationName
        {
            get
            {
                return _organizationname;
            }
            set
            {
                if (_organizationname != value)
                {
                    _organizationname = value;
                    base.OnPropertyChanged("OrganizationName", value);
                }
            }
        }


        Guid? _jobid;
        /// <summary>
        /// 使用对象ID
        /// </summary>
        public Guid? JobID
        {
            get
            {
                return _jobid;
            }
            set
            {
                if (_jobid != value)
                {
                    _jobid = value;
                    base.OnPropertyChanged("JobID", value);
                }
            }
        }


        string _jobName;
        /// <summary>
        /// 使用对象
        /// </summary>
        public string JobName
        {
            get
            {
                return _jobName;
            }
            set
            {
                if (_jobName != value)
                {
                    _jobName = value;
                    base.OnPropertyChanged("JobName", value);
                }
            }
        }

    }
 
    #endregion

}
