using Client.DataAccess;
using Client.Extensions;
using Client.Helpers;
using Client.Interfaces;
using Client.Models;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Threading;
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
                    RaisePropertyChanged(nameof(Traits));
                    RaisePropertyChanged(nameof(Stats));
                    RaisePropertyChanged(nameof(Equipments));
                    RaisePropertyChanged(nameof(SpellBooks));
                    RaisePropertyChanged(nameof(Items));
                    RaisePropertyChanged(nameof(Acts));

                    RaisePropertyChanged(nameof(ExperienceBar));
                    RaisePropertyChanged(nameof(ItemBar));
                    RaisePropertyChanged(nameof(PlotBar));
                    RaisePropertyChanged(nameof(QuestBar));
                    RaisePropertyChanged(nameof(CurrentBar));
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
        public ObservableCollection<CharacterTrait> Traits
        {
            get => Current.Traits();
        }

        /// <summary>
        /// 属性集合
        /// </summary>
        public ObservableCollection<CharacterStat> Stats
        {
            get => Current.Stats;
        }

        /// <summary>
        /// 装备集合
        /// </summary>
        public ObservableCollection<CharacterEquipment> Equipments
        {
            get => Current.Equipments;
        }

        /// <summary>
        /// 法术书集合
        /// </summary>
        public ObservableCollection<CharacterSpellBook> SpellBooks
        {
            get => Current.SpellBooks;
        }

        /// <summary>
        /// 货物集合
        /// </summary>
        public ObservableCollection<CharacterItem> Items
        {
            get => Current.Items;
        }

        /// <summary>
        /// 剧幕集合
        /// </summary>
        public ObservableCollection<CharacterAct> Acts
        {
            get
            {
                if(Current.QuestBook==null)
                    return new ObservableCollection<CharacterAct>();
                return Current.QuestBook.Acts;
            }
        }

        /// <summary>
        /// 任务集合
        /// </summary>
        public ObservableCollection<CharacterQuest> Quests
        {
            get
            {
                if (Current.QuestBook == null)
                    return new ObservableCollection<CharacterQuest>();
                return Current.QuestBook.Quests;
            }
        }
        #endregion

        #region 进度条(ProgressBar)

        /// <summary>
        /// 经验进度条
        /// </summary>
        public ExperienceBarModel ExperienceBar
        {
            get => Current.ExperienceBar;
        }
        /// <summary>
        /// 货物进度条
        /// </summary>
        public ItemBarModel ItemBar
        {
            get => Current.ItemBar;
        }

        /// <summary>
        /// 剧情进度条
        /// </summary>
        public PlotBarModel PlotBar
        {
            get => Current.PlotBar;
        }

        /// <summary>
        /// 任务进度条
        /// </summary>
        public QuestBarModel QuestBar
        {
            get => Current.QuestBar;
        }

        /// <summary>
        /// 当前进度条
        /// </summary>
        public CurrentBarModel CurrentBar
        {
            get => Current.CurrentBar;
        }

        #endregion

        #region 计时器(DispatcherTimer)
        /// <summary>
        /// 剧情计时器
        /// </summary>
        DispatcherTimer plotTimer = new DispatcherTimer();
        /// <summary>
        /// 任务计时器
        /// </summary>
        DispatcherTimer taskTimer = new DispatcherTimer();
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
            windowService.AddFunction(SaveCharacter);
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
            if(Current.TaskQueue.Count>0)
            {
                object objTask = Current.TaskQueue.Peek();
                if (!CurrentBar.IsCommplete)
                {
                    CurrentBar.Increment(1);
                }
                RaisePropertyChanged(nameof(CurrentBar));
                if (objTask != null)
                {
                    if (objTask is KillTask)
                    {
                        // 升级
                        if (ExperienceBar.IsCommplete)
                        {
                            LevelUp();
                        }
                        else
                        {
                            ExperienceBar.Increment(ExperienceBar.MaxValue / 1000);
                        }
                        // 推进任务
                        if (Current.QuestBook.ActIndex >= 1)
                        {
                            if (QuestBar.IsCommplete)
                            {
                                CompleteQuest();
                            }
                            else
                            {
                                QuestBar.Increment(QuestBar.MaxValue / 1000);
                            }
                        }
                        // 推进剧情
                        if (PlotBar.IsCommplete)
                        {
                            InterPlotCinematic();
                        }
                        else
                        {
                            PlotBar.Reset(CurrentBar.MaxValue / 1000);
                        }
                    }

                    Dequeue();
                }
            }
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
                    RaisePropertyChanged(nameof(Current));
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
            taskTimer.Interval = TimeSpan.FromSeconds(0.05);
            taskTimer.Tick += TaskTimer_Tick;
            taskTimer.Start();
        }

        /// <summary>
        /// 开启旅程
        /// </summary>
        private void BeginQuest()
        {
           
            
        }
        void CompleteQuest()
        {
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
                    count = 2;
                    break;
            }
            Current.QuestBook.AddQuest(questType, monsterKey, monsterLevel, count, itemKey, specialKey);
        }

        /// <summary>
        /// 人物升级
        /// </summary>
        public void LevelUp()
        {
            Current.Level += 1;
            Current.SetStatValue(EnumStat.HPMax, CharacterHelper.LevelUpMaxHPOrMP(Current.GetStatValue(EnumStat.Constitution)));
            Current.SetStatValue(EnumStat.MPMax, CharacterHelper.LevelUpMaxHPOrMP(Current.GetStatValue(EnumStat.Intelligence)));

            WinStat();
            WinStat();
            WinSpell();
            //设置经验进程最大值
            Current.ExperienceBar.MaxValue = CharacterHelper.GetMaxExperienceByLevel(Current.Level);
        }

        /// <summary>
        /// 赢得属性
        /// </summary>
        public void WinStat()
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
        public void WinSpell()
        {
            int maxValue = Math.Min(Current.GetStatValue(EnumStat.Wisdom) + Current.Level, Repository.Spells.Count);
            SpellModel spell = Repository.Spells.Pick(maxValue);
            if (spell != null)
            {
                Current.AddSpellBook(new CharacterSpellBook { Key = spell.Key, Level = 1 });
            }
        }
        /// <summary>
        /// 赢得装备
        /// </summary>
        public void WinEquipment()
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
        public void WinItem()
        {
            string key = Repository.ItemOfs.Pick().Key;
            string itemKey1 = Repository.ItemAttributes.Pick().Key;
            string itemKey2 = Repository.Specials.Pick().Key;
            int quality = 1;
            Current.AddItem(key, itemKey1, itemKey2, quality);
        }

        /// <summary>
        /// 挑选装备
        /// </summary>
        /// <param name="level">等级</param>
        /// <param name="stuff"></param>
        /// <returns></returns>
        private EquipmentPresetModel PickEquipment(int level, ObservableCollection<EquipmentPresetModel> stuff)
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


        private void Dequeue()
        {
            while (CurrentBar.IsCommplete && Current.TaskQueue.Count>0)
            {
                object objTask = Current.TaskQueue.Peek();
                if (objTask is KillTask)
                {
                    KillTask killTask = (KillTask)objTask;
                    if (string.IsNullOrWhiteSpace(killTask.MonsterKey)
                        || string.IsNullOrWhiteSpace(killTask.MonsterItemKey))
                    {
                        //NPC
                        WinItem();
                    }
                    else
                    {
                        Current.AddItem("", killTask.MonsterKey, killTask.MonsterItemKey, 1);
                    }
                    RaisePropertyChanged(nameof(Items));
                }
                else if (objTask is BuyTask)
                {
                    //购买装备
                    Current.AddGold(-CharacterHelper.EquipmentPrice(Current.Level));
                    WinEquipment();
                    RaisePropertyChanged(nameof(Equipments));
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
                        RaisePropertyChanged(nameof(Items));
                    }
                    if (Current.Items.Count > 1)
                    {
                        var item = Current.Items[1];
                        SellTask taskModel = new SellTask()
                        {
                            ItemKey = item.Key,
                            ItemKey1 = item.ItemKey1,
                            ItemKey2 = item.ItemKey2,
                            ItemQuantity = item.Quality,
                        };
                        Current.SetTask(taskModel);
                    }
                }
                else if (objTask is PlotTask)
                {
                    CompleteAct();
                }
                if(Current.TaskQueue.Count>0)
                {
                    Current.TaskQueue.Dequeue();
                }else if (Current.ItemBar.IsCommplete)
                {
                    HeadingToMarketTask taskModel = new HeadingToMarketTask()
                    {
                        Duration = 4,
                    };
                    Current.SetTask(taskModel);
                }
                else if (!(objTask is KillTask) && !(objTask is HeadingToMarketTask))
                {
                    //金币够买装备
                    if (Current.GetGold() > CharacterHelper.EquipmentPrice(Current.Level))
                    {
                        BuyTask taskModel = new BuyTask()
                        {
                            Duration = 5,
                        };
                        Current.SetTask(taskModel);
                    }
                    else
                    {
                        HeadingToMarketTask taskModel = new HeadingToMarketTask()
                        {
                            Duration = 5,
                        };
                        Current.SetTask(taskModel);
                    }
                }
                else
                {
                    Current.SetTask(
                        BuilderKillMonster(Current.Level
                        , Current.QuestBook.MonsterKey
                        , Current.QuestBook.MonsterLevel));
                }
            }
        }

        /// <summary>
        /// 构建怪物
        /// </summary>
        /// <param name="characterLevel"></param>
        /// <param name="monsterKey"></param>
        /// <param name="monsterLevel"></param>
        /// <returns></returns>
        KillTask BuilderKillMonster(int characterLevel, string monsterKey, int monsterLevel)
        {
            KillTask taskModel = new KillTask();
            int level = characterLevel;
            int newLevel = 0;
            bool isNPC = false;
            bool isDefinite = false;
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
                    isNPC = true;
                    taskModel.MonsterKey = "";
                    taskModel.NPCClassKey = Repository.Classes.Pick().Key;
                }
                else
                {
                    isNPC = false;
                    taskModel.TitleKey = Repository.Titles.Pick().Key;
                    taskModel.MonsterName = CharacterHelper.GenerateEnglishName();
                    isDefinite = true;
                }
                newLevel = level;
            }
            else if (!string.IsNullOrWhiteSpace(monsterKey) && RandomHelper.Odds(1, 4))
            {
                //使用任务中的怪物
                isNPC = false;
                taskModel.MonsterKey = monsterKey;
                newLevel = monsterLevel;
            }
            else
            {
                //从最接近我们想要的水平的随机怪物中挑选怪物
                var monster = UnNamedMonster(level);
                taskModel.MonsterKey = monster.Key;
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

            if (level - newLevel <= -10)
            {
                //imaginary

            }
            else if (level - newLevel < -5)
            {
                int i = 10 + level - newLevel;
                i = 5 - RandomHelper.Value(i + 1);
                //TODO:YOUNG
            }
            else if (level - newLevel < 0)
            {
                //TODO:YOUNG
            }
            else if (level - newLevel >= 10)
            {
                //TOD:Messianic
            }
            else if (level - newLevel > 5)
            {
                int i = 10 - (level - newLevel);
                i = 5 - RandomHelper.Value(i + 1);
                //TODO:Big Special
            }
            else if (level - newLevel > 0 && RandomHelper.Value(2) == 1)
            {
                //TODO:Big
            }
            else if (level - newLevel > 0)
            {
                //TODO:Special
            }
            newLevel = level;
            level = newLevel * qty;
            if (!isDefinite)
            {
                //TODO:复数
            }
            taskModel.IsNPC = isNPC;
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

        /// <summary>
        /// 插曲电影
        /// </summary>
        private void InterPlotCinematic()
        {
            int choice = RandomHelper.Value(3);
            string monsterName = "";
            switch (choice)
            {
                case 1:
                    //TODO:常规任务键值添加
                    //Exhausted, you arrive at a friendly oasis in a hostile land
                    Current.SetTask(
                        new RegularTask
                        {
                            Key = "TaskRegularExhausted",
                            Duration = 1,
                        });
                    //You greet old friends and meet new allies
                    Current.SetTask(
                        new RegularTask
                        {
                            Key = "TaskRegularGreet",
                            Duration = 2,
                        });
                    //You are privy to a council of powerful do-gooders
                    Current.SetTask(
                        new RegularTask
                        {
                            Key = "TaskRegularCouncil",
                            Duration = 2,
                        });
                    //There is much to be done. You are chosen!
                    Current.SetTask(
                        new RegularTask
                        {
                            Key = "TaskRegularChosen",
                            Duration = 2,
                        });
                    break;
                case 2:
                    //Your quarry is in sight, but a mighty enemy bars your path!
                    Current.SetTask(
                       new RegularTask
                       {
                           Key = "TaskRegularSight",
                           Duration = 2,
                       });
                    //遇到克星
                    MonsterModel nemesis = UnNamedMonster(Current.Level + 3);
                    //A desperate struggle commences with {nemesis}
                    monsterName = CharacterHelper.GenerateEnglishName();
                    Current.SetTask(
                       new RegularTask
                       {
                           Key = "TaskRegularNemesis",
                           MonsterKey = nemesis.Key,
                           MonsterName = monsterName,
                           Duration = 4,
                       });
                    int s = RandomHelper.Value(3);
                    for (int i = 0; i < RandomHelper.Value(1 + Current.QuestBook.ActIndex + 1); i++)
                    {
                        s += 1 + RandomHelper.Value(2);
                        switch (s % 3)
                        {
                            case 0:
                                //Locked in grim combat with {nemesis}
                                Current.SetTask(
                                   new RegularTask
                                   {
                                       Key = "TaskRegularLockedInNemesis",
                                       MonsterKey = nemesis.Key,
                                       MonsterName = monsterName,
                                       Duration = 4,
                                   });
                                break;
                            case 1:
                                //{nemesis} seems to have the upper hand
                                Current.SetTask(
                                   new RegularTask
                                   {
                                       Key = "TaskRegularNemesisUpperHand",
                                       MonsterKey = nemesis.Key,
                                       MonsterName = monsterName,
                                       Duration = 4,
                                   });
                                break;
                            case 2:
                                //You seem to gain the advantage over {nemesis}
                                Current.SetTask(
                                   new RegularTask
                                   {
                                       Key = "TaskRegularAdvantageOverNemesis",
                                       MonsterKey = nemesis.Key,
                                       MonsterName = monsterName,
                                       Duration = 4,
                                   });
                                break;
                        }
                    }
                    //Victory! {nemesis} is slain!Exhausted, you lose conciousness
                    Current.SetTask(
                                   new RegularTask
                                   {
                                       Key = "TaskRegularLoseConciousness",
                                       MonsterKey = nemesis.Key,
                                       MonsterName = monsterName,
                                       Duration = 4,
                                   });
                    //You awake in a friendly place, but the road awaits
                    Current.SetTask(
                                  new RegularTask
                                  {
                                      Key = "TaskRegularFriendlyPlace",
                                      MonsterName = monsterName,
                                      Duration = 4,
                                  });
                    break;
                case 3:
                    string impressiveTitleKey = Repository.ImpressiveTitles.Pick().Key;
                    string raceKey = Repository.Races.Pick().Key;
                    if (RandomHelper.Value(2) == 2)
                        monsterName = CharacterHelper.GenerateEnglishName();
                    //Oh sweet relief!You've reached the protection of the good{nemesis}
                    Current.SetTask(
                       new RegularTask
                       {
                           Key = "TaskRegularRelief",
                           Duration = 2,
                       });
                    //There is rejoicing,and an unnerving encouter with {nemesis} in private
                    Current.SetTask(
                       new RegularTask
                       {
                           Key = "TaskRegular",
                           Duration = 3,
                       });
                    //You forget your {boring_item()} and go back to get it
                    Current.SetTask(
                       new RegularTask
                       {
                           Key = "TaskRegular",
                           Duration = 2,
                       });
                    //What's this!? You overhear something shocking!
                    Current.SetTask(
                       new RegularTask
                       {
                           Key = "TaskRegular",
                           Duration = 2,
                       });
                    //Could {nemesis} be a dirty double-dealer?
                    Current.SetTask(
                       new RegularTask
                       {
                           Key = "TaskRegular",
                           Duration = 2,
                       });
                    //Who can possibly be trusted with this news!? ...Oh yes, of course
                    Current.SetTask(
                       new RegularTask
                       {
                           Key = "TaskRegular",
                           Duration = 3,
                       });
                    break;
            }
            Current.SetTask(new PlotTask { ActIndex = Current.QuestBook.ActIndex + 1, Duration = 1 });
        }
        /// <summary>
        /// 完成剧幕
        /// </summary>
        private void CompleteAct()
        {
            Current.QuestBook.CommpleteAct(Current.QuestBook.ActIndex);
            RaisePropertyChanged(nameof(Acts));
            Current.QuestBook.AddAct(Current.QuestBook.ActIndex + 1);
            PlotBar.Reset(CharacterHelper.ActTime(Current.QuestBook.ActIndex));
            if (Current.QuestBook.ActIndex > 1)
            {
                WinItem();
                WinEquipment();
            }
            RaisePropertyChanged(nameof(Acts));
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
            RaisePropertyChanged(nameof(Current));
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
            //保存当前人物
            await _CacheService.SaveAsync(Current.Name, Current);
            return true;
        }
        #endregion
    }
}
