

namespace ICP.WF.FormDesigner
{
    internal class Utility
    {
        /// <summary>
        /// 判断是否英文环境
        /// </summary>
        public static bool IsEnglish
        {
            get
            {
                if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 根据关键字查找中英文资源
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="defaultValue">如果在当前语言环境中，没找到，就设置为当前的默认值</param>
        /// <returns></returns>
        public static string GetString(
            string key, 
            string defaultValue)
        {
            try
            {
                if (IsEnglish)
                {
                    //查找英文资源
                    string enVal = Resources.Resource_EN.ResourceManager.GetString(key);
                    if (string.IsNullOrEmpty(enVal)==false)
                    {
                        return enVal;
                    }
                }
                else
                {
                    //查找中文资源
                    string cnVal = Resources.Resource_CN.ResourceManager.GetString(key);
                    if (string.IsNullOrEmpty(cnVal)==false)
                    {
                        return cnVal;
                    }
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 根据关键字查找中英文资源
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="args">格式化值</param>
        /// <returns>返回资源字符串</returns>
        public static string GetString(
            string key, 
            string defaultValue, 
            params object[] args)
        {
            try
            {
                if (IsEnglish)
                {
                    //查找英文资源
                    string enVal = Resources.Resource_EN.ResourceManager.GetString(key);
                    if (string.IsNullOrEmpty(enVal)==false)
                    {
                        return string.Format(enVal, args);
                    }
                }
                else
                {
                    //查找中文资源
                    string cnVal = Resources.Resource_CN.ResourceManager.GetString(key);
                    if (string.IsNullOrEmpty(cnVal)==false)
                    {
                        return string.Format(cnVal, args); ;
                    }
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }


    }
}
