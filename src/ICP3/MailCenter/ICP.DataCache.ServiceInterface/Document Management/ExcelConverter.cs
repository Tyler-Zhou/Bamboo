using System;
using System.Collections.Generic;


namespace ICP.DataCache.ServiceInterface.File
{
    public class ExcelConverter : BaseFileConverter
    {

        public override FileType FileType
        {
            get
            {
                return FileType.Excel;
            }

        }
        public override List<String> FileExtensions
        {
            get
            {
                return new List<String> { ".xls", ".xlsx" };
            }
        }
        public override void Convert(String path)
        {
            base.Convert(path);
            FileNewPath = FileConvertService.ConvertExcel2PDF(path);
        }

    }
}
