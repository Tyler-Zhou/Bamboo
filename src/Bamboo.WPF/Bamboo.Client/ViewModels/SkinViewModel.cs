using Bamboo.Client.Core.Common;
using Bamboo.Client.Interface;
using MaterialDesignColors;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Windows.Media;

namespace Bamboo.Client.ViewModels
{
    /// <summary>
    /// 皮肤视图模型
    /// </summary>
    public class SkinViewModel : BindableBase
    {
        #region 是否深色模式
        /// <summary>
        /// IsDarkTheme
        /// </summary>
        private bool _isDarkTheme;
        /// <summary>
        /// 是否深色模式
        /// </summary>
        public bool IsDarkTheme
        {
            get
            {
                _isDarkTheme = ApplicationContext.IsDarkTheme;
                return _isDarkTheme;
            }
            set
            {
                if (SetProperty(ref _isDarkTheme, value))
                {
                    ApplicationContext.IsDarkTheme = value;
                    Configure.Current.Add("IsDarkTheme", ApplicationContext.IsDarkTheme);
                    _ConfigureService.ConfigureTheme();
                }
            }
        }
        #endregion

        #region 色板
        /// <summary>
        /// 色板
        /// </summary>
        public IEnumerable<ISwatch> Swatches { get; } = SwatchHelper.Swatches;
        #endregion

        #region 服务(Service)
        /// <summary>
        /// 配置服务
        /// </summary>
        private readonly IConfigureService _ConfigureService;
        #endregion

        #region 命令(Command)
        /// <summary>
        /// 更改色调
        /// </summary>
        public DelegateCommand<object> ChangeHueCommand { get; private set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 皮肤视图模型
        /// </summary>
        /// <param name="configureService"></param>
        public SkinViewModel(IConfigureService configureService)
        {
            _ConfigureService = configureService;
            ChangeHueCommand = new DelegateCommand<object>(ChangeHue);
        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// 更改色调
        /// </summary>
        /// <param name="obj"></param>
        private void ChangeHue(object obj)
        {
            var hue = (Color)obj;
            ApplicationContext.HueColor = hue.ToString();
            Configure.Current.Add("HueColor", ApplicationContext.HueColor);
            _ConfigureService.ConfigureHueColor();
        }
        #endregion

    }
}
