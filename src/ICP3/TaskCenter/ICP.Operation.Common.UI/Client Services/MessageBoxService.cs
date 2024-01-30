//using DevExpress.XtraEditors;
//using ICP.Operation.Common.ServiceInterface;
//using System.Windows.Forms;

//namespace ICP.Operation.Common.UI
//{
//    /// <summary>
//    /// 消息对话框服务
//    /// </summary>
//    public class MessageBoxService : IMessageBoxService
//    {
//        /// <summary>
//        /// 默认标题文本
//        /// </summary>
//        string defaultCaption = "ICP";
//        #region IMessageBoxService 成员

//        /// <summary>
//        /// 显示消息提示框
//        /// 标题：ICP
//        /// 按钮:确认按钮
//        /// </summary>
//        /// <param name="text">消息内容</param>
//        public void ShowInfo(string text)
//        {
//            ShowInfo(text, defaultCaption);
//        }
//        /// <summary>
//        /// 显示消息提示框
//        /// </summary>
//        /// <param name="text">消息内容</param>
//        /// <param name="caption">标题</param>
//        public void ShowInfo(string text, string caption)
//        {
//            ShowInfo(text, caption, MessageBoxButtons.OK);
//        }
//        /// <summary>
//        /// 显示消息提示框
//        /// </summary>
//        /// <param name="text">消息内容</param>
//        /// <param name="caption">标题</param>
//        /// <param name="buttons">显示按钮组合</param>
//        /// <returns>对话框返回值</returns>
//        public DialogResult ShowInfo(string text, string caption, MessageBoxButtons buttons)
//        {
//            return Show(text, caption, buttons, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
//        }
//        /// <summary>
//        /// 显示警告提示框
//        /// </summary>
//        /// <param name="text">警告提示内容</param>
//        /// <returns>对话框返回值</returns>
//        public DialogResult ShowWarning(string text)
//        {
//            return ShowWarning(text, defaultCaption);
//        }
//        /// <summary>
//        /// 显示警告提示框
//        /// </summary>
//        /// <param name="text">警告提示内容</param>
//        /// <param name="caption">标题</param>
//        /// <returns>对话框返回值</returns>
//        public DialogResult ShowWarning(string text, string caption)
//        {
//            return ShowWarning(text, caption, MessageBoxButtons.OK);
//        }
//        /// <summary>
//        /// 显示警告提示框
//        /// </summary>
//        /// <param name="text">警告提示内容</param>
//        /// <param name="caption">标题</param>
//        /// <param name="buttons">显示按钮组合</param>
//        /// <returns>对话框返回值</returns>
//        public DialogResult ShowWarning(string text, string caption, MessageBoxButtons buttons)
//        {
//            return ShowWarning(text, caption, buttons, MessageBoxDefaultButton.Button1);
//        }
//        /// <summary>
//        /// 显示警告提示框
//        /// </summary>
//        /// <param name="text">警告提示内容</param>
//        /// <param name="caption">标题</param>
//        /// <param name="buttons">显示按钮组合</param>
//        /// <param name="defaultButton">默认获取焦点的按钮</param>
//        /// <returns>对话框返回值</returns>
//        public DialogResult ShowWarning(string text, string caption, MessageBoxButtons buttons, MessageBoxDefaultButton defaultButton)
//        {
//            return Show(text, caption, buttons, MessageBoxIcon.Warning, defaultButton);
//        }
//        /// <summary>
//        /// 显示询问提示框
//        /// </summary>
//        /// <param name="text">询问提示内容</param>
//        /// <returns>对话框返回值</returns>
//        public DialogResult ShowQuestion(string text)
//        {
//            return ShowQuestion(text, defaultCaption);
//        }
//        /// <summary>
//        /// 显示询问提示框
//        /// </summary>
//        /// <param name="text">询问提示内容</param>
//        /// <param name="caption">标题</param>
//        /// <returns>对话框返回值</returns>
//        public DialogResult ShowQuestion(string text, string caption)
//        {
//            return ShowQuestion(text, caption, MessageBoxButtons.OKCancel);
//        }
//        /// <summary>
//        /// 显示询问提示框
//        /// </summary>
//        /// <param name="text">询问提示内容</param>
//        /// <param name="caption">标题</param>
//        /// <param name="buttons">显示按钮组合</param>
//        /// <returns>对话框返回值</returns>
//        public DialogResult ShowQuestion(string text, string caption, MessageBoxButtons buttons)
//        {
//            return ShowQuestion(text, caption, buttons, MessageBoxDefaultButton.Button1);
//        }
//        /// <summary>
//        /// 显示询问提示框
//        /// </summary>
//        /// <param name="text">询问提示内容</param>
//        /// <param name="caption">标题</param>
//        /// <param name="buttons">显示按钮组合</param>
//        /// <param name="defaultButton">默认获得焦点的按钮</param>
//        /// <returns>对话框返回值</returns>
//        public DialogResult ShowQuestion(string text, string caption, MessageBoxButtons buttons, MessageBoxDefaultButton defaultButton)
//        {
//            return Show(text, caption, buttons, MessageBoxIcon.Question, defaultButton);
//        }
//        /// <summary>
//        /// 显示错误提示框
//        /// </summary>
//        /// <param name="text">错误提示内容</param>
//        public void ShowError(string text)
//        {
//            ShowError(text, defaultCaption);
//        }
//        /// <summary>
//        /// 显示错误提示框
//        /// </summary>
//        /// <param name="text">错误提示内容</param>
//        /// <param name="caption">标题</param>
//        public void ShowError(string text, string caption)
//        {
//            ShowError(text, caption, MessageBoxButtons.OK);
//        }
//        /// <summary>
//        /// 显示错误提示框
//        /// </summary>
//        /// <param name="text">错误提示内容</param>
//        /// <param name="caption">标题</param>
//        /// <param name="buttons">显示按钮组合</param>
//        /// <returns>对话框返回值</returns>
//        public DialogResult ShowError(string text, string caption, MessageBoxButtons buttons)
//        {
//            return Show(text, caption, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
//        }
//        /// <summary>
//        /// 显示提示框
//        /// </summary>
//        /// <param name="text">提示内容</param>
//        /// <param name="caption">标题</param>
//        /// <param name="buttons">显示按钮组合</param>
//        /// <param name="icon">窗体图标</param>
//        /// <param name="defaultButton">默认获得焦点的按钮</param>
//        /// <returns>对话框返回值</returns>
//        public DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
//        {
//            return XtraMessageBox.Show(text, caption, buttons, icon, defaultButton);
//        }

//        #endregion
//    }
//}
