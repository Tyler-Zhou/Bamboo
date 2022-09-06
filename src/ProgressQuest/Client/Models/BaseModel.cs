using Client.Extensions;
using Newtonsoft.Json;
using Prism.Mvvm;

namespace Client.Models
{
    /// <summary>
    /// 模型基类
    /// </summary>
    public class BaseModel: BindableBase
    {
        #region 键值
        private string _Key = "";
        /// <summary>
        /// 键值
        /// </summary>
        public virtual string Key
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

        /// <summary>
        /// 名称
        /// </summary>
        [JsonIgnore]
        public virtual string Name
        {
            get
            {
                return Key.FindResourceDictionary();
            }
        }
    }
}
