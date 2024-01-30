﻿using System;
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
    public class Cost_DetailWorkItem:WorkItem
    {
        ReportMainSpace reportMainWorkSpace;
        Cost_Detail_SearchPart _SearchPart;
        ReportViewBase _ListPart;
        /// <summary>
        /// 报表参数
        /// </summary>
        List<ReportParameter> _paramList;

        internal void Show(IWorkspace workspace)
        {
            if (this.reportMainWorkSpace == null)
            {
                this.reportMainWorkSpace = this.Items.AddNew<ReportMainSpace>((Guid.NewGuid()).ToString());

                this.reportMainWorkSpace.Disposed += new EventHandler(reportMainWorkSpace_Disposed);

                this.reportMainWorkSpace.Dock = DockStyle.Fill;
                SmartPartInfo info = new SmartPartInfo();
                info.Title = Utility.GetValueString("个人帐单", "成本分析-个人帐单");
                    //"成本分析-个人帐单";

                if (this._ListPart == null)
                {
                    this._ListPart = this.Items.AddNew<ReportViewBase>();
                    this._ListPart.Dock = DockStyle.Fill;
                }
                this.reportMainWorkSpace.ListSpace.Show(this._ListPart);


                if (this._SearchPart == null)
                {
                    this._SearchPart = this.Items.AddNew<Cost_Detail_SearchPart>();
                    this._SearchPart.Dock = DockStyle.Fill;
                    this._SearchPart.SearchResult += new EventHandler(_SearchPart_SearchResult);
                }

                this.reportMainWorkSpace.SearchSpace.Show(this._SearchPart);
                workspace.Show(this.reportMainWorkSpace, info);
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
            this._paramList.Add(new ReportParameter("Beginning_Date", this._SearchPart.BeginTime.ToShortDateString()));
            this._paramList.Add(new ReportParameter("Ending_Date", this._SearchPart.EndTime.ToShortDateString()));
            this._paramList.Add(new ReportParameter("StructNodeId", this._SearchPart.StructNodeId.ToString()));
            this._paramList.Add(new ReportParameter("CostItemID", this._SearchPart.CostItemID.ToString()));
            this._paramList.Add(new ReportParameter("Peried", this._SearchPart.Peried));
            this._paramList.Add(new ReportParameter("StructureNodeName", this._SearchPart.StructureNodeName));
            //this._paramList.Add(new ReportParameter("ConditionRemark", this._SearchPart.ConditionRemark));
            this._paramList.Add(new ReportParameter("EmployeeID", this._SearchPart.EmployeeIDs.ToString()));
            this._paramList.Add(new ReportParameter("IsEnglish", Utility.IsEnglish ? "1" : "0"));
            this._paramList.Add(new ReportParameter("IsLikeStructNode", "1"));
            

            
            //显示报表的用法



            this._ListPart.ReportName = "RPT_ALLCostDetail";

            this._ListPart.ParamList = this._paramList;
            this._ListPart.DisplayData();
        }
    }
}
