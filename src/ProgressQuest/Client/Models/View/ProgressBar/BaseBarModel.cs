using System;

namespace Client.Models
{
    /// <summary>
    /// 进度条
    /// </summary>
    public class BaseBarModel
    {
        /// <summary>
        /// 当前位置
        /// </summary>
        public double Position { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public double MaxValue { get; set; }

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
        public void Increment(double increment)
        {
            Position += increment;
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="maxValue"></param>
        /// <param name="position"></param>
        public void Reset(double maxValue, double position = 0)
        {
            Position = position;
            MaxValue = maxValue;
        }
        /// <summary>
        /// 复位
        /// </summary>
        /// <param name="position"></param>
        public void Reposition(double position)
        {
            double oldPosition = Position;
            position = Math.Min(position, MaxValue);
            Position = position;
        }
    }
}
