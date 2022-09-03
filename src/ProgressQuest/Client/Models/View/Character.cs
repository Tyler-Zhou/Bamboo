using Client.Extensions;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Client.Models
{
    /// <summary>
    /// 人物模型(仅序列化/反序列化存储)
    /// </summary>
    public class Character
    {
        #region 成员(Member)
        #region 基本信息
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 种族
        /// </summary>
        public string RaceKey { get; set; }
        /// <summary>
        /// 种族名称,显示在存档选择界面
        /// </summary>
        [JsonIgnore]
        public string RaceName
        {
            get { return this.GetRaceName(); }
        }

        /// <summary>
        /// 职业Key
        /// </summary>
        public string ClassKey { get; set; }

        /// <summary>
        /// 职业名称,显示在存档选择界面
        /// </summary>
        [JsonIgnore]
        public string ClassName
        {
            get { return this.GetClassName(); }
        }

        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; } = 1;
        /// <summary>
        /// 经验
        /// </summary>
        public int Experience { get; set; } = 0;
        #endregion

        #region 属性集合
        /// <summary>
        /// 属性集合
        /// </summary>
        public ObservableCollection<CharacterStat> Stats { get; set; }
        #endregion

        #region 装备集合
        /// <summary>
        /// 装备集合集合
        /// </summary>
        public ObservableCollection<CharacterEquipment> Equipments { get; set; }
        #endregion

        #region 法术书集合
        /// <summary>
        /// 法术书集合
        /// </summary>
        public ObservableCollection<CharacterSpellBook> SpellBooks { get; }
        #endregion

        #region 货物集合
        /// <summary>
        /// 货物集合
        /// </summary>
        public ObservableCollection<CharacterItem> Items { get; set; }
        #endregion

        #region 剧幕集合
        /// <summary>
        /// 剧幕集合
        /// </summary>
        public ObservableCollection<CharacterAct> Acts { get; set; }
        #endregion

        #region 任务集合
        /// <summary>
        /// 任务集合
        /// </summary>
        public ObservableCollection<CharacterQuest> Quests { get; set; }
        #endregion

        #region 经验进程
        /// <summary>
        /// 经验进程
        /// </summary>
        public ProgressRateExperience ExpTask { get; set; }
        #endregion

        #region 货物进程
        /// <summary>
        /// 货物进程
        /// </summary>
        public ProgressRateItem ItemTask { get; set; }
        #endregion

        #region 剧情任务进程
        /// <summary>
        /// 剧情任务进程
        /// </summary>
        public ProgressRatePlot PlotTask { get; set; }
        #endregion

        #region 任务进程
        /// <summary>
        /// 任务进程
        /// </summary>
        public ProgressRateQuest QuestTask { get; set; }
        #endregion

        #region 当前任务进程
        /// <summary>
        /// 当前任务进程
        /// </summary>
        public ProgressRateCurrent CurrentTask { get; set; }
        #endregion

        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        public Character()
        {
            Stats = new ObservableCollection<CharacterStat>();
            SpellBooks = new ObservableCollection<CharacterSpellBook>();
            Equipments = new ObservableCollection<CharacterEquipment>();
            Items = new ObservableCollection<CharacterItem>();
            Acts = new ObservableCollection<CharacterAct>();
            Quests = new ObservableCollection<CharacterQuest>();
        }
        #endregion

        #region 公共方法(Public Method)
        #endregion

        #region 私有方法(Private Method)

        #endregion
    }
}
