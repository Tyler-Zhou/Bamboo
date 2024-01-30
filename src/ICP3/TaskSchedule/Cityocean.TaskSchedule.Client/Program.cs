#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/3/3 14:48:52
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Windows.Forms;
using Cityocean.Crawl.LogComponents;

namespace Cityocean.TaskSchedule.Client
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            LogService.Instance().Register();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
