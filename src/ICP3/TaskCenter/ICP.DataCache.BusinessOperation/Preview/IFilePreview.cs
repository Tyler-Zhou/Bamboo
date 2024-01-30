using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.DataCache.BusinessOperation1.Preview
{  
    /// <summary>
    /// 文档预览控件接口
    /// </summary>
   public interface IFilePreview
    {
       List<String> FileExtensions { get; }
       //void Preview(Guid id);
       void Preview(String path);
    }
}
