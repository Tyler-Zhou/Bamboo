using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Linq;

namespace Client.Models
{
    /// <summary>
    /// 剧本
    /// </summary>
    public class CharacterQuestBook
    {
        #region 成员(Member)
        /// <summary>
        /// 剧幕索引
        /// </summary>
        public int ActIndex
        {
            get
            {
                if (Acts == null)
                    return 0;
                return Acts.Max(item=>item.Index);
            }
        }

        /// <summary>
        /// 怪物 Key
        /// </summary>
        [JsonIgnore]
        public string MonsterKey
        {
            get
            {
                if (CurrentQuest == null)
                    return "";
                return CurrentQuest.MonsterKey;
            }
        }

        /// <summary>
        /// 怪物携带货物 Key
        /// </summary>
        [JsonIgnore]
        public string MonsterItemKey
        {
            get
            {
                if (CurrentQuest == null)
                    return "";
                return CurrentQuest.ItemKey;
            }
        }

        /// <summary>
        /// 怪物等级
        /// </summary>
        [JsonIgnore]
        public int MonsterLevel
        {
            get
            {
                if (CurrentQuest == null)
                    return 0;
                return CurrentQuest.MonsterLevel;
            }
        }

        /// <summary>
        /// 当前任务
        /// </summary>
        [JsonIgnore]
        public CharacterQuest CurrentQuest
        {
            get
            {
                return Quests.SingleOrDefault(item => EnumQuest.Exterminate.Equals(item.QuestType) && !item.IsCommplete);
            }
        }

        #region 集合
        /// <summary>
        /// 剧幕集合
        /// </summary>
        public ObservableCollection<CharacterAct> Acts = new ObservableCollection<CharacterAct>();

        /// <summary>
        /// 任务集合
        /// </summary>
        public ObservableCollection<CharacterQuest> Quests = new ObservableCollection<CharacterQuest>();
        #endregion

        #endregion

        #region 公共方法(Public Method)
        
        #endregion
    }
}
