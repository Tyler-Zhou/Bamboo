using System.Collections.Generic;
using ICP.FileSystem.ServiceInterface;

namespace ICP.Business.Common.UI.Document
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDocumentInfoDataSource
    {
        /// <summary>
        /// 
        /// </summary>
        DocumentInfo CurrentDocument { get; }
        /// <summary>
        /// 
        /// </summary>
        List<DocumentInfo> DataSource { get; set; }
    }
}
