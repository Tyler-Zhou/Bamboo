namespace CRM.Client.Models
{
    /// <summary>
    /// 客户
    /// </summary>
    public class CustomerModel: BaseInfo
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

    }
}
