using System;

namespace ICP.DataSynchronization.ServiceInterface
{   
    /// <summary>
    /// 邮件附档类
    /// </summary>
    [Serializable]
    public sealed class Attachment
    {
        public String Name { get; set; }
        public Byte[] Content { get; set; }
    }
}
