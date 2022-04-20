using Bamboo.Common;

namespace Bamboo.Library.Common.Dto
{
    /// <summary>
    /// 书籍传输对象
    /// </summary>
    public class BookDto : BaseDto
    {
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

        #region 作者
        /// <summary>
        /// Author
        /// </summary>
        private string _Author = "";
        /// <summary>
        /// 作者
        /// </summary>
        public string Author
        {
            get { return _Author; }
            set { _Author = value; OnPropertyChanged(); }
        }
        #endregion

        #region 标签
        /// <summary>
        /// Tag
        /// </summary>
        private string _Tag = "";
        /// <summary>
        /// 标签
        /// </summary>
        public string Tag
        {
            get { return _Tag; }
            set { _Tag = value; OnPropertyChanged(); }
        }
        #endregion

        #region 简介
        /// <summary>
        /// Introduction
        /// </summary>
        private string _Introduction = "";
        /// <summary>
        /// 简介
        /// </summary>
        public string Introduction
        {
            get { return _Introduction; }
            set { _Introduction = value; OnPropertyChanged(); }
        }
        #endregion

        #region 状态
        /// <summary>
        /// Status
        /// </summary>
        private int _Status;
        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return _Status; }
            set { _Status = value; OnPropertyChanged(); }
        } 
        #endregion
    }
}
