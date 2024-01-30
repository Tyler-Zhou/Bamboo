using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.ObjectBuilder;
using ICP.Message.ServiceInterface;
using ICP.Framework.ClientComponents.Controls;

namespace ICP.Business.Common.UI.Communication
{
    /// <summary>
    /// 沟通历史列表操作工具栏控件
    /// </summary>
    public partial class UCCommunicationHistoryOperationBar : UserControl
    {
        [ServiceDependency]
        public ICommunicationHistoryList ucList { get; set; }
        /// <summary>
        /// 呈现管理
        /// </summary>
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public CommunicationHistoryListPresenter ListPresenter { get; set; }

        /// <summary>
        /// 沟通列表类型选中集合
        /// </summary>
        List<MessageType> messagetype = null;

        bool isEnglish = LocalData.IsEnglish;
        /// <summary>
        /// 构造函数
        /// </summary>
        public UCCommunicationHistoryOperationBar()
        {
            InitializeComponent();
            this.Load += (sender, e) =>
            {

                Locale();
            };
            this.Disposed += (sender, e) =>
            {
                if (this.ucList != null)
                {
                    this.ucList.CurrentChanged -= this.ucList_CurrentChanged;
                    this.ucList = null;

                }
                this.ListPresenter = null;

            };

        }

        public void RegisterEvent()
        {
            if (this.ucList != null)
            {
                this.ucList.CurrentChanged += new EventHandler<ICP.Framework.CommonLibrary.Common.CommonEventArgs<ICP.Message.ServiceInterface.Message>>(ucList_CurrentChanged);
            }
        }

        void ucList_CurrentChanged(object sender, ICP.Framework.CommonLibrary.Common.CommonEventArgs<ICP.Message.ServiceInterface.Message> e)
        {
            if (e == null || e.Data == null)
            {
                this.barItemReply.Enabled = this.barItemResend.Enabled = false;
                return;
            }
            else
            {
                if (e.Data.Type == MessageType.EDI)
                    this.barItemReply.Enabled = false;
                else
                    this.barItemReply.Enabled = true;
            }

            ICP.Message.ServiceInterface.Message currentMessage = e.Data;
            this.barItemResend.Enabled = currentMessage.State == MessageState.Failure;
        }

        private void Locale()
        {
            if (!isEnglish && !LocalData.IsDesignMode)
            {
                this.barsubItemview.Caption = "查看(&W)";
                this.barChitemEmail.Caption = "邮件(&M)";
                this.barChItemFax.Caption = "传真(&F)";
                this.barItemOpen.Caption = "打开(&O)";
                this.barItemReply.Caption = "回复(&R)";
                this.barItemResend.Caption = "重发(&S)";
                this.barChItemEDI.Caption = "EDI(&E)";
                this.barSetStage.Caption = "设置沟通阶段";

            }
        }

        private void barItemEmail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListPresenter.FilterAgainst(MessageType.Email);
        }

        private void barItemFax_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListPresenter.FilterAgainst(MessageType.Fax);
        }

        private void barItemEDI_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListPresenter.FilterAgainst(MessageType.EDI);
        }

        private void barItemOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ucList == null)
                return;
            if (ucList.Current == null)
                return;
            ListPresenter.Open(ucList.Current);
        }

        private void barItemReply_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ucList == null)
                return;
            if (ucList.Current == null)
                return;
            ListPresenter.Reply(ucList.Current);
        }

        private void barItemResend_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ucList == null)
                return;
            if (ucList.Current == null)
                return;
            try
            {
                ListPresenter.Resend(ucList.Current);
                ucList.Current.State = MessageState.Sending;
                ucList_CurrentChanged(this, new ICP.Framework.CommonLibrary.Common.CommonEventArgs<ICP.Message.ServiceInterface.Message>(ucList.Current));
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo((Control)ucList, ex);
            }
        }

        private void barItemNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ListPresenter != null)
            {
                //ListPresenter.Add();
            }
        }

        private void barItemEmail_Fax_EDI_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ListPresenter != null)
            {
                ListPresenter.FilterAgainst(GetMessageTypeList());
            }

        }

        /// <summary>
        /// 获取沟通列表类型选中集合
        /// </summary>
        /// <returns></returns>
        private List<MessageType> GetMessageTypeList()
        {
            messagetype = new List<MessageType>();
            if (barChItemFax.Checked == true)
            {
                messagetype.Add(MessageType.Fax);
            }
            if (barChitemEmail.Checked == true)
            {
                messagetype.Add(MessageType.Email);
            }
            if (barChItemEDI.Checked == true)
            {
                messagetype.Add(MessageType.EDI);
            }
            return messagetype;
        }

        private void barSetStage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ucList == null)
                return;
            if (ucList.Current != null)
            {
                CommunicationHistoryStage stage = new CommunicationHistoryStage();
                stage.ListPresenter = ListPresenter;
                string title = LocalData.IsEnglish ? "Set Communication Stage" : "设置邮件沟通阶段";
                PartLoader.ShowDialog(stage, title);
            }
        }
 
    }
}
