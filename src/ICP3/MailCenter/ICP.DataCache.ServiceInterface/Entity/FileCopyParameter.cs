using ICP.FileSystem.ServiceInterface;
using System;
using System.Collections.Generic;

namespace ICP.DataCache.ServiceInterface
{
    /// <summary>
    /// 附件列表参数类
    /// </summary>
    public class FileCopyParameters     
    {
        /// <summary>
        /// 业务ID集合
        /// </summary>
        public Guid[] OperationIDs { get; set; }
        /// <summary>
        /// 文档集合
        /// </summary>
        public List<DocumentInfo> Documents { get; set; }
        /// <summary>
        /// 删除文件集合
        /// </summary>
        public List<string> DeleteFiles { get; set; }

        /// <summary>
        /// SO　ＮＯ集合
        /// </summary>
        public String[] SONOs { get; set; }
        /// <summary>
        /// 更新时间集合
        /// </summary>
        public DateTime?[] UpdateDates { get; set; }
        /// <summary>
        /// 是否包含SONO这列
        /// </summary>
        public bool HasSONOColumn { get; set; }
    }
}
