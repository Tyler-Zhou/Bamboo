using ICP.Message.ServiceInterface;

namespace ICP.MailCenter.Business.ServiceInterface
{
    /// <summary>
    /// 内部邮件业务面板接口
    /// </summary>
    public interface IInternalMailPart
    {
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="userProperties"></param>
        void BindData(MessageUserPropertiesObject userProperties);
        /// <summary>
        /// 数据源
        /// </summary>
        object DataSource { set; }
        /// <summary>
        /// 动作类型
        /// </summary>
        MailActionType ActionType { get; set; }
        /// <summary>
        /// 是否只读
        /// </summary>
        bool ReadOnly { get; set; }
    }
    /// <summary>
    /// 内部邮件链接动作类型
    /// </summary>
    public enum MailActionType
    {
        /// <summary>
        /// 创建订单
        /// </summary>
        COD = 1,
        /// <summary>
        /// 下达订单（业务联单）
        /// </summary>
        ODA = 2,
        /// <summary>
        /// 变更订单
        /// </summary>
        ODM = 3,
        /// <summary>
        /// 创建订舱单
        /// </summary>
        CSO = 4,
        /// <summary>
        /// 申请订舱
        /// </summary>
        SOA = 5,
    }
}
