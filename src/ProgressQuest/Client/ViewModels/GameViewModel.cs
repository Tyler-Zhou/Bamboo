using Client.DataAccess;
using Client.Enums;
using Client.Extensions;
using Client.Helpers;
using Client.Interfaces;
using Client.Models;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class GameViewModel : BaseViewModel
    {
        #region 成员(Member)
        #region 人物
        /// <summary>
        /// 人物
        /// </summary>
        private Character _Character = new Character();
        /// <summary>
        /// 人物
        /// </summary>
        public Character Current
        {
            get => _Character;
            set
            {
                if (value != null)
                {
                    _Character = value;

                    RaisePropertyChanged(nameof(ContentVisible));
                    RaisePropertyChanged(nameof(Traits));
                    RaisePropertyChanged(nameof(Stats));
                    RaisePropertyChanged(nameof(Equipments));
                    RaisePropertyChanged(nameof(SpellBooks));
                    RaisePropertyChanged(nameof(Inventorys));
                    RaisePropertyChanged(nameof(Acts));

                    SetProgressBarExperience(_Character.ExpTask.Position);
                    RaisePropertyChanged(nameof(ExpTask));
                    SetProgressBarInventory(_Character.InventoryTask.Position);
                    RaisePropertyChanged(nameof(InventoryTask));
                    RaisePropertyChanged(nameof(PlotTask));
                }
            }
        }
        #endregion

        #region 内容是否可见
        /// <summary>
        /// 内容是否可见
        /// </summary>
        public bool ContentVisible
        {
            get
            {
                if (Current == null || string.IsNullOrWhiteSpace(Current.Name))
                    return false;
                return true;
            }
        } 
        #endregion

        #region 特征集合
        /// <summary>
        /// 特征集合
        /// </summary>
        public ObservableCollection<TraitModel> Traits
        {
            get => Current.Traits();
        }
        #endregion

        #region 属性集合
        /// <summary>
        /// 属性集合
        /// </summary>
        public ObservableCollection<CharacterStat> Stats
        {
            get => Current.Stats;
        }
        #endregion

        #region 装备
        /// <summary>
        /// 装备
        /// </summary>
        public ObservableCollection<CharacterEquipment> Equipments
        {
            get => Current.Equipments;
        }
        #endregion

        #region 法术书集合
        /// <summary>
        /// 法术书集合
        /// </summary>
        public ObservableCollection<CharacterSpellBook> SpellBooks
        {
            get => Current.SpellBooks;
        }
        #endregion

        #region 详细目录
        /// <summary>
        /// 详细目录
        /// </summary>
        public ObservableCollection<CharacterInventory> Inventorys
        {
            get => Current.Inventorys;
        }
        #endregion

        #region 剧幕集合
        /// <summary>
        /// 剧幕集合
        /// </summary>
        public ObservableCollection<CharacterAct> Acts
        {
            get => Current.Acts;
        }
        #endregion

        #region 任务集合
        /// <summary>
        /// 任务集合
        /// </summary>
        public ObservableCollection<CharacterQuest> Quests
        {
            get => Current.Quests;
        }
        #endregion

        #region 进度条(ProgressBar)

        #region 经验
        /// <summary>
        /// 
        /// </summary>
        public ProgressRateExperience ExpTask
        {
            get => Current.ExpTask;
        }
        #endregion

        #region 详细目录任务进程
        /// <summary>
        /// 详细目录任务进程
        /// </summary>
        public ProgressRateInventory InventoryTask
        {
            get => Current.InventoryTask;
        }
        #endregion

        #region 剧情
        /// <summary>
        /// 剧情任务进程
        /// </summary>
        public ProgressRatePlot PlotTask
        {
            get => Current.PlotTask;
        }
        #endregion

        #region 任务
        /// <summary>
        /// 任务
        /// </summary>
        public ProgressRateQuest QuestTask
        {
            get => Current.QuestTask;
        }
        #endregion

        #region 当前

        #endregion

        #endregion
        #endregion

        #region 服务(Service)
        /// <summary>
        /// 缓存服务
        /// </summary>
        ICacheService _CacheService;
        /// <summary>
        /// 日志服务
        /// </summary>
        ILogger _Logger;
        #endregion

        #region 命令(Command)

        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="cacheService"></param>
        /// <param name="windowService"></param>
        /// <param name="logger"></param>
        public GameViewModel(IContainerProvider provider, ICacheService cacheService, IWindowService windowService, ILogger logger) : base(provider)
        {
            _CacheService = cacheService;
            _Logger = logger;
            windowService.AddFunction(SaveCharacter);
            InitData();
        }
        #endregion

        #region 重写方法(Override)
        /// <summary>
        /// 是否可以处理请求的导航行为,当前视图/模型是否可以重用
        /// </summary>
        /// <param name="navigationContext">导航内容</param>
        /// <remarks>true:</remarks>
        /// <returns></returns>
        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }
        /// <summary>
        /// 从本页面转到其它页面时
        /// </summary>
        /// <param name="navigationContext">导航内容</param>
        /// <remarks>NavigationContext包含目标页面的URI</remarks>
        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        /// <summary>
        /// 从其它页面导航至本页面时
        /// </summary>
        /// <param name="navigationContext">导航内容</param>
        /// <remarks>NavigationContext包含传递过来的参数</remarks>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext != null)
            {
                if (navigationContext.Parameters.ContainsKey("Character"))
                {
                    Character character = navigationContext.Parameters["Character"] as Character;
                    var result = Task.Run(() => LoadCharacter(character).Result).Result;
                }
            }
        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            
        }
        /// <summary>
        /// 加载人物
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        async Task<bool> LoadCharacter(Character character)
        {
            if (character == null)
                return false;
            if (Current != null)
            {
                //过滤同名人物操作
                if (character.Name.Equals(Current.Name))
                    return false;
                //当前人物不为空
                if(!string.IsNullOrWhiteSpace(Current.Name))
                {
                    await SaveCharacter();
                    //重置人物信息
                    Current = null;
                }
            }
            Current = character;
            await SaveCharacter();
            return true;
        }
        /// <summary>
        /// 保存当前人物
        /// </summary>
        async Task<bool> SaveCharacter()
        {
            if (Current == null || string.IsNullOrWhiteSpace(Current.Name))
                return false;
            for (int i = 0; i < 100; i++)
            {
                LevelUpgrade();
            }
            //保存当前人物
            await _CacheService.SaveAsync(Current.Name, Current);
            return true;
        }

        /// <summary>
        /// 设置经验进度条
        /// </summary>
        void SetProgressBarExperience(int position)
        {
            ExpTask.Position = position;
            ExpTask.MaxValue = CharacterHelper.GetMaxExperienceByLevel(Current.Level);
        }
        /// <summary>
        /// 设置详细目录进度条
        /// </summary>
        void SetProgressBarInventory(int position)
        {
            InventoryTask.Position = position;
            SetCapacity(Current.GetCapacity());
        }
        /// <summary>
        /// 等级提升
        /// </summary>
        void LevelUpgrade()
        {
            _Character.Level += 1;
            _Logger.LogInformation($"升级到{Current.Level}");
            Current.SetStatValue(EnumStat.HPMax, CharacterHelper.LevelUpMaxHPOrMP(Current.GetStatValue(EnumStat.Constitution)));
            Current.SetStatValue(EnumStat.MPMax, CharacterHelper.LevelUpMaxHPOrMP(Current.GetStatValue(EnumStat.Intelligence)));

            WinStat();
            WinStat();
            WinSpell();
            WinEquipment();
            //self.exp_bar.reset(level_up_time(self.level))
            //self.emit("level_up")
        }

        /// <summary>
        /// 赢得属性
        /// </summary>
        void WinStat()
        {
            EnumStat chosenType = EnumStat.UnKnown;
            if(RandomHelper.Odds(1,2))
            {
                chosenType = (EnumStat)RandomHelper.Value(6);
            }else
            {
                //favor the best stat so it will tend to clump
                int sumValue = 0;
                for (int i = 0; i < 7; i++)
                {
                    sumValue+= Current.Stats[i].Value * Current.Stats[i].Value;
                }
                sumValue = RandomHelper.Value(sumValue);
                for (int i = 0; i < 6; i++)
                {
                    chosenType = Current.Stats[i].StatType;
                    sumValue -= Current.Stats[i].Value * Current.Stats[i].Value;
                    if (sumValue < 0)
                        break;
                }
            }
            Current.SetStatValue(chosenType, 1);
            if (chosenType == EnumStat.Strength)
                SetCapacity(Current.GetCapacity());
            RaisePropertyChanged(nameof(Stats));
        }

        /// <summary>
        /// 设置容量
        /// </summary>
        /// <param name="capacity"></param>
        void SetCapacity(int capacity)
        {
            InventoryTask.Reset(capacity);
            RaisePropertyChanged(nameof(InventoryTask));
        }
        /// <summary>
        /// 赢得法术书
        /// </summary>
        void WinSpell()
        {
            int maxValue = Math.Min(Current.GetStatValue(EnumStat.Wisdom) + Current.Level, Repository.Spells.Count);
            int randomNum = RandomHelper.MinValue(maxValue);
            SpellModel spell= Repository.Spells[randomNum];
            if(spell!=null)
            {
                Current.AddSpellBook(new CharacterSpellBook { Key = spell.Key, Level = 1 });
                RaisePropertyChanged(nameof(SpellBooks));
                _Logger.LogInformation($"获得法术书{spell.Key}");
            }
        }
        /// <summary>
        /// 赢得装备
        /// </summary>
        void WinEquipment()
        {
            EnumEquipment equipmentType = (EnumEquipment)RandomHelper.Value(12);
            ObservableCollection<EquipmentPresetModel> stuff;
            ObservableCollection<ModifierModel> better;
            ObservableCollection<ModifierModel> worse;
            ObservableCollection<ModifierModel> modifier_pool;
            if (equipmentType==EnumEquipment.Weapon)
            {
                stuff = Repository.Weapons;
                better = Repository.OffenseAttributes;
                worse = Repository.OffenseBads;
            }else
            {
                if (equipmentType == EnumEquipment.Shield)
                    stuff = Repository.Shields;
                else
                    stuff = Repository.Armors;
                better = Repository.DefenseAttributes;
                worse = Repository.DefenseBads;
            }
            EquipmentPresetModel equipment = PickEquipment(stuff);
            int plus = Current.Level - equipment.Quality;
            if (plus < 0)
                modifier_pool = worse;
            else
                modifier_pool = better;
            int count = 0;
            string modifierKey1 = "";
            string modifierKey2 = "";
            while (count < 2 && count < plus)
            {
                ModifierModel modifier= modifier_pool[RandomHelper.Value(modifier_pool.Count)];
                
                if (modifier.Key.Equals(modifierKey1)) //已选择修饰符
                    break;
                if(Math.Abs(plus) < Math.Abs(modifier.Quality)) //加成太多
                    break;

                if (count == 0)
                    modifierKey1 = modifier.Key;
                else
                    modifierKey2 = modifier.Key;
                plus -= modifier.Quality;
                count += 1;
            }
            Current.UpdateEquipment(equipmentType, equipment.Key, modifierKey1, modifierKey2, plus);
            _Logger.LogInformation($"获得装备 {equipment.Name} {modifierKey1} {modifierKey2}");
        }
        /// <summary>
        /// 挑选装备
        /// </summary>
        /// <param name="stuff"></param>
        /// <returns></returns>
        EquipmentPresetModel PickEquipment(ObservableCollection<EquipmentPresetModel> stuff)
        {
            EquipmentPresetModel result= stuff[RandomHelper.Value(stuff.Count)];
            for (int i = 0; i < 5; i++)
            {
                EquipmentPresetModel alternative = stuff[RandomHelper.Value(stuff.Count)];
                if(Math.Abs(Current.Level - alternative.Quality) < Math.Abs(Current.Level - result.Quality))
                {
                    result = alternative;
                }
            }
            return result;
        }
        #endregion
    }
}
