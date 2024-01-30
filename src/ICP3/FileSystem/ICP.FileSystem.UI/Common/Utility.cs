

using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ICP.FileSystem.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class FileSystemUIUtility
    {
        /// <summary>
        /// 验证文件名是否有重复
        /// </summary>
        /// <param name="sourceNames">源文件</param>
        /// <param name="validateNames">验证的文件名集合</param>
        /// <returns>True:重复 False:不重复</returns>
        public static bool ValidateFileNameDuplicate(List<string> sourceNames, string[] validateNames)
        {
            foreach (string fileName in validateNames)
            {
                string fileName2 = Path.GetFileName(fileName);
                if (sourceNames.Contains(fileName2))
                {

                    string errorMsg = LocalData.IsEnglish ? string.Format("Document :{0} exists.", fileName2) : string.Format("文档:{0}已经存在。", fileName2);
                    throw new ICPException(errorMsg);
                }
            }
            return false;
        }

        /// <summary>
        /// 弹出选择文件对话框
        /// </summary>
        /// <returns>选择的文件路径集合</returns>
        public static string[] SelectFilesToUpload()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.Filter = GetFilterString();
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string[] filePaths = dialog.FileNames;
                return filePaths;
            }
            return null;
        }

        /// <summary>
        /// 获取筛字符串
        /// </summary>
        /// <returns></returns>
        public static string GetFilterString()
        {
            return string.Format(LocalData.IsEnglish ? "Files{0}" : "文件{0}", GetFileExtensions());
        }
        /// <summary>
        /// 支持上传文件扩展名
        /// </summary>
        /// <returns></returns>
        public static string GetFileExtensions()
        {
            return "(*.txt,*.pdf,*.doc,*.docx,*.rtf,*.xls,*.xlsx,*.ppt,*.pptx,*.jpg,*.jpeg,*.gif,*.png,*.bmp,*.tif,*.tiff,*.msg,*.html,*.htm,*.mht)|*.txt;*.pdf;*.doc;*.docx;*.rtf;*.xls;*.xlsx;*.ppt;*.pptx;*.jpg;*.jpeg;*.gif;*.png;*.bmp;*.tif;*.msg;*.html;*.htm;*.mht";
        }

        /// <summary>
        /// 验证是否存在空路径
        /// </summary>
        /// <param name="filePaths">文件路径集合</param>
        /// <returns>True:存在 False:不存在</returns>
        private static bool ValidateFileEmtpyOrNull(IEnumerable<string> filePaths)
        {
            return filePaths.ToList().Any(string.IsNullOrEmpty);

        }

        /// <summary>
        /// 验证文件信息
        /// 1.路径是否为空
        /// 2.文件大小是否超出限制
        /// </summary>
        /// <param name="filePaths">文件路径集合</param>
        /// <returns>True:有效文件 False:无效文件</returns>
        public static bool ValidateFileInfo(string[] filePaths)
        {
            if (ValidateFileEmtpyOrNull(filePaths))
                return false;

            foreach (string filePath in filePaths)
            {
                FileInfo file = new FileInfo(filePath);
                string fileSize = CommonHelper.GetFileSizeString(file.Length);
                if (file.Length > (FileSystemUIConstants.FileMaxSize * 1024 * 1024))
                {
                    string errorTemplate = LocalData.IsEnglish ? "The size of file:{0} is {1},exceeds the limit {2}MB." : "文档：{0} 大小为{1},超过了限制({2}MB)。";
                    throw new ICPException(string.Format(errorTemplate, file.Name, fileSize, FileSystemUIConstants.FileMaxSize));
                }
            }
            return true;
        }

        /// <summary>
        /// 通过对字符的unicode编码进行判断来确定字符是否为中文.
        /// 在unicode 字符串中，中文的范围是在4E00..9FFF:
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns>返回是否含有中文</returns>
        public static bool IsContainsChinese(string input)
        {
            if (string.IsNullOrEmpty(input)) return false;

            int chfrom = Convert.ToInt32("4e00", 16);    //范围（0x4e00～0x9fff）转换成int（chfrom～chend）
            int chend = Convert.ToInt32("9fff", 16);


            for (int i = 0; i < input.Length; i++)
            {
                int code = Char.ConvertToUtf32(input, i);
                if (code >= chfrom && code <= chend)
                    return true;
            }

            return false;
        }
    }
}
