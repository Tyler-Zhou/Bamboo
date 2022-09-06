using Client.Extensions;
using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 售卖任务
    /// </summary>
    public class SellTask: BaseTask
    {
        /// <summary>
        /// 货物 Key
        /// </summary>
        public string ItemKey { get; set; } = "";
        /// <summary>
        /// Item Key 1
        /// </summary>
        public string ItemKey1 { get; set; } = "";

        /// <summary>
        /// Item Key 1
        /// </summary>
        public string ItemKey2 { get; set; } = "";
        /// <summary>
        /// 货物数量
        /// </summary>
        public int ItemQuantity { get; set; } = 0;
        /// <summary>
        /// 任务类型
        /// </summary>
        public override EnumTask TaskType
        {
            get
            {
                return EnumTask.Sell;
            }
        }
        /// <summary>
        /// SellTaskKey
        /// </summary>
        public override string Key
        {
            get
            {
                return "TaskSell";
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
                //NPC货物
                if(!string.IsNullOrWhiteSpace(ItemKey))
                {
                    name = name.Replace("^ItemName$"
                        , $"{ItemKey.FindResourceDictionary()} {ItemKey1.FindResourceDictionary()} {ItemKey2.FindResourceDictionary()}");
                    name = name.Replace($"^ItemQuantity$", "" + ItemQuantity);
                }else
                {
                    name = name.Replace("^ItemName$" , $"{ItemKey1.FindResourceDictionary()} {ItemKey2.FindResourceDictionary()}");
                    name = name.Replace($"^ItemQuantity$", "" + ItemQuantity);
                }
                return name;
            }
            set
            {

            }
        }
    }
}
