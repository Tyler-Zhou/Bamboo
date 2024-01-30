using System;
using System.Runtime.Serialization;

namespace ICP.DataSynchronization.ServiceInterface
{   
    /// <summary>
    /// 数据同步异常信息实体类
    /// </summary>
    [DataContract]
    public class DataSyncFaultException 
    {
        public String message;
        public Exception innerException;

        public DataSyncFaultException(String message, Exception innerException) 
        {
            this.message = message;
            this.innerException = innerException;
        }

        [DataMember]
        public String Message
        {
            get
            {
                return this.message;
            }

            set
            {
                this.message = value;
            }
        }

        [DataMember]
        public Exception InnerException
        {
            get
            {
                return this.innerException;
            }

            set
            {
                this.innerException = value;
            }
        }
    }
}
