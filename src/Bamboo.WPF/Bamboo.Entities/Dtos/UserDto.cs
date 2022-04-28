namespace Bamboo.Entities
{
    /// <summary>
    /// 用户传输对象
    /// </summary>
    public class UserDto : BaseDto
    {
        #region 账号
        /// <summary>
        /// Account
        /// </summary>
        private string _Account;
        /// <summary>
        /// 账号
        /// </summary>
        public string Account
        {
            get { return _Account; }
            set { _Account = value; OnPropertyChanged(); }
        }
        #endregion

        #region 用户名
        /// <summary>
        /// UserName
        /// </summary>
        private string _UserName;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; OnPropertyChanged(); }
        }
        #endregion

        #region 密码
        /// <summary>
        /// Password
        /// </summary>
        private string _Password;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return _Password; }
            set { _Password = value; OnPropertyChanged(); }
        }
        #endregion
    }
}
