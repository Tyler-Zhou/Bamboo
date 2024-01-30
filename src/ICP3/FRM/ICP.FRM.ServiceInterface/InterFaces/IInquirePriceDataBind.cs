using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FRM.ServiceInterface.InterFaces
{
    public interface IInquirePriceDataBind
    {
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="data">数据源</param>
        void DataBind(object data);
    }
}
