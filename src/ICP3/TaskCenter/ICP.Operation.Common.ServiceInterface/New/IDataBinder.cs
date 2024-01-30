
namespace ICP.Operation.Common.ServiceInterface
{  
    /// <summary>
    /// 业务面板数据绑定接口
    /// </summary>
   public interface IDataBinder
    {  
       /// <summary>
       /// 绑定数据
       /// </summary>
       /// <param name="basePart">业务面板</param>
        /// <param name="data">需要绑定的数据</param>
       /// <param name="parameter">驱动面板显示的参数，如任务中心所选择的视图节点NodeInfo,邮件中心所选择的邮件转换后的消息(Message)实体</param>
       void DataBind(IBaseBusinessPart_New basePart, object data, object parameter);
    }
}
