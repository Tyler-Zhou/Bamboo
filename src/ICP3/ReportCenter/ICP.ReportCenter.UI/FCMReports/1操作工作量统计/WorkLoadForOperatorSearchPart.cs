using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.ClientComponents.Controls;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.ClientComponents;

namespace ICP.ReportCenter.UI.FCMReports
{
    [ToolboxItem(false)]
    public partial class WorkLoadForOperatorSearchPart : ReportBaseSearchPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region  init

        public WorkLoadForOperatorSearchPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.OnSearched = null;

                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
            }
        }

        private void InitControls()
        {

            chkcmbGroupBy.Items.Clear();
            chkcmbGroupBy.Items.Add("CompanyName", LocalData.IsEnglish ? "Company" : "业务发生地", CheckState.Checked, true);
            chkcmbGroupBy.Items.Add("UserName", LocalData.IsEnglish ? "Operator" : "操作", CheckState.Checked, true);
            chkcmbGroupBy.Items.Add("JobTypeName", LocalData.IsEnglish ? "Job Type" : "业务类型", CheckState.Checked, true);
            chkcmbGroupBy.Items.Add("ShippingLineName", LocalData.IsEnglish ? "Line" : "航线", CheckState.Unchecked, true);
            chkcmbGroupBy.RefreshText();

            Utility.BulidComboboxItem<ReportUserState>(cmbUserState, 1);

        }


        #endregion

        #region event
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.btnSearch.Enabled = false;
            try
            {
                if (OnSearched != null)
                {
                    using (new CursorHelper())
                    {
                        OnSearched(this, GetData());
                    }
                }

            }
            finally
            {
                this.btnSearch.Enabled = true;
            }
        }

        #endregion

        #region ISearchPart 成员

        public override object GetData()
        {
            if (chkcmbGroupBy.CheckCount == 0)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "GroupBy must choose one." : "请至少选择一种分组方式.");
                return null;
            }

            if (chkcmbGroupBy.CheckCount > 3)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "最多可以选择三种分组方式." : "最多可以选择三种分组方式.");
                return null;
            }

            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("ETD_Beginning_Date", operationDatePart1.FromDate.ToString("yyyy-MM-dd")));
            paramList.Add(new ReportParameter("ETD_Ending_Date", operationDatePart1.ToDate.ToString("yyyy-MM-dd")));
            paramList.Add(new ReportParameter("StructNodeId", treeBoxSalesDep.GetAllEditValue.ToSplitString(",")));
            paramList.Add(new ReportParameter("UserState", cmbUserState.SelectedIndex.ToString()));
            paramList.Add(new ReportParameter("Period", operationDatePart1.FromDate.ToShortDateString() + "-" + operationDatePart1.ToDate.ToShortDateString()));

            string groupBy1 = string.Empty, groupBy2 = string.Empty, groupBy3 = string.Empty;

            string[] strgrp = chkcmbGroupBy.EditText.Split(',');

            int igrp = 1;

            if (strgrp != null)
            {
                foreach (var item in strgrp)
                {
                    if (item.ToString() == "Company" || item.ToString() == "业务发生地")
                    {
                        if (igrp == 1)
                        {
                            groupBy1 = "CompanyName";
                        }
                        if (igrp == 2)
                        {
                            groupBy2 = "CompanyName";
                        }
                        if (igrp == 3)
                        {
                            groupBy3 = "CompanyName";
                        }
                    }
                    else if (item.ToString() == "Job Type" || item.ToString() == "业务类型")
                    {
                        if (igrp == 1)
                        {
                            groupBy1 = "JobTypeName";
                        }
                        if (igrp == 2)
                        {
                            groupBy2 = "JobTypeName";
                        }
                        if (igrp == 3)
                        {
                            groupBy3 = "JobTypeName";
                        }
                    }
                    else if (item.ToString() == "Operator" || item.ToString() == "操作")
                    {
                        if (igrp == 1)
                        {
                            groupBy1 = "UserName";
                        }
                        if (igrp == 2)
                        {
                            groupBy2 = "UserName";
                        }
                        if (igrp == 3)
                        {
                            groupBy3 = "UserName";
                        }
                    }

                    else if (item.ToString() == "Line" || item.ToString() == "航线")
                    {
                        if (igrp == 1)
                        {
                            groupBy1 = "ShippingLineName";
                        }
                        if (igrp == 2)
                        {
                            groupBy2 = "ShippingLineName";
                        }
                        if (igrp == 3)
                        {
                            groupBy3 = "ShippingLineName";
                        }
                    }           
                    igrp++;
                }
            }

            paramList.Add(new ReportParameter("GroupBy1", groupBy1));
            paramList.Add(new ReportParameter("GroupBy2", groupBy2));
            paramList.Add(new ReportParameter("GroupBy3", groupBy3));

            ReportData rd = new ReportData { Parameters = paramList, ReportName = "RPT_WorkLoadForOperatorGroupByALL_Total" };
            return rd;
        }
        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion
    }
}
