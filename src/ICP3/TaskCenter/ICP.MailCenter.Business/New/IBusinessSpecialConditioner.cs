using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 基类业务面板特殊处理逻辑
    /// </summary>
    public interface IBusinessSpecialConditioner
    {
        /// <summary>
        /// 特殊业务逻辑判断决定后，是否可以执行查询数据
        /// </summary>
        /// <param name="baseBusinessPart"></param>
        /// <param name="templateCode"></param>
        /// <returns></returns>
        bool QueryDataFired(IBaseBusinessPart_New baseBusinessPart, string templateCode, string queryString);
        /// <summary>
        /// 特殊处理，判断是否是未知业务面板
        /// </summary>
        /// <param name="templateCode"></param>
        /// <returns></returns>
        bool IsUnknownBusinessPart(string templateCode);
        /// <summary>
        /// 设置工具栏Element是否可见
        /// </summary>
        /// <param name="visibility"></param>
        void BarItemVisibility(BarManager baseBarManager, BarItemVisibility visibility);
    }
}
