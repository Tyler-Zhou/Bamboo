namespace FSClient.Models
{
    /// <summary>
    /// 书籍模型
    /// </summary>
    public class BookModel:BaseModel
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
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 作者
        private string _Author = "";
        /// <summary>
        /// 作者
        /// </summary>
        public string Author
        {
            get
            {
                return _Author;
            }
            set
            {
                _Author = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}
