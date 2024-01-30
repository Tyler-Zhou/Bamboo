using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Common;
using ICP.Common.ServiceInterface.DataObjects;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace ICP.DataCache.ServiceInterface1
{  
    /// <summary>
    /// 客户列表自定义信息类
    /// </summary>
    [Serializable]
   public class UserCustomGridInfo
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid? UserId { get; set; }
        /// <summary>
        /// 列表类型
        /// </summary>
       
    
      //  public ListFormType ListType { get; set; }

        /// <summary>
        ///操作视图代码
        /// </summary>
        public string TemplateCode { get; set; }
        /// <summary>
        /// 列自定义信息
        /// </summary>
        public List<CustomColumnInfo> Columns { get; set; }
        /// <summary>
        /// 更新或创建时间
        /// </summary>
        public DateTime? UpdateDate { get; set; }
    }
}
