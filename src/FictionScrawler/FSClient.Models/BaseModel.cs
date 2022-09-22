using Prism.Mvvm;
using System;

namespace FSClient.Models
{
    /// <summary>
    /// 模型基类
    /// </summary>
    public class BaseModel : BindableBase
    {
        #region 键值
        private Guid _Key = Guid.Empty;
        /// <summary>
        /// 键值
        /// </summary>
        public Guid Key
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
