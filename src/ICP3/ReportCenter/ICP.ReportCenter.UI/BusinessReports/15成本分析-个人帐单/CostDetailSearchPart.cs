using System;
using System.Collections.Generic;
using System.ComponentModel;

using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;

namespace ICP.ReportCenter.UI.BusinessReports
{   
    /// <summary>
    /// 成本分析-个人账单
    /// </summary>
    [ToolboxItem(false)]
    public partial class CostDetailSearchPart : ReportBaseSearchPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public ReportCenterHelper ReportCenterHelper
        {
            get
            {
                return ClientHelper.Get<ReportCenterHelper, ReportCenterHelper>();
            }
        }

        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }

        #endregion

        #region  init

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
            List<DataDictionaryList> dataList = TransportFoundationService.GetDataDictionaryList(string.Empty,string.Empty,DataDictionaryType.MovieProjects,true,0);
            this.cmbMovieProjects.Properties.BeginUpdate();
            this.cmbMovieProjects.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ?  "All":"全部" , ""));
            foreach (DataDictionaryList item in dataList)
            {
                this.cmbMovieProjects.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish?item.EName:item.CName, item.ID));
            }
            this.cmbMovieProjects.Properties.EndUpdate();
            this.cmbMovieProjects.SelectedIndex = 0;
        }


        public CostDetailSearchPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.OnSearched = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
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
            DateTime from=this.operationDatePart1.FromDate;
            DateTime to = this.operationDatePart1.ToDate;

            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("Beginning_Date", operationDatePart1.FromDate.ToString("yyyy-MM-dd")));
            paramList.Add(new ReportParameter("Ending_Date", operationDatePart1.ToDate.ToString("yyyy-MM-dd")));
            paramList.Add(new ReportParameter("StructureNodeName", treeDepartment.Text));
            paramList.Add(new ReportParameter("IsEnglish", LocalData.IsEnglish ? "1" : "0"));
            paramList.Add(new ReportParameter("StructNodeId", treeDepartment.EditValue.ToString()));
            paramList.Add(new ReportParameter("MovieProjectID", cmbMovieProjects.EditValue.ToString()));
            paramList.Add(new ReportParameter("CostItemID", (treeCostItem.EditValue == ReportCenterHelper.TopCostItem.ID ? Guid.Empty : treeCostItem.EditValue).ToString()));
            paramList.Add(new ReportParameter("Peried", operationDatePart1.FromDate.ToShortDateString() + "-" + operationDatePart1.ToDate.ToShortDateString()));
            paramList.Add(new ReportParameter("EmployeeID", this.txtSales.Tag.TagToSplitString(",")));
            paramList.Add(new ReportParameter("IsLikeStructNode", "1"));

            ReportData rd = new ReportData { Parameters = paramList, ReportName = "RPT_ALLCostDetail" };
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
