using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ICP.Common.ServiceInterface
{  
    /// <summary>
    /// 客户端基础数据更新服务接口
    /// </summary>
    [ServiceContract]
   public interface IBaseDataNotifyService
    {
       /// <summary>
        /// 更新基础数据内存缓存
        /// </summary>
        /// <param name="dataType">基础数据类型</param>
        /// <param name="items">新增或更新的数据</param>
        /// <param name="deleteIds">需删除的数据Id列表</param>
       [OperationContract(IsOneWay=true)]
        void UpdateBaseData(BaseDataType dataType, List<BaseDataInfo> items, List<Guid> deleteIds);
    }
    /// <summary>
    /// 基础数据实体类
    /// </summary>
    [Serializable]
    public class BaseDataInfo
    {   
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID
        {
            get;
            set;
        }
        /// <summary>
        /// 父级ID
        /// </summary>
        public Guid? ParentID
        {
            get;
            set;
        }
        /// <summary>
        /// 代码
        /// </summary>
        public string Code
        {
            get;
            set;
        }
        /// <summary>
        /// 中文名称
        /// </summary>
        public string CName
        {
            get;
            set;
        }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string EName
        {
            get;
            set;
        }
    }
}
