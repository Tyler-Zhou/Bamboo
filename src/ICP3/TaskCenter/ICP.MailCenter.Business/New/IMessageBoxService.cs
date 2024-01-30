using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using System.Windows.Forms;
namespace ICP.Operation.Common.ServiceInterface
{ 
    /// <summary>
    /// 消息对话框服务接口
    /// </summary>
   public interface IMessageBoxService
    {  
       /// <summary>
       /// 显示消息提示框
       /// 标题：ICP
       /// 按钮:确认按钮
       /// </summary>
        /// <param name="text">消息内容</param>
       void ShowInfo(string text);
       /// <summary>
       /// 显示消息提示框
       /// </summary>
       /// <param name="text">消息内容</param>
       /// <param name="caption">标题</param>
       void ShowInfo(string text, string caption);
       /// <summary>
       /// 显示消息提示框
       /// </summary>
       /// <param name="text">消息内容</param>
       /// <param name="caption">标题</param>
       /// <param name="buttons">显示按钮组合</param>
       /// <returns></returns>
       DialogResult ShowInfo(string text, string caption, MessageBoxButtons buttons);
       /// <summary>
       /// 显示警告提示框
       /// </summary>
       /// <param name="text">警告提示内容</param>
       /// <returns></returns>
       DialogResult ShowWarning(string text);
       /// <summary>
       /// 显示警告提示框
       /// </summary>
       /// <param name="text">警告提示内容</param>
       /// <param name="caption">标题</param>
       /// <returns></returns>
       DialogResult ShowWarning(string text, string caption);
       /// <summary>
       /// 显示警告提示框
       /// </summary>
       /// <param name="text">警告提示内容</param>
       /// <param name="caption">标题</param>
       /// <param name="buttons">显示按钮组合</param>
       /// <returns></returns>
       DialogResult ShowWarning(string text, string caption, MessageBoxButtons buttons);
       /// <summary>
       /// 显示警告提示框
       /// </summary>
       /// <param name="text">警告提示内容</param>
       /// <param name="caption">标题</param>
       /// <param name="buttons">显示按钮组合</param>
       /// <param name="defaultButton">默认获取焦点的按钮</param>
       /// <returns></returns>
       DialogResult ShowWarning(string text, string caption, MessageBoxButtons buttons, MessageBoxDefaultButton defaultButton);
       /// <summary>
       /// 显示询问提示框
       /// </summary>
       /// <param name="text">询问提示内容</param>
       /// <returns></returns>
       DialogResult ShowQuestion(string text);
       /// <summary>
       /// 显示询问提示框
       /// </summary>
       /// <param name="text">询问提示内容</param>
       /// <param name="caption">标题</param>
       /// <returns></returns>
       DialogResult ShowQuestion(string text, string caption);
       /// <summary>
       /// 显示询问提示框
       /// </summary>
       /// <param name="text">询问提示内容</param>
       /// <param name="caption">标题</param>
       /// <param name="buttons">显示按钮组合</param>
       /// <returns></returns>
       DialogResult ShowQuestion(string text, string caption, MessageBoxButtons buttons);
       /// <summary>
       /// 显示询问提示框
       /// </summary>
       /// <param name="text">询问提示内容</param>
       /// <param name="caption">标题</param>
       /// <param name="buttons">显示按钮组合</param>
       /// <param name="defaultButton">默认获得焦点的按钮</param>
       /// <returns></returns>
       DialogResult ShowQuestion(string text, string caption, MessageBoxButtons buttons, MessageBoxDefaultButton defaultButton);
       /// <summary>
       /// 显示错误提示框
       /// </summary>
       /// <param name="text">错误提示内容</param>
       void ShowError(string text);
       /// <summary>
       /// 显示错误提示框
       /// </summary>
       /// <param name="text">错误提示内容</param>
       /// <param name="caption">标题</param>
       void ShowError(string text, string caption);
       /// <summary>
       /// 显示错误提示框
       /// </summary>
       /// <param name="text">错误提示内容</param>
       /// <param name="caption">标题</param>
       /// <param name="buttons">显示按钮组合</param>
       /// <returns></returns>
       DialogResult ShowError(string text, string caption, MessageBoxButtons buttons);
       /// <summary>
       /// 显示提示框
       /// </summary>
       /// <param name="text">提示内容</param>
       /// <param name="caption">标题</param>
       /// <param name="buttons">显示按钮组合</param>
       /// <param name="icon">窗体图标</param>
       /// <param name="defaultButton">默认获得焦点的按钮</param>
       /// <returns></returns>
       DialogResult Show(string text, string caption, MessageBoxButtons buttons,MessageBoxIcon icon,MessageBoxDefaultButton defaultButton);

    }
}
