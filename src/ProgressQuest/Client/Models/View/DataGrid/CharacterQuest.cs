using Client.Extensions;
using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 任务模型
    /// </summary>
    public class CharacterQuest
    {
        /// <summary>
        /// 任务类型
        /// </summary>
        public EnumQuest QuestType { get; set; }

        /// <summary>
        /// 怪物 Key
        /// </summary>
        public string MonsterKey { get; set; }
        /// <summary>
        /// 怪物 等级
        /// </summary>
        public int MonsterLevel { get; set; }

        /// <summary>
        /// 货物 Key
        /// </summary>
        public string ItemKey { get; set; }
        /// <summary>
        /// 特价 Key
        /// </summary>
        public string SpecialKey { get; set; }

        /// <summary>
        /// 怪物或货物数量
        /// </summary>
        public int Count { get; set; } = 0;

        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsCommplete { get; set; } = false;

        /// <summary>
        /// 描述
        /// </summary>
        [JsonIgnore]
        public string Description
        {
            get
            {
                //TODO:词缀复数添加 complete_quest
                string description = "";
                switch (QuestType)
                {
                    case EnumQuest.Exterminate:
                        description = "EnumQuestExterminate".FindResourceDictionary();
                        description += " " + MonsterKey.FindResourceDictionary();
                        break;
                    case EnumQuest.Seek:
                        description = "EnumQuestSeek".FindResourceDictionary();
                        description += " " + ItemKey.FindResourceDictionary();
                        break;
                    case EnumQuest.DeliverThis:
                        description = "EnumQuestDeliverThis".FindResourceDictionary();
                        description += " " + ItemKey.FindResourceDictionary();
                        break;
                    case EnumQuest.FetchMe:
                        description = "EnumQuestFetchMe".FindResourceDictionary();
                        description += " " + ItemKey.FindResourceDictionary();
                        break;
                    case EnumQuest.Placate:
                        description = "EnumQuestPlacate".FindResourceDictionary();
                        description += " " + MonsterKey.FindResourceDictionary();
                        break;
                }
                return description;
            }
        }
    }
}
