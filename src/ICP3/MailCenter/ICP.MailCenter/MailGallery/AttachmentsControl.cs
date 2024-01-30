using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Collections.Specialized;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Practices.CompositeUI;
using ICP.MailCenter.ServiceInterface;
using ICP.Message.ServiceInterface;
using Exception = System.Exception;

namespace ICP.MailCenter.UI
{

    /// <summary>
    /// 附件图标和附件名类
    /// </summary>
    /// 
    public partial class AttachmentsControl : System.Windows.Forms.UserControl
    {
        #region const
        List<String> lstSelectedAllAttachment = null;
        public ContextMenu fileMenu = null;

        private Microsoft.Practices.CompositeUI.Collections.ManagedObjectCollection<object> _SmartParts;
        public Microsoft.Practices.CompositeUI.Collections.ManagedObjectCollection<object> SmartParts
        {
            get
            {
                if (_SmartParts == null)
                    _SmartParts = ServiceClient.GetClientService<WorkItem>().SmartParts;
                return _SmartParts;
            }
        }
        /// <summary>
        /// 邮件明细面板
        /// </summary>
        public EmailDetailPart DetailPart
        {
            get
            {
                return SmartParts.Get<EmailDetailPart>(MailCenterWorkSpaceConstants.EmailDetailPart);
            }
        }


        /// <summary>
        ///附件的的名称
        /// </summary>
        public string _fileName
        {
            get;
            set;
        }
        /// <summary>
        /// 附件显示名称
        /// </summary>
        public string _displayName { get; set; }
        /// <summary>
        /// 文件完整路径
        /// </summary>
        public string _fileFullPath { get; set; }

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public AttachmentsControl()
        {
            InitializeComponent();
            this.AutoSize = true;
            this.Disposed += delegate { DisposedComponent(); };
        }

        private void DisposedComponent()
        {
            _SmartParts = null;
            lstSelectedAllAttachment = null;
            fileMenu = null;
        }


        #region Method
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="img">图标</param>
        /// <param name="attFullName">名称及大小字符串</param>
        /// <param name="fullPath">附件完整路径</param>
        public void Init(string fileName, string fileExtension, string fileSizeString, string displayName)
        {
            this.picAtt.Image = ClientUtility.GetAttachmentIcon(fileName, fileExtension);
            this.lnkAtt.Text = string.Format("{0}({1})", fileName, fileSizeString);
            this._fileName = fileName;
            this._displayName = displayName;
            this.Name = displayName;
        }
        #endregion

        #region Click
        /// <summary>
        /// 双击打开文件
        /// </summary>
        private void lnkAtt_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                ServiceClient.GetClientService<IOutLookService>().Open(GetFileFullPath());
            }
        }

        /// <summary>
        /// file context menu
        /// </summary>
        public void InitFileMenu()
        {
            fileMenu = new ContextMenu();
            bool isEnglish = LocalData.IsEnglish;

            fileMenu.MenuItems.Add(CreateMenuItem((isEnglish ? "Open(&O)" : "打开(&O)"), OnMenuItemClick, AttachmentAction.Open));
            fileMenu.MenuItems.Add(CreateMenuItem((isEnglish ? "Save As(&S).." : "另存为(&S).."), OnMenuItemClick, AttachmentAction.SaveAs));
            fileMenu.MenuItems.Add(CreateMenuItem((isEnglish ? "Copy(&C)" : "复制(&C)"), OnMenuItemClick, AttachmentAction.Copy));
            fileMenu.MenuItems.Add(CreateMenuItem((isEnglish ? "Select All(&L)" : "全选(&L)"), OnMenuItemClick, AttachmentAction.SelectAll));
            fileMenu.MenuItems.Add(CreateMenuItem((isEnglish ? "Print(&P)" : "打印(&P)"), OnMenuItemClick, AttachmentAction.Print));
            this.AutoSize = true;
        }
        private MenuItem CreateMenuItem(string text, EventHandler clickHander, AttachmentAction action)
        {
            MenuItem menuItem = new MenuItem();
            menuItem.Text = text;
            menuItem.Click += clickHander;
            menuItem.Tag = action;
            return menuItem;
        }
        /// <summary>
        /// 右键单击附件名称显示上下文菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lnkAtt_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                LinkLabel lnkAtt = sender as LinkLabel;
                AttachmentsControl attachmentsControl = lnkAtt.Parent as AttachmentsControl;
                attachmentsControl.InitFileMenu();
                attachmentsControl.lnkAtt.ContextMenu = attachmentsControl.fileMenu;
                attachmentsControl.fileMenu.Show(lnkAtt, e.Location);

                attachmentsControl = null;
                lnkAtt = null;
            }
        }
        /// <summary>
        /// 选择右键菜单中的项
        /// </summary>
        void OnMenuItemClick(object sender, EventArgs e)
        {
            MenuItem menu = sender as MenuItem;
            AttachmentAction action = (AttachmentAction)menu.Tag;
            if (action == AttachmentAction.Open)
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    ServiceClient.GetClientService<IOutLookService>().Open(this.GetFileFullPath());
                }
            }

            else if (action == AttachmentAction.SaveAs)
            {
                SaveFile(Path.GetExtension(GetFileFullPath()));
            }

            else if (action == AttachmentAction.Copy)
            {
                StringCollection paths = new StringCollection();
                if (DetailPart.IsSelectAllAttachments)
                {
                    paths.AddRange(GetAttachmentNames());
                }
                else
                {
                    paths.Add(this.GetFileFullPath());
                }

                Clipboard.SetFileDropList(paths);
            }
            else if (action == AttachmentAction.SelectAll)
            {
                IDataObject data = new DataObject(DataFormats.FileDrop, GetAllFileFullPath());
                Clipboard.SetDataObject(data);
                DoDragDrop(data, DragDropEffects.Copy | DragDropEffects.Move);
                this.DetailPart.SelectAllAttachmentControls();

            }
            else if (action == AttachmentAction.Print)
            {
                System.Windows.Forms.Application.DoEvents();
                MailUtility.PrintFile(this.GetFileFullPath());
            }
        }

        private string[] GetAttachmentNames()
        {
            return (from item in ClientProperties.Attachments select item.Name).ToArray();
        }

        /// <summary>
        /// copy and save file
        /// </summary>
        /// <param name="fileName"></param>
        public void SaveFile(string extension)
        {

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = Path.GetFileName(this.GetFileFullPath());
            dialog.Filter = LocalData.IsEnglish ? string.Format("ALL Files(*{0})|{0}", extension) : string.Format("所有文件(*{0})|{0}", extension);
            dialog.RestoreDirectory = true;

            try
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(this.GetFileFullPath(), dialog.FileName, true);  //文件另存为  
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Save failure." : "保存失败.") + ex.Message);
            }
            finally
            {
                dialog.Dispose();
            }
        }


        private void SetSelectedAttachmentBackColor()
        {
            DetailPart.RestoreBackColor();
            this.BackColor = Color.LightSteelBlue;
        }

        private string[] GetSelectedAttachments(object sender)
        {
            string[] files = null;
            LinkLabel att = sender as LinkLabel;
            if (att.BackColor == Color.LightSteelBlue)
            {
                string[] selectedAttachments = DetailPart.SelectedAttachments();
                if (selectedAttachments != null && selectedAttachments.Length > 0)
                    files = selectedAttachments;
            }
            else
            {
                files = new string[1] { GetFileFullPath() };
            }

            return files;
        }
        #endregion


        private void lnkAtt_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks == 1)
                {
                    MailListPresenter.CopyText(this._displayName);
                    String[] filePath = GetSelectedAttachments(sender);
                    DataObject data = new DataObject(DataFormats.FileDrop, filePath);
                    //also add the selection as textdata
                    data.SetData(DataFormats.StringFormat, filePath);
                    //Do DragDrop
                    DoDragDrop(data, DragDropEffects.Copy | DragDropEffects.Move);
                }
                //else
                //{
                //    if (Path.GetExtension(filePath) == ".msg")
                //        MailListPresenter.OpenMsgFile(filePath);
                //    else
                //        MailUtility.StartFileApplication(filePath);
                //}
            }
        }



        private void lnkAtt_DragLeave(object sender, EventArgs e)
        {
            string[] files = null;
            LinkLabel att = sender as LinkLabel;
            if (att.BackColor == Color.LightSteelBlue)
            {
                string[] selectedAttachments = DetailPart.SelectedAttachments();
                if (selectedAttachments != null && selectedAttachments.Length > 0)
                    files = selectedAttachments;
            }
            else
            {
                files = new string[1] { GetFileFullPath() };
            }
            //DoDragDrop(new DataObject(DataFormats.FileDrop, files), DragDropEffects.Copy | DragDropEffects.Move);
            //ICP.Framework.CommonLibrary.Logger.Log.Error("DragLeave|" + files.Length);
        }

        private void lnkAtt_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private string[] GetAllFileFullPath()
        {
            return ClientUtility.GetRealAttachmentList().Select(item => item.PreviewPath).ToArray();
        }

        public string GetFileFullPath()
        {
            if (string.IsNullOrEmpty(_fileFullPath))
            {
                AttachmentContent attachmentInfo =
                    ClientProperties.Attachments.Find(
                        item => item.Name.Equals(_fileName) && item.DisplayName.Equals(_displayName));
                //将Attachment保存到本地
                if (attachmentInfo != null)
                {
                    if (attachmentInfo.OLAttachment != null)
                    {
                        _fileFullPath = MailListPresenter.GetFileFullPath(attachmentInfo.Name);
                        MailListPresenter.SaveAsLocalFile(attachmentInfo.OLAttachment as Attachment, _fileFullPath);
                        attachmentInfo.PreviewPath = _fileFullPath;
                        return _fileFullPath;
                    }
                    else
                        return attachmentInfo.PreviewPath;
                }
                else
                    return _fileName;
            }
            else
                return _fileFullPath;
        }
    }
    /// <summary>
    /// 操作附件动作类型
    /// </summary>
    public enum AttachmentAction
    {
        Open,
        SaveAs,
        Copy,
        SelectAll,
        Print
    }

}
