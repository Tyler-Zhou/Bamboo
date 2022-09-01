using System;
using System.Threading.Tasks;

namespace Client.Interfaces
{
    /// <summary>
    /// 主窗体服务
    /// </summary>
    public interface IWindowService
    {
        /// <summary>
        /// 添加方法
        /// </summary>
        /// <param name="func"></param>
        void AddFunction(Func<Task> func);
    }
}
