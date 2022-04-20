using System;
using System.Globalization;
using System.Windows.Data;

namespace Bamboo.Client.Core.Converters
{
    /// <summary>
    /// 是否选中(int and Bool)
    /// </summary>
    public class IntToBoolConveter : IValueConverter
    {
        /// <summary>
        /// int 转换 Bool
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
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Bool 转换 int
        /// </summary>
        /// <param name="value">绑定目标生成的值</param>
        /// <param name="targetType">要转换为的类型</param>
        /// <param name="parameter">要使用的转换器参数</param>
        /// <param name="culture">要用在转换器中的区域性</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value!=null&&bool.TryParse(value.ToString(), out bool result))
            {
                if (result)
                    return 1;
            }
            return 0;
        }
    }
}
