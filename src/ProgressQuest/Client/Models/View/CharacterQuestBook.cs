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
                return Acts.Count - 1;
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
        /// <summary>
        /// 添加剧幕
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool AddAct(int index)
        {
            var model = Acts.SingleOrDefault(item => item.Index.Equals(index));
            if (model != null)
                return false;
            CharacterAct modelNew = new CharacterAct()
            {
                Key = index == 0 ? "DataGridPlotPrologue" : "DataGridPlotAct",
                Index = index,
                IsCommplete = false
            };
            Acts.Add(modelNew);
            return true;
        }

        /// <summary>
        /// 添加剧幕
        /// </summary>
        /// <returns></returns>
        public bool CommpleteAct()
        {
            var acts = Acts.Where(item => !item.IsCommplete);
            foreach (var item in acts)
            {
                item.IsCommplete = true;
            }
            return true;
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="questType">任务类型</param>
        /// <param name="monsterKey">怪物 Key</param>
        /// <param name="monsterLevel">怪物等级</param>
        /// <param name="count">怪物/货物 数量</param>
        /// <param name="itemKey">货物 Key</param>
        /// <param name="specialKey">特价 Key</param>
        /// <returns></returns>
        public bool AddQuest(EnumQuest questType
            , string monsterKey, int monsterLevel, int count
            , string itemKey, string specialKey
            )
        {
            CharacterQuest modelNew = new CharacterQuest()
            {
                Key = "DataGridQuestDescription",
                QuestType = questType,
                MonsterKey = monsterKey,
                MonsterLevel = monsterLevel,
                Count = count,
                ItemKey = itemKey,
                SpecialKey = specialKey,
                IsCommplete = false
            };
            Quests.Add(modelNew);
            return true;
        }

        /// <summary>
        /// 完成任务
        /// </summary>
        /// <returns></returns>
        public bool CommpleteQuest()
        {
            var quests = Quests.Where(item => !item.IsCommplete);
            foreach (var item in quests)
            {
                item.IsCommplete = true;
            }
            return true;
        }
        #endregion
    }
}
