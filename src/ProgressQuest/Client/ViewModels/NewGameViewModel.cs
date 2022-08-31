using Client.Common;
using Client.DataAccess;
using Client.Enums;
using Client.Helpers;
using Client.Interfaces;
using Client.Models;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using System;
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
        /// <summary>
        /// 人物实体
        /// </summary>
        private Character _newCharacter = new Character();
        /// <summary>
        /// 人物属性
        /// </summary>
        private Stack<CharacterStats> _previousCharacterStats = new Stack<CharacterStats>();

        #region 种族(Race)
        private RaceModel _Race;
        /// <summary>
        /// 种族
        /// </summary>
        public RaceModel Race
        {
            get
            {
                return _Race;
            }
            set
            {
                _Race = value;
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

        #region 职业(Class)
        private ClassModel _Class;
        /// <summary>
        /// 职业
        /// </summary>
        public ClassModel Class
        {
            get
            {
                return _Class;
            }
            set
            {
                _Class = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 职业集合(Races)
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

        public string CharacterName
        {
            get => _newCharacter.Name;
            set
            {
                if (value != _newCharacter.Name)
                {
                    _newCharacter.Name = value;
                    RaisePropertyChanged(nameof(CharacterName));
                }
            }
        }

        public int CharacterStrength
        {
            get => _newCharacter.CharacterStats.Strength;
            set
            {
                if (value != _newCharacter.CharacterStats.Strength)
                {
                    _newCharacter.CharacterStats.Strength = value;
                    RaisePropertyChanged(nameof(CharacterStrength));
                    RaisePropertyChanged(nameof(TotalStats));
                }
            }
        }

        public int CharacterConstitution
        {
            get => _newCharacter.CharacterStats.Constitution;
            set
            {
                if (value != _newCharacter.CharacterStats.Constitution)
                {
                    _newCharacter.CharacterStats.Constitution = value;
                    RaisePropertyChanged(nameof(CharacterConstitution));
                    RaisePropertyChanged(nameof(TotalStats));
                }
            }
        }

        public int CharacterDexterity
        {
            get => _newCharacter.CharacterStats.Dexterity;
            set
            {
                if (value != _newCharacter.CharacterStats.Dexterity)
                {
                    _newCharacter.CharacterStats.Dexterity = value;
                    RaisePropertyChanged(nameof(CharacterDexterity));
                    RaisePropertyChanged(nameof(TotalStats));
                    BeginQuestCmd.RaiseCanExecuteChanged();
                }
            }
        }

        public int CharacterIntelligence
        {
            get => _newCharacter.CharacterStats.Intelligence;
            set
            {
                if (value != _newCharacter.CharacterStats.Intelligence)
                {
                    _newCharacter.CharacterStats.Intelligence = value;
                    RaisePropertyChanged(nameof(CharacterIntelligence));
                    RaisePropertyChanged(nameof(TotalStats));
                    BeginQuestCmd.RaiseCanExecuteChanged();
                }
            }
        }

        public int CharacterWisdom
        {
            get => _newCharacter.CharacterStats.Wisdom;
            set
            {
                if (value != _newCharacter.CharacterStats.Wisdom)
                {
                    _newCharacter.CharacterStats.Wisdom = value;
                    RaisePropertyChanged(nameof(CharacterWisdom));
                    RaisePropertyChanged(nameof(TotalStats));
                    BeginQuestCmd.RaiseCanExecuteChanged();
                }
            }
        }

        public int CharacterCharisma
        {
            get => _newCharacter.CharacterStats.Charisma;
            set
            {
                if (value != _newCharacter.CharacterStats.Charisma)
                {
                    _newCharacter.CharacterStats.Charisma = value;
                    RaisePropertyChanged(nameof(CharacterCharisma));
                    RaisePropertyChanged(nameof(TotalStats));
                    BeginQuestCmd.RaiseCanExecuteChanged();
                }
            }
        }

        public RaceModel CharacterRace
        {
            get => _newCharacter.Race;
            set
            {
                if (value != _newCharacter.Race)
                {
                    _newCharacter.Race = value;
                    RaisePropertyChanged(nameof(CharacterRace));
                    BeginQuestCmd.RaiseCanExecuteChanged();
                }
            }
        }
        public ClassModel CharacterClass
        {
            get => _newCharacter.Class;
            set
            {
                if (value != _newCharacter.Class)
                {
                    _newCharacter.Class = value;
                    RaisePropertyChanged(nameof(CharacterClass));
                    BeginQuestCmd.RaiseCanExecuteChanged();
                }
            }
        }
        #region 用户名
        /// <summary>
        /// UserName
        /// </summary>
        private Brush _TotalStatsBackground;
        /// <summary>
        /// 用户名
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

        public int TotalStats
        {
            get 
            {
                var total = CharacterStrength + CharacterConstitution + CharacterDexterity + CharacterIntelligence + CharacterWisdom + CharacterCharisma;
                if (total >= (63 + 18))
                    TotalStatsBackground = new SolidColorBrush(Colors.Red);
                else if(total > (4 * 18))
                    TotalStatsBackground = new SolidColorBrush(Colors.Yellow);
                else if(total <= (63 - 18))
                    TotalStatsBackground = new SolidColorBrush(Colors.Green);
                else if(total < (3 * 18))
                    TotalStatsBackground = new SolidColorBrush(Colors.Silver);
                else
                    TotalStatsBackground = new SolidColorBrush(Colors.White);
                return total;
            }
        }

        
        #endregion

        #region 服务(Services)
        /// <summary>
        /// 
        /// </summary>
        IRepository _Repository;
        #endregion

        #region 命令(Commands)
        public DelegateCommand BeginQuestCmd { get; set; }
        public DelegateCommand RollStats { get; set; }
        public DelegateCommand UnrollStats { get; set; }
        public DelegateCommand GetNewCharacterName { get; set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 新游戏视图模型
        /// </summary>
        /// <param name="provider">容器提供者</param>
        public NewGameViewModel(IContainerProvider provider, IRepository repository) : base(provider)
        {
            _Repository = repository;
            BeginQuestCmd = new DelegateCommand(BeginQuest, CanBeginQuest);
            RollStats = new DelegateCommand(RollNewStatsForCharacter);
            UnrollStats = new DelegateCommand(UnrollPreviousStatsForCharacter, CanUnrollStats);
            GetNewCharacterName = new DelegateCommand(RandomlySetCharacterName);

            Races = _Repository.GetAllRace();
            Classes = _Repository.GetAllClass();

            RandomlySetCharacterName();
            SetCharacterStats(GenerateRandomCharacterStatSet());
            RandomlySetCharacterRace();
            RandomlySetCharacterClass();


        }
        #endregion

        #region 方法(Methods)
        /// <summary>
        /// 上一个设置人物属性按钮是否可用
        /// </summary>
        /// <returns></returns>
        private bool CanUnrollStats()
        {
            return _previousCharacterStats.Count > 0;
        }
        /// <summary>
        /// 是否可以开始任务
        /// </summary>
        /// <returns></returns>
        private bool CanBeginQuest()
        {
            return _newCharacter?.IsValid ?? false;
        }
        /// <summary>
        /// 随机设置人物名
        /// </summary>
        private void RandomlySetCharacterName()
        {
            CharacterName = NameHelper.GenerateName(ApplicationContext.Setting.CultureName);
        }
        /// <summary>
        /// 随机设置人物种族
        /// </summary>
        private void RandomlySetCharacterRace()
        {
            var raceCount=Races.Count();
            CharacterRace = Race = Races[(new Random()).Next(1, raceCount)];
        }
        /// <summary>
        /// 随机设置人物职业
        /// </summary>
        private void RandomlySetCharacterClass()
        {
            var raceCount = Classes.Count();
            CharacterClass = Class = Classes[(new Random()).Next(1, raceCount)];
        }
        /// <summary>
        /// 新的人物属性
        /// </summary>
        private void RollNewStatsForCharacter()
        {
            _previousCharacterStats.Push(_newCharacter.CharacterStats);
            SetCharacterStats(GenerateRandomCharacterStatSet());
            UnrollStats.RaiseCanExecuteChanged();
        }
        /// <summary>
        /// 上一个人物属性
        /// </summary>
        private void UnrollPreviousStatsForCharacter()
        {
            SetCharacterStats(_previousCharacterStats.Pop());
            UnrollStats.RaiseCanExecuteChanged();
        }
        /// <summary>
        /// 随机生成人物属性
        /// </summary>
        /// <returns></returns>
        private CharacterStats GenerateRandomCharacterStatSet()
        {
            Random statGenerator = new Random();
            var stats = new CharacterStats { Strength = statGenerator.Next(1, 16), Constitution = statGenerator.Next(1, 16), Dexterity = statGenerator.Next(1, 16), Intelligence = statGenerator.Next(1, 16), Wisdom = statGenerator.Next(1, 16), Charisma = statGenerator.Next(1, 16), HpMax = statGenerator.Next(1, 16), MpMax = statGenerator.Next(1, 16) };
            return stats;
        }
        /// <summary>
        /// 设置人物属性
        /// </summary>
        /// <param name="stats">人物属性</param>
        private void SetCharacterStats(CharacterStats stats)
        {
            _newCharacter.CharacterStats = stats;

            RaisePropertyChanged(nameof(CharacterStrength));
            RaisePropertyChanged(nameof(CharacterConstitution));
            RaisePropertyChanged(nameof(CharacterDexterity));
            RaisePropertyChanged(nameof(CharacterIntelligence));
            RaisePropertyChanged(nameof(CharacterWisdom));
            RaisePropertyChanged(nameof(CharacterCharisma));
            RaisePropertyChanged(nameof(TotalStats));
            BeginQuestCmd.RaiseCanExecuteChanged();
        }
        /// <summary>
        /// 开始任务
        /// </summary>
        private void BeginQuest()
        {
            //_newCharacter.Save();
            NavigationParameters param = new NavigationParameters();
            param.Add("NewCharacter", _newCharacter);
            NavigationToView("GameView", param);
        }
        #endregion
    }
}
