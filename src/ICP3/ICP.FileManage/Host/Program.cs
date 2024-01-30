using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ICPwcfServer
{
    public static class Program
    {
        static Host hostForm;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            hostForm = new Host();
            Application.Run(new Host());
        }
        public static Host GetLog()
        {
            return hostForm;
        }
    }
}
