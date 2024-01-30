using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using ICP.MailCenterFramework.UI;
namespace ICP_OutlookAddIn
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            new Start().PluginStart();
            if (HelpMailStore.IsNeedLoginICP)
            {
                Program.Main();
                OutlookPlugInManageService outlookPlug = new OutlookPlugInManageService();
                outlookPlug.InitControl();
            }
        }


        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            OutlookPlugInManageService.DisposedControl();
        }

        #region VSTO 生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion
    }
}
