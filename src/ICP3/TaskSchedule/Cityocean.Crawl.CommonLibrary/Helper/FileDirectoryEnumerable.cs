#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/5/15 15:03:20
 * URL:http://www.cnblogs.com/xdesigner/archive/2006/12/08/586177.html
 *
 * Description:
 *         ->以下代码演示使用这个文件目录遍历器
 *         
 *          FileDirectoryEnumerable e = new FileDirectoryEnumerable();
 *          e.SearchPath = @"c:\";
 *          e.ReturnStringType = true ;
 *          e.SearchPattern = "*.exe";
 *          e.SearchDirectory = false ;
 *          e.SearchFile = true;
 *          foreach (object name in e)
 *          {
 *              System.Console.WriteLine(name);
 *          }
 *          System.Console.ReadLine();
 *
 * History:
 *         ->
 */
#endregion

using System.Collections;

namespace Cityocean.Crawl.CommonLibrary
{
    /// <summary>
    /// 文件或目录遍历器,本类型为 FileDirectoryEnumerator 的一个包装
    /// </summary>
    public sealed class FileDirectoryEnumerable : IEnumerable
    {
        private ArrayList myList = new ArrayList();
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
        /// 文件或目录名的通配符
        /// </summary>
        public string SearchPattern
        {
            get { return strSearchPattern; }
            set { strSearchPattern = value; }
        }
        private string strSearchPath = null;
        /// <summary>
        /// 搜索路径,必须为绝对路径
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

        private bool bolThrowIOException = true;
        /// <summary>
        /// 发生IO错误时是否抛出异常
        /// </summary>
        public bool ThrowIOException
        {
            get { return bolThrowIOException; }
            set { bolThrowIOException = value; }
        }
        /// <summary>
        /// 返回内置的文件和目录遍历器
        /// </summary>
        /// <returns>遍历器对象</returns>
        public IEnumerator GetEnumerator()
        {
            FileDirectoryEnumerator e = new FileDirectoryEnumerator
            {
                ReturnStringType = bolReturnStringType,
                SearchForDirectory = bolSearchForDirectory,
                SearchForFile = bolSearchForFile,
                SearchPath = strSearchPath,
                SearchPattern = strSearchPattern,
                ThrowIOException = bolThrowIOException
            };
            myList.Add(e);
            return e;
        }

        /// <summary>
        /// 查询结果数量
        /// </summary>
        public int Count
        {
            get { return myList.Count; }
        }

        /// <summary>
        /// 关闭对象
        /// </summary>
        public void Close()
        {
            foreach (FileDirectoryEnumerator e in myList)
            {
                e.Close();
            }
            myList.Clear();
        }
    }
}
