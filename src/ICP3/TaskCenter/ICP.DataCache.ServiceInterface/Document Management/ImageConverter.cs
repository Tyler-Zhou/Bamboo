using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.DataCache.ServiceInterface1;

namespace ICP.DataCache.ServiceInterface1.File
{
    public class ImageConverter : BaseFileConverter
    {

        public override FileType FileType
        {
            get
            {
                return FileType.Image;
            }
        }
        public override List<String> FileExtensions
        {
            get { return new List<String> { ".png", ".bmp", ".jpeg", ".gif", ".jpg", ".png", ".tif" }; }
        }
        public override void Convert(String path)
        {
            base.Convert(path);
            FileNewPath = ConvertFileHelper.Current.ExportImage2PDF(path);
        }
    }
}
