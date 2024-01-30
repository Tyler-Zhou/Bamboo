using System;
using System.Collections.Generic;
using System.Linq;
using ICP.MailCenter.ServiceInterface;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;
using ICP.Framework.CommonLibrary.Common;
using System.IO;
using System.Text.RegularExpressions;
using ICP.Framework.CommonLibrary.Client;
using DevExpress.XtraEditors;
using System.Diagnostics;
using Microsoft.Practices.CompositeUI;
using AxMicrosoft.Office.Interop.OutlookViewCtl;
using ICP.Message.ServiceInterface;
using System.Threading;
using System.Text;
using MailContactType = ICP.MailCenter.ServiceInterface.ContactType;

namespace ICP.MailCenter.UI
{
    /// <summary>
    /// 邮件列表呈现类
    /// </summary>
    public class MailListPresenter
    {
        #region  往搜索界面填入值
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, string lParam);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        const int WM_SETTEXT = 0x000C;
        #endregion

        public static WorkItem workItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }

        public static Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace ListWorkSpace
        {
            get
            {
                return workItem.Workspaces[MailCenterWorkSpaceConstants.ListWorkSpace] as Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace;
            }
        }
        public static EmailFolderPart FolderPart
        {
            get
            {
                return workItem.SmartParts.Get<EmailFolderPart>(MailCenterWorkSpaceConstants.EmailFolderPart);
            }
        }
        public static EmailListPart EmailListPart
        {
            get
            {
                return workItem.SmartParts.Get<EmailListPart>(MailCenterWorkSpaceConstants.EmailListPart);
            }
        }
        public static MAPIFolder DefaultContactsFolder
        {
            get { return ClientUtility.CreateOutlookNameSpaceInstance().GetDefaultFolder(OlDefaultFolders.olFolderContacts); }
        }
        /// <summary>
        /// 读取定制的视图配置文件 
        /// </summary>
        public static string _CustomViewXml;
        public static string CustomViewXml
        {
            get
            {
                if (_CustomViewXml == null)
                {
                    string filePath = string.Format(@"{0}\mail\CustomViewXml_{1}.xml", AppDomain.CurrentDomain.BaseDirectory, ClientUtility.olCultrue == "zh-cn" ? "CH" : "EN");
                    if (File.Exists(filePath))
                    {
                        _CustomViewXml = File.ReadAllText(filePath, System.Text.Encoding.UTF8);
                    }
                }
                return _CustomViewXml;
            }
        }

        /// <summary>
        /// 读取默认的视图配置文件 
        /// </summary>
        private static string _DefaultViewXml;
        public static string DefaultViewXml
        {
            get
            {
                if (_DefaultViewXml == null)
                {

                    string filePath = string.Format(@"{0}\mail\DefaultViewXml_{1}.xml", AppDomain.CurrentDomain.BaseDirectory, ClientUtility.olCultrue == "zh-cn" ? "CH" : "EN");

                    if (File.Exists(filePath))
                    {
                        _DefaultViewXml = File.ReadAllText(filePath, System.Text.Encoding.UTF8);
                    }
                }
                return _DefaultViewXml;
            }
        }


        public static void InvalidateEmailListRegion()
        {
            if (ListWorkSpace != null)
                ListWorkSpace.Invalidate(true);
        }

        public static void CopyText(string text)
        {
            try
            {
                Clipboard.Clear();
            }
            catch (System.Exception ex)
            {
                Thread.Sleep(100);
                Clipboard.Clear();
            }
            finally
            {
                try
                {
                    Clipboard.SetText(text);
                }
                catch (System.Exception ex)
                {
                }
            }
        }

        public static void CopyStringCollection(System.Collections.Specialized.StringCollection strCollection)
        {
            System.Windows.Forms.Clipboard.Clear();
            System.Windows.Forms.Clipboard.SetFileDropList(strCollection);
        }

        public static void AddRootFolderToList(string folderName)
        {
            string _folderName = folderName.ToLower();
            ClientProperties.RootFolders.Add(_folderName);
        }

        /// <summary>
        /// 是否包含根目录
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsContainsRootFolder(string name)
        {
            if (ClientProperties.RootFolders.Any(item => item.Equals(name.ToLower())))
                return true;

            return false;
        }
        public static MAPIFolder GetFolderByEntryID(string entryID)
        {
            MAPIFolder olFolder = null;
            try
            {
                olFolder = ClientUtility.OlNS.GetFolderFromID(entryID, Type.Missing);
            }
            catch (System.Exception ex)
            {
                try
                {
                    olFolder = ClientUtility.CreateOutlookNameSpaceInstance().GetFolderFromID(entryID, Type.Missing);
                }
                catch (System.Exception e)
                {
                    ICP.Framework.CommonLibrary.LogHelper.SaveLog(ICP.Framework.CommonLibrary.Common.CommonHelper.BuildExceptionString(ex));
                    return null;
                }
            }
            return olFolder;
        }

        public static MailItem GetMailItemByEntryID(string entryIDItem)
        {
            MailItem olItem = null;
            try
            {
                olItem = (MailItem)ClientUtility.OlNS.GetItemFromID(entryIDItem, Type.Missing);

            }
            catch (System.Exception ex)
            {
                try
                {
                    olItem =
                        (MailItem)
                        ClientUtility.CreateOutlookNameSpaceInstance().GetItemFromID(entryIDItem, Type.Missing);
                }
                catch (System.Exception e)
                {
                    ICP.Framework.CommonLibrary.LogHelper.SaveLog(ICP.Framework.CommonLibrary.Common.CommonHelper.BuildExceptionString(ex));
                    return null;
                }
            }
            return olItem;
        }

        /// <summary>
        /// 获取一个不能确定类型的Item对象
        /// </summary>
        /// <param name="entryID"></param>
        /// <returns></returns>
        public static object GetItemByEntryID(string entryID)
        {
            object objItem = null;
            try
            {
                objItem = ClientUtility.OlNS.GetItemFromID(entryID, Type.Missing);
            }
            catch (System.Exception ex)
            {
                try
                {
                    objItem = ClientUtility.CreateOutlookNameSpaceInstance().GetItemFromID(entryID, Type.Missing);
                }
                catch
                {
                }
            }

            return objItem;
        }


        public static void ShowReminderForm(string message, bool stay)
        {
            frmReminder frmReminder = new frmReminder(message, stay);
            frmReminder.Show();
        }

        /// <summary>
        /// 是否包含需要排序的列
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public static bool IsContainsOrderColumn(string folderName)
        {
            bool isContains = false;
            MailUtility.IsContainsFolders(MailUtility.OrderFolders, folderName, ref isContains);
            return isContains;
        }
        /// <summary>
        /// 根据选择文件夹来显示不同的排序列
        /// </summary>        
        static int flag = 0;  //避免重复排序列
        public static string OrderByViewXml(string folderName)
        {
            string viewXml = string.Empty;
            if (IsContainsOrderColumn(folderName))
            {
                if (flag == 2)
                    return viewXml;
                viewXml = string.IsNullOrEmpty(CustomViewXml) ? ClientProperties.ViewXmlTemp : CustomViewXml;
                flag = 2;
            }
            else
            {
                if (flag == 1)
                    return viewXml;
                viewXml = ClientProperties.ViewXmlTemp;
                flag = 1;
            }

            return viewXml;
        }

        public static bool IsVisitInternalMailBusinessPart()
        {
            object fixedMailCenterSize = workItem.State["LimitInternalMailSize"];
            if (fixedMailCenterSize == null || fixedMailCenterSize == "")
                return false;
            else
            {
                int num = -1;
                int.TryParse(fixedMailCenterSize.ToString(), out num);

                if (num == 1 || num == 2)
                    return true;
            }

            return false;
        }


        /// <summary>
        /// 获取配置文件中节点的配置信息
        /// </summary>
        /// <param name="splitControl"></param>
        /// <param name="configId"></param>
        public static void SetSplitterPosition(SplitContainerControl splitControl, string configId, int defaultPosition)
        {
            if (ClientConfig.Current.Contains(configId))
            {
                if (!string.IsNullOrEmpty(ClientConfig.Current.GetValue(configId)))
                {
                    int width = 0;
                    Int32.TryParse(ClientConfig.Current.GetValue(configId), out width);
                    splitControl.SplitterPosition = width == 0 ? defaultPosition : width;
                }
            }
            else
            {
                splitControl.SplitterPosition = defaultPosition;
            }
        }


        /// <summary>
        /// 记录分隔符位置到用户配置文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void SaveSplitterPosition(string key, string value)
        {
            ClientConfig.Current.AddValue(key, value);
        }
        public static string GetAccountType(SelectNamesDialog snd)
        {
            if (snd.Recipients.Count == 0)
            {
                return "Unknown";
            }
            //默认账户类型SMTP
            else if (string.IsNullOrEmpty(snd.Recipients[1].Address) && ClientProperties.CurrentMailItem != null)
            {
                return "SMTP";
            }
            else if (ClientProperties.CurrentMailItem == null)
            {
                return "SYSTEM";
            }
            else
            {
                return snd.Recipients[1].AddressEntry.Type;
            }
        }


        /// <summary>
        /// 打开搜索界面后将控件填充值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="text"></param>
        public static void OpenAdvancedSearchPart(MailContactType type, string text)
        {
            MailListPresenter.ShowSearchPart();

            IntPtr parenthWnd = new IntPtr(0);
            string title = ClientUtility.olCultrue == "zh-cn" ? "高级查找" : "Advanced Find";
            parenthWnd = FindWindow("rctrl_renwnd32", title);
            WindowsExtension.BringWindowToTop(parenthWnd);
            parenthWnd = FindWindow("rctrl_renwnd32", title);
            IntPtr formHwnd = FindWindowEx(parenthWnd, IntPtr.Zero, "rctrl_renwnd32", "");
            IntPtr dialogHwnd = FindWindowEx(formHwnd, IntPtr.Zero, "#32770", "");
            IntPtr subDialogHwnd = FindWindowEx(dialogHwnd, IntPtr.Zero, "#32770", "");
            IntPtr sunDialogHwnd = FindWindowEx(subDialogHwnd, IntPtr.Zero, "#32770", (ClientUtility.olCultrue == "zh-cn" ? "电子邮件" : "E-mail"));

            if (type == MailContactType.SendingByContact)
            {
                SendControlMessage(sunDialogHwnd, text, string.Empty);
            }
            else if (type == MailContactType.ReceivingByContact)
            {
                SendControlMessage(sunDialogHwnd, string.Empty, text);
            }
            else
            {
                SendControlMessage(sunDialogHwnd, text, text);
            }
        }

        public static void SendControlMessage(IntPtr sunDialogHwnd, string sender, string receiver)
        {
            IntPtr fromTextHwnd = new IntPtr(0);
            fromTextHwnd = FindWindowEx(sunDialogHwnd, IntPtr.Zero, "RichEdit20WPT", "");
            SendMessage(fromTextHwnd, WM_SETTEXT, IntPtr.Zero, sender);
            fromTextHwnd = FindWindowEx(sunDialogHwnd, fromTextHwnd, "RichEdit20WPT", "");
            SendMessage(fromTextHwnd, WM_SETTEXT, IntPtr.Zero, receiver);
        }


        /// <summary>
        /// 打开搜索界面
        /// </summary>
        /// <param name="_emailList"></param>
        public static void ShowSearchPart()
        {
            if (Process.GetProcessesByName("outlook").Length == 0)
            {

                try
                {
                    MailUtility.StartProcess(false, string.Empty);
                    ClientUtility.GetOutlookNewNameSpace();
                    BindViewCtlData(EmailListPart.axViewCtlEmailList, ClientProperties.folder_FullPath, string.Empty);
                }
                catch
                {
                }
                finally
                {
                    ClientUtility.GetOutlookNewNameSpace();
                    BindViewCtlData(EmailListPart.axViewCtlEmailList, ClientProperties.folder_FullPath, string.Empty);
                    ShowAdvanceFindDailog(EmailListPart);
                    TreeViewPresenter.RegisterAllExpandedNodes(FolderPart.trvFolder, true);
                }
            }
            else
            {
                ShowAdvanceFindDailog(EmailListPart);
            }
        }

        public static void BindViewCtlData(AxViewCtl axviewCtlEmailList, string fullFolderPath, string folderName)
        {
            string viewXml = MailListPresenter.OrderByViewXml(folderName);
            if (!string.IsNullOrEmpty(viewXml))
            {
                ClientProperties.selectedChanged = false;
                axviewCtlEmailList.ViewXML = viewXml;
            }
            ClientProperties.selectedChanged = false;
            axviewCtlEmailList.Folder = fullFolderPath;
        }
        /// <summary>
        /// 有权限去更改文件夹信息
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public static bool HasPermissionsChangedFolder(string folderName)
        {
            bool isContainsFolder = false;
            MailUtility.IsContainsFolders(MailUtility.AllFolders, folderName, ref isContainsFolder);
            return isContainsFolder;
        }

        public static void ShowAdvanceFindDailog(EmailListPart _emailList)
        {
            try
            {
                _emailList.axViewCtlEmailList.Refresh();
                _emailList.axViewCtlEmailList.AdvancedFind();
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(_emailList.FindForm(), ex.Message);
            }
        }

        /// <summary>
        /// add to contact
        /// </summary>
        /// <param name="emailAddress"></param>
        public static void AddToContact(string nickName, String emailAddress)
        {
            MAPIFolder folder = null; Items items = null; ContactItem newContact = null;
            try
            {
                newContact = (ContactItem)ClientUtility.OlApp.CreateItem(OlItemType.olContactItem);

                //filter the contact mail address
                if (!String.IsNullOrEmpty(emailAddress))
                {
                    newContact.NickName = nickName;
                    newContact.LastName = emailAddress;
                    newContact.Email1Address = emailAddress;
                    newContact.Email1DisplayName = emailAddress;
                }
                else
                {
                    MessageBox.Show((ClientUtility.olCultrue == "en-us" ? "Email address isn't complete." : "邮件地址不完整."));
                    return;
                }

                //Judge whether there is the same contact
                folder = ClientUtility.OlNS.GetDefaultFolder(OlDefaultFolders.olFolderContacts);
                String temp = String.Format("@SQL=" + "\"" + "urn:schemas:contacts:nickname" + "\"" + " like '%{0}%'", newContact.Email1Address);
                items = folder.Items.Restrict(temp);
                if (items.Count > 0)
                {
                    MessageBox.Show((ClientUtility.olCultrue == "en-us" ? "the contact has added." : "改联系人已添加."));
                    return;
                }
                else
                {
                    //newContact.Save();
                    newContact.Display(false);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show((ClientUtility.olCultrue == "en-us" ? "save contact failure: " : "保存联系人失败: ") + ex.Message);
            }
            finally
            {
                //if (newContact != null) Marshal.ReleaseComObject(newContact);
                MailUtility.ReleaseComObject(folder);
                MailUtility.ReleaseComObject(items);
                newContact = null;
            }
        }

        /// <summary>
        /// dynamic set control height
        /// </summary>
        /// <param name="width"></param>
        /// <param name="flpPanel"></param>
        /// <param name="pnlControl"></param>
        public static void SetControlHeight(Int32 width, FlowLayoutPanel flpPanel, Panel pnlControl)
        {
            //sett the control height
            if (width > flpPanel.Width)
            {
                float temp = (float)width / flpPanel.Width;
                int num_Height = (int)Math.Ceiling(temp);

                if (num_Height >= 1 && num_Height < 2)
                {
                    pnlControl.Height = 38; flpPanel.Height = 38;
                    flpPanel.AutoScroll = false;
                }
                else if (num_Height >= 2 && num_Height < 3)
                {
                    pnlControl.Height = flpPanel.Height = 50;
                    flpPanel.AutoScroll = false;

                }
                else
                {
                    ScrollControl(width, flpPanel, pnlControl);
                }
            }
            else { flpPanel.AutoScroll = false; }
        }

        private static void ScrollControl(int width, FlowLayoutPanel flpPanel, Panel pnlControl)
        {
            pnlControl.Height = flpPanel.Height = 65;
            // flpPanel.AutoScroll = true;
            flpPanel.AutoScroll = true;

            // WindowsExtension.ShowScrollBar(flpPanel.Handle, (int)ScrollBarDirection.SB_VERT, true);
            WindowsExtension.ShowScrollBar(flpPanel.Handle, (int)ScrollBarDirection.SB_HORZ, false);
        }

        //动态设置附件列表宽度
        public static void SetAttachmentControlHeight(Int32 width, FlowLayoutPanel flpPanel, Panel pnlControl)
        {
            //set the attachment height
            if (width > flpPanel.Width)
            {
                float num_height = (float)width / flpPanel.Width;
                if (num_height >= 1 && num_height < 2)
                {
                    pnlControl.Height = 52;
                    flpPanel.Height = 49;
                    flpPanel.AutoScroll = false;
                }

                else if (num_height >= 2 && num_height < 3)
                {
                    SetAttachmentHeight(flpPanel, pnlControl);
                }
                else
                {
                    SetAttachmentHeight(flpPanel, pnlControl);
                }
            }

            else { flpPanel.AutoScroll = false; }
        }

        private static void SetAttachmentHeight(FlowLayoutPanel flpPanel, Panel pnlControl)
        {
            flpPanel.AutoScroll = true;
            flpPanel.Height = 59;
            pnlControl.Height = 61;
            WindowsExtension.ShowScrollBar(flpPanel.Handle, (int)ScrollBarDirection.SB_HORZ, false);
        }

        public static SelectNamesDialog ResettingRecipients(SelectNamesDialog snd, string address)
        {
            Recipients olRecipients = snd.Recipients;

            if (olRecipients.Count > 0)
            {
                for (int i = 1; i <= olRecipients.Count; i++)
                {
                    snd.Recipients.Remove(i);
                }
            }
            snd.Recipients.Add(address);

            return snd;
        }

        //重新启动outlook后，重新获取邮件地址
        public static string ReGetEmailAddress(string text, object tag)
        {
            MailItem olItem = null;

            string address = string.Empty;
            //MailUtility.StartProcess();

            try
            {
                ClientUtility.GetOutlookNewNameSpace();
                EmailListPart.RefershCtl();
                ClientProperties.selectedChanged = false;
                EmailListPart.axViewCtlEmailList.Folder = ClientProperties.folder_FullPath;
            }
            catch { }
            finally
            {
                olItem = (MailItem)ClientUtility.CreateOutlookNameSpaceInstance().GetItemFromID(ClientProperties.SelectMail_EntryID, Type.Missing);
                if (olItem != null)
                {
                    address = MailListPresenter.GetEmailAddress(text, tag, olItem.Recipients);
                    TreeViewPresenter.RegisterAllExpandedNodes(FolderPart.trvFolder, true);
                }
            }

            return address;
        }

        /// <summary>
        /// 获取附件名称
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns></returns>
        public static string GetFileNameAndMemory(String filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            String fileSizeString = CommonHelper.GetFileSizeString(fileInfo.Length);
            return String.Format("{0}({1})", Path.GetFileName(filePath), fileSizeString);
        }

        public static void OpenMsgFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string destFilePath = Path.Combine(ClientProperties.TempPath, (Path.GetRandomFileName() + ".msg"));
                File.Copy(filePath, destFilePath);
                NameSpace olNS = null;
                MailItem olItem = null;
                try
                {
                    olNS = ClientUtility.OlApp.Session;
                    olItem = olNS.OpenSharedItem(destFilePath) as MailItem;
                    if (olItem != null)
                        olItem.Display(false);
                }
                catch
                {
                    using (
                        Process proc = System.Diagnostics.Process.Start("rundll32.exe",
                                                                        @"shell32,OpenAs_RunDLL " + destFilePath + ""))
                    {
                        if (proc != null)
                            proc.Dispose();
                    }
                }
                finally
                {
                    MailUtility.ReleaseComObject(olItem);
                    MailUtility.ReleaseComObject(olNS);
                }
            }
            else
            {
                throw new ICPException(LocalData.IsEnglish ? "the file does not exsit." : "该文件不存在.");
            }
        }

        /// <summary>
        /// 解析邮件的主体
        /// 1.将邮件中的图片保存到本地，并将邮件中的图片路径替换为本地路径
        /// 2.抽取邮件中真正的附件，剔除邮件背景图片附件
        /// </summary>
        /// <param name="attachments"></param>
        /// <param name="htmlBody"></param>
        /// <returns></returns>
        public static string FixHtmlBody(Attachments attachments, string htmlBody)
        {
            string body = htmlBody;
            Attachments _attachments = attachments;
            foreach (Attachment att in _attachments)
            {
                string fileName = GetAttachmentName(att);

                //http://schemas.microsoft.com/mapi/proptag/0x3712001F
                //http://schemas.microsoft.com/mapi/proptag/0x3704001F
                //http://schemas.microsoft.com/mapi/proptag/0x3707001F                
                //查询图片contentID
                string emCID = att.PropertyAccessor.GetProperty("http://schemas.microsoft.com/mapi/proptag/0x3712001F").ToString();

                if (!string.IsNullOrEmpty(emCID))
                {
                    //当可以查询到图片contentID，却没有要替换htmlBody中的字符，则该文件就是附件
                    bool isReplaceSign = false;
                    string fullFilePath = GetFileFullPath(fileName);
                    SaveAsLocalFile(att, fullFilePath);
                    //将平铺的背景图片替换
                    body = ReplaceHtmlBodyBackgroundImage(body, emCID, ref isReplaceSign);
                    //将图片路径图换掉
                    body = ReplaceHtmlBodyImagePathWithLocalPath(emCID, body, fullFilePath, ref isReplaceSign);

                    if (!isReplaceSign)
                    {
                        AttachmentContent attachmentInfo = AddAttachmentToList(att, body);
                        if (attachmentInfo != null)
                        {
                            ClientProperties.Attachments.Add(attachmentInfo);
                        }
                    }
                }
                else
                {
                    //特殊处理： 把Attachment为Image的文件类型去判断HtmlBody中是否包含该图片，如果不包含，则添加到集合;否则表示是真正的附件
                    AttachmentContent attachmentInfo = AddAttachmentToList(att, body);
                    if (attachmentInfo != null)
                    {
                        ClientProperties.Attachments.Add(attachmentInfo);
                    }
                }
            }
            MailUtility.ReleaseComObject(_attachments);

            return body;
        }

        /// <summary>
        /// 得到文件完整路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFileFullPath(string fileName)
        {
            string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempPath);
            return Path.Combine(tempPath, fileName);
        }
        private static AttachmentContent AddAttachmentToList(Attachment att, string body)
        {
            object objMime = att.PropertyAccessor.GetProperty("http://schemas.microsoft.com/mapi/proptag/0x370E001E");

            string emMime = objMime == null ? "" : objMime.ToString();
            if (emMime.ToLower().Contains("image"))
            {
                if (!body.Contains(att.FileName))
                {
                    return CreateAttachmentContentInfo(att);
                }
            }
            else
            {
                //将真正的附件添加到集合中
                return CreateAttachmentContentInfo(att);
            }

            return null;
        }
        /// <summary>
        /// 将附件转换成实体类
        /// </summary>
        /// <param name="att"></param>
        /// <returns></returns>
        public static AttachmentContent CreateAttachmentContentInfo(Attachment att)
        {
            return new AttachmentContent()
            {
                OLAttachment = att,
                Name = GetFileName(att),
                DisplayName = att.DisplayName ?? string.Empty,
                Size = att.Size,
                UploadTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified)
            };
        }

        private static string GetFileName(Attachment att)
        {
            string fileName = string.Empty;
            try
            {
                fileName = att.FileName;
            }
            catch (System.Exception ex)
            {
                fileName = att.DisplayName;
            }

            return fileName;
        }

        /// <summary>
        /// 获取附件名称
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns></returns>
        private static string GetAttachmentName(Attachment attachment)
        {
            string fileName = string.Empty;
            if (attachment.Type.Equals(OlAttachmentType.olOLE))
                fileName = attachment.DisplayName;
            else
                fileName = attachment.FileName;
            return fileName;
        }
        /// <summary>
        /// 获取附件的本地保存路径
        /// </summary>
        /// <param name="att"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static string GetAttachmentFullPath(Attachment att, ref string fileName)
        {
            if (att.Type.Equals(OlAttachmentType.olOLE))
                fileName = att.DisplayName;
            else
                fileName = att.FileName;

            return Path.Combine(ClientProperties.TempPath, fileName);
        }



        /// <summary>
        /// 将附件保存到本地磁盘
        /// </summary>
        /// <param name="att"></param>
        /// <param name="fullFilePath"></param>
        /// <returns></returns>
        public static string SaveAsLocalFile(Attachment att, string fullFilePath)
        {
            try
            {
                //将附件保存到本地
                att.SaveAsFile(fullFilePath);
            }
            //如果出现 “尝试读取或写入受保护的内存。这通常指示其他内存已损坏”异常，则文件名称重命名为 FileName(1).extension
            catch (System.AccessViolationException ave)
            {
                try
                {
                    string extension = Path.GetExtension(fullFilePath);
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fullFilePath);

                    string fileName = string.Format("{0}(1){1}", fileNameWithoutExtension, extension);
                    fullFilePath = Path.Combine(Path.GetDirectoryName(fullFilePath), fileName);
                    att.SaveAsFile(fullFilePath);
                }
                catch (System.Exception ex)
                {
                    throw;
                }
            }

            return fullFilePath;
        }

        public static Recipients GetMailRecipients()
        {
            Recipients olRecipients = null;
            MailItem olItem = null;
            try
            {
                olItem = MailListPresenter.GetMailItemByEntryID(ClientProperties.SelectMail_EntryID);
                if (olItem != null)
                    olRecipients = olItem.Recipients;
            }
            catch (System.Exception ex)
            {
                ICP.Framework.CommonLibrary.LogHelper.SaveLog(ICP.Framework.CommonLibrary.Common.CommonHelper.BuildExceptionString(ex));
            }
            finally
            {
                MailUtility.ReleaseComObject(olItem);
            }

            return olRecipients;
        }





        public static string GetEmailAddress(String address, object RecipientType, Recipients olRecipients)
        {
            String _address = String.Empty;
            string strExp = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            Regex rgMail = new Regex(strExp);
            _address = FixEmailAddress(address);
            //表示包含正确邮箱地址
            if (rgMail.IsMatch(address))
            {

                return _address;
            }
            else
            {

                int type = Int32.Parse(RecipientType.ToString());
                Recipient recipient = (from Recipient item in olRecipients
                                       where item.Type == type && (item.Name.Contains(_address) || item.Name.Contains(address))
                                       select item).First();
                bool isSucessed = recipient.Resolve();
                if (isSucessed)
                    _address = recipient.Address;
            }

            return _address.Trim();
        }

        public static string FixEmailAddress(string address)
        {
            return FixEmailAddress(address, true);
        }

        public static string GetNickName(string address)
        {
            return FixEmailAddress(address, false);
        }

        public static string FixEmailAddress(string address, bool returnEmailAddress)
        {
            address = address.Trim().TrimEnd(';');
            //mail address contains "'" and "'"
            if (address.StartsWith("'") && address.EndsWith("'"))
                address = address.Trim("'".ToCharArray());

            if (address.Contains("[") && address.Contains("]"))
            {
                address = SplitEmailAddress(address, '[', returnEmailAddress);
            }

            else if (address.Contains("<") && address.Contains(">"))
            {
                address = SplitEmailAddress(address, '<', returnEmailAddress);
            }
            return address.Trim();
        }

        private static string SplitEmailAddress(string address, char charSplit, bool returnEmailAddress)
        {
            string[] add = address.Split(charSplit);
            if (add.Length > 1)
            {
                if (returnEmailAddress)
                    return add[1].Substring(0, add[1].Length - 1);
                else
                    return add[0].ToString();
            }

            return string.Empty;
        }

        public static bool RefileContacts(Microsoft.Office.Interop.Outlook.MAPIFolder folder, string address)
        {
            bool isContainsContactItem = false;
            Items items = folder.Items;
            if (items.Count > 0)
            {
                Microsoft.Office.Interop.Outlook.Items contactItems =
                  items.Restrict("[MessageClass]='IPM.Contact'");

                Microsoft.Office.Interop.Outlook.ContactItem olContact = (contactItems.Find("[Email1Address] = '" + address + "'") as Microsoft.Office.Interop.Outlook.ContactItem);
                if (olContact != null)
                {
                    olContact.Display(false);

                    isContainsContactItem = true;
                }
                contactItems = null;
            }
            MailUtility.ReleaseComObject(items);

            return isContainsContactItem;
        }

        /// <summary>
        /// 获取默认搜索文件夹
        /// </summary>
        /// <returns></returns>
        public static Folders GetDefaultSearchFolders(MAPIFolder rootFolder)
        {
            Folders olFolders = null;

            if (rootFolder == null)
            {
                try
                {
                    olFolders = ClientUtility.OlNS.DefaultStore.GetSearchFolders();
                }
                catch (System.Exception ex)
                {
                    try
                    {
                        olFolders = ClientUtility.CreateOutlookNameSpaceInstance().DefaultStore.GetSearchFolders();
                    }
                    catch (System.Exception e)
                    {
                        ICP.Framework.CommonLibrary.LogHelper.SaveLog(ICP.Framework.CommonLibrary.Common.CommonHelper.BuildExceptionString(ex));
                        return null;
                    }
                }
            }
            else
            {
                olFolders = rootFolder.Session.DefaultStore.GetSearchFolders();
            }
            return olFolders;
        }


        public static MAPIFolder GetDeleteFolder()
        {
            return GetDefaultFolder(OlDefaultFolders.olFolderDeletedItems);
        }

        public static MAPIFolder GetDefaultInboxFolder()
        {
            return GetDefaultFolder(OlDefaultFolders.olFolderInbox);
        }

        public static MAPIFolder GetDefaultFolder(OlDefaultFolders defaultFolders)
        {
            MAPIFolder olFolder = null;
            try
            {
                olFolder = ClientUtility.OlNS.GetDefaultFolder(defaultFolders);

            }
            catch (System.Exception ex)
            {
                olFolder =
                    ClientUtility.CreateOutlookNameSpaceInstance()
                                 .GetDefaultFolder(defaultFolders);
            }

            return olFolder;
        }

        public static Folders GetFolders(object tag, string searchFolders)
        {
            Folders olFolders = null;
            MAPIFolder olFolder = null;
            if (tag.ToString().Equals(searchFolders))
            {
                olFolders = GetDefaultSearchFolders(null);
            }
            else
            {
                olFolder = GetFolderByEntryID(tag.ToString());
                if (olFolder != null)
                {
                    olFolders = olFolder.Folders;
                    MailUtility.ReleaseComObject(olFolder);
                }
            }

            return olFolders;
        }
        /// <summary>
        /// 是否存在子文件夹
        /// </summary>
        /// <param name="olFolder"></param>
        /// <returns></returns>
        public static bool HasSubFolder(MAPIFolder olFolder)
        {
            bool hasSubFolder = false;
            if (olFolder != null)
            {
                try
                {
                    Folders _oFolders = olFolder.Folders;
                    if (_oFolders.Count > 0)
                        hasSubFolder = true;

                    MailUtility.ReleaseComObject(_oFolders);
                }
                catch
                {
                    hasSubFolder = false;
                }
            }

            return hasSubFolder;
        }

        /// <summary>
        /// 将邮件接收人转换成string字符串，以 ‘;’分割
        /// </summary>
        /// <param name="olRecipients"></param>
        /// <param name="type">类型:发件人为0，接收人为1，CC为2</param>
        /// <param name="nameList">发件人或接收人的名称</param>
        /// <returns></returns>
        public static string ConvertRecipientsToString(Recipients olRecipients, int type, out string nameList)
        {
            StringBuilder strAddress = new StringBuilder();
            StringBuilder strNames = new StringBuilder();
            try
            {
                int count = olRecipients.Count;
                for (int i = 1; i <= count; i++)
                {
                    if (olRecipients[i].Type == type)
                    {
                        string address = !string.IsNullOrEmpty(olRecipients[i].Address) ? olRecipients[i].Address : olRecipients[i].Name;
                        strAddress.Append(string.Format("{0};", address));
                        strNames.Append(string.Format("{0};", olRecipients[i].Name));
                    }
                }
            }
            finally
            {
                MailUtility.ReleaseComObject(olRecipients);
            }


            if (strAddress.Length > 0)
            {
                nameList = strNames.ToString(0, strNames.Length - 1);
                //nameList = names.Aggregate((a, b) => string.Format("{0};{1}", a, b));
                //return addresses.Aggregate((a, b) => string.Format("{0};{1}", a, b));
                return strAddress.ToString(0, strAddress.Length - 1);
            }
            else
            {
                nameList = string.Empty;
                return string.Empty;
            }
        }

        private static string ReplaceHtmlBodyBackgroundImage(string htmlBody, string contentID, ref bool isReplaceSign)
        {
            string body = htmlBody;
            // “background=” 代表背景图片
            if (body.Contains("background="))
            {
                string pattern = string.Format("background=(\")?(cid:)?{0}(\")?", contentID);
                body = Regex.Replace(htmlBody, pattern, "", RegexOptions.IgnoreCase);
                isReplaceSign = true;
            }
            return body;
        }

        /// <summary>
        /// 将htmlbody中图片路径替换
        /// </summary>
        /// <param name="htmlBody"></param>
        /// <returns></returns>
        private static string ReplaceHtmlBodyImagePathWithLocalPath(string contentID, string htmlBody, string fullFilePath, ref bool isReplaceSign)
        {
            if (htmlBody.Contains(contentID))
            {
                string pattern = string.Format("cid:(\")?{0}(\")?", contentID);
                htmlBody = Regex.Replace(htmlBody, pattern, fullFilePath + "\"", RegexOptions.IgnoreCase);
                isReplaceSign = true;
            }
            return htmlBody;
        }
        /// <summary>
        ///拖拽文件夹中是否包含未读邮件
        /// </summary>
        /// <param name="dragDropUnreadMail"></param>
        /// <returns></returns>
        public static bool HasUnreadMail(List<bool> dragDropUnreadMail)
        {
            if (dragDropUnreadMail.Count == 0)
                return false;
            else
            {
                return dragDropUnreadMail.Any(item => item == true);
            }
        }

        /// <summary>
        /// 重命名Outlook 文件夹名称
        /// </summary>
        /// <param name="olFolder"></param>
        /// <param name="newName"></param>
        public static void RenameMAPIFolder(MAPIFolder olFolder, string newName)
        {
            if (olFolder != null)
            {
                olFolder.Name = newName;
                olFolder.CopyTo(olFolder);
                FolderWrapper.Wrapper(olFolder, true);
            }
        }
        /// <summary>
        /// 设置邮件中心标题
        /// </summary>
        /// <param name="control"></param>
        /// <param name="text"></param>
        /// <param name="setMainFormText"></param>
        public static void SetMailCenterText(Control control, string text, bool setMainFormText)
        {
            if (control.InvokeRequired)
                control.Invoke((System.Action)(() => SetMainFormText(control, text, setMainFormText)));
            
            else
                SetMainFormText(control, text, setMainFormText);            
        }

        private static void SetMainFormText(Control control, string text, bool setMainFormText)
        {
            control.Parent.TopLevelControl.TopLevelControl.Text = text;
            if (setMainFormText)
            {
                //control.Parent.TopLevelControl.Controls[0].Controls[0].Text = LocalData.IsEnglish ? "Mail" : "邮件";
                control.FindForm().Text = LocalData.IsEnglish ? "Mail" : "邮件";

            }
        }
    }
}
