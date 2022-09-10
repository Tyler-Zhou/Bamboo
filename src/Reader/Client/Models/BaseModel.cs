using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid ID { get; set; }

    }
}
