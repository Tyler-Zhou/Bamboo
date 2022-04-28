using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Bamboo.Client.Core.Converters
{
    /// <summary>
    /// 根据数据决定控件显示或隐藏(int and Visibility)
    /// </summary>
    public class IntToVisibilityConveter : IValueConverter
    {
        /// <summary>
        /// int 转换 Visibility
        /// </summary>
        /// <param name="value">绑定源生成的值</param>
        /// <param name="targetType">绑定目标属性的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && int.TryParse(value.ToString(), out int result))
            {
                if (result == 0)
                    return Visibility.Visible;
            }
            return Visibility.Hidden;
        }
        /// <summary>
        /// Visibility 转换 int
        /// </summary>
        /// <param name="value">绑定源生成的值</param>
        /// <param name="targetType">绑定目标属性的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
