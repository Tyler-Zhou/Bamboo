#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/5/12 17:30:05
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System.IO;
using System.Linq;

namespace Cityocean.Crawl.CommonLibrary
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public sealed class FileHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FPath"></param>
        public static bool RemoteDelDir(string FPath)
        {
            FileInfo RFile = new FileInfo(FPath);
            if (RFile.Exists)
            {
                return SigleFileDel(FPath);
            }
            DirectoryInfo Dir = new DirectoryInfo(FPath);
            if (!Dir.Exists) return false;
            if (Dir.GetFiles().Length <= 0)
            {
                Dir.Delete();
                return true;
            }
            if (Dir.GetFiles().Select(file => file.FullName).Any(FilePath => !SigleFileDel(FilePath)))
            {
                return false;
            }
            Dir.Delete();
            return true;
        }
        private static bool SigleFileDel(string FilePath)
        {
            FileInfo file = new FileInfo(FilePath);
            if (!file.Exists) 
                return false;
            file.Delete();
            return true;
        }
    }
}
