using System.Collections.ObjectModel;
using System.Linq;

namespace Client.Models
{
    /// <summary>
    /// 人物
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
        /// 职业Key
        /// </summary>
        public string ClassKey { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; } = 1;
        /// <summary>
        /// 经验
        /// </summary>
        public int Experience { get; set; } = 0;

        private int _Gold = 0;
        /// <summary>
        /// 金币
        /// </summary>
        public int Gold 
        { 
            get
            {
                return _Gold;
            }
            set
            {
                
                _Gold = value;
                Inventorys.SingleOrDefault(item => "CharacterGold".Equals(item.Key)).Quality = _Gold;
            }
        }
        #endregion

        #region 属性
        int _Strength = 0;
        /// <summary>
        /// 力量
        /// </summary>
        public int Strength
        {
            get
            {
                return _Strength;
            }
            set
            {
                _Strength = value;
            }
        }

        /// <summary>
        /// 体质
        /// </summary>
        public int Constitution { get; set; }

        /// <summary>
        /// 敏捷
        /// </summary>
        public int Dexterity { get; set; }

        /// <summary>
        /// 智力
        /// </summary>
        public int Intelligence { get; set; }

        /// <summary>
        /// 智慧
        /// </summary>
        public int Wisdom { get; set; }

        /// <summary>
        /// 魅力
        /// </summary>
        public int Charisma { get; set; }

        /// <summary>
        /// 最大生命值
        /// </summary>
        public int HPMax { get; set; }

        /// <summary>
        /// 最大魔法值
        /// </summary>
        public int MPMax { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsValid => Validate();
        #endregion

        #region 装备
        /// <summary>
        /// 武器
        /// </summary>
        public string WeaponKey { get; set; }

        /// <summary>
        /// 盾牌
        /// </summary>
        public string ShieldKey { get; set; }

        /// <summary>
        /// 头盔
        /// </summary>
        public string HelmKey { get; set; }
        /// <summary>
        /// 胸甲
        /// </summary>
        public string HauberkKey { get; set; }
        /// <summary>
        /// 肩甲
        /// </summary>
        public string BrassairtsKey { get; set; }
        /// <summary>
        /// 臂甲
        /// </summary>
        public string VambracesKey { get; set; }
        /// <summary>
        /// 手套
        /// </summary>
        public string GauntletsKey { get; set; }
        /// <summary>
        /// 内甲
        /// </summary>
        public string GambesonKey { get; set; }
        /// <summary>
        /// 裤甲
        /// </summary>
        public string CuissesKey { get; set; }
        /// <summary>
        /// 腿甲
        /// </summary>
        public string GreavesKey { get; set; }
        /// <summary>
        /// 足甲
        /// </summary>
        public string SolleretsKey { get; set; }
        #endregion

        #region 法术书
        /// <summary>
        /// 法术书
        /// </summary>
        public ObservableCollection<SpellBookModel> SpellBooks { get; set; }
        #endregion

        #region 详细目录
        /// <summary>
        /// 详细目录
        /// </summary>
        public ObservableCollection<InventoryModel> Inventorys { get; set; }
        #endregion

        #region 详细目录任务进程
        /// <summary>
        /// 详细目录任务进程
        /// </summary>
        public ProgressBarInventory InventoryTask { get; set; }
        #endregion

        #region 经验任务进程
        /// <summary>
        /// 经验任务进程
        /// </summary>
        public ProgressBarExperience ExpTask { get; set; }
        #endregion

        #region 剧情任务进程
        /// <summary>
        /// 剧情任务进程
        /// </summary>
        public TaskProgressModel PlotTask { get; set; }
        #endregion

        #region 队列任务进程
        /// <summary>
        /// 队列任务进程
        /// </summary>
        public TaskProgressModel QuestTask { get; set; }
        #endregion

        #region 当前任务进程
        /// <summary>
        /// 当前任务进程
        /// </summary>
        public TaskProgressModel CurrentTask { get; set; }
        #endregion

        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        public Character()
        {
            SpellBooks = new ObservableCollection<SpellBookModel>();
            Inventorys = new ObservableCollection<InventoryModel>()
            {
                new InventoryModel(){Key="CharacterGold",Quality = Gold },
            };
            InventoryTask = new ProgressBarInventory() {Key= "ProgressBarToolTipInventory", Position = 0, MaxValue = 0 };
            ExpTask = new ProgressBarExperience() { Key = "ProgressBarToolTipExperience", Position = 0, MaxValue = 0 };
            PlotTask = new TaskProgressModel() { Position = 0, MaxValue = 26 };
            QuestTask = new TaskProgressModel() { Position = 0, MaxValue = 1 };

        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// 
        /// </summary>
        private bool Validate()
        {
            return ValueFallsInRange(Strength)
                && ValueFallsInRange(Constitution)
                && ValueFallsInRange(Dexterity)
                && ValueFallsInRange(Intelligence)
                && ValueFallsInRange(Wisdom)
                && ValueFallsInRange(Charisma)
                && HPMax > 0
                && MPMax > 0;
        }

        /// <summary>
        /// 值在范围内
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool ValueFallsInRange(int value)
        {
            return value > 0 && value < 16;
        }
        #endregion
    }
}
