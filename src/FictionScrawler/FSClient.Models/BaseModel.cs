using Prism.Mvvm;

namespace FSClient.Models
{
    /// <summary>
    /// 模型基类
    /// </summary>
    public class BaseModel : BindableBase
    {
        #region 键值
        private string _Key = "";
        /// <summary>
        /// 键值
        /// </summary>
        public string Key
        {
            get
            {
                return _Key;
            }
            set
            {
                _Key = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}
