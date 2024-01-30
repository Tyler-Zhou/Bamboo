using System;
using System.Drawing;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using System.IO;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;
using System.Collections.Generic;

namespace ICP.Sys.UI.UserManage
{
    public partial class UserInfoEditPart : BasePart
    {
        #region 服务注入

        public IUserClientService UserClientService
        {
            get
            {
                return ServiceClient.GetClientService<IUserClientService>();
            }
        }

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        public IClientMessageService ClientMessageService
        {
            get
            {
                return ServiceClient.GetClientService<IClientMessageService>();
            }
        }

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        List<UserMailAccountList> accountList = null;

        #endregion
        /// <summary>
        /// 自定义控制字段
        /// </summary>
        public static class UserInfoCustom
        {
            /// <summary>
            /// 产看信息时判断是否是用户本人
            /// </summary>
            public static Guid UserID
            { get; set; }
            /// <summary>
            /// 强制更改密码标识
            /// </summary>
            public static bool PoweModifyPassword
            {
                get;
                set;
            }
            /// <summary>
            /// 用于验证修改密码时判断是否一致
            /// </summary>
            public static string CurrentPassword
            {
                get;
                set;
            }
        }
        #region init
        /// <summary>
        /// 
        /// </summary>
        public UserInfoEditPart()
        {
            try
            {
                InitializeComponent();

                this.Disposed += delegate
                {
                    this.dxErrorProvider1.DataSource = null;
                    this.bindingSource1.DataSource = null;
                    this.bindingSource1.Dispose();
                    UserInfoCustom.PoweModifyPassword = false;
                    if (Workitem != null)
                    {
                        Workitem.Items.Remove(this);
                        Workitem = null;
                    }
                };
                if (!DesignMode)
                {
                    InitMessage();
                    SetCNText();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void UserInfoEditPart_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (UserInfoCustom.PoweModifyPassword)
            {
                string Text = string.Format(LocalData.IsEnglish ?
                    "You have to finish setup new password first, because the password is expired" :
                    "不能退出，因为密码已过期，您必须先完成修改密码。");
                string Tip = string.Format(LocalData.IsEnglish ? "Tip" : "提示");
                MessageBox.Show(Text, Tip, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
        }
        private void SetCNText()
        {
            if (!LocalData.IsEnglish)
            {
                this.tabPageEmail.Text = "邮箱密码";
                labEmail.Text = "邮箱地址";
                lblOldMailPassword.Text = "密码";
                this.btnTest.Text = "发送测试邮件";
                lblEmailAddressBlankTip.Text = "如果您的邮箱账号为空，请联系系统管理员为您分配邮箱账号。";
                btnSaveEmail.Text = "保存";
                this.btnSaveEmail.Left = this.txtOldEmailPassword.Right - this.btnSaveEmail.Width;

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                lbNewPassword.Visible = false;
                lbOldPassword.Visible = false;
                lbConfirmPassword.Visible = false;
                //不显示生日及地址栏位
                labAddress.Visible = false;
                txtAddress.Visible = false;
                labBirthday.Visible = false;
                dteBirthday.Visible = false;

                accountList = UserService.GetUserMailAccountList(new Guid[] { LocalData.UserInfo.LoginID });
                if (accountList != null && accountList.Count > 0)
                {
                    this.cmbEmail.Properties.Items.Clear();
                    foreach (UserMailAccountList account in accountList)
                    {
                        this.cmbEmail.Properties.Items.Add(account.Email);
                    }
                    this.cmbEmail.SelectedIndex = 0;
                }

                this.cmbType.Properties.Items.Clear();
                this.cmbType.Properties.Items.Add("催放单签名");
                this.cmbType.SelectedIndex = 0;

                this.FindForm().FormClosing += UserInfoEditPart_FormClosing;

                UserDetailInfo info = UserService.GetUserDetailInfo(UserInfoCustom.UserID);

                if (info == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("请行联系管理员设置部门、职位信息");
                    return;
                }
                //LocalData.UserInfo.LoginID
                if (UserInfoCustom.UserID == null)
                    return;
                if (UserInfoCustom.UserID != LocalData.UserInfo.LoginID)
                {
                    btnSave.Enabled = false;
                    btnUpLoadPhoto.Enabled = false;
                    btnSavePassword.Enabled = false;
                    btnTest.Enabled = false;
                    btnSaveEmail.Enabled = false;
                }
                bindingSource1.DataSource = info;

                byte[] content = null;
                try
                {
                    content = UserClientService.DownloadUserPhoto(info.ID);
                }
                catch { }

                if (content != null && content.Length > 0)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(content);
                    Image image = Image.FromStream(ms);
                    if (image != null) { pictureEdit1.Image = image; }
                }

                if (info.Gender == GenderType.Female) rdoGender.SelectedIndex = 0;
                else rdoGender.SelectedIndex = 1;

                //设置修改密码提示
                string strChinese = @"
    设置密码时请注意：
       1:密码长度不能小于6位
       2:密码不能设置为连续的数字或字母，如123456、abcdef、123abc
       3:尽量设置为数字、字母及符号的混合密码";
                string strEnglish = @"
    Note that when you set a password:
        1:Password length can not be less than six
        2:The password can not be set to consecutive numbers or letters,For example 123456、abcdef、123abc
        3:Try to set numbers, letters and symbols mixed password";
                lbPasswordTips.Text = LocalData.IsEnglish ? strEnglish : strChinese;

                //强制更改密码
                if (UserInfoCustom.PoweModifyPassword)
                {
                    btnSave.Enabled = false;
                    btnUpLoadPhoto.Enabled = false;
                    btnTest.Enabled = false;
                    btnSaveEmail.Enabled = false;
                    //修改密码界面
                    xtraTabControl1.SelectedTabPage = xtraTabControl1.TabPages[1];
                    xtraTabControl1.TabPages.Remove(xtraTabPage1);
                    xtraTabControl1.TabPages.Remove(tabPageEmail);
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }


        }
        private void InitMessage()
        {

            this.RegisterMessage("SaveSuccessfully", "Save Successfully.");
            this.RegisterMessage("SetPasswordSuccessfully", "Set Password Successfully.");

            this.RegisterMessage("AttachmentLength", "UpLoad Image Faild:{0}'s Length is overstep 10M.");
            this.RegisterMessage("FileFormatError", "File Format Error.{0}");

            this.RegisterMessage("ValidatePassword", "Must input.");
            this.RegisterMessage("ValidatePasswordLength", "Password Length can not less than 6");
            this.RegisterMessage("ValidatePasswordInconsistent", "Inconsistent with the password twice.");


        }

        #endregion

        #region 工作流

        #region Cancel
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        #endregion


        #region 上传文件

        string _FileFilter = "Image Files(*.BMP;*.JPG;*.JPEG;*.PNG;*.GIF)|*.BMP;*.JPG;*.JPEG;*.PNG;*.GIF";
        bool isChangedImage = false;

        private void btnUpLoadPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = _FileFilter;
            if (of.ShowDialog() != DialogResult.OK || of.FileNames == null || of.FileNames.Length <= 0) return;

            FileInfo fi = new FileInfo(of.FileName);

            #region 文件格式
            Image image = null;

            try
            {
                image = Image.FromFile(of.FileName);
            }
            catch
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(string.Format(NativeLanguageService.GetText(this, "FileFormatError"), fi.Name));
            }

            #endregion

            if (image == null) return;

            Image newImage = MakeThumbnail(image, pictureEdit1.Width, pictureEdit1.Height);

            isChangedImage = true;
            pictureEdit1.Image = newImage;

        }

        #region  生成缩略图
        ///<summary> 
        /// 生成缩略图 
        /// </summary> 
        /// <param name="originalImagePath">源图路径（物理路径）</param> 
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param> 
        /// <param name="width">缩略图宽度</param> 
        /// <param name="height">缩略图高度</param> 
        /// <param name="mode">生成缩略图的方式</param>     
        Image MakeThumbnail(Image originalImage, int towidth, int toheight)
        {

            int ow = originalImage.Width;
            int oh = originalImage.Height;

            #region 返回原始图的副本
            if (ow <= towidth && oh <= toheight)
            {
                try
                {
                    return new Bitmap(originalImage) as Image;
                }
                catch (Exception ex) { return null; }
            }
            #endregion


            bool isCutByWidth = false;

            if ((double)originalImage.Width == (double)originalImage.Height)
            {
                if (towidth < toheight) isCutByWidth = true;
            }
            else if ((double)originalImage.Width > (double)originalImage.Height)
                isCutByWidth = true;

            //H:W
            double originalProportion = (double)originalImage.Height / (double)originalImage.Width; ;

            if (isCutByWidth)
                toheight = (int)((double)towidth * originalProportion);
            else
                towidth = (int)((double)toheight / originalProportion);


            //新建一个bmp图片 
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
            //新建一个画板 
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法 
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充 
            g.Clear(Color.Transparent);

            // 在指定位置并且按指定大小绘制原图片的指定部分 
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
             new Rectangle(0, 0, ow, oh),
             GraphicsUnit.Pixel);

            try
            {

                g.Dispose();
                return bitmap as Image;

            }
            catch (System.Exception e)
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
                return null;
            }
        }
        #endregion

        #endregion

        #region Save

        private void btnSave_Click(object sender, EventArgs e)
        {
            UserDetailInfo info = this.bindingSource1.DataSource as UserDetailInfo;
            if (rdoGender.SelectedIndex == 0)
                info.Gender = GenderType.Female;
            else
                info.Gender = GenderType.Male;

            if (this.ValidateData(info) == false) return;

            try
            {
                byte[] content = null;

                if (isChangedImage)
                {
                    Image image = pictureEdit1.Image;
                    if (image != null)
                    {
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        content = ms.ToArray();
                    }
                }
                SingleResultData result = UserClientService.SaveUserDetailInfo
                    (info.ID, info.CName, info.EName, info.Gender
                    , info.Tel, info.Fax, info.Mobile
                    , info.Address, info.Remark, info.Birthday
                    , content
                    , LocalData.UserInfo.LoginID
                    , info.UpdateDate);

                info.UpdateDate = result.UpdateDate;

                info.EndEdit();
                info.CancelEdit();
                info.BeginEdit();
                isChangedImage = false;

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), NativeLanguageService.GetText(this, "SaveSuccessfully"));

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }

        private bool ValidateData(UserDetailInfo info)
        {
            if (info == null || info.Validate() == false) return false;

            return true;
        }

        #endregion

        #region Password

        private void btnSavePassword_Click(object sender, EventArgs e)
        {
            if (this.ValidatePassWord() == false) return;

            try
            {
                //保存密码按钮不可用
                btnSavePassword.Enabled = false;

                UserDetailInfo info = this.bindingSource1.DataSource as UserDetailInfo;
                string oldPassword = txtOldPassword.Text;
                string newPassword = txtConfirmPassword.Text;


                SingleResultData result = UserService.ChangedUserPassword(info.ID, oldPassword, newPassword, info.UpdateDate);
                info.UpdateDate = result.UpdateDate;
                LocalData.UserInfo.Password = newPassword;

                //强制更改密码时不提示保存成功！
                if (!UserInfoCustom.PoweModifyPassword)
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), NativeLanguageService.GetText(this, "SetPasswordSuccessfully"));

                //密码修改成功后关闭界面
                if (UserInfoCustom.PoweModifyPassword)
                {
                    UserInfoCustom.PoweModifyPassword = false;
                    this.FindForm().Close();
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                btnSavePassword.Enabled = true;
            }

        }

        private bool ValidatePassWord()
        {
            bool isSrcc = true;
            bool isEnglish = LocalData.IsEnglish;
            dxErrorProvider1.ClearErrors();

            lbNewPassword.Visible = false;
            lbOldPassword.Visible = false;
            lbConfirmPassword.Visible = false;

            //与登录密码验证
            if (UserInfoCustom.CurrentPassword != null)
                if (txtOldPassword.Text != UserInfoCustom.CurrentPassword)
                {
                    lbOldPassword.Visible = true;
                    lbOldPassword.Text = isEnglish ? "The old password is incorrect！" : "与登录密码不匹配！";
                    isSrcc = false;
                }

            //验证老密码与新密码是否相同
            if (txtOldPassword.Text == txtNewPassword.Text)
            {
                lbNewPassword.Visible = false;
                lbNewPassword.Text = isEnglish ? "The new password and the old is same" : "新密码和老密码不能相同！";
                isSrcc = false;
            }

            if (txtNewPassword.Text.IsNullOrEmpty())
            {
                dxErrorProvider1.SetError(txtNewPassword, NativeLanguageService.GetText(this, "ValidatePassword"));
                lbNewPassword.Visible = true;
                lbNewPassword.Text = isEnglish ? "New Password is empty" : "新密码不能为空！";
                isSrcc = false;
            }

            if (txtConfirmPassword.Text.IsNullOrEmpty())
            {
                dxErrorProvider1.SetError(txtConfirmPassword, NativeLanguageService.GetText(this, "ValidatePassword"));
                lbConfirmPassword.Visible = true;
                lbConfirmPassword.Text = isEnglish ? "New confirmed password is empty" : "确认密码不能为空！";
                isSrcc = false;
            }

            if (txtNewPassword.Text.IsNullOrEmpty() == false && txtConfirmPassword.Text.IsNullOrEmpty() == false)
            {
                if (txtNewPassword.Text != txtConfirmPassword.Text)
                {
                    dxErrorProvider1.SetError(txtConfirmPassword, NativeLanguageService.GetText(this, "ValidatePasswordInconsistent"));
                    lbConfirmPassword.Visible = true;
                    lbConfirmPassword.Text = isEnglish ? "The new password and the new confirmed password must be same." :
                        "新密码与确认密码不相同！";
                    isSrcc = false;
                }
                else
                {
                    if (txtNewPassword.Text.Length < 6)
                    {
                        dxErrorProvider1.SetError(txtNewPassword, NativeLanguageService.GetText(this, "ValidatePasswordLength"));
                        lbNewPassword.Visible = true;
                        lbNewPassword.Text = isEnglish ? "Password length is less than 6" : "新密码长度不能小于6位！";
                        isSrcc = false;
                    }

                    else if (txtConfirmPassword.Text.Length < 6)
                    {
                        dxErrorProvider1.SetError(txtConfirmPassword, NativeLanguageService.GetText(this, "ValidatePasswordLength"));
                        lbConfirmPassword.Visible = true;
                        lbConfirmPassword.Text = isEnglish ? "Password length is less than 6" : "确认密码长度不能小于6位！";
                        isSrcc = false;
                    }
                }
            }

            if (ArgumentHelper.HaveSimpleCode(txtNewPassword.Text, 3))
            {
                dxErrorProvider1.SetError(txtNewPassword, NativeLanguageService.GetText(this, "Password is too simple."));
                lbNewPassword.Visible = true;
                lbNewPassword.Text = isEnglish ? "Password is too simple." : "新密码太简单！";
                isSrcc = false;
            }


            return isSrcc;
        }

        #endregion

        private void btnSavemail_Click(object sender, EventArgs e)
        {
            if (!ValidateEmailPassword()) return;
            try
            {
                UserDetailInfo info = this.bindingSource1.DataSource as UserDetailInfo;
                string newPassword = this.txtOldEmailPassword.Text;

                UserService.ChangedUserMailPassword(info.ID, newPassword);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), NativeLanguageService.GetText(this, "SetPasswordSuccessfully"));
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }

        }

        private bool ValidateEmailPassword()
        {
            //if (string.IsNullOrEmpty(this.txtNewEmailPassword.Text) && string.IsNullOrEmpty(this.txtNewPasswordAgain.Text))
            //    return true;
            //bool result = true;
            //if (string.IsNullOrEmpty(this.txtNewEmailPassword.Text) || string.IsNullOrEmpty(this.txtNewPasswordAgain.Text))
            //{
            //    result = false;
            //}
            //if (!this.txtNewEmailPassword.Text.Equals(this.txtNewPasswordAgain.Text))
            //{
            //    result = false;
            //}
            //if (!result)
            //{
            //    this.lblTip.Visible = true;
            //    this.lblTip.Text = LocalData.IsEnglish ? "Password isn't correct,please check and enter again." : "密码不正确，请重新输入。";
            //    return false;

            //}
            //if (result)
            //{
            //    this.lblTip.Text = string.Empty;
            //    this.lblTip.Visible = false;
            //}
            return true;
        }

        #endregion

        private void btnTest_Click(object sender, EventArgs e)
        {
            string emailAdderess = txtEmail.Text;
            if (string.IsNullOrEmpty(emailAdderess))
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), lblEmailAddressBlankTip.Text);
                return;
            }
            try
            {
                ICP.Message.ServiceInterface.Message dataSource = new ICP.Message.ServiceInterface.Message();

                dataSource.BodyFormat = BodyFormat.olFormatPlain;
                dataSource.Body = "ICP Test Email Sent Successfully";
                dataSource.SendTo = emailAdderess;
                dataSource.SendFrom = LocalData.UserInfo.EmailAddress;
                dataSource.Priority = MessagePriority.High;
                dataSource.State = MessageState.Sending;
                dataSource.Type = MessageType.Email;
                dataSource.Way = MessageWay.Send;
                dataSource.CreateBy = LocalData.UserInfo.LoginID;

                ClientMessageService.Send(dataSource);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "The test email has been send,please check." : "测试邮件已发送，请查收。");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }

        }

        private void btSaveSignature_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbEmail.Text))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? @"Please select the Email you need to set！" : @"请选择需要设置的邮箱地址！",
                                              LocalData.IsEnglish ? "Tip" : "提示");
                return;
            }
            UserMailAccountList account = accountList.Find(r => r.Email == cmbEmail.Text);
            if (account == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? @"Please configure your email address first！" : @"请先配置好您的邮箱地址！",
                                                             LocalData.IsEnglish ? "Tip" : "提示");
                return;
            }

            UserService.SaveEmailAccountSignature(account.ID, cmbType.SelectedIndex, txtSignature.Text, LocalData.UserInfo.LoginID);
            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Email Signature Successfully" : "保存邮件签名成功！");
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            txtSignature.Text = string.Empty;
        }

        private void cmbEmail_EditValueChanged(object sender, EventArgs e)
        {
            UserMailAccountList account = accountList.Find(r => r.Email == cmbEmail.Text);
            if (account == null)
                return;

            List<EmailAccountSignature> list = UserService.GetEmailAccountSignature(account.ID,string.Empty);
            if (list.Find(r => r.Type == 0) != null)
            {
                EmailAccountSignature sig = list.Find(r => r.Type == 0);
                txtSignature.Text = sig.Signature;
            }

        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



    }
}
