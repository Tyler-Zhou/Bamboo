namespace ICP.OA.ServiceInterface.DataObjects
{
    using System;
    using System.Collections.Generic;
    using ICP.Framework.CommonLibrary.Common;

    /// <summary>
    /// 公告对象
    /// </summary>
    [Serializable]
    public class BulletinData : BaseDataObject
    {
        /// <summary>
        /// ID
        /// </summary>@IsNew
        public System.Guid ID { get; set; }
        /// <summary>
        /// StructureNodeCName
        /// </summary>
        public string StructureNodeCName { get; set; }
        /// <summary>
        /// StructureNodeEName
        /// </summary>
        public string StructureNodeEName { get; set; }
        /// <summary>
        /// Subject
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Content
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// FromTime
        /// </summary>
        public DateTime FromTime { get; set; }
        /// <summary>
        /// ToTime
        /// </summary>
        public DateTime ToTime { get; set; }
        /// <summary>
        /// Priority
        /// </summary>
        public BulletinPriority Priority { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// BulletinType
        /// </summary>
        public System.Guid BulletinType { get; set; }
        /// <summary>
        /// BulletinTypeCName
        /// </summary>
        public string BulletinTypeCName { get; set; }
        /// <summary>
        /// BulletinTypeEName
        /// </summary>
        public string BulletinTypeEName { get; set; }
        /// <summary>
        /// 发布人
        /// </summary>
        public string Publisher { get; set; }
        /// <summary>
        /// 发布人中文名
        /// </summary>
        public string PublisherCName { get; set; }
        /// <summary>
        /// 发布人英文名
        /// </summary>
        public string PublisherEName { get; set; }
        /// <summary>
        /// Readers
        /// </summary>
        public List<Guid> Departments { get; set; }
    }

    /// <summary>
    /// 2.0中字典
    /// </summary>
    [Serializable]
    public class BulletinTypeData
    {
        /// <summary>
        /// ID
        /// </summary>
        public System.Guid ID { get; set; }
        /// <summary>
        /// Code
        /// </summary>
        public System.String Code { get; set; }
        /// <summary>
        /// CName
        /// </summary>
        public System.String CName { get; set; }
        /// <summary>
        /// EName
        /// </summary>
        public System.String EName { get; set; }
    }

    /// <summary>
    /// 2.0中组织结构树
    /// </summary>
    [Serializable]
    public class OrganizationTreeData
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 父节点ID
        /// </summary>
        public Guid? ParentID { get; set; }
        /// <summary>
        /// 中文简称
        /// </summary>
        public string CName { get; set; }
        /// <summary>
        /// 英文简称
        /// </summary>
        public string EName { get; set; }

        /// <summary>
        /// 是否有权限
        /// </summary>
        public bool HasPermission { get; set; }

    }
    
}
