using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Reporting.WinForms;
using System;

namespace ICP.ReportCenter.UI
{
    public class ReportWorkitem<SearchPart> : WorkItem where SearchPart : ReportBaseSearchPart
    {

        public string Titel { get; set; }

        #region Show

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            ReportMainSpace reportMainSpace = this.SmartParts.Get<ReportMainSpace>("ReportMainSpace");
            if (reportMainSpace == null)
            {
                reportMainSpace = this.SmartParts.AddNew<ReportMainSpace>("ReportMainSpace");
                #region AddPart

                ReportViewBase reportViewBase = this.SmartParts.AddNew<ReportViewBase>();
                IWorkspace reportWorkspace = (IWorkspace)this.Workspaces[ReportWorkSpaceConstants.ReportWorkspace];
                reportWorkspace.Show(reportViewBase);

                SearchPart searchPart = this.SmartParts.AddNew<SearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[ReportWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);

                #endregion

                #region 定义面板连接

                searchPart.OnSearched += delegate(object sender, object results)
                {
                     OnSearchPartSearched(reportViewBase, results);
                };

                reportViewBase.Drillthrough += delegate(object drillthroughSender, DrillthroughEventArgs e)
                {
                    OnReportViewBaseDrillthrough(searchPart, e);
                   
                };

                #endregion

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = Titel;
                mainWorkspace.Show(reportMainSpace, smartPartInfo);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(reportMainSpace);
            }
        }

        private void OnReportViewBaseDrillthrough(SearchPart searchPart, DrillthroughEventArgs e)
        {
            if ((e.Report is Microsoft.Reporting.WinForms.LocalReport) == false) return;

            Microsoft.Reporting.WinForms.LocalReport localReport = (Microsoft.Reporting.WinForms.LocalReport)e.Report;
            if (localReport == null) { e.Cancel = true; return; }

            ReportData rd = searchPart.GetDrillthroughData(localReport.ReportEmbeddedResource, localReport.OriginalParametersToDrillthrough);
            if (rd == null) { e.Cancel = true; return; }

            if (rd.DataSource != null && rd.DataSource.Count > 0)
            {
                localReport.DataSources.Clear();
                foreach (var item in rd.DataSource)
                {
                    localReport.DataSources.Add(item);
                }
            }
            if (rd.Parameters != null && rd.Parameters.Count > 0)
            {
                localReport.SetParameters(rd.Parameters);
            }
            localReport.Refresh();
        }

        public void OnSearchPartSearched(ReportViewBase reportViewBase, object results)
        {
            ReportData rd = results as ReportData;
            if (rd != null)
            {
                if (rd.IsLocalReport)//本地报表
                {
                    reportViewBase.DisplayLocalReport(rd.ReportName, rd.DataSource, rd.Parameters,rd.SubDataSource);
                }
                else
                {
                    reportViewBase.ReportName = rd.ReportName;
                    reportViewBase.ParamList = rd.Parameters;
                    if(string.IsNullOrEmpty(rd.ServiceReportPath))
                        reportViewBase.DisplayData();
                    else
                        reportViewBase.DisplayData(rd.ServiceReportPath);
                }

                if (rd.CustomerID!=null&&rd.CustomerID!=Guid.Empty)
                {
                    reportViewBase.CustomerID = (Guid)rd.CustomerID;
                }
            }
        }

        #endregion
    }

}
