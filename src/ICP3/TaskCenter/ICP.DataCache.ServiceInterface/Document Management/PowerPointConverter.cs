using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.DataCache.ServiceInterface1;
using Microsoft.Office.Interop.PowerPoint;

namespace ICP.DataCache.ServiceInterface1.File
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
            //object missingValue = System.Reflection.Missing.Value;
            //object readOnly = (object)false;
            //ApplicationClass pptApplication = null;
            //System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            //try
            //{
            //    pptApplication = new ApplicationClass();
            //    Presentation doc = pptApplication.Presentations.Open(path, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoFalse);
            //    doc.SaveAs(FileNewPath, Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType.ppSaveAsHTML, Microsoft.Office.Core.MsoTriState.msoFalse);
            //    doc.Close();
            //}
            //finally
            //{
            //    if (pptApplication != null)
            //        pptApplication.Quit();
            //    System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
            //}

            FileNewPath = ConvertFileHelper.Current.ExportPPTtoPDF(path);
        }
    }
}
