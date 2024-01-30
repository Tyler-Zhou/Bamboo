using System;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.DataCache.ServiceInterface1
{
    /// <summary>
    /// 文档实体类
    /// </summary>
    [Serializable]
    public class DocumentInfo : IEquatable<DocumentInfo>
    {
        /// <summary>
        ///文档Id
        /// </summary>
        public Guid Id { get; set; }


        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID
        {
            get;
            set;
        }
        /// <summary>
        /// 业务类型
        /// </summary>
        public OperationType Type
        {
            get;
            set;
        }
        /// <summary>
        /// 文档类型
        /// </summary>
        public DocumentType DocumentType
        {
            get;
            set;
        }

        // joe 2013-05-27 添加，
        // 原因：为解决各种业务使用同样的上传功能

        /// <summary>
        /// 文档类型值对应于DocumentType的int值
        /// </summary>
        public int DocumentTypeValue
        {
            get;
            set;
        }
        public FormType FormType
        {
            get;
            set;
        }
        /// <summary>
        /// 文档状态
        /// </summary>
        public DocumentState DocumentState
        {
            get;
            set;
        }

        /// <summary>
        /// 文件名
        /// </summary>
        public String Name
        {
            get;
            set;
        }
        /// <summary>
        /// 上传人
        /// </summary>
        public String CreateByName
        {
            get;
            set;
        }
        public Guid CreateBy
        {
            get;
            set;
        }

        /// <summary>
        /// 文档是否选择
        /// </summary>
        public bool Selected
        {
            get;
            set;
        }

        /// <summary>
        /// 上传日期
        /// </summary>
        public DateTime CreateDate
        {
            get;
            set;
        }
        public Guid? UpdateBy
        {
            get;
            set;
        }
        public string UpdateByName
        {
            get;
            set;
        }
        public DateTime? UpdateDate
        {
            get;
            set;
        }
        /// <summary>
        /// 文档类型名称
        /// </summary>
        public String DocumentTypeName
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public String Remark
        {
            get;
            set;
        }
        /// <summary>
        /// 上传文档时文档初始所在路径
        /// <remarks>构建业务文档列表时添加</remarks>
        /// </summary>
        public string OriginalPath
        {
            get;
            set;
        }

        /// <summary>
        /// 文件来源
        /// </summary>
        FileSource source = FileSource.FDocument;
        public FileSource FileSources
        {
            get { return source; }
            set { source = value; }
        }

        public Byte[] HtmlContent { get; set; }
        public Byte[] Content { get; set; }
        UploadState state = UploadState.Successed;
        /// <summary>
        /// 上传状态
        /// </summary>
        public UploadState State
        {
            get { return this.state; }
            set { this.state = value; }
        }


        #region IEquatable<DocumentInfo> 成员

        public bool Equals(DocumentInfo other)
        {
            if (other == null)
                return false;
            if (this.Id == other.Id)
                return true;
            else
                return false;

        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion


    }
}
