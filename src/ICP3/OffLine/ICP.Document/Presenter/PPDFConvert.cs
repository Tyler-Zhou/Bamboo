#region Comment

/*
 * 
 * FileName:    PPDFConvert.cs
 * CreatedOn:   2014/5/19 星期一 16:42:00
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->PDF预览逻辑处理类
 *      ->1.ConvertFile:转换文件至PDF格式并加载文件到
 *      ->2.ConvertFileToPDF:转换文件至PDF格式
 * History：
 * 
 * 
 * 
 * 
 */

#endregion

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ICP.Document
{
    /// <summary>
    /// PDF预览逻辑处理类
    /// </summary>
    public class PPDFConvert : Presenter<IVPDFConvert>
    {
        /// <summary>
        /// 转换文件帮助类
        /// </summary>
        PDFConvert pdfConvert = null;

        public PPDFConvert(IVPDFConvert view)
            : base(view)
        {
            pdfConvert = new PDFConvert();
        }

        protected override void OnViewSet()
        {
            #region 转换文件
            this.View.ConvertFile += (sender, args) =>
                {
                    string targetFile = ConvertFileToPDF(new string[] { args.FilePath });
                    this.View.ControlLoadFile(targetFile);
                }; 
            #endregion
        }

        #region 转换文件到PDF
        /// <summary>
        /// 转换文件到PDF
        /// </summary>
        /// <param name="files">文件名称集合</param>
        /// <returns>路径</returns>
        private string ConvertFileToPDF(String[] files)
        {
            List<string> pdfFiles = new List<string>();
            foreach (String file in files)
            {
                if (!String.IsNullOrEmpty(file))
                {
                    string pdfFile = string.Empty;
                    String fileExtension = IOHelper.GetFileExtension(file);
                    switch (fileExtension)
                    {
                        case ".doc":
                        case ".docx":
                        case ".docm":
                            pdfFile = pdfConvert.FromWord(file);
                            break;
                        case ".xls":
                        case ".xlsx":
                        case ".xlsm":
                        case ".xltx":
                        case ".xlam":
                        case ".xlsb":
                        case ".xltm":
                            pdfFile = pdfConvert.FromExcel(file);
                            break;
                        case ".pdf":
                        case ".pdfxml":
                            pdfFile = file;
                            break;
                        case ".jpg":
                        case ".png":
                        case ".bmp":
                        case ".tif":
                        case ".jpe":
                        case ".jfif":
                        case ".gif":
                        case ".dib":
                        case ".jpeg":
                            pdfFile = pdfConvert.FromImage(file);
                            break;
                        case ".txt":
                        case ".log":
                        case ".scp":
                        case ".ps1":
                        case ".ps1xml":
                            pdfFile = pdfConvert.FromText(file);
                            break;
                        case ".pptx":
                        case ".ppt":
                            pdfFile = pdfConvert.FromPPT(file);
                            break;
                        default:
                            MessageBox.Show("Don't support the current file type."
                                , "System Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                    }
                    pdfFiles.Add(pdfFile);


                }
            }
            string targetFilePath = pdfConvert.MergePDFFiles(pdfFiles);
            return targetFilePath;

        } 
        #endregion
    }
}
