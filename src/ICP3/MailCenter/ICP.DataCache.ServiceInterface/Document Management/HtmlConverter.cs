using System;
using System.Collections.Generic;

namespace ICP.DataCache.ServiceInterface.File
{
    public class HtmlConverter : BaseFileConverter
    {

        public override FileType FileType
        {
            get
            {

                return FileType.Html;
            }
        }
        public override List<String> FileExtensions
        {
            get { return new List<String> { ".html", ".xhtml", ".mhtml", ".htm", ".mht" }; }
        }
        public override void Convert(String path)
        {
            base.Convert(path);
            FileNewPath = FileConvertService.ConvertHtml2PDF(path);
        }
    }
}
