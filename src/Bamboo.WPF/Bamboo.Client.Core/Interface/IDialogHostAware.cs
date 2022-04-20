using Prism.Commands;
using Prism.Services.Dialogs;

namespace Bamboo.Client.Core.Interface
{
    /// <summary>
    /// 为涉及对话的对象提供一种通知对话活动的方法。
    /// </summary>
    public interface IDialogHostAware
    {
        /// <summary>
        /// DialoHost名称
        /// </summary>
        string DialogHostName { get; set; }

        /// <summary>
        /// 打开过程中执行
        /// </summary>
        /// <param name="parameters"></param>
        void OnDialogOpend(IDialogParameters parameters);

        /// <summary>
        /// 确定
        /// </summary>
        DelegateCommand SureCommand { get; set; }

        /// <summary>
        /// 取消
        /// </summary>
        DelegateCommand CancelCommand { get; set; }
    }
}
