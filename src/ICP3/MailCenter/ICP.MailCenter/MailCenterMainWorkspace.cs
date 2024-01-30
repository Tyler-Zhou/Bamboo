using System;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
namespace ICP.MailCenter.UI
{
    [ToolboxItem(false)]
    [SmartPart]
    public partial class MailCenterMainWorkspace : DevExpress.XtraEditors.XtraUserControl
    {
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public MailCenterMainWorkspace()
        {
            InitializeComponent();
            if (!LocalData.IsDesignMode)
            {
                this.KeyDown += new KeyEventHandler(MailCenterMainWorkspace_KeyDown);
                this.Load += new EventHandler(MailCenterMainWorkspace_Load);
                this.splitContainerControlMain.SplitterPositionChanged += OnSplitterPositionChanged;
                this.SizeChanged += new EventHandler(MailCenterMainWorkspace_SizeChanged);
                this.Disposed += delegate
                {
                    this.KeyDown -= this.MailCenterMainWorkspace_KeyDown;
                    IMainForm mainForm = ServiceClient.GetClientService<IMainForm>();
                    mainForm.ApplicationExit -= new EventHandler(mainForm_ApplicationExit);
                    this.SizeChanged -= new EventHandler(MailCenterMainWorkspace_SizeChanged);
                    this.splitContainerControlMain.SplitterPositionChanged -= OnSplitterPositionChanged;
                    if (this.WorkItem != null)
                    {
                        this.WorkItem.Items.Remove(this);
                        this.WorkItem = null;
                    }
                };
            }
        }
        /// <summary>
        /// 避免系统将文件夹列表和邮件列表宽度设置为零,没有将其显示出来
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MailCenterMainWorkspace_SizeChanged(object sender, EventArgs e)
        {
            if (this.splitContainerControlFolderAndList.SplitterPosition <= 25)
            {
                this.splitContainerControlFolderAndList.SplitterPosition = 50;
            }
            if (this.splitContainerControlMain.SplitterPosition <= 50)
            {
                this.splitContainerControlMain.SplitterPosition = 120;
            }

            this.BringToFront();
        }

        void MailCenterMainWorkspace_Load(object sender, EventArgs e)
        {
            IMainForm mainForm = ServiceClient.GetClientService<IMainForm>();
            mainForm.ApplicationExit += new EventHandler(mainForm_ApplicationExit);
            this.SetWorkSpaceWidth();

        }
        private void OnSplitterPositionChanged(object sender, EventArgs e)
        {
            if (isFirstSplitterViewList)
            {
                isFirstSplitterViewList = false;
                return;
            }

            InnerSplitter();
        }
        /// <summary>
        /// 用户使用分割控件分割一次后，然后代码执行分割，会自动再执行一次分割事件，需要过滤
        /// </summary>
        private bool IsSplitter = false;
        private delegate void SplitterDelegate();
        [CommandHandler("Command_InternalMailBusinessPartFixedSize")]
        public void Command_InternalMailBusinessPartFixedSize(object sende, EventArgs e)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new SplitterDelegate(InnerSplitter));
            else
                InnerSplitter();
        }

        private void InnerSplitter()
        {
            if (IsSplitter)
            {
                IsSplitter = false;
                return;
            }

            //当前查看的是否是内部邮件沟通面板
            if (MailListPresenter.IsVisitInternalMailBusinessPart())
            {
                int width = splitContainerControlMain.SplitterPosition;
                if (this.Size.Width - this.splitContainerControlFolderAndList.Panel1.Width > 500)
                {
                    if (this.Size.Width - width < 415)
                    {
                        IsSplitter = true;
                        if ((this.Size.Width - 415) > 10)
                            splitContainerControlMain.SplitterPosition = this.Size.Width - 415;
                        else
                            splitContainerControlMain.SplitterPosition = this.Size.Width - 415;
                    }
                }
            }
        }

        void mainForm_ApplicationExit(object sender, EventArgs e)
        {
            this.SaveWorkSpaceWidth();
        }

        void MailCenterMainWorkspace_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && e.Modifiers == Keys.Alt)
            {
                e.Handled = true;
            }
        }
        #region 面板大小自定义相关
        private string folderWidthKey = "FolderWidth";
        private const string listWidthKey = "ListWidth";

        /// <summary>
        /// 标识第一次打开邮件中心分割视图列表
        /// </summary>
        private bool isFirstSplitterViewList = true;
        /// <summary>
        /// 设置文件夹列表面板和邮件列表面板宽度，设置值来源于客户端用户自定义配置文件
        /// </summary>                
        private void SetWorkSpaceWidth()
        {
            MailListPresenter.SetSplitterPosition(splitContainerControlFolderAndList, folderWidthKey, 220);
            MailListPresenter.SetSplitterPosition(splitContainerControlMain, listWidthKey, 750);
        }
        void SaveWorkSpaceWidth()
        {
            MailListPresenter.SaveSplitterPosition(folderWidthKey, this.splitContainerControlFolderAndList.SplitterPosition.ToString());
            MailListPresenter.SaveSplitterPosition(listWidthKey, this.splitContainerControlMain.SplitterPosition.ToString());
        }

        #endregion
    }
}
