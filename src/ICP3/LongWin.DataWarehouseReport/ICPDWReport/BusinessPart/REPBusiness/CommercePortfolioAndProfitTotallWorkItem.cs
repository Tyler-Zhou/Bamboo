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
    public class CommercePortfolioAndProfitTotallWorkItem : WorkItem
    {
        ReportMainSpace reportMainWorkSpace;
        TotalForBox_Search _SearchPart;
        ReportViewBase _ListPart;
        /// <summary>
        /// 报表参数
        /// </summary>     
        List<ReportParameter> _paramList;

        internal void GetService()
        {
            if (this._SearchPart == null)
            {
                this._SearchPart = this.Items.AddNew<TotalForBox_Search>();
                this._SearchPart.IsCommerce = true;
                this._SearchPart.Dock = DockStyle.Fill;
                this._SearchPart.SearchResult += new EventHandler(_SearchPart_SearchResult);
            }
        }

        internal void Show(IWorkspace workspace)
        {
            if (this.reportMainWorkSpace == null)
            {
                this.reportMainWorkSpace = this.Items.AddNew<ReportMainSpace>((new Guid()).ToString());
                this.reportMainWorkSpace.Disposed += new EventHandler(reportMainWorkSpace_Disposed);
                this.reportMainWorkSpace.Dock = DockStyle.Fill;
                SmartPartInfo info = new SmartPartInfo();
                info.Title = Utility.GetValueString("商务箱量统计表", "商务箱量统计表");



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
                    this._SearchPart = this.Items.AddNew<TotalForBox_Search>();
                    this._SearchPart.IsCommerce = true;
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

            this._paramList.Clear();
            this._paramList.Add(new ReportParameter("ETD_Beginning_Date", this._SearchPart.BeginTime.ToString("yyyy-MM-dd")));
            this._paramList.Add(new ReportParameter("ETD_Ending_Date", this._SearchPart.EndTime.ToString("yyyy-MM-dd")));
            this._paramList.Add(new ReportParameter("DateType", this._SearchPart.DateType.ToString()));

            this._paramList.Add(new ReportParameter("GroupString", this._SearchPart.GroupBy));
            this._paramList.Add(new ReportParameter("GroupCount", this._SearchPart.GroupCount.ToString()));
            this._paramList.Add(new ReportParameter("Group3", this._SearchPart.Group3));
            this._paramList.Add(new ReportParameter("JobPlace", this._SearchPart.JobPlace));
            this._paramList.Add(new ReportParameter("CompanyName", this._SearchPart.CompanyCName));
            this._paramList.Add(new ReportParameter("CompanyAddress", this._SearchPart.CompanyEAddress));
            this._paramList.Add(new ReportParameter("TelOrFax", this._SearchPart.TelOrFax));
            this._paramList.Add(new ReportParameter("ConditionRemark", this._SearchPart.ConditionRemark));
            this._paramList.Add(new ReportParameter("IsEnglish", Utility.IsEnglish ? "1" : "0"));
            this._paramList.Add(new ReportParameter("Condition", this._SearchPart.XMLCondition));
            this._paramList.Add(new ReportParameter("JobTypeBit", this._SearchPart.JobTypeBit));
            this._paramList.Add(new ReportParameter("IsCommerceReport", "1"));

            this._ListPart.ReportName = "RPT_ALLPortfolioAndProfitForGeneral_Total";

            //显示报表的用法
            this._ListPart.ParamList = this._paramList;
            this._ListPart.DisplayData();
        }
    }
}
