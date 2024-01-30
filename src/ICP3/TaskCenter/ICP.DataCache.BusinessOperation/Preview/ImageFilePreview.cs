using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace ICP.DataCache.BusinessOperation1.Preview
{   
    /// <summary>
    /// 图片预览控件
    /// </summary>
    [ToolboxItem(false)]
    public sealed class ImageFilePreview : PictureBox,IFilePreview
    {
        public ImageFilePreview()
        {
            SizeMode = PictureBoxSizeMode.CenterImage;
        }
        public  void Preview(String path)
        {
           Image =Image.FromFile(path);
        }

        public  List<String> FileExtensions
        {
            get { return new List<String> { ".bmp", ".jpeg", ".gif", ".jpg", ".png" }; }
        }

        #region IFilePreview 成员


        public void Preview(Guid id)
        {
           String filePath= BusinessServices.ClientFileService.SaveHtmlContentToDisk(id);
           Preview(filePath);
        }

        protected override void Dispose(bool disposing)
        {
            if (this.Image != null)
            {
                this.Image.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
