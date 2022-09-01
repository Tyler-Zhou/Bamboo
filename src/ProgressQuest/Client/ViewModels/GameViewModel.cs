using Client.DataAccess;
using Client.Extensions;
using Client.Helpers;
using Client.Interfaces;
using Client.Models;
using Prism.Ioc;
using Prism.Regions;
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
        #region 内容是否可见
        /// <summary>
        /// 内容是否可见
        /// </summary>
        public bool ContentVisible
        {
            get
            {
                if (CurrentCharacter == null || string.IsNullOrWhiteSpace(CurrentCharacter.Name))
                    return false;
                return true;
            }
        } 
        #endregion

        #region 人物
        /// <summary>
        /// 人物
        /// </summary>
        private Character _Character;
        /// <summary>
        /// 人物
        /// </summary>
        public Character CurrentCharacter
        {
            get => _Character;
            set
            {
                if (value != null)
                {
                    _Character = value;
                    RaisePropertyChanged(nameof(CurrentCharacter));
                    RaisePropertyChanged(nameof(ContentVisible));
                    Traits = _Character.Traits();
                    RaisePropertyChanged(nameof(Traits));
                    Stats = _Character.Stats();
                    RaisePropertyChanged(nameof(Stats));
                    Equipments = _Character.Equipments();
                    RaisePropertyChanged(nameof(Equipments));
                    SetProgressBarExperience(_Character.ExpTask.Position);
                    RaisePropertyChanged(nameof(ExpTaskToolTip));
                    SetProgressBarInventory(_Character.InventoryTask.Position);
                    RaisePropertyChanged(nameof(InventoryTaskToolTip));
                }
            }
        }
        #endregion

        #region 特征集合
        ObservableCollection<TraitModel> _Traits;
        /// <summary>
        /// 特征集合
        /// </summary>
        public ObservableCollection<TraitModel> Traits
        {
            get => _Traits;
            set
            {
                _Traits = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 属性集合
        ObservableCollection<StatModel> _Stats;
        /// <summary>
        /// 属性集合
        /// </summary>
        public ObservableCollection<StatModel> Stats
        {
            get => _Stats;
            set
            {
                _Stats = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 法术书
        /// <summary>
        /// 法术书
        /// </summary>
        public ObservableCollection<SpellBookModel> SpellBooks
        {
            get => _Character.SpellBooks;
            set
            {
                _Character.SpellBooks = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 装备
        ObservableCollection<EquipmentModel> _Equipments;
        /// <summary>
        /// 装备
        /// </summary>
        public ObservableCollection<EquipmentModel> Equipments
        {
            get => _Equipments;
            set
            {
                _Equipments = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 详细目录
        /// <summary>
        /// 详细目录
        /// </summary>
        public ObservableCollection<InventoryModel> Inventorys
        {
            get => _Character.Inventorys;
            set
            {
                if (value != _Character.Inventorys)
                {
                    _Character.Inventorys = value;
                    RaisePropertyChanged(nameof(Inventorys));
                }
            }
        }
        #endregion

        #region 进度条(ProgressBar)

        #region 经验
        /// <summary>
        /// 经验当前位置
        /// </summary>
        public int ExpTaskPosition
        {
            get => CurrentCharacter.ExpTask.Position;
            set
            {
                CurrentCharacter.ExpTask.Position = value;
                RaisePropertyChanged(nameof(ExpTaskPosition));
                RaisePropertyChanged(nameof(ExpTaskToolTip));
            }
        }

        /// <summary>
        /// 经验最大值
        /// </summary>
        public int ExpTaskMax
        {
            get => CurrentCharacter.ExpTask.MaxValue;
            set
            {
                CurrentCharacter.ExpTask.MaxValue = value;
                RaisePropertyChanged(nameof(ExpTaskMax));
                RaisePropertyChanged(nameof(ExpTaskToolTip));
            }
        }

        /// <summary>
        /// 悬浮提示
        /// </summary>
        public string ExpTaskToolTip
        {
            get => CurrentCharacter.ExpTask.Name;
        }
        #endregion

        #region 当前

        #endregion

        #region 目录(负重)
        /// <summary>
        /// 目录当前位置
        /// </summary>
        public int InventoryTaskPosition
        {
            get => CurrentCharacter.InventoryTask.Position;
            set
            {
                CurrentCharacter.InventoryTask.Position = value;
                RaisePropertyChanged(nameof(InventoryTaskPosition));
                RaisePropertyChanged(nameof(InventoryTaskToolTip));
            }
        }

        /// <summary>
        /// 目录最大值
        /// </summary>
        public int InventoryTaskMax
        {
            get => CurrentCharacter.InventoryTask.MaxValue;
            set
            {
                CurrentCharacter.InventoryTask.MaxValue = value;
                RaisePropertyChanged(nameof(InventoryTaskMax));
                RaisePropertyChanged(nameof(InventoryTaskToolTip));
            }
        }

        /// <summary>
        /// 悬浮提示
        /// </summary>
        public string InventoryTaskToolTip
        {
            get => CurrentCharacter.InventoryTask.Name;
        }
        #endregion
        #endregion
        #endregion

        #region 服务(Service)
        /// <summary>
        /// 缓存服务
        /// </summary>
        ICacheService _CacheService;
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
        public GameViewModel(IContainerProvider provider, ICacheService cacheService, IWindowService windowService) : base(provider)
        {
            _CacheService = cacheService;
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
            if (CurrentCharacter == null)
            {
                CurrentCharacter = new Character();
                //SetProgressBarExperience(0);
                //SetProgressBarInventory(0);
            }
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
            if (CurrentCharacter != null)
            {
                //过滤同名人物操作
                if (character.Equals(CurrentCharacter.Name))
                    return false;
                //当前人物不为空
                if(!string.IsNullOrWhiteSpace(CurrentCharacter.Name))
                {
                    await SaveCharacter();
                    //重置人物信息
                    CurrentCharacter = null;
                }
            }
            //获取存档
            Character archiveCharacter = await _CacheService.GetAsync<Character>(character.Name);
            if (archiveCharacter != null)
            {
                CurrentCharacter = archiveCharacter;
            }
            else
            {
                CurrentCharacter = character;
                await SaveCharacter();
            }
            return true;
        }
        /// <summary>
        /// 保存当前人物
        /// </summary>
        async Task<bool> SaveCharacter()
        {
            if (CurrentCharacter == null || string.IsNullOrWhiteSpace(CurrentCharacter.Name))
                return false;
            //保存当前人物
            await _CacheService.SaveAsync(CurrentCharacter.Name, CurrentCharacter);
            return true;
        }

        /// <summary>
        /// 设置经验进度条
        /// </summary>
        void SetProgressBarExperience(int position)
        {
            CurrentCharacter.ExpTask.Position = position;
            CurrentCharacter.ExpTask.MaxValue = CharacterHelper.GetMaxExperienceByLevel(CurrentCharacter.Level);
        }
        /// <summary>
        /// 设置详细目录进度条
        /// </summary>
        void SetProgressBarInventory(int position)
        {
            CurrentCharacter.InventoryTask.Position = position;
            CurrentCharacter.InventoryTask.MaxValue = CurrentCharacter.Strength + 10;
        }
        #endregion
    }
}
