using Client.Extensions;
using Client.Interfaces;
using Client.Models;
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
                    RaisePropertyChanged(nameof(Traits));
                    RaisePropertyChanged(nameof(Stats));
                    RaisePropertyChanged(nameof(Equipments));
                    RaisePropertyChanged(nameof(SpellBooks));
                    RaisePropertyChanged(nameof(Items));
                    RaisePropertyChanged(nameof(Acts));

                    RaisePropertyChanged(nameof(ExpTask));
                    RaisePropertyChanged(nameof(InventoryTask));
                    RaisePropertyChanged(nameof(PlotTask));
                    RaisePropertyChanged(nameof(QuestTask));
                    RaisePropertyChanged(nameof(CurrentTask));
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

        #region 装备集合
        /// <summary>
        /// 装备集合
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

        #region 货物集合
        /// <summary>
        /// 货物集合
        /// </summary>
        public ObservableCollection<CharacterItem> Items
        {
            get => Current.Items;
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
        public ProgressRateItem InventoryTask
        {
            get => Current.ItemTask;
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
        /// <summary>
        /// 当前
        /// </summary>
        public ProgressRateCurrent CurrentTask
        {
            get => Current.CurrentTask;
        }
        #endregion

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
                    Current.LevelUp();
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
            taskTimer.Interval = TimeSpan.FromSeconds(0.1);
            taskTimer.Tick += TaskTimer_Tick;
            taskTimer.Start();
        }

        private void TaskTimer_Tick(object sender, EventArgs e)
        {
            CurrentTask.Increment(1);
            if(CurrentTask.IsCommplete)
            {
                CurrentTask.Reset(30);
            }
            RaisePropertyChanged(nameof(CurrentTask));
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
