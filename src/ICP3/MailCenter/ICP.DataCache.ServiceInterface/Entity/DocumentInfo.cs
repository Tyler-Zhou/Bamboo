////using System;
////using System.ServiceModel;
////using ICP.Framework.CommonLibrary.Common;

////namespace ICP.DataCache.ServiceInterface
////{
////    /// <summary>
////    /// 文档实体类
////    /// </summary>
////    [Serializable]
////    [MessageContract]
////    public class DocumentInfo : IEquatable<DocumentInfo>
////    {
////        /// <summary>
////        /// 空实例
////        /// </summary>
////        public DocumentInfo()
////        {
////            IsDirty = false;
////        }
////        /// <summary>
////        /// 备注: 设置DocumentTypeValue,方便过滤数据
////        /// </summary>
////        /// <param name="documentTypeValue"></param>
////        public DocumentInfo(int documentTypeValue)
////        {
////            IsDirty = false;
////            DocumentTypeValue = documentTypeValue;
////        }

////        /// <summary>
////        ///文档Id
////        /// </summary>
////        [MessageHeader(MustUnderstand = true)]
////        public Guid Id { get; set; }

////        /// <summary>
////        /// 从数据库取出来的文档IsDirty=false，用户电脑上上传的文档IsDirty=true
////        /// </summary>
////        [MessageHeader(MustUnderstand = true)]
////        public bool IsDirty { get; set; }

////        /// <summary>
////        /// 业务ID
////        /// </summary>
////        [MessageHeader(MustUnderstand = true)]
////        public Guid OperationID
////        {
////            get;
////            set;
////        }

////        /// <summary>
////        /// oa.IMessage.ID
////        /// </summary>
////        [MessageHeader(MustUnderstand = true)]
////        public Guid IMessageID
////        {
////            get;
////            set;
////        }

////        /// <summary>
////        /// 业务类型
////        /// </summary>
////        [MessageHeader(MustUnderstand = true)]
////        public OperationType Type
////        {
////            get;
////            set;
////        }

////        /// <summary>
////        /// 文档类型
////        /// </summary>
////        [MessageHeader(MustUnderstand = true)]
////        public DocumentType? DocumentType
////        {
////            get;
////            set;
////        }

////        // joe 2013-05-27 添加，
////        // 原因：为解决各种业务使用同样的上传功能

////        /// <summary>
////        /// 文档类型值对应于DocumentType的int值
////        /// </summary>
////        [MessageHeader(MustUnderstand = true)]
////        public int DocumentTypeValue
////        {
////            get;
////            set;
////        }

////        [MessageHeader(MustUnderstand = true)]
////        public FormType FormType
////        {
////            get;
////            set;
////        }

////        /// <summary>
////        /// 关联HBLID
////        /// </summary>
////        [MessageHeader(MustUnderstand = true)]
////        public Guid? FormId
////        {
////            get;
////            set;
////        }

////        /// <summary>
////        /// 文档状态
////        /// </summary>
////        [MessageHeader(MustUnderstand = true)]
////        public DocumentState DocumentState
////        {
////            get;
////            set;
////        }

////        /// <summary>
////        /// 文件名
////        /// </summary>
////        [MessageHeader(MustUnderstand = true)]
////        public String Name
////        {
////            get;
////            set;
////        }

////        /// <summary>
////        /// 上传人
////        /// </summary>
////        [MessageHeader(MustUnderstand = true)]
////        public String CreateByName
////        {
////            get;
////            set;
////        }
////        /// <summary>
////        /// 创建者
////        /// </summary>
////        [MessageHeader(MustUnderstand = true)]
////        public Guid CreateBy
////        {
////            get;
////            set;
////        }

////        /// <summary>
////        /// 文档是否选择
////        /// </summary>
////        [MessageHeader(MustUnderstand = true)]
////        public bool Selected
////        {
////            get;
////            set;
////        }

////        /// <summary>
////        /// 上传日期
////        /// </summary>
////        [MessageHeader(MustUnderstand = true)]
////        public DateTime CreateDate
////        {
////            get;
////            set;
////        }
////        [MessageHeader(MustUnderstand = true)]
////        public Guid? UpdateBy
////        {
////            get;
////            set;
////        }
////        [MessageHeader(MustUnderstand = true)]
////        public string UpdateByName
////        {
////            get;
////            set;
////        }
////        [MessageHeader(MustUnderstand = true)]
////        public DateTime? UpdateDate
////        {
////            get;
////            set;
////        }

////        /// <summary>
////        /// 文档类型名称
////        /// </summary>
////        [MessageHeader(MustUnderstand = true)]
////        public String DocumentTypeName
////        {
////            get;
////            set;
////        }

////        /// <summary>
////        /// 备注
////        /// </summary>
////        [MessageHeader(MustUnderstand = true)]
////        public String Remark
////        {
////            get;
////            set;
////        }

////        /// <summary>
////        /// 上传文档时文档初始所在路径
////        /// <remarks>构建业务文档列表时添加</remarks>
////        /// </summary>
////        [MessageHeader(MustUnderstand = true)]
////        public string OriginalPath
////        {
////            get;
////            set;
////        }

////        /// <summary>
////        /// 文件预览路径
////        /// </summary>
////        [MessageHeader(MustUnderstand = true)]
////        public string PreviewPath { get; set; }

////        /// <summary>
////        /// 文件来源
////        /// </summary>
////        FileSource source = FileSource.FDocument;

////        public FileSource FileSources
////        {
////            get { return source; }
////            set { source = value; }
////        }
////        /// <summary>
////        /// 
////        /// </summary>
////        //[MessageHeader(MustUnderstand = true)]
////        public Byte[] HtmlContent { get; set; }

////        /// <summary>
////        /// 
////        /// </summary>
////        //[MessageHeader(MustUnderstand = true)]
////        public Byte[] Content { get; set; }

////        UploadState state = UploadState.Successed;
////        /// <summary>
////        /// 上传状态
////        /// </summary>
////        public UploadState State
////        {
////            get { return state; }
////            set { state = value; }
////        }
////        #region IEquatable<DocumentInfo> 成员

////        public bool Equals(DocumentInfo other)
////        {
////            if (other == null)
////                return false;
////            if (Id == other.Id)
////                return true;
////            else
////                return false;
////        }
////        public override int GetHashCode()
////        {
////            return Id.GetHashCode();
////        }
////        #endregion

////        #region NRAS文档成员
////        /// <summary>
////        /// 客户ID
////        /// </summary>
////        [MessageHeader(MustUnderstand = true)]
////        public Guid CustomerID
////        {
////            get;
////            set;
////        }
////        /// <summary>
////        /// 客户
////        /// </summary>
////        [MessageHeader(MustUnderstand = true)]
////        public string CustomerName
////        {
////            get;
////            set;
////        }
////        /// <summary>
////        /// 有效期
////        /// </summary>
////        [MessageHeader(MustUnderstand = true)]
////        public DateTime ValidityDate
////        {
////            get;
////            set;
////        }
////        #endregion
////    }
////}
