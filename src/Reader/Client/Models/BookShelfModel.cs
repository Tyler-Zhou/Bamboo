using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    /// <summary>
    /// 书架实体
    /// </summary>
    public class BookShelfModel:BaseModel
    {

        #region 名字
        private string _Name = "";
        /// <summary>
        /// 名字
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
            }
        }
        #endregion

        

    }
}
