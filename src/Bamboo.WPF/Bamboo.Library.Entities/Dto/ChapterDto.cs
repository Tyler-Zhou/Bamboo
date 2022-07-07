using Bamboo.Entities;

namespace Bamboo.Library.Entities
{
    /// <summary>
    /// 章节传输对象
    /// </summary>
    public class ChapterDto : BaseDto
    {
        #region 书籍标识
        /// <summary>
        /// BookKey
        /// </summary>
        private string _BookKey = "";
        /// <summary>
        /// 书籍标识
        /// </summary>
        public string BookKey
        {
            get { return _BookKey; }
            set { _BookKey = value; OnPropertyChanged(); }
        }
        #endregion

        #region 标识
        /// <summary>
        /// Key
        /// </summary>
        private string _Key = "";
        /// <summary>
        /// 标识
        /// </summary>
        public string Key
        {
            get { return _Key; }
            set { _Key = value; OnPropertyChanged(); }
        }
        #endregion

        #region 名称
        /// <summary>
        /// Name
        /// </summary>
        private string _Name = "";
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged(); }
        }
        #endregion

        #region 内容
        /// <summary>
        /// Content
        /// </summary>
        private string _Content = "";
        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get { return _Content; }
            set { _Content = value; OnPropertyChanged(); }
        }
        #endregion

        #region 链接
        /// <summary>
        /// Link
        /// </summary>
        private string _Link = "";
        /// <summary>
        /// 链接
        /// </summary>
        public string Link
        {
            get { return _Link; }
            set { _Link = value; OnPropertyChanged(); }
        }
        #endregion

        #region 排序索引
        /// <summary>
        /// OrderIndex
        /// </summary>
        private int _OrderIndex = 0;
        /// <summary>
        /// 排序索引
        /// </summary>
        public int OrderIndex
        {
            get { return _OrderIndex; }
            set { _OrderIndex = value; OnPropertyChanged(); }
        }
        #endregion

        #region 是否错误(章节)
        /// <summary>
        /// IsError
        /// </summary>
        private bool _IsError = false;
        /// <summary>
        /// 是否错误(章节)
        /// </summary>
        public bool IsError
        {
            get { return _IsError; }
            set { _IsError = value; OnPropertyChanged(); }
        }
        #endregion
    }
}
