using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 当前任务进度条
    /// </summary>
    public class CurrentBarModel: BaseBarModel
    {
        #region 任务类型
        private EnumTask _TaskType = EnumTask.UnKnown;
        /// <summary>
        /// 任务类型
        /// </summary>
        public EnumTask TaskType
        {
            get
            {
                return _TaskType;
            }
            set
            {
                _TaskType = value;
                RaisePropertyChanged(nameof(TaskType));
            }
        }
        #endregion
    }
}
