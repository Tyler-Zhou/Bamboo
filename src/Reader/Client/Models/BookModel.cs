namespace Client.Models
{
    /// <summary>
    /// 书籍实体
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
            }
        }
        #endregion

        #region 标签 Key
        private string _TagKey = "";
        /// <summary>
        /// 标签 Key
        /// </summary>
        public string TagKey
        {
            get
            {
                return _TagKey;
            }
            set
            {
                _TagKey = value;
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
            }
        }
        #endregion
    }
}
