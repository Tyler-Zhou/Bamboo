namespace Reader.Client.Models
{
    /// <summary>
    /// 书源模型
    /// </summary>
    public class BookSourceModel : BaseModel
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

        #region 分组
        private string _Group = "";
        /// <summary>
        /// 分组
        /// </summary>
        public string Group
        {
            get
            {
                return _Group;
            }
            set
            {
                _Group = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 网址
        private string _Url = "";
        /// <summary>
        /// 网址
        /// </summary>
        public string Url
        {
            get
            {
                return _Url;
            }
            set
            {
                _Url = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 查询网址
        private string _SearchUrl = "";
        /// <summary>
        /// 查询网址
        /// </summary>
        public string SearchUrl
        {
            get
            {
                return _SearchUrl;
            }
            set
            {
                _SearchUrl = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 查询-XPath书籍列表
        private string _XPath_List = "";
        /// <summary>
        /// 查询-XPath书籍列表
        /// </summary>
        public string XPath_List
        {
            get
            {
                return _XPath_List;
            }
            set
            {
                _XPath_List = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 查询-XPath书籍名称
        private string _XPath_Name = "";
        /// <summary>
        /// 查询-XPath书籍名称
        /// </summary>
        public string XPath_Name
        {
            get
            {
                return _XPath_Name;
            }
            set
            {
                _XPath_Name = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 查询-XPath书籍类型
        private string _XPath_Type = "";
        /// <summary>
        /// 查询-XPath书籍类型
        /// </summary>
        public string XPath_Type
        {
            get
            {
                return _XPath_Type;
            }
            set
            {
                _XPath_Type = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 查询-XPath书籍作者
        private string _XPath_Author = "";
        /// <summary>
        /// 查询-XPath书籍作者
        /// </summary>
        public string XPath_Author
        {
            get
            {
                return _XPath_Author;
            }
            set
            {
                _XPath_Author = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 查询-XPath书籍更新时间
        private string _XPath_UpdateTime = "";
        /// <summary>
        /// 查询-XPath书籍更新时间
        /// </summary>
        public string XPath_UpdateTime
        {
            get
            {
                return _XPath_UpdateTime;
            }
            set
            {
                _XPath_UpdateTime = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 查询-XPath书籍状态
        private string _XPath_Status = "";
        /// <summary>
        /// 查询-XPath书籍状态
        /// </summary>
        public string XPath_Status
        {
            get
            {
                return _XPath_Status;
            }
            set
            {
                _XPath_Status = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}
