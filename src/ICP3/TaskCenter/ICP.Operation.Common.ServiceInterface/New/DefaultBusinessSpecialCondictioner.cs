using System;
using System.Collections.Generic;
using DevExpress.Utils;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.Operation.Common.ServiceInterface
{
    public class DefaultBusinessSpecialConditioner : IBusinessSpecialConditioner
    {
        #region IBusinessSpecialConditioner 成员

        /// <summary>
        /// 是否执行查询方法
        /// </summary>
        /// <param name="baseBusinessPart"></param>
        /// <param name="templateCode"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public bool QueryDataFired(IBaseBusinessPart_New baseBusinessPart, string templateCode, string queryString)
        {
            return true;
        }

        /// <summary>
        /// 保存业务日志
        /// </summary>
        /// <param name="templateCode"></param>
        /// <param name="operationIDs"></param>
        /// <param name="operationType"></param>
        /// <param name="formID"></param>
        public void SaveOperationMemo(string templateCode, List<Guid> operationIDs, OperationType operationType, Guid formID)
        {

        }

        /// <summary>
        /// 是否自动关联邮件
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="parameter"></param>
        /// <param name="advanceQueryString"></param>
        public void DefaultMessageRelation(BusinessQueryCriteria criteria,
                                            object parameter, string advanceQueryString)
        {
        }
        /// <summary>
        /// 是否过滤SONO
        /// </summary>
        /// <returns></returns>
        public bool IsAutoFileSONO()
        {
            return true;
        }

        /// <summary>
        /// 设置行样式
        /// </summary>
        /// <param name="appearance"></param>
        public void SetGridRowStyle(AppearanceObject appearance, Guid operationID, bool isValid)
        {
            if (!isValid)
                GridHelper.SetColorStyle(appearance,
                                                                                    PresenceStyle.Disabled);

        }

        #endregion
    }
}
