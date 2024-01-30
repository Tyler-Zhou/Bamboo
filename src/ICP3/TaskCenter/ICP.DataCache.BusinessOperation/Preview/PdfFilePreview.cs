using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ICP.DataCache.BusinessOperation1.Preview
{ 
    /// <summary>
    /// PDF文档预览控件
    /// </summary>
    [ToolboxItem(false)]
    public sealed class PdfFilePreview : AxAcroPDFLib.AxAcroPDF,IFilePreview
    {
        
        public  void Preview(String path)
        {
            try
            {
                this.src = path;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public  List<String> FileExtensions
        {
            get { return new List<String> { ".pdf" }; }
        }


        #region IFilePreview 成员


        public void Preview(Guid id)
        {
            String filePath = BusinessServices.ClientFileService.SaveHtmlContentToDisk(id);
            Preview(filePath);
        }

        #endregion
     
    }
}
