using Client.Enums;
using Client.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        #region 属性
        /// <summary>
        /// 属性集合
        /// </summary>
        public ObservableCollection<CharacterStat> Stats { get; set; }
        #endregion

        #region 装备
        /// <summary>
        /// 装备集合
        /// </summary>
        public ObservableCollection<CharacterEquipment> Equipments { get; set; }
        #endregion

        #region 法术书
        /// <summary>
        /// 法术书
        /// </summary>
        public ObservableCollection<CharacterSpellBook> SpellBooks { get; }
        #endregion

        #region 详细目录
        /// <summary>
        /// 详细目录
        /// </summary>
        public ObservableCollection<CharacterInventory> Inventorys { get; set; }
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

        #region 经验任务进程
        /// <summary>
        /// 经验任务进程
        /// </summary>
        public ProgressRateExperience ExpTask { get; set; }
        #endregion

        #region 详细目录任务进程
        /// <summary>
        /// 详细目录任务进程
        /// </summary>
        public ProgressRateInventory InventoryTask { get; set; }
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
        public TaskProgressModel CurrentTask { get; set; }
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
            Inventorys = new ObservableCollection<CharacterInventory>();
            Acts = new ObservableCollection<CharacterAct>();
            Quests = new ObservableCollection<CharacterQuest>();
            InventoryTask = new ProgressRateInventory() { Key = "ProgressBarToolTipInventory", Position = 0, MaxValue = 0 };
            ExpTask = new ProgressRateExperience() { Key = "ProgressBarToolTipExperience", Position = 0, MaxValue = 0 };
            PlotTask = new ProgressRatePlot() {Key= "ProgressBarToolTipPlot", Position = 0, MaxValue = 26,CommpleteNeedTime=1 };
            QuestTask = new ProgressRateQuest() {Key= "ProgressBarToolTipQuest", Position = 0, MaxValue = 1 };
        }
        #endregion

        #region 公共方法(Public Method)
        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InitData()
        {
            InitStatData();
            InitEquipmentData();

            #region Inventorys
            Inventorys.Clear();
            Inventorys.AddRange(new ObservableCollection<CharacterInventory>()
            {
                new CharacterInventory(){Key="CharacterGold",Quality = 0 },
            });
            #endregion

            #region Acts
            Acts.Clear();
            Acts.AddRange(new ObservableCollection<CharacterAct>()
            {
                new CharacterAct() { Index = 0,IsCommplete=true },
                new CharacterAct() { Index = 1,IsCommplete=false },
            }); 
            #endregion
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="statType">属性类型</param>
        /// <returns>属性值</returns>
        /// <exception cref="Exception"></exception>
        public int GetStatValue(EnumStat statType)
        {
            try
            {
                InitStatData();
                var stat= Stats.SingleOrDefault(item => item.StatType == statType);
                if(stat==null)
                    throw new Exception("属性未初始化");
                return stat.Value;
            }
            catch(Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="statType">属性类型</param>
        /// <param name="statValue"></param>
        /// <param name="isInit">是否初始化</param>
        /// <returns>是否设置成功</returns>
        /// <exception cref="Exception"></exception>
        public bool SetStatValue(EnumStat statType,int statValue,bool isInit=false)
        {
            try
            {
                InitStatData();
                var stat = Stats.SingleOrDefault(item => item.StatType == statType);
                if (stat == null)
                    throw new Exception("属性未初始化");
                if (isInit)
                    stat.Value = statValue;
                else
                    stat.Value += statValue;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
        public bool UpdateEquipment(EnumEquipment equipmentType, string equipmentKey, string modifierKey1, string modifierKey2,int plus)
        {
            try
            {
                var equipment = Equipments.SingleOrDefault(item => item.EquipmentType == equipmentType);
                if (equipment == null)
                    throw new Exception("装备未初始化");
                equipment.EquipmentKey = equipmentKey;
                equipment.ModifierKey1 = modifierKey1;
                equipment.ModifierKey2 = modifierKey2;
                equipment.Plus = plus;
            }
            catch (Exception)
            {
                return false;
            }
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
            try
            {
                if (SpellBooks == null)
                    throw new Exception("法术书集合未实例化");
                var spellBook=SpellBooks.SingleOrDefault(item => item.Key.Equals(model.Key));
                if (spellBook == null)
                {
                    SpellBooks.Add(model);
                }else
                {
                    spellBook.Level += 1;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 私有方法(Private Method)
        /// <summary>
        /// 初始化属性数据
        /// </summary>
        private void InitStatData()
        {
            if(Stats == null|| Stats.Count<=0)
            {
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
            }
        }
        /// <summary>
        /// 初始化装备数据
        /// </summary>
        private void InitEquipmentData()
        {
            if(Equipments==null||Equipments.Count<=0)
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
        }
        #endregion
    }
}
