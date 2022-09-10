using Client.Common;
using Client.DataAccess;
using Client.Extensions;
using Client.Helpers;
using Client.Models;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Media;

namespace Client.ViewModels
{
    /// <summary>
    /// 新游戏视图模型
    /// </summary>
    public class NewGameViewModel : BaseViewModel
    {
        #region 成员(Member)
        #region 人物
        /// <summary>
        /// 人物实体
        /// </summary>
        private Character _Character = new Character()
        {
            Name = "",
            RaceKey = "",
            ClassKey = "",
            Level = 1,
            QuestBook = new CharacterQuestBook()
            {
                Acts = new ObservableCollection<CharacterAct>(),
                Quests = new ObservableCollection<CharacterQuest>(),
            },

            Stats = new ObservableCollection<CharacterStat>(),
            Equipments = new ObservableCollection<CharacterEquipment>(),
            SpellBooks = new ObservableCollection<CharacterSpellBook>(),
            Items = new ObservableCollection<CharacterItem>(),

            TaskQueue = new Queue<BaseTask>(),
        };
        #endregion

        #region 种族
        private RaceModel _CharacterRace;
        /// <summary>
        /// 种族
        /// </summary>
        public RaceModel CharacterRace
        {
            get => _CharacterRace;
            set
            {
                if (value != _CharacterRace)
                {
                    _CharacterRace = value;
                    _Character.RaceKey = _CharacterRace.Key;
                    RaisePropertyChanged(nameof(CharacterRace));
                    BeginQuestCommand.RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region 职业
        private ClassModel _CharacterClass;
        /// <summary>
        /// 职业
        /// </summary>
        public ClassModel CharacterClass
        {
            get => _CharacterClass;
            set
            {
                if (value != _CharacterClass)
                {
                    _CharacterClass = value;
                    _Character.ClassKey = _CharacterClass.Key;
                    RaisePropertyChanged(nameof(CharacterClass));
                    BeginQuestCommand.RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region 名字
        /// <summary>
        /// 名字
        /// </summary>
        public string CharacterName
        {
            get => _Character.Name;
            set
            {
                if (value != _Character.Name)
                {
                    _Character.Name = value;
                    RaisePropertyChanged(nameof(CharacterName));
                }
            }
        }
        #endregion

        #region 力量
        /// <summary>
        /// 力量
        /// </summary>
        public int CharacterStrength
        {
            get => GetStatValue(EnumStat.Strength);
            set
            {
                if (value != GetStatValue(EnumStat.Strength))
                {
                    SetStatValue(EnumStat.Strength, value);
                    RaisePropertyChanged(nameof(CharacterStrength));
                    RaisePropertyChanged(nameof(TotalStats));
                }
            }
        }
        #endregion

        #region 体质
        /// <summary>
        /// 体质
        /// </summary>
        public int CharacterConstitution
        {
            get => GetStatValue(EnumStat.Constitution);
            set
            {
                if (value != GetStatValue(EnumStat.Constitution))
                {
                    SetStatValue(EnumStat.Constitution, value);
                    RaisePropertyChanged(nameof(CharacterConstitution));
                    RaisePropertyChanged(nameof(TotalStats));
                }
            }
        }
        #endregion

        #region 敏捷
        /// <summary>
        /// 敏捷
        /// </summary>
        public int CharacterDexterity
        {
            get => GetStatValue(EnumStat.Dexterity);
            set
            {
                if (value != GetStatValue(EnumStat.Dexterity))
                {
                    SetStatValue(EnumStat.Dexterity, value);
                    RaisePropertyChanged(nameof(CharacterDexterity));
                    RaisePropertyChanged(nameof(TotalStats));
                    BeginQuestCommand.RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region 智力
        /// <summary>
        /// 智力
        /// </summary>
        public int CharacterIntelligence
        {
            get => GetStatValue(EnumStat.Intelligence);
            set
            {
                if (value != GetStatValue(EnumStat.Intelligence))
                {
                    SetStatValue(EnumStat.Intelligence, value);
                    RaisePropertyChanged(nameof(CharacterIntelligence));
                    RaisePropertyChanged(nameof(TotalStats));
                    BeginQuestCommand.RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region 智慧
        /// <summary>
        /// 智慧
        /// </summary>
        public int CharacterWisdom
        {
            get => GetStatValue(EnumStat.Wisdom);
            set
            {
                if (value != GetStatValue(EnumStat.Wisdom))
                {
                    SetStatValue(EnumStat.Wisdom, value);
                    RaisePropertyChanged(nameof(CharacterWisdom));
                    RaisePropertyChanged(nameof(TotalStats));
                    BeginQuestCommand.RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region 魅力
        /// <summary>
        /// 魅力
        /// </summary>
        public int CharacterCharisma
        {
            get => GetStatValue(EnumStat.Charisma);
            set
            {
                if (value != GetStatValue(EnumStat.Charisma))
                {
                    SetStatValue(EnumStat.Charisma, value);
                    RaisePropertyChanged(nameof(CharacterCharisma));
                    RaisePropertyChanged(nameof(TotalStats));
                    BeginQuestCommand.RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region 最大生命值
        /// <summary>
        /// 最大生命值
        /// </summary>
        public int CharacterHPMax
        {
            get => GetStatValue(EnumStat.HPMax);
            set
            {
                if (value != GetStatValue(EnumStat.HPMax))
                {
                    SetStatValue(EnumStat.HPMax, value);
                    RaisePropertyChanged(nameof(CharacterHPMax));
                    BeginQuestCommand.RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region 最大魔法值
        /// <summary>
        /// 最大魔法值
        /// </summary>
        public int CharacterMPMax
        {
            get => GetStatValue(EnumStat.MPMax);
            set
            {
                if (value != GetStatValue(EnumStat.MPMax))
                {
                    SetStatValue(EnumStat.MPMax, value);
                    RaisePropertyChanged(nameof(CharacterMPMax));
                    BeginQuestCommand.RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region 属性合计
        /// <summary>
        /// 属性合计
        /// </summary>
        public int TotalStats
        {
            get
            {
                var total = CharacterStrength + CharacterConstitution + CharacterDexterity + CharacterIntelligence + CharacterWisdom + CharacterCharisma;
                if (total >= (63 + 18))
                    TotalStatsBackground = new SolidColorBrush(Colors.Red);
                else if (total > (4 * 18))
                    TotalStatsBackground = new SolidColorBrush(Colors.Yellow);
                else if (total <= (63 - 18))
                    TotalStatsBackground = new SolidColorBrush(Colors.Green);
                else if (total < (3 * 18))
                    TotalStatsBackground = new SolidColorBrush(Colors.Silver);
                else
                    TotalStatsBackground = new SolidColorBrush(Colors.White);
                return total;
            }
        }
        #endregion

        #region 属性合集背景颜色
        /// <summary>
        /// 属性合集背景颜色
        /// </summary>
        private Brush _TotalStatsBackground;
        /// <summary>
        /// 属性合集背景颜色
        /// </summary>
        public Brush TotalStatsBackground
        {
            get
            {
                return _TotalStatsBackground;
            }
            set
            {
                _TotalStatsBackground = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 种族集合(Races)
        private ObservableCollection<RaceModel> _Races;
        /// <summary>
        /// 种族集合
        /// </summary>
        public ObservableCollection<RaceModel> Races
        {
            get
            {
                return _Races;
            }
            set
            {
                _Races = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 职业集合(Classes)
        private ObservableCollection<ClassModel> _Classes;
        /// <summary>
        /// 职业集合
        /// </summary>
        public ObservableCollection<ClassModel> Classes
        {
            get
            {
                return _Classes;
            }
            set
            {
                _Classes = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 已生成属性集合
        /// <summary>
        /// 已生成属性集合
        /// </summary>
        private Stack<RollStatModel> _RollStat = new Stack<RollStatModel>();
        #endregion

        #endregion

        #region 服务(Services)
        #endregion

        #region 命令(Commands)
        /// <summary>
        /// 新名字
        /// </summary>
        public DelegateCommand NewNameCommand { get; private set; }
        /// <summary>
        /// 随机生成属性
        /// </summary>
        public DelegateCommand RollStatCommand { get; private set; }
        /// <summary>
        /// 上一个生成的属性
        /// </summary>
        public DelegateCommand UnrollStatCommand { get; private set; }
        /// <summary>
        /// 开启旅程
        /// </summary>
        public DelegateCommand BeginQuestCommand { get; private set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 新游戏视图模型
        /// </summary>
        /// <param name="provider">容器提供者</param>
        public NewGameViewModel(IContainerProvider provider) : base(provider)
        {
            NewNameCommand = new DelegateCommand(RandomlySetCharacterName);
            RollStatCommand = new DelegateCommand(RollNewStatsForCharacter);
            UnrollStatCommand = new DelegateCommand(UnrollPreviousStatsForCharacter, CanUnrollStats);
            BeginQuestCommand = new DelegateCommand(BeginQuest, CanBeginQuest);
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
            InitData();
        }
        #endregion

        #region 方法(Methods)
        /// <summary>
        /// 初始化界面数据
        /// </summary>
        private void InitData()
        {
            #region 属性集合
            _Character.Stats.Clear();
            _Character.Stats.AddRange(new List<CharacterStat>
            {
                new CharacterStat(){StatType = EnumStat.Strength,Value = 0 },
                new CharacterStat(){StatType = EnumStat.Constitution,Value = 0 },
                new CharacterStat(){StatType = EnumStat.Dexterity,Value = 0 },
                new CharacterStat(){StatType = EnumStat.Intelligence,Value = 0 },
                new CharacterStat(){StatType = EnumStat.Wisdom,Value = 0 },
                new CharacterStat(){StatType = EnumStat.Charisma,Value = 0 },
                new CharacterStat(){StatType = EnumStat.HPMax,Value = 0 },
                new CharacterStat(){StatType = EnumStat.MPMax,Value = 0 },
            });
            #endregion

            #region 装备集合
            _Character.Equipments.Clear();
            _Character.Equipments.AddRange(new ObservableCollection<CharacterEquipment>
            {
                new CharacterEquipment(){EquipmentType = EnumEquipment.Weapon },
                new CharacterEquipment(){EquipmentType = EnumEquipment.Shield },
                new CharacterEquipment(){EquipmentType = EnumEquipment.Helm },
                new CharacterEquipment(){EquipmentType = EnumEquipment.Hauberk },
                new CharacterEquipment(){EquipmentType = EnumEquipment.Brassairts },
                new CharacterEquipment(){EquipmentType = EnumEquipment.Vambraces},
                new CharacterEquipment(){EquipmentType = EnumEquipment.Gauntlets },
                new CharacterEquipment(){EquipmentType = EnumEquipment.Gambeson },
                new CharacterEquipment(){EquipmentType = EnumEquipment.Cuisses },
                new CharacterEquipment(){EquipmentType = EnumEquipment.Greaves},
                new CharacterEquipment(){EquipmentType = EnumEquipment.Sollerets},
            });
            #endregion

            #region 货物集合
            //货物默认添加金币
            _Character.Items.Clear();
            _Character.Items.AddRange(new ObservableCollection<CharacterItem>()
            {
                new CharacterItem(){Key="DataGridGold",Quality = 0 },
            });
            #endregion

            Races = Repository.Races;
            Classes = Repository.Classes;

            RandomlySetCharacterName();
            SetCharacterStats(GenerateRandomCharacterStatSet());
            RandomlySetCharacterRace();
            RandomlySetCharacterClass();
        }
        /// <summary>
        /// 上一个设置人物属性按钮是否可用
        /// </summary>
        /// <returns></returns>
        private bool CanUnrollStats()
        {
            return _RollStat.Count > 0;
        }
        /// <summary>
        /// 是否可以开始任务
        /// </summary>
        /// <returns></returns>
        private bool CanBeginQuest()
        {
            return ValueFallsInRange(CharacterStrength)
                && ValueFallsInRange(CharacterConstitution)
                && ValueFallsInRange(CharacterDexterity)
                && ValueFallsInRange(CharacterIntelligence)
                && ValueFallsInRange(CharacterWisdom)
                && ValueFallsInRange(CharacterCharisma)
                && CharacterHPMax > 0
                && CharacterMPMax > 0;
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

        /// <summary>
        /// 随机设置人物名
        /// </summary>
        private void RandomlySetCharacterName()
        {
            CharacterName = CharacterHelper.GenerateName(ApplicationContext.Setting.CultureName);
        }
        /// <summary>
        /// 随机设置人物种族
        /// </summary>
        private void RandomlySetCharacterRace()
        {
            CharacterRace = Races.Pick();
        }
        /// <summary>
        /// 随机设置人物职业
        /// </summary>
        private void RandomlySetCharacterClass()
        {
            CharacterClass = Classes.Pick();
        }
        /// <summary>
        /// 新的人物属性
        /// </summary>
        private void RollNewStatsForCharacter()
        {
            RollStatModel model = new RollStatModel()
            {
                Strength = CharacterStrength,
                Constitution = CharacterConstitution,
                Dexterity = CharacterDexterity,
                Intelligence = CharacterIntelligence,
                Wisdom = CharacterWisdom,
                Charisma = CharacterCharisma,
                HPMax = CharacterHPMax,
                MPMax = CharacterMPMax,
            };
            _RollStat.Push(model);
            SetCharacterStats(GenerateRandomCharacterStatSet());
            UnrollStatCommand.RaiseCanExecuteChanged();
        }
        /// <summary>
        /// 上一个人物属性
        /// </summary>
        private void UnrollPreviousStatsForCharacter()
        {
            SetCharacterStats(_RollStat.Pop());
            UnrollStatCommand.RaiseCanExecuteChanged();
        }
        /// <summary>
        /// 随机生成人物属性
        /// </summary>
        /// <returns></returns>
        private RollStatModel GenerateRandomCharacterStatSet()
        {
            var stats = new RollStatModel
            {
                Strength = CharacterHelper.InitGeneralStat(),
                Constitution = CharacterHelper.InitGeneralStat(),
                Dexterity = CharacterHelper.InitGeneralStat(),
                Intelligence = CharacterHelper.InitGeneralStat(),
                Wisdom = CharacterHelper.InitGeneralStat(),
                Charisma = CharacterHelper.InitGeneralStat(),
            };

            stats.HPMax = CharacterHelper.InitMaxHPOrMP(stats.Constitution);
            stats.MPMax = CharacterHelper.InitMaxHPOrMP(stats.Intelligence);
            return stats;
        }
        /// <summary>
        /// 设置人物属性
        /// </summary>
        /// <param name="stats">人物属性</param>
        private void SetCharacterStats(RollStatModel stats)
        {
            CharacterStrength = stats.Strength;
            CharacterConstitution = stats.Constitution;
            CharacterDexterity = stats.Dexterity;
            CharacterIntelligence = stats.Intelligence;
            CharacterWisdom = stats.Wisdom;
            CharacterCharisma = stats.Charisma;
            CharacterHPMax = stats.HPMax;
            CharacterMPMax = stats.MPMax;
        }
        /// <summary>
        /// 开始任务
        /// </summary>
        private void BeginQuest()
        {
            _Character.BirthDay = DateTime.Now;
            _Character.IsOnLine = true;
            _Character.ItemBar = new ItemBarModel()
            {
                Position = 0,
                MaxValue = CharacterHelper.GetCapacity(GetStatValue(EnumStat.Strength))
            };
            _Character.ExperienceBar = new ExperienceBarModel()
            {
                Position = 0,
                MaxValue = CharacterHelper.GetMaxExperienceByLevel(_Character.Level)
            };
            _Character.PlotBar = new PlotBarModel()
            {
                Position = 0,
                MaxValue = 26,
            };
            _Character.QuestBar = new QuestBarModel()
            {
                Position = 0,
                MaxValue = 28
            };
            _Character.QuestBook.Acts.Add(new CharacterAct {Key = "DataGridPlotPrologue",Index = 0,IsCommplete = false });
            PlotTask plotPrologue = new PlotTask() { Key = "TaskPlot", ActIndex = _Character.QuestBook.ActIndex, Duration = 2 };
            _Character.CurrentBar = new CurrentBarModel()
            {
                TaskType = EnumTask.Plot,
                ToolTip = plotPrologue.Description,
                Position = 0,
                MaxValue = plotPrologue.Duration,
            };
            _Character.TaskQueue.Enqueue(new RegularTask() { Key = "RegularTaskNightVision", Duration = 4 });
            _Character.TaskQueue.Enqueue(new RegularTask() { Key = "RegularTaskUnderestimated", Duration = 6 });
            _Character.TaskQueue.Enqueue(new RegularTask() { Key = "RegularTaskEvents", Duration = 6 });
            _Character.TaskQueue.Enqueue(new RegularTask() { Key = "RegularTaskJourney", Duration = 10 });
            _Character.TaskQueue.Enqueue(plotPrologue);

            NavigationParameters param = new NavigationParameters();
            param.Add("Character", _Character);
            NavigationToView("GameView", param);
        }
        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="statType">属性类型</param>
        /// <returns>属性值</returns>
        /// <exception cref="Exception"></exception>
        public int GetStatValue(EnumStat statType)
        {
            return _Character.Stats.SingleOrDefault(item => item.StatType == statType).Value;
        }
        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="statType">属性类型</param>
        /// <param name="statValue"></param>
        /// <returns>是否设置成功</returns>
        /// <exception cref="Exception"></exception>
        public bool SetStatValue(EnumStat statType, int statValue)
        {
            var stat = _Character.Stats.SingleOrDefault(item => item.StatType == statType);
            stat.Value = statValue;
            return true;
        }

        #endregion
    }
}
