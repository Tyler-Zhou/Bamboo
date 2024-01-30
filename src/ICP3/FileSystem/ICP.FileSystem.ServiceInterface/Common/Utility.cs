using System;
using System.Data;

namespace ICP.FileSystem.ServiceInterface
{
    /// <summary>
    /// 通用工具类
    /// </summary>
    public class FileSystemUtility
    {
        /// <summary>
        /// 创建附件DataTable
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable CreateAttachmentTable(string tableName)
        {
            DataTable dt = new DataTable(tableName);
            dt.Columns.Add("Id", typeof(Guid));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Content", typeof(Byte[]));
            return dt;
        }
    }
}
