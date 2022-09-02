using Client.Common;
using Client.DataAccess;
using Client.Enums;
using Client.Helpers;
using Client.Models;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace Client.ViewModels
{
    /// <summary>
    /// 新游戏视图模型
    /// </summary>
    public class NewGameViewModel: BaseViewModel
    {
        #region 成员(Member)
        #region 人物
        /// <summary>
        /// 人物实体
        /// </summary>
        private Character _Character = new Character();
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
            get => _Character.GetStatValue(EnumStat.Strength);
            set
            {
                if (value != _Character.GetStatValue(EnumStat.Strength))
                {
                    _Character.SetStatValue(EnumStat.Strength,value,true);
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
            get => _Character.GetStatValue(EnumStat.Constitution);
            set
            {
                if (value != _Character.GetStatValue(EnumStat.Constitution))
                {
                    _Character.SetStatValue(EnumStat.Constitution, value, true);
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
            get => _Character.GetStatValue(EnumStat.Dexterity);
            set
            {
                if (value != _Character.GetStatValue(EnumStat.Dexterity))
                {
                    _Character.SetStatValue(EnumStat.Dexterity, value,true);
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
            get => _Character.GetStatValue(EnumStat.Intelligence);
            set
            {
                if (value != _Character.GetStatValue(EnumStat.Intelligence))
                {
                    _Character.SetStatValue(EnumStat.Intelligence, value, true);
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
            get => _Character.GetStatValue(EnumStat.Wisdom);
            set
            {
                if (value != _Character.GetStatValue(EnumStat.Wisdom))
                {
                    _Character.SetStatValue(EnumStat.Wisdom, value, true);
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
            get => _Character.GetStatValue(EnumStat.Charisma);
            set
            {
                if (value != _Character.GetStatValue(EnumStat.Charisma))
                {
                    _Character.SetStatValue(EnumStat.Charisma, value, true);
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
            get => _Character.GetStatValue(EnumStat.HPMax);
            set
            {
                if (value != _Character.GetStatValue(EnumStat.HPMax))
                {
                    _Character.SetStatValue(EnumStat.HPMax, value, true);
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
            get => _Character.GetStatValue(EnumStat.MPMax);
            set
            {
                if (value != _Character.GetStatValue(EnumStat.MPMax))
                {
                    _Character.SetStatValue(EnumStat.MPMax, value, true);
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
            _Character.InitData();
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
            var raceCount=Races.Count();
            CharacterRace = Races[RandomHelper.Value(raceCount)];
        }
        /// <summary>
        /// 随机设置人物职业
        /// </summary>
        private void RandomlySetCharacterClass()
        {
            var classCount = Classes.Count();
            CharacterClass = Classes[RandomHelper.Value(classCount)];
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
            NavigationParameters param = new NavigationParameters();
            param.Add("Character", _Character);
            NavigationToView("GameView", param);
        }
        #endregion
    }
}
