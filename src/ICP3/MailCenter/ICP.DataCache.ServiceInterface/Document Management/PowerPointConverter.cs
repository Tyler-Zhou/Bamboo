using System;
using System.Collections.Generic;

namespace ICP.DataCache.ServiceInterface.File
{
    public class PowerPointConverter : BaseFileConverter
    {

        public override FileType FileType
        {
            get { return FileType.PowerPoint; }
        }
        public override List<String> FileExtensions
        {
            get { return new List<String> { ".ppt", ".pptx" }; }
        }
        public override void Convert(String path)
        {
            base.Convert(path);

            FileNewPath = FileConvertService.ConvertPPT2PDF(path);
        }
    }
}
