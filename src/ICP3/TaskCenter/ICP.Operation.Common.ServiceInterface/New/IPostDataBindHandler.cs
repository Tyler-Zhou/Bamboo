
namespace ICP.Operation.Common.ServiceInterface
{  
    /// <summary>
    /// 数据绑定后处理接口
    /// </summary>
   public interface IPostDataBindHandler
    {  
       /// <summary>
       /// 数据绑定后处理
       /// </summary>
       /// <param name="businessPart">业务面板</param>
       /// <param name="result">查询返回的数据</param>
       /// <param name="parameter">驱动面板显示的参数</param>
       void PostHandle(IBaseBusinessPart_New businessPart, object result, object parameter);
    }
}
