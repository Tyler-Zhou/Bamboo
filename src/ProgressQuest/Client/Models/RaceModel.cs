using Client.Enums;
using System.Collections.ObjectModel;

namespace Client.Models
{
    /// <summary>
    /// 种族
    /// </summary>
    public class RaceModel : BaseModel
    {
        /// <summary>
        /// 属性集合
        /// </summary>
        public ObservableCollection<EnumStat> Stats { get; set; }
    }
}
