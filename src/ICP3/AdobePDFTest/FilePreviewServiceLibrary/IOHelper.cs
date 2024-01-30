using System;
using System.IO;

namespace ICP.FilePreviewServiceLibrary
{
    /// <summary>
    /// 文件和文件夹辅助类
    /// </summary>
    public class IOHelper
    {
        static IOHelper()
        {
            if (!Directory.Exists(HtmlTempPath))
            {
                Directory.CreateDirectory(HtmlTempPath);
            }
            if (!Directory.Exists(ContentTempPath))
            {
                Directory.CreateDirectory(ContentTempPath);
            }
            if (!Directory.Exists(MailTempRootPath))
            {
                Directory.CreateDirectory(MailTempRootPath);
            }
            if (!Directory.Exists(ThumbImageRootPath))
            {
                Directory.CreateDirectory(ThumbImageRootPath);
            }

        }
        /// <summary>
        /// 程序路径
        /// </summary>
        private static String basePath = System.AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// 
        /// </summary>
        public static String HtmlTempPath = Path.Combine(basePath, "pdftemp");
        public static String ContentTempPath = Path.Combine(basePath, "filetemp");
        public static String MailTempRootPath = Path.Combine(basePath, "mail");
        public static string ThumbImageRootPath = Path.Combine(basePath, "ThumbImages");
        /// <summary>
        /// 检测文件是否存在
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="throwExceptionIfNotExists">如果文件不存在，是否抛出异常</param>
        /// <returns></returns>
        public static bool CheckFileExists(String path, bool throwExceptionIfNotExists)
        {
            if (!System.IO.File.Exists(path))
            {
                if (throwExceptionIfNotExists)
                {
                    throw new FileNotFoundException(path);
                }
                return false;
            }
            return true;
        }
        /// <summary>
        /// 读取文件获取字节流
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static Byte[] ReadFileContentFromDisk(String filePath)
        {
            CheckFileExists(filePath, true);
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                byte[] content = new byte[(int)fs.Length];
                fs.Read(content, 0, content.Length);
                fs.Close();
                return content;
            }

        }
        /// <summary>
        /// 将字节流写入指定文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="content"></param>
        public static void WriteToDisk(string filePath, Byte[] content)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                stream.Write(content, 0, content.Length);
            }
        }
        /// <summary>
        /// 确保路径存在
        /// </summary>
        /// <param name="directoryName"></param>
        public static void EnsureDirectoryExists(string directoryName)
        {
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
        }
        /// <summary>
        /// 将文本文件写入另一文本文件中 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public String CopyNewFile(String inputFile, String outputFile)
        {
            string outPutDirectory = Path.GetDirectoryName(outputFile);
            EnsureDirectoryExists(outPutDirectory);
            File.Copy(inputFile, outputFile, true);
            return outputFile;
        }



    }
}
