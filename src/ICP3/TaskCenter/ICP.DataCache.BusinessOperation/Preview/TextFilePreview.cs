using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.ComponentModel;


namespace ICP.DataCache.BusinessOperation1.Preview
{ 
    /// <summary>
    /// 文本文件预览控件
    /// </summary>
    [ToolboxItem(false)]
   public sealed class TextFilePreview:RichTextBox,IFilePreview
    {
       public TextFilePreview()
       {
           this.Multiline = this.ReadOnly = true;
           
       }
       public  void Preview(String path)
       {

           this.Text = File.ReadAllText(path, Encoding.UTF8);
       }
        public  List<String> FileExtensions
        {
            get { return new List<String> { ".txt" }; }
        }

        #region IFilePreview 成员

        public void Preview(Guid id)
        {
            String filePath = BusinessServices.ClientFileService.SaveHtmlContentToDisk(id);
            Preview(filePath);
        }
        protected override void Dispose(bool disposing)
        {
            this.Text = null;
            base.Dispose(disposing);
        }
        #endregion
    }
}
