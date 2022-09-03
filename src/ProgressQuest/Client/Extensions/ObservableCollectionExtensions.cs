using Client.Helpers;
using System.Collections.ObjectModel;

namespace Client.Extensions
{
    /// <summary>
    /// 集合扩展方法
    /// </summary>
    public static class ObservableCollectionExtensions
    {
        /// <summary>
        /// 从集合中挑选一个
        /// </summary>
        /// <param name="observableCollection">待挑选数据集合</param>
        /// <returns></returns>
        public static T Pick<T>(this ObservableCollection<T> observableCollection)
        {
            return observableCollection[RandomHelper.Value(0, observableCollection.Count)];
        }

        /// <summary>
        /// 从集合中挑选一个
        /// </summary>
        /// <param name="observableCollection">待挑选数据集合</param>
        /// <param name="maxValue">最大值</param>
        /// <returns></returns>
        public static T Pick<T>(this ObservableCollection<T> observableCollection, int maxValue)
        {
            return observableCollection[RandomHelper.Value(0, maxValue)];
        }
    }
}
