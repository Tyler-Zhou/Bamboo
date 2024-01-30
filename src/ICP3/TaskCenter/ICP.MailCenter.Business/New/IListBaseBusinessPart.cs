using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.DataCache.ServiceInterface;
using System.Data;
using System.Drawing;

namespace ICP.Operation.Common.ServiceInterface
{
   public interface IListBaseBusinessPart:IBaseBusinessPart_New
    {
        /// <summary>
        /// 保存列表自定义信息
        /// </summary>
        void SaveCustomColumnInfo();
        /// <summary>
        /// 获取列表自定义显示信息
        /// </summary>
        /// <returns></returns>
        UserCustomGridInfo GetUserCustomGridInfo();
        /// <summary>
        /// 获取列构建信息
        /// </summary>
        List<BusinessColumnInfo> GetColumnInfos(string templateCode);
       /// <summary>
       /// 设置列表数据源
       /// </summary>
       /// <param name="data"></param>
        void SetDataSource(object data);
       /// <summary>
       /// 列表行字体名称
       /// </summary>
        Font RowFont
        {
            get;
           
        }
    }
}
