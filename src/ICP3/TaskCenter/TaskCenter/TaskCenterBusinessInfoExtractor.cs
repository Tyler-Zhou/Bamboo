using ICP.Framework.CommonLibrary.Common;
using ICP.Operation.Common.ServiceInterface;
using ICP.TaskCenter.ServiceInterface;
using System;
using EnumCommonConstants = ICP.Operation.Common.ServiceInterface.CommonConstants;


namespace ICP.TaskCenter.UI
{
    /// <summary>
    /// 业务信息抽取
    /// </summary>
    public class TaskCenterBusinessInfoExtractor : IBusinessInfoExtractor
    {
        #region IBusinessInfoExtractor 成员

        /// <summary>
        /// 业务信息抽取：从传入参数中抽取业务信息转换成[业务操作上下文]
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="getRealTemplateCode"></param>
        /// <returns>业务操作上下文</returns>
        public BusinessOperationContext Extract(object parameter, bool getRealTemplateCode)
        {
            NodeInfo nodeInfo = parameter as NodeInfo;
            BusinessOperationContext context = new BusinessOperationContext();
            if (nodeInfo != null && nodeInfo.OperationType == null)
            {
                string typeName = nodeInfo.ViewCode.Split(new char[] { '_' })[1];
                context.OperationType = (OperationType)Enum.Parse(typeof(OperationType), typeName);
            }
            else
            {
                context.OperationType = nodeInfo.OperationType;
            }
            context[EnumCommonConstants.TemplateCodeKey] = nodeInfo.ViewCode;
            if (!string.IsNullOrEmpty(nodeInfo.AdvanceQueryString))
            {
                context[EnumCommonConstants.AdvanceQueryStringKey] = nodeInfo.AdvanceQueryString;
            }
            else
            {
                context[EnumCommonConstants.AdvanceQueryStringKey] = string.Empty;
            }
            return context;

        }

        #endregion
    }
}
