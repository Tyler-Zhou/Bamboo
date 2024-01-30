using System;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.OA.ServiceInterface.Client;
using ICP.OA.UI.Service;
using ICP.Framework.ClientComponents.UIManagement;
using ICP.OA.ServiceInterface.DataObjects;
using ICP.OA.ServiceInterface;
using ICP.OA.UI.Contact;
using ICP.OA.UI.UserInformation;
//using ICP.OA.UI.Contact;IClientMessageService
namespace ICP.OA.UI
{
    public class OAModuleInit : Microsoft.Practices.CompositeUI.ModuleInit
    {
        #region init
        Microsoft.Practices.CompositeUI.WorkItem _rootWorkItem;
        IDataUIManageService _uiService;
        IDataFinderFactory _datafinderFactory;

        public OAModuleInit([ServiceDependency]WorkItem rootWorkItem
            , [ServiceDependency]IDataUIManageService uiService
            , [ServiceDependency]IDataFinderFactory datafinderFactory
            )
        {
            _rootWorkItem = rootWorkItem;
            _uiService = uiService;
            _datafinderFactory = datafinderFactory;
        }

        public override void AddServices()
        {

            _rootWorkItem.Services.AddNew<FaxClientService, IFaxClientService>();
            _rootWorkItem.Services.AddNew<DocumentClientService, IDocumentClientService>();
            _rootWorkItem.Services.AddNew<BulletinNotifyService, IBulletinNotifyService>();
            base.AddServices();
        }

        #endregion

        #region 打开邮件列表管理
        [CommandHandler(FunctionConstants.OA_EMAILLIST)]
        public void Open_OA_EMAILLIST(object sender, EventArgs e)
        {
            int theradID = 0;
            if (string.IsNullOrEmpty(LocalData.UserInfo.EmailAddress))
            {
                string tip = LocalData.IsEnglish ? "Please configure your email account first." : "系统检测到您还未配置邮箱账号，请先配置。";
                LocalCommonServices.ErrorTrace.SetErrorInfo(null, tip);
                return;
            }
            theradID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm();

            EmailManage.EMailWorkitem emailWorkitem = _rootWorkItem.WorkItems.AddNew<EmailManage.EMailWorkitem>();
            emailWorkitem.Run();

            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 打开文档列表管理
        [CommandHandler(FunctionConstants.OA_DOCUMENTLIST)]
        public void Open_OA_DOCUMENTLIST(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm();

            Document.DocumentWorkitem docWorkitem = _rootWorkItem.WorkItems.AddNew<Document.DocumentWorkitem>();
            docWorkitem.Run();

            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 打开商务信息
        [CommandHandler(FunctionConstants.OA_BUSINESSLIST)]
        public void OA_BUSINESSLIST(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm();

            BusinessDocumentWorkitem docWorkitem = _rootWorkItem.WorkItems.AddNew<BusinessDocumentWorkitem>();
            docWorkitem.Foldertype = FolderType.Business;
            docWorkitem.Run();

            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 打开订舱统计
        [CommandHandler(FunctionConstants.OA_BOOKINGLIST)]
        public void OA_BOOKINGLIST(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm();

            BusinessDocumentWorkitem docWorkitem = _rootWorkItem.WorkItems.AddNew<BusinessDocumentWorkitem>();
            docWorkitem.Foldertype = FolderType.Booking;
            docWorkitem.Run();

            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 公告列表
        [CommandHandler(FunctionConstants.OA_BULLETINLIST)]
        public void OA_BULLETINLIST(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm();

            ICP.OA.UI.Bulletin.BulletinWorkitem bulletinWorkitem =
                _rootWorkItem.WorkItems.AddNew<ICP.OA.UI.Bulletin.BulletinWorkitem>();
            bulletinWorkitem.Run();

            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion 员工信息列表
        # region
        /// <summary>
        /// 员工信息列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(FunctionConstants.OA_UserInfoList)]
        public void OA_UserInfoList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm("Loading...");
            UserInfoWorkItem contactWorkItem = _rootWorkItem.WorkItems.AddNew<UserInfoWorkItem>();
            contactWorkItem.Run();

            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 通讯录
        [CommandHandler(FunctionConstants.OA_ADDRESSLIST)]
        public void OA_ADDRESSLIST(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm();

            ContactWorkItem contactWorkItem = _rootWorkItem.WorkItems.AddNew<ContactWorkItem>();
            contactWorkItem.Run();

            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion


        #region 邮件中心日志
        [CommandHandler(FunctionConstants.OA_MAILCENTERMEMO)]
        public void OA_MailCenterMemo(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm();

            MailCenterOperationWorkItem mailCenterWorkItem = _rootWorkItem.WorkItems.AddNew<MailCenterOperationWorkItem>();
            mailCenterWorkItem.Run();

            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion
    }
}
