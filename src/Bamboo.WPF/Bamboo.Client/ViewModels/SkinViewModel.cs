using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using System;
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
            get => _isDarkTheme;
            set
            {
                if (SetProperty(ref _isDarkTheme, value))
                {
                    ModifyTheme(theme => theme.SetBaseTheme(value ? Theme.Dark : Theme.Light));
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
        /// 调色板帮助类
        /// </summary>
        private readonly PaletteHelper paletteHelper = new PaletteHelper();

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
        public SkinViewModel()
        {
            ChangeHueCommand = new DelegateCommand<object>(ChangeHue);
        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// 修改主题
        /// </summary>
        /// <param name="modificationAction"></param>
        private static void ModifyTheme(Action<ITheme> modificationAction)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();
            modificationAction?.Invoke(theme);
            paletteHelper.SetTheme(theme);
        }
        /// <summary>
        /// 更改色调
        /// </summary>
        /// <param name="obj"></param>
        private void ChangeHue(object obj)
        {
            var hue = (Color)obj;
            ITheme theme = paletteHelper.GetTheme();
            theme.PrimaryLight = new ColorPair(hue.Lighten());
            theme.PrimaryMid = new ColorPair(hue);
            theme.PrimaryDark = new ColorPair(hue.Darken());
            paletteHelper.SetTheme(theme);
        } 
        #endregion

    }
}
