using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.Business.Common.ServiceInterface;
using ICP.Common.CommandHandler.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.IO;
using MethodInvoker = System.Windows.Forms.MethodInvoker;

namespace ICP.TaskCenter.UI.MainWork
{

    /// <summary>
    /// 任务中心总面板定位SmartPart
    /// </summary>
    [SmartPart]
    public partial class MainWorkSpace : XtraUserControl
    {
        #region 变量
        /// <summary>
        /// 根WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem RootWorkItem { get; set; }

        /// <summary>
        /// 海出面板分隔宽度固定值
        /// </summary>
        const string OceanExportSplitWidthKey = "OceanExportSplitWidthKey";
        /// <summary>
        /// 业务面板工作空间
        /// </summary>
        public SplitGroupPanel TaskItemsWorkSpace
        {
            get
            {
                return splitContainerControl1.Panel2;
            }
        }
        /// <summary>
        /// 海进命令处理类
        /// </summary>
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public OceanImportCommandHandler OceanImportCommandHandler
        {
            get;
            set;
        }
        //void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    OceanImportCommandHandler.PayNtautomaticMail();
        //}
        #endregion

        #region 构造函数
        /// <summary>
        ///默认构造函数
        /// </summary>
        public MainWorkSpace()
        {
            InitializeComponent();
            if (LocalData.IsDesignMode)
            {
                return;
            }
            Load += delegate
            {
                IMainForm form = ServiceClient.GetClientService<IMainForm>();
                Form mainForm = form as Form;
                mainForm.SizeChanged += new EventHandler(mainForm_SizeChanged);
                //每次打开任务中心把ICP文件夹的任务中心记录的错误日志删除，保留当天的
                if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "任务中心.Log")))
                {
                    File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "任务中心.Log"));
                }
                if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "回调记录.Log")))
                {
                    File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "回调记录.Log"));
                }
                if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BL列表记录日志.Log")))
                {
                    File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BL列表记录日志.Log"));
                }
                //if (LocalData.UserInfo.DefaultDepartmentName.Contains("财务") || LocalData.UserInfo.DefaultDepartmentName.Contains("CW"))
                //{
                //    System.Timers.Timer t = new System.Timers.Timer(6000);//15分钟执行一次计划任务 
                //    t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)； 
                //    t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
                //    t.Elapsed += t_Elapsed;
                //}
            };
            Disposed += delegate
            {
                IMainForm form = ServiceClient.GetClientService<IMainForm>();
                Form mainForm = form as Form;
                mainForm.SizeChanged -= mainForm_SizeChanged;
                if (RootWorkItem != null)
                {
                    RootWorkItem.Workspaces.Remove(ViewListWorkSpace);
                    RootWorkItem.SmartParts.Remove(this);
                    ViewListWorkSpace.PerformLayout();


                    RootWorkItem.Items.Remove(this);

                    RootWorkItem = null;
                    PerformLayout();
                    //清空TemplateCode，邮件服务列表将不再通过TemplateCode获取右键菜单(右键菜单包含通过模板发送方法)
                    ServiceClient.GetService<IICPCommonOperationService>().SetTemplateCode("");
                }
            };
            Load += delegate
            {
                UserPrivateInfo.SetSplitterPosition(splitContainerControl1, OceanExportSplitWidthKey, 220);
            };

        }

        #endregion

        #region 窗体的尺寸改变
        /// <summary>
        /// 窗体的尺寸改变
        /// </summary>
        void mainForm_SizeChanged(object sender, EventArgs e)
        {
            Form mainForm = sender as Form;
            if (mainForm.WindowState != FormWindowState.Minimized)
            {
                ChangePanel2FirstChildWidth();
            }
        }
        /// <summary>
        /// 分隔符位置改变
        /// </summary>
        private void splitContainerControl1_SplitterPositionChanged(object sender, EventArgs e)
        {
            UserPrivateInfo.SaveSplitterPosition(OceanExportSplitWidthKey, splitContainerControl1.SplitterPosition.ToString());
            ChangePanel2FirstChildWidth();
        }
        /// <summary>
        /// 业务面板对应子面板宽度调整
        /// </summary>
        private void ChangePanel2FirstChildWidth()
        {
            if (splitContainerControl1.Panel2.Controls.Count > 0)
            {
                Control firstChildControl = splitContainerControl1.Panel2.Controls[0];
                firstChildControl.Width = splitContainerControl1.Panel2.Width;
                firstChildControl.Height = splitContainerControl1.Panel2.Height;
            }

        }
        #endregion

        #region  方法
        /// <summary>
        /// 页面取消隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(TaskCenterCommandConstants.SetReadOnly)]
        public void Command_SetReadOnly(object sender, EventArgs e)
        {
            splitContainerControl1.Panel2.Visible = false;
        }
        /// <summary>
        /// 页面隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(TaskCenterCommandConstants.CancelReadOnly)]
        public void Command_CancelReadOnly(object sender, EventArgs e)
        {
            splitContainerControl1.Panel2.Visible = true;
        }

        /// <summary>
        /// 页面置灰
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(TaskCenterCommandConstants.CommandDisableTaskCenter)]
        public void CommandDisableTaskCenter(object sender, EventArgs e)
        {

            ViewListWorkSpace.Enabled = false;
            splitContainerControl1.Panel2.Enabled = false;

        }

        /// <summary>
        /// 页面取消置灰
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(TaskCenterCommandConstants.CommandEnableTaskCenter)]
        public void CommandEnableTaskCenter(object sender, EventArgs e)
        {
            lock (MainWorkWorkItem.synObj)
            {
                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(InnerSetEnabled), null);

                }
                else
                {
                    InnerSetEnabled();
                }

            }
        }

        /// <summary>
        /// 初始化设置面板的可用状态
        /// </summary>
        private void InnerSetEnabled()
        {
            if (splitContainerControl1.Panel2.Enabled == false && ViewListWorkSpace.Enabled == false)
            {
                splitContainerControl1.Panel2.Enabled = true;
                ViewListWorkSpace.Enabled = true;
            }
        }

        #endregion
    }
}
