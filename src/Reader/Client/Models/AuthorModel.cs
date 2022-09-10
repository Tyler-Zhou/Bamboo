namespace Client.Models
{
    /// <summary>
    /// 作者
    /// </summary>
    public class AuthorModel:BaseModel
    {
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

        #region 简介
        private string _Introduction = "";
        /// <summary>
        /// 简介
        /// </summary>
        public string Introduction
        {
            get
            {
                return _Introduction;
            }
            set
            {
                _Introduction = value;
            }
        }
        #endregion
    }
}
