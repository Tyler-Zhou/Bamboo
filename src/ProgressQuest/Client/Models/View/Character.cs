using Client.Extensions;
using Client.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Client.Models
{
    /// <summary>
    /// 人物模型
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
            get { return RaceKey.FindResourceDictionary(); }
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
            get { return ClassKey.FindResourceDictionary(); }
        }

        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; } = 1;

        #endregion

        #region 任务书
        /// <summary>
        /// 任务书
        /// </summary>
        public CharacterQuestBook QuestBook { get; set; }
        #endregion

        #region 集合
        /// <summary>
        /// 属性集合
        /// </summary>
        public ObservableCollection<CharacterStat> Stats = new ObservableCollection<CharacterStat>();
        /// <summary>
        /// 装备集合集合
        /// </summary>
        public ObservableCollection<CharacterEquipment> Equipments = new ObservableCollection<CharacterEquipment>();
        /// <summary>
        /// 法术书集合
        /// </summary>
        public ObservableCollection<CharacterSpellBook> SpellBooks = new ObservableCollection<CharacterSpellBook>();
        /// <summary>
        /// 货物集合
        /// </summary>
        public ObservableCollection<CharacterItem> Items = new ObservableCollection<CharacterItem>();
        
       
        #endregion

        #region 进度条
        /// <summary>
        /// 经验进度条
        /// </summary>
        public ExperienceBarModel ExperienceBar { get; set; }
        /// <summary>
        /// 货物进度条
        /// </summary>
        public ItemBarModel ItemBar { get; set; }
        /// <summary>
        /// 剧情进度条
        /// </summary>
        public PlotBarModel PlotBar { get; set; }
        /// <summary>
        /// 任务进度条
        /// </summary>
        public QuestBarModel QuestBar { get; set; }
        /// <summary>
        /// 当前执行任务进度条
        /// </summary>
        public CurrentBarModel CurrentBar { get; set; }
        #endregion

        #region 当前待执行任务队列
        /// <summary>
        /// 当前待执行任务队列
        /// </summary>
        public Queue TaskQueue = new Queue(); 
        #endregion

        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        public Character()
        {

        }
        #endregion

        #region 公共方法(Public Method)
        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InitData()
        {
            #region 属性集合
            Stats.Clear();
            Stats.AddRange(new List<CharacterStat>
            {
                new CharacterStat(){ Key="EnumStatStrength",StatType = EnumStat.Strength,Value = 0 },
                new CharacterStat(){ Key="EnumStatConstitution",StatType = EnumStat.Constitution,Value = 0 },
                new CharacterStat(){ Key="EnumStatDexterity",StatType = EnumStat.Dexterity,Value = 0 },
                new CharacterStat(){ Key="EnumStatIntelligence",StatType = EnumStat.Intelligence,Value = 0 },
                new CharacterStat(){ Key="EnumStatWisdom",StatType = EnumStat.Wisdom,Value = 0 },
                new CharacterStat(){ Key="EnumStatCharisma",StatType = EnumStat.Charisma,Value = 0 },
                new CharacterStat(){ Key="EnumStatHPMax",StatType = EnumStat.HPMax,Value = 0 },
                new CharacterStat(){ Key="EnumStatMPMax",StatType = EnumStat.MPMax,Value = 0 },
            });
            #endregion

            #region 装备集合
            Equipments.Clear();
            if (Equipments == null || Equipments.Count <= 0)
            {
                Equipments.AddRange(new ObservableCollection<CharacterEquipment>
                {
                    new CharacterEquipment(){ Key="EnumEquipmentWeapon" ,EquipmentType = EnumEquipment.Weapon },
                    new CharacterEquipment(){ Key="EnumEquipmentShield",EquipmentType = EnumEquipment.Shield },
                    new CharacterEquipment(){ Key="EnumEquipmentHelm",EquipmentType = EnumEquipment.Helm },
                    new CharacterEquipment(){ Key="EnumEquipmentHauberk",EquipmentType = EnumEquipment.Hauberk },
                    new CharacterEquipment(){ Key="EnumEquipmentBrassairts",EquipmentType = EnumEquipment.Brassairts },
                    new CharacterEquipment(){ Key="EnumEquipmentVambraces",EquipmentType = EnumEquipment.Vambraces},
                    new CharacterEquipment(){ Key="EnumEquipmentGauntlets",EquipmentType = EnumEquipment.Gauntlets },
                    new CharacterEquipment(){ Key="EnumEquipmentGambeson",EquipmentType = EnumEquipment.Gambeson },
                    new CharacterEquipment(){ Key="EnumEquipmentCuisses",EquipmentType = EnumEquipment.Cuisses },
                    new CharacterEquipment(){ Key="EnumEquipmentGreaves",EquipmentType = EnumEquipment.Greaves},
                    new CharacterEquipment(){ Key="EnumEquipmentSollerets",EquipmentType = EnumEquipment.Sollerets},
                });
            }
            #endregion

            #region 货物集合
            //货物默认添加金币
            Items.Clear();
            Items.AddRange(new ObservableCollection<CharacterItem>()
            {
                new CharacterItem(){Key="CharacterGold",Quality = 0 },
            });
            #endregion

            QuestBook = new CharacterQuestBook();

        }
        /// <summary>
        /// 获取特征集合
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<CharacterTrait> Traits()
        {
            return new ObservableCollection<CharacterTrait>
            {
                new CharacterTrait(){ Key="CharacterName",Value = Name },
                new CharacterTrait(){ Key="CharacterRace",Value = RaceKey.FindResourceDictionary() },
                new CharacterTrait(){ Key="CharacterClass",Value = ClassKey.FindResourceDictionary()},
                new CharacterTrait(){ Key="CharacterLevel",Value = ""+Level },
            };
        }
        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="statType">属性类型</param>
        /// <returns>属性值</returns>
        /// <exception cref="Exception"></exception>
        public int GetStatValue(EnumStat statType)
        {
            return Stats.SingleOrDefault(item => item.StatType == statType).Value;
        }
        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="statType">属性类型</param>
        /// <param name="statValue"></param>
        /// <param name="isInit">是否初始化</param>
        /// <returns>是否设置成功</returns>
        /// <exception cref="Exception"></exception>
        public bool SetStatValue(EnumStat statType, int statValue, bool isInit = false)
        {
            var stat = Stats.SingleOrDefault(item => item.StatType == statType);
            if (isInit)
                stat.Value = statValue;
            else
            {
                stat.Value += statValue;
                if (stat.StatType == EnumStat.Strength)
                    SetCapacity(CharacterHelper.GetCapacity(stat.Value));
            }
            return true;
        }
        /// <summary>
        /// 设置容量
        /// </summary>
        /// <param name="capacity">容量</param>
        public void SetCapacity(int capacity)
        {
            ItemBar.MaxValue = capacity;
        }
        /// <summary>
        /// 启程
        /// </summary>
        public void BeginQuest()
        {
            ItemBar = new ItemBarModel()
            {
                Position = 0,
                MaxValue = CharacterHelper.GetCapacity(GetStatValue(EnumStat.Strength))
            };
            ExperienceBar = new ExperienceBarModel()
            {
                Position = 0,
                MaxValue = CharacterHelper.GetMaxExperienceByLevel(Level)
            };
            PlotBar = new PlotBarModel()
            {
                Position = 0,
                MaxValue = 26,
                CommpleteNeedTime = CharacterHelper.ActTime(0)
            };
            QuestBar = new QuestBarModel()
            {
                Position = 0,
                MaxValue = 0
            };
            CurrentBar = new CurrentBarModel()
            {
                Position = 0,
                MaxValue = 30,
            };
            QuestBook.AddAct(0);
            TaskQueue.Enqueue(new PlotTask() { Key = "TaskPlot", ActIndex = QuestBook.ActIndex, Duration = 2 });
            TaskQueue.Enqueue(new RegularTask() { Key = "RegularTaskBeginQuest", Duration = 4 });
            TaskQueue.Enqueue(new RegularTask() { Key = "RegularTaskFeeling", Duration = 6 });
            TaskQueue.Enqueue(new RegularTask() { Key = "RegularTaskSecret", Duration = 6 });
            TaskQueue.Enqueue(new RegularTask() { Key = "RegularTaskNightVision", Duration = 10 });
        }

        /// <summary>
        /// 设置任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public bool SetTask(object task)
        {
            BaseTask taskModel = (BaseTask)task;
            CurrentBar.ToolTip = taskModel.Description;
            CurrentBar.Reset(taskModel.Duration);
            TaskQueue.Enqueue(task);
            return true;
        }
        /// <summary>
        /// 更新装备
        /// </summary>
        /// <param name="equipmentType">装备类型</param>
        /// <param name="equipmentKey">装备 Key </param>
        /// <param name="modifierKey1">修饰符 Key 1 </param>
        /// <param name="modifierKey2">修饰符 Key 2 </param>
        /// <param name="plus">加成</param>
        /// <returns>是否更新成功</returns>
        /// <exception cref="Exception"></exception>
        public bool UpdateEquipment(EnumEquipment equipmentType, string equipmentKey, string modifierKey1, string modifierKey2, int plus)
        {
            var equipment =Equipments.SingleOrDefault(item => item.EquipmentType == equipmentType);
            equipment.EquipmentKey = equipmentKey;
            equipment.ModifierKey1 = modifierKey1;
            equipment.ModifierKey2 = modifierKey2;
            equipment.Plus = plus;
            return true;
        }
        /// <summary>
        /// 存款
        /// </summary>
        /// <param name="quality"></param>
        /// <returns></returns>
        public bool AddGold(int quality)
        {
            var singItem = Items.SingleOrDefault(item => "CharacterGold".Equals(item.Key));
            singItem.Quality += quality;
            return true;
        }

        /// <summary>
        /// 获取金币数量
        /// </summary>
        /// <returns></returns>
        public int GetGold()
        {
            return Items.SingleOrDefault(item => "CharacterGold".Equals(item.Key)).Quality;
        }

        /// <summary>
        /// 添加货物
        /// </summary>
        /// <param name="key"></param>
        /// <param name="itemKey1"></param>
        /// <param name="itemKey2"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public bool AddItem(string key,string itemKey1,string itemKey2,int quality)
        {
            var singItem = Items.SingleOrDefault(
                item => item.Key.Equals(key)
                && item.ItemKey1.Equals(itemKey1)
                && item.ItemKey2.Equals(itemKey2)
                );
            if (singItem == null)
            {
                CharacterItem model = new CharacterItem()
                {
                    Key = key,
                    ItemKey1 = itemKey1,
                    ItemKey2 = itemKey2,
                };
                Items.Add(model);
            }
            else
            {
                singItem.Quality += quality;
            }
            return true;
        }

        /// <summary>
        /// 售卖货物
        /// </summary>
        /// <param name="key"></param>
        /// <param name="itemKey1"></param>
        /// <param name="itemKey2"></param>
        /// <returns></returns>
        public bool SellItem(string key, string itemKey1, string itemKey2)
        {
            var singItem = Items.SingleOrDefault(
                item => item.Key.Equals(key)
                && item.ItemKey1.Equals(itemKey1)
                && item.ItemKey2.Equals(itemKey2)
                );
            Items.Remove(singItem);
            return true;
        }

        /// <summary>
        /// 移除货物
        /// </summary>
        /// <param name="model">待移除货物实体</param>
        /// <returns></returns>
        public bool RemoveItem(CharacterItem model)
        {
            if (Items.Count == 1)
                return true;
            Items.Remove(model);
            return true;
        }
        /// <summary>
        /// 添加法术书
        /// </summary>
        /// <param name="model">法术书实体</param>
        /// <returns>是否新增成功</returns>
        /// <exception cref="Exception"></exception>
        public bool AddSpellBook(CharacterSpellBook model)
        {
            var spellBook = SpellBooks.SingleOrDefault(item => item.Key.Equals(model.Key));
            if (spellBook == null)
            {
                SpellBooks.Add(model);
            }
            else
            {
                spellBook.Level += 1;
            }
            return true;
        }
        #endregion

        #region 私有方法(Private Method)

        #endregion
    }
}
