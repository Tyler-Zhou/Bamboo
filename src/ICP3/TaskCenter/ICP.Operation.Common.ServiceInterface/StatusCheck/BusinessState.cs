using ICP.Framework.CommonLibrary.Common;

namespace ICP.Operation.Common.ServiceInterface
{
    public class BusinessState
    {
        /// <summary>
        /// 修改状态的名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 使用的方法名称(WorkItem里面的按钮命名-参照CommonCommandName)
        /// </summary>
        public string Methods { get; set; }
        /// <summary>
        /// 修改值
        /// </summary>
        public string ModifyValue { get; set; }
        /// <summary>
        /// 原值
        /// </summary>
        public string OriginalValue { get; set; }
        /// <summary>
        /// 用于UI转换类型使用
        /// </summary>
        public string SqlType { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public OperationType OperationType { get; set; }
        /// <summary>
        /// 提示中文信息
        /// </summary>
        public string CaptionCn { get; set; }
        /// <summary>
        /// 提示英文信息
        /// </summary>
        public string CaptionEn { get; set; }
        /// <summary>
        /// 关联事件
        /// </summary>
        public string AssociatedEvent { get; set; }

        /// <summary>
        /// 操作权限
        /// </summary>
        public string Permissions { get; set; }
    }
}
