using System;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.MailCenter.Business.ServiceInterface
{
    /// <summary>
    /// 内部邮件面板构造抽象工厂
    /// </summary>
    public class InternalMailBusinessPartFactory
    {
        private static WorkItem _RootWorkItem;
        public static WorkItem RootWorkItem
        {
            get
            {
                if (_RootWorkItem == null)
                    _RootWorkItem = ServiceClient.GetClientService<WorkItem>();
                return _RootWorkItem;
            }

        }

        /// <summary>
        /// 构造实例
        /// </summary>
        /// <param name="assmbly"></param>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public static IInternalMailElement CreateInstance(string assmbly, string controlName, string templateCode)
        {

            Type type = Type.GetType(string.Format("{0}.{1},{0}", assmbly, controlName));
            IInternalMailElement ucElement = RootWorkItem.Items.AddNew(type, string.Format("{0}_InternalMail", templateCode)) as IInternalMailElement;            
            //IInternalMailContainer internalMailPart = (IInternalMailContainer)Assembly.Load(assmbly).CreateInstance(type.ToString());
            if (ucElement == null)
            {
                throw new NotImplementedException(LocalData.IsEnglish ? "the relation business part is not found!" : "没有找到相对应的面板!");
            }

            return ucElement;
        }

    }
}
