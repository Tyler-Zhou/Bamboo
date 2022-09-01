using Client.Extensions;
using Client.Interfaces;
using Client.Models;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
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
            if (CurrentCharacter == null)
                CurrentCharacter = new Character();
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
        #endregion
    }
}
