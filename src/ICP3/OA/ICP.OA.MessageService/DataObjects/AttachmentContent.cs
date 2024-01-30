using System;

namespace ICP.Message.ServiceInterface
{
    /// <summary>
    /// 消息附件类
    /// </summary>
    [Serializable]
    public class AttachmentContent
    {
        public AttachmentContent()
        {
            Id = Guid.NewGuid();
            UploadTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
        }
        public Guid Id { get; set; }
        public String Name { get; set; }
        public string DisplayName { get; set; }
        public Byte[] Content { get; set; }
        public long Size { get; set; }
        public DateTime UploadTime { get; set; }
        /// <summary>
        /// 保存到数据库映射客户端的路径（如果文件正在被预览，保存文件时就会出现”文件正在被使用“的错误）
        /// </summary>
        public String ClientPath { get; set; }
        /// <summary>
        /// 预览文件映射客户端路径
        /// </summary>
        public string PreviewPath { get; set; }

        /// <summary>
        /// outlook附件对象，为了保存附件到本地
        /// </summary>
        public object OLAttachment { get; set; }

    }
}
