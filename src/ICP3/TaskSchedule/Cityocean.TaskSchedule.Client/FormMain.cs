#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/4/27 11:46:35
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using Cityocean.Crawl.CommonLibrary;
using Cityocean.Crawl.LogComponents;
using System;
using System.Windows.Forms;

namespace Cityocean.TaskSchedule.Client
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormMain : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 菜单点击
        /// </summary>
        private void tsMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem tsMenuItem = sender  as ToolStripMenuItem;
                if (tsMenuItem != null)
                {
                    string menuTag = tsMenuItem.Tag==null?"":tsMenuItem.Tag.ToString();
                    if (menuTag.IsNullOrEmpty())
                        return;
                    switch (menuTag)
                    {
                        case "ServiceStatus":
                            FormServiceStatus formMonitor = new FormServiceStatus
                            {
                                MdiParent = this,
                                WindowState = FormWindowState.Maximized,
                                ShowIcon=false,
                            };
                            formMonitor.Show();
                            break;
                        case "KillJSEXE":
                            KillCrawlProcess();
                            break;
                        case "Terminal":
                            FormTerminal formTerminal = new FormTerminal
                            {
                                MdiParent = this,
                                WindowState = FormWindowState.Maximized,
                                ShowIcon = false,
                            };
                            formTerminal.Show();
                            break;
                        case "CargoTracking":
                            FormCargoTracking formCargoTracking = new FormCargoTracking
                            {
                                MdiParent = this,
                                WindowState = FormWindowState.Maximized,
                                ShowIcon=false,
                            };
                            formCargoTracking.Show();
                            break;
                        case "SailingSchedule":
                            FormSailingSchedule formSailingSchedule = new FormSailingSchedule
                            {
                                MdiParent = this,
                                WindowState = FormWindowState.Maximized,
                                ShowIcon = false,
                            };
                            formSailingSchedule.Show();
                            break;
                        case "Clean":
                            CleanFile();
                            break;
                        case "Exit":
                            Environment.Exit(0);
                            break;
                    }
                    //ShowChildFrom(menuTag);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramFromName"></param>
        private void ShowChildFrom(string paramFromName)
        {
            foreach (Form childrenForm in MdiChildren)
            {
                //检测是不是当前子窗体名称

                if (childrenForm.Name != paramFromName) continue;
                //是的话就是把他显示   
                childrenForm.Visible = true;
                //并激活该窗体  
                childrenForm.Activate();
                childrenForm.WindowState = FormWindowState.Maximized;
                return;
            }
            Form formChildren = new Form
            {
                MdiParent = this, 
                WindowState = FormWindowState.Maximized,
                Name=paramFromName,
            };
            formChildren.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        private void KillCrawlProcess()
        {
            try
            {
                string crawlProcesss = INIHelper.Instance.IniReadValue(CommonConstants.MODULENAME_SERVICECONFIG,
                        CommonConstants.CONFIG_CRAWLPROCESSS);
                foreach (var crawlProcess in crawlProcesss.Split(','))
                {
                    ProcessHelper.KillProcess(crawlProcess);
                }
            }
            catch (Exception ex)
            {
                LogService.Error("System","",ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CleanFile()
        {
            LogService.Info("System", "清除日志");
            FileDirectoryEnumerable fdeOldFile = new FileDirectoryEnumerable
            {
                SearchPath = GlobalVariable.ProgramDirectory,
                ReturnStringType = false,
                SearchForDirectory = true,
                SearchForFile = true,
                ThrowIOException = true,
            };
            foreach (object fdItem in fdeOldFile)
            {

            }
        }
    }
}
