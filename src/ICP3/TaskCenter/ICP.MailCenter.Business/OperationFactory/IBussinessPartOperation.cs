namespace ICP.Common.Business.ServiceInterface
{
    /// <summary>
    /// 业务处理接口
    /// </summary>
    public interface IBussinessPartOperation
    {
        BaseBusinessPart BaseBusinessPart { get; set; }

        /// <summary>
        /// 业务面板初始化入口
        /// 动作:1.解析邮件信息
        ///      2.注册工具栏，右键菜单栏拓展点
        ///      3.获取工具栏命令实体，构造工具栏
        ///      4.获取列实体列表，构造列表列
        ///      5.挂接命令处理
        /// </summary>
        /// <param name="initObj"></param>
        void init(string templateCode);

        /// <summary>
        /// 列表数据绑定
        /// </summary>
        /// <param name="initObj"></param>
        void BindData();

        ///// <summary>
        /////  构造函数
        ///// </summary>
        ///// <param name="baseBusinessPart">当前业务面板</param>
        ///// <param name="operationSourceType">当前业务面板来源</param>
        //IBussinessPartOperation(BaseBusinessPart BaseBusinessPart, OperationSourceType operationSourceType);
    }
}
