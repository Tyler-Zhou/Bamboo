using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Client.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class CityInfo : BaseInfo
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
    }
}
