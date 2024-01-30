namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    using System;
    using System.Collections.Generic;
    using System.Text; 
    using ICP.Framework.CommonLibrary.Common;
    using CommonData = ICP.Common.ServiceInterface.DataObjects;

    /// <summary>
    /// 单证日志列表
    /// </summary>
    [Serializable]
    public partial class CommonDocumentList : BaseDataObject
    {
        /// <summary>
        /// IsNew
        /// </summary>
        public override bool IsNew { get { return id == Guid.Empty; } }
        private Guid id;

        /// <summary>
        /// 唯一键
        /// </summary>
        [Required(CMessage = "ID",EMessage="ID")]
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

        private Guid oceanbookingid;

        /// <summary>
        /// 托运单ID
        /// </summary>
        public Guid OceanBookingID
        {
            get
            {
                return oceanbookingid;
            }

            set
            {
                oceanbookingid = value;
                base.OnPropertyChanged("OceanBookingID", value);
            }
        }

        private string documentno;

        /// <summary>
        /// 单证号
        /// </summary>
        [StringLength(MaximumLength =20,CMessage="单证号",EMessage="DocumentNo")]
        public string DocumentNo
        {
            get
            {
                return documentno;
            }

            set
            {
                documentno = value;
                base.OnPropertyChanged("DocumentNo", value);
            }
        }

        private DocumentType documenttype;

        /// <summary>
        /// 类型（0核销单，1提单等）
        /// </summary>
        [Required(CMessage = "类型",EMessage="Type")]
        public DocumentType DocumentType
        {
            get
            {
                return documenttype;
            }

            set
            {
                documenttype = value;
                base.OnPropertyChanged("DocumentType", value);
            }
        }

        private short numberoforiginal;

        /// <summary>
        /// 正本份数
        /// </summary>
        [Required(CMessage = "正本份数",EMessage="NumberOfOriginal")]
        public short NumberOfOriginal
        {
            get
            {
                return numberoforiginal;
            }
            set
            {
                numberoforiginal = value;
                base.OnPropertyChanged("NumberOfOriginal", value);
            }
        }

        private short numberofcopies;

        /// <summary>
        /// 副本份数
        /// </summary>
        [Required(CMessage = "副本份数", EMessage = "NumberOfCopies")]
        public short NumberOfCopies
        {
            get
            {
                return numberofcopies;
            }

            set
            {
                numberofcopies = value;
                base.OnPropertyChanged("NumberOfCopies", value);
            }
        }

        private DateTime receiveddate;

        /// <summary>
        /// 接收日期
        /// </summary>
        [Required(ErrorMessage = "接收日期",EMessage="ReceivedDate")]
        public DateTime ReceivedDate
        {
            get
            {
                return receiveddate;
            }

            set
            {
                receiveddate = value;
                base.OnPropertyChanged("ReceivedDate", value);
            }
        }

        private DateTime? releasedate;

        /// <summary>
        /// 放单日期
        /// </summary>
        public DateTime? ReleaseDate
        {
            get
            {
                return releasedate;
            }

            set
            {
                releasedate = value;
                base.OnPropertyChanged("ReleaseDate", value);
            }
        }

        private DocumentReleaseMode releasemode;

        /// <summary>
        /// 放单方式（0快递，1当面交接）
        /// </summary>
        public DocumentReleaseMode ReleaseMode
        {
            get
            {
                return releasemode;
            }

            set
            {
                releasemode = value;
                base.OnPropertyChanged("ReleaseMode", value);
            }
        }

        private DateTime? returndate;

        /// <summary>
        /// 退回日期
        /// </summary>
        public DateTime? ReturnDate
        {
            get
            {
                return returndate;
            }

            set
            {
                returndate = value;
                base.OnPropertyChanged("ReturnDate", value);
            }
        }

        private string trackingno;

        /// <summary>
        /// 快递单号
        /// </summary>
        public string TrackingNo
        {
            get
            {
                return trackingno;
            }

            set
            {
                trackingno = value;
                base.OnPropertyChanged("TrackingNo", value);
            }
        }

        private string attachmentname;

        /// <summary>
        /// 附件名称
        /// </summary>
        [StringLength(MaximumLength=100,CMessage="附件名称",EMessage="AttachmentName")]
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

        private string remark;

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(MaximumLength=500,CMessage="备注",EMessage="Remark")]
        public string Remark
        {
            get
            {
                return remark;
            }

            set
            {
                remark = value;
                base.OnPropertyChanged("Remark", value);
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
        /// 备注列表
        /// </summary>
        public List<CommonMemoList> Memos { get; set; }

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
            CommonDocumentList newObj = obj as CommonDocumentList;
            if (newObj == null)
            {
                return false;
            }

            return newObj.ID == this.ID;
        }
    }

    /// <summary>
    /// 单证日志信息
    /// </summary>
    [Serializable]
    public partial class CommonDocumentInfo : CommonDocumentList
    {
    }
}
