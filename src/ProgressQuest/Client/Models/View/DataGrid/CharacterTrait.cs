namespace Client.Models
{
    /// <summary>
    /// 人物特征
    /// </summary>
    public class CharacterTrait : BaseModel
    {
        #region 值
        private string _Value = "";
        /// <summary>
        /// 值
        /// </summary>
        public string Value
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
