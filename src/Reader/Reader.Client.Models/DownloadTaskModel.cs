using Prism.Mvvm;
using System;

namespace Reader.Client.Models
{
    /// <summary>
    /// 下载任务模型
    /// </summary>
    public class DownloadTaskModel : BindableBase
    {
        #region 书源 Key
        private Guid _SourceID = Guid.Empty;
        /// <summary>
        /// 书源 Key
        /// </summary>
        public Guid SourceID
        {
            get
            {
                return _SourceID;
            }
            set
            {
                _SourceID = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 书籍主键
        private Guid _BookID = Guid.Empty;
        /// <summary>
        /// 书籍主键
        /// </summary>
        public Guid BookID
        {
            get
            {
                return _BookID;
            }
            set
            {
                _BookID = value;
                RaisePropertyChanged();
            }
        }
        #endregion

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

        #region 章节标识键
        private string _Key = "";
        /// <summary>
        /// 章节标识键
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

        #region 章节名称
        private string _Name = "";
        /// <summary>
        /// 章节名称
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

        #region 章节链接
        private string _Link = "";
        /// <summary>
        /// 章节链接
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

        #region 是否已下载
        private bool _IsDownload = false;
        /// <summary>
        /// 是否已下载
        /// </summary>
        public bool IsDownload
        {
            get
            {
                return _IsDownload;
            }
            set
            {
                _IsDownload = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}
