using Client.Extensions;
using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 杀怪任务
    /// </summary>
    public class KillTask:BaseTask
    {
        /// <summary>
        /// 怪物 Key
        /// </summary>
        public string MonsterKey { get; set; }
        /// <summary>
        /// 怪物携带货物 Key
        /// </summary>
        public string MonsterItemKey { get; set; }

        /// <summary>
        /// 怪物名称
        /// </summary>
        public string MonsterName { get; set; }

        /// <summary>
        /// 是否NPC
        /// </summary>
        public bool IsNPC { get; set; }

        /// <summary>
        /// 种族
        /// </summary>
        public string RaceKey { get; set; }

        /// <summary>
        /// NPC职业
        /// </summary>
        public string NPCClassKey { get; set; }

        /// <summary>
        /// 标题 Key
        /// </summary>
        public string TitleKey { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// SellTaskKey
        /// </summary>
        public override string Key
        {
            get
            {
                if (IsNPC)
                    return "TaskKillKeyForNPC";
                else
                    return "TaskKillKeyForMonster";
            }
        }

        /// <summary>
        /// 描述
        /// </summary>
        [JsonIgnore]
        public override string Description
        {
            get
            {
                string name = Key.FindResourceDictionary();
                if(!string.IsNullOrWhiteSpace(RaceKey))
                {
                    //当前为NPC
                    if (IsNPC)
                    {
                        //Executing passing ^NPCRaceName$ ^NPCClassName$
                        name = name.Replace($"^NPCRaceName$", RaceKey.FindResourceDictionary());
                        name = name.Replace($"^NPCClassName$", RaceKey.FindResourceDictionary());
                        
                    }
                    else
                    {
                        //Executing ^TitleName$ ^NPCClassName$ the ^NPCRaceName$
                        name = name.Replace($"^TitleName$", TitleKey.FindResourceDictionary());
                        name = name.Replace($"^MonsterName$", MonsterName.FindResourceDictionary());
                        name = name.Replace($"^NPCRaceName$", RaceKey.FindResourceDictionary());
                    }
                }
                return name;
            }
            set
            {

            }
        }
    }
}
