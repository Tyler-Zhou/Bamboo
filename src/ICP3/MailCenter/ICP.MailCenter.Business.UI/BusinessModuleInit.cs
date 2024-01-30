using System;
using ICP.Business.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Operation.Common.ServiceInterface;
using ICP.Operation.Common.UI;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.DataCache.ServiceInterface;
using Microsoft.Practices.CompositeUI.Commands;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using ICP.Message.ServiceInterface;
namespace ICP.MailCenter.Business.UI
{
    /// <summary>
    /// 邮件业务链接面板模块入口类
    /// </summary>
    public class BusinessModuleInit : ModuleInit, IDisposable
    {
        private System.Windows.Forms.Timer showBusinessPartTimer;
        /// <summary>
        /// 根WorkItem
        /// </summary>
        public WorkItem RootWorkItem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }
        }

        public override void Load()
        {
            base.Load();
            showBusinessPartTimer = new System.Windows.Forms.Timer();
            showBusinessPartTimer.Interval = 1000;
            showBusinessPartTimer.Enabled = false;
            showBusinessPartTimer.Tick += OnShowBusinessPartTimerTick;
        }

        private void OnShowBusinessPartTimerTick(object sender, EventArgs e)
        {
            StopTimer();
            InnerShowBusinessPart(null);
        }
        private void StopTimer()
        {
            this.showBusinessPartTimer.Stop();
            this.showBusinessPartTimer.Enabled = false;
        }

        private void StartTimer()
        {
            this.showBusinessPartTimer.Enabled = true;
            this.showBusinessPartTimer.Start();
        }

        public override void AddServices()
        {
            if (!RootWorkItem.Services.Contains<IDocumentNotifyService>())
            {
                RootWorkItem.Services.AddNew<DocumentNotifyService, IDocumentNotifyService>();
            }

            // ServiceClient.GetService<ICP.Business.Common.ServiceInterface.IICPCommonOperationService>();

            ServiceClient.GetService<IBusinessQueryService>();
            ServiceClient.GetService<ICP.FCM.OceanExport.ServiceInterface.IOceanExportService>();
            ServiceClient.GetService<IOperationMessageRelationService>();

        }

        private BusinessPartFactory businessPartFactory;
        private BusinessPartFactory BusinessPartFactory
        {
            get
            {
                if (businessPartFactory == null)
                {
                    businessPartFactory = RootWorkItem.Items.AddNew<BusinessPartFactory>();
                }
                return businessPartFactory;
            }
        }
        /// <summary>
        /// 业务面板区域中当前显示的业务面板
        /// </summary>
        private BaseBusinessPart currentPart = null;
        /// <summary>
        /// 邮件暗码
        /// </summary>
        private string MailAction { get; set; }

        private XtraPanel MailCenterParentBusinessPart
        {
            get { return this.RootWorkItem.State[Constants.BusinessPartPanelKey] as XtraPanel; }
        }

        /// <summary>
        /// 显示业务面板
        /// 如果参数中模板代码为空，则根据解析消息获取的模板代码来显示面板
        /// ，否则以参数中的模板代码为准
        /// </summary>
        /// <param name="templateCode"></param>
        private void InnerShowBusinessPart(string templateCode)
        {
            ICP.Message.ServiceInterface.Message mail =
                this.RootWorkItem.State[Constants.CurrentMessageKey] as ICP.Message.ServiceInterface.Message;
            if (mail == null || string.IsNullOrEmpty(mail.SendFrom))//邮件为空隐藏面板
            {
                //currentPart = null;
                MailCenterParentBusinessPart.Visible = false;
                return;
            }
            if (!MailCenterParentBusinessPart.Visible)
                MailCenterParentBusinessPart.Visible = true;

            if (string.IsNullOrEmpty(templateCode))
            {
                templateCode = MessageBusinessInfoExtractor.GetTemplateCode(mail, false);//根据消息获取模板代码
            }
            else
            {
                if (mail.UserProperties == null)
                {
                    mail.UserProperties = new MessageUserPropertiesObject();
                }
                mail.UserProperties.TemplateCode = templateCode;
            }
            UIHelper.ApplicationCacheSubjectKeyWord(templateCode, mail.Subject);//缓存主题单号

            if (IsSameBusinessPart(mail, templateCode))//当前显示的业务面板和即将显示的业务面板是否相同
            {
                InnerBindData(mail);//绑定数据
                return;
            }
            string partName = templateCode;
            //////加载面板不需要判断，传入一个固定的templateCode（MailLink4in1）就行了
            ////string partName = ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4in1.ToString();

            BaseBusinessPart businessPart;

            if (MailCenterParentBusinessPart.Controls.ContainsKey(partName))//以前逻辑
            {
                businessPart = MailCenterParentBusinessPart.Controls[partName] as BaseBusinessPart; AddBusinessControl(businessPart, false, templateCode);
                currentPart = businessPart;
                //////加载面板不需要判断，传入一个固定的templateCode（MailLink4in1）就行了
                ////AddBusinessControl(businessPart, false, ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4in1.ToString());

                InnerBindData(mail);
                return;
            }
            // CreatePartDelegate createDelegate = new CreatePartDelegate(CreatePart);
            businessPart = CreatePart(templateCode, true, mail) as BaseBusinessPart;//创建业务面板
            //////加载面板不需要判断，传入一个固定的templateCode（MailLink4in1）就行了
            ////businessPart = CreatePart(ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4in1.ToString(), true, mail) as BaseBusinessPart;

            // panel.Invoke(createDelegate, templateCode, true, mail) as BaseBusinessPart;
            currentPart = businessPart;
            InnerBindData(mail);
        }

        private bool IsSameBusinessPart(Message.ServiceInterface.Message mail, string templateCode)
        {
            bool isSame = false;
            if (currentPart != null && currentPart.TemplateCode.Equals(templateCode))
            {

                isSame = true;

            }
            return isSame;
        }


        private void InnerBindData(Message.ServiceInterface.Message mail)
        {
            currentPart.baseBindingSource = null;
            currentPart.BindData(mail);
        }

        private BaseBusinessPart CreatePart(string templateCode, bool add, ICP.Message.ServiceInterface.Message mail)
        {
            BaseBusinessPart businessPart;
            if (!HasUserPropertiesAction(mail.UserProperties) && templateCode.Equals(ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4Unknown.ToString()))//以前逻辑
            {
                businessPart = BusinessPartFactory.Get<ListBaseBusinessPart>(templateCode, mail);
            }
            //  else if (templateCode.Equals(ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4Carrier.ToString()))
            //     businessPart = BusinessPartFactory.Get<BaseBusinessPart>(templateCode, mail);
            else
            {
                //if (HasUserPropertiesAction(mail.UserProperties))
                //    businessPart = BusinessPartFactory.Get<InternalMailBaseBusinessPart>(templateCode, mail);
                //else
                businessPart = BusinessPartFactory.Get<ListBaseBusinessPart>(templateCode, mail);
            }
            this.AddBusinessControl(businessPart, true, templateCode);

            return businessPart;
        }

        private string GetUserPropertiesAction(Message.ServiceInterface.Message message)
        {
            if (message == null)
                return string.Empty;
            else
            {
                if (message.UserProperties == null)
                    return string.Empty;
                else
                    return message.UserProperties.Action;
            }


        }

        private bool HasUserPropertiesAction(MessageUserPropertiesObject userProperties)
        {
            if (userProperties != null && !string.IsNullOrEmpty(userProperties.Action))
            {
                MailAction = userProperties.Action;
                return true;
            }
            else
            {
                MailAction = string.Empty;
                return false;
            }

        }


        private delegate BaseBusinessPart CreatePartDelegate(string templateCode, bool add, ICP.Message.ServiceInterface.Message mail);
        private delegate void InvokeDelegate(BaseBusinessPart businessPart, string templateCode, bool add);
        private delegate void BindDataDelegate(BaseBusinessPart businessPart, Message.ServiceInterface.Message mail);
        private void AddControlAndSetIndex(BaseBusinessPart businessPart, string templateCode, bool add)
        {
            AddBusinessControl(businessPart, add, templateCode);
        }

        private void AddBusinessControl(BaseBusinessPart businessPart, bool add, string templateCode)
        {

            if (add)
            {
                businessPart.Name = templateCode;
                businessPart.Dock = DockStyle.Fill;
                try
                {
                    MailCenterParentBusinessPart.Controls.Add(businessPart);
                    MailCenterParentBusinessPart.Controls.SetChildIndex(businessPart, 0);
                }
                catch (System.Exception ex)
                {
                    ICP.Framework.CommonLibrary.Logger.Log.Error(ICP.Framework.CommonLibrary.Common.CommonHelper.BuildExceptionString(ex));
                    throw new ICPException(ex.Message);
                }
            }
        }

        [CommandHandler(Constants.ShowBusinessPartCommand)]
        public void ShowBusinessPart(object sender, EventArgs e)
        {
            //if (currentPart != null)
            //{
            //    currentPart.SetBaseBindSource(null);
            //}
            StopTimer();
            StartTimer();
        }

        /// <summary>
        /// 打开4合1面板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_EmailCenter_4in1")]
        public void Command_EmailCenter_4in1(object sender, EventArgs e)
        {
            InnerShowBusinessPart(ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4in1.ToString());
        }

        /// <summary>
        /// 打开客户面板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_EmailCenter_Customer")]
        public void Command_EmailCenter_Customer(object sender, EventArgs e)
        {
            InnerShowBusinessPart(ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4Customer.ToString());
        }

        [CommandHandler("Command_EmailCenter_Carrier")]
        public void Command_EmailCenter_Carrier(object sender, EventArgs e)
        {
            InnerShowBusinessPart(ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4Carrier.ToString());
        }

        /// <summary>
        /// 邮件中心显示分配SO界面命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_EmailCenter_AllocateSO")]
        public void Command_EmailCenter_AllocateSO(object sender, EventArgs e)
        {
            string templateCode = ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4CarrierSO.ToString();
            InnerShowBusinessPart(templateCode);
        }
        /// <summary>
        /// 邮件中心显示上传MBL界面命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_EmailCenter_UploadMBL")]
        public void Command_EmailCenter_UploadMBL(object sender, EventArgs e)
        {
            string templateCode = ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4CarrierMBL.ToString();
            InnerShowBusinessPart(templateCode);
        }
        /// <summary>
        /// 邮件中心显示上传AP界面命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_EmailCenter_UploadAP")]
        public void Command_EmailCenter_UploadAP(object sender, EventArgs e)
        {
            string templateCode = ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4CarrierAP.ToString();
            InnerShowBusinessPart(templateCode);
        }
        /// <summary>
        /// 邮件中心显示上传AP界面命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_EmailCenter_UploadAN")]
        public void Command_EmailCenter_UploadAN(object sender, EventArgs e)
        {
            string templateCode = ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4CarrierAN.ToString();
            InnerShowBusinessPart(templateCode);
        }
        /// <summary>
        /// 新增反馈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("SYSTEM_NEWFEEDBACK")]
        public void Command_New_Feedback(object sender, EventArgs e)
        {
            IICPCommonOperationService operationService = ServiceClient.GetService<IICPCommonOperationService>();
            operationService.AddFeedback(this.RootWorkItem.State["ScreenImagePath"] as string, this.RootWorkItem.State["TotalErrorMessage"] as string);
        }


        #region IDisposable 成员

        public void Dispose()
        {

        }

        #endregion
    }
}
