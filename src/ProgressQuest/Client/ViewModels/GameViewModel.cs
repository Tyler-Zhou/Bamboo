using Client.DataAccess;
using Client.Extensions;
using Client.Helpers;
using Client.Interfaces;
using Client.Models;
using Client.Models.View.Task;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Threading;

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
                    RaisePropertyChanged(nameof(DataGridTraits));
                    RaisePropertyChanged(nameof(DataGridStats));
                    RaisePropertyChanged(nameof(DataGridEquipments));
                    RaisePropertyChanged(nameof(DataGridSpellBooks));
                    RaisePropertyChanged(nameof(DataGridActs));
                    RaisePropertyChanged(nameof(DataGridItems));
                    RaisePropertyChanged(nameof(DataGridQuests));
                    RaisePropertyChanged(nameof(ProgressBarExperience));
                    RaisePropertyChanged(nameof(ProgressBarItem));
                    RaisePropertyChanged(nameof(ProgressBarPlot));
                    RaisePropertyChanged(nameof(ProgressBarQuest));
                    RaisePropertyChanged(nameof(ProgressBarCurrent));
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

        #region 展示集合
        /// <summary>
        /// 特征集合
        /// </summary>
        public ObservableCollection<CharacterTrait> DataGridTraits
        {
            get => Current.Traits();
        }

        /// <summary>
        /// 属性集合
        /// </summary>
        public ObservableCollection<CharacterStat> DataGridStats
        {
            get => Current.Stats;
        }

        /// <summary>
        /// 装备集合
        /// </summary>
        public ObservableCollection<CharacterEquipment> DataGridEquipments
        {
            get => Current.Equipments;
        }

        /// <summary>
        /// 法术书集合
        /// </summary>
        public ObservableCollection<CharacterSpellBook> DataGridSpellBooks
        {
            get => Current.SpellBooks;
        }

        /// <summary>
        /// 货物集合
        /// </summary>
        public ObservableCollection<CharacterItem> DataGridItems
        {
            get => Current.Items;
        }

        /// <summary>
        /// 剧幕集合
        /// </summary>
        public ObservableCollection<CharacterAct> DataGridActs
        {
            get
            {
                if (Current.QuestBook == null)
                    return null;
                return Current.QuestBook.Acts;
            }
        }

        /// <summary>
        /// 任务集合
        /// </summary>
        public ObservableCollection<CharacterQuest> DataGridQuests
        {
            get
            {
                if (Current.QuestBook == null)
                    return null;
                return Current.QuestBook.Quests;
            }
        }
        #endregion

        #region 进度条(ProgressBar)

        /// <summary>
        /// 经验进度条
        /// </summary>
        public ExperienceBarModel ProgressBarExperience
        {
            get => Current.ExperienceBar;
        }
        /// <summary>
        /// 货物进度条
        /// </summary>
        public ItemBarModel ProgressBarItem
        {
            get => Current.ItemBar;
        }

        /// <summary>
        /// 剧情进度条
        /// </summary>
        public PlotBarModel ProgressBarPlot
        {
            get => Current.PlotBar;
        }

        /// <summary>
        /// 任务进度条
        /// </summary>
        public QuestBarModel ProgressBarQuest
        {
            get => Current.QuestBar;
        }

        /// <summary>
        /// 当前进度条
        /// </summary>
        public CurrentBarModel ProgressBarCurrent
        {
            get => Current.CurrentBar;
        }

        #endregion

        #region 计时器(DispatcherTimer)
        /// <summary>
        /// 自动保存
        /// </summary>
        DispatcherTimer _AutoSaveTimer = new DispatcherTimer();

        /// <summary>
        /// 任务计时器
        /// </summary>
        DispatcherTimer _TaskTimer = new DispatcherTimer();
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
        /// <param name="windowService">主窗体服务</param>
        /// <param name="logger"></param>
        public GameViewModel(IContainerProvider provider, ICacheService cacheService, IWindowService windowService, ILogger logger) : base(provider)
        {
            _CacheService = cacheService;
            _Logger = logger;
            windowService.AddFunction(StopDispatcherTimer);
            windowService.AddFunction(() => SaveCharacter());
            InitData();
        }
        #endregion

        #region 事件(Event)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskTimer_Tick(object sender, EventArgs e)
        {
            if (!ProgressBarCurrent.IsCommplete)
            {
                ProgressBarCurrent.Increment(0.1);
                RaisePropertyChanged(nameof(ProgressBarCurrent));
                return;
            }
            if (EnumTask.Kill.Equals(ProgressBarCurrent.TaskType))
            {
                // 升级
                if (ProgressBarExperience.IsCommplete)
                {
                    LevelUp();
                    //设置经验进程最大值
                    ProgressBarExperience.Reset(CharacterHelper.GetMaxExperienceByLevel(Current.Level));
                    RaisePropertyChanged(nameof(DataGridTraits));
                    RaisePropertyChanged(nameof(DataGridStats));
                }
                else
                {
                    ProgressBarExperience.Increment(ProgressBarCurrent.MaxValue);
                }
                RaisePropertyChanged(nameof(ProgressBarExperience));
            }
            // 推进任务
            if (Current.QuestBook.ActIndex > 0)
            {
                if (ProgressBarQuest.IsCommplete)
                {
                    CompleteQuest();
                    ProgressBarQuest.Reset(50 + RandomHelper.MinValue(1000));
                    RaisePropertyChanged(nameof(DataGridQuests));
                }
                else
                {
                    ProgressBarQuest.Increment(ProgressBarCurrent.MaxValue);
                }
                RaisePropertyChanged(nameof(ProgressBarQuest));
            }
            //推进剧情
            if (ProgressBarPlot.IsCommplete)
            {
                InterPlotCinematic();
            }
            else
            {
                ProgressBarPlot.Increment(ProgressBarCurrent.MaxValue);
            }
            RaisePropertyChanged(nameof(ProgressBarPlot));
            Dequeue();
        }
        /// <summary>
        /// 自动保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoSaveTimer_Tick(object sender, EventArgs e)
        {
            Task.Run(() => SaveCharacter(true));
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
            _TaskTimer.Stop();
            _AutoSaveTimer.Stop();
            var result = Task.Run(() => SaveCharacter().Result).Result;
            _Character = null;
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
                    if (character != null)
                    {
                        var result = Task.Run(() => LoadCharacter(character).Result).Result;
                        RaisePropertyChanged(nameof(Current));
                        _TaskTimer.Start();
                        _AutoSaveTimer.Start();
                    }
                }
            }
        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// 初始化数据
        /// </summary>
        void InitData()
        {
            _TaskTimer.Interval = TimeSpan.FromSeconds(0.1);
            _TaskTimer.Tick += TaskTimer_Tick;
            _TaskTimer.Start();

            _AutoSaveTimer.Interval = new TimeSpan(0, 5, 0);
            _AutoSaveTimer.Tick += AutoSaveTimer_Tick;
            _AutoSaveTimer.Start();
        }



        /// <summary>
        /// 设置任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        bool TaskSet(BaseTask task)
        {
            TaskAdd(task);
            ProgressBarCurrent.Reset(task.Duration);
            RaisePropertyChanged(nameof(ProgressBarCurrent));
            return true;
        }
        /// <summary>
        /// 任务添加(仅添加，不重置当前任务栏)
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        bool TaskAdd(BaseTask task)
        {
            if (task.Description.Contains("^"))
                throw new Exception("包含未定义键值");
            ProgressBarCurrent.TaskType = task.TaskType;
            ProgressBarCurrent.ToolTip = task.Description;
            Current.TaskQueue.Enqueue(task);
            return true;
        }

        /// <summary>
        /// 当前任务
        /// </summary>
        /// <returns></returns>
        object TaskPeek()
        {
            if (Current.TaskQueue.Count > 0)
                return Current.TaskQueue.Peek();
            else
                return null;
        }
        /// <summary>
        /// 任务数量
        /// </summary>
        /// <returns></returns>
        int TaskCount()
        {
            return Current.TaskQueue.Count;
        }
        /// <summary>
        /// 移除任务
        /// </summary>
        /// <returns></returns>
        BaseTask TaskDequeue()
        {
            return Current.TaskQueue.Dequeue();
        }
        /// <summary>
        /// 完成队列
        /// </summary>
        void CompleteQuest()
        {
            Current.QuestBook.CommpleteQuest();
            //任务完成随机获得法术书、装备、属性、货物
            int methodNo = RandomHelper.Value(4);
            switch (methodNo)
            {
                case 1:
                    WinSpell();
                    break;
                case 2:
                    WinEquipment();
                    break;
                case 3:
                    WinStat();
                    break;
                case 4:
                    WinItem();
                    break;
            }

            EnumQuest questType = (EnumQuest)(RandomHelper.Value(1, 5));
            string monsterKey = "";
            int monsterLevel = 0;
            int count = 0;
            string itemKey = "";
            string specialKey = "";
            switch (questType)
            {
                case EnumQuest.Exterminate:
                    MonsterModel monster1 = UnNamedMonster(Current.Level, 3);
                    monsterKey = monster1.Key;
                    itemKey = monster1.Item;
                    monsterLevel = monster1.Level;
                    count = 2;
                    break;
                case EnumQuest.Seek:
                    itemKey = Repository.ItemAttributes.Pick().Key;
                    specialKey = Repository.Specials.Pick().Key;
                    count = 1;
                    break;
                case EnumQuest.DeliverThis:
                    itemKey = Repository.BoringItems.Pick().Key;
                    break;
                case EnumQuest.FetchMe:
                    itemKey = Repository.BoringItems.Pick().Key;
                    count = 1;
                    break;
                case EnumQuest.Placate:
                    MonsterModel monster4 = UnNamedMonster(Current.Level, 1);
                    monsterKey = monster4.Key;
                    monsterLevel = monster4.Level;
                    itemKey = monster4.Item;
                    count = 2;
                    break;
            }
            Current.QuestBook.AddQuest(questType, monsterKey, monsterLevel, count, itemKey, specialKey);
        }
        /// <summary>
        /// 人物升级
        /// </summary>
        void LevelUp()
        {
            Current.Level += 1;
            Current.SetStatValue(EnumStat.HPMax, CharacterHelper.LevelUpMaxHPOrMP(Current.GetStatValue(EnumStat.Constitution)));
            Current.SetStatValue(EnumStat.MPMax, CharacterHelper.LevelUpMaxHPOrMP(Current.GetStatValue(EnumStat.Intelligence)));

            WinStat();
            WinStat();
            WinSpell();

        }
        /// <summary>
        /// 赢得属性
        /// </summary>
        void WinStat()
        {
            EnumStat chosenType = EnumStat.UnKnown;
            if (RandomHelper.Odds(1, 2))
            {
                chosenType = (EnumStat)RandomHelper.Value(CharacterHelper.EnumStatScope);
            }
            else
            {
                //favor the best stat so it will tend to clump
                int sumValue = 0;
                for (int i = 0; i < CharacterHelper.EnumStatScope; i++)
                {
                    sumValue += Current.Stats[i].Value * Current.Stats[i].Value;
                }
                sumValue = RandomHelper.Value(sumValue);
                for (int i = 0; i < CharacterHelper.EnumStatScope; i++)
                {
                    chosenType = Current.Stats[i].StatType;
                    sumValue -= Current.Stats[i].Value * Current.Stats[i].Value;
                    if (sumValue < 0)
                        break;
                }
            }
            Current.SetStatValue(chosenType, 1);
        }
        /// <summary>
        /// 赢得法术书
        /// </summary>
        void WinSpell()
        {
            int maxValue = Math.Min(Current.GetStatValue(EnumStat.Wisdom) + Current.Level, Repository.Spells.Count);
            BaseModel spell = Repository.Spells.Pick(maxValue);
            if (spell != null)
            {
                Current.AddSpellBook(new CharacterSpellBook { Key = spell.Key, Level = 1 });
            }
        }
        /// <summary>
        /// 赢得装备
        /// </summary>
        void WinEquipment()
        {
            EnumEquipment equipmentType = (EnumEquipment)RandomHelper.Value(CharacterHelper.EnumEquipmentScope);
            ObservableCollection<EquipmentPresetModel> stuff;
            ObservableCollection<ModifierModel> better;
            ObservableCollection<ModifierModel> worse;
            ObservableCollection<ModifierModel> modifier_pool;
            if (equipmentType == EnumEquipment.Weapon)
            {
                stuff = Repository.Weapons;
                better = Repository.OffenseAttributes;
                worse = Repository.OffenseBads;
            }
            else
            {
                if (equipmentType == EnumEquipment.Shield)
                    stuff = Repository.Shields;
                else
                    stuff = Repository.Armors;
                better = Repository.DefenseAttributes;
                worse = Repository.DefenseBads;
            }
            EquipmentPresetModel equipment = PickEquipment(Current.Level, stuff);
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
                ModifierModel modifier = modifier_pool.Pick();

                if (modifier.Key.Equals(modifierKey1)) //已选择修饰符
                    break;
                if (Math.Abs(plus) < Math.Abs(modifier.Quality)) //加成太多
                    break;

                if (count == 0)
                    modifierKey1 = modifier.Key;
                else
                    modifierKey2 = modifier.Key;
                plus -= modifier.Quality;
                count += 1;
            }
            Current.UpdateEquipment(equipmentType, equipment.Key, modifierKey1, modifierKey2, plus);
        }
        /// <summary>
        /// 赢得物品
        /// </summary>
        void WinItem()
        {
            string itemKey1 = Repository.ItemOfs.Pick().Key;
            string itemKey2 = Repository.ItemAttributes.Pick().Key;
            string itemKey3 = Repository.Specials.Pick().Key;
            int quality = 1;
            Current.AddItem(itemKey1, itemKey2, itemKey3, quality);
        }
        /// <summary>
        /// 队尾
        /// </summary>
        void Dequeue()
        {
            while (ProgressBarCurrent.IsCommplete)
            {
                object objTask = TaskPeek();
                if (objTask is KillTask)
                {
                    KillTask killTask = (KillTask)objTask;
                    if (killTask.IsNPC || (string.IsNullOrEmpty(killTask.MonsterKey) && string.IsNullOrEmpty(killTask.MonsterItemKey)))
                    {
                        //NPC
                        WinItem();
                    }
                    else
                    {
                        Current.AddItem(killTask.MonsterKey, killTask.MonsterItemKey, "", killTask.Quality);
                    }
                    RaisePropertyChanged(nameof(DataGridItems));
                    RaisePropertyChanged(nameof(ProgressBarItem));
                }
                else if (objTask is BuyTask)
                {
                    //购买装备
                    Current.AddGold(-CharacterHelper.EquipmentPrice(Current.Level));
                    WinEquipment();
                    RaisePropertyChanged(nameof(DataGridEquipments));
                }
                else if ((objTask is HeadingToMarketTask) || (objTask is SellTask))
                {
                    if (objTask is SellTask)
                    {
                        SellTask taskModel = (SellTask)objTask;
                        var amount = taskModel.ItemQuantity * Current.Level;
                        //从NPC处获得物品
                        if (!string.IsNullOrWhiteSpace(taskModel.ItemKey))
                        {
                            amount *= (1 + RandomHelper.MinValue(10)) * (1 + RandomHelper.MinValue(Current.Level));
                        }
                        Current.SellItem(taskModel.ItemKey, taskModel.ItemKey1, taskModel.ItemKey2);
                        Current.AddGold(amount);
                        RaisePropertyChanged(nameof(DataGridItems));
                        RaisePropertyChanged(nameof(ProgressBarItem));
                    }
                    if (Current.Items.Count > 1)
                    {
                        var item = Current.Items[1];
                        SellTask taskModel = new SellTask()
                        {
                            ItemKey = item.ItemKey1,
                            ItemKey1 = item.ItemKey2,
                            ItemKey2 = item.ItemKey3,
                            ItemQuantity = item.Quality,
                            Duration = 1,
                        };
                        TaskSet(taskModel);
                    }
                }
                else if (objTask is PlotTask)
                {
                    PlotTask taskModel = new PlotTask();
                    Current.QuestBook.CommpleteAct();
                    ProgressBarPlot.Reset(CharacterHelper.ActTime(Current.QuestBook.ActIndex));
                    Current.QuestBook.AddAct(Current.QuestBook.ActIndex + 1);
                    RaisePropertyChanged(nameof(DataGridActs));
                    if (Current.QuestBook.ActIndex > 1)
                    {
                        WinItem();
                        RaisePropertyChanged(nameof(DataGridItems));
                        WinEquipment();
                        RaisePropertyChanged(nameof(DataGridEquipments));
                    }
                }
                if (TaskCount() > 0)
                {
                    BaseTask task = TaskDequeue();
                    ProgressBarCurrent.TaskType = task.TaskType;
                    ProgressBarCurrent.ToolTip = task.Description;
                    ProgressBarCurrent.Reset(task.Duration);
                }
                else if (ProgressBarItem.IsCommplete)
                {
                    HeadingToMarketTask taskModel = new HeadingToMarketTask()
                    {
                        Duration = 4,
                    };
                    TaskSet(taskModel);
                }
                else if (objTask != null &&
                    (!(objTask is KillTask) && !(objTask is HeadingToKillingFieldsTask)))
                {
                    //金币够买装备
                    if (Current.GetGold() > CharacterHelper.EquipmentPrice(Current.Level))
                    {
                        BuyTask taskModel = new BuyTask()
                        {
                            Duration = 5,
                        };
                        TaskSet(taskModel);
                    }
                    else
                    {
                        HeadingToKillingFieldsTask taskModel = new HeadingToKillingFieldsTask()
                        {
                            Duration = 4,
                        };
                        TaskSet(taskModel);
                    }
                }
                else
                {
                    TaskSet(
                        BuilderKillTask(Current.Level
                        , Current.QuestBook.MonsterKey
                        , Current.QuestBook.MonsterItemKey
                        , Current.QuestBook.MonsterLevel));
                }
            }
        }
        /// <summary>
        /// 插曲电影
        /// </summary>
        void InterPlotCinematic()
        {
            int choice = RandomHelper.Value(3);
            string monsterName = "";
            switch (choice)
            {
                case 1:
                    TaskAdd(
                        new RegularTask
                        {
                            Key = "TaskRegularExhausted",
                            Duration = 1,
                        });
                    TaskAdd(
                        new RegularTask
                        {
                            Key = "TaskRegularGreet",
                            Duration = 2,
                        });
                    TaskAdd(
                        new RegularTask
                        {
                            Key = "TaskRegularCouncil",
                            Duration = 2,
                        });
                    TaskAdd(
                        new RegularTask
                        {
                            Key = "TaskRegularChosen",
                            Duration = 2,
                        });
                    break;
                case 2:
                    TaskAdd(
                       new RegularTask
                       {
                           Key = "TaskRegularSight",
                           Duration = 2,
                       });
                    //遇到克星
                    MonsterModel nemesis = UnNamedMonster(Current.Level + 3);
                    monsterName = CharacterHelper.GenerateEnglishName();
                    TaskAdd(
                       new NemesisTask
                       {
                           Key = "TaskNemesisStart",
                           Key1 = nemesis.Key,
                           MonsterName = monsterName,
                           Duration = 4,
                       });
                    int s = RandomHelper.Value(3);
                    for (int i = 0; ; i++)
                    {
                        if (i > RandomHelper.Value(1 + Current.QuestBook.ActIndex + 1))
                            break;
                        s += 1 + RandomHelper.Value(2);
                        switch (s % 3)
                        {
                            case 0:
                                TaskAdd(
                                   new NemesisTask
                                   {
                                       Key = "TaskNemesisCombat",
                                       Key1 = nemesis.Key,
                                       MonsterName = monsterName,
                                       Duration = 4,
                                   });
                                break;
                            case 1:
                                TaskAdd(
                                   new NemesisTask
                                   {
                                       Key = "TaskNemesisUpperHand",
                                       Key1 = nemesis.Key,
                                       MonsterName = monsterName,
                                       Duration = 4,
                                   });
                                break;
                            case 2:
                                TaskAdd(
                                   new NemesisTask
                                   {
                                       Key = "TaskNemesisAdvantageOver",
                                       Key1 = nemesis.Key,
                                       MonsterName = monsterName,
                                       Duration = 4,
                                   });
                                break;
                        }
                    }
                    TaskAdd(
                        new NemesisTask
                        {
                            Key = "TaskNemesisLoseConciousness",
                            Key1 = nemesis.Key,
                            MonsterName = monsterName,
                            Duration = 4,
                        });
                    TaskAdd(
                        new RegularTask
                        {
                            Key = "TaskRegularFriendlyPlace",
                            Duration = 4,
                        });
                    break;
                case 3:
                    string impressiveTitleKey = Repository.ImpressiveTitles.Pick().Key;
                    string raceKey = Repository.Races.Pick().Key;
                    if (RandomHelper.Value(2) == 2)
                        monsterName = CharacterHelper.GenerateEnglishName();
                    TaskAdd(
                       new NemesisTask
                       {
                           Key = "TaskNemesisRelief",
                           Key1 = impressiveTitleKey,
                           Key2 = raceKey,
                           MonsterName = monsterName,
                           Duration = 2,
                       });
                    TaskAdd(
                       new NemesisTask
                       {
                           Key = "TaskNemesisPrivateRejoicing",
                           Key1 = impressiveTitleKey,
                           Key2 = raceKey,
                           MonsterName = monsterName,
                           Duration = 3,
                       });
                    TaskAdd(
                       new NemesisTask
                       {
                           Key = "TaskNemesisForgetItem",
                           Key1 = Repository.BoringItems.Pick().Key,
                           Duration = 2,
                       });
                    TaskAdd(
                       new RegularTask
                       {
                           Key = "TaskRegularOverhear",
                           Duration = 2,
                       });
                    TaskAdd(
                       new NemesisTask
                       {
                           Key = "TaskNemesisDoubleDealer",
                           Duration = 2,
                       });
                    TaskAdd(
                       new RegularTask
                       {
                           Key = "TaskRegularNews",
                           Duration = 3,
                       });
                    break;
            }
            TaskAdd(new PlotTask { ActIndex = Current.QuestBook.ActIndex + 1, Duration = 2 });
        }
        /// <summary>
        /// 生病的前缀
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        string SickPrefix(int index)
        {
            index = 6 - Math.Abs(index);
            return MonsterPrefix(Repository.SickPrefixs, index);
        }
        /// <summary>
        /// 年轻的前缀
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        string YoungPrefix(int index)
        {
            index = 6 - Math.Abs(index);
            return MonsterPrefix(Repository.YoungPrefixs, index);
        }
        /// <summary>
        /// 巨大的前缀
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        string BigPrefix(int index)
        {
            return MonsterPrefix(Repository.BigPrefixs, index);
        }
        /// <summary>
        /// 特别的前缀
        /// </summary>
        /// <param name="index"></param>
        /// <param name="isNPC"></param>
        /// <returns></returns>
        string SpecialPrefix(int index, bool isNPC)
        {
            if (isNPC)
                return MonsterPrefix(Repository.SpecialOnePrefixs, index);
            return MonsterPrefix(Repository.SpecialTwoPrefixs, index);
        }
        /// <summary>
        /// 怪物前缀
        /// </summary>
        /// <param name="prefixs">前缀待选列表</param>
        /// <param name="index">前缀索引</param>
        /// <returns></returns>
        string MonsterPrefix(ObservableCollection<BaseModel> prefixs, int index)
        {
            index = Math.Abs(index);
            if (index < 1 || index > prefixs.Count)
                return "";
            return prefixs[index - 1].Key;
        }
        /// <summary>
        /// 挑选装备
        /// </summary>
        /// <param name="level">等级</param>
        /// <param name="stuff"></param>
        /// <returns></returns>
        EquipmentPresetModel PickEquipment(int level, ObservableCollection<EquipmentPresetModel> stuff)
        {
            EquipmentPresetModel result = stuff.Pick();
            for (int i = 0; i < 5; i++)
            {
                EquipmentPresetModel alternative = stuff.Pick();
                if (Math.Abs(level - alternative.Quality) < Math.Abs(level - result.Quality))
                {
                    result = alternative;
                }
            }
            return result;
        }
        /// <summary>
        /// 构建杀怪任务
        /// </summary>
        /// <param name="characterLevel">人物等级</param>
        /// <param name="monsterKey">怪物 Key</param>
        /// <param name="monsterItemKey">怪物携带货物 Key</param>
        /// <param name="monsterLevel">怪物等级</param>
        /// <returns></returns>
        KillTask BuilderKillTask(int characterLevel, string monsterKey, string monsterItemKey, int monsterLevel)
        {
            KillTask taskModel = new KillTask();
            int level = characterLevel;
            int newLevel = 0;
            bool isNPC = false;
            string prefixKey1 = "";
            string prefixKey2 = "";
            //设置怪物：种族；标题(NPC时为职业)；名称
            for (int i = 0; i < level; i++)
            {
                if (RandomHelper.Odds(2, 5))
                    level += RandomHelper.Value(2) * 2 - 1;
                if (level < 1)
                    level = 1;
            }
            if (RandomHelper.Odds(1, 25))
            {
                //每隔一段时间使用一个NPC
                RaceModel race = Repository.Races.Pick();
                taskModel.RaceKey = race.Key;
                if (RandomHelper.Odds(1, 2))
                {
                    //Passing ^RaceName$ ^ClassName$
                    isNPC = true;
                    taskModel.MonsterKey = "";
                    taskModel.ClassOrTitleKey = Repository.Classes.Pick().Key;
                }
                else
                {
                    // ^TitleName$ ^MonsterName$ the ^RaceName$
                    isNPC = false;
                    taskModel.ClassOrTitleKey = Repository.Titles.Pick().Key;
                    taskModel.MonsterName = CharacterHelper.GenerateEnglishName();
                }
                newLevel = level;
            }
            else if (!string.IsNullOrWhiteSpace(monsterKey) && RandomHelper.Odds(1, 4))
            {
                //使用任务中的怪物
                isNPC = false;
                taskModel.MonsterKey = monsterKey;
                taskModel.MonsterItemKey = monsterItemKey;
                newLevel = monsterLevel;
            }
            else
            {
                //从最接近我们想要的水平的随机怪物中挑选怪物
                var monster = UnNamedMonster(level);
                taskModel.MonsterKey = monster.Key;
                taskModel.MonsterItemKey = monster.Item;
                newLevel = monster.Level;
            }
            int qty = 1;
            if ((level - newLevel) > 10)
            {
                //等级太低，相乘
                qty = (level + RandomHelper.Value(Math.Max(newLevel, 1))) / (Math.Max(newLevel, 1));
                if (qty < 1)
                    qty = 1;
                level = level / qty;
            }
            //添加前缀Prefix
            if (level - newLevel <= -10)
            {
                //人物比怪物低10级以上-怪物为假想敌
                prefixKey2 = "PrefixImaginary";
            }
            else if (level - newLevel < -5)
            {
                int i = 10 + level - newLevel;
                i = 5 - RandomHelper.Value(i + 1);
                //人物比怪物低5级以上 
                prefixKey1 = SickPrefix(i);
                prefixKey2 = YoungPrefix(newLevel - level - i);
            }
            else if ((level - newLevel < 0) && (RandomHelper.Value(2) == 1))
            {
                //人物比怪物等级低
                prefixKey2 = SickPrefix(level - newLevel);
            }
            else if (level - newLevel < 0)
            {
                //人物比怪物等级低
                prefixKey2 = YoungPrefix(level - newLevel);
            }
            else if (level - newLevel >= 10)
            {
                //人物比怪物等级高10级及其以上
                prefixKey2 = "PrefixMessianic";
            }
            else if (level - newLevel > 5)
            {
                int i = 10 - (level - newLevel);
                i = 5 - RandomHelper.Value(i + 1);
                //人物比怪物等级高5级
                prefixKey1 = BigPrefix(i);
                prefixKey2 = SpecialPrefix(level - newLevel - i, isNPC);
            }
            else if (level - newLevel > 0 && RandomHelper.Value(2) == 1)
            {
                prefixKey2 = BigPrefix(level - newLevel);
            }
            else if (level - newLevel > 0)
            {
                prefixKey2 = SpecialPrefix(level - newLevel, isNPC);
            }
            newLevel = level;
            level = newLevel * qty;
            taskModel.PrefixKey1 = prefixKey1;
            taskModel.PrefixKey2 = prefixKey2;
            taskModel.IsNPC = isNPC;
            taskModel.Level = level;
            taskModel.Quality = qty;
            taskModel.Duration = CharacterHelper.KillTaskDuration(level, Current.Level);
            return taskModel;
        }
        /// <summary>
        /// 无名怪物
        /// </summary>
        /// <param name="level"></param>
        /// <param name="iterations"></param>
        /// <returns></returns>
        MonsterModel UnNamedMonster(int level, int iterations = 5)
        {
            var result = Repository.Monsters.Pick();
            for (int i = 0; i < iterations; i++)
            {
                var monster = Repository.Monsters.Pick();
                if (Math.Abs(level - monster.Level) < Math.Abs(level - result.Level))
                {
                    result = monster;
                }
            }
            return result;
        }
        #endregion

        #region 异步方法(Async Method)
        /// <summary>
        /// 停止计时器
        /// </summary>
        async Task<bool> StopDispatcherTimer()
        {
            await Task.Run(() => _AutoSaveTimer.Stop());
            await Task.Run(() => _TaskTimer.Stop());
            return true;
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
                if (!string.IsNullOrWhiteSpace(Current.Name))
                {
                    await SaveCharacter();
                    //重置人物信息
                    Current = null;
                }
            }
            Current = character;
            Current.IsOnLine = true;
            RaisePropertyChanged(nameof(Current));
            await SaveCharacter(true);
            return true;
        }
        /// <summary>
        /// 保存当前人物
        /// </summary>
        async Task<bool> SaveCharacter(bool isAutoSave = false)
        {
            if (Current == null || string.IsNullOrWhiteSpace(Current.Name))
                return false;
            Current.IsOnLine = isAutoSave;
            //保存当前人物
            await _CacheService.SaveAsync(Current.Name, Current);
            return true;
        }
        #endregion
    }
}
