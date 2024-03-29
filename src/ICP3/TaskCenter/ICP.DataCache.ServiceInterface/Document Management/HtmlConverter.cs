﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.DataCache.ServiceInterface1;

namespace ICP.DataCache.ServiceInterface1.File
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
            //CopyFile(path);
            FileNewPath = ConvertFileHelper.Current.ExportHtml2PDF(path);
        }
    }
}
