using Client.Extensions;
using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 货物实体
    /// </summary>
    public class CharacterItem : BaseModel
    {
        #region 特征 Key(NPC)；怪物 Key(Monster)
        private string _ItemKey1 = "";
        /// <summary>
        /// 特征 Key(NPC)；怪物 Key(Monster)
        /// </summary>
        public string ItemKey1
        {
            get
            {
                return _ItemKey1;
            }
            set
            {
                _ItemKey1 = value;
                RaisePropertyChanged(nameof(ItemKey1));
                RaisePropertyChanged(nameof(Description));
            }
        }
        #endregion

        #region 特征 Key(NPC)；怪物 Key(Monster)
        private string _ItemKey2 = "";
        /// <summary>
        /// 特价 Key(NPC)；怪物装备 Key(Monster)
        /// </summary>
        public string ItemKey2
        {
            get
            {
                return _ItemKey2;
            }
            set
            {
                _ItemKey2 = value;
                RaisePropertyChanged(nameof(ItemKey2));
                RaisePropertyChanged(nameof(Description));
            }
        }
        #endregion

        #region 特征 Key(NPC)；怪物 Key(Monster)
        private string _ItemKey3 = "";
        /// <summary>
        /// 货物 Key(NPC)
        /// </summary>
        public string ItemKey3
        {
            get
            {
                return _ItemKey3;
            }
            set
            {
                _ItemKey3 = value;
                RaisePropertyChanged(nameof(ItemKey3));
                RaisePropertyChanged(nameof(Description));
            }
        }
        #endregion

        #region 数量
        private int _Quality = 0;
        /// <summary>
        /// 数量
        /// </summary>
        public int Quality
        {
            get
            {
                return _Quality;
            }
            set
            {
                _Quality = value;
                RaisePropertyChanged(nameof(Quality));
                RaisePropertyChanged(nameof(Description));
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
                string description = "";
                if ("DataGridGold".Equals(Key))
                {
                    description = Name;
                }
                else
                {
                    //NPC处随机获取物品 货物特征 + 特价 + 货物名称
                    //战斗获取装备 怪物名称 + 怪物装备
                    description = Key.FindResourceDictionary();
                    description = description.Replace("^ItemKey1$", ItemKey1.FindResourceDictionary());
                    description = description.Replace("^ItemKey2$", ItemKey2.FindResourceDictionary());
                    description = description.Replace("^ItemKey3$", ItemKey3.FindResourceDictionary());
                }
                return description;
            }
        } 
        #endregion
    }
}
