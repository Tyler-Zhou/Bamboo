namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    using System;
    using System.Collections.Generic;
    using Framework.CommonLibrary.Common;
    using CommonData = ICP.Common.ServiceInterface.DataObjects;

    /// <summary>
    /// 邮件传真日志列表
    /// </summary>
    [Serializable]
    public partial class CommonMailFaxLogList : BaseDataObject
    {
        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return id == Guid.Empty; } }
        private Guid id;

        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid ID
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
                base.OnPropertyChanged("ID", value);
            }
        }

        private Guid operationID;
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID
        {
            get
            {
                return operationID;
            }
            set
            {
                operationID = value;
                base.OnPropertyChanged("OperationID", value);
            }
        }

        private Guid userMailAccountID;
        /// <summary>
        /// 用户邮件帐号ID
        /// </summary>
        public Guid UserMailAccountID
        {
            get
            {
                return userMailAccountID;
            }

            set
            {
                userMailAccountID = value;
                base.OnPropertyChanged("UserMailAccountID", value);
            }
        }

        private Guid folderID;
        /// <summary>
        /// 文件ID
        /// </summary>
        public Guid FolderID
        {
            get
            {
                return folderID;
            }

            set
            {
                folderID = value;
                base.OnPropertyChanged("FolderID", value);
            }
        }

        private string sender;
        /// <summary>
        /// 发件人
        /// </summary>
        public string Sender
        {
            get
            {
                return sender;
            }

            set
            {
                sender = value;
                base.OnPropertyChanged("Sender", value);
            }
        }

        private string subject;
        /// <summary>
        /// 邮件主题
        /// </summary>
        [StringLength(MaximumLength=500,CMessage="主题",EMessage="Subject")]
        public string Subject
        {
            get
            {
                return subject;
            }
            set
            {
                subject = value;
                base.OnPropertyChanged("Subject", value);
            }
        }

        private string to;
        /// <summary>
        /// 收件人
        /// </summary>
        public string To
        {
            get
            {
                return to;
            }
            set
            {
                to = value;
                base.OnPropertyChanged("To", value);
            }
        }

        private string cc;
        ///<summary>
        ///抄送人
        ///</summary>
        public string CC
        {
            get
            {
                return cc;
            }
            set
            {
                cc=value;
                base.OnPropertyChanged("CC",value);
            }
        }

        private string size;
        ///<summary>
        ///邮件大小
        ///</summary>
        public string Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
                base.OnPropertyChanged("Size", value);
            }
        }

        private string priority;
        ///<summary>
        ///重要性
        ///</summary>
        public string Priority
        {
            get
            {
                return priority;
            }
            set
            {
                priority = value;
                base.OnPropertyChanged("Priority", value);
            }
        }

        private string flag;
        ///<summary>
        ///标记状态
        ///</summary>
        public string Flag
        {
            get
            {
                return flag;
            }
            set
            {
                flag = value;
                base.OnPropertyChanged("Flag", value);
            }
        }

        private string isIncludeAttachment;
        ///<summary>
        ///是否包括附件
        ///</summary>
        public string IsIncludeAttachment
        {
            get
            {
                return isIncludeAttachment;
            }
            set
            {
                isIncludeAttachment = value;
                base.OnPropertyChanged("IsIncludeAttachment", value);
            }
        }

        private string mailcontent;
        /// <summary>
        /// 邮件内容
        /// </summary>
        public string MailContent
        {
            get
            {
                return mailcontent;
            }
            set
            {
                mailcontent = value;
                base.OnPropertyChanged("MailContent", value);
            }
        }

        private bool isfax;
        /// <summary>
        /// 是否发送传真
        /// </summary>
        public bool IsFax
        {
            get
            {
                return isfax;
            }

            set
            {
                isfax = value;
                base.OnPropertyChanged("IsFax", value);
            }
        }

        private List<AttachmentList> attachmentlist;
        /// <summary>
        /// 附件列表
        /// </summary>
        public List<AttachmentList> AttachmentList
        {
            get
            {
                return attachmentlist;
            }

            set
            {
                attachmentlist = value;
                base.OnPropertyChanged("AttachmentList", value);
            }
        }

        private Guid createbyid;
        /// <summary>
        /// 建立人
        /// </summary>
        public Guid CreateByID
        {
            get
            {
                return createbyid;
            }

            set
            {
                createbyid = value;
                base.OnPropertyChanged("CreateByID", value);
            }
        }

        private string createbyname;

        /// <summary>
        /// 建立人
        /// </summary>
        public string CreateByName
        {
            get
            {
                return createbyname;
            }

            set
            {
                createbyname = value;
                base.OnPropertyChanged("CreateByName", value);
            }
        }

        private DateTime createdate;

        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime CreateDate
        {
            get
            {
                return createdate;
            }

            set
            {
                createdate = value;
                base.OnPropertyChanged("CreateDate", value);
            }
        }

        DateTime? _updateDate;
        /// <summary>
        /// 更新时间-做数据版本控制用
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

        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj">obj</param>
        /// <returns>bool</returns>
        public override bool Equals(object obj)
        {
            CommonMailFaxLogList newObj = obj as CommonMailFaxLogList;
            if (newObj == null)
            {
                return false;
            }

            return newObj.ID == ID;
        }

    }

    /// <summary>
    /// 附件列表
    /// </summary>
    [Serializable]
    public class AttachmentList
    {
        /// <summary>
        /// 附件名
        /// </summary>
        public string AttachmentName { get; set; }

        /// <summary>
        /// 附件内容
        /// </summary>
        public byte[] AttachmentConent { get; set; }

        /// <summary>
        /// 附件大小
        /// </summary>
        public float AttachmentSize { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
