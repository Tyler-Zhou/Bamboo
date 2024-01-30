using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ICP.Framework.CommonLibrary.Common;
using ICP.DataCache.ServiceInterface1;

namespace ICP.DataCache.ServiceInterface1
{

    
    /// <summary>
    /// 上传文档时所对应的列名，文档路径列名，文档类型
    /// 创建人：joe
    /// 创建时间：2013-05-27
    /// </summary>
    [Serializable]
    public class UploadColumnType
    {
        /// <summary>
        /// 文档列名
        /// </summary>
        public string DocColumnName { get; set; }
        ///// <summary>
        ///// 文档路径保存列名（隐藏列）
        ///// </summary>
        //public string DocPathColumnName{get;set;}

        /// <summary>
        /// 文档类型
        /// </summary>
        public int DocumentType { get; set; }

        /// <summary>
        ///业务类型
        /// </summary>
        public int OperateType { get; set; }


    }
}
