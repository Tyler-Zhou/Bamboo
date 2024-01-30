using ICP.Framework.CommonLibrary.Client;
using ICP.ReportCenter.UI;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;


namespace ICP.OA.UI.Contact
{
    /// <summary>
    /// 通讯录工作区域
    /// </summary>
    public class MailCenterOperationWorkItem : WorkItem
    {
        #region services
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region 常量
        public ReportViewBase reportViewBase = null;

        #endregion

        #region 方法
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        public void Show()
        {
            MailCenterMainWorkSpace mailCenterMainWorkSpace = this.SmartParts.Get<MailCenterMainWorkSpace>("MailCenterMainWorkSpace");
            if (mailCenterMainWorkSpace == null)
            {
                mailCenterMainWorkSpace = this.SmartParts.AddNew<MailCenterMainWorkSpace>("MailCenterMainWorkSpace");

                reportViewBase = this.SmartParts.AddNew<ReportViewBase>();
                IWorkspace listViewWorkSpace = (IWorkspace)this.Workspaces[ContactWorkSpaceConstants.MailCenterReportWorkspace];
                listViewWorkSpace.Show(reportViewBase);

                MailCenterSearchPart mailCenterSearchPart = this.SmartParts.AddNew<MailCenterSearchPart>();
                IWorkspace searchViewWorkspce = (IWorkspace)this.Workspaces[ContactWorkSpaceConstants.MailCenterSearchWorkspace];
                searchViewWorkspce.Show(mailCenterSearchPart);


                mailCenterSearchPart.OnSearched += delegate(object sender, object results)
                {
                    OnSearchPartSearched(reportViewBase, results);
                };

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "MailCenter Memo" : "邮件中心操作日志";
                mainWorkspace.Show(mailCenterMainWorkSpace, smartPartInfo);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(mailCenterMainWorkSpace);
            }
        }

        public void OnSearchPartSearched(ReportViewBase reportViewBase, object results)
        {
            ReportData rd = results as ReportData;
            if (rd != null)
            {
                //本地报表
                if (rd.IsLocalReport)
                {
                    reportViewBase.DisplayLocalReport(rd.ReportName, rd.DataSource, rd.Parameters, rd.SubDataSource);
                }
            }
        }

        #endregion
    }


    public partial class ContactWorkSpaceConstants
    {
        public const string MailCenterSearchWorkspace = "MailCenterSearchWorkspace";
        public const string MailCenterReportWorkspace = "MailCenterReportWorkspace";
        public const string MailCenterMainViewWorkspce = "MailCenterMainViewWorkspce";
    }
}
