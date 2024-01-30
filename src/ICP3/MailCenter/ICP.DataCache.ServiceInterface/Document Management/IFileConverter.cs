using System;
using System.Collections.Generic;

namespace ICP.DataCache.ServiceInterface.File
{
    /// <summary>
    /// 文档转换器接口
    /// </summary>
    public interface IFileConverter
    {
        FileType FileType { get; }
        String TempPath { get; }
        void Convert(String path);
        String FileOriginalName { get; set; }
        String FileNewPath { get; set; }
        List<String> FileExtensions { get; }
   
    }
}
