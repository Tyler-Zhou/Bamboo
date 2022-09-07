using Client.Extensions;
using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 剧幕实体
    /// </summary>
    public class CharacterAct : BaseModel
    {
        #region 剧幕索引
        private int _Index = 0;
        /// <summary>
        /// 剧幕索引
        /// </summary>
        public int Index
        {
            get
            {
                return _Index;
            }
            set
            {
                _Index = value;
                RaisePropertyChanged(nameof(Index));
                RaisePropertyChanged(nameof(Description));
            }
        }
        #endregion

        #region 是否完成
        private bool _IsCommplete = false;
        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsCommplete
        {
            get
            {
                return _IsCommplete;
            }
            set
            {
                _IsCommplete = value;
                RaisePropertyChanged(nameof(IsCommplete));
                RaisePropertyChanged(nameof(Description));
            }
        }
        #endregion

        #region 描述
        /// <summary>
        /// 描述
        /// </summary>
        [JsonIgnore]
        public string Description
        {
            get
            {
                string name = Key.FindResourceDictionary();
                name = name.Replace($"^RomanNumber$", Index.ToRomanNumber());
                return name;
            }
        }
        #endregion
    }
}
