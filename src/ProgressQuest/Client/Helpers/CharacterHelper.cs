using System;

namespace Client.Helpers
{
    /// <summary>
    /// 人物帮助类
    /// </summary>
    public class CharacterHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static int GetMaxExperienceByLevel(int level)
        {
            //1级20分钟,之后指数增长
            return (int)Math.Round((20 + Math.Pow(1.15, level)) * 60);
        }
    }
}
