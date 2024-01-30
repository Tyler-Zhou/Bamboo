namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    using System;
    using System.Collections.Generic;
    using System.Text; 
    using ICP.Framework.CommonLibrary.Common;
    using CommonData = ICP.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Client;

    /// <summary>
    /// 备注列表
    /// </summary> 
    [Serializable]
    public partial class CommonMemoList : BaseDataObject
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

        public Guid FormID
        {
            get;
            set;
        }
        public ICP.Framework.CommonLibrary.Common.FormType FormType
        {
            get;
            set;
        }

        private Guid ownerid;

        /// <summary>
        /// 托运单ID，即表单id
        /// </summary>
        public Guid OwnerID
        {
            get
            {
                return ownerid;
            }

            set
            {
                ownerid = value;
                base.OnPropertyChanged("OwnerID", value);
            }
        }

        private ICP.Framework.CommonLibrary.Common.OperationType ownersource;

        /// <summary>
        /// 备注所属业务
        /// </summary>
        public ICP.Framework.CommonLibrary.Common.OperationType OwnerSource
        {
            get
            {
                return ownersource;
            }

            set
            {
                ownersource = value;
                base.OnPropertyChanged("OwnerSource", value);
            }
        }

        private string subject;

        /// <summary>
        /// 主题
        /// </summary>
        [StringLength(MaximumLength=200,CMessage="主题",EMessage="Subject")]
        [Required(CMessage = "主题", EMessage = "Subject")]
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

        private string content;

        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
                base.OnPropertyChanged("Content", value);
            }
        }

        private bool isshowagent;

        /// <summary>
        /// 是否显示给代理
        /// </summary>
        [Required(CMessage = "是否显示给代理",EMessage="IsShowAgent")]
        public bool IsShowAgent
        {
            get
            {
                return isshowagent;
            }

            set
            {
                isshowagent = value;
                base.OnPropertyChanged("IsShowAgent", value);
            }
        }

        private bool isshowcustomer;

        /// <summary>
        /// 是否显示给客户
        /// </summary>
        [Required(CMessage = "是否显示给客户",EMessage="IsShowCustomer")]
        public bool IsShowCustomer
        {
            get
            {
                return isshowcustomer;
            }

            set
            {
                isshowcustomer = value;
                base.OnPropertyChanged("IsShowCustomer", value);
            }
        }

        private Guid createbyId;
        /// <summary>
        /// 建立人ID
        /// </summary>
        public Guid CreateByID
        {
            get
            {
                return createbyId;
            }

            set
            {
                createbyId = value;
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
        /// 行版本
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
            CommonMemoList newObj = obj as CommonMemoList;
            if (newObj == null)
            {
                return false;
            }

            return newObj.ID == this.ID;
        }
    }

    /// <summary>
    /// 备注信息
    /// </summary>
    [Serializable]
    public partial class CommonMemoInfo : CommonMemoList
    {
        private Guid? keyid;

        /// <summary>
        /// 关键字ID
        /// </summary>
        public Guid? KeyID
        {
            get
            {
                return keyid;
            }

            set
            {
                keyid = value;
                base.OnPropertyChanged("KeyID", value);
            }
        }

        private string keyname;

        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyName
        {
            get
            {
                return keyname;
            }

            set
            {
                keyname = value;
                base.OnPropertyChanged("KeyName", value);
            }
        }

        private string attachmentname;

        /// <summary>
        /// 附件名
        /// </summary>
        [StringLength(MaximumLength=100,CMessage="附件名",EMessage="AttachmentName")]
        public string AttachmentName
        {
            get
            {
                return attachmentname;
            }

            set
            {
                attachmentname = value;
                base.OnPropertyChanged("AttachmentName", value);
            }
        }

        private byte[] attachment;

        /// <summary>
        /// 附件
        /// </summary>
        public byte[] Attachment
        {
            get
            {
                return attachment;
            }

            set
            {
                attachment = value;
                base.OnPropertyChanged("Attachment", value);
            }
        }

        private CommonData.MemoType type;

        /// <summary>
        /// 类型（系统、重要、正常等）
        /// </summary>
        [Required(CMessage = "类型",EMessage="Type")]
        public CommonData.MemoType Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
                base.OnPropertyChanged("Type", value);
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

    }

    public class MemoParam
    {
        public Guid OperationId { get; set; }
        public Guid FormID { get; set; }
        public ICP.Framework.CommonLibrary.Common.OperationType OperationType { get; set; }
        public ICP.Framework.CommonLibrary.Common.FormType FormType { get; set; }
    }
}
