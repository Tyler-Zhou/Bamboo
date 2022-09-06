using Client.Extensions;
using Microsoft.Xaml.Behaviors.Layout;
using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 人物装备
    /// </summary>
    public class CharacterEquipment : BaseModel
    {
        #region 键值
        /// <summary>
        /// 键值
        /// </summary>
        public override string Key
        {
            get
            {
                return "DataGridEquipmentDescription";
            }
        }
        #endregion

        #region 装备类型
        private EnumEquipment _EnumEquipment = EnumEquipment.UnKnown;
        /// <summary>
        /// 装备类型
        /// </summary>
        public EnumEquipment EquipmentType
        {
            get
            {
                return _EnumEquipment;
            }
            set
            {
                _EnumEquipment = value;
                RaisePropertyChanged(nameof(EquipmentType));
                RaisePropertyChanged(nameof(Description));
            }
        }
        #endregion

        #region 装备类型名称
        /// <summary>
        /// 装备类型名称
        /// </summary>
        public string EquipmentTypeName
        {
            get
            {
                return ($"EnumEquipment{EquipmentType}").FindResourceDictionary();
            }
        }
        #endregion

        #region 装备 Key
        private string _EquipmentKey = "";
        /// <summary>
        /// 装备 Key
        /// </summary>
        public string EquipmentKey
        {
            get
            {
                return _EquipmentKey;
            }
            set
            {
                _EquipmentKey = value;
                RaisePropertyChanged(nameof(EquipmentKey));
                RaisePropertyChanged(nameof(Description));
            }
        }
        #endregion

        #region 修饰符 Key 1
        private string _ModifierKey1 = "";
        /// <summary>
        /// 修饰符 Key 1
        /// </summary>
        public string ModifierKey1
        {
            get
            {
                return _ModifierKey1;
            }
            set
            {
                _ModifierKey1 = value;
                RaisePropertyChanged(nameof(ModifierKey1));
                RaisePropertyChanged(nameof(Description));
            }
        }
        #endregion

        #region 修饰符 Key 2
        private string _ModifierKey2 = "";
        /// <summary>
        /// 修饰符 Key 2
        /// </summary>
        public string ModifierKey2
        {
            get
            {
                return _ModifierKey2;
            }
            set
            {
                _ModifierKey2 = value;
                RaisePropertyChanged(nameof(ModifierKey2));
                RaisePropertyChanged(nameof(Description));
            }
        }
        #endregion

        #region 加成
        private int _Plus = 0;
        /// <summary>
        /// 加成
        /// </summary>
        public int Plus
        {
            get
            {
                return _Plus;
            }
            set
            {
                _Plus = value;
                RaisePropertyChanged(nameof(Plus));
                RaisePropertyChanged(nameof(PlusDescription));
            }
        }
        #endregion

        #region 加成
        /// <summary>
        /// 加成描述
        /// </summary>
        public string PlusDescription
        {
            get
            {
                if (_Plus == 0)
                    return "";
                return $"{(Plus > 0 ? "+" : "")}{Plus} ";
            }
        }
        #endregion

        #region 描述
        /// <summary>
        /// 描述
        /// </summary>
        [JsonIgnore]
        public string Description
        {
            get
            {
                string description = Key.FindResourceDictionary();
                description = description.Replace("^Modifier1$", ModifierKey1.FindResourceDictionary());
                description = description.Replace("^Modifier2$", ModifierKey2.FindResourceDictionary());
                description = description.Replace("^Equipment$", EquipmentKey.FindResourceDictionary());
                return description;
            }
        } 
        #endregion
    }
}
