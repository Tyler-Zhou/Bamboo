using ICP.EDI.ServiceInterface;
using Microsoft.Practices.CompositeUI;

namespace ICP.EDI.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class EDIModuleInit : ModuleInit
    {
        /// <summary>
        /// 
        /// </summary>
        WorkItem _rootWorkItem;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootWorkItem"></param>
        public EDIModuleInit([ServiceDependency]WorkItem rootWorkItem)
        {
            _rootWorkItem = rootWorkItem;
        }
        /// <summary>
        /// 
        /// </summary>
        public override void AddServices()
        {
            base.AddServices();
            _rootWorkItem.Services.AddNew<EDIClientService, IEDIClientService>();
        }


        ///// <summary>
        ///// EDI配置列表命令名
        ///// </summary>
        //[CommandHandler(EDICommandConstants.Common_EDIConfigureList)]
        //public void Open_Common_EDIConfigureList(object sender, EventArgs e)
        //{
        //    int theradID = 0;
        //    theradID = LoadingServce.ShowLoadingForm("Loading...");
        //    EDIConfigureWorkItem ediConfigureWorkItem = _rootWorkItem.WorkItems.AddNew<EDIConfigureWorkItem>();
        //    IWorkspace mainWorkspace = _rootWorkItem.Workspaces[ClientConstants.MainWorkspace];
        //    ediConfigureWorkItem.Show(mainWorkspace, "EDI配置");
        //    LoadingServce.CloseLoadingForm(theradID);
        //}
    }
}
