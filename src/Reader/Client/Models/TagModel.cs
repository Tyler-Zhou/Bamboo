namespace Client.Models
{
    /// <summary>
    /// 标签实体
    /// </summary>
    public class TagModel:BaseModel
    {
        #region 上级 Key
        private string _ParenKey = "";
        /// <summary>
        /// 上级 Key
        /// </summary>
        public string ParenKey
        {
            get
            {
                return _ParenKey;
            }
            set
            {
                _ParenKey = value;
            }
        }
        #endregion

        #region 名称
        private string _Name = "";
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
        #endregion
    }
}
