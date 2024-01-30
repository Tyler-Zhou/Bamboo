using ICP.Common.ServiceInterface;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.MailCenter.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ICP.FileSystem.ServiceInterface;

namespace ICP.Business.Common.UI
{
    /// <summary>
    /// UI工具类
    /// </summary>
    public class CommonUIUtility
    {
        #region Windows API
        [DllImport("kernel32.dll")]
        public static extern IntPtr _lopen(string lpPathName, int iReadWrite);
        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);
        public const int OF_READWRITE = 2;
        public const int OF_SHARE_DENY_NONE = 0x40;
        public static IntPtr HFILE_ERROR = new IntPtr(-1);
        #endregion

        #region Fields & Property & Services
        /// <summary>
        /// 是否英文环境
        /// </summary>
        private static bool isEnglish = LocalData.IsEnglish;
        /// <summary>
        /// 客户端文件上传服务
        /// </summary>
        private static IClientFileService clientFileService;
        /// <summary>
        /// 客户端文件上传服务
        /// </summary>
        public static IClientFileService ClientFileService
        {
            get
            {

                if (clientFileService == null)
                {
                    clientFileService = ServiceClient.GetService<IClientFileService>();
                }
                return clientFileService;
            }
        }
        /// <summary>
        /// Outlook 服务
        /// </summary>
        private static OutlookService OutlookService
        {
            get
            {
                ClientHelper.EnsureEmailCenterAppStarted();
                return new OutlookService();
            }
        }

        public static IFileSystemService FileServiceWCF
        {
            get
            {
                return ServiceClient.GetService<IFileSystemService>();
            }
        }


        #endregion
        /// <summary>
        /// 获取单号序号
        /// </summary>
        /// <param name="operationNo"></param>
        /// <returns></returns>
        public static string GetSerialNumber(string operationNo)
        {
            if (string.IsNullOrEmpty(operationNo))
            {
                return string.Empty;
            }
            if (operationNo.Length <= 4)
            {
                return (LocalData.IsEnglish ? ":" : "：") + operationNo;
            }
            return (LocalData.IsEnglish ? ":" : "：") + operationNo.Substring(operationNo.Length - 4, 4);
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

        /// <summary>
        /// 通过文档ID打开文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static void OpenFile(Guid id)
        {
            ContentInfo info = ClientFileService.GetDocumentContent(id);
            string filePath = DataCacheUtility.SaveFileContentToDisk(info);
            using (Process proc = Process.Start(filePath))
            {
                if (proc != null)
                    proc.Dispose();
            }
        }

        /// <summary>
        /// 通过文档ID打开文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <param name="dataSearchType">数据查询类型</param>
        public static void OpenFile(Guid id, DataSearchType dataSearchType)
        {
            ContentInfo info = ClientFileService.GetDocumentContent(id, dataSearchType);
            string filePath = DataCacheUtility.SaveFileContentToDisk(info);
            using (Process proc = Process.Start(filePath))
            {
                if (proc != null)
                    proc.Dispose();
            }
        }

        /// <summary>
        /// 根据文件id选择打开方式
        /// </summary>
        /// <param name="id"></param>
        public static void OpenWith(Guid id)
        {
            ContentInfo info = ClientFileService.GetDocumentContent(id);
            string filePath = DataCacheUtility.SaveFileContentToDisk(info);
            OpenWith(filePath);
        }

        /// <summary>
        /// 根据文件id选择打开方式
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dataSearchType">数据查询类型</param>
        public static void OpenWith(Guid id, DataSearchType dataSearchType)
        {
            ContentInfo info = ClientFileService.GetDocumentContent(id, dataSearchType);
            string filePath = DataCacheUtility.SaveFileContentToDisk(info);
            OpenWith(filePath);
        }

        /// <summary>
        /// 根据文件路径选择打开方式
        /// </summary>
        /// <param name="filePath">路径</param>
        public static void OpenFileByPath(string filePath)
        {
            using (Process proc = Process.Start(filePath))
            {
                if (proc != null)
                    proc.Dispose();
            }
        }

        /// <summary>
        /// 根据文件名选择打开方式
        /// </summary>
        /// <param name="filePath"></param>
        public static void OpenWith(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }
            using (Process proc = Process.Start("rundll32.exe", @"shell32,OpenAs_RunDLL " + filePath))
            {
                if (proc != null)
                {
                    proc.Dispose();
                }
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="document"></param>
        public static void DownLoad(DocumentInfo document)
        {
            string fileSavePath = GetFileSavePath(document);
            if (!string.IsNullOrEmpty(fileSavePath))
            {
                ContentInfo info = ClientFileService.GetDocumentContent(document.Id);


                //DocumentStream infosend = new DocumentStream();
                //infosend.Id = document.Id;
                //infosend.IsDownCopy = false;
                //DocumentStream inforeturn = FileServiceWCF.ServiceTransferFileToClint(infosend);


                DataCacheUtility.SaveFileContentToDisk(info, fileSavePath);
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="document"></param>
        /// <param name="dataSearchType"></param>
        public static void DownLoad(DocumentInfo document,DataSearchType dataSearchType)
        {
            string fileSavePath = GetFileSavePath(document);
            if (!string.IsNullOrEmpty(fileSavePath))
            {
                ContentInfo info = ClientFileService.GetDocumentContent(document.Id, dataSearchType);
                DataCacheUtility.SaveFileContentToDisk(info, fileSavePath);
            }
        }

        /// <summary>
        /// 打开保存文件对话框，获取文件保存路径，如果未选择，则返回空字符串
        /// </summary>
        /// <param name="document">文档信息</param>
        /// <returns></returns>
        public static string GetFileSavePath(DocumentInfo document)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.AddExtension = false;
            dialog.CheckPathExists = true;
            dialog.FileName = document.Name;
            dialog.OverwritePrompt = true;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;

            }
            else
                return string.Empty;

        }

        /// <summary>
        /// 初始化文档信息(构建文档对象)
        /// </summary>
        /// <param name="context">业务操作上下文</param>
        /// <param name="documentType">文档类型</param>
        /// <returns>文档对象</returns>
        public static DocumentInfo InitDocumentInfo(BusinessOperationContext context, DocumentType? documentType)
        {
            DocumentInfo info = new DocumentInfo();
            info.Id = Guid.NewGuid();
            info.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            info.CreateBy = LocalData.UserInfo.LoginID;
            info.CreateByName = LocalData.IsEnglish ? LocalData.UserInfo.LoginName : LocalData.UserInfo.UserName;
            info.OperationID = context.OperationID;
            info.Type = context.OperationType;
            info.FormType = context.FormType;
            info.DocumentType = documentType.HasValue ? documentType.Value : DocumentType.Other;
            info.UpdateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            info.State = UploadState.LocalProcessing;
            info.FileSources = FileSource.FDocument;
            return info;
        }
        /// <summary>
        /// 初始化文档信息(设置传入文档对象)
        /// </summary>
        /// <param name="document">文档对象</param>
        /// <param name="context">业务操作上下文</param>
        public static void InitDocumentInfo(DocumentInfo document, BusinessOperationContext context)
        {
            document.Id = Guid.NewGuid();
            document.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            document.CreateBy = LocalData.UserInfo.LoginID;
            document.CreateByName = LocalData.IsEnglish ? LocalData.UserInfo.LoginName : LocalData.UserInfo.UserName;
            document.OperationID = context.OperationID;
            document.Type = context.OperationType;
            document.FormType = context.FormType;
            document.UpdateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            document.State = UploadState.LocalProcessing;
            document.FileSources = FileSource.FDocument;
            if (!string.IsNullOrEmpty(document.OriginalPath) && document.OriginalPath.ToLower().EndsWith(".msg"))
            {
                document.Content = IOHelper.ReadFileContentFromDisk(document.OriginalPath);
            }
            if (document.HtmlContent == null && !string.IsNullOrEmpty(document.OriginalPath) && document.OriginalPath.ToLower().EndsWith(".msg"))
            {
                string pdfFilePath = OutlookService.ConvertMailToPDF(document.OriginalPath);
                document.HtmlContent = IOHelper.ReadFileContentFromDisk(pdfFilePath);
            }
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
                if (file.Length > (Constants.FileMaxSize * 1024 * 1024))
                {
                    string errorTemplate = isEnglish ? "The size of file:{0} is {1},exceeds the limit {2}MB." : "文档：{0} 大小为{1},超过了限制({2}MB)。";
                    //LocalCommonServices.ErrorTrace.SetErrorInfo(null, string.Format(errorTemplate, file.Name, fileSize, Constants.FileMaxSize));
                    throw new ICPException(string.Format(errorTemplate, file.Name, fileSize, Constants.FileMaxSize));
                    // return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 验证文件集合信息
        /// 1.路径是否为空
        /// 2.文件大小是否超出限制
        /// </summary>
        /// <param name="filePaths">文件路径集合</param>
        /// <returns>True:有效文件 False:无效文件</returns>
        public static bool ValidateFilesInfo(string[] filePaths)
        {
            if (ValidateFileEmtpyOrNull(filePaths))
                return false;
            Regex rx = new Regex(@"[0-9a-zA-Z_&#@() -]");
            foreach (string filePath in filePaths)
            {
                //0.验证文件名
                string filename = System.IO.Path.GetFileNameWithoutExtension(filePath);
                if (filename == null) continue;
                foreach (char charname in filename)
                {
                    if (!rx.IsMatch(charname.ToString()) && !IsContainsChinese(charname.ToString()))
                    {
                        throw new ICPException(isEnglish ? "The document name contains illegal characters，Please change the name and then upload it！" : "文档名称包含非法字符，请修改名称后重新上传！");
                    }
                }
                FileInfo file = new FileInfo(filePath);
                //1.验证文件大小
                string fileSize = CommonHelper.GetFileSizeString(file.Length);
                if (file.Length > (Constants.FileMaxSize * 1024 * 1024))
                {
                    string errorTemplate = isEnglish ? "The size of file:{0} is {1},exceeds the limit {2}MB." : "文档：{0} 大小为{1},超过了限制({2}MB)。";
                    throw new ICPException(string.Format(errorTemplate, file.Name, fileSize, Constants.FileMaxSize));
                }
            }
            return true;
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
        /// 验证文件名是否有重复
        /// </summary>
        /// <param name="sourceNames">源文件</param>
        /// <param name="validateNames">验证的文件名集合</param>
        /// <returns>True:重复 False:不重复</returns>
        public static bool ValidateFileNameDuplicate(List<string> sourceNames, String[] validateNames)
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
        /// 可上传文件扩展名集合
        /// </summary>
        /// <returns>扩展名集合</returns>
        public static string[] FilterFilesExtension()
        {

            string[] extensions = new string[] { ".txt", ".pdf", ".doc", ".docx", ".rtf", ".xls", ".xlsx", ".ppt", ".pptx", ".jpg", ".jpeg", ".gif", ".png", ".tif", ".tiff", ".bmp", ".msg", ".html", ".htm", ".mht" };
            string[] upperExtensions = (from item in extensions
                                        select item.ToUpper()).ToArray();
            return extensions.Union(upperExtensions).ToArray();
        }
        /// <summary>
        /// 验证文件是否被占用
        /// </summary>
        /// <param name="filePaths">文件路径集合</param>
        /// <returns>True:未被占用 False:抛出文件被占用异常</returns>
        public static bool ValidateFileInUse(string[] filePaths)
        {
            foreach (string fileName in filePaths)
            {
                IntPtr vHandle = _lopen(fileName, OF_READWRITE | OF_SHARE_DENY_NONE);
                if (vHandle == HFILE_ERROR)
                {
                    string errorTemplate = isEnglish ? "The file:{0} is being used by another application." : "文档：{0} 正被其他程序打开或占用。";
                    throw new ICPException(string.Format(errorTemplate, Path.GetFileName(fileName)));
                }
                CloseHandle(vHandle);
            }
            return true;
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
                String[] filePaths = dialog.FileNames;
                //CommonUIUtility.ValidateFileInfo(filePaths);
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
            return string.Format(isEnglish ? "Business Files{0}" : "业务文件{0}", GetFileExtensions());
        }
        /// <summary>
        /// 支持上传文件扩展名
        /// </summary>
        /// <returns></returns>
        public static string GetFileExtensions()
        {
            return "(*.txt,*.pdf,*.doc,*.docx,*.rtf,*.xls,*.xlsx,*.ppt,*.pptx,*.jpg,*.jpeg,*.gif,*.png,*.bmp,*.tif,*.tiff,*.msg,*.html,*.htm,*.mht)|*.txt;*.pdf;*.doc;*.docx;*.rtf;*.xls;*.xlsx;*.ppt;*.pptx;*.jpg;*.jpeg;*.gif;*.png;*.bmp;*.tif;*.msg;*.html;*.htm;*.mht";
        }
    }

}
