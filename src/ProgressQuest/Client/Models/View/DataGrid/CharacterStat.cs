using Client.Extensions;

namespace Client.Models
{
    /// <summary>
    /// 属性模型
    /// </summary>
    public class CharacterStat : BaseModel
    {
        #region 属性
        private EnumStat _StatType = EnumStat.UnKnown;
        /// <summary>
        /// 属性
        /// </summary>
        public EnumStat StatType
        {
            get
            {
                return _StatType;
            }
            set
            {
                _StatType = value;
                RaisePropertyChanged(nameof(StatType));
            }
        }
        #endregion

        #region 属性名称
        /// <summary>
        /// 属性名称
        /// </summary>
        public string StatTypeName
        {
            get
            {
                return ($"EnumStat{StatType}").FindResourceDictionary();
            }
        }
        #endregion

        #region 属性值
        private int _Value = 0;
        /// <summary>
        /// 属性值
        /// </summary>
        public int Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
                RaisePropertyChanged(nameof(Value));
            }
        }
        #endregion
    }
}
