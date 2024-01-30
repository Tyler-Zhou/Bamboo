using System;
using System.Collections.Generic;

namespace ICP.DataCache.ServiceInterface.File
{
   public class PdfConverter:BaseFileConverter
    {
      
       public override FileType FileType
       {
           get
           {
               return FileType.Pdf;
           }
       }
       public override List<String> FileExtensions
       {
           get { return new List<String> {".pdf"}; }
       }
       public override void Convert(String path)
       {
           base.Convert(path);

           CopyFile(path);
       }
    }
}
