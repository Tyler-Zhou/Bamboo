/**
 *  创建时间:2014-07-18
 *  创建人:Joabwang    
 *  描  述:鼠标右键菜单集合
 **/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using ICP.Business.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Operation.Common.ServiceInterface;
//using Microsoft.Office.Interop.Outlook;
using Exception = System.Exception;

namespace ICP.MailCenterFramework.UI
{
    public class ContextMenuTemplate
    {
        #region 全局变量，服务

        public static object obj = new object();
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationId
        {
            get { return Parameter.OperationId; }
        }

        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNo
        {
            get { return Parameter.OperationNo; }
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateDate
        {
            get { return Parameter.UpdateDate; }
        }

        /// <summary>
        /// 业务类型
        /// </summary>
        public OperationType OperationType
        {
            get { return Parameter.OperationType; }
        }
        /// <summary>
        /// MBL,HBL号
        /// </summary>
        public string BLNO
        {
            get { return Parameter.BLNO; }
        }

        /// <summary>
        /// 数据库视图CODE的名称
        /// </summary>
        public string TemplateCode
        {
            get { return Parameter.TemplateCode; }
        }

        /// <summary>
        /// 当前邮件实体
        /// </summary>
        public Message.ServiceInterface.Message Message
        {
            get { return Parameter.Message; }

        }

        /// <summary>
        /// 业务查询接口
        /// </summary>
        public IBusinessQueryService BusinessQueryService
        {

            get
            {
                IBusinessQueryService temp = null;
                try
                {
                    temp = ServiceClient.GetService<IBusinessQueryService>();
                }
                catch (Exception ex)
                {
                    ToolUtility.WriteLog("ContextMenuTemplate IBusinessQueryService", ex);
                    temp = null;
                }
                return temp;
            }
        }

        /// <summary>
        /// FCM 公共服务接口
        /// </summary>
        public IICPCommonOperationService ICPCommonOperationService
        {
            get
            {
                IICPCommonOperationService temp = null;
                try
                {
                    temp = ServiceClient.GetService<IICPCommonOperationService>();
                }
                catch (Exception ex)
                {
                    ToolUtility.WriteLog("Assist ICPCommonOperationService", ex);
                    temp = null;
                }
                return temp;
            }
        }

        /// <summary>
        /// 面板使用方法集合  确保只有一个实体
        /// </summary>
        private Assist _Assist = new Assist();
        /// <summary>
        /// 构造鼠标右键菜单项
        /// </summary>
        private readonly string fileRootDirectory = Path.Combine(LocalData.MainPath, "BusinessTemplates");

        /// <summary>
        /// 
        /// </summary>
        private readonly string tempalteFileName = "ContextMenuTemplate.xml";

        /// <summary>
        /// HBL号
        /// </summary>
        public string HblNo = string.Empty;
        /// <summary>
        /// MBL号
        /// </summary>
        public string MbLno = string.Empty;
        #endregion



        /// <summary>
        /// 生成ToolStripMenuItem集合
        /// </summary>
        /// <param name="sectionCode"></param>
        /// <returns></returns>
        public List<ToolStripMenuItem> GetItems(string sectionCode)
        {
            string templateFilePath = Path.Combine(fileRootDirectory, tempalteFileName);
            XDocument document = XDocument.Load(templateFilePath);
            IEnumerable<XElement> result = document.Descendants(XName.Get(sectionCode));
            if (result == null || result.Count() == 0)
            {
                return new List<ToolStripMenuItem>();
            }
            if (result.First() == null)
            {
                return new List<ToolStripMenuItem>();
            }
            var elements = document.Descendants(XName.Get(sectionCode)).First().Descendants(XName.Get("Item"));
            if (elements.Count() <= 0)
                return new List<ToolStripMenuItem>();
            List<ToolStripMenuItem> toolStripMenu = new List<ToolStripMenuItem>();
            #region 当前业务是海出的业务制动生成的菜单项
            if (OperationType == OperationType.OceanExport && !string.IsNullOrEmpty(BLNO))
            {
                string Text = LocalData.IsEnglish ? "Open" : "打开";
                string[] str = BLNO.Replace("\n", "*").Split('*');
                for (int i = 0; i < str.Count(); i++)
                {
                    ToolStripMenuItem tool = new ToolStripMenuItem();
                    if (str[i].Contains("MBL"))
                    {
                        MbLno = str[i].Split(':')[1];
                        tool.Name = "MBL" + i;
                        tool.Text = Text + str[i].Replace(" ", string.Empty);
                        tool.Tag = string.Empty;
                        tool.Click += EditMBL;

                    }
                    else
                    {
                        HblNo = str[i].Split(':')[1];
                        tool.Name = "HBL" + i;
                        tool.Tag = string.Empty;
                        tool.Text = Text + str[i].Replace(" ", string.Empty);
                        tool.Click += EditHBL;
                    }
                    toolStripMenu.Add(tool);
                }
            }
            #endregion

            toolStripMenu.AddRange(elements.Select(e => GetToolStripMenuItem(e)));

            return toolStripMenu;
        }



        /// <summary>
        /// 根据XML构造对应的菜单项
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public ToolStripMenuItem GetToolStripMenuItem(XElement e)
        {
            ToolStripMenuItem tool = new ToolStripMenuItem();
            tool.Name = e.Attribute("Id").Value;
            tool.Tag = e.Attribute("RegisterSite").Value;
            XElement captionElement = e.Element(XName.Get("Text"));
            string attributeName = LocalData.IsEnglish ? "enus" : "zhcn";
            if (captionElement != null)
            {
                tool.Text = captionElement.Attribute(XName.Get(attributeName)).Value;
            }
            string name = e.Attribute("Name").Value;
            if (!string.IsNullOrEmpty(name))
            {
                MethodInfo _objs = this.GetType().GetMethod(name);
                object meobj = System.Activator.CreateInstance(this.GetType());
                tool.Click += delegate { _objs.Invoke(meobj, null); };
            }
            //快捷方式
            tool.AccessibleName = name;
            if (e.Attribute("BeginGroup") != null)
            {
                tool.AutoToolTip = bool.Parse(e.Attribute("BeginGroup").Value);
            }
            return tool;
        }
        #region  按钮实现方法

        private Thread thread = null;
        private string text = null;
        /// <summary>
        /// 打开订舱单
        /// </summary>
        public void Command_ForSoEdit()
        {
            thread = new Thread(new ThreadStart(ForSoEdit));
            thread.Name = "ForSoEdit";
            thread.Start();
            text = LocalData.IsEnglish ? "Cancel So Edit" : "取消打开订舱单";
            MessageBoxShow(thread, text);
        }
        /// <summary>
        /// 打开派车单
        /// </summary>
        public void Command_CommunicationOrderTruck()
        {
            thread = new Thread(new ThreadStart(CommunicationOrderTruck));
            thread.Name = "CommunicationOrderTruck";
            thread.Start();
            text = LocalData.IsEnglish ? "Cancel Open Order Truck" : "取消打开派车单";
            MessageBoxShow(thread, text);
        }
        /// <summary>
        /// 打开报关单
        /// </summary>
        public void Command_CommunicationOrderCustoms()
        {
            thread = new Thread(new ThreadStart(CommunicationOrderCustoms));
            thread.Name = "CommunicationOrderCustoms";
            thread.Start();
            text = LocalData.IsEnglish ? "Cancel Open Order Customs" : "取消打开报关单";
            MessageBoxShow(thread, text);
        }
        /// <summary>
        /// 打开账单
        /// </summary>
        public void Command_CommunicationAccInfo()
        {
            thread = new Thread(new ThreadStart(CommunicationAccInfo));
            thread.Name = "CommunicationAccInfo";
            thread.Start();
            text = LocalData.IsEnglish ? "Cancel Open Bill" : "取消打开账单";
            MessageBoxShow(thread, text);
        }
        /// <summary>
        /// 上传SI附件
        /// </summary>
        public void Command_EmailCenterUploadSIAttachment()
        {
            thread = new Thread(new ThreadStart(EmailCenterUploadSIAttachment));
            thread.Name = "EmailCenterUploadSIAttachment";
            thread.Start();
            text = LocalData.IsEnglish ? "Cancel Upload SI Attachment" : "取消上传SI附件";
            MessageBoxShow(thread, text);
        }
        /// <summary>
        /// 上传SO附件
        /// </summary>
        public void Command_EmailCenterUploadSOAttachment()
        {
            thread = new Thread(new ThreadStart(EmailCenterUploadSOAttachment));
            thread.Name = "EmailCenterUploadSOAttachment";
            thread.Start();
            text = LocalData.IsEnglish ? "Cancel Upload SO Attachment" : "取消上传SO附件";
            MessageBoxShow(thread, text);
        }
        /// <summary>
        /// 上传MBL附件
        /// </summary>
        public void Command_EmailCenterUploadMBLAttachment()
        {
            thread = new Thread(new ThreadStart(EmailCenterUploadMBLAttachment));
            thread.Name = "EmailCenterUploadMBLAttachment";
            thread.Start();
            text = LocalData.IsEnglish ? "Cancel Upload MBL Attachment" : "取消上传MBL附件";
            MessageBoxShow(thread, text);
        }
        /// <summary>
        /// 上传A/P附件
        /// </summary>
        public void Command_EmailCenterUploadAPAttachment()
        {
            thread = new Thread(new ThreadStart(EmailCenterUploadAPAttachment));
            thread.Name = "EmailCenterUploadAPAttachment";
            thread.Start();
            text = LocalData.IsEnglish ? "Cancel Upload AP Attachment" : "取消上传A/P附件";
            MessageBoxShow(thread, text);
        }
        /// <summary>
        /// 上传附件
        /// </summary>
        public void Command_EmailCenterUploadAttachment()
        {
            thread = new Thread(new ThreadStart(EmailCenterUploadAttachment));
            thread.Name = "EmailCenterUploadAttachment";
            thread.Start();
            text = LocalData.IsEnglish ? "Cancel Upload Attachment" : "取消上传附件";
            MessageBoxShow(thread, text);
        }
        /// <summary>
        /// 新增MBL
        /// </summary>
        public void Command_EmailCenterAddMBL()
        {
            thread = new Thread(new ThreadStart(EmailCenterAddMBL));
            thread.Name = "EmailCenterAddMBL";
            thread.Start();
            text = LocalData.IsEnglish ? "Cancel Add MBL" : "取消新增MBL";
            MessageBoxShow(thread, text);
        }
        /// <summary>
        /// 新增HBL
        /// </summary>
        public void Command_EmailCenterAddHBL()
        {
            thread = new Thread(new ThreadStart(EmailCenterAddHBL));
            thread.Name = "EmailCenterAddHBL";
            thread.Start();
            text = LocalData.IsEnglish ? "Cancel Add HBL" : "取消新增HBL";
            MessageBoxShow(thread, text);
        }
        /// <summary>
        /// 货物跟踪
        /// </summary>
        public void Command_CommunicationCargoTracking()
        {

        }
        /// <summary>
        /// 打开任务中心
        /// </summary>
        public void Command_EmailCenterOpenTaskCenter()
        {
            thread = new Thread(new ThreadStart(EmailCenterOpenTaskCenter));
            thread.Name = "EmailCenterOpenTaskCenter";
            thread.Start();
            text = LocalData.IsEnglish ? "Cancel Open TaskCenter" : "取消打开任务中心";
            MessageBoxShow(thread, text);
        }
        /// <summary>
        /// 查看联系人/参与者
        /// </summary>
        public void Command_EmailCenterShowContactsAndAssistants()
        {
            thread = new Thread(new ThreadStart(EmailCenterShowContactsAndAssistants));
            thread.Name = "EmailCenterShowContactsAndAssistants";
            thread.Start();
            text = LocalData.IsEnglish ? "Cancel Show Contacts And Assistants" : "取消显示联系人和参与者";
            MessageBoxShow(thread, text);
        }
        /// <summary>
        /// 此业务的客户历史邮件
        /// </summary>
        public void CommandMail_MailofCustomer()
        {
            thread = new Thread(new ThreadStart(MailofCustomer));
            thread.Name = "MailofCustomer";
            thread.Start();
            text = LocalData.IsEnglish ? "Cancel Open The shipment's mail history of Customer" : "取消打开此业务的客户历史邮件";
            MessageBoxShow(thread, text);
        }
        /// <summary>
        /// 此业务的承运人历史邮件
        /// </summary>
        public void CommandMail_HistoryofCarrier()
        {
            thread = new Thread(new ThreadStart(HistoryofCarrier));
            thread.Name = "HistoryofCarrier";
            thread.Start();
            text = LocalData.IsEnglish ? "Cancel Open The shipment's mail history of Carrier" : "取消打开此业务的承运人历史邮件";
            MessageBoxShow(thread, text);
        }
        /// <summary>
        /// 此业务的代理历史邮件
        /// </summary>
        public void CommandMail_HistoryofAgent()
        {
            thread = new Thread(new ThreadStart(HistoryofAgent));
            thread.Name = "HistoryofAgent";
            thread.Start();
            text = LocalData.IsEnglish ? "Cancel Open The shipment's mail history of Agent" : "取消打开此业务的代理历史邮件";
            MessageBoxShow(thread, text);
        }
        /// <summary>
        /// 此业务的历史邮件
        /// </summary>
        public void CommandMail_MailHistory()
        {
            thread = new Thread(new ThreadStart(MailHistory));
            thread.Name = "MailHistory";
            thread.Start();
            text = LocalData.IsEnglish ? "Cancel Open The shipment's mail history" : "取消打开此业务的历史邮件";
            MessageBoxShow(thread, text);
        }
        /// <summary>
        /// 客户的历史邮件
        /// </summary>
        public void CommandMail_HistoryofCustomers()
        {
            thread = new Thread(new ThreadStart(HistoryofCustomers));
            thread.Name = "HistoryofCustomers";
            thread.Start();
            text = LocalData.IsEnglish ? "Cancel Open Mail history of Customer" : "取消打开客户的历史邮件";
            MessageBoxShow(thread, text);
        }
        /// <summary>
        /// 业务的承运人历史邮件
        /// </summary>
        public void CommandMail_HistoryofCarriers()
        {
            thread = new Thread(new ThreadStart(HistoryofCarriers));
            thread.Name = "HistoryofCarriers";
            thread.Start();
            text = LocalData.IsEnglish ? "Cancel Open Mail history of Carrier" : "取消打开业务的承运人历史邮件";
            MessageBoxShow(thread, text);
        }
        /// <summary>
        /// 业务的代理历史邮件
        /// </summary>
        public void CommandMail_HistoryofAgents()
        {
            thread = new Thread(new ThreadStart(HistoryofAgents));
            thread.Name = "HistoryofAgents";
            thread.Start();
            text = LocalData.IsEnglish ? "Cancel Open Mail history of Agent" : "取消打开业务的代理历史邮件";
            MessageBoxShow(thread, text);
        }
        /// <summary>
        /// 海运进口打开业务
        /// </summary>
        public void OceanImport_Edit()
        {
            thread = new Thread(new ThreadStart(OceanImportEdit));
            thread.Name = "OceanImportEdit";
            thread.Start();
            text = LocalData.IsEnglish ? "Cancel Edit Business" : "取消打开业务";
            MessageBoxShow(thread, text);

        }
        /// <summary>
        /// 打开账单
        /// </summary>
        public void OceanImport_Bill()
        {
            thread = new Thread(new ThreadStart(OceanImportBill));
            thread.Name = "OceanImportBill";
            thread.Start();
            text = LocalData.IsEnglish ? "Cancel Open Bill" : "取消打开账单";
            MessageBoxShow(thread, text);
        }
        /// <summary>
        /// 上传A/N附件
        /// </summary>
        public void Command_EmailCenterUploadANAttachment()
        {
            thread = new Thread(new ThreadStart(EmailCenterUploadANAttachment));
            thread.Name = "EmailCenterUploadANAttachment";
            thread.Start();
            text = LocalData.IsEnglish ? "Cancel Upload AN Attachment" : "取消上传A/N附件";
            MessageBoxShow(thread, text);
        }
        /// <summary>
        /// 邮件-询问人
        /// </summary>
        public void Command_EmailMailtoAskpeople()
        {
            thread = new Thread(new ThreadStart(EmailMailtoAskpeople));
            thread.Name = "EmailMailtoAskpeople";
            thread.Start();
            text = LocalData.IsEnglish ? "Cancel Email Mail to Askpeople" : "取消邮件-询问人";
            MessageBoxShow(thread, text);
        }
        /// <summary>
        /// 邮件-承运人
        /// </summary>
        public void Command_EmailMailtoCarrier()
        {
            thread = new Thread(new ThreadStart(EmailMailtoCarrier));
            thread.Name = "EmailMailtoCarrier";
            thread.Start();
            text = LocalData.IsEnglish ? "Cancel Email Mail to Carrier" : "取消邮件-承运人";
            MessageBoxShow(thread, text);
        }
        /// <summary>
        /// 编辑HBL
        /// </summary>
        public void EditHBL(object sender, EventArgs e)
        {
            thread = new Thread(new ThreadStart(EditHBLMethods));
            thread.Name = "EditHBLMethods";
            thread.Start();
            text = LocalData.IsEnglish ? "Cancel Edit HBL" : "取消编辑HBL";
            MessageBoxShow(thread, text);
        }
        /// <summary>
        /// 打开MBL
        /// </summary>
        public void EditMBL(object sender, EventArgs e)
        {
            thread = new Thread(new ThreadStart(EditMBLMethods));
            thread.Name = "EditMBLMethods";
            thread.Start();
            text = LocalData.IsEnglish ? "Cancel Edit MBL" : "取消编辑MBL";
            MessageBoxShow(thread, text);
        }
        #endregion

        #region  方法
        /// <summary>
        /// 对话框弹出
        /// </summary>
        /// <param name="thread">当前使用的线程</param>
        /// <param name="text">文本信息</param>
        public void MessageBoxShow(Thread thread, string text)
        {
            //线程执行完,隐藏对话框
            if (thread.IsAlive)
            {
                var messageBoxFrom = new MessageBoxFrom
                {
                    Thread = thread,
                    Text = text
                };
                messageBoxFrom.Hide();
            }
        }
        /// <summary>
        /// 打开订舱单
        /// </summary>
        public void ForSoEdit()
        {
            try
            {
                Parameter.Performflg = false;
                Parameter.Calledtime = DateTime.Now;
                if (OperationId != Guid.Empty && !string.IsNullOrEmpty(OperationNo))
                {
                    var editPartShowCriteria = new EditPartShowCriteria
                    {
                        BillNo = OperationId,
                        OperationNo = OperationNo
                    };
                    Dictionary<string, object> dictionary = _Assist.CreateBusinessParameter(ActionType.Edit, false, OperationId, OperationNo, OperationType, UpdateDate, TemplateCode, Message);
                    ICPCommonOperationService.EditBooking(editPartShowCriteria, dictionary);
                    Parameter.Performflg = true;
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("ContextMenuTemplate---ForSoEdit" , ex);
            }

        }
        /// <summary>
        /// 打开派车单
        /// </summary>
        public void CommunicationOrderTruck()
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Parameter.Performflg = false;
                    Parameter.Calledtime = DateTime.Now;
                    Dictionary<string, object> dictionary = _Assist.CreateBusinessParameter(ActionType.Edit, false, OperationId, OperationNo, OperationType, UpdateDate, TemplateCode, Message);
                    ICPCommonOperationService.OpenTruckOrder(OperationId, OperationNo, dictionary);
                    Parameter.Performflg = true;
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("ContextMenuTemplate---CommunicationOrderTruck" , ex);
            }
        }
        /// <summary>
        /// 打开报关单
        /// </summary>
        public void CommunicationOrderCustoms()
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Parameter.Performflg = false;
                    Parameter.Calledtime = DateTime.Now;
                    Dictionary<string, object> dictionary = _Assist.CreateBusinessParameter(ActionType.Edit, false, OperationId, OperationNo, OperationType, UpdateDate, TemplateCode, Message);
                    ICPCommonOperationService.OpenCustomsOrder(OperationId, dictionary);
                    Parameter.Performflg = true;
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("ContextMenuTemplate---CommunicationOrderCustoms",ex);
            }
        }
        /// <summary>
        /// 打开账单
        /// </summary>
        public void CommunicationAccInfo()
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Parameter.Performflg = false;
                    Parameter.Calledtime = DateTime.Now;
                    ICPCommonOperationService.OpenBill(OperationId, OperationType);
                    Parameter.Performflg = true;
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("ContextMenuTemplate---CommunicationAccInfo",ex);
            }

        }
        /// <summary>
        /// 上传SI附件
        /// </summary>
        public void EmailCenterUploadSIAttachment()
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Parameter.Performflg = false;
                    Parameter.Calledtime = DateTime.Now;
                    Message.Attachments = Parameter.MailAttachmentContents;

                    ICPCommonOperationService.Upload(UploadWay.DirectOpen, OperationId, Message.Subject, Message, SelectionType.SI, OperationType, new List<string>(), OperationNo, UpdateDate, Message);
                    Parameter.Performflg = true;
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("ContextMenuTemplate---EmailCenterUploadSIAttachment",ex);
            }
        }

        /// <summary>
        /// 上传SO附件
        /// </summary>
        public void EmailCenterUploadSOAttachment()
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Parameter.Performflg = false;
                    Parameter.Calledtime = DateTime.Now;
                    Message.Attachments = Parameter.MailAttachmentContents;

                    ICPCommonOperationService.Upload(UploadWay.DirectOpen, OperationId, Message.Subject, Message,
                        SelectionType.SO, OperationType, new List<string>(), OperationNo, UpdateDate, Message);
                    Parameter.Performflg = true;
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("ContextMenuTemplate---EmailCenterUploadSOAttachment" , ex);
            }
        }

        /// <summary>
        /// 上传MBL附件
        /// </summary>
        public void EmailCenterUploadMBLAttachment()
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Parameter.Performflg = false;
                    Parameter.Calledtime = DateTime.Now;
                    Message.Attachments = Parameter.MailAttachmentContents;

                    ICPCommonOperationService.Upload(UploadWay.DirectOpen, OperationId, Message.Subject, Message,
                        SelectionType.MBL, OperationType, new List<string>(), OperationNo, UpdateDate, Message);
                    Parameter.Performflg = true;
                }
            }
            catch (Exception ex)
            {
               ToolUtility.WriteLog("ContextMenuTemplate---EmailCenterUploadMBLAttachment", ex);
            }
        }

        /// <summary>
        /// 上传A/P附件
        /// </summary>
        public void EmailCenterUploadAPAttachment()
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Parameter.Performflg = false;
                    Parameter.Calledtime = DateTime.Now;
                    Message.Attachments = Parameter.MailAttachmentContents;

                    ICPCommonOperationService.Upload(UploadWay.DirectOpen, OperationId, Message.Subject, Message,
                        SelectionType.AP, OperationType, new List<string>(), OperationNo, UpdateDate, Message);
                    Parameter.Performflg = true;
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("ContextMenuTemplate---EmailCenterUploadAPAttachment" , ex);
            }
        }


        /// <summary>
        /// 上传附件
        /// </summary>
        public void EmailCenterUploadAttachment()
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Parameter.Performflg = false;
                    Parameter.Calledtime = DateTime.Now;
                    Message.Attachments = Parameter.MailAttachmentContents;
                    ICPCommonOperationService.Upload(UploadWay.DirectOpen, OperationId, Message.Subject, Message,
                        SelectionType.Normal, OperationType, new List<string>(), OperationNo, UpdateDate, Message);
                    Parameter.Performflg = true;
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("ContextMenuTemplate---EmailCenterUploadAttachment" , ex);
            }
        }

        /// <summary>
        /// 新增MBL
        /// </summary>
        public void EmailCenterAddMBL()
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Parameter.Performflg = false;
                    Parameter.Calledtime = DateTime.Now;
                    Dictionary<string, object> dic = _Assist.CreateBusinessParameter(ActionType.Create, false,
                        OperationId, OperationNo, OperationType, UpdateDate, TemplateCode, Message);
                    ICPCommonOperationService.AddMBL(dic);
                    Parameter.Performflg = true;
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("ContextMenuTemplate---EmailCenterAddMBL" , ex);
            }

        }

        /// <summary>
        /// 新增HBL
        /// </summary>
        public void EmailCenterAddHBL()
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Parameter.Performflg = false;
                    Parameter.Calledtime = DateTime.Now;
                    Dictionary<string, object> dictionary = _Assist.CreateBusinessParameter(ActionType.Create, false,
                        OperationId, OperationNo, OperationType, UpdateDate, TemplateCode, Message);
                    ICPCommonOperationService.AddHBL(dictionary);
                    Parameter.Performflg = true;
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("ContextMenuTemplate---EmailCenterAddHBL" , ex);
            }
        }


        /// <summary>
        /// 打开任务中心
        /// </summary>
        public void EmailCenterOpenTaskCenter()
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Parameter.Performflg = false;
                    Parameter.Calledtime = DateTime.Now;
                    BusinessOperationContext operationContext = new BusinessOperationContext
                    {
                        OperationID = OperationId,
                        OperationNO = OperationNo,
                        OperationType = OperationType
                    };
                    ICPCommonOperationService.OpenTaskCenter(operationContext);
                    Parameter.Performflg = true;
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("ContextMenuTemplate---EmailCenterOpenTaskCenter" , ex);
            }

        }

        /// <summary>
        /// 查看联系人/参与者
        /// </summary>
        public void EmailCenterShowContactsAndAssistants()
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Parameter.Performflg = false;
                    Parameter.Calledtime = DateTime.Now;
                    ICPCommonOperationService.ShowContactsAndAssistants(OperationId, OperationType, OperationNo);
                    Parameter.Performflg = true;
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("ContextMenuTemplate---EmailCenterShowContactsAndAssistants" , ex);
            }
        }


        /// <summary>
        ///  此业务的客户历史邮件
        /// </summary>
        public void MailofCustomer()
        {
            try
            {
                Parameter.Performflg = false;
                Parameter.Calledtime = DateTime.Now;
                ICPCommonOperationService.OeEmailQueryPartShow(OperationNo, 0, 1, 0, string.Empty, null,
                             LocalData.IsEnglish ? "The shipment's mail history of Customer" : "此业务的客户历史邮件", null, OperationType);
                Parameter.Performflg = true;
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("ContextMenuTemplate---MailofCustomer" , ex);
            }
        }

        /// <summary>
        /// 此业务的承运人历史邮件
        /// </summary>
        public void HistoryofCarrier()
        {
            try
            {
                Parameter.Performflg = false;
                Parameter.Calledtime = DateTime.Now;
                ICPCommonOperationService.OeEmailQueryPartShow(OperationNo, 0, 2, 0, string.Empty, null,
                          LocalData.IsEnglish ? "The shipment's mail history of Carrier" : "此业务的承运人历史邮件", null, OperationType);
                Parameter.Performflg = true;
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("ContextMenuTemplate---HistoryofCarrier" , ex);
            }
        }

        /// <summary>
        /// 此业务的代理历史邮件
        /// </summary>
        public void HistoryofAgent()
        {
            try
            {
                Parameter.Performflg = false;
                Parameter.Calledtime = DateTime.Now;
                ICPCommonOperationService.OeEmailQueryPartShow(OperationNo, 0, 3, 0, string.Empty, null,
                                            LocalData.IsEnglish ? "The shipment's mail history of Agent" : "此业务的代理历史邮件", null, OperationType);
                Parameter.Performflg = true;
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("ContextMenuTemplate---HistoryofAgent" , ex);
            }
        }

        /// <summary>
        /// 此业务的历史邮件
        /// </summary>
        public void MailHistory()
        {
            try
            {
                Parameter.Performflg = false;
                Parameter.Calledtime = DateTime.Now;
                ICPCommonOperationService.OeEmailQueryPartShow(OperationNo, 0, 0, 0, string.Empty, null,
                           LocalData.IsEnglish ? "The shipment's mail history" : "此业务的历史邮件", null, OperationType);
                Parameter.Performflg = true;
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("ContextMenuTemplate---MailHistory" , ex);
            }
        }
        /// <summary>
        /// 客户的历史邮件
        /// </summary>
        public void HistoryofCustomers()
        {
            try
            {
                Parameter.Performflg = false;
                Parameter.Calledtime = DateTime.Now;
                ICPCommonOperationService.OeEmailQueryPartShow(string.Empty, 1, 0, 1, "4,5,6,1", 7,
                                               LocalData.IsEnglish ? "Mail history of Customer" : "客户的历史邮件", OperationId, OperationType);
                Parameter.Performflg = true;
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("ContextMenuTemplate---HistoryofCustomers" , ex);
            }
        }

        /// <summary>
        /// 业务的承运人历史邮件
        /// </summary>
        public void HistoryofCarriers()
        {
            try
            {
                Parameter.Performflg = false;
                Parameter.Calledtime = DateTime.Now;
                ICPCommonOperationService.OeEmailQueryPartShow(string.Empty, 1, 0, 1, "3,2,1", 7,
                             LocalData.IsEnglish ? "Mail history of Carrier" : "业务的承运人历史邮件", OperationId, OperationType);
                Parameter.Performflg = true;
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("ContextMenuTemplate---HistoryofCarriers" , ex);
            }

        }

        /// <summary>
        /// 业务的代理历史邮件
        /// </summary>
        public void HistoryofAgents()
        {
            try
            {
                Parameter.Performflg = false;
                Parameter.Calledtime = DateTime.Now;
                ICPCommonOperationService.OeEmailQueryPartShow(string.Empty, 1, 0, 1, "8,1", 7,
                                              LocalData.IsEnglish ? "Mail history of Agent" : "业务的代理历史邮件", OperationId, OperationType);
                Parameter.Performflg = true;
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("ContextMenuTemplate---HistoryofAgents" , ex);
            }
        }

        /// <summary>
        /// 海运进口打开业务
        /// </summary>
        public void OceanImportEdit()
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Parameter.Performflg = false;
                    Parameter.Calledtime = DateTime.Now;
                    if (OperationId != Guid.Empty && OperationNo != string.Empty)
                    {
                        var editPartShowCriteria = new EditPartShowCriteria
                        {
                            BillNo = OperationId,
                            OperationNo = OperationNo
                        };
                        Dictionary<string, object> dictionary = _Assist.CreateBusinessParameter(ActionType.Edit, false,
                            OperationId, OperationNo, OperationType, UpdateDate, TemplateCode, Message);
                        ICPCommonOperationService.EditOIBusiness(editPartShowCriteria, dictionary);
                        Parameter.Performflg = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("ContextMenuTemplate---OceanImportEdit" , ex);
            }

        }

        /// <summary>
        /// 打开账单
        /// </summary>
        public void OceanImportBill()
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Parameter.Performflg = false;
                    Parameter.Calledtime = DateTime.Now;
                    if (OperationId != Guid.Empty && OperationNo != string.Empty)
                    {
                        ICPCommonOperationService.OpenOIBill(OperationId);
                        Parameter.Performflg = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("ContextMenuTemplate---OceanImportBill" , ex);
            }

        }

        /// <summary>
        /// 上传A/N附件
        /// </summary>
        public void EmailCenterUploadANAttachment()
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Parameter.Performflg = false;
                    Parameter.Calledtime = DateTime.Now;
                    Message.Attachments = Parameter.MailAttachmentContents;
                    ICPCommonOperationService.Upload(UploadWay.DirectOpen, OperationId, Message.Subject, Message,
                        SelectionType.AN, OperationType, new List<string>(), OperationNo, UpdateDate, Message);
                    Parameter.Performflg = true;
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("ContextMenuTemplate---EmailCenterUploadANAttachment" , ex);
            }

        }

        /// <summary>
        /// 邮件-询问人
        /// </summary>
        public void EmailMailtoAskpeople()
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Parameter.Performflg = false;
                    Parameter.Calledtime = DateTime.Now;
                    ICPCommonOperationService.EmailMailtoAskpeople(OperationId);
                    Parameter.Performflg = true;
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("ContextMenuTemplate---EmailMailtoAskpeople" , ex);
            }

        }

        /// <summary>
        /// 邮件-承运人
        /// </summary>
        public void EmailMailtoCarrier()
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Parameter.Performflg = false;
                    Parameter.Calledtime = DateTime.Now;
                    ICPCommonOperationService.EmailtoCarrier(OperationId);
                    Parameter.Performflg = true;
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("ContextMenuTemplate---EmailMailtoCarrier" , ex);
            }
        }

        /// <summary>
        /// 编辑HBL
        /// </summary>
        public void EditHBLMethods()
        {
            try
            {
                Parameter.Performflg = false;
                Parameter.Calledtime = DateTime.Now;
                Dictionary<string, object> dic = _Assist.CreateBusinessParameter(ActionType.Edit, false, OperationId, OperationNo, OperationType, UpdateDate, TemplateCode, Message);
                ICPCommonOperationService.EditHBL(OperationNo, HblNo.Trim(), dic);
                Parameter.Performflg = true;
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("ContextMenuTemplate---EditHBLMethods" , ex);
            }
        }

        /// <summary>
        /// 编辑MBL
        /// </summary>
        public void EditMBLMethods()
        {
            try
            {
                Parameter.Performflg = false;
                Parameter.Calledtime = DateTime.Now;
                Dictionary<string, object> dic = _Assist.CreateBusinessParameter(ActionType.Edit, false, OperationId, OperationNo, OperationType, UpdateDate, TemplateCode, Message);
                ICPCommonOperationService.EditMBL(OperationNo, MbLno.Trim(), dic);
                Parameter.Performflg = true;
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("ContextMenuTemplate---EditMBLMethods" , ex);
            }
        }
        #endregion
    }
}
