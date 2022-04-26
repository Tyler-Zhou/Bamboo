using Bamboo.Client.Core.Common;
using Bamboo.Client.Core.Extensions;
using Bamboo.Client.Core.Helper;
using Bamboo.Client.Interface;
using Bamboo.Common;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace Bamboo.Client.ViewModels
{
    /// <summary>
    /// 登录视图模型
    /// </summary>
    public class LoginViewModel : BindableBase, IDialogAware
    {

        #region 成员(Member)
        #region 标题
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = "Bamboo";
        #endregion

        #region 账号
        /// <summary>
        /// Account
        /// </summary>
        private string _Account = "";
        /// <summary>
        /// 账号
        /// </summary>
        public string Account
        {
            get 
            { 
                if(string.IsNullOrWhiteSpace(_Account))
                {
                    _Account = ApplicationContext.Account;

                }
                return _Account; 
            }
            set { _Account = value; RaisePropertyChanged(); }
        }
        #endregion

        #region 密码
        /// <summary>
        /// Password
        /// </summary>
        private string _Password = "";
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return _Password; }
            set { _Password = value; RaisePropertyChanged(); }
        }
        #endregion

        #region 选择项索引
        /// <summary>
        /// SelectIndex
        /// </summary>
        private int _SelectIndex = 0;
        /// <summary>
        /// 选择项索引
        /// </summary>
        public int SelectIndex
        {
            get { return _SelectIndex; }
            set { _SelectIndex = value; RaisePropertyChanged(); }
        }
        #endregion

        #region 注册用户传输对象
        /// <summary>
        /// ResgiterUser
        /// </summary>
        private ResgiterUserDto _ResgiterUser = new ResgiterUserDto();
        /// <summary>
        /// 注册用户传输对象
        /// </summary>
        public ResgiterUserDto ResgiterUser
        {
            get { return _ResgiterUser; }
            set { _ResgiterUser = value; RaisePropertyChanged(); }
        }
        #endregion 
        #endregion

        #region 命令(Command)
        /// <summary>
        /// 执行命令
        /// </summary>
        public DelegateCommand<string> ExecuteCommand { get; private set; }
        #endregion

        #region 服务(Service)
        /// <summary>
        /// 验证服务
        /// </summary>
        private readonly IAuthenticationService _AuthenticationService;
        /// <summary>
        /// 事件聚合器
        /// </summary>
        private readonly IEventAggregator _EventAggregator;

        /// <summary>
        /// 请求关闭动作
        /// </summary>
        public event Action<IDialogResult>? RequestClose;
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 登录视图模型
        /// </summary>
        /// <param name="authenticationService">验证服务</param>
        /// <param name="eventAggregator">事件聚合器</param>
        public LoginViewModel(IAuthenticationService authenticationService, IEventAggregator eventAggregator)
        {
            ResgiterUser = new ResgiterUserDto();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            _AuthenticationService = authenticationService;
            _EventAggregator = eventAggregator;
        }
        #endregion

        #region 方法(Method)

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CanCloseDialog()
        {
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        public void OnDialogClosed()
        {
            LoginOut();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        public void OnDialogOpened(IDialogParameters parameters)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        private void Execute(string obj)
        {
            switch (obj)
            {
                case "Login": Login(); break;
                case "LoginOut": LoginOut(); break;
                case "Resgiter": Resgiter(); break;
                case "ResgiterPage": SelectIndex = 1; break;
                case "Return": SelectIndex = 0; break;
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        async void Login()
        {
            if (string.IsNullOrWhiteSpace(Account) ||
                string.IsNullOrWhiteSpace(Password))
            {
                return;
            }

            var loginResult = await _AuthenticationService.Login(new UserDto()
            {
                Account = Account,
                Password = Password
            });
            if (loginResult != null)
            {
                if (loginResult.Status)
                {
                    UserDto userDto = JsonSerializerHelper.DeserializeObject<UserDto>("" + loginResult.Result);
                    ApplicationContext.UserId = userDto.Id;
                    ApplicationContext.Account = userDto.Account;
                    ApplicationContext.UserName = userDto.UserName;
                    Configure.Current.Add("Account", ApplicationContext.Account);
                    RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
                }
                else
                {
                    //登录失败提示...
                    _EventAggregator.SendMessage(loginResult.Message, "Login");

                }
            }
            else
            {
                _EventAggregator.SendMessage("返回对象[loginResult]为空！", "Login");
            }
        }
        /// <summary>
        /// 注册
        /// </summary>
        private async void Resgiter()
        {
            if (string.IsNullOrWhiteSpace(ResgiterUser.Account) ||
                string.IsNullOrWhiteSpace(ResgiterUser.UserName) ||
                string.IsNullOrWhiteSpace(ResgiterUser.Password) ||
                string.IsNullOrWhiteSpace(ResgiterUser.NewPassword))
            {
                _EventAggregator.SendMessage("请输入完整的注册信息！", "Login");
                return;
            }

            if (ResgiterUser.Password != ResgiterUser.NewPassword)
            {
                _EventAggregator.SendMessage("密码不一致,请重新输入！", "Login");
                return;
            }

            var resgiterResult = await _AuthenticationService.Resgiter(new UserDto()
            {
                Account = ResgiterUser.Account,
                UserName = ResgiterUser.UserName,
                Password = ResgiterUser.Password
            });
            if (resgiterResult != null)
            {
                if (resgiterResult.Status)
                {
                    _EventAggregator.SendMessage("注册成功", "Login");
                    //注册成功,返回登录页页面
                    SelectIndex = 0;
                }
                else
                {
                    _EventAggregator.SendMessage(resgiterResult.Message, "Login");
                }
            }
            else
            {
                _EventAggregator.SendMessage("返回对象为空！", "Login");
            }
        }
        /// <summary>
        /// 注销
        /// </summary>
        void LoginOut()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.No));
        }

        #endregion
    }
}
