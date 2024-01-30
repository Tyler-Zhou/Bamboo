#region Comment

/*
 * 
 * FileName:    FrmEmailArchiving.cs
 * CreatedOn:   2014/4/30 星期三 18:03:06
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->邮件归档
 *   
 * History：
 * 
 * 
 * 
 * 
 */

#endregion

using DevExpress.XtraEditors;
using System.Windows.Forms;
using System;
using System.Threading;
using ICP.Framework.CommonLibrary.Client;
using System.Collections.Generic;
using Microsoft.Office.Interop.Outlook;
using ICP.MailCenter.ServiceInterface;
using Microsoft.Practices.CompositeUI;

namespace ICP.MailCenter.UI
{
    public partial class FrmEmailArchiving : XtraForm
    {
        #region 成员变量

        #region 服务
        /// <summary>
        /// 根WorkItem
        /// </summary>
        public WorkItem RootWorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        } 
        #endregion

        #region 私有变量
        //集合列表
        List<Object> items = null;
        //存储关联邮件OR回执文件夹对象
        MAPIFolder _relationOLFolder = null;
        //收件箱
        MAPIFolder _inboxFolder = null;
        //邮件对象
        _MailItem olItem = null;
        //回执对象
        ReportItem olReportItem = null;
        //进程最大值
        private int Process_MaxValue = 100;
        //进度
        private float num_Percent = 0;

        private string _ProjectName;
        //总邮件数量
        private int _MailCount = 0; 
        #endregion

        #region 属性
        private Microsoft.Practices.CompositeUI.Collections.ManagedObjectCollection<object> _SmartParts;
        public Microsoft.Practices.CompositeUI.Collections.ManagedObjectCollection<object> SmartParts
        {
            get
            {
                if (_SmartParts == null)
                    _SmartParts = this.RootWorkItem.SmartParts;
                return _SmartParts;
            }
        }

        public EmailFolderPart FolderPart
        {
            get
            {
                return SmartParts.Get<EmailFolderPart>(MailCenterWorkSpaceConstants.EmailFolderPart);
            }
        }
        /// <summary>
        /// 总邮件数量
        /// </summary>
        public int MailCount
        {
            get { return _MailCount; }
            set
            {
                _MailCount = value;
                num_Percent = (float)Process_MaxValue / _MailCount;
            }
        }
        /// <summary>
        /// 邮件项目
        /// </summary>
        public string MailProject
        {
            get { return this.lblMailProject.Text; }
            set
            {
                this.lblTotalEmail.Text = string.Format((LocalData.IsEnglish ? "From [{0}] search associated mail..." : "正在从[{0}]中搜索已关联邮件..."), value);
                this.lblMailProject.Text = value;
            }
        } 
        #endregion

        /// <summary>
        /// 关闭窗体
        /// </summary>
        private delegate void OnClosed();
        #endregion

        #region 构造方法
        public FrmEmailArchiving()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(FrmEmailArchiving_FormClosing);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            if (!LocalData.IsDesignMode)
            {
                InitControl();
                this.Disposed += delegate
                {
                    this.FormClosing -= new FormClosingEventHandler(FrmEmailArchiving_FormClosing);
                    this.btnCancel.Click -= new EventHandler(btnCancel_Click);
                    if (olItem != null)
                        MailUtility.ReleaseComObject(olItem);
                    if (olReportItem != null)
                        MailUtility.ReleaseComObject(olReportItem);
                    if (_relationOLFolder != null)
                        MailUtility.ReleaseComObject(_relationOLFolder);
                    if (items != null)
                    {
                        items.Clear();
                        items = null;
                    }
                    _SmartParts = null;
                    if (RootWorkItem != null) this.RootWorkItem.Items.Remove(this);
                };
            }
        }
        #endregion

        #region 窗体事件
        /// <summary>
        /// 取消归档并关闭窗体
        /// </summary>
        void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }
        /// <summary>
        /// 窗体即将关闭
        /// </summary>
        void FrmEmailArchiving_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        } 
        #endregion

        #region 方法定义
        /// <summary>
        /// 初始化控件
        /// </summary>
        void InitControl()
        {
            this.lblMailProject.Text = "";
            this.lblTo.Text = "";
            this.lblRemainded.Text = "";
            if (!LocalData.IsEnglish)
            {
                this.Text = "邮件归档";
                this.lblTotalEmail.Text = "共移动 0 封邮件.";
                this.lblMailProjectTitle.Text = "邮件项目:";
                this.lblToTitle.Text = "移动到:";
                this.lblRemaindedTitle.Text = "剩余项:";
                this.btnCancel.Text = "取消";
            }
        }
        /// <summary>
        /// 邮件归档
        /// </summary>
        public void EmailArchiving(ref string defaultRelationEntryID,ref string fullPath)
        {
            items = new List<Object>();
            //循环所有Store
            foreach (Store storeItem in ClientUtility.OlNS.Stores)
            {
                if (items == null)  //中间取消归档时items会清空
                    return;
                //查找outlook中账户的收件箱文件夹
                _inboxFolder = ClientUtility.GetInboxFolder(storeItem);
                if(_inboxFolder==null)
                    continue;
                bool isExsits = false;
                //1.判断文件夹是否存在，不存在则创建
                _relationOLFolder = ClientUtility.AddFolder(_inboxFolder,ClientUtility.EmailRelationSaveFolder,ref isExsits);
                //当前为默认Store
                if (ClientUtility.OlNS.DefaultStore.StoreID.Equals(storeItem.StoreID))
                {
                    defaultRelationEntryID = _relationOLFolder.EntryID;
                    fullPath=_relationOLFolder.FullFolderPath;
                }
                if (!isExsits)  //文件夹已存在
                {
                    //收件箱树形节点对象
                    TreeNode inboxNode=null;
                    //查找所有节点，返回树形控件节点对象
                    TreeViewPresenter.FindTreeNodeByTag(FolderPart.trvFolder.Nodes, _inboxFolder.EntryID, ref inboxNode);
                    TreeNode relationNode=null;
                    //查找收件箱节点，返回在树形控件节点对象
                    TreeViewPresenter.FindTreeNodeByTag(inboxNode.Nodes, _relationOLFolder.EntryID, ref relationNode);
                    //判断节点是否存在，不存在则添加
                    if (relationNode == null)
                    {
                        TreeViewPresenter.AddShamAndRealNode(inboxNode,_relationOLFolder,true);
                    }
                }
                //清除原有数据
                items.Clear();
                _ProjectName = storeItem.GetRootFolder().Name;
                this.lblTotalEmail.Text = string.Format((LocalData.IsEnglish ? "From [{0}] collecting associated mail... " : "正在从[{0}]中收集已关联的邮件..."), _ProjectName);
                this.lblMailProject.Text = _ProjectName;
                //2.获取已关联邮件
                TreeViewPresenter.GetRelationMails(ClientUtility.GetInboxFolder(storeItem), ref items);
                if (items == null)
                    return;
                _MailCount = items.Count;
                num_Percent = (float)Process_MaxValue / _MailCount;
                //3.遍历(已关联)邮件集合对象：移入已关联文件夹
                for (int index2 = 1; index2 <= _MailCount && (items != null); index2++)
                {
                    object item = items[index2-1];
                    olItem = item as MailItem;
                    if (olItem != null)
                    {
                        olItem.Move(_relationOLFolder);
                    }
                    else
                    {
                        olReportItem = item as ReportItem;
                        olReportItem.Move(_relationOLFolder);
                    }
                    System.Windows.Forms.Application.DoEvents();
                    OnProgessChanged(index2);
                }
            }
            Thread.Sleep(400);
            Cancel();
        }
        /// <summary>
        /// 设置控件显示文本
        /// </summary>
        /// <param name="paramIndex">第Index封邮件</param>
        /// <param name="paramNum">进度数</param>
        void SetCtlMessage(int paramIndex,int paramNum)
        {
            this.lblTotalEmail.Text = string.Format((LocalData.IsEnglish ? "Mail Account: {0} ,Progress: {1}%" : "共移动 {0} 封邮件,当前进度为:{1}%"), _MailCount, paramNum);
            this.lblTo.Text = string.Format((LocalData.IsEnglish ? @"Inbox\{0}" : @"收件箱\{0}"), ClientUtility.EmailRelationSaveFolder);
            this.lblRemainded.Text = (_MailCount - paramIndex).ToString();
        }
        /// <summary>
        /// 取消移动：关闭窗体
        /// </summary>
        void Cancel()
        {
            if (this.InvokeRequired)
            {
                OnClosed close = new OnClosed(ThisClose);
                this.Invoke(close);
            }
            else { ThisClose(); }
        }
        /// <summary>
        /// 进度条改变
        /// </summary>
        /// <param name="index"></param>
        void OnProgessChanged(int index)
        {
            int percentage = 0;
            int.TryParse((Math.Ceiling(index * num_Percent)).ToString(), out percentage);
            if (percentage > 100)
                percentage = 100;
            SetCtlMessage(index,percentage);
            this.progressBar1.EditValue = percentage;
        }
        /// <summary>
        /// 关闭窗体
        /// </summary>
        void ThisClose()
        {
            this.Close();
        } 
        #endregion
    }
}
