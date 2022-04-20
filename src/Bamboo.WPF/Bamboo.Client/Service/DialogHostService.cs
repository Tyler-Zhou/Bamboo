using Bamboo.Client.Core.Interface;
using MaterialDesignThemes.Wpf;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Bamboo.Client.Service
{
    /// <summary>
    /// 对话主机服务(自定义)
    /// </summary>
    public class DialogHostService : DialogService, IDialogHostService
    {
        private readonly IContainerExtension? _ContainerExtension;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerExtension"></param>
        public DialogHostService(IContainerExtension containerExtension) : base(containerExtension)
        {
            _ContainerExtension = containerExtension;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        /// <param name="dialogHostName"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<IDialogResult> ShowDialog(string name, IDialogParameters parameters, string dialogHostName = "Root")
        {
            if (_ContainerExtension == null)
                throw new Exception("DialogHostService ContainerExtension 为空");
            if (parameters == null)
                parameters = new DialogParameters();

            //从容器当中去除弹出窗口的实例
            var content = _ContainerExtension.Resolve<object>(name);

            //验证实例的有效性 
            if (!(content is FrameworkElement dialogContent))
                throw new NullReferenceException("A dialog's content must be a FrameworkElement");

            if (dialogContent is FrameworkElement view && view.DataContext is null && ViewModelLocator.GetAutoWireViewModel(view) is null)
                ViewModelLocator.SetAutoWireViewModel(view, true);

            if (!(dialogContent.DataContext is IDialogHostAware viewModel))
                throw new NullReferenceException("A dialog's ViewModel must implement the IDialogAware interface");

            viewModel.DialogHostName = dialogHostName;

            DialogOpenedEventHandler eventHandler = (sender, eventArgs) =>
            {
                if (viewModel is IDialogHostAware aware)
                {
                    aware.OnDialogOpend(parameters);
                }
                eventArgs.Session.UpdateContent(content);
            };
            IDialogResult? result= (IDialogResult?)await DialogHost.Show(dialogContent, viewModel.DialogHostName, eventHandler);
            if(result==null)
                result=new DialogResult(ButtonResult.Cancel);
            return result;
        }
    }
}
