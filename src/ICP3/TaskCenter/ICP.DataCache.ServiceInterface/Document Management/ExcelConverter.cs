using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.DataCache.ServiceInterface1;
using Microsoft.Office.Interop.Excel;

namespace ICP.DataCache.ServiceInterface1.File
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
            //object missingValue = System.Reflection.Missing.Value;
            //object saveChanges=false;
            //object readOnly = false;
            //ApplicationClass excelApplication = null;
            //System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            //object fileType = (object)Microsoft.Office.Interop.Excel.XlFileFormat.xlHtml; 
            //try
            //{
            //    excelApplication = new ApplicationClass();
            //    Workbook doc = excelApplication.Workbooks.Open(path, missingValue, readOnly,missingValue, missingValue, missingValue, missingValue, missingValue, missingValue, missingValue,missingValue, missingValue, missingValue, missingValue, missingValue);                    
            //    doc.SaveAs(FileNewPath, fileType,missingValue, missingValue, missingValue, missingValue, XlSaveAsAccessMode.xlExclusive, missingValue, missingValue, missingValue,missingValue, missingValue);
            //    doc.Close(saveChanges, missingValue, missingValue);
            //}
            //finally
            //{
            //    if (excelApplication != null)
            //        excelApplication.Quit();
            //    System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
            //}
            FileNewPath = ConvertFileHelper.Current.ExportExcel2PDF(path);
        }

    }
}
