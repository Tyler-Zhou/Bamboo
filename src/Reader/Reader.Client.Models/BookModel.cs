using System;

namespace Reader.Client.Models
{
    /// <summary>
    /// 书籍模型
    /// </summary>
    public class BookModel : BaseModel
    {
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

        #region 标签(类型)
        private string _Tag = "";
        /// <summary>
        /// 标签(类型)
        /// </summary>
        public string Tag
        {
            get
            {
                return _Tag;
            }
            set
            {
                _Tag = value;
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
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 封面链接
        private string _PosterLink = "";
        /// <summary>
        /// 封面链接
        /// </summary>
        public string PosterLink
        {
            get
            {
                return _PosterLink;
            }
            set
            {
                _PosterLink = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 封面链接
        private byte[] _PosterContent = null;
        /// <summary>
        /// 封面(二进制)
        /// </summary>
        public byte[] PosterContent
        {
            get
            {
                return _PosterContent;
            }
            set
            {
                _PosterContent = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 书源 Key
        private string _SourceKey = "";
        /// <summary>
        /// 书源 Key
        /// </summary>
        public string SourceKey
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
