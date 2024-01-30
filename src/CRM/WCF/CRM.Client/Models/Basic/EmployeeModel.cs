namespace CRM.Client.Models
{
    /// <summary>
    /// 员工信息
    /// </summary>
    public class EmployeeModel : BaseInfo
    {
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

        #region 电话
        private string _Phone = "";
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone
        {
            get
            {
                return _Phone;
            }
            set
            {
                _Phone = value;
                RaisePropertyChanged(nameof(Phone));
            }
        }
        #endregion
    }
}
