using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.WinForms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;

using Microsoft.Reporting.WinForms;

namespace LongWin.DataWarehouseReport.WinUI
{
    public class JobInformationWorkItem:WorkItem
    {
        ReportMainSpace reportMainWorkSpace;
        JobInformation_SearchPart _SearchPart;
        ReportViewBase _ListPart;
        /// <summary>
        /// 报表参数
        /// </summary>
        List<ReportParameter> _paramList;

        internal void Show(IWorkspace workspace)
        {
            if (this.reportMainWorkSpace == null)
            {
                this.reportMainWorkSpace = this.Items.AddNew<ReportMainSpace>((new Guid()).ToString());
                this.reportMainWorkSpace.Disposed += new EventHandler(reportMainWorkSpace_Disposed);
                this.reportMainWorkSpace.Dock = DockStyle.Fill;
                SmartPartInfo info = new SmartPartInfo();
                info.Title = Utility.GetValueString("业务信息详细", "业务信息详细");

                workspace.Show(this.reportMainWorkSpace, info);
                //内容显示面板
                if (this._ListPart == null)
                {
                    this._ListPart = this.Items.AddNew<ReportViewBase>();
                    this._ListPart.Dock = DockStyle.Fill;
                }
                this.reportMainWorkSpace.ListSpace.Show(this._ListPart);

                //查询面板
                if (this._SearchPart == null)
                {
                    this._SearchPart = this.Items.AddNew<JobInformation_SearchPart>();
                    this._SearchPart.Dock = DockStyle.Fill;
                    this._SearchPart.SearchResult += new EventHandler(_SearchPart_SearchResult);
                }
                this.reportMainWorkSpace.SearchSpace.Show(this._SearchPart);

            }
            else { workspace.Show(this.reportMainWorkSpace); }
        }

        void reportMainWorkSpace_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _SearchPart_SearchResult(object sender, EventArgs e)
        {
            this.reportMainWorkSpace.Cursor = Cursors.WaitCursor;
            if (this._paramList == null)
            {
                this._paramList = new List<ReportParameter>();
            }
            //如果是统计表
            this._ListPart.ReportName = "RPT_ALLJobInformation_Detail";

            this._paramList.Clear();
            this._paramList.Add(new ReportParameter("StructNodeId", this._SearchPart.StructNodeId.ToString()));
            this._paramList.Add(new ReportParameter("Beginning_Date", this._SearchPart.BeginTime.ToString("yyyy-MM-dd")));
            this._paramList.Add(new ReportParameter("Ending_Date", this._SearchPart.EndTime.ToString("yyyy-MM-dd")));
            this._paramList.Add(new ReportParameter("ProfitType", this._SearchPart.ProfitFlag.ToString()));
            this._paramList.Add(new ReportParameter("ConsignerSet", this._SearchPart.CustomerIds));
            this._paramList.Add(new ReportParameter("SalesSet", this._SearchPart.EmployeeIDs));
            this._paramList.Add(new ReportParameter("SeekField", this._SearchPart.SearchField));
            this._paramList.Add(new ReportParameter("SeekValue",this._SearchPart.SearchValue));
            this._paramList.Add(new ReportParameter("IsDepartment", this._SearchPart.IsViewShipper.ToString()));
            this._paramList.Add(new ReportParameter("IsEnglish", Utility.IsEnglish ? "1" : "0"));

            
            this._paramList.Add(new ReportParameter("JobType", this._SearchPart.BusinessTypes));
            this._paramList.Add(new ReportParameter("DateType", this._SearchPart.DateType.ToString()));
            
            //显示报表的用法
            this._ListPart.ParamList = this._paramList;
            this._ListPart.DisplayData();
        }
    }
}
