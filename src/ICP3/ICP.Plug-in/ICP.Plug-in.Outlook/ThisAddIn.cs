using System.Globalization;
using ICP.MailCenterFramework.UI;
using System;
using Microsoft.Office.Core;

namespace ICP.Plug_in.Outlook
{
    public partial class ThisAddIn
    {
        #region 属性-国际化
        /// <summary>
        /// 国际化
        /// </summary>
        public static bool IsEnglish { get; set; }
        #endregion
        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            new Start().PluginStart();
            try
            {
                LanguageSettings languageSettings = Application.LanguageSettings;
                int lcid = languageSettings.LanguageID[MsoAppLanguageID.msoLanguageIDUI];
                CultureInfo officeUICulture = new CultureInfo(lcid);
                string culture = officeUICulture.Name.ToLower();
                IsEnglish=!culture.Equals("zh-cn");
                languageSettings = null;
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("OLCultrue", ex);
                IsEnglish = true;
            }
            if (HelpMailStore.IsNeedLoginICP)
            {
                Program.Main();
                OutlookPlugInManageService outlookPlug = new OutlookPlugInManageService();
                outlookPlug.InitControl();
            }
        }

        private void ThisAddIn_Shutdown(object sender, EventArgs e)
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
            Startup += ThisAddIn_Startup;
            Shutdown += ThisAddIn_Shutdown;
        }
        
        #endregion
    }
}
