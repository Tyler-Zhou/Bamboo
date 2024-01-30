using ICP.FileSystem.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FileSystem.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientFileSystemService
    {
        IFileSystemService FileSystemService
        {
            get
            {
                return ServiceClient.GetService<IFileSystemService>();
            }
        }

        /// <summary>
        /// 上传OA文档
        /// </summary>
        /// <param name="documents"></param>
        public void UplaodCustomerDocument(DocumentInfo[] documents)
        {
            //FileSystemService.SaveDocumentToOADoc(documents);
        }
    }
}
