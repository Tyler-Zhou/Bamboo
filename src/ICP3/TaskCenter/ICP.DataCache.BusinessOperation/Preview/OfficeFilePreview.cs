using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.ComponentModel;

namespace ICP.DataCache.BusinessOperation1.Preview
{ 
    /// <summary>
    /// office和html文件查看器
    /// </summary>
    [ToolboxItem(false)]
  public sealed  class OfficeFilePreview:WebBrowser,IFilePreview
    {
      
        public  void Preview(String htmlFilePath)
        {
          
            //this.Url = new Uri(htmlFilePath);
            this.Navigate(htmlFilePath);
        }

        public  List<String> FileExtensions
        {
            get { return new List<String> { ".xls", ".xlsx", ".html", ".xhtml", ".mhtml", ".ppt", ".pptx", ".doc", ".docx" }; }
        }

        #region IFilePreview 成员


        public void Preview(Guid id)
        {
           String filePath= BusinessServices.ClientFileService.SaveHtmlContentToDisk(id);
           Preview(filePath);
        }
    

        #endregion
    }
}
