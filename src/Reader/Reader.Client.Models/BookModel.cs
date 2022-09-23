using System;

namespace Reader.Client.Models
{
    /// <summary>
    /// 书籍模型
    /// </summary>
    public class BookModel : BaseModel
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

        #region 类型
        private string _Type = "";
        /// <summary>
        /// 类型
        /// </summary>
        public string Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
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

        #region 更新时间
        private DateTime _UpdateTime = DateTime.MinValue;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            get
            {
                return _UpdateTime;
            }
            set
            {
                _UpdateTime = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 状态
        private string _Status = "";
        /// <summary>
        /// 状态
        /// </summary>
        public string Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 书源 Key
        private Guid _SourceKey = Guid.Empty;
        /// <summary>
        /// 书源 Key
        /// </summary>
        public Guid SourceKey
        {
            get
            {
                return _SourceKey;
            }
            set
            {
                _SourceKey = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}
