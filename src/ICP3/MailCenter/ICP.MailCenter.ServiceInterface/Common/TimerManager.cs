using ICP.Framework.CommonLibrary.Client;

namespace ICP.MailCenter.ServiceInterface
{
    /// <summary>
    /// 计时器管理
    /// </summary>
    public class TimerManager
    {
        public static string ReveiveTimer = "ReceiveTime";
        public static string MailReadTime = "MailReadTime";

        /// <summary>
        /// 设置邮件已读时间间隔
        /// </summary>
        public static int MailReadTimerInterval
        {
            get
            {
                return ConvertSecondToMillisecond(GetConfigNodeValue(MailReadTime));
            }
            set
            {
                SetConfigNodeValue(MailReadTime, value.ToString());
            }
        }
        /// <summary>
        /// 设置接收或发送邮件的时间间隔
        /// </summary>
        public static int ReceivedMailTimerInterval
        {
            get { return CovnertMinutesToMillisecond(GetConfigNodeValue(ReveiveTimer)); }
            set { SetConfigNodeValue(ReveiveTimer, value.ToString()); }
        }

        /// <summary>
        /// 获取配置文件节点值
        /// </summary>
        /// <param name="node"></param>
        public static string GetConfigNodeValue(string node)
        {
            string value = string.Empty;
            if (ClientConfig.Current.Contains(node))
            {
                value = ClientConfig.Current.GetValue(node);
            }
            else
            {
                value = "";
                ClientConfig.Current.AddValue(node, value);
            }
            return value;
        }

        /// <summary>
        /// 设置配置文件节点值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        public static void SetConfigNodeValue(string node, string text)
        {
            ClientConfig.Current.AddValue(node, text);
        }
        /// <summary>
        /// 默认为2秒时间
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int ConvertSecondToMillisecond(string num)
        {
            if (!string.IsNullOrEmpty(num))
            {
                int time = 0;
                int.TryParse(num, out time);
                return time * 1000;
            }
            else
            {
                return 2 * 1000;
            }
        }

        /// <summary>
        /// 将分钟转换为毫秒
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int CovnertMinutesToMillisecond(string num)
        {
            if (!string.IsNullOrEmpty(num))
            {
                int time = 0;
                int.TryParse(num, out time);
                return time * 60000;
            }
            else
            {
                return 5 * 60000;
            }
        }

        /// <summary>
        /// 获取配置文件的Key值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="minutes">如果没有找到Value值，赋值默认值</param>
        /// <returns></returns>
        public static double GetAppSettingValue(string key, double minutes)
        {
            double fixedMinutes = 0;
            try
            {
                string value = ClientHelper.GetAppSettingValue(key);
                if (!string.IsNullOrEmpty(value))
                    double.TryParse(value, out fixedMinutes);

                else
                    fixedMinutes = minutes;

            }
            catch
            {
                fixedMinutes = minutes;
            }

            return fixedMinutes;
        }
    }
}
