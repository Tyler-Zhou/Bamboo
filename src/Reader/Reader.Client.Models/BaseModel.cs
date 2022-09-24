using Prism.Mvvm;
using System;

namespace Reader.Client.Models
{
    /// <summary>
    /// 模型基类
    /// </summary>
    public class BaseModel : BindableBase
    {
        #region 主键
        private Guid _ID = Guid.Empty;
        /// <summary>
        /// 主键
        /// </summary>
        public Guid ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}
