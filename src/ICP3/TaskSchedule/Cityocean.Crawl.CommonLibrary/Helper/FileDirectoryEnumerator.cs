#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/5/15 15:06:04
 * URL:http://www.cnblogs.com/xdesigner/archive/2006/12/08/586177.html
 *
 * Description:
 *         ->本对象为Win32API函数 FindFirstFile , FindNextFile 
             和 FindClose 的一个包装
             
             以下代码演示使用了 FileDirectoryEnumerator 
             
             FileDirectoryEnumerator e = new FileDirectoryEnumerator();
             e.SearchPath = @"c:\";
             e.Reset();
             e.ReturnStringType = true ;
             while (e.MoveNext())
             {
                 System.Console.WriteLine
                     ( e.LastAccessTime.ToString("yyyy-MM-dd HH:mm:ss")
                     + "   " + e.FileLength + "  \t" + e.Name );
             }
             e.Close();
             System.Console.ReadLine();
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;

namespace Cityocean.Crawl.CommonLibrary
{
    /// <summary>
    /// 文件和目录的遍历器
    /// </summary>
    public sealed class FileDirectoryEnumerator : IEnumerator
    {

        #region 表示对象当前状态的数据和属性 **********************************

        /// <summary>
        /// 当前对象
        /// </summary>
        private object objCurrentObject = null;

        private bool bolIsEmpty = false;
        /// <summary>
        /// 该目录为空
        /// </summary>
        public bool IsEmpty
        {
            get { return bolIsEmpty; }
        }
        private int intSearchedCount = 0;
        /// <summary>
        /// 已找到的对象的个数
        /// </summary>
        public int SearchedCount
        {
            get { return intSearchedCount; }
        }
        private bool bolIsFile = true;
        /// <summary>
        /// 当前对象是否为文件,若为true则当前对象为文件,否则为目录
        /// </summary>
        public bool IsFile
        {
            get { return bolIsFile; }
        }
        private int intLastErrorCode = 0;
        /// <summary>
        /// 最后一次操作的Win32错误代码
        /// </summary>
        public int LastErrorCode
        {
            get { return intLastErrorCode; }
        }
        /// <summary>
        /// 当前对象的名称
        /// </summary>
        public string Name
        {
            get
            {
                if (objCurrentObject != null)
                {
                    if (objCurrentObject is string)
                        return (string)objCurrentObject;
                    else
                        return ((FileSystemInfo)objCurrentObject).Name;
                }
                return null;
            }
        }
        /// <summary>
        /// 当前对象属性
        /// </summary>
        public FileAttributes Attributes
        {
            get { return (FileAttributes)myData.dwFileAttributes; }
        }
        /// <summary>
        /// 当前对象创建时间
        /// </summary>
        public DateTime CreationTime
        {
            get
            {
                long time = ToLong(myData.ftCreationTime_dwHighDateTime, myData.ftCreationTime_dwLowDateTime);
                DateTime dtm = DateTime.FromFileTimeUtc(time);
                return dtm.ToLocalTime();
            }
        }
        /// <summary>
        /// 当前对象最后访问时间
        /// </summary>
        public DateTime LastAccessTime
        {
            get
            {
                long time = ToLong(myData.ftLastAccessTime_dwHighDateTime, myData.ftLastAccessTime_dwLowDateTime);
                DateTime dtm = DateTime.FromFileTimeUtc(time);
                return dtm.ToLocalTime();
            }
        }
        /// <summary>
        /// 当前对象最后保存时间
        /// </summary>
        public DateTime LastWriteTime
        {
            get
            {
                long time = ToLong(myData.ftLastWriteTime_dwHighDateTime, myData.ftLastWriteTime_dwLowDateTime);
                DateTime dtm = DateTime.FromFileTimeUtc(time);
                return dtm.ToLocalTime();
            }
        }
        /// <summary>
        /// 当前文件长度,若为当前对象为文件则返回文件长度,若当前对象为目录则返回0
        /// </summary>
        public long FileLength
        {
            get
            {
                if (bolIsFile)
                    return ToLong(myData.nFileSizeHigh, myData.nFileSizeLow);
                else
                    return 0;
            }
        }

        #endregion

        #region 控制对象特性的一些属性 ****************************************

        private bool bolThrowIOException = true;
        /// <summary>
        /// 发生IO错误时是否抛出异常
        /// </summary>
        public bool ThrowIOException
        {
            get { return bolThrowIOException; }
            set { bolThrowIOException = value; }
        }
        private bool bolReturnStringType = true;
        /// <summary>
        /// 是否以字符串方式返回查询结果,若返回true则当前对象返回为字符串,
        /// 否则返回 System.IO.FileInfo或System.IO.DirectoryInfo类型
        /// </summary>
        public bool ReturnStringType
        {
            get { return bolReturnStringType; }
            set { bolReturnStringType = value; }
        }

        private string strSearchPattern = "*";
        /// <summary>
        /// 要匹配的文件或目录名,支持通配符
        /// </summary>
        public string SearchPattern
        {
            get { return strSearchPattern; }
            set { strSearchPattern = value; }
        }
        private string strSearchPath = null;
        /// <summary>
        /// 搜索的父目录,必须为绝对路径,不得有通配符,该目录必须存在
        /// </summary>
        public string SearchPath
        {
            get { return strSearchPath; }
            set { strSearchPath = value; }
        }

        private bool bolSearchForFile = true;
        /// <summary>
        /// 是否查找文件
        /// </summary>
        public bool SearchForFile
        {
            get { return bolSearchForFile; }
            set { bolSearchForFile = value; }
        }
        private bool bolSearchForDirectory = true;
        /// <summary>
        /// 是否查找子目录
        /// </summary>
        public bool SearchForDirectory
        {
            get { return bolSearchForDirectory; }
            set { bolSearchForDirectory = value; }
        }

        #endregion

        /// <summary>
        /// 关闭对象,停止搜索
        /// </summary>
        public void Close()
        {
            CloseHandler();
        }

        #region IEnumerator 成员 **********************************************

        /// <summary>
        /// 返回当前对象
        /// </summary>
        public object Current
        {
            get { return objCurrentObject; }
        }
        /// <summary>
        /// 找到下一个文件或目录
        /// </summary>
        /// <returns>操作是否成功</returns>
        public bool MoveNext()
        {
            bool success = false;
            while (true)
            {
                if (bolStartSearchFlag)
                    success = SearchNext();
                else
                    success = StartSearch();
                if (success)
                {
                    if (UpdateCurrentObject())
                        return true;
                }
                else
                {
                    objCurrentObject = null;
                    return false;
                }
            }
        }

        /// <summary>
        /// 重新设置对象
        /// </summary>
        public void Reset()
        {
            if (strSearchPath == null)
                throw new ArgumentNullException("SearchPath can not null");
            if (!strSearchPath.IsExistsDirectory())
            {
                Directory.CreateDirectory(strSearchPath);
            }
            if (strSearchPattern == null || strSearchPattern.Length == 0)
                strSearchPattern = "*";

            intSearchedCount = 0;
            objCurrentObject = null;
            CloseHandler();
            bolStartSearchFlag = false;
            bolIsEmpty = false;
            intLastErrorCode = 0;
        }

        #endregion

        #region 声明WIN32API函数以及结构 **************************************

        [Serializable,
        StructLayout
            (LayoutKind.Sequential,
            CharSet = CharSet.Auto
            ),
        BestFitMapping(false)]
        private struct WIN32_FIND_DATA
        {
            public int dwFileAttributes;
            public int ftCreationTime_dwLowDateTime;
            public int ftCreationTime_dwHighDateTime;
            public int ftLastAccessTime_dwLowDateTime;
            public int ftLastAccessTime_dwHighDateTime;
            public int ftLastWriteTime_dwLowDateTime;
            public int ftLastWriteTime_dwHighDateTime;
            public int nFileSizeHigh;
            public int nFileSizeLow;
            public int dwReserved0;
            public int dwReserved1;
            [MarshalAs
                (UnmanagedType.ByValTStr,
                SizeConst = 260)]
            public string cFileName;
            [MarshalAs
                (UnmanagedType.ByValTStr,
                SizeConst = 14)]
            public string cAlternateFileName;
        }

        [DllImport
            ("kernel32.dll",
            CharSet = CharSet.Auto,
            SetLastError = true)]
        private static extern IntPtr FindFirstFile(string pFileName, ref WIN32_FIND_DATA pFindFileData);

        [DllImport
            ("kernel32.dll",
           CharSet = CharSet.Auto,
            SetLastError = true)]
        private static extern bool FindNextFile(IntPtr hndFindFile, ref WIN32_FIND_DATA lpFindFileData);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FindClose(IntPtr hndFindFile);

        private static long ToLong(int height, int low)
        {
            long v = (uint)height;
            v = v << 0x20;
            v = v | ((uint)low);
            return v;
        }

        private static void WinIOError(int errorCode, string str)
        {
            switch (errorCode)
            {
                case 80:
                    throw new IOException("IO_FileExists :" + str);
                case 0x57:
                    throw new IOException("IOError:" + MakeHRFromErrorCode(errorCode));
                case 0xce:
                    throw new PathTooLongException("PathTooLong:" + str);
                case 2:
                    throw new FileNotFoundException("FileNotFound:" + str);
                case 3:
                    throw new DirectoryNotFoundException("PathNotFound:" + str);
                case 5:
                    throw new UnauthorizedAccessException("UnauthorizedAccess:" + str);
                case 0x20:
                    throw new IOException("IO_SharingViolation:" + str);
            }
            throw new IOException("IOError:" + MakeHRFromErrorCode(errorCode));
        }

        private static int MakeHRFromErrorCode(int errorCode)
        {
            return (-2147024896 | errorCode);
        }

        #endregion

        #region 内部代码群 ****************************************************

        private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
        /// <summary>
        /// 查找处理的底层句柄
        /// </summary>
        private IntPtr intSearchHandler = INVALID_HANDLE_VALUE;

        private WIN32_FIND_DATA myData = new WIN32_FIND_DATA();
        /// <summary>
        /// 开始搜索标志
        /// </summary>
        private bool bolStartSearchFlag = false;
        /// <summary>
        /// 关闭内部句柄
        /// </summary>
        private void CloseHandler()
        {
            if (intSearchHandler != INVALID_HANDLE_VALUE)
            {
                FindClose(intSearchHandler);
                intSearchHandler = INVALID_HANDLE_VALUE;
            }
        }
        /// <summary>
        /// 开始搜索
        /// </summary>
        /// <returns>操作是否成功</returns>
        private bool StartSearch()
        {
            bolStartSearchFlag = true;
            bolIsEmpty = false;
            objCurrentObject = null;
            intLastErrorCode = 0;

            string strPath = Path.Combine(strSearchPath, strSearchPattern);
            CloseHandler();
            intSearchHandler = FindFirstFile(strPath, ref myData);
            if (intSearchHandler == INVALID_HANDLE_VALUE)
            {
                intLastErrorCode = Marshal.GetLastWin32Error();
                if (intLastErrorCode == 2)
                {
                    bolIsEmpty = true;
                    return false;
                }
                if (bolThrowIOException)
                    WinIOError(intLastErrorCode, strSearchPath);
                else
                    return false;
            }
            return true;
        }
        /// <summary>
        /// 搜索下一个
        /// </summary>
        /// <returns>操作是否成功</returns>
        private bool SearchNext()
        {
            if (bolStartSearchFlag == false)
                return false;
            if (bolIsEmpty)
                return false;
            if (intSearchHandler == INVALID_HANDLE_VALUE)
                return false;
            intLastErrorCode = 0;
            if (FindNextFile(intSearchHandler, ref myData) == false)
            {
                intLastErrorCode = Marshal.GetLastWin32Error();
                CloseHandler();
                if (intLastErrorCode != 0 && intLastErrorCode != 0x12)
                {
                    if (bolThrowIOException)
                        WinIOError(intLastErrorCode, strSearchPath);
                    else
                        return false;
                }
                return false;
            }
            return true;
        }//private bool SearchNext()

        /// <summary>
        /// 更新当前对象
        /// </summary>
        /// <returns>操作是否成功</returns>
        private bool UpdateCurrentObject()
        {
            if (intSearchHandler == INVALID_HANDLE_VALUE)
                return false;
            bool Result = false;
            objCurrentObject = null;
            if ((myData.dwFileAttributes & 0x10) == 0)
            {
                // 当前对象为文件
                bolIsFile = true;
                if (bolSearchForFile)
                    Result = true;
            }
            else
            {
                // 当前对象为目录
                bolIsFile = false;
                if (bolSearchForDirectory)
                {
                    if (myData.cFileName == "." || myData.cFileName == "..")
                        Result = false;
                    else
                        Result = true;
                }
            }
            if (Result)
            {
                if (bolReturnStringType)
                    objCurrentObject = myData.cFileName;
                else
                {
                    string p = Path.Combine(strSearchPath, myData.cFileName);
                    if (bolIsFile)
                    {
                        objCurrentObject = new FileInfo(p);
                    }
                    else
                    {
                        objCurrentObject = new DirectoryInfo(p);
                    }
                }
                intSearchedCount++;
            }
            return Result;
        }//private bool UpdateCurrentObject()

        #endregion

    }//public class FileDirectoryEnumerator : System.Collections.IEnumerator
}
