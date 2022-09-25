using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Reader.Client.Models
{
    /// <summary>
    /// 书籍任务模型
    /// </summary>
    public class BookTaskModel: BindableBase
    {
        #region 成员(Member)
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

        #region 是否完成下载
        /// <summary>
        /// 是否完成下载
        /// </summary>
        public bool IsComplete
        {
            get
            {
                return ChapterTasks.Count(item => !item.IsDownload) <= 0;
            }
        }
        #endregion

        #region 章节任务集合
        /// <summary>
        /// 章节任务集合
        /// </summary>
        public ObservableCollection<ChapterTaskModel> ChapterTasks { get; set; }
        #endregion 
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 书籍任务模型
        /// </summary>
        public BookTaskModel()
        {
            ChapterTasks = new ObservableCollection<ChapterTaskModel>();
        } 
        #endregion
    }
}
