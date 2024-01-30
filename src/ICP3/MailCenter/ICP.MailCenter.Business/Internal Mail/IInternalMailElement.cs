
namespace ICP.MailCenter.Business.ServiceInterface
{
    /// <summary>
    /// 实现工厂组成元素，邮件中心内部邮件业务面板必须继承该接口，以便生产对应的控件面板
    /// </summary>
    public interface IInternalMailElement
    {  
        /// <summary>
        ///界面是否可编辑
        /// </summary>
        bool Editable { get; set; }
        /// <summary>
        /// 设置数据源
        /// </summary>
        void DataBind(object param);
        /// <summary>
        /// 加载控件
        /// </summary>
        void LoadControl();
        /// <summary>
        /// 如果ReadOnly为true，则不保存数据，否则保存数据
        /// </summary>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        void SaveEditData(bool editable);
    }
}
