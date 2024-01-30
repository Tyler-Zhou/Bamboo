using System.Data;
using System.Linq;
using ICP.Common.CommandHandler.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using ICP.Operation.Common.UI;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;

namespace ICP.MailCenter.Business.UI
{
    /// <summary>
    /// 消息业务面板数据绑定器
    /// </summary>
    public class MessageDataBinder : IDataBinder
    {
        [ServiceDependency]
        public WorkItem RootWorkItem
        {
            get;
            set;
        }
        /// <summary>
        /// 海出命令处理类
        /// </summary>
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public CommonCommandHandler commandHandler
        {
            get;
            set;
        }

        private ListBaseBusinessPart currentBasePart;
        private string EmailAddress { get; set; }

        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="basePart"></param>
        /// <param name="data"></param>
        /// <param name="parameter"></param>
        public void DataBind(IBaseBusinessPart_New basePart, object data, object parameter)
        {
            ICP.Message.ServiceInterface.Message message = parameter as ICP.Message.ServiceInterface.Message;
            EmailAddress = message.SendFrom;
            commandHandler.UnRegisterFocusedRowChanged();
            currentBasePart = basePart as ListBaseBusinessPart;
            commandHandler.RegisterFocusedRowChanged();
            BusinessQueryResult result = data as BusinessQueryResult;
            if (currentBasePart != null && result != null)
            {
                ////未知业务面板特殊处理逻辑，如果是根据Subject关键字来搜索，并且没有查找到数据，不需要显示列。
                ////如果是根据用户输入关键字去搜索，就需要显示列。
                //int flag = -1, count = -1;
                //if (!UIHelper.IsNeedGenerateColumns(RootWorkItem))
                //{
                //    if (result.Dt == null || result.Dt.Rows.Count == 0)
                //        count = 0;
                //    else
                //    {
                //        //如果是Sql Server 返回没有找到数据的数据行，需要过滤

                //        if (result.Dt.Rows[0][0] == null ||
                //            result.Dt.Rows[0][0].ToString().Contains("传入在视图Code不存在"))
                //        {
                //            count = 0;
                //        }
                //        else
                //        {
                //            count = result.Dt.Rows.Count;
                //        }
                //    }
                //    flag = 0;
                //}
                //else
                //{
                //    if (result.Dt == null)
                //        count = -1;
                //    else
                //    {
                //        if (result.Dt.Rows.Count == 0)
                //            count = 0;
                //        else if (result.Dt.Rows.Count > 0)
                //        {
                //            if (result.Dt.Rows[0][0].ToString().Contains("传入在视图Code不存在"))
                //                count = 0;
                //            else
                //                count = result.Dt.Rows.Count;
                //        }
                //    }

                //    flag = -1;
                //}

                //if (
                //    currentBasePart.TemplateCode.Equals(
                //        ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4Unknown.ToString()) &&
                //    count == flag)
                //{
                //    currentBasePart.SpeciallyInitMailLink4Unknown(false);
                //    currentBasePart.SetDataSource(new DataTable());
                //}
                //else
                //{
                currentBasePart.GenerateGridColumns();
                //commandHandler.InitControl(basePart, count, currentBasePart.TemplateCode, message.SendFrom);
                if (result.Dt != null && result.Dt.Rows.Count > 0)
                {
                    //过滤重复的业务数据
                    result.Dt = result.Dt.DefaultView.ToTable(true, (from DataColumn col in result.Dt.Columns
                                                                     select col.ColumnName).ToArray());

                    result.Dt.Columns["Selected"].ReadOnly = false;

                }

                currentBasePart.SetDataSource(result.Dt);
                //}
                commandHandler.OnListPartFocusedRowChanged(basePart, null);
            }
        }

    }
}
