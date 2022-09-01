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
            var result = Task.Run(() => InitData().Result).Result;
        }
        #endregion

        #region 方法(Method)
        async Task<bool> InitData()
        {
            string basePath=await _CacheService.GetSavePathAsync();
            DirectoryInfo dir = new DirectoryInfo(basePath);
            FileInfo[] fis = dir.GetFiles();
            for (int i = 0; i < fis.Length; i++)
            {
                FileInfo fi = fis[i];
                string name = fi.Name.Replace(fi.Extension,"");
                Character character=await _CacheService.GetAsync<Character>(name);
                Characters.Add(character);
            }
            return true;
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
