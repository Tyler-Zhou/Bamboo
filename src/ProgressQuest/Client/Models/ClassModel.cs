using Client.Enums;
using System.Collections.ObjectModel;

namespace Client.Models
{
    public class ClassModel:BaseModel
    {
        /// <summary>
        /// 属性集合
        /// </summary>
        public ObservableCollection<EnumStat> Stats { get; set; }
    }
}
