using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.ReportCenter.UI;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.OA.UI.Contact
{
    /// <summary>
    /// 通讯录工作区域
    /// </summary>
    public class ContactWorkItem : WorkItem
    {
        #region services
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region 常量
        public ReportViewBase reportViewBase = null;

        #endregion

        #region 方法
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (reportViewBase != null)
                {
                    this.Items.Remove(reportViewBase);
                    reportViewBase = null;
                }
                Workitem = null;
            }
            base.Dispose(disposing);
        }
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        public void Show()
        {
            ContactMainWorkSpace contactMainWorkSpace = this.SmartParts.Get<ContactMainWorkSpace>("ContactMainWorkSpace");
            if (contactMainWorkSpace == null)
            {
                contactMainWorkSpace = this.SmartParts.AddNew<ContactMainWorkSpace>("ContactMainWorkSpace");

                reportViewBase = this.SmartParts.AddNew<ReportViewBase>();
                IWorkspace listViewWorkSpace = (IWorkspace)this.Workspaces[ContactWorkSpaceConstants.ReportWorkspace];
                listViewWorkSpace.Show(reportViewBase);

                ContactSearchPart contactSearchPart = this.SmartParts.AddNew<ContactSearchPart>();
                IWorkspace searchViewWorkspce = (IWorkspace)this.Workspaces[ContactWorkSpaceConstants.SearchWorkspace];
                searchViewWorkspce.Show(contactSearchPart);


                contactSearchPart.OnSearched += delegate(object sender, object results)
                {
                    OnSearchPartSearched(reportViewBase, results);
                };

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Address List" : "通讯录";
                mainWorkspace.Show(contactMainWorkSpace, smartPartInfo);

                ////初始化部门列表
                ////contactSearchPart.InitControl();
                ////初始化通讯录列表
                //object obj = contactSearchPart.GetData();
                //OnSearchPartSearched(reportViewBase, obj);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(contactMainWorkSpace);
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
                else
                {
                    reportViewBase.ReportName = rd.ReportName;
                    reportViewBase.ParamList = rd.Parameters;
                    if (string.IsNullOrEmpty(rd.ServiceReportPath))
                        reportViewBase.DisplayData();
                    else
                        reportViewBase.DisplayData(rd.ServiceReportPath);
                }
            }
        }

        #endregion
    }


    public partial class ContactWorkSpaceConstants
    {
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ReportWorkspace = "ReportWorkspace";
        public const string MainViewWorkspce = "MainViewWorkspce";
    }
}
