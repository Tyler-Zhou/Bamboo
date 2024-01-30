namespace CRM.Client.Models
{
    public class UserInfo : BaseInfo
    {
        #region 代码
        private string _Code = "";
        /// <summary>
        /// 代码
        /// </summary>
        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                _Code = value;
                RaisePropertyChanged(nameof(Code));
            }
        }
        #endregion

        #region 名称
        private string _Name = "";
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                RaisePropertyChanged(nameof(Name));
            }
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
            get 
            { 
                return _Password; 
            }
            set 
            { 
                _Password = value; 
                RaisePropertyChanged(nameof(Password)); 
            }
        }
        #endregion

        #region 头像
        /// <summary>
        /// Password
        /// </summary>
        private byte[]? _Avatar;
        /// <summary>
        /// 头像
        /// </summary>
        public byte[]? Avatar
        {
            get
            {
                return _Avatar;
            }
            set
            {
                _Avatar = value;
                RaisePropertyChanged(nameof(Avatar));
            }
        }
        #endregion
    }
}
