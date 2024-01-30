using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using ICP.DataCache.ServiceInterface;
using ICP.FileSystem.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ICP.Business.Common.UI.Communication
{
    /// <summary>
    /// 沟通历史记录列表控件类
    /// </summary>
    public partial class UCCommunicationHistoryList : UserControl, ICommunicationHistoryList
    {
        #region 成员变量

        /// <summary>
        /// 
        /// </summary>
        public WorkItem Workitem
        {
            get;
            set;
        }
        /// <summary>
        /// 是否英文
        /// </summary>
        bool isEnglish = LocalData.IsEnglish;

        /// <summary>
        /// 当前选择项改变
        /// </summary>
        public event EventHandler<CommonEventArgs<Message.ServiceInterface.Message>> CurrentChanged;

        #region 业务操作上下文
        /// <summary>
        /// 业务操作上下文
        /// </summary>
        public BusinessOperationContext BusinessContext { get; set; }
        #endregion

        #region 属性-是否显示复选框
        /// <summary>
        /// 设置复选框显示状态
        /// </summary>
        public bool IsShowChoose
        {
            set
            {
                colIschoose.Visible = value;
                if (colIschoose.Visible)
                {
                    colIschoose.VisibleIndex = 0;
                }
            }
        }
        #endregion

        #region 属性-呈现管理
        /// <summary>
        /// 呈现管理
        /// </summary>
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public CommunicationHistoryListPresenter ListPresenter { get; set; }

        #endregion 

        #region 属性-列表当前选择项
        /// <summary>
        /// 列表当前选择项
        /// </summary>
        public CommunicationHistory Current
        {
            get { return bindingSource.Current as CommunicationHistory; }
        } 
        #endregion

        #region 属性-列表数据源
        /// <summary>
        /// 列表数据源
        /// </summary>
        public List<CommunicationHistory> DataSource
        {
            get { return bindingSource.DataSource as List<CommunicationHistory>; }
            set
            {
                bindingSource.DataSource = value;
                bindingSource.ResetBindings(false);
            }
        } 
        #endregion

        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public UCCommunicationHistoryList()
        {
            InitializeComponent();
            repositoryItemImageComboBox1.BeginUpdate();
            repositoryItemImageComboBox1.Items.Add(new ImageComboBoxItem("EMail", MessageType.Email, 0));
            repositoryItemImageComboBox1.Items.Add(new ImageComboBoxItem("Fax", MessageType.Fax, 1));
            repositoryItemImageComboBox1.Items.Add(new ImageComboBoxItem("EDI", MessageType.EDI, 2));
            repositoryItemImageComboBox1.EndUpdate();

            

            if (!LocalData.IsDesignMode)
            {
                bindingSource.DataSourceChanged += bindingSource_PositionChanged;
                bindingSource.PositionChanged += bindingSource_PositionChanged;
                gridViewList.IndicatorWidth = 30;
                gridViewList.CustomDrawRowIndicator += OnDrawCustomRowIndicator;
                gridControlList.MouseDown += gridControlList_MouseDown;

                MenuItemOpen.Click += MenuItemOpen_Click;
                MenuItemrepl.Click += MenuItemrepl_Click;
                MenuItemAllrepl.Click += MenuItemAllrepl_Click;
                MenuItemAllReplyAttachment.Click += MenuItemAllReplyAttachment_Click;
                MenuItemforwardin.Click += MenuItemforwardin_Click;
                MenuItemMarkNRAS.Click += MenuItemMarkNRAS_Click;
                
            }
            Load += (sender, e) =>
            {
                Locale();
                if (BusinessContext != null && BusinessContext.OperationType == OperationType.QuotedPrice)
                {
                    MenuItemMarkNRAS.Visible = true;
                }
            };
            Disposed += delegate
            {
                ListPresenter = null;
                gridControlList.DataSource = null;
                bindingSource.DataSource = null;
                bindingSource = null;

            };
        }

        

        
       
        #endregion

        #region 窗体事件
        /// <summary>
        /// 行号显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnDrawCustomRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1));
            }
        }

        /// <summary>
        /// 选择项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bindingSource_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null)
            {
                CurrentChanged(this, new CommonEventArgs<Message.ServiceInterface.Message>(Current));
            }
        }

        /// <summary>
        /// 双击列表弹出预览界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControlList_DoubleClick(object sender, EventArgs e)
        {
            if (Current == null) return;
            ListPresenter.Open(Current);
        }

        void gridControlList_MouseDown(object sender, MouseEventArgs e)
        {
            //右键点击且操作类型改变过
            if (Current == null) return;
            //右键点击且操作类型改变过
            if (e.Button == MouseButtons.Right && (!ListPresenter.IsLoadContextMenu))
            {
                AddMenuStrip(ListPresenter.GetMenuItems());
                MenuItemSend.Visible = MenuItemSend.DropDownItems.Count > 0;
                ListPresenter.IsLoadContextMenu = true;
            }
        }

        #region Sent Email

        #region 发送到港通知书给客户
        public void OceanImport_MailANToCustomerCHS(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                ListPresenter.MailAnToCustomer(false);
            }
        }

        public void OceanImport_MailANToCustomerENG(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                ListPresenter.MailAnToCustomer(true);
            }
        }  
        #endregion

        #region 发送提货通知书给客户
        public void OceanImport_MailPickUpToCustomerCHS(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                ListPresenter.MailPickUpToCustomer(false);
            }
        }  
        public void OceanImport_MailPickUpToCustomerENG(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                ListPresenter.MailPickUpToCustomer(true);
            }
        }  
        #endregion

        #region 通知客户ADJ SO Copy、告知客户订舱成功
        public void Command_CommunicationMailSOCopyToCustomerCHS(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string result=ListPresenter.MailSOCopyToCustomer(false);
                if (string.IsNullOrEmpty(result)) return;
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), result);

            }
        }
        public void Command_CommunicationMailSOCopyToCustomerENG(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string result= ListPresenter.MailSOCopyToCustomer(true);
                if (string.IsNullOrEmpty(result)) return;
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), result);
            }
        }
        #endregion

        #region 通知客户订舱确认书
        public void Command_MailSoConfirmationToCustomerCHS(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                ListPresenter.MailSoConfirmationToCustomer(false);
            }
        }

        public void Command_MailSoConfirmationToCustomerENG(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                ListPresenter.MailSoConfirmationToCustomer(true);
            }
        }
        #endregion

        #region 客户确认补料
		public void Command_Communication_MailCustomerAskForConfirmSICHS(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                if (item == null) return;

                ListPresenter.MailCustomerAskForConfirmSI(new Guid(item.Tag.ToString()),false);
            }
        }

        public void Command_Communication_MailCustomerAskForConfirmSIENG(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                if (item == null) return;
                ListPresenter.MailCustomerAskForConfirmSI(new Guid(item.Tag.ToString()),true);
            }
        }

        #endregion

        #region 通知代理订舱确认书
        public void Command_MailSoConfirmationToAgent(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string result=ListPresenter.MailSoConfirmationToAgent();
                if (string.IsNullOrEmpty(result)) return;
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), result);
            }
        }

        public void Command_MailSoConfirmationToAgentENG(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string result=ListPresenter.MailSoConfirmationToAgent();
                if (string.IsNullOrEmpty(result)) return;
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), result);
            }
        }
        #endregion

        #region 向代理确认提单
        public void Command_BLCommand_MailAllBLCopyToAgentCHS(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                ListPresenter.MailAnToCustomer(true);
            }
        }

        public void Command_BLCommand_MailAllBLCopyToAgentENG(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                ListPresenter.MailAnToCustomer(true);
            }
        }
        #endregion

        #region 利润承诺
        public void Command_CommunicationAskProfitPromiseCHS(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string result=ListPresenter.MailSalesManAskForProfitPromise(false);
                if (string.IsNullOrEmpty(result)) return;
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), result);
            }
        }

        public void Command_CommunicationAskProfitPromiseENG(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string result=ListPresenter.MailSalesManAskForProfitPromise(true);
                if (string.IsNullOrEmpty(result)) return;
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), result);
            }
        }
        #endregion

        #region 提醒客户补料
        public void Command_MailCustomerAskForSiCHS(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string result=ListPresenter.MailCustomerAskForSi(true);
                if (string.IsNullOrEmpty(result)) return;
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), result);
            }
        }
        public void Command_MailCustomerAskForSiENG(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string result=ListPresenter.MailCustomerAskForSi(true);
                if (string.IsNullOrEmpty(result)) return;
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), result);
            }
        }
        #endregion

        #region 确认费用
        public void Command_MailSalesForConfirmDebitFeesCHS(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string result=ListPresenter.MailSalesForConfirmDebitFees(false);
                if (string.IsNullOrEmpty(result)) return;
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), result);
            }
        }
        public void Command_MailSalesForConfirmDebitFeesENG(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string result=ListPresenter.MailSalesForConfirmDebitFees(true);
                if (string.IsNullOrEmpty(result)) return;
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), result);
            }
        }
        #endregion

        #endregion

        #region 鼠标右键菜单
        /// <summary>
        ///打开邮件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemOpen_Click(object sender, EventArgs e)
        {
            if (Current == null) return;
            ListPresenter.Open(Current);
        }

        /// <summary>
        /// 答复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemrepl_Click(object sender, EventArgs e)
        {
            if (Current == null) return;
            ListPresenter.Reply(Current);
        }

        /// <summary>
        /// 全部答复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemAllrepl_Click(object sender, EventArgs e)
        {
            if (Current == null) return;
            ListPresenter.ReplyAll(Current);
        }

        void MenuItemAllReplyAttachment_Click(object sender, EventArgs e)
        {
            if (Current == null) return;
            ListPresenter.ReplyAllAttachment(Current);
        }

        /// <summary>
        /// 转发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemforwardin_Click(object sender, EventArgs e)
        {
            if (Current == null) return;
            ListPresenter.Forward(Current);
        }
        /// <summary>
        /// 标记为NRAS文档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MenuItemMarkNRAS_Click(object sender, EventArgs e)
        {
            if (Current == null) return;
            ListPresenter.ConvertMailItemToDocumentInfo(Current, DocumentType.NRAS);
        }
        #endregion

        #endregion

        #region 方法定义
        /// <summary>
        /// 本地化文本
        /// </summary>
        private void Locale()
        {
            if (!isEnglish && !LocalData.IsDesignMode)
            {
                colType.Caption = "类型";
                colContactStage.Caption = "阶段";
                colSubject.Caption = "主题";
                //colSendFrom.Caption = "发送人";
                colSendFromName.Caption = "发送人";
                colSendTo.Caption = "接收人";
                colCreateDate.Caption = "发送时间";
                colStateDescription.Caption = "状态";
                colRemark.Caption = "备注";
                Text = isEnglish ? "Document List" : "文档列表";

                MenuItemOpen.Text = "打开";
                MenuItemSend.Text = "发送";
                MenuItemrepl.Text = "答复";
                MenuItemAllrepl.Text = "答复全部";
                MenuItemAllReplyAttachment.Text = "答复全部(含附件)";
                MenuItemforwardin.Text = "转发";
                MenuItemMarkNRAS.Text = "标记为NARS文档";
            }
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="toolStripMenu"></param>
        void AddMenuStrip(IEnumerable<ToolStripMenuItem> toolStripMenu)
        {
            MenuItemSend.DropDownItems.Clear();
            #region 发送邮件-模板选择

            foreach (var i in toolStripMenu)
            {
                if (MenuItemSend.DropDownItems.Contains(i))
                    continue;
                if (i.AutoToolTip)
                {
                    ToolStripSeparator toolStripSeparator = new ToolStripSeparator();
                    toolStripSeparator.Name = "ToolStrip" + i.Name;
                    MenuItemSend.DropDownItems.Add(toolStripSeparator);
                }
                MenuItemSend.DropDownItems.Add(i);
            }

            #endregion
        } 
        #endregion

    }
}
