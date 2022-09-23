using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Reader.Client.Converters
{
    /// <summary>
    /// 布尔值与控件可见性转换器
    /// </summary>
    public class BooleanToVisibilityConveter : IValueConverter
    {
        /// <summary>
        /// 布尔值 转换 控件可见性
        /// </summary>
        /// <param name="value">绑定源生成的值</param>
        /// <param name="targetType">绑定目标属性的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && bool.TryParse(value.ToString(), out bool result))
            {
                if (result)
                    return Visibility.Visible;
            }
            return Visibility.Collapsed;
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
