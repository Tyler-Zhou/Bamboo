using ICP.Framework.CommonLibrary.Common;

namespace ICP.Operation.Common.ServiceInterface
{  
    /// <summary>
    /// 业务信息抽取接口
    /// </summary>
   public interface IBusinessInfoExtractor
    {
        /// <summary>
        /// 业务信息抽取：从传入参数中抽取业务信息转换成[业务操作上下文]
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="getRealTemplateCode"></param>
        /// <returns>业务操作上下文</returns>
        BusinessOperationContext Extract(object parameter, bool getRealTemplateCode);
    }
}
