namespace Reader.Client.Models
{
    /// <summary>
    /// 章节模型
    /// </summary>
    public class ChapterModel:BaseModel
    {
        #region 书籍标识键
        private string _BookKey = "";
        /// <summary>
        /// 书籍标识键
        /// </summary>
        public string BookKey
        {
            get
            {
                return _BookKey;
            }
            set
            {
                _BookKey = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 标识键
        private string _Key = "";
        /// <summary>
        /// 标识键
        /// </summary>
        public string Key
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

        #region 内容
        private string _Content = "";
        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get
            {
                return _Content;
            }
            set
            {
                _Content = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 链接
        private string _Link = "";
        /// <summary>
        /// 链接
        /// </summary>
        public string Link
        {
            get
            {
                return _Link;
            }
            set
            {
                _Link = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 排序索引
        private int _OrderIndex = 0;
        /// <summary>
        /// 排序索引
        /// </summary>
        public int OrderIndex
        {
            get
            {
                return _OrderIndex;
            }
            set
            {
                _OrderIndex = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 是否错误(章节)
        private bool _IsError = false;
        /// <summary>
        /// 是否错误(章节)
        /// </summary>
        public bool IsError
        {
            get
            {
                return _IsError;
            }
            set
            {
                _IsError = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}
