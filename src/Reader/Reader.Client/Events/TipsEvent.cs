using Prism.Events;
using Prism.Mvvm;

namespace Reader.Client.Events
{
    #region 提示(信息)模型
    /// <summary>
    /// 提示(信息)模型
    /// </summary>
    public class TipsModel
    {
        /// <summary>
        /// 筛选
        /// </summary>
        public string Filter { get; set; } = "";
        /// <summary>
        /// 消息
        /// </summary>
        public TipsInfo Tips { get; set; }
    }
    #endregion

    #region 提示(信息)
    /// <summary>
    /// 提示(信息)
    /// </summary>
    public class TipsInfo : BindableBase
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
    #endregion

    #region 提示(信息)类型
    /// <summary>
    /// 提示(信息)类型
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
        Error = 3,
    }
    #endregion

    #region 发布提示(信息)事件
    /// <summary>
    /// 发布提示(信息)事件
    /// </summary>
    public class TipsEvent : PubSubEvent<TipsModel>
    {
    } 
    #endregion
}
