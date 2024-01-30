using System;
using System.Collections.Generic;

namespace ICP.DataCache.ServiceInterface.File
{
    /// <summary>
    /// Word文件转换封装类
    /// </summary>
    public class WordConverter : BaseFileConverter
    {
        /// <summary>
        /// 文档类型
        /// </summary>
        public override FileType FileType
        {
            get { return FileType.Word; }
        }

        /// <summary>
        /// 文件扩展名集合
        /// </summary>
        public override List<String> FileExtensions
        {
            get
            {
                return new List<String> { ".doc", ".docx", ".rtf" };
            }
        }

        /// <summary>
        /// 传入指定文件路径转换为PDF文件
        /// </summary>
        /// <param name="path">指定文件路径</param>
        public override void Convert(String path)
        {
            base.Convert(path);

            FileNewPath = FileConvertService.ConvertWord2PDF(path);
        }
    }
}
