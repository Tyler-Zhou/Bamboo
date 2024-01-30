using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.DataCache.ServiceInterface1;
using Microsoft.Office.Interop.Word;

namespace ICP.DataCache.ServiceInterface1.File
{
    public class WordConverter : BaseFileConverter
    {

        public override FileType FileType
        {
            get { return FileType.Word; }
        }

        public override List<String> FileExtensions
        {
            get
            {
                return new List<String> { ".doc", ".docx" };
            }
        }
        public override void Convert(String path)
        {
            base.Convert(path);
            //object missingValue = System.Reflection.Missing.Value;
            //object oldFileName = (object)path;
            //object newFileName = (object)FileNewPath;
            //object readOnly = false;
            //ApplicationClass wordApplication = null;
            //try
            //{   
            //    wordApplication = new ApplicationClass();  
            //    Document doc = wordApplication.Documents.Open(ref oldFileName, ref missingValue, ref readOnly,
            //        ref missingValue, ref missingValue, ref missingValue, ref missingValue, ref missingValue, ref missingValue, ref missingValue,
            //        ref missingValue, ref missingValue, ref missingValue, ref missingValue, ref missingValue, ref missingValue);             
            //    object fileType = (object)WdSaveFormat.wdFormatHTML;
            //    doc.SaveAs(ref newFileName, ref fileType,
            //        ref missingValue, ref missingValue, ref missingValue, ref missingValue, ref missingValue, ref missingValue, ref missingValue,
            //        ref missingValue, ref missingValue, ref missingValue, ref missingValue, ref missingValue, ref missingValue, ref missingValue);
            //    doc.Close(ref missingValue, ref missingValue, ref missingValue);
            //}
            //finally
            //{     
            //    if (wordApplication != null)
            //        wordApplication.Quit(ref readOnly, ref missingValue, ref missingValue);
            //}

            FileNewPath = ConvertFileHelper.Current.ExportWord2PDF(path);
        }
    }
}
