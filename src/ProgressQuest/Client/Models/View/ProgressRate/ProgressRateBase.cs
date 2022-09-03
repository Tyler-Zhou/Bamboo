namespace Client.Models
{
    /// <summary>
    /// 进度对象
    /// </summary>
    public class ProgressRateBase
    {
        /// <summary>
        /// 键值
        /// </summary>
        public string Key { get; set; } = "";
        /// <summary>
        /// 
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public int MaxValue { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsCommplete
        {
            get
            {
                return Position >= MaxValue;
            }
        }

        /// <summary>
        /// 增量
        /// </summary>
        /// <param name="increment"></param>
        public void Increment(int increment)
        {
            Position += increment;
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="maxValue"></param>
        /// <param name="position"></param>
        public void Reset(int maxValue, int position = 0)
        {
            Position = position;
            MaxValue = maxValue;
        }
    }
}
