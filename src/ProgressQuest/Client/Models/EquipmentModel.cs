using Client.Enums;

namespace Client.Models
{
    /// <summary>
    /// 装备实体
    /// </summary>
    public class EquipmentModel:BaseModel
    {
        /// <summary>
        /// 装备类型
        /// </summary>
        public EnumEquipment EquipmentType { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
