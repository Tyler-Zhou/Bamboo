using Client.Extensions;

namespace Client.Models
{
    /// <summary>
    /// 杀怪任务
    /// </summary>
    public class KillTask : BaseTask
    {
        /// <summary>
        /// 是否NPC
        /// </summary>
        public bool IsNPC { get; set; } = false;
        /// <summary>
        /// 前缀 Key 1
        /// </summary>
        public string PrefixKey1 { get; set; } = "";
        /// <summary>
        /// 前缀 Key 2
        /// </summary>
        public string PrefixKey2 { get; set; } = "";
        /// <summary>
        /// 种族
        /// </summary>
        public string RaceKey { get; set; } = "";
        /// <summary>
        /// NPC(职业) 怪物(标题)
        /// </summary>
        public string ClassOrTitleKey { get; set; } = "";
        /// <summary>
        /// 怪物 Key
        /// </summary>
        public string MonsterKey { get; set; } = "";
        /// <summary>
        /// 怪物携带货物 Key
        /// </summary>
        public string MonsterItemKey { get; set; } = "";

        /// <summary>
        /// 怪物名称
        /// </summary>
        public string MonsterName { get; set; } = "";

        /// <summary>
        /// 数量
        /// </summary>
        public int Quality { get; set; } = 0;

        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; } = 0;


        /// <summary>
        /// 任务类型
        /// </summary>
        public override EnumTask TaskType
        {
            get
            {
                return EnumTask.Kill;
            }
        }

        /// <summary>
        /// SellTaskKey
        /// </summary>
        public override string Key
        {
            get
            {
                return "TaskKillKey";
            }
        }

        /// <summary>
        /// 描述
        /// </summary>
        public override string Description
        {
            get
            {
                string name = Key.FindResourceDictionary();
                name = name.Replace($"^Executing$", (IsNPC ? "TaskKillKeyForNPC".FindResourceDictionary() : "TaskKillKeyForMonster".FindResourceDictionary()));
                name = name.Replace($"^Level$", "" + Level);

                string description;
                if (IsNPC)
                {
                    description = RaceKey.FindResourceDictionary()
                        + ClassOrTitleKey.FindResourceDictionary();
                }
                else
                {
                    description = ClassOrTitleKey.FindResourceDictionary()
                        + MonsterName + RaceKey.FindResourceDictionary();
                }
                description = PrefixKey1.FindResourceDictionary()
                    + PrefixKey2.FindResourceDictionary()
                    + description
                    + MonsterKey.FindResourceDictionary()
                    + MonsterItemKey.FindResourceDictionary();
                //无名怪物，前缀前再附加冠词
                if (string.IsNullOrEmpty(MonsterName))
                {
                    description = description.AdditionalIndefiniteArticle(Quality);
                    if (Quality > 1)
                        description = description.ToPlural();
                }
                name = name.Replace($"^MonsterDescript$", description);
                return name;
            }
            set
            {

            }
        }
    }
}
