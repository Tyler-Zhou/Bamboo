using Prism.Mvvm;
using System;

namespace Client.Models
{
    /// <summary>
    /// 进度条
    /// </summary>
    public class BaseBarModel: BindableBase
    {
        #region 当前位置
        private double _Position = 0;
        /// <summary>
        /// 当前位置
        /// </summary>
        public double Position
        {
            get
            {
                return _Position;
            }
            set
            {
                _Position = value;
                RaisePropertyChanged(nameof(Position));
                RaisePropertyChanged(nameof(ToolTip));
            }
        }
        #endregion

        #region 最大值
        private double _MaxValue = 0;
        /// <summary>
        /// 最大值
        /// </summary>
        public double MaxValue
        {
            get
            {
                return _MaxValue;
            }
            set
            {
                _MaxValue = value;
                RaisePropertyChanged(nameof(MaxValue));
                RaisePropertyChanged(nameof(ToolTip));
            }
        }
        #endregion

        #region 工具栏提示
        private string _ToolTip = "";
        /// <summary>
        /// 工具栏提示
        /// </summary>
        public virtual string ToolTip
        {
            get
            {
                return _ToolTip;
            }
            set
            {
                _ToolTip = value;
                RaisePropertyChanged(nameof(ToolTip));
            }
        }
        #endregion


        #region 是否完成
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
        #endregion

        #region 增量
        /// <summary>
        /// 增量
        /// </summary>
        /// <param name="increment"></param>
        public void Increment(double increment)
        {
            Position += increment;
        }
        #endregion

        #region 重置
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
        #endregion

        #region 复位
        /// <summary>
        /// 复位
        /// </summary>
        /// <param name="position"></param>
        public void Reposition(double position)
        {
            position = Math.Min(position, MaxValue);
            Position = position;
        } 
        #endregion
    }
}
