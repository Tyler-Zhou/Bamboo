using Prism.Mvvm;

namespace Reader.Client.Models
{
    /// <summary>
    /// 提示信息
    /// </summary>
    public class TipsInfo: BindableBase
    {
        #region 类型
        private EnumTipsType _Type = EnumTipsType.UnKnown;
        /// <summary>
        /// 类型
        /// </summary>
        public EnumTipsType Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 内容
        private string _Content = "";
        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get
            {
                return _Content;
            }
            set
            {
                _Content = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }

    /// <summary>
    /// 提示类型
    /// </summary>
    public enum EnumTipsType
    {
        /// <summary>
        /// 未知
        /// </summary>
        UnKnown = 0,
        /// <summary>
        /// 信息
        /// </summary>
        Information = 1,
        /// <summary>
        /// 警告
        /// </summary>
        Warning = 2,
        /// <summary>
        /// 错误
        /// </summary>
        Error= 3,
    }
}
