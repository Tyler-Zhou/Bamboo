using ICP.Framework.CommonLibrary.Common;
using System;
using System.IO;
using System.ServiceModel;

namespace ICP.FileSystem.ServiceInterface
{
    /// <summary>
    /// 文档实体类
    /// </summary>
    [Serializable]
    [MessageContract]
    public class DocumentStream//: IEquatable<DocumentStream>
    {
        /// <summary>
        /// 设置默认值
        /// </summary>
        public DocumentStream()
        {
            DataSearchTypeCode = DataSearchType.Local;
        }
        /// <summary>
        ///文档Id
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public Guid Id { get; set; }
        
        /// <summary>
        ///拆文件第一段文件流大小
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public int FirstFileStreamSize { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public String Name { get; set; }
        /// <summary>
        /// 文档类型
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public DocumentType? TypeCode { get; set; }
        /// <summary>
        /// 数据查询类型
        /// </summary>
        [MessageHeader(MustUnderstand = false)]
        public DataSearchType DataSearchTypeCode { get; set; }
        /// <summary>
        /// 是否下载复件
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public bool IsDownCopy { get; set; }
        /// <summary>
        /// 操作ID
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public Guid OperationID { get; set; }
        /// <summary>
        /// 文件流
        /// </summary>
        [MessageBodyMember(Order = 1)]
        public Stream Content { get; set; }
        /// <summary>
        /// 是否包含PDF附件
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public bool IncludePDF { get; set; }
    }
}
