using Client.Interfaces;
using Client.Models;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    /// <summary>
    /// 存档视图模型
    /// </summary>
    public class ArchiveViewModel : BaseViewModel
    {
        #region 成员(Member)

        #region 无数据是否可见
        /// <summary>
        /// 无数据否可见
        /// </summary>
        public bool NoDataVisible
        {
            get
            {
                if (_Characters.Count > 0)
                    return false;
                return true;
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
                if (_Characters.Count <= 0)
                    return false;
                return true;
            }
        }
        #endregion

        #region 人物集合(Characters)
        private ObservableCollection<Character> _Characters = new ObservableCollection<Character>();
        /// <summary>
        /// 人物集合
        /// </summary>
        public ObservableCollection<Character> Characters
        {
            get
            {
                return _Characters;
            }
            set
            {
                _Characters = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #endregion

        #region 服务(Service)
        /// <summary>
        /// 缓存服务
        /// </summary>
        ICacheService _CacheService;
        #endregion

        #region 命令(Command)
        /// <summary>
        /// 加载存档命令
        /// </summary>
        public DelegateCommand<Character> LoadArchiveCommand { get; private set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerProvider"></param>
        /// <param name="cacheService"></param>
        public ArchiveViewModel(IContainerProvider containerProvider, ICacheService cacheService) : base(containerProvider)
        {
            _CacheService = cacheService;
            LoadArchiveCommand = new DelegateCommand<Character>(LoadArchive);
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

        #region 方法(Method)
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <returns></returns>
        bool InitData()
        {
            Characters.Clear();
            var result = Task.Run(() => GetCharactersAsync().Result).Result;
            Characters.AddRange(result);
            RaisePropertyChanged(nameof(NoDataVisible));
            RaisePropertyChanged(nameof(ContentVisible));
            return true;
        }
        /// <summary>
        /// 获取所有人物信息
        /// </summary>
        /// <returns></returns>
        async Task<ObservableCollection<Character>> GetCharactersAsync()
        {
            ObservableCollection<Character> result = new ObservableCollection<Character>();
            string basePath = await _CacheService.GetSavePathAsync();
            DirectoryInfo dir = new DirectoryInfo(basePath);
            FileInfo[] fis = dir.GetFiles();
            for (int i = 0; i < fis.Length; i++)
            {
                FileInfo fi = fis[i];
                string name = fi.Name.Replace(fi.Extension, "");
                Character character = await _CacheService.GetAsync<Character>(name);
                if(character!=null && !character.IsOnLine)
                    result.Add(character);
            }
            return result;
        }

        /// <summary>
        /// 加载存档
        /// </summary>
        /// <param name="obj"></param>
        private void LoadArchive(Character obj)
        {
            NavigationParameters param = new NavigationParameters();
            param.Add("Character", obj);
            NavigationToView("GameView", param);
        }
        #endregion
    }
}
