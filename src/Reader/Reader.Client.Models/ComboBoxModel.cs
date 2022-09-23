using Prism.Mvvm;

namespace Reader.Client.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ComboBoxModel: BindableBase
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
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 描述
        private string _Description = "";
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}
