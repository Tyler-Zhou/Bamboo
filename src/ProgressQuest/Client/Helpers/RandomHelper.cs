using System;

namespace Client.Helpers
{
    /// <summary>
    /// 随机帮助类
    /// </summary>
    public class RandomHelper
    {
        #region 生成随机因子
        /// <summary>
        /// 随机因子
        /// </summary>
        /// <returns></returns>
        static int RandomSeed()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt32(buffer, 0);
        }
        #endregion

        /// <summary>
        /// 随机值
        /// </summary>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <returns>随机值</returns>
        public static int Value(int minValue, int maxValue)
        {
            return (new Random(RandomSeed())).Next(minValue, maxValue);
        }

        /// <summary>
        /// 随机值
        /// </summary>
        /// <param name="maxValue">最大值</param>
        /// <returns>随机值</returns>
        public static int Value(int maxValue)
        {
            return new Random(RandomSeed()).Next(1, maxValue);
        }

        /// <summary>
        /// 生成两个随机数，返回较小的数字
        /// </summary>
        /// <param name="maxValue">最大值</param>
        /// <returns>随机值</returns>
        public static int MinValue(int maxValue)
        {
            int num1= new Random(RandomSeed()).Next(1, maxValue);
            int num2= new Random(RandomSeed()).Next(1, maxValue);
            return Math.Min(num1, num2);
        }

        /// <summary>
        /// 随机值
        /// </summary>
        /// <param name="chance">机会值</param>
        /// <param name="out_of">在...之外的值</param>
        /// <returns>随机值</returns>
        public static bool Odds(int chance, int out_of)
        {
            return Value(0, out_of) < chance;
        }
    }
}
