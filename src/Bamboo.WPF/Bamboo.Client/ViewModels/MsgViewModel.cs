using Bamboo.Client.Core.Interface;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace Bamboo.Client.ViewModels
{
    /// <summary>
    /// 消息视图模型
    /// </summary>
    public class MsgViewModel : BindableBase, IDialogHostAware
    {
        #region 标题
        /// <summary>
        /// Title
        /// </summary>
        private string _Title = "";
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set { _Title = value; RaisePropertyChanged(); }
        }
        #endregion

        #region 内容
        /// <summary>
        /// Content
        /// </summary>
        private string _Content = "";
        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get { return _Content; }
            set { _Content = value; RaisePropertyChanged(); }
        }
        #endregion

        #region 对话框主机名称
        /// <summary>
        /// 对话框主机名称
        /// </summary>
        public string DialogHostName { get; set; } = "Root";
        #endregion

        #region 命令
        /// <summary>
        /// 确定
        /// </summary>
        public DelegateCommand SureCommand { get; set; }
        /// <summary>
        /// 取消
        /// </summary>
        public DelegateCommand CancelCommand { get; set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 消息视图模型
        /// </summary>
        public MsgViewModel()
        {
            SureCommand = new DelegateCommand(Sure);
            CancelCommand = new DelegateCommand(Cancel);
        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// 取消
        /// </summary>
        private void Cancel()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.No));
        }
        /// <summary>
        /// 确定
        /// </summary>
        private void Sure()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                DialogParameters param = new DialogParameters();
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK, param));
            }
        }

        /// <summary>
        /// 对话框打开时为控件赋值
        /// </summary>
        /// <param name="parameters"></param>
        public void OnDialogOpend(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Title"))
                Title = parameters.GetValue<string>("Title");

            if (parameters.ContainsKey("Content"))
                Content = parameters.GetValue<string>("Content");
        }
        #endregion
    }
}
