#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/1/8 星期一 13:58:25
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.IO;
using System.Text;
using System.Threading;
using Cityocean.Crawl.CommonLibrary;

namespace Cityocean.Crawl.ServerComponents
{
    /// <summary>
    /// HTML缓存服务
    /// </summary>
    public sealed class HTMLCacheService
    {
        #region Fields
        static private ReaderWriterLockSlim cacheWriterLockSlim = new ReaderWriterLockSlim();
        static private ReaderWriterLockSlim cacheReaderLockSlim = new ReaderWriterLockSlim();
        #endregion

        #region Public
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramDirectory">目录</param>
        /// <param name="paramFileName">文件名称</param>
        /// <param name="paramContent">内容</param>
        /// <param name="paramFileExistsSkip">文件存在则跳过</param>
        /// <returns>文件是否存在</returns>
        public static bool WriteCache(string paramDirectory, string paramFileName, string paramContent, bool paramFileExistsSkip = false)
        {
            cacheWriterLockSlim.EnterWriteLock();
            try
            {
                if (paramContent.IsNullOrEmpty())
                    return false;
                if (!paramDirectory.IsExistsDirectory())
                {
                    Directory.CreateDirectory(paramDirectory);
                }
                string LogFile = paramDirectory + paramFileName + ".cache";
                if (paramFileExistsSkip)
                {
                    if (File.Exists(LogFile))
                        return true;
                }
                CustomStreamWriter(LogFile, paramContent);
                return false;
            }
            catch (IOException)
            {
                return false;
            }
            catch (Exception)
            {
                //Write(ex);
                return false;
            }
            finally
            {
                cacheWriterLockSlim.ExitWriteLock();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramFileName"></param>
        public static string ReadCache(string paramFileName)
        {
            cacheReaderLockSlim.EnterReadLock();
            try
            {
                return !File.Exists(paramFileName) ? string.Empty : CustomStreamReader(paramFileName);
            }
            catch (IOException)
            {
            }
            catch (Exception)
            {
                //Write(ex);
            }
            finally
            {
                cacheReaderLockSlim.ExitReadLock();
            }
            return string.Empty;
        }
        #endregion

        #region Private
        /// <summary>
        /// 自定义写入日志
        /// </summary>
        /// <param name="paramFileName">文件名</param>
        /// <param name="paramContent">日志内容</param>
        private static void CustomStreamWriter(string paramFileName, string paramContent)
        {
            StreamWriter writer = new StreamWriter(new FileStream(paramFileName, FileMode.Append, FileAccess.Write), Encoding.GetEncoding("GBK"));
            writer.WriteLine(paramContent);
            writer.Close();
        }

        /// <summary>
        /// 自定义写入日志
        /// </summary>
        /// <param name="paramFileName">文件名</param>
        private static string CustomStreamReader(string paramFileName)
        {
            StreamReader reader = new StreamReader(new FileStream(paramFileName, FileMode.Open, FileAccess.Read),
                    Encoding.GetEncoding("GBK"));
            StringBuilder strResult = new StringBuilder();
            string sLine = "";
            while (sLine != null)
            {
                sLine = reader.ReadLine();
                if (sLine != null && !sLine.Equals(""))
                    strResult.Append(sLine);
            }
            reader.Close();
            return strResult.ToString();
        }
        #endregion
    }
}
