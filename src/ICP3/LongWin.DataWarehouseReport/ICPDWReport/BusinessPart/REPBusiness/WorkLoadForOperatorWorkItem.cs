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
    /// <summary>
    /// 操作工作量统计
    /// </summary>
    public class WorkLoadForOperatorWorkItem:WorkItem
    {
        ReportMainSpace reportMainWorkSpace;
        WorkLoadForOperator_SearchPart _SearchPart;
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
                info.Title = Utility.GetValueString("操作工作量统计", "操作工作量统计"); 

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
                    this._SearchPart = this.Items.AddNew<WorkLoadForOperator_SearchPart>();
                    this._SearchPart.RepBaseDataService = REPModuleInit.RepService;
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

            ////如果是公司＋员工
            //if (this._SearchPart.GroupBy == "操作员,公司")
            //{
            //    this._ListPart.ReportName = "RPT_WorkLoadForOperator_Detail";
            //}
            //else
            //{
            //    this._ListPart.ReportName = "RPT_WorkLoadForOperator_Total";
            //}

            this._ListPart.ReportName = "RPT_WorkLoadForOperatorGroupByALL_Total";

            this._paramList.Clear();
            this._paramList.Add(new ReportParameter("StructNodeId", this._SearchPart.StructNodeId.ToString()));
            this._paramList.Add(new ReportParameter("ETD_Beginning_Date", this._SearchPart.BeginTime.ToString("yyyy-MM-dd")));
            this._paramList.Add(new ReportParameter("ETD_Ending_Date", this._SearchPart.EndTime.ToString("yyyy-MM-dd")));
            this._paramList.Add(new ReportParameter("UserState", this._SearchPart.GetUserState));

            string[] groupBy = this._SearchPart.GroupBy.Split(new string[] { "," }, StringSplitOptions.None);
            
            string groupBy1="";


            if (groupBy.Length > 0)
            {
                groupBy1 = groupBy[0];
                if (groupBy1 == Utility.GetValueString("业务发生地", "业务发生地"))
                    groupBy1 = "CompanyName";
                else if (groupBy1 == Utility.GetValueString("操作", "操作"))
                    groupBy1 = "UserName";
                else if (groupBy1 == Utility.GetValueString("业务类型", "业务类型"))
                    groupBy1 = "JobTypeName";
                else if (groupBy1 == Utility.GetValueString("航线", "航线"))
                    groupBy1 = "ShippingLineName";
                else
                    groupBy1 = "CompanyName";

            }

            string groupBy2 = "";
          
            if (groupBy.Length>1)
            {
                groupBy2 = groupBy[1];
                if (groupBy2 == Utility.GetValueString("业务发生地", "业务发生地"))
                    groupBy2 = "CompanyName";
                else if (groupBy2 == Utility.GetValueString("操作", "操作"))
                    groupBy2 = "UserName";
                else if (groupBy2 == Utility.GetValueString("业务类型", "业务类型"))
                    groupBy2 = "JobTypeName";
                else if (groupBy2 == Utility.GetValueString("航线", "航线"))
                    groupBy2 = "ShippingLineName";
                else
                    groupBy2 = "CompanyName";
            }
                 
            string groupBy3 = "";
            if (groupBy.Length > 2)
            {
                groupBy3 = groupBy[2];
                if (groupBy3 == Utility.GetValueString("业务发生地", "业务发生地"))
                    groupBy3 = "CompanyName";
                else if (groupBy3 == Utility.GetValueString("操作", "操作"))
                    groupBy3 = "UserName";
                else if (groupBy3 == Utility.GetValueString("业务类型", "业务类型"))
                    groupBy3 = "JobTypeName";
                else if (groupBy3 == Utility.GetValueString("航线", "航线"))
                    groupBy3 = "ShippingLineName";
                else
                    groupBy3 = "CompanyName";
            }

            this._paramList.Add(new ReportParameter("GroupBy1", groupBy1));
            this._paramList.Add(new ReportParameter("GroupBy2", groupBy2));
            this._paramList.Add(new ReportParameter("GroupBy3", groupBy3));

            this._paramList.Add(new ReportParameter("Period", this._SearchPart.Peried));


            //显示报表的用法
            this._ListPart.ParamList = this._paramList;
            this._ListPart.DisplayData();
        }
    }

  
}
