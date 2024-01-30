using System;
using System.Collections.Generic;

namespace ICP.DataCache.ServiceInterface.File
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
            get { return new List<String> { ".png", ".bmp", ".jpeg", ".gif", ".jpg", ".png", ".tif", ".tiff" }; }
        }
        public override void Convert(String path)
        {
            base.Convert(path);
            FileNewPath = FileConvertService.ConvertImage2PDF(path);
        }
    }
}
