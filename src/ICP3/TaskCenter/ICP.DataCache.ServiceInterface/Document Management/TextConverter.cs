using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.DataCache.ServiceInterface1;
namespace ICP.DataCache.ServiceInterface1.File
{
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
            FileNewPath = ConvertFileHelper.Current.ExportText2PDF(path);
        }
    }
}
