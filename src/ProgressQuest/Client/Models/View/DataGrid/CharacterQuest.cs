using Client.Extensions;
using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 任务模型
    /// </summary>
    public class CharacterQuest : BaseModel
    {
        #region 任务类型
        private EnumQuest _QuestType = EnumQuest.UnKnown;
        /// <summary>
        /// 任务类型
        /// </summary>
        public EnumQuest QuestType
        {
            get
            {
                return _QuestType;
            }
            set
            {
                _QuestType = value;
                RaisePropertyChanged(nameof(QuestType));
                RaisePropertyChanged(nameof(Description));
            }
        }
        #endregion

        #region 怪物 Key
        private string _MonsterKey = "";
        /// <summary>
        /// 怪物 Key
        /// </summary>
        public string MonsterKey
        {
            get
            {
                return _MonsterKey;
            }
            set
            {
                _MonsterKey = value;
                RaisePropertyChanged(nameof(MonsterKey));
                RaisePropertyChanged(nameof(Description));
            }
        }
        #endregion

        #region 怪物 等级
        private int _MonsterLevel = 0;
        /// <summary>
        /// 怪物 等级
        /// </summary>
        public int MonsterLevel
        {
            get
            {
                return _MonsterLevel;
            }
            set
            {
                _MonsterLevel = value;
                RaisePropertyChanged(nameof(MonsterLevel));
                RaisePropertyChanged(nameof(Description));
            }
        }
        #endregion

        #region 货物 Key
        private string _ItemKey = "";
        /// <summary>
        /// 货物 Key
        /// </summary>
        public string ItemKey
        {
            get
            {
                return _ItemKey;
            }
            set
            {
                _ItemKey = value;
                RaisePropertyChanged(nameof(ItemKey));
                RaisePropertyChanged(nameof(Description));
            }
        }
        #endregion

        #region 特价 Key
        private string _SpecialKey = "";
        /// <summary>
        /// 特价 Key
        /// </summary>
        public string SpecialKey
        {
            get
            {
                return _SpecialKey;
            }
            set
            {
                _SpecialKey = value;
                RaisePropertyChanged(nameof(SpecialKey));
                RaisePropertyChanged(nameof(Description));
            }
        }
        #endregion

        #region 怪物或货物数量
        private int _Count = 0;
        /// <summary>
        /// 怪物或货物数量
        /// </summary>
        public int Count
        {
            get
            {
                return _Count;
            }
            set
            {
                _Count = value;
                RaisePropertyChanged(nameof(Count));
                RaisePropertyChanged(nameof(Description));
            }
        }
        #endregion

        #region 是否完成
        private bool _IsCommplete = false;
        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsCommplete
        {
            get
            {
                return _IsCommplete;
            }
            set
            {
                _IsCommplete = value;
                RaisePropertyChanged(nameof(IsCommplete));
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
                string description = Key.FindResourceDictionary();
                description = description.Replace("^QuestType$", ($"EnumQuest{QuestType}").FindResourceDictionary());
                string description1 = "";
                switch (QuestType)
                {
                    case EnumQuest.Exterminate:
                        description1 = " " + MonsterKey.FindResourceDictionary().AdditionalDefiniteArticle(Count);
                        break;
                    case EnumQuest.Seek:
                        description1 = " " + ItemKey.FindResourceDictionary().AdditionalDefiniteArticle(Count);
                        break;
                    case EnumQuest.DeliverThis:
                        description1 = " " + ItemKey.FindResourceDictionary();
                        break;
                    case EnumQuest.FetchMe:
                        description1 = " " + ItemKey.FindResourceDictionary();
                        break;
                    case EnumQuest.Placate:
                        description1 = " " + MonsterKey.FindResourceDictionary().AdditionalDefiniteArticle(Count);
                        break;
                }
                description = description.Replace("^QuestDescription$", description1);
                return description;
            }
        }
        #endregion
    }
}
