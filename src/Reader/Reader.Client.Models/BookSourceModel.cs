using System.Runtime.Serialization;

namespace Reader.Client.Models
{
    /// <summary>
    /// 书源模型
    /// </summary>
    public class BookSourceModel: BaseModel
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

        #region 是否调试
        private bool _IsDebug = false;
        /// <summary>
        /// 是否调试
        /// </summary>
        [IgnoreDataMember]
        public bool IsDebug
        {
            get
            {
                return _IsDebug;
            }
            set
            {
                _IsDebug = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 搜索(Search)

        #region 搜索-链接
        private string _SearchLink = "";
        /// <summary>
        /// 搜索-链接
        /// </summary>
        public string SearchLink
        {
            get
            {
                return _SearchLink;
            }
            set
            {
                _SearchLink = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 搜索-XPath书籍列表
        private string _SearchXPathList = "";
        /// <summary>
        /// 搜索-XPath书籍列表
        /// </summary>
        public string SearchXPathList
        {
            get
            {
                return _SearchXPathList;
            }
            set
            {
                _SearchXPathList = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 搜索-XPath书籍Key
        private string _SearchXPathKey = "";
        /// <summary>
        /// 搜索-XPath书籍Key
        /// </summary>
        public string SearchXPathKey
        {
            get
            {
                return _SearchXPathKey;
            }
            set
            {
                _SearchXPathKey = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 搜索-Regex书籍Key
        private string _SearchRegexKey = "";
        /// <summary>
        /// 搜索-Regex书籍Key
        /// </summary>
        public string SearchRegexKey
        {
            get
            {
                return _SearchRegexKey;
            }
            set
            {
                _SearchRegexKey = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 搜索-XPath书籍名称
        private string _SearchXPathName = "";
        /// <summary>
        /// 搜索-XPath书籍名称
        /// </summary>
        public string SearchXPathName
        {
            get
            {
                return _SearchXPathName;
            }
            set
            {
                _SearchXPathName = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 搜索-XPath书籍链接
        private string _SearchXPathLink = "";
        /// <summary>
        /// 搜索-XPath书籍链接
        /// </summary>
        public string SearchXPathLink
        {
            get
            {
                return _SearchXPathLink;
            }
            set
            {
                _SearchXPathLink = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 搜索-Attribute书籍链接
        private string _SearchAttributeLink = "href";
        /// <summary>
        /// 搜索-Attribute书籍链接
        /// </summary>
        public string SearchAttributeLink
        {
            get
            {
                return _SearchAttributeLink;
            }
            set
            {
                _SearchAttributeLink = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 搜索-XPath书籍封面链接
        private string _SearchXPathPosterLink = "";
        /// <summary>
        /// 搜索-XPath书籍封面链接
        /// </summary>
        public string SearchXPathPosterLink
        {
            get
            {
                return _SearchXPathPosterLink;
            }
            set
            {
                _SearchXPathPosterLink = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 搜索-Attribute书籍封面链接
        private string _SearchAttributePosterLink = "src";
        /// <summary>
        /// 搜索-Attribute书籍封面链接
        /// </summary>
        public string SearchAttributePosterLink
        {
            get
            {
                return _SearchAttributePosterLink;
            }
            set
            {
                _SearchAttributePosterLink = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 搜索-XPath书籍标签(分类)
        private string _SearchXPathTag = "";
        /// <summary>
        /// 搜索-XPath书籍标签(分类)
        /// </summary>
        public string SearchXPathTag
        {
            get
            {
                return _SearchXPathTag;
            }
            set
            {
                _SearchXPathTag = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 搜索-XPath书籍作者
        private string _SearchXPathAuthor = "";
        /// <summary>
        /// 搜索-XPath书籍作者
        /// </summary>
        public string SearchXPathAuthor
        {
            get
            {
                return _SearchXPathAuthor;
            }
            set
            {
                _SearchXPathAuthor = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 搜索-XPath书籍更新时间
        private string _SearchXPathUpdateTime = "";
        /// <summary>
        /// 搜索-XPath书籍更新时间
        /// </summary>
        public string SearchXPathUpdateTime
        {
            get
            {
                return _SearchXPathUpdateTime;
            }
            set
            {
                _SearchXPathUpdateTime = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 搜索-XPath书籍状态
        private string _SearchXPathStatus = "";
        /// <summary>
        /// 搜索-XPath书籍状态
        /// </summary>
        public string SearchXPathStatus
        {
            get
            {
                return _SearchXPathStatus;
            }
            set
            {
                _SearchXPathStatus = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #endregion

        #region 详细(Detail)

        #region 详细-XPath书籍信息
        private string _DetailXPathInfo = "";
        /// <summary>
        /// 详细-XPath书籍信息
        /// </summary>
        public string DetailXPathInfo
        {
            get
            {
                return _DetailXPathInfo;
            }
            set
            {
                _DetailXPathInfo = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 详细-XPath书籍Key
        private string _DetailXPathKey = "";
        /// <summary>
        /// 详细-XPath书籍Key
        /// </summary>
        public string DetailXPathKey
        {
            get
            {
                return _DetailXPathKey;
            }
            set
            {
                _DetailXPathKey = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 详细-Regex书籍Key
        private string _DetailRegexKey = "";
        /// <summary>
        /// 详细-Regex书籍Key
        /// </summary>
        public string DetailRegexKey
        {
            get
            {
                return _DetailRegexKey;
            }
            set
            {
                _DetailRegexKey = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 详细-XPath书籍名称
        private string _DetailXPathName = "";
        /// <summary>
        /// 详细-XPath书籍名称
        /// </summary>
        public string DetailXPathName
        {
            get
            {
                return _DetailXPathName;
            }
            set
            {
                _DetailXPathName = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 详细-XPath书籍标签(分类)
        private string _DetailXPathTag = "";
        /// <summary>
        /// 详细-XPath书籍标签(分类)
        /// </summary>
        public string DetailXPathTag
        {
            get
            {
                return _DetailXPathTag;
            }
            set
            {
                _DetailXPathTag = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 详细-XPath书籍作者
        private string _DetailXPathAuthor = "";
        /// <summary>
        /// 详细-XPath书籍作者
        /// </summary>
        public string DetailXPathAuthor
        {
            get
            {
                return _DetailXPathAuthor;
            }
            set
            {
                _DetailXPathAuthor = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 详细-XPath书籍简介
        private string _DetailXPathIntroduction = "";
        /// <summary>
        /// 详细-XPath书籍简介
        /// </summary>
        public string DetailXPathIntroduction
        {
            get
            {
                return _DetailXPathIntroduction;
            }
            set
            {
                _DetailXPathIntroduction = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 详细-XPath书籍更新时间
        private string _DetailXPathUpdateTime = "";
        /// <summary>
        /// 详细-XPath书籍更新时间
        /// </summary>
        public string DetailXPathUpdateTime
        {
            get
            {
                return _DetailXPathUpdateTime;
            }
            set
            {
                _DetailXPathUpdateTime = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 详细-XPath书籍状态
        private string _DetailXPathStatus = "";
        /// <summary>
        /// 详细-XPath书籍状态
        /// </summary>
        public string DetailXPathStatus
        {
            get
            {
                return _DetailXPathStatus;
            }
            set
            {
                _DetailXPathStatus = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 详细-XPath书籍封面链接
        private string _DetailXPathPosterLink = "";
        /// <summary>
        /// 详细-XPath书籍封面链接
        /// </summary>
        public string DetailXPathPosterLink
        {
            get
            {
                return _DetailXPathPosterLink;
            }
            set
            {
                _DetailXPathPosterLink = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 详细-Attribute书籍封面链接
        private string _DetailAttributePosterLink = "src";
        /// <summary>
        /// 详细-Attribute书籍封面链接
        /// </summary>
        public string DetailAttributePosterLink
        {
            get
            {
                return _DetailAttributePosterLink;
            }
            set
            {
                _DetailAttributePosterLink = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 详细-XPath章节列表
        private string _DetailXPathChapterList = "";
        /// <summary>
        /// 详细-XPath章节列表
        /// </summary>
        public string DetailXPathChapterList
        {
            get
            {
                return _DetailXPathChapterList;
            }
            set
            {
                _DetailXPathChapterList = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 详细-XPath章节Key
        private string _DetailXPathChapterKey = "";
        /// <summary>
        /// 详细-XPath章节Key
        /// </summary>
        public string DetailXPathChapterKey
        {
            get
            {
                return _DetailXPathChapterKey;
            }
            set
            {
                _DetailXPathChapterKey = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 详细-Regex章节Key
        private string _DetailRegexChapterKey = "";
        /// <summary>
        /// 详细-Regex章节Key
        /// </summary>
        public string DetailRegexChapterKey
        {
            get
            {
                return _DetailRegexChapterKey;
            }
            set
            {
                _DetailRegexChapterKey = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 详细-XPath章节名称
        private string _DetailXPathChapterName = "";
        /// <summary>
        /// 详细-XPath章节名称
        /// </summary>
        public string DetailXPathChapterName
        {
            get
            {
                return _DetailXPathChapterName;
            }
            set
            {
                _DetailXPathChapterName = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 详细-XPath章节链接
        private string _DetailXPathChapterLink = "";
        /// <summary>
        /// 详细-XPath章节链接
        /// </summary>
        public string DetailXPathChapterLink
        {
            get
            {
                return _DetailXPathChapterLink;
            }
            set
            {
                _DetailXPathChapterLink = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 详细-Attribute章节链接
        private string _DetailAttributeChapterLink = "href";
        /// <summary>
        /// 详细-Attribute章节链接
        /// </summary>
        public string DetailAttributeChapterLink
        {
            get
            {
                return _DetailAttributeChapterLink;
            }
            set
            {
                _DetailAttributeChapterLink = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #endregion

        #region 章节(Chapter)

        #region 章节-XPath名称
        private string _ChapterXPathName = "";
        /// <summary>
        /// 章节-XPath名称
        /// </summary>
        public string ChapterXPathName
        {
            get
            {
                return _ChapterXPathName;
            }
            set
            {
                _ChapterXPathName = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 章节-XPath内容
        private string _ChapterXPathContent = "";
        /// <summary>
        /// 章节-XPath内容
        /// </summary>
        public string ChapterXPathContent
        {
            get
            {
                return _ChapterXPathContent;
            }
            set
            {
                _ChapterXPathContent = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 章节-Regex内容
        private string _ChapterRegexContent = "";
        /// <summary>
        /// 章节-Regex内容
        /// </summary>
        public string ChapterRegexContent
        {
            get
            {
                return _ChapterRegexContent;
            }
            set
            {
                _ChapterRegexContent = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #endregion
    }
}
