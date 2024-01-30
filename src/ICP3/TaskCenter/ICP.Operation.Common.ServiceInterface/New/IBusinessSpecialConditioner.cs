using System;
using System.Collections.Generic;
using DevExpress.Utils;
using ICP.Framework.CommonLibrary.Common;

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
        /// 是否自动填充SONO
        /// </summary>
        /// <returns></returns>
        bool IsAutoFileSONO();

        /// <summary>
        /// 保存操作日志
        /// </summary>
        /// <param name="templateCode"></param>
        /// <param name="operationIDs"></param>
        /// <param name="operationType"></param>
        /// <param name="formID"></param>
        void SaveOperationMemo(string templateCode, List<Guid> operationIDs, OperationType operationType, Guid formID);
        /// <summary>
        /// 邮件中心默认关联
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="parameter"></param>
        /// <param name="advanceQueryString"></param>
        void DefaultMessageRelation(BusinessQueryCriteria criteria,
                                    object parameter, string advanceQueryString);
        /// <summary>
        /// 设置行样式
        /// </summary>
        /// <param name="appearance"></param>
        void SetGridRowStyle(AppearanceObject appearance, Guid operationID, bool isValid);
    }
}
