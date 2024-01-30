using ICP.Operation.Common.ServiceInterface;

namespace ICP.TaskCenter.UI
{
    /// <summary>
    /// 数据绑定后处理
    /// </summary>
    public class TaskCenterQueryPostHandler : IPostDataBindHandler
    {

        #region IPostDataBindHandler 成员
        /// <summary>
        /// 数据绑定后处理
        /// </summary>
        /// <param name="businessPart">业务面板</param>
        /// <param name="result">查询返回的数据</param>
        /// <param name="parameter">驱动面板显示的参数</param>
        public void PostHandle(IBaseBusinessPart_New businessPart, object result, object parameter)
        {
            //在没有数据的情况下，触发当前行改变事件，以刷新子列表
            //ICP.Operation.Common.UI.ListBaseBusinessPart listPart = businessPart as ICP.Operation.Common.UI.ListBaseBusinessPart;

            //string expresstion = "IsValid='False'";
            //StyleFormatCondition formatCondition = UIHelper.GetIsValidStyleFormationCondition(expresstion,
            //                                                                                  FontStyle.Strikeout,
            //                                                                                  listPart);
            //listPart.AddBusinessStyleFormationCondition(formatCondition);
            //System.Data.DataTable dt = result as System.Data.DataTable;

            //listPart.SetFocusedRowHandle(-1);

        }

        #endregion
    }
}
