using Prism.Services.Dialogs;
using System.Threading.Tasks;

namespace Bamboo.Client.Core.Interface
{
    /// <summary>
    /// 对话框主机服务
    /// </summary>
    public interface IDialogHostService : IDialogService
    {
        /// <summary>
        /// 显示对话框
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="parameters">对话框参数</param>
        /// <param name="dialogHostName">对话框主机名称</param>
        /// <returns></returns>
        Task<IDialogResult> ShowDialog(string viewName, IDialogParameters parameters, string dialogHostName = "Root");
    }
}
