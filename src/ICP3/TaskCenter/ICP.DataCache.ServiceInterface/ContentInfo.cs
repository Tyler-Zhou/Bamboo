using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.DataCache.ServiceInterface1
{   
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
   public class ContentInfo
    {  
        
        public String Name { get; set; }
        public Byte[] Content { get; set; }
        public Guid Id { get; set; }
        public UploadState UploadState { get; set; }
    }
}
