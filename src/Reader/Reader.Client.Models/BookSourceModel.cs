namespace Reader.Client.Models
{
    /// <summary>
    /// 书源模型
    /// </summary>
    public class BookSourceModel
    {
        /// <summary>
        /// 键值
        /// </summary>
        public string Key { get; set; } = "";
        /// <summary>
        /// 名称
        /// </summary>
        public string Name{ get;set;} = "";

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; set; } = "";

        /// <summary>
        /// 链接
        /// </summary>
        public string Link { get; set; } = "";
        /// <summary>
        /// 是否调试
        /// </summary>
        public bool IsDebug { get; set; } = false;

        #region 搜索(Search)
        /// <summary>
        /// 搜索-链接
        /// </summary>
        public string SearchLink { get; set; } = "";

        /// <summary>
        /// 搜索-XPath书籍列表
        /// </summary>
        public string SearchXPathList { get; set; } = "";

        /// <summary>
        /// 搜索-XPath书籍Key
        /// </summary>
        public string SearchXPathKey { get; set; } = "";

        /// <summary>
        /// 搜索-Regex书籍Key
        /// </summary>
        public string SearchRegexKey { get; set; } = "";

        /// <summary>
        /// 搜索-XPath书籍名称
        /// </summary>
        public string SearchXPathName { get; set; } = "";

        /// <summary>
        /// 搜索-XPath书籍链接
        /// </summary>
        public string SearchXPathLink { get; set; } = "";

        /// <summary>
        /// 搜索-Attribute书籍链接
        /// </summary>
        public string SearchAttributeLink { get; set; } = "";

        /// <summary>
        /// 搜索-XPath书籍封面链接
        /// </summary>
        public string SearchXPathPosterLink { get; set; } = "";

        /// <summary>
        /// 搜索-Attribute书籍封面Url
        /// </summary>
        public string SearchAttributePosterUrl { get; set; } = "";

        /// <summary>
        /// 搜索-XPath书籍标签(分类)
        /// </summary>
        public string SearchXPathTag { get; set; } = "";

        /// <summary>
        /// 搜索-XPath书籍作者
        /// </summary>
        public string SearchXPathAuthor { get; set; } = "";

        /// <summary>
        /// 搜索-XPath书籍更新时间
        /// </summary>
        public string SearchXPathUpdateTime { get; set; } = "";

        /// <summary>
        /// 搜索-XPath书籍状态
        /// </summary>
        public string SearchXPathStatus { get; set; } = "";
        #endregion

        #region 详细(Detail)
        /// <summary>
        /// 详细-XPath书籍信息
        /// </summary>
        public string DetailXPathInfo { get; set; } = "";

        /// <summary>
        /// 详细-XPath书籍Key
        /// </summary>
        public string DetailXPathKey { get; set; } = "";

        /// <summary>
        /// 详细-Regex书籍Key
        /// </summary>
        public string DetailRegexKey { get; set; } = "";

        /// <summary>
        /// 详细-XPath书籍名称
        /// </summary>
        public string DetailXPathName { get; set; } = "";

        /// <summary>
        /// 详细-XPath书籍标签(分类)
        /// </summary>
        public string DetailXPathTag { get; set; } = "";

        /// <summary>
        /// 详细-XPath书籍作者
        /// </summary>
        public string DetailXPathAuthor { get; set; } = "";

        /// <summary>
        /// 详细-XPath书籍简介
        /// </summary>
        public string DetailXPathIntroduction { get; set; } = "";

        /// <summary>
        /// 详细-XPath书籍更新时间
        /// </summary>
        public string DetailXPathUpdateTime { get; set; } = "";

        /// <summary>
        /// 详细-XPath书籍状态
        /// </summary>
        public string DetailXPathStatus { get; set; } = "";

        /// <summary>
        /// 详细-XPath书籍封面链接
        /// </summary>
        public string DetailXPathPosterLink { get; set; } = "";

        /// <summary>
        /// 详细-Attribute书籍封面Url
        /// </summary>
        public string DetailAttributePosterUrl { get; set; } = "";

        /// <summary>
        /// 详细-XPath章节列表
        /// </summary>
        public string DetailXPathChapterList { get; set; } = "";

        /// <summary>
        /// 详细-XPath章节Key
        /// </summary>
        public string DetailXPathChapterKey { get; set; } = "";

        /// <summary>
        /// 详细-Regex章节Key
        /// </summary>
        public string DetailRegexChapterKey { get; set; } = "";

        /// <summary>
        /// 详细-XPath章节名称
        /// </summary>
        public string DetailXPathChapterName { get; set; } = "";

        /// <summary>
        /// 详细-XPath章节链接
        /// </summary>
        public string DetailXPathChapterLink { get; set; } = "";
        #endregion

        #region 章节(Chapter)
        /// <summary>
        /// 章节-XPath名称
        /// </summary>
        public string ChapterXPathName { get; set; } = "";

        /// <summary>
        /// 章节-XPath内容
        /// </summary>
        public string ChapterXPathContent { get; set; } = "";
        /// <summary>
        /// 章节-Regex内容
        /// </summary>
        public string ChapterRegexContent { get; set; } = "";
        #endregion
    }
}
