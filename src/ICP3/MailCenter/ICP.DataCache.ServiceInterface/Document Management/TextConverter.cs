using System;
using System.Collections.Generic;

namespace ICP.DataCache.ServiceInterface.File
{
    /// <summary>
    /// Txt文件转换封装类
    /// </summary>
    public class TextConverter : BaseFileConverter
    {

        public override FileType FileType
        {
            get
            {
                return FileType.Text;
            }
        }
        public override List<String> FileExtensions
        {
            get { return new List<String> { ".txt" }; }
        }
        public override void Convert(String path)
        {
            base.Convert(path);
            FileNewPath = FileConvertService.ConvertText2PDF(path);
        }
    }
}
