using System;
using System.Collections.Generic;
using ICP.DataCache.ServiceInterface1.File;
using System.IO;
using Microsoft.Office.Interop.Outlook;


namespace ICP.DataCache.ServiceInterface1.File
{
    public class MailConverter : BaseFileConverter
    {
        public override FileType FileType
        {
            get
            {
                return FileType.msg;
            }
        }

        public override List<String> FileExtensions
        {
            get { return new List<String> { ".msg" }; }
        }

        public override void Convert(String path)
        {
            base.Convert(path);
            CopyFile(path);
           // FileNewPath = ConvertFileHelper.Current.ExportMail2PDF(path);
        }
    }

}
