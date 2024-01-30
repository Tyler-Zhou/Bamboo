#region Comment

/*
 * 
 * FileName:    ProgramStart.cs
 * CreatedOn:   2014/5/14 星期三 9:36:16
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->程序启动管理类
 *      ->1.通过判断当前打开窗体的个数来决定是否退出程序(进程)
 * History：
 * 
 * 
 * 
 * 
 */

#endregion


using System.Windows.Forms;
using System;
using System.ComponentModel;

namespace ICP.Document
{
    /// <summary>
    /// 程序启动管理类
    /// </summary>
    public class ProgramStart : ApplicationContext
    {
        /// <summary>
        /// 正在运行窗体数量
        /// </summary>
        private int formCount;
        /// <summary>
        /// 启动窗体
        /// </summary>
        private FormStart form1;
        /// <summary>
        /// 主窗体
        /// </summary>
        private FormMain form2;

        public ProgramStart()
        {
            formCount = 0;

            //监听应用程序退出事件
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);

            //创建程序窗体
            form1 = new FormStart();
            //监听关闭窗体后事件(FormClosed)
            form1.Closed += new EventHandler(OnFormClosed);
            //监听正在关闭窗体事件(OnFormClosing)
            form1.Closing += new CancelEventHandler(OnFormClosing);
            formCount++;
            form1.Show();
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
        }

        private void OnFormClosing(object sender, CancelEventArgs e)
        {
            if (sender is FormStart)
            {
                if (form1.DialogResult == DialogResult.OK)
                {
                    //创建程序窗体
                    form2 = new FormMain();
                    //监听关闭窗体后事件(FormClosed)
                    form2.Closed += new EventHandler(OnFormClosed);
                    //监听正在关闭窗体事件(OnFormClosing)
                    form2.Closing += new CancelEventHandler(OnFormClosing);
                    formCount++;

                    form2.Show();
                }
            }
        }

        private void OnFormClosed(object sender, EventArgs e)
        {
            //总窗体数量-1
            formCount--;
            //不包含窗体时退出程序
            if (formCount == 0)
            {
                ExitThread();
            }
        }
    }
}
