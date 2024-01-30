using SqlSugar;
using System;
using System.ComponentModel;

namespace CRM.Client.Entities
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class BaseEntity
    {
        /// <summary>
        /// 获取或设置 Id，雪花Id 
        /// </summary>
        [DisplayName("ID")]
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "编号,主键", IsIdentity = false)]
        public long ID { get; set; }

        /// <summary>
        /// 设置或获取创建日期
        /// </summary>
        [SugarColumn(ColumnDescription = "创建日期")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 设置或获取创建用户主键
        /// </summary>
        [SugarColumn(ColumnDescription = "创建用户主键")]
        public long? CreateUserId { get; set; }

        /// <summary>
        /// 设置或获取最后修改时间
        /// </summary>
        [SugarColumn(ColumnDescription = "最后修改时间")]
        public DateTime? ModifyTime { get; set; }

        /// <summary>
        /// 设置或获取最后修改用户
        /// </summary>
        [SugarColumn(ColumnDescription = "最后修改用户")]
        public long? ModifyUserId { get; set; }

        /// <summary>
        /// 设置或获取删除时间
        /// </summary>
        [SugarColumn(ColumnDescription = "删除时间")]
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 设置或获取删除用户
        /// </summary>
        [SugarColumn(ColumnDescription = "删除用户")]
        public long? DeleteUserId { get; set; }
    }
}
