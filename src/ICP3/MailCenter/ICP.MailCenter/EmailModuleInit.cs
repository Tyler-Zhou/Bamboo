using System;
using Microsoft.Practices.CompositeUI;
using ICP.MailCenter.ServiceInterface;
using Microsoft.Practices.CompositeUI.Commands;
using System.Windows.Forms;




namespace ICP.MailCenter.UI
{
    /// <summary>
    /// 邮件中心模块初始化类
    /// </summary>
    public class EmailModuleInit : ModuleInit
    {
        /// <summary>
        /// 根WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem RootWorkItem { get; set; }
        /// <summary>
        /// EmailWorkItem在容器中的ID
        /// </summary>
        string emailWorkItemId = "EmailWorkItem";
        /// <summary>
        /// 注入客户端服务
        /// </summary>
        public override void AddServices()
        {
            RootWorkItem.Services.AddNew<OutlookService, IOutLookService>();
            RootWorkItem.Services.AddNew<MailCenterTemplateService, IMailCenterTemplateService>();
            RootWorkItem.Services.AddNew<EmailTemplateGetter, IEmailTemplateGetter>();
        }

        /// <summary>
        /// 通过菜单打开邮件中心
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandNames.EmailList)]
        public void Email_EmailList(object sender, EventArgs e)
        {
            try
            {
                EmailWorkItem emailWorkItem = RootWorkItem.WorkItems.AddNew<EmailWorkItem>(emailWorkItemId);
                emailWorkItem.Run();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("MailCenter:" + ex.Message);
            }
        }



    }
}
