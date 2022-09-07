using Client.Extensions;
using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 法术书
    /// </summary>
    public class CharacterSpellBook : BaseModel
    {
        #region 等级
        private int _Level = 0;
        /// <summary>
        /// 等级
        /// </summary>
        public int Level
        {
            get
            {
                return _Level;
            }
            set
            {
                _Level = value;
                RaisePropertyChanged(nameof(Level));
                RaisePropertyChanged(nameof(Description));
            }
        }
        #endregion

        #region 等级描述
        /// <summary>
        /// 等级描述
        /// </summary>
        [JsonIgnore]
        public string Description
        {
            get
            {
                return _Level.ToRomanNumber();
            }
        }
        #endregion
    }
}
