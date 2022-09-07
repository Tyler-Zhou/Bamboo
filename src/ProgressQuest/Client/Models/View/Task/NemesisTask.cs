using Client.Extensions;
using Client.Helpers;
using Newtonsoft.Json;

namespace Client.Models.View.Task
{
    /// <summary>
    /// 克星任务
    /// </summary>
    public class NemesisTask : BaseTask
    {
        /// <summary>
        /// 怪物 Key / 令人印象深刻的标题 Key
        /// </summary>
        public string Key1 { get; set; } = "";
        /// <summary>
        /// Null  / 种族 Key
        /// </summary>
        public string Key2 { get; set; } = "";

        /// <summary>
        /// 怪物名字
        /// </summary>
        public string MonsterName { get; set; } = "";

        /// <summary>
        /// 任务类型
        /// </summary>
        public override EnumTask TaskType
        {
            get
            {
                return EnumTask.Nemesis;
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
                string description = Key.FindResourceDictionary();
                string description1;
                if (!string.IsNullOrEmpty(Key1))
                {
                    if (string.IsNullOrEmpty(Key2))
                    {
                        description1 = MonsterName + Key1.FindResourceDictionary();
                    }
                    else
                    {
                        description1 = Key1.FindResourceDictionary()
                            + Key2.FindResourceDictionary();
                        if (RandomHelper.Value(0, 2) > 0)
                        {
                            description1 += MonsterName;
                        }
                    }
                    description = description.Replace("^nemesis$", description1);
                }
                return description;
            }
            set
            {

            }
        }
    }
}
