using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;
using Microsoft.Practices.CompositeUI;

namespace ICP.OA.UI.FaxManage
{
    public partial class frmSendFax : DevExpress.XtraEditors.XtraForm
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IClientMessageService ClientMessageService
        {
            get
            {
                return ServiceClient.GetClientService<IClientMessageService>();
            }
        }
       
        #endregion

        #region 本地属性
        private delegate void AddImageListBoxItemDelegate(ImageListBoxItem item);
        System.Windows.Forms.Timer tipTimer = new System.Windows.Forms.Timer();
        ICP.Message.ServiceInterface.Message dataSource = new ICP.Message.ServiceInterface.Message();
        MessagePriority _priority;
        MessagePriority Priority
        {
            get
            {
                if (barHigh.Checked)
                    _priority = MessagePriority.High;
                else if (barNormal.Checked)
                    _priority = MessagePriority.Normal;
                else
                    _priority = MessagePriority.Low;

                return _priority;
            }
        }
        #endregion

        #region 初始化
        private void AddImageListBoxItem(ImageListBoxItem item)
        {
            if (imageLBAttachment.InvokeRequired)
            {
                AddImageListBoxItemDelegate addDelegate = delegate(ImageListBoxItem listItem)
                {
                    InnerAddImageListBoxItem(item);

                };
                imageLBAttachment.Invoke(addDelegate, new object[] { item });

            }
            else
            {
                InnerAddImageListBoxItem(item);
            }

        }
        private void InnerAddImageListBoxItem(ImageListBoxItem item)
        {
            imageLBAttachment.Items.Add(item);
            groupBox1.Height = GetGruopBaseHeight();
            imageLBAttachment.Height = GetImageLBHeight();
        }

        public frmSendFax()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.dataSource = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            };
            if (LocalData.IsEnglish == false && !LocalData.IsDesignMode) SetCnText();
        }

        private void SetCnText()
        {
            this.btnContent.Text = "传真";
            lblCC.Text = "抄送";
            labSubject.Text = "主题";
            lblTo.Text = "收件人";
            this.barItemSaveDraft.Caption = "保存(&V)";
            barPriority.Caption = "普通优先级";
            barSend.Caption = "发送(&S)";
            barClose.Caption = "关闭(&C)";
            barAttachment.Caption = "附件(&A)";
            barHigh.Caption = "高优先级(&H)";
            barNormal.Caption = "普通优先级(&N)";
            barLow.Caption = "低优先级(&L)";
            barCheck.Caption = "检查(&K)";

            barAddAttachment.Caption = "增加附件(&A)";
            barRemoveAchment.Caption = "删除(&D)";
            barOpenFile.Caption = "打开文件(&O)";
            this.Text = "发送传真";
        }

        #endregion

        #region 接口


        public void BindData()
        {
            if (dataSource.Id == null)
                return;
            txtMailTo.Text = dataSource.SendTo;
            txtCC.Text = dataSource.CC;

            txtSubject.Text = dataSource.Subject;
            rtxtContent.HtmlText = dataSource.Body;

            if (dataSource.HasAttachment)
            {
                foreach (var attachment in dataSource.Attachments)
                {
                    imageListAttachment.Images.Add(GetSystemIcon.GetIconByFileType(attachment.DisplayName, false));

                    try
                    {

                        Icon myIcon = GetSystemIcon.GetIconByFileName(attachment.ClientPath);
                        if (myIcon != null) imageListAttachment.Images.Add(myIcon);
                        else imageListAttachment.Images.Add(new Bitmap(16, 16));
                    }
                    catch
                    {

                        imageListAttachment.Images.Add(new Bitmap(16, 16));
                    }
                    ImageListBoxItem item = new ImageListBoxItem();

                    string fileSize = Utility.GetFileSizeString(attachment.Size);
                    item.Value = attachment.DisplayName + " (" + fileSize + ")";
                    item.ImageIndex = imageListAttachment.Images.Count - 1;
                    InnerAddImageListBoxItem(item);
                }
            }

        }
        /// <summary>
        /// 设置数据源
        /// </summary>
        /// <param name="mailInfo"></param>
        public void SetSource(ICP.Message.ServiceInterface.Message messageInfo)
        {
            dataSource = messageInfo;

            BindData();
        }
        private void InnerBindData()
        {

        }

        #endregion

        #region 优先级

        private void barHigh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetHighPriority();
        }
        private void barNormal_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetNormalPriority();
        }
        private void barLow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetLowPriority();
        }

        private void SetLowPriority()
        {
            barHigh.Checked = false;
            barNormal.Checked = false;
            barLow.Checked = true;
            barPriority.Caption = barLow.Caption;
            barPriority.Glyph = barLow.Glyph;
        }
        private void SetNormalPriority()
        {
            barHigh.Checked = false;
            barNormal.Checked = true;
            barLow.Checked = false;
            barPriority.Caption = barNormal.Caption;
            barPriority.Glyph = barNormal.Glyph;
        }
        private void SetHighPriority()
        {
            barHigh.Checked = true;
            barNormal.Checked = false;
            barLow.Checked = false;
            barPriority.Caption = barHigh.Caption;
            barPriority.Glyph = barHigh.Glyph;
        }

        private void barPriority_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (barNormal.Checked)
                SetHighPriority();
            else if (barHigh.Checked)
                SetLowPriority();
            else
                SetNormalPriority();


        }

        #endregion

        #region 附件

        private void barAttachment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddAttachment();
        }
        private void barAddAchment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddAttachment();
        }

        private void InnerAddAttachment(string[] paths)
        {

            if (paths == null || paths.Length <= 0)
                return;
            #region add
            for (int i = 0; i < paths.Length; i++)
            {
                FileInfo fi = new FileInfo(paths[i]);
                if (dataSource.Attachments.Find(delegate(AttachmentContent fItem) { return fItem.ClientPath == paths[i]; }) != null) continue;

                string fileSize = string.Empty;
                #region 文件大小控制
                if (fi.Length > 10000000)
                {
                    if (LocalData.IsEnglish)
                        DevExpress.XtraEditors.XtraMessageBox.Show("Add Attachment Faild:" + fi.Name + "''s Length is overstep 10M.");
                    else
                        DevExpress.XtraEditors.XtraMessageBox.Show("添加附件失败:" + fi.Name + "的大小超出10M");
                    continue;
                }

                fileSize = Utility.GetFileSizeString(fi.Length);

                #endregion

                #region Icon

                try
                {

                    Icon myIcon = GetSystemIcon.GetIconByFileName(fi.FullName);
                    if (myIcon != null) imageListAttachment.Images.Add(myIcon);

                    else imageListAttachment.Images.Add(new Bitmap(16, 16));
                }
                catch { imageListAttachment.Images.Add(new Bitmap(16, 16)); }

                #endregion
                AttachmentContent content = new AttachmentContent();
                content.Id = Guid.NewGuid();
                content.ClientPath = paths[i];
                content.Name = content.DisplayName = Path.GetFileName(paths[i]);
                content.Size = fi.Length;
                dataSource.Attachments.Add(content);

                ImageListBoxItem item = new ImageListBoxItem();
                item.Value = fi.Name + " (" + fileSize + ")";
                item.ImageIndex = imageListAttachment.Images.Count - 1;
                AddImageListBoxItem(item);
            }
            #endregion
        }
        private void AddAttachment()
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Multiselect = true;
            of.ValidateNames = true;
            of.CheckFileExists = true;
            of.CheckPathExists = true;
            of.Filter = ICP.Business.Common.UI.CommonUIUtility.GetFilterString();
            if (of.ShowDialog() != DialogResult.OK)
                return;
            string[] attachmentFilePaths = of.FileNames;
            if (attachmentFilePaths == null || attachmentFilePaths.Length <= 0)
                return;
            InnerAddAttachment(attachmentFilePaths);
        }

        private void barRemoveAchment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RemoveAttachment();
        }
        private void imageLBAttachment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) RemoveAttachment();
        }
        private void RemoveAttachment()
        {
            if (imageLBAttachment.SelectedItems == null) return;
            List<ImageListBoxItem> indexs = new List<ImageListBoxItem>();
            foreach (ImageListBoxItem item in imageLBAttachment.SelectedItems)
            {
                indexs.Add(item);
            }

            int imageCount = imageLBAttachment.SelectedItems.Count;

            for (int j = 0; j < imageCount; j++)
            {
                imageListAttachment.Images.RemoveAt(indexs[j].ImageIndex);
                dataSource.Attachments.RemoveAt(j);
                imageLBAttachment.Items.Remove(indexs[j]);
            }

            groupBox1.Height = GetGruopBaseHeight();
            imageLBAttachment.Height = GetImageLBHeight();
            int itemCount = this.imageLBAttachment.Items.Count;
            for (int i = 0; i < itemCount; i++)
            {
                this.imageLBAttachment.Items[i].ImageIndex = i;
            }
        }

        private void barOpenFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenAttachment();
        }

        private void OpenAttachment()
        {
            int selectedIndex = imageLBAttachment.SelectedIndex;
            if (selectedIndex < 0) return;
            System.Diagnostics.Process.Start(dataSource.Attachments[selectedIndex].ClientPath);
        }

        private void imageLBAttachment_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PreviewAttachment();
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (imageLBAttachment.SelectedIndex < 0 || imageLBAttachment.ItemCount == 0)
                    barRemoveAchment.Enabled = barOpenFile.Enabled = false;
                else
                    barRemoveAchment.Enabled = barOpenFile.Enabled = true;

                popupMenuAttachment.ShowPopup(MousePosition);
            }
        }

        private void PreviewAttachment()
        {
            int selectedIndex = this.imageLBAttachment.SelectedIndex;
            AttachmentContent content = this.dataSource.Attachments[selectedIndex];
            
            BindPreviewControlData(content);
           // SwitchControl(true);

        }

        private void BindPreviewControlData(AttachmentContent content)
        {
            //this.ucPreviewControl.SuspendLayout();
            //this.ucPreviewControl.Load(content.ClientPath);
            //this.ucPreviewControl.ResumeLayout();
            ServiceClient.FilePreviewService.Preview(content.ClientPath, PointToScreen(this.groupBox2.Location), this.groupBox2.Size, true);
        }
        private void btnContent_Click(object sender, EventArgs e)
        {
            SwitchControl(false);
        }
        private void SwitchControl(bool isShowPreviewControl)
        {
          
            this.rtxtContent.Visible = this.barDockControlRcMain.Visible = this.bar3.Visible = !isShowPreviewControl;


        }

  

        public int AttachmentsCount { get { return dataSource.Attachments.Count; } }

        int GetGruopBaseHeight()
        {
            int noAttachment = 85, haveAttachment = 130;
            if (AttachmentsCount == 0)
                return noAttachment;
            else
            {
                int i = AttachmentsCount > 4 ? 4 : AttachmentsCount;
                return haveAttachment + (i * 12);
            }
        }

        int GetImageLBHeight()
        {
            int countAdd = 19;
            if (AttachmentsCount <= 2)
                return 2 * countAdd;
            else
            {
                int count = (AttachmentsCount > 4 ? 4 : AttachmentsCount);
                return count * countAdd;
            }
        }

        #endregion

        #region Send And Check

        private void barSend_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Send(MessageState.Sending);
        }

        private void Send(MessageState state)
        {
            if (this.ValidateData() == false) return;
            using (new CursorHelper(Cursors.WaitCursor))
            {

                try
                {
                    if (dataSource == null)
                        dataSource = new ICP.Message.ServiceInterface.Message();
                    #region 附件
                    GetAttachments();

                    #endregion
                    dataSource.BodyFormat = BodyFormat.olFormatHTML;// rtxtContent.HtmlText.Contains("<html>") ? BodyFormat.olFormatHTML : BodyFormat.olFormatPlain;
                    dataSource.Body = rtxtContent.HtmlText;
                    if (!string.IsNullOrEmpty(txtMailTo.Text))
                    {
                        dataSource.SendTo = txtMailTo.Text.Trim();
                    }
                    dataSource.CC = txtCC.Text;
                    dataSource.SendFrom = LocalData.UserInfo.EmailAddress;
                    dataSource.Priority = this.Priority;
                    dataSource.State = state;
                    dataSource.Subject = this.txtSubject.Text;
                    dataSource.Type = MessageType.Fax;
                    dataSource.Way = MessageWay.Send;


                    //dataSource.Id = Guid.NewGuid();
                    if (state == MessageState.Sending)
                    {
                        dataSource.CreateBy = LocalData.UserInfo.LoginID;
                        dataSource.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                        SingleResult result = ClientMessageService.Send(dataSource);
                        if (result != null)
                        {
                            dataSource.MessageId = result.GetValue<string>("MessageID");
                            dataSource.SendFrom = result.GetValue<String>("SendFrom");
                             
                        }
                    }

                    this.DialogResult = DialogResult.OK;


                    this.Close();
                }
                catch (Exception ex)
                {
                    this.ShowTip((LocalData.IsEnglish ? "Send Failed." : "发送失败.") + ex.Message);
                }
            }
        }




        void GetAttachments()
        {
            List<AttachmentContent> contents = dataSource.Attachments;
            int count = contents.Count;
            dataSource.HasAttachment = count > 0;
            for (int i = 0; i < count; i++)
            {
                if (contents[i].Content == null)
                {
                    contents[i].Content = IOHelper.ReadFileContentFromDisk(contents[i].ClientPath);
                }
            }
        }

        private void barCheck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.ValidateData() == true)
                this.ShowTip(LocalData.IsEnglish ? "Check Pass." : "检查通过.");
        }

        bool ValidateData()
        {
            this.Validate();
            bool isSrcc = true;
            if (string.IsNullOrEmpty(txtMailTo.Text.Trim()))
            {
                dxErrorProvider1.SetError(txtMailTo, LocalData.IsEnglish ? "MailTo Must Input." : "必须输入收件人.");
                isSrcc = false;
            }
            else if (!(isSrcc = ValidateFaxNo(txtMailTo.Text.Trim())))
                dxErrorProvider1.SetError(txtMailTo, LocalData.IsEnglish ? "The fax No. is invalid" : "传真号格式不正确.");
            else
                dxErrorProvider1.SetError(txtMailTo, string.Empty);
            string cc = txtCC.Text.Trim();
            if (!string.IsNullOrEmpty(cc))
            {
                if (!ValidateFaxNo(cc))
                {
                    dxErrorProvider2.SetError(txtCC, LocalData.IsEnglish ? "The fax No. is invalid" : "传真号格式不正确.");
                    isSrcc = false;
                }
            }

            return isSrcc;
        }




        /// <summary>
        /// 验证传真号码
        /// </summary>
        /// <param name="faxNo"></param>
        /// <returns></returns>
        protected bool ValidateFaxNo(string faxNo)
        {
            if (string.IsNullOrEmpty(faxNo.Trim()))
                return false;
            string faxNos = faxNo.Trim();
            string[] faxArray = faxNos.Split(';');
            for (int i = 0; i < faxArray.Length; i++)
            {
                if (!this._ValidFaxNo(faxArray[i]))
                {
                    return false;
                }

            }
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="faxNo"></param>
        /// <returns></returns>
        private bool _ValidFaxNo(string faxNo)
        {
            string patten = @"^[0-9\-]{7,20}$";

            if (Regex.IsMatch(faxNo, patten))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        void ShowTip(string tip)
        {
            if (string.IsNullOrEmpty(tip)) return;

            tipTimer.Stop();
            labTip.Text = tip;
            labTip.Visible = true;
            tipTimer.Start();
        }

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void labTip_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DevExpress.XtraEditors.XtraMessageBox.Show(labTip.Text);
        }

        #endregion

        #region RichEdit

        private void barFont_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (barDockControlRichEdit.Visible)
                barDockControlRichEdit.Hide();
            else
                barDockControlRichEdit.Show();
        }

        #endregion

        private void barItemSaveDraft_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Send(MessageState.Draft);

        }

    }


}
