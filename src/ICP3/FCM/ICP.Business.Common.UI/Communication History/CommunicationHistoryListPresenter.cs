using System.Reflection;
using System.Windows.Forms;
using ICP.Business.Common.ServiceInterface;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.MailCenter.ServiceInterface;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ICP.FileSystem.ServiceInterface;

namespace ICP.Business.Common.UI.Communication
{
    /// <summary>
    /// 沟通历史Presenter
    /// </summary>
    public sealed class CommunicationHistoryListPresenter
    {
        #region 属性及字段定义
        /// <summary>
        /// 鼠标右键菜单  确保只有一个实体
        /// </summary>
        MailContextMenuTemplate _ContextMenuTemplate;
        /// <summary>
        /// 筛选器操作
        /// </summary>
        private Dictionary<MessageType, Boolean> filterActions = new Dictionary<MessageType, bool>();

        /// <summary>
        /// 
        /// </summary>
        public WorkItem CurWorkItem { get; set; }

        /// <summary>
        /// 客户文件上传服务
        /// </summary>
        public IClientFileService ClientFileService
        {
            get
            {
                return ServiceClient.GetClientService<IClientFileService>();
            }
        }

        delegate void ReloadDelegate(List<CommunicationHistory> list);
        public List<CommunicationHistory> CommunicationList { get; set; }
        public UCCommunicationHistoryList ucList { get; set; }

        public BusinessOperationContext BusinessContext { get; set; }

        public IClientCommunicationHistoryService ClientCommunicationHistoryService
        {
            get
            {
                return ServiceClient.GetClientService<IClientCommunicationHistoryService>();
            }
        }

        /// <summary>
        /// 海进业务服务
        /// </summary>
        public IClientOceanImportService ClientOceanImportService
        {
            get
            {
                return ServiceClient.GetClientService<IClientOceanImportService>();
            }
        }
        /// <summary>
        /// 海出业务服务
        /// </summary>
        public IClientOceanExportService ClientOceanExportService
        {
            get
            {
                return ServiceClient.GetClientService<IClientOceanExportService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }
        public IClientMessageService ClientMessageService
        {
            get
            {
                return ServiceClient.GetClientService<IClientMessageService>();
            }
        }

        /// <summary>
        /// OutLook服务接口
        /// </summary>
        public IOutLookService OutLookService
        {
            get
            {
                return ServiceClient.GetClientService<IOutLookService>();
            }
        }

        

        private List<OceanBLList> oblList = null;

        /// <summary>
        /// 是否加载右键菜单
        /// </summary>
        public bool IsLoadContextMenu { get; set; }


        private CommunicationHistory CurrentSelect=null;
        private string _MailInformation = string.Empty;
        /// <summary>
        /// 所有邮件地址：发送、接收、抄送
        /// </summary>
        public string MailInformation
        {
            get
            {
                if (ucList != null)
                {
                    if(CurrentSelect==null)
                        CurrentSelect = ucList.Current;
                    if (string.IsNullOrEmpty(_MailInformation) || CurrentSelect != ucList.Current)
                        _MailInformation = (LocalData.UserInfo.EmailAddress.Equals(CurrentSelect.SendFrom) ? "" : (CurrentSelect.SendFrom + ";"))
                            + CurrentSelect.SendTo + ";" + "|All" + "|" + CurrentSelect.CC;
                }
                return _MailInformation;
            }
        }
        

        #endregion

        public CommunicationHistoryListPresenter()
        {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ucList"></param>
        public CommunicationHistoryListPresenter(UCCommunicationHistoryList ucList)
        {
            this.ucList = ucList;
        }

        private ICP.Message.ServiceInterface.Message GetMessage(CommunicationHistory item)
        {

            ICP.Message.ServiceInterface.Message message = ClientMessageService.Get(item.Id);
            message.EntryID = item.EntryId;
            return message;

        }
        /// <summary>
        /// 打开传真预览界面方法
        /// </summary>
        /// <param name="entry"></param>
        public void Open(CommunicationHistory entry)
        {
            int toid = -1;
            try
            {

                toid =
                    ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm(LocalData.IsEnglish == false
                        ? "正在加载邮件..."
                        : "Fetching the mail...");
                ClientMessageService.Open(GetMessage(entry));

                #region Comment Code

                //if (!string.IsNullOrEmpty(entry.EntryId))
                //{
                //    int toid = -1;
                //    Microsoft.Office.Interop.Outlook.Application myOutlookApp = new Microsoft.Office.Interop.Outlook.Application();
                //    Microsoft.Office.Interop.Outlook.NameSpace myNameSpace = myOutlookApp.GetNamespace("MAPI");
                //    try
                //    {
                //        Microsoft.Office.Interop.Outlook.MailItem mailItem = myNameSpace.Session.GetItemFromID(entry.EntryId, Type.Missing) as Microsoft.Office.Interop.Outlook.MailItem;
                //        mailItem.Display(false);

                //    }
                //    catch (System.Exception ex)
                //    {
                //        toid = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm(LocalData.IsEnglish == false ? "正在加载邮件..." : "Fetching the mail...");
                //        //从服务端下载邮件并打开邮件
                //        if (string.IsNullOrEmpty(entry.MessageId))
                //            ClientMessageService.Open(entry.Id);
                //        else
                //            ClientMessageService.OpenMsgFile(entry.MessageId);
                //    }
                //    finally
                //    {
                //        //关闭动画
                //        ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(toid);
                //    }
                //}
                //else
                //{
                //    ClientMessageService.Open(entry.Id);
                //} 

                #endregion
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.ucList, ex);
            }
            finally
            {
                ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(toid);
            }
        }

        public void Reply(CommunicationHistory entry)
        {
            try
            {
                ICP.Message.ServiceInterface.Message message = GetMessage(entry);
                ClientMessageService.Reply(message.Id, message.UpdateDate, message);
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.ucList, ex);
            }
        }
        public void ReplyAll(CommunicationHistory entry)
        {
            try
            {
                ICP.Message.ServiceInterface.Message message = GetMessage(entry);
                ClientMessageService.ReplyAll(message.Id, message.UpdateDate, message);
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.ucList, ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entry"></param>
        public void ReplyAllAttachment(CommunicationHistory entry)
        {
            try
            {
                ICP.Message.ServiceInterface.Message message = GetMessage(entry);
                ClientMessageService.ReplyAllAttachment(message);
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.ucList, ex);
            }
        }
        public void Forward(CommunicationHistory entry)
        {
            try
            {
                ICP.Message.ServiceInterface.Message message = GetMessage(entry);
                ClientMessageService.Reply(message.Id, message.UpdateDate, message);
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.ucList, ex);
            }
        }

        /// <summary>
        /// 转换邮件
        /// </summary>
        /// <param name="entry">邮件记录实体</param>
        /// <param name="documentType">文档类型</param>
        public void ConvertMailItemToDocumentInfo(CommunicationHistory entry, DocumentType documentType)
        {
            try
            {
                ICP.Message.ServiceInterface.Message message = GetMessage(entry);
                string mailSaveAsPath = OutLookService.GetMailItemSaveAsPath(message.EntryID, message.MessageId, message.Id.ToString());
                if (CommonUIUtility.ValidateFilesInfo(new[] {mailSaveAsPath}))
                {
                    DocumentInfo document = new DocumentInfo {OriginalPath = mailSaveAsPath};
                    CommonUIUtility.InitDocumentInfo(document, BusinessContext);
                    document.Name = Path.GetFileNameWithoutExtension(mailSaveAsPath);

                    ClientFileService.Upload(new[] { document }, new[] { document.OriginalPath });
                }
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.ucList, ex);
            }
        }
        public void Resend(CommunicationHistory entry)
        {

            ICP.Message.ServiceInterface.Message message = GetMessage(entry);
            ClientMessageService.Resend(message);

        }
        private void ChangeFilterRecord(MessageType type)
        {
            if (!filterActions.ContainsKey(type))
            {
                filterActions.Add(type, true);
            }
            else
            {
                filterActions[type] = !filterActions[type];
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramWorkItem"></param>
        /// <returns></returns>
        public List<ToolStripMenuItem> GetMenuItems()
        {
            if (_ContextMenuTemplate == null)
                _ContextMenuTemplate = new MailContextMenuTemplate();
            List<ToolStripMenuItem> tsmis = new List<ToolStripMenuItem>();
            List<OperationToolbarCommand> commandList = ToolbarTemplateLoader.Current["CommunicationHistory" + BusinessContext.OperationType];
            if (commandList != null && commandList.Count > 0)
            {
                oblList = OceanExportService.GetOceanBLListByOperationInfo(BusinessContext);
                tsmis= GetToolStripMenuItem(commandList);
            }
            return tsmis;
        }

        public void FilterAgainst(MessageType type)
        {
            ChangeFilterRecord(type);
            bool isFilter = filterActions[type];
            if (isFilter)
            {
                List<CommunicationHistory> temp = new List<CommunicationHistory>();
                if (CommunicationList != null && CommunicationList.Count > 0)
                {
                    temp = CommunicationList.Where<CommunicationHistory>(entry => entry.Type == type).ToList();
                    temp = CommunicationList.Where<CommunicationHistory>(entry => entry.Type == type).ToList();
                }
                this.ucList.DataSource = temp;
            }
            else
            {
                if (CommunicationList == null || CommunicationList.Count == 0)
                { this.CommunicationList = new List<CommunicationHistory>(); }

                this.ucList.DataSource = this.CommunicationList;
            }

        }

        /// <summary>
        /// 根据选择类型过滤沟通文档列表
        /// </summary>
        /// <param name="type"></param>
        public void FilterAgainst(List<MessageType> type)
        {
            List<CommunicationHistory> temp = new List<CommunicationHistory>();
            if (CommunicationList != null && CommunicationList.Count > 0)
            {
                temp = CommunicationList.Where<CommunicationHistory>(entry => type.Contains(entry.Type)).ToList();
            }
            this.ucList.DataSource = temp;
        }

        public void LoadData(BusinessOperationContext context)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(data =>
            {
                try
                {
                    IsLoadContextMenu = false;
                    //0.当前操作类型改变
                    BusinessContext = context;
                    ucList.BusinessContext = context;
                    ClientHelper.SetApplicationContext();
                    List<CommunicationHistory> list = ClientCommunicationHistoryService.GetCommunicationHistoryList(context);
                    if (this.ucList.IsDisposed)
                        return;
                    ReloadDelegate reloadDelegate = new ReloadDelegate(InnerLoadData);
                    if (list.Any())
                    {
                        if (list[0].OperationId != context.OperationID)
                        {
                            list = new List<CommunicationHistory>();
                        }
                    }
                    this.ucList.Invoke(reloadDelegate, list);
                }
                catch (System.Exception ex)
                {
                    if (!this.ucList.IsDisposed)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex);
                    }
                }

            });

        }
        private void InnerLoadData(List<CommunicationHistory> list)
        {
            this.ucList.DataSource = null;
            this.CommunicationList = list;
            this.ucList.DataSource = list;
        }
        /// <summary>
        /// 沟通历史回调通知处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [EventSubscription("FaxNotfiy", Thread = ThreadOption.Background)]
        public void NotifyHandler(object sender, DataEventArgs<Mail> e)
        {
            //BusinessOperationContext context = e.Data.Convert();
            //if (IsSameBusinessContext(context))
            //{
            //    LoadData(context);
            //}
        }

        #region Sent Email
        /// <summary>
        /// 发送到港通知书给客户
        /// </summary>
        /// <param name="isEnglish">是否英文</param>
        public void MailAnToCustomer(bool isEnglish)
        {
            if (BusinessContext == null || BusinessContext.OperationID == Guid.Empty) return;

            ClientOceanImportService.MailAnToCustomer(BusinessContext.OperationID, MailInformation, isEnglish);
        }

        /// <summary>
        /// 发送提货通知书给客户
        /// </summary>
        /// <param name="isEnglish">是否英文</param>
        public void MailPickUpToCustomer(bool isEnglish)
        {
            if (BusinessContext == null || BusinessContext.OperationID == Guid.Empty) return;

            ClientOceanImportService.MailPickUpToCustomer(BusinessContext.OperationID, MailInformation, isEnglish);
        }

        /// <summary>
        /// 通知客户ADJ SO Copy、告知客户订舱成功
        /// </summary>
        /// <param name="isEnglish">是否英文</param>
        public string MailSOCopyToCustomer(bool isEnglish)
        {
            if (BusinessContext == null || BusinessContext.OperationID == Guid.Empty) return "";

            return ClientOceanExportService.MailSoCopyToCustomer(isEnglish, BusinessContext.OperationID, MailInformation);
        }

        /// <summary>
        /// 通知客户订舱确认书
        /// </summary>
        /// <param name="isEnglish">是否英文</param>
        public void MailSoConfirmationToCustomer(bool isEnglish)
        {
            if (BusinessContext == null || BusinessContext.OperationID == Guid.Empty) return;

            ClientOceanExportService.MailSoConfirmationToCustomer(BusinessContext.OperationID,MailInformation, isEnglish);
        }

        /// <summary>
        /// 通知代理订舱确认书
        /// </summary>
        public string MailSoConfirmationToAgent()
        {
            if (BusinessContext == null || BusinessContext.OperationID == Guid.Empty) return "";

            return ClientOceanExportService.MailSoConfirmationToAgent(BusinessContext.OperationID,MailInformation);
        }

        /// <summary>
        /// 客户确认补料
        /// </summary>
        /// <param name="isEnglish">是否英文</param>
        public string MailCustomerAskForConfirmSI(Guid mhblID,bool isEnglish)
        {
            if (BusinessContext == null || BusinessContext.OperationID == Guid.Empty) return "";
            OceanMBLInfo oceanMblInfo = null;
            OceanHBLInfo oceanHbl = null;
            OceanBLList currentItem = null;
            foreach (var item in oblList.Where(item => item.ID == mhblID))
            {
                currentItem = item;
            }
            if (currentItem != null)
            {
                if (currentItem.BLType == FCM.Common.ServiceInterface.DataObjects.FCMBLType.MBL)
                {
                    //MBL
                    oceanMblInfo = new OceanMBLInfo
                    {
                        No = currentItem.No,
                        ReleaseType = currentItem.ReleaseType,
                        ID = currentItem.MBLID
                    };
                }
                else
                {
                    //HBL;
                    oceanHbl = new OceanHBLInfo
                    {
                        No = currentItem.No,
                        ReleaseType = currentItem.ReleaseType,
                        ID = currentItem.ID
                    };
                }
            }

            return ClientOceanExportService.MailCustomerAskForConfirmSI(isEnglish, MailInformation, BusinessContext.OperationID, oceanHbl, oceanMblInfo);
        }

        /// <summary>
        /// 利润承诺
        /// </summary>
        /// <param name="isEnglish">是否英文</param>
        public string MailSalesManAskForProfitPromise(bool isEnglish)
        {
            if (BusinessContext == null || BusinessContext.OperationID == Guid.Empty) return "";

            return ClientOceanExportService.MailSalesManAskForProfitPromise(isEnglish, BusinessContext.OperationID,MailInformation);
        }

        /// <summary>
        /// 提醒客户补料
        /// </summary>
        /// <param name="isEnglish">是否英文</param>
        public string MailCustomerAskForSi(bool isEnglish)
        {
            if (BusinessContext == null || BusinessContext.OperationID == Guid.Empty) return "";

            return ClientOceanExportService.MailCustomerAskForSi(isEnglish, BusinessContext.OperationID,MailInformation);
        }

        /// <summary>
        /// 确认费用
        /// </summary>
        /// <param name="isEnglish">是否英文</param>
        public string MailSalesForConfirmDebitFees(bool isEnglish)
        {
            if (BusinessContext == null || BusinessContext.OperationID == Guid.Empty) return "";

            return ClientOceanExportService.MailSalesForConfirmDebitFees(isEnglish, BusinessContext.OperationID);
        }
        #endregion

        /// <summary>
        /// 根据XML构造对应的菜单项
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private List<ToolStripMenuItem> GetToolStripMenuItem(List<OperationToolbarCommand> cmds)
        {
            List<ToolStripMenuItem> tools = new List<ToolStripMenuItem>();
            foreach (OperationToolbarCommand cmd in cmds)
            {
                ToolStripMenuItem tool = new ToolStripMenuItem();

                tool.Name = cmd.Name;

                if (cmd.Type != MenuItemType.TextBox)
                {
                    tool.Text = cmd.Text;
                }
                tool.Enabled = cmd.Enabled;
                tool.Visible = cmd.Visible;
                if (!string.IsNullOrEmpty(cmd.Name))
                {
                    if ("MBLHBL".Equals(cmd.ClickOperation))
                    {
                        foreach (var item in oblList)
                        {
                            ToolStripMenuItem tsmi = new ToolStripMenuItem();
                            tsmi.Name = cmd.Name;
                            tsmi.Text = (item.BLType == FCM.Common.ServiceInterface.DataObjects.FCMBLType.MBL ? "MBL:" : "HBL:") + item.No;
                            tsmi.Tag = item.ID;
                            tsmi.Click += toolStripItem_Click;
                            tool.DropDownItems.Add(tsmi);
                        }
                    }
                    else
                    {
                        tool.Click += toolStripItem_Click;
                    }
                    tools.Add(tool);
                }
            }
            return tools;
        }

        private void toolStripItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item == null) return;
            string itemName = item.Name;
            MethodInfo method = this.ucList.GetType().GetMethod(itemName);
            method.Invoke(this.ucList, new object[] { sender, e });
        }
    }
}
